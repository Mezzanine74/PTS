
Partial Class open_po
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' it provides query parameter for DropDownListSupplier
        If IsPostBack Or Not IsPostBack Then
            TextBoxUserName.Text = Page.User.Identity.Name
        End If

        ' it removes SELECT PROJECT after posback
        If IsPostBack Then
            Dim controll1 As New String(Page.Request.Params.Get("__EVENTTARGET"))
            If (Not controll1 Is Nothing) Or (controll1 <> "") Then
                If controll1 = "ctl00$MainContent$DropDownListPrj" Then
                    If Me.DropDownListPrj.Items(0).ToString = "Select Project" Then
                        Me.DropDownListPrj.Items.RemoveAt(0)
                    End If
                End If
            End If
        End If

    End Sub

    Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
        If Not IsPostBack Then
            Dim lst1 As New ListItem("Select Project", "0")
            Me.DropDownListPrj.Items.Insert(0, lst1)
        End If

    End Sub

    Protected Sub GridViewOpenPO_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewOpenPO.RowDataBound

    Dim MyTask As New MyCommonTasks
    MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        'it defines CurrencyImage.
            If DirectCast(e.Row.FindControl("LabelCurrency"), Label) IsNot Nothing Then
                If DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text = "Rub" Then
                    DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/ruble_icon_.bmp"
                ElseIf DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text = "Dollar" Then
                    DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/dollar_icon_.bmp"
                ElseIf DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text = "Euro" Then
                    DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/euro_icon_.bmp"
                End If
            End If

    End Sub

End Class
