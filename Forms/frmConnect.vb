Imports System.Globalization
Imports System.IO
Imports GuessMyDraw.DataClass
Imports ProtoBuf
Imports System.Security.Cryptography
Imports System.Net
Imports System.Runtime.InteropServices

Public Class frmConnect

    Private WithEvents Client As UserClient
    Private WithEvents WC As New WebClient

    'I know that I'm doing something not really good here.
    'In order to avoid the use of a DB, everytime you connect,
    'I'm quickly checking if the username is already used,
    'and if not, I disconnect the user, load the main form, and connect again.
    'I'm just not really familiar with DB, and don't wanna bother managing one.

    'ns16132.c-dedie.net

#Region "AutoUpdater"
    Private Sub AutoUpdate()
        WC.DownloadStringAsync(New Uri("https://dl.dropboxusercontent.com/u/19643954/UpdateGuessMyDraw.txt"))
    End Sub

    Private Sub WC_DownloadStringCompleted(sender As Object, e As DownloadStringCompletedEventArgs) Handles WC.DownloadStringCompleted
        If Not e.Result.Split("|"c)(0).Contains(Me.ProductVersion) Then
            If MessageBox.Show("New Update Available. Would you like to download it ?", "GuessMyDraw", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                frmUpdate.Show()
                Hide()
            End If
        End If
    End Sub
#End Region

    Private Sub frmConnect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AutoUpdate()
        Settings.Setup(Environment.SpecialFolder.ApplicationData, "GuessMyDraw") 'Deserialize username
        Label2.Text = String.Format("Location: {0}", RegionInfo.CurrentRegion.EnglishName)
        If Settings.Data.Remember Then
            CheckBox1.Checked = Settings.Data.Remember
            TBUsername.Text = Settings.Data.Username
        End If
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles BtnConnect.Click
        If Not String.IsNullOrEmpty(TBUsername.Text) Then
            Client = New UserClient()
            Client.Connect(TextBox1.Text, 1234)
            Settings.Data.Remember = CheckBox1.Checked
            Settings.Data.Username = TBUsername.Text
            Settings.SerializeToXML(Settings.Data) 'Serialize and save username
        End If
    End Sub
    Private Sub TBUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles TBUsername.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Not String.IsNullOrEmpty(TBUsername.Text) Then
            e.SuppressKeyPress = True
            Client = New UserClient()
            Client.Connect(TextBox1.Text, 1234)
            Settings.Data.Remember = CheckBox1.Checked
            Settings.Data.Username = TBUsername.Text
            Settings.SerializeToXML(Settings.Data)
        End If
    End Sub

    Private Sub Client_ExceptionThrown(sender As UserClient, ex As Exception) Handles Client.ExceptionThrown
        MessageBox.Show(ex.Message)
    End Sub

    Private Sub HandleCheckIfUsernameIsAlreadyTaken(DataReceived As Data)
        'Here is where we check if the username is already taken.
        Dim UsersConnectedList As List(Of User) = DataReceived.UsersConnected

        If UsersConnectedList.Exists(Function(x) x.Username = TBUsername.Text) Then
            MessageBox.Show("Username is already taken. Choose another one.")
        Else
            Hide()
            frmMain.Show()
        End If
    End Sub
    Private Sub Client_ReadPacket(sender As UserClient, data() As Byte) Handles Client.ReadPacket
        Using ms = New MemoryStream(data)
            Dim DataReceived As Data = Serializer.Deserialize(Of Data)(ms)
            Select Case DataReceived.Commander.Command
                Case Command.UsersAndPartyOnConnect 'Give you all the users already connected
                    HandleCheckIfUsernameIsAlreadyTaken(DataReceived)
            End Select
        End Using
    End Sub

End Class