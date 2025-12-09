<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ChangePassword
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
        
        PictureBoxLogo = New PictureBox()
        cardPanel = New Panel()
        Label1 = New Label() ' Title
        Label2 = New Label() ' Username
        TextBox1 = New TextBox()
        Label3 = New Label() ' New Password
        TextBox2 = New TextBox()
        Label4 = New Label() ' Confirm Password
        TextBox3 = New TextBox()
        Button1 = New Button() ' Save
        Button2 = New Button() ' Back

        cardPanel.SuspendLayout()
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()

        ' 
        ' ChangePassword
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(240, 242, 245)
        ClientSize = New Size(600, 650)
        Controls.Add(PictureBoxLogo)
        Controls.Add(cardPanel)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "ChangePassword"
        StartPosition = FormStartPosition.CenterScreen
        Text = "SITOPSI - Ganti Password"

        ' 
        ' PictureBoxLogo
        ' 
        PictureBoxLogo.BackColor = Color.Transparent
        PictureBoxLogo.Image = System.Drawing.Image.FromFile("logo.png")
        PictureBoxLogo.Location = New Point((Me.ClientSize.Width - 100) \ 2, 20)
        PictureBoxLogo.Name = "PictureBoxLogo"
        PictureBoxLogo.Size = New Size(100, 80)
        PictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom
        PictureBoxLogo.TabIndex = 0
        PictureBoxLogo.TabStop = False

        ' 
        ' cardPanel
        ' 
        cardPanel.BackColor = Color.White
        cardPanel.BorderStyle = BorderStyle.FixedSingle
        cardPanel.Location = New Point(75, 120)
        cardPanel.Size = New Size(450, 480)
        cardPanel.Controls.Add(Label1)
        cardPanel.Controls.Add(Label2)
        cardPanel.Controls.Add(TextBox1)
        cardPanel.Controls.Add(Label3)
        cardPanel.Controls.Add(TextBox2)
        cardPanel.Controls.Add(Label4)
        cardPanel.Controls.Add(TextBox3)
        cardPanel.Controls.Add(Button1)
        cardPanel.Controls.Add(Button2)

        ' 
        ' Label1 (Title)
        ' 
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        Label1.ForeColor = navyColor
        Label1.Location = New Point(0, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(450, 41)
        Label1.TabIndex = 2
        Label1.Text = "Reset Password"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        
        ' 
        ' Label2 (Username)
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label2.ForeColor = navyColor
        Label2.Location = New Point(71, 80)
        Label2.Name = "Label2"
        Label2.Size = New Size(97, 23)
        Label2.TabIndex = 0
        Label2.Text = "Username"

        ' 
        ' TextBox1 (Username)
        ' 
        TextBox1.BackColor = Color.White
        TextBox1.ForeColor = Color.Black
        TextBox1.Font = New Font("Segoe UI", 10.0F)
        TextBox1.Location = New Point(75, 106)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(300, 40)
        TextBox1.TabIndex = 1
        TextBox1.Text = ""

        ' 
        ' Label3 (New Password)
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label3.ForeColor = navyColor
        Label3.Location = New Point(71, 160)
        Label3.Name = "Label3"
        Label3.Size = New Size(127, 23)
        Label3.TabIndex = 3
        Label3.Text = "Password Baru"

        ' 
        ' TextBox2 (New Password)
        ' 
        TextBox2.BackColor = Color.White
        TextBox2.ForeColor = Color.Black
        TextBox2.Font = New Font("Segoe UI", 10.0F)
        TextBox2.Location = New Point(75, 186)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(300, 40)
        TextBox2.TabIndex = 4
        TextBox2.PasswordChar = "●"c
        TextBox2.Text = ""
        
        ' 
        ' Label4 (Confirm Password)
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label4.ForeColor = navyColor
        Label4.Location = New Point(71, 240)
        Label4.Name = "Label4"
        Label4.Size = New Size(201, 23)
        Label4.TabIndex = 5
        Label4.Text = "Konfirmasi Password Baru"

        ' 
        ' TextBox3 (Confirm Password)
        ' 
        TextBox3.BackColor = Color.White
        TextBox3.ForeColor = Color.Black
        TextBox3.Font = New Font("Segoe UI", 10.0F)
        TextBox3.Location = New Point(75, 266)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(300, 40)
        TextBox3.TabIndex = 6
        TextBox3.PasswordChar = "●"c
        TextBox3.Text = ""

        ' 
        ' Button1 (Save)
        ' 
        Button1.BackColor = navyColor
        Button1.Cursor = Cursors.Hand
        Button1.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        Button1.ForeColor = Color.White
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0
        Button1.Location = New Point(75, 350)
        Button1.Name = "Button1"
        Button1.Size = New Size(300, 50)
        Button1.TabIndex = 7
        Button1.Text = "Ganti Password"

        ' 
        ' Button2 (Back)
        ' 
        Button2.BackColor = Color.White
        Button2.Cursor = Cursors.Hand
        Button2.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Button2.ForeColor = navyColor
        Button2.FlatStyle = FlatStyle.Flat
        Button2.FlatAppearance.BorderSize = 1
        Button2.FlatAppearance.BorderColor = navyColor
        Button2.Location = New Point(75, 410)
        Button2.Name = "Button2"
        Button2.Size = New Size(300, 50)
        Button2.TabIndex = 8
        Button2.Text = "⬅ Kembali ke Login"
        
        cardPanel.ResumeLayout(False)
        cardPanel.PerformLayout()
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents PictureBoxLogo As PictureBox
    Friend WithEvents cardPanel As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class