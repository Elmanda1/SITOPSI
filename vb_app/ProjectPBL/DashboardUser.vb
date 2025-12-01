Imports MySql.Data.MySqlClient

Public Class DashboardUser
    Private Sub DashboardUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Cek apakah user sudah login
        If Not IsLoggedIn() Then
            MessageBox.Show("Silakan login terlebih dahulu!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Dim loginForm As New LoginForm()
            loginForm.Show()
            Me.Close()
            Return
        End If

        ' Cek apakah user adalah mahasiswa
        If Not IsMahasiswa() Then
            MessageBox.Show("Anda tidak memiliki akses ke halaman ini!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error)

            ' Redirect ke dashboard yang sesuai
            If IsAdmin() Then
                Dim dashboardAdmin As New DashboardAdmin()
                dashboardAdmin.Show()
            Else
                Dim loginForm As New LoginForm()
                loginForm.Show()
            End If

            Me.Close()
            Return
        End If

        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(236, 240, 241)

        ' Set form title
        Me.Text = $"Dashboard Mahasiswa - SITOPSI"

        ' Setup modern UI
        SetupModernUI()
    End Sub

    Private Sub SetupModernUI()
        ' Clear existing controls except basic ones
        Me.Controls.Clear()

        ' Header Panel
        Dim headerPanel As New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 100,
            .BackColor = Color.FromArgb(41, 128, 185)
        }
        Me.Controls.Add(headerPanel)

        ' Welcome Label
        Dim lblWelcome As New Label() With {
            .Text = $"🎓 Selamat Datang, {LoggedFullName}!",
            .Font = New Font("Segoe UI", 18, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(30, 25)
        }
        headerPanel.Controls.Add(lblWelcome)

        ' Info minat bakat
        Dim lblMinatBakat As New Label() With {
            .Text = If(String.IsNullOrEmpty(LoggedMinatBakat), 
                      "Belum mengikuti tes minat bakat", 
                      $"Minat Bakat: {LoggedMinatBakat}"),
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .ForeColor = Color.FromArgb(236, 240, 241),
            .AutoSize = True,
            .Location = New Point(30, 60)
        }
        headerPanel.Controls.Add(lblMinatBakat)

        ' Logout button on header
        Dim btnLogout As New Button() With {
            .Text = "🚪 Logout",
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .Size = New Size(120, 40),
            .Location = New Point(Me.ClientSize.Width - 150, 30),
            .BackColor = Color.FromArgb(231, 76, 60),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnLogout.FlatAppearance.BorderSize = 0
        AddHandler btnLogout.Click, AddressOf ButtonLogout_Click
        headerPanel.Controls.Add(btnLogout)

        ' Main Content Panel
        Dim contentPanel As New Panel() With {
            .Location = New Point(0, 100),
            .Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height - 100),
            .BackColor = Color.FromArgb(236, 240, 241),
            .AutoScroll = True
        }
        Me.Controls.Add(contentPanel)

        ' Title for menu
        Dim lblMenu As New Label() With {
            .Text = "📋 Menu Utama",
            .Font = New Font("Segoe UI", 14, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Location = New Point(30, 20)
        }
        contentPanel.Controls.Add(lblMenu)

        ' Menu Cards Container
        Dim yPosition As Integer = 70
        Dim xPosition As Integer = 30
        Dim cardWidth As Integer = 250
        Dim cardHeight As Integer = 180
        Dim spacing As Integer = 30

        ' Card 1: Tes Minat Bakat
        Dim card1 As New Panel() With {
            .Location = New Point(xPosition, yPosition),
            .Size = New Size(cardWidth, cardHeight),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .Cursor = Cursors.Hand
        }
        contentPanel.Controls.Add(card1)

        Dim icon1 As New Label() With {
            .Text = "📋",
            .Font = New Font("Segoe UI", 48, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point((cardWidth - 60) \ 2, 20)
        }
        card1.Controls.Add(icon1)

        Dim title1 As New Label() With {
            .Text = "Tes Minat Bakat",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Size = New Size(cardWidth - 20, 25),
            .Location = New Point(10, 90)
        }
        card1.Controls.Add(title1)

        Dim desc1 As New Label() With {
            .Text = "Ikuti tes untuk mengetahui minat dan bakat Anda",
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .ForeColor = Color.FromArgb(127, 140, 141),
            .AutoSize = False,
            .TextAlign = ContentAlignment.TopCenter,
            .Size = New Size(cardWidth - 20, 50),
            .Location = New Point(10, 120)
        }
        card1.Controls.Add(desc1)

        AddHandler card1.Click, AddressOf Card1_Click
        AddHandler icon1.Click, AddressOf Card1_Click
        AddHandler title1.Click, AddressOf Card1_Click
        AddHandler desc1.Click, AddressOf Card1_Click

        ' Card 2: Lihat Hasil Tes
        xPosition += cardWidth + spacing
        Dim card2 As New Panel() With {
            .Location = New Point(xPosition, yPosition),
            .Size = New Size(cardWidth, cardHeight),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .Cursor = Cursors.Hand
        }
        contentPanel.Controls.Add(card2)

        Dim icon2 As New Label() With {
            .Text = "📊",
            .Font = New Font("Segoe UI", 48, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point((cardWidth - 60) \ 2, 20)
        }
        card2.Controls.Add(icon2)

        Dim title2 As New Label() With {
            .Text = "Lihat Hasil Tes",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Size = New Size(cardWidth - 20, 25),
            .Location = New Point(10, 90)
        }
        card2.Controls.Add(title2)

        Dim desc2 As New Label() With {
            .Text = "Lihat hasil tes minat bakat yang sudah Anda ikuti",
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .ForeColor = Color.FromArgb(127, 140, 141),
            .AutoSize = False,
            .TextAlign = ContentAlignment.TopCenter,
            .Size = New Size(cardWidth - 20, 50),
            .Location = New Point(10, 120)
        }
        card2.Controls.Add(desc2)

        AddHandler card2.Click, AddressOf Card2_Click
        AddHandler icon2.Click, AddressOf Card2_Click
        AddHandler title2.Click, AddressOf Card2_Click
        AddHandler desc2.Click, AddressOf Card2_Click

        ' Card 3: Generate Topik
        xPosition += cardWidth + spacing
        Dim card3 As New Panel() With {
            .Location = New Point(xPosition, yPosition),
            .Size = New Size(cardWidth, cardHeight),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .Cursor = Cursors.Hand
        }
        contentPanel.Controls.Add(card3)

        Dim icon3 As New Label() With {
            .Text = "📝",
            .Font = New Font("Segoe UI", 48, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point((cardWidth - 60) \ 2, 20)
        }
        card3.Controls.Add(icon3)

        Dim title3 As New Label() With {
            .Text = "Generate Topik",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Size = New Size(cardWidth - 20, 25),
            .Location = New Point(10, 90)
        }
        card3.Controls.Add(title3)

        Dim desc3 As New Label() With {
            .Text = "Generate rekomendasi topik skripsi sesuai minat Anda",
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .ForeColor = Color.FromArgb(127, 140, 141),
            .AutoSize = False,
            .TextAlign = ContentAlignment.TopCenter,
            .Size = New Size(cardWidth - 20, 50),
            .Location = New Point(10, 120)
        }
        card3.Controls.Add(desc3)

        AddHandler card3.Click, AddressOf Card3_Click
        AddHandler icon3.Click, AddressOf Card3_Click
        AddHandler title3.Click, AddressOf Card3_Click
        AddHandler desc3.Click, AddressOf Card3_Click

        ' Row 2
        yPosition += cardHeight + spacing
        xPosition = 30

        ' Card 4: Change Password
        Dim card4 As New Panel() With {
            .Location = New Point(xPosition, yPosition),
            .Size = New Size(cardWidth, cardHeight),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .Cursor = Cursors.Hand
        }
        contentPanel.Controls.Add(card4)

        Dim icon4 As New Label() With {
            .Text = "🔐",
            .Font = New Font("Segoe UI", 48, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point((cardWidth - 60) \ 2, 20)
        }
        card4.Controls.Add(icon4)

        Dim title4 As New Label() With {
            .Text = "Ubah Password",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Size = New Size(cardWidth - 20, 25),
            .Location = New Point(10, 90)
        }
        card4.Controls.Add(title4)

        Dim desc4 As New Label() With {
            .Text = "Ganti password akun Anda untuk keamanan",
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .ForeColor = Color.FromArgb(127, 140, 141),
            .AutoSize = False,
            .TextAlign = ContentAlignment.TopCenter,
            .Size = New Size(cardWidth - 20, 50),
            .Location = New Point(10, 120)
        }
        card4.Controls.Add(desc4)

        AddHandler card4.Click, AddressOf Card4_Click
        AddHandler icon4.Click, AddressOf Card4_Click
        AddHandler title4.Click, AddressOf Card4_Click
        AddHandler desc4.Click, AddressOf Card4_Click

        ' Card 5: Profile (NEW)
        xPosition += cardWidth + spacing
        Dim card5 As New Panel() With {
            .Location = New Point(xPosition, yPosition),
            .Size = New Size(cardWidth, cardHeight),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .Cursor = Cursors.Hand
        }
        contentPanel.Controls.Add(card5)

        Dim icon5 As New Label() With {
            .Text = "👤",
            .Font = New Font("Segoe UI", 48, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point((cardWidth - 60) \ 2, 20)
        }
        card5.Controls.Add(icon5)

        Dim title5 As New Label() With {
            .Text = "Profile Saya",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Size = New Size(cardWidth - 20, 25),
            .Location = New Point(10, 90)
        }
        card5.Controls.Add(title5)

        Dim desc5 As New Label() With {
            .Text = "Lihat dan kelola informasi profile Anda",
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .ForeColor = Color.FromArgb(127, 140, 141),
            .AutoSize = False,
            .TextAlign = ContentAlignment.TopCenter,
            .Size = New Size(cardWidth - 20, 50),
            .Location = New Point(10, 120)
        }
        card5.Controls.Add(desc5)

        AddHandler card5.Click, AddressOf Card5_Click
        AddHandler icon5.Click, AddressOf Card5_Click
        AddHandler title5.Click, AddressOf Card5_Click
        AddHandler desc5.Click, AddressOf Card5_Click
    End Sub

    Private Sub Card1_Click(sender As Object, e As EventArgs)
        ' Tes Minat Bakat
        Button1_Click(sender, e)
    End Sub

    Private Sub Card2_Click(sender As Object, e As EventArgs)
        ' Lihat Hasil Tes
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                
                Dim query As String = "SELECT COUNT(*) FROM tests WHERE user_id = @userId"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    
                    If count > 0 Then
                        Dim hasilForm As New HasilTesMinatBakat()
                        hasilForm.ParentDashboard = Me
                        hasilForm.Show()
                        Me.Hide()
                    Else
                        MessageBox.Show("Anda belum pernah mengikuti tes minat bakat!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Card3_Click(sender As Object, e As EventArgs)
        ' Generate Topik
        Button2_Click(sender, e)
    End Sub

    Private Sub Card4_Click(sender As Object, e As EventArgs)
        ' Change Password
        Dim changePasswordForm As New ChangePassword()
        changePasswordForm.ShowDialog()
    End Sub

    Private Sub Card5_Click(sender As Object, e As EventArgs)
        ' Profile
        Dim profileForm As New ProfileForm()
        profileForm.ParentDashboard = Me
        profileForm.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ' Tombol Tes Minat Bakat
        Try
            ' Cek apakah user sudah pernah tes
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                
                Dim query As String = "SELECT COUNT(*) FROM tests WHERE user_id = @userId"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    
                    If count > 0 Then
                        ' User sudah pernah tes, tanya mau lihat hasil atau tes ulang
                        Dim result As DialogResult = MessageBox.Show(
                            "Anda sudah pernah mengikuti tes. Apakah ingin melihat hasil tes terakhir?" & vbCrLf & vbCrLf &
                            "Klik YES untuk melihat hasil" & vbCrLf &
                            "Klik NO untuk mengikuti tes lagi",
                            "Tes Minat Bakat",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question)
                        
                        If result = DialogResult.Yes Then
                            ' Tampilkan hasil tes terakhir
                            Dim hasilForm As New HasilTesMinatBakat()
                            hasilForm.ParentDashboard = Me
                            hasilForm.Show()
                            Me.Hide()
                        ElseIf result = DialogResult.No Then
                            ' Ikuti tes lagi
                            Dim tesForm As New TesMinatBakat()
                            tesForm.ParentDashboard = Me
                            tesForm.Show()
                            Me.Hide()
                        End If
                    Else
                        ' User belum pernah tes, langsung ke form tes
                        Dim tesForm As New TesMinatBakat()
                        tesForm.ParentDashboard = Me
                        tesForm.Show()
                        Me.Hide()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        ' Tombol Generate Topik Skripsi
        Try
            ' Cek apakah user sudah pernah tes minat bakat
            If String.IsNullOrEmpty(LoggedMinatBakat) Then
                Dim result As DialogResult = MessageBox.Show(
                    "Anda belum mengikuti Tes Minat Bakat." & vbCrLf &
                    "Untuk hasil topik skripsi yang lebih akurat, disarankan untuk mengikuti tes terlebih dahulu." & vbCrLf & vbCrLf &
                    "Apakah ingin mengikuti tes sekarang?",
                    "Tes Minat Bakat Belum Dilakukan",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                
                If result = DialogResult.Yes Then
                    ' Redirect ke tes minat bakat
                    Dim tesForm As New TesMinatBakat()
                    tesForm.ParentDashboard = Me
                    tesForm.Show()
                    Me.Hide()
                    Return
                End If
            End If
            
            ' Lanjut ke Generate Topik Skripsi
            Dim generateForm As New GenerateTopikSkripsi()
            generateForm.ParentDashboard = Me
            generateForm.Show()
            Me.Hide()
            
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ButtonLogout_Click(sender As Object, e As EventArgs)
        ' Konfirmasi logout
        Dim result As DialogResult = MessageBox.Show(
            "Apakah Anda yakin ingin logout?",
            "Konfirmasi Logout",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)
        
        If result = DialogResult.Yes Then
            ' Log activity
            Try
                Using conn As New MySqlConnection(ConnectionString)
                    conn.Open()
                    Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, 'Logout', @detail, NOW())"
                    Using logCmd As New MySqlCommand(logQuery, conn)
                        logCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                        logCmd.Parameters.AddWithValue("@detail", $"User {LoggedUsername} logout")
                        logCmd.ExecuteNonQuery()
                    End Using
                End Using
            Catch
                ' Silent fail
            End Try
            
            ' Clear session
            ClearSession()
            
            ' Redirect ke landing page
            Dim landingPage As New LandingPage()
            landingPage.Show()
            Me.Close()
        End If
    End Sub

    Private Sub DashboardUser_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Jika form di-close tapi session masih ada, berarti user close window tanpa logout
        ' Tanya apakah ingin logout
        If IsLoggedIn() Then
            Dim result As DialogResult = MessageBox.Show(
                "Apakah Anda ingin logout?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
            
            If result = DialogResult.Yes Then
                ' Logout
                ClearSession()
                
                ' Tampilkan landing page
                Dim landingPage As New LandingPage()
                landingPage.Show()
            Else
                ' Cancel close
                e.Cancel = True
            End If
        End If
    End Sub
End Class