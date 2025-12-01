<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangePassword
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
        Label2 = New Label()
        TextBox1 = New TextBox()
        Label1 = New Label()
        TextBox2 = New TextBox()
        Label3 = New Label()
        TextBox3 = New TextBox()
        Label4 = New Label()
        Button1 = New Button()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = ProjectPBL.Resources.logo
        PictureBox1.Location = New Point(256, 34)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(259, 205)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 9
        PictureBox1.TabStop = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(137, 344)
        Label2.Name = "Label2"
        Label2.Size = New Size(107, 20)
        Label2.TabIndex = 22
        Label2.Text = "Password Baru:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(259, 341)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(256, 27)
        TextBox1.TabIndex = 23
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 13F)
        Label1.Location = New Point(256, 253)
        Label1.Name = "Label1"
        Label1.Size = New Size(273, 30)
        Label1.TabIndex = 24
        Label1.Text = "Ganti Password Akun anda!"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(259, 298)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(256, 27)
        TextBox2.TabIndex = 26
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(166, 301)
        Label3.Name = "Label3"
        Label3.Size = New Size(78, 20)
        Label3.TabIndex = 25
        Label3.Text = "Username:"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(259, 386)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(256, 27)
        TextBox3.TabIndex = 28
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(74, 393)
        Label4.Name = "Label4"
        Label4.Size = New Size(170, 20)
        Label4.TabIndex = 27
        Label4.Text = "Verifikasi Password Baru:"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(400, 435)
        Button1.Name = "Button1"
        Button1.Size = New Size(115, 62)
        Button1.TabIndex = 29
        Button1.Text = "Ganti Password"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' ChangePassword
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 509)
        Controls.Add(Button1)
        Controls.Add(TextBox3)
        Controls.Add(Label4)
        Controls.Add(TextBox2)
        Controls.Add(Label3)
        Controls.Add(Label1)
        Controls.Add(TextBox1)
        Controls.Add(Label2)
        Controls.Add(PictureBox1)
        Name = "ChangePassword"
        Text = "Change Password - SITOPSI"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Button1 As Button
End Class
