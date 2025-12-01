Imports MySql.Data.MySqlClient

Public Class RulesManagementForm
    Public Property ParentDashboard As DashboardAdmin
    Private currentRuleId As Integer = 0

    Private Sub RulesManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Or Not IsAdmin() Then
            MessageBox.Show("Akses ditolak!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(236, 240, 241)
        Me.Size = New Size(1000, 700)
        Me.Text = "Manajemen Rules/Kategori - SITOPSI"
        
        BuildUI()
        LoadRulesData()
    End Sub

    Private Sub BuildUI()
        Me.Controls.Clear()

        ' Header Panel
        Dim headerPanel As New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 80,
            .BackColor = Color.FromArgb(231, 76, 60)
        }
        Me.Controls.Add(headerPanel)

        Dim lblTitle As New Label() With {
            .Text = "?? Manajemen Rules/Kategori",
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

        ' Left Panel - Form Input
        Dim leftPanel As New Panel() With {
            .Location = New Point(20, 20),
            .Size = New Size(400, 550),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle
        }
        contentPanel.Controls.Add(leftPanel)

        ' Form Title
        Dim lblFormTitle As New Label() With {
            .Text = "Form Tambah/Edit Kategori",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Location = New Point(20, 20)
        }
        leftPanel.Controls.Add(lblFormTitle)

        ' Kode Kategori
        Dim lblKode As New Label() With {
            .Text = "Kode Kategori (1 Karakter):",
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point(20, 70)
        }
        leftPanel.Controls.Add(lblKode)

        Dim txtKode As New TextBox() With {
            .Name = "txtKode",
            .Font = New Font("Segoe UI", 10),
            .Size = New Size(360, 30),
            .Location = New Point(20, 95),
            .MaxLength = 1
        }
        leftPanel.Controls.Add(txtKode)

        ' Nama Kategori
        Dim lblNama As New Label() With {
            .Text = "Nama Kategori:",
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point(20, 140)
        }
        leftPanel.Controls.Add(lblNama)

        Dim txtNama As New TextBox() With {
            .Name = "txtNama",
            .Font = New Font("Segoe UI", 10),
            .Size = New Size(360, 30),
            .Location = New Point(20, 165)
        }
        leftPanel.Controls.Add(txtNama)

        ' Deskripsi
        Dim lblDeskripsi As New Label() With {
            .Text = "Deskripsi:",
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point(20, 210)
        }
        leftPanel.Controls.Add(lblDeskripsi)

        Dim txtDeskripsi As New TextBox() With {
            .Name = "txtDeskripsi",
            .Font = New Font("Segoe UI", 10),
            .Size = New Size(360, 100),
            .Location = New Point(20, 235),
            .Multiline = True,
            .ScrollBars = ScrollBars.Vertical
        }
        leftPanel.Controls.Add(txtDeskripsi)

        ' Buttons Panel
        Dim btnPanel As New Panel() With {
            .Location = New Point(20, 360),
            .Size = New Size(360, 50)
        }
        leftPanel.Controls.Add(btnPanel)

        ' Save Button
        Dim btnSave As New Button() With {
            .Name = "btnSave",
            .Text = "?? Simpan",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .Size = New Size(110, 40),
            .Location = New Point(0, 0),
            .BackColor = Color.FromArgb(46, 204, 113),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnSave.FlatAppearance.BorderSize = 0
        AddHandler btnSave.Click, AddressOf BtnSave_Click
        btnPanel.Controls.Add(btnSave)

        ' Update Button
        Dim btnUpdate As New Button() With {
            .Name = "btnUpdate",
            .Text = "?? Update",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .Size = New Size(110, 40),
            .Location = New Point(120, 0),
            .BackColor = Color.FromArgb(52, 152, 219),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand,
            .Visible = False
        }
        btnUpdate.FlatAppearance.BorderSize = 0
        AddHandler btnUpdate.Click, AddressOf BtnUpdate_Click
        btnPanel.Controls.Add(btnUpdate)

        ' Clear Button
        Dim btnClear As New Button() With {
            .Name = "btnClear",
            .Text = "?? Clear",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .Size = New Size(110, 40),
            .Location = New Point(240, 0),
            .BackColor = Color.FromArgb(149, 165, 166),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnClear.FlatAppearance.BorderSize = 0
        AddHandler btnClear.Click, AddressOf BtnClear_Click
        btnPanel.Controls.Add(btnClear)

        ' Right Panel - DataGridView
        Dim rightPanel As New Panel() With {
            .Location = New Point(440, 20),
            .Size = New Size(520, 550),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle
        }
        contentPanel.Controls.Add(rightPanel)

        ' Grid Title
        Dim lblGridTitle As New Label() With {
            .Text = "Daftar Kategori",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Location = New Point(20, 20)
        }
        rightPanel.Controls.Add(lblGridTitle)

        ' DataGridView
        Dim dgv As New DataGridView() With {
            .Name = "dgvRules",
            .Location = New Point(20, 60),
            .Size = New Size(480, 470),
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
        rightPanel.Controls.Add(dgv)

        ' DataGridView Styling
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94)
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219)
        dgv.DefaultCellStyle.SelectionForeColor = Color.White
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 240, 241)

        AddHandler dgv.CellDoubleClick, AddressOf DgvRules_CellDoubleClick
    End Sub

    Private Sub LoadRulesData()
        Try
            Dim dgv As DataGridView = CType(Me.Controls.Find("dgvRules", True)(0).Parent.Controls.Find("dgvRules", True)(0), DataGridView)
            
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT id AS 'ID', kode AS 'Kode', nama AS 'Nama Kategori', deskripsi AS 'Deskripsi' FROM categories ORDER BY kode"
                
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

    Private Sub BtnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim txtKode As TextBox = CType(Me.Controls.Find("txtKode", True)(0), TextBox)
            Dim txtNama As TextBox = CType(Me.Controls.Find("txtNama", True)(0), TextBox)
            Dim txtDeskripsi As TextBox = CType(Me.Controls.Find("txtDeskripsi", True)(0), TextBox)

            ' Validation
            If String.IsNullOrWhiteSpace(txtKode.Text) Then
                MessageBox.Show("Kode kategori harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtKode.Focus()
                Return
            End If

            If txtKode.Text.Length <> 1 Then
                MessageBox.Show("Kode kategori harus 1 karakter!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtKode.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(txtNama.Text) Then
                MessageBox.Show("Nama kategori harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtNama.Focus()
                Return
            End If

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                
                ' Check if kode already exists
                Dim checkQuery As String = "SELECT COUNT(*) FROM categories WHERE kode = @kode"
                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@kode", txtKode.Text.ToUpper())
                    Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                    
                    If count > 0 Then
                        MessageBox.Show("Kode kategori sudah ada!", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                Dim query As String = "INSERT INTO categories (kode, nama, deskripsi) VALUES (@kode, @nama, @deskripsi)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@kode", txtKode.Text.ToUpper())
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text)
                    cmd.Parameters.AddWithValue("@deskripsi", If(String.IsNullOrWhiteSpace(txtDeskripsi.Text), DBNull.Value, txtDeskripsi.Text))
                    
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Kategori berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    
                    BtnClear_Click(Nothing, Nothing)
                    LoadRulesData()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs)
        Try
            Dim txtKode As TextBox = CType(Me.Controls.Find("txtKode", True)(0), TextBox)
            Dim txtNama As TextBox = CType(Me.Controls.Find("txtNama", True)(0), TextBox)
            Dim txtDeskripsi As TextBox = CType(Me.Controls.Find("txtDeskripsi", True)(0), TextBox)

            ' Validation
            If String.IsNullOrWhiteSpace(txtNama.Text) Then
                MessageBox.Show("Nama kategori harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtNama.Focus()
                Return
            End If

            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin mengupdate kategori ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            
            If result = DialogResult.Yes Then
                Using conn As New MySqlConnection(ConnectionString)
                    conn.Open()
                    
                    Dim query As String = "UPDATE categories SET nama = @nama, deskripsi = @deskripsi WHERE id = @id"
                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@id", currentRuleId)
                        cmd.Parameters.AddWithValue("@nama", txtNama.Text)
                        cmd.Parameters.AddWithValue("@deskripsi", If(String.IsNullOrWhiteSpace(txtDeskripsi.Text), DBNull.Value, txtDeskripsi.Text))
                        
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("Kategori berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        
                        BtnClear_Click(Nothing, Nothing)
                        LoadRulesData()
                    End Using
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs)
        Dim txtKode As TextBox = CType(Me.Controls.Find("txtKode", True)(0), TextBox)
        Dim txtNama As TextBox = CType(Me.Controls.Find("txtNama", True)(0), TextBox)
        Dim txtDeskripsi As TextBox = CType(Me.Controls.Find("txtDeskripsi", True)(0), TextBox)
        Dim btnSave As Button = CType(Me.Controls.Find("btnSave", True)(0), Button)
        Dim btnUpdate As Button = CType(Me.Controls.Find("btnUpdate", True)(0), Button)

        txtKode.Text = ""
        txtNama.Text = ""
        txtDeskripsi.Text = ""
        txtKode.Enabled = True
        btnSave.Visible = True
        btnUpdate.Visible = False
        currentRuleId = 0
    End Sub

    Private Sub DgvRules_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Try
                Dim dgv As DataGridView = CType(sender, DataGridView)
                Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)
                
                currentRuleId = Convert.ToInt32(row.Cells("ID").Value)
                
                Dim txtKode As TextBox = CType(Me.Controls.Find("txtKode", True)(0), TextBox)
                Dim txtNama As TextBox = CType(Me.Controls.Find("txtNama", True)(0), TextBox)
                Dim txtDeskripsi As TextBox = CType(Me.Controls.Find("txtDeskripsi", True)(0), TextBox)
                Dim btnSave As Button = CType(Me.Controls.Find("btnSave", True)(0), Button)
                Dim btnUpdate As Button = CType(Me.Controls.Find("btnUpdate", True)(0), Button)

                txtKode.Text = row.Cells("Kode").Value.ToString()
                txtNama.Text = row.Cells("Nama Kategori").Value.ToString()
                txtDeskripsi.Text = If(IsDBNull(row.Cells("Deskripsi").Value), "", row.Cells("Deskripsi").Value.ToString())
                
                txtKode.Enabled = False
                btnSave.Visible = False
                btnUpdate.Visible = True
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

    Private Sub RulesManagementForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboard IsNot Nothing Then
            ParentDashboard.Show()
        End If
    End Sub
End Class
