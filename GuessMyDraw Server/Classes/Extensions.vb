Public Class IDictionaryExtensions
    Private Sub New()
    End Sub

    Public Shared Function FindKeyByValue(Of TKey, TValue)(dictionary As IDictionary(Of TKey, TValue), value As TValue) As TKey
        If dictionary Is Nothing Then
            Throw New ArgumentNullException("dictionary")
        End If

        For Each pair As KeyValuePair(Of TKey, TValue) In dictionary
            If value.Equals(pair.Value) Then
                Return pair.Key
            End If
        Next

        Throw New Exception("the value is not found in the dictionary")
    End Function
End Class
