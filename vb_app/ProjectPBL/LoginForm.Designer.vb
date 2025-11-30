<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginForm
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
        Label1 = New Label()
        PictureBox1 = New PictureBox()
        Button1 = New Button()
        TextBox1 = New TextBox()
        Label2 = New Label()
        Label3 = New Label()
        TextBox2 = New TextBox()
        Label4 = New Label()
        Label6 = New Label()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 13F)
        Label1.Location = New Point(238, 235)
        Label1.Name = "Label1"
        Label1.Size = New Size(338, 30)
        Label1.TabIndex = 8
        Label1.Text = "Silahkan Isi Kredensial Akun anda!"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(265, 12)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(259, 205)
        PictureBox1.TabIndex = 7
        PictureBox1.TabStop = False
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(421, 395)
        Button1.Name = "Button1"
        Button1.Size = New Size(115, 41)
        Button1.TabIndex = 5
        Button1.Text = "Login "
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(280, 285)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(256, 27)
        TextBox1.TabIndex = 9
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(187, 288)
        Label2.Name = "Label2"
        Label2.Size = New Size(78, 20)
        Label2.TabIndex = 10
        Label2.Text = "Username:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(192, 332)
        Label3.Name = "Label3"
        Label3.Size = New Size(73, 20)
        Label3.TabIndex = 11
        Label3.Text = "Password:"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(280, 332)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(256, 27)
        TextBox2.TabIndex = 12
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 8F)
        Label4.ForeColor = Color.Blue
        Label4.Location = New Point(280, 362)
        Label4.Name = "Label4"
        Label4.Size = New Size(112, 19)
        Label4.TabIndex = 13
        Label4.Text = "Forgot Password"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 13F)
        Label6.Location = New Point(365, 95)
        Label6.Name = "Label6"
        Label6.Size = New Size(71, 30)
        Label6.TabIndex = 21
        Label6.Text = "LOGO"
        ' 
        ' LoginForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 496)
        Controls.Add(Label6)
        Controls.Add(Label4)
        Controls.Add(TextBox2)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(TextBox1)
        Controls.Add(Label1)
        Controls.Add(PictureBox1)
        Controls.Add(Button1)
        Name = "LoginForm"
        Text = "LoginForm"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
End Class
