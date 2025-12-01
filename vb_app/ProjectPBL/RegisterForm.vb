Imports MySql.Data.MySqlClient

Public Class RegisterForm
    Private isRegistrationSuccess As Boolean = False

    Private Sub RegisterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Set default radio button
        RadioButton1.Checked = True

        ' Set password character untuk TextBox5
        TextBox5.PasswordChar = "●"c

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

        ' Validasi username
        If String.IsNullOrWhiteSpace(TextBox4.Text) Then
            MessageBox.Show("Username tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox4.Focus()
            Return
        End If

        ' Validasi password
        If String.IsNullOrWhiteSpace(TextBox5.Text) Then
            MessageBox.Show("Password tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox5.Focus()
            Return
        End If

        ' Validasi panjang password minimal 5 karakter
        If TextBox5.Text.Length < 5 Then
            MessageBox.Show("Password minimal 5 karakter!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox5.Focus()
            Return
        End If

        ' Validasi gender
        If Not RadioButton1.Checked And Not RadioButton2.Checked Then
            MessageBox.Show("Pilih jenis kelamin!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' Cek apakah username sudah ada
                Dim checkQuery As String = "SELECT COUNT(*) FROM users WHERE username = @username"
                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@username", TextBox4.Text.Trim())
                    Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                    If count > 0 Then
                        MessageBox.Show("Username sudah digunakan! Pilih username lain.", "Registrasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        TextBox4.Focus()
                        Return
                    End If
                End Using

                ' Cek apakah email sudah terdaftar
                Dim checkEmailQuery As String = "SELECT COUNT(*) FROM users WHERE email = @email"
                Using checkEmailCmd As New MySqlCommand(checkEmailQuery, conn)
                    checkEmailCmd.Parameters.AddWithValue("@email", TextBox3.Text.Trim())
                    Dim emailCount As Integer = Convert.ToInt32(checkEmailCmd.ExecuteScalar())

                    If emailCount > 0 Then
                        MessageBox.Show("Email sudah terdaftar! Gunakan email lain atau login.", "Registrasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        TextBox3.Focus()
                        Return
                    End If
                End Using

                ' Insert user baru (role_id = 2 untuk mahasiswa)
                Dim insertQuery As String = "INSERT INTO users (fullname, username, password, email, no_hp, role_id, status, created_at) " &
                                           "VALUES (@fullname, @username, @password, @email, @no_hp, 2, 'active', NOW())"

                Using insertCmd As New MySqlCommand(insertQuery, conn)
                    insertCmd.Parameters.AddWithValue("@fullname", TextBox1.Text.Trim())
                    insertCmd.Parameters.AddWithValue("@username", TextBox4.Text.Trim())
                    insertCmd.Parameters.AddWithValue("@password", TextBox5.Text)
                    insertCmd.Parameters.AddWithValue("@email", TextBox3.Text.Trim())
                    insertCmd.Parameters.AddWithValue("@no_hp", TextBox2.Text.Trim())

                    Dim result As Integer = insertCmd.ExecuteNonQuery()

                    If result > 0 Then
                        isRegistrationSuccess = True

                        MessageBox.Show($"Registrasi berhasil!" & vbCrLf & vbCrLf &
                                      $"Username: {TextBox4.Text.Trim()}" & vbCrLf &
                                      "Silakan login dengan akun Anda!",
                                      "Registrasi Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' Langsung ke login form
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Kembali ke landing page
        Dim landingPage As New LandingPage()
        landingPage.Show()
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ' Keep existing
    End Sub

    Private Sub RegisterForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Hanya tampilkan landing page jika tidak berhasil register
        If Not isRegistrationSuccess Then
            Dim landingPage As New LandingPage()
            landingPage.Show()
        End If
    End Sub
End Class