<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DashboardAdmin
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
        Button1 = New Button()
        Button2 = New Button()
        PictureBox1 = New PictureBox()
        Label2 = New Label()
        Button3 = New Button()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 16F)
        Label1.Location = New Point(29, 39)
        Label1.Name = "Label1"
        Label1.Size = New Size(314, 37)
        Label1.TabIndex = 1
        Label1.Text = "Selamat Datang, ADMIN!"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(29, 139)
        Button1.Name = "Button1"
        Button1.Size = New Size(214, 201)
        Button1.TabIndex = 2
        Button1.Text = "Lihat User Aktif"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(294, 139)
        Button2.Name = "Button2"
        Button2.Size = New Size(214, 201)
        Button2.TabIndex = 3
        Button2.Text = "Ganti Password"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(701, 24)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(74, 62)
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 8F)
        Label2.Location = New Point(718, 46)
        Label2.Name = "Label2"
        Label2.Size = New Size(40, 19)
        Label2.TabIndex = 6
        Label2.Text = "Profil"
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(561, 139)
        Button3.Name = "Button3"
        Button3.Size = New Size(214, 201)
        Button3.TabIndex = 7
        Button3.Text = "Tambah Pertanyaan"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' DashboardAdmin
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Button3)
        Controls.Add(Label2)
        Controls.Add(PictureBox1)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label1)
        Name = "DashboardAdmin"
        Text = "DashboardAdmin"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Button3 As Button
End Class
