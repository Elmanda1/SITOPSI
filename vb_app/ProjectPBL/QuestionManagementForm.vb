Imports MySql.Data.MySqlClient

Public Class QuestionManagementForm
    Public Property ParentDashboard As DashboardAdmin
    Private currentQuestionId As Integer = 0
    Private optionControls As New List(Of Panel)

    Private Sub QuestionManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Or Not IsAdmin() Then
            MessageBox.Show("Akses ditolak!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(236, 240, 241)
        Me.Size = New Size(1200, 800)
        Me.Text = "Manajemen Pertanyaan - SITOPSI"
        
        BuildUI()
        LoadQuestionsData()
    End Sub

    Private Sub BuildUI()
        Me.Controls.Clear()

        ' Header Panel
        Dim headerPanel As New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 80,
            .BackColor = Color.FromArgb(52, 152, 219)
        }
        Me.Controls.Add(headerPanel)

        Dim lblTitle As New Label() With {
            .Text = "? Manajemen Pertanyaan",
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

        ' Main Content Panel with AutoScroll
        Dim contentPanel As New Panel() With {
            .Name = "contentPanel",
            .Location = New Point(0, 80),
            .Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height - 80),
            .BackColor = Color.FromArgb(236, 240, 241),
            .AutoScroll = True
        }
        Me.Controls.Add(contentPanel)

        ' Left Panel - Form Input
        Dim leftPanel As New Panel() With {
            .Name = "leftPanel",
            .Location = New Point(20, 20),
            .Size = New Size(700, 650),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .AutoScroll = True
        }
        contentPanel.Controls.Add(leftPanel)

        ' Form Title
        Dim lblFormTitle As New Label() With {
            .Text = "Form Tambah/Edit Pertanyaan",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Location = New Point(20, 20)
        }
        leftPanel.Controls.Add(lblFormTitle)

        ' Nomor Pertanyaan
        Dim lblNomor As New Label() With {
            .Text = "Nomor Pertanyaan:",
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point(20, 60)
        }
        leftPanel.Controls.Add(lblNomor)

        Dim txtNomor As New NumericUpDown() With {
            .Name = "txtNomor",
            .Font = New Font("Segoe UI", 10),
            .Size = New Size(150, 30),
            .Location = New Point(20, 85),
            .Minimum = 1,
            .Maximum = 1000,
            .Value = 1
        }
        leftPanel.Controls.Add(txtNomor)

        ' Isi Pertanyaan
        Dim lblPertanyaan As New Label() With {
            .Text = "Isi Pertanyaan:",
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point(20, 130)
        }
        leftPanel.Controls.Add(lblPertanyaan)

        Dim txtPertanyaan As New TextBox() With {
            .Name = "txtPertanyaan",
            .Font = New Font("Segoe UI", 10),
            .Size = New Size(660, 80),
            .Location = New Point(20, 155),
            .Multiline = True,
            .ScrollBars = ScrollBars.Vertical
        }
        leftPanel.Controls.Add(txtPertanyaan)

        ' Opsi Jawaban Section
        Dim lblOpsi As New Label() With {
            .Text = "Opsi Jawaban (A, B, C, D):",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Location = New Point(20, 250)
        }
        leftPanel.Controls.Add(lblOpsi)

        ' Container for options
        Dim optionsContainer As New Panel() With {
            .Name = "optionsContainer",
            .Location = New Point(20, 280),
            .Size = New Size(660, 320),
            .AutoScroll = True
        }
        leftPanel.Controls.Add(optionsContainer)

        ' Create 4 option panels (A, B, C, D)
        CreateOptionPanels(optionsContainer)

        ' Buttons Panel
        Dim btnPanel As New Panel() With {
            .Location = New Point(20, 610),
            .Size = New Size(660, 50)
        }
        leftPanel.Controls.Add(btnPanel)

        ' Save Button
        Dim btnSave As New Button() With {
            .Name = "btnSave",
            .Text = "?? Simpan",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .Size = New Size(150, 40),
            .Location = New Point(0, 0),
            .BackColor = Color.FromArgb(46, 204, 113),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnSave.FlatAppearance.BorderSize = 0
        AddHandler btnSave.Click, AddressOf BtnSave_Click
        btnPanel.Controls.Add(btnSave)

        ' Clear Button
        Dim btnClear As New Button() With {
            .Name = "btnClear",
            .Text = "?? Clear",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .Size = New Size(150, 40),
            .Location = New Point(160, 0),
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
            .Location = New Point(740, 20),
            .Size = New Size(420, 650),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle
        }
        contentPanel.Controls.Add(rightPanel)

        ' Grid Title
        Dim lblGridTitle As New Label() With {
            .Text = "Daftar Pertanyaan",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Location = New Point(20, 20)
        }
        rightPanel.Controls.Add(lblGridTitle)

        ' Search Box
        Dim txtSearch As New TextBox() With {
            .Name = "txtSearch",
            .Font = New Font("Segoe UI", 9),
            .Size = New Size(200, 25),
            .Location = New Point(20, 55),
            .PlaceholderText = "Cari pertanyaan..."
        }
        rightPanel.Controls.Add(txtSearch)
        AddHandler txtSearch.TextChanged, AddressOf TxtSearch_TextChanged

        ' DataGridView
        Dim dgv As New DataGridView() With {
            .Name = "dgvQuestions",
            .Location = New Point(20, 90),
            .Size = New Size(380, 540),
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

        AddHandler dgv.CellDoubleClick, AddressOf DgvQuestions_CellDoubleClick
    End Sub

    Private Sub CreateOptionPanels(container As Panel)
        optionControls.Clear()
        Dim yPos As Integer = 0
        Dim kodes() As String = {"A", "B", "C", "D"}

        For i As Integer = 0 To 3
            Dim optionPanel As New Panel() With {
                .Name = $"panelOption{kodes(i)}",
                .Location = New Point(0, yPos),
                .Size = New Size(640, 70),
                .BorderStyle = BorderStyle.FixedSingle,
                .BackColor = Color.FromArgb(236, 240, 241)
            }
            container.Controls.Add(optionPanel)
            optionControls.Add(optionPanel)

            ' Label Opsi
            Dim lblKode As New Label() With {
                .Text = $"Opsi {kodes(i)}:",
                .Font = New Font("Segoe UI", 9, FontStyle.Bold),
                .AutoSize = True,
                .Location = New Point(5, 5)
            }
            optionPanel.Controls.Add(lblKode)

            ' TextBox Teks Opsi
            Dim txtTeks As New TextBox() With {
                .Name = $"txtTeks{kodes(i)}",
                .Font = New Font("Segoe UI", 9),
                .Size = New Size(300, 25),
                .Location = New Point(5, 25)
            }
            optionPanel.Controls.Add(txtTeks)

            ' ComboBox Kategori
            Dim cmbKategori As New ComboBox() With {
                .Name = $"cmbKategori{kodes(i)}",
                .Font = New Font("Segoe UI", 9),
                .Size = New Size(150, 25),
                .Location = New Point(310, 25),
                .DropDownStyle = ComboBoxStyle.DropDownList
            }
            optionPanel.Controls.Add(cmbKategori)
            LoadCategories(cmbKategori)

            ' Label CF
            Dim lblCF As New Label() With {
                .Text = "CF:",
                .Font = New Font("Segoe UI", 9),
                .AutoSize = True,
                .Location = New Point(465, 7)
            }
            optionPanel.Controls.Add(lblCF)

            ' ComboBox CF Pakar
            Dim cmbCF As New ComboBox() With {
                .Name = $"cmbCF{kodes(i)}",
                .Font = New Font("Segoe UI", 9),
                .Size = New Size(150, 25),
                .Location = New Point(465, 25),
                .DropDownStyle = ComboBoxStyle.DropDownList
            }
            optionPanel.Controls.Add(cmbCF)
            LoadCFLevels(cmbCF)

            yPos += 75
        Next
    End Sub

    Private Sub LoadCategories(cmb As ComboBox)
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT id, CONCAT(kode, ' - ', nama) AS display FROM categories ORDER BY kode"
                
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Dim dt As New DataTable()
                        dt.Load(reader)
                        
                        cmb.DataSource = dt
                        cmb.DisplayMember = "display"
                        cmb.ValueMember = "id"
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCFLevels(cmb As ComboBox)
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT cf_value, CONCAT(cf_value, ' - ', label) AS display FROM cf_levels ORDER BY cf_value DESC"
                
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Dim dt As New DataTable()
                        dt.Load(reader)
                        
                        cmb.DataSource = dt
                        cmb.DisplayMember = "display"
                        cmb.ValueMember = "cf_value"
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error loading CF levels: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadQuestionsData()
        Try
            Dim contentPanel As Panel = CType(Me.Controls.Find("contentPanel", True)(0), Panel)
            Dim dgv As DataGridView = CType(contentPanel.Controls.Find("dgvQuestions", True)(0), DataGridView)
            
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT id AS 'ID', nomor AS 'No', LEFT(isi_pertanyaan, 50) AS 'Pertanyaan', is_active AS 'Aktif' FROM questions ORDER BY nomor"
                
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
            Dim txtNomor As NumericUpDown = CType(Me.Controls.Find("txtNomor", True)(0), NumericUpDown)
            Dim txtPertanyaan As TextBox = CType(Me.Controls.Find("txtPertanyaan", True)(0), TextBox)

            ' Validation
            If String.IsNullOrWhiteSpace(txtPertanyaan.Text) Then
                MessageBox.Show("Isi pertanyaan harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPertanyaan.Focus()
                Return
            End If

            ' Validate all options
            For Each kode As String In {"A", "B", "C", "D"}
                Dim txtTeks As TextBox = CType(Me.Controls.Find($"txtTeks{kode}", True)(0), TextBox)
                Dim cmbKategori As ComboBox = CType(Me.Controls.Find($"cmbKategori{kode}", True)(0), ComboBox)
                Dim cmbCF As ComboBox = CType(Me.Controls.Find($"cmbCF{kode}", True)(0), ComboBox)

                If String.IsNullOrWhiteSpace(txtTeks.Text) Then
                    MessageBox.Show($"Teks opsi {kode} harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtTeks.Focus()
                    Return
                End If

                If cmbKategori.SelectedIndex = -1 Then
                    MessageBox.Show($"Kategori opsi {kode} harus dipilih!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbKategori.Focus()
                    Return
                End If

                If cmbCF.SelectedIndex = -1 Then
                    MessageBox.Show($"CF Pakar opsi {kode} harus dipilih!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbCF.Focus()
                    Return
                End If
            Next

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                
                ' Check if nomor already exists
                Dim checkQuery As String = "SELECT COUNT(*) FROM questions WHERE nomor = @nomor"
                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@nomor", txtNomor.Value)
                    Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                    
                    If count > 0 Then
                        MessageBox.Show("Nomor pertanyaan sudah ada!", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                ' Begin transaction
                Using transaction As MySqlTransaction = conn.BeginTransaction()
                    Try
                        ' Insert question
                        Dim questionQuery As String = "INSERT INTO questions (nomor, isi_pertanyaan, is_active) VALUES (@nomor, @pertanyaan, 1); SELECT LAST_INSERT_ID();"
                        Dim questionId As Integer = 0
                        
                        Using cmd As New MySqlCommand(questionQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@nomor", txtNomor.Value)
                            cmd.Parameters.AddWithValue("@pertanyaan", txtPertanyaan.Text)
                            questionId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        ' Insert options
                        Dim sortOrder As Integer = 0
                        For Each kode As String In {"A", "B", "C", "D"}
                            Dim txtTeks As TextBox = CType(Me.Controls.Find($"txtTeks{kode}", True)(0), TextBox)
                            Dim cmbKategori As ComboBox = CType(Me.Controls.Find($"cmbKategori{kode}", True)(0), ComboBox)
                            Dim cmbCF As ComboBox = CType(Me.Controls.Find($"cmbCF{kode}", True)(0), ComboBox)

                            Dim optionQuery As String = "INSERT INTO question_options (question_id, kode, teks, category_id, sort_order, cf_pakar) VALUES (@qid, @kode, @teks, @catId, @sortOrder, @cf)"
                            Using optCmd As New MySqlCommand(optionQuery, conn, transaction)
                                optCmd.Parameters.AddWithValue("@qid", questionId)
                                optCmd.Parameters.AddWithValue("@kode", kode)
                                optCmd.Parameters.AddWithValue("@teks", txtTeks.Text)
                                optCmd.Parameters.AddWithValue("@catId", cmbKategori.SelectedValue)
                                optCmd.Parameters.AddWithValue("@sortOrder", sortOrder)
                                optCmd.Parameters.AddWithValue("@cf", cmbCF.SelectedValue)
                                optCmd.ExecuteNonQuery()
                            End Using
                            sortOrder += 1
                        Next

                        transaction.Commit()
                        MessageBox.Show("Pertanyaan berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        
                        BtnClear_Click(Nothing, Nothing)
                        LoadQuestionsData()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs)
        Dim txtNomor As NumericUpDown = CType(Me.Controls.Find("txtNomor", True)(0), NumericUpDown)
        Dim txtPertanyaan As TextBox = CType(Me.Controls.Find("txtPertanyaan", True)(0), TextBox)

        txtNomor.Value = 1
        txtPertanyaan.Text = ""
        
        For Each kode As String In {"A", "B", "C", "D"}
            Dim txtTeks As TextBox = CType(Me.Controls.Find($"txtTeks{kode}", True)(0), TextBox)
            Dim cmbKategori As ComboBox = CType(Me.Controls.Find($"cmbKategori{kode}", True)(0), ComboBox)
            Dim cmbCF As ComboBox = CType(Me.Controls.Find($"cmbCF{kode}", True)(0), ComboBox)

            txtTeks.Text = ""
            cmbKategori.SelectedIndex = -1
            cmbCF.SelectedIndex = -1
        Next

        currentQuestionId = 0
    End Sub

    Private Sub DgvQuestions_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Try
                Dim dgv As DataGridView = CType(sender, DataGridView)
                Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)
                
                Dim questionId As Integer = Convert.ToInt32(row.Cells("ID").Value)
                
                ' Load question details
                Using conn As New MySqlConnection(ConnectionString)
                    conn.Open()
                    
                    ' Load question
                    Dim qQuery As String = "SELECT nomor, isi_pertanyaan FROM questions WHERE id = @id"
                    Using cmd As New MySqlCommand(qQuery, conn)
                        cmd.Parameters.AddWithValue("@id", questionId)
                        Using reader As MySqlDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                Dim txtNomor As NumericUpDown = CType(Me.Controls.Find("txtNomor", True)(0), NumericUpDown)
                                Dim txtPertanyaan As TextBox = CType(Me.Controls.Find("txtPertanyaan", True)(0), TextBox)
                                
                                txtNomor.Value = reader.GetInt32("nomor")
                                txtPertanyaan.Text = reader.GetString("isi_pertanyaan")
                            End If
                        End Using
                    End Using
                    
                    ' Load options
                    Dim optQuery As String = "SELECT kode, teks, category_id, cf_pakar FROM question_options WHERE question_id = @qid ORDER BY sort_order"
                    Using cmd As New MySqlCommand(optQuery, conn)
                        cmd.Parameters.AddWithValue("@qid", questionId)
                        Using reader As MySqlDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                Dim kode As String = reader.GetString("kode")
                                Dim txtTeks As TextBox = CType(Me.Controls.Find($"txtTeks{kode}", True)(0), TextBox)
                                Dim cmbKategori As ComboBox = CType(Me.Controls.Find($"cmbKategori{kode}", True)(0), ComboBox)
                                Dim cmbCF As ComboBox = CType(Me.Controls.Find($"cmbCF{kode}", True)(0), ComboBox)
                                
                                txtTeks.Text = reader.GetString("teks")
                                cmbKategori.SelectedValue = reader.GetInt32("category_id")
                                cmbCF.SelectedValue = reader.GetDecimal("cf_pakar")
                            End While
                        End Using
                    End Using
                End Using
                
                MessageBox.Show("Data pertanyaan dimuat. Anda dapat melihat detailnya.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim txtSearch As TextBox = CType(sender, TextBox)
            Dim contentPanel As Panel = CType(Me.Controls.Find("contentPanel", True)(0), Panel)
            Dim dgv As DataGridView = CType(contentPanel.Controls.Find("dgvQuestions", True)(0), DataGridView)
            
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT id AS 'ID', nomor AS 'No', LEFT(isi_pertanyaan, 50) AS 'Pertanyaan', is_active AS 'Aktif' FROM questions WHERE nomor LIKE @search OR isi_pertanyaan LIKE @search ORDER BY nomor"
                
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

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        If ParentDashboard IsNot Nothing Then
            ParentDashboard.Show()
        End If
        Me.Close()
    End Sub

    Private Sub QuestionManagementForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboard IsNot Nothing Then
            ParentDashboard.Show()
        End If
    End Sub
End Class
