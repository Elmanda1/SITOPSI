<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RegisterForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        PictureBox1 = New PictureBox()
        Label1 = New Label()
        TextBox1 = New TextBox()
        Label2 = New Label()
        Label3 = New Label()
        TextBox2 = New TextBox()
        Label4 = New Label()
        TextBox3 = New TextBox()
        Label5 = New Label()
        RadioButton1 = New RadioButton()
        RadioButton2 = New RadioButton()
        Button1 = New Button()
        Label6 = New Label()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(263, 21)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(259, 205)
        PictureBox1.TabIndex = 8
        PictureBox1.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 13F)
        Label1.Location = New Point(239, 240)
        Label1.Name = "Label1"
        Label1.Size = New Size(326, 30)
        Label1.TabIndex = 9
        Label1.Text = "Daftarkan Kredensial Akun anda!"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(263, 295)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(256, 27)
        TextBox1.TabIndex = 10
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(132, 298)
        Label2.Name = "Label2"
        Label2.Size = New Size(112, 20)
        Label2.TabIndex = 11
        Label2.Text = "Nama Lengkap:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(152, 342)
        Label3.Name = "Label3"
        Label3.Size = New Size(92, 20)
        Label3.TabIndex = 13
        Label3.Text = "No. Telepon:"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(263, 339)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(256, 27)
        TextBox2.TabIndex = 12
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(195, 460)
        Label4.Name = "Label4"
        Label4.Size = New Size(49, 20)
        Label4.TabIndex = 15
        Label4.Text = "Email:"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(263, 457)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(256, 27)
        TextBox3.TabIndex = 14
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(143, 388)
        Label5.Name = "Label5"
        Label5.Size = New Size(101, 20)
        Label5.TabIndex = 16
        Label5.Text = "Jenis Kelamin:"
        ' 
        ' RadioButton1
        ' 
        RadioButton1.AutoSize = True
        RadioButton1.Location = New Point(263, 386)
        RadioButton1.Name = "RadioButton1"
        RadioButton1.Size = New Size(88, 24)
        RadioButton1.TabIndex = 17
        RadioButton1.TabStop = True
        RadioButton1.Text = "Laki-Laki"
        RadioButton1.UseVisualStyleBackColor = True
        ' 
        ' RadioButton2
        ' 
        RadioButton2.AutoSize = True
        RadioButton2.Location = New Point(263, 416)
        RadioButton2.Name = "RadioButton2"
        RadioButton2.Size = New Size(104, 24)
        RadioButton2.TabIndex = 18
        RadioButton2.TabStop = True
        RadioButton2.Text = "Perempuan"
        RadioButton2.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(404, 511)
        Button1.Name = "Button1"
        Button1.Size = New Size(115, 41)
        Button1.TabIndex = 19
        Button1.Text = "Register"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 13F)
        Label6.Location = New Point(356, 108)
        Label6.Name = "Label6"
        Label6.Size = New Size(71, 30)
        Label6.TabIndex = 20
        Label6.Text = "LOGO"
        ' 
        ' RegisterForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 585)
        Controls.Add(Label6)
        Controls.Add(Button1)
        Controls.Add(RadioButton2)
        Controls.Add(RadioButton1)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(TextBox3)
        Controls.Add(Label3)
        Controls.Add(TextBox2)
        Controls.Add(Label2)
        Controls.Add(TextBox1)
        Controls.Add(Label1)
        Controls.Add(PictureBox1)
        Name = "RegisterForm"
        Text = "RegisterForm"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents Button1 As Button
    Friend WithEvents Label6 As Label
End Class
