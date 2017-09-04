Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

<DefaultEvent("ColorPicked")> _
Public Class ColorPickerControl
    Inherits Control
    Private ReadOnly _selectedColorBox As New Panel()
    Private ReadOnly _hoverColorBox As New Panel()
    Private _canvas As Bitmap
    Private _graphicsBuffer As Graphics
    Private _spectrumGradient As LinearGradientBrush
    Private _boxSizeRatio As Single = 0.15F
    Private _paddingPercentage As Single = 0.05F
    Public Event ColorPicked As EventHandler

    Public Sub New()
        MyBase.Cursor = Cursors.Hand
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)

        Me.Size = New Size(200, 100)
        UpdateLinearGradientBrushes()
        UpdateGraphicsBuffer()
        SetupInnerBoxes()
    End Sub

    Private Sub SetupInnerBoxes()
        _selectedColorBox.Anchor = (AnchorStyles.Bottom Or AnchorStyles.Right)
        _selectedColorBox.BorderStyle = BorderStyle.FixedSingle
        Controls.Add(_selectedColorBox)
        _selectedColorBox.Visible = False

        _hoverColorBox.Anchor = (AnchorStyles.Bottom Or AnchorStyles.Right)
        _hoverColorBox.BorderStyle = BorderStyle.FixedSingle
        Controls.Add(_hoverColorBox)
        _hoverColorBox.Visible = False

        ResizeChildControls()
    End Sub

    Protected Overridable Sub OnColorPicked()
        RaiseEvent ColorPicked(Me, EventArgs.Empty)
    End Sub

    Private Sub UpdateLinearGradientBrushes()
        ' Update spectrum gradient
        _spectrumGradient = New LinearGradientBrush(Point.Empty, New Point(Me.Width, 0), Color.White, Color.White)
        Dim blend As New ColorBlend()
        blend.Positions = New Single() {0, 1 / 7.0F, 2 / 7.0F, 3 / 7.0F, 4 / 7.0F, 5 / 7.0F, _
                6 / 7.0F, 1}
        blend.Colors = New Color() {Color.Black, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, _
                Color.Indigo, Color.Violet}
        _spectrumGradient.InterpolationColors = blend
        ' Update greyscale gradient
        Dim rect As New RectangleF(0, Me.Height * 0.7F, Me.Width, Me.Height * 0.3F)
        rect = New RectangleF(Point.Empty, New SizeF(Me.Width, Me.Height * 0.3F))
    End Sub

    Private Sub UpdateGraphicsBuffer()
        If Me.Width > 0 Then
            _canvas = New Bitmap(Me.Width, Me.Height)
            _graphicsBuffer = Graphics.FromImage(_canvas)
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        ResizeChildControls()
        UpdateLinearGradientBrushes()
        UpdateGraphicsBuffer()
    End Sub


    Private Sub ResizeChildControls()
        Dim width As Integer = CInt(Math.Truncate(Me.Width * _boxSizeRatio + 0.5F))
        Dim height As Integer = CInt(Math.Truncate(Me.Height * _boxSizeRatio + 0.5F))
        _selectedColorBox.Size = New Size(width, height)
        _hoverColorBox.Size = New Size(width, height)

        Dim padding As Integer = CInt(Math.Truncate(Me.Height * _paddingPercentage))

        Dim x As Integer = Me.Width - _selectedColorBox.Width - _hoverColorBox.Width - padding * 2
        Dim y As Integer = Me.Height - _hoverColorBox.Height - padding
        _selectedColorBox.Location = New Point(x, y)

        x = Me.Width - _selectedColorBox.Width - padding
        y = Me.Height - _selectedColorBox.Height - padding
        _hoverColorBox.Location = New Point(x, y)
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        If Me.ClientRectangle.Contains(e.Location) Then
            _hoverColorBox.BackColor = _canvas.GetPixel(e.X, e.Y)

            If Not _hoverColorBox.Visible Then
                _hoverColorBox.Show()
            End If
        End If
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)
        _selectedColorBox.Visible = True
        _selectedColorBox.BackColor = _canvas.GetPixel(e.X, e.Y)
        OnColorPicked()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        _graphicsBuffer.FillRectangle(_spectrumGradient, Me.ClientRectangle)
        e.Graphics.DrawImageUnscaled(_canvas, Point.Empty)
        e.Graphics.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(172, 172, 172)), 2), Me.ClientRectangle)
    End Sub

    <Description("The current selected color of the color picker control")> _
    Public Property SelectedColor() As Color
        Get
            Return _selectedColorBox.BackColor
        End Get
        Set(value As Color)
            _selectedColorBox.BackColor = value
            _selectedColorBox.Visible = True
        End Set
    End Property

    <DefaultValue(0.15F)> _
    <Description("The size of the color boxes in relation to the parent control")> _
    <Category("Layout")> _
    Public Property ColorBoxSizeRatio() As Single
        Get
            Return _boxSizeRatio
        End Get
        Set(value As Single)
            _boxSizeRatio = value
            ResizeChildControls()
        End Set
    End Property

    <DefaultValue(0.05F)> _
    <Description("The size of the color boxes in relation to the parent control")> _
    <Category("Layout")> _
    Public Property ColorBoxPaddingRatio() As Single
        Get
            Return _paddingPercentage
        End Get
        Set(value As Single)
            _paddingPercentage = value
            ResizeChildControls()
        End Set
    End Property

    Private _fullColorSpectrum As Boolean = True
    <DefaultValue(True)> _
    <Description("Determines whether or not to use a full color spectrum for color picking." & vbCr & vbLf & "                If set to false, a RGB spectrum will be used")> _
    <Category("Appearance")> _
    Public Property FullColorSpectrum() As Boolean
        Get
            Return _fullColorSpectrum
        End Get
        Set(value As Boolean)
            _fullColorSpectrum = value
            UpdateLinearGradientBrushes()
            Me.Invalidate(False)
        End Set
    End Property
End Class