<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form 
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.tmrIdle = New System.Windows.Forms.Timer(Me.components)
        Me.MainTabControl = New GuessMyDraw.DotNetBarTabcontrol()
        Me.TabPageUserList = New System.Windows.Forms.TabPage()
        Me.TabControlUsersOnline = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.LblUsersConnected = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LVUserList = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPageParty = New System.Windows.Forms.TabPage()
        Me.BtnCreateParty = New GuessMyDraw.GuessButton()
        Me.LVPartyCreated = New System.Windows.Forms.ListView()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPagePublicChat = New System.Windows.Forms.TabPage()
        Me.TBPublicChatReceive = New System.Windows.Forms.TextBox()
        Me.TBPublicChatSend = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPageGame = New System.Windows.Forms.TabPage()
        Me.TabControlGame = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TBPartyReceivedMessage = New System.Windows.Forms.TextBox()
        Me.TBPartySendChatMessage = New System.Windows.Forms.TextBox()
        Me.LblScore = New System.Windows.Forms.Label()
        Me.LblCountdown = New System.Windows.Forms.Label()
        Me.GBDrawing = New System.Windows.Forms.GroupBox()
        Me.ColorPickerControl1 = New GuessMyDraw.ColorPickerControl()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PBBrown = New System.Windows.Forms.PictureBox()
        Me.BtnEraseDrawing = New GuessMyDraw.GuessButton()
        Me.BtnClearDrawing = New GuessMyDraw.GuessButton()
        Me.BtnStartGame = New GuessMyDraw.GuessButton()
        Me.BtnGenerateWord = New System.Windows.Forms.Label()
        Me.PBBlack = New System.Windows.Forms.PictureBox()
        Me.PBGreen = New System.Windows.Forms.PictureBox()
        Me.PBRed = New System.Windows.Forms.PictureBox()
        Me.PBBlue = New System.Windows.Forms.PictureBox()
        Me.PBYellow = New System.Windows.Forms.PictureBox()
        Me.LblGameStart = New System.Windows.Forms.Label()
        Me.PBDrawing = New System.Windows.Forms.PictureBox()
        Me.LblWordToGuess = New System.Windows.Forms.Label()
        Me.TrackBarWidth = New System.Windows.Forms.TrackBar()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GBWordGuess = New System.Windows.Forms.GroupBox()
        Me.LblHiddenText = New System.Windows.Forms.Label()
        Me.BtnGuessTheDraw = New GuessMyDraw.GuessButton()
        Me.LblLenghtOfWordToGuess = New System.Windows.Forms.Label()
        Me.LblWarnWrongWord = New System.Windows.Forms.Label()
        Me.TBWordToGuess = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.LblScore2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LVUserConnectedToParty = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPagePublicChatFromParty = New System.Windows.Forms.TabPage()
        Me.TBPublicChatReceiveFromParty = New System.Windows.Forms.TextBox()
        Me.TBPublicChatSendFromParty = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MainTabControl.SuspendLayout()
        Me.TabPageUserList.SuspendLayout()
        Me.TabControlUsersOnline.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPageParty.SuspendLayout()
        Me.TabPagePublicChat.SuspendLayout()
        Me.TabPageGame.SuspendLayout()
        Me.TabControlGame.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GBDrawing.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBBrown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBBlack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBGreen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBRed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBBlue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBYellow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBDrawing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBWordGuess.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPagePublicChatFromParty.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmrIdle
        '
        Me.tmrIdle.Enabled = True
        Me.tmrIdle.Interval = 60000
        '
        'MainTabControl
        '
        Me.MainTabControl.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.MainTabControl.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MainTabControl.Color1 = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.MainTabControl.Color2 = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.MainTabControl.Color3 = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.MainTabControl.Controls.Add(Me.TabPageUserList)
        Me.MainTabControl.Controls.Add(Me.TabPageGame)
        Me.MainTabControl.Controls.Add(Me.TabPage5)
        Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTabControl.ItemSize = New System.Drawing.Size(44, 136)
        Me.MainTabControl.Location = New System.Drawing.Point(0, 0)
        Me.MainTabControl.Multiline = True
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.ShowOuterBorders = False
        Me.MainTabControl.Size = New System.Drawing.Size(1064, 502)
        Me.MainTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.MainTabControl.TabIndex = 0
        '
        'TabPageUserList
        '
        Me.TabPageUserList.BackColor = System.Drawing.Color.White
        Me.TabPageUserList.Controls.Add(Me.TabControlUsersOnline)
        Me.TabPageUserList.Location = New System.Drawing.Point(140, 4)
        Me.TabPageUserList.Name = "TabPageUserList"
        Me.TabPageUserList.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageUserList.Size = New System.Drawing.Size(920, 494)
        Me.TabPageUserList.TabIndex = 1
        Me.TabPageUserList.Text = "Online Player"
        '
        'TabControlUsersOnline
        '
        Me.TabControlUsersOnline.Controls.Add(Me.TabPage1)
        Me.TabControlUsersOnline.Controls.Add(Me.TabPageParty)
        Me.TabControlUsersOnline.Controls.Add(Me.TabPagePublicChat)
        Me.TabControlUsersOnline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlUsersOnline.Location = New System.Drawing.Point(3, 3)
        Me.TabControlUsersOnline.Name = "TabControlUsersOnline"
        Me.TabControlUsersOnline.SelectedIndex = 0
        Me.TabControlUsersOnline.Size = New System.Drawing.Size(914, 488)
        Me.TabControlUsersOnline.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.LblUsersConnected)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.LVUserList)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(906, 462)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Users"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'LblUsersConnected
        '
        Me.LblUsersConnected.AutoSize = True
        Me.LblUsersConnected.Font = New System.Drawing.Font("Segoe UI Light", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUsersConnected.Location = New System.Drawing.Point(114, 9)
        Me.LblUsersConnected.Name = "LblUsersConnected"
        Me.LblUsersConnected.Size = New System.Drawing.Size(22, 25)
        Me.LblUsersConnected.TabIndex = 35
        Me.LblUsersConnected.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Light", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 25)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Users online:"
        '
        'LVUserList
        '
        Me.LVUserList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LVUserList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.LVUserList.FullRowSelect = True
        Me.LVUserList.Location = New System.Drawing.Point(3, 39)
        Me.LVUserList.MultiSelect = False
        Me.LVUserList.Name = "LVUserList"
        Me.LVUserList.Size = New System.Drawing.Size(897, 420)
        Me.LVUserList.TabIndex = 0
        Me.LVUserList.UseCompatibleStateImageBehavior = False
        Me.LVUserList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Username"
        Me.ColumnHeader1.Width = 380
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Location"
        Me.ColumnHeader2.Width = 427
        '
        'TabPageParty
        '
        Me.TabPageParty.Controls.Add(Me.BtnCreateParty)
        Me.TabPageParty.Controls.Add(Me.LVPartyCreated)
        Me.TabPageParty.Location = New System.Drawing.Point(4, 22)
        Me.TabPageParty.Name = "TabPageParty"
        Me.TabPageParty.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageParty.Size = New System.Drawing.Size(906, 462)
        Me.TabPageParty.TabIndex = 1
        Me.TabPageParty.Text = "Public Party"
        Me.TabPageParty.UseVisualStyleBackColor = True
        '
        'BtnCreateParty
        '
        Me.BtnCreateParty.BackColorDown = System.Drawing.Color.Empty
        Me.BtnCreateParty.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.BtnCreateParty.BackColorOver = System.Drawing.Color.Gainsboro
        Me.BtnCreateParty.BorderColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer))
        Me.BtnCreateParty.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.BtnCreateParty.ForeColor = System.Drawing.Color.Black
        Me.BtnCreateParty.Location = New System.Drawing.Point(6, 6)
        Me.BtnCreateParty.Name = "BtnCreateParty"
        Me.BtnCreateParty.Size = New System.Drawing.Size(169, 30)
        Me.BtnCreateParty.TabIndex = 55
        Me.BtnCreateParty.Text = "Create a new public party"
        Me.BtnCreateParty.TextColor = System.Drawing.Color.Black
        '
        'LVPartyCreated
        '
        Me.LVPartyCreated.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LVPartyCreated.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5, Me.ColumnHeader6})
        Me.LVPartyCreated.FullRowSelect = True
        Me.LVPartyCreated.Location = New System.Drawing.Point(3, 42)
        Me.LVPartyCreated.MultiSelect = False
        Me.LVPartyCreated.Name = "LVPartyCreated"
        Me.LVPartyCreated.Size = New System.Drawing.Size(897, 417)
        Me.LVPartyCreated.TabIndex = 1
        Me.LVPartyCreated.UseCompatibleStateImageBehavior = False
        Me.LVPartyCreated.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Name"
        Me.ColumnHeader5.Width = 391
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Creator"
        Me.ColumnHeader6.Width = 416
        '
        'TabPagePublicChat
        '
        Me.TabPagePublicChat.Controls.Add(Me.TBPublicChatReceive)
        Me.TabPagePublicChat.Controls.Add(Me.TBPublicChatSend)
        Me.TabPagePublicChat.Controls.Add(Me.Label4)
        Me.TabPagePublicChat.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePublicChat.Name = "TabPagePublicChat"
        Me.TabPagePublicChat.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePublicChat.Size = New System.Drawing.Size(906, 462)
        Me.TabPagePublicChat.TabIndex = 2
        Me.TabPagePublicChat.Text = "Public Chat"
        Me.TabPagePublicChat.UseVisualStyleBackColor = True
        '
        'TBPublicChatReceive
        '
        Me.TBPublicChatReceive.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TBPublicChatReceive.BackColor = System.Drawing.SystemColors.Control
        Me.TBPublicChatReceive.Location = New System.Drawing.Point(8, 32)
        Me.TBPublicChatReceive.Multiline = True
        Me.TBPublicChatReceive.Name = "TBPublicChatReceive"
        Me.TBPublicChatReceive.ReadOnly = True
        Me.TBPublicChatReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TBPublicChatReceive.Size = New System.Drawing.Size(892, 398)
        Me.TBPublicChatReceive.TabIndex = 43
        '
        'TBPublicChatSend
        '
        Me.TBPublicChatSend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TBPublicChatSend.Location = New System.Drawing.Point(8, 436)
        Me.TBPublicChatSend.Name = "TBPublicChatSend"
        Me.TBPublicChatSend.Size = New System.Drawing.Size(892, 20)
        Me.TBPublicChatSend.TabIndex = 44
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Light", 14.25!)
        Me.Label4.Location = New System.Drawing.Point(6, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 25)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Chat Room :"
        '
        'TabPageGame
        '
        Me.TabPageGame.BackColor = System.Drawing.Color.White
        Me.TabPageGame.Controls.Add(Me.TabControlGame)
        Me.TabPageGame.Location = New System.Drawing.Point(140, 4)
        Me.TabPageGame.Name = "TabPageGame"
        Me.TabPageGame.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGame.Size = New System.Drawing.Size(920, 494)
        Me.TabPageGame.TabIndex = 2
        Me.TabPageGame.Text = "Game"
        '
        'TabControlGame
        '
        Me.TabControlGame.Controls.Add(Me.TabPage3)
        Me.TabControlGame.Controls.Add(Me.TabPage4)
        Me.TabControlGame.Controls.Add(Me.TabPagePublicChatFromParty)
        Me.TabControlGame.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlGame.Location = New System.Drawing.Point(3, 3)
        Me.TabControlGame.Name = "TabControlGame"
        Me.TabControlGame.SelectedIndex = 0
        Me.TabControlGame.Size = New System.Drawing.Size(914, 488)
        Me.TabControlGame.TabIndex = 53
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label10)
        Me.TabPage3.Controls.Add(Me.TBPartyReceivedMessage)
        Me.TabPage3.Controls.Add(Me.TBPartySendChatMessage)
        Me.TabPage3.Controls.Add(Me.LblScore)
        Me.TabPage3.Controls.Add(Me.LblCountdown)
        Me.TabPage3.Controls.Add(Me.GBDrawing)
        Me.TabPage3.Controls.Add(Me.GBWordGuess)
        Me.TabPage3.Controls.Add(Me.Label1)
        Me.TabPage3.Controls.Add(Me.Label17)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(906, 462)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "Drawing"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Light", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(548, 167)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(107, 25)
        Me.Label10.TabIndex = 61
        Me.Label10.Text = "Chat Room:"
        '
        'TBPartyReceivedMessage
        '
        Me.TBPartyReceivedMessage.BackColor = System.Drawing.SystemColors.Control
        Me.TBPartyReceivedMessage.Location = New System.Drawing.Point(548, 195)
        Me.TBPartyReceivedMessage.Multiline = True
        Me.TBPartyReceivedMessage.Name = "TBPartyReceivedMessage"
        Me.TBPartyReceivedMessage.ReadOnly = True
        Me.TBPartyReceivedMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TBPartyReceivedMessage.Size = New System.Drawing.Size(348, 136)
        Me.TBPartyReceivedMessage.TabIndex = 40
        '
        'TBPartySendChatMessage
        '
        Me.TBPartySendChatMessage.Location = New System.Drawing.Point(548, 337)
        Me.TBPartySendChatMessage.Name = "TBPartySendChatMessage"
        Me.TBPartySendChatMessage.Size = New System.Drawing.Size(348, 20)
        Me.TBPartySendChatMessage.TabIndex = 41
        '
        'LblScore
        '
        Me.LblScore.AutoSize = True
        Me.LblScore.Font = New System.Drawing.Font("Segoe UI Light", 23.0!)
        Me.LblScore.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblScore.Location = New System.Drawing.Point(708, 408)
        Me.LblScore.Name = "LblScore"
        Me.LblScore.Size = New System.Drawing.Size(34, 42)
        Me.LblScore.TabIndex = 47
        Me.LblScore.Text = "0"
        '
        'LblCountdown
        '
        Me.LblCountdown.AutoSize = True
        Me.LblCountdown.Font = New System.Drawing.Font("Segoe UI Light", 23.0!)
        Me.LblCountdown.ForeColor = System.Drawing.Color.LimeGreen
        Me.LblCountdown.Location = New System.Drawing.Point(723, 361)
        Me.LblCountdown.Name = "LblCountdown"
        Me.LblCountdown.Size = New System.Drawing.Size(54, 42)
        Me.LblCountdown.TabIndex = 45
        Me.LblCountdown.Text = "0 s"
        '
        'GBDrawing
        '
        Me.GBDrawing.Controls.Add(Me.ColorPickerControl1)
        Me.GBDrawing.Controls.Add(Me.PictureBox4)
        Me.GBDrawing.Controls.Add(Me.PBBrown)
        Me.GBDrawing.Controls.Add(Me.BtnEraseDrawing)
        Me.GBDrawing.Controls.Add(Me.BtnClearDrawing)
        Me.GBDrawing.Controls.Add(Me.BtnStartGame)
        Me.GBDrawing.Controls.Add(Me.BtnGenerateWord)
        Me.GBDrawing.Controls.Add(Me.PBBlack)
        Me.GBDrawing.Controls.Add(Me.PBGreen)
        Me.GBDrawing.Controls.Add(Me.PBRed)
        Me.GBDrawing.Controls.Add(Me.PBBlue)
        Me.GBDrawing.Controls.Add(Me.PBYellow)
        Me.GBDrawing.Controls.Add(Me.LblGameStart)
        Me.GBDrawing.Controls.Add(Me.PBDrawing)
        Me.GBDrawing.Controls.Add(Me.LblWordToGuess)
        Me.GBDrawing.Controls.Add(Me.TrackBarWidth)
        Me.GBDrawing.Controls.Add(Me.Label14)
        Me.GBDrawing.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.GBDrawing.Location = New System.Drawing.Point(6, 6)
        Me.GBDrawing.Name = "GBDrawing"
        Me.GBDrawing.Size = New System.Drawing.Size(536, 447)
        Me.GBDrawing.TabIndex = 33
        Me.GBDrawing.TabStop = False
        Me.GBDrawing.Text = "Drawing"
        '
        'ColorPickerControl1
        '
        Me.ColorPickerControl1.ColorBoxPaddingRatio = 0.1!
        Me.ColorPickerControl1.ColorBoxSizeRatio = 0.189!
        Me.ColorPickerControl1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ColorPickerControl1.Location = New System.Drawing.Point(124, 401)
        Me.ColorPickerControl1.Name = "ColorPickerControl1"
        Me.ColorPickerControl1.SelectedColor = System.Drawing.Color.Black
        Me.ColorPickerControl1.Size = New System.Drawing.Size(284, 32)
        Me.ColorPickerControl1.TabIndex = 60
        Me.ColorPickerControl1.Text = "ColorPickerControl1"
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.Purple
        Me.PictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox4.Location = New System.Drawing.Point(158, 402)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(30, 30)
        Me.PictureBox4.TabIndex = 59
        Me.PictureBox4.TabStop = False
        '
        'PBBrown
        '
        Me.PBBrown.BackColor = System.Drawing.Color.SaddleBrown
        Me.PBBrown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBBrown.Location = New System.Drawing.Point(335, 402)
        Me.PBBrown.Name = "PBBrown"
        Me.PBBrown.Size = New System.Drawing.Size(30, 30)
        Me.PBBrown.TabIndex = 58
        Me.PBBrown.TabStop = False
        '
        'BtnEraseDrawing
        '
        Me.BtnEraseDrawing.BackColorDown = System.Drawing.Color.Empty
        Me.BtnEraseDrawing.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.BtnEraseDrawing.BackColorOver = System.Drawing.Color.Gainsboro
        Me.BtnEraseDrawing.BorderColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer))
        Me.BtnEraseDrawing.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.BtnEraseDrawing.ForeColor = System.Drawing.Color.Black
        Me.BtnEraseDrawing.Location = New System.Drawing.Point(416, 400)
        Me.BtnEraseDrawing.Name = "BtnEraseDrawing"
        Me.BtnEraseDrawing.Size = New System.Drawing.Size(54, 32)
        Me.BtnEraseDrawing.TabIndex = 57
        Me.BtnEraseDrawing.Text = "Erase"
        Me.BtnEraseDrawing.TextColor = System.Drawing.Color.Black
        '
        'BtnClearDrawing
        '
        Me.BtnClearDrawing.BackColorDown = System.Drawing.Color.Empty
        Me.BtnClearDrawing.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.BtnClearDrawing.BackColorOver = System.Drawing.Color.Gainsboro
        Me.BtnClearDrawing.BorderColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer))
        Me.BtnClearDrawing.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.BtnClearDrawing.ForeColor = System.Drawing.Color.Black
        Me.BtnClearDrawing.Location = New System.Drawing.Point(476, 400)
        Me.BtnClearDrawing.Name = "BtnClearDrawing"
        Me.BtnClearDrawing.Size = New System.Drawing.Size(54, 32)
        Me.BtnClearDrawing.TabIndex = 56
        Me.BtnClearDrawing.Text = "Clear"
        Me.BtnClearDrawing.TextColor = System.Drawing.Color.Black
        '
        'BtnStartGame
        '
        Me.BtnStartGame.BackColorDown = System.Drawing.Color.Empty
        Me.BtnStartGame.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.BtnStartGame.BackColorOver = System.Drawing.Color.Gainsboro
        Me.BtnStartGame.BorderColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer))
        Me.BtnStartGame.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.BtnStartGame.ForeColor = System.Drawing.Color.Black
        Me.BtnStartGame.Location = New System.Drawing.Point(335, 16)
        Me.BtnStartGame.Name = "BtnStartGame"
        Me.BtnStartGame.Size = New System.Drawing.Size(194, 38)
        Me.BtnStartGame.TabIndex = 54
        Me.BtnStartGame.Text = "Start game"
        Me.BtnStartGame.TextColor = System.Drawing.Color.Black
        '
        'BtnGenerateWord
        '
        Me.BtnGenerateWord.AutoSize = True
        Me.BtnGenerateWord.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.BtnGenerateWord.Location = New System.Drawing.Point(12, 41)
        Me.BtnGenerateWord.Name = "BtnGenerateWord"
        Me.BtnGenerateWord.Size = New System.Drawing.Size(176, 13)
        Me.BtnGenerateWord.TabIndex = 52
        Me.BtnGenerateWord.Text = "(Click here to generate another one)"
        '
        'PBBlack
        '
        Me.PBBlack.BackColor = System.Drawing.Color.Black
        Me.PBBlack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBBlack.Location = New System.Drawing.Point(191, 402)
        Me.PBBlack.Name = "PBBlack"
        Me.PBBlack.Size = New System.Drawing.Size(30, 30)
        Me.PBBlack.TabIndex = 51
        Me.PBBlack.TabStop = False
        '
        'PBGreen
        '
        Me.PBGreen.BackColor = System.Drawing.Color.Lime
        Me.PBGreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBGreen.Location = New System.Drawing.Point(227, 402)
        Me.PBGreen.Name = "PBGreen"
        Me.PBGreen.Size = New System.Drawing.Size(30, 30)
        Me.PBGreen.TabIndex = 50
        Me.PBGreen.TabStop = False
        '
        'PBRed
        '
        Me.PBRed.BackColor = System.Drawing.Color.Red
        Me.PBRed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBRed.Location = New System.Drawing.Point(263, 402)
        Me.PBRed.Name = "PBRed"
        Me.PBRed.Size = New System.Drawing.Size(30, 30)
        Me.PBRed.TabIndex = 49
        Me.PBRed.TabStop = False
        '
        'PBBlue
        '
        Me.PBBlue.BackColor = System.Drawing.Color.DodgerBlue
        Me.PBBlue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBBlue.Location = New System.Drawing.Point(299, 402)
        Me.PBBlue.Name = "PBBlue"
        Me.PBBlue.Size = New System.Drawing.Size(30, 30)
        Me.PBBlue.TabIndex = 48
        Me.PBBlue.TabStop = False
        '
        'PBYellow
        '
        Me.PBYellow.BackColor = System.Drawing.Color.Yellow
        Me.PBYellow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBYellow.Location = New System.Drawing.Point(371, 402)
        Me.PBYellow.Name = "PBYellow"
        Me.PBYellow.Size = New System.Drawing.Size(30, 30)
        Me.PBYellow.TabIndex = 47
        Me.PBYellow.TabStop = False
        '
        'LblGameStart
        '
        Me.LblGameStart.AutoSize = True
        Me.LblGameStart.Font = New System.Drawing.Font("Segoe UI Semilight", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGameStart.Location = New System.Drawing.Point(64, 211)
        Me.LblGameStart.Name = "LblGameStart"
        Me.LblGameStart.Size = New System.Drawing.Size(399, 32)
        Me.LblGameStart.TabIndex = 46
        Me.LblGameStart.Text = "Game is about to start in 4 seconds..."
        Me.LblGameStart.Visible = False
        '
        'PBDrawing
        '
        Me.PBDrawing.BackColor = System.Drawing.Color.Transparent
        Me.PBDrawing.Location = New System.Drawing.Point(6, 63)
        Me.PBDrawing.Name = "PBDrawing"
        Me.PBDrawing.Size = New System.Drawing.Size(524, 325)
        Me.PBDrawing.TabIndex = 2
        Me.PBDrawing.TabStop = False
        '
        'LblWordToGuess
        '
        Me.LblWordToGuess.AutoSize = True
        Me.LblWordToGuess.Font = New System.Drawing.Font("Segoe UI Light", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblWordToGuess.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblWordToGuess.Location = New System.Drawing.Point(142, 16)
        Me.LblWordToGuess.Name = "LblWordToGuess"
        Me.LblWordToGuess.Size = New System.Drawing.Size(0, 25)
        Me.LblWordToGuess.TabIndex = 34
        '
        'TrackBarWidth
        '
        Me.TrackBarWidth.BackColor = System.Drawing.Color.White
        Me.TrackBarWidth.Location = New System.Drawing.Point(19, 398)
        Me.TrackBarWidth.Maximum = 18
        Me.TrackBarWidth.Minimum = 1
        Me.TrackBarWidth.Name = "TrackBarWidth"
        Me.TrackBarWidth.Size = New System.Drawing.Size(101, 45)
        Me.TrackBarWidth.TabIndex = 3
        Me.TrackBarWidth.Value = 3
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Light", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(10, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(131, 25)
        Me.Label14.TabIndex = 33
        Me.Label14.Text = "Word to draw :"
        '
        'GBWordGuess
        '
        Me.GBWordGuess.Controls.Add(Me.LblHiddenText)
        Me.GBWordGuess.Controls.Add(Me.BtnGuessTheDraw)
        Me.GBWordGuess.Controls.Add(Me.LblLenghtOfWordToGuess)
        Me.GBWordGuess.Controls.Add(Me.LblWarnWrongWord)
        Me.GBWordGuess.Controls.Add(Me.TBWordToGuess)
        Me.GBWordGuess.Controls.Add(Me.Label6)
        Me.GBWordGuess.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.GBWordGuess.Location = New System.Drawing.Point(549, 7)
        Me.GBWordGuess.Name = "GBWordGuess"
        Me.GBWordGuess.Size = New System.Drawing.Size(304, 151)
        Me.GBWordGuess.TabIndex = 34
        Me.GBWordGuess.TabStop = False
        Me.GBWordGuess.Text = "Guessing"
        '
        'LblHiddenText
        '
        Me.LblHiddenText.AutoSize = True
        Me.LblHiddenText.Font = New System.Drawing.Font("Segoe UI Light", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblHiddenText.Location = New System.Drawing.Point(10, 40)
        Me.LblHiddenText.Name = "LblHiddenText"
        Me.LblHiddenText.Size = New System.Drawing.Size(0, 25)
        Me.LblHiddenText.TabIndex = 61
        '
        'BtnGuessTheDraw
        '
        Me.BtnGuessTheDraw.BackColorDown = System.Drawing.Color.Empty
        Me.BtnGuessTheDraw.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.BtnGuessTheDraw.BackColorOver = System.Drawing.Color.Gainsboro
        Me.BtnGuessTheDraw.BorderColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer))
        Me.BtnGuessTheDraw.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.BtnGuessTheDraw.ForeColor = System.Drawing.Color.Black
        Me.BtnGuessTheDraw.Location = New System.Drawing.Point(11, 99)
        Me.BtnGuessTheDraw.Name = "BtnGuessTheDraw"
        Me.BtnGuessTheDraw.Size = New System.Drawing.Size(228, 27)
        Me.BtnGuessTheDraw.TabIndex = 55
        Me.BtnGuessTheDraw.Text = "Guess Me"
        Me.BtnGuessTheDraw.TextColor = System.Drawing.Color.Black
        '
        'LblLenghtOfWordToGuess
        '
        Me.LblLenghtOfWordToGuess.AutoSize = True
        Me.LblLenghtOfWordToGuess.Font = New System.Drawing.Font("Segoe UI Light", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLenghtOfWordToGuess.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblLenghtOfWordToGuess.Location = New System.Drawing.Point(125, 15)
        Me.LblLenghtOfWordToGuess.Name = "LblLenghtOfWordToGuess"
        Me.LblLenghtOfWordToGuess.Size = New System.Drawing.Size(22, 25)
        Me.LblLenghtOfWordToGuess.TabIndex = 14
        Me.LblLenghtOfWordToGuess.Text = "0"
        '
        'LblWarnWrongWord
        '
        Me.LblWarnWrongWord.AutoSize = True
        Me.LblWarnWrongWord.Location = New System.Drawing.Point(11, 131)
        Me.LblWarnWrongWord.Name = "LblWarnWrongWord"
        Me.LblWarnWrongWord.Size = New System.Drawing.Size(11, 13)
        Me.LblWarnWrongWord.TabIndex = 36
        Me.LblWarnWrongWord.Text = "/"
        '
        'TBWordToGuess
        '
        Me.TBWordToGuess.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.TBWordToGuess.Location = New System.Drawing.Point(11, 71)
        Me.TBWordToGuess.Name = "TBWordToGuess"
        Me.TBWordToGuess.Size = New System.Drawing.Size(287, 22)
        Me.TBWordToGuess.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Light", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(121, 25)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Word Length:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Light", 23.0!)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(556, 406)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 42)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Your score:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Light", 23.0!)
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(554, 361)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(176, 42)
        Me.Label17.TabIndex = 44
        Me.Label17.Text = "Countdown:"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.LblScore2)
        Me.TabPage4.Controls.Add(Me.Label3)
        Me.TabPage4.Controls.Add(Me.LVUserConnectedToParty)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(906, 462)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "Scoreboard"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'LblScore2
        '
        Me.LblScore2.AutoSize = True
        Me.LblScore2.Font = New System.Drawing.Font("Segoe UI Light", 23.0!)
        Me.LblScore2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblScore2.Location = New System.Drawing.Point(160, 4)
        Me.LblScore2.Name = "LblScore2"
        Me.LblScore2.Size = New System.Drawing.Size(34, 42)
        Me.LblScore2.TabIndex = 49
        Me.LblScore2.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Light", 23.0!)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(6, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(160, 42)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "Your score:"
        '
        'LVUserConnectedToParty
        '
        Me.LVUserConnectedToParty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LVUserConnectedToParty.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4})
        Me.LVUserConnectedToParty.Location = New System.Drawing.Point(3, 48)
        Me.LVUserConnectedToParty.Name = "LVUserConnectedToParty"
        Me.LVUserConnectedToParty.Size = New System.Drawing.Size(897, 411)
        Me.LVUserConnectedToParty.TabIndex = 1
        Me.LVUserConnectedToParty.UseCompatibleStateImageBehavior = False
        Me.LVUserConnectedToParty.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Username"
        Me.ColumnHeader3.Width = 387
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Score"
        Me.ColumnHeader4.Width = 413
        '
        'TabPagePublicChatFromParty
        '
        Me.TabPagePublicChatFromParty.Controls.Add(Me.TBPublicChatReceiveFromParty)
        Me.TabPagePublicChatFromParty.Controls.Add(Me.TBPublicChatSendFromParty)
        Me.TabPagePublicChatFromParty.Controls.Add(Me.Label9)
        Me.TabPagePublicChatFromParty.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePublicChatFromParty.Name = "TabPagePublicChatFromParty"
        Me.TabPagePublicChatFromParty.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePublicChatFromParty.Size = New System.Drawing.Size(906, 462)
        Me.TabPagePublicChatFromParty.TabIndex = 2
        Me.TabPagePublicChatFromParty.Text = "Public Chat"
        Me.TabPagePublicChatFromParty.UseVisualStyleBackColor = True
        '
        'TBPublicChatReceiveFromParty
        '
        Me.TBPublicChatReceiveFromParty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TBPublicChatReceiveFromParty.BackColor = System.Drawing.SystemColors.Control
        Me.TBPublicChatReceiveFromParty.Location = New System.Drawing.Point(8, 33)
        Me.TBPublicChatReceiveFromParty.Multiline = True
        Me.TBPublicChatReceiveFromParty.Name = "TBPublicChatReceiveFromParty"
        Me.TBPublicChatReceiveFromParty.ReadOnly = True
        Me.TBPublicChatReceiveFromParty.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TBPublicChatReceiveFromParty.Size = New System.Drawing.Size(892, 398)
        Me.TBPublicChatReceiveFromParty.TabIndex = 46
        '
        'TBPublicChatSendFromParty
        '
        Me.TBPublicChatSendFromParty.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TBPublicChatSendFromParty.Location = New System.Drawing.Point(8, 437)
        Me.TBPublicChatSendFromParty.Name = "TBPublicChatSendFromParty"
        Me.TBPublicChatSendFromParty.Size = New System.Drawing.Size(892, 20)
        Me.TBPublicChatSendFromParty.TabIndex = 47
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Light", 14.25!)
        Me.Label9.Location = New System.Drawing.Point(6, 5)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(112, 25)
        Me.Label9.TabIndex = 48
        Me.Label9.Text = "Chat Room :"
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.Color.White
        Me.TabPage5.Controls.Add(Me.Label7)
        Me.TabPage5.Controls.Add(Me.Label8)
        Me.TabPage5.Controls.Add(Me.Label5)
        Me.TabPage5.Controls.Add(Me.PictureBox3)
        Me.TabPage5.Controls.Add(Me.PictureBox2)
        Me.TabPage5.Controls.Add(Me.PictureBox1)
        Me.TabPage5.Location = New System.Drawing.Point(140, 4)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(920, 494)
        Me.TabPage5.TabIndex = 3
        Me.TabPage5.Text = "About"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Light", 18.0!)
        Me.Label7.Location = New System.Drawing.Point(573, 286)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(255, 64)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Me, TRANSLU6DE, who " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "did all the rest :)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Light", 18.0!)
        Me.Label8.Location = New System.Drawing.Point(42, 286)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(234, 64)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Aeonhack who " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "made the socket class"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Light", 18.0!)
        Me.Label5.Location = New System.Drawing.Point(300, 286)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(258, 64)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Mavamaarten~ who " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "made the TabBarControl" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.GuessMyDraw.My.Resources.Resources.avatar_101640
        Me.PictureBox3.Location = New System.Drawing.Point(48, 83)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(200, 200)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 6
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.GuessMyDraw.My.Resources.Resources.tMLCm
        Me.PictureBox2.Location = New System.Drawing.Point(315, 58)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(200, 249)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 4
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.GuessMyDraw.My.Resources.Resources.avatar_129283
        Me.PictureBox1.Location = New System.Drawing.Point(594, 83)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(200, 200)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1064, 502)
        Me.Controls.Add(Me.MainTabControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "GuessMyDraw"
        Me.MainTabControl.ResumeLayout(False)
        Me.TabPageUserList.ResumeLayout(False)
        Me.TabControlUsersOnline.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPageParty.ResumeLayout(False)
        Me.TabPagePublicChat.ResumeLayout(False)
        Me.TabPagePublicChat.PerformLayout()
        Me.TabPageGame.ResumeLayout(False)
        Me.TabControlGame.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.GBDrawing.ResumeLayout(False)
        Me.GBDrawing.PerformLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBBrown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBBlack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBGreen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBRed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBBlue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBYellow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBDrawing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBWordGuess.ResumeLayout(False)
        Me.GBWordGuess.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.TabPagePublicChatFromParty.ResumeLayout(False)
        Me.TabPagePublicChatFromParty.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainTabControl As DotNetBarTabcontrol
    Friend WithEvents TabPageUserList As System.Windows.Forms.TabPage
    Friend WithEvents TabPageGame As System.Windows.Forms.TabPage
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TBPartySendChatMessage As System.Windows.Forms.TextBox
    Friend WithEvents TBPartyReceivedMessage As System.Windows.Forms.TextBox
    Friend WithEvents LblCountdown As System.Windows.Forms.Label
    Friend WithEvents GBDrawing As System.Windows.Forms.GroupBox
    Friend WithEvents LblWordToGuess As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TrackBarWidth As System.Windows.Forms.TrackBar
    Friend WithEvents PBDrawing As System.Windows.Forms.PictureBox
    Friend WithEvents GBWordGuess As System.Windows.Forms.GroupBox
    Friend WithEvents TBWordToGuess As System.Windows.Forms.TextBox
    Friend WithEvents LblLenghtOfWordToGuess As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents LblWarnWrongWord As System.Windows.Forms.Label
    Friend WithEvents LVUserList As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabControlUsersOnline As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPageParty As System.Windows.Forms.TabPage
    Friend WithEvents LVPartyCreated As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents LVUserConnectedToParty As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents LblGameStart As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LblScore As System.Windows.Forms.Label
    Friend WithEvents PBBlack As System.Windows.Forms.PictureBox
    Friend WithEvents PBGreen As System.Windows.Forms.PictureBox
    Friend WithEvents PBRed As System.Windows.Forms.PictureBox
    Friend WithEvents PBBlue As System.Windows.Forms.PictureBox
    Friend WithEvents PBYellow As System.Windows.Forms.PictureBox
    Friend WithEvents BtnGenerateWord As System.Windows.Forms.Label
    Friend WithEvents TabControlGame As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents LblScore2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents BtnStartGame As GuessButton
    Friend WithEvents GuessButton1 As GuessButton
    Friend WithEvents BtnGuessTheDraw As GuessButton
    Friend WithEvents BtnClearDrawing As GuessButton
    Friend WithEvents BtnEraseDrawing As GuessButton
    Friend WithEvents BtnCreateParty As GuessMyDraw.GuessButton
    Friend WithEvents PBBrown As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents ColorPickerControl1 As GuessMyDraw.ColorPickerControl
    Friend WithEvents LblHiddenText As System.Windows.Forms.Label
    Friend WithEvents LblUsersConnected As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabPagePublicChat As System.Windows.Forms.TabPage
    Friend WithEvents TBPublicChatReceive As System.Windows.Forms.TextBox
    Friend WithEvents TBPublicChatSend As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TabPagePublicChatFromParty As System.Windows.Forms.TabPage
    Friend WithEvents TBPublicChatReceiveFromParty As System.Windows.Forms.TextBox
    Friend WithEvents TBPublicChatSendFromParty As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tmrIdle As System.Windows.Forms.Timer
End Class
