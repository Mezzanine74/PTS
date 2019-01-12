Imports Microsoft.VisualBasic

Public Class GetCountryByIp
    Shared Function GetCountry(ByVal _ip As String) As String

        Return "error"
        Exit Function

        Dim strURL0 As String = "http://api.hostip.info/country.php?ip=" + _ip
        Dim objWebRequest0 As System.Net.HttpWebRequest
        Dim objWebResponse0 As System.Net.HttpWebResponse
        Dim streamReader0 As System.IO.StreamReader

        objWebRequest0 = CType(System.Net.WebRequest.Create(strURL0), System.Net.HttpWebRequest)
        objWebRequest0.Method = "GET"
        objWebResponse0 = CType(objWebRequest0.GetResponse(), System.Net.HttpWebResponse)

        streamReader0 = New System.IO.StreamReader(objWebResponse0.GetResponseStream)
        Return streamReader0.ReadToEnd

    End Function

End Class
