NotInheritable Class GuessButton
    Inherits Control
    Enum MouseState
        None = 0
        Over = 1
        Down = 2
    End Enum
    Private State As MouseState = MouseState.None

#Region "Properties"
    Dim _BGOver As Color
    Property BackColorOver As Color
        Get
            Return _BGOver
        End Get
        Set(value As Color)
            _BGOver = value
            Invalidate()
        End Set
    End Property

    Dim _BGDown As Color
    Property BackColorDown As Color
        Get
            Return _BGDown
        End Get
        Set(value As Color)
            _BGDown = value
            Invalidate()
        End Set
    End Property

    Dim _BorderColor As Color
    Property BorderColor As Color
        Get
            Return _BorderColor
        End Get
        Set(value As Color)
            _BorderColor = value
            Invalidate()
        End Set
    End Property

    Dim _BGC As Color
    Property BackColorNormal As Color
        Get
            Return _BGC
        End Get
        Set(value As Color)
            _BGC = value
        End Set
    End Property

    Private _TextColor As Color = Me.ForeColor
    Public Property TextColor As Color
        Get
            Return _TextColor
        End Get
        Set(value As Color)
            _TextColor = value
        End Set
    End Property

#End Region

    Sub New()
        ForeColor = Color.White
        Font = New Font("Segoe UI", 9)
        SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True

        Dim R, G, B As Integer
        Dim BG As Color = BackColor

        R = BG.R - 20
        G = BG.G - 20
        B = BG.B - 20

        If R < 0 Then R = 0
        If G < 0 Then G = 0
        If B < 0 Then B = 0

        _BGC = Color.FromArgb(R, G, B)

        Size = New Size(105, 27)
    End Sub

    Protected Overrides Sub OnMouseEnter(e As System.EventArgs)
        State = MouseState.Over
        Invalidate()
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As System.EventArgs)
        State = MouseState.None
        Invalidate()
        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnMouseDown(e As System.Windows.Forms.MouseEventArgs)
        State = MouseState.Down
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As System.Windows.Forms.MouseEventArgs)
        State = MouseState.Over
        Invalidate()
        MyBase.OnMouseUp(e)
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        Invalidate()
        MyBase.OnMouseMove(e)
    End Sub

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics

        If Not Enabled Then
            _BorderColor = Color.FromArgb(207, 207, 207)
            _BGC = Color.FromArgb(239, 239, 239)
            _TextColor = Color.FromArgb(207, 207, 207)
        Else
            _BGC = Color.FromArgb(234, 234, 234)
            _BorderColor = Color.FromArgb(172, 172, 172)
            _TextColor = Color.Black
        End If


        Select Case State
            Case MouseState.None
                G.FillRectangle(New SolidBrush(_BGC), New Rectangle(1, 1, Width - 2, Height - 2))
            Case MouseState.Over
                G.FillRectangle(New SolidBrush(BackColorOver), New Rectangle(1, 1, Width - 2, Height - 2))
            Case MouseState.Down
                G.FillRectangle(New SolidBrush(BackColorDown), New Rectangle(1, 1, Width - 2, Height - 2))
        End Select
        G.DrawRectangle(New Pen(_BorderColor), New Rectangle(0, 0, Width - 1, Height - 1))
        Dim SF As New StringFormat : SF.Alignment = StringAlignment.Center : SF.LineAlignment = StringAlignment.Center
        G.DrawString(Text, Font, New SolidBrush(_TextColor), New Rectangle(0, 0, Width - 1, Height - 1), SF)
        MyBase.OnPaint(e)
    End Sub

End Class