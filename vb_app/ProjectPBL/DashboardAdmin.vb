Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class DashboardAdmin
    Public Property ParentLanding As LandingPage
    Private isLoggingOut As Boolean = False
    Private ReadOnly cardHoverColor As Color = Color.FromArgb(240, 240, 240)
    Private ReadOnly cardDefaultColor As Color = Color.White

    Private Sub DashboardAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Or Not IsAdmin() Then
            MessageBox.Show("Akses ditolak!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If ParentLanding IsNot Nothing AndAlso Not ParentLanding.IsDisposed Then
                ParentLanding.Show()
            Else
                Dim login As New LoginForm()
                login.Show()
            End If
            Me.Close()
            Return
        End If

        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(245, 247, 250)
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
            .BackColor = Color.FromArgb(44, 62, 80)
        }
        Me.Controls.Add(header)

        Dim lblTitle As New Label With {
            .Text = "⚙️ Admin Dashboard - " & LoggedFullName,
            .Font = New Font("Segoe UI", 18, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(30, 30)
        }
        header.Controls.Add(lblTitle)

        ' Logout Button
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
        header.Controls.Add(btnLogout)

        ' Content Panel
        Dim content As New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.FromArgb(245, 247, 250),
            .Padding = New Padding(30)
        }
        Me.Controls.Add(content)
        content.BringToFront()

        ' Menu Title
        Dim lblMenu As New Label With {
            .Text = "Menu Manajemen",
            .Font = New Font("Segoe UI", 16, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Location = New Point(30, 0)
        }
        content.Controls.Add(lblMenu)

        ' FlowLayoutPanel for cards
        Dim flowPanel As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .Location = New Point(30, 50),
            .AutoScroll = True,
            .FlowDirection = FlowDirection.LeftToRight,
            .WrapContents = True
        }
        content.Controls.Add(flowPanel)
        flowPanel.BringToFront()

        ' Add Cards
        flowPanel.Controls.Add(CreateMenuCard("👤", "User Management", "Kelola data pengguna", AddressOf Card1Click))
        flowPanel.Controls.Add(CreateMenuCard("📄", "Manajemen Pertanyaan", "Kelola pertanyaan dan statement CF", AddressOf Card2Click))
        flowPanel.Controls.Add(CreateMenuCard("🏷️", "Rules", "Kelola rules (Gejala)", AddressOf Card4Click))
        flowPanel.Controls.Add(CreateMenuCard("🔗", "Rules Combination", "Kelola kombinasi rules", AddressOf Card5Click))
        flowPanel.Controls.Add(CreateMenuCard("📚", "Topics Management", "Kelola topik skripsi", AddressOf Card6Click))
        flowPanel.Controls.Add(CreateMenuCard("📜", "Activity Logs", "Lihat log aktivitas", AddressOf Card3Click))
    End Sub

    Private Function CreateMenuCard(icon As String, title As String, description As String, handler As EventHandler) As Panel
        Dim card As New Panel With {
            .Size = New Size(280, 180),
            .BackColor = cardDefaultColor,
            .Margin = New Padding(15),
            .Cursor = Cursors.Hand
        }

        Dim pnlBorder As New Panel() With {.BackColor = Color.LightGray, .Dock = DockStyle.Bottom, .Height = 1}
        card.Controls.Add(pnlBorder)

        Dim lblIcon As New Label With {
            .Text = icon,
            .Font = New Font("Segoe UI Emoji", 28, FontStyle.Regular), ' Reduced icon size
            .AutoSize = False,
            .Size = New Size(card.Width, 60),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(0, 20)
        }
        card.Controls.Add(lblIcon)

        Dim lblTitle As New Label With {
            .Text = title,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Size = New Size(card.Width, 30),
            .Location = New Point(0, 85)
        }
        card.Controls.Add(lblTitle)

        Dim lblDesc As New Label With {
            .Text = description,
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .ForeColor = Color.FromArgb(127, 140, 141),
            .AutoSize = False,
            .TextAlign = ContentAlignment.TopCenter,
            .Size = New Size(card.Width - 20, 50),
            .Location = New Point(10, 120)
        }
        card.Controls.Add(lblDesc)

        For Each ctrl As Control In card.Controls
            AddHandler ctrl.Click, handler
            AddHandler ctrl.MouseEnter, AddressOf Card_MouseEnter
            AddHandler ctrl.MouseLeave, AddressOf Card_MouseLeave
        Next
        AddHandler card.Click, handler
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

    Private Sub Card1Click(s As Object, e As EventArgs)
        Dim userManagementForm As New UserManagementForm()
        userManagementForm.ParentDashboardAdmin = Me
        userManagementForm.Show()
        Me.Hide()
    End Sub

    Private Sub Card3Click(s As Object, e As EventArgs)
        Dim activityLogForm As New ActivityLogForm()
        activityLogForm.ParentDashboardAdmin = Me
        activityLogForm.Show()
        Me.Hide()
    End Sub
    
    Private Sub Card4Click(s As Object, e As EventArgs)
        Dim rulesManagementForm As New RulesManagementForm()
        rulesManagementForm.ParentDashboardAdmin = Me
        rulesManagementForm.Show()
        Me.Hide()
    End Sub
    
    Private Sub Card5Click(s As Object, e As EventArgs)
        Dim rulesCombinationForm As New RulesCombinationManagementForm()
        rulesCombinationForm.ParentDashboardAdmin = Me
        rulesCombinationForm.Show()
        Me.Hide()
    End Sub
    
    Private Sub Card6Click(s As Object, e As EventArgs)
        Dim topicManagementForm As New TopicManagementForm()
        topicManagementForm.ParentDashboardAdmin = Me
        topicManagementForm.Show()
        Me.Hide()
    End Sub

    Private Sub Card2Click(s As Object, e As EventArgs)
        Dim questionCFForm As New QuestionCFManagementForm()
        questionCFForm.ParentDashboardAdmin = Me
        questionCFForm.Show()
        Me.Hide()
    End Sub

    Private Sub ButtonLogout_Click(sender As Object, e As EventArgs)
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
                        logCmd.Parameters.AddWithValue("@detail", "Admin " & LoggedUsername & " logout")
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

    Private Sub DashboardAdmin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not isLoggingOut Then
            ' User menutup form dengan X button
            Dim result As DialogResult = MessageBox.Show(
                "Apakah Anda yakin ingin keluar dari aplikasi?",
                "Konfirmasi Keluar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
            
            If result = DialogResult.No Then
                e.Cancel = True ' Cancel form closing
            Else
                Application.Exit() ' Close app completely
            End If
        End If
        ' Jika isLoggingOut = True, biarkan form close normally (akan ke LandingPage)
    End Sub
End Class
