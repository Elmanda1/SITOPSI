Imports MySql.Data.MySqlClient

Public Class ProfileForm
    Public Property ParentDashboard As DashboardUser

    ' --- Colors ---
    Private ReadOnly navyColor As Color = Color.FromArgb(12, 45, 72)
    Private ReadOnly lightNavyColor As Color = Color.FromArgb(28, 68, 105)
    Private ReadOnly whiteColor As Color = Color.White
    Private ReadOnly lightGreyHoverColor As Color = Color.FromArgb(236, 240, 241)
    Private ReadOnly accentColor As Color = Color.FromArgb(52, 152, 219)
    Private ReadOnly successColor As Color = Color.FromArgb(46, 204, 113)

    ' User statistics
    Private totalTests As Integer = 0
    Private lastTestDate As String = "-"
    Private accountCreatedDate As String = ""
    Private accountStatus As String = ""
    Private userRole As String = ""
    Private latestCFScore As Decimal = 0

    Private Sub ProfileForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Then
            MessageBox.Show("Silakan login terlebih dahulu!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If

        ' Load user data and statistics
        LoadUserData()
        LoadUserStatistics()

        ' Populate fields
        txtFullName.Text = LoggedFullName
        txtUsername.Text = LoggedUsername
        txtEmail.Text = LoggedEmail
        txtPhone.Text = LoggedNoHP

        ' Make username read-only
        txtUsername.Enabled = False

        ' Add handlers
        AddHandler btnSaveChanges.Click, AddressOf BtnSaveChanges_Click
        AddHandler btnBack.Click, AddressOf BtnBack_Click
        AddHandler btnSaveChanges.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnSaveChanges.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnBack.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnBack.MouseLeave, AddressOf Button_MouseLeave
    End Sub

    Private Sub BtnSaveChanges_Click(sender As Object, e As EventArgs)
        ' Validation
        If String.IsNullOrWhiteSpace(txtFullName.Text) OrElse
           String.IsNullOrWhiteSpace(txtEmail.Text) OrElse
           String.IsNullOrWhiteSpace(txtPhone.Text) Then
            MessageBox.Show("Semua field harus diisi!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "UPDATE users SET fullname = @fullname, email = @email, no_hp = @noHp WHERE user_id = @userId"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@fullname", txtFullName.Text.Trim())
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim())
                    cmd.Parameters.AddWithValue("@noHp", txtPhone.Text.Trim())
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)

                    Dim result = cmd.ExecuteNonQuery()

                    If result > 0 Then
                        ' Update global session variables
                        LoggedFullName = txtFullName.Text.Trim()
                        LoggedEmail = txtEmail.Text.Trim()
                        LoggedNoHP = txtPhone.Text.Trim()
                        
                        ' Log activity
                        Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, 'Update Profile', @detail, NOW())"
                        Using logCmd As New MySqlCommand(logQuery, conn)
                            logCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                            logCmd.Parameters.AddWithValue("@detail", "User " & LoggedUsername & " memperbarui profil")
                            logCmd.ExecuteNonQuery()
                        End Using

                        MessageBox.Show("Profil berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close() ' Close profile form and return to dashboard
                    Else
                        MessageBox.Show("Gagal memperbarui profil. Tidak ada perubahan yang disimpan.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat menyimpan profil: " & ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    
    Private Sub LoadUserData()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT u.created_at, u.status, r.name AS role_name " &
                                     "FROM users u " &
                                     "INNER JOIN roles r ON u.role_id = r.role_id " &
                                     "WHERE u.user_id = @userId"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            accountCreatedDate = Convert.ToDateTime(reader("created_at")).ToString("dd MMM yyyy HH:mm")
                            accountStatus = reader("status").ToString()
                            userRole = reader("role_name").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading user data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadUserStatistics()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' Get total tests
                Dim testQuery As String = "SELECT COUNT(*) FROM tests WHERE user_id = @userId"
                Using cmd As New MySqlCommand(testQuery, conn)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    totalTests = Convert.ToInt32(cmd.ExecuteScalar())
                End Using

                ' Get last test date and CF score
                If totalTests > 0 Then
                    Dim lastTestQuery As String = "SELECT tanggal, nilai_cf_akhir FROM tests WHERE user_id = @userId ORDER BY tanggal DESC LIMIT 1"
                    Using cmd As New MySqlCommand(lastTestQuery, conn)
                        cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                        Using reader As MySqlDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                lastTestDate = Convert.ToDateTime(reader("tanggal")).ToString("dd MMM yyyy")
                                If Not IsDBNull(reader("nilai_cf_akhir")) Then
                                    latestCFScore = Convert.ToDecimal(reader("nilai_cf_akhir"))
                                End If
                            End If
                        End Using
                    End Using
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading statistics: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub ProfileForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboard IsNot Nothing AndAlso Not ParentDashboard.IsDisposed Then
            ParentDashboard.Show()
        End If
    End Sub

    ' --- Hover Handlers ---
    Private Sub Button_MouseEnter(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        If btn.Name = "btnSaveChanges" Then
            btn.BackColor = lightNavyColor
        Else ' btnBack
            btn.BackColor = lightGreyHoverColor
        End If
    End Sub

    Private Sub Button_MouseLeave(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        If btn.Name = "btnSaveChanges" Then
            btn.BackColor = navyColor
        Else ' btnBack
            btn.BackColor = whiteColor
        End If
    End Sub
End Class