Imports MySql.Data.MySqlClient

Public Class ChangePassword
    ' Reference ke parent login form
    Public Property ParentLogin As LoginForm

    Private Sub ChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set password character
        TextBox2.PasswordChar = "●"c
        TextBox3.PasswordChar = "●"c

        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen

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
                Dim checkQuery As String = "SELECT id, fullname FROM users WHERE username = @username"
                Dim userId As Integer = 0
                Dim fullname As String = ""

                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@username", TextBox1.Text.Trim())

                    Using reader As MySqlDataReader = checkCmd.ExecuteReader()
                        If reader.Read() Then
                            userId = Convert.ToInt32(reader("id"))
                            fullname = reader("fullname").ToString()
                        Else
                            MessageBox.Show("Username tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return
                        End If
                    End Using
                End Using

                ' Update password
                Dim updateQuery As String = "UPDATE users SET password = @password, updated_at = NOW() WHERE id = @userId"
                Using updateCmd As New MySqlCommand(updateQuery, conn)
                    updateCmd.Parameters.AddWithValue("@password", TextBox2.Text)
                    updateCmd.Parameters.AddWithValue("@userId", userId)

                    Dim result As Integer = updateCmd.ExecuteNonQuery()

                    If result > 0 Then
                        ' Log activity
                        Try
                            Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, 'Change Password', @detail, NOW())"
                            Using logCmd As New MySqlCommand(logQuery, conn)
                                logCmd.Parameters.AddWithValue("@userId", userId)
                                logCmd.Parameters.AddWithValue("@detail", $"User {TextBox1.Text} berhasil mengganti password")
                                logCmd.ExecuteNonQuery()
                            End Using
                        Catch
                            ' Silent fail for activity log
                        End Try

                        MessageBox.Show($"Password berhasil diubah untuk {fullname}!" & vbCrLf & "Silakan login dengan password baru.", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' Kembali ke login form
                        Dim loginForm As New LoginForm()
                        loginForm.Show()
                        Me.Close()
                    Else
                        MessageBox.Show("Gagal mengubah password! Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show($"Error database: {ex.Message}", "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Kembali ke login form
        If ParentLogin IsNot Nothing Then
            ParentLogin.Show()
        Else
            Dim loginForm As New LoginForm()
            loginForm.Show()
        End If
        Me.Close()
    End Sub

    Private Sub ChangePassword_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Kembali ke login form
        If ParentLogin IsNot Nothing Then
            ParentLogin.Show()
        Else
            Dim loginForm As New LoginForm()
            loginForm.Show()
        End If
    End Sub
End Class