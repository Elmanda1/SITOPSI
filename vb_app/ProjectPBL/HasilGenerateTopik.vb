Imports MySql.Data.MySqlClient

Public Class HasilGenerateTopik
    Private topicResults As New List(Of TopicResult)

    ' Reference ke dashboard untuk kembali
    Public Property ParentDashboard As DashboardUser

    ' Class untuk menyimpan hasil topik
    Public Class TopicResult
        Public Property TopicId As Integer
        Public Property TopicTitle As String
        Public Property Description As String
        Public Property Category As String
        Public Property Relevance As Decimal
        Public Property Rank As Integer
    End Class

    ' Property untuk menerima data topik dari form sebelumnya
    Public Property GeneratedTopics As List(Of TopicResult)

    Private Sub HasilGenerateTopik_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Me.BackColor = Color.FromArgb(240, 244, 248)

        ' Load hasil topik
        If GeneratedTopics IsNot Nothing AndAlso GeneratedTopics.Count > 0 Then
            topicResults = GeneratedTopics
        Else
            LoadLatestGeneratedTopics()
        End If

        ' Tampilkan hasil
        DisplayResults()
    End Sub

    Private Sub LoadLatestGeneratedTopics()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' Query untuk mendapatkan topik yang di-generate untuk user ini
                ' (Jika ada tabel history generate topik)
                Dim query As String = "SELECT t.id, t.judul, t.deskripsi, c.nama_category, 1.0 as relevance " &
                                     "FROM thesis_topics t " &
                                     "INNER JOIN categories c ON t.category_id = c.id " &
                                     "WHERE c.nama_category = @minatBakat " &
                                     "ORDER BY RAND() " &
                                     "LIMIT 5"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@minatBakat", LoggedMinatBakat)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Dim rank As Integer = 1

                        While reader.Read()
                            Dim topicResult As New TopicResult() With {
                                .TopicId = Convert.ToInt32(reader("id")),
                                .TopicTitle = reader("judul").ToString(),
                                .Description = reader("deskripsi").ToString(),
                                .Category = reader("nama_category").ToString(),
                                .Relevance = Convert.ToDecimal(reader("relevance")),
                                .Rank = rank
                            }
                            topicResults.Add(topicResult)
                            rank += 1
                        End While
                    End Using
                End Using

                If topicResults.Count = 0 Then
                    MessageBox.Show("Tidak ada topik yang ditemukan untuk bidang minat Anda.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show($"Error loading topics: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DisplayResults()
        ' Clear existing controls
        Me.Controls.Clear()

        ' Header Panel
        Dim headerPanel As New Panel() With {
            .Location = New Point(0, 0),
            .Size = New Size(Me.ClientSize.Width, 100),
            .BackColor = Color.FromArgb(52, 73, 94),
            .Dock = DockStyle.Top
        }
        Me.Controls.Add(headerPanel)

        ' Title Label
        Dim lblTitle As New Label() With {
            .Text = $"Hasil Generate Topik Skripsi",
            .Font = New Font("Segoe UI", 18, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(30, 20)
        }
        headerPanel.Controls.Add(lblTitle)

        ' Subtitle Label
        Dim lblSubtitle As New Label() With {
            .Text = $"Halo {LoggedFullName}, berikut adalah rekomendasi topik skripsi berdasarkan minat Anda di bidang {LoggedMinatBakat}",
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .ForeColor = Color.FromArgb(236, 240, 241),
            .AutoSize = False,
            .Size = New Size(Me.ClientSize.Width - 60, 40),
            .Location = New Point(30, 55)
        }
        headerPanel.Controls.Add(lblSubtitle)

        ' Main Content Panel with scroll
        Dim contentPanel As New Panel() With {
            .Location = New Point(0, 100),
            .Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height - 100),
            .AutoScroll = True,
            .BackColor = Color.FromArgb(240, 244, 248)
        }
        Me.Controls.Add(contentPanel)

        Dim yPosition As Integer = 20

        ' Display topics
        For Each topic In topicResults.OrderBy(Function(t) t.Rank)
            ' Topic Card Panel
            Dim cardPanel As New Panel() With {
                .Location = New Point(30, yPosition),
                .Size = New Size(Me.ClientSize.Width - 80, 180),
                .BackColor = Color.White,
                .BorderStyle = BorderStyle.FixedSingle
            }
            contentPanel.Controls.Add(cardPanel)

            ' Rank Badge
            Dim lblRank As New Label() With {
                .Text = $"#{topic.Rank}",
                .Font = New Font("Segoe UI", 14, FontStyle.Bold),
                .ForeColor = Color.White,
                .BackColor = Color.FromArgb(52, 152, 219),
                .TextAlign = ContentAlignment.MiddleCenter,
                .Size = New Size(50, 50),
                .Location = New Point(10, 10)
            }
            cardPanel.Controls.Add(lblRank)

            ' Topic Title
            Dim lblTopicTitle As New Label() With {
                .Text = topic.TopicTitle,
                .Font = New Font("Segoe UI", 13, FontStyle.Bold),
                .ForeColor = Color.FromArgb(44, 62, 80),
                .AutoSize = False,
                .Size = New Size(cardPanel.Width - 90, 50),
                .Location = New Point(70, 10)
            }
            cardPanel.Controls.Add(lblTopicTitle)

            ' Category Badge
            Dim lblCategory As New Label() With {
                .Text = $"?? {topic.Category}",
                .Font = New Font("Segoe UI", 9, FontStyle.Regular),
                .ForeColor = Color.FromArgb(127, 140, 141),
                .AutoSize = True,
                .Location = New Point(70, 60)
            }
            cardPanel.Controls.Add(lblCategory)

            ' Description
            Dim lblDescription As New Label() With {
                .Text = topic.Description,
                .Font = New Font("Segoe UI", 10, FontStyle.Regular),
                .ForeColor = Color.FromArgb(52, 73, 94),
                .AutoSize = False,
                .Size = New Size(cardPanel.Width - 30, 80),
                .Location = New Point(15, 85)
            }
            cardPanel.Controls.Add(lblDescription)

            ' Relevance Label
            Dim lblRelevance As New Label() With {
                .Text = $"Relevansi: {(topic.Relevance * 100):F0}%",
                .Font = New Font("Segoe UI", 9, FontStyle.Italic),
                .ForeColor = Color.FromArgb(39, 174, 96),
                .AutoSize = True,
                .Location = New Point(cardPanel.Width - 150, cardPanel.Height - 30)
            }
            cardPanel.Controls.Add(lblRelevance)

            yPosition += 200
        Next

        ' Footer dengan tombol
        yPosition += 20

        Dim btnGenerateAgain As New Button() With {
            .Text = "?? Generate Ulang",
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .Size = New Size(180, 45),
            .Location = New Point(30, yPosition),
            .BackColor = Color.FromArgb(41, 128, 185),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnGenerateAgain.FlatAppearance.BorderSize = 0
        AddHandler btnGenerateAgain.Click, AddressOf BtnGenerateAgain_Click
        contentPanel.Controls.Add(btnGenerateAgain)

        Dim btnSaveTopics As New Button() With {
            .Text = "?? Simpan Topik",
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .Size = New Size(180, 45),
            .Location = New Point(220, yPosition),
            .BackColor = Color.FromArgb(39, 174, 96),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnSaveTopics.FlatAppearance.BorderSize = 0
        AddHandler btnSaveTopics.Click, AddressOf BtnSaveTopics_Click
        contentPanel.Controls.Add(btnSaveTopics)

        Dim btnBack As New Button() With {
            .Text = "?? Kembali ke Dashboard",
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .Size = New Size(220, 45),
            .Location = New Point(410, yPosition),
            .BackColor = Color.FromArgb(149, 165, 166),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnBack.FlatAppearance.BorderSize = 0
        AddHandler btnBack.Click, AddressOf BtnBack_Click
        contentPanel.Controls.Add(btnBack)

        ' Log activity
        LogActivity()
    End Sub

    Private Sub BtnGenerateAgain_Click(sender As Object, e As EventArgs)
        ' Kembali ke form generate topik
        Dim generateForm As New GenerateTopikSkripsi()
        generateForm.ParentDashboard = Me.ParentDashboard
        generateForm.Show()
        Me.Close()
    End Sub

    Private Sub BtnSaveTopics_Click(sender As Object, e As EventArgs)
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' Simpan ke tabel generated_topics_history (jika ada)
                For Each topic In topicResults
                    Dim query As String = "INSERT INTO generated_topics_history (user_id, topic_id, generated_at) " &
                                         "VALUES (@userId, @topicId, NOW()) " &
                                         "ON DUPLICATE KEY UPDATE generated_at = NOW()"

                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@userId", LoggedUserId)
                        cmd.Parameters.AddWithValue("@topicId", topic.TopicId)
                        cmd.ExecuteNonQuery()
                    End Using
                Next

                MessageBox.Show("Topik berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Log activity
                Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, 'Simpan Topik', @detail, NOW())"
                Using logCmd As New MySqlCommand(logQuery, conn)
                    logCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    logCmd.Parameters.AddWithValue("@detail", $"User menyimpan {topicResults.Count} topik hasil generate")
                    logCmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show($"Error menyimpan topik: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub LogActivity()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                Dim logQuery As String = "INSERT INTO activity_logs (user_id, aksi, detail, waktu) VALUES (@userId, 'Lihat Hasil Topik', @detail, NOW())"
                Using logCmd As New MySqlCommand(logQuery, conn)
                    logCmd.Parameters.AddWithValue("@userId", LoggedUserId)
                    logCmd.Parameters.AddWithValue("@detail", $"User melihat {topicResults.Count} hasil generate topik")
                    logCmd.ExecuteNonQuery()
                End Using
            End Using
        Catch
            ' Silent fail
        End Try
    End Sub

    Private Sub HasilGenerateTopik_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Kembali ke dashboard user
        If ParentDashboard IsNot Nothing Then
            ParentDashboard.Show()
        Else
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
