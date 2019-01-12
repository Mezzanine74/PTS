
Partial Class PTS_1S_ProjectSummaryComparison
    Inherits System.Web.UI.Page

    Dim Total1S As Decimal = 0.0
    Dim TotalPTS As Decimal = 0.0


    Protected Sub GridViewProjectSummaryComparison_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewProjectSummaryComparison.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Value1S As Decimal = 0.0
            Dim ValuePTS As Decimal = 0.0

            ' PTS sums
            If IsNumeric(e.Row.Cells(3).Text.Replace(",", String.Empty)) Then
                TotalPTS = TotalPTS + e.Row.Cells(3).Text
                ValuePTS = Convert.ToDecimal(e.Row.Cells(3).Text)
            Else
                ValuePTS = 0
            End If

            ' 1S sums
            If IsNumeric(e.Row.Cells(4).Text.Replace(",", String.Empty)) Then
                Total1S = Total1S + e.Row.Cells(4).Text
                Value1S = Convert.ToDecimal(e.Row.Cells(4).Text)
            Else
                Value1S = 0
            End If

            ' Diff
            'Dim PTSvalue As Decimal = 0
            'Dim _1Svalue As String = e.Row.Cells(4).Text

            'If Not IsNumeric(e.Row.Cells(3).Text.Replace(",", String.Empty)) Then
            '    PTSvalue = 0
            'Else
            '    PTSvalue = (e.Row.Cells(3).Text.Replace(",", String.Empty))
            'End If

            'If Not IsNumeric(e.Row.Cells(4).Text.Replace(",", String.Empty)) Then
            '    _1Svalue = 0
            'Else
            '    _1Svalue = (e.Row.Cells(4).Text.Replace(",", String.Empty))
            'End If

            Dim LiteralDiff As Literal = DirectCast(e.Row.FindControl("LiteralDiff"), Literal)
            LiteralDiff.Text = String.Format("{0:N2}", ValuePTS - Value1S)



            Dim _percent As Decimal = 0

            Try
                If Value1S < ValuePTS Then
                    _percent = Math.Round(Value1S / ValuePTS * 100, 0)
                    e.Row.Cells(6).Text = Convert.ToString(_percent) + "<a style=" + """" + "color:white;" + """" + " target=" + """" + "_blank" + """" + " href=" + """" + "/webforms/PTS_1S_DeliveryDocComparisonGrid.aspx?ProjectID=" + ddlProject.SelectedValue.ToString + "&SupplierID=" + DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString + "" + """" + "> see details</a>"
                Else
                    _percent = Math.Round(ValuePTS / Value1S * 100, 0)
                    e.Row.Cells(6).Text = Convert.ToString(_percent) + "<a style=" + """" + "color:white;" + """" + " target=" + """" + "_blank" + """" + " href=" + """" + "/webforms/PTS_1S_DeliveryDocComparisonGrid.aspx?ProjectID=" + ddlProject.SelectedValue.ToString + "&SupplierID=" + DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString + "" + """" + "> see details</a>"

                End If

            Catch ex As Exception

            End Try

            ' Provide color details
            e.Row.BackColor = GetColor(Convert.ToDecimal(_percent))


        End If

        If e.Row.RowType = DataControlRowType.Footer Then

            e.Row.Cells(3).Text = String.Format("{0:N2}", TotalPTS)
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells(4).Text = String.Format("{0:N2}", Total1S)
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells(5).Text = String.Format("{0:N2}", TotalPTS - Total1S)
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right


        End If

    End Sub

    Protected Function GetColor(ByVal _Scale As Decimal) As System.Drawing.Color

        Dim _return As System.Drawing.Color = System.Drawing.Color.White

        If _Scale >= 0 And _Scale < 10 Then
            _return = System.Drawing.Color.FromArgb(248, 105, 107)

        ElseIf _Scale >= 10 And _Scale < 20 Then
            _return = System.Drawing.Color.FromArgb(249, 133, 112)

        ElseIf _Scale >= 20 And _Scale < 30 Then
            _return = System.Drawing.Color.FromArgb(251, 162, 118)

        ElseIf _Scale >= 30 And _Scale < 40 Then
            _return = System.Drawing.Color.FromArgb(252, 191, 123)

        ElseIf _Scale >= 40 And _Scale < 50 Then
            _return = System.Drawing.Color.FromArgb(254, 220, 129)

        ElseIf _Scale >= 50 And _Scale < 60 Then
            _return = System.Drawing.Color.FromArgb(238, 230, 131)

        ElseIf _Scale >= 60 And _Scale < 70 Then
            _return = System.Drawing.Color.FromArgb(204, 221, 130)

        ElseIf _Scale >= 70 And _Scale < 80 Then
            _return = System.Drawing.Color.FromArgb(169, 210, 127)

        ElseIf _Scale >= 80 And _Scale < 90 Then
            _return = System.Drawing.Color.FromArgb(134, 201, 126)

        ElseIf _Scale >= 90 And _Scale <= 100 Then
            _return = System.Drawing.Color.FromArgb(99, 190, 123)

        End If

        Return _return

    End Function


    Protected Sub ddlProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProject.SelectedIndexChanged

        GridViewProjectSummaryComparison.DataBind()

    End Sub
End Class
