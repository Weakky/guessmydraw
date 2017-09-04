Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Net

Public Class frmUpdate


#Region "Helper Methods"
    Public Declare Function GetModuleFileName Lib "kernel32" Alias "GetModuleFileNameA" (ByVal hModule As Integer, ByVal lpFileName As String, ByVal nSize As Integer) As Integer
    Public Declare Function ExitProcess Lib "kernel32" Alias "ExitProcess" (ByVal uExitCode As UInteger) As Integer
    Public Declare Function MoveFile Lib "kernel32" Alias "MoveFileExW" (<[In](), MarshalAs(UnmanagedType.LPTStr)> ByVal lpExistingFileName As String, <[In](), MarshalAs(UnmanagedType.LPTStr)> ByVal lpNewFileName As String, ByVal dwFlags As Long) As Integer

    Public Sub MeltFile()
        File.SetAttributes(Application.ExecutablePath, FileAttributes.Normal)
        MoveFile(Strings.Left(Application.ExecutablePath, GetModuleFileName(0, Application.ExecutablePath, 256)), System.IO.Path.GetTempPath + "tmp" + Date.Now.Millisecond.ToString + ".tmp", 8)
    End Sub
#End Region

    Dim Client As New WebClient
    Private Sub frmUpdate_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler Client.DownloadProgressChanged, AddressOf client_ProgressChanged
        AddHandler Client.DownloadFileCompleted, AddressOf client_DownloadCompleted

        Dim sFile As String = Application.ExecutablePath
        MeltFile()
        Dim a As String = Client.DownloadString("https://dl.dropboxusercontent.com/u/19643954/UpdateGuessMyDraw.txt")
        Client.DownloadFileAsync(New Uri(Split(a, "|")(1)), sFile)
    End Sub

    Private Sub client_ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())
        Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())
        Dim percentage As Double = bytesIn / totalBytes * 100
        ProgressBar1.Value = Int32.Parse(Math.Truncate(percentage).ToString())
        Label1.Text = String.Format("Downloading update: {0} %", Integer.Parse(Math.Truncate(percentage).ToString()))
    End Sub

    Private Sub client_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        Dim sFile As String = Application.ExecutablePath
        File.Copy(Application.ExecutablePath, Application.StartupPath & "\backup.bak")
        If Shell(sFile) <> 0 Then
            File.Delete(Application.StartupPath & "\backup.bak")
            ExitProcess(0)
        Else
            If MessageBox.Show("Error. Do you want to restore the previous version ?", "GuessMyDraw", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                File.Move(Application.StartupPath & "\backup.bak", Application.StartupPath & "\" & Path.GetFileName(Application.ExecutablePath))
            End If
        End If
    End Sub
End Class