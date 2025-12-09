Imports MySql.Data.MySqlClient

Public Class QuestionCFManagementForm
    Public Property ParentDashboardAdmin As DashboardAdmin
    Private currentQuestionId As Integer = 0

    Private ReadOnly navyColor As Color = Color.FromArgb(12, 45, 72)
    Private ReadOnly lightNavyColor As Color = Color.FromArgb(28, 68, 105)
    Private ReadOnly whiteColor As Color = Color.White
    Private ReadOnly lightGreyHoverColor As Color = Color.FromArgb(236, 240, 241)

    Private Sub QuestionCFManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Or Not IsAdmin() Then
            MessageBox.Show("Akses ditolak!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        ' Add Handlers
        AddHandler btnSave.Click, AddressOf BtnSave_Click
        AddHandler btnUpdate.Click, AddressOf BtnUpdate_Click
        AddHandler btnDelete.Click, AddressOf BtnDelete_Click
        AddHandler btnClear.Click, AddressOf BtnClear_Click
        AddHandler btnBack.Click, AddressOf BtnBack_Click
        AddHandler txtSearch.TextChanged, AddressOf TxtSearch_TextChanged
        AddHandler dgvQuestions.CellClick, AddressOf DgvQuestions_CellClick

        ' Hover Handlers
        AddHandler btnSave.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnSave.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnUpdate.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnUpdate.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnDelete.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnDelete.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnClear.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnClear.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnBack.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnBack.MouseLeave, AddressOf Button_MouseLeave

        ' Initial Load
        LoadCategories(cmbKategori)
        LoadCFLevels(cmbCF)
        LoadQuestionsData()
        ClearForm()
    End Sub

#Region "Data Loading"

    Private Sub LoadCategories(cmb As ComboBox)
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT category_id, CONCAT(kode, ' - ', nama) AS display FROM categories ORDER BY kode"
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Dim dt As New DataTable()
                        dt.Load(reader)
                        cmb.DataSource = dt
                        cmb.DisplayMember = "display"
                        cmb.ValueMember = "category_id"
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memuat kategori: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCFLevels(cmb As ComboBox)
        Try
            cmb.Items.Clear()
            cmb.DisplayMember = "Display"
            cmb.ValueMember = "Value"
            cmb.Items.Add(New With {.Value = 1.0D, .Display = "1.00 - Sangat Pasti"})
            cmb.Items.Add(New With {.Value = 0.8D, .Display = "0.80 - Pasti"})
            cmb.Items.Add(New With {.Value = 0.6D, .Display = "0.60 - Cukup Yakin"})
            cmb.Items.Add(New With {.Value = 0.4D, .Display = "0.40 - Kemungkinan"})
            cmb.SelectedIndex = 1 ' Default 0.80
        Catch ex As Exception
            MessageBox.Show("Error memuat level CF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadQuestionsData(Optional searchText As String = "")
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT q.question_id AS 'ID', q.nomor AS 'No', LEFT(q.isi_pertanyaan, 50) AS 'Statement', c.nama AS 'Kategori', q.cf_pakar AS 'CF Pakar', IF(q.is_active=1, 'Aktif', 'Nonaktif') AS 'Status' FROM questions q INNER JOIN categories c ON q.target_category_id = c.category_id "
                If Not String.IsNullOrWhiteSpace(searchText) Then
                    query &= "WHERE q.isi_pertanyaan LIKE @search "
                End If
                query &= "ORDER BY q.nomor"
                
                Using adapter As New MySqlDataAdapter(query, conn)
                    If Not String.IsNullOrWhiteSpace(searchText) Then
                        adapter.SelectCommand.Parameters.AddWithValue("@search", "%" & searchText & "%")
                    End If
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    dgvQuestions.DataSource = dt
                    If dgvQuestions.Columns.Count > 0 AndAlso dgvQuestions.Columns.Contains("ID") Then
                        dgvQuestions.Columns("ID").Visible = False
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memuat data statement: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Form and Button Logic"

    Private Sub BtnSave_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrWhiteSpace(txtPertanyaan.Text) OrElse cmbKategori.SelectedIndex = -1 OrElse cmbCF.SelectedIndex = -1 Then
                MessageBox.Show("Semua field harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim checkQuery As String = "SELECT COUNT(*) FROM questions WHERE nomor = @nomor"
                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@nomor", txtNomor.Value)
                    If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                        MessageBox.Show("Nomor statement sudah ada!", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                Dim query As String = "INSERT INTO questions (nomor, isi_pertanyaan, target_category_id, cf_pakar, is_active, is_priority, created_at) VALUES (@nomor, @statement, @catId, @cf, @active, @priority, NOW())"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@nomor", txtNomor.Value)
                    cmd.Parameters.AddWithValue("@statement", txtPertanyaan.Text.Trim())
                    cmd.Parameters.AddWithValue("@catId", cmbKategori.SelectedValue)
                    cmd.Parameters.AddWithValue("@cf", CType(cmbCF.SelectedItem, Object).Value)
                    cmd.Parameters.AddWithValue("@active", If(chkActive.Checked, 1, 0))
                    cmd.Parameters.AddWithValue("@priority", If(chkPriority.Checked, 1, 0))
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Statement berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearForm()
                    LoadQuestionsData()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs)
        If currentQuestionId = 0 OrElse MessageBox.Show("Apakah Anda yakin ingin mengupdate statement ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim checkQuery As String = "SELECT COUNT(*) FROM questions WHERE nomor = @nomor AND question_id <> @id"
                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@nomor", txtNomor.Value)
                    checkCmd.Parameters.AddWithValue("@id", currentQuestionId)
                    If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                        MessageBox.Show("Nomor pertanyaan sudah ada!", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                Dim query As String = "UPDATE questions SET nomor = @nomor, isi_pertanyaan = @pertanyaan, target_category_id = @catId, cf_pakar = @cf, is_active = @active, is_priority = @priority WHERE question_id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", currentQuestionId)
                    cmd.Parameters.AddWithValue("@nomor", txtNomor.Value)
                    cmd.Parameters.AddWithValue("@pertanyaan", txtPertanyaan.Text.Trim())
                    cmd.Parameters.AddWithValue("@catId", cmbKategori.SelectedValue)
                    cmd.Parameters.AddWithValue("@cf", CType(cmbCF.SelectedItem, Object).Value)
                    cmd.Parameters.AddWithValue("@active", If(chkActive.Checked, 1, 0))
                    cmd.Parameters.AddWithValue("@priority", If(chkPriority.Checked, 1, 0))
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Pertanyaan berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearForm()
                    LoadQuestionsData()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs)
        If currentQuestionId = 0 OrElse MessageBox.Show("Apakah Anda yakin ingin menghapus statement ini secara permanen?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then Return
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                ' Optional: Log activity before deleting
                Dim logDetail As String = "Statement ID " & currentQuestionId & " dihapus oleh user ID " & LoggedUserId
                Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, 'Delete Statement', @detail, NOW())"
                Using logCmd As New MySqlCommand(logQuery, conn)
                    logCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    logCmd.Parameters.AddWithValue("@detail", logDetail)
                    logCmd.ExecuteNonQuery()
                End Using

                Dim query As String = "DELETE FROM questions WHERE question_id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", currentQuestionId)
                    Dim result = cmd.ExecuteNonQuery()

                    If result > 0 Then
                        MessageBox.Show("Statement berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ClearForm()
                        LoadQuestionsData()
                    Else
                        MessageBox.Show("Gagal menghapus statement. Mungkin sudah dihapus.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End Using
            End Using
        Catch ex As MySqlException
            If ex.Number = 1451 Then ' Foreign Key constraint fails
                MessageBox.Show("Gagal menghapus: Statement ini sudah digunakan dalam data tes jawaban user dan tidak dapat dihapus untuk menjaga integritas data.", "Error Relasional", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error database saat menghapus: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error saat menghapus: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs)
        ClearForm()
    End Sub

    Private Sub ClearForm()
        currentQuestionId = 0
        txtNomor.Value = GetNextQuestionNumber()
        txtPertanyaan.Text = ""
        cmbKategori.SelectedIndex = -1
        cmbCF.SelectedIndex = 1 ' Default 0.80
        chkActive.Checked = True
        chkPriority.Checked = False
        txtNomor.Enabled = True
        btnSave.Visible = True
        btnUpdate.Visible = False
        btnDelete.Visible = False
    End Sub

    Private Function GetNextQuestionNumber() As Integer
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT COALESCE(MAX(nomor), 0) + 1 FROM questions"
                Using cmd As New MySqlCommand(query, conn)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        Return Convert.ToInt32(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error mengambil nomor pertanyaan berikutnya: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        Return 1 ' Default jika error
    End Function

    Private Sub DgvQuestions_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Try
            Dim row As DataGridViewRow = dgvQuestions.Rows(e.RowIndex)
            currentQuestionId = Convert.ToInt32(row.Cells("ID").Value)

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT nomor, isi_pertanyaan, target_category_id, cf_pakar, is_active, is_priority FROM questions WHERE question_id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", currentQuestionId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txtNomor.Value = reader.GetInt32("nomor")
                            txtPertanyaan.Text = reader.GetString("isi_pertanyaan")
                            cmbKategori.SelectedValue = reader.GetInt32("target_category_id")
                            Dim cfValue As Decimal = reader.GetDecimal("cf_pakar")
                            For i As Integer = 0 To cmbCF.Items.Count - 1
                                If CType(cmbCF.Items(i), Object).Value = cfValue Then
                                    cmbCF.SelectedIndex = i
                                    Exit For
                                End If
                            Next
                            chkActive.Checked = (reader.GetByte("is_active") = 1)
                            chkPriority.Checked = (reader.GetByte("is_priority") = 1)

                            txtNomor.Enabled = True
                            btnSave.Visible = False
                            btnUpdate.Visible = True
                            btnDelete.Visible = True
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs)
        LoadQuestionsData(txtSearch.Text)
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub QuestionCFManagementForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboardAdmin IsNot Nothing AndAlso Not ParentDashboardAdmin.IsDisposed Then
            ParentDashboardAdmin.Show()
        End If
    End Sub

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