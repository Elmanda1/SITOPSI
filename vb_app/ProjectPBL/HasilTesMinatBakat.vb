Imports MySql.Data.MySqlClient

Public Class HasilTesMinatBakat
    Private testId As Integer = 0
    Private categoryResults As New Dictionary(Of Integer, CategoryResult)

    ' Reference ke dashboard untuk kembali
    Public Property ParentDashboard As DashboardUser

    ' Class untuk menyimpan hasil per kategori
    Public Class CategoryResult
        Public Property CategoryId As Integer
        Public Property CategoryName As String
        Public Property TotalCF As Decimal
        Public Property Percentage As Decimal
        Public Property Rank As Integer
    End Class

    Private Sub HasilTesMinatBakat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        ' Load hasil tes terakhir user
        LoadLatestTestResult()
    End Sub

    Private Sub LoadLatestTestResult()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' Ambil test_id terakhir user
                Dim getTestQuery As String = "SELECT id FROM tests WHERE user_id = @userId ORDER BY tanggal DESC LIMIT 1"

                Using cmd As New MySqlCommand(getTestQuery, conn)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing Then
                        MessageBox.Show("Anda belum pernah mengikuti tes!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                        Return
                    End If

                    testId = Convert.ToInt32(result)
                End Using

                ' Hitung CF untuk setiap kategori
                CalculateCertaintyFactors(conn)

                ' Tampilkan hasil
                DisplayResults()
            End Using

        Catch ex As Exception
            MessageBox.Show($"Error loading test results: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    Private Sub CalculateCertaintyFactors(conn As MySqlConnection)
        Try
            ' Query untuk mendapatkan semua jawaban user beserta CF pakar
            Dim query As String = "SELECT ta.question_id, ta.option_id, ta.cf_user_value, " &
                                 "qo.category_id, qo.cf_pakar, c.nama_category " &
                                 "FROM test_answers ta " &
                                 "INNER JOIN question_options qo ON ta.option_id = qo.id " &
                                 "INNER JOIN categories c ON qo.category_id = c.id " &
                                 "WHERE ta.test_id = @testId " &
                                 "ORDER BY ta.question_id"

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@testId", testId)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    ' Dictionary untuk menyimpan CF per kategori per pertanyaan
                    Dim categoryQuestionCF As New Dictionary(Of Integer, Dictionary(Of Integer, Decimal))

                    While reader.Read()
                        Dim categoryId As Integer = Convert.ToInt32(reader("category_id"))
                        Dim questionId As Integer = Convert.ToInt32(reader("question_id"))
                        Dim cfUser As Decimal = Convert.ToDecimal(reader("cf_user_value"))
                        Dim cfPakar As Decimal = Convert.ToDecimal(reader("cf_pakar"))
                        Dim categoryName As String = reader("nama_category").ToString()

                        ' Hitung CF Kombinasi = CF User * CF Pakar
                        Dim cfCombined As Decimal = cfUser * cfPakar

                        ' Inisialisasi dictionary untuk kategori jika belum ada
                        If Not categoryQuestionCF.ContainsKey(categoryId) Then
                            categoryQuestionCF(categoryId) = New Dictionary(Of Integer, Decimal)()

                            ' Inisialisasi category result
                            If Not categoryResults.ContainsKey(categoryId) Then
                                categoryResults(categoryId) = New CategoryResult() With {
                                    .CategoryId = categoryId,
                                    .CategoryName = categoryName,
                                    .TotalCF = 0
                                }
                            End If
                        End If

                        ' Simpan CF untuk pertanyaan ini
                        categoryQuestionCF(categoryId)(questionId) = cfCombined
                    End While

                    reader.Close()

                    ' Hitung CF Total untuk setiap kategori menggunakan rumus CF kombinasi
                    For Each categoryId In categoryQuestionCF.Keys
                        Dim questionCFs = categoryQuestionCF(categoryId).Values.ToList()

                        If questionCFs.Count > 0 Then
                            ' CF Total dimulai dari CF pertama
                            Dim cfTotal As Decimal = questionCFs(0)

                            ' Kombinasikan dengan CF berikutnya
                            For i As Integer = 1 To questionCFs.Count - 1
                                Dim cfOld As Decimal = cfTotal
                                Dim cfNew As Decimal = questionCFs(i)

                                ' Rumus CF Kombinasi:
                                ' CF(CF1, CF2) = CF1 + CF2 * (1 - CF1) jika keduanya positif
                                If cfOld >= 0 AndAlso cfNew >= 0 Then
                                    cfTotal = cfOld + cfNew * (1 - cfOld)
                                ElseIf cfOld < 0 AndAlso cfNew < 0 Then
                                    cfTotal = cfOld + cfNew * (1 + cfOld)
                                Else
                                    cfTotal = (cfOld + cfNew) / (1 - Math.Min(Math.Abs(cfOld), Math.Abs(cfNew)))
                                End If
                            Next

                            categoryResults(categoryId).TotalCF = cfTotal
                        End If
                    Next

                    ' Hitung persentase
                    Dim totalAllCF As Decimal = categoryResults.Values.Sum(Function(x) x.TotalCF)
                    If totalAllCF > 0 Then
                        For Each result In categoryResults.Values
                            result.Percentage = (result.TotalCF / totalAllCF) * 100
                        Next
                    End If

                    ' Beri ranking (1 = tertinggi)
                    Dim sortedResults = categoryResults.Values.OrderByDescending(Function(x) x.TotalCF).ToList()
                    For i As Integer = 0 To sortedResults.Count - 1
                        sortedResults(i).Rank = i + 1
                    Next
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"Error calculating CF: {ex.Message}")
        End Try
    End Sub

    Private Sub DisplayResults()
        ' Update label dengan nama user
        Label1.Text = $"🎯 Hasil Tes Minat Bakat, {LoggedFullName}!"
        Label2.Text = $"Halo {LoggedFullName},"

        ' Ambil kategori dengan CF tertinggi
        Dim topCategory = categoryResults.Values.OrderByDescending(Function(x) x.TotalCF).FirstOrDefault()

        If topCategory IsNot Nothing Then
            Label3.Text = $"Anda memiliki kecondongan lebih di bidang {topCategory.CategoryName} 🌟"
            Label4.Text = $"Berdasarkan jawaban Anda, CF = {topCategory.TotalCF:F4} ({topCategory.Percentage:F2}%)"

            ' Update minat bakat di database user
            UpdateUserMinatBakat(topCategory.CategoryName)

            ' Tampilkan detail semua kategori
            DisplayDetailedResults()
        Else
            Label3.Text = "Tidak ada hasil yang tersedia"
            Label4.Text = ""
        End If
    End Sub

    Private Sub DisplayDetailedResults()
        ' Hapus PictureBox1 dan gunakan area untuk menampilkan hasil detail
        Dim yPosition As Integer = 240

        ' Panel untuk detail hasil
        Dim detailPanel As New Panel() With {
            .Location = New Point(30, yPosition),
            .Size = New Size(Me.ClientSize.Width - 60, 280),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .AutoScroll = True
        }
        Me.Controls.Add(detailPanel)

        ' Header
        Dim lblHeader As New Label() With {
            .Text = "📊 Detail Hasil Per Bidang:",
            .Location = New Point(15, 15),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(52, 73, 94)
        }
        detailPanel.Controls.Add(lblHeader)

        Dim innerYPosition As Integer = 50

        ' Tampilkan semua kategori terurut
        Dim sortedResults = categoryResults.Values.OrderByDescending(Function(x) x.TotalCF).ToList()

        For Each result In sortedResults
            ' Card untuk setiap kategori
            Dim categoryCard As New Panel() With {
                .Location = New Point(15, innerYPosition),
                .Size = New Size(detailPanel.Width - 40, 60),
                .BackColor = GetCategoryColor(result.Rank)
            }
            detailPanel.Controls.Add(categoryCard)

            ' Rank Label
            Dim lblRank As New Label() With {
                .Text = $"#{result.Rank}",
                .Location = New Point(10, 15),
                .Size = New Size(40, 30),
                .Font = New Font("Segoe UI", 14, FontStyle.Bold),
                .ForeColor = Color.White,
                .TextAlign = ContentAlignment.MiddleCenter
            }
            categoryCard.Controls.Add(lblRank)

            ' Category Name
            Dim lblCategoryName As New Label() With {
                .Text = result.CategoryName,
                .Location = New Point(60, 10),
                .AutoSize = True,
                .Font = New Font("Segoe UI", 11, FontStyle.Bold),
                .ForeColor = Color.White
            }
            categoryCard.Controls.Add(lblCategoryName)

            ' CF and Percentage
            Dim lblCFPercentage As New Label() With {
                .Text = $"CF: {result.TotalCF:F4} | {result.Percentage:F2}%",
                .Location = New Point(60, 35),
                .AutoSize = True,
                .Font = New Font("Segoe UI", 9, FontStyle.Regular),
                .ForeColor = Color.FromArgb(236, 240, 241)
            }
            categoryCard.Controls.Add(lblCFPercentage)

            innerYPosition += 70
        Next

        ' Tombol Actions
        yPosition += 300

        Dim btnGenerateTopik As New Button() With {
            .Text = "📝 Generate Topik Skripsi",
            .Location = New Point(30, yPosition),
            .Size = New Size(250, 45),
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .BackColor = Color.FromArgb(41, 128, 185),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnGenerateTopik.FlatAppearance.BorderSize = 0
        AddHandler btnGenerateTopik.Click, AddressOf BtnGenerateTopik_Click
        Me.Controls.Add(btnGenerateTopik)

        Dim btnRetakeTest As New Button() With {
            .Text = "🔄 Tes Ulang",
            .Location = New Point(290, yPosition),
            .Size = New Size(150, 45),
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .BackColor = Color.FromArgb(243, 156, 18),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnRetakeTest.FlatAppearance.BorderSize = 0
        AddHandler btnRetakeTest.Click, AddressOf BtnRetakeTest_Click
        Me.Controls.Add(btnRetakeTest)

        Dim btnBack As New Button() With {
            .Text = "🏠 Kembali ke Dashboard",
            .Location = New Point(450, yPosition),
            .Size = New Size(250, 45),
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .BackColor = Color.FromArgb(149, 165, 166),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnBack.FlatAppearance.BorderSize = 0
        AddHandler btnBack.Click, AddressOf BtnBack_Click
        Me.Controls.Add(btnBack)

        ' Hide PictureBox1 karena tidak digunakan
        PictureBox1.Visible = False
    End Sub

    Private Function GetCategoryColor(rank As Integer) As Color
        Select Case rank
            Case 1
                Return Color.FromArgb(46, 204, 113) ' Green for #1
            Case 2
                Return Color.FromArgb(52, 152, 219) ' Blue for #2
            Case 3
                Return Color.FromArgb(155, 89, 182) ' Purple for #3
            Case Else
                Return Color.FromArgb(149, 165, 166) ' Gray for others
        End Select
    End Function

    Private Sub BtnGenerateTopik_Click(sender As Object, e As EventArgs)
        ' Redirect ke form generate topik
        Dim generateForm As New GenerateTopikSkripsi()
        generateForm.ParentDashboard = Me.ParentDashboard
        generateForm.Show()
        Me.Close()
    End Sub

    Private Sub BtnRetakeTest_Click(sender As Object, e As EventArgs)
        ' Konfirmasi untuk tes ulang
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin mengulang tes? Hasil tes sebelumnya akan tetap tersimpan.", "Konfirmasi Tes Ulang", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Dim tesForm As New TesMinatBakat()
            tesForm.ParentDashboard = Me.ParentDashboard
            tesForm.Show()
            Me.Close()
        End If
    End Sub

    Private Sub UpdateUserMinatBakat(categoryName As String)
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                Dim query As String = "UPDATE users SET minat_bakat = @minatBakat WHERE id = @userId"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@minatBakat", categoryName)
                    cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    cmd.ExecuteNonQuery()
                End Using

                ' Update global session
                LoggedMinatBakat = categoryName

                ' Log activity
                Try
                    Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, 'Hasil Tes', @detail, NOW())"
                    Using logCmd As New MySqlCommand(logQuery, conn)
                        logCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                        logCmd.Parameters.AddWithValue("@detail", $"Hasil tes: {categoryName}")
                        logCmd.ExecuteNonQuery()
                    End Using
                Catch
                    ' Silent fail
                End Try
            End Using

        Catch ex As Exception
            ' Silent fail - not critical
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub HasilTesMinatBakat_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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