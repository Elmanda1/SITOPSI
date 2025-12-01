Imports MySql.Data.MySqlClient

Public Module GlobalModule
    ' ========== DATABASE CONNECTION ==========
    Public ConnectionString As String = "Server=localhost;Database=dbsitopsi;User ID=root;Password=;Port=3306;CharSet=utf8mb4;"

    ' ========== SESSION VARIABLES ==========
    Public LoggedUserId As Integer = 0
    Public LoggedUsername As String = ""
    Public LoggedFullName As String = ""
    Public LoggedEmail As String = ""
    Public LoggedRoleId As Integer = 0
    Public LoggedRoleName As String = ""
    Public LoggedNoHP As String = ""
    Public LoggedMinatBakat As String = ""

    ' ========== HELPER FUNCTIONS ==========
    Public Function IsLoggedIn() As Boolean
        Return LoggedUserId > 0
    End Function

    Public Function IsAdmin() As Boolean
        Return LoggedRoleId = 1
    End Function

    Public Function IsMahasiswa() As Boolean
        Return LoggedRoleId = 2
    End Function

    Public Sub ClearSession()
        LoggedUserId = 0
        LoggedUsername = ""
        LoggedFullName = ""
        LoggedEmail = ""
        LoggedRoleId = 0
        LoggedRoleName = ""
        LoggedNoHP = ""
        LoggedMinatBakat = ""
    End Sub
End Module
