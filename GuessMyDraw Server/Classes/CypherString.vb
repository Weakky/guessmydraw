Imports System.Text
Imports System.Security.Cryptography
Imports System.IO

Public NotInheritable Class StringCipher
    Private Sub New()
    End Sub
    ' This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
    ' This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
    ' 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
    Private Const initVector As String = "tu89geji340t89u2"

    ' This constant is used to determine the keysize of the encryption algorithm.
    Private Const keysize As Integer = 256

    Public Shared Function Encrypt(plainText As String, passPhrase As String) As String
        Dim initVectorBytes As Byte() = Encoding.UTF8.GetBytes(initVector)
        Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(plainText)
        Dim password As New PasswordDeriveBytes(passPhrase, Nothing)
        Dim keyBytes As Byte() = password.GetBytes(keysize \ 8)
        Dim symmetricKey As New RijndaelManaged()
        symmetricKey.Mode = CipherMode.CBC
        Dim encryptor As ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)
        Dim memoryStream As New MemoryStream()
        Dim cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
        cryptoStream.FlushFinalBlock()
        Dim cipherTextBytes As Byte() = memoryStream.ToArray()
        memoryStream.Close()
        cryptoStream.Close()
        Return Convert.ToBase64String(cipherTextBytes)
    End Function

End Class
