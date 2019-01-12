Imports System.Data.SqlClient
Imports System.Data

Partial Class Paylog2
    Inherits System.Web.UI.Page

    Dim _myCommanTask As New MyCommonTasks

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Page.User.Identity.Name.ToLower() = "dzera" OrElse Page.User.Identity.Name.ToLower() = "savas" OrElse Page.User.Identity.Name.ToLower() = "inna" OrElse Page.User.Identity.Name.ToLower() = "elmira.shabaeva" _
            OrElse Page.User.Identity.Name.ToLower() = "mariya.podobueva" OrElse Page.User.Identity.Name.ToLower() = "natalia.larionova" Then
                ' do nothing 
            Else
                Response.Redirect("~/webforms/AccessDenied.aspx")
            End If
        End If

        If Not IsPostBack Then
            PaymentDateTextBoxShown.Text = Format(DateTime.Parse(Mid(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time")).ToString(), 1, 10).ToString()), "dd/MM/yyyy")

            SqlDataSourceItemsToPay.SelectParameters("Date").DefaultValue = Mid(PaymentDateTextBoxShown.Text.ToString, 1, 2) + "/" + Mid(PaymentDateTextBoxShown.Text.ToString, 4, 2) + "/" + Mid(PaymentDateTextBoxShown.Text.ToString, 7, 4)
            SqlDataSourceItemsToPay.SelectParameters("Date").Type = TypeCode.DateTime

        End If

        If IsPostBack Or Not IsPostBack Then
            showPaidItems()
        End If

    End Sub

    Protected Sub showPaidItems()
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT count([PayReqNo]) as PaidItemNumber " + _
        "   FROM [Table5_PayLog] " + _
        "   where PaymentDate = @PaymentDate "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim UserParm As SqlParameter = cmd.Parameters.Add("@PaymentDate", System.Data.SqlDbType.SmallDateTime)
            UserParm.Value = Convert.ToDateTime(Mid(PaymentDateTextBoxShown.Text.ToString, 1, 2) + "/" + Mid(PaymentDateTextBoxShown.Text.ToString, 4, 2) + "/" + Mid(PaymentDateTextBoxShown.Text.ToString, 7, 4))
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                LabelTotalPaidItem.Text = dr(0).ToString + " items paid"
            End While
            con.Close()
            dr.Close()
            con.Dispose()

        End Using
    End Sub

    Protected Sub PaymentDateTextBoxShown_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PaymentDateTextBoxShown.TextChanged
        SqlDataSourceItemsToPay.SelectParameters("Date").DefaultValue = Mid(PaymentDateTextBoxShown.Text.ToString, 1, 2) + "/" + Mid(PaymentDateTextBoxShown.Text.ToString, 4, 2) + "/" + Mid(PaymentDateTextBoxShown.Text.ToString, 7, 4)
        SqlDataSourceItemsToPay.SelectParameters("Date").Type = TypeCode.DateTime

        ' remove all sessions
        Session.RemoveAll()

    End Sub

    Protected Sub GridViewItemsToPay_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewItemsToPay.Load
        GridViewItemsToPay.Sort("SupplierName", SortDirection.Ascending)
    End Sub

    Protected Sub GridViewItemsToPay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewItemsToPay.RowCommand
        ' it will move ideal payment value then assign into Session
        If (e.CommandName = "Move") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewItemsToPay.Rows(index)
            Session(row.Cells(0).Text.ToString) = row.Cells(8).Text.Replace(",", "")
        End If
    End Sub

    Protected Sub GridViewItemsToPay_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewItemsToPay.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            ' it resets TextBoxFinanceNoValidation
            TextBoxFinanceNoValidation.Text = "0"
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then

            ' Assign Exchange rates to TextBoxes
            Dim TextBoxDollar As TextBox = DirectCast(e.Row.FindControl("TextBoxDollar"), TextBox)
            Dim TextBoxEuro As TextBox = DirectCast(e.Row.FindControl("TextBoxEuro"), TextBox)
            Dim LabelPayReqNo As Label = DirectCast(e.Row.FindControl("LabelPayReqNo"), Label)

            If Session(TextBoxDollar.ID.ToString() + LabelPayReqNo.Text) IsNot Nothing Then
                TextBoxDollar.Text = Session(TextBoxDollar.ID.ToString() + LabelPayReqNo.Text)
            Else
                TextBoxDollar.Text = DataBinder.Eval(e.Row.DataItem, "RubbleDollar").ToString
            End If

            If Session(TextBoxEuro.ID.ToString() + LabelPayReqNo.Text) IsNot Nothing Then
                TextBoxEuro.Text = Session(TextBoxEuro.ID.ToString() + LabelPayReqNo.Text)
            Else
                TextBoxEuro.Text = DataBinder.Eval(e.Row.DataItem, "RubbleEuro").ToString
            End If

            If TextBoxDollar.Text <> DataBinder.Eval(e.Row.DataItem, "RubbleDollar").ToString Then
                TextBoxDollar.BackColor = System.Drawing.Color.OrangeRed
            End If

            If TextBoxEuro.Text <> DataBinder.Eval(e.Row.DataItem, "RubbleEuro").ToString Then
                TextBoxEuro.BackColor = System.Drawing.Color.OrangeRed
            End If

            ' End of Assign Exchange rates to TextBoxes

            ' Calculate RubleWithVATToPay
            e.Row.Cells(8).Text = String.Format("{0:N2}", PTSMainClass.GetRubWithVATToPay(DataBinder.Eval(e.Row.DataItem, "PayReqNo"), TextBoxDollar.Text, TextBoxEuro.Text))

            Dim TextBoxPaidValue As TextBox = DirectCast(e.Row.FindControl("TextBoxPaidValue"), TextBox)
            Dim LabelWarningPaidValue As Label = DirectCast(e.Row.FindControl("LabelWarningPaidValue"), Label)
            Dim TextBoxFinanceNo As TextBox = DirectCast(e.Row.FindControl("TextBoxFinanceNo"), TextBox)

            ' it transfers payment value into TextBox control from Session variable
            If Session(e.Row.Cells(0).Text.ToString) IsNot Nothing Then
                TextBoxPaidValue.Text = Session(e.Row.Cells(0).Text.ToString)
            End If

            ' it transfers finance no into TextBox control from Session variable
            If Session(e.Row.Cells(0).Text.ToString + "FinanceNo") IsNot Nothing Then
                TextBoxFinanceNo.Text = Session(e.Row.Cells(0).Text.ToString + "FinanceNo")
            End If

            If Len(TextBoxPaidValue.Text) > 0 Then
                If Math.Abs(Convert.ToDouble(TextBoxPaidValue.Text) - Convert.ToDouble(e.Row.Cells(8).Text.Replace(",", ""))) / Convert.ToDouble(e.Row.Cells(8).Text.Replace(",", "")) * 100 >= 2 Then
                    LabelWarningPaidValue.Text = "difference more than 2%. This payment cannot be done. Invoice value should be changed accordingly. Please talk to PTS user on site to change invoice value."
                    _myCommanTask.SendEmailToAdmin("exceeding payment warning", produce_emailBody(DataBinder.Eval(e.Row.DataItem, "PayReqNo"), PaymentDateTextBoxShown))
                    '_myCommanTask.SendEmailToAdmin("exceeding payment warning", DataBinder.Eval(e.Row.DataItem, "PayReqNo"))
                    LabelWarningPaidValue.ForeColor = System.Drawing.Color.Red
                    LabelWarningPaidValue.BackColor = System.Drawing.Color.Yellow
                End If

                If Len(TextBoxFinanceNo.Text) = 0 Then
                    TextBoxFinanceNo.BackColor = System.Drawing.Color.Red
                    TextBoxFinanceNo.ForeColor = System.Drawing.Color.Yellow
                    TextBoxFinanceNo.Font.Bold = True
                    TextBoxFinanceNoValidation.Text = Convert.ToString((Convert.ToInt32(TextBoxFinanceNoValidation.Text) + 1))
                End If
            End If

            If Len(TextBoxPaidValue.Text) > 0 Then
                If Math.Abs(Convert.ToDouble(TextBoxPaidValue.Text) - Convert.ToDouble(e.Row.Cells(8).Text.Replace(",", ""))) / Convert.ToDouble(e.Row.Cells(8).Text.Replace(",", "")) * 100 >= 2 Then
                    ' more than 2 %, highlight the row as RED
                    e.Row.BackColor = System.Drawing.Color.Red
                End If
            End If

            If Len(TextBoxPaidValue.Text) > 0 AndAlso Len(TextBoxFinanceNo.Text) > 0 Then
                ' less than 2 %, valid row, highlight the row as GREEN
                If Not (Math.Abs(Convert.ToDouble(TextBoxPaidValue.Text) - Convert.ToDouble(e.Row.Cells(8).Text.Replace(",", ""))) / Convert.ToDouble(e.Row.Cells(8).Text.Replace(",", "")) * 100 >= 2) Then
                    ' more than 2 %, highlight the row as RED
                    e.Row.BackColor = System.Drawing.Color.LightGreen
                End If
            End If

            If Session(e.Row.Cells(0).Text + "Dublicating") = "Dublicating" Then

                e.Row.BackColor = System.Drawing.Color.Red

                Dim LabelWarningFinanceNo As Label = DirectCast(e.Row.FindControl("LabelWarningFinanceNo"), Label)
                LabelWarningFinanceNo.Text = "Finance No is dublicating."
                LabelWarningFinanceNo.ForeColor = System.Drawing.Color.Red
                LabelWarningFinanceNo.BackColor = System.Drawing.Color.Yellow

            End If

        End If
    End Sub


    Public Shared Function produce_emailBody(ByVal _payreqNo As Integer, ByVal _PaymentDateTextBoxShown As TextBox) As String

        ' read the row
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT * FROM View_WhatToBePaidRevised WHERE PayReqNo = @PayReqNo AND Date = @Date "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim PayReqNo As SqlParameter = cmd.Parameters.Add("@PayReqNo", System.Data.SqlDbType.Int)
            PayReqNo.Value = _payreqNo
            Dim _Date As SqlParameter = cmd.Parameters.Add("@Date", System.Data.SqlDbType.Date)
            _Date.Value = Mid(_PaymentDateTextBoxShown.Text.ToString, 1, 2) + "/" + Mid(_PaymentDateTextBoxShown.Text.ToString, 4, 2) + "/" + Mid(_PaymentDateTextBoxShown.Text.ToString, 7, 4)

            Dim dr As SqlDataReader = cmd.ExecuteReader

            ' create dataTable
            Dim dt As New DataTable()
            dt.Load(dr)

            ' convert dataTable to Row
            Dim sb As New StringBuilder()
            sb.Append("<html><body><table><thead><tr>")
            For Each c As DataColumn In dt.Columns
                sb.AppendFormat("<th>{0}</th>", c.ColumnName)
            Next
            sb.AppendLine("</tr></thead><tbody>")
            For Each _row As DataRow In dt.Rows
                sb.Append("<tr>")
                For Each o As Object In _row.ItemArray
                    sb.AppendFormat("<td>{0}</td>", o.ToString())
                Next
                sb.AppendLine("</tr>")
            Next
            sb.AppendLine("</tbody></table></body></html>")
            Return sb.ToString()

            ' close connection
            con.Close()
            dr.Close()

        End Using

    End Function

    Protected Function FinanceNoExist(ByVal _FinanceNo As String, ByVal _PaymentDate As Date) As Integer

        Using conPay As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            conPay.Open()
            Dim sqlstringPay As String = " SELECT     COUNT(FinanceNo) AS CountOfYear " +
                                        " FROM         dbo.Table5_PayLog " +
                                        " WHERE     (FinanceNo = @FinanceNo) AND (YEAR(PaymentDate) = YEAR(@PaymentDate)) "

            Dim cmdPay As New SqlCommand(sqlstringPay, conPay)
            cmdPay.CommandType = System.Data.CommandType.Text

            Dim FinanceNo As SqlParameter = cmdPay.Parameters.Add("@FinanceNo", System.Data.SqlDbType.NVarChar, 10)
            FinanceNo.Value = _FinanceNo

            Dim PaymentDate As SqlParameter = cmdPay.Parameters.Add("@PaymentDate", System.Data.SqlDbType.Date)
            PaymentDate.Value = _PaymentDate

            Dim drPay As SqlDataReader = cmdPay.ExecuteReader

            While drPay.Read
                Return drPay(0)
            End While

            conPay.Close()
            drPay.Close()
            conPay.Dispose()

        End Using

    End Function

    Protected Sub ButtonPayAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPayAll.Click
        ' PAY ALL
        ' If there is any missing Finance No, then Button Click will stop by validation control.
        ' Start loop from the first datarow. If there is value for payment figure, then PAY this item
        ' _ regardless it violates 2% rule or not. Because user has been informed before.

        For Each row As GridViewRow In GridViewItemsToPay.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim TextBoxPaidValue As TextBox = DirectCast(row.FindControl("TextBoxPaidValue"), TextBox)
                Dim TextBoxFinanceNo As TextBox = DirectCast(row.FindControl("TextBoxFinanceNo"), TextBox)
                Dim TextBoxDollar As TextBox = DirectCast(row.FindControl("TextBoxDollar"), TextBox)
                Dim TextBoxEuro As TextBox = DirectCast(row.FindControl("TextBoxEuro"), TextBox)
                Dim PersonPaid As String = ""

                If row.Cells(13).Text <> "PTS" Then
                    PersonPaid = row.Cells(13).Text
                Else
                    PersonPaid = "PTS"
                End If

                If Len(TextBoxPaidValue.Text) > 0 Then
                    If Not (Math.Abs(Convert.ToDouble(TextBoxPaidValue.Text) - Convert.ToDouble(row.Cells(8).Text.Replace(",", ""))) / Convert.ToDouble(row.Cells(8).Text.Replace(",", "")) * 100 >= 2) Then
                        ' there is paymentValue and it is valid, so go

                        ' Approve this item automatically on PaymentRequest page as "PTS"
                        Dim zoneId As String = "Russian Standard Time"
                        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
                        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)
                        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                            con.Open()
                            Dim sqlstring As String = " UPDATE [Table4_PaymentRequest] " +
                                                      "    SET [Approved] = N'Approved' " +
                                                      "       ,[PersonApprove] = '" + PersonPaid.Replace("&nbsp;", String.Empty).Replace("&amp;", String.Empty) + "' " +
                                                      "       ,[LastAction] = " + "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'" + " " +
                                                      " WHERE PayReqNo = " + row.Cells(0).Text + " "
                            Dim cmd As New SqlCommand(sqlstring, con)
                            cmd.CommandType = System.Data.CommandType.Text
                            Dim dr As SqlDataReader = cmd.ExecuteReader
                            con.Close()
                            dr.Close()
                            con.Dispose()

                        End Using

                        ' Enter figures into PayLog table based on PayReqNo
                        Dim zoneIdPay As String = "Russian Standard Time"
                        Dim tziPay As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneIdPay)
                        Dim resultPay As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tziPay)
                        Using conPay As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                            conPay.Open()
                            Dim sqlstringPay As String = " INSERT INTO [Table5_PayLog] " +
                                                        " ([PayReqNo] " +
                                                        " ,[FinanceNo] " +
                                                        " ,[PaymentDate] " +
                                                        " ,[Amount] " +
                                                        " ,[Currency] " +
                                                        " ,[CreatedBy] " +
                                                        " ,[PersonCreated] " +
                                                        " ,[RubbleDollar] " +
                                                        " ,[RubbleEuro]) " +
                                                        " VALUES " +
                                                        " (" + row.Cells(0).Text + " " +
                                                        " ,'" + TextBoxFinanceNo.Text + "' " +
                                                        " ,'" + Mid(PaymentDateTextBoxShown.Text, 7, 4) + "-" + Mid(PaymentDateTextBoxShown.Text, 4, 2) + "-" + Mid(PaymentDateTextBoxShown.Text, 1, 2) + "' " +
                                                        " ," + TextBoxPaidValue.Text + " " +
                                                        " ,N'Rub' " +
                                                        " ," + "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'" + " " +
                                                        " , N'PTS' " +
                                                        " , " + TextBoxDollar.Text + " " +
                                                        " , " + TextBoxEuro.Text + ") "
                            Dim cmdPay As New SqlCommand(sqlstringPay, conPay)
                            cmdPay.CommandType = System.Data.CommandType.Text

                            If TextBoxFinanceNo.Text <> "PTS" And FinanceNoExist(TextBoxFinanceNo.Text.Trim, Mid(PaymentDateTextBoxShown.Text, 7, 4) + "-" + Mid(PaymentDateTextBoxShown.Text, 4, 2) + "-" + Mid(PaymentDateTextBoxShown.Text, 1, 2)) > 0 Then
                                ' do nothing, pick reference number to be used on rowDataBound for highlighting Row
                                Session(row.Cells(0).Text + "Dublicating") = "Dublicating"
                            Else
                                Session.Remove((row.Cells(0).Text + "Dublicating"))
                                Dim drPay As SqlDataReader
                                drPay = cmdPay.ExecuteReader
                                conPay.Close()
                                drPay.Close()
                                conPay.Dispose()
                            End If

                        End Using

                    End If
                End If
            End If
        Next

	showPaidItems()

    End Sub

    Protected Sub TextBoxPaidValue_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ' It will assign revised value into Session
        Dim TextBoxPaidValue As TextBox = DirectCast(sender, TextBox)
        Dim Row As GridViewRow = DirectCast(TextBoxPaidValue.NamingContainer, GridViewRow)
        Session(Row.Cells(0).Text.ToString) = TextBoxPaidValue.Text
    End Sub

    Protected Sub TextBoxFinanceNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ' It will assign revised value into Session
        Dim TextBoxFinanceNo As TextBox = DirectCast(sender, TextBox)
        Dim Row As GridViewRow = DirectCast(TextBoxFinanceNo.NamingContainer, GridViewRow)
        Session(Row.Cells(0).Text.ToString + "FinanceNo") = TextBoxFinanceNo.Text
    End Sub

    Protected Sub ButtonExchangeRate_Click(sender As Object, e As EventArgs) Handles ButtonExchangeRate.Click

        Dim _myCommonTask As New MyCommonTasks
        _myCommonTask.UpdateExchangeRateRevision()
        _myCommonTask = Nothing

    End Sub

    Protected Sub TextBoxDollar_TextChanged(sender As Object, e As EventArgs)

        Dim _textbox As TextBox = sender

        Dim gvr As GridViewRow = _textbox.Parent.Parent
        Dim LabelPayReqNo As Label = DirectCast(gvr.FindControl("LabelPayReqNo"), Label)

        Session(_textbox.ID.ToString() + LabelPayReqNo.Text) = sender.text

    End Sub

    Protected Sub TextBoxEuro_TextChanged(sender As Object, e As EventArgs)

        Dim _textbox As TextBox = sender

        Dim gvr As GridViewRow = _textbox.Parent.Parent
        Dim LabelPayReqNo As Label = DirectCast(gvr.FindControl("LabelPayReqNo"), Label)

        Session(_textbox.ID.ToString() + LabelPayReqNo.Text) = sender.text

    End Sub
End Class
