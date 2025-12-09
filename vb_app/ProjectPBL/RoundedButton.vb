Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.ComponentModel

Public Class RoundedButton
    Inherits Button

    ' --- Properties ---
    <Category("Appearance")>
    Public Property CornerRadius As Integer = 10
    
    <Category("Appearance")>
    Public Property BorderSize As Single = 0.0F

    <Category("Appearance")>
    Public Property BorderColor As Color = Color.Black

    Public Sub New()
        Me.FlatStyle = FlatStyle.Flat
        Me.FlatAppearance.BorderSize = 0
        Me.DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        MyBase.OnPaint(pevent)

        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        Dim rect As New Rectangle(0, 0, Me.Width, Me.Height)
        Using path As New GraphicsPath()
            path.AddArc(rect.X, rect.Y, CornerRadius * 2, CornerRadius * 2, 180, 90)
            path.AddArc(rect.Right - (CornerRadius * 2), rect.Y, CornerRadius * 2, CornerRadius * 2, 270, 90)
            path.AddArc(rect.Right - (CornerRadius * 2), rect.Bottom - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 0, 90)
            path.AddArc(rect.X, rect.Bottom - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 90, 90)
            path.CloseAllFigures()

            Me.Region = New Region(path)

            ' Fill button
            Using brush As New SolidBrush(Me.BackColor)
                pevent.Graphics.FillPath(brush, path)
            End Using
            
            ' Draw Border
            If BorderSize > 0 Then
                Using pen As New Pen(BorderColor, BorderSize)
                    pen.Alignment = PenAlignment.Inset
                    pevent.Graphics.DrawPath(pen, path)
                End Using
            End If

            ' Draw text
            Dim sf As New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            Using textBrush As New SolidBrush(Me.ForeColor)
                pevent.Graphics.DrawString(Me.Text, Me.Font, textBrush, rect, sf)
            End Using
        End Using
    End Sub
End Class
