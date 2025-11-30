<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HasilTesMinatBakat
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
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 16F)
        Label1.Location = New Point(218, 44)
        Label1.Name = "Label1"
        Label1.Size = New Size(350, 37)
        Label1.TabIndex = 28
        Label1.Text = "Hasil Tes Minat Bakat, USER!"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(60, 99)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(691, 329)
        PictureBox1.TabIndex = 29
        PictureBox1.TabStop = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 13F)
        Label2.Location = New Point(72, 112)
        Label2.Name = "Label2"
        Label2.Size = New Size(75, 30)
        Label2.TabIndex = 30
        Label2.Text = "USER, "
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F)
        Label3.Location = New Point(86, 166)
        Label3.Name = "Label3"
        Label3.Size = New Size(407, 28)
        Label3.TabIndex = 31
        Label3.Text = "Anda memiliki kecondongan lebih di bidang.."
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 12F)
        Label4.Location = New Point(86, 212)
        Label4.Name = "Label4"
        Label4.Size = New Size(257, 28)
        Label4.TabIndex = 32
        Label4.Text = "Berdasarkan Jawaban anda..."
        ' 
        ' HasilTesMinatBakat
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(PictureBox1)
        Controls.Add(Label1)
        Name = "HasilTesMinatBakat"
        Text = "HasilTesMinatBakat"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
End Class
