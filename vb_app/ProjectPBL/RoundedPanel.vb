Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.ComponentModel

Public Class RoundedPanel
    Inherits Panel

    ' --- Properties ---
    <Category("Appearance")>
    Public Property CornerRadius As Integer = 24

    <Category("Appearance")>
    Public Property GradientColor1 As Color = Color.White

    <Category("Appearance")>
    Public Property GradientColor2 As Color = Color.LightGray

    <Category("Appearance")>
    Public Property GradientAngle As Single = 90.0F

    <Category("Appearance")>
    Public Property ShadowColor As Color = Color.FromArgb(100, 0, 0, 0)

    <Category("Appearance")>
    Public Property ShadowOffset As Integer = 5

    Public Sub New()
        Me.DoubleBuffered = True
        ' The panel will draw its own background, so tell Windows not to.
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.Padding = New Padding(ShadowOffset)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        Dim mainRect As New Rectangle(0, 0, Me.Width - ShadowOffset - 1, Me.Height - ShadowOffset - 1)
        Dim shadowRect As New Rectangle(ShadowOffset, ShadowOffset, Me.Width - ShadowOffset - 1, Me.Height - ShadowOffset - 1)

        If CornerRadius > 0 Then
            ' --- Draw with Rounded Corners ---
            Using shadowPath As New GraphicsPath()
                shadowPath.AddArc(shadowRect.X, shadowRect.Y, CornerRadius * 2, CornerRadius * 2, 180, 90)
                shadowPath.AddArc(shadowRect.Right - (CornerRadius * 2), shadowRect.Y, CornerRadius * 2, CornerRadius * 2, 270, 90)
                shadowPath.AddArc(shadowRect.Right - (CornerRadius * 2), shadowRect.Bottom - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 0, 90)
                shadowPath.AddArc(shadowRect.X, shadowRect.Bottom - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 90, 90)
                shadowPath.CloseAllFigures()
                If ShadowOffset > 0 Then
                    Using shadowBrush As New SolidBrush(ShadowColor)
                        e.Graphics.FillPath(shadowBrush, shadowPath)
                    End Using
                End If
            End Using

            Using mainPath As New GraphicsPath()
                mainPath.AddArc(mainRect.X, mainRect.Y, CornerRadius * 2, CornerRadius * 2, 180, 90)
                mainPath.AddArc(mainRect.Right - (CornerRadius * 2), mainRect.Y, CornerRadius * 2, CornerRadius * 2, 270, 90)
                mainPath.AddArc(mainRect.Right - (CornerRadius * 2), mainRect.Bottom - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 0, 90)
                mainPath.AddArc(mainRect.X, mainRect.Bottom - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 90, 90)
                mainPath.CloseAllFigures()

                Using brush As New LinearGradientBrush(mainRect, GradientColor1, GradientColor2, GradientAngle)
                    e.Graphics.FillPath(brush, mainPath)
                End Using
            End Using
        Else
            ' --- Draw with Sharp Corners ---
            If ShadowOffset > 0 Then
                Using shadowBrush As New SolidBrush(ShadowColor)
                    e.Graphics.FillRectangle(shadowBrush, shadowRect)
                End Using
            End If
            
            Using brush As New LinearGradientBrush(mainRect, GradientColor1, GradientColor2, GradientAngle)
                e.Graphics.FillRectangle(brush, mainRect)
            End Using
        End If
    End Sub

End Class
