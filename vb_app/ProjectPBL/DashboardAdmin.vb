Imports MySql.Data.MySqlClient

Public Class DashboardAdmin
    Private Sub DashboardAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Or Not IsAdmin() Then
            MessageBox.Show("Akses ditolak!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim loginForm As New LoginForm()
            loginForm.Show()
            Me.Close()
            Return
        End If

        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(236, 240, 241)
        Me.Size = New Size(1100, 700)
        Me.Text = "Dashboard Admin - SITOPSI"
        BuildUI()
    End Sub

    Private Sub BuildUI()
        Me.Controls.Clear()
        
        ' Header Panel
        Dim header As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 100,
            .BackColor = Color.FromArgb(231, 76, 60)
        }
        
        Dim lblTitle As New Label With {
            .Text = $"?? Admin Dashboard - {LoggedFullName}",
            .Font = New Font("Segoe UI", 18, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(30, 30)
        }
        header.Controls.Add(lblTitle)
        
        ' Logout Button
        Dim btnLogout As New Button() With {
            .Text = "?? Logout",
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .Size = New Size(120, 40),
            .Location = New Point(Me.ClientSize.Width - 150, 30),
            .BackColor = Color.FromArgb(52, 73, 94),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnLogout.FlatAppearance.BorderSize = 0
        AddHandler btnLogout.Click, AddressOf ButtonLogout_Click
        header.Controls.Add(btnLogout)
        
        Me.Controls.Add(header)

        ' Content Panel
        Dim content As New Panel With {
            .Location = New Point(0, 100),
            .Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height - 100),
            .BackColor = Color.FromArgb(236, 240, 241),
            .AutoScroll = True
        }
        
        ' Menu Title
        Dim lblMenu As New Label With {
            .Text = "?? Menu Manajemen",
            .Font = New Font("Segoe UI", 14, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Location = New Point(30, 20)
        }
        content.Controls.Add(lblMenu)
        
        ' Row 1 Cards
        AddCard(content, "??", "User Management", "Kelola data pengguna", 30, 70, AddressOf Card1Click)
        AddCard(content, "?", "Questions", "Kelola pertanyaan tes", 310, 70, AddressOf Card2Click)
        AddCard(content, "??", "Rules/Kategori", "Kelola kategori/rules", 590, 70, AddressOf Card5Click)
        
        ' Row 2 Cards
        AddCard(content, "??", "Activity Logs", "Lihat log aktivitas", 30, 280, AddressOf Card3Click)
        
        Me.Controls.Add(content)
    End Sub

    Private Sub AddCard(parent As Panel, icon As String, title As String, description As String, x As Integer, y As Integer, handler As EventHandler)
        Dim card As New Panel With {
            .Location = New Point(x, y),
            .Size = New Size(250, 180),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .Cursor = Cursors.Hand
        }
        
        ' Icon
        Dim lblIcon As New Label With {
            .Text = icon,
            .Font = New Font("Segoe UI", 48),
            .AutoSize = True,
            .Location = New Point((card.Width - 60) \ 2, 20)
        }
        card.Controls.Add(lblIcon)
        
        ' Title
        Dim lblTitle As New Label With {
            .Text = title,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Size = New Size(230, 25),
            .Location = New Point(10, 90)
        }
        card.Controls.Add(lblTitle)
        
        ' Description
        Dim lblDesc As New Label With {
            .Text = description,
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .ForeColor = Color.FromArgb(127, 140, 141),
            .AutoSize = False,
            .TextAlign = ContentAlignment.TopCenter,
            .Size = New Size(230, 50),
            .Location = New Point(10, 120)
        }
        card.Controls.Add(lblDesc)
        
        AddHandler card.Click, handler
        AddHandler lblIcon.Click, handler
        AddHandler lblTitle.Click, handler
        AddHandler lblDesc.Click, handler
        
        parent.Controls.Add(card)
    End Sub

    Private Sub Card1Click(s As Object, e As EventArgs)
        ' User Management
        Dim userManagementForm As New UserManagementForm()
        userManagementForm.ParentDashboard = Me
        userManagementForm.Show()
        Me.Hide()
    End Sub

    Private Sub Card2Click(s As Object, e As EventArgs)
        ' Question Management
        Dim questionManagementForm As New QuestionManagementForm()
        questionManagementForm.ParentDashboard = Me
        questionManagementForm.Show()
        Me.Hide()
    End Sub

    Private Sub Card3Click(s As Object, e As EventArgs)
        ' Activity Logs
        Dim activityLogForm As New ActivityLogForm()
        activityLogForm.ParentDashboard = Me
        activityLogForm.Show()
        Me.Hide()
    End Sub
    
    Private Sub Card5Click(s As Object, e As EventArgs)
        ' Rules Management
        Dim rulesManagementForm As New RulesManagementForm()
        rulesManagementForm.ParentDashboard = Me
        rulesManagementForm.Show()
        Me.Hide()
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
                        logCmd.Parameters.AddWithValue("@detail", $"Admin {LoggedUsername} logout")
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

    Private Sub DashboardAdmin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Jika form di-close tapi session masih ada, tanya logout
        If IsLoggedIn() Then
            Dim result As DialogResult = MessageBox.Show(
                "Apakah Anda ingin logout?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
            
            If result = DialogResult.Yes Then
                ClearSession()
                Dim landingPage As New LandingPage()
                landingPage.Show()
            Else
                e.Cancel = True
            End If
        End If
    End Sub
End Class
