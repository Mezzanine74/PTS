
Partial Class Pages_PTS_UserControls_WebUserControlContractDetails
    Inherits System.Web.UI.UserControl

    Private _contractidParameter As Integer = 0
    Public Property contractidParameter() As Integer
        Get
            Return _contractidParameter
        End Get
        Set(ByVal value As Integer)
            contractid.Value = value
        End Set
    End Property

    Protected Sub FormviewcontractDetails_CallingDataMethods(sender As Object, e As CallingDataMethodsEventArgs)

        e.DataMethodsObject = New PTS_MERCURY.BL.BL()

    End Sub

    Protected Sub GridviewApprovalStatuscontract_RowDataBound(sender As Object, e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            If DataBinder.Eval(e.Row.DataItem, "Approved").ToString() = "1" Then
                e.Row.Cells(1).Text = "YES"
            Else
                e.Row.Cells(1).Text = "NO"
            End If


            If DataBinder.Eval(e.Row.DataItem, "Rank").ToString() = "0" Then

                e.Row.BackColor = Drawing.Color.LightGray
                e.Row.Font.Italic = True
                e.Row.ForeColor = Drawing.Color.Gray

            End If

        End If

    End Sub
End Class
