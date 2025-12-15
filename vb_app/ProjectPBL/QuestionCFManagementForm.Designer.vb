<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class QuestionCFManagementForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        lblTitle = New Label()
        btnBack = New Button()
        leftPanel = New Panel()
        lblFormTitle = New Label()
        lblNomor = New Label()
        txtNomor = New NumericUpDown()
        lblPertanyaan = New Label()
        txtPertanyaan = New TextBox()
        lblKategori = New Label()
        cmbKategori = New ComboBox()
        lblCF = New Label()
        cmbCF = New ComboBox()
        chkActive = New CheckBox()
        chkPriority = New CheckBox()
        btnSave = New Button()
        btnUpdate = New Button()
        btnDelete = New Button()
        btnClear = New Button()
        rightPanel = New Panel()
        lblGridTitle = New Label()
        txtSearch = New TextBox()
        dgvQuestions = New DataGridView()
        leftPanel.SuspendLayout()
        CType(txtNomor, ComponentModel.ISupportInitialize).BeginInit()
        rightPanel.SuspendLayout()
        CType(dgvQuestions, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 18F, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblTitle.Location = New Point(30, 20)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(414, 41)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Manajemen Pertanyaan (CF)"
        ' 
        ' btnBack
        ' 
        btnBack.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnBack.BackColor = Color.White
        btnBack.FlatAppearance.BorderColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        btnBack.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        btnBack.Location = New Point(1000, 20)
        btnBack.Name = "btnBack"
        btnBack.Size = New Size(170, 50)
        btnBack.TabIndex = 1
        btnBack.Text = "⬅ Kembali"
        btnBack.UseVisualStyleBackColor = False
        ' 
        ' leftPanel
        ' 
        leftPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        leftPanel.BackColor = Color.White
        leftPanel.BorderStyle = BorderStyle.FixedSingle
        leftPanel.Controls.Add(lblFormTitle)
        leftPanel.Controls.Add(lblNomor)
        leftPanel.Controls.Add(txtNomor)
        leftPanel.Controls.Add(lblPertanyaan)
        leftPanel.Controls.Add(txtPertanyaan)
        leftPanel.Controls.Add(lblKategori)
        leftPanel.Controls.Add(cmbKategori)
        leftPanel.Controls.Add(lblCF)
        leftPanel.Controls.Add(cmbCF)
        leftPanel.Controls.Add(chkActive)
        leftPanel.Controls.Add(chkPriority)
        leftPanel.Controls.Add(btnSave)
        leftPanel.Controls.Add(btnUpdate)
        leftPanel.Controls.Add(btnDelete)
        leftPanel.Controls.Add(btnClear)
        leftPanel.Location = New Point(30, 80)
        leftPanel.Name = "leftPanel"
        leftPanel.Size = New Size(450, 640)
        leftPanel.TabIndex = 2
        ' 
        ' lblFormTitle
        ' 
        lblFormTitle.AutoSize = True
        lblFormTitle.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        lblFormTitle.Location = New Point(20, 20)
        lblFormTitle.Name = "lblFormTitle"
        lblFormTitle.Size = New Size(173, 28)
        lblFormTitle.TabIndex = 0
        lblFormTitle.Text = "Form Pertanyaan"
        ' 
        ' lblNomor
        ' 
        lblNomor.AutoSize = True
        lblNomor.Location = New Point(20, 65)
        lblNomor.Name = "lblNomor"
        lblNomor.Size = New Size(135, 20)
        lblNomor.TabIndex = 1
        lblNomor.Text = "Nomor Pertanyaan:"
        ' 
        ' txtNomor
        ' 
        txtNomor.Font = New Font("Segoe UI", 10F)
        txtNomor.Location = New Point(20, 90)
        txtNomor.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        txtNomor.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        txtNomor.Name = "txtNomor"
        txtNomor.Size = New Size(150, 30)
        txtNomor.TabIndex = 2
        txtNomor.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' lblPertanyaan
        ' 
        lblPertanyaan.AutoSize = True
        lblPertanyaan.Location = New Point(20, 135)
        lblPertanyaan.Name = "lblPertanyaan"
        lblPertanyaan.Size = New Size(102, 20)
        lblPertanyaan.TabIndex = 3
        lblPertanyaan.Text = "Isi Pertanyaan:"
        ' 
        ' txtPertanyaan
        ' 
        txtPertanyaan.Font = New Font("Segoe UI", 10F)
        txtPertanyaan.Location = New Point(20, 160)
        txtPertanyaan.Multiline = True
        txtPertanyaan.Name = "txtPertanyaan"
        txtPertanyaan.ScrollBars = ScrollBars.Vertical
        txtPertanyaan.Size = New Size(400, 100)
        txtPertanyaan.TabIndex = 4
        ' 
        ' lblKategori
        ' 
        lblKategori.AutoSize = True
        lblKategori.Location = New Point(20, 275)
        lblKategori.Name = "lblKategori"
        lblKategori.Size = New Size(114, 20)
        lblKategori.TabIndex = 5
        lblKategori.Text = "Target Kategori:"
        ' 
        ' cmbKategori
        ' 
        cmbKategori.DropDownStyle = ComboBoxStyle.DropDownList
        cmbKategori.Font = New Font("Segoe UI", 10F)
        cmbKategori.Location = New Point(20, 300)
        cmbKategori.Name = "cmbKategori"
        cmbKategori.Size = New Size(250, 31)
        cmbKategori.TabIndex = 6
        ' 
        ' lblCF
        ' 
        lblCF.AutoSize = True
        lblCF.Location = New Point(290, 275)
        lblCF.Name = "lblCF"
        lblCF.Size = New Size(67, 20)
        lblCF.TabIndex = 7
        lblCF.Text = "CF Pakar:"
        ' 
        ' cmbCF
        ' 
        cmbCF.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCF.Font = New Font("Segoe UI", 10F)
        cmbCF.Location = New Point(290, 300)
        cmbCF.Name = "cmbCF"
        cmbCF.Size = New Size(130, 31)
        cmbCF.TabIndex = 8
        ' 
        ' chkActive
        ' 
        chkActive.Checked = True
        chkActive.CheckState = CheckState.Checked
        chkActive.Location = New Point(20, 350)
        chkActive.Name = "chkActive"
        chkActive.Size = New Size(104, 24)
        chkActive.TabIndex = 9
        chkActive.Text = "Aktif"
        ' 
        ' chkPriority
        ' 
        chkPriority.Location = New Point(130, 350)
        chkPriority.Name = "chkPriority"
        chkPriority.Size = New Size(104, 24)
        chkPriority.TabIndex = 10
        chkPriority.Text = "Prioritas"
        ' 
        ' btnSave
        ' 
        btnSave.BackColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.ForeColor = Color.White
        btnSave.Location = New Point(20, 400)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(125, 45)
        btnSave.TabIndex = 11
        btnSave.Text = "➕ Simpan"
        btnSave.UseVisualStyleBackColor = False
        ' 
        ' btnUpdate
        ' 
        btnUpdate.BackColor = Color.FromArgb(CByte(52), CByte(152), CByte(219))
        btnUpdate.FlatAppearance.BorderSize = 0
        btnUpdate.FlatStyle = FlatStyle.Flat
        btnUpdate.ForeColor = Color.White
        btnUpdate.Location = New Point(155, 400)
        btnUpdate.Name = "btnUpdate"
        btnUpdate.Size = New Size(125, 45)
        btnUpdate.TabIndex = 12
        btnUpdate.Text = "💾 Update"
        btnUpdate.UseVisualStyleBackColor = False
        btnUpdate.Visible = False
        ' 
        ' btnDelete
        ' 
        btnDelete.BackColor = Color.FromArgb(CByte(231), CByte(76), CByte(60))
        btnDelete.FlatAppearance.BorderSize = 0
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.ForeColor = Color.White
        btnDelete.Location = New Point(290, 400)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(130, 45)
        btnDelete.TabIndex = 13
        btnDelete.Text = "🗑️ Hapus"
        btnDelete.UseVisualStyleBackColor = False
        btnDelete.Visible = False
        ' 
        ' btnClear
        ' 
        btnClear.BackColor = Color.White
        btnClear.FlatAppearance.BorderColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        btnClear.Location = New Point(20, 455)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(400, 45)
        btnClear.TabIndex = 14
        btnClear.Text = "🔄 Clear Form"
        btnClear.UseVisualStyleBackColor = False
        ' 
        ' rightPanel
        ' 
        rightPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        rightPanel.BackColor = Color.White
        rightPanel.BorderStyle = BorderStyle.FixedSingle
        rightPanel.Controls.Add(lblGridTitle)
        rightPanel.Controls.Add(txtSearch)
        rightPanel.Controls.Add(dgvQuestions)
        rightPanel.Location = New Point(500, 80)
        rightPanel.Name = "rightPanel"
        rightPanel.Size = New Size(670, 640)
        rightPanel.TabIndex = 3
        ' 
        ' lblGridTitle
        ' 
        lblGridTitle.AutoSize = True
        lblGridTitle.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        lblGridTitle.Location = New Point(20, 20)
        lblGridTitle.Name = "lblGridTitle"
        lblGridTitle.Size = New Size(186, 28)
        lblGridTitle.TabIndex = 0
        lblGridTitle.Text = "Daftar Pertanyaan"
        ' 
        ' txtSearch
        ' 
        txtSearch.Font = New Font("Segoe UI", 10F)
        txtSearch.Location = New Point(20, 55)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(300, 30)
        txtSearch.TabIndex = 1
        ' 
        ' dgvQuestions
        ' 
        dgvQuestions.AllowUserToAddRows = False
        dgvQuestions.AllowUserToDeleteRows = False
        dgvQuestions.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvQuestions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvQuestions.BackgroundColor = Color.White
        dgvQuestions.BorderStyle = BorderStyle.None
        dgvQuestions.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        dgvQuestions.ColumnHeadersHeight = 40
        dgvQuestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvQuestions.EnableHeadersVisualStyles = False
        dgvQuestions.Location = New Point(20, 100)
        dgvQuestions.Name = "dgvQuestions"
        dgvQuestions.ReadOnly = True
        dgvQuestions.RowHeadersVisible = False
        dgvQuestions.RowHeadersWidth = 51
        dgvQuestions.RowTemplate.Height = 35
        dgvQuestions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvQuestions.Size = New Size(630, 510)
        dgvQuestions.TabIndex = 2
        ' 
        ' QuestionCFManagementForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(247), CByte(250))
        ClientSize = New Size(1200, 750)
        Controls.Add(lblTitle)
        Controls.Add(btnBack)
        Controls.Add(leftPanel)
        Controls.Add(rightPanel)
        Name = "QuestionCFManagementForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Manajemen Pertanyaan - SITOPSI"
        leftPanel.ResumeLayout(False)
        leftPanel.PerformLayout()
        CType(txtNomor, ComponentModel.ISupportInitialize).EndInit()
        rightPanel.ResumeLayout(False)
        rightPanel.PerformLayout()
        CType(dgvQuestions, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents leftPanel As Panel
    Friend WithEvents rightPanel As Panel
    Friend WithEvents lblFormTitle As Label
    Friend WithEvents lblNomor As Label
    Friend WithEvents txtNomor As NumericUpDown
    Friend WithEvents lblPertanyaan As Label
    Friend WithEvents txtPertanyaan As TextBox
    Friend WithEvents lblKategori As Label
    Friend WithEvents cmbKategori As ComboBox
    Friend WithEvents lblCF As Label
    Friend WithEvents cmbCF As ComboBox
    Friend WithEvents chkActive As CheckBox
    Friend WithEvents chkPriority As CheckBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents lblGridTitle As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents dgvQuestions As DataGridView

End Class
