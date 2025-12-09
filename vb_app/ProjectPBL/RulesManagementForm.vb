Imports MySql.Data.MySqlClient

Public Class RulesManagementForm
    Public Property ParentDashboardAdmin As DashboardAdmin
    Private currentRuleId As Integer = 0

    ' --- Colors ---
    Private ReadOnly navyColor As Color = Color.FromArgb(12, 45, 72)
    Private ReadOnly lightNavyColor As Color = Color.FromArgb(28, 68, 105)
    Private ReadOnly whiteColor As Color = Color.White
    Private ReadOnly lightGreyHoverColor As Color = Color.FromArgb(236, 240, 241)

    Private Sub RulesManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Or Not IsAdmin() Then
            MessageBox.Show("Akses ditolak!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        ' Add Handlers
        AddHandler btnSave.Click, AddressOf BtnSave_Click
        AddHandler btnUpdate.Click, AddressOf BtnUpdate_Click
        AddHandler btnClear.Click, AddressOf BtnClear_Click
        AddHandler btnBack.Click, AddressOf BtnBack_Click
        AddHandler dgvRules.CellDoubleClick, AddressOf DgvRules_CellDoubleClick
        
        ' Hover Handlers
        AddHandler btnSave.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnSave.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnUpdate.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnUpdate.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnClear.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnClear.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnBack.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnBack.MouseLeave, AddressOf Button_MouseLeave

        ' Initial Load
        LoadRulesData()
        ClearForm()
    End Sub

#Region "Data Loading"
    Private Sub LoadRulesData()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT category_id AS 'ID', kode AS 'Kode', nama AS 'Nama Kategori', deskripsi AS 'Deskripsi' FROM categories ORDER BY kode"
                Using adapter As New MySqlDataAdapter(query, conn)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    dgvRules.DataSource = dt
                    If dgvRules.Columns.Count > 0 AndAlso dgvRules.Columns.Contains("ID") Then
                        dgvRules.Columns("ID").Visible = False
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memuat data kategori: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Form and Button Logic"
    Private Sub BtnSave_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrWhiteSpace(txtKode.Text) OrElse String.IsNullOrWhiteSpace(txtNama.Text) Then
                MessageBox.Show("Kode dan Nama Kategori harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If txtKode.Text.Length <> 1 Then
                MessageBox.Show("Kode kategori harus 1 karakter!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim checkQuery As String = "SELECT COUNT(*) FROM categories WHERE kode = @kode"
                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@kode", txtKode.Text.ToUpper())
                    If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                        MessageBox.Show("Kode kategori sudah ada!", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                Dim query As String = "INSERT INTO categories (kode, nama, deskripsi) VALUES (@kode, @nama, @deskripsi)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@kode", txtKode.Text.ToUpper())
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text)
                    cmd.Parameters.AddWithValue("@deskripsi", If(String.IsNullOrWhiteSpace(txtDeskripsi.Text), CType(DBNull.Value, Object), txtDeskripsi.Text))
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Kategori berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearForm()
                    LoadRulesData()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs)
        If currentRuleId = 0 OrElse MessageBox.Show("Apakah Anda yakin ingin mengupdate kategori ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "UPDATE categories SET nama = @nama, deskripsi = @deskripsi WHERE category_id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", currentRuleId)
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text)
                    cmd.Parameters.AddWithValue("@deskripsi", If(String.IsNullOrWhiteSpace(txtDeskripsi.Text), CType(DBNull.Value, Object), txtDeskripsi.Text))
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Kategori berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearForm()
                    LoadRulesData()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs)
        ClearForm()
    End Sub

    Private Sub ClearForm()
        currentRuleId = 0
        txtKode.Text = ""
        txtNama.Text = ""
        txtDeskripsi.Text = ""
        txtKode.Enabled = True
        btnSave.Visible = True
        btnUpdate.Visible = False
    End Sub

    Private Sub DgvRules_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Try
            Dim row As DataGridViewRow = dgvRules.Rows(e.RowIndex)
            currentRuleId = Convert.ToInt32(row.Cells("ID").Value)

            txtKode.Text = row.Cells("Kode").Value.ToString()
            txtNama.Text = row.Cells("Nama Kategori").Value.ToString()
            txtDeskripsi.Text = If(IsDBNull(row.Cells("Deskripsi").Value), "", row.Cells("Deskripsi").Value.ToString())
            
            txtKode.Enabled = False
            btnSave.Visible = False
            btnUpdate.Visible = True
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub RulesManagementForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboardAdmin IsNot Nothing AndAlso Not ParentDashboardAdmin.IsDisposed Then
            ParentDashboardAdmin.Show()
        End If
    End Sub

    ' --- Hover Handlers ---
    Private Sub Button_MouseEnter(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        If btn Is btnSave OrElse btn Is btnUpdate Then
            btn.BackColor = lightNavyColor
        Else
            btn.BackColor = lightGreyHoverColor
        End If
    End Sub

    Private Sub Button_MouseLeave(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        If btn Is btnSave OrElse btn Is btnUpdate Then
            btn.BackColor = navyColor
        Else
            btn.BackColor = whiteColor
        End If
    End Sub

#End Region

End Class