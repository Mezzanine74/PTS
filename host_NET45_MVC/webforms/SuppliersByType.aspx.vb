Imports System.Data.SqlClient
Partial Class SuppliersByType
    Inherits System.Web.UI.Page

  Protected Sub DataListSupplierByType_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataListSupplierByType.ItemDataBound
    If e.Item.ItemType = ListItemType.Header Then
      DirectCast(e.Item.FindControl("LabelSupplierType"), Label).Text = GetSupplierType(Request.QueryString("SupplierTypeId"))
    End If
  End Sub

  Protected Function GetSupplierType(ByVal SupplierTypeId As Integer) As String
    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    con.Open()
    Dim sqlstring As String = " SELECT     TOP (100) PERCENT RTRIM(SupplierType) AS SupplierType " + _
    " FROM         dbo.Table_SupplierType " + _
    " WHERE     (SupplierTypeId = " + SupplierTypeId.ToString + ") "
    Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim dr As SqlDataReader = cmd.ExecuteReader
    Dim SupplierType As String = ""
    While dr.Read
      SupplierType = dr(0).ToString
    End While
    con.Close()
    dr.Close()
    Return SupplierType
  End Function
End Class
