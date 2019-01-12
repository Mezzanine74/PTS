
Partial Class showKPIs
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    'iframepdf.Attributes.Add("src", Request.QueryString("link"))
    iframepdf.Attributes.Add("src", "/KPIs/KPI_master_file.xlsx")

  End Sub
End Class
