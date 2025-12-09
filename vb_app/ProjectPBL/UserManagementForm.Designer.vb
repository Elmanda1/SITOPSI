<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UserManagementForm
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
        mainPanel = New Panel()
        dgvUsers = New DataGridView()
        txtSearch = New TextBox()
        btnRefresh = New Button()
        btnBack = New Button()
        
        mainPanel.SuspendLayout()
        CType(dgvUsers, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()

        ' 
        ' UserManagementForm
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(245, 247, 250)
        ClientSize = New Size(1100, 700)
        Controls.Add(lblTitle)
        Controls.Add(mainPanel)
        Controls.Add(btnBack)
        Name = "UserManagementForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Manajemen User - SITOPSI"

        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        lblTitle.ForeColor = navyColor
        lblTitle.Location = New Point(30, 20)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(284, 41)
        lblTitle.Text = "Manajemen User"

        ' 
        ' mainPanel
        ' 
        mainPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        mainPanel.BackColor = Color.White
        mainPanel.Location = New Point(30, 80)
        mainPanel.Size = New Size(1040, 520)
        mainPanel.BorderStyle = BorderStyle.FixedSingle
        mainPanel.Controls.Add(txtSearch)
        mainPanel.Controls.Add(btnRefresh)
        mainPanel.Controls.Add(dgvUsers)

        '
        ' txtSearch
        '
        txtSearch.Font = New Font("Segoe UI", 11.0F)
        txtSearch.Location = New Point(20, 20)
        txtSearch.Size = New Size(300, 32)
        txtSearch.PlaceholderText = "Cari user..."

        '
        ' btnRefresh
        '
        btnRefresh.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnRefresh.BackColor = navyColor
        btnRefresh.ForeColor = Color.White
        btnRefresh.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnRefresh.Location = New Point(900, 20)
        btnRefresh.Size = New Size(120, 40)
        btnRefresh.Text = "🔃 Refresh"
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0

        ' 
        ' dgvUsers
        ' 
        dgvUsers.AllowUserToAddRows = False
        dgvUsers.AllowUserToDeleteRows = False
        dgvUsers.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvUsers.BackgroundColor = Color.White
        dgvUsers.BorderStyle = BorderStyle.None
        dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        dgvCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgvCellStyle1.BackColor = navyColor
        dgvCellStyle1.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        dgvCellStyle1.ForeColor = Color.White
        dgvCellStyle1.SelectionBackColor = navyColor
        dgvCellStyle1.SelectionForeColor = SystemColors.HighlightText
        dgvCellStyle1.WrapMode = DataGridViewTriState.True
        dgvUsers.ColumnHeadersDefaultCellStyle = dgvCellStyle1
        dgvUsers.ColumnHeadersHeight = 40
        dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgvCellStyle2.BackColor = SystemColors.Window
        dgvCellStyle2.Font = New Font("Segoe UI", 9.0F)
        dgvCellStyle2.ForeColor = SystemColors.ControlText
        dgvCellStyle2.SelectionBackColor = SystemColors.Highlight
        dgvCellStyle2.SelectionForeColor = SystemColors.HighlightText
        dgvCellStyle2.WrapMode = DataGridViewTriState.False
        dgvUsers.DefaultCellStyle = dgvCellStyle2
        dgvUsers.EnableHeadersVisualStyles = False
        dgvUsers.GridColor = Color.Gainsboro
        dgvUsers.Location = New Point(20, 80)
        dgvUsers.Name = "dgvUsers"
        dgvUsers.ReadOnly = True
        dgvUsers.RowHeadersVisible = False
        dgvCellStyle3.Padding = New Padding(5)
        dgvCellStyle3.SelectionBackColor = Color.FromArgb(220, 235, 255)
        dgvCellStyle3.SelectionForeColor = Color.Black
        dgvUsers.RowsDefaultCellStyle = dgvCellStyle3
        dgvUsers.RowTemplate.Height = 35
        dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvUsers.Size = New Size(1000, 420)
        
        ' 
        ' btnBack
        ' 
        btnBack.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnBack.BackColor = Color.White
        btnBack.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        btnBack.ForeColor = navyColor
        btnBack.Location = New Point(900, 620)
        btnBack.Size = New Size(170, 50)
        btnBack.Text = "⬅ Kembali"
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.FlatAppearance.BorderSize = 1
        btnBack.FlatAppearance.BorderColor = navyColor
        
        mainPanel.ResumeLayout(False)
        mainPanel.PerformLayout()
        CType(dgvUsers, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents mainPanel As Panel
    Friend WithEvents dgvUsers As DataGridView
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnBack As Button
End Class
