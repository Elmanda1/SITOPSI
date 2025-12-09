Imports System
Imports System.Windows.Forms

Friend Module Program
    ''' <summary>
    ''' The main entry point for the application.
    ''' </summary>
    <STAThread()>
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.SetHighDpiMode(HighDpiMode.SystemAware)
        
        ' Run application with LandingPage as startup form
        Application.Run(New LandingPage())
    End Sub
End Module
