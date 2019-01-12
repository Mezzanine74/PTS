Imports System
Imports System.IO
Imports System.Net
Imports System.Data.SqlClient

Partial Class editinvoice
    Inherits System.Web.UI.Page

    Dim Notification As New _GiveNotification

    Public Function GetDropDownListInvoiceType() As IQueryable(Of PTS_MERCURY.db.Table3_Invoice_Type)
        Return PTS_MERCURY.helper.Table3_Invoice_Type.GetDropDownListInvoice_Type()
    End Function

    Protected Sub GridViewEditInvoice_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewEditInvoice.PreRender

        ' it binds datasource to Gridview based on supplier DDL value
        If Not IsPostBack Then
            SqlDataSourceEditInvoice.SelectCommand = " SELECT     dbo.Table3_Invoice.InvoiceID, RTRIM(dbo.Table2_PONo.PO_No) AS PO_No, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR " + _
      "   dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description) + ' / ' + RTRIM(dbo.Table3_Invoice.Notes) " + _
      "    END AS Description, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName , RTRIM(dbo.Table3_Invoice.Invoice_No) AS Invoice_No, dbo.Table3_Invoice.Invoice_Date, dbo.Table3_Invoice.InvoiceValue,  " + _
      "   RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency, RTRIM(dbo.Table3_Invoice.Notes) AS Notes,   " + _
      "   dbo.Table5_PayLog.PayReqNo AS Paid " + _
      " FROM         dbo.Table5_PayLog RIGHT OUTER JOIN " + _
      "   dbo.Table4_PaymentRequest ON dbo.Table5_PayLog.PayReqNo = dbo.Table4_PaymentRequest.PayReqNo RIGHT OUTER JOIN " + _
      "   dbo.Table1_Project INNER JOIN " + _
      "   dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
      "   dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " + _
      "   dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID ON dbo.Table4_PaymentRequest.InvoiceID = dbo.Table3_Invoice.InvoiceID " + _
                  " WHERE     (dbo.Table1_Project.ProjectID =  " + "'" + DropDownListPrj.SelectedValue.ToString + "'" + " ) AND (dbo.Table6_Supplier.SupplierID =  " + "'" + DropDownListSupplier.SelectedValue.ToString + "'" + "   )"
        ElseIf DropDownListPrj.SelectedValue.ToString <> "0" AndAlso DropDownListSupplier.SelectedValue.ToString = "" Then
            SqlDataSourceEditInvoice.SelectCommand = " SELECT     dbo.Table3_Invoice.InvoiceID, RTRIM(dbo.Table2_PONo.PO_No) AS PO_No, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR " + _
      "   dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description) + ' / ' + RTRIM(dbo.Table3_Invoice.Notes) " + _
      "    END AS Description, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName , RTRIM(dbo.Table3_Invoice.Invoice_No) AS Invoice_No, dbo.Table3_Invoice.Invoice_Date, dbo.Table3_Invoice.InvoiceValue,  " + _
      "   RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency, RTRIM(dbo.Table3_Invoice.Notes) AS Notes,   " + _
      "   dbo.Table5_PayLog.PayReqNo AS Paid " + _
      " FROM         dbo.Table5_PayLog RIGHT OUTER JOIN " + _
      "   dbo.Table4_PaymentRequest ON dbo.Table5_PayLog.PayReqNo = dbo.Table4_PaymentRequest.PayReqNo RIGHT OUTER JOIN " + _
      "   dbo.Table1_Project INNER JOIN " + _
      "   dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
      "   dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " + _
      "   dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID ON dbo.Table4_PaymentRequest.InvoiceID = dbo.Table3_Invoice.InvoiceID " + _
                  " WHERE     (dbo.Table1_Project.ProjectID =  " + "'" + DropDownListPrj.SelectedValue.ToString + "'" + "  )"
        ElseIf DropDownListPrj.SelectedValue.ToString <> "0" AndAlso DropDownListSupplier.SelectedValue.ToString <> "0" Then
            SqlDataSourceEditInvoice.SelectCommand = " SELECT     dbo.Table3_Invoice.InvoiceID, RTRIM(dbo.Table2_PONo.PO_No) AS PO_No, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR " + _
      "   dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description) + ' / ' + RTRIM(dbo.Table3_Invoice.Notes) " + _
      "    END AS Description, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName , RTRIM(dbo.Table3_Invoice.Invoice_No) AS Invoice_No, dbo.Table3_Invoice.Invoice_Date, dbo.Table3_Invoice.InvoiceValue,  " + _
      "   RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency, RTRIM(dbo.Table3_Invoice.Notes) AS Notes,   " + _
      "   dbo.Table5_PayLog.PayReqNo AS Paid " + _
      " FROM         dbo.Table5_PayLog RIGHT OUTER JOIN " + _
      "   dbo.Table4_PaymentRequest ON dbo.Table5_PayLog.PayReqNo = dbo.Table4_PaymentRequest.PayReqNo RIGHT OUTER JOIN " + _
      "   dbo.Table1_Project INNER JOIN " + _
      "   dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
      "   dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " + _
      "   dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID ON dbo.Table4_PaymentRequest.InvoiceID = dbo.Table3_Invoice.InvoiceID " + _
                  " WHERE     (dbo.Table1_Project.ProjectID =  " + "'" + DropDownListPrj.SelectedValue.ToString + "'" + " ) AND (dbo.Table6_Supplier.SupplierID =  " + "'" + DropDownListSupplier.SelectedValue.ToString + "'" + "   )"
        End If

    End Sub

    Protected Sub GridViewEditInvoice_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridViewEditInvoice.RowCancelingEdit
        ' Clear the error message.
        Message.Text = ""
    End Sub

    Protected Sub GridViewEditInvoice_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewEditInvoice.RowDataBound

        Dim MyTask As New MyCommonTasks
        MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        ' it will create a condition to enable/disable buttons depends on the user type
        If DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton) IsNot Nothing Then
            SqlDataSourceFinanceCheck.SelectCommand = " SELECT     RTRIM(Table7_CostCode.Type) AS Type, Table3_Invoice.InvoiceID " + _
      " FROM         Table7_CostCode INNER JOIN " + _
      "                       Table2_PONo ON Table7_CostCode.CostCode = Table2_PONo.CostCode INNER JOIN " + _
      "                       Table3_Invoice ON Table2_PONo.PO_No = Table3_Invoice.PO_No " + _
      " WHERE     (RTRIM(Table7_CostCode.Type) = 'Finance') AND (Table3_Invoice.InvoiceID = " + GridViewEditInvoice.DataKeys(e.Row.RowIndex).Values("InvoiceID").ToString() + ") "
            DropDownListFinanceCheck.DataBind()
            If DropDownListFinanceCheck.Items.Count > 0 Then
                If Roles.IsUserInRole("Finance") Then
                    DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton).Enabled = True
                    DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton).Enabled = True
                Else
                    DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton).Enabled = False
                    DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton).Enabled = False
                End If
            End If
        End If


        If (e.Row.RowState = (DataControlRowState.Edit Or DataControlRowState.Alternate)) OrElse (e.Row.RowState = DataControlRowState.Edit) Then
            ' it transfers POno of selected row into textbox and add parameters
            Dim POno As String = DataBinder.Eval(e.Row.DataItem, "PO_No").ToString()

            TextBoxPOFromEditGridRow.Text = POno
            Dim TextBxInvValue As TextBox = DirectCast(e.Row.FindControl("TextBoxInvoiceValue"), TextBox)
            TextBoxInvoiceValueBeforeEdit.Text = TextBxInvValue.Text
            SqlDataSourceTotalPOvalue.SelectParameters("PO_No").DefaultValue = TextBoxPOFromEditGridRow.Text
            SqlDataSourceTotalPOvalue.SelectParameters("PO_No").Type = TypeCode.String
            SqlDataSourceTotalInvoiced.SelectParameters("PO_No").DefaultValue = TextBoxPOFromEditGridRow.Text
            SqlDataSourceTotalInvoiced.SelectParameters("PO_No").Type = TypeCode.String

        End If

        'If e.Row.RowType = DataControlRowType.DataRow Then
        ' if it is approved, do not alloww edit.
        'If DirectCast(e.Row.FindControl("LabelApproved"), Label) IsNot Nothing Then
        'Dim TextBoxInvoiceNo As TextBox = DirectCast(e.Row.FindControl("TextBoxInvoiceNo"), TextBox)
        'Dim TextBoxInvoiceDateShown As TextBox = DirectCast(e.Row.FindControl("TextBoxInvoiceDateShown"), TextBox)
        'Dim TextBoxInvoiceValue As TextBox = DirectCast(e.Row.FindControl("TextBoxInvoiceValue"), TextBox)
        'Dim TextBoxNoteEdit As TextBox = DirectCast(e.Row.FindControl("TextBoxNoteEdit"), TextBox)


        'If DirectCast(e.Row.FindControl("LabelApproved"), Label).Text = "Approved" Then
        'TextBoxInvoiceNo.Enabled = False
        'TextBoxInvoiceDateShown.Enabled = False
        'TextBoxInvoiceValue.Enabled = False
        'TextBoxNoteEdit.Enabled = False
        'Else
        'TextBoxInvoiceNo.Enabled = True
        'TextBoxInvoiceDateShown.Enabled = True
        'TextBoxInvoiceValue.Enabled = True
        'TextBoxNoteEdit.Enabled = True
        'End If
        'End If
        'End If

        ' if it is paid, then freeze InvoiceValue textbox
        If (e.Row.RowState = (DataControlRowState.Edit Or DataControlRowState.Alternate)) OrElse (e.Row.RowState = DataControlRowState.Edit) Then
            If User.IsInRole("Finance") Then
                ' do nothing
            Else
                If Len(DataBinder.Eval(e.Row.DataItem, "Paid").ToString()) > 0 Then
                    DirectCast(e.Row.FindControl("TextBoxInvoiceValue"), TextBox).Text = "Paid, frozen"
                    DirectCast(e.Row.FindControl("TextBoxInvoiceValue"), TextBox).Enabled = False
                    DirectCast(e.Row.FindControl("TextBoxInvoiceValue"), TextBox).Enabled = False
                Else
                    DirectCast(e.Row.FindControl("TextBoxInvoiceValue"), TextBox).Enabled = True
                End If
            End If
        End If

        ' It fixes column width problem
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Label1 As Label = DirectCast(e.Row.FindControl("Label6"), Label)

            If Label1 IsNot Nothing Then
                Label1.Text = Label1.Text.Replace(",", "," + " ")
            End If
        End If

        ' It fixes column width problem
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Label111 As Label = DirectCast(e.Row.FindControl("Label1"), Label)

            If Label111 IsNot Nothing Then
                Label111.Text = Label111.Text.Replace(",", "," + " ")
            End If
        End If


        'If e.Row.RowType = DataControlRowType.Header Then
        '    GridViewEditInvoice.HeaderRow.TableSection = TableRowSection.TableHeader
        'End If

        'e.Row.Cells(0).Text = e.Row.RowIndex.ToString

        Dim _invoiceid As Integer = DataBinder.Eval(e.Row.DataItem, "InvoiceID")
        Dim LiteralInvoiceType As Literal = TryCast(e.Row.FindControl("LiteralInvoiceType"), Literal)
        If LiteralInvoiceType IsNot Nothing Then

            If Not PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table3_Invoice_Type.GetTypeFromInvoiceId(_invoiceid) Is Nothing Then
                LiteralInvoiceType.Text = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table3_Invoice_Type.GetTypeFromInvoiceId(_invoiceid).Type_name
            Else
                Dim _pono As String = DataBinder.Eval(e.Row.DataItem, "PO_No").ToString()
                If PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table2_PONo.IfPoCorrespondsToSubcontractorContractOrAddendum(_pono) Then
                    LiteralInvoiceType.Text = "<span style=" + """" + "color:Red;" + """" + "><b>Required!</b></span>"
                Else
                    LiteralInvoiceType.Text = "-"
                End If

            End If

        End If

        Dim DropDownListInvoiceType As DropDownList = TryCast(e.Row.FindControl("DropDownListInvoiceType"), DropDownList)
        If DropDownListInvoiceType IsNot Nothing Then
            If Not PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table3_Invoice_Type.GetTypeFromInvoiceId(_invoiceid) Is Nothing Then
                DropDownListInvoiceType.SelectedValue = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table3_Invoice_Type.GetTypeFromInvoiceId(_invoiceid).Type_id
            End If

            ' validation
            Dim RequiredFieldValidatorInvoiceType As RequiredFieldValidator = TryCast(e.Row.FindControl("RequiredFieldValidatorInvoiceType"), RequiredFieldValidator)
            Dim _pono As String = DataBinder.Eval(e.Row.DataItem, "PO_No").ToString()

            If PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table2_PONo.IfPoCorrespondsToSubcontractorContractOrAddendum(_pono) Then
                RequiredFieldValidatorInvoiceType.Enabled = True
            Else
                RequiredFieldValidatorInvoiceType.Enabled = False
            End If

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

    Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
        If Not IsPostBack Then
            Dim lst1 As New ListItem("Select Project", "0")
            Me.DropDownListPrj.Items.Insert(0, lst1)
        End If

        If Request.QueryString("ProjectID") IsNot Nothing And Not IsPostBack Then
            DropDownListPrj.SelectedValue = Request.QueryString("ProjectID")
        End If

    End Sub

    Protected Sub GridViewEditInvoice_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewEditInvoice.PageIndexChanging
        ' Cancel the paging operation if the user attempts to navigate
        ' to another page while the GridView control is in edit mode. 
        If GridViewEditInvoice.EditIndex <> -1 Then
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

    Protected Sub GridViewEditInvoice_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles GridViewEditInvoice.RowUpdated
        ' Clear the error message.
        Message.Text = ""
        Notification._GiveNotification _
          (Page.Request.Headers("X-Requested-With"), Page, _
           "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Updated!</p>")

    End Sub

    Protected Sub GridViewEditInvoice_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewEditInvoice.RowUpdating
        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewEditInvoice.Rows(index)
        Dim TextBoxRevisedInvoice As TextBox = DirectCast(row.FindControl("TextBoxInvoiceValue"), TextBox)
        Dim Status As Decimal
        Status = Convert.ToDecimal(TextBoxRevisedInvoice.Text) - Convert.ToDecimal(e.OldValues("InvoiceValue").ToString) + Convert.ToDecimal(DropDownListTotalInvoiced.SelectedValue) - Convert.ToDecimal(DropDownListTotalPoValue.SelectedValue)

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim TextBoxInvoiceDateShown As TextBox = DirectCast(row.FindControl("TextBoxInvoiceDateShown"), TextBox)


        If Convert.ToDateTime(result) < Convert.ToDateTime(Mid(TextBoxInvoiceDateShown.Text.ToString, 1, 2).ToString + "/" + Mid(TextBoxInvoiceDateShown.Text.ToString, 4, 2).ToString + "/" + Mid(TextBoxInvoiceDateShown.Text.ToString, 7, 4).ToString) Then
            Message.Text = "Invoice date can not be later than today"
            e.Cancel = True
            Exit Sub
        ElseIf Status > 0 Then
            Message.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(TextBoxRevisedInvoice.Text)) + " is " + Status.ToString + " greater than total PO value " + String.Format("{0:#,##0.00}", Convert.ToDecimal(DropDownListTotalPoValue.SelectedValue)) + " excluding VAT"
            e.Cancel = True
            Exit Sub
        Else

            e.NewValues("Invoice_Date") = Convert.ToDateTime(Mid(TextBoxInvoiceDateShown.Text.ToString, 1, 2).ToString + "/" + Mid(TextBoxInvoiceDateShown.Text.ToString, 4, 2).ToString + "/" + Mid(TextBoxInvoiceDateShown.Text.ToString, 7, 4).ToString)

            ' update action details
            Dim InstanceOfUpdate As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'"
            Dim PersonUpdate As String = Page.User.Identity.Name.ToString
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = "update Table3_Invoice set UpdatedBy = " + InstanceOfUpdate + _
                                          ", PersonUpdated = '" + PersonUpdate + "'" + _
                                          " where InvoiceID = " + GridViewEditInvoice.DataKeys(e.RowIndex).Value.ToString()
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                Dim dr As SqlDataReader = cmd.ExecuteReader
                con.Close()
                dr.Close()
                con.Dispose()
            End Using

        End If

        Dim DropDownListInvoiceType As DropDownList = DirectCast(row.FindControl("DropDownListInvoiceType"), DropDownList)
        Dim HiddenInvoiceId As HiddenField = DirectCast(row.FindControl("HiddenInvoiceId"), HiddenField)

        If DropDownListInvoiceType.SelectedValue = "0" Then
            ' if exist, delete
            PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table3_Invoice_Type_Junction.Delete(HiddenInvoiceId.Value)
        Else
            ' if different, update
            PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table3_Invoice_Type_Junction.Update(HiddenInvoiceId.Value, DropDownListInvoiceType.SelectedValue)

        End If

    End Sub

    Protected Sub DropDownListSupplier_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListSupplier.DataBound
        Dim lst As New ListItem("ALL SUPPLIER", "")
        DropDownListSupplier.Items.Insert(0, lst)

        If Request.QueryString("SupplierID") IsNot Nothing And Not IsPostBack Then
            DropDownListSupplier.SelectedValue = Request.QueryString("SupplierID")
        End If

    End Sub

    Protected Sub DropDownListSupplier_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListSupplier.SelectedIndexChanged

    End Sub

    Protected Sub DropDownListPrj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.SelectedIndexChanged
        ' it rejects Marina from TNK-BP to Access Denied Page
        If Page.User.Identity.Name.ToString = "marina" OrElse Page.User.Identity.Name.ToString = "n.komleva" AndAlso DropDownListPrj.SelectedValue = 123 Then
            Response.Redirect("~/webforms/AccessDenied.aspx")
        End If

        Dim nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString())

        nameValues.Remove("SupplierID")

        nameValues.Set("ProjectID", DropDownListPrj.SelectedValue.ToString)
        Dim url As String = Request.Url.AbsolutePath
        Dim updatedQueryString As String = "?" + nameValues.ToString()
        Response.Redirect(url + updatedQueryString)

    End Sub

    Protected Sub GridViewEditInvoice_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewEditInvoice.RowDeleting
        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewEditInvoice.Rows(index)
        Dim invoiceID As String = GridViewEditInvoice.DataKeys(index).Values("InvoiceID").ToString

        If CanWeDeleteThisInvoice(invoiceID) Then
            Dim zoneId As String = "Russian Standard Time"
            Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
            Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

            Dim InstanceOfDeletion As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'"

            Using cn As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                Dim cmd As New System.Data.SqlClient.SqlCommand()
                cmd.Connection = cn

                cmd.CommandText = "UPDATE Table3_Invoice SET DeletedBy = " + InstanceOfDeletion + ", PersonDeleted = " + "'" + Page.User.Identity.Name.ToString + "'" + " WHERE InvoiceID = " + invoiceID.ToString()
                cmd.CommandType = System.Data.CommandType.Text
                cn.Open()
                cmd.ExecuteNonQuery()
                cn.Close()
                cn.Dispose()
            End Using
        Else
            ' Show modalpopup
            e.Cancel = True
            ModalPopupExtender1.Show()
        End If

    End Sub

    Protected Function CanWeDeleteThisInvoice(ByVal InvoiceID As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     COUNT(dbo.Table3_Invoice.InvoiceID) AS CountOfInvoiceID " +
        " FROM         dbo.Table3_Invoice LEFT OUTER JOIN " +
        "                       dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID " +
        " WHERE     (dbo.Table4_PaymentRequest.InvoiceID = " + InvoiceID + ") "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim CanWe As Boolean = False
            While dr.Read
                If dr(0) = 0 Then
                    CanWe = True
                End If
            End While
            con.Close()
            dr.Close()
            con.Dispose()
            Return CanWe

        End Using
    End Function

    Protected Sub GridViewEditInvoice_RowDeleted(sender As Object, e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridViewEditInvoice.RowDeleted
        Notification._GiveNotification _
      (Page.Request.Headers("X-Requested-With"), Page, _
       "<p style=" + """" + "text-align: center; font-weight: bold" + """" + ">Deleted Successfully!</p>")
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If GridViewEditInvoice.Rows.Count > 0 Then
            GridViewEditInvoice.HeaderRow.TableSection = TableRowSection.TableHeader
        End If

    End Sub

    Protected Sub DropDownListSupplier_SelectedIndexChanged1(sender As Object, e As EventArgs)

        Dim nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString())
        nameValues.Set("SupplierID", DropDownListSupplier.SelectedValue.ToString.Trim)

        If DropDownListSupplier.SelectedIndex = 0 Then
            nameValues.Remove("SupplierID")
        End If

        Dim url As String = Request.Url.AbsolutePath
        Dim updatedQueryString As String = "?" + nameValues.ToString()
        Response.Redirect(url + updatedQueryString)

    End Sub
End Class
