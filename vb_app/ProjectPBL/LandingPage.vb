Public Class LandingPage
    Private Sub LandingPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen
        
        ' Clear session saat buka landing page
        ClearSession()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Buka Login Form
        Dim loginForm As New LoginForm()
        loginForm.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Buka Register Form
        Dim registerForm As New RegisterForm()
        registerForm.Show()
        Me.Hide()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ' Keep existing
    End Sub
End Class
