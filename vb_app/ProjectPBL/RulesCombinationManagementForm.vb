Imports MySql.Data.MySqlClient

Public Class RulesCombinationManagementForm
    Public Property ParentDashboardAdmin As DashboardAdmin
    Private currentRuleId As Integer = 0

    Private ReadOnly navyColor As Color = Color.FromArgb(44, 62, 80)
    Private ReadOnly lightNavyColor As Color = Color.FromArgb(52, 73, 94)
    Private ReadOnly whiteColor As Color = Color.White
    Private ReadOnly lightGreyHoverColor As Color = Color.FromArgb(236, 240, 241)

    Private Sub RulesCombinationManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Or Not IsAdmin() Then
            MessageBox.Show("Akses ditolak!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        AddHandler btnAdd.Click, AddressOf BtnAdd_Click
        AddHandler btnUpdate.Click, AddressOf BtnUpdate_Click
        AddHandler btnClear.Click, AddressOf BtnClear_Click
        AddHandler btnBack.Click, AddressOf BtnBack_Click
        AddHandler txtSearch.TextChanged, AddressOf TxtSearch_TextChanged
        AddHandler dgvRules.CellClick, AddressOf DgvRules_CellClick

        AddHandler btnAdd.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnAdd.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnUpdate.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnUpdate.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnClear.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnClear.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnBack.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnBack.MouseLeave, AddressOf Button_MouseLeave

        LoadCategories()
        LoadRules()
        ClearForm()
    End Sub

#Region "Data Loading"
    Private Sub LoadCategories()
        Try
            Dim categoryData As New List(Of Object)
            categoryData.Add(New With {.Id = 0, .Name = "(Tidak ada)"})

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT category_id, CONCAT(kode, ' - ', nama) AS display_name FROM categories ORDER BY kode"
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            categoryData.Add(New With {
                                .Id = reader.GetInt32("category_id"),
                                .Name = reader.GetString("display_name")
                            })
                        End While
                    End Using
                End Using
            End Using

            cmbCat1.DataSource = New List(Of Object)(categoryData)
            cmbCat1.DisplayMember = "Name"
            cmbCat1.ValueMember = "Id"

            cmbCat2.DataSource = New List(Of Object)(categoryData)
            cmbCat2.DisplayMember = "Name"
            cmbCat2.ValueMember = "Id"

            cmbCat3.DataSource = New List(Of Object)(categoryData)
            cmbCat3.DisplayMember = "Name"
            cmbCat3.ValueMember = "Id"

        Catch ex As Exception
            MessageBox.Show("Error memuat kategori: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadRules(Optional searchText As String = "")
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT r.rule_id, r.role_name, r.description, " &
                                     "c1.nama AS cat1_name, c2.nama AS cat2_name, c3.nama AS cat3_name " &
                                     "FROM rules r " &
                                     "INNER JOIN categories c1 ON r.category_1_id = c1.category_id " &
                                     "INNER JOIN categories c2 ON r.category_2_id = c2.category_id " &
                                     "LEFT JOIN categories c3 ON r.category_3_id = c3.category_id "

                If Not String.IsNullOrEmpty(searchText) Then
                    query &= "WHERE r.role_name LIKE @search "
                End If
                query &= "ORDER BY r.rule_id DESC"

                Using cmd As New MySqlCommand(query, conn)
                    If Not String.IsNullOrEmpty(searchText) Then
                        cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                    End If
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    dgvRules.DataSource = dt
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memuat rules: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Form and Button Logic"
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs)
        Try
            If cmbCat1.SelectedIndex <= 0 OrElse cmbCat2.SelectedIndex <= 0 Then
                MessageBox.Show("Kategori 1 dan 2 wajib dipilih!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If CInt(cmbCat1.SelectedValue) = CInt(cmbCat2.SelectedValue) Then
                MessageBox.Show("Kategori 1 dan 2 harus berbeda!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If cmbCat3.SelectedIndex > 0 AndAlso (CInt(cmbCat3.SelectedValue) = CInt(cmbCat1.SelectedValue) OrElse CInt(cmbCat3.SelectedValue) = CInt(cmbCat2.SelectedValue)) Then
                MessageBox.Show("Kategori 3 harus berbeda dari kategori 1 dan 2!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If String.IsNullOrWhiteSpace(txtRoleName.Text) Then
                MessageBox.Show("Nama role tidak boleh kosong!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "INSERT INTO rules (category_1_id, category_2_id, category_3_id, role_name, description) VALUES (@cat1, @cat2, @cat3, @roleName, @description)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@cat1", cmbCat1.SelectedValue)
                    cmd.Parameters.AddWithValue("@cat2", cmbCat2.SelectedValue)
                    cmd.Parameters.AddWithValue("@cat3", If(CInt(cmbCat3.SelectedValue) = 0, CType(DBNull.Value, Object), cmbCat3.SelectedValue))
                    cmd.Parameters.AddWithValue("@roleName", txtRoleName.Text)
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Rule berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearForm()
                    LoadRules()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs)
        If currentRuleId = 0 OrElse MessageBox.Show("Yakin ingin mengupdate rule ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "UPDATE rules SET category_1_id = @cat1, category_2_id = @cat2, category_3_id = @cat3, role_name = @roleName, description = @description WHERE rule_id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", currentRuleId)
                    cmd.Parameters.AddWithValue("@cat1", cmbCat1.SelectedValue)
                    cmd.Parameters.AddWithValue("@cat2", cmbCat2.SelectedValue)
                    cmd.Parameters.AddWithValue("@cat3", If(CInt(cmbCat3.SelectedValue) = 0, CType(DBNull.Value, Object), cmbCat3.SelectedValue))
                    cmd.Parameters.AddWithValue("@roleName", txtRoleName.Text)
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Rule berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearForm()
                    LoadRules()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteRule(ruleId As Integer)
        If MessageBox.Show("Yakin ingin menghapus rule ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "DELETE FROM rules WHERE rule_id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", ruleId)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Rule berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadRules()
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
        cmbCat1.SelectedIndex = -1
        cmbCat2.SelectedIndex = -1
        cmbCat3.SelectedIndex = 0
        txtRoleName.Clear()
        txtDescription.Clear()
        btnUpdate.Enabled = False
        btnAdd.Enabled = True
    End Sub

    Private Sub DgvRules_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Try
            currentRuleId = Convert.ToInt32(dgvRules.Rows(e.RowIndex).Cells("rule_id").Value)
            LoadRuleToForm(currentRuleId)
        Catch
        End Try
    End Sub

    Private Sub LoadRuleToForm(ruleId As Integer)
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT * FROM rules WHERE rule_id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", ruleId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            cmbCat1.SelectedValue = reader.GetInt32("category_1_id")
                            cmbCat2.SelectedValue = reader.GetInt32("category_2_id")
                            cmbCat3.SelectedValue = If(IsDBNull(reader("category_3_id")), 0, reader.GetInt32("category_3_id"))
                            txtRoleName.Text = reader("role_name").ToString()
                            txtDescription.Text = If(IsDBNull(reader("description")), "", reader.GetString("description"))
                            btnUpdate.Enabled = True
                            btnAdd.Enabled = False
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memuat rule: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs)
        LoadRules(txtSearch.Text)
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub RulesCombinationManagementForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboardAdmin IsNot Nothing AndAlso Not ParentDashboardAdmin.IsDisposed Then
            ParentDashboardAdmin.Show()
        End If
    End Sub

    Private Sub Button_MouseEnter(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        If btn Is btnAdd OrElse btn Is btnUpdate Then
            btn.BackColor = lightNavyColor
        Else
            btn.BackColor = lightGreyHoverColor
        End If
    End Sub

    Private Sub Button_MouseLeave(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        If btn Is btnAdd OrElse btn Is btnUpdate Then
            btn.BackColor = navyColor
        Else
            btn.BackColor = whiteColor
        End If
    End Sub
#End Region

End Class
