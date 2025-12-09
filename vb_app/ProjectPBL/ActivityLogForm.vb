Imports MySql.Data.MySqlClient

Public Class ActivityLogForm
    Public Property ParentDashboardAdmin As DashboardAdmin

    Private ReadOnly navyColor As Color = Color.FromArgb(44, 62, 80)
    Private ReadOnly lightNavyColor As Color = Color.FromArgb(52, 73, 94)
    Private ReadOnly whiteColor As Color = Color.White
    Private ReadOnly lightGreyHoverColor As Color = Color.FromArgb(236, 240, 241)

    Private Sub ActivityLogForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Or Not IsAdmin() Then
            MessageBox.Show("Akses ditolak!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        ' Add Handlers
        AddHandler btnBack.Click, AddressOf BtnBack_Click
        AddHandler btnRefresh.Click, AddressOf BtnRefresh_Click
        AddHandler txtSearch.TextChanged, AddressOf TxtSearch_TextChanged
        AddHandler cmbFilterAksi.SelectedIndexChanged, AddressOf CmbFilterAksi_SelectedIndexChanged

        ' Hover Handlers
        AddHandler btnBack.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnBack.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnRefresh.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnRefresh.MouseLeave, AddressOf Button_MouseLeave

        ' Initial Load
        LoadFilterOptions()
        LoadActivityLogs()
    End Sub

#Region "Data Loading"
    Private Sub LoadFilterOptions()
        cmbFilterAksi.Items.Clear()
        cmbFilterAksi.Items.Add("Semua")
        cmbFilterAksi.Items.Add("Login")
        cmbFilterAksi.Items.Add("Logout")
        cmbFilterAksi.Items.Add("Register")
        cmbFilterAksi.Items.Add("Tes Minat")
        cmbFilterAksi.Items.Add("Generate Topik")
        cmbFilterAksi.SelectedIndex = 0
    End Sub

    Private Sub LoadActivityLogs(Optional filterAksi As String = "Semua", Optional searchText As String = "")
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT al.log_id AS 'ID', u.fullname AS 'User', al.aksi AS 'Aksi', al.detail AS 'Detail', al.waktu AS 'Waktu' FROM activity_logs al LEFT JOIN users u ON al.user_id = u.user_id WHERE 1=1"
                
                If filterAksi <> "Semua" Then
                    query &= " AND al.aksi = @aksi"
                End If
                
                If Not String.IsNullOrWhiteSpace(searchText) Then
                    query &= " AND (u.fullname LIKE @search OR al.aksi LIKE @search OR al.detail LIKE @search)"
                End If
                
                query &= " ORDER BY al.waktu DESC LIMIT 1000"
                
                Using cmd As New MySqlCommand(query, conn)
                    If filterAksi <> "Semua" Then
                        cmd.Parameters.AddWithValue("@aksi", filterAksi)
                    End If
                    
                    If Not String.IsNullOrWhiteSpace(searchText) Then
                        cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                    End If
                    
                    Using adapter As New MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        dgvLogs.DataSource = dt
                        
                        If dgvLogs.Columns.Count > 0 AndAlso dgvLogs.Columns.Contains("ID") Then
                            dgvLogs.Columns("ID").Visible = False
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memuat log aktivitas: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Form and Button Logic"
    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs)
        LoadActivityLogs(cmbFilterAksi.SelectedItem.ToString(), txtSearch.Text)
    End Sub

    Private Sub CmbFilterAksi_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadActivityLogs(cmbFilterAksi.SelectedItem.ToString(), txtSearch.Text)
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        txtSearch.Clear()
        cmbFilterAksi.SelectedIndex = 0
        LoadActivityLogs()
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub ActivityLogForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboardAdmin IsNot Nothing AndAlso Not ParentDashboardAdmin.IsDisposed Then
            ParentDashboardAdmin.Show()
        End If
    End Sub

    ' --- Hover Handlers ---
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

#End Region

End Class