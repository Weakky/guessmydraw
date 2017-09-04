Option Strict On
Imports System.Text
Imports System.IO
Imports System.Collections.Specialized
Imports GuessMyDraw_Server.Commands
Imports System.Security.Cryptography
Imports ProtoBuf
Imports System.Reflection

Module ServerConsole

    'Project started: 19/12/13
    'Beta: 26/12/13
    'Release: ???

    Private WithEvents Server As New ServerListener 'This handle the whole server. Thanks aeon <3
    Private UsersList As Dictionary(Of User, ServerClient)  'This is a dictionnary that contains all users class as key, and their socket connection as value. I use this to identify each client easily.
    Private PartyList As List(Of Party) 'And here's the list of all party created.


#Region " Helper Methods "
    Private Function SerializeData(ByVal Data As Data) As Byte()
        Using ms = New MemoryStream()
            Serializer.Serialize(ms, Data)
            Return ms.ToArray()
        End Using
    End Function

    Private Sub LogMessage(ByVal message As String, ParamArray args As Object())
        Console.WriteLine(String.Format(message, args))
    End Sub

    'We can send data to every connected client.
    Private Sub Broadcast(data As Byte())
        For Each C As ServerClient In Server.Clients
            C.Send(data)
        Next
    End Sub
    'We can do the same as above, but exclude a specific client like this.
    Private Sub BroadcastExclude(client As ServerClient, data As Byte())
        For Each C As ServerClient In Server.Clients
            If C IsNot client Then C.Send(data)
        Next
    End Sub

    'Send to a specific user
    Private Sub SendToUser(ByVal user As User, data As Byte())
        For Each UserConnect As KeyValuePair(Of User, ServerClient) In UsersList
            If UserConnect.Key.Username = user.Username Then
                UserConnect.Value.Send(data)
                Exit For
            End If
        Next
    End Sub
    'Broadcast to everyone, excluding a user
    Private Sub BroadcastExcludeUser(user As User, DataReceived As Data)
        For Each ConnectedUser As KeyValuePair(Of User, ServerClient) In UsersList
            If Not ConnectedUser.Key.Username = user.Username Then
                ConnectedUser.Value.Send(SerializeData(DataReceived))
            End If
        Next
    End Sub

    'Broadcast to a whole party
    Private Sub BroadcastToParty(PartyToSend As Party, data As Byte())
        If PartyList.Exists(Function(x) x.ID = PartyToSend.ID) Then
            SendToUser(PartyToSend.Creator, data)
            For Each User As User In PartyToSend.UsersConnected
                SendToUser(User, data)
            Next
        End If
    End Sub

    'Broadcast to a whole party, excluding a user
    Private Sub BroadcastToPartyExclude(ByVal sender As User, ByVal PartyToSend As Party, data As Byte())
        'Make sure the list exists
        If PartyList.Exists(Function(x) x.ID = PartyToSend.ID) Then

            'Send to creator of the party
            If Not sender.Username = PartyToSend.Creator.Username Then
                SendToUser(PartyToSend.Creator, data)
            End If
            'Then send to all the others
            For Each UserConnected As User In PartyToSend.UsersConnected
                If Not sender.Username = UserConnected.Username Then
                    SendToUser(UserConnected, data)
                End If
            Next
        End If
    End Sub
    Private Sub BroadcastToPartyExclude(ByVal sender As User, ByVal PartyToSend As Party, DataReceived As Data)
        If PartyList.Exists(Function(x) x.ID = PartyToSend.ID) Then
            'Send to creator of the party
            If Not sender.Username = PartyToSend.Creator.Username Then
                SendToUser(PartyToSend.Creator, SerializeData(DataReceived))
            End If
            'Then send to all the others
            For Each UserConnected As User In PartyToSend.UsersConnected
                If Not sender.Username = UserConnected.Username Then
                    SendToUser(UserConnected, SerializeData(DataReceived))
                End If
            Next
        End If
    End Sub
#End Region

    Sub Main()
        Server.MaxConnections = 50 'Maximum number of clients to allow to connect at once. Default is 20.
        Server.KeepAlive = True 'Keep idle sockets from disconnecting automatically. Default is True.
        Server.BufferSize = 8192 'Default amount of data to send / receive on sockets, per chunk, in bytes. Default is 8KB.
        Server.MaxPacketSize = 10485760 'Default size limit of packets, in bytes. Default is 10MB.
        Server.Listen(1234) 'Begin listening for connections on the specified port.
        UsersList = New Dictionary(Of User, ServerClient)
        PartyList = New List(Of Party)
        Console.ReadKey()
    End Sub


    Private Sub Server_ClientReadPacket(sender As ServerListener, client As ServerClient, data() As Byte) Handles Server.ClientReadPacket
        'Here's where we receive all the data.
        Dim DataReceived As Data
        Using ms = New MemoryStream(data)
            DataReceived = Serializer.Deserialize(Of Data)(ms) 'Deserialize the received data, and handle everything based on the command sent
            Select Case DataReceived.Commander.Command
                Case Command.ClientInformations
                    HandleClientInformations(client, DataReceived, data)
                Case Command.Party
                    HandleParty(DataReceived, data)
                Case Command.Game
                    HandleGame(DataReceived, data)
                Case Command.PublicChat
                    BroadcastExclude(client, data)
                Case Else
                    HandleDataToSend(DataReceived, data)
            End Select
        End Using
    End Sub

#Region "Main Handlers"
    Private Sub HandleClientInformations(client As ServerClient, DataReceived As Data, data() As Byte)
        If DataReceived.Key = ":fuckyourselfbitches:" Then
            Dim NewUser As User = DataReceived.Player
            UsersList.Add(NewUser, client)
            BroadcastExclude(client, data)
            LogMessage(">>>> User Connected with Username: {0}", NewUser.Username)
        Else
            Console.WriteLine("{0} has just tried to crack the protection.", DataReceived.Player.Username)
            client.Disconnect()
        End If
    End Sub 'Add a new user connected to the dictionnary
    Private Sub HandleDataToSend(DataReceived As Data, data() As Byte)
        BroadcastToPartyExclude(DataReceived.Player, DataReceived.PartyConnectedTo, data)
    End Sub 'Simply send back the data received to the clients
    Private Sub HandleParty(DataReceived As Data, data As Byte())
        Select Case DataReceived.Commander.Party
            Case PartyCommands.CreateNewParty
                HandleNewPartyCreated(DataReceived)
            Case PartyCommands.NewUserJoining
                HandleNewUserJoining(DataReceived)
            Case PartyCommands.AlreadyReceivedARequest
                HandleAlreadyReceivedARequest(DataReceived, data)
        End Select
    End Sub 'Handle everything about party
    Private Sub HandleGame(DataReceived As Data, data() As Byte)
        Select Case DataReceived.Commander.Game
            Case Game.AlreadyPlaying
                HandleAlreadyPlaying(DataReceived, data)
            Case Game.PlayerDisconnectedFromParty
                HandlePlayerDisconnectedFromParty(DataReceived)
            Case Game.LaunchGame
                HandleLaunchGameAndSetWord(DataReceived, data)
            Case Game.WordTry
                HandleWordTry(DataReceived, data)
            Case Game.WordFound
                HandleWordFound(DataReceived, data)
            Case Game.WordNotFound
                HandleWordNotFound(DataReceived, data)
            Case Else
                BroadcastToPartyExclude(DataReceived.Player, DataReceived.PartyConnectedTo, data)
        End Select
    End Sub 'And everything 'bout the game
#End Region

#Region "Party Handlers"
    Private Sub HandleNewPartyCreated(DataReceived As Data)
        Select Case DataReceived.Commander.Answer
            Case Answer.Deny 'If the user didn't want to start the party, then directly delete it
                SendNewPartyCreated(DataReceived)
                PartyList.RemoveAll(Function(x) x.ID = DataReceived.PartyConnectedTo.ID)
            Case Else
                If Not PartyList.Exists(Function(x) x.ID = DataReceived.PartyConnectedTo.ID) Then
                    PartyList.Add(DataReceived.PartyConnectedTo) 'Add the new party to the main list if it doesn't already exists
                    PartyList.Find(Function(x) x.ID = DataReceived.PartyConnectedTo.ID).CurrentDrawer = DataReceived.Player 'Set the creator as the current drawer
                End If
                SendNewPartyCreated(DataReceived) 'Send the new party created
        End Select
    End Sub
    Private Sub HandleNewUserJoining(DataReceived As Data)
        Dim SelectedParty As Party = DataReceived.PartyConnectedTo

        'Send new user to user connected to the party
        For Each Party As Party In PartyList
            If SelectedParty.ID = Party.ID Then
                Party.UsersConnected.Add(DataReceived.Player)
                DataReceived.PartyConnectedTo = Party

                'Send to creator of the party
                SendToUser(Party.Creator, SerializeData(DataReceived))

                'Send to the others one
                For Each PlayerConnectedToParty As User In Party.UsersConnected
                    SendToUser(PlayerConnectedToParty, SerializeData(DataReceived))
                Next
            End If
        Next
    End Sub
    Private Sub HandleAlreadyReceivedARequest(DataReceived As Data, data As Byte())
        SendToUser(DataReceived.Player, data)
    End Sub
#End Region

#Region "Game Handlers"
    Private Sub HandleAlreadyPlaying(DataReceived As Data, data() As Byte)
        'Send the data back to warn the user that he's already planing
        'Then delete the party just created
        PartyList.RemoveAll(Function(x) x.ID = DataReceived.PartyConnectedTo.ID)
        SendToUser(DataReceived.Player, data)
    End Sub

    Private Sub HandleLaunchGameAndSetWord(DataReceived As Data, data() As Byte)
        Select Case DataReceived.Commander.Request
            Case Request.Ask
                Dim PartyToUpdate As Party = PartyList.Find(Function(x) x.ID = DataReceived.PartyConnectedTo.ID)
                PartyToUpdate.WordToGuess = DataReceived.WordToGuess

                If Not PartyToUpdate.Creator.Username = DataReceived.Player.Username Then
                    Dim TranslatedWordToGuess As Word = TranslateWord(DataReceived.WordToGuess, PartyToUpdate.Creator.Location)
                    DataReceived.WordToGuess.FirstLetters = TranslatedWordToGuess.GetFirstLetters()
                    DataReceived.WordToGuess.Length = TranslatedWordToGuess.GetLength()
                    SendToUser(PartyToUpdate.Creator, SerializeData(DataReceived))
                End If

                For Each U As User In DataReceived.PartyConnectedTo.UsersConnected
                    If Not DataReceived.Player.Username = U.Username Then
                        Dim TranslatedWordToGuess As Word = TranslateWord(DataReceived.WordToGuess, U.Location)
                        DataReceived.WordToGuess.FirstLetters = TranslatedWordToGuess.GetFirstLetters()
                        DataReceived.WordToGuess.Length = TranslatedWordToGuess.GetLength()
                        SendToUser(U, SerializeData(DataReceived))
                    End If
                Next
            Case Else
                BroadcastToPartyExclude(DataReceived.Player, DataReceived.PartyConnectedTo, data)
        End Select
    End Sub

    Private Sub HandlePlayerDisconnectedFromPartyLeavingTheApp(PlayerDisconnected As User, Party As Party)
        Dim DataReceived As New Data(PlayerDisconnected, Party)
        DataReceived.Commander.Command = Command.Game
        DataReceived.Commander.Game = Game.PlayerDisconnectedFromParty

        DataReceived.UserDisconnected = PlayerDisconnected.Username

        'If the creator leave the party then delete it
        If Not Party.IsPrivate AndAlso PlayerDisconnected.Username = Party.Creator.Username Then
            BroadcastExcludeUser(DataReceived.Player, DataReceived)
            PartyList.RemoveAll(Function(x) x.ID = Party.ID)
            Exit Sub
        End If

        'User who was supposed to draw disconnected
        If Not Party.IsPrivate AndAlso Party.CurrentDrawer.Username = PlayerDisconnected.Username Then
            HandleDrawerDisconnected(DataReceived)
            Exit Sub
        End If

        'If the party is private then delete the party (cause there's only two players playing)
        If Party.IsPrivate Then
            BroadcastToPartyExclude(PlayerDisconnected, Party, DataReceived)
            PartyList.RemoveAll(Function(x) x.ID = Party.ID)

        Else 'Else, just remove the users from the connected list
            PartyList.Find(Function(x) x.ID = DataReceived.PartyConnectedTo.ID).UsersConnected.RemoveAll(Function(y) y.Username = PlayerDisconnected.Username)
            DataReceived.PartyConnectedTo = Party
            BroadcastToPartyExclude(PlayerDisconnected, Party, DataReceived)
        End If
    End Sub

    Private Sub HandlePlayerDisconnectedFromParty(DataReceived As Data)
        Dim PlayerDisconnected As User = DataReceived.Player
        Dim Party As Party = PartyList.Find(Function(x) x.ID = DataReceived.PartyConnectedTo.ID) 'Find the party in the main list
        DataReceived.UserDisconnected = PlayerDisconnected.Username

        'If the creator leave the party then delete it
        If Not Party.IsPrivate AndAlso PlayerDisconnected.Username = Party.Creator.Username Then
            BroadcastExcludeUser(DataReceived.Player, DataReceived)
            PartyList.RemoveAll(Function(x) x.ID = Party.ID)
            Exit Sub
        End If

        'User who was supposed to draw disconnected
        If Not Party.IsPrivate AndAlso Party.CurrentDrawer.Username = PlayerDisconnected.Username Then
            HandleDrawerDisconnected(DataReceived)
            Exit Sub
        End If

        'If the party is private then delete the party (cause there's only two players playing)
        If Party.IsPrivate Then
            BroadcastToPartyExclude(PlayerDisconnected, Party, DataReceived)
            PartyList.RemoveAll(Function(x) x.ID = Party.ID)

        Else 'Else, just remove the users from the connected list
            PartyList.Find(Function(x) x.ID = DataReceived.PartyConnectedTo.ID).UsersConnected.RemoveAll(Function(y) y.Username = PlayerDisconnected.Username)
            DataReceived.PartyConnectedTo = Party
            BroadcastToPartyExclude(PlayerDisconnected, Party, DataReceived)
        End If
    End Sub
    Private Sub HandleDrawerDisconnected(DataReceived As Data)
        Dim PlayerDisconnected As User = DataReceived.Player
        Dim Party As Party = PartyList.Find(Function(x) x.ID = DataReceived.PartyConnectedTo.ID)

        'Remove user disonnected from list
        Party.UsersConnected.RemoveAll(Function(x) x.Username = PlayerDisconnected.Username)

        'Make a new list with the creator inside.
        Dim ListWithCreator As New List(Of User) : ListWithCreator.Add(Party.Creator)
        For Each U As User In Party.UsersConnected
            ListWithCreator.Add(U)
        Next

        'Choose a new user randomly
        Dim RandomUser As User = ListWithCreator(New Random().Next(ListWithCreator.Count))
        DataReceived.Commander.Game = Game.DrawerDisconnected
        DataReceived.PartyConnectedTo.CurrentDrawer = RandomUser
        DataReceived.UserDisconnected = PlayerDisconnected.Username
        DataReceived.PartyConnectedTo = Party

        'Update new drawer
        PartyList.Find(Function(x) x.ID = DataReceived.PartyConnectedTo.ID).CurrentDrawer = RandomUser

        BroadcastToPartyExclude(PlayerDisconnected, Party, SerializeData(DataReceived))
    End Sub

    Private Sub HandleWordTry(DataReceived As Data, data() As Byte)
        Dim Party As Party = PartyList.Find(Function(x) x.ID = DataReceived.PartyConnectedTo.ID)
        Dim Player As User = DataReceived.Player
        Dim WordToGuess As String = DataReceived.WordToGuess.Word

        If DataReceived.Player.Username = Party.CurrentDrawer.Username Then Exit Sub

        If WordToGuess = TranslateWord(Party.WordToGuess, DataReceived.Player.Location).Word Then
            DataReceived.WordToGuess = Party.WordToGuess
            DataReceived.WordToGuess.Found = True
            HandleWordFound(DataReceived, data)
        Else
            DataReceived.WordToGuess.Found = False
            DataReceived.Commander.Game = Game.WordTry
            SendToUser(Player, SerializeData(DataReceived))
        End If
    End Sub
    Private Sub HandleWordFound(DataReceived As Data, data() As Byte)
        Dim PlayerWhoFoundTheWord As User = DataReceived.Player
        Dim Party As Party = PartyList.Find(Function(x) x.ID = DataReceived.PartyConnectedTo.ID)

        DataReceived.Commander.Game = Game.WordFound

        'Send to the party that the word has been found
        BroadcastToParty(Party, SerializeData(DataReceived))

        'And then update all scores
        HandleUpdateScoresWordFound(DataReceived)
    End Sub
    Private Sub HandleWordNotFound(DataReceived As Data, data() As Byte)
        HandleUpdateScoresWordNotFound(DataReceived)
    End Sub
    Private Sub HandleUpdateScoresWordFound(DataReceived As Data)
        'Using the data received from the HandleWordFound Sub
        DataReceived.Commander.Game = Game.UpdateScores

        'Update scores
        Dim PlayerWhoFoundTheWord As User = DataReceived.Player
        Dim CurrentDrawer As User = DataReceived.PartyConnectedTo.CurrentDrawer

        Dim PartyToUpdate As Party = PartyList.Find(Function(x) x.ID = DataReceived.PartyConnectedTo.ID)

        If PartyToUpdate.Creator.Username = DataReceived.Player.Username Then
            PartyToUpdate.Creator.Score += 2
        End If

        If PartyToUpdate.Creator.Username = PartyToUpdate.CurrentDrawer.Username Then
            PartyToUpdate.Creator.Score += 1
        End If

        For Each User As User In PartyToUpdate.UsersConnected
            If User.Username = DataReceived.Player.Username Then User.Score += 2 'User who found the word gets 2 points
            If User.Username = DataReceived.PartyConnectedTo.CurrentDrawer.Username Then User.Score += 1 'User who was the drawer gets 1 point
        Next

        'Update the new drawer
        'Create a new list with the creator of the party cause he's not on the users connected list
        Dim ListWithCreator As New List(Of User)
        For Each U As User In PartyToUpdate.UsersConnected
            ListWithCreator.Add(U)
        Next
        ListWithCreator.Add(PartyToUpdate.Creator)
        ListWithCreator.RemoveAll(Function(x) x.Username = PartyToUpdate.CurrentDrawer.Username)

        'As nobody found the word, choose a random user from the users connected to the party
        Dim RandomUser As User = ListWithCreator(New Random().Next(PartyToUpdate.UsersConnected.Count))

        PartyToUpdate.CurrentDrawer = RandomUser

        DataReceived.PartyConnectedTo = PartyToUpdate
        BroadcastToParty(DataReceived.PartyConnectedTo, SerializeData(DataReceived))
    End Sub
    Private Sub HandleUpdateScoresWordNotFound(DataReceived As Data)
        'Using the data received from the HandleWordNotFound Sub
        DataReceived.Commander.Game = Game.UpdateScores

        'NOTE: The drawer sent these data
        Dim PartyToUpdate As Party = PartyList.Find(Function(x) x.ID = DataReceived.PartyConnectedTo.ID)


        If DataReceived.Player.Username = PartyToUpdate.Creator.Username Then
            PartyToUpdate.Creator.Score -= 1
        Else
            'Remove one point to the drawer because no one guesses his drawing
            PartyToUpdate.UsersConnected.Find(Function(x) x.Username = DataReceived.Player.Username).Score -= 1
        End If

        'Update the new drawer
        'Create a new list with the creator of the party cause he's not on the users connected list
        Dim ListWithCreator As New List(Of User)
        For Each U As User In PartyToUpdate.UsersConnected
            ListWithCreator.Add(U)
        Next
        ListWithCreator.Add(PartyToUpdate.Creator)
        ListWithCreator.RemoveAll(Function(x) x.Username = PartyToUpdate.CurrentDrawer.Username)

        'As nobody found the word, choose a random user from the users connected to the party
        Dim RandomUser As User = ListWithCreator(New Random().Next(PartyToUpdate.UsersConnected.Count))

        PartyToUpdate.CurrentDrawer = RandomUser

        DataReceived.PartyConnectedTo = PartyToUpdate
        DataReceived.WordToGuess = PartyToUpdate.WordToGuess
        DataReceived.WordToGuess.Found = False

        BroadcastToParty(DataReceived.PartyConnectedTo, SerializeData(DataReceived))
    End Sub
#End Region

#Region "Send Data Handler"
    Private Sub SendUsersAndParty(client As ServerClient)
        Dim D As New Data : D.Commander.Command = Command.UsersAndPartyOnConnect
        For Each Player As KeyValuePair(Of User, ServerClient) In UsersList
            D.UsersConnected.Add(Player.Key)
        Next
        For Each Party As Party In PartyList
            If Not Party.IsPrivate Then D.AllPartyCreated.Add(Party)
        Next

        D.Key = StringCipher.Encrypt(":fuckyourselfbitches:", ":mabite:")
        client.Send(SerializeData(D))
    End Sub 'This send all users and party to the users connecting to the server
    Private Sub SendUserDisconnected(DisconnectedClient As ServerClient)
        Dim D As New Data : D.Commander.Command = Command.ClientDisconnected
        For Each Client As KeyValuePair(Of User, ServerClient) In UsersList
            If Client.Value Is DisconnectedClient Then
                D.UserDisconnected = Client.Key.Username
                UsersList.Remove(Client.Key)
                Exit For
            End If
        Next
        Broadcast(SerializeData(D))
    End Sub 'Send back to all clients when a user is disconnected
    Private Sub SendNewPartyCreated(DataReceived As Data)
        If DataReceived.PartyConnectedTo.IsPrivate Then
            BroadcastToPartyExclude(DataReceived.Player, DataReceived.PartyConnectedTo, SerializeData(DataReceived))
        Else
            Dim D As New Data()
            D.Commander.Command = Command.Party
            D.Commander.Party = PartyCommands.ReceiveAllParty
            D.AllPartyCreated = PartyList.FindAll(Function(x) x.IsPrivate = False)
            Dim Sender As User = DataReceived.Player
            BroadcastExcludeUser(Sender, D)
        End If
    End Sub 'Send the users a new party created
#End Region

    Private Function GenerateRandomWord(_Location As String) As Word  'Generate a new word, french or english, based on your location
        Dim _Index As Integer
        Dim _Word As String

        If _Location = "France" Then
            _Index = New Random().Next(0, Split(My.Resources.WordsFrench, Environment.NewLine).Length - 1)
            _Word = Split(My.Resources.WordsFrench, Environment.NewLine)(_Index)
        Else
            _Index = New Random().Next(0, Split(My.Resources.WordsEnglish, Environment.NewLine).Length - 1)
            _Word = Split(My.Resources.WordsEnglish, Environment.NewLine)(_Index)
        End If

        Return New Word With {.Index = _Index, .Word = _Word}
    End Function
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

    Private Sub Server_ClientWritePacket(sender As ServerListener, client As ServerClient, size As Integer) Handles Server.ClientWritePacket
        Debug.WriteLine(String.Format("Client Sent [{1}]: {0}", size, client.EndPoint))
    End Sub

    Private Sub Server_ClientStateChanged(sender As ServerListener, client As ServerClient, connected As Boolean) Handles Server.ClientStateChanged
        Console.WriteLine("Client Connected [{1}]: {0}", connected, client.EndPoint)
        If connected Then
            SendUsersAndParty(client) 'Broadcast when a user has just connect to the server
        End If
    End Sub

    Dim PlayerInParty As Boolean = False
    Private Sub Server_ClientExceptionThrown(sender As ServerListener, client As ServerClient, ex As Exception) Handles Server.ClientExceptionThrown
        Console.WriteLine("Client Exception [{1}]: {0}", ex, client.EndPoint)
        Debug.WriteLine(String.Format("Client Exception [{1}]: {0}", ex, client.EndPoint))

        'remove user from party and userslist
        client.Disconnect()

        For Each User As KeyValuePair(Of User, ServerClient) In UsersList
            If User.Value Is client Then
                For Each P As Party In PartyList
                    If P.UsersConnected.Exists(Function(x) x.Username = User.Key.Username) Or P.Creator.Username = User.Key.Username Then
                        HandlePlayerDisconnectedFromPartyLeavingTheApp(User.Key, P)
                        SendUserDisconnected(client)
                        PlayerInParty = True
                        Exit Sub
                    End If
                Next
            End If
        Next

        If PlayerInParty = False Then
            SendUserDisconnected(client)
        Else
            PlayerInParty = False
        End If

        Console.WriteLine("Client disconnected.")
    End Sub

    Private Sub Server_StateChanged(sender As ServerListener, listening As Boolean) Handles Server.StateChanged
        Console.WriteLine("Server Listening: {0}", listening)
    End Sub

    Private Sub Server_ExceptionThrown(sender As ServerListener, ex As Exception) Handles Server.ExceptionThrown
        Console.WriteLine("Server Exception: {0}", ex)
        Debug.WriteLine(String.Format("Server Exception: {0}", ex))
    End Sub

End Module