<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DashboardUser
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
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 16F)
        Label1.Location = New Point(34, 45)
        Label1.Name = "Label1"
        Label1.Size = New Size(290, 37)
        Label1.TabIndex = 0
        Label1.Text = "Selamat Datang, USER!"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(64, 145)
        Button1.Name = "Button1"
        Button1.Size = New Size(214, 201)
        Button1.TabIndex = 1
        Button1.Text = "Tes Minat Bakat"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(510, 145)
        Button2.Name = "Button2"
        Button2.Size = New Size(214, 201)
        Button2.TabIndex = 2
        Button2.Text = "Generate Topik Skripsi"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(690, 20)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(74, 62)
        PictureBox1.TabIndex = 4
        PictureBox1.TabStop = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 8F)
        Label2.Location = New Point(707, 41)
        Label2.Name = "Label2"
        Label2.Size = New Size(40, 19)
        Label2.TabIndex = 5
        Label2.Text = "Profil"
        ' 
        ' DashboardUser
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Label2)
        Controls.Add(PictureBox1)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label1)
        Name = "DashboardUser"
        Text = "DashboardUser"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label2 As Label
End Class
