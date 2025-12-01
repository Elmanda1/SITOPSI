Imports MySql.Data.MySqlClient

Public Class DashboardAdmin
    Private Sub DashboardAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Cek apakah user sudah login
        If Not IsLoggedIn() Then
            MessageBox.Show("Silakan login terlebih dahulu!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Dim loginForm As New LoginForm()
            loginForm.Show()
            Me.Close()
            Return
        End If

        ' Cek apakah user adalah admin
        If Not IsAdmin() Then
            MessageBox.Show("Anda tidak memiliki akses ke halaman ini!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error)
            
            ' Redirect ke dashboard yang sesuai
            If IsMahasiswa() Then
                Dim dashboardUser As New DashboardUser()
                dashboardUser.Show()
            Else
                Dim loginForm As New LoginForm()
                loginForm.Show()
            End If
            
            Me.Close()
            Return
        End If

        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen
        
        ' Set form title
        Me.Text = $"Dashboard Admin - {LoggedFullName}"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Logout button
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin logout?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        
        If result = DialogResult.Yes Then
            ' Log activity
            Try
                Using conn As New MySqlConnection(ConnectionString)
                    conn.Open()
                    Dim query As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, 'Logout', @detail, NOW())"
                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                        cmd.Parameters.AddWithValue("@detail", $"User {LoggedUsername} logout")
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            Catch
                ' Silent fail untuk logging
            End Try

            ' Clear session
            ClearSession()

            ' Kembali ke landing page
            Dim landingPage As New LandingPage()
            landingPage.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ' Keep existing
    End Sub

    Private Sub DashboardAdmin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Logout saat form ditutup
        ClearSession()
        
        ' Tampilkan landing page
        Dim landingPage As New LandingPage()
        landingPage.Show()
    End Sub
End Class