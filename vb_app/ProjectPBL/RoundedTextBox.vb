Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class RoundedTextBox
    Inherits UserControl

    Private WithEvents txtInput As TextBox
    Private WithEvents toggleButton As CheckBox ' The eye button
    Private _cornerRadius As Integer = 10
    Private _borderColor As Color = Color.Gray
    Private _borderFocusColor As Color = Color.FromArgb(242, 193, 59) ' Gold
    Private _showPasswordToggle As Boolean = False
    Private defaultPasswordChar As Char = "●"c

    Public Sub New()
        ' InitializeComponent
        txtInput = New TextBox()
        toggleButton = New CheckBox()
        Me.SuspendLayout()
        '
        ' txtInput
        '
        txtInput.BorderStyle = BorderStyle.None
        txtInput.Dock = DockStyle.Fill
        txtInput.Multiline = False
        txtInput.Font = New Font("Segoe UI", 12.0F)
        txtInput.UseSystemPasswordChar = False ' Force this to false so PasswordChar takes precedence
        AddHandler txtInput.Enter, AddressOf TxtInput_Enter
        AddHandler txtInput.Leave, AddressOf TxtInput_Leave
        AddHandler txtInput.TextChanged, AddressOf TxtInput_TextChanged
        AddHandler txtInput.KeyPress, AddressOf TxtInput_KeyPress
        '
        ' toggleButton
        '
        toggleButton.Appearance = Appearance.Button
        toggleButton.FlatAppearance.BorderSize = 0
        toggleButton.FlatStyle = FlatStyle.Flat
        toggleButton.Size = New Size(30, 30)
        toggleButton.Text = "👁️"
        toggleButton.Dock = DockStyle.Right
        toggleButton.Cursor = Cursors.Hand
        toggleButton.Visible = _showPasswordToggle
        AddHandler toggleButton.CheckedChanged, AddressOf ToggleButton_CheckedChanged
        '
        ' RoundedTextBox
        '
        Me.Controls.Add(txtInput)
        Me.Controls.Add(toggleButton)
        Me.DoubleBuffered = True
        ' Sync child control colors with parent
        AddHandler Me.BackColorChanged, Sub(s, ev)
                                            txtInput.BackColor = Me.BackColor
                                            toggleButton.BackColor = Me.BackColor
                                        End Sub
        AddHandler Me.ForeColorChanged, Sub(s, ev)
                                            txtInput.ForeColor = Me.ForeColor
                                            toggleButton.ForeColor = Me.ForeColor
                                        End Sub
        ' Initial sync
        txtInput.BackColor = Me.BackColor
        toggleButton.BackColor = Me.BackColor
        txtInput.ForeColor = Me.ForeColor
        toggleButton.ForeColor = Me.ForeColor
        Me.ResumeLayout(False)
    End Sub

#Region "Properties"

    <Category("Appearance")>
    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(value As Color)
            MyBase.ForeColor = value
            txtInput.ForeColor = value
            toggleButton.ForeColor = value
        End Set
    End Property

    <Category("Appearance")>
    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(value As Color)
            MyBase.BackColor = value
            txtInput.BackColor = value
            toggleButton.BackColor = value
        End Set
    End Property

    <Category("Appearance")>
    Public Property ShowPasswordToggle As Boolean
        Get
            Return _showPasswordToggle
        End Get
        Set(value As Boolean)
            _showPasswordToggle = value
            toggleButton.Visible = value
        End Set
    End Property

    <Category("Appearance")>
    Public Property CornerRadius As Integer
        Get
            Return _cornerRadius
        End Get
        Set(value As Integer)
            _cornerRadius = value
            Me.Invalidate()
        End Set
    End Property

    <Category("Appearance")>
    Public Overrides Property Text As String
        Get
            Return txtInput.Text
        End Get
        Set(value As String)
            txtInput.Text = value
        End Set
    End Property

    <Category("Appearance")>
    Public Property PasswordChar As Char
        Get
            Return txtInput.PasswordChar
        End Get
        Set(value As Char)
            txtInput.PasswordChar = value
            If value <> ChrW(0) Then
                defaultPasswordChar = value
            End If
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub Clear()
        txtInput.Text = String.Empty
    End Sub

    Public Shadows Function Focus() As Boolean
        Return txtInput.Focus()
    End Function
#End Region

#Region "Events"
    Private Sub ToggleButton_CheckedChanged(sender As Object, e As EventArgs)
        If toggleButton.Checked Then
            ' Show password
            txtInput.PasswordChar = ChrW(0)
            toggleButton.Text = "👁️" ' Keep eye open
        Else
            ' Hide password
            txtInput.PasswordChar = defaultPasswordChar
            toggleButton.Text = "👁️" ' Keep eye open
        End If
    End Sub

    Private Sub TxtInput_Enter(sender As Object, e As EventArgs)
        _borderColor = _borderFocusColor
        Me.Invalidate()
        MyBase.OnEnter(e)
    End Sub

    Private Sub TxtInput_Leave(sender As Object, e As EventArgs)
        _borderColor = Color.Gray
        Me.Invalidate()
        MyBase.OnLeave(e)
    End Sub

    Private Sub TxtInput_TextChanged(sender As Object, e As EventArgs)
        MyBase.OnTextChanged(e)
    End Sub

    Private Sub TxtInput_KeyPress(sender As Object, e As KeyPressEventArgs)
        MyBase.OnKeyPress(e)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        Using path As New GraphicsPath()
            Dim rect As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
            path.AddArc(rect.X, rect.Y, _cornerRadius * 2, _cornerRadius * 2, 180, 90)
            path.AddArc(rect.Right - (_cornerRadius * 2), rect.Y, _cornerRadius * 2, _cornerRadius * 2, 270, 90)
            path.AddArc(rect.Right - (_cornerRadius * 2), rect.Bottom - (_cornerRadius * 2), _cornerRadius * 2, _cornerRadius * 2, 0, 90)
            path.AddArc(rect.X, rect.Bottom - (_cornerRadius * 2), _cornerRadius * 2, _cornerRadius * 2, 90, 90)
            path.CloseAllFigures()

            Me.Region = New Region(path)

            Using borderPen As New Pen(_borderColor, 1.5F)
                e.Graphics.DrawPath(borderPen, path)
            End Using
        End Using
    End Sub
#End Region

End Class