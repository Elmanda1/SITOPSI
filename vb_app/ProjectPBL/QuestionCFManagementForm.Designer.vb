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
        Dim dgvCellStyle1 As New DataGridViewCellStyle()
        Dim dgvCellStyle2 As New DataGridViewCellStyle()
        Dim dgvCellStyle3 As New DataGridViewCellStyle()
        Dim navyColor As Color = Color.FromArgb(44, 62, 80)
        
        lblTitle = New Label()
        btnBack = New Button()
        leftPanel = New Panel()
        rightPanel = New Panel()
        
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
        
        lblGridTitle = New Label()
        txtSearch = New TextBox()
        dgvQuestions = New DataGridView()
        
        leftPanel.SuspendLayout()
        rightPanel.SuspendLayout()
        CType(txtNomor, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvQuestions, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()

        ' 
        ' QuestionCFManagementForm
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(245, 247, 250)
        ClientSize = New Size(1200, 750)
        Controls.Add(lblTitle)
        Controls.Add(btnBack)
        Controls.Add(leftPanel)
        Controls.Add(rightPanel)
        Name = "QuestionCFManagementForm"
        Text = "Manajemen Pertanyaan - SITOPSI"
        StartPosition = FormStartPosition.CenterScreen

        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        lblTitle.ForeColor = navyColor
        lblTitle.Location = New Point(30, 20)
        lblTitle.Text = "Manajemen Pertanyaan (CF)"

        ' 
        ' btnBack
        ' 
        btnBack.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnBack.BackColor = Color.White
        btnBack.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        btnBack.ForeColor = navyColor
        btnBack.Location = New Point(1000, 20)
        btnBack.Size = New Size(170, 50)
        btnBack.Text = "⬅ Kembali"
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.FlatAppearance.BorderSize = 1
        btnBack.FlatAppearance.BorderColor = navyColor

        '
        ' leftPanel
        '
        leftPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        leftPanel.BackColor = Color.White
        leftPanel.Location = New Point(30, 80)
        leftPanel.Size = New Size(450, 640)
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

        '
        ' rightPanel
        '
        rightPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        rightPanel.BackColor = Color.White
        rightPanel.Location = New Point(500, 80)
        rightPanel.Size = New Size(670, 640)
        rightPanel.BorderStyle = BorderStyle.FixedSingle
        rightPanel.Controls.Add(lblGridTitle)
        rightPanel.Controls.Add(txtSearch)
        rightPanel.Controls.Add(dgvQuestions)

        ' -- Controls for Left Panel --

        lblFormTitle.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        lblFormTitle.Location = New Point(20, 20)
        lblFormTitle.AutoSize = True
        lblFormTitle.Text = "Form Pertanyaan"

        lblNomor.Location = New Point(20, 65)
        lblNomor.AutoSize = True
        lblNomor.Text = "Nomor Pertanyaan:"

        txtNomor.Location = New Point(20, 90)
        txtNomor.Size = New Size(150, 27)
        txtNomor.Font = New Font("Segoe UI", 10.0F)
        txtNomor.Minimum = 1
        txtNomor.Maximum = 10000

        lblPertanyaan.Location = New Point(20, 135)
        lblPertanyaan.AutoSize = True
        lblPertanyaan.Text = "Isi Pertanyaan:"

        txtPertanyaan.Location = New Point(20, 160)
        txtPertanyaan.Size = New Size(400, 100)
        txtPertanyaan.Multiline = True
        txtPertanyaan.ScrollBars = ScrollBars.Vertical
        txtPertanyaan.Font = New Font("Segoe UI", 10.0F)

        lblKategori.Location = New Point(20, 275)
        lblKategori.AutoSize = True
        lblKategori.Text = "Target Kategori:"

        cmbKategori.Location = New Point(20, 300)
        cmbKategori.Size = New Size(250, 28)
        cmbKategori.DropDownStyle = ComboBoxStyle.DropDownList
        cmbKategori.Font = New Font("Segoe UI", 10.0F)
        
        lblCF.Location = New Point(290, 275)
        lblCF.AutoSize = True
        lblCF.Text = "CF Pakar:"
        
        cmbCF.Location = New Point(290, 300)
        cmbCF.Size = New Size(130, 28)
        cmbCF.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCF.Font = New Font("Segoe UI", 10.0F)

        chkActive.Location = New Point(20, 350)
        chkActive.Text = "Aktif"
        chkActive.Checked = True
        
        chkPriority.Location = New Point(120, 350)
        chkPriority.Text = "Prioritas"

        btnSave.Location = New Point(20, 400)
        btnSave.Size = New Size(125, 45)
        btnSave.BackColor = navyColor
        btnSave.ForeColor = Color.White
        btnSave.Text = "➕ Simpan"
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 0
        
        btnUpdate.Location = New Point(155, 400)
        btnUpdate.Size = New Size(125, 45)
        btnUpdate.BackColor = Color.FromArgb(52, 152, 219)
        btnUpdate.ForeColor = Color.White
        btnUpdate.Text = "💾 Update"
        btnUpdate.Visible = False
        btnUpdate.FlatStyle = FlatStyle.Flat
        btnUpdate.FlatAppearance.BorderSize = 0

        btnDelete.Location = New Point(290, 400)
        btnDelete.Size = New Size(130, 45)
        btnDelete.BackColor = Color.FromArgb(231, 76, 60)
        btnDelete.ForeColor = Color.White
        btnDelete.Text = "🗑️ Hapus"
        btnDelete.Visible = False
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.FlatAppearance.BorderSize = 0

        btnClear.Location = New Point(20, 455)
        btnClear.Size = New Size(400, 45)
        btnClear.BackColor = Color.White
        btnClear.ForeColor = navyColor
        btnClear.Text = "🔄 Clear Form"
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.FlatAppearance.BorderSize = 1
        btnClear.FlatAppearance.BorderColor = navyColor

        ' -- Controls for Right Panel --

        lblGridTitle.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        lblGridTitle.Location = New Point(20, 20)
        lblGridTitle.AutoSize = True
        lblGridTitle.Text = "Daftar Pertanyaan"

        txtSearch.Location = New Point(20, 55)
        txtSearch.Size = New Size(300, 27)
        txtSearch.Font = New Font("Segoe UI", 10.0F)
        
        dgvQuestions.Location = New Point(20, 100)
        dgvQuestions.Size = New Size(630, 510)
        dgvQuestions.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvQuestions.BackgroundColor = Color.White
        dgvQuestions.BorderStyle = BorderStyle.None
        dgvQuestions.AllowUserToAddRows = False
        dgvQuestions.AllowUserToDeleteRows = False
        dgvQuestions.ReadOnly = True
        dgvQuestions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvQuestions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvQuestions.RowHeadersVisible = False
        dgvQuestions.EnableHeadersVisualStyles = False
        dgvQuestions.ColumnHeadersHeight = 40
        dgvQuestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvQuestions.ColumnHeadersDefaultCellStyle = dgvCellStyle1
        dgvQuestions.DefaultCellStyle = dgvCellStyle2
        dgvQuestions.RowsDefaultCellStyle = dgvCellStyle3
        dgvQuestions.RowTemplate.Height = 35

        leftPanel.ResumeLayout(False)
        leftPanel.PerformLayout()
        rightPanel.ResumeLayout(False)
        rightPanel.PerformLayout()
        CType(txtNomor, ComponentModel.ISupportInitialize).EndInit()
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
