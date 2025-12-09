Imports MySql.Data.MySqlClient
Imports System.Diagnostics

Public Class DashboardUser
    Public Property ParentLanding As LandingPage
    Private isLoggingOut As Boolean = False
    Private ReadOnly cardHoverColor As Color = Color.FromArgb(240, 240, 240)
    Private ReadOnly cardDefaultColor As Color = Color.White

    Private Sub DashboardUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
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
                If IsAdmin() Then
                    Dim dashboardAdmin As New DashboardAdmin()
                    dashboardAdmin.ParentLanding = Me.ParentLanding
                    dashboardAdmin.Show()
                Else
                    Dim loginForm As New LoginForm()
                    loginForm.ParentLanding = Me.ParentLanding
                    loginForm.Show()
                End If
                Me.Close()
                Return
            End If

            ' Center form
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.BackColor = Color.FromArgb(245, 247, 250)
            Me.Text = "Dashboard Mahasiswa - SITOPSI"

            ' Setup modern UI
            SetupModernUI()

        Catch ex As Exception
            MessageBox.Show("Error loading dashboard: " & ex.Message & vbCrLf & vbCrLf & ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetupModernUI()
        ' Hide all designer controls first instead of clearing
        For Each ctrl As Control In Me.Controls
            ctrl.Visible = False
        Next

        Me.ClientSize = New Size(1000, 650)

        ' Header Panel
        Dim headerPanel As New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 100,
            .BackColor = Color.FromArgb(12, 45, 72) ' Navy Blue
        }
        Me.Controls.Add(headerPanel)

        ' Welcome Label
        Dim lblWelcome As New Label() With {
            .Text = "🎓 Selamat Datang, " & LoggedFullName & "!",
            .Font = New Font("Segoe UI", 18, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(30, 25)
        }
        headerPanel.Controls.Add(lblWelcome)

        ' Info minat bakat
        Dim minatText As String = If(String.IsNullOrEmpty(LoggedMinatBakat), "Anda belum mengikuti tes minat bakat.", "Minat Bakat Anda: " & LoggedMinatBakat)
        Dim lblMinatBakat As New Label() With {
            .Text = minatText,
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .ForeColor = Color.FromArgb(220, 220, 220),
            .AutoSize = True,
            .Location = New Point(30, 60)
        }
        headerPanel.Controls.Add(lblMinatBakat)

        ' Logout button on header
        Dim btnLogout As New Button() With {
            .Text = "Logout",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
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
            .Dock = DockStyle.Fill,
            .BackColor = Color.FromArgb(245, 247, 250),
            .AutoScroll = True,
            .Padding = New Padding(30)
        }
        Me.Controls.Add(contentPanel)
        contentPanel.BringToFront()

        ' Title for menu
        Dim lblMenu As New Label() With {
            .Text = "Menu Utama",
            .Font = New Font("Segoe UI", 16, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Location = New Point(30, 0)
        }
        contentPanel.Controls.Add(lblMenu)

        ' Cards FlowLayoutPanel
        Dim flowPanel As New FlowLayoutPanel() With {
            .Dock = DockStyle.Fill,
            .Location = New Point(30, 50),
            .AutoScroll = True,
            .FlowDirection = FlowDirection.LeftToRight,
            .WrapContents = True
        }
        contentPanel.Controls.Add(flowPanel)
        flowPanel.BringToFront()

        ' Create cards
        Dim card1 = CreateMenuCard("Tes Minat Bakat", "Ikuti tes untuk mengetahui minat dan bakat Anda", "📋", AddressOf Card1_Click)
        Dim card2 = CreateMenuCard("Lihat Hasil Tes", "Lihat hasil tes minat bakat yang sudah Anda ikuti", "📊", AddressOf Card2_Click)
        Dim card3 = CreateMenuCard("Generate Topik", "Dapatkan rekomendasi topik skripsi sesuai minat Anda", "📝", AddressOf Card3_Click)
        Dim card4 = CreateMenuCard("Ubah Password", "Ganti password akun Anda untuk keamanan", "🔐", AddressOf Card4_Click)
        Dim card5 = CreateMenuCard("Profile Saya", "Lihat dan kelola informasi profile Anda", "👤", AddressOf Card5_Click)

        flowPanel.Controls.Add(card1)
        flowPanel.Controls.Add(card2)
        flowPanel.Controls.Add(card3)
        flowPanel.Controls.Add(card4)
        flowPanel.Controls.Add(card5)
    End Sub

    Private Function CreateMenuCard(title As String, description As String, icon As String, clickHandler As EventHandler) As Panel
        Dim card As New Panel() With {
            .Size = New Size(280, 180),
            .BackColor = cardDefaultColor,
            .Margin = New Padding(15),
            .Cursor = Cursors.Hand
        }

        Dim pnlBorder As New Panel() With {.BackColor = Color.LightGray, .Dock = DockStyle.Bottom, .Height = 1}
        card.Controls.Add(pnlBorder)

        Dim iconLabel As New Label() With {
            .Text = icon,
            .Font = New Font("Segoe UI Emoji", 28, FontStyle.Regular),
            .AutoSize = False,
            .Size = New Size(card.Width, 60),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(0, 20)
        }
        card.Controls.Add(iconLabel)

        Dim titleLabel As New Label() With {
            .Text = title,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Size = New Size(card.Width, 30),
            .Location = New Point(0, 85)
        }
        card.Controls.Add(titleLabel)

        Dim descLabel As New Label() With {
            .Text = description,
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .ForeColor = Color.FromArgb(127, 140, 141),
            .AutoSize = False,
            .TextAlign = ContentAlignment.TopCenter,
            .Size = New Size(card.Width - 20, 50),
            .Location = New Point(10, 120)
        }
        card.Controls.Add(descLabel)

        ' Add handlers to all controls
        For Each ctrl As Control In card.Controls
            AddHandler ctrl.Click, clickHandler
            AddHandler ctrl.MouseEnter, AddressOf Card_MouseEnter
            AddHandler ctrl.MouseLeave, AddressOf Card_MouseLeave
        Next
        AddHandler card.Click, clickHandler
        AddHandler card.MouseEnter, AddressOf Card_MouseEnter
        AddHandler card.MouseLeave, AddressOf Card_MouseLeave

        Return card
    End Function

    Private Sub Card_MouseEnter(sender As Object, e As EventArgs)
        Dim ctrl = CType(sender, Control)
        Dim card As Panel = If(TypeOf ctrl Is Panel, CType(ctrl, Panel), CType(ctrl.Parent, Panel))
        card.BackColor = cardHoverColor
    End Sub

    Private Sub Card_MouseLeave(sender As Object, e As EventArgs)
        Dim ctrl = CType(sender, Control)
        Dim card As Panel = If(TypeOf ctrl Is Panel, CType(ctrl, Panel), CType(ctrl.Parent, Panel))
        card.BackColor = cardDefaultColor
    End Sub

    Private Sub Card1_Click(sender As Object, e As EventArgs)
        ' Tes Minat Bakat
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT COUNT(*) FROM tests WHERE user_id = @userId"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    If count > 0 Then
                        Dim result As DialogResult = MessageBox.Show(
                            "Anda sudah pernah mengikuti tes. Apakah ingin melihat hasil tes terakhir?" & vbCrLf & vbCrLf &
                            "Klik YES untuk melihat hasil" & vbCrLf &
                            "Klik NO untuk mengikuti tes lagi",
                            "Tes Minat Bakat", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                        If result = DialogResult.Yes Then
                            Dim hasilForm As New HasilTesMinatBakat()
                            hasilForm.ParentDashboard = Me
                            hasilForm.Show()
                            Me.Hide()
                        ElseIf result = DialogResult.No Then
                            Dim tesForm As New TesMinatBakat()
                            tesForm.ParentDashboard = Me
                            tesForm.Show()
                            Me.Hide()
                        End If
                    Else
                        Dim tesForm As New TesMinatBakat()
                        tesForm.ParentDashboard = Me
                        tesForm.Show()
                        Me.Hide()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Card3_Click(sender As Object, e As EventArgs)
        ' Generate Topik
        Try
            If String.IsNullOrEmpty(LoggedMinatBakat) Then
                Dim result As DialogResult = MessageBox.Show(
                    "Anda belum mengikuti Tes Minat Bakat." & vbCrLf &
                    "Untuk hasil topik skripsi yang lebih akurat, disarankan untuk mengikuti tes terlebih dahulu." & vbCrLf & vbCrLf &
                    "Apakah ingin mengikuti tes sekarang?",
                    "Tes Minat Bakat Belum Dilakukan", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    Dim tesForm As New TesMinatBakat()
                    tesForm.ParentDashboard = Me
                    tesForm.Show()
                    Me.Hide()
                    Return
                End If
            End If
            Dim generateForm As New GenerateTopikSkripsi()
            generateForm.ParentDashboard = Me
            generateForm.Show()
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

    Private Sub ButtonLogout_Click(sender As Object, e As EventArgs)
        ' Konfirmasi logout
        Dim result As DialogResult = MessageBox.Show(
            "Apakah Anda yakin ingin logout?",
            "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            isLoggingOut = True
            Try
                Using conn As New MySqlConnection(ConnectionString)
                    conn.Open()
                    Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, 'Logout', @detail, NOW())"
                    Using logCmd As New MySqlCommand(logQuery, conn)
                        logCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                        logCmd.Parameters.AddWithValue("@detail", "User " & LoggedUsername & " logout")
                        logCmd.ExecuteNonQuery()
                    End Using
                End Using
            Catch
            End Try
            ClearSession()
            If ParentLanding IsNot Nothing AndAlso Not ParentLanding.IsDisposed Then
                ParentLanding.Show()
            Else
                Dim lp As New LandingPage()
                lp.Show()
            End If
            Me.Close()
        End If
    End Sub

    Private Sub DashboardUser_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' When the main dashboard form closes (by clicking X), exit the application
        If Not isLoggingOut Then
            Application.Exit()
        End If
    End Sub
End Class