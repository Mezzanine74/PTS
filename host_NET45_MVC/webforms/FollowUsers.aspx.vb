
Partial Class FollowUsers
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Protected Sub DropDownListUserName_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListUserName.DataBound
        If Not IsPostBack Then
            Dim lst1 As New ListItem("All Users", String.Empty)
            Me.DropDownListUserName.Items.Insert(0, lst1)
        End If
    End Sub

    Protected Sub DropDownListPageName_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPageName.DataBound
        If Not IsPostBack Then
            Dim lst1 As New ListItem("All Pages", String.Empty)
            Me.DropDownListPageName.Items.Insert(0, lst1)
        End If
    End Sub
End Class
