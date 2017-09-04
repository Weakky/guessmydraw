Option Strict on
''' <summary>
''' Implements a countdown timer that raises an event each millisecond but most important when the time have run out.
''' </summary>
<System.ComponentModel.DefaultEvent("TimesOut")> Public Class CountDown

    'That class isn't from me, I don't remember from who I took it, but well, I could've easily made it.
    'That what just to earn time.

    Private Time As Timer
    Private TimeLeft As Double
    Private SetTimeLeft As Double

    ''' <summary>
    ''' Initializes a new instance of the CountDown class.
    ''' </summary>
    Public Sub New()
        SetNew()
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the CountDown class with the value from a System.Timespan.
    ''' </summary>
    ''' <param name="Span">The System.TimeSpan to take the value from</param>
    Public Sub New(ByVal Span As TimeSpan)
        TimeLeft = Span.TotalSeconds()
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the CountDown class with the value from Seconds.
    ''' </summary>
    ''' <param name="Seconds">Number of Seconds</param>
    Public Sub New(ByVal Seconds As Integer)
        SetNew()
        TimeLeft = Seconds
        SetTimeLeft = TimeLeft
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the CountDown class with the value from Seconds and Minutes.
    ''' </summary>
    ''' <param name="Seconds">Number of Seconds</param>
    ''' <param name="Minutes">Number of Minutes</param>
    Public Sub New(ByVal Seconds As Integer, ByVal Minutes As Integer)
        SetNew()
        TimeLeft = Seconds + Minutes * 60
        SetTimeLeft = TimeLeft
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the CountDown class with the value from Seconds, Minutes and Hours.
    ''' </summary>
    ''' <param name="Seconds">Number of Seconds</param>
    ''' <param name="Minutes">Number of Minutes</param>
    ''' <param name="Hours">Number of Hours</param>
    Public Sub New(ByVal Seconds As Integer, ByVal Minutes As Integer, ByVal Hours As Integer)
        SetNew()
        TimeLeft = Seconds + Minutes * 60 + Hours * 3600
        SetTimeLeft = TimeLeft
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the CountDown class with the value from Seconds, Minutes, Hours and Days.
    ''' </summary>
    ''' <param name="Seconds">Number of Seconds</param>
    ''' <param name="Minutes">Number of Minutes</param>
    ''' <param name="Hours">Number of Hours</param>
    ''' <param name="Days">Number of Days</param>
    Public Sub New(ByVal Seconds As Integer, ByVal Minutes As Integer, ByVal Hours As Integer, ByVal Days As Integer)
        SetNew()
        SetTimeLeft = TimeLeft
        TimeLeft = Seconds + Minutes * 60 + Hours * 3600 + Days * 86400
    End Sub

    Private Sub SetNew()
        Time = New Timer
        Time.Interval = 1000
        AddHandler Time.Tick, AddressOf Timer_Tick
    End Sub


    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TimeLeft -= 1

        RaiseEvent Tick(Me, e)

        If TimeLeft = 0 Then
            Time.Stop()
            RaiseEvent TimesOut(Me, e)
        End If
    End Sub


    ''' <summary>
    ''' Start the timer.
    ''' </summary>
    Public Sub Start()
        If TimeLeft = 0 Then TimeLeft = SetTimeLeft
        If TimeLeft <> 0 Then
            Time.Start()
        End If
    End Sub
    ''' <summary>
    ''' Pause the timer.
    ''' </summary>
    Public Sub Pause()
        Time.Stop()
    End Sub
    ''' <summary>
    ''' Stop the timer and reset the time.
    ''' </summary>
    Public Sub Reset()
        Time.Stop()
        TimeLeft = SetTimeLeft
    End Sub

    ''' <summary>
    ''' Set the time of the timer with the value from a System.Timespan.
    ''' </summary>
    ''' <param name="Span">The System.TimeSpan to take the value from</param>
    Public Sub SetTime(ByVal Span As TimeSpan)
        TimeLeft = Span.TotalSeconds
        SetTimeLeft = TimeLeft
    End Sub
    ''' <summary>
    ''' Set the time of the timer with Seconds.
    ''' </summary>
    ''' <param name="Seconds">Number of Seconds</param>
    Public Sub SetTime(ByVal Seconds As Integer)
        TimeLeft = Seconds
        SetTimeLeft = TimeLeft
    End Sub
    ''' <summary>
    ''' Set the time of the timer with Seconds and Minutes.
    ''' </summary>
    ''' <param name="Seconds">Number of Seconds</param>
    ''' <param name="Minutes">Number of Minutes</param>
    Public Sub SetTime(ByVal Seconds As Integer, ByVal Minutes As Integer)
        TimeLeft = Seconds + Minutes * 60
        SetTimeLeft = TimeLeft
    End Sub
    ''' <summary>
    ''' Set the time of the timer with Seconds, Minutes and Hours.
    ''' </summary>
    ''' <param name="Seconds">Number of Seconds</param>
    ''' <param name="Minutes">Number of Minutes</param>
    ''' <param name="Hours">Number of Hours</param>
    Public Sub SetTime(ByVal Seconds As Integer, ByVal Minutes As Integer, ByVal Hours As Integer)
        TimeLeft = Seconds + Minutes * 60 + Hours * 3600
        SetTimeLeft = TimeLeft
    End Sub
    ''' <summary>
    ''' Set the time of the timer with Seconds, Minutes, Hours and Days.
    ''' </summary>
    ''' <param name="Seconds">Number of Seconds</param>
    ''' <param name="Minutes">Number of Minutes</param>
    ''' <param name="Hours">Number of Hours</param>
    ''' <param name="Days">Number of Days</param>
    Public Sub SetTime(ByVal Seconds As Integer, ByVal Minutes As Integer, ByVal Hours As Integer, ByVal Days As Integer)
        TimeLeft = Seconds + Minutes * 60 + Hours * 3600 + Days * 86400
        SetTimeLeft = TimeLeft
    End Sub

    ''' <summary>
    ''' Add extra time to the timer with the value from a System.Timespan.
    ''' </summary>
    ''' <param name="Span">The System.TimeSpan to take the value from</param>
    Public Sub AddTime(ByVal Span As TimeSpan)
        TimeLeft += Span.TotalSeconds
        SetTimeLeft = TimeLeft
    End Sub
    ''' <summary>
    ''' Add extra time to the timer with Seconds.
    ''' </summary>
    ''' <param name="Seconds">Number of Seconds</param>
    Public Sub AddTime(ByVal Seconds As Integer)
        TimeLeft += Seconds
        SetTimeLeft = TimeLeft
    End Sub
    ''' <summary>
    ''' Add extra time to the timer with Seconds and Minutes.
    ''' </summary>
    ''' <param name="Seconds">Number of Seconds</param>
    ''' <param name="Minutes">Number of Minutes</param>
    Public Sub AddTime(ByVal Seconds As Integer, ByVal Minutes As Integer)
        TimeLeft += Seconds + Minutes * 60
        SetTimeLeft = TimeLeft
    End Sub
    ''' <summary>
    ''' Add extra time to the timer with Seconds, Minutes and Hours.
    ''' </summary>
    ''' <param name="Seconds">Number of Seconds</param>
    ''' <param name="Minutes">Number of Minutes</param>
    ''' <param name="Hours">Number of Hours</param>
    Public Sub AddTime(ByVal Seconds As Integer, ByVal Minutes As Integer, ByVal Hours As Integer)
        TimeLeft += Seconds + Minutes * 60 + Hours * 3600
        SetTimeLeft = TimeLeft
    End Sub
    ''' <summary>
    ''' Add extra time to the timer with Seconds, Minutes, Hours and Days.
    ''' </summary>
    ''' <param name="Seconds">Number of Seconds</param>
    ''' <param name="Minutes">Number of Minutes</param>
    ''' <param name="Hours">Number of Hours</param>
    ''' <param name="Days">Number of Days</param>
    Public Sub AddTime(ByVal Seconds As Integer, ByVal Minutes As Integer, ByVal Hours As Integer, ByVal Days As Integer)
        TimeLeft += Seconds + Minutes * 60 + Hours * 3600 + Days * 86400
        SetTimeLeft = TimeLeft
    End Sub


    ''' <summary>
    ''' Returns the number of Seconds left.
    ''' </summary>
    Public ReadOnly Property Seconds() As Integer
        Get
            Return TimeSplit(3)
        End Get
    End Property
    ''' <summary>
    ''' Returns the number of Minutes left.
    ''' </summary>
    Public ReadOnly Property Minutes() As Integer
        Get
            Return TimeSplit(2)
        End Get
    End Property
    ''' <summary>
    ''' Returns the number of Hours left.
    ''' </summary>
    Public ReadOnly Property Hours() As Integer
        Get
            Return TimeSplit(1)
        End Get
    End Property
    ''' <summary>
    ''' Returns the number of Days left.
    ''' </summary>
    Public ReadOnly Property Days() As Integer
        Get
            Return TimeSplit(0)
        End Get
    End Property

    Private Function TimeSplit() As Integer()
        Dim TimeLeftClone As Double = TimeLeft
        Dim ReturnValue(4) As Integer


        While TimeLeftClone > 0
            Select Case TimeLeftClone
                Case Is >= 86400
                    TimeLeftClone -= 86400
                    ReturnValue(0) += 1
                Case Is >= 3600
                    TimeLeftClone -= 3600
                    ReturnValue(1) += 1
                Case Is >= 60
                    TimeLeftClone -= 60
                    ReturnValue(2) += 1
                Case Else
                    ReturnValue(3) = CInt(TimeLeftClone)
                    TimeLeftClone = 0
            End Select
        End While

        Return ReturnValue
    End Function



    ''' <summary>
    ''' Returns the time in Seconds.
    ''' </summary>
    Public ReadOnly Property TotalSeconds() As Double
        Get
            Return TimeLeft
        End Get
    End Property
    ''' <summary>
    ''' Returns the time in Minutes.
    ''' </summary>
    Public ReadOnly Property TotalMinutes() As Double
        Get
            Return TimeLeft / 60
        End Get
    End Property
    ''' <summary>
    ''' Returns the time in Hours.
    ''' </summary>
    Public ReadOnly Property TotalHours() As Double
        Get
            Return TimeLeft / 3600
        End Get
    End Property
    ''' <summary>
    ''' Returns the time in Days.
    ''' </summary>
    Public ReadOnly Property TotalDays() As Double
        Get
            Return TimeLeft / 86400
        End Get
    End Property


    ''' <summary>
    ''' Raises when the timer reaches 0.
    ''' </summary>
    Public Event TimesOut(ByVal sender As Object, ByVal e As EventArgs)
    ''' <summary>
    ''' Raises each Seconds.
    ''' </summary>
    Public Event Tick(ByVal sender As Object, ByVal e As EventArgs)


End Class

