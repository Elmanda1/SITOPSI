<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RulesCombinationManagementForm
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
        lblCat1 = New Label()
        cmbCat1 = New ComboBox()
        lblCat2 = New Label()
        cmbCat2 = New ComboBox()
        lblCat3 = New Label()
        cmbCat3 = New ComboBox()
        lblRoleName = New Label()
        txtRoleName = New TextBox()
        lblDescription = New Label()
        txtDescription = New TextBox()
        btnAdd = New Button()
        btnUpdate = New Button()
        btnClear = New Button()
        
        lblGridTitle = New Label()
        txtSearch = New TextBox()
        dgvRules = New DataGridView()
        
        leftPanel.SuspendLayout()
        rightPanel.SuspendLayout()
        CType(dgvRules, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()

        ' 
        ' RulesCombinationManagementForm
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(245, 247, 250)
        ClientSize = New Size(1200, 750)
        Controls.Add(lblTitle)
        Controls.Add(btnBack)
        Controls.Add(leftPanel)
        Controls.Add(rightPanel)
        Name = "RulesCombinationManagementForm"
        Text = "Manajemen Kombinasi Rules - SITOPSI"
        StartPosition = FormStartPosition.CenterScreen

        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        lblTitle.ForeColor = navyColor
        lblTitle.Location = New Point(30, 20)
        lblTitle.Text = "Manajemen Kombinasi Rules"

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
        leftPanel.Controls.Add(lblCat1)
        leftPanel.Controls.Add(cmbCat1)
        leftPanel.Controls.Add(lblCat2)
        leftPanel.Controls.Add(cmbCat2)
        leftPanel.Controls.Add(lblCat3)
        leftPanel.Controls.Add(cmbCat3)
        leftPanel.Controls.Add(lblRoleName)
        leftPanel.Controls.Add(txtRoleName)
        leftPanel.Controls.Add(lblDescription)
        leftPanel.Controls.Add(txtDescription)
        leftPanel.Controls.Add(btnAdd)
        leftPanel.Controls.Add(btnUpdate)
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
        rightPanel.Controls.Add(dgvRules)

        ' -- Controls for Left Panel --

        lblFormTitle.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        lblFormTitle.Location = New Point(20, 20)
        lblFormTitle.AutoSize = True
        lblFormTitle.Text = "Form Rule Kombinasi"

        lblCat1.Location = New Point(20, 70)
        lblCat1.AutoSize = True
        lblCat1.Text = "Kategori 1 (Wajib):"

        cmbCat1.Location = New Point(20, 95)
        cmbCat1.Size = New Size(400, 28)
        cmbCat1.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCat1.Font = New Font("Segoe UI", 10.0F)

        lblCat2.Location = New Point(20, 145)
        lblCat2.AutoSize = True
        lblCat2.Text = "Kategori 2 (Wajib):"

        cmbCat2.Location = New Point(20, 170)
        cmbCat2.Size = New Size(400, 28)
        cmbCat2.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCat2.Font = New Font("Segoe UI", 10.0F)
        
        lblCat3.Location = New Point(20, 220)
        lblCat3.AutoSize = True
        lblCat3.Text = "Kategori 3 (Opsional):"

        cmbCat3.Location = New Point(20, 245)
        cmbCat3.Size = New Size(400, 28)
        cmbCat3.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCat3.Font = New Font("Segoe UI", 10.0F)

        lblRoleName.Location = New Point(20, 295)
        lblRoleName.AutoSize = True
        lblRoleName.Text = "Nama Role Hasil:"

        txtRoleName.Location = New Point(20, 320)
        txtRoleName.Size = New Size(400, 27)
        txtRoleName.Font = New Font("Segoe UI", 10.0F)
        
        lblDescription.Location = New Point(20, 370)
        lblDescription.AutoSize = True
        lblDescription.Text = "Deskripsi Role:"
        
        txtDescription.Location = New Point(20, 395)
        txtDescription.Size = New Size(400, 100)
        txtDescription.Multiline = True
        txtDescription.ScrollBars = ScrollBars.Vertical
        txtDescription.Font = New Font("Segoe UI", 10.0F)

        btnAdd.Location = New Point(20, 520)
        btnAdd.Size = New Size(125, 45)
        btnAdd.BackColor = navyColor
        btnAdd.ForeColor = Color.White
        btnAdd.Text = "➕ Tambah"
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.FlatAppearance.BorderSize = 0
        
        btnUpdate.Location = New Point(155, 520)
        btnUpdate.Size = New Size(125, 45)
        btnUpdate.BackColor = Color.FromArgb(52, 152, 219)
        btnUpdate.ForeColor = Color.White
        btnUpdate.Text = "💾 Update"
        btnUpdate.Enabled = False
        btnUpdate.FlatStyle = FlatStyle.Flat
        btnUpdate.FlatAppearance.BorderSize = 0

        btnClear.Location = New Point(290, 520)
        btnClear.Size = New Size(130, 45)
        btnClear.BackColor = Color.White
        btnClear.ForeColor = navyColor
        btnClear.Text = "🔄 Clear"
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.FlatAppearance.BorderSize = 1
        btnClear.FlatAppearance.BorderColor = navyColor

        ' -- Controls for Right Panel --

        lblGridTitle.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        lblGridTitle.Location = New Point(20, 20)
        lblGridTitle.AutoSize = True
        lblGridTitle.Text = "Daftar Kombinasi Rules"

        txtSearch.Location = New Point(20, 55)
        txtSearch.Size = New Size(300, 27)
        txtSearch.Font = New Font("Segoe UI", 10.0F)
        
        dgvRules.Location = New Point(20, 100)
        dgvRules.Size = New Size(630, 510)
        dgvRules.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ' Other DGV properties...
        dgvRules.BackgroundColor = Color.White
        dgvRules.BorderStyle = BorderStyle.None
        dgvRules.AllowUserToAddRows = False
        dgvRules.AllowUserToDeleteRows = False
        dgvRules.ReadOnly = True
        dgvRules.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvRules.RowHeadersVisible = False
        dgvRules.EnableHeadersVisualStyles = False
        dgvRules.ColumnHeadersHeight = 40
        dgvRules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvRules.ColumnHeadersDefaultCellStyle = dgvCellStyle1
        dgvRules.DefaultCellStyle = dgvCellStyle2
        dgvRules.RowsDefaultCellStyle = dgvCellStyle3
        dgvRules.RowTemplate.Height = 35
        
        leftPanel.ResumeLayout(False)
        leftPanel.PerformLayout()
        rightPanel.ResumeLayout(False)
        rightPanel.PerformLayout()
        CType(dgvRules, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents leftPanel As Panel
    Friend WithEvents rightPanel As Panel
    Friend WithEvents lblFormTitle As Label
    Friend WithEvents lblCat1 As Label
    Friend WithEvents cmbCat1 As ComboBox
    Friend WithEvents lblCat2 As Label
    Friend WithEvents cmbCat2 As ComboBox
    Friend WithEvents lblCat3 As Label
    Friend WithEvents cmbCat3 As ComboBox
    Friend WithEvents lblRoleName As Label
    Friend WithEvents txtRoleName As TextBox
    Friend WithEvents lblDescription As Label
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents lblGridTitle As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents dgvRules As DataGridView

End Class
