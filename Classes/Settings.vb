Imports System.Xml.Serialization
Imports System.IO
Imports System.Globalization

Public Class Settings

    'This class save your username using XML Serialization.

    Public Shared Property Data As New DataSettings 'Contains all your settings

    Public Class Serializable
        Public Class FontX
            Public Property FontFamily() As String
                Get
                    Return m_FontFamily
                End Get
                Set(value As String)
                    m_FontFamily = value
                End Set
            End Property
            Private m_FontFamily As String
            Public Property GraphicsUnit() As GraphicsUnit
                Get
                    Return m_GraphicsUnit
                End Get
                Set(value As GraphicsUnit)
                    m_GraphicsUnit = value
                End Set
            End Property
            Private m_GraphicsUnit As GraphicsUnit
            Public Property Size() As Single
                Get
                    Return m_Size
                End Get
                Set(value As Single)
                    m_Size = value
                End Set
            End Property
            Private m_Size As Single
            Public Property Style() As FontStyle
                Get
                    Return m_Style
                End Get
                Set(value As FontStyle)
                    m_Style = value
                End Set
            End Property
            Private m_Style As FontStyle

            ''' <summary>
            ''' Intended for xml serialization purposes only
            ''' </summary>
            Private Sub New()
            End Sub

            Public Sub New(f As Font)
                FontFamily = f.FontFamily.Name
                GraphicsUnit = f.Unit
                Size = f.Size
                Style = f.Style
            End Sub

            Public Shared Function FromFont(f As Font) As FontX
                Return New FontX(f)
            End Function

            Public Function ToFont() As Font
                Return New Font(FontFamily, Size, Style, GraphicsUnit)
            End Function
        End Class
        Public Class ColorX
            Private m_Color As Color = Drawing.Color.White

            Public Sub New()
            End Sub

            Public Sub New(newColor As Color)
                m_Color = newColor
            End Sub

            <XmlIgnoreAttribute> _
            Public Property Color() As Color
                Get
                    Return m_Color
                End Get
                Set(value As Color)
                    m_Color = value
                End Set
            End Property

            <System.Xml.Serialization.XmlAttribute("Name")> _
            Public Property Name() As String
                Get
                    Return ColorTranslator.ToHtml(m_Color)
                End Get
                Set(value As String)
                    m_Color = ColorTranslator.FromHtml(value)
                End Set
            End Property

            <System.Xml.Serialization.XmlAttribute("Alpha")> _
            Public Property Alpha() As Integer
                Get
                    Return m_Color.A
                End Get
                Set(value As Integer)
                    m_Color = Color.FromArgb(value, m_Color.R, m_Color.G, m_Color.B)
                End Set
            End Property

            <System.Xml.Serialization.XmlAttribute("Red")> _
            Public Property Red() As Integer
                Get
                    Return m_Color.R
                End Get
                Set(value As Integer)
                    m_Color = Color.FromArgb(m_Color.A, value, m_Color.G, m_Color.B)
                End Set
            End Property

            <System.Xml.Serialization.XmlAttribute("Green")> _
            Public Property Green() As Integer
                Get
                    Return m_Color.G
                End Get
                Set(value As Integer)
                    m_Color = Color.FromArgb(m_Color.A, m_Color.R, value, m_Color.B)
                End Set
            End Property

            <System.Xml.Serialization.XmlAttribute("Blue")> _
            Public Property Blue() As Integer
                Get
                    Return m_Color.B
                End Get
                Set(value As Integer)
                    m_Color = Color.FromArgb(m_Color.A, m_Color.R, m_Color.G, value)
                End Set
            End Property
        End Class
    End Class

    Public Class DataSettings
        Public Property Username As String
        Public Property Remember As Boolean
    End Class

    Private Shared XMLPath As String = String.Empty

#Region "Constructor"
    Public Shared Sub Setup(Path As Environment.SpecialFolder, FolderName As String, Optional FileName As String = "Settings.xml")
        If Not Directory.Exists(CombinePaths(Environment.GetFolderPath(Path), FolderName)) Then
            Directory.CreateDirectory(CombinePaths(Environment.GetFolderPath(Path), FolderName))
        End If

        XMLPath = CombinePaths(Environment.GetFolderPath(Path), FolderName, FileName)

        If Not File.Exists(CombinePaths(Environment.GetFolderPath(Path), FolderName, FileName)) Then
            SerializeToXML(Data)
            Data = DeserializeFromXML()
        Else
            Data = DeserializeFromXML()
        End If
    End Sub
#End Region

#Region "Public Methods"
    Public Shared Sub SerializeToXML(settings As DataSettings)
        With New XmlSerializer(GetType(DataSettings))
            Using writeStream As New StreamWriter(XMLPath)
                .Serialize(writeStream, settings)
            End Using
        End With
    End Sub
    Public Shared Function DeserializeFromXML() As DataSettings
        Dim settings As DataSettings = Nothing
        With New XmlSerializer(GetType(DataSettings))
            Using readStream As New StreamReader(XMLPath)
                settings = CType(.Deserialize(readStream), DataSettings)
            End Using
        End With
        Return settings
    End Function

#End Region

#Region "Helper Methods"
    Private Shared Function CombinePaths(first As String, ParamArray others As String()) As String
        Dim _path As String = first
        For Each section As String In others
            _path = Path.Combine(_path, section)
        Next
        Return _path
    End Function
#End Region
End Class