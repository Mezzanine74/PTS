Imports System.Data.SqlClient

Partial Class editpaylog
    Inherits System.Web.UI.Page

    Dim Notification As New _GiveNotification

    Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
        ' it provides Select Project Statement for DDL
        If Not IsPostBack Then
            Dim lst As New ListItem("Select Project", "0")
            DropDownListPrj.Items.Insert(0, lst)
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Page.User.Identity.Name.ToLower() = "julia" _
                OrElse Page.User.Identity.Name.ToLower() = "savas" _
                OrElse Page.User.Identity.Name.ToLower() = "mariya" _
                OrElse Page.User.Identity.Name.ToLower() = "katya" _
                OrElse Page.User.Identity.Name.ToLower() = "p.silantiev" _
                OrElse Page.User.Identity.Name.ToLower() = "dzera" _
                OrElse Page.User.Identity.Name.ToLower() = "inna" _
                OrElse Page.User.Identity.Name.ToLower() = "krystsina" _
                OrElse Page.User.Identity.Name.ToLower() = "natalia.surskaya" _
                OrElse Page.User.Identity.Name.ToLower() = "elmira.shabaeva" _
                OrElse Page.User.Identity.Name.ToLower() = "mariya.podobueva" _
                OrElse Page.User.Identity.Name.ToLower() = "natalia.larionova" Then
                ' do nothing 
            Else
                Response.Redirect("~/webforms/AccessDenied.aspx")
            End If
        End If





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

    Protected Sub DropDownListPrj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.SelectedIndexChanged
        ' it rejects Marina from TNK-BP to Access Denied Page
        If Page.User.Identity.Name.ToString = "marina" AndAlso DropDownListPrj.SelectedValue = 123 Then
            Response.Redirect("~/webforms/AccessDenied.aspx")
        End If
        ' it binds data for gridview
    End Sub

    Protected Sub GridViewEditPayLog_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewEditPayLog.PageIndexChanging
        ' Cancel the paging operation if the user attempts to navigate
        ' to another page while the GridView control is in edit mode. 
        If GridViewEditPayLog.EditIndex <> -1 Then
            ' Use the Cancel property to cancel the paging operation.
            e.Cancel = True
            ' Display an error message.
            Dim newPageNumber As Integer = e.NewPageIndex + 1
            Message.Text = "Please update the record before moving to page " & _
              newPageNumber.ToString() & "."
        Else
            ' Clear the error message.
            Message.Text = ""
        End If
    End Sub

    Protected Sub GridViewEditPayLog_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewEditPayLog.PreRender
        ' it binds datasource to Gridview based on supplier DDL value
        If Not IsPostBack Then
            SqlDataSourceEditPayLog.SelectCommand = "SELECT PO_No, RTRIM(SupplierName) AS SupplierName, SupplierID,  Description, RTRIM(Invoice_No) AS Invoice_No, Invoice_Date, RTRIM(FinanceNo) AS FinanceNo, PaymentDate, Amount, RTRIM(Currency) AS Currency, RTRIM(Note) AS Note, ProjectID, PayReqNo, RubbleDollar, RubbleEuro " + _
  " FROM View_editpaylog " + _
      " WHERE     (ProjectID =  " + "'" + DropDownListPrj.SelectedValue.ToString + "'" + " ) AND (SupplierID =  " + "'" + DropDownListSupplier.SelectedValue.ToString + "'" + "   )"
        ElseIf DropDownListPrj.SelectedValue.ToString <> "0" AndAlso DropDownListSupplier.SelectedValue.ToString = "" Then
            SqlDataSourceEditPayLog.SelectCommand = "SELECT PO_No, RTRIM(SupplierName) AS SupplierName, SupplierID,  Description, RTRIM(Invoice_No) AS Invoice_No, Invoice_Date, RTRIM(FinanceNo) AS FinanceNo, PaymentDate, Amount, RTRIM(Currency) AS Currency, RTRIM(Note) AS Note, ProjectID, PayReqNo, RubbleDollar, RubbleEuro " + _
  " FROM View_editpaylog " + _
      " WHERE     (ProjectID =  " + "'" + DropDownListPrj.SelectedValue.ToString + "'" + " ) "
        ElseIf DropDownListPrj.SelectedValue.ToString <> "0" AndAlso DropDownListSupplier.SelectedValue.ToString <> "0" Then
            SqlDataSourceEditPayLog.SelectCommand = "SELECT PO_No, RTRIM(SupplierName) AS SupplierName, SupplierID,  Description, RTRIM(Invoice_No) AS Invoice_No, Invoice_Date, RTRIM(FinanceNo) AS FinanceNo, PaymentDate, Amount, RTRIM(Currency) AS Currency, RTRIM(Note) AS Note, ProjectID, PayReqNo, RubbleDollar, RubbleEuro " + _
  " FROM View_editpaylog " + _
      " WHERE     (ProjectID =  " + "'" + DropDownListPrj.SelectedValue.ToString + "'" + " ) AND (SupplierID =  " + "'" + DropDownListSupplier.SelectedValue.ToString + "'" + "   )"
        End If

    End Sub

    Protected Sub GridViewEditPayLog_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridViewEditPayLog.RowCancelingEdit
        ' Clear the error message.
        Message.Text = ""

        Session.RemoveAll()

    End Sub

    Protected Sub GridViewEditPayLog_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewEditPayLog.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            ' assign PaymentDate if changed
            Dim LabelPayReqNo As Label = DirectCast(e.Row.FindControl("LabelPayReqNo"), Label)
            If LabelPayReqNo IsNot Nothing Then

                Dim TextBoxDollar As TextBox = DirectCast(e.Row.FindControl("TextBoxDollar"), TextBox)
                Dim TextBoxEuro As TextBox = DirectCast(e.Row.FindControl("TextBoxEuro"), TextBox)

                If Session("Date" + LabelPayReqNo.Text) IsNot Nothing Then
                    Dim TextBoxpaymentdateShown As TextBox = DirectCast(e.Row.FindControl("TextBoxpaymentdateShown"), TextBox)
                    TextBoxpaymentdateShown.Text = Session("Date" + LabelPayReqNo.Text)

                    Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                        con.Open()
                        Dim sqlstring As String = "SP_GetExcRateFromDate"
                        Dim cmd As New SqlCommand(sqlstring, con)
                        cmd.CommandType = System.Data.CommandType.StoredProcedure

                        'syntax for parameter adding
                        Dim _Date As SqlParameter = cmd.Parameters.Add("@Date", System.Data.SqlDbType.SmallDateTime)
                        _Date.Value = Mid(Session("Date" + LabelPayReqNo.Text), 7, 4) + "-" + Mid(Session("Date" + LabelPayReqNo.Text), 4, 2) + "-" + Mid(Session("Date" + LabelPayReqNo.Text), 1, 2)
                        Dim dr As SqlDataReader = cmd.ExecuteReader

                        While dr.Read
                            TextBoxDollar.Text = dr(0)
                            TextBoxEuro.Text = dr(1)
                        End While

                        con.Close()
                        dr.Close()
                    End Using

                End If

                If Session("Dollar" + LabelPayReqNo.Text) IsNot Nothing Then

                    TextBoxDollar.Text = Session("Dollar" + LabelPayReqNo.Text)

                End If

                If Session("Euro" + LabelPayReqNo.Text) IsNot Nothing Then

                    TextBoxEuro.Text = Session("Euro" + LabelPayReqNo.Text)

                End If

                ' Provide validation 
                Dim CompareValidatorPaymentMax As CompareValidator = DirectCast(e.Row.FindControl("CompareValidatorPaymentMax"), CompareValidator)
                Dim CompareValidatorPaymentMin As CompareValidator = DirectCast(e.Row.FindControl("CompareValidatorPaymentMin"), CompareValidator)

                If CompareValidatorPaymentMax IsNot Nothing Then
                    Dim ValueToPayRubleWithVAT As Decimal = PTSMainClass.GetRubWithVATToPay(DataBinder.Eval(e.Row.DataItem, "PayReqNo"), TextBoxDollar.Text, TextBoxEuro.Text)
                    Dim max As Decimal = Math.Round(ValueToPayRubleWithVAT * 1.02, 2)
                    Dim min As Decimal = Math.Round(ValueToPayRubleWithVAT / 1.02, 2)

                    If CompareValidatorPaymentMax IsNot Nothing Then
                        CompareValidatorPaymentMax.ValueToCompare = max
                        CompareValidatorPaymentMin.ValueToCompare = min

                        CompareValidatorPaymentMax.ErrorMessage = "Value cannot be greater than " + max.ToString
                        CompareValidatorPaymentMin.ErrorMessage = "Value cannot be smaller than " + min.ToString

                    End If

                End If

            End If

        End If

        Dim MyTask As New MyCommonTasks
        MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        ' It fixes column width problem
        Dim Label1 As Label = DirectCast(e.Row.FindControl("Labeldescription1"), Label)

        If Label1 IsNot Nothing Then
            Label1.Text = Label1.Text.Replace(",", "," + " ")
        End If

        ' It fixes column width problem

        Dim Label111 As Label = DirectCast(e.Row.FindControl("Labeldescription"), Label)

        If Label111 IsNot Nothing Then
            Label111.Text = Label111.Text.Replace(",", "," + " ")
        End If

    End Sub

    Protected Sub GridViewEditPayLog_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewEditPayLog.RowDeleting
        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewEditPayLog.Rows(index)

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim InstanceOfDeletion As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'"

        Using cn As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            Dim cmd As New System.Data.SqlClient.SqlCommand()
            cmd.Connection = cn

            cmd.CommandText = "UPDATE Table5_PayLog SET DeletedBy = " + InstanceOfDeletion + ", PersonDeleted = " + "'" + Page.User.Identity.Name.ToString + "'" + " WHERE PayReqNo = " + DirectCast(row.FindControl("LabelPayReqNoInEdit"), Label).Text
            cmd.CommandType = System.Data.CommandType.Text
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()
            cn.Dispose()

        End Using

        '' Take PayReqNo Into Temporary_PayLog
        '    Using cn77 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        '        Dim cmd77 As New System.Data.SqlClient.SqlCommand()
        '        cmd77.Connection = cn77
        '        cmd77.CommandText = " INSERT INTO [Table5_PayLog_Temporary] " + _
        '                          "            ([PayReqNo]) " + _
        '                          "      VALUES " + _
        '                          "            (" + DirectCast(row.FindControl("LabelPayReqNoInEdit"), Label).Text + ") "
        '        cmd77.CommandType = System.Data.CommandType.Text
        '        cn77.Open()
        '        cmd77.ExecuteNonQuery()
        '        cn77.Close()
        '        cn77.Dispose()

        '    End Using

    End Sub

    Protected Sub GridViewEditPayLog_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles GridViewEditPayLog.RowUpdated
        ' Clear the error message.
        Message.Text = ""

        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Updated Successfully!</p>")

        Session.RemoveAll()

    End Sub

    Protected Sub GridViewEditPayLog_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridViewEditPayLog.RowDeleted

        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Deleted Successfully!</p>")

    End Sub

    Protected Sub GridViewEditPayLog_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewEditPayLog.RowUpdating

        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewEditPayLog.Rows(index)

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        e.NewValues("UpdatedBy") = result

        e.NewValues("PersonUpdated") = Page.User.Identity.Name.ToString

        Dim TextBoxpaymentdateShown As TextBox = DirectCast(row.FindControl("TextBoxpaymentdateShown"), TextBox)
        e.NewValues("PaymentDate") = Convert.ToDateTime(Mid(TextBoxpaymentdateShown.Text.ToString, 1, 2).ToString + "/" + Mid(TextBoxpaymentdateShown.Text.ToString, 4, 2).ToString + "/" + Mid(TextBoxpaymentdateShown.Text.ToString, 7, 4).ToString)

        If Convert.ToDateTime(result) < Convert.ToDateTime(Mid(TextBoxpaymentdateShown.Text.ToString, 1, 2).ToString + "/" + Mid(TextBoxpaymentdateShown.Text.ToString, 4, 2).ToString + "/" + Mid(TextBoxpaymentdateShown.Text.ToString, 7, 4).ToString) Then
            Message.Text = "Payment date can not be later than today"
            e.Cancel = True
        End If

        ' prevent dublication on Finance No
        Dim TextBoxfinNo As TextBox = DirectCast(row.FindControl("TextBoxfinNo"), TextBox)
        Dim LabelPayReqNo As Label = DirectCast(row.FindControl("LabelPayReqNo"), Label)
        Dim _payreqno As String = LabelPayReqNo.Text.Trim
        Dim _PaymentYear As String = Mid(TextBoxpaymentdateShown.Text.ToString, 7, 4).ToString
        Dim _financeNo As String = TextBoxfinNo.Text.Trim

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
            If (From C In db.Table5_PayLog Where C.PayReqNo = _payreqno).ToList()(0).FinanceNo.Trim <> _financeNo Then
                Dim CountOfFinance = Aggregate C In db.Table5_PayLog Where C.FinanceNo = _financeNo And C.PaymentDate.Year = _PaymentYear Into Count()
                If CountOfFinance > 0 Then
                    ' Finance No already exists, 
                    Message.Text = "Finance No already exists for this year"
                    TextBoxfinNo.BackColor = System.Drawing.Color.Red
                    e.Cancel = True
                End If
            End If
            db.Dispose()
        End Using

    End Sub

    Protected Sub DropDownListSupplier_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListSupplier.DataBound
        Dim lst As New ListItem("ALL SUPPLIER", "")
        DropDownListSupplier.Items.Insert(0, lst)
    End Sub

    Protected Sub DropDownListSupplier_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListSupplier.SelectedIndexChanged

    End Sub

    Protected Sub TextBoxpaymentdateShown_TextChanged(sender As Object, e As EventArgs)

        Dim _row As GridViewRow = sender.Parent.Parent
        Dim LabelPayReqNo As Label = DirectCast(_row.FindControl("LabelPayReqNo"), Label)

        Session("Date" + LabelPayReqNo.Text) = sender.text

    End Sub

    Protected Sub TextBoxDollar_TextChanged(sender As Object, e As EventArgs)

        Dim _row As GridViewRow = sender.Parent.Parent
        Dim LabelPayReqNo As Label = DirectCast(_row.FindControl("LabelPayReqNo"), Label)

        Session("Dollar" + LabelPayReqNo.Text) = sender.text

    End Sub

    Protected Sub TextBoxEuro_TextChanged(sender As Object, e As EventArgs)

        Dim _row As GridViewRow = sender.Parent.Parent
        Dim LabelPayReqNo As Label = DirectCast(_row.FindControl("LabelPayReqNo"), Label)

        Session("Euro" + LabelPayReqNo.Text) = sender.text

    End Sub

    Protected Sub GridViewEditPayLog_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridViewEditPayLog.RowEditing

        Session.RemoveAll()

    End Sub
End Class
