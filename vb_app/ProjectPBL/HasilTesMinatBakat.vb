Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.IO

Public Class HasilTesMinatBakat
    Public Property ParentDashboard As DashboardUser

    Private testId As Integer = 0
    Private categoryResults As New Dictionary(Of Integer, CategoryResult)
    Private ReadOnly navyColor As Color = Color.FromArgb(12, 45, 72)
    Private ReadOnly whiteColor As Color = Color.White
    Private ReadOnly lightGreyHoverColor As Color = Color.FromArgb(236, 240, 241)
    Private ReadOnly blueColor As Color = Color.FromArgb(41, 128, 185)
    Private ReadOnly lightBlueColor As Color = Color.FromArgb(52, 152, 219)
    
    ' For printing
    Private WithEvents printDocument As New PrintDocument()
    Private printPreviewDialog As New PrintPreviewDialog()
    Private printContent As String = ""
    Private printLines As New List(Of String)
    Private printPageNumber As Integer = 0
    Private printLineIndex As Integer = 0

    Public Class CategoryResult
        Public Property CategoryId As Integer
        Public Property CategoryName As String
        Public Property TotalCF As Decimal
        Public Property Percentage As Decimal
        Public Property Rank As Integer
    End Class

    Private Sub HasilTesMinatBakat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Then
            MessageBox.Show("Silakan login terlebih dahulu!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If

        AddHandler ButtonBack.Click, AddressOf BtnBack_Click
        AddHandler ButtonBack.MouseEnter, AddressOf ButtonBack_MouseEnter
        AddHandler ButtonBack.MouseLeave, AddressOf ButtonBack_MouseLeave

        AddHandler ButtonPrintPreview.Click, AddressOf BtnPrintPreview_Click
        AddHandler ButtonPrintPreview.MouseEnter, AddressOf ButtonPrintPreview_MouseEnter
        AddHandler ButtonPrintPreview.MouseLeave, AddressOf ButtonPrintPreview_MouseLeave

        AddHandler ButtonPrint.Click, AddressOf BtnPrint_Click
        AddHandler ButtonPrint.MouseEnter, AddressOf ButtonPrint_MouseEnter
        AddHandler ButtonPrint.MouseLeave, AddressOf ButtonPrint_MouseLeave

        LoadLatestTestResult()
    End Sub

    Private Sub LoadLatestTestResult()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim getTestQuery As String = "SELECT test_id FROM tests WHERE user_id = @userId ORDER BY tanggal DESC LIMIT 1"
                Using cmd As New MySqlCommand(getTestQuery, conn)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    Dim result = cmd.ExecuteScalar()
                    If result Is Nothing OrElse IsDBNull(result) Then
                        MessageBox.Show("Anda belum pernah mengikuti tes!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                        Return
                    End If
                    testId = Convert.ToInt32(result)
                End Using

                LoadCategoryScores(conn)
                DisplayResults()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memuat hasil tes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    Private Sub LoadCategoryScores(conn As MySqlConnection)
        Dim query As String = "SELECT tcs.category_id, c.nama AS category_name, tcs.total_cf FROM test_category_scores tcs INNER JOIN categories c ON tcs.category_id = c.category_id WHERE tcs.test_id = @testId ORDER BY tcs.total_cf DESC"
        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@testId", testId)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim categoryId As Integer = Convert.ToInt32(reader("category_id"))
                    categoryResults(categoryId) = New CategoryResult() With {
                        .CategoryId = categoryId,
                        .CategoryName = reader("category_name").ToString(),
                        .TotalCF = Convert.ToDecimal(reader("total_cf"))
                    }
                End While
            End Using
        End Using

        ' Check if we got any results
        If categoryResults.Count = 0 Then
            Throw New Exception("Tidak ada data kategori yang ditemukan untuk test_id: " & testId & ". Pastikan Python Expert System berhasil menyimpan hasil ke test_category_scores.")
        End If

        Dim totalAllCF As Decimal = categoryResults.Values.Sum(Function(x) x.TotalCF)
        If totalAllCF > 0 Then
            For Each result In categoryResults.Values
                result.Percentage = (result.TotalCF / totalAllCF) * 100
            Next
        End If

        Dim sortedResults = categoryResults.Values.OrderByDescending(Function(x) x.TotalCF).ToList()
        For i As Integer = 0 To sortedResults.Count - 1
            sortedResults(i).Rank = i + 1
        Next
    End Sub

    Private Sub DisplayResults()
        Label1.Text = "🎯 Hasil Tes Minat Bakat"
        Label2.Text = "Halo " & LoggedFullName & ","

        Dim topCategory = categoryResults.Values.OrderByDescending(Function(x) x.TotalCF).FirstOrDefault()
        If topCategory IsNot Nothing Then
            Label3.Text = "Anda memiliki kecondongan lebih di bidang " & topCategory.CategoryName & " 🌟"
            Label4.Text = "Berdasarkan hasil tes dengan metode Certainty Factor, nilai CF tertinggi adalah " & topCategory.TotalCF.ToString("F4") & " (" & topCategory.Percentage.ToString("F2") & "%)"
            DisplayDetailedResults()
        Else
            Label3.Text = "Tidak ada hasil yang tersedia."
            Label4.Text = ""
        End If
    End Sub

    Private Sub DisplayDetailedResults()
        Dim resultPanel As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.TopDown,
            .AutoScroll = True,
            .WrapContents = False,
            .Padding = New Padding(20, 0, 20, 20)
        }
        cardPanel.Controls.Clear()
        cardPanel.Controls.Add(resultPanel)

        Dim lblHeader As New Label() With {
            .Text = "Detail Peringkat Minat Bakat:",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .AutoSize = True,
            .Margin = New Padding(0, 10, 0, 10)
        }
        resultPanel.Controls.Add(lblHeader)

        Dim sortedResults = categoryResults.Values.OrderByDescending(Function(x) x.TotalCF).ToList()
        For Each result In sortedResults
            Dim categoryCard As New Panel() With {
                .Size = New Size(resultPanel.ClientSize.Width - 10, 60),
                .BackColor = GetCategoryColor(result.Rank),
                .Margin = New Padding(3, 3, 3, 3)
            }

            Dim lblRank As New Label() With {
                .Text = "#" & result.Rank,
                .Location = New Point(10, 15),
                .Size = New Size(40, 30),
                .Font = New Font("Segoe UI", 14, FontStyle.Bold),
                .ForeColor = Color.White,
                .TextAlign = ContentAlignment.MiddleCenter,
                .BackColor = Color.Transparent
            }
            categoryCard.Controls.Add(lblRank)

            Dim lblCategoryName As New Label() With {
                .Text = result.CategoryName,
                .Location = New Point(60, 10),
                .AutoSize = True,
                .Font = New Font("Segoe UI", 11, FontStyle.Bold),
                .ForeColor = Color.White,
                .BackColor = Color.Transparent
            }
            categoryCard.Controls.Add(lblCategoryName)

            Dim lblCFPercentage As New Label() With {
                .Text = "CF: " & result.TotalCF.ToString("F4") & " | " & result.Percentage.ToString("F2") & "%",
                .Location = New Point(60, 35),
                .AutoSize = True,
                .Font = New Font("Segoe UI", 9, FontStyle.Regular),
                .ForeColor = Color.FromArgb(236, 240, 241),
                .BackColor = Color.Transparent
            }
            categoryCard.Controls.Add(lblCFPercentage)

            resultPanel.Controls.Add(categoryCard)
        Next

        ' Add separator
        Dim separator As New Label() With {
            .Text = "",
            .Height = 20,
            .Width = resultPanel.ClientSize.Width - 10,
            .Margin = New Padding(0, 10, 0, 10)
        }
        resultPanel.Controls.Add(separator)

        ' Display answer details
        DisplayAnswerDetails(resultPanel)
    End Sub

    Private Sub DisplayAnswerDetails(parentPanel As FlowLayoutPanel)
        Try
            Dim lblAnswerHeader As New Label() With {
                .Text = "📝 Detail Jawaban Anda:",
                .Font = New Font("Segoe UI", 12, FontStyle.Bold),
                .ForeColor = Color.FromArgb(44, 62, 80),
                .AutoSize = True,
                .Margin = New Padding(0, 10, 0, 10)
            }
            parentPanel.Controls.Add(lblAnswerHeader)

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT ta.question_id, q.isi_pertanyaan, c.nama AS category_name, cl.nama_level, cl.nilai_cf AS cf_user, q.cf_pakar, ta.cf_item_score " &
                                     "FROM test_answers ta " &
                                     "INNER JOIN questions q ON ta.question_id = q.question_id " &
                                     "INNER JOIN categories c ON q.target_category_id = c.category_id " &
                                     "INNER JOIN confidence_levels cl ON ta.confidence_id = cl.confidence_id " &
                                     "WHERE ta.test_id = @testId " &
                                     "ORDER BY ta.question_id"
                
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@testId", testId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Dim questionNumber As Integer = 1
                        While reader.Read()
                            Dim answerCard As New Panel() With {
                                .Width = parentPanel.ClientSize.Width - 10,
                                .AutoSize = True,
                                .BackColor = Color.FromArgb(250, 250, 250),
                                .Margin = New Padding(3, 3, 3, 3),
                                .Padding = New Padding(10)
                            }

                            Dim lblQuestion As New Label() With {
                                .Text = questionNumber & ". " & reader("isi_pertanyaan").ToString(),
                                .Location = New Point(10, 10),
                                .MaximumSize = New Size(answerCard.Width - 20, 0),
                                .AutoSize = True,
                                .Font = New Font("Segoe UI", 10, FontStyle.Bold),
                                .ForeColor = Color.FromArgb(52, 73, 94)
                            }
                            answerCard.Controls.Add(lblQuestion)

                            Dim yPos As Integer = lblQuestion.Bottom + 5
                            
                            Dim lblCategory As New Label() With {
                                .Text = "Kategori: " & reader("category_name").ToString(),
                                .Location = New Point(10, yPos),
                                .AutoSize = True,
                                .Font = New Font("Segoe UI", 9, FontStyle.Regular),
                                .ForeColor = Color.FromArgb(127, 140, 141)
                            }
                            answerCard.Controls.Add(lblCategory)
                            yPos += 20

                            Dim lblConfidence As New Label() With {
                                .Text = "Keyakinan: " & reader("nama_level").ToString() & " (CF User: " & Convert.ToDecimal(reader("cf_user")).ToString("F2") & ")",
                                .Location = New Point(10, yPos),
                                .AutoSize = True,
                                .Font = New Font("Segoe UI", 9, FontStyle.Regular),
                                .ForeColor = Color.FromArgb(41, 128, 185)
                            }
                            answerCard.Controls.Add(lblConfidence)
                            yPos += 20

                            Dim cfPakar As Decimal = Convert.ToDecimal(reader("cf_pakar"))
                            Dim cfItemScore As Decimal = Convert.ToDecimal(reader("cf_item_score"))
                            Dim lblCF As New Label() With {
                                .Text = "CF Pakar: " & cfPakar.ToString("F2") & " | CF Item: " & cfItemScore.ToString("F4"),
                                .Location = New Point(10, yPos),
                                .AutoSize = True,
                                .Font = New Font("Segoe UI", 9, FontStyle.Italic),
                                .ForeColor = Color.FromArgb(39, 174, 96)
                            }
                            answerCard.Controls.Add(lblCF)

                            answerCard.Height = lblCF.Bottom + 10
                            parentPanel.Controls.Add(answerCard)
                            questionNumber += 1
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error memuat detail jawaban: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetCategoryColor(rank As Integer) As Color
        Select Case rank
            Case 1 : Return Color.FromArgb(46, 204, 113)
            Case 2 : Return Color.FromArgb(52, 152, 219)
            Case 3 : Return Color.FromArgb(155, 89, 182)
            Case Else : Return Color.FromArgb(127, 140, 141)
        End Select
    End Function

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub HasilTesMinatBakat_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboard IsNot Nothing AndAlso Not ParentDashboard.IsDisposed Then
            ParentDashboard.Show()
        End If
    End Sub

    Private Sub ButtonBack_MouseEnter(sender As Object, e As EventArgs)
        ButtonBack.BackColor = lightGreyHoverColor
    End Sub

    Private Sub ButtonBack_MouseLeave(sender As Object, e As EventArgs)
        ButtonBack.BackColor = whiteColor
    End Sub

    Private Sub ButtonPrintPreview_MouseEnter(sender As Object, e As EventArgs)
        ButtonPrintPreview.BackColor = lightBlueColor
    End Sub

    Private Sub ButtonPrintPreview_MouseLeave(sender As Object, e As EventArgs)
        ButtonPrintPreview.BackColor = blueColor
    End Sub

    Private Sub ButtonPrint_MouseEnter(sender As Object, e As EventArgs)
        ButtonPrint.BackColor = Color.FromArgb(28, 68, 105)
    End Sub

    Private Sub ButtonPrint_MouseLeave(sender As Object, e As EventArgs)
        ButtonPrint.BackColor = navyColor
    End Sub

    Private Sub BtnPrintPreview_Click(sender As Object, e As EventArgs)
        Try
            ' Generate print content
            printContent = GeneratePrintContent()
            
            ' Split into lines and wrap long lines
            printLines = New List(Of String)
            Dim allLines() As String = printContent.Split(New String() {vbCrLf, vbLf}, StringSplitOptions.None)
            For Each line In allLines
                If line.Length > 80 Then
                    ' Wrap long lines
                    Dim words() As String = line.Split(" "c)
                    Dim currentLine As String = ""
                    For Each word In words
                        If (currentLine & " " & word).Length > 80 Then
                            If currentLine.Length > 0 Then printLines.Add(currentLine)
                            currentLine = word
                        Else
                            currentLine = If(currentLine.Length > 0, currentLine & " " & word, word)
                        End If
                    Next
                    If currentLine.Length > 0 Then printLines.Add(currentLine)
                Else
                    printLines.Add(line)
                End If
            Next
            
            printPageNumber = 0
            printLineIndex = 0

            ' Setup print preview
            printPreviewDialog.Document = printDocument
            printPreviewDialog.Width = 800
            printPreviewDialog.Height = 600
            printPreviewDialog.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error membuka print preview: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs)
        Try
            Dim saveDialog As New SaveFileDialog() With {
                .Filter = "PDF Files (*.pdf)|*.pdf",
                .DefaultExt = "pdf",
                .FileName = "Hasil_Tes_Minat_Bakat_" & LoggedFullName.Replace(" ", "_") & "_" & DateTime.Now.ToString("yyyyMMdd") & ".pdf"
            }

            If saveDialog.ShowDialog() = DialogResult.OK Then
                ExportToPdf(saveDialog.FileName)
                MessageBox.Show("File PDF berhasil disimpan!" & vbCrLf & vbCrLf & "Lokasi: " & saveDialog.FileName, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                
                Dim result = MessageBox.Show("Apakah Anda ingin membuka file PDF?", "Buka File", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    Process.Start(New ProcessStartInfo(saveDialog.FileName) With {.UseShellExecute = True})
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error saat export PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExportToPdf(filePath As String)
        ' Create HTML content for PDF
        Dim html As New System.Text.StringBuilder()
        html.AppendLine("<!DOCTYPE html>")
        html.AppendLine("<html><head><meta charset='UTF-8'>")
        html.AppendLine("<style>")
        html.AppendLine("body { font-family: 'Segoe UI', Arial, sans-serif; margin: 40px; color: #2c3e50; }")
        html.AppendLine("h1 { color: #0c2d48; border-bottom: 3px solid #0c2d48; padding-bottom: 10px; }")
        html.AppendLine("h2 { color: #34495e; margin-top: 30px; }")
        html.AppendLine(".header { background-color: #0c2d48; color: white; padding: 20px; margin: -40px -40px 30px -40px; }")
        html.AppendLine(".info { background-color: #ecf0f1; padding: 15px; border-radius: 5px; margin: 20px 0; }")
        html.AppendLine(".category { margin: 15px 0; padding: 15px; border-radius: 5px; color: white; }")
        html.AppendLine(".rank1 { background-color: #2ecc71; }")
        html.AppendLine(".rank2 { background-color: #3498db; }")
        html.AppendLine(".rank3 { background-color: #9b59b6; }")
        html.AppendLine(".rankOther { background-color: #7f8c8d; }")
        html.AppendLine(".question { background-color: #f8f9fa; padding: 12px; margin: 10px 0; border-left: 4px solid #0c2d48; }")
        html.AppendLine(".question-text { font-weight: bold; color: #2c3e50; }")
        html.AppendLine(".question-detail { color: #7f8c8d; font-size: 0.9em; margin-top: 5px; }")
        html.AppendLine(".footer { margin-top: 40px; padding-top: 20px; border-top: 1px solid #bdc3c7; text-align: center; color: #95a5a6; font-size: 0.85em; }")
        html.AppendLine("</style></head><body>")

        ' Header
        html.AppendLine("<div class='header'>")
        html.AppendLine("<h1 style='margin:0; color: white; border:none; padding:0;'>HASIL TES MINAT BAKAT</h1>")
        html.AppendLine("<p style='margin:5px 0 0 0;'>Sistem Informasi Topik Skripsi (SITOPSI)</p>")
        html.AppendLine("</div>")

        ' User info
        html.AppendLine("<div class='info'>")
        html.AppendLine($"<strong>Nama:</strong> {LoggedFullName}<br>")
        html.AppendLine($"<strong>Tanggal Tes:</strong> {DateTime.Now:dd MMMM yyyy}<br>")
        html.AppendLine($"<strong>Test ID:</strong> {testId}")
        html.AppendLine("</div>")

        ' Top result
        Dim topCategory = categoryResults.Values.OrderByDescending(Function(x) x.TotalCF).FirstOrDefault()
        If topCategory IsNot Nothing Then
            html.AppendLine("<h2>Hasil Utama</h2>")
            html.AppendLine($"<p><strong>Anda memiliki kecondongan lebih di bidang: {topCategory.CategoryName} 🌟</strong></p>")
            html.AppendLine($"<p>Berdasarkan hasil tes dengan metode Certainty Factor, nilai CF tertinggi adalah <strong>{topCategory.TotalCF:F4}</strong> ({topCategory.Percentage:F2}%)</p>")
        End If

        ' Category rankings
        html.AppendLine("<h2>Peringkat Kategori Minat Bakat</h2>")
        Dim sortedResults = categoryResults.Values.OrderByDescending(Function(x) x.TotalCF).ToList()
        For Each result In sortedResults
            Dim rankClass = If(result.Rank = 1, "rank1", If(result.Rank = 2, "rank2", If(result.Rank = 3, "rank3", "rankOther")))
            html.AppendLine($"<div class='category {rankClass}'>")
            html.AppendLine($"<strong>#{result.Rank} - {result.CategoryName}</strong><br>")
            html.AppendLine($"CF: {result.TotalCF:F4} | Persentase: {result.Percentage:F2}%")
            html.AppendLine("</div>")
        Next

        ' Answer details
        html.AppendLine("<h2>Detail Jawaban</h2>")
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT ta.question_id, q.isi_pertanyaan, c.nama AS category_name, cl.nama_level, cl.nilai_cf AS cf_user, q.cf_pakar, ta.cf_item_score " &
                                 "FROM test_answers ta " &
                                 "INNER JOIN questions q ON ta.question_id = q.question_id " &
                                 "INNER JOIN categories c ON q.target_category_id = c.category_id " &
                                 "INNER JOIN confidence_levels cl ON ta.confidence_id = cl.confidence_id " &
                                 "WHERE ta.test_id = @testId " &
                                 "ORDER BY ta.question_id"
            
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@testId", testId)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    Dim questionNumber As Integer = 1
                    While reader.Read()
                        html.AppendLine("<div class='question'>")
                        html.AppendLine($"<div class='question-text'>{questionNumber}. {reader("isi_pertanyaan")}</div>")
                        html.AppendLine("<div class='question-detail'>")
                        html.AppendLine($"Kategori: {reader("category_name")} | ")
                        html.AppendLine($"Keyakinan: {reader("nama_level")} (CF User: {Convert.ToDecimal(reader("cf_user")):F2}) | ")
                        html.AppendLine($"CF Pakar: {Convert.ToDecimal(reader("cf_pakar")):F2} | ")
                        html.AppendLine($"CF Item: {Convert.ToDecimal(reader("cf_item_score")):F4}")
                        html.AppendLine("</div>")
                        html.AppendLine("</div>")
                        questionNumber += 1
                    End While
                End Using
            End Using
        End Using

        ' Footer
        html.AppendLine("<div class='footer'>")
        html.AppendLine($"<p>Dokumen ini digenerate otomatis oleh SITOPSI pada {DateTime.Now:dd MMMM yyyy HH:mm:ss}</p>")
        html.AppendLine("<p>© 2025 SITOPSI - Sistem Informasi Topik Skripsi</p>")
        html.AppendLine("</div>")

        html.AppendLine("</body></html>")

        ' Convert HTML to PDF using built-in WebBrowser control
        ConvertHtmlToPdf(html.ToString(), filePath)
    End Sub

    Private Sub ConvertHtmlToPdf(htmlContent As String, pdfPath As String)
        ' Save HTML to temp file
        Dim tempHtmlPath As String = Path.Combine(Path.GetTempPath(), "sitopsi_temp_" & Guid.NewGuid().ToString() & ".html")
        File.WriteAllText(tempHtmlPath, htmlContent, System.Text.Encoding.UTF8)

        ' Use default browser to print to PDF (Windows 10+)
        Try
            ' Create print command for edge/chrome
            Dim psi As New ProcessStartInfo() With {
                .FileName = "msedge.exe",
                .Arguments = $"--headless --disable-gpu --print-to-pdf=""{pdfPath}"" ""{tempHtmlPath}""",
                .UseShellExecute = False,
                .CreateNoWindow = True
            }
            
            Using process As Process = Process.Start(psi)
                process.WaitForExit(10000)
            End Using

            ' Wait a bit for file to be written
            System.Threading.Thread.Sleep(1000)

            ' Clean up temp file
            Try
                File.Delete(tempHtmlPath)
            Catch
            End Try

        Catch ex As Exception
            ' Fallback: open in browser and let user print
            Process.Start(New ProcessStartInfo(tempHtmlPath) With {.UseShellExecute = True})
            MessageBox.Show("Browser telah dibuka. Silakan gunakan fungsi Print to PDF di browser (Ctrl+P)." & vbCrLf & vbCrLf & "File akan disimpan ke: " & pdfPath, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Function GeneratePrintContent() As String
        Dim content As New System.Text.StringBuilder()

        ' Header
        content.AppendLine("========================================")
        content.AppendLine("     HASIL TES MINAT BAKAT")
        content.AppendLine("     SITOPSI - Sistem Informasi Topik Skripsi")
        content.AppendLine("========================================")
        content.AppendLine()
        content.AppendLine($"Nama      : {LoggedFullName}")
        content.AppendLine($"Tanggal   : {DateTime.Now:dd MMMM yyyy HH:mm:ss}")
        content.AppendLine()
        content.AppendLine("========================================")
        content.AppendLine("     HASIL KATEGORI MINAT BAKAT")
        content.AppendLine("========================================")
        content.AppendLine()

        ' Category rankings
        Dim sortedCategories = categoryResults.Values.OrderByDescending(Function(c) c.TotalCF).ToList()
        Dim rank As Integer = 1
        For Each cat In sortedCategories
            content.AppendLine($"#{rank}. {cat.CategoryName}")
            content.AppendLine($"    CF Combined : {cat.TotalCF:F4}")
            content.AppendLine($"    Persentase  : {cat.Percentage:F2}%")
            content.AppendLine()
            rank += 1
        Next

        content.AppendLine("========================================")
        content.AppendLine("     DETAIL JAWABAN TES")
        content.AppendLine("========================================")
        content.AppendLine()

        ' Load answer details for print
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "
                    SELECT q.isi_pertanyaan, c.nama AS category_name, cl.nama_level, cl.nilai_cf, ta.cf_item_score
                    FROM test_answers ta
                    JOIN questions q ON ta.question_id = q.question_id
                    JOIN categories c ON q.target_category_id = c.category_id
                    JOIN confidence_levels cl ON ta.confidence_id = cl.confidence_id
                    WHERE ta.test_id = @testId
                    ORDER BY ta.answer_id"
                
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@testId", testId)
                    Using reader = cmd.ExecuteReader()
                        Dim qNum As Integer = 1
                        While reader.Read()
                            content.AppendLine($"Pertanyaan {qNum}:")
                            content.AppendLine($"  {reader("isi_pertanyaan")}")
                            content.AppendLine($"  Kategori : {reader("category_name")}")
                            content.AppendLine($"  Jawaban  : {reader("nama_level")}")
                            content.AppendLine($"  CF User  : {Convert.ToDecimal(reader("nilai_cf")):F2}")
                            content.AppendLine($"  CF Item  : {Convert.ToDecimal(reader("cf_item_score")):F4}")
                            content.AppendLine()
                            qNum += 1
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            content.AppendLine("Error memuat detail jawaban: " & ex.Message)
        End Try

        content.AppendLine("========================================")
        content.AppendLine()
        content.AppendLine("Catatan:")
        content.AppendLine("Hasil tes ini menggunakan metode Certainty Factor")
        content.AppendLine("untuk menghitung tingkat keyakinan minat bakat Anda.")
        content.AppendLine()
        content.AppendLine("© 2025 SITOPSI")
        content.AppendLine("========================================")

        Return content.ToString()
    End Function

    Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDocument.PrintPage
        Try
            Dim font As New Font("Consolas", 9)
            Dim brush As New SolidBrush(Color.Black)
            
            Dim leftMargin As Single = e.MarginBounds.Left
            Dim topMargin As Single = e.MarginBounds.Top
            Dim yPos As Single = topMargin
            Dim lineHeight As Single = font.GetHeight(e.Graphics)
            
            ' Calculate lines per page
            Dim linesPerPage As Integer = CInt((e.MarginBounds.Height - 40) / lineHeight)
            
            ' Print lines for this page
            Dim linesPrinted As Integer = 0
            While printLineIndex < printLines.Count AndAlso linesPrinted < linesPerPage
                e.Graphics.DrawString(printLines(printLineIndex), font, brush, leftMargin, yPos)
                yPos += lineHeight
                printLineIndex += 1
                linesPrinted += 1
            End While
            
            ' Add page number at bottom
            printPageNumber += 1
            Dim pageNumText As String = $"Halaman {printPageNumber}"
            Dim pageNumSize = e.Graphics.MeasureString(pageNumText, font)
            e.Graphics.DrawString(pageNumText, font, brush, 
                                e.MarginBounds.Right - pageNumSize.Width, 
                                e.MarginBounds.Bottom + 10)
            
            ' Check if more pages needed
            If printLineIndex < printLines.Count Then
                e.HasMorePages = True
            Else
                e.HasMorePages = False
                printLineIndex = 0
                printPageNumber = 0
            End If
        Catch ex As Exception
            MessageBox.Show("Error printing page: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            e.HasMorePages = False
        End Try
    End Sub
End Class