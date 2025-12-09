<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RegisterForm
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
        
        PictureBoxLogo = New PictureBox() ' New logo
        cardPanel = New Panel()
        Label1 = New Label() ' Title
        Label2 = New Label() ' Nama Lengkap
        TextBox1 = New TextBox()
        Label3 = New Label() ' No. Telepon
        TextBox2 = New TextBox()
        Label4 = New Label() ' Email
        TextBox3 = New TextBox()
        Label6 = New Label() ' Username
        TextBox4 = New TextBox()
        Label7 = New Label() ' Password
        TextBox5 = New TextBox()
        Label5 = New Label() ' Jenis Kelamin
        RadioButton1 = New RadioButton()
        RadioButton2 = New RadioButton()
        Button1 = New Button() ' Register
        Button2 = New Button() ' Kembali
        
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).BeginInit() ' Initialize new PictureBox
        cardPanel.SuspendLayout()
        SuspendLayout()

        ' 
        ' RegisterForm
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(240, 242, 245) ' Light Grey background
        ClientSize = New Size(600, 750)
        Controls.Add(PictureBoxLogo) ' Add logo to form controls
        Controls.Add(cardPanel)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "RegisterForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "SITOPSI - Daftar Akun"

        ' 
        ' PictureBoxLogo
        ' 
        PictureBoxLogo.BackColor = Color.Transparent
        PictureBoxLogo.Image = System.Drawing.Image.FromFile("logo.png")
        PictureBoxLogo.Location = New Point((Me.ClientSize.Width - 100) \ 2, 20) ' Centered, 20px from top
        PictureBoxLogo.Name = "PictureBoxLogo"
        PictureBoxLogo.Size = New Size(100, 80)
        PictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom
        PictureBoxLogo.TabIndex = 0
        PictureBoxLogo.TabStop = False

        ' 
        ' cardPanel
        ' 
        cardPanel.AutoScroll = True
        cardPanel.BackColor = Color.White
        cardPanel.BorderStyle = BorderStyle.FixedSingle
        cardPanel.Location = New Point(75, 120) ' Adjusted location to make space for logo
        cardPanel.Size = New Size(450, 600) ' Adjusted size to fit within form with logo
        cardPanel.Controls.Add(Label1)
        cardPanel.Controls.Add(Label2)
        cardPanel.Controls.Add(TextBox1)
        cardPanel.Controls.Add(Label3)
        cardPanel.Controls.Add(TextBox2)
        cardPanel.Controls.Add(Label4)
        cardPanel.Controls.Add(TextBox3)
        cardPanel.Controls.Add(Label6)
        cardPanel.Controls.Add(TextBox4)
        cardPanel.Controls.Add(Label7)
        cardPanel.Controls.Add(TextBox5)
        cardPanel.Controls.Add(Label5)
        cardPanel.Controls.Add(RadioButton1)
        cardPanel.Controls.Add(RadioButton2)
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
        Label1.TabIndex = 0
        Label1.Text = "Daftar Akun Baru"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        
        '
        ' Label2 (Full Name)
        '
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label2.ForeColor = navyColor
        Label2.Location = New Point(71, 80)
        Label2.Name = "Label2"
        Label2.Size = New Size(134, 23)
        Label2.TabIndex = 2
        Label2.Text = "Nama Lengkap"

        '
        ' TextBox1 (Full Name)
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
        ' Label3 (Phone)
        '
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label3.ForeColor = navyColor
        Label3.Location = New Point(71, 160)
        Label3.Name = "Label3"
        Label3.Size = New Size(108, 23)
        Label3.TabIndex = 4
        Label3.Text = "No. Telepon"
        
        '
        ' TextBox2 (Phone)
        '
        TextBox2.BackColor = Color.White
        TextBox2.ForeColor = Color.Black
        TextBox2.Font = New Font("Segoe UI", 10.0F)
        TextBox2.Location = New Point(75, 186)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(300, 40)
        TextBox2.TabIndex = 3
        TextBox2.Text = ""

        '
        ' Label4 (Email)
        '
        Label4.AutoSize = True
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label4.ForeColor = navyColor
        Label4.Location = New Point(71, 240)
        Label4.Name = "Label4"
        Label4.Size = New Size(54, 23)
        Label4.TabIndex = 6
        Label4.Text = "Email"
        
        '
        ' TextBox3 (Email)
        '
        TextBox3.BackColor = Color.White
        TextBox3.ForeColor = Color.Black
        TextBox3.Font = New Font("Segoe UI", 10.0F)
        TextBox3.Location = New Point(75, 266)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(300, 40)
        TextBox3.TabIndex = 5
        TextBox3.Text = ""

        '
        ' Label6 (Username)
        '
        Label6.AutoSize = True
        Label6.BackColor = Color.Transparent
        Label6.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label6.ForeColor = navyColor
        Label6.Location = New Point(71, 320)
        Label6.Name = "Label6"
        Label6.Size = New Size(97, 23)
        Label6.TabIndex = 9
        Label6.Text = "Username"

        '
        ' TextBox4 (Username)
        '
        TextBox4.BackColor = Color.White
        TextBox4.ForeColor = Color.Black
        TextBox4.Font = New Font("Segoe UI", 10.0F)
        TextBox4.Location = New Point(75, 346)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(300, 40)
        TextBox4.TabIndex = 8
        TextBox4.Text = ""

        '
        ' Label7 (Password)
        '
        Label7.AutoSize = True
        Label7.BackColor = Color.Transparent
        Label7.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label7.ForeColor = navyColor
        Label7.Location = New Point(71, 400)
        Label7.Name = "Label7"
        Label7.Size = New Size(85, 23)
        Label7.TabIndex = 11
        Label7.Text = "Password"

        '
        ' TextBox5 (Password)
        '
        TextBox5.BackColor = Color.White
        TextBox5.ForeColor = Color.Black
        TextBox5.Font = New Font("Segoe UI", 10.0F)
        TextBox5.Location = New Point(75, 426)
        TextBox5.Name = "TextBox5"
        TextBox5.Size = New Size(300, 40)
        TextBox5.TabIndex = 10
        TextBox5.PasswordChar = "●"c
        TextBox5.Text = ""

        '
        ' Label5 (Gender)
        '
        Label5.AutoSize = True
        Label5.BackColor = Color.Transparent
        Label5.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label5.ForeColor = navyColor
        Label5.Location = New Point(71, 480)
        Label5.Name = "Label5"
        Label5.Size = New Size(122, 23)
        Label5.TabIndex = 7
        Label5.Text = "Jenis Kelamin"

        '
        ' RadioButton1
        '
        RadioButton1.AutoSize = True
        RadioButton1.BackColor = Color.Transparent
        RadioButton1.Font = New Font("Segoe UI", 10.0F)
        RadioButton1.Location = New Point(75, 506)
        RadioButton1.Name = "RadioButton1"
        RadioButton1.Size = New Size(103, 27)
        RadioButton1.TabIndex = 12
        RadioButton1.TabStop = True
        RadioButton1.Text = "Laki-Laki"
        RadioButton1.UseVisualStyleBackColor = False

        '
        ' RadioButton2
        '
        RadioButton2.AutoSize = True
        RadioButton2.BackColor = Color.Transparent
        RadioButton2.Font = New Font("Segoe UI", 10.0F)
        RadioButton2.Location = New Point(225, 506)
        RadioButton2.Name = "RadioButton2"
        RadioButton2.Size = New Size(125, 27)
        RadioButton2.TabIndex = 13
        RadioButton2.TabStop = True
        RadioButton2.Text = "Perempuan"
        RadioButton2.UseVisualStyleBackColor = False

        ' 
        ' Button1 (Register)
        ' 
        Button1.BackColor = navyColor
        Button1.Cursor = Cursors.Hand
        Button1.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        Button1.ForeColor = Color.White
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0
        Button1.Location = New Point(75, 550)
        Button1.Name = "Button1"
        Button1.Size = New Size(300, 50)
        Button1.TabIndex = 14
        Button1.Text = "Register"

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
        Button2.Location = New Point(75, 610)
        Button2.Name = "Button2"
        Button2.Size = New Size(300, 50)
        Button2.TabIndex = 15
        Button2.Text = "⬅ Kembali"
        
        cardPanel.ResumeLayout(False)
        cardPanel.PerformLayout()
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
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
