
Partial Class MissingDocs
    Inherits System.Web.UI.Page

  Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
    If Not IsPostBack Then
      Dim lst1 As New ListItem("Select Project", "0")
      Me.DropDownListPrj.Items.Insert(0, lst1)
    End If
  End Sub


  Protected Sub DropDownListPrj_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListPrj.SelectedIndexChanged
    RenderReport.Render("HTML", ReportViewerCostReport, "MissingDocPOs", "ProjectID", DropDownListPrj.SelectedValue)
  End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As EventArgs)
        RenderReport.Render("Excel", ReportViewerCostReport, "MissingDocPOs", "ProjectID", DropDownListPrj.SelectedValue)
    End Sub
End Class
