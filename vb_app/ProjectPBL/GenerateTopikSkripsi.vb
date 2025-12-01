Imports MySql.Data.MySqlClient

Public Class GenerateTopikSkripsi
    ' Reference ke dashboard untuk kembali
    Public Property ParentDashboard As DashboardUser

    Private Sub GenerateTopikSkripsi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Cek apakah user sudah login
        If Not IsLoggedIn() Then
            MessageBox.Show("Silakan login terlebih dahulu!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Dim loginForm As New LoginForm()
            loginForm.Show()
            Me.Close()
            Return
        End If

        ' Center form dan set background
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(236, 240, 241)

        ' Cek apakah user sudah punya minat bakat
        If String.IsNullOrEmpty(LoggedMinatBakat) Then
            MessageBox.Show("Anda harus mengikuti Tes Minat Bakat terlebih dahulu!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            Return
        End If

        SetupUI()
    End Sub

    Private Sub SetupUI()
        Me.Controls.Clear()

        ' Header Panel
        Dim headerPanel As New Panel() With {
            .Size = New Size(Me.ClientSize.Width, 120),
            .BackColor = Color.FromArgb(41, 128, 185),
            .Dock = DockStyle.Top
        }
        Me.Controls.Add(headerPanel)

        Dim lblTitle As New Label() With {
            .Text = "🎓 Generate Topik Skripsi",
            .Font = New Font("Segoe UI", 20, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .Location = New Point(30, 20)
        }
        headerPanel.Controls.Add(lblTitle)

        Dim lblSubtitle As New Label() With {
            .Text = $"Berdasarkan minat Anda di bidang {LoggedMinatBakat}",
            .Font = New Font("Segoe UI", 11, FontStyle.Regular),
            .ForeColor = Color.FromArgb(236, 240, 241),
            .AutoSize = True,
            .Location = New Point(30, 65)
        }
        headerPanel.Controls.Add(lblSubtitle)

        ' Content Panel
        Dim contentPanel As New Panel() With {
            .Location = New Point(0, 120),
            .Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height - 120),
            .BackColor = Color.FromArgb(236, 240, 241),
            .AutoScroll = True
        }
        Me.Controls.Add(contentPanel)

        ' Generate Button
        Dim btnGenerate As New Button() With {
            .Text = "🚀 Generate Topik Skripsi",
            .Font = New Font("Segoe UI", 13, FontStyle.Bold),
            .Size = New Size(300, 55),
            .Location = New Point((Me.ClientSize.Width - 300) \ 2, 100),
            .BackColor = Color.FromArgb(46, 204, 113),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand
        }
        btnGenerate.FlatAppearance.BorderSize = 0
        AddHandler btnGenerate.Click, AddressOf GenerateTopics
        contentPanel.Controls.Add(btnGenerate)
    End Sub

    Private Sub GenerateTopics(sender As Object, e As EventArgs)
        Try
            Cursor = Cursors.WaitCursor
            Dim topicResults As New List(Of HasilGenerateTopik.TopicResult)

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT t.id, t.judul, t.deskripsi, c.nama_category " &
                                     "FROM thesis_topics t " &
                                     "INNER JOIN categories c ON t.category_id = c.id " &
                                     "WHERE c.nama_category = @minatBakat " &
                                     "ORDER BY RAND() LIMIT 5"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@minatBakat", LoggedMinatBakat)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Dim rank As Integer = 1
                        While reader.Read()
                            topicResults.Add(New HasilGenerateTopik.TopicResult() With {
                                .TopicId = Convert.ToInt32(reader("id")),
                                .TopicTitle = reader("judul").ToString(),
                                .Description = reader("deskripsi").ToString(),
                                .Category = reader("nama_category").ToString(),
                                .Relevance = 1.0D - (rank * 0.05D),
                                .Rank = rank
                            })
                            rank += 1
                        End While
                    End Using
                End Using
            End Using

            If topicResults.Count = 0 Then
                MessageBox.Show("Tidak ada topik yang ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim hasilForm As New HasilGenerateTopik()
            hasilForm.ParentDashboard = Me.ParentDashboard
            hasilForm.GeneratedTopics = topicResults
            hasilForm.Show()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateTopikSkripsi_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ParentDashboard IsNot Nothing Then
            ParentDashboard.Show()
        End If
    End Sub
End Class