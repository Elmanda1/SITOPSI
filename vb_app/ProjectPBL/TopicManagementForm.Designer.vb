<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TopicManagementForm
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
        lblCategory = New Label()
        cmbCategory = New ComboBox()
        lblTargetRole = New Label()
        txtTargetRole = New TextBox()
        lblJudul = New Label()
        txtJudul = New TextBox()
        lblDeskripsi = New Label()
        txtDeskripsi = New TextBox()
        chkActive = New CheckBox()
        chkFeasible = New CheckBox()
        btnAdd = New Button()
        btnUpdate = New Button()
        btnClear = New Button()
        
        lblGridTitle = New Label()
        txtSearch = New TextBox()
        dgvTopics = New DataGridView()
        
        leftPanel.SuspendLayout()
        rightPanel.SuspendLayout()
        CType(dgvTopics, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()

        ' 
        ' TopicManagementForm
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(245, 247, 250)
        ClientSize = New Size(1200, 750)
        Controls.Add(lblTitle)
        Controls.Add(btnBack)
        Controls.Add(leftPanel)
        Controls.Add(rightPanel)
        Name = "TopicManagementForm"
        Text = "Manajemen Topik Skripsi - SITOPSI"
        StartPosition = FormStartPosition.CenterScreen

        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        lblTitle.ForeColor = navyColor
        lblTitle.Location = New Point(30, 20)
        lblTitle.Text = "Manajemen Topik Skripsi"

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
        leftPanel.Controls.Add(lblCategory)
        leftPanel.Controls.Add(cmbCategory)
        leftPanel.Controls.Add(lblTargetRole)
        leftPanel.Controls.Add(txtTargetRole)
        leftPanel.Controls.Add(lblJudul)
        leftPanel.Controls.Add(txtJudul)
        leftPanel.Controls.Add(lblDeskripsi)
        leftPanel.Controls.Add(txtDeskripsi)
        leftPanel.Controls.Add(chkActive)
        leftPanel.Controls.Add(chkFeasible)
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
        rightPanel.Controls.Add(dgvTopics)

        ' -- Controls for Left Panel --

        lblFormTitle.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        lblFormTitle.Location = New Point(20, 20)
        lblFormTitle.AutoSize = True
        lblFormTitle.Text = "Form Topik"

        lblCategory.Location = New Point(20, 70)
        lblCategory.AutoSize = True
        lblCategory.Text = "Kategori:"

        cmbCategory.Location = New Point(20, 95)
        cmbCategory.Size = New Size(400, 28)
        cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCategory.Font = New Font("Segoe UI", 10.0F)

        lblTargetRole.Location = New Point(20, 145)
        lblTargetRole.AutoSize = True
        lblTargetRole.Text = "Target Role (Opsional):"

        txtTargetRole.Location = New Point(20, 170)
        txtTargetRole.Size = New Size(400, 27)
        txtTargetRole.Font = New Font("Segoe UI", 10.0F)
        
        lblJudul.Location = New Point(20, 220)
        lblJudul.AutoSize = True
        lblJudul.Text = "Judul Topik:"
        
        txtJudul.Location = New Point(20, 245)
        txtJudul.Size = New Size(400, 60)
        txtJudul.Multiline = True
        txtJudul.ScrollBars = ScrollBars.Vertical
        txtJudul.Font = New Font("Segoe UI", 10.0F)

        lblDeskripsi.Location = New Point(20, 320)
        lblDeskripsi.AutoSize = True
        lblDeskripsi.Text = "Deskripsi:"
        
        txtDeskripsi.Location = New Point(20, 345)
        txtDeskripsi.Size = New Size(400, 100)
        txtDeskripsi.Multiline = True
        txtDeskripsi.ScrollBars = ScrollBars.Vertical
        txtDeskripsi.Font = New Font("Segoe UI", 10.0F)

        chkActive.Location = New Point(20, 460)
        chkActive.Text = "Aktif (Ditampilkan)"
        chkActive.Checked = True
        
        chkFeasible.Location = New Point(180, 460)
        chkFeasible.Text = "Layak (Sudah direview)"
        chkFeasible.Checked = True

        btnAdd.Location = New Point(20, 500)
        btnAdd.Size = New Size(125, 45)
        btnAdd.BackColor = navyColor
        btnAdd.ForeColor = Color.White
        btnAdd.Text = "➕ Tambah"
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.FlatAppearance.BorderSize = 0
        
        btnUpdate.Location = New Point(155, 500)
        btnUpdate.Size = New Size(125, 45)
        btnUpdate.BackColor = Color.FromArgb(52, 152, 219)
        btnUpdate.ForeColor = Color.White
        btnUpdate.Text = "💾 Update"
        btnUpdate.Enabled = False
        btnUpdate.FlatStyle = FlatStyle.Flat
        btnUpdate.FlatAppearance.BorderSize = 0

        btnClear.Location = New Point(290, 500)
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
        lblGridTitle.Text = "Daftar Topik"

        txtSearch.Location = New Point(20, 55)
        txtSearch.Size = New Size(300, 27)
        txtSearch.Font = New Font("Segoe UI", 10.0F)
        
        dgvTopics.Location = New Point(20, 100)
        dgvTopics.Size = New Size(630, 510)
        dgvTopics.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ' Other DGV properties...
        dgvTopics.BackgroundColor = Color.White
        dgvTopics.BorderStyle = BorderStyle.None
        dgvTopics.AllowUserToAddRows = False
        dgvTopics.AllowUserToDeleteRows = False
        dgvTopics.ReadOnly = True
        dgvTopics.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvTopics.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvTopics.RowHeadersVisible = False
        dgvTopics.EnableHeadersVisualStyles = False
        dgvTopics.ColumnHeadersHeight = 40
        dgvTopics.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvTopics.ColumnHeadersDefaultCellStyle = dgvCellStyle1
        dgvTopics.DefaultCellStyle = dgvCellStyle2
        dgvTopics.RowsDefaultCellStyle = dgvCellStyle3
        dgvTopics.RowTemplate.Height = 35
        
        leftPanel.ResumeLayout(False)
        leftPanel.PerformLayout()
        rightPanel.ResumeLayout(False)
        rightPanel.PerformLayout()
        CType(dgvTopics, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents leftPanel As Panel
    Friend WithEvents rightPanel As Panel
    Friend WithEvents lblFormTitle As Label
    Friend WithEvents lblCategory As Label
    Friend WithEvents cmbCategory As ComboBox
    Friend WithEvents lblTargetRole As Label
    Friend WithEvents txtTargetRole As TextBox
    Friend WithEvents lblJudul As Label
    Friend WithEvents txtJudul As TextBox
    Friend WithEvents lblDeskripsi As Label
    Friend WithEvents txtDeskripsi As TextBox
    Friend WithEvents chkActive As CheckBox
    Friend WithEvents chkFeasible As CheckBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents lblGridTitle As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents dgvTopics As DataGridView

End Class