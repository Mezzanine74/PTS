Imports Microsoft.VisualBasic

Public Class PreventMultipleClick

    Public Shared Sub PreventMultipleClicks(ByRef _Linkbutton As System.Web.UI.WebControls.LinkButton)
        _Linkbutton.Attributes.Add("onclick", "this.disabled=true;" & _Linkbutton.Page.ClientScript.GetPostBackEventReference(_Linkbutton, String.Empty).ToString)
    End Sub

End Class
