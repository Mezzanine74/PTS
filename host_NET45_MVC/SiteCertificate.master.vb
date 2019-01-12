Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.HttpBrowserCapabilities
Partial Class SiteCertificate
    Inherits System.Web.UI.MasterPage

    Protected Sub LoginStatus_LoggedOut(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginStatus.LoggedOut
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Roles.IsUserInRole("Certification") Then
            Response.Redirect("~/AccessDenied.aspx")
        End If

        ' banned browsers to stop
        If Not IsPostBack Then
            If CheckBrowser.Validated Then
                ' do nothing 
            Else
                PanelShowBrowserError.Visible = True
                AspPanel.Visible = False
                'Exit Sub
            End If
        End If

        If Request.QueryString("ReturnUrl") IsNot Nothing Then
            If Request.ServerVariables("URL").ToString.ToLower = "/login.aspx" And Request.QueryString("ReturnUrl").ToLower = "/vote.aspx" Then
                ' dont ban browser for vote.aspx

                PanelShowBrowserError.Visible = False
                AspPanel.Visible = True

            End If
        Else

        End If

        If Page.User.Identity.Name.ToLower = "auditers" Then
            If HttpContext.Current.Request.Url.AbsolutePath.ToLower <> "/contractview.aspx" Then
                Page.Visible = False
                Response.Write("<h1>You cannot go this page. Sorry!</h1>")

            End If
        End If

        ' XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        ' XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX THIS CODE FOLLOW USER  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        ' XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        Dim mycommantask As New MyCommonTasks
        mycommantask.UpdateFollowUsersTable(Page, SqlDataSourceFollowUsers)
        mycommantask = Nothing

        ' XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        ' XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX //THIS CODE FOLLOW USER  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        ' XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    End Sub

End Class