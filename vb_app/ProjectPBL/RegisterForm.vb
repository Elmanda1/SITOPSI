Imports MySql.Data.MySqlClient

Public Class RegisterForm
    Private Sub RegisterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Set default radio button
        RadioButton1.Checked = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Validasi input
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Nama lengkap tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox1.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("No. Telepon tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox2.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Email tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox3.Focus()
            Return
        End If

        ' Validasi format email
        If Not TextBox3.Text.Contains("@") Or Not TextBox3.Text.Contains(".") Then
            MessageBox.Show("Format email tidak valid!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox3.Focus()
            Return
        End If

        ' Validasi gender
        If Not RadioButton1.Checked And Not RadioButton2.Checked Then
            MessageBox.Show("Pilih jenis kelamin!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Generate username dari email (bagian sebelum @)
        Dim generatedUsername As String = TextBox3.Text.Split("@"c)(0).ToLower()

        ' Generate password default (bisa diubah nanti)
        Dim defaultPassword As String = "12345"

        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' Cek apakah username sudah ada
                Dim checkQuery As String = "SELECT COUNT(*) FROM users WHERE username = @username"
                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@username", generatedUsername)
                    Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                    If count > 0 Then
                        ' Username sudah ada, tambahkan angka random
                        Dim rnd As New Random()
                        generatedUsername = generatedUsername & rnd.Next(100, 999).ToString()
                    End If
                End Using

                ' Cek apakah email sudah terdaftar
                Dim checkEmailQuery As String = "SELECT COUNT(*) FROM users WHERE email = @email"
                Using checkEmailCmd As New MySqlCommand(checkEmailQuery, conn)
                    checkEmailCmd.Parameters.AddWithValue("@email", TextBox3.Text.Trim())
                    Dim emailCount As Integer = Convert.ToInt32(checkEmailCmd.ExecuteScalar())

                    If emailCount > 0 Then
                        MessageBox.Show("Email sudah terdaftar! Gunakan email lain atau login.", "Registrasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                ' Insert user baru (role_id = 2 untuk mahasiswa)
                Dim insertQuery As String = "INSERT INTO users (fullname, username, password, email, no_hp, role_id, status, created_at) " &
                                           "VALUES (@fullname, @username, @password, @email, @no_hp, 2, 'active', NOW())"

                Using insertCmd As New MySqlCommand(insertQuery, conn)
                    insertCmd.Parameters.AddWithValue("@fullname", TextBox1.Text.Trim())
                    insertCmd.Parameters.AddWithValue("@username", generatedUsername)
                    insertCmd.Parameters.AddWithValue("@password", defaultPassword)
                    insertCmd.Parameters.AddWithValue("@email", TextBox3.Text.Trim())
                    insertCmd.Parameters.AddWithValue("@no_hp", TextBox2.Text.Trim())

                    Dim result As Integer = insertCmd.ExecuteNonQuery()

                    If result > 0 Then
                        MessageBox.Show($"Registrasi berhasil!" & vbCrLf & vbCrLf &
                                      $"Username: {generatedUsername}" & vbCrLf &
                                      $"Password: {defaultPassword}" & vbCrLf & vbCrLf &
                                      "Silakan login dan ganti password Anda!",
                                      "Registrasi Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' Kembali ke login form
                        Dim loginForm As New LoginForm()
                        loginForm.Show()
                        Me.Close()
                    Else
                        MessageBox.Show("Registrasi gagal! Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show($"Error database: {ex.Message}", "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ' Keep existing
    End Sub

    Private Sub RegisterForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Kembali ke landing page
        Dim landingPage As New LandingPage()
        landingPage.Show()
    End Sub
End Class