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

        ' Set form title
        Me.Text = $"Dashboard Mahasiswa - {LoggedFullName}"
    End Sub

    Private Sub DashboardUser_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Logout saat form ditutup
        ClearSession()

        ' Tampilkan landing page
        Dim landingPage As New LandingPage()
        landingPage.Show()
    End Sub
End Class