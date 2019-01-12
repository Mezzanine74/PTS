Imports System.Data.SqlClient
Partial Class PaymentTerms
  Inherits System.Web.UI.Page

  Dim Column1Total As Decimal = 0.0
  Dim Column2Total As Decimal = 0.0
  Dim Column3Total As Decimal = 0.0
  Dim Column4Total As Decimal = 0.0


  Protected Sub GridViewPaymentTerms_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewPaymentTerms.RowCommand
    Dim zoneId As String = "Russian Standard Time"
    Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
    Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = ""

            If result.DayOfWeek.ToString = "Monday" Then
                If (e.CommandName = "Column1") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(11).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = NULL WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(11).Text) = 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn1.Text, 7, 4) + "-" + Mid(LabelHeaderColumn1.Text, 4, 2) + "-" + Mid(LabelHeaderColumn1.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                ElseIf (e.CommandName = "Column2") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(11).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn2.Text, 7, 4) + "-" + Mid(LabelHeaderColumn2.Text, 4, 2) + "-" + Mid(LabelHeaderColumn2.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(13).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn1.Text, 7, 4) + "-" + Mid(LabelHeaderColumn1.Text, 4, 2) + "-" + Mid(LabelHeaderColumn1.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                ElseIf (e.CommandName = "Column3") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(13).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(15).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn2.Text, 7, 4) + "-" + Mid(LabelHeaderColumn2.Text, 4, 2) + "-" + Mid(LabelHeaderColumn2.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                ElseIf (e.CommandName = "Column4") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(15).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn4.Text, 7, 4) + "-" + Mid(LabelHeaderColumn4.Text, 4, 2) + "-" + Mid(LabelHeaderColumn4.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(17).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                End If

            ElseIf result.DayOfWeek.ToString = "Tuesday" Then
                If (e.CommandName = "Column1") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(11).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = NULL WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(11).Text) = 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn1.Text, 7, 4) + "-" + Mid(LabelHeaderColumn1.Text, 4, 2) + "-" + Mid(LabelHeaderColumn1.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                ElseIf (e.CommandName = "Column2") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(11).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn2.Text, 7, 4) + "-" + Mid(LabelHeaderColumn2.Text, 4, 2) + "-" + Mid(LabelHeaderColumn2.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(13).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn1.Text, 7, 4) + "-" + Mid(LabelHeaderColumn1.Text, 4, 2) + "-" + Mid(LabelHeaderColumn1.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                ElseIf (e.CommandName = "Column3") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(13).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(15).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn2.Text, 7, 4) + "-" + Mid(LabelHeaderColumn2.Text, 4, 2) + "-" + Mid(LabelHeaderColumn2.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                ElseIf (e.CommandName = "Column4") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(15).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn4.Text, 7, 4) + "-" + Mid(LabelHeaderColumn4.Text, 4, 2) + "-" + Mid(LabelHeaderColumn4.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(17).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                End If

            ElseIf result.DayOfWeek.ToString = "Wednesday" Then
                If (e.CommandName = "Column1") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn2.Text, 7, 4) + "-" + Mid(LabelHeaderColumn2.Text, 4, 2) + "-" + Mid(LabelHeaderColumn2.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                ElseIf (e.CommandName = "Column2") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = NULL WHERE PayReqNo = " + row.Cells(0).Text
                ElseIf (e.CommandName = "Column3") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(13).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(15).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn2.Text, 7, 4) + "-" + Mid(LabelHeaderColumn2.Text, 4, 2) + "-" + Mid(LabelHeaderColumn2.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                ElseIf (e.CommandName = "Column4") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(15).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn4.Text, 7, 4) + "-" + Mid(LabelHeaderColumn4.Text, 4, 2) + "-" + Mid(LabelHeaderColumn4.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(17).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                End If

            ElseIf result.DayOfWeek.ToString = "Thursday" Then
                If (e.CommandName = "Column1") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn2.Text, 7, 4) + "-" + Mid(LabelHeaderColumn2.Text, 4, 2) + "-" + Mid(LabelHeaderColumn2.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                ElseIf (e.CommandName = "Column2") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = NULL WHERE PayReqNo = " + row.Cells(0).Text
                ElseIf (e.CommandName = "Column3") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(13).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(15).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn2.Text, 7, 4) + "-" + Mid(LabelHeaderColumn2.Text, 4, 2) + "-" + Mid(LabelHeaderColumn2.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                ElseIf (e.CommandName = "Column4") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(15).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn4.Text, 7, 4) + "-" + Mid(LabelHeaderColumn4.Text, 4, 2) + "-" + Mid(LabelHeaderColumn4.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(17).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                End If

            ElseIf result.DayOfWeek.ToString = "Friday" Then
                If (e.CommandName = "Column1") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                ElseIf (e.CommandName = "Column2") Then
                    ' not possible
                ElseIf (e.CommandName = "Column3") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = NULL WHERE PayReqNo = " + row.Cells(0).Text
                ElseIf (e.CommandName = "Column4") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(15).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn4.Text, 7, 4) + "-" + Mid(LabelHeaderColumn4.Text, 4, 2) + "-" + Mid(LabelHeaderColumn4.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(17).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                End If

            ElseIf result.DayOfWeek.ToString = "Saturday" Then
                If (e.CommandName = "Column1") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                ElseIf (e.CommandName = "Column2") Then
                    ' not possible
                ElseIf (e.CommandName = "Column3") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = NULL WHERE PayReqNo = " + row.Cells(0).Text
                ElseIf (e.CommandName = "Column4") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(15).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn4.Text, 7, 4) + "-" + Mid(LabelHeaderColumn4.Text, 4, 2) + "-" + Mid(LabelHeaderColumn4.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(17).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                End If

            ElseIf result.DayOfWeek.ToString = "Sunday" Then
                If (e.CommandName = "Column1") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                ElseIf (e.CommandName = "Column2") Then
                    ' not possible
                ElseIf (e.CommandName = "Column3") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = NULL WHERE PayReqNo = " + row.Cells(0).Text
                ElseIf (e.CommandName = "Column4") Then
                    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                    Dim row As GridViewRow = GridViewPaymentTerms.Rows(index)
                    If Len(row.Cells(15).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn4.Text, 7, 4) + "-" + Mid(LabelHeaderColumn4.Text, 4, 2) + "-" + Mid(LabelHeaderColumn4.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    ElseIf Len(row.Cells(17).Text) > 0 Then
                        sqlstring = "UPDATE Table4_PaymentRequest SET [PaymentTerm] = " + "'" + Mid(LabelHeaderColumn3.Text, 7, 4) + "-" + Mid(LabelHeaderColumn3.Text, 4, 2) + "-" + Mid(LabelHeaderColumn3.Text, 1, 2) + "'" + " WHERE PayReqNo = " + row.Cells(0).Text
                    End If
                End If

            End If

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
            con.Dispose()

        End Using

        GridViewPaymentTerms.DataBind()

    End Sub

    Protected Sub GridViewPaymentTerms_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewPaymentTerms.RowCreated
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)



        ' disable tuesday and thursday if required
        If e.Row.RowType = DataControlRowType.DataRow Then
            If result.DayOfWeek.ToString = "Wednesday" OrElse result.DayOfWeek.ToString = "Thursday" Then
                e.Row.Cells(11).BackColor = System.Drawing.Color.LightGray
            ElseIf result.DayOfWeek.ToString = "Friday" OrElse result.DayOfWeek.ToString = "Saturday" OrElse result.DayOfWeek.ToString = "Sunday" Then
                e.Row.Cells(11).BackColor = System.Drawing.Color.LightGray
                e.Row.Cells(13).BackColor = System.Drawing.Color.LightGray
            End If
        End If

        ' define headers
        If e.Row.RowType = DataControlRowType.Header Then
            If result.DayOfWeek.ToString = "Monday" Then
                e.Row.Cells(11).Text = String.Format("{0:d}", result.AddDays(1)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(13).Text = String.Format("{0:d}", result.AddDays(3)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(15).Text = String.Format("{0:d}", result.AddDays(8)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(17).Text = String.Format("{0:d}", result.AddDays(10)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"

                LabelHeaderColumn1.Text = String.Format("{0:d}", result.AddDays(1))
                LabelHeaderColumn2.Text = String.Format("{0:d}", result.AddDays(3))
                LabelHeaderColumn3.Text = String.Format("{0:d}", result.AddDays(8))
                LabelHeaderColumn4.Text = String.Format("{0:d}", result.AddDays(10))

            ElseIf result.DayOfWeek.ToString = "Tuesday" Then
                e.Row.Cells(11).Text = String.Format("{0:d}", result.AddDays(0)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(13).Text = String.Format("{0:d}", result.AddDays(2)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(15).Text = String.Format("{0:d}", result.AddDays(7)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(17).Text = String.Format("{0:d}", result.AddDays(9)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"

                LabelHeaderColumn1.Text = String.Format("{0:d}", result.AddDays(0))
                LabelHeaderColumn2.Text = String.Format("{0:d}", result.AddDays(2))
                LabelHeaderColumn3.Text = String.Format("{0:d}", result.AddDays(7))
                LabelHeaderColumn4.Text = String.Format("{0:d}", result.AddDays(9))

            ElseIf result.DayOfWeek.ToString = "Wednesday" Then
                e.Row.Cells(11).Text = String.Format("{0:d}", result.AddDays(-1)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(13).Text = String.Format("{0:d}", result.AddDays(1)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(15).Text = String.Format("{0:d}", result.AddDays(6)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(17).Text = String.Format("{0:d}", result.AddDays(8)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"

                LabelHeaderColumn1.Text = String.Format("{0:d}", result.AddDays(-1))
                LabelHeaderColumn2.Text = String.Format("{0:d}", result.AddDays(1))
                LabelHeaderColumn3.Text = String.Format("{0:d}", result.AddDays(6))
                LabelHeaderColumn4.Text = String.Format("{0:d}", result.AddDays(8))

            ElseIf result.DayOfWeek.ToString = "Thursday" Then
                e.Row.Cells(11).Text = String.Format("{0:d}", result.AddDays(-2)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(13).Text = String.Format("{0:d}", result.AddDays(0)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(15).Text = String.Format("{0:d}", result.AddDays(5)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(17).Text = String.Format("{0:d}", result.AddDays(7)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"

                LabelHeaderColumn1.Text = String.Format("{0:d}", result.AddDays(-2))
                LabelHeaderColumn2.Text = String.Format("{0:d}", result.AddDays(0))
                LabelHeaderColumn3.Text = String.Format("{0:d}", result.AddDays(5))
                LabelHeaderColumn4.Text = String.Format("{0:d}", result.AddDays(7))

            ElseIf result.DayOfWeek.ToString = "Friday" Then
                e.Row.Cells(11).Text = String.Format("{0:d}", result.AddDays(-3)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(13).Text = String.Format("{0:d}", result.AddDays(-1)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(15).Text = String.Format("{0:d}", result.AddDays(4)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(17).Text = String.Format("{0:d}", result.AddDays(6)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"

                LabelHeaderColumn1.Text = String.Format("{0:d}", result.AddDays(-3))
                LabelHeaderColumn2.Text = String.Format("{0:d}", result.AddDays(-1))
                LabelHeaderColumn3.Text = String.Format("{0:d}", result.AddDays(4))
                LabelHeaderColumn4.Text = String.Format("{0:d}", result.AddDays(6))

            ElseIf result.DayOfWeek.ToString = "Saturday" Then
                e.Row.Cells(11).Text = String.Format("{0:d}", result.AddDays(-4)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(13).Text = String.Format("{0:d}", result.AddDays(-2)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(15).Text = String.Format("{0:d}", result.AddDays(3)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(17).Text = String.Format("{0:d}", result.AddDays(5)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"

                LabelHeaderColumn1.Text = String.Format("{0:d}", result.AddDays(-4))
                LabelHeaderColumn2.Text = String.Format("{0:d}", result.AddDays(-2))
                LabelHeaderColumn3.Text = String.Format("{0:d}", result.AddDays(3))
                LabelHeaderColumn4.Text = String.Format("{0:d}", result.AddDays(5))

            ElseIf result.DayOfWeek.ToString = "Sunday" Then
                e.Row.Cells(11).Text = String.Format("{0:d}", result.AddDays(-5)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(13).Text = String.Format("{0:d}", result.AddDays(-3)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(15).Text = String.Format("{0:d}", result.AddDays(2)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"
                e.Row.Cells(17).Text = String.Format("{0:d}", result.AddDays(4)) + "<br/>" + "<span style=" + """" + "color: #ffff00" + """" + ">Rub.With VAT</span>"

                LabelHeaderColumn1.Text = String.Format("{0:d}", result.AddDays(-5))
                LabelHeaderColumn2.Text = String.Format("{0:d}", result.AddDays(-3))
                LabelHeaderColumn3.Text = String.Format("{0:d}", result.AddDays(2))
                LabelHeaderColumn4.Text = String.Format("{0:d}", result.AddDays(4))

            End If
        End If

    End Sub

    Protected Sub GridViewPaymentTerms_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewPaymentTerms.RowDataBound

        Dim MyTask As New MyCommonTasks
        MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim RateTodayDollar As Decimal
        Dim RateTodayEuro As Decimal

        If Len(DropDownTodayRates.SelectedValue.ToString) = 0 Then
            RateTodayDollar = Convert.ToDecimal(Mid(DropDownListRatesLatest.SelectedValue.ToString, 1, DropDownListRatesLatest.SelectedValue.ToString.IndexOf("/")))
            RateTodayEuro = Convert.ToDecimal(Mid(DropDownListRatesLatest.SelectedValue.ToString, DropDownListRatesLatest.SelectedValue.ToString.IndexOf("/") + 2, Len(DropDownListRatesLatest.SelectedValue.ToString) - DropDownListRatesLatest.SelectedValue.ToString.IndexOf("/")))
        Else
            RateTodayDollar = Convert.ToDecimal(Mid(DropDownTodayRates.SelectedValue.ToString, 1, DropDownTodayRates.SelectedValue.ToString.IndexOf("/")))
            RateTodayEuro = Convert.ToDecimal(Mid(DropDownTodayRates.SelectedValue.ToString, DropDownTodayRates.SelectedValue.ToString.IndexOf("/") + 2, Len(DropDownTodayRates.SelectedValue.ToString) - DropDownTodayRates.SelectedValue.ToString.IndexOf("/")))
        End If

        Dim RateLatestDollar As Decimal = Convert.ToDecimal(Mid(DropDownListRatesLatest.SelectedValue.ToString, 1, DropDownListRatesLatest.SelectedValue.ToString.IndexOf("/")))
        Dim RateLatestEuro As Decimal = Convert.ToDecimal(Mid(DropDownListRatesLatest.SelectedValue.ToString, DropDownListRatesLatest.SelectedValue.ToString.IndexOf("/") + 2, Len(DropDownListRatesLatest.SelectedValue.ToString) - DropDownListRatesLatest.SelectedValue.ToString.IndexOf("/")))

        ' define arrows
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' if PaymentTerm DOES NOT exist
            If e.Row.Cells(18).Text.IndexOf("/") = -1 Then
                If Roles.IsUserInRole("DefinePaymentTerms") Then
                    DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).Visible = True
                    DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                Else
                    ' do not show arrows

                End If


                ' if PaymentTerm EXISTS
            ElseIf e.Row.Cells(18).Text.IndexOf("/") > -1 Then
                ' decide what to do depending on day
                If result.DayOfWeek.ToString = "Monday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        ' use first column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton2"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton2"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If

                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(11).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(11).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(11).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        ' use second column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton2"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton2"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If

                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        ' use third column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        ' use fourth column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    End If
                ElseIf result.DayOfWeek.ToString = "Tuesday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        ' invoices created by today to be highlighted
                        If Len(DataBinder.Eval(e.Row.DataItem, "CreatedBy").ToString) > 0 Then
                            If String.Format("{0:d}", DataBinder.Eval(e.Row.DataItem, "CreatedBy")) = LabelHeaderColumn1.Text Then
                                e.Row.BackColor = System.Drawing.Color.Gold
                                e.Row.Cells(3).Text = e.Row.Cells(3).Text + " " + "<span style=" + """" + "font-family: tahoma,arial,helvetica,sans-serif; color: #ff0000; font-size: 10px; font-weight: bold" + """" + ">(Requested Today)</span>"
                            End If
                        End If

                        ' use first column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton2"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton2"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(11).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(11).Text = String.Format("{0:#,##0.00}", RateTodayDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(11).Text = String.Format("{0:#,##0.00}", RateTodayEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        ' use second column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton2"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton2"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        ' use third column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        ' use fourth column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    End If
                ElseIf result.DayOfWeek.ToString = "Wednesday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        'use only first arrow key
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        ' use second column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton2"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton2"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        ' use third column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        ' use fourth column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    End If
                ElseIf result.DayOfWeek.ToString = "Thursday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        ' use first arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        ' invoices created by today to be highlighted
                        If Len(DataBinder.Eval(e.Row.DataItem, "CreatedBy").ToString) > 0 Then
                            If String.Format("{0:d}", DataBinder.Eval(e.Row.DataItem, "CreatedBy")) = LabelHeaderColumn2.Text Then
                                e.Row.BackColor = System.Drawing.Color.Gold
                                e.Row.Cells(3).Text = e.Row.Cells(3).Text + " " + "<span style=" + """" + "font-family: tahoma,arial,helvetica,sans-serif; color: #ff0000; font-size: 10px; font-weight: bold" + """" + ">(Requested Today)</span>"
                            End If
                        End If

                        ' use second column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton2"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton2"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", RateTodayDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", RateTodayEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        ' use third column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        ' use fourth column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    End If
                ElseIf result.DayOfWeek.ToString = "Friday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        ' use first arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        ' use first arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        ' use third column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        ' use fourth column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    End If
                ElseIf result.DayOfWeek.ToString = "Saturday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        ' use first arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        ' use first arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        ' use third column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        ' use fourth column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    End If
                ElseIf result.DayOfWeek.ToString = "Sunday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        ' use first arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        ' use first arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        ' use third column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton3"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/RightArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        ' use fourth column arrow keys
                        If Roles.IsUserInRole("DefinePaymentTerms") Then
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).Visible = True
                            DirectCast(e.Row.FindControl("ImageButton4"), ImageButton).ImageUrl = "~/Images/LeftArrow.png"
                        Else
                            ' do not show arrows

                        End If


                        If e.Row.Cells(5).Text = "Rub" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Dollar" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestDollar * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        ElseIf e.Row.Cells(5).Text = "Euro" Then
                            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", RateLatestEuro * Convert.ToDecimal(e.Row.Cells(9).Text.Replace(",", "")))
                        End If

                    End If
                End If
            End If

            ' Sum all Columns for footer
            If Len(e.Row.Cells(11).Text) = 0 Then
                Column1Total = Column1Total
            ElseIf Len(e.Row.Cells(11).Text) > 0 Then
                Column1Total = Column1Total + Convert.ToDecimal(e.Row.Cells(11).Text.Replace(",", ""))
            End If

            If Len(e.Row.Cells(13).Text) = 0 Then
                Column2Total = Column2Total
            ElseIf Len(e.Row.Cells(13).Text) > 0 Then
                Column2Total = Column2Total + Convert.ToDecimal(e.Row.Cells(13).Text.Replace(",", ""))
            End If

            If Len(e.Row.Cells(15).Text) = 0 Then
                Column3Total = Column3Total
            ElseIf Len(e.Row.Cells(15).Text) > 0 Then
                Column3Total = Column3Total + Convert.ToDecimal(e.Row.Cells(15).Text.Replace(",", ""))
            End If

            If Len(e.Row.Cells(17).Text) = 0 Then
                Column4Total = Column4Total
            ElseIf Len(e.Row.Cells(17).Text) > 0 Then
                Column4Total = Column4Total + Convert.ToDecimal(e.Row.Cells(17).Text.Replace(",", ""))
            End If

        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(11).Text = String.Format("{0:#,##0.00}", Column1Total)
            e.Row.Cells(13).Text = String.Format("{0:#,##0.00}", Column2Total)
            e.Row.Cells(15).Text = String.Format("{0:#,##0.00}", Column3Total)
            e.Row.Cells(17).Text = String.Format("{0:#,##0.00}", Column4Total)

            e.Row.Cells(11).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(13).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(15).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(17).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells(11).BackColor = System.Drawing.Color.Red
            e.Row.Cells(13).BackColor = System.Drawing.Color.Red
            e.Row.Cells(15).BackColor = System.Drawing.Color.Red
            e.Row.Cells(17).BackColor = System.Drawing.Color.Red

            e.Row.Cells(11).ForeColor = System.Drawing.Color.White
            e.Row.Cells(13).ForeColor = System.Drawing.Color.White
            e.Row.Cells(15).ForeColor = System.Drawing.Color.White
            e.Row.Cells(17).ForeColor = System.Drawing.Color.White
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Or Not IsPostBack Then
            TextBoxUserName.Text = Page.User.Identity.Name
        End If

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        SqlDataSourceTodayRates.SelectCommand = "SELECT rtrim(convert(NvarChar(7),[RubbleDollar]))+ " +
      " N'/'+rtrim(convert(NvarChar(7),[RubbleEuro]))  AS Rates FROM [Table8_ExchangeRates] " +
      " WHERE Date = '" + Mid(result.ToString, 7, 4) + "-" + Mid(result.ToString, 4, 2) + "-" + Mid(result.ToString, 1, 2) + "'"

        DropDownTodayRates.DataBind()
        DropDownListRatesLatest.DataBind()

        If Len(DropDownTodayRates.SelectedValue.ToString) = 0 Then
            LabelRatesToShow.Text = "<span style=" + """" + "font-size: 10px; font-weight: bold; text-decoration: underline" + """" + ">The latest rates:</span><span style=" + """" + "font-size: 10px" + """" + "> 1 Euro = </span><span style=" + """" + "color: #0000ff; font-size: 10px; font-weight: bold; text-decoration: underline" + """" + ">" + Mid(DropDownListRatesLatest.SelectedValue.ToString, DropDownListRatesLatest.SelectedValue.ToString.IndexOf("/") + 2, Len(DropDownListRatesLatest.SelectedValue.ToString) - DropDownListRatesLatest.SelectedValue.ToString.IndexOf("/")) + "</span><span style=" + """" + "font-size: 10px" + """" + "> Ruble, 1 Dollar = </span><span style=" + """" + "color: #0000ff; font-size: 10px; font-weight: bold; text-decoration: underline" + """" + ">" + Mid(DropDownListRatesLatest.SelectedValue.ToString, 1, DropDownListRatesLatest.SelectedValue.ToString.IndexOf("/")) + "</span><span style=" + """" + "font-size: 10px" + """" + "> Ruble</span>"
        Else
            LabelRatesToShow.Text = "<span style=" + """" + "font-size: 10px; font-weight: bold; text-decoration: underline" + """" + ">Today Rate:</span><span style=" + """" + "font-size: 10px" + """" + "> 1 Euro = </span><span style=" + """" + "color: #0000ff; font-size: 10px; font-weight: bold; text-decoration: underline" + """" + ">" + Mid(DropDownTodayRates.SelectedValue.ToString, DropDownTodayRates.SelectedValue.ToString.IndexOf("/") + 2, Len(DropDownTodayRates.SelectedValue.ToString) - DropDownTodayRates.SelectedValue.ToString.IndexOf("/")) + "</span><span style=" + """" + "font-size: 10px" + """" + "> Ruble, 1 Dollar = </span><span style=" + """" + "color: #0000ff; font-size: 10px; font-weight: bold; text-decoration: underline" + """" + ">" + Mid(DropDownTodayRates.SelectedValue.ToString, 1, DropDownTodayRates.SelectedValue.ToString.IndexOf("/")) + "</span><span style=" + """" + "font-size: 10px" + """" + "> Ruble</span>"
        End If

        If Not IsPostBack Then
            ' Reset All Payment Terms Which is earlier than MinRequiredDate and later than MaxRequiredDate
            Dim MinRequiredDate As String = ""
            Dim MaxRequiredDate As String = ""

            If result.DayOfWeek.ToString = "Monday" Then
                MinRequiredDate = String.Format("{0:d}", result.AddDays(1))
                MaxRequiredDate = String.Format("{0:d}", result.AddDays(10))

            ElseIf result.DayOfWeek.ToString = "Tuesday" Then
                MinRequiredDate = String.Format("{0:d}", result.AddDays(0))
                MaxRequiredDate = String.Format("{0:d}", result.AddDays(9))

            ElseIf result.DayOfWeek.ToString = "Wednesday" Then
                MinRequiredDate = String.Format("{0:d}", result.AddDays(-1))
                MaxRequiredDate = String.Format("{0:d}", result.AddDays(8))

            ElseIf result.DayOfWeek.ToString = "Thursday" Then
                MinRequiredDate = String.Format("{0:d}", result.AddDays(-2))
                MaxRequiredDate = String.Format("{0:d}", result.AddDays(7))

            ElseIf result.DayOfWeek.ToString = "Friday" Then
                MinRequiredDate = String.Format("{0:d}", result.AddDays(-3))
                MaxRequiredDate = String.Format("{0:d}", result.AddDays(6))

            ElseIf result.DayOfWeek.ToString = "Saturday" Then
                MinRequiredDate = String.Format("{0:d}", result.AddDays(-4))
                MaxRequiredDate = String.Format("{0:d}", result.AddDays(5))

            ElseIf result.DayOfWeek.ToString = "Sunday" Then
                MinRequiredDate = String.Format("{0:d}", result.AddDays(-5))
                MaxRequiredDate = String.Format("{0:d}", result.AddDays(4))

            End If

            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " UPDATE [Table4_PaymentRequest] SET [PaymentTerm] = NULL " +
                " WHERE Table4_PaymentRequest.PaymentTerm < " + "'" + Mid(MinRequiredDate, 7, 4) + "-" + Mid(MinRequiredDate, 4, 2) + "-" + Mid(MinRequiredDate, 1, 2) + "'" + " " +
                " OR Table4_PaymentRequest.PaymentTerm > " + "'" + Mid(MaxRequiredDate, 7, 4) + "-" + Mid(MaxRequiredDate, 4, 2) + "-" + Mid(MaxRequiredDate, 1, 2) + "'" + " "

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                Dim dr As SqlDataReader = cmd.ExecuteReader
                con.Close()
                dr.Close()
                con.Dispose()

            End Using
        End If

  End Sub

  Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
    Dim lst1 As New ListItem("ALL Project", String.Empty)
    Me.DropDownListPrj.Items.Insert(0, lst1)
  End Sub

  Protected Sub DDLsort_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLsort.SelectedIndexChanged
    If DDLsort.SelectedValue = "Sort By Payment Term" Then
      GridViewPaymentTerms.Sort("PaymentTerm", SortDirection.Ascending)
    ElseIf DDLsort.SelectedValue = "Not Sorted" Then
      GridViewPaymentTerms.Sort("ProjectName, PayReqNo", SortDirection.Ascending)
    End If
  End Sub

End Class
