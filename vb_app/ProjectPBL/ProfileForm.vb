Imports MySql.Data.MySqlClient

Public Class ProfileForm
    ' Reference ke dashboard untuk kembali
    Public Property ParentDashboard As DashboardUser

    Private Sub ProfileForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Cek apakah user sudah login
        If Not IsLoggedIn() Then
            MessageBox.Show("Silakan login terlebih dahulu!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Dim loginForm As New LoginForm()
            loginForm.Show()
            Me.Close()
            Return
        End If

        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(236, 240, 241)

        ' Setup UI
        SetupProfileUI()
    End Sub

    Private Sub SetupProfileUI()
        ' Clear existing controls
        Me.Controls.Clear()

        ' Header Panel
        Dim headerPanel As New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 150,
            .BackColor = Color.FromArgb(41, 128, 185)
        }
        Me.Controls.Add(headerPanel)

        ' Avatar/Profile Picture Placeholder
        Dim avatarPanel As New Panel() With {
            .Location = New Point(30, 30),
            .Size = New Size(90, 90),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle
        }
        
        Dim lblAvatar As New Label() With {
            .Text = LoggedFullName.Substring(0, 1).ToUpper(),
            .Font = New Font("Segoe UI", 36, FontStyle.Bold),
            .ForeColor = Color.FromArgb(41, 128, 185),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Dock = DockStyle.Fill
        }
        avatarPanel.Controls.Add(lblAvatar)
        headerPanel.Controls.Add(avatarPanel)

        ' User Name
        Dim lblName As New Label() With {
            .Text = LoggedFullName,
            .Font = New Font("Segoe UI", 20, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(140, 35)
        }
        headerPanel.Controls.Add(lblName)

        ' Username
        Dim lblUsername As New Label() With {
            .Text = $"@{LoggedUsername}",
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .ForeColor = Color.FromArgb(236, 240, 241),
            .AutoSize = True,
            .Location = New Point(140, 70)
        }
        headerPanel.Controls.Add(lblUsername)

        ' Role Badge
        Dim lblRole As New Label() With {
            .Text = If(IsAdmin(), "????? Admin", "?? Mahasiswa"),
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(140, 100)
        }
        headerPanel.Controls.Add(lblRole)

        ' Back Button
        Dim btnBack As New Button() With {
            .Text = "? Kembali",
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .Size = New Size(120, 40),
            .Location = New Point(Me.ClientSize.Width - 150, 55),
            .BackColor = Color.FromArgb(52, 152, 219),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnBack.FlatAppearance.BorderSize = 0
        AddHandler btnBack.Click, AddressOf BtnBack_Click
        headerPanel.Controls.Add(btnBack)

        ' Main Content Panel
        Dim contentPanel As New Panel() With {
            .Location = New Point(0, 150),
            .Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height - 150),
            .BackColor = Color.FromArgb(236, 240, 241),
            .AutoScroll = True
        }
        Me.Controls.Add(contentPanel)

        Dim yPosition As Integer = 20

        ' Personal Information Section
        CreateSection(contentPanel, "?? Informasi Pribadi", yPosition)
        yPosition += 40

        ' Info Cards Row 1
        Dim xPosition As Integer = 30
        CreateInfoCard(contentPanel, "?? Email", GetUserEmail(), xPosition, yPosition)
        xPosition += 270
        CreateInfoCard(contentPanel, "?? No. HP", GetUserPhone(), xPosition, yPosition)
        xPosition += 270
        CreateInfoCard(contentPanel, "?? Minat Bakat", If(String.IsNullOrEmpty(LoggedMinatBakat), "Belum Tes", LoggedMinatBakat), xPosition, yPosition)

        yPosition += 120

        ' Statistics Section
        CreateSection(contentPanel, "?? Statistik", yPosition)
        yPosition += 40

        ' Stats Cards
        xPosition = 30
        Dim testCount As Integer = GetTestCount()
        Dim topicCount As Integer = GetGeneratedTopicCount()
        
        CreateStatCard(contentPanel, "??", "Total Tes", testCount.ToString(), xPosition, yPosition)
        xPosition += 270
        CreateStatCard(contentPanel, "??", "Topik Generated", topicCount.ToString(), xPosition, yPosition)
        xPosition += 270
        CreateStatCard(contentPanel, "?", "Status Akun", "Aktif", xPosition, yPosition)

        yPosition += 140

        ' Recent Activity Section
        CreateSection(contentPanel, "?? Aktivitas Terakhir", yPosition)
        yPosition += 40

        ' Activity List
        LoadRecentActivities(contentPanel, yPosition)

        yPosition += 200

        ' Action Buttons Section
        CreateSection(contentPanel, "?? Pengaturan Akun", yPosition)
        yPosition += 40

        ' Action Buttons
        xPosition = 30
        
        Dim btnChangePassword As New Button() With {
            .Text = "?? Ubah Password",
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .Size = New Size(200, 45),
            .Location = New Point(xPosition, yPosition),
            .BackColor = Color.FromArgb(243, 156, 18),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnChangePassword.FlatAppearance.BorderSize = 0
        AddHandler btnChangePassword.Click, AddressOf BtnChangePassword_Click
        contentPanel.Controls.Add(btnChangePassword)

        xPosition += 220

        Dim btnEditProfile As New Button() With {
            .Text = "?? Edit Profile",
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .Size = New Size(200, 45),
            .Location = New Point(xPosition, yPosition),
            .BackColor = Color.FromArgb(52, 152, 219),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnEditProfile.FlatAppearance.BorderSize = 0
        AddHandler btnEditProfile.Click, AddressOf BtnEditProfile_Click
        contentPanel.Controls.Add(btnEditProfile)

        xPosition += 220

        Dim btnLogout As New Button() With {
            .Text = "?? Logout",
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .Size = New Size(200, 45),
            .Location = New Point(xPosition, yPosition),
            .BackColor = Color.FromArgb(231, 76, 60),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnLogout.FlatAppearance.BorderSize = 0
        AddHandler btnLogout.Click, AddressOf BtnLogout_Click
        contentPanel.Controls.Add(btnLogout)
    End Sub

    Private Sub CreateSection(parent As Panel, title As String, yPos As Integer)
        Dim lblSection As New Label() With {
            .Text = title,
            .Font = New Font("Segoe UI", 14, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Location = New Point(30, yPos)
        }
        parent.Controls.Add(lblSection)
    End Sub

    Private Sub CreateInfoCard(parent As Panel, title As String, value As String, x As Integer, y As Integer)
        Dim card As New Panel() With {
            .Location = New Point(x, y),
            .Size = New Size(250, 90),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle
        }
        
        Dim lblTitle As New Label() With {
            .Text = title,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .ForeColor = Color.FromArgb(127, 140, 141),
            .AutoSize = True,
            .Location = New Point(15, 15)
        }
        card.Controls.Add(lblTitle)

        Dim lblValue As New Label() With {
            .Text = value,
            .Font = New Font("Segoe UI", 12, FontStyle.Regular),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = False,
            .Size = New Size(220, 40),
            .Location = New Point(15, 45)
        }
        card.Controls.Add(lblValue)

        parent.Controls.Add(card)
    End Sub

    Private Sub CreateStatCard(parent As Panel, icon As String, title As String, value As String, x As Integer, y As Integer)
        Dim card As New Panel() With {
            .Location = New Point(x, y),
            .Size = New Size(250, 110),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle
        }
        
        Dim lblIcon As New Label() With {
            .Text = icon,
            .Font = New Font("Segoe UI", 32, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point(15, 15)
        }
        card.Controls.Add(lblIcon)

        Dim lblTitle As New Label() With {
            .Text = title,
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .ForeColor = Color.FromArgb(127, 140, 141),
            .AutoSize = True,
            .Location = New Point(15, 70)
        }
        card.Controls.Add(lblTitle)

        Dim lblValue As New Label() With {
            .Text = value,
            .Font = New Font("Segoe UI", 20, FontStyle.Bold),
            .ForeColor = Color.FromArgb(52, 152, 219),
            .AutoSize = True,
            .Location = New Point(150, 30)
        }
        card.Controls.Add(lblValue)

        parent.Controls.Add(card)
    End Sub

    Private Sub LoadRecentActivities(parent As Panel, yPos As Integer)
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                Dim query As String = "SELECT aksi, detail, waktu FROM activity_logs WHERE user_id = @userId ORDER BY waktu DESC LIMIT 5"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Dim activityY As Integer = yPos

                        If Not reader.HasRows Then
                            Dim lblNoActivity As New Label() With {
                                .Text = "Belum ada aktivitas",
                                .Font = New Font("Segoe UI", 10, FontStyle.Italic),
                                .ForeColor = Color.FromArgb(127, 140, 141),
                                .Location = New Point(30, activityY)
                            }
                            parent.Controls.Add(lblNoActivity)
                        Else
                            While reader.Read()
                                Dim activityPanel As New Panel() With {
                                    .Location = New Point(30, activityY),
                                    .Size = New Size(Me.ClientSize.Width - 80, 50),
                                    .BackColor = Color.White,
                                    .BorderStyle = BorderStyle.FixedSingle
                                }

                                Dim lblAction As New Label() With {
                                    .Text = reader("aksi").ToString(),
                                    .Font = New Font("Segoe UI", 10, FontStyle.Bold),
                                    .ForeColor = Color.FromArgb(44, 62, 80),
                                    .AutoSize = True,
                                    .Location = New Point(15, 10)
                                }
                                activityPanel.Controls.Add(lblAction)

                                Dim lblDetail As New Label() With {
                                    .Text = reader("detail").ToString(),
                                    .Font = New Font("Segoe UI", 9, FontStyle.Regular),
                                    .ForeColor = Color.FromArgb(127, 140, 141),
                                    .AutoSize = False,
                                    .Size = New Size(500, 20),
                                    .Location = New Point(15, 28)
                                }
                                activityPanel.Controls.Add(lblDetail)

                                Dim lblTime As New Label() With {
                                    .Text = Convert.ToDateTime(reader("waktu")).ToString("dd MMM yyyy HH:mm"),
                                    .Font = New Font("Segoe UI", 9, FontStyle.Regular),
                                    .ForeColor = Color.FromArgb(149, 165, 166),
                                    .AutoSize = True,
                                    .Location = New Point(activityPanel.Width - 150, 15)
                                }
                                activityPanel.Controls.Add(lblTime)

                                parent.Controls.Add(activityPanel)
                                activityY += 55
                            End While
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error loading activities: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetUserEmail() As String
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT email FROM users WHERE id = @userId"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    Dim result = cmd.ExecuteScalar()
                    Return If(result IsNot Nothing, result.ToString(), "N/A")
                End Using
            End Using
        Catch
            Return "N/A"
        End Try
    End Function

    Private Function GetUserPhone() As String
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT no_hp FROM users WHERE id = @userId"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    Dim result = cmd.ExecuteScalar()
                    Return If(result IsNot Nothing, result.ToString(), "N/A")
                End Using
            End Using
        Catch
            Return "N/A"
        End Try
    End Function

    Private Function GetTestCount() As Integer
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT COUNT(*) FROM tests WHERE user_id = @userId"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch
            Return 0
        End Try
    End Function

    Private Function GetGeneratedTopicCount() As Integer
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT COUNT(DISTINCT topic_id) FROM generated_topics_history WHERE user_id = @userId"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch
            Return 0
        End Try
    End Function

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub BtnChangePassword_Click(sender As Object, e As EventArgs)
        Dim changePasswordForm As New ChangePassword()
        changePasswordForm.ShowDialog()
    End Sub

    Private Sub BtnEditProfile_Click(sender As Object, e As EventArgs)
        ' Edit Profile Form (will be created)
        MessageBox.Show("Fitur Edit Profile akan segera hadir!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub BtnLogout_Click(sender As Object, e As EventArgs)
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin logout?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        
        If result = DialogResult.Yes Then
            ' Log activity
            Try
                Using conn As New MySqlConnection(ConnectionString)
                    conn.Open()
                    Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, 'Logout', @detail, NOW())"
                    Using logCmd As New MySqlCommand(logQuery, conn)
                        logCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                        logCmd.Parameters.AddWithValue("@detail", $"User {LoggedUsername} logout dari Profile")
                        logCmd.ExecuteNonQuery()
                    End Using
                End Using
            Catch
                ' Silent fail
            End Try
            
            ClearSession()
            Dim landingPage As New LandingPage()
            landingPage.Show()
            Me.Close()
        End If
    End Sub

    Private Sub ProfileForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboard IsNot Nothing Then
            ParentDashboard.Show()
        Else
            If IsLoggedIn() Then
                Dim dashboardUser As New DashboardUser()
                dashboardUser.Show()
            End If
        End If
    End Sub
End Class
