Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

    ' **NEW** ApplyApplicationDefaults: Raised when the application queries default values to be set for the application.

    Partial Friend Class MyApplication
        ' NOTE: Application Framework events are disabled when using custom Startup module
        ' These event handlers are no longer used with Startup.vb
        
        ' Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        '     ' Set shutdown mode to only shutdown when last form closes
        '     ' This ensures the application stays alive as long as there's at least one form open
        ' End Sub

        ' Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
        '     ' Log the exception
        '     System.Diagnostics.Debug.WriteLine($"Unhandled Exception: {e.Exception.Message}")
        '     System.Diagnostics.Debug.WriteLine($"StackTrace: {e.Exception.StackTrace}")
        '     
        '     MessageBox.Show($"Terjadi kesalahan yang tidak terduga:{vbCrLf}{vbCrLf}{e.Exception.Message}{vbCrLf}{vbCrLf}Aplikasi akan ditutup.", 
        '                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '     
        '     ' Mark as handled to prevent app crash
        '     e.ExitApplication = False
        ' End Sub
    End Class
End Namespace
