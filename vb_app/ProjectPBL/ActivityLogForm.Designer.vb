<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ActivityLogForm
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
        mainPanel = New Panel()
        lblGridTitle = New Label()
        txtSearch = New RoundedTextBox()
        lblFilter = New Label()
        cmbFilterAksi = New ComboBox()
        btnRefresh = New Button()
        dgvLogs = New DataGridView()
        
        mainPanel.SuspendLayout()
        CType(dgvLogs, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()

        ' 
        ' ActivityLogForm
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(245, 247, 250)
        ClientSize = New Size(1100, 700)
        Controls.Add(lblTitle)
        Controls.Add(btnBack)
        Controls.Add(mainPanel)
        Name = "ActivityLogForm"
        Text = "Log Aktivitas - SITOPSI"
        StartPosition = FormStartPosition.CenterScreen

        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        lblTitle.ForeColor = navyColor
        lblTitle.Location = New Point(30, 20)
        lblTitle.Text = "Log Aktivitas"

        ' 
        ' btnBack
        ' 
        btnBack.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnBack.BackColor = Color.White
        btnBack.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        btnBack.ForeColor = navyColor
        btnBack.Location = New Point(900, 20)
        btnBack.Size = New Size(170, 50)
        btnBack.Text = "⬅ Kembali"
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.FlatAppearance.BorderSize = 1
        btnBack.FlatAppearance.BorderColor = navyColor

        '
        ' mainPanel
        '
        mainPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        mainPanel.BackColor = Color.White
        mainPanel.Location = New Point(30, 80)
        mainPanel.Size = New Size(1040, 580)
        mainPanel.BorderStyle = BorderStyle.FixedSingle
        mainPanel.Controls.Add(lblGridTitle)
        mainPanel.Controls.Add(txtSearch)
        mainPanel.Controls.Add(lblFilter)
        mainPanel.Controls.Add(cmbFilterAksi)
        mainPanel.Controls.Add(btnRefresh)
        mainPanel.Controls.Add(dgvLogs)

        ' -- Controls for mainPanel --

        lblGridTitle.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        lblGridTitle.Location = New Point(20, 20)
        lblGridTitle.AutoSize = True
        lblGridTitle.Text = "Riwayat Aktivitas"

        txtSearch.Location = New Point(20, 55)
        txtSearch.Size = New Size(250, 40)
        
        lblFilter.Location = New Point(290, 65)
        lblFilter.AutoSize = True
        lblFilter.Text = "Filter Aksi:"

        cmbFilterAksi.Location = New Point(360, 55)
        cmbFilterAksi.Size = New Size(180, 28)
        cmbFilterAksi.DropDownStyle = ComboBoxStyle.DropDownList
        
        btnRefresh.Location = New Point(550, 55)
        btnRefresh.Size = New Size(120, 40)
        btnRefresh.BackColor = navyColor
        btnRefresh.ForeColor = Color.White
        btnRefresh.Text = "🔃 Refresh"
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0

        dgvLogs.Location = New Point(20, 110)
        dgvLogs.Size = New Size(1000, 450)
        dgvLogs.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvLogs.BackgroundColor = Color.White
        dgvLogs.BorderStyle = BorderStyle.None
        dgvLogs.AllowUserToAddRows = False
        dgvLogs.AllowUserToDeleteRows = False
        dgvLogs.ReadOnly = True
        dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvLogs.RowHeadersVisible = False
        dgvLogs.EnableHeadersVisualStyles = False
        dgvLogs.ColumnHeadersHeight = 40
        dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvLogs.ColumnHeadersDefaultCellStyle = dgvCellStyle1
        dgvLogs.DefaultCellStyle = dgvCellStyle2
        dgvLogs.RowsDefaultCellStyle = dgvCellStyle3
        dgvLogs.RowTemplate.Height = 35
        
        mainPanel.ResumeLayout(False)
        mainPanel.PerformLayout()
        CType(dgvLogs, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents mainPanel As Panel
    Friend WithEvents lblGridTitle As Label
    Friend WithEvents txtSearch As RoundedTextBox
    Friend WithEvents lblFilter As Label
    Friend WithEvents cmbFilterAksi As ComboBox
    Friend WithEvents btnRefresh As Button
    Friend WithEvents dgvLogs As DataGridView

End Class