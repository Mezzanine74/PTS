' XXXXXX important note !!!
' XXXXXX if you place any control on EDIT mode which creates a postback, then consider your invoice update control accordingly.
Imports System
Imports System.IO
Imports System.Net
Imports System.Data.SqlClient
Partial Class editpaymentreqFASTERXXXrandomZZ
  Inherits System.Web.UI.Page

  Dim Notification As New _GiveNotification

  Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
    If Not IsPostBack Then
            Dim lst1 As New ListItem("Select Project", "0")
      Me.DropDownListPrj.Items.Insert(0, lst1)
    End If

        If Request.QueryString("ProjectID") IsNot Nothing And Not IsPostBack Then
            DropDownListPrj.SelectedValue = Request.QueryString("ProjectID")
        End If

  End Sub

  Protected Sub DropDownListPrj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.SelectedIndexChanged
    ' it rejects Marina from TNK-BP to Access Denied Page
    If Page.User.Identity.Name.ToString = "marina" OrElse Page.User.Identity.Name.ToString = "n.komleva" AndAlso DropDownListPrj.SelectedValue = 123 Then
            Response.Redirect("~/webforms/AccessDenied.aspx")
    End If

        'GridViewEditPaymentReq.DataBind()

        Dim nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString())

        nameValues.Remove("SupplierID")

        nameValues.Set("ProjectID", DropDownListPrj.SelectedValue.ToString)
        Dim url As String = Request.Url.AbsolutePath
        Dim updatedQueryString As String = "?" + nameValues.ToString()
        Response.Redirect(url + updatedQueryString)

  End Sub

  Protected Sub GridViewEditPaymentReq_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewEditPaymentReq.PageIndexChanging
    ' Cancel the paging operation if the user attempts to navigate
    ' to another page while the GridView control is in edit mode. 
    If GridViewEditPaymentReq.EditIndex <> -1 Then
      ' Use the Cancel property to cancel the paging operation.
      e.Cancel = True
      ' Display an error message.
      Dim newPageNumber As Integer = e.NewPageIndex + 1
      Message.Text = "Please update the record before moving to page " & _
        newPageNumber.ToString() & "."
    Else
      ' Clear the error message.
      Message.Text = ""
      LabelFileName.Text = ""
    End If
  End Sub

  Protected Sub GridViewEditPaymentReq_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewEditPaymentReq.PreRender
    If Not IsPostBack Then
            SqlDataSourceEditPaymentReq.SelectCommand = " SELECT     Table4_PaymentRequest.PayReqNo ,rtrim(dbo.Table2_PONo.PO_No) as PO_No, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR " + _
      "   dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description)  " + _
      "   + ' / ' + RTRIM(dbo.Table3_Invoice.Notes) END AS Description, rtrim(dbo.Table6_Supplier.SupplierName) as SupplierName, " + _
      "    rtrim(dbo.Table3_Invoice.Invoice_No) as Invoice_No, dbo.Table3_Invoice.InvoiceValue, " + _
      " rtrim(dbo.Table2_PONo.PO_Currency) as PO_Currency, rtrim(dbo.Table4_PaymentRequest.SiteRecordNo) as SiteRecordNo,  " + _
      "   dbo.Table4_PaymentRequest.PayReqDate, rtrim(dbo.Table4_PaymentRequest.Notes) as Notes, rtrim(dbo.Table4_PaymentRequest.LinkToInvoice) as LinkToInvoice,  " + _
      "   RTRIM(ActivityCode) AS ActivityCode, Table4_PaymentRequest.AttachmentExist, rtrim(Table4_PaymentRequest.ContractReference) AS ContractReference, dbo.Table3_Invoice.InvoiceID  " + _
      " FROM         dbo.Table1_Project INNER JOIN " + _
      "   dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
      "   dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " + _
      "   dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN " + _
      "   dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID " + _
                  " WHERE     (ProjectID =  " + DropDownListPrj.SelectedValue.ToString + " ) AND (dbo.Table6_Supplier.SupplierID =  " + "'" + DropDownListSupplier.SelectedValue.ToString + "'" + "   )"
    ElseIf DropDownListPrj.SelectedValue.ToString <> "0" AndAlso DropDownListSupplier.SelectedValue.ToString = "" Then
            SqlDataSourceEditPaymentReq.SelectCommand = " SELECT     Table4_PaymentRequest.PayReqNo ,rtrim(dbo.Table2_PONo.PO_No) as PO_No, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR " + _
      "   dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description)  " + _
      "   + ' / ' + RTRIM(dbo.Table3_Invoice.Notes) END AS Description, rtrim(dbo.Table6_Supplier.SupplierName) as SupplierName, " + _
      "    rtrim(dbo.Table3_Invoice.Invoice_No) as Invoice_No, dbo.Table3_Invoice.InvoiceValue, " + _
      " rtrim(dbo.Table2_PONo.PO_Currency) as PO_Currency, rtrim(dbo.Table4_PaymentRequest.SiteRecordNo) as SiteRecordNo,  " + _
      "   dbo.Table4_PaymentRequest.PayReqDate, rtrim(dbo.Table4_PaymentRequest.Notes) as Notes, rtrim(dbo.Table4_PaymentRequest.LinkToInvoice) as LinkToInvoice,  " + _
      "   RTRIM(ActivityCode) AS ActivityCode, Table4_PaymentRequest.AttachmentExist, rtrim(Table4_PaymentRequest.ContractReference) AS ContractReference, dbo.Table3_Invoice.InvoiceID  " + _
      " FROM         dbo.Table1_Project INNER JOIN " + _
      "   dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
      "   dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " + _
      "   dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN " + _
      "   dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID " + _
                  " WHERE     (ProjectID =  " + DropDownListPrj.SelectedValue.ToString + "  )"
    ElseIf DropDownListPrj.SelectedValue.ToString <> "0" AndAlso DropDownListSupplier.SelectedValue.ToString <> "0" Then
            SqlDataSourceEditPaymentReq.SelectCommand = " SELECT     Table4_PaymentRequest.PayReqNo ,rtrim(dbo.Table2_PONo.PO_No) as PO_No, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR " + _
      "   dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description)  " + _
      "   + ' / ' + RTRIM(dbo.Table3_Invoice.Notes) END AS Description, rtrim(dbo.Table6_Supplier.SupplierName) as SupplierName, " + _
      "    rtrim(dbo.Table3_Invoice.Invoice_No) as Invoice_No, dbo.Table3_Invoice.InvoiceValue, " + _
      " rtrim(dbo.Table2_PONo.PO_Currency) as PO_Currency, rtrim(dbo.Table4_PaymentRequest.SiteRecordNo) as SiteRecordNo,  " + _
      "   dbo.Table4_PaymentRequest.PayReqDate, rtrim(dbo.Table4_PaymentRequest.Notes) as Notes, rtrim(dbo.Table4_PaymentRequest.LinkToInvoice) as LinkToInvoice,  " + _
      "   RTRIM(ActivityCode) AS ActivityCode, Table4_PaymentRequest.AttachmentExist, rtrim(Table4_PaymentRequest.ContractReference) AS ContractReference, dbo.Table3_Invoice.InvoiceID  " + _
      " FROM         dbo.Table1_Project INNER JOIN " + _
      "   dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
      "   dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " + _
      "   dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN " + _
      "   dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID " + _
                  " WHERE     (ProjectID =  " + DropDownListPrj.SelectedValue.ToString + " ) AND (dbo.Table6_Supplier.SupplierID =  " + "'" + DropDownListSupplier.SelectedValue.ToString + "'" + "   )"
    End If

  End Sub

  Protected Sub GridViewEditPaymentReq_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridViewEditPaymentReq.RowCancelingEdit
    ' Clear the error message.
    Message.Text = ""
    LabelFileName.Text = ""
  End Sub

  Protected Sub GridViewEditPaymentReq_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewEditPaymentReq.RowCommand
    If (e.CommandName = "OpenPdfItem") Then
      Dim LinkToFile As String = e.CommandArgument.ToString
      Dim openpdf As New MyCommonTasks
      openpdf.OpenPDF(LinkToFile)
    End If

    If (e.CommandName = "OpenPdfEdit") Then
      Dim LinkToFile As String = e.CommandArgument.ToString
      Dim openpdf As New MyCommonTasks
      openpdf.OpenPDF(LinkToFile)
    End If

    If (e.CommandName = "UploadInvoice") Then
      Dim index As Integer = Convert.ToInt32(e.CommandArgument)
      Dim row As GridViewRow = GridViewEditPaymentReq.Rows(index)
      Dim FileToUpload As FileUpload = DirectCast(row.FindControl("FileUploadInvoice"), FileUpload)
      Dim ImageButtonEdit As ImageButton = DirectCast(row.FindControl("ImageButtonEdit"), ImageButton)
      Dim ButtonUploadInvoice As Button = DirectCast(row.FindControl("ButtonUploadInvoice"), Button)

            ' Take Project Name for Folder Name
            Dim LabelProjectName As String = ""
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " SELECT     RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, dbo.Table4_PaymentRequest.PayReqNo " + _
                        " FROM         dbo.Table1_Project INNER JOIN " + _
                        "                       dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
                        "                       dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " + _
                        "                       dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID " + _
                        " WHERE     (dbo.Table4_PaymentRequest.PayReqNo = " + GridViewEditPaymentReq.DataKeys(index).Value.ToString + ") "
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    LabelProjectName = dr(0).ToString
                End While
                con.Close()
                dr.Close()
                con.Dispose()

            End Using

            ' Take InvoiceID for using on file name
            Dim LabelInvoiceId As String = ""
            Using conInvoiceId As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                conInvoiceId.Open()
                Dim sqlstringInvoiceId As String = " SELECT InvoiceID FROM dbo.Table4_PaymentRequest " +
                                                  " WHERE (PayReqNo = " + GridViewEditPaymentReq.DataKeys(index).Value.ToString + ") "
                Dim cmdInvoiceId As New SqlCommand(sqlstringInvoiceId, conInvoiceId)
                cmdInvoiceId.CommandType = System.Data.CommandType.Text
                Dim drInvoiceId As SqlDataReader = cmdInvoiceId.ExecuteReader
                While drInvoiceId.Read
                    LabelInvoiceId = drInvoiceId(0).ToString
                End While
                conInvoiceId.Close()
                drInvoiceId.Close()
                conInvoiceId.Dispose()

            End Using

            If FileToUpload.HasFile Then
                If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) <> ".pdf" Then
                    LabelFileName.ForeColor = System.Drawing.Color.Red
                    LabelFileName.Text = "Please select PDF format file"
                Else
                    If FileToUpload.PostedFile.ContentLength / 1000 > 1200 Then
                        ModalPopupExtender1.Show()
                    Else
                        If Directory.Exists(Server.MapPath("~/REQUEST/") + LabelProjectName + "/") Then
                            Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            FileToUpload.SaveAs(MapPath("~/REQUEST/" + LabelProjectName + "/" + LabelInvoiceId + UniqueString1 + ".pdf"))
                            LabelLinkTransferFromCommandEvent.Text = "~/REQUEST/" + LabelProjectName + "/" + LabelInvoiceId + UniqueString1 + ".pdf"
                            LabelFileName.ForeColor = System.Drawing.Color.DarkGreen
                            LabelFileName.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                        Else
                            Dim UniqueString2 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Directory.CreateDirectory(Server.MapPath("~/REQUEST/") + LabelProjectName)
                            FileToUpload.SaveAs(MapPath("~/REQUEST/" + LabelProjectName + "/" + LabelInvoiceId + UniqueString2 + ".pdf"))
                            LabelLinkTransferFromCommandEvent.Text = "~/REQUEST/" + LabelProjectName + "/" + LabelInvoiceId + UniqueString2 + ".pdf"
                            LabelFileName.ForeColor = System.Drawing.Color.DarkGreen
                            LabelFileName.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                        End If
                    End If
                End If
            Else
                LabelFileName.ForeColor = System.Drawing.Color.Red
                LabelFileName.Text = "you did not specify any file"
            End If
        End If

    End Sub

    Protected Sub GridViewEditPaymentReq_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewEditPaymentReq.RowCreated

    End Sub

    Protected Sub GridViewEditPaymentReq_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewEditPaymentReq.RowDataBound

        Dim MyTask As New MyCommonTasks
        MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        ' show ContractReference Edit box accordingly
        'Dim LabelContractReference As Label = DirectCast(e.Row.FindControl("LabelContractReference"), Label)
        Dim LabelContractReferenceItem As Label = DirectCast(e.Row.FindControl("LabelContractReferenceItem"), Label)
        'Dim TextBoxContractReference As TextBox = DirectCast(e.Row.FindControl("TextBoxContractReference"), TextBox)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim HyperLinkSplit As HyperLink = TryCast(e.Row.FindControl("HyperLinkSplit"), HyperLink)
            If HyperLinkSplit IsNot Nothing Then

                Dim _paylogno As Integer = 0

                Try
                    _paylogno = PTS_MERCURY.helper.Table5_PayLog.GetRowByPrimaryKey(DataBinder.Eval(e.Row.DataItem, "PayReqNo")).PayReqNo
                Catch ex As Exception

                End Try

                If _paylogno = 0 Then
                    HyperLinkSplit.Visible = True
                Else
                    HyperLinkSplit.Visible = False
                End If


            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            'If DropDownListPrj.SelectedValue = 108 AndAlso TextBoxContractReference IsNot Nothing Then
            '          'TextBoxContractReference.Visible = True
            '  LabelContractReference.Visible = True
            'End If
            If DropDownListPrj.SelectedValue = 108 AndAlso LabelContractReferenceItem IsNot Nothing Then
                LabelContractReferenceItem.Visible = True
            End If
        End If

        ' it will decide to show ActivityCode DDL in EDIT MODE
        If DirectCast(e.Row.FindControl("LinkButtonUpdate"), LinkButton) IsNot Nothing Then

            ' it is edit mode, so transfer linkinvoice, then clear content
            DirectCast(e.Row.FindControl("LabelLinkToInvoiceToTransfer"), Label).Text = LabelLinkTransferFromCommandEvent.Text
            LabelLinkTransferFromCommandEvent.Text = ""

            'If DropDownListPrj.SelectedValue = 104 OrElse DropDownListPrj.SelectedValue = 34 Then
            '  DirectCast(e.Row.FindControl("DropDownListActivityCode"), DropDownList).Visible = True
            'Else
            '  DirectCast(e.Row.FindControl("DropDownListActivityCode"), DropDownList).Visible = False
            'End If
        End If

        ' it will create a condition to enable/disable buttons depends on the user type
        If DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton) IsNot Nothing Then
            SqlDataSourceFinanceCheck.SelectCommand = " SELECT     RTRIM(Table7_CostCode.Type) AS Type, Table4_PaymentRequest.PayReqNo " +
" FROM         Table7_CostCode INNER JOIN " +
"                       Table2_PONo ON Table7_CostCode.CostCode = Table2_PONo.CostCode INNER JOIN " +
"                       Table3_Invoice ON Table2_PONo.PO_No = Table3_Invoice.PO_No INNER JOIN " +
"                       Table4_PaymentRequest ON Table3_Invoice.InvoiceID = Table4_PaymentRequest.InvoiceID " +
" WHERE     (RTRIM(Table7_CostCode.Type) = 'Finance') AND (Table4_PaymentRequest.PayReqNo = " + GridViewEditPaymentReq.DataKeys(e.Row.RowIndex).Value.ToString() + ") "
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


        If e.Row.RowType = DataControlRowType.DataRow Then
            'it defines type of PDF image if it exist or not.
            If DirectCast(e.Row.FindControl("ImageButtonItem"), ImageButton) IsNot Nothing Then
                If DataBinder.Eval(e.Row.DataItem, "AttachmentExist") = True Then
                    DirectCast(e.Row.FindControl("ImageButtonItem"), ImageButton).ImageUrl = "~/Images/pdf.bmp"
                Else
                    DirectCast(e.Row.FindControl("ImageButtonItem"), ImageButton).ImageUrl = "~/Images/pdf_bw.bmp"
                End If
            End If

            If DirectCast(e.Row.FindControl("ImageButtonEdit"), ImageButton) IsNot Nothing Then
                If DataBinder.Eval(e.Row.DataItem, "AttachmentExist") = True Then
                    DirectCast(e.Row.FindControl("ImageButtonEdit"), ImageButton).ImageUrl = "~/Images/pdf.bmp"
                Else
                    DirectCast(e.Row.FindControl("ImageButtonEdit"), ImageButton).ImageUrl = "~/Images/pdf_bw.bmp"
                End If
            End If

            ' if it is approved, do not alloww edit.
            'If DirectCast(e.Row.FindControl("LabelApproved"), Label) IsNot Nothing Then
            'Dim FileToUpload As FileUpload = DirectCast(e.Row.FindControl("FileUploadInvoice"), FileUpload)
            'Dim ButtonUploadInvoice As Button = DirectCast(e.Row.FindControl("ButtonUploadInvoice"), Button)
            'Dim TextBoxSiteRecordEdit As TextBox = DirectCast(e.Row.FindControl("TextBoxSiteRecordEdit"), TextBox)
            'Dim TextBoxPayReqDateShown As TextBox = DirectCast(e.Row.FindControl("TextBoxPayReqDateShown"), TextBox)
            'Dim TextBoxNoteEdit As TextBox = DirectCast(e.Row.FindControl("TextBoxNoteEdit"), TextBox)

            'If DirectCast(e.Row.FindControl("LabelApproved"), Label).Text = "Approved" Then
            'FileToUpload.Enabled = False
            'ButtonUploadInvoice.Enabled = False
            'TextBoxSiteRecordEdit.Enabled = False
            'TextBoxPayReqDateShown.Enabled = False
            'TextBoxNoteEdit.Enabled = False
            'Else
            'FileToUpload.Enabled = True
            'ButtonUploadInvoice.Enabled = True
            'TextBoxSiteRecordEdit.Enabled = True
            'TextBoxPayReqDateShown.Enabled = True
            'TextBoxNoteEdit.Enabled = True
            'End If
            'End If
        End If

        ' It fixes column width problem
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Label1 As Label = DirectCast(e.Row.FindControl("Label1"), Label)

            If Label1 IsNot Nothing Then
                Label1.Text = Label1.Text.Replace(",", "," + " ")
            End If
        End If

        ' It fixes column width problem
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Label111 As Label = DirectCast(e.Row.FindControl("Label2"), Label)

            If Label111 IsNot Nothing Then
                Label111.Text = Label111.Text.Replace(",", "," + " ")
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Hyp As HyperLink = DirectCast(e.Row.FindControl("HypCvr"), HyperLink)
            If Hyp IsNot Nothing Then
                Hyp.NavigateUrl = "~/webforms/PRN_coverpage.aspx?InvoiceID=" + DataBinder.Eval(e.Row.DataItem, "InvoiceID").ToString + "&PRN=" + DataBinder.Eval(e.Row.DataItem, "SiteRecordNo").ToString
            End If
        End If

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

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

    Protected Sub GridViewEditPaymentReq_RowDeleted(sender As Object, e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridViewEditPaymentReq.RowDeleted

        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Deleted Successfully!</p>")

    End Sub

    Protected Sub GridViewEditPaymentReq_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewEditPaymentReq.RowDeleting
        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewEditPaymentReq.Rows(index)

        If CanWeDeleteThisPaymentRequest(GridViewEditPaymentReq.DataKeys(e.RowIndex).Value.ToString()) Then
            Dim zoneId As String = "Russian Standard Time"
            Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
            Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

            Dim InstanceOfDeletion As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'"

            Using cn As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                Dim cmd As New System.Data.SqlClient.SqlCommand()
                cmd.Connection = cn

                cmd.CommandText = "UPDATE Table4_PaymentRequest SET DeletedBy = " + InstanceOfDeletion + ", PersonDeleted = " + "'" + Page.User.Identity.Name.ToString + "'" + " WHERE PayReqNo = " + GridViewEditPaymentReq.DataKeys(e.RowIndex).Value.ToString()
                cmd.CommandType = System.Data.CommandType.Text
                cn.Open()
                cmd.ExecuteNonQuery()
                cn.Close()
                cn.Dispose()

            End Using

            ' delete existing PDF files for this specific payment
            ' make a connection to take LinkToInvoice
            Using conPDF As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                conPDF.Open()
                Dim sqlstringPDF As String = "SELECT RTRIM(LinkToInvoice) AS LinkToInvoice FROM Table4_PaymentRequest WHERE payreqno = " + GridViewEditPaymentReq.DataKeys(index).Value.ToString
                Dim cmdPDF As New SqlCommand(sqlstringPDF, conPDF)
                cmdPDF.CommandType = System.Data.CommandType.Text
                Dim drPDF As SqlDataReader = cmdPDF.ExecuteReader
                Dim LinkToPDF As String = ""
                While drPDF.Read
                    LinkToPDF = drPDF(0).ToString
                End While
                conPDF.Close()
                drPDF.Close()
                conPDF.Dispose()

            End Using

            Dim MyTask As New MyCommonTasks
            'MyTask.DeleteAllFileOnLocalOnFTP(LinkToPDF)
        Else
            ' Show modalpopup
            e.Cancel = True
            ModalPopupExtenderDeleteWarning.Show()
        End If

    End Sub

    Protected Function CanWeDeleteThisPaymentRequest(ByVal PayReqNo As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     COUNT(dbo.Table5_PayLog.PayReqNo) AS Expr1 " +
        " FROM         dbo.Table4_PaymentRequest LEFT OUTER JOIN " +
        "                       dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo " +
        " WHERE     (dbo.Table4_PaymentRequest.PayReqNo = " + PayReqNo + ") "

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

    Protected Sub GridViewEditPaymentReq_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles GridViewEditPaymentReq.RowUpdated
        ' Clear the error message.
        Message.Text = ""
        LabelFileName.Text = ""

        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Updated Successfully!</p>")

    End Sub

    Protected Sub GridViewEditPaymentReq_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewEditPaymentReq.RowUpdating
        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewEditPaymentReq.Rows(index)

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim TextBoxPayReqDateShown As TextBox = DirectCast(row.FindControl("TextBoxPayReqDateShown"), TextBox)

        Dim LabelLinkToInvoiceToTransfer As Label = DirectCast(row.FindControl("LabelLinkToInvoiceToTransfer"), Label)

        If Convert.ToDateTime(result) < Convert.ToDateTime(Mid(TextBoxPayReqDateShown.Text.ToString, 1, 2).ToString + "/" + Mid(TextBoxPayReqDateShown.Text.ToString, 4, 2).ToString + "/" + Mid(TextBoxPayReqDateShown.Text.ToString, 7, 4).ToString) Then
            Message.Text = "Payment Requsition date can not be later than today"
            e.Cancel = True
            Exit Sub
        Else

            ' e.NewValues("ContractReference") = LTrim(RTrim(DirectCast(row.FindControl("TextBoxContractReference"), TextBox).Text))
            e.NewValues("ContractReference") = String.Empty

            ' move all codes here
            ' update action details 
            Dim InstanceOfUpdate As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'"
            Dim PersonUpdate As String = Page.User.Identity.Name.ToString
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = "update Table4_PaymentRequest set UpdatedBy = " + InstanceOfUpdate +
                                          ", PersonUpdated = '" + PersonUpdate + "'" +
                                          " where PayReqNo = " + GridViewEditPaymentReq.DataKeys(e.RowIndex).Value.ToString()
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                Dim dr As SqlDataReader = cmd.ExecuteReader
                con.Close()
                dr.Close()
                con.Dispose()

            End Using

            e.NewValues("PayReqDate") = Convert.ToDateTime(Mid(TextBoxPayReqDateShown.Text.ToString, 1, 2).ToString + "/" + Mid(TextBoxPayReqDateShown.Text.ToString, 4, 2).ToString + "/" + Mid(TextBoxPayReqDateShown.Text.ToString, 7, 4).ToString)

            Dim LabelInvoiceLink As Label = DirectCast(row.FindControl("LabelInvoiceEdit"), Label)

            ' update action details 
            Dim LabelAttachmentChange As String = ""
            Dim LabelAttachmentChangeWhen As String = ""
            Dim LabelPersonChangedAttachment As String = ""
            Dim InstanceOfChange As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'"

            ' Delete existing PDF file if user replace with another one.

            If LabelLinkToInvoiceToTransfer.Text = "" Then
                e.NewValues("LinkToInvoice") = LabelInvoiceLink.Text
            Else
                e.NewValues("LinkToInvoice") = LabelLinkToInvoiceToTransfer.Text

                ' delete existing PDF
                Dim MyTask As New MyCommonTasks
                'MyTask.DeleteAllFileOnLocalOnFTP(e.OldValues("LinkToInvoice").ToString)

                ' mark this row as Attachment Changed
                LabelAttachmentChange = "1"
                LabelAttachmentChangeWhen = InstanceOfChange
                LabelPersonChangedAttachment = "'" + Page.User.Identity.Name.ToString + "'"

                ' execute update
                Using conUpdate As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    conUpdate.Open()
                    Dim sqlstringUpdate As String = " UPDATE [dbo].[Table4_PaymentRequest] " +
                                                    "    SET [AttachmentChange] = " + LabelAttachmentChange +
                                                    "       ,[AttachmentChangeWhen] =  " + LabelAttachmentChangeWhen +
                                                    "       ,[PersonChangedAttachment] =  " + LabelPersonChangedAttachment +
                                                    "  WHERE PayReqNo= " + GridViewEditPaymentReq.DataKeys(e.RowIndex).Value.ToString()
                    Dim cmdUpdate As New SqlCommand(sqlstringUpdate, conUpdate)
                    cmdUpdate.CommandType = System.Data.CommandType.Text
                    Dim drUpdate As SqlDataReader = cmdUpdate.ExecuteReader
                    conUpdate.Close()
                    drUpdate.Close()
                    conUpdate.Dispose()

                End Using
            End If

    End If
  End Sub

  Protected Sub DropDownListSupplier_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListSupplier.DataBound
    Dim lst As New ListItem("ALL SUPPLIER", String.Empty)
        DropDownListSupplier.Items.Insert(0, lst)

        If Request.QueryString("SupplierID") IsNot Nothing And Not IsPostBack Then
            DropDownListSupplier.SelectedValue = Request.QueryString("SupplierID")
        End If

  End Sub

    Protected Sub DropDownListSupplier_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListSupplier.SelectedIndexChanged

        'GridViewEditPaymentReq.DataBind()

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
