<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GenerateTopikSkripsi
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
        Label2 = New Label() ' "Berdasarkan Minat..."
        Label3 = New Label() ' "Berikut rekomendasinya..."
        TopicsListBox = New ListBox() ' To display topics
        ButtonGenerateUlang = New RoundedButton() ' Generate Ulang button
        ButtonPrintPreview = New RoundedButton() ' Print Preview button
        ButtonPrint = New RoundedButton() ' Print PDF button
        ButtonBack = New RoundedButton()

        cardPanel.SuspendLayout()
        SuspendLayout()

        ' 
        ' GenerateTopikSkripsi
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(245, 247, 250)
        ClientSize = New Size(900, 650)
        Controls.Add(cardPanel)
        Controls.Add(ButtonGenerateUlang)
        Controls.Add(ButtonPrintPreview)
        Controls.Add(ButtonPrint)
        Controls.Add(ButtonBack)
        Name = "GenerateTopikSkripsi"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Rekomendasi Topik Skripsi - SITOPSI"

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
        cardPanel.Controls.Add(TopicsListBox)

        ' 
        ' Label1 (Title)
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        Label1.ForeColor = navyColor
        Label1.Location = New Point(30, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(403, 41)
        Label1.TabIndex = 28
        Label1.Text = "Rekomendasi Topik Skripsi"

        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 12.0F, FontStyle.Italic)
        Label2.ForeColor = Color.FromArgb(52, 73, 94)
        Label2.Location = New Point(32, 80)
        Label2.Name = "Label2"
        Label2.Size = New Size(343, 28)
        Label2.TabIndex = 29
        Label2.Text = "Berdasarkan minat bakat Anda di bidang..."

        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label3.ForeColor = navyColor
        Label3.Location = New Point(33, 130)
        Label3.Name = "Label3"
        Label3.Size = New Size(258, 23)
        Label3.TabIndex = 30
        Label3.Text = "Berikut Rekomendasi dari Kami:"

        '
        ' TopicsListBox
        '
        TopicsListBox.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TopicsListBox.Font = New Font("Segoe UI", 11.0F)
        TopicsListBox.FormattingEnabled = True
        TopicsListBox.ItemHeight = 25
        TopicsListBox.Location = New Point(35, 170)
        TopicsListBox.Name = "TopicsListBox"
        TopicsListBox.Size = New Size(770, 304)
        TopicsListBox.TabIndex = 36

        ' 
        ' ButtonGenerateUlang
        ' 
        ButtonGenerateUlang.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ButtonGenerateUlang.BackColor = navyColor
        ButtonGenerateUlang.Cursor = Cursors.Hand
        ButtonGenerateUlang.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        ButtonGenerateUlang.ForeColor = Color.White
        ButtonGenerateUlang.CornerRadius = 12
        ButtonGenerateUlang.BorderSize = 0.0F
        ButtonGenerateUlang.Location = New Point(70, 580)
        ButtonGenerateUlang.Name = "ButtonGenerateUlang"
        ButtonGenerateUlang.Size = New Size(180, 50)
        ButtonGenerateUlang.TabIndex = 45
        ButtonGenerateUlang.Text = "Generate Ulang"

        ' 
        ' ButtonPrintPreview
        ' 
        ButtonPrintPreview.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ButtonPrintPreview.BackColor = Color.FromArgb(41, 128, 185)
        ButtonPrintPreview.Cursor = Cursors.Hand
        ButtonPrintPreview.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        ButtonPrintPreview.ForeColor = Color.White
        ButtonPrintPreview.CornerRadius = 12
        ButtonPrintPreview.BorderSize = 0.0F
        ButtonPrintPreview.Location = New Point(270, 580)
        ButtonPrintPreview.Name = "ButtonPrintPreview"
        ButtonPrintPreview.Size = New Size(180, 50)
        ButtonPrintPreview.TabIndex = 47
        ButtonPrintPreview.Text = "Print Preview"

        ' 
        ' ButtonPrint
        ' 
        ButtonPrint.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ButtonPrint.BackColor = Color.FromArgb(39, 174, 96)
        ButtonPrint.Cursor = Cursors.Hand
        ButtonPrint.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        ButtonPrint.ForeColor = Color.White
        ButtonPrint.CornerRadius = 12
        ButtonPrint.BorderSize = 0.0F
        ButtonPrint.Location = New Point(470, 580)
        ButtonPrint.Name = "ButtonPrint"
        ButtonPrint.Size = New Size(180, 50)
        ButtonPrint.TabIndex = 46
        ButtonPrint.Text = "Export PDF"

        ' 
        ' ButtonBack
        ' 
        ButtonBack.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ButtonBack.BackColor = Color.White
        ButtonBack.Cursor = Cursors.Hand
        ButtonBack.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        ButtonBack.ForeColor = navyColor
        ButtonBack.CornerRadius = 12
        ButtonBack.BorderSize = 2.0F
        ButtonBack.BorderColor = navyColor
        ButtonBack.Location = New Point(700, 580)
        ButtonBack.Name = "ButtonBack"
        ButtonBack.Size = New Size(170, 50)
        ButtonBack.TabIndex = 44
        ButtonBack.Text = "⬅ Kembali"

        cardPanel.ResumeLayout(False)
        cardPanel.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents cardPanel As RoundedPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TopicsListBox As ListBox
    Friend WithEvents ButtonGenerateUlang As RoundedButton
    Friend WithEvents ButtonPrintPreview As RoundedButton
    Friend WithEvents ButtonPrint As RoundedButton
    Friend WithEvents ButtonBack As RoundedButton
End Class