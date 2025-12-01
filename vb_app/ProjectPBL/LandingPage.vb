Public Class LandingPage
    Private Sub LandingPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Clear session saat buka landing page
        ClearSession()

        ' Load logo SITOPSI kiri
        Try
            Dim logoPath As String = System.IO.Path.Combine(Application.StartupPath, "..\..\..\logobg.jpg")
            If System.IO.File.Exists(logoPath) Then
                PictureBox1.Image = Image.FromFile(logoPath)
            Else
                ' Try PNG version
                logoPath = System.IO.Path.Combine(Application.StartupPath, "..\..\..\logo.png")
                If System.IO.File.Exists(logoPath) Then
                    PictureBox1.Image = Image.FromFile(logoPath)
                End If
            End If
        Catch ex As Exception
            ' Silent fail - logo tidak ditemukan
        End Try

        ' Load logo PNJ kanan
        Try
            Dim pnjPath As String = System.IO.Path.Combine(Application.StartupPath, "..\..\..\Logo_Politeknik_Negeri_Jakarta.jpg")
            If System.IO.File.Exists(pnjPath) Then
                PictureBox2.Image = Image.FromFile(pnjPath)
            Else
                ' Try PNG version
                pnjPath = System.IO.Path.Combine(Application.StartupPath, "..\..\..\logo_pnj.png")
                If System.IO.File.Exists(pnjPath) Then
                    PictureBox2.Image = Image.FromFile(pnjPath)
                End If
            End If
        Catch ex As Exception
            ' Silent fail - logo tidak ditemukan
        End Try
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

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub
End Class
