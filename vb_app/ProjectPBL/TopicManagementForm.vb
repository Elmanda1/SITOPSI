Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class TopicManagementForm
    Public Property ParentDashboardAdmin As DashboardAdmin
    Private currentTopicId As Integer = 0

    Private ReadOnly navyColor As Color = Color.FromArgb(44, 62, 80)
    Private ReadOnly lightNavyColor As Color = Color.FromArgb(52, 73, 94)
    Private ReadOnly whiteColor As Color = Color.White
    Private ReadOnly lightGreyHoverColor As Color = Color.FromArgb(236, 240, 241)

    Private Sub TopicManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        AddHandler dgvTopics.CellClick, AddressOf DgvTopics_CellClick

        AddHandler btnAdd.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnAdd.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnUpdate.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnUpdate.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnClear.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnClear.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btnBack.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btnBack.MouseLeave, AddressOf Button_MouseLeave

        LoadCategories()
        LoadTopics()
        ClearForm()
    End Sub

#Region "Data Loading"
    Private Sub LoadCategories()
        Try
            cmbCategory.Items.Clear()

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT category_id, nama FROM categories ORDER BY nama"
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            cmbCategory.Items.Add(New With {
                                .Id = reader.GetInt32("category_id"),
                                .Name = reader.GetString("nama")
                            })
                        End While
                    End Using
                End Using
            End Using

            cmbCategory.DisplayMember = "Name"
            cmbCategory.ValueMember = "Id"
        Catch ex As Exception
            MessageBox.Show("Error memuat kategori: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadTopics(Optional searchText As String = "")
        Try
            dgvTopics.Columns.Clear()

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                Dim query As String = "SELECT t.topic_id AS 'ID', t.judul AS 'Judul Topik', t.deskripsi AS 'Deskripsi', t.target_role AS 'Target Role', c.nama AS 'Kategori', t.is_active AS 'Aktif', t.is_feasible AS 'Layak', t.popularity AS 'Populer', t.created_at AS 'Dibuat' FROM topics t INNER JOIN categories c ON t.kategori_id = c.category_id "
                If Not String.IsNullOrEmpty(searchText) Then
                    query &= "WHERE t.judul LIKE @search OR t.target_role LIKE @search "
                End If
                query &= "ORDER BY t.created_at DESC"

                Using cmd As New MySqlCommand(query, conn)
                    If Not String.IsNullOrEmpty(searchText) Then
                        cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                    End If

                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    dgvTopics.DataSource = dt
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memuat topik: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Form and Button Logic"
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs)
        Try
            If cmbCategory.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(txtJudul.Text) Then
                MessageBox.Show("Kategori dan Judul Topik harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                Dim query As String = "INSERT INTO topics (kategori_id, target_role, judul, deskripsi, is_active, is_feasible, popularity, created_at) " &
                                     "VALUES (@categoryId, @targetRole, @judul, @deskripsi, @isActive, @isFeasible, 0, NOW())"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@categoryId", cmbCategory.SelectedValue)
                    cmd.Parameters.AddWithValue("@targetRole", If(String.IsNullOrWhiteSpace(txtTargetRole.Text), CType(DBNull.Value, Object), txtTargetRole.Text))
                    cmd.Parameters.AddWithValue("@judul", txtJudul.Text)
                    cmd.Parameters.AddWithValue("@deskripsi", If(String.IsNullOrWhiteSpace(txtDeskripsi.Text), CType(DBNull.Value, Object), txtDeskripsi.Text))
                    cmd.Parameters.AddWithValue("@isActive", If(chkActive.Checked, 1, 0))
                    cmd.Parameters.AddWithValue("@isFeasible", If(chkFeasible.Checked, 1, 0))

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Topik berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ClearForm()
                    LoadTopics()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs)
        If currentTopicId = 0 OrElse MessageBox.Show("Apakah Anda yakin ingin mengupdate topik ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                Dim query As String = "UPDATE topics SET " &
                                     "kategori_id = @categoryId, " &
                                     "target_role = @targetRole, " &
                                     "judul = @judul, " &
                                     "deskripsi = @deskripsi, " &
                                     "is_active = @isActive, " &
                                     "is_feasible = @isFeasible " &
                                     "WHERE topic_id = @id"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@categoryId", cmbCategory.SelectedValue)
                    cmd.Parameters.AddWithValue("@targetRole", If(String.IsNullOrWhiteSpace(txtTargetRole.Text), CType(DBNull.Value, Object), txtTargetRole.Text))
                    cmd.Parameters.AddWithValue("@judul", txtJudul.Text)
                    cmd.Parameters.AddWithValue("@deskripsi", If(String.IsNullOrWhiteSpace(txtDeskripsi.Text), CType(DBNull.Value, Object), txtDeskripsi.Text))
                    cmd.Parameters.AddWithValue("@isActive", If(chkActive.Checked, 1, 0))
                    cmd.Parameters.AddWithValue("@isFeasible", If(chkFeasible.Checked, 1, 0))
                    cmd.Parameters.AddWithValue("@id", currentTopicId)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Topik berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ClearForm()
                    LoadTopics()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteTopic(topicId As Integer)
        If MessageBox.Show("Yakin ingin menghapus topik ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                Dim query As String = "DELETE FROM topics WHERE topic_id = @id"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", topicId)
                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Topik berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    LoadTopics()
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
        currentTopicId = 0
        cmbCategory.SelectedIndex = -1
        txtTargetRole.Clear()
        txtJudul.Clear()
        txtDeskripsi.Clear()
        chkActive.Checked = True
        chkFeasible.Checked = True
        btnUpdate.Enabled = False
        btnAdd.Enabled = True
    End Sub

    Private Sub DgvTopics_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Try
            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)

            currentTopicId = Convert.ToInt32(row.Cells("ID").Value)

            ' Load full topic details
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                Dim query As String = "SELECT kategori_id, target_role, judul, deskripsi, is_active, is_feasible FROM topics WHERE topic_id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", currentTopicId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            cmbCategory.SelectedValue = reader.GetInt32("kategori_id")
                            txtTargetRole.Text = If(IsDBNull(reader("target_role")), "", reader.GetString("target_role"))
                            txtJudul.Text = reader.GetString("judul")
                            txtDeskripsi.Text = If(IsDBNull(reader("deskripsi")), "", reader.GetString("deskripsi"))
                            chkActive.Checked = Convert.ToBoolean(reader("is_active"))
                            chkFeasible.Checked = Convert.ToBoolean(reader("is_feasible"))

                            btnUpdate.Enabled = True
                            btnAdd.Enabled = False
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs)
        LoadTopics(txtSearch.Text)
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub TopicManagementForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboardAdmin IsNot Nothing AndAlso Not ParentDashboardAdmin.IsDisposed Then
            ParentDashboardAdmin.Show()
        End If
    End Sub

    ' --- Hover Handlers ---
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