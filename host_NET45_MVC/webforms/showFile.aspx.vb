
Partial Class showFile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Request.QueryString("link") IsNot Nothing Then

            Dim _link As String = ""

            If Left(Request.QueryString("link").Trim(), 1) = "~" Then
                _link = Mid(Request.QueryString("link").Trim(), 2, Len(Request.QueryString("link").Trim()) - 1)
            Else
                _link = Request.QueryString("link").Trim()
            End If

            iframepdf.Attributes.Add("src", _link)

        End If
    End Sub

End Class
