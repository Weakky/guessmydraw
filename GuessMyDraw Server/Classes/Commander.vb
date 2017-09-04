Imports ProtoBuf
Imports System.Drawing
Imports System.Security.Cryptography

Public Class Commands
    'Contains the exact same thing as the data class of the client.

#Region "Enums"
    Public Enum Command As Byte
        ClientInformations = 0
        UsersAndPartyOnConnect = 1
        ClientDisconnected = 2
        Game = 3
        Party = 4
        PartyChat = 5
        PublicChat = 6
    End Enum

    Public Enum PartyCommands As Byte
        CreateNewParty = 0
        AlreadyReceivedARequest = 1
        ReceiveAllParty = 2
        NewUserJoining = 3
        ReceiveAllUsersConnectToParty = 4
    End Enum

    Public Enum Game As Byte
        LaunchGame = 1
        AlreadyPlaying = 2
        RealTimeDrawing = 3
        ClearDrawing = 4
        WordTry = 5
        WordFound = 6
        WordNotFound = 7
        UpdateScores = 8
        PlayerDisconnectedFromParty = 9
        DrawerDisconnected = 10
    End Enum

    Public Enum Request As Byte
        Ask = 0
        Anwser = 1
        Confirmation = 2
    End Enum

    Public Enum Answer As Byte
        Accept = 0
        Deny = 1
    End Enum
#End Region

#Region "Structures & Classes"

    <ProtoContract> _
    Public Class Line
        Implements IDisposable
        <ProtoMember(1)> _
        Public Property Points As List(Of Circle)
            Get
                Return _Points
            End Get
            Set(value As List(Of Circle))
                _Points = value
            End Set
        End Property
        Private _Points As List(Of Circle)

        <ProtoMember(2)> _
        Public Property Pen As Pen
            Get
                Return _Pen
            End Get
            Set(value As Pen)
                _Pen = value
            End Set
        End Property
        Private _Pen As Pen

        Public Sub AddLine(_Circle As Circle, Pen As Pen)
            _Points.Add(_Circle)
            _Pen = Pen
        End Sub

        Sub New()
            _Points = New List(Of Circle)
            _Pen = New Pen()
        End Sub

#Region "IDisposable Support"
        Private DisposedValue As Boolean
        Private Sub Dispose(disposing As Boolean)
            If Not DisposedValue AndAlso disposing Then
                _Points = Nothing
                _Pen = Nothing
            End If
            DisposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    <ProtoContract> _
    Public Class Circle

        <ProtoMember(1)> _
        Public Property Point1 As PointX
            Get
                Return _Point1
            End Get
            Set(value As PointX)
                _Point1 = value
            End Set
        End Property
        Private _Point1 As PointX

        <ProtoMember(2)> _
        Public Property Point2 As PointX
            Get
                Return _Point2
            End Get
            Set(value As PointX)
                _Point2 = value
            End Set
        End Property
        Private _Point2 As PointX

        Sub New()
        End Sub

        Sub New(OldPoint As PointX, CurrentPoint As PointX)
            _Point1 = OldPoint : _Point2 = CurrentPoint
        End Sub
    End Class

    <ProtoContract> _
    Public Class User
        <ProtoMember(1)> _
        Public Property Username As String
            Get
                Return _Username
            End Get
            Set(value As String)
                _Username = value
            End Set
        End Property
        Private _Username As String

        <ProtoMember(2)> _
        Public Property Location As String
            Get
                Return _Location
            End Get
            Set(value As String)
                _Location = value
            End Set
        End Property
        Private _Location As String

        <ProtoMember(3)> _
        Public Property Score As Integer
            Get
                Return _Score
            End Get
            Set(value As Integer)
                _Score = value
            End Set
        End Property
        Private _Score As Integer

        <ProtoMember(4)> _
        Public Property SecureConnection As Boolean
            Get
                Return _SecureConnection
            End Get
            Set(value As Boolean)
                _SecureConnection = value
            End Set
        End Property
        Private _SecureConnection As Boolean = False

        Sub New()

        End Sub

        Sub New(PlayerUsername As String, PlayerLocation As String)
            Me._Username = PlayerUsername
            Me._Location = PlayerLocation
            Me._Score = 0
        End Sub

    End Class

    <ProtoContract> _
    Public Class Commander
        <ProtoMember(1)> _
        Public Property Command As Command
            Get
                Return _Command
            End Get
            Set(value As Command)
                _Command = value
            End Set
        End Property
        Private _Command As Command

        <ProtoMember(2)> _
        Public Property Game As Game
            Get
                Return _Game
            End Get
            Set(value As Game)
                _Game = value
            End Set
        End Property
        Private _Game As Game

        <ProtoMember(3)> _
        Public Property Party As PartyCommands
            Get
                Return _Party
            End Get
            Set(value As PartyCommands)
                _Party = value
            End Set
        End Property
        Private _Party As PartyCommands

        <ProtoMember(4)> _
        Public Property Request As Request
            Get
                Return _RequestType
            End Get
            Set(value As Request)
                _RequestType = value
            End Set
        End Property
        Private _RequestType As Request

        <ProtoMember(5)> _
        Public Property Answer As Answer
            Get
                Return _AnswerType
            End Get
            Set(value As Answer)
                _AnswerType = value
            End Set
        End Property
        Private _AnswerType As Answer
    End Class
    <ProtoContract> _
    Public Class Party

        <ProtoMember(1)> _
        Public Property Name As String
            Get
                Return _Name
            End Get
            Set(value As String)
                _Name = value
            End Set
        End Property
        Private _Name As String

        <ProtoMember(2)> _
        Public Property ID As Guid
            Get
                Return _ID
            End Get
            Set(value As Guid)
                _ID = value
            End Set
        End Property
        Private _ID As Guid

        <ProtoMember(3)> _
        Public Property Creator As User
            Get
                Return _Creator
            End Get
            Set(value As User)
                _Creator = value
            End Set
        End Property
        Private _Creator As User

        <ProtoMember(4)> _
        Public Property CurrentDrawer As User
            Get
                Return _CurrentDrawer
            End Get
            Set(value As User)
                _CurrentDrawer = value
            End Set
        End Property
        Private _CurrentDrawer As User

        <ProtoMember(5)> _
        Public Property UsersConnected As List(Of User)
            Get
                Return _UserConnected
            End Get
            Set(value As List(Of User))
                _UserConnected = value
            End Set
        End Property
        Private _UserConnected As List(Of User)

        <ProtoMember(6)> _
        Public Property IsPrivate As Boolean
            Get
                Return _IsPrivate
            End Get
            Set(value As Boolean)
                _IsPrivate = value
            End Set
        End Property
        Private _IsPrivate As Boolean

        <ProtoMember(7)> _
        Public Property WordToGuess As Word
            Get
                Return _WordToGuess
            End Get
            Set(value As Word)
                _WordToGuess = value
            End Set
        End Property
        Private _WordToGuess As Word

        Sub New()
            _ID = Guid.NewGuid()
            _UserConnected = New List(Of User)
            _Creator = New User()
            _WordToGuess = New Word()
        End Sub

        Sub New(PartyName As String, PartyCreator As User, IsPrivateParty As Boolean)
            _Name = PartyName
            _Creator = PartyCreator
            _ID = Guid.NewGuid()
            _UserConnected = New List(Of User)
            _IsPrivate = IsPrivateParty
        End Sub

    End Class
    <ProtoContract> _
    Public Class Word

        Public Sub New()
        End Sub

        Public Sub New(FirsLetter As String, Length As Integer)
            _FirstLetter = FirsLetter
            _Length = Length
        End Sub

        <ProtoMember(1)> _
        Public Property Word As String
            Get
                Return _Word
            End Get
            Set(value As String)
                _Word = value
            End Set
        End Property
        Private _Word As String

        <ProtoMember(2)> _
        Public Property Length As Integer
            Get
                Return _Length
            End Get
            Set(value As Integer)
                _Length = value
            End Set
        End Property
        Private _Length As Integer

        <ProtoMember(3)> _
        Public Property FirstLetters As String
            Get
                Return _FirstLetter
            End Get
            Set(value As String)
                _FirstLetter = value
            End Set
        End Property
        Private _FirstLetter As String

        <ProtoMember(4)> _
        Public Property Index As Integer
            Get
                Return _Index
            End Get
            Set(value As Integer)
                _Index = value
            End Set
        End Property
        Private _Index As Integer

        <ProtoMember(5)> _
        Public Property Found As Boolean
            Get
                Return _Found
            End Get
            Set(value As Boolean)
                _Found = value
            End Set
        End Property
        Private _Found As Boolean

        Public Function GetFirstLetters() As String
            If Not String.IsNullOrEmpty(Word) Then Return Word.Substring(0, 2) Else Return String.Empty
        End Function

        Public Function GetLength() As Integer
            If Not String.IsNullOrEmpty(Word) Then Return Word.Length Else Return 0
        End Function

    End Class

    <ProtoContract> _
    Structure ColorX

        Sub New(newColor As Color)
            _Red = newColor.R
            _Green = newColor.G
            _Blue = newColor.B
        End Sub

        Public Function ToColor() As Color
            Return Color.FromArgb(_Red, _Green, _Blue)
        End Function

        <ProtoMember(2)> _
        Public Property Red() As Integer
            Get
                Return _Red
            End Get
            Set(value As Integer)
                _Red = value
            End Set
        End Property
        Private _Red As Integer

        <ProtoMember(3)> _
        Public Property Green() As Integer
            Get
                Return _Green
            End Get
            Set(value As Integer)
                _Green = value
            End Set
        End Property
        Private _Green As Integer

        <ProtoMember(4)> _
        Public Property Blue() As Integer
            Get
                Return _Blue
            End Get
            Set(value As Integer)
                _Blue = value
            End Set
        End Property
        Private _Blue As Integer
    End Structure

    <ProtoContract> _
    Structure Pen
        <ProtoMember(1)> _
        Public Property Color As ColorX
            Get
                Return _Color
            End Get
            Set(value As ColorX)
                _Color = value
            End Set
        End Property
        Private _Color As ColorX

        <ProtoMember(2)> _
        Public Property Width As Single
            Get
                Return _Width
            End Get
            Set(value As Single)
                _Width = value
            End Set
        End Property
        Private _Width As Single

        Sub New(color As ColorX, width As Integer)
            _Color = color
            _Width = width
        End Sub

        Sub New(Pen As System.Drawing.Pen)
            _Color = New ColorX(Pen.Color)
            _Width = Pen.Width
        End Sub

        Public Function ToPen() As System.Drawing.Pen
            Return New System.Drawing.Pen(Color.ToColor(), _Width)
        End Function

    End Structure
    <ProtoContract> _
    Structure PointX
        <ProtoMember(1)> _
        Public Property X() As Integer
            Get
                Return m_X
            End Get
            Set(value As Integer)
                m_X = value
            End Set
        End Property
        Private m_X As Integer
        <ProtoMember(2)> _
        Public Property Y() As Integer
            Get
                Return m_Y
            End Get
            Set(value As Integer)
                m_Y = value
            End Set
        End Property
        Private m_Y As Integer

        Sub New(PointX As Integer, PointY As Integer)
            m_X = PointX : m_Y = PointY
        End Sub

    End Structure

#End Region

    <ProtoContract> _
    Public Class Data
        Implements IDisposable

        Sub New(_PlayerUser As User, _Party As Party)
            _PartyConnectedTo = _Party
            _Player = _PlayerUser
            _Line = New Line()
            _UsersConnected = New List(Of User)
            _Commander = New Commander()
            _AllPartyCreated = New List(Of Party)
        End Sub

        Sub New()
            _Line = New Line()
            _UsersConnected = New List(Of User)
            _Commander = New Commander()
            _AllPartyCreated = New List(Of Party)
        End Sub

        <ProtoMember(1)> _
        Public Property Commander As Commander
            Get
                Return _Commander
            End Get
            Set(value As Commander)
                _Commander = value
            End Set
        End Property
        Private _Commander As Commander

        <ProtoMember(2)> _
        Public Property UsersConnected As List(Of User)
            Get
                Return _UsersConnected
            End Get
            Set(value As List(Of User))
                _UsersConnected = value
            End Set
        End Property
        Private _UsersConnected As List(Of User)

        <ProtoMember(3)> _
        Public Property UserDisconnected As String
            Get
                Return _UserDisconnected
            End Get
            Set(value As String)
                _UserDisconnected = value
            End Set
        End Property
        Private _UserDisconnected As String

        <ProtoMember(4)> _
        Public Property Player As User
            Get
                Return _Player
            End Get
            Set(value As User)
                _Player = value
            End Set
        End Property
        Private _Player As User

        <ProtoMember(5)> _
        Public Property WordToGuess As Word
            Get
                Return _WordToGuess
            End Get
            Set(value As Word)
                _WordToGuess = value
            End Set
        End Property
        Private _WordToGuess As Word

        <ProtoMember(6)> _
        Public Property Line As Line
            Get
                Return _Line
            End Get
            Set(value As Line)
                _Line = value
            End Set
        End Property
        Private _Line As Line

        <ProtoMember(7)> _
        Public Property ChatMessage As String
            Get
                Return _ChatMessage
            End Get
            Set(value As String)
                _ChatMessage = value
            End Set
        End Property
        Private _ChatMessage As String

        <ProtoMember(8)> _
        Public Property PartyConnectedTo As Party
            Get
                Return _PartyConnectedTo
            End Get
            Set(value As Party)
                _PartyConnectedTo = value
            End Set
        End Property
        Private _PartyConnectedTo As Party

        <ProtoMember(9)> _
        Public Property AllPartyCreated As List(Of Party)
            Get
                Return _AllPartyCreated
            End Get
            Set(value As List(Of Party))
                _AllPartyCreated = value
            End Set
        End Property
        Private _AllPartyCreated As List(Of Party)

        <ProtoMember(10)> _
        Public Property Key As String
            Get
                Return _PublicKey
            End Get
            Set(value As String)
                _PublicKey = value
            End Set
        End Property
        Private _PublicKey As String

#Region " IDisposable Support "

        Private DisposedValue As Boolean

        Private Sub Dispose(disposing As Boolean)
            If Not DisposedValue AndAlso disposing Then
                Line.Dispose()
                _Line.Dispose()
                _ChatMessage = Nothing
                _Commander = Nothing
                _Player = Nothing
                _WordToGuess = Nothing
                _UserDisconnected = Nothing
                _UsersConnected = Nothing
            End If
            DisposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Class
