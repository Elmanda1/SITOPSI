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
        Dim navyColor As Color = Color.FromArgb(12, 45, 72)
        
        cardPanel = New RoundedPanel()
        Label1 = New Label() ' Title
        Label2 = New Label() ' "USER, "
        Label3 = New Label() ' "Anda memiliki..."
        Label4 = New Label() ' "Berdasarkan..."
        PictureBox1 = New PictureBox() ' Chart
        ButtonBack = New RoundedButton()
        ButtonPrintPreview = New RoundedButton()
        ButtonPrint = New RoundedButton()

        cardPanel.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()

        ' 
        ' HasilTesMinatBakat
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(245, 247, 250)
        ClientSize = New Size(900, 650)
        Controls.Add(cardPanel)
        Controls.Add(ButtonPrintPreview)
        Controls.Add(ButtonPrint)
        Controls.Add(ButtonBack)
        Name = "HasilTesMinatBakat"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Hasil Tes Minat Bakat - SITOPSI"

        ' 
        ' cardPanel
        ' 
        cardPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        cardPanel.BackColor = Color.White
        cardPanel.CornerRadius = 0
        cardPanel.GradientColor1 = Color.White
        cardPanel.GradientColor2 = Color.White
        cardPanel.ShadowOffset = 0
        cardPanel.Location = New Point(30, 30)
        cardPanel.Size = New Size(840, 520)
        cardPanel.Controls.Add(Label1)
        cardPanel.Controls.Add(Label2)
        cardPanel.Controls.Add(Label3)
        cardPanel.Controls.Add(Label4)
        cardPanel.Controls.Add(PictureBox1)

        ' 
        ' Label1 (Title)
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        Label1.ForeColor = navyColor
        Label1.Location = New Point(30, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(328, 41)
        Label1.TabIndex = 28
        Label1.Text = "Hasil Tes Minat Bakat"

        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
        Label2.ForeColor = Color.FromArgb(52, 73, 94)
        Label2.Location = New Point(30, 100)
        Label2.Name = "Label2"
        Label2.Size = New Size(100, 32)
        Label2.TabIndex = 30
        Label2.Text = "USER,"

        ' 
        ' Label3
        ' 
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 12.0F)
        Label3.ForeColor = Color.FromArgb(52, 73, 94)
        Label3.Location = New Point(30, 145)
        Label3.Name = "Label3"
        Label3.Size = New Size(780, 35)
        Label3.TabIndex = 31
        Label3.Text = "Anda memiliki kecondongan lebih di bidang..."

        ' 
        ' Label4
        ' 
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Segoe UI", 11.0F, FontStyle.Italic)
        Label4.ForeColor = Color.FromArgb(127, 140, 141)
        Label4.Location = New Point(30, 190)
        Label4.Name = "Label4"
        Label4.Size = New Size(780, 30)
        Label4.TabIndex = 32
        Label4.Text = "Berdasarkan Jawaban anda..."

        ' 
        ' PictureBox1 (Chart)
        ' 
        PictureBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        PictureBox1.Location = New Point(30, 240)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(780, 260)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 29
        PictureBox1.TabStop = False
        PictureBox1.Visible = False

        ' 
        ' ButtonBack
        ' 
        ButtonBack.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ButtonBack.BackColor = Color.White
        ButtonBack.Cursor = Cursors.Hand
        ButtonBack.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        ButtonBack.ForeColor = navyColor
        ButtonBack.CornerRadius = 12
        ButtonBack.BorderSize = 2.0F
        ButtonBack.BorderColor = navyColor
        ButtonBack.Location = New Point(700, 580)
        ButtonBack.Name = "ButtonBack"
        ButtonBack.Size = New Size(170, 50)
        ButtonBack.TabIndex = 43
        ButtonBack.Text = "⬅ Kembali"

        ' 
        ' ButtonPrintPreview
        ' 
        ButtonPrintPreview.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ButtonPrintPreview.BackColor = Color.FromArgb(41, 128, 185)
        ButtonPrintPreview.Cursor = Cursors.Hand
        ButtonPrintPreview.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        ButtonPrintPreview.ForeColor = Color.White
        ButtonPrintPreview.CornerRadius = 12
        ButtonPrintPreview.BorderSize = 0F
        ButtonPrintPreview.BorderColor = Color.FromArgb(41, 128, 185)
        ButtonPrintPreview.Location = New Point(300, 580)
        ButtonPrintPreview.Name = "ButtonPrintPreview"
        ButtonPrintPreview.Size = New Size(180, 50)
        ButtonPrintPreview.TabIndex = 45
        ButtonPrintPreview.Text = "Print Preview"

        ' 
        ' ButtonPrint
        ' 
        ButtonPrint.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ButtonPrint.BackColor = navyColor
        ButtonPrint.Cursor = Cursors.Hand
        ButtonPrint.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        ButtonPrint.ForeColor = Color.White
        ButtonPrint.CornerRadius = 12
        ButtonPrint.BorderSize = 0F
        ButtonPrint.BorderColor = navyColor
        ButtonPrint.Location = New Point(500, 580)
        ButtonPrint.Name = "ButtonPrint"
        ButtonPrint.Size = New Size(180, 50)
        ButtonPrint.TabIndex = 44
        ButtonPrint.Text = "Export PDF"

        cardPanel.ResumeLayout(False)
        cardPanel.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents cardPanel As RoundedPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ButtonBack As RoundedButton
    Friend WithEvents ButtonPrintPreview As RoundedButton
    Friend WithEvents ButtonPrint As RoundedButton
End Class