Imports System
Imports System.Diagnostics
Imports System.Text
Imports System.Text.Json
Imports System.Collections.Generic

Public Module PythonBridge

    ' Configure these paths if Python is not on PATH or script moved
    Private ReadOnly PythonExe As String = "python"
    Private ReadOnly ScriptPath As String = "c:\\Users\\lunox\\Documents\\SITOPSI\\python_experts\\main.py"

    ''' <summary>
    ''' Call the Python AI CLI with a list of facts and return raw JSON output.
    ''' Throws an exception on process failure.
    ''' </summary>
    Public Function CallPythonInference(facts As List(Of String)) As String
        Dim inputObj = New Dictionary(Of String, Object) From {{"facts", facts}}
        Dim inputJson As String = JsonSerializer.Serialize(inputObj)

        Dim psi As New ProcessStartInfo()
        psi.FileName = PythonExe
        psi.Arguments = $"""{ScriptPath}"""
        psi.RedirectStandardInput = True
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True
        psi.UseShellExecute = False
        psi.CreateNoWindow = True

        Using p As Process = Process.Start(psi)
            If p Is Nothing Then
                Throw New Exception("Failed to start Python process.")
            End If

            p.StandardInput.Write(inputJson)
            p.StandardInput.Close()

            Dim output As String = p.StandardOutput.ReadToEnd()
            Dim err As String = p.StandardError.ReadToEnd()
            p.WaitForExit()

            If p.ExitCode <> 0 Then
                Throw New Exception($"Python process failed (exit {p.ExitCode}): {err}")
            End If

            Return output
        End Using
    End Function

    ''' <summary>
    ''' Try to parse the JSON result and return a JsonElement. Caller may inspect it.
    ''' </summary>
    Public Function TryParseResult(json As String, ByRef parsed As JsonElement) As Boolean
        Try
            parsed = JsonSerializer.Deserialize(Of JsonElement)(json)
            Return True
        Catch ex As Exception
            parsed = Nothing
            Return False
        End Try
    End Function

End Module
