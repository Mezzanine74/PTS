Imports Microsoft.VisualBasic

Public Class DomainPTS

    Shared Function GetDomain() As String

        ' it returns this format http://pts.mercuryeng.ru
        'Return "http://" + HttpContext.Current.Request.Url.Host ' THIS DIDNT WORK
        Return "http://pts.mercuryeng.ru"

    End Function

End Class
