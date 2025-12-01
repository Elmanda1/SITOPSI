Imports MySql.Data.MySqlClient

Public Class ActivityLogForm
    Public Property ParentDashboard As DashboardAdmin

    Private Sub ActivityLogForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Or Not IsAdmin() Then
            MessageBox.Show("Akses ditolak!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(236, 240, 241)
        Me.Size = New Size(1100, 700)
        Me.Text = "Activity Logs - SITOPSI"
        
        BuildUI()
        LoadActivityLogs()
    End Sub

    Private Sub BuildUI()
        Me.Controls.Clear()

        ' Header Panel
        Dim headerPanel As New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 80,
            .BackColor = Color.FromArgb(155, 89, 182)
        }
        Me.Controls.Add(headerPanel)

        Dim lblTitle As New Label() With {
            .Text = "?? Activity Logs",
            .Font = New Font("Segoe UI", 16, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(20, 25)
        }
        headerPanel.Controls.Add(lblTitle)

        ' Back Button
        Dim btnBack As New Button() With {
            .Text = "? Kembali",
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .Size = New Size(120, 35),
            .Location = New Point(Me.ClientSize.Width - 140, 22),
            .BackColor = Color.FromArgb(52, 73, 94),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnBack.FlatAppearance.BorderSize = 0
        AddHandler btnBack.Click, AddressOf BtnBack_Click
        headerPanel.Controls.Add(btnBack)

        ' Main Content Panel
        Dim contentPanel As New Panel() With {
            .Location = New Point(0, 80),
            .Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height - 80),
            .BackColor = Color.FromArgb(236, 240, 241),
            .AutoScroll = True
        }
        Me.Controls.Add(contentPanel)

        ' Panel for DataGridView
        Dim gridPanel As New Panel() With {
            .Location = New Point(20, 20),
            .Size = New Size(1040, 560),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle
        }
        contentPanel.Controls.Add(gridPanel)

        ' Grid Title
        Dim lblGridTitle As New Label() With {
            .Text = "Riwayat Aktivitas",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Location = New Point(20, 20)
        }
        gridPanel.Controls.Add(lblGridTitle)

        ' Filter Panel
        Dim filterPanel As New Panel() With {
            .Location = New Point(20, 55),
            .Size = New Size(1000, 40),
            .BackColor = Color.Transparent
        }
        gridPanel.Controls.Add(filterPanel)

        ' Search Box
        Dim txtSearch As New TextBox() With {
            .Name = "txtSearch",
            .Font = New Font("Segoe UI", 9),
            .Size = New Size(200, 25),
            .Location = New Point(0, 8),
            .PlaceholderText = "Cari aktivitas..."
        }
        filterPanel.Controls.Add(txtSearch)
        AddHandler txtSearch.TextChanged, AddressOf TxtSearch_TextChanged

        ' Filter Aksi
        Dim lblFilter As New Label() With {
            .Text = "Filter Aksi:",
            .Font = New Font("Segoe UI", 9),
            .AutoSize = True,
            .Location = New Point(220, 12)
        }
        filterPanel.Controls.Add(lblFilter)

        Dim cmbFilterAksi As New ComboBox() With {
            .Name = "cmbFilterAksi",
            .Font = New Font("Segoe UI", 9),
            .Size = New Size(150, 25),
            .Location = New Point(295, 8),
            .DropDownStyle = ComboBoxStyle.DropDownList
        }
        cmbFilterAksi.Items.AddRange(New String() {"Semua", "Login", "Logout", "Register", "Tes Minat", "Generate Topik"})
        cmbFilterAksi.SelectedIndex = 0
        filterPanel.Controls.Add(cmbFilterAksi)
        AddHandler cmbFilterAksi.SelectedIndexChanged, AddressOf CmbFilterAksi_SelectedIndexChanged

        ' Refresh Button
        Dim btnRefresh As New Button() With {
            .Text = "?? Refresh",
            .Font = New Font("Segoe UI", 9),
            .Size = New Size(100, 30),
            .Location = New Point(460, 5),
            .BackColor = Color.FromArgb(52, 152, 219),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnRefresh.FlatAppearance.BorderSize = 0
        AddHandler btnRefresh.Click, AddressOf BtnRefresh_Click
        filterPanel.Controls.Add(btnRefresh)

        ' DataGridView
        Dim dgv As New DataGridView() With {
            .Name = "dgvLogs",
            .Location = New Point(20, 105),
            .Size = New Size(1000, 435),
            .BackgroundColor = Color.White,
            .BorderStyle = BorderStyle.None,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .ReadOnly = True,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            .RowHeadersVisible = False,
            .ColumnHeadersHeight = 40
        }
        gridPanel.Controls.Add(dgv)

        ' DataGridView Styling
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94)
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219)
        dgv.DefaultCellStyle.SelectionForeColor = Color.White
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 240, 241)
    End Sub

    Private Sub LoadActivityLogs(Optional filterAksi As String = "Semua", Optional searchText As String = "")
        Try
            Dim dgv As DataGridView = CType(Me.Controls.Find("dgvLogs", True)(0), DataGridView)
            
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT al.id AS 'ID', u.fullname AS 'User', al.aksi AS 'Aksi', al.detail AS 'Detail', al.waktu AS 'Waktu' FROM activity_logs al LEFT JOIN users u ON al.user_id = u.id WHERE 1=1"
                
                If filterAksi <> "Semua" Then
                    query &= " AND al.aksi LIKE @aksi"
                End If
                
                If Not String.IsNullOrWhiteSpace(searchText) Then
                    query &= " AND (u.fullname LIKE @search OR al.aksi LIKE @search OR al.detail LIKE @search)"
                End If
                
                query &= " ORDER BY al.waktu DESC LIMIT 1000"
                
                Using adapter As New MySqlDataAdapter(query, conn)
                    If filterAksi <> "Semua" Then
                        adapter.SelectCommand.Parameters.AddWithValue("@aksi", $"%{filterAksi}%")
                    End If
                    
                    If Not String.IsNullOrWhiteSpace(searchText) Then
                        adapter.SelectCommand.Parameters.AddWithValue("@search", $"%{searchText}%")
                    End If
                    
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    dgv.DataSource = dt
                    
                    ' Hide ID column
                    If dgv.Columns.Count > 0 Then
                        dgv.Columns("ID").Visible = False
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim txtSearch As TextBox = CType(sender, TextBox)
            Dim cmbFilterAksi As ComboBox = CType(Me.Controls.Find("cmbFilterAksi", True)(0), ComboBox)
            LoadActivityLogs(cmbFilterAksi.SelectedItem.ToString(), txtSearch.Text)
        Catch ex As Exception
            ' Silent error
        End Try
    End Sub

    Private Sub CmbFilterAksi_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim cmbFilterAksi As ComboBox = CType(sender, ComboBox)
            Dim txtSearch As TextBox = CType(Me.Controls.Find("txtSearch", True)(0), TextBox)
            LoadActivityLogs(cmbFilterAksi.SelectedItem.ToString(), txtSearch.Text)
        Catch ex As Exception
            ' Silent error
        End Try
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        LoadActivityLogs()
        MessageBox.Show("Data berhasil di-refresh!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        If ParentDashboard IsNot Nothing Then
            ParentDashboard.Show()
        End If
        Me.Close()
    End Sub

    Private Sub ActivityLogForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboard IsNot Nothing Then
            ParentDashboard.Show()
        End If
    End Sub
End Class
