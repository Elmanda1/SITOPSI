Imports MySql.Data.MySqlClient

Public Class LoginForm
    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set password character
        TextBox2.PasswordChar = "●"c
        
        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Validasi input
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Username tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox1.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Password tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox2.Focus()
            Return
        End If

        ' Proses login
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' Query untuk get user data
                Dim query As String = "SELECT u.id, u.fullname, u.username, u.password, u.email, u.no_hp, " &
                                      "u.role_id, r.name as role_name, u.minat_bakat, u.status " &
                                      "FROM users u " &
                                      "INNER JOIN roles r ON u.role_id = r.id " &
                                      "WHERE u.username = @username"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@username", TextBox1.Text.Trim())

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' User ditemukan
                            Dim storedPassword As String = reader("password").ToString()
                            Dim inputPassword As String = TextBox2.Text
                            Dim userStatus As String = reader("status").ToString()

                            ' Cek status akun
                            If userStatus.ToLower() = "inactive" Then
                                MessageBox.Show("Akun Anda tidak aktif. Silahkan hubungi administrator.", "Akun Tidak Aktif", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return
                            ElseIf userStatus.ToLower() = "banned" Then
                                MessageBox.Show("Akun Anda telah diblokir. Silahkan hubungi administrator.", "Akun Diblokir", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Return
                            End If

                            ' Verifikasi password (untuk sementara direct comparison)
                            ' Untuk production gunakan BCrypt
                            Dim passwordMatch As Boolean = False
                            
                            ' Cek jika hash BCrypt dari PHP
                            If storedPassword.StartsWith("$2y$") Or storedPassword.StartsWith("$2a$") Then
                                ' Untuk admin default
                                If TextBox1.Text.Trim() = "admin" And inputPassword = "admin123" Then
                                    passwordMatch = True
                                Else
                                    MessageBox.Show("Password menggunakan enkripsi lama. Hubungi admin untuk reset password.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Return
                                End If
                            Else
                                ' Direct comparison untuk password plain atau yang sudah di-update
                                passwordMatch = (inputPassword = storedPassword)
                            End If

                            If passwordMatch Then
                                ' Set session
                                LoggedUserId = Convert.ToInt32(reader("id"))
                                LoggedUsername = reader("username").ToString()
                                LoggedFullName = reader("fullname").ToString()
                                LoggedEmail = reader("email").ToString()
                                LoggedRoleId = Convert.ToInt32(reader("role_id"))
                                LoggedRoleName = reader("role_name").ToString()
                                LoggedNoHP = If(IsDBNull(reader("no_hp")), "", reader("no_hp").ToString())
                                LoggedMinatBakat = If(IsDBNull(reader("minat_bakat")), "", reader("minat_bakat").ToString())

                                reader.Close()

                                ' Update last login
                                Dim updateQuery As String = "UPDATE users SET last_login = NOW() WHERE id = @userId"
                                Using updateCmd As New MySqlCommand(updateQuery, conn)
                                    updateCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                                    updateCmd.ExecuteNonQuery()
                                End Using

                                ' Log activity
                                Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, @aksi, @detail, NOW())"
                                Using logCmd As New MySqlCommand(logQuery, conn)
                                    logCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                                    logCmd.Parameters.AddWithValue("@aksi", "Login")
                                    logCmd.Parameters.AddWithValue("@detail", $"User {LoggedUsername} berhasil login")
                                    logCmd.ExecuteNonQuery()
                                End Using

                                MessageBox.Show($"Selamat datang, {LoggedFullName}!", "Login Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                ' Redirect berdasarkan role
                                If IsAdmin() Then
                                    Dim dashboardAdmin As New DashboardAdmin()
                                    dashboardAdmin.Show()
                                    Me.Close()
                                ElseIf IsMahasiswa() Then
                                    Dim dashboardUser As New DashboardUser()
                                    dashboardUser.Show()
                                    Me.Close()
                                End If
                            Else
                                MessageBox.Show("Username atau password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                TextBox2.Clear()
                                TextBox2.Focus()
                            End If
                        Else
                            ' User tidak ditemukan
                            MessageBox.Show("Username atau password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            TextBox2.Clear()
                            TextBox1.Focus()
                        End If
                    End Using
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show($"Error koneksi database: {ex.Message}", "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        ' Forgot Password - Buka form change password
        Dim changePasswordForm As New ChangePassword()
        changePasswordForm.Show()
        Me.Hide()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ' Keep existing
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        ' Keep existing
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        ' Login dengan Enter
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            Button1.PerformClick()
            e.Handled = True
        End If
    End Sub

    Private Sub LoginForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Kembali ke landing page
        Dim landingPage As New LandingPage()
        landingPage.Show()
    End Sub
End Class