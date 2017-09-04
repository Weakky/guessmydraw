Imports System.IO
Imports System.Windows
Imports ProtoBuf
Imports System.Text
Imports System.Reflection
Imports GuessMyDraw.GameManager
Imports GuessMyDraw.DataClass
Imports System.Globalization
Imports System.Net
Imports System.Security.Cryptography

Public Class frmMain

    'GuessMyDraw
    'Coded by TRANSLU6DE
    'Project started: 19/12/13
    'Beta: 26/12/13
    'Release: 30/12/13

    'TODO: Finish to translate these 800 words.
    'TODO: Fix drawing lines problem
    'TODO: When a user is connected to a public party and get a request, make the user able to directly join that party
    'TODO: Make server logs much cleaner removing that shitty console
    'TODO: Add Xertz new custom generate word button


    'THINGS ALREADY FIXED (MAY NOT BE COMPLETELY WORKING):
    'FIXED: User can't receive more than one private party request at once
    'FIXED: Minor issues fixed
    'FIXED: Record idle time and kick user if he's idle too long.

    Public WithEvents Client As UserClient 'Handles the client socket connection.
    Public GM As GameManager 'Contains all the data client about the game, that may be reused to send data.

#Region "Helper Methods"
    Private Sub SendData(ByVal Data As Data)
        Dim B As Byte() = SerializeData(Data)
        Client.Send(B) 'Send the data to the server
    End Sub 'Send the data serialized to the server
    Private Function SerializeData(ByVal Data As Data) As Byte()
        Using ms = New MemoryStream()
            Serializer.Serialize(ms, Data)
            Return ms.ToArray()
        End Using
    End Function 'Serialize the data using protobuf serializer
    Private Sub LogMessage(Message As String, ParamArray items() As Object)
        TBPartyReceivedMessage.AppendText(String.Format("Server: " & Message & Environment.NewLine, items))
    End Sub
#End Region


    Private Sub Initialize() 'This just initialize all classes, and connect to the server.
        Client = New UserClient
        Client.Connect(frmConnect.TextBox1.Text, 1234) 'Connect to the server

        'Initialize the GameManager class, with the username and the location of the user
        GM = New GameManager(frmConnect.TBUsername.Text, RegionInfo.CurrentRegion.EnglishName)
        Text = String.Format("GuessMyDraw: {0}", GM.Player.Username)
        DirectCast(TabPageGame, Control).Enabled = False 'Disable the tabpage game.
    End Sub
    Private Sub InitializeGame()
        GM.Playing = True 'Define you as "Playing", in order to avoid new request from player while you play.
        DirectCast(TabPageGame, Control).Enabled = True
        MainTabControl.SelectTab(1)
        TabControlUsersOnline.SelectTab(0)

        'If you're the current drawer, then make you able to start the game, else, just wait for the game to start
        If Not GM.Player.Username = GM.PartyConnectedTo.CurrentDrawer.Username Then
            MakeUserWaitingForGameToStart()
        Else
            MakeUserEnableToStartTheGame()
        End If
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Initialize()
    End Sub

    Private Sub Client_StateChanged(sender As UserClient, connected As Boolean) Handles Client.StateChanged
        If connected Then 'When you're connected
            MainTabControl.SelectTab(0) 'Select the first tab.
        End If
    End Sub

    Private Sub Client_ReadPacket(sender As UserClient, data() As Byte) Handles Client.ReadPacket
        'Here's where all the magic happens, where all the data is received.
        Using ms = New MemoryStream(data)
            Using DataReceived As Data = Serializer.Deserialize(Of Data)(ms) 'Deserialize the data received with protobuf
                Select Case DataReceived.Commander.Command 'Then do different things based on the packet received
                    Case Command.ClientInformations 'This handle a new user connected
                        HandleClientInformations(DataReceived)
                    Case Command.ClientDisconnected 'This handle a user disconnected
                        HandleClientDisconnected(DataReceived)
                    Case Command.UsersAndPartyOnConnect 'This give you all the users and the party already connected/created when you connect
                        HandleUsersAndPartyOnConnect(DataReceived)
                    Case Command.Game 'This handle the whole game
                        HandleGame(DataReceived)
                    Case Command.Party 'This handle everything about a party
                        HandleParty(DataReceived)
                    Case Command.PartyChat 'And this handle a chat message received
                        HandlePartyChatMessage(DataReceived)
                    Case Command.PublicChat
                        HandlePublicChatMessage(DataReceived)
                End Select
            End Using
        End Using
    End Sub


#Region "Send Data Handlers"

    'There we have all the method to send the packets.
    'They all initialize a new instance of the data class, in order to avoid sending not needed data.
    'The name of each method are again self-explanatories. No need to explain when they do.

    Private Sub SendConnectionClientInfo(Datareceived As Data, _PlayerInfo As User)
        Using D As New Data With {.Player = _PlayerInfo}
            D.Key = Handshake(Datareceived.Key, ":mabite:")
            With D.Commander
                .Command = Command.ClientInformations
            End With
            SendData(D)
        End Using
    End Sub
    Private Sub SendChatMessageToParty(_Message As String)
        Using D As New Data(GM.Player, GM.PartyConnectedTo)
            D.Commander.Command = Command.PartyChat
            D.ChatMessage = _Message
            SendData(D)
            TBPartyReceivedMessage.AppendText(String.Format("Me: {0}{1}", _Message, Environment.NewLine))
            TBPartySendChatMessage.Clear()
        End Using
    End Sub

    Private Sub SendChatMessageToPublicChat(_Message As String)
        Using D As New Data() With {.Player = GM.Player}
            D.Commander.Command = Command.PublicChat
            D.ChatMessage = _Message
            SendData(D)
            TBPublicChatReceive.AppendText(String.Format("Me: {0}{1}", _Message, Environment.NewLine))
            TBPublicChatReceiveFromParty.AppendText(String.Format("Me: {0}{1}", _Message, Environment.NewLine))
            TBPublicChatSend.Clear()
            TBPublicChatSendFromParty.Clear()
        End Using
    End Sub


    Private Sub SendReponseAlreadyPlaying(Opponent As User, PartyCreated As Party)
        Using D As New Data(Opponent, PartyCreated)
            With D.Commander
                .Command = Command.Game
                .Game = Game.AlreadyPlaying
            End With
            SendData(D)
        End Using
    End Sub

    Private Sub SendRequestToBeginGame(NewParty As Party, OpponentUser As User)
        NewParty.UsersConnected.Add(OpponentUser)
        GM.PartyConnectedTo = NewParty
        Using D As New Data(GM.Player, NewParty)
            With D.Commander
                .Command = Command.Party
                .Party = PartyCommands.CreateNewParty
                .Request = Request.Ask
            End With
            SendData(D)
        End Using
    End Sub
    Private Sub SendResponseToBeginGame(_Answer As DataClass.Answer)
        Using D As New Data(GM.Player, GM.PartyConnectedTo)
            With D.Commander
                .Command = Command.Party
                .Party = PartyCommands.CreateNewParty
                .Request = Request.Anwser
                .Answer = _Answer
            End With
            SendData(D)
        End Using
    End Sub
    Private Sub SendConfirmationToBeginGameAndInitializeGame()
        Using D As New Data(GM.Player, GM.PartyConnectedTo)
            With D.Commander
                .Command = Command.Party
                .Party = PartyCommands.CreateNewParty
                .Request = Request.Confirmation
            End With
            SendData(D)
            InitializeGame()
        End Using
    End Sub

    Private Sub SendRequestToLaunchGame(_WordToGuess As Word, _CurrentDrawer As User)
        Using D As New Data(GM.Player, GM.PartyConnectedTo) With {.WordToGuess = _WordToGuess}
            D.PartyConnectedTo.CurrentDrawer = _CurrentDrawer
            With D.Commander
                .Command = Command.Game
                .Game = Game.LaunchGame
                .Request = Request.Ask
            End With
            SendData(D)
        End Using
    End Sub
    Private Sub SendResponseToLaunchGame(_Answer As DataClass.Answer)
        Using D As New Data(GM.Player, GM.PartyConnectedTo)
            D.PartyConnectedTo.CurrentDrawer = GM.PartyConnectedTo.CurrentDrawer
            With D.Commander
                .Command = Command.Game
                .Game = Game.LaunchGame
                .Request = Request.Anwser
                .Answer = _Answer
            End With
            SendData(D)
        End Using
    End Sub
    Private Sub SendConfirmationToLaunchGameAndInitializeLaunchGame()
        Dim D As New Data(GM.Player, GM.PartyConnectedTo)
        With D.Commander
            .Command = Command.Game
            .Game = Game.LaunchGame
            .Request = Request.Confirmation
        End With
        SendData(D)

        'Initialize launch game
        MakeUserEnableToDraw()
    End Sub

    Private Sub SendRealTimeDrawing(_Points As List(Of Circle))
        Using D As New Data(GM.Player, GM.PartyConnectedTo)
            With D.Commander
                .Command = Command.Game
                .Game = Game.RealTimeDrawing
            End With
            D.Line.Pen = GM.CurrentPen
            D.Line.Points = _Points
            SendData(D)
            DrawingPoints.Clear()
        End Using
        GC.Collect()
    End Sub

    Private Sub SendClearImage()
        Using D As New Data(GM.Player, GM.PartyConnectedTo)
            With D.Commander
                .Command = Command.Game
                .Game = Game.ClearDrawing
            End With
            SendData(D)
        End Using
    End Sub

    Private Sub SendWordToGuess(_WordToGuess As String)
        Using D As New Data(GM.Player, GM.PartyConnectedTo)
            With D.Commander
                .Command = Command.Game
                .Game = Game.WordTry
            End With

            D.WordToGuess = New Word With {.Word = _WordToGuess}
            SendData(D)
        End Using
    End Sub

    Private Sub SendWordNotFound()
        Dim D As New Data(GM.Player, GM.PartyConnectedTo)
        With D.Commander
            .Command = Command.Game
            .Game = Game.WordNotFound
        End With
        SendData(D)
    End Sub

    Private Sub SendUserDisconnectedFromParty()
        Using D As New Data(GM.Player, GM.PartyConnectedTo)
            With D.Commander
                .Command = Command.Game
                .Game = Game.PlayerDisconnectedFromParty
            End With
            SendData(D)
        End Using
    End Sub

#End Region
#Region "Send Party Method"
    Private Sub SendNewParty(Name As String, Creator As User, _PrivateParty As Boolean)
        Dim NewParty As New Party(Name, Creator, _PrivateParty) With {.CurrentDrawer = GM.Player}
        GM.PartyConnectedTo = NewParty 'Set the party created to the client side class

        'If the party is public, then add it to the listview that contains all the party created
        If Not _PrivateParty Then LVPartyCreated.Items.Add(New ListViewItem(New String() {NewParty.Name, NewParty.Creator.Username}) With {.Tag = NewParty})

        Dim D As New Data()
        D.Player = GM.Player
        D.PartyConnectedTo = NewParty
        D.Commander.Command = Command.Party
        D.Commander.Party = PartyCommands.CreateNewParty
        SendData(D)

    End Sub
    Private Sub SendConnectionToParty(User As User, Party As Party)
        GM.PartyConnectedTo = Party
        Dim D As New Data()
        D.Player = GM.Player
        D.PartyConnectedTo = GM.PartyConnectedTo

        D.Commander.Command = Command.Party
        D.Commander.Party = PartyCommands.NewUserJoining

        SendData(D)
    End Sub
    Private Sub SendAlreadyReceivedARequest(Opponent As User, Party As Party)
        Dim D As New Data(Opponent, Party)
        With D.Commander
            .Command = Command.Party
            .Party = PartyCommands.AlreadyReceivedARequest
        End With
        SendData(D)
    End Sub
#End Region

#Region "Main Data Handlers"
    Private Sub HandleClientInformations(DataReceived As Data)
        Dim Opponent As User = DataReceived.Player
        LVUserList.Items.Add(New ListViewItem(New String() {Opponent.Username, Opponent.Location}))
        LblUsersConnected.Text = CStr(LVUserList.Items.Count)
    End Sub 'Add a new user connected
    Private Sub HandleClientDisconnected(DataReceived As Data)
        Dim ClientDisconnected As String = DataReceived.UserDisconnected
        For Each item As ListViewItem In LVUserList.Items
            If item.Text = ClientDisconnected Then
                LVUserList.Items.Remove(item)
            End If
        Next
        LblUsersConnected.Text = CStr(LVUserList.Items.Count)
    End Sub 'Remove a user disconnected
    Private Sub HandleUsersAndPartyOnConnect(DataReceived As Data)
        Dim UsersConnectedList As List(Of User) = DataReceived.UsersConnected

        For Each User As User In UsersConnectedList
            If Not User Is DataReceived.Player Then
                LVUserList.Items.Add(New ListViewItem(New String() {User.Username, User.Location}))
            End If
        Next
        For Each Party As Party In DataReceived.AllPartyCreated
            LVPartyCreated.Items.Add(New ListViewItem(New String() {Party.Name, Party.Creator.Username}) With {.Tag = Party})
        Next

        TabPageParty.Text = String.Format("Public Party ({0})", LVPartyCreated.Items.Count)
        LblUsersConnected.Text = CStr(LVUserList.Items.Count)

        SendConnectionClientInfo(DataReceived, GM.Player)
    End Sub 'Gives you all the users and the party already connected/created when you connect to the server.
    Private Sub HandlePartyChatMessage(DataReceived As Data)
        Dim MessageReceived As String = DataReceived.ChatMessage
        Dim Sender As String = DataReceived.Player.Username
        TBPartyReceivedMessage.AppendText(String.Format("{0}: {1}{2}", Sender, MessageReceived, Environment.NewLine))
    End Sub 'Handle received chat message
    Private Sub HandlePublicChatMessage(DataReceived As Data)
        Dim MessageReceived As String = DataReceived.ChatMessage
        Dim Sender As String = DataReceived.Player.Username
        TBPublicChatReceive.AppendText(String.Format("{0}: {1}{2}", Sender, MessageReceived, Environment.NewLine))
        TBPublicChatReceiveFromParty.AppendText(String.Format("{0}: {1}{2}", Sender, MessageReceived, Environment.NewLine))

        If Not TabControlUsersOnline.SelectedIndex = 2 Then TabPagePublicChat.Text = "Public Chat (New Messages)"
        If Not TabControlGame.SelectedIndex = 2 Then TabPagePublicChatFromParty.Text = "Public Chat (New Messages)"
    End Sub
    Private Sub HandleParty(DataReceived As Data)
        Select Case DataReceived.Commander.Party
            Case PartyCommands.CreateNewParty
                HandleCreateNewPrivateParty(DataReceived)
            Case PartyCommands.ReceiveAllParty
                HandleAllPartyReceived(DataReceived)
            Case PartyCommands.NewUserJoining
                HandleReceiveAllUsersConnectToParty(DataReceived)
            Case PartyCommands.ReceiveAllUsersConnectToParty
                HandleReceiveAllUsersConnectToParty(DataReceived)
            Case PartyCommands.AlreadyReceivedARequest
                HandleRequestAlreadySent(DataReceived)
        End Select
    End Sub 'Handle everything about parties.
    Private Sub HandleGame(DataReceived As Data)
        Select Case DataReceived.Commander.Game
            Case Game.LaunchGame  'Handle Request to start countdown
                HandleLaunchGame(DataReceived)
            Case Game.AlreadyPlaying 'Handle when player wants to play but you're already playing
                HandleOpponentAlreadyPlaying(DataReceived)
            Case Game.RealTimeDrawing 'Handle RealTimeDrawing on picturebox
                HandleRealTimeDrawing(DataReceived)
            Case Game.WordTry
                HandleWordTry(DataReceived)
            Case Game.WordFound 'Handle when the word to guess has been found
                HandleWordFound(DataReceived)
                'Case Game.WordNotFound 'Handle when the word hasn't been found
                '    HandleWordNotFound(DataReceived)
            Case Game.UpdateScores
                HandleUpdateScoresAndSetNewDrawer(DataReceived)
            Case Game.ClearDrawing 'Handle when user clear his drawing
                HandleClearDrawing()
            Case Game.PlayerDisconnectedFromParty 'Handle when opponent quit the party
                HandleOpponentDisconnected(DataReceived)
            Case Game.DrawerDisconnected
                HandleDrawerDisconnected(DataReceived)
        End Select
    End Sub 'Handle everything about the game itself.
#End Region

#Region "Game Handlers"
    'Here are all the handlers about the game. Therefore, these methods handle all the data sent from the players.
    Private WithEvents CT As New CountDown(55) 'Class to handle the countdown easier
    Private WithEvents CTStartGame As New CountDown(4)
    Private OnePlayerConnected As Boolean = False 'Make sure at least one player is ready to play on public party

    'Here I decided to make two way of handling the beginning of a game, depending if it's a public or a private party.
    Private Sub HandleLaunchGame(DataReceived As Data)
        HandleLaunchGamePublic(DataReceived)
    End Sub
    Private Sub HandleLaunchGamePrivate(DataReceived As Data)
        Select Case DataReceived.Commander.Request
            Case Request.Ask 'Opponent form
                TopMost = True
                If MessageBox.Show("Your opponent wants to start a new game. Are you ready to play ?", "GuessMyDraw", MessageBoxButtons.YesNo) = Forms.DialogResult.Yes Then 'User received a request to launch the game
                    GM.WordToGuess = TranslateWord(DataReceived.WordToGuess, GM.Player.Location) 'Set the word to guess and translate it depending on your location
                    GM.PartyConnectedTo.CurrentDrawer = DataReceived.Player 'Set the current drawer as the person who just sent the data.
                    LblHiddenText.Text = MakeWordHidden(GM.WordToGuess) 'Show the word with all the underscore "A _ _ _ _ _"
                    SendResponseToLaunchGame(Answer.Accept) 'Then, say if you accepted or denied the party;
                Else
                    SendResponseToLaunchGame(Answer.Deny)
                End If
                TopMost = False

            Case Request.Anwser 'Player form
                TopMost = True
                If DataReceived.Commander.Answer = Answer.Accept Then ' 'If the user accepted, then start the game
                    MessageBox.Show("Starting game.", "GuessMyDraw")
                    SendConfirmationToLaunchGameAndInitializeLaunchGame() 'Send a final confirmation to start the countdown simultaneously on both side
                    CT.Start() 'Start countdown
                Else 'Else, just make him wait to start the game
                    GM.WordToGuess = Nothing
                    MakeUserWaitingForGameToStart()
                End If
                TopMost = False
            Case Request.Confirmation 'Opponent form
                TopMost = True
                LblLenghtOfWordToGuess.Text = GM.WordToGuess.Word.Length.ToString.Trim() 'Set the length of the word
                MakeUserEnableToGuess() 'Allow the user to guess
                CT.Start() 'Then finally start the countdown too. The game is now launched, and the user can try to guess the word.
                TopMost = False
        End Select
    End Sub
    Private Sub HandleLaunchGamePublic(DataReceived As Data)
        Dim Drawer As User = DataReceived.PartyConnectedTo.CurrentDrawer
        'We're handling things quite differently on a public party.
        Select Case DataReceived.Commander.Request
            Case Request.Ask 'Here everything is as above
                LblGameStart.Visible = True
                GM.WordToGuess = New Word(DataReceived.WordToGuess.FirstLetters, DataReceived.WordToGuess.Length)
                LblHiddenText.Text = MakeWordHidden(DataReceived.WordToGuess)
                LblLenghtOfWordToGuess.Text = CStr(DataReceived.WordToGuess.Length)
                CTStartGame.Start()
                GM.PartyConnectedTo.CurrentDrawer = Drawer
                SendResponseToLaunchGame(Answer.Accept) 'Except that all the users automatically accept the party when they receive the packet.
            Case Request.Anwser
                'Make sure at least one player is connected, and doesn't fire this more than once.
                'Make also sure it's the drawer
                If OnePlayerConnected = False AndAlso GM.Player.Username = Drawer.Username Then
                    CTStartGame.Start()
                    OnePlayerConnected = True
                    LblGameStart.Visible = True
                End If
        End Select
    End Sub

    Private Sub HandleCountdownFinishedPrivate()
        If GBWordGuess.Enabled Then 'If the user if try to guess
            MessageBox.Show(String.Format("You failed. The word was ""{0}""", GM.WordToGuess.Word))
            MakeUserEnableToStartTheGame()
        Else 'Or if the user if the current drawer
            MessageBox.Show("Your opponent didn't manage to guess the word. Wait for him to start a new game.", "GuessMyDraw")
            SendWordNotFound()
            MakeUserWaitingForGameToStart()
        End If
        CT.Reset()
        GenerateRandomWord(GM.Player.Location)
    End Sub
    Private Sub HandleCountdownFinishedPublic()
        If GM.Player.Username = GM.PartyConnectedTo.CurrentDrawer.Username Then
            SendWordNotFound()
        End If
        MakeUserWaitingForGameToStart()
        GenerateRandomWord(GM.Player.Location)
    End Sub

    Private Sub CT_Tick(sender As Object, e As EventArgs) Handles CT.Tick
        If CT.Seconds = 20 And Not GM.Player.Username = GM.PartyConnectedTo.CurrentDrawer.Username Then
            LblHiddenText.Text = GiveSecondLetter(MakeWordHidden(GM.WordToGuess)) 'Give a second letter to the word at 20 seconds remaining
        End If
        If CT.Seconds < 20 AndAlso CT.Minutes < 1 Then
            LblCountdown.ForeColor = Color.Red
        Else
            LblCountdown.ForeColor = Color.LimeGreen
        End If

        LblCountdown.Text = String.Format("{0} s", CT.TotalSeconds())
    End Sub
    Private Sub CT_TimesOut(sender As Object, e As EventArgs) Handles CT.TimesOut
        HandleCountdownFinishedPublic()
    End Sub

    Private Sub CTStartGame_Tick(sender As Object, e As EventArgs) Handles CTStartGame.Tick
        LblGameStart.Text = String.Format("Game is about to start in {0} seconds...", CTStartGame.TotalSeconds())
    End Sub
    Private Sub CTStartGame_TimesOut(sender As Object, e As EventArgs) Handles CTStartGame.TimesOut
        If Not GM.PartyConnectedTo.CurrentDrawer.Username = GM.Player.Username Then 'If the user is not the drawer
            MakeUserEnableToGuess() 'Then allow him the guess
        Else
            MakeUserEnableToDraw() 'Else, allow him to draw
        End If
        OnePlayerConnected = False
        CTStartGame.Reset()
        CT.Start() 'And start the main game 75sec countdown
    End Sub

    'There's no wordnotfound sub anymore, everythings updated in the updatescore sub now.
    Private Sub HandleWordTry(DataReceived As Data)
        TBWordToGuess.Text = GM.WordToGuess.FirstLetters(0)
        TBWordToGuess.Select(1, 1)
        LblWarnWrongWord.Text = "Wrong word. Try again."
    End Sub
    Private Sub HandleWordFound(DataReceived As Data)
        If GM.PartyConnectedTo Is Nothing Then Exit Sub
        If DataReceived.WordToGuess.Found Then
            If Not DataReceived.Player.Username = GM.Player.Username Then
                GM.PartyConnectedTo.CurrentDrawer = DataReceived.PartyConnectedTo.CurrentDrawer
                CT.Reset()
                MakeUserWaitingForGameToStart()
                LogMessage("The word ""{0}"" has been found by ""{1}"" !", TranslateWord(DataReceived.WordToGuess, GM.Player.Location).Word, DataReceived.Player.Username)
            Else
                GM.PartyConnectedTo.CurrentDrawer = DataReceived.PartyConnectedTo.CurrentDrawer
                CT.Reset()
                MakeUserWaitingForGameToStart()
                LogMessage("Congrats, you just found the word")
            End If
        End If

    End Sub

    Private Function TranslateWord(WordToGuess As Word, MyLocation As String) As Word
        Select Case MyLocation 'If the user is french, then transalte the word in french, else, give it in english
            'There's two list of words where the words have the EXACT same index, for both language.
            'I'm just storing that index into the Word class, and then retrieving that word.
            Case "France"
                Dim _Index As Integer = WordToGuess.Index
                Dim TranslatedWord As String = Split(My.Resources.WordsFrench, Environment.NewLine)(_Index)
                Return New Word With {.Index = _Index, .Word = TranslatedWord}
            Case Else
                Dim _Index As Integer = WordToGuess.Index
                Dim TranslatedWord As String = Split(My.Resources.WordsEnglish, Environment.NewLine)(_Index)
                Return New Word With {.Index = _Index, .Word = TranslatedWord}
        End Select
    End Function

    Private Sub HandleUpdateScoresAndSetNewDrawer(DataReceived As Data)
        'All the score update is handled server side.
        'Therefore, here's i'm just clearing the whole listview, and adding again all the player with their new score

        LVUserConnectedToParty.Items.Clear()
        Dim PartyCreator As User = DataReceived.PartyConnectedTo.Creator
        If Not GM.Player.Username = PartyCreator.Username Then
            LVUserConnectedToParty.Items.Add(New ListViewItem(New String() {PartyCreator.Username, CStr(PartyCreator.Score)}))
        Else
            LblScore.Text = CStr(PartyCreator.Score)
        End If

        For Each User As User In DataReceived.PartyConnectedTo.UsersConnected
            If Not User.Username = GM.Player.Username Then
                LVUserConnectedToParty.Items.Add(New ListViewItem(New String() {User.Username, CStr(User.Score)}))
            Else
                LblScore.Text = CStr(User.Score)
            End If
        Next

        If DataReceived.WordToGuess IsNot Nothing AndAlso Not DataReceived.WordToGuess.Found Then
            LogMessage("You all failed, the word was: ""{0}""", TranslateWord(DataReceived.WordToGuess, GM.Player.Location).Word)
        End If

        'Set the new drawer
        If DataReceived.WordToGuess IsNot Nothing AndAlso GM.Player.Username = DataReceived.PartyConnectedTo.CurrentDrawer.Username Then

            GM.PartyConnectedTo.CurrentDrawer = GM.Player

            CT.Reset()
            TBWordToGuess.Clear()
            LblWarnWrongWord.Text = String.Empty
            If PBDrawing.Image IsNot Nothing Then PBDrawing.Image = Nothing
            LblWarnWrongWord.Text = String.Empty
            MakeUserEnableToStartTheGame()

            LogMessage("You're the new drawer! Go ahead!", GM.Player.Username)
        Else
            LogMessage("{0} is the new drawer, wait for him to start the party.", DataReceived.PartyConnectedTo.CurrentDrawer.Username)
        End If



    End Sub

    Private Sub HandleRealTimeDrawing(DataReceived As Data)
        Dim Layer1 As New Bitmap(PBDrawing.Width, PBDrawing.Height) 'Create a new image of the picturebox
        PBDrawing.DrawToBitmap(Layer1, New Rectangle(0, 0, Layer1.Width - 1, Layer1.Height - 1))
        PBDrawing.Image = Layer1 'Apply the created image to the picturebox

        GM.CurrentPen = DataReceived.Line.Pen

        'Add all the points received to the client side GraphicPath var, to draw them in the paint evenn
        GM.StartedDrawing = True
        GM.ListOfCircles.Clear()
        DrawingPoints.Clear()

        For Each C As Circle In DataReceived.Line.Points
            GM.ListOfCircles.Add(C)
        Next

        PBDrawing.Invalidate()
    End Sub

    Private Sub HandleOpponentAlreadyPlaying(DataReceived As Data)
        TopMost = True 'If the asked opponent is already playing, then this is fired.
        MessageBox.Show(String.Format("{0} is already playing. Try with someone else.", LVUserList.SelectedItems(0).Text))
        GM.Reset()
        TopMost = False
    End Sub
    Private Sub HandleOpponentDisconnected(DataReceived As Data)
        If DataReceived.PartyConnectedTo Is Nothing Then Exit Sub
        'Keep in mind that the creator of the party isn't listed in the UsersConnected property
        Dim PartyCreator As User = DataReceived.PartyConnectedTo.Creator

        If DataReceived.PartyConnectedTo.IsPrivate Then 'If the party if private, then it means that there's only two player
            MakeUserGoToMainMenuAndResetEverything() 'There we can directly make the user back to the main menu cause there's no opponent anymore.
            MessageBox.Show("Your opponent has just left the game. Back to the online user list.", "GuessMyDraw")
            Exit Sub
        End If

        'If the party is public, and if the user disconnected is the creator of the party, then delete the party.
        If Not DataReceived.PartyConnectedTo.IsPrivate AndAlso DataReceived.UserDisconnected = PartyCreator.Username Then
            If GM.PartyConnectedTo IsNot Nothing AndAlso GM.PartyConnectedTo.Creator.Username = PartyCreator.Username Then 'Show that only if you're connected to the party
                LVPartyCreated.Items.Remove(LVPartyCreated.FindItemWithText(DataReceived.PartyConnectedTo.Name))
                MakeUserGoToMainMenuAndResetEverything()
                MessageBox.Show("The creator of the party has just left the game. Back to the online user list.", "GuessMyDraw")
            Else 'Else, it just removes that party from the list of all parties.
                LVPartyCreated.Items.Remove(LVPartyCreated.FindItemWithText(DataReceived.PartyConnectedTo.Name))
            End If
            'Here, if the party is public, and if you're connected to it, then remove the users disconnected from the scoreboard.
        ElseIf Not DataReceived.PartyConnectedTo.IsPrivate Then
            LVUserConnectedToParty.Items.Remove(LVUserConnectedToParty.FindItemWithText(DataReceived.UserDisconnected))
            GM.PartyConnectedTo.UsersConnected.RemoveAll(Function(x) x.Username = DataReceived.UserDisconnected)
        End If


        'Here, if everyone has left the party, and if you're the creator of it, then make the user wait for new user to join.
        If DataReceived.PartyConnectedTo.UsersConnected.Count = 0 AndAlso GM.Player.Username = PartyCreator.Username Then
            MessageBox.Show("All users have left the party. Now waiting for new users to join.", "GuessMyDraw")
            MakeUserWaitingForGameToStart()
        End If

        TabPageParty.Text = String.Format("Public Party ({0})", LVPartyCreated.Items.Count)
        LogMessage("Player ""{0}"" has just left the party.", DataReceived.UserDisconnected)
    End Sub
    Private Sub HandleDrawerDisconnected(DataReceived As Data)
        'Here, i'm handling things differently when the user disconnected is the one who was supposed to draw
        'Cause we needed to choose a new drawer randomly. Everything's handled server side tho.
        Dim NewDrawer As User = DataReceived.PartyConnectedTo.CurrentDrawer
        Dim Party As Party = DataReceived.PartyConnectedTo

        If GM.Player.Username = DataReceived.UserDisconnected Then Exit Sub
        If GM.PartyConnectedTo IsNot Nothing Then
            GM.PartyConnectedTo.CurrentDrawer = NewDrawer
            GM.PartyConnectedTo.UsersConnected.Clear()
            For Each U As User In Party.UsersConnected
                GM.PartyConnectedTo.UsersConnected.Add(U)
            Next
        End If

        Dim LVT As ListViewItem = LVUserConnectedToParty.FindItemWithText(DataReceived.UserDisconnected)
        If LVT IsNot Nothing Then LVT.Remove() 'Start by removing the user disconnected from the scoreboard

        'If the user disconnceted was the last one of the party, then wait for new users to join
        If GM.Player.Username = DataReceived.PartyConnectedTo.Creator.Username AndAlso DataReceived.PartyConnectedTo.UsersConnected.Count = 0 Then
            MakeUserWaitingForGameToStart()
            MessageBox.Show("All users have left the party. Now waiting for new users to join.", "GuessMyDraw")
            Exit Sub
        End If

        If GM.Player.Username = NewDrawer.Username Then 'If you're the one choosen randomly
            MakeUserEnableToStartTheGame()
            TopMost = True
            MessageBox.Show("The drawer has just left the party. You've been chosen randomly.", "GuessMyDraw")
            TopMost = False
        Else
            MakeUserWaitingForGameToStart()
            LogMessage("The drawer ""{0}"" has just left the party. Wait for the new drawer ""{1}"" to start the party", DataReceived.Player.Username, NewDrawer.Username)
        End If

    End Sub

    Private Sub HandleClearDrawing() 'Clear the picturebox if the drawer cleared it
        If PBDrawing.Image IsNot Nothing Then PBDrawing.Image = Nothing
        GM.StartedDrawing = False
    End Sub

#End Region
#Region "Party Handlers"

    Private Sub HandleAllPartyReceived(DataReceived As Data)
        Dim Username As String = GM.Player.Username
        GM.AllPartyCreated = DataReceived.AllPartyCreated

        LVPartyCreated.Items.Clear()
        For Each PartyCreated As Party In GM.AllPartyCreated
            Dim NewParty As New ListViewItem(New String() {PartyCreated.Name, PartyCreated.Creator.Username}) With {.Tag = PartyCreated}
            LVPartyCreated.Items.Add(NewParty)
        Next

        TabPageParty.Text = String.Format("Public Party ({0})", LVPartyCreated.Items.Count)

    End Sub 'Fired when a new party is created
    Private Sub HandleReceiveAllUsersConnectToParty(DataReceived As Data)
        Dim Username As String = GM.Player.Username
        Dim UsersConnected As List(Of User) = DataReceived.PartyConnectedTo.UsersConnected
        LVUserConnectedToParty.Items.Clear()

        GM.PartyConnectedTo.UsersConnected.Clear() 'Clear the list of current 

        Dim Creator As User = DataReceived.PartyConnectedTo.Creator 'Define the creator of the party
        If Not Creator.Username = GM.Player.Username Then LVUserConnectedToParty.Items.Add(New ListViewItem(New String() {Creator.Username, CStr(Creator.Score)}))
        GM.PartyConnectedTo.Creator = Creator


        For Each UserConnect As User In UsersConnected 'And all the users connected
            If Not UserConnect.Username = GM.Player.Username Then LVUserConnectedToParty.Items.Add(New ListViewItem(New String() {UserConnect.Username, CStr(UserConnect.Score)}))
            GM.PartyConnectedTo.UsersConnected.Add(UserConnect)
        Next

        If UsersConnected.Count = 1 AndAlso GM.Player.Username = Creator.Username Then 'If there's only one player connected
            MessageBox.Show("A user joined your party. You can now start the game.")
            MakeUserEnableToStartTheGame()
        End If

        If Not GM.Player.Username = DataReceived.Player.Username Then
            LogMessage("Player ""{0}"" has just connected to the party.", DataReceived.Player.Username)
        Else
            LogMessage("You're now connected to the party !")
        End If

    End Sub 'This is fired when you connect to a party or when a new user is joining. It gives you all the users already connected to a party.
    Private Sub HandleCreateNewPrivateParty(DataReceived As Data)
        Dim Opponent As User = DataReceived.Player
        Select Case DataReceived.Commander.Request
            Case Request.Ask 'Opponent form

                If GM.AlreadyReceivedARequest Then 'If you already received a request, then tell the player to avoid getting spammed of request
                    SendAlreadyReceivedARequest(Opponent, DataReceived.PartyConnectedTo)
                    Exit Sub
                ElseIf GM.Playing Then 'If you're already playing, then tell it to the user asking, and avoid all the shit below.
                    SendReponseAlreadyPlaying(Opponent, DataReceived.PartyConnectedTo)
                    Exit Sub
                End If

                GM.AlreadyReceivedARequest = True : TopMost = True
                If MessageBox.Show(String.Format("{0} would like to play with you.", Opponent.Username), "GuessMyDraw", MessageBoxButtons.YesNo) = Forms.DialogResult.Yes Then
                    GM.AlreadyReceivedARequest = False
                    GM.PartyConnectedTo = DataReceived.PartyConnectedTo 'Set the new party you just accepted.
                    GM.PartyConnectedTo.CurrentDrawer = DataReceived.Player 'Set the current drawer of that party
                    LVUserConnectedToParty.Items.Add(New ListViewItem(New String() {GM.PartyConnectedTo.Creator.Username, CStr(GM.PartyConnectedTo.Creator.Score)})) 'Add the player to the scoreboard
                    SendResponseToBeginGame(Answer.Accept)
                Else 'Send whether you accepted or refused to play
                    GM.AlreadyReceivedARequest = False
                    GM.PartyConnectedTo = DataReceived.PartyConnectedTo
                    SendResponseToBeginGame(Answer.Deny)
                    GM.Reset()
                End If
                TopMost = False

            Case Request.Anwser 'Player form

                TopMost = True
                If DataReceived.Commander.Answer = Answer.Accept Then 'If the user accepted your request
                    MessageBox.Show(String.Format("{0} accepted to play with you.", Opponent.Username), "GuessMyDraw", MessageBoxButtons.OK)
                    GM.PartyConnectedTo = DataReceived.PartyConnectedTo
                    GM.PartyConnectedTo.CurrentDrawer = GM.Player
                    LVUserConnectedToParty.Items.Add(New ListViewItem(New String() {GM.PartyConnectedTo.UsersConnected(0).Username, CStr(GM.PartyConnectedTo.UsersConnected(0).Score)}))
                    SendConfirmationToBeginGameAndInitializeGame()
                Else 'If the user refused
                    GM.Reset()
                    MessageBox.Show(String.Format("{0} did not accept your request.", Opponent.Username))
                End If
                TopMost = False

            Case Request.Confirmation 'Opponent form
                InitializeGame()
        End Select
    End Sub 'This handles the creation of a new private party between two users.
    Private Sub HandleRequestAlreadySent(DataReceived As Data)
        MessageBox.Show(String.Format("{0} already received a request. Wait for him to accept it before.", LVUserList.SelectedItems(0).Text), "GuessMyDraw")
    End Sub
#End Region

#Region "Drawing Handlers"

    Private myPenWidth As Integer = 3 'Pen width variable
    Private DrawingPoints As New List(Of Circle) 'That list contains all the points sent to the players

    Private OldPoint As PointX
    Private CurrentPoint As PointX

    Private Sub PBDrawing_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PBDrawing.MouseDown
        If e.Button = MouseButtons.Left Then
            GM.StartedDrawing = True
        Else
            GM.StartedDrawing = True
            GM.CurrentPen = New Pen(New ColorX(PBDrawing.BackColor), myPenWidth)
        End If
    End Sub


    Private Sub PBDrawing_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PBDrawing.MouseMove
        OldPoint = CurrentPoint
        CurrentPoint = New PointX(e.X, e.Y)

        If e.Button = Windows.Forms.MouseButtons.Left OrElse e.Button = Windows.Forms.MouseButtons.Right Then
            GM.ListOfCircles.Add(New Circle(OldPoint, CurrentPoint))
            DrawingPoints.Add(New Circle(OldPoint, CurrentPoint))
        End If

        PBDrawing.Invalidate()
    End Sub


    Private Sub PBDrawing_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PBDrawing.MouseUp
        Dim Layer1 As New Bitmap(PBDrawing.Width, PBDrawing.Height)
        PBDrawing.DrawToBitmap(Layer1, New Rectangle(0, 0, Layer1.Width, Layer1.Height))
        PBDrawing.Image = Layer1
        GM.StartedDrawing = False


        SendRealTimeDrawing(DrawingPoints) 'Then, when the user mouse up, I send these points.
        GM.CurrentPen = New Pen(New ColorX(ColorPickerControl1.SelectedColor), myPenWidth)

        GM.ListOfCircles.Clear()
        DrawingPoints.Clear()
    End Sub

    Private Sub PBDrawing_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PBDrawing.Paint

        If GM.StartedDrawing Then
            For Each C As Circle In GM.ListOfCircles

                Dim PointA As PointX = C.Point1
                Dim PointB As PointX = C.Point2

                Dim DeltaX As Integer = PointB.X - PointA.X
                Dim DeltaY As Integer = PointB.Y - PointA.Y

                Dim DrawStep As Integer = 30

                For i As Integer = 0 To DrawStep
                    Dim P As Point = New Point(CInt(PointA.X + (DeltaX * (i / DrawStep))), CInt(PointA.Y + (DeltaY * (i / DrawStep))))
                    e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                    e.Graphics.FillEllipse(New SolidBrush(GM.CurrentPen.Color.ToColor), New Rectangle(P.X - 5, P.Y - 5, CInt(GM.CurrentPen.Width), CInt(GM.CurrentPen.Width)))
                    e.Graphics.DrawEllipse(GM.CurrentPen.ToPen(), New Rectangle(P.X - 5, P.Y - 5, CInt(GM.CurrentPen.Width), CInt(GM.CurrentPen.Width)))
                Next
            Next
        End If

    End Sub
    Private Sub TrackBar1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrackBarWidth.ValueChanged
        myPenWidth = TrackBarWidth.Value 'Change the value of the Pen width
        If GM IsNot Nothing Then GM.CurrentPen = New Pen(New ColorX(GM.CurrentPen.Color.ToColor), myPenWidth)
    End Sub

    Private Sub ColorPickerControl1_ColorPicked(sender As Object, e As EventArgs) Handles ColorPickerControl1.ColorPicked
        GM.CurrentPen = New Pen(New ColorX(ColorPickerControl1.SelectedColor), myPenWidth) 'Change the color of the pen
    End Sub
#End Region

#Region "Local Game Method"
    Private Sub GenerateRandomWord(_Location As String) 'Generate a new word, french or english, based on your location
        Dim _Index As Integer
        Dim _Word As String

        If _Location = "France" Then
            _Index = New Random().Next(0, Split(My.Resources.WordsFrench, Environment.NewLine).Length - 1)
            _Word = Split(My.Resources.WordsFrench, Environment.NewLine)(_Index)
        Else
            _Index = New Random().Next(0, Split(My.Resources.WordsEnglish, Environment.NewLine).Length - 1)
            _Word = Split(My.Resources.WordsEnglish, Environment.NewLine)(_Index)
        End If

        LblWordToGuess.Text = _Word
        GM.WordToGuess = New Word With {.Index = _Index, .Word = _Word}
    End Sub
    Private Function MakeWordHidden(WordToHide As Word) As String 'Ugly function that convert a word to something "A _ _ _ _ _". Won't explain it.
        Dim FinalText As String = String.Empty

        FinalText = WordToHide.FirstLetters.Chars(0) & " "

        For I As Integer = 1 To WordToHide.Length - 1
            FinalText &= "_ "
        Next

        Return FinalText
        'If Text.Contains(" ") Then
        '    Dim SplittedText() As String = Text.Split(" "c)

        '    FinalText &= SplittedText(0).Chars(0) & " "

        '    For Each C As Char In SplittedText(0).Substring(1, SplittedText(0).Length - 1)
        '        FinalText &= "_" & " "
        '    Next

        '    For i = 1 To SplittedText.Length - 1
        '        FinalText &= "  "
        '        For Each C As Char In SplittedText(i)

        '            If Not C = " " AndAlso Not C = "'" Then
        '                FinalText &= "_" & " "
        '            ElseIf C = "'" Then
        '                FinalText &= "'"
        '            End If

        '        Next
        '        FinalText &= " "
        '    Next

        'Else
        '    FinalText &= Text.Chars(0) & " "
        '    For Each C As Char In Text.Substring(1, Text.Length - 1)
        '        FinalText &= "_" & " "
        '    Next
        'End If

        'Return FinalText
    End Function
    Private Function GiveSecondLetter(Word As String) As String 'Give the second letter on the hidden text "A _ _ _ _"
        Dim SB As New StringBuilder(Word)
        SB(2) = GM.WordToGuess.FirstLetters(1)
        Return SB.ToString
    End Function

    Private Const initVector As String = "tu89geji340t89u2"
    Private Const keysize As Integer = 256
    Public Function Handshake(cipherText As String, passPhrase As String) As String
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim cipherTextBytes As Byte() = Convert.FromBase64String(cipherText)
        Dim password As New PasswordDeriveBytes(passPhrase, Nothing)
        Dim keyBytes As Byte() = password.GetBytes(keysize \ 8)
        Dim symmetricKey As New RijndaelManaged()
        symmetricKey.Mode = CipherMode.CBC
        Dim decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)
        Dim memoryStream As New MemoryStream(cipherTextBytes)
        Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
        Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}
        Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)
        memoryStream.Close()
        cryptoStream.Close()
        Return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)
    End Function

    'The rest of the method below, enable/disable a lot of controls to make the users able/unable to draw/guess, start/wait the game.
    'Quite ugly, but necessary
    Public Sub MakeUserEnableToDraw()
        GBDrawing.Enabled = True
        PBDrawing.Enabled = True
        BtnClearDrawing.Enabled = True
        BtnEraseDrawing.Enabled = True
        GBWordGuess.Enabled = False
        BtnStartGame.Enabled = False
        TBWordToGuess.Text = String.Empty
        LblGameStart.Visible = False
        LblLenghtOfWordToGuess.Text = String.Empty
        GM.ListOfCircles.Clear()
        PBDrawing.Image = Nothing
        GM.CurrentPen = New Pen(New ColorX(Color.Black), myPenWidth)
    End Sub
    Public Sub MakeUserEnableToGuess()
        GBDrawing.Enabled = False
        GBWordGuess.Enabled = True
        BtnStartGame.Enabled = False
        LblGameStart.Visible = False
        TBWordToGuess.Text = GM.WordToGuess.FirstLetters.Chars(0)
        GM.ListOfCircles.Clear()
        PBDrawing.Image = Nothing
        BtnGenerateWord.Enabled = False
    End Sub
    Public Sub MakeUserEnableToStartTheGame()
        GM.PartyConnectedTo.CurrentDrawer = GM.Player
        PBDrawing.Image = Nothing
        GBDrawing.Enabled = True
        BtnStartGame.Enabled = True
        BtnClearDrawing.Enabled = False
        BtnEraseDrawing.Enabled = False
        BtnGenerateWord.Enabled = True
        PBDrawing.Enabled = False
        GBWordGuess.Enabled = False
        LblLenghtOfWordToGuess.Text = String.Empty
        TBWordToGuess.Text = String.Empty
        GM.ListOfCircles.Clear()
        GM.RandomWordRemaining = 5
        LblHiddenText.Text = String.Empty
        BtnGenerateWord.Text = String.Format("Generate word ({0} remainings)", GM.RandomWordRemaining)
        GenerateRandomWord(GM.Player.Location)
        CT.Reset() : CTStartGame.Reset()
    End Sub
    Public Sub MakeUserWaitingForGameToStart()
        GBDrawing.Enabled = False
        GBWordGuess.Enabled = False
        BtnStartGame.Enabled = False
        LblLenghtOfWordToGuess.Text = String.Empty
        TBWordToGuess.Text = String.Empty
        GM.ListOfCircles.Clear()
        PBDrawing.Image = Nothing
        BtnGenerateWord.Enabled = False
        CT.Reset() : CTStartGame.Reset()
    End Sub
    Public Sub MakeUserGoToMainMenuAndResetEverything()
        CType(TabPageGame, Control).Enabled = False
        LblScore.Text = "0"
        TBPartyReceivedMessage.Clear()
        LblGameStart.Visible = False
        CTStartGame.Reset()
        CT.Reset()
        LVUserConnectedToParty.Items.Clear()
        If GM.PartyConnectedTo IsNot Nothing AndAlso GM.Player.Username = GM.PartyConnectedTo.Creator.Username Then LVPartyCreated.Items.Remove(LVPartyCreated.FindItemWithText(GM.PartyConnectedTo.Name))
        TabPageParty.Text = String.Format("Public Party ({0})", LVPartyCreated.Items.Count)
        GM.Reset()
        MainTabControl.SelectTab(0)
    End Sub
#End Region
#Region "Other Handlers"
    Private Sub CreatePrivatePartyHandler(sender As Object, e As EventArgs) Handles LVUserList.DoubleClick
        If String.IsNullOrEmpty(LVUserList.SelectedItems(0).Text) Then Exit Sub
        Dim OpponentUser As New User(LVUserList.SelectedItems(0).Text, LVUserList.SelectedItems(0).SubItems(1).Text)
        Dim NewParty As New Party(Guid.NewGuid.ToString, GM.Player, True)
        SendRequestToBeginGame(NewParty, OpponentUser)
    End Sub 'When you double click on a player, it send him a request to begin a new game.
    Private Sub CreatePublicPartyHandler(sender As Object, e As EventArgs) Handles BtnCreateParty.Click
        Dim PartyName As String = InputBox("Insert the name of your party", "GuessMyDraw")
        If PartyName.Trim() = String.Empty Then Exit Sub
        SendNewParty(PartyName, GM.Player, False) 'Send the new party to the users connected, and define yourself as the creator
        GM.PartyConnectedTo.CurrentDrawer = GM.Player
        InitializeGame()
        MakeUserWaitingForGameToStart()
        MessageBox.Show("Now waiting for users to join your party.", "GuessMyDraw")
    End Sub  'Create a new public party
    Private Sub JoinPublicPartyHandler(sender As Object, e As EventArgs) Handles LVPartyCreated.DoubleClick
        Dim PartyToConnect As Party = DirectCast(LVPartyCreated.SelectedItems(0).Tag, Party)
        SendConnectionToParty(GM.Player, PartyToConnect)
        InitializeGame()
        MakeUserWaitingForGameToStart()
    End Sub 'Join a public party

    Private Sub ChangeTabHandler(sender As Object, e As EventArgs) Handles MainTabControl.SelectedIndexChanged
        'If the user is playing, and choose the user list tab, then ask if he wants to leave the party.
        'If he does, then warn the others users that he's leaving, and reset all the vars of the GameManager class.
        If MainTabControl.SelectedTab Is TabPageUserList AndAlso GM.Playing Then
            If MessageBox.Show("Do you really want to quit the current game ?", "GuessMyDraw", MessageBoxButtons.YesNo) = Forms.DialogResult.Yes Then
                SendUserDisconnectedFromParty()
                MakeUserGoToMainMenuAndResetEverything()
            Else
                MainTabControl.SelectTab(1)
            End If
        End If

    End Sub

    Private Sub StartNewGameHandler(sender As Object, e As EventArgs) Handles BtnStartGame.Click
        BtnGenerateWord.Enabled = False
        SendRequestToLaunchGame(GM.WordToGuess, GM.Player)
    End Sub 'Send the request the start the game

    Private Sub SendWordFromButton(sender As Object, e As EventArgs) Handles BtnGuessTheDraw.Click
        'SendWordIfFound()
        SendWordToGuess(TBWordToGuess.Text)
    End Sub 'Send the word found to the others
    Private Sub SendWordFromShortcut(sender As Object, e As KeyEventArgs) Handles TBWordToGuess.KeyDown
        If e.KeyData = Keys.Enter Then
            e.SuppressKeyPress = True
            SendWordToGuess(TBWordToGuess.Text)
        End If
    End Sub

    Private Sub TBSendChatMessage_KeyDown(sender As Object, e As KeyEventArgs) Handles TBPartySendChatMessage.KeyDown
        If e.KeyData = Keys.Enter AndAlso Not String.IsNullOrEmpty(TBPartySendChatMessage.Text) Then
            e.SuppressKeyPress = True
            SendChatMessageToParty(TBPartySendChatMessage.Text)
        End If
    End Sub 'Send a chat message to a party

    Private Sub TBPublicChatSend_KeyDown(sender As Object, e As KeyEventArgs) Handles TBPublicChatSend.KeyDown
        If e.KeyData = Keys.Enter AndAlso Not String.IsNullOrEmpty(TBPublicChatSend.Text) Then
            e.SuppressKeyPress = True
            SendChatMessageToPublicChat(TBPublicChatSend.Text)
        End If
    End Sub

    Private Sub TBPublicChatSendFromParty_KeyDown(sender As Object, e As KeyEventArgs) Handles TBPublicChatSendFromParty.KeyDown
        If e.KeyData = Keys.Enter AndAlso Not String.IsNullOrEmpty(TBPublicChatSendFromParty.Text) Then
            e.SuppressKeyPress = True
            SendChatMessageToPublicChat(TBPublicChatSendFromParty.Text)
        End If
    End Sub

    Private Sub BtnClearDrawing_Click(sender As Object, e As EventArgs) Handles BtnClearDrawing.Click
        If PBDrawing.Image IsNot Nothing Then PBDrawing.Image = Nothing
        GM.ListOfCircles.Clear()
        SendClearImage()
    End Sub 'Clear the drawing


    'Not used anymore, because of the custom color picker. Decided to leave them anyway. They're hidden behind the color picker.
    Private Sub PBBlack_Click(sender As Object, e As EventArgs) Handles PBBlack.Click
        GM.CurrentPen = New Pen(New ColorX(PBBlack.BackColor), myPenWidth)
    End Sub
    Private Sub PBGreen_Click(sender As Object, e As EventArgs) Handles PBGreen.Click
        GM.CurrentPen = New Pen(New ColorX(PBGreen.BackColor), myPenWidth)
    End Sub
    Private Sub PBRed_Click(sender As Object, e As EventArgs) Handles PBRed.Click
        GM.CurrentPen = New Pen(New ColorX(PBRed.BackColor), myPenWidth)
    End Sub
    Private Sub PBYellow_Click(sender As Object, e As EventArgs) Handles PBYellow.Click
        GM.CurrentPen = New Pen(New ColorX(PBYellow.BackColor), myPenWidth)
    End Sub
    Private Sub PBBlue_Click(sender As Object, e As EventArgs) Handles PBBlue.Click
        GM.CurrentPen = New Pen(New ColorX(PBBlue.BackColor), myPenWidth)
    End Sub
    Private Sub BtnErase_Click(sender As Object, e As EventArgs) Handles BtnEraseDrawing.Click
        GM.CurrentPen = New Pen(New ColorX(PBDrawing.BackColor), myPenWidth)
    End Sub

    Private Sub BtnGenerateWord_Click(sender As Object, e As EventArgs) Handles BtnGenerateWord.Click
        GM.RandomWordRemaining -= 1
        If GM.RandomWordRemaining >= 0 Then
            GenerateRandomWord(GM.Player.Location)
            BtnGenerateWord.Text = String.Format("Generate word ({0} remainings)", GM.RandomWordRemaining)
        Else
            BtnGenerateWord.Enabled = False
        End If
    End Sub 'Generate a new word

    Private Sub LblScore_TextChanged(sender As Object, e As EventArgs) Handles LblScore.TextChanged
        LblScore2.Text = LblScore.Text
    End Sub

    Private Sub GuessMyDraw_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Client.Disconnect()
        Application.Exit()
    End Sub 'Disconnect if you close the form

    Private Sub TabControlGame_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControlGame.SelectedIndexChanged
        If TabControlGame.SelectedIndex = 2 Then
            TabPagePublicChatFromParty.Text = "Public Chat"
        End If
    End Sub

    Private Sub TabControlUsersOnline_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControlUsersOnline.SelectedIndexChanged
        If TabControlUsersOnline.SelectedIndex = 2 Then
            TabPagePublicChat.Text = "Public Chat"
        End If
    End Sub

#End Region

    Private cursorPoint As System.Drawing.Point
    Private oldCursorPoint As System.Drawing.Point
    Private minutesIdle As Integer = 0

    Private Function IsIdle(minutes As Integer) As Boolean
        If minutesIdle >= minutes Then Return True Else Return False
    End Function
    Private Sub frmConnect_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        cursorPoint = e.Location
    End Sub
    Private Sub tmrIdle_Tick(sender As Object, e As EventArgs) Handles tmrIdle.Tick
        '    If oldCursorPoint <> cursorPoint Then
        '        minutesIdle = 0
        '    Else
        '        minutesIdle += 1
        '        If IsIdle(3) AndAlso GM.PartyConnectedTo IsNot Nothing Then
        '            SendUserDisconnectedFromParty()
        '            MakeUserGoToMainMenuAndResetEverything()
        '            minutesIdle = 0
        '            MessageBox.Show("You've been kicked from the party for being away.")
        '        ElseIf IsIdle(2) AndAlso GM.PartyConnectedTo IsNot Nothing Then
        '            TopMost = True
        '            MessageBox.Show("Warning. You're gonna be kicked in a minute if you don't get online again.")
        '            TopMost = False
        '        End If
        '    End If
        '    oldCursorPoint = cursorPoint
    End Sub
End Class