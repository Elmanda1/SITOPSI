Imports MySql.Data.MySqlClient

Public Class ChangePassword
    Private Sub ChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set password character
        TextBox1.PasswordChar = "●"c
        TextBox3.PasswordChar = "●"c

        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Validasi input
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Username tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox2.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Password baru tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox1.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Verifikasi password tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox3.Focus()
            Return
        End If

        ' Validasi password minimal 5 karakter
        If TextBox1.Text.Length < 5 Then
            MessageBox.Show("Password minimal 5 karakter!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox1.Focus()
            Return
        End If

        ' Validasi password cocok
        If TextBox1.Text <> TextBox3.Text Then
            MessageBox.Show("Password baru dan verifikasi password tidak cocok!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox3.Clear()
            TextBox3.Focus()
            Return
        End If

        ' Proses ganti password
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' Cek apakah username ada
                Dim checkQuery As String = "SELECT COUNT(*) FROM users WHERE username = @username"
                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@username", TextBox2.Text.Trim())
                    Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                    If count = 0 Then
                        MessageBox.Show("Username tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If
                End Using

                ' Update password
                Dim updateQuery As String = "UPDATE users SET password = @password, updated_at = NOW() WHERE username = @username"
                Using updateCmd As New MySqlCommand(updateQuery, conn)
                    updateCmd.Parameters.AddWithValue("@password", TextBox1.Text)
                    updateCmd.Parameters.AddWithValue("@username", TextBox2.Text.Trim())

                    Dim result As Integer = updateCmd.ExecuteNonQuery()

                    If result > 0 Then
                        ' Log activity
                        Try
                            Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) " &
                                                    "VALUES ((SELECT id FROM users WHERE username = @username), @aksi, @detail, NOW())"
                            Using logCmd As New MySqlCommand(logQuery, conn)
                                logCmd.Parameters.AddWithValue("@username", TextBox2.Text.Trim())
                                logCmd.Parameters.AddWithValue("@aksi", "Change Password")
                                logCmd.Parameters.AddWithValue("@detail", $"User {TextBox2.Text.Trim()} mengganti password")
                                logCmd.ExecuteNonQuery()
                            End Using
                        Catch
                            ' Silent fail untuk log
                        End Try

                        MessageBox.Show("Password berhasil diganti! Silakan login dengan password baru.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' Kembali ke login form
                        Dim loginForm As New LoginForm()
                        loginForm.Show()
                        Me.Close()
                    Else
                        MessageBox.Show("Gagal mengganti password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show($"Error database: {ex.Message}", "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        ' Keep existing
    End Sub

    Private Sub ChangePassword_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Kembali ke login form
        Dim loginForm As New LoginForm()
        loginForm.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class