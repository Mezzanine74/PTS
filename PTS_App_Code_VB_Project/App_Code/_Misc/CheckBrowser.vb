Imports Microsoft.VisualBasic
Imports System.Web

Public Class CheckBrowser
    Shared Function Validated() As Boolean
        Dim browserTypeCheck As String = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version
        Dim _return As Boolean = False
        ' i added this function, becase Scheduled Works on VB.NET not browser compatiple.
        If HttpContext.Current.Request.ServerVariables("remote_addr").ToString = "10.8.35.4" Or _
           HttpContext.Current.Request.ServerVariables("remote_addr").ToString = "10.8.35.6" Or _
           HttpContext.Current.Request.ServerVariables("remote_addr").ToString = "10.8.35.7" Then
            _return = True
            Return _return
            Exit Function
        End If

        If browserTypeCheck = "IE 8.0" _
          OrElse browserTypeCheck = "IE 9.0" _
          OrElse browserTypeCheck = "IE 10.0" _
          OrElse InStr(browserTypeCheck, "AppleMAC") > 0 _
          OrElse InStr(browserTypeCheck, "Safari") > 0 _
          OrElse InStr(browserTypeCheck, "Chrome") _
          OrElse browserTypeCheck = "Mozilla 0.0" Then
            _return = True
        Else
            _return = False
        End If
        Return _return
    End Function
End Class
