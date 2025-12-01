<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LandingPage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        Button1 = New Button()
        PictureBox1 = New PictureBox()
        Label1 = New Label()
        Button3 = New Button()
        Label2 = New Label()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(142, 347)
        Button1.Name = "Button1"
        Button1.Size = New Size(111, 52)
        Button1.TabIndex = 0
        Button1.Text = "Login "
        Button1.UseVisualStyleBackColor = True
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = ProjectPBL.Resources.logo
        PictureBox1.Location = New Point(212, 25)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(259, 205)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 3
        PictureBox1.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 13F)
        Label1.Location = New Point(200, 242)
        Label1.Name = "Label1"
        Label1.Size = New Size(283, 30)
        Label1.TabIndex = 4
        Label1.Text = "Selamat Datang di SITOPSI !"
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(429, 347)
        Button3.Name = "Button3"
        Button3.Size = New Size(111, 52)
        Button3.TabIndex = 5
        Button3.Text = "Register"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 13F)
        Label2.Location = New Point(170, 284)
        Label2.Name = "Label2"
        Label2.Size = New Size(348, 30)
        Label2.TabIndex = 23
        Label2.Text = "Sistem Topik Skripsi Berbasis Pakar"
        ' 
        ' LandingPage
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(724, 450)
        Controls.Add(Label2)
        Controls.Add(Button3)
        Controls.Add(Label1)
        Controls.Add(PictureBox1)
        Controls.Add(Button1)
        Name = "LandingPage"
        Text = "SITOPSI - Landing Page"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Label2 As Label

End Class
