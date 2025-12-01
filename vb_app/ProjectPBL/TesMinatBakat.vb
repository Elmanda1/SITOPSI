Imports MySql.Data.MySqlClient

Public Class TesMinatBakat
    ' Data structures untuk menyimpan pertanyaan dan jawaban
    Private questions As New List(Of QuestionData)
    Private userAnswers As New Dictionary(Of Integer, AnswerData)
    Private currentPage As Integer = 1
    Private totalPages As Integer = 10 ' 100 pertanyaan / 10 per halaman
    Private questionsPerPage As Integer = 10

    ' Reference ke dashboard untuk kembali
    Public Property ParentDashboard As DashboardUser

    ' Class untuk menyimpan data pertanyaan
    Public Class QuestionData
        Public Property QuestionId As Integer
        Public Property QuestionNumber As Integer
        Public Property QuestionText As String
        Public Property Options As New List(Of OptionData)
    End Class

    ' Class untuk menyimpan data option
    Public Class OptionData
        Public Property OptionId As Integer
        Public Property OptionCode As String
        Public Property OptionText As String
        Public Property CategoryId As Integer
        Public Property CfPakar As Decimal
    End Class

    ' Class untuk menyimpan jawaban user
    Public Class AnswerData
        Public Property QuestionId As Integer
        Public Property OptionId As Integer
        Public Property CfUser As Decimal
    End Class

    Private Sub TesMinatBakat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Cek apakah user sudah login
        If Not IsLoggedIn() Then
            MessageBox.Show("Silakan login terlebih dahulu!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Dim loginForm As New LoginForm()
            loginForm.Show()
            Me.Close()
            Return
        End If

        ' Center form
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Set background color
        Me.BackColor = Color.FromArgb(236, 240, 241)

        ' Set welcome message
        Label1.Text = $"📋 Tes Minat Bakat, {LoggedFullName}!"

        ' Load questions dari database
        LoadQuestions()

        ' Tampilkan halaman pertama
        DisplayCurrentPage()
    End Sub

    Private Sub LoadQuestions()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' Query untuk mengambil semua pertanyaan dengan options
                Dim query As String = "SELECT q.id as question_id, q.nomor, q.isi_pertanyaan, " &
                                     "qo.id as option_id, qo.kode, qo.teks, qo.category_id, qo.cf_pakar " &
                                     "FROM questions q " &
                                     "INNER JOIN question_options qo ON q.id = qo.question_id " &
                                     "WHERE q.is_active = 1 " &
                                     "ORDER BY q.nomor, qo.sort_order"

                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Dim currentQuestionId As Integer = 0
                        Dim currentQuestion As QuestionData = Nothing

                        While reader.Read()
                            Dim questionId As Integer = Convert.ToInt32(reader("question_id"))

                            ' Jika pertanyaan baru
                            If questionId <> currentQuestionId Then
                                ' Simpan pertanyaan sebelumnya
                                If currentQuestion IsNot Nothing Then
                                    questions.Add(currentQuestion)
                                End If

                                ' Buat pertanyaan baru
                                currentQuestion = New QuestionData() With {
                                    .QuestionId = questionId,
                                    .QuestionNumber = Convert.ToInt32(reader("nomor")),
                                    .QuestionText = reader("isi_pertanyaan").ToString()
                                }
                                currentQuestionId = questionId
                            End If

                            ' Tambahkan option ke pertanyaan current
                            If currentQuestion IsNot Nothing Then
                                Dim optionData As New OptionData() With {
                                    .OptionId = Convert.ToInt32(reader("option_id")),
                                    .OptionCode = reader("kode").ToString(),
                                    .OptionText = reader("teks").ToString(),
                                    .CategoryId = Convert.ToInt32(reader("category_id")),
                                    .CfPakar = Convert.ToDecimal(reader("cf_pakar"))
                                }
                                currentQuestion.Options.Add(optionData)
                            End If
                        End While

                        ' Simpan pertanyaan terakhir
                        If currentQuestion IsNot Nothing Then
                            questions.Add(currentQuestion)
                        End If
                    End Using
                End Using
            End Using

            If questions.Count = 0 Then
                MessageBox.Show("Tidak ada pertanyaan yang tersedia!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Return
            End If

            ' Hitung total pages berdasarkan jumlah pertanyaan
            totalPages = Math.Ceiling(questions.Count / questionsPerPage)

        Catch ex As Exception
            MessageBox.Show($"Error loading questions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    Private Sub DisplayCurrentPage()
        ' Clear semua controls yang ada
        ClearDynamicControls()

        ' Hitung index awal dan akhir pertanyaan untuk halaman ini
        Dim startIndex As Integer = (currentPage - 1) * questionsPerPage
        Dim endIndex As Integer = Math.Min(startIndex + questionsPerPage - 1, questions.Count - 1)

        Dim yPosition As Integer = 80 ' Posisi Y awal

        ' Tampilkan pertanyaan untuk halaman ini
        For i As Integer = startIndex To endIndex
            Dim question As QuestionData = questions(i)

            ' Buat panel untuk setiap pertanyaan
            Dim questionPanel As New Panel() With {
                .Location = New Point(30, yPosition),
                .Size = New Size(Me.ClientSize.Width - 70, 45 + (question.Options.Count * 30)),
                .BackColor = Color.White,
                .BorderStyle = BorderStyle.FixedSingle,
                .Tag = "dynamic"
            }
            Me.Controls.Add(questionPanel)

            ' Buat label untuk pertanyaan
            Dim lblQuestion As New Label() With {
                .Text = $"{question.QuestionNumber}. {question.QuestionText}",
                .Location = New Point(10, 10),
                .AutoSize = False,
                .Width = questionPanel.Width - 20,
                .Height = 30,
                .Font = New Font("Segoe UI", 10, FontStyle.Bold),
                .ForeColor = Color.FromArgb(44, 62, 80)
            }
            questionPanel.Controls.Add(lblQuestion)

            Dim optionYPosition As Integer = 40

            ' Buat radio buttons untuk setiap option
            For Each opt As OptionData In question.Options
                Dim rb As New RadioButton() With {
                    .Text = $"{opt.OptionCode}. {opt.OptionText}",
                    .Location = New Point(20, optionYPosition),
                    .AutoSize = False,
                    .Width = questionPanel.Width - 40,
                    .Height = 25,
                    .Font = New Font("Segoe UI", 9, FontStyle.Regular),
                    .ForeColor = Color.FromArgb(52, 73, 94),
                    .Tag = New With {
                        .Type = "dynamic",
                        .QuestionId = question.QuestionId,
                        .OptionId = opt.OptionId,
                        .CfPakar = opt.CfPakar
                    },
                    .Checked = False
                }

                ' Cek apakah sudah ada jawaban sebelumnya
                If userAnswers.ContainsKey(question.QuestionId) Then
                    If userAnswers(question.QuestionId).OptionId = opt.OptionId Then
                        rb.Checked = True
                    End If
                End If

                AddHandler rb.CheckedChanged, AddressOf RadioButton_CheckedChanged
                questionPanel.Controls.Add(rb)

                optionYPosition += 30
            Next

            yPosition += questionPanel.Height + 15
        Next

        ' Update button text dan visibility
        If currentPage = 1 Then
            Button2.Visible = False
        Else
            Button2.Visible = True
            Button2.Text = "« Halaman Sebelumnya"
        End If

        If currentPage = totalPages Then
            Button3.Text = "✓ Selesai & Hitung"
        Else
            Button3.Text = $"Halaman {currentPage + 1} »"
        End If

        ' Update progress
        UpdateProgress()

        ' Update info halaman di Label1
        Label1.Text = $"📋 Tes Minat Bakat, {LoggedFullName}! - Halaman {currentPage}/{totalPages}"
    End Sub

    Private Sub UpdateProgress()
        ' Hitung berapa pertanyaan yang sudah dijawab
        Dim answeredCount As Integer = userAnswers.Count
        Dim totalQuestions As Integer = questions.Count

        ' Update label progress
        LabelProgress.Text = $"Progress: {answeredCount} dari {totalQuestions} pertanyaan"

        ' Update progress bar
        ProgressBar1.Maximum = totalQuestions
        ProgressBar1.Value = answeredCount
    End Sub

    Private Sub ClearDynamicControls()
        ' Hapus semua controls yang dinamis
        Dim controlsToRemove As New List(Of Control)

        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl.Tag Is String AndAlso ctrl.Tag.ToString() = "dynamic" Then
                controlsToRemove.Add(ctrl)
            ElseIf ctrl.Tag IsNot Nothing AndAlso Not TypeOf ctrl.Tag Is String Then
                Try
                    Dim tagObj = ctrl.Tag
                    If tagObj.GetType().GetProperty("Type") IsNot Nothing Then
                        Dim typeValue = tagObj.GetType().GetProperty("Type").GetValue(tagObj, Nothing)
                        If typeValue IsNot Nothing AndAlso typeValue.ToString() = "dynamic" Then
                            controlsToRemove.Add(ctrl)
                        End If
                    End If
                Catch
                    ' Ignore
                End Try
            End If
        Next

        For Each ctrl In controlsToRemove
            If TypeOf ctrl Is Panel Then
                ' Hapus semua radio button di panel terlebih dahulu
                Dim panelControls As New List(Of Control)
                For Each panelCtrl As Control In CType(ctrl, Panel).Controls
                    panelControls.Add(panelCtrl)
                Next

                For Each panelCtrl In panelControls
                    If TypeOf panelCtrl Is RadioButton Then
                        RemoveHandler CType(panelCtrl, RadioButton).CheckedChanged, AddressOf RadioButton_CheckedChanged
                    End If
                    CType(ctrl, Panel).Controls.Remove(panelCtrl)
                    panelCtrl.Dispose()
                Next
            End If

            Me.Controls.Remove(ctrl)
            ctrl.Dispose()
        Next
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs)
        Dim rb As RadioButton = CType(sender, RadioButton)
        If rb.Checked Then
            Dim tagObj = rb.Tag
            Dim questionId As Integer = CInt(tagObj.GetType().GetProperty("QuestionId").GetValue(tagObj, Nothing))
            Dim optionId As Integer = CInt(tagObj.GetType().GetProperty("OptionId").GetValue(tagObj, Nothing))
            Dim cfPakar As Decimal = CDec(tagObj.GetType().GetProperty("CfPakar").GetValue(tagObj, Nothing))

            ' Simpan jawaban (CF User = 1.0 karena user memilih opsi ini)
            If userAnswers.ContainsKey(questionId) Then
                userAnswers(questionId).OptionId = optionId
            Else
                userAnswers.Add(questionId, New AnswerData() With {
                    .QuestionId = questionId,
                    .OptionId = optionId,
                    .CfUser = 1.0D
                })
            End If

            ' Update progress
            UpdateProgress()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Kembali ke halaman sebelumnya
        If currentPage > 1 Then
            currentPage -= 1
            DisplayCurrentPage()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Validasi: pastikan semua pertanyaan di halaman ini sudah dijawab
        Dim startIndex As Integer = (currentPage - 1) * questionsPerPage
        Dim endIndex As Integer = Math.Min(startIndex + questionsPerPage - 1, questions.Count - 1)
        
        For i As Integer = startIndex To endIndex
            Dim questionId As Integer = questions(i).QuestionId
            If Not userAnswers.ContainsKey(questionId) Then
                MessageBox.Show($"Mohon jawab pertanyaan nomor {questions(i).QuestionNumber}!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        ' Jika belum halaman terakhir, lanjut ke halaman berikutnya
        If currentPage < totalPages Then
            currentPage += 1
            DisplayCurrentPage()
        Else
            ' Halaman terakhir - proses hasil
            ProcessResults()
        End If
    End Sub

    Private Sub ProcessResults()
        Try
            ' Hitung total pertanyaan yang dijawab
            If userAnswers.Count < questions.Count Then
                MessageBox.Show($"Anda baru menjawab {userAnswers.Count} dari {questions.Count} pertanyaan. Mohon lengkapi semua jawaban!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' Mulai transaction
                Using transaction As MySqlTransaction = conn.BeginTransaction()
                    Try
                        ' Insert ke tabel tests
                        Dim insertTestQuery As String = "INSERT INTO tests (user_id, tanggal) VALUES (@userId, NOW()); SELECT LAST_INSERT_ID();"
                        Dim testId As Integer = 0

                        Using cmd As New MySqlCommand(insertTestQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                            testId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        ' Insert semua jawaban ke test_answers
                        For Each answer In userAnswers.Values
                            Dim insertAnswerQuery As String = "INSERT INTO test_answers (test_id, question_id, option_id, cf_user_value) " &
                                                             "VALUES (@testId, @questionId, @optionId, @cfUser)"

                            Using cmd As New MySqlCommand(insertAnswerQuery, conn, transaction)
                                cmd.Parameters.AddWithValue("@testId", testId)
                                cmd.Parameters.AddWithValue("@questionId", answer.QuestionId)
                                cmd.Parameters.AddWithValue("@optionId", answer.OptionId)
                                cmd.Parameters.AddWithValue("@cfUser", answer.CfUser)
                                cmd.ExecuteNonQuery()
                            End Using
                        Next

                        ' Commit transaction
                        transaction.Commit()

                        ' Log activity
                        Try
                            Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, 'Tes Minat Bakat', @detail, NOW())"
                            Using logCmd As New MySqlCommand(logQuery, conn)
                                logCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                                logCmd.Parameters.AddWithValue("@detail", $"User menyelesaikan tes minat bakat dengan {userAnswers.Count} jawaban")
                                logCmd.ExecuteNonQuery()
                            End Using
                        Catch
                            ' Silent fail
                        End Try

                        MessageBox.Show("Tes berhasil diselesaikan! Hasil akan ditampilkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' Redirect ke Hasil Tes dengan pass ParentDashboard
                        Dim hasilForm As New HasilTesMinatBakat()
                        hasilForm.ParentDashboard = Me.ParentDashboard
                        hasilForm.Show()
                        Me.Close()

                    Catch ex As Exception
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show($"Error menyimpan hasil tes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TesMinatBakat_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Konfirmasi jika ada jawaban yang belum disimpan
        If userAnswers.Count > 0 AndAlso userAnswers.Count < questions.Count Then
            Dim result As DialogResult = MessageBox.Show("Anda belum menyelesaikan tes. Apakah Anda yakin ingin keluar?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
                e.Cancel = True
                Return
            End If
        End If

        ' Kembali ke dashboard user
        If ParentDashboard IsNot Nothing Then
            ParentDashboard.Show()
        Else
            ' Jika tidak ada parent (seharusnya tidak terjadi), buat baru
            ' Tapi ini berarti user membuka form ini secara langsung
            If IsLoggedIn() Then
                Dim dashboardUser As New DashboardUser()
                dashboardUser.Show()
            Else
                Dim loginForm As New LoginForm()
                loginForm.Show()
            End If
        End If
    End Sub
End Class