Imports System.Data.SqlClient
Partial Class PaymentTermsPDF
  Inherits System.Web.UI.Page

  Protected Sub GridViewPaymentTerms_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewPaymentTerms.RowCommand

    If (e.CommandName = "ColumnPDF1") Then
      Dim LinkToFile As String = e.CommandArgument.ToString
      Dim openpdf As New MyCommonTasks
      openpdf.OpenPDF(LinkToFile)
    End If

    If (e.CommandName = "ColumnPDF2") Then
      Dim LinkToFile As String = e.CommandArgument.ToString
      Dim openpdf As New MyCommonTasks
      openpdf.OpenPDF(LinkToFile)
    End If

    If (e.CommandName = "ColumnPDF3") Then
      Dim LinkToFile As String = e.CommandArgument.ToString
      Dim openpdf As New MyCommonTasks
      openpdf.OpenPDF(LinkToFile)
    End If

    If (e.CommandName = "ColumnPDF4") Then
      Dim LinkToFile As String = e.CommandArgument.ToString
      Dim openpdf As New MyCommonTasks
      openpdf.OpenPDF(LinkToFile)
    End If

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

        ' define arrows
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' if PaymentTerm DOES NOT exist
            If e.Row.Cells(18).Text.IndexOf("/") = -1 Then

                ' if PaymentTerm EXISTS
            ElseIf e.Row.Cells(18).Text.IndexOf("/") > -1 Then
                ' decide what to do depending on day
                If result.DayOfWeek.ToString = "Monday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then

                        DirectCast(e.Row.FindControl("ImageButtonPDF1"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF1"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"
                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(11).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(11).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(11).BorderWidth = 2
                            e.Row.Cells(11).BackColor = System.Drawing.Color.LightCoral
                        End If
                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF2"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF2"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"
                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(13).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(13).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(13).BorderWidth = 2
                            e.Row.Cells(13).BackColor = System.Drawing.Color.LightCoral
                        End If
                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"
                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(15).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(15).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(15).BorderWidth = 2
                            e.Row.Cells(15).BackColor = System.Drawing.Color.LightCoral
                        End If
                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"
                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(17).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(17).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(17).BorderWidth = 2
                            e.Row.Cells(17).BackColor = System.Drawing.Color.LightCoral
                        End If
                    End If
                ElseIf result.DayOfWeek.ToString = "Tuesday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        ' the latest created invoices to be highlighted
                        If Len(DataBinder.Eval(e.Row.DataItem, "CreatedBy").ToString) > 0 Then
                            If String.Format("{0:d}", Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "CreatedBy"))) = LabelHeaderColumn1.Text Then
                                e.Row.BackColor = System.Drawing.Color.Gold
                                e.Row.Cells(3).Text = e.Row.Cells(3).Text + " " + "<span style=" + """" + "font-family: tahoma,arial,helvetica,sans-serif; color: #ff0000; font-size: 10px; font-weight: bold" + """" + ">(Requested Today)</span>"
                            End If
                        End If

                        DirectCast(e.Row.FindControl("ImageButtonPDF1"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF1"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(11).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(11).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(11).BorderWidth = 2
                            e.Row.Cells(11).BackColor = System.Drawing.Color.LightCoral
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF2"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF2"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(13).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(13).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(13).BorderWidth = 2
                            e.Row.Cells(13).BackColor = System.Drawing.Color.LightCoral
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(15).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(15).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(15).BorderWidth = 2
                            e.Row.Cells(15).BackColor = System.Drawing.Color.LightCoral
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(17).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(17).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(17).BorderWidth = 2
                            e.Row.Cells(17).BackColor = System.Drawing.Color.LightCoral
                        End If

                    End If
                ElseIf result.DayOfWeek.ToString = "Wednesday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        ' do nothing

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF2"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF2"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(13).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(13).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(13).BorderWidth = 2
                            e.Row.Cells(13).BackColor = System.Drawing.Color.LightCoral
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(15).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(15).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(15).BorderWidth = 2
                            e.Row.Cells(15).BackColor = System.Drawing.Color.LightCoral
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(17).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(17).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(17).BorderWidth = 2
                            e.Row.Cells(17).BackColor = System.Drawing.Color.LightCoral
                        End If

                    End If
                ElseIf result.DayOfWeek.ToString = "Thursday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        ' do nothing

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        ' the latest created invoices to be highlighted
                        If Len(DataBinder.Eval(e.Row.DataItem, "CreatedBy").ToString) > 0 Then
                            If String.Format("{0:d}", Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "CreatedBy"))) = LabelHeaderColumn2.Text Then
                                e.Row.BackColor = System.Drawing.Color.Gold
                                e.Row.Cells(3).Text = e.Row.Cells(3).Text + " " + "<span style=" + """" + "font-family: tahoma,arial,helvetica,sans-serif; color: #ff0000; font-size: 10px; font-weight: bold" + """" + ">(Requested Today)</span>"
                            End If
                        End If

                        DirectCast(e.Row.FindControl("ImageButtonPDF2"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF2"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(13).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(13).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(13).BorderWidth = 2
                            e.Row.Cells(13).BackColor = System.Drawing.Color.LightCoral
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(15).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(15).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(15).BorderWidth = 2
                            e.Row.Cells(15).BackColor = System.Drawing.Color.LightCoral
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(17).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(17).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(17).BorderWidth = 2
                            e.Row.Cells(17).BackColor = System.Drawing.Color.LightCoral
                        End If

                    End If
                ElseIf result.DayOfWeek.ToString = "Friday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        ' do nothing

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        ' do nothing

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(15).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(15).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(15).BorderWidth = 2
                            e.Row.Cells(15).BackColor = System.Drawing.Color.LightCoral
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(17).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(17).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(17).BorderWidth = 2
                            e.Row.Cells(17).BackColor = System.Drawing.Color.LightCoral
                        End If

                    End If
                ElseIf result.DayOfWeek.ToString = "Saturday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        ' do nothing

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        ' do nothing

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(15).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(15).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(15).BorderWidth = 2
                            e.Row.Cells(15).BackColor = System.Drawing.Color.LightCoral
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(17).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(17).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(17).BorderWidth = 2
                            e.Row.Cells(17).BackColor = System.Drawing.Color.LightCoral
                        End If

                    End If
                ElseIf result.DayOfWeek.ToString = "Sunday" Then
                    If e.Row.Cells(18).Text = LabelHeaderColumn1.Text Then
                        ' do nothing

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn2.Text Then
                        ' do nothing

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn3.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF3"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(15).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(15).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(15).BorderWidth = 2
                            e.Row.Cells(15).BackColor = System.Drawing.Color.LightCoral
                        End If

                    ElseIf e.Row.Cells(18).Text = LabelHeaderColumn4.Text Then
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).Visible = True
                        DirectCast(e.Row.FindControl("ImageButtonPDF4"), ImageButton).ImageUrl = "~/Images/PDFpaymentTerms.png"

                        If DataBinder.Eval(e.Row.DataItem, "AttachmentChange").ToString = "Changed" Then
                            e.Row.Cells(17).BorderStyle = BorderStyle.Solid
                            e.Row.Cells(17).BorderColor = System.Drawing.Color.Red
                            e.Row.Cells(17).BorderWidth = 2
                            e.Row.Cells(17).BackColor = System.Drawing.Color.LightCoral
                        End If

          End If
        End If
      End If
    End If

  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    If IsPostBack Or Not IsPostBack Then
      TextBoxUserName.Text = Page.User.Identity.Name
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
