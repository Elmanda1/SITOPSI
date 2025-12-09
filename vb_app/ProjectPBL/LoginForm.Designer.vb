<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoginForm
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
        Dim goldColor As Color = Color.FromArgb(242, 193, 59)

        cardPanel = New Panel()
        PictureBox1 = New PictureBox()
        titleLabel = New Label()
        usernameLabel = New Label()
        passwordLabel = New Label()
        forgotPasswordLink = New LinkLabel()
        usernameTextBox = New TextBox()
        passwordTextBox = New TextBox()
        loginButton = New Button()
        backButton = New Button()

        cardPanel.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()

        ' 
        ' LoginForm
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(240, 242, 245) ' Light Grey background
        ClientSize = New Size(800, 600)
        Controls.Add(cardPanel)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "LoginForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "SITOPSI - Login"

        ' 
        ' cardPanel
        ' 
        cardPanel.BackColor = Color.White
        cardPanel.BorderStyle = BorderStyle.FixedSingle
        cardPanel.Location = New Point(200, 50)
        cardPanel.Size = New Size(400, 500)
        cardPanel.Controls.Add(PictureBox1)
        cardPanel.Controls.Add(titleLabel)
        cardPanel.Controls.Add(usernameLabel)
        cardPanel.Controls.Add(passwordLabel)
        cardPanel.Controls.Add(forgotPasswordLink)
        cardPanel.Controls.Add(usernameTextBox)
        cardPanel.Controls.Add(passwordTextBox)
        cardPanel.Controls.Add(loginButton)
        cardPanel.Controls.Add(backButton)

        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.Image = System.Drawing.Image.FromFile("logo.png")
        PictureBox1.Location = New Point(150, 25)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(100, 80)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False

        ' 
        ' titleLabel
        ' 
        titleLabel.BackColor = Color.Transparent
        titleLabel.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        titleLabel.ForeColor = navyColor
        titleLabel.Location = New Point(0, 115)
        titleLabel.Name = "titleLabel"
        titleLabel.Size = New Size(400, 41)
        titleLabel.TabIndex = 1
        titleLabel.Text = "Login ke Akun"
        titleLabel.TextAlign = ContentAlignment.MiddleCenter

        ' 
        ' usernameLabel
        ' 
        usernameLabel.AutoSize = True
        usernameLabel.BackColor = Color.Transparent
        usernameLabel.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        usernameLabel.ForeColor = navyColor
        usernameLabel.Location = New Point(46, 180)
        usernameLabel.Name = "usernameLabel"
        usernameLabel.Size = New Size(97, 23)
        usernameLabel.TabIndex = 2
        usernameLabel.Text = "Username"

        ' 
        ' passwordLabel
        ' 
        passwordLabel.AutoSize = True
        passwordLabel.BackColor = Color.Transparent
        passwordLabel.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        passwordLabel.ForeColor = navyColor
        passwordLabel.Location = New Point(46, 260)
        passwordLabel.Name = "passwordLabel"
        passwordLabel.Size = New Size(85, 23)
        passwordLabel.TabIndex = 4
        passwordLabel.Text = "Password"
        
        ' 
        ' forgotPasswordLink
        ' 
        forgotPasswordLink.AutoSize = True
        forgotPasswordLink.BackColor = Color.Transparent
        forgotPasswordLink.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        forgotPasswordLink.Location = New Point(250, 435)
        forgotPasswordLink.Name = "forgotPasswordLink"
        forgotPasswordLink.Size = New Size(120, 20)
        forgotPasswordLink.TabIndex = 8
        forgotPasswordLink.TabStop = True
        forgotPasswordLink.Text = "Lupa Password?"
        forgotPasswordLink.LinkColor = navyColor
        forgotPasswordLink.ActiveLinkColor = goldColor

        ' 
        ' usernameTextBox
        ' 
        usernameTextBox.BackColor = Color.White
        usernameTextBox.ForeColor = Color.Black
        usernameTextBox.Font = New Font("Segoe UI", 10.0F)
        usernameTextBox.Location = New Point(50, 208)
        usernameTextBox.Name = "usernameTextBox"
        usernameTextBox.Size = New Size(300, 40)
        usernameTextBox.TabIndex = 3
        usernameTextBox.Text = ""

        ' 
        ' passwordTextBox
        ' 
        passwordTextBox.BackColor = Color.White
        passwordTextBox.ForeColor = Color.Black
        passwordTextBox.Font = New Font("Segoe UI", 10.0F)
        passwordTextBox.Location = New Point(50, 288)
        passwordTextBox.Name = "passwordTextBox"
        passwordTextBox.Size = New Size(300, 40)
        passwordTextBox.TabIndex = 5
        passwordTextBox.PasswordChar = "●"c
        passwordTextBox.Text = ""

        ' 
        ' loginButton
        ' 
        loginButton.BackColor = navyColor
        loginButton.Cursor = Cursors.Hand
        loginButton.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        loginButton.ForeColor = Color.White
        loginButton.FlatStyle = FlatStyle.Flat
        loginButton.FlatAppearance.BorderSize = 0
        loginButton.Location = New Point(50, 355)
        loginButton.Name = "loginButton"
        loginButton.Size = New Size(300, 50)
        loginButton.TabIndex = 6
        loginButton.Text = "Login"

        ' 
        ' backButton
        ' 
        backButton.BackColor = Color.White
        backButton.Cursor = Cursors.Hand
        backButton.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        backButton.ForeColor = navyColor
        backButton.FlatStyle = FlatStyle.Flat
        backButton.FlatAppearance.BorderSize = 1
        backButton.FlatAppearance.BorderColor = navyColor
        backButton.Location = New Point(50, 425)
        backButton.Name = "backButton"
        backButton.Size = New Size(140, 40)
        backButton.TabIndex = 7
        backButton.Text = "Kembali"
        
        cardPanel.ResumeLayout(False)
        cardPanel.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents cardPanel As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents titleLabel As Label
    Friend WithEvents usernameLabel As Label
    Friend WithEvents passwordLabel As Label
    Friend WithEvents forgotPasswordLink As LinkLabel
    Friend WithEvents usernameTextBox As TextBox
    Friend WithEvents passwordTextBox As TextBox
    Friend WithEvents loginButton As Button
    Friend WithEvents backButton As Button
End Class