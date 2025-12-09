Imports MySql.Data.MySqlClient

Public Class ChangePassword
    Public Property ParentLogin As LoginForm
    Private isSuccess As Boolean = False

    ' --- Colors ---
    Private ReadOnly navyColor As Color = Color.FromArgb(12, 45, 72)
    Private ReadOnly lightNavyColor As Color = Color.FromArgb(28, 68, 105)
    Private ReadOnly whiteColor As Color = Color.White
    Private ReadOnly lightGreyHoverColor As Color = Color.FromArgb(236, 240, 241)

    Private Sub ChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Add hover effects
        AddHandler Button1.MouseEnter, AddressOf Button1_MouseEnter
        AddHandler Button1.MouseLeave, AddressOf Button1_MouseLeave
        AddHandler Button2.MouseEnter, AddressOf Button2_MouseEnter
        AddHandler Button2.MouseLeave, AddressOf Button2_MouseLeave
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Validasi input
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Username tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox1.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Password baru tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox2.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Verifikasi password tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox3.Focus()
            Return
        End If

        ' Validasi panjang password minimal 5 karakter
        If TextBox2.Text.Length < 5 Then
            MessageBox.Show("Password minimal 5 karakter!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox2.Focus()
            Return
        End If

        ' Validasi password cocok
        If TextBox2.Text <> TextBox3.Text Then
            MessageBox.Show("Password tidak cocok!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox3.Clear()
            TextBox3.Focus()
            Return
        End If

        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' Cek apakah username ada
                Dim checkQuery As String = "SELECT user_id, fullname FROM users WHERE username = @username"
                Dim userId As Integer = 0
                Dim fullname As String = ""

                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@username", TextBox1.Text.Trim())

                    Using reader As MySqlDataReader = checkCmd.ExecuteReader()
                        If reader.Read() Then
                            userId = Convert.ToInt32(reader("user_id"))
                            fullname = reader("fullname").ToString()
                        Else
                            MessageBox.Show("Username tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return
                        End If
                    End Using
                End Using

                ' Update password
                Dim updateQuery As String = "UPDATE users SET password = @password, updated_at = NOW() WHERE user_id = @userId"
                Using updateCmd As New MySqlCommand(updateQuery, conn)
                    updateCmd.Parameters.AddWithValue("@password", TextBox2.Text)
                    updateCmd.Parameters.AddWithValue("@userId", userId)

                    Dim result As Integer = updateCmd.ExecuteNonQuery()

                    If result > 0 Then
                        isSuccess = True ' Set flag
                        
                        ' Log activity
                        Try
                            Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, 'Change Password', @detail, NOW())"
                            Using logCmd As New MySqlCommand(logQuery, conn)
                                logCmd.Parameters.AddWithValue("@userId", userId)
                                logCmd.Parameters.AddWithValue("@detail", "User " & TextBox1.Text & " berhasil mengganti password")
                                logCmd.ExecuteNonQuery()
                            End Using
                        Catch
                            ' Silent fail for activity log
                        End Try

                        MessageBox.Show("Password berhasil diubah untuk " & fullname & "!" & vbCrLf & "Silakan login dengan password baru.", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' Kembali ke login form
                        If ParentLogin IsNot Nothing Then
                            ParentLogin.Show()
                        Else
                            Dim loginForm As New LoginForm()
                            loginForm.Show()
                        End If
                        Me.Close()
                    Else
                        MessageBox.Show("Gagal mengubah password! Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show("Error database: " & ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Kembali ke login form
        Me.Close() ' FormClosing event will handle showing the parent
    End Sub

    Private Sub ChangePassword_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Show parent login form unless registration was a success
        If Not isSuccess AndAlso ParentLogin IsNot Nothing Then
            ParentLogin.Show()
        End If
    End Sub
    
    ' --- Hover Handlers ---
    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs)
        Button1.BackColor = lightNavyColor
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs)
        Button1.BackColor = navyColor
    End Sub

    Private Sub Button2_MouseEnter(sender As Object, e As EventArgs)
        Button2.BackColor = lightGreyHoverColor
    End Sub

    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs)
        Button2.BackColor = whiteColor
    End Sub
End Class