Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.IO

Public Class GenerateTopikSkripsi
    Public Property ParentDashboard As DashboardUser

    Private ReadOnly navyColor As Color = Color.FromArgb(12, 45, 72)
    Private ReadOnly lightNavyColor As Color = Color.FromArgb(28, 68, 105)
    Private ReadOnly whiteColor As Color = Color.White
    Private ReadOnly lightGreyHoverColor As Color = Color.FromArgb(236, 240, 241)
    Private ReadOnly greenColor As Color = Color.FromArgb(39, 174, 96)
    Private ReadOnly lightGreenColor As Color = Color.FromArgb(46, 204, 113)
    Private ReadOnly blueColor As Color = Color.FromArgb(41, 128, 185)
    Private ReadOnly lightBlueColor As Color = Color.FromArgb(52, 152, 219)
    
    ' Track previously shown topics to avoid duplicates
    Private previouslyShownTopicIds As New List(Of Integer)
    
    ' Store current topics for PDF export
    Private currentTopics As New List(Of TopicResult)
    
    ' For printing
    Private WithEvents printDocument As New PrintDocument()
    Private printPreviewDialog As New PrintPreviewDialog()
    Private printContent As String = ""
    Private printLines As New List(Of String)
    Private printPageNumber As Integer = 0
    Private printLineIndex As Integer = 0

    Private Sub GenerateTopikSkripsi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsLoggedIn() Then
            MessageBox.Show("Silakan login terlebih dahulu!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If
        If String.IsNullOrEmpty(LoggedMinatBakat) Then
            MessageBox.Show("Anda harus mengikuti Tes Minat Bakat terlebih dahulu untuk mendapatkan rekomendasi.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            Return
        End If

        AddHandler ButtonBack.Click, AddressOf BtnBack_Click
        AddHandler ButtonBack.MouseEnter, AddressOf ButtonBack_MouseEnter
        AddHandler ButtonBack.MouseLeave, AddressOf ButtonBack_MouseLeave
        
        AddHandler ButtonGenerateUlang.Click, AddressOf BtnGenerateUlang_Click
        AddHandler ButtonGenerateUlang.MouseEnter, AddressOf ButtonGenerateUlang_MouseEnter
        AddHandler ButtonGenerateUlang.MouseLeave, AddressOf ButtonGenerateUlang_MouseLeave

        AddHandler ButtonPrintPreview.Click, AddressOf BtnPrintPreview_Click
        AddHandler ButtonPrintPreview.MouseEnter, AddressOf ButtonPrintPreview_MouseEnter
        AddHandler ButtonPrintPreview.MouseLeave, AddressOf ButtonPrintPreview_MouseLeave

        AddHandler ButtonPrint.Click, AddressOf BtnPrint_Click
        AddHandler ButtonPrint.MouseEnter, AddressOf ButtonPrint_MouseEnter
        AddHandler ButtonPrint.MouseLeave, AddressOf ButtonPrint_MouseLeave

        LoadAndDisplayTopics()
    End Sub

    Private Sub LoadAndDisplayTopics()
        Cursor = Cursors.WaitCursor
        Try
            Label1.Text = "Rekomendasi untuk " & LoggedFullName
            Label2.Text = "Berdasarkan minat bakat Anda di bidang: " & LoggedMinatBakat

            Dim topics = GenerateTopics()
            currentTopics = topics ' Store for PDF export

            TopicsListBox.Items.Clear()
            If topics.Count > 0 Then
                ' Store the topic IDs for tracking
                For Each topic In topics
                    If Not previouslyShownTopicIds.Contains(topic.TopicId) Then
                        previouslyShownTopicIds.Add(topic.TopicId)
                    End If
                Next
                
                For i As Integer = 0 To topics.Count - 1
                    TopicsListBox.Items.Add((i + 1) & ". " & topics(i).TopicTitle & " (" & topics(i).Category & ")")
                Next
            Else
                TopicsListBox.Items.Add("Tidak ada topik yang ditemukan untuk profil Anda saat ini.")
                TopicsListBox.Items.Add("Mohon hubungi admin untuk menambahkan lebih banyak topik.")
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat men-generate topik: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Function GenerateTopics() As List(Of TopicResult)
        Dim topicResults As New List(Of TopicResult)
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            
            ' Build query with exclusion of previously shown topics
            Dim query As String = "SELECT t.topic_id, t.judul, t.deskripsi, c.nama as category_name " &
                                 "FROM topics t INNER JOIN categories c ON t.kategori_id = c.category_id " &
                                 "WHERE t.is_active = 1 AND c.nama = @minatBakat "
            
            ' Exclude previously shown topics if any
            If previouslyShownTopicIds.Count > 0 Then
                query &= "AND t.topic_id NOT IN (" & String.Join(",", previouslyShownTopicIds) & ") "
            End If
            
            query &= "ORDER BY t.popularity DESC, RAND() LIMIT 10"

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@minatBakat", LoggedMinatBakat)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        topicResults.Add(New TopicResult() With {
                            .TopicId = Convert.ToInt32(reader("topic_id")),
                            .TopicTitle = reader("judul").ToString(),
                            .Description = If(IsDBNull(reader("deskripsi")), "", reader("deskripsi").ToString()),
                            .Category = reader("category_name").ToString()
                        })
                    End While
                End Using
            End Using
            
            ' Update popularity for shown topics
            For Each topic In topicResults
                Dim updateQuery As String = "UPDATE topics SET popularity = popularity + 1 WHERE topic_id = @topicId"
                Using updateCmd As New MySqlCommand(updateQuery, conn)
                    updateCmd.Parameters.AddWithValue("@topicId", topic.TopicId)
                    updateCmd.ExecuteNonQuery()
                End Using
            Next
        End Using
        Return topicResults
    End Function

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
    
    Private Sub BtnGenerateUlang_Click(sender As Object, e As EventArgs)
        ' Check if there are still topics available
        Dim availableTopicsCount As Integer = 0
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim countQuery As String = "SELECT COUNT(*) FROM topics t " &
                                          "INNER JOIN categories c ON t.kategori_id = c.category_id " &
                                          "WHERE t.is_active = 1 AND c.nama = @minatBakat"
                
                If previouslyShownTopicIds.Count > 0 Then
                    countQuery &= " AND t.topic_id NOT IN (" & String.Join(",", previouslyShownTopicIds) & ")"
                End If
                
                Using cmd As New MySqlCommand(countQuery, conn)
                    cmd.Parameters.AddWithValue("@minatBakat", LoggedMinatBakat)
                    availableTopicsCount = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
            
            If availableTopicsCount = 0 Then
                Dim result = MessageBox.Show("Semua topik untuk kategori Anda sudah ditampilkan." & vbCrLf & vbCrLf & 
                                            "Apakah Anda ingin mereset dan melihat topik dari awal lagi?", 
                                            "Informasi", 
                                            MessageBoxButtons.YesNo, 
                                            MessageBoxIcon.Information)
                If result = DialogResult.Yes Then
                    previouslyShownTopicIds.Clear()
                    LoadAndDisplayTopics()
                End If
            Else
                LoadAndDisplayTopics()
            End If
            
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GenerateTopikSkripsi_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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
    
    Private Sub ButtonGenerateUlang_MouseEnter(sender As Object, e As EventArgs)
        ButtonGenerateUlang.BackColor = lightNavyColor
    End Sub
    
    Private Sub ButtonGenerateUlang_MouseLeave(sender As Object, e As EventArgs)
        ButtonGenerateUlang.BackColor = navyColor
    End Sub

    Private Sub ButtonPrintPreview_MouseEnter(sender As Object, e As EventArgs)
        ButtonPrintPreview.BackColor = lightBlueColor
    End Sub

    Private Sub ButtonPrintPreview_MouseLeave(sender As Object, e As EventArgs)
        ButtonPrintPreview.BackColor = blueColor
    End Sub

    Private Sub ButtonPrint_MouseEnter(sender As Object, e As EventArgs)
        ButtonPrint.BackColor = lightGreenColor
    End Sub

    Private Sub ButtonPrint_MouseLeave(sender As Object, e As EventArgs)
        ButtonPrint.BackColor = greenColor
    End Sub

    Private Sub BtnPrintPreview_Click(sender As Object, e As EventArgs)
        If currentTopics Is Nothing OrElse currentTopics.Count = 0 Then
            MessageBox.Show("Belum ada topik yang di-generate. Silakan generate topik terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

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
        If currentTopics Is Nothing OrElse currentTopics.Count = 0 Then
            MessageBox.Show("Belum ada topik yang di-generate. Silakan generate topik terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim saveDialog As New SaveFileDialog() With {
                .Filter = "PDF Files (*.pdf)|*.pdf",
                .DefaultExt = "pdf",
                .FileName = "Rekomendasi_Topik_Skripsi_" & LoggedFullName.Replace(" ", "_") & "_" & DateTime.Now.ToString("yyyyMMdd") & ".pdf"
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
        html.AppendLine(".topic { background-color: #f8f9fa; padding: 15px; margin: 15px 0; border-left: 5px solid #3498db; page-break-inside: avoid; }")
        html.AppendLine(".topic-number { font-weight: bold; color: #0c2d48; font-size: 1.1em; }")
        html.AppendLine(".topic-title { font-weight: bold; color: #2c3e50; margin: 8px 0; }")
        html.AppendLine(".topic-category { color: #27ae60; font-style: italic; font-size: 0.9em; }")
        html.AppendLine(".topic-desc { color: #7f8c8d; margin-top: 5px; font-size: 0.95em; }")
        html.AppendLine(".footer { margin-top: 40px; padding-top: 20px; border-top: 1px solid #bdc3c7; text-align: center; color: #95a5a6; font-size: 0.85em; }")
        html.AppendLine(".highlight { background-color: #fff3cd; padding: 15px; border-left: 4px solid #ffc107; margin: 20px 0; }")
        html.AppendLine("</style></head><body>")

        ' Header
        html.AppendLine("<div class='header'>")
        html.AppendLine("<h1 style='margin:0; border:none; padding:0; color: white;'>REKOMENDASI TOPIK SKRIPSI</h1>")
        html.AppendLine("<p style='margin:5px 0 0 0;'>Sistem Informasi Topik Skripsi (SITOPSI)</p>")
        html.AppendLine("</div>")

        ' User info
        html.AppendLine("<div class='info'>")
        html.AppendLine($"<strong>Nama:</strong> {LoggedFullName}<br>")
        html.AppendLine($"<strong>Minat Bakat:</strong> {LoggedMinatBakat}<br>")
        html.AppendLine($"<strong>Tanggal Generate:</strong> {DateTime.Now:dd MMMM yyyy}")
        html.AppendLine("</div>")

        ' Introduction
        html.AppendLine("<div class='highlight'>")
        html.AppendLine($"<strong>Catatan:</strong> Rekomendasi ini berdasarkan hasil tes minat bakat Anda di bidang <strong>{LoggedMinatBakat}</strong>. ")
        html.AppendLine("Total topik yang direkomendasikan: <strong>" & currentTopics.Count & " topik</strong>")
        html.AppendLine("</div>")

        ' Topics list
        html.AppendLine("<h2>Daftar Rekomendasi Topik</h2>")
        For i As Integer = 0 To currentTopics.Count - 1
            Dim topic = currentTopics(i)
            html.AppendLine("<div class='topic'>")
            html.AppendLine($"<div class='topic-number'>Topik #{i + 1}</div>")
            html.AppendLine($"<div class='topic-title'>{topic.TopicTitle}</div>")
            html.AppendLine($"<div class='topic-category'>Kategori: {topic.Category}</div>")
            If Not String.IsNullOrEmpty(topic.Description) Then
                html.AppendLine($"<div class='topic-desc'>Deskripsi: {topic.Description}</div>")
            End If
            html.AppendLine("</div>")
        Next

        ' Footer
        html.AppendLine("<div class='footer'>")
        html.AppendLine($"<p>Dokumen ini digenerate otomatis oleh SITOPSI pada {DateTime.Now:dd MMMM yyyy HH:mm:ss}</p>")
        html.AppendLine("<p>© 2025 SITOPSI - Sistem Informasi Topik Skripsi</p>")
        html.AppendLine("<p><em>Rekomendasi ini bersifat saran dan dapat disesuaikan dengan kebutuhan akademik Anda</em></p>")
        html.AppendLine("</div>")

        html.AppendLine("</body></html>")

        ' Convert HTML to PDF
        ConvertHtmlToPdf(html.ToString(), filePath)
    End Sub

    Private Sub ConvertHtmlToPdf(htmlContent As String, pdfPath As String)
        ' Save HTML to temp file
        Dim tempHtmlPath As String = Path.Combine(Path.GetTempPath(), "sitopsi_topics_" & Guid.NewGuid().ToString() & ".html")
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
        content.AppendLine("   REKOMENDASI TOPIK SKRIPSI")
        content.AppendLine("   SITOPSI - Sistem Informasi Topik Skripsi")
        content.AppendLine("========================================")
        content.AppendLine()
        content.AppendLine($"Nama           : {LoggedFullName}")
        content.AppendLine($"Minat Bakat    : {LoggedMinatBakat}")
        content.AppendLine($"Tanggal        : {DateTime.Now:dd MMMM yyyy HH:mm:ss}")
        content.AppendLine()
        content.AppendLine("========================================")
        content.AppendLine($"   DAFTAR REKOMENDASI ({currentTopics.Count} Topik)")
        content.AppendLine("========================================")
        content.AppendLine()

        ' Topics list
        For i As Integer = 0 To currentTopics.Count - 1
            Dim topic = currentTopics(i)
            content.AppendLine($"Topik #{i + 1}")
            content.AppendLine($"Judul      : {topic.TopicTitle}")
            content.AppendLine($"Kategori   : {topic.Category}")
            If Not String.IsNullOrEmpty(topic.Description) Then
                content.AppendLine($"Deskripsi  : {topic.Description}")
            End If
            content.AppendLine()
            content.AppendLine("----------------------------------------")
            content.AppendLine()
        Next
        content.AppendLine("========================================")
        content.AppendLine()
        content.AppendLine("Catatan:")
        content.AppendLine("Rekomendasi ini berdasarkan hasil tes")
        content.AppendLine("minat bakat Anda dan dapat disesuaikan")
        content.AppendLine("dengan kebutuhan akademik.")
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

    Public Class TopicResult
        Public Property TopicId As Integer
        Public Property TopicTitle As String
        Public Property Description As String
        Public Property Category As String
    End Class
End Class