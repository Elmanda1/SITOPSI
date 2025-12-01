Imports MySql.Data.MySqlClient

Public Class UserManagementForm
    Public Property ParentDashboard As DashboardAdmin

    Private Sub UserManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Or Not IsAdmin() Then
            MessageBox.Show("Akses ditolak!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(236, 240, 241)
        Me.Size = New Size(1100, 700)
        Me.Text = "Manajemen User - SITOPSI"
        
        BuildUI()
        LoadUsersData()
    End Sub

    Private Sub BuildUI()
        Me.Controls.Clear()

        ' Header Panel
        Dim headerPanel As New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 80,
            .BackColor = Color.FromArgb(41, 128, 185)
        }
        Me.Controls.Add(headerPanel)

        Dim lblTitle As New Label() With {
            .Text = "?? Manajemen User",
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
            .Text = "Daftar User",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Location = New Point(20, 20)
        }
        gridPanel.Controls.Add(lblGridTitle)

        ' Search Box
        Dim txtSearch As New TextBox() With {
            .Name = "txtSearch",
            .Font = New Font("Segoe UI", 9),
            .Size = New Size(250, 25),
            .Location = New Point(20, 55),
            .PlaceholderText = "Cari nama atau username..."
        }
        gridPanel.Controls.Add(txtSearch)
        AddHandler txtSearch.TextChanged, AddressOf TxtSearch_TextChanged

        ' DataGridView
        Dim dgv As New DataGridView() With {
            .Name = "dgvUsers",
            .Location = New Point(20, 90),
            .Size = New Size(1000, 450),
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

        AddHandler dgv.CellDoubleClick, AddressOf DgvUsers_CellDoubleClick
    End Sub

    Private Sub LoadUsersData()
        Try
            Dim dgv As DataGridView = CType(Me.Controls.Find("dgvUsers", True)(0), DataGridView)
            
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT u.id AS 'ID', u.fullname AS 'Nama Lengkap', u.username AS 'Username', u.email AS 'Email', r.name AS 'Role', u.minat_bakat AS 'Minat Bakat', u.status AS 'Status', u.created_at AS 'Terdaftar' FROM users u INNER JOIN roles r ON u.role_id = r.id ORDER BY u.created_at DESC"
                
                Using adapter As New MySqlDataAdapter(query, conn)
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
            Dim dgv As DataGridView = CType(Me.Controls.Find("dgvUsers", True)(0), DataGridView)
            
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT u.id AS 'ID', u.fullname AS 'Nama Lengkap', u.username AS 'Username', u.email AS 'Email', r.name AS 'Role', u.minat_bakat AS 'Minat Bakat', u.status AS 'Status', u.created_at AS 'Terdaftar' FROM users u INNER JOIN roles r ON u.role_id = r.id WHERE u.fullname LIKE @search OR u.username LIKE @search OR u.email LIKE @search ORDER BY u.created_at DESC"
                
                Using adapter As New MySqlDataAdapter(query, conn)
                    adapter.SelectCommand.Parameters.AddWithValue("@search", $"%{txtSearch.Text}%")
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    dgv.DataSource = dt
                    
                    If dgv.Columns.Count > 0 Then
                        dgv.Columns("ID").Visible = False
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Silent error for search
        End Try
    End Sub

    Private Sub DgvUsers_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Try
                Dim dgv As DataGridView = CType(sender, DataGridView)
                Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)
                
                Dim userId As Integer = Convert.ToInt32(row.Cells("ID").Value)
                Dim username As String = row.Cells("Username").Value.ToString()
                Dim fullname As String = row.Cells("Nama Lengkap").Value.ToString()
                Dim status As String = row.Cells("Status").Value.ToString()
                
                Dim message As String = $"Detail User:{vbCrLf}{vbCrLf}" &
                    $"Nama: {fullname}{vbCrLf}" &
                    $"Username: {username}{vbCrLf}" &
                    $"Email: {row.Cells("Email").Value}{vbCrLf}" &
                    $"Role: {row.Cells("Role").Value}{vbCrLf}" &
                    $"Status: {status}{vbCrLf}" &
                    $"Minat Bakat: {If(IsDBNull(row.Cells("Minat Bakat").Value), "Belum tes", row.Cells("Minat Bakat").Value)}"
                
                MessageBox.Show(message, "Detail User", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        If ParentDashboard IsNot Nothing Then
            ParentDashboard.Show()
        End If
        Me.Close()
    End Sub

    Private Sub UserManagementForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboard IsNot Nothing Then
            ParentDashboard.Show()
        End If
    End Sub
End Class
