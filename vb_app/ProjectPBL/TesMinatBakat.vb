Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.IO
Imports System.Diagnostics

Public Class TesMinatBakat
    Public Property ParentDashboard As DashboardUser

    Private questions As New List(Of QuestionData)
    Private userAnswers As New Dictionary(Of Integer, AnswerData)
    Private confidenceLevels As New List(Of ConfidenceLevel)
    Private currentQuestionIndex As Integer = 0
    Private totalQuestions As Integer = 15 ' Default 15
    Private testStarted As Boolean = False

    Private ReadOnly navyColor As Color = Color.FromArgb(12, 45, 72)
    Private ReadOnly lightNavyColor As Color = Color.FromArgb(28, 68, 105)
    Private ReadOnly whiteColor As Color = Color.White
    Private ReadOnly lightGreyHoverColor As Color = Color.FromArgb(236, 240, 241)

    Public Class QuestionData
        Public Property QuestionId As Integer
        Public Property Nomor As Integer
        Public Property IsiPertanyaan As String
        Public Property TargetCategoryId As Integer
        Public Property CfPakar As Decimal
        Public Property IsPriority As Boolean
    End Class
    
    Public Class ConfidenceLevel
        Public Property ConfidenceId As Integer
        Public Property NamaLevel As String
        Public Property NilaiCf As Decimal
        Public Property Urutan As Integer
    End Class
    
    Public Class AnswerData
        Public Property QuestionId As Integer
        Public Property ConfidenceId As Integer
        Public Property CfItemScore As Decimal
    End Class

    Private Function CallPythonInferenceRaw(jsonInput As String) As String
        Try
            Dim pythonExe As String = "python"
            Dim scriptPath As String = "c:\Users\lunox\Documents\SITOPSI\python_experts\main.py"
            
            ' Check if Python script exists
            If Not System.IO.File.Exists(scriptPath) Then
                Throw New Exception("Python script tidak ditemukan di: " & scriptPath)
            End If
            
            Dim psi As New ProcessStartInfo() With {
                .FileName = pythonExe,
                .Arguments = """" & scriptPath & """",
                .UseShellExecute = False,
                .RedirectStandardInput = True,
                .RedirectStandardOutput = True,
                .RedirectStandardError = True,
                .CreateNoWindow = True,
                .WorkingDirectory = System.IO.Path.GetDirectoryName(scriptPath)
            }
            
            Using process As Process = Process.Start(psi)
                If process Is Nothing Then
                    Throw New Exception("Gagal memulai Python process. Pastikan Python terinstall dan ada di PATH.")
                End If
                
                ' Send JSON to stdin
                Using writer As StreamWriter = process.StandardInput
                    writer.WriteLine(jsonInput)
                    writer.Flush()
                End Using
                
                ' Read output
                Dim output As String = process.StandardOutput.ReadToEnd()
                Dim errorOutput As String = process.StandardError.ReadToEnd()
                
                process.WaitForExit(30000) ' 30 second timeout
                
                If Not process.HasExited Then
                    process.Kill()
                    Throw New Exception("Python process timeout (lebih dari 30 detik)")
                End If
                
                If process.ExitCode <> 0 Then
                    Throw New Exception("Python exit code " & process.ExitCode & vbCrLf & "Error: " & errorOutput & vbCrLf & "Output: " & output)
                End If
                
                If String.IsNullOrWhiteSpace(output) Then
                    Throw New Exception("Python tidak mengembalikan output." & vbCrLf & "Error: " & errorOutput)
                End If
                
                Return output.Trim()
            End Using
        Catch ex As Exception
            Throw New Exception("Gagal memanggil Python Expert System: " & ex.Message & vbCrLf & vbCrLf & "Detail: " & ex.StackTrace)
        End Try
    End Function

    Private Sub TesMinatBakat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Then
            MessageBox.Show("Silakan login terlebih dahulu!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If

        ' Event handlers untuk navigasi
        AddHandler Button2.Click, AddressOf Button2_Click
        AddHandler Button3.Click, AddressOf Button3_Click
        AddHandler Button2.MouseEnter, AddressOf Button_MouseEnter
        AddHandler Button2.MouseLeave, AddressOf Button_MouseLeave
        AddHandler Button3.MouseEnter, AddressOf Button_MouseEnter
        AddHandler Button3.MouseLeave, AddressOf Button_MouseLeave
        
        ' Event handler untuk button Start Test
        AddHandler btnStartTest.Click, AddressOf BtnStartTest_Click
        AddHandler btnStartTest.MouseEnter, AddressOf BtnStart_MouseEnter
        AddHandler btnStartTest.MouseLeave, AddressOf BtnStart_MouseLeave

        ' Load confidence levels dulu
        LoadConfidenceLevels()
    End Sub
    
    Private Sub BtnStartTest_Click(sender As Object, e As EventArgs)
        ' Ambil jumlah pertanyaan yang dipilih
        Select Case cmbQuestionCount.SelectedIndex
            Case 0
                totalQuestions = 10
            Case 1
                totalQuestions = 15
            Case 2
                totalQuestions = 20
            Case 3
                totalQuestions = 25
            Case Else
                totalQuestions = 15 ' Default
        End Select
        
        ' Hide selection panel
        lblSelectQuestions.Visible = False
        cmbQuestionCount.Visible = False
        btnStartTest.Visible = False
        
        ' Show test components
        QuestionsPanel.Visible = True
        LabelProgress.Visible = True
        ProgressBar1.Visible = True
        Button2.Visible = True
        Button3.Visible = True
        
        ' Load questions and start
        testStarted = True
        LoadRandomQuestions()

        If questions.Count > 0 Then
            DisplayQuestion()
        Else
            MessageBox.Show("Tidak ada pertanyaan yang tersedia!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If
    End Sub
    
    Private Sub BtnStart_MouseEnter(sender As Object, e As EventArgs)
        btnStartTest.BackColor = lightNavyColor
    End Sub
    
    Private Sub BtnStart_MouseLeave(sender As Object, e As EventArgs)
        btnStartTest.BackColor = navyColor
    End Sub

    Private Sub LoadConfidenceLevels()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT confidence_id, nama_level, nilai_cf, urutan FROM confidence_levels ORDER BY urutan"
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            confidenceLevels.Add(New ConfidenceLevel() With {
                                .ConfidenceId = Convert.ToInt32(reader("confidence_id")),
                                .NamaLevel = reader("nama_level").ToString(),
                                .NilaiCf = Convert.ToDecimal(reader("nilai_cf")),
                                .Urutan = Convert.ToInt32(reader("urutan"))
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memuat level keyakinan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadRandomQuestions()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT question_id, nomor, isi_pertanyaan, target_category_id, cf_pakar, is_priority FROM questions WHERE is_active = 1 ORDER BY RAND() LIMIT @maxQuestions"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@maxQuestions", totalQuestions)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            questions.Add(New QuestionData() With {
                                .QuestionId = Convert.ToInt32(reader("question_id")),
                                .Nomor = Convert.ToInt32(reader("nomor")),
                                .IsiPertanyaan = reader("isi_pertanyaan").ToString(),
                                .TargetCategoryId = Convert.ToInt32(reader("target_category_id")),
                                .CfPakar = Convert.ToDecimal(reader("cf_pakar")),
                                .IsPriority = Convert.ToBoolean(reader("is_priority"))
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memuat pertanyaan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub DisplayQuestion()
        QuestionsPanel.Controls.Clear()
        QuestionsPanel.AutoScrollPosition = New Point(0, 0)

        If currentQuestionIndex >= questions.Count Then
            ProcessResults()
            Return
        End If

        Dim question As QuestionData = questions(currentQuestionIndex)
        Label1.Text = "Tes Minat Bakat - Pertanyaan " & (currentQuestionIndex + 1) & " dari " & questions.Count

        Dim lblStatement As New Label() With {
            .Text = question.IsiPertanyaan,
            .Location = New Point(10, 10),
            .AutoSize = False,
            .Width = QuestionsPanel.ClientSize.Width - 40,
            .Height = 80,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80)
        }
        QuestionsPanel.Controls.Add(lblStatement)

        Dim lblInstruction As New Label() With {
            .Text = "Seberapa yakin Anda dengan pernyataan di atas?",
            .Location = New Point(10, 100),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 9, FontStyle.Italic),
            .ForeColor = Color.FromArgb(127, 140, 141)
        }
        QuestionsPanel.Controls.Add(lblInstruction)

        Dim yPos As Integer = 140
        For Each confidenceLevel In confidenceLevels.OrderBy(Function(c) c.Urutan)
            Dim rb As New RadioButton() With {
                .Text = confidenceLevel.NamaLevel,
                .Location = New Point(20, yPos),
                .AutoSize = True,
                .Font = New Font("Segoe UI", 10, FontStyle.Regular),
                .ForeColor = Color.FromArgb(52, 73, 94),
                .Tag = New With {
                    .ConfidenceId = confidenceLevel.ConfidenceId,
                    .CfValue = confidenceLevel.NilaiCf
                }
            }

            If userAnswers.ContainsKey(question.QuestionId) AndAlso userAnswers(question.QuestionId).ConfidenceId = confidenceLevel.ConfidenceId Then
                rb.Checked = True
            End If

            AddHandler rb.CheckedChanged, AddressOf RadioButton_CheckedChanged
            QuestionsPanel.Controls.Add(rb)
            yPos += 40
        Next

        Button2.Enabled = (currentQuestionIndex > 0)
        UpdateProgress()
    End Sub

    Private Sub UpdateProgress()
        LabelProgress.Text = "Progress: " & userAnswers.Count & " dari " & questions.Count & " pertanyaan"
        ProgressBar1.Maximum = questions.Count
        ProgressBar1.Value = If(userAnswers.Count > ProgressBar1.Maximum, ProgressBar1.Maximum, userAnswers.Count)
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs)
        Dim rb = CType(sender, RadioButton)
        If rb.Checked Then
            Dim tagObj = rb.Tag
            Dim confidenceId = CInt(tagObj.GetType().GetProperty("ConfidenceId").GetValue(tagObj, Nothing))
            Dim cfValue = CDec(tagObj.GetType().GetProperty("CfValue").GetValue(tagObj, Nothing))
            Dim currentQuestion = questions(currentQuestionIndex)
            Dim cfItemScore = cfValue * currentQuestion.CfPakar

            If userAnswers.ContainsKey(currentQuestion.QuestionId) Then
                userAnswers(currentQuestion.QuestionId).ConfidenceId = confidenceId
                userAnswers(currentQuestion.QuestionId).CfItemScore = cfItemScore
            Else
                userAnswers.Add(currentQuestion.QuestionId, New AnswerData() With {
                    .QuestionId = currentQuestion.QuestionId,
                    .ConfidenceId = confidenceId,
                    .CfItemScore = cfItemScore
                })
            End If
            UpdateProgress()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        If currentQuestionIndex > 0 Then
            currentQuestionIndex -= 1
            DisplayQuestion()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        If Not userAnswers.ContainsKey(questions(currentQuestionIndex).QuestionId) Then
            MessageBox.Show("Mohon pilih tingkat keyakinan Anda untuk pertanyaan ini!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If currentQuestionIndex < questions.Count - 1 Then
            currentQuestionIndex += 1
            DisplayQuestion()
        Else
            ProcessResults()
        End If
    End Sub

    Private Sub ProcessResults()
        If userAnswers.Count < questions.Count Then
            MessageBox.Show("Anda baru menjawab " & userAnswers.Count & " dari " & questions.Count & " pertanyaan. Mohon lengkapi semua jawaban!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Using transaction As MySqlTransaction = conn.BeginTransaction()
                    Try
                        ' 1. Insert test record
                        Dim insertTestQuery As String = "INSERT INTO tests (user_id, tanggal) VALUES (@userId, NOW()); SELECT LAST_INSERT_ID();"
                        Dim testId As Integer
                        Using cmd As New MySqlCommand(insertTestQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                            testId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        ' 2. Save user answers to database
                        For Each answer In userAnswers.Values
                            Dim insertAnswerQuery As String = "INSERT INTO test_answers (test_id, question_id, confidence_id, cf_item_score) VALUES (@testId, @questionId, @confidenceId, @cfItemScore)"
                            Using cmd As New MySqlCommand(insertAnswerQuery, conn, transaction)
                                cmd.Parameters.AddWithValue("@testId", testId)
                                cmd.Parameters.AddWithValue("@questionId", answer.QuestionId)
                                cmd.Parameters.AddWithValue("@confidenceId", answer.ConfidenceId)
                                cmd.Parameters.AddWithValue("@cfItemScore", answer.CfItemScore)
                                cmd.ExecuteNonQuery()
                            End Using
                        Next

                        ' 3. Build JSON data untuk Python inference engine
                        ' Get CF_USER from confidence_levels
                        Dim answersForPython As New List(Of Dictionary(Of String, Object))
                        For Each question In questions
                            If userAnswers.ContainsKey(question.QuestionId) Then
                                Dim userAnswer = userAnswers(question.QuestionId)
                                
                                ' Find CF_USER value from confidenceLevels
                                Dim cfUserValue As Decimal = 0.0
                                Dim confidenceLevel = confidenceLevels.FirstOrDefault(Function(c) c.ConfidenceId = userAnswer.ConfidenceId)
                                If confidenceLevel IsNot Nothing Then
                                    cfUserValue = confidenceLevel.NilaiCf
                                End If
                                
                                answersForPython.Add(New Dictionary(Of String, Object) From {
                                    {"question_id", question.QuestionId},
                                    {"option_id", userAnswer.ConfidenceId},
                                    {"category_id", question.TargetCategoryId},
                                    {"cf_user", cfUserValue},
                                    {"cf_pakar", question.CfPakar}
                                })
                            End If
                        Next

                        Dim inputData As New Dictionary(Of String, Object) From {
                            {"user_id", LoggedUserId},
                            {"answers", answersForPython}
                        }
                        Dim inputJson As String = System.Text.Json.JsonSerializer.Serialize(inputData)

                        ' Debug: Show input JSON
                        Console.WriteLine("Python Input JSON: " & inputJson)

                        ' 4. Call Python inference engine
                        Dim pythonResult As String = ""
                        Try
                            pythonResult = CallPythonInferenceRaw(inputJson)
                            Console.WriteLine("Python Output: " & pythonResult)
                        Catch pyEx As Exception
                            transaction.Rollback()
                            MessageBox.Show("Error saat memanggil Python Expert System:" & vbCrLf & pyEx.Message, "Python Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return
                        End Try

                        If String.IsNullOrWhiteSpace(pythonResult) Then
                            transaction.Rollback()
                            MessageBox.Show("Python Expert System tidak mengembalikan hasil.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return
                        End If
                        
                        ' 5. Parse Python result
                        Dim resultDoc = System.Text.Json.JsonDocument.Parse(pythonResult)
                        Dim root = resultDoc.RootElement
                        
                        ' Check for errors
                        Dim errorProp As System.Text.Json.JsonElement
                        If root.TryGetProperty("error", errorProp) Then
                            transaction.Rollback()
                            MessageBox.Show("Python inference error: " & errorProp.GetString(), "Python Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return
                        End If

                        ' 6. Get recommended category from Python
                        Dim recommendedCategory = root.GetProperty("recommended_category")
                        Dim topCategoryId As Integer = recommendedCategory.GetProperty("id").GetInt32()
                        Dim topCategoryName As String = recommendedCategory.GetProperty("name").GetString()
                        Dim topCfScore As Decimal = Convert.ToDecimal(recommendedCategory.GetProperty("score").GetDouble())

                        ' 7. Save category scores from Python
                        Dim categoryScores = root.GetProperty("category_scores")
                        For Each categoryProp In categoryScores.EnumerateObject()
                            Dim categoryId As Integer = Integer.Parse(categoryProp.Name)
                            Dim categoryData = categoryProp.Value
                            Dim cfCombined As Decimal = Convert.ToDecimal(categoryData.GetProperty("cf_combined").GetDouble())
                            
                            If cfCombined > 0 Then
                                Dim insertScoreQuery As String = "INSERT INTO test_category_scores (test_id, category_id, total_cf) VALUES (@testId, @categoryId, @totalCf)"
                                Using cmd As New MySqlCommand(insertScoreQuery, conn, transaction)
                                    cmd.Parameters.AddWithValue("@testId", testId)
                                    cmd.Parameters.AddWithValue("@categoryId", categoryId)
                                    cmd.Parameters.AddWithValue("@totalCf", cfCombined)
                                    cmd.ExecuteNonQuery()
                                End Using
                            End If
                        Next

                        ' 8. Update test with final result
                        Dim updateTestQuery As String = "UPDATE tests SET hasil_akhir = @hasilAkhir, nilai_cf_akhir = @nilaiCf WHERE test_id = @testId"
                        Using cmd As New MySqlCommand(updateTestQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@hasilAkhir", topCategoryName)
                            cmd.Parameters.AddWithValue("@nilaiCf", topCfScore)
                            cmd.Parameters.AddWithValue("@testId", testId)
                            cmd.ExecuteNonQuery()
                        End Using

                        ' 9. Update user minat_bakat
                        Dim updateUserQuery As String = "UPDATE users SET minat_bakat = @minatBakat WHERE user_id = @userId"
                        Using cmd As New MySqlCommand(updateUserQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@minatBakat", topCategoryName)
                            cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                            cmd.ExecuteNonQuery()
                        End Using

                        LoggedMinatBakat = topCategoryName
                        transaction.Commit()

                        MessageBox.Show("Tes berhasil diselesaikan! Hasil perhitungan menggunakan metode Certainty Factor.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Dim hasilForm As New HasilTesMinatBakat()
                        hasilForm.ParentDashboard = Me.ParentDashboard
                        hasilForm.Show()
                        Me.Close()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw New Exception("Gagal memproses hasil tes. " & ex.Message)
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error menyimpan hasil tes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TesMinatBakat_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboard IsNot Nothing AndAlso Not ParentDashboard.IsDisposed Then
            ParentDashboard.Show()
        End If
    End Sub

    Private Sub Button_MouseEnter(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        If btn Is Button3 Then
            btn.BackColor = lightNavyColor
        Else
            btn.BackColor = lightGreyHoverColor
        End If
    End Sub

    Private Sub Button_MouseLeave(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        If btn Is Button3 Then
            btn.BackColor = navyColor
        Else
            btn.BackColor = whiteColor
        End If
    End Sub
End Class