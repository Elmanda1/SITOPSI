Imports MySql.Data.MySqlClient

Public Class UserManagementForm
    Public Property ParentDashboardAdmin As DashboardAdmin

    Private ReadOnly navyColor As Color = Color.FromArgb(44, 62, 80)
    Private ReadOnly lightNavyColor As Color = Color.FromArgb(52, 73, 94)
    Private ReadOnly whiteColor As Color = Color.White
    Private ReadOnly lightGreyHoverColor As Color = Color.FromArgb(236, 240, 241)

    Private Sub UserManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Or Not IsAdmin() Then
            MessageBox.Show("Akses ditolak!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        ' Add Handlers
        AddHandler txtSearch.TextChanged, AddressOf TxtSearch_TextChanged
        AddHandler dgvUsers.CellDoubleClick, AddressOf DgvUsers_CellDoubleClick
        AddHandler btnRefresh.Click, AddressOf BtnRefresh_Click
        AddHandler btnBack.Click, AddressOf BtnBack_Click

        AddHandler btnRefresh.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnRefresh.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnBack.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnBack.MouseLeave, AddressOf Button_MouseLeave

        LoadUsersData()
    End Sub

    Private Sub LoadUsersData(Optional filter As String = "")
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT u.user_id AS 'ID', u.fullname AS 'Nama Lengkap', u.username AS 'Username', " &
                                     "u.email AS 'Email', r.name AS 'Role', u.minat_bakat AS 'Minat Bakat', " &
                                     "u.status AS 'Status', u.created_at AS 'Terdaftar' " &
                                     "FROM users u " &
                                     "INNER JOIN roles r ON u.role_id = r.role_id "
                
                If Not String.IsNullOrWhiteSpace(filter) Then
                    query &= "WHERE u.fullname LIKE @search OR u.username LIKE @search OR u.email LIKE @search "
                End If

                query &= "ORDER BY u.created_at DESC"
                
                Using adapter As New MySqlDataAdapter(query, conn)
                    If Not String.IsNullOrWhiteSpace(filter) Then
                        adapter.SelectCommand.Parameters.AddWithValue("@search", "%" & filter & "%")
                    End If

                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    dgvUsers.DataSource = dt
                    
                    If dgvUsers.Columns.Count > 0 AndAlso dgvUsers.Columns.Contains("ID") Then
                        dgvUsers.Columns("ID").Visible = False
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memuat data user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs)
        LoadUsersData(txtSearch.Text)
    End Sub

    Private Sub DgvUsers_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Try
                Dim row As DataGridViewRow = dgvUsers.Rows(e.RowIndex)
                Dim status As String = row.Cells("Status").Value.ToString()
                Dim message As String = "Detail User:" & vbCrLf & vbCrLf &
                    "Nama: " & row.Cells("Nama Lengkap").Value.ToString() & vbCrLf &
                    "Username: " & row.Cells("Username").Value.ToString() & vbCrLf &
                    "Email: " & row.Cells("Email").Value.ToString() & vbCrLf &
                    "Role: " & row.Cells("Role").Value.ToString() & vbCrLf &
                    "Status: " & status & vbCrLf &
                    "Minat Bakat: " & If(IsDBNull(row.Cells("Minat Bakat").Value), "Belum tes", row.Cells("Minat Bakat").Value.ToString())
                
                MessageBox.Show(message, "Detail User", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error menampilkan detail: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        txtSearch.Clear()
        LoadUsersData()
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub UserManagementForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboardAdmin IsNot Nothing AndAlso Not ParentDashboardAdmin.IsDisposed Then
            ParentDashboardAdmin.Show()
        End If
    End Sub

    Private Sub Button_MouseEnter(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        If btn Is btnRefresh Then
            btn.BackColor = lightNavyColor
        Else ' btnBack
            btn.BackColor = lightGreyHoverColor
        End If
    End Sub

    Private Sub Button_MouseLeave(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        If btn Is btnRefresh Then
            btn.BackColor = navyColor
        Else ' btnBack
            btn.BackColor = whiteColor
        End If
    End Sub
End Class
