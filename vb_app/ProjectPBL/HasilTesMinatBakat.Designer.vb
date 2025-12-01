<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HasilTesMinatBakat
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
        Label1.Font = New Font("Segoe UI", 18F, FontStyle.Bold)
        Label1.ForeColor = Color.FromArgb(44, 62, 80)
        Label1.Location = New Point(30, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(450, 41)
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
        PictureBox1.Visible = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        Label2.ForeColor = Color.FromArgb(52, 73, 94)
        Label2.Location = New Point(40, 100)
        Label2.Name = "Label2"
        Label2.Size = New Size(100, 32)
        Label2.TabIndex = 30
        Label2.Text = "USER, "
        ' 
        ' Label3
        ' 
        Label3.AutoSize = False
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Regular)
        Label3.ForeColor = Color.FromArgb(52, 73, 94)
        Label3.Location = New Point(40, 145)
        Label3.Name = "Label3"
        Label3.Size = New Size(700, 35)
        Label3.TabIndex = 31
        Label3.Text = "Anda memiliki kecondongan lebih di bidang.."
        ' 
        ' Label4
        ' 
        Label4.AutoSize = False
        Label4.Font = New Font("Segoe UI", 11F, FontStyle.Italic)
        Label4.ForeColor = Color.FromArgb(127, 140, 141)
        Label4.Location = New Point(40, 190)
        Label4.Name = "Label4"
        Label4.Size = New Size(700, 30)
        Label4.TabIndex = 32
        Label4.Text = "Berdasarkan Jawaban anda..."
        ' 
        ' HasilTesMinatBakat
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(236, 240, 241)
        ClientSize = New Size(900, 650)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(PictureBox1)
        Name = "HasilTesMinatBakat"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Hasil Tes Minat Bakat - SITOPSI"
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
