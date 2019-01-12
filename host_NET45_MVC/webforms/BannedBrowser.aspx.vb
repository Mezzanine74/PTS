Imports System.IO

Partial Class BannedBrowser
  Inherits System.Web.UI.Page

  Public Sub message(ByVal Message As String)
    Dim BuildScript As New System.Text.StringBuilder
    Dim cs As ClientScriptManager = Page.ClientScript
    BuildScript.Append("<script>")
    BuildScript.Append(Environment.NewLine)
    BuildScript.Append("alert('" & Message & "');")
    BuildScript.Append(Environment.NewLine)
    BuildScript.Append("</" + "script>")
    cs.RegisterStartupScript(Me.[GetType](), "asd", BuildScript.ToString())
  End Sub

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    Dim updatedPageSource As String = ""
    ' render the markup into the output stream verbatim
    Response.Write(updatedPageSource)
  End Sub
End Class
