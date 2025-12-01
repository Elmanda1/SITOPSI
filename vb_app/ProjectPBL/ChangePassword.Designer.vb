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
        Panel1 = New Panel()
        PictureBox2 = New PictureBox()
        PictureBox1 = New PictureBox()
        Panel2 = New Panel()
        Button2 = New Button()
        Button1 = New Button()
        TextBox3 = New TextBox()
        Label4 = New Label()
        TextBox2 = New TextBox()
        Label3 = New Label()
        Label1 = New Label()
        TextBox1 = New TextBox()
        Label2 = New Label()
        Panel1.SuspendLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(41), CByte(128), CByte(185))
        Panel1.Controls.Add(PictureBox2)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(900, 180)
        Panel1.TabIndex = 0
        ' 
        ' PictureBox2
        ' 
        PictureBox2.BackColor = Color.Transparent
        PictureBox2.Location = New Point(695, 12)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(180, 155)
        PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox2.TabIndex = 1
        PictureBox2.TabStop = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.Location = New Point(28, 12)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(180, 155)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.WhiteSmoke
        Panel2.Controls.Add(Button2)
        Panel2.Controls.Add(Button1)
        Panel2.Controls.Add(TextBox3)
        Panel2.Controls.Add(Label4)
        Panel2.Controls.Add(TextBox2)
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(Label1)
        Panel2.Controls.Add(TextBox1)
        Panel2.Controls.Add(Label2)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(0, 180)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(900, 470)
        Panel2.TabIndex = 1
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.FromArgb(CByte(149), CByte(165), CByte(166))
        Button2.Cursor = Cursors.Hand
        Button2.FlatAppearance.BorderSize = 0
        Button2.FlatStyle = FlatStyle.Flat
        Button2.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        Button2.ForeColor = Color.White
        Button2.Location = New Point(280, 390)
        Button2.Name = "Button2"
        Button2.Size = New Size(340, 50)
        Button2.TabIndex = 8
        Button2.Text = "? Kembali ke Login"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        Button1.Cursor = Cursors.Hand
        Button1.FlatAppearance.BorderSize = 0
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        Button1.ForeColor = Color.White
        Button1.Location = New Point(280, 330)
        Button1.Name = "Button1"
        Button1.Size = New Size(340, 50)
        Button1.TabIndex = 7
        Button1.Text = "?? Ganti Password"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' TextBox3
        ' 
        TextBox3.Font = New Font("Segoe UI", 12.0F)
        TextBox3.Location = New Point(280, 280)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(340, 34)
        TextBox3.TabIndex = 6
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        Label4.ForeColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        Label4.Location = New Point(280, 250)
        Label4.Name = "Label4"
        Label4.Size = New Size(211, 25)
        Label4.TabIndex = 5
        Label4.Text = "Konfirmasi Password Baru"
        ' 
        ' TextBox2
        ' 
        TextBox2.Font = New Font("Segoe UI", 12.0F)
        TextBox2.Location = New Point(280, 200)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(340, 34)
        TextBox2.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        Label3.ForeColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        Label3.Location = New Point(280, 170)
        Label3.Name = "Label3"
        Label3.Size = New Size(133, 25)
        Label3.TabIndex = 3
        Label3.Text = "Password Baru"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        Label1.ForeColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        Label1.Location = New Point(310, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(280, 41)
        Label1.TabIndex = 2
        Label1.Text = "Reset Password ??"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TextBox1
        ' 
        TextBox1.Font = New Font("Segoe UI", 12.0F)
        TextBox1.Location = New Point(280, 120)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(340, 34)
        TextBox1.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        Label2.ForeColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        Label2.Location = New Point(280, 90)
        Label2.Name = "Label2"
        Label2.Size = New Size(99, 25)
        Label2.TabIndex = 0
        Label2.Text = "Username"
        ' 
        ' ChangePassword
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(900, 650)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "ChangePassword"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Change Password - SITOPSI"
        Panel1.ResumeLayout(False)
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
