Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class LoginForm
    ' Reference ke parent form (Landing Page)
    Public Property ParentLanding As LandingPage

    ' Colors from the palette
    Private ReadOnly navyColor As Color = Color.FromArgb(12, 45, 72)
    Private ReadOnly lightNavyColor As Color = Color.FromArgb(28, 68, 105)
    Private ReadOnly whiteColor As Color = Color.White

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Private Sub loginButton_Click(sender As Object, e As EventArgs) Handles loginButton.Click
        ' Validasi input
        If String.IsNullOrWhiteSpace(usernameTextBox.Text) Then
            MessageBox.Show("Username tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            usernameTextBox.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(passwordTextBox.Text) Then
            MessageBox.Show("Password tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            passwordTextBox.Focus()
            Return
        End If

        ' Proses login
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' Query untuk get user data
                Dim query As String = "SELECT u.user_id, u.fullname, u.username, u.password, u.email, u.no_hp, " &
                                      "u.role_id, r.name as role_name, u.minat_bakat, u.status " &
                                      "FROM users u " &
                                      "INNER JOIN roles r ON u.role_id = r.role_id " &
                                      "WHERE u.username = @username"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@username", usernameTextBox.Text.Trim())

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' User ditemukan
                            Dim storedPassword As String = reader("password").ToString()
                            Dim inputPassword As String = passwordTextBox.Text
                            Dim userStatus As String = reader("status").ToString()

                            ' Cek status akun
                            If userStatus.ToLower() = "inactive" Then
                                MessageBox.Show("Akun Anda tidak aktif. Silahkan hubungi administrator.", "Akun Tidak Aktif", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return
                            ElseIf userStatus.ToLower() = "banned" Then
                                MessageBox.Show("Akun Anda telah diblokir. Silahkan hubungi administrator.", "Akun Diblokir", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Return
                            End If

                            ' Verifikasi password (Direct comparison)
                            Dim passwordMatch As Boolean = (inputPassword = storedPassword)

                            If passwordMatch Then
                                ' Set session
                                LoggedUserId = Convert.ToInt32(reader("user_id"))
                                LoggedUsername = reader("username").ToString()
                                LoggedFullName = reader("fullname").ToString()
                                LoggedEmail = reader("email").ToString()
                                LoggedRoleId = Convert.ToInt32(reader("role_id"))
                                LoggedRoleName = reader("role_name").ToString()
                                LoggedNoHP = If(IsDBNull(reader("no_hp")), "", reader("no_hp").ToString())
                                LoggedMinatBakat = If(IsDBNull(reader("minat_bakat")), "", reader("minat_bakat").ToString())

                                reader.Close()

                                ' Update last login
                                Dim updateQuery As String = "UPDATE users SET last_login = NOW() WHERE user_id = @userId"
                                Using updateCmd As New MySqlCommand(updateQuery, conn)
                                    updateCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                                    updateCmd.ExecuteNonQuery()
                                End Using

                                ' Log activity
                                Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, @aksi, @detail, NOW())"
                                Using logCmd As New MySqlCommand(logQuery, conn)
                                    logCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                                    logCmd.Parameters.AddWithValue("@aksi", "Login")
                                    logCmd.Parameters.AddWithValue("@detail", "User " & LoggedUsername & " berhasil login")
                                    logCmd.ExecuteNonQuery()
                                End Using

                                MessageBox.Show("Selamat datang, " & LoggedFullName & "!", "Login Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                
                                ' Redirect berdasarkan role
                                If IsAdmin() Then
                                    Dim dashboardAdmin As New DashboardAdmin()
                                    dashboardAdmin.ParentLanding = Me.ParentLanding
                                    dashboardAdmin.Show()
                                ElseIf IsMahasiswa() Then
                                    Dim dashboardUser As New DashboardUser()
                                    dashboardUser.ParentLanding = Me.ParentLanding
                                    dashboardUser.Show()
                                End If

                                Me.Close() ' Close this login form

                            Else
                                MessageBox.Show("Username atau password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                passwordTextBox.Clear()
                                passwordTextBox.Focus()
                            End If
                        Else
                            ' User tidak ditemukan
                            MessageBox.Show("Username atau password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            passwordTextBox.Clear()
                            usernameTextBox.Focus()
                        End If
                    End Using
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show("Error koneksi database: " & ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub backButton_Click(sender As Object, e As EventArgs) Handles backButton.Click
        ' Kembali ke landing page
        If ParentLanding IsNot Nothing Then
            ParentLanding.Show()
        Else
            Dim landingPage As New LandingPage()
            landingPage.Show()
        End If
        Me.Close()
    End Sub

    Private Sub forgotPasswordLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles forgotPasswordLink.LinkClicked
        ' Forgot Password - Buka form change password
        Dim changePasswordForm As New ChangePassword()
        changePasswordForm.ParentLogin = Me
        changePasswordForm.Show()
        Me.Hide()
    End Sub

    Private Sub passwordTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles passwordTextBox.KeyPress
        ' Login dengan Enter
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            loginButton.PerformClick()
            e.Handled = True
        End If
    End Sub

    Private Sub LoginForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Hanya tampilkan landing page jika form ditutup tanpa berhasil login
        If Not IsLoggedIn() Then
            If ParentLanding IsNot Nothing AndAlso Not ParentLanding.IsDisposed Then
                ParentLanding.Show()
            ElseIf ParentLanding Is Nothing Then
                Dim landingPage As New LandingPage()
                landingPage.Show()
            End If
        End If
    End Sub

    ' --- UI Interaction Handlers ---

    Private Sub loginButton_MouseEnter(sender As Object, e As EventArgs) Handles loginButton.MouseEnter
        loginButton.BackColor = lightNavyColor
    End Sub

    Private Sub loginButton_MouseLeave(sender As Object, e As EventArgs) Handles loginButton.MouseLeave
        loginButton.BackColor = navyColor
    End Sub

    Private Sub backButton_MouseEnter(sender As Object, e As EventArgs) Handles backButton.MouseEnter
        backButton.BackColor = Color.FromArgb(236, 240, 241) ' Light grey
    End Sub

    Private Sub backButton_MouseLeave(sender As Object, e As EventArgs) Handles backButton.MouseLeave
        backButton.BackColor = whiteColor
    End Sub
End Class