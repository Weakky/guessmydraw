Imports ProtoBuf
Imports System.IO
Imports System.Drawing.Drawing2D
Imports GuessMyDraw.DataClass

Public Class GameManager

    'This class contains all the informations about the player client side.
    'All the properties are pretty self-explanatories, I'm not gonna explain the purposes of them.

    Sub New()
        _CurrentPen = New DataClass.Pen()
        _StartedDrawing = False

        _ListOfCircles = New List(Of Circle)
        _AllPartyCreated = New List(Of Party)
        _WordToGuess = New Word()
    End Sub

    Sub New(Username As String, Location As String)
        _Player = New DataClass.User(Username, Location)
        _StartedDrawing = False

        _ListOfCircles = New List(Of Circle)
        _CurrentPen = New DataClass.Pen()
        _AllPartyCreated = New List(Of Party)
        _WordToGuess = New Word()
    End Sub

    Private _Playing As Boolean = False
    Public Property Playing As Boolean
        Get
            Return _Playing
        End Get
        Set(value As Boolean)
            _Playing = value
        End Set
    End Property

    Private _AlreadyReceivedARequest As Boolean = False
    Public Property AlreadyReceivedARequest As Boolean
        Get
            Return _AlreadyReceivedARequest
        End Get
        Set(value As Boolean)
            _AlreadyReceivedARequest = value
        End Set
    End Property

    Private _PartyConnectedTo As Party
    Public Property PartyConnectedTo As Party
        Get
            Return _PartyConnectedTo
        End Get
        Set(value As Party)
            _PartyConnectedTo = value
        End Set
    End Property

    Private _AllPartyCreated As List(Of Party)
    Public Property AllPartyCreated As List(Of Party)
        Get
            Return _AllPartyCreated
        End Get
        Set(value As List(Of Party))
            _AllPartyCreated = value
        End Set
    End Property

    Private _Player As User
    Public Property Player As User
        Get
            Return _Player
        End Get
        Set(value As User)
            _Player = value
        End Set
    End Property

    Private _WordToGuess As Word
    Public Property WordToGuess As Word
        Get
            Return _WordToGuess
        End Get
        Set(value As Word)
            _WordToGuess = value
        End Set
    End Property

    Private _RandomWordRemaining As Integer = 5
    Public Property RandomWordRemaining As Integer
        Get
            Return _RandomWordRemaining
        End Get
        Set(value As Integer)
            _RandomWordRemaining = value
        End Set
    End Property

    Private _CurrentPen As DataClass.Pen
    Public Property CurrentPen As DataClass.Pen
        Get
            Return _CurrentPen
        End Get
        Set(value As DataClass.Pen)
            _CurrentPen = value
        End Set
    End Property

    Private _ListOfCircles As List(Of Circle)
    Public Property ListOfCircles As List(Of Circle)
        Get
            Return _ListOfCircles
        End Get
        Set(value As List(Of Circle))
            _ListOfCircles = value
        End Set
    End Property

    Private _StartedDrawing As Boolean
    Public Property StartedDrawing As Boolean
        Get
            Return _StartedDrawing
        End Get
        Set(value As Boolean)
            _StartedDrawing = value
        End Set
    End Property

    Public Sub Reset()
        _Playing = False
        _PartyConnectedTo = Nothing
        _ListOfCircles.Clear()
        _WordToGuess = Nothing
        _RandomWordRemaining = 5
    End Sub
End Class