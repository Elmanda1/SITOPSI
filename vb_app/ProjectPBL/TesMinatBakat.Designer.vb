<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TesMinatBakat
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
        Button3 = New Button()
        Button2 = New Button()
        ProgressBar1 = New ProgressBar()
        LabelProgress = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        Label1.ForeColor = Color.FromArgb(44, 62, 80)
        Label1.Location = New Point(30, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(300, 37)
        Label1.TabIndex = 27
        Label1.Text = "Tes Minat Bakat, USER!"
        ' 
        ' Button3
        ' 
        Button3.BackColor = Color.FromArgb(41, 128, 185)
        Button3.FlatAppearance.BorderSize = 0
        Button3.FlatStyle = FlatStyle.Flat
        Button3.Font = New Font("Segoe UI", 11F, FontStyle.Regular)
        Button3.ForeColor = Color.White
        Button3.Location = New Point(590, 670)
        Button3.Name = "Button3"
        Button3.Size = New Size(180, 45)
        Button3.TabIndex = 42
        Button3.Text = "Halaman 2 »"
        Button3.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.FromArgb(149, 165, 166)
        Button2.FlatAppearance.BorderSize = 0
        Button2.FlatStyle = FlatStyle.Flat
        Button2.Font = New Font("Segoe UI", 11F, FontStyle.Regular)
        Button2.ForeColor = Color.White
        Button2.Location = New Point(400, 670)
        Button2.Name = "Button2"
        Button2.Size = New Size(180, 45)
        Button2.TabIndex = 43
        Button2.Text = "« Halaman Sebelumnya"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New Point(30, 635)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(740, 23)
        ProgressBar1.TabIndex = 44
        ' 
        ' LabelProgress
        ' 
        LabelProgress.AutoSize = True
        LabelProgress.Font = New Font("Segoe UI", 10F, FontStyle.Regular)
        LabelProgress.ForeColor = Color.FromArgb(127, 140, 141)
        LabelProgress.Location = New Point(30, 605)
        LabelProgress.Name = "LabelProgress"
        LabelProgress.Size = New Size(180, 23)
        LabelProgress.TabIndex = 45
        LabelProgress.Text = "Progress: 10 dari 100"
        ' 
        ' TesMinatBakat
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        AutoScroll = True
        BackColor = Color.FromArgb(236, 240, 241)
        ClientSize = New Size(800, 750)
        Controls.Add(LabelProgress)
        Controls.Add(ProgressBar1)
        Controls.Add(Button2)
        Controls.Add(Button3)
        Controls.Add(Label1)
        Name = "TesMinatBakat"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Tes Minat Bakat - SITOPSI"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents LabelProgress As Label
End Class
