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
        Dim navyColor As Color = Color.FromArgb(12, 45, 72)
        
        Label1 = New Label() ' Title
        lblSelectQuestions = New Label() ' Label untuk pilihan jumlah soal
        cmbQuestionCount = New ComboBox() ' Dropdown pilihan jumlah soal
        btnStartTest = New Button() ' Button mulai tes
        ProgressBar1 = New ProgressBar()
        LabelProgress = New Label()
        Button2 = New Button() ' Previous
        Button3 = New Button() ' Next
        QuestionsPanel = New Panel() ' Container for dynamic questions
        
        SuspendLayout()
        ' 
        ' TesMinatBakat
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(245, 247, 250) ' Light Grey background
        ClientSize = New Size(900, 750)
        Name = "TesMinatBakat"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Tes Minat Bakat - SITOPSI"
        Controls.Add(Label1)
        Controls.Add(lblSelectQuestions)
        Controls.Add(cmbQuestionCount)
        Controls.Add(btnStartTest)
        Controls.Add(QuestionsPanel)
        Controls.Add(LabelProgress)
        Controls.Add(ProgressBar1)
        Controls.Add(Button2)
        Controls.Add(Button3)
        
        ' 
        ' Label1 (Title)
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        Label1.ForeColor = navyColor
        Label1.Location = New Point(30, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(260, 41)
        Label1.TabIndex = 27
        Label1.Text = "Tes Minat Bakat"
        
        ' 
        ' lblSelectQuestions
        ' 
        lblSelectQuestions.AutoSize = True
        lblSelectQuestions.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        lblSelectQuestions.ForeColor = navyColor
        lblSelectQuestions.Location = New Point(30, 80)
        lblSelectQuestions.Name = "lblSelectQuestions"
        lblSelectQuestions.Size = New Size(250, 25)
        lblSelectQuestions.TabIndex = 50
        lblSelectQuestions.Text = "Pilih Jumlah Pertanyaan:"
        
        ' 
        ' cmbQuestionCount
        ' 
        cmbQuestionCount.DropDownStyle = ComboBoxStyle.DropDownList
        cmbQuestionCount.Font = New Font("Segoe UI", 11.0F)
        cmbQuestionCount.Location = New Point(30, 110)
        cmbQuestionCount.Name = "cmbQuestionCount"
        cmbQuestionCount.Size = New Size(200, 33)
        cmbQuestionCount.TabIndex = 51
        cmbQuestionCount.Items.AddRange(New Object() {"10 Pertanyaan", "15 Pertanyaan", "20 Pertanyaan", "25 Pertanyaan"})
        cmbQuestionCount.SelectedIndex = 1 ' Default 15 pertanyaan
        
        ' 
        ' btnStartTest
        ' 
        btnStartTest.BackColor = navyColor
        btnStartTest.Cursor = Cursors.Hand
        btnStartTest.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        btnStartTest.ForeColor = Color.White
        btnStartTest.FlatStyle = FlatStyle.Flat
        btnStartTest.FlatAppearance.BorderSize = 0
        btnStartTest.Location = New Point(250, 105)
        btnStartTest.Name = "btnStartTest"
        btnStartTest.Size = New Size(150, 40)
        btnStartTest.TabIndex = 52
        btnStartTest.Text = "Mulai Tes"
        
        '
        ' QuestionsPanel
        '
        QuestionsPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        QuestionsPanel.AutoScroll = True
        QuestionsPanel.Location = New Point(30, 160)
        QuestionsPanel.Name = "QuestionsPanel"
        QuestionsPanel.Size = New Size(840, 420)
        QuestionsPanel.Visible = False ' Hidden sampai user pilih jumlah soal
        
        ' 
        ' LabelProgress
        ' 
        LabelProgress.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        LabelProgress.AutoSize = True
        LabelProgress.Font = New Font("Segoe UI", 10.0F)
        LabelProgress.ForeColor = Color.FromArgb(44, 62, 80)
        LabelProgress.Location = New Point(30, 605)
        LabelProgress.Name = "LabelProgress"
        LabelProgress.Size = New Size(180, 23)
        LabelProgress.TabIndex = 45
        LabelProgress.Text = "Progress: 0 dari 0"
        LabelProgress.Visible = False
        
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ProgressBar1.Location = New Point(30, 635)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(840, 23)
        ProgressBar1.TabIndex = 44
        ProgressBar1.Visible = False
        
        ' 
        ' Button2 (Previous)
        ' 
        Button2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button2.BackColor = Color.White
        Button2.Cursor = Cursors.Hand
        Button2.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        Button2.ForeColor = navyColor
        Button2.FlatStyle = FlatStyle.Flat
        Button2.FlatAppearance.BorderSize = 1
        Button2.FlatAppearance.BorderColor = navyColor
        Button2.Location = New Point(480, 670)
        Button2.Name = "Button2"
        Button2.Size = New Size(180, 45)
        Button2.TabIndex = 43
        Button2.Text = "« Sebelumnya"
        Button2.Visible = False
        
        ' 
        ' Button3 (Next)
        ' 
        Button3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button3.BackColor = navyColor
        Button3.Cursor = Cursors.Hand
        Button3.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        Button3.ForeColor = Color.White
        Button3.FlatStyle = FlatStyle.Flat
        Button3.FlatAppearance.BorderSize = 0
        Button3.Location = New Point(690, 670)
        Button3.Name = "Button3"
        Button3.Size = New Size(180, 45)
        Button3.TabIndex = 42
        Button3.Text = "Selanjutnya »"
        Button3.Visible = False
        
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents lblSelectQuestions As Label
    Friend WithEvents cmbQuestionCount As ComboBox
    Friend WithEvents btnStartTest As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents LabelProgress As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents QuestionsPanel As Panel
End Class