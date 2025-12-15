<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProfileForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim navyColor As Color = Color.FromArgb(12, 45, 72)
        Dim lightGrayColor As Color = Color.FromArgb(245, 247, 250)
        Dim whiteColor As Color = Color.White
        Dim blackColor As Color = Color.Black
        
        cardPanel = New Panel()
        lblTitle = New Label()
        
        ' Profile Picture
        picProfile = New PictureBox()
        
        ' Main layout
        mainTableLayoutPanel = New TableLayoutPanel()

        ' Details Panel
        detailsTableLayoutPanel = New TableLayoutPanel()
        lblFullName = New Label()
        txtFullName = New TextBox()
        lblUsername = New Label()
        txtUsername = New TextBox()
        lblEmail = New Label()
        txtEmail = New TextBox()
        lblPhone = New Label()
        txtPhone = New TextBox()

        ' Stats Panel
        statsTableLayoutPanel = New TableLayoutPanel()

        btnSaveChanges = New Button()
        btnBack = New Button()
        
        cardPanel.SuspendLayout()
        CType(picProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        mainTableLayoutPanel.SuspendLayout()
        detailsTableLayoutPanel.SuspendLayout()
        statsTableLayoutPanel.SuspendLayout()
        SuspendLayout()

        ' 
        ' ProfileForm
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = lightGrayColor
        ClientSize = New Size(800, 750)
        Controls.Add(cardPanel)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "ProfileForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Profile Saya - SITOPSI"

        ' 
        ' cardPanel
        ' 
        cardPanel.BackColor = whiteColor
        cardPanel.BorderStyle = BorderStyle.FixedSingle
        cardPanel.Location = New Point(50, 50)
        cardPanel.Size = New Size(700, 650)
        cardPanel.Controls.Add(lblTitle)
        cardPanel.Controls.Add(picProfile)
        cardPanel.Controls.Add(mainTableLayoutPanel)
        cardPanel.Controls.Add(btnSaveChanges)
        cardPanel.Controls.Add(btnBack)
        
        '
        ' lblTitle
        ' 
        lblTitle.AutoSize = False
        lblTitle.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        lblTitle.ForeColor = navyColor
        lblTitle.Location = New Point(0, 20)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(700, 41)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Profile Saya"
        lblTitle.TextAlign = ContentAlignment.MiddleCenter

        '
        ' mainTableLayoutPanel
        '
        mainTableLayoutPanel.ColumnCount = 2
        mainTableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        mainTableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        mainTableLayoutPanel.Controls.Add(detailsTableLayoutPanel, 0, 0)
        mainTableLayoutPanel.Controls.Add(statsTableLayoutPanel, 1, 0)
        mainTableLayoutPanel.Location = New Point(25, 180)
        mainTableLayoutPanel.Name = "mainTableLayoutPanel"
        mainTableLayoutPanel.RowCount = 1
        mainTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
        mainTableLayoutPanel.Size = New Size(650, 320)
        
        '
        ' detailsTableLayoutPanel
        '
        detailsTableLayoutPanel.ColumnCount = 1
        detailsTableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        detailsTableLayoutPanel.Controls.Add(lblFullName, 0, 0)
        detailsTableLayoutPanel.Controls.Add(txtFullName, 0, 1)
        detailsTableLayoutPanel.Controls.Add(lblUsername, 0, 2)
        detailsTableLayoutPanel.Controls.Add(txtUsername, 0, 3)
        detailsTableLayoutPanel.Controls.Add(lblEmail, 0, 4)
        detailsTableLayoutPanel.Controls.Add(txtEmail, 0, 5)
        detailsTableLayoutPanel.Controls.Add(lblPhone, 0, 6)
        detailsTableLayoutPanel.Controls.Add(txtPhone, 0, 7)
        detailsTableLayoutPanel.Dock = DockStyle.Fill
        detailsTableLayoutPanel.Name = "detailsTableLayoutPanel"
        detailsTableLayoutPanel.RowCount = 8
        detailsTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 30.0F))
        detailsTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 40.0F))
        detailsTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 30.0F))
        detailsTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 40.0F))
        detailsTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 30.0F))
        detailsTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 40.0F))
        detailsTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 30.0F))
        detailsTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 40.0F))

        ' Full Name
        lblFullName.Text = "Nama Lengkap"
        lblFullName.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblFullName.ForeColor = navyColor
        txtFullName.Size = New Size(280, 35)
        txtFullName.Font = New Font("Segoe UI", 10.0F)

        ' Username
        lblUsername.Text = "Username"
        lblUsername.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblUsername.ForeColor = navyColor
        txtUsername.Size = New Size(280, 35)
        txtUsername.Font = New Font("Segoe UI", 10.0F)
        txtUsername.BackColor = lightGrayColor
        ' txtUsername will be set to Enabled = False in code-behind

        ' Email
        lblEmail.Text = "Email"
        lblEmail.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblEmail.ForeColor = navyColor
        txtEmail.Size = New Size(280, 35)
        txtEmail.Font = New Font("Segoe UI", 10.0F)

        ' Phone
        lblPhone.Text = "No. Telepon"
        lblPhone.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblPhone.ForeColor = navyColor
        txtPhone.Size = New Size(280, 35)
        txtPhone.Font = New Font("Segoe UI", 10.0F)
        
        '
        ' statsTableLayoutPanel
        '
        statsTableLayoutPanel.ColumnCount = 1
        statsTableLayoutPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        statsTableLayoutPanel.Dock = DockStyle.Fill
        statsTableLayoutPanel.Name = "statsTableLayoutPanel"
        statsTableLayoutPanel.RowCount = 14
        For i As Integer = 0 To 13
            statsTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F / 14.0F))
        Next

        ' Save Button
        btnSaveChanges.BackColor = navyColor
        btnSaveChanges.Cursor = Cursors.Hand
        btnSaveChanges.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        btnSaveChanges.ForeColor = whiteColor
        btnSaveChanges.FlatStyle = FlatStyle.Flat
        btnSaveChanges.FlatAppearance.BorderSize = 0
        btnSaveChanges.Location = New Point(200, 520)
        btnSaveChanges.Size = New Size(300, 50)
        btnSaveChanges.Text = "Simpan Perubahan"
        
        ' Back Button
        btnBack.BackColor = whiteColor
        btnBack.Cursor = Cursors.Hand
        btnBack.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnBack.ForeColor = navyColor
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.FlatAppearance.BorderSize = 1
        btnBack.FlatAppearance.BorderColor = navyColor
        btnBack.Location = New Point(200, 580)
        btnBack.Size = New Size(300, 50)
        btnBack.Text = "Kembali"
        
        cardPanel.ResumeLayout(False)
        cardPanel.PerformLayout()
        CType(picProfile, System.ComponentModel.ISupportInitialize).EndInit()
        mainTableLayoutPanel.ResumeLayout(False)
        detailsTableLayoutPanel.ResumeLayout(False)
        statsTableLayoutPanel.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents cardPanel As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents picProfile As PictureBox
    Friend WithEvents mainTableLayoutPanel As TableLayoutPanel
    Friend WithEvents detailsTableLayoutPanel As TableLayoutPanel
    Friend WithEvents statsTableLayoutPanel As TableLayoutPanel
    Friend WithEvents lblFullName As Label
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents lblUsername As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents lblEmail As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lblPhone As Label
    Friend WithEvents txtPhone As TextBox
    Friend WithEvents btnSaveChanges As Button
    Friend WithEvents btnBack As Button
End Class