Imports System.Data
Imports System.Data.SqlClient

Partial Class PTS_1S_DeliverySearchPTSEntry
    Inherits System.Web.UI.Page



    Protected Sub SqlDataSourceSearchDeliveryDocInPTS_Selecting(sender As Object, e As SqlDataSourceSelectingEventArgs) Handles SqlDataSourceSearchDeliveryDocInPTS.Selecting


        If TextBoxDocDate.Text.Trim.Length = 0 Then
            DirectCast(e.Command, SqlCommand).Parameters("@DocDate").Value = DBNull.Value

        End If

    End Sub

    Protected Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click

        GridViewSearchResult.DataBind()

        If TextBoxDocDate.Text.Trim.Length > 0 Then

            SqlDataSourceSearchDeliveryDocInPTS.SelectParameters("DocDate").DefaultValue = Convert.ToDateTime(Mid(TextBoxDocDate.Text.Trim, 7, 4) + "/" + Mid(TextBoxDocDate.Text.Trim, 4, 2) + "/" + Mid(TextBoxDocDate.Text.Trim, 1, 2))

        End If

        If TextBoxDocNo.Text.Trim.Length = 0 Then
            SqlDataSourceSearchDeliveryDocInPTS.SelectParameters("DocNo").DefaultValue = ""

        End If

        If TextBoxSupplierINN.Text.Trim.Length = 0 Then
            SqlDataSourceSearchDeliveryDocInPTS.SelectParameters("SupplierID").DefaultValue = ""

        End If

    End Sub

    Protected Sub GridViewSearchResult_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewSearchResult.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim HyLinkToComparison As HyperLink = DirectCast(e.Row.FindControl("HyLinkToComparison"), HyperLink)

            If HyLinkToComparison IsNot Nothing Then
                HyLinkToComparison.NavigateUrl = "~/webforms/PTS_1S_DeliveryDocComparisonGrid.aspx?ProjectID=" + Mid(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString, 4, 3) + _
                    "&SupplierID=" + DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString + _
                    "&ID_PTS=" + DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString
            End If

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim parameter As String = DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString

                If (Aggregate C In db.Table_Delivery_MatchingIndexes Where C.Index_PTS_Or_1S = parameter Into Count()) > 0 Then
                    e.Row.Cells(14).BackColor = System.Drawing.Color.LightGreen
                Else
                    e.Row.Cells(14).BackColor = System.Drawing.Color.LightPink
                End If

            End Using

        End If

    End Sub
End Class
