Imports System.IO
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Partial Class PR_PMapproval
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Request.QueryString("invoiceid") IsNot Nothing Then

            Session("highlight_invoiceid") = IIf(IsNumeric(Request.QueryString("invoiceid")), Request.QueryString("invoiceid"), 0)
            Response.Redirect("~/webforms/PR_PMapproval.aspx")

        End If

        SqlDataSourcePMapproval.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToLower

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     COUNT(dbo.Table1_Project.ProjectManager) AS CountPM " + _
 " FROM         dbo.Table3_Invoice INNER JOIN " + _
 "                      dbo.Table3_Invoice_PRrequestToPM ON dbo.Table3_Invoice.InvoiceID = dbo.Table3_Invoice_PRrequestToPM.InvoiceID INNER JOIN " + _
 "                      dbo.Table2_PONo ON dbo.Table3_Invoice.PO_No = dbo.Table2_PONo.PO_No INNER JOIN " + _
 "                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID LEFT OUTER JOIN " + _
 "                      dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID " + _
 " WHERE     (dbo.Table3_Invoice_PRrequestToPM.ApprovedOrNot = 0) AND (dbo.Table4_PaymentRequest.InvoiceID IS NULL) AND  " + _
 "                      (dbo.Table1_Project.ProjectManager = @ProjectManager) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim PM As SqlParameter = cmd.Parameters.Add("@ProjectManager", System.Data.SqlDbType.NVarChar, 256)
            PM.Value = Page.User.Identity.Name.ToLower
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                If dr(0) > 0 Then
                    BtnApproveAll.Text = "Approve All " + dr(0).ToString + " Items"
                    BtnApproveAll.OnClientClick = "return confirm('Are you sure to approve " + dr(0).ToString + " payment request?');"
                Else
                    BtnApproveAll.Visible = False
                End If
            End While

            con.Close()
            dr.Close()
        End Using


    End Sub

    Protected Sub GridViewPMapproval_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewPMapproval.RowCommand

        If (e.CommandName = "Approval") Then
            Dim rowIndex As String = e.CommandArgument
            Dim row As GridViewRow = sender.Rows(rowIndex)

            Dim LabelInvoiceID As Label = DirectCast(row.FindControl("LabelInvoiceID"), Label)
            Dim LabelProjectID As Label = DirectCast(row.FindControl("LabelProjectID"), Label)
            Dim LabelProjectName As Label = DirectCast(row.FindControl("LabelProjectName"), Label)

            ' Execute Approval
            ExecuteApproval(LabelInvoiceID.Text, LabelProjectID.Text, LabelProjectName.Text)
            GridViewPMapproval.DataBind()

        End If
    End Sub

    Protected Sub GridViewPMapproval_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewPMapproval.RowDataBound

        Dim _user As String = Page.User.Identity.Name.ToLower

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim SqlDataSourceApprovalStatus As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceApprovalStatus"), SqlDataSource)

            If SqlDataSourceApprovalStatus IsNot Nothing Then
                SqlDataSourceApprovalStatus.SelectParameters("InvoiceID").DefaultValue =
                  DataBinder.Eval(e.Row.DataItem, "InvoiceID")
            End If

            If DataBinder.Eval(e.Row.DataItem, "InvoiceID") = Session("highlight_invoiceid") Then
                e.Row.Attributes.Add("data-highligh", String.Empty)
                Session.Remove("highlight_invoiceid")
            End If

            Dim ImageButtonApproval As LinkButton = DirectCast(e.Row.FindControl("ImageButtonApproval"), LinkButton)

            If DataBinder.Eval(e.Row.DataItem, "ApprovedOrNot") = True Then
                ' approved
                ImageButtonApproval.CssClass = "label label-xlg label-success arrowed-in arrowed-in-right cursor_notallowed"
                ImageButtonApproval.Text = "approved"
            Else
                If DataBinder.Eval(e.Row.DataItem, "ProjectManager").tolower = _user Or User.Identity.Name.ToLower = "savas" Then
                    ' clikc to approve
                    ImageButtonApproval.CssClass = "label label-success label-xlg arrowed-in icon-animated-vertical"
                    ImageButtonApproval.Text = "click to approve"
                Else
                    ' not approved
                    ImageButtonApproval.CssClass = "label label-xlg label-inverse arrowed-in cursor_notallowed"
                    ImageButtonApproval.Text = "not approved"
                End If

            End If

            If DataBinder.Eval(e.Row.DataItem, "ProjectManager").ToLower = _user Or User.Identity.Name.ToLower = "savas" Then
                ImageButtonApproval.Enabled = True
                ImageButtonApproval.OnClientClick = "return confirm('Are you sure to approve this payment request?');"
            Else
                ImageButtonApproval.Enabled = False
            End If

            DirectCast(e.Row.FindControl("ImageButtonPdf"), HyperLink).ImageUrl = "~/Images/pdf.bmp"
            DirectCast(e.Row.FindControl("ImageButtonPdf"), HyperLink).NavigateUrl = "~/webforms/showFile.aspx?link=" + Replace(GetLinkToInvoice(DataBinder.Eval(e.Row.DataItem, "InvoiceID")), "~", "")

            ' provide project icon
            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
                Dim prjid As Int16 = DataBinder.Eval(e.Row.DataItem, "ProjectID")
                Dim Prj = (From C In db.Table1_Project Where C.ProjectID = prjid)
                Try
                    e.Row.Cells(1).Text = e.Row.Cells(1).Text + "<img style=" + """" + "width:80px;" + """" + "  src=" + """" + Prj.ToList()(0).Logo.ToString.Replace("~", "") + """" + " />"
                Catch ex As Exception

                End Try

            End Using

            ' approve if needed
            If PTS_MERCURY.helper.View_PaymentRequestApprovalStatus.IfAllApproved(DataBinder.Eval(e.Row.DataItem, "InvoiceID")) = True Then
                Try
                    ExecuteApproval(DataBinder.Eval(e.Row.DataItem, "InvoiceID"), DataBinder.Eval(e.Row.DataItem, "ProjectID"), DataBinder.Eval(e.Row.DataItem, "ProjectName"))

                Catch ex As Exception

                End Try

            End If

        End If



    End Sub

    Protected Function GetLinkToInvoice(ByVal _invoiceid As Integer) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SELECT RTRIM([LinkPDF]) FROM [dbo].[Table3_Invoice_PDF] WHERE InvoiceID = @InvoiceID"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim InvoiceID As SqlParameter = cmd.Parameters.Add("@InvoiceID", System.Data.SqlDbType.Int)
            InvoiceID.Value = _invoiceid
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                If String.IsNullOrEmpty(dr(0).ToString) Then
                    Return "-"
                Else
                    Return dr(0)
                End If
            End While
            con.Close()
            dr.Close()
        End Using

    End Function

    Protected Sub ExecuteApproval(ByVal _InvoiceID As Integer, ByVal _ProjectID As Integer, ByVal _ProjectName As String)

        ' Process payment request INSERT

        ' this condition is not necessarily needed but keep it here, it doesnt disturb new logic
        If PTS.CoreTables.CreateDataReader.Create_Table1_Project(_ProjectID).PR_Coverpage_Approval_Compulsory = True _
        And InvoicePDFTables.GetCountOfInvoicePDF(_InvoiceID) > 0 Then

            Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
            Dim _Path As String = "REQUEST/_CoverPagePDF/" + _InvoiceID.ToString + UniqueString1 + ".pdf"

            ' Process cover page.
            RenderReport.Render("discPDF", ReportViewerInvoiceCoverPage, "PR_CoverPages",
                                "InvoiceID", _InvoiceID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                _Path)

            ' Merge with invoice
            ' This is cover page
            Dim file1 As String = Server.MapPath("~/" + _Path)
            ' This is invoice
            Dim file2 As String = Server.MapPath(InvoicePDFTables.Table3_Invoice_PDF_Row_ByInvoiceID(_InvoiceID).LinkPDF.Trim)

            Dim anArrayOfPDFs() As String = {file1, file2}

            ' Determine destination file name
            Dim DestinationFileName As String
            If Directory.Exists(Server.MapPath("~/REQUEST/") + _ProjectName + "/") Then
                DestinationFileName = "~/REQUEST/" + _ProjectName + "/" + _InvoiceID.ToString + UniqueString1 + ".pdf"
            Else
                Directory.CreateDirectory(Server.MapPath("~/REQUEST/") + _ProjectName)
                DestinationFileName = "~/REQUEST/" + _ProjectName + "/" + _InvoiceID.ToString + UniqueString1 + ".pdf"
            End If

            InvoicePDFTables.MergeFiles(Server.MapPath(DestinationFileName), anArrayOfPDFs)

            ' process payment request insert
            Using Adapter As New InvoicePDFTableAdapters.QueriesTableAdapter

                Try
                    Adapter.InsertPaymentRequest(
                        InvoicePDFTables.Table3_Invoice_PRrequestToPM_Row_ByInvoiceID(_InvoiceID).SiteReqNo,
                        _InvoiceID,
                        InvoicePDFTables.Table3_Invoice_PRrequestToPM_Row_ByInvoiceID(_InvoiceID).PayReqDate,
                        DestinationFileName,
                        DateTime.Now,
                        Page.User.Identity.Name.ToLower)

                Catch ex As Exception

                End Try

            End Using


            ' Send Email Notifications

            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = "SELECT     RTRIM(dbo.aspnet_Membership.Email) AS Email " +
          " FROM         dbo.aspnet_Membership INNER JOIN " +
          "                       dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId INNER JOIN " +
          "                       dbo.Table_Prj_User_Junction ON dbo.aspnet_Users.UserId = dbo.Table_Prj_User_Junction.UserID INNER JOIN " +
          "                       dbo.Table1_Project ON dbo.Table_Prj_User_Junction.ProjectID = dbo.Table1_Project.ProjectID " +
          " WHERE     (RTRIM(dbo.Table1_Project.ProjectID) = @ProjectID) AND (dbo.aspnet_Users.SendEmailPaymentReq = 1)  "

                Dim cmd As New SqlCommand(sqlstring, con)

                cmd.CommandType = System.Data.CommandType.Text

                Dim ProjectN As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)

                ProjectN.Value = _ProjectID
                Dim dr As SqlDataReader = cmd.ExecuteReader
                ' __________THIS PART MAY BE ACTIVATED LATER. IT SEND EMAIL TO ONLY SERGEI AND OTHERS

                '(1) Create the MailMessage instance
                Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Mercury Russia Procurement")
                Dim mm1 As New MailMessage()
                mm1.From = MailFrom

                ' THIS PART MAY BE ACTIVATED LATER. IT SEND EMAIL TO ONLY SERGEI AND OTHERS
                While dr.Read
                    If dr(0).ToString.Length <> 0 Then
                        mm1.Bcc.Add(dr(0).ToString)
                    End If
                End While

                mm1.Bcc.Add(GetRequesterEmail(_InvoiceID))

                con.Close()
                con.Dispose()
                dr.Close()
                ' __________THIS PART MAY BE ACTIVATED LATER. IT SEND EMAIL TO ONLY SERGEI AND OTHERS

                ' THIS PART MAY BE REMOVED LATER IF WE NEED TO SEND NOT ONLY TO SERGEI AND OTHERS

                'mm1.To.Add("savas.karaduman@mercuryeng.ru")
                'mm1.To.Add("sergei.borisov@mercuryeng.ru")
                'mm1.To.Add("kirill.golubin@mercuryeng.ru")
                'mm1.To.Add("pavel.silantiev@mercuryeng.ru")
                'mm1.CC.Add("jim.collender@mercuryeng.ru")

                ' ______THIS PART MAY BE REMOVED LATER IF WE NEED TO SEND NOT ONLY TO SERGEI AND OTHERS

                '(2) Assign the MailMessage's properties
                Dim urg As String = ""

                Try
                    If InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).Urgency = "Urgent" Then
                        urg = "Urgent ! "
                    Else
                        urg = ""
                    End If

                Catch ex As Exception

                End Try

                Dim ErrorMsg As String = ""

                Try
                    ErrorMsg = urg + "Новый запрос платежа от " + Page.User.Identity.Name.ToString + "| проект: " + _ProjectName +
                                          "| поставщик : " + InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).SupplierName +
                                          "| Номер счета : " + InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).Invoice_No +
                                          "| Дата счета : " + InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).Invoice_Date +
                                          "| Описание : " + InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).Description


                Catch ex As Exception

                End Try

                mm1.Subject = ErrorMsg.Replace(Environment.NewLine, " ")

                ' update Notification table
                Dim SendNotification As New MyCommonTasks
                SendNotification.SendNotification(ErrorMsg, _ProjectID.ToString, 1)

                mm1.Body = "<div style=" + """" + "color: #FF0000; font-weight: bold" + """" + "> Это автоматическая электронная почта. Пожалуйста, не отвечайте </div>" +
            " <hr />" +
            "    <table style=" + """" + "background-color: #C0C0C0; font-family: tahoma; font-size: medium;" + """" + " > " +
            "        <tr> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                Новый запрос платежа от</td> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                " + Page.User.Identity.Name.ToString + "</td> " +
            "        </tr> " +
            "        <tr> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                проект:</td> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                " + _ProjectName + "</td> " +
            "        </tr> " +
            "        <tr> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                поставщик :</td> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                " + InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).SupplierName + "</td> " +
            "        </tr> " +
            "        <tr> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                Номер счета :</td> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                " + InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).Invoice_No + "</td> " +
            "        </tr> " +
            "        <tr> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                Дата счета :</td> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                " + String.Format("{0:dd/MM/yyyy}", InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).Invoice_Date) + "</td> " +
            "        </tr> " +
            "        <tr> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                Описание :</td> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                " + InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).Description + " </td> " +
            "        </tr> " +
            "        <tr> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                Запись сайта Номер :</td> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                " + InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).SiteRecordNo + "</td> " +
            "        </tr> " +
            "        <tr> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                Стоимость счета без НДС :</td> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                " + String.Format("{0:N2}", InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).InvoiceValue) + "</td> " +
            "        </tr> " +
            "        <tr> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                валюта :</td> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                " + InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).PO_Currency + "</td> " +
            "        </tr> " +
            "        <tr> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                Срочность :</td> " +
            "            <td style=" + """" + "width: 249px; background-color: #F5F5F5" + """" + ">" +
            "                " + InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).Urgency + "</td> " +
            "        </tr> " +
            "    </table> "

                mm1.IsBodyHtml = True

                ' add attachment if exist
                Dim path As String = Server.MapPath(InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).LinkToInvoice)
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                If file.Exists Then
                    ' JUST ACTIVATE IF REQUIRED
                    mm1.Attachments.Add(New Attachment(path))
                End If

                '(3) Create the SmtpClient object
                Dim smtp As New SmtpClient_RussianEncoded

                '(4) Send the MailMessage (will use the Web.config settings)
                ' JUST ACTIVATE IF REQUIRED
                If InvoicePDFTables.PayReqEmailBodyByInvoiceID(_InvoiceID).SupplierName = "MERCURY, OOO" Then
                Else

                    'Try
                    smtp.Send(mm1)
                    'Catch ex As Exception

                    'End Try

                End If

                '***************************************
                '______________________________

            End Using

        Else
            ' payment request insert will be executed by user
            ' send email notification to Requester
            Using Adapter As New MercuryTableAdapters.Table3_Invoice_PRrequestToPMTableAdapter
                Dim table As New Mercury.Table3_Invoice_PRrequestToPMDataTable
                table = Adapter.GetDataByInvoiceID(_InvoiceID)
                For Each _row As Mercury.Table3_Invoice_PRrequestToPMRow In table
                    If _row.ApprovedOrNot = True Then
                        ' send email to Requester
                        ' patameters to be >toWhom
                        Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", "Mercury Russia Procurement")
                        Dim mm1 As New MailMessage()
                        mm1.From = MailFrom
                        'mm1.To.Add("savas.erzin@gmail.com")
                        mm1.To.Add(GetRequesterEmail(_InvoiceID))
                        mm1.To.Add("sk@mercuryeng.ru")

                        mm1.Subject = GetSubject(_InvoiceID)
                        mm1.Body = GetBody(_InvoiceID)
                        mm1.IsBodyHtml = True

                        Dim smtp As New SmtpClient_RussianEncoded
                        smtp.Send(mm1)

                    End If
                Next
                table.Dispose()
                Adapter.Dispose()
            End Using

        End If

        Response.Redirect("~/webforms/PR_PMapproval.aspx")

        ' this part is not needed anymore. Gridview doing this part

        'Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        '    con.Open()
        '    Dim sqlstring As String = _
        '    " IF (SELECT [ApprovedOrNot] FROM [Table3_Invoice_PRrequestToPM] WHERE InvoiceID = @InvoiceID) = 0  " + _
        '    " BEGIN " + _
        '    " 	UPDATE [dbo].[Table3_Invoice_PRrequestToPM] " + _
        '    " 	   SET [ApprovedOrNot] = 1 " + _
        '    " 		  ,[WhenApproved] = GETDATE() " + _
        '    " 		  ,[WhoApproved] = @WhoApproved " + _
        '    " 	 WHERE InvoiceID = @InvoiceID " + _
        '    " END " + _
        '    " ELSE " + _
        '    " BEGIN " + _
        '    " 	UPDATE [dbo].[Table3_Invoice_PRrequestToPM] " + _
        '    " 	   SET [ApprovedOrNot] = 0 " + _
        '    " 		  ,[WhenApproved] = NULL " + _
        '    " 		  ,[WhoApproved] = NULL " + _
        '    " 	 WHERE InvoiceID = @InvoiceID " + _
        '    " END "

        '    Dim cmd As New SqlCommand(sqlstring, con)
        '    cmd.CommandType =System.Data.CommandType.Text

        '    'syntax for parameter adding
        '    Dim InvoiceID As SqlParameter = cmd.Parameters.Add("@InvoiceID",System.Data.SqlDbType.Int)
        '    InvoiceID.Value = _InvoiceID
        '    Dim WhoApproved As SqlParameter = cmd.Parameters.Add("@WhoApproved",System.Data.SqlDbType.NVarChar, 256)
        '    WhoApproved.Value = Page.User.Identity.Name.ToLower

        '    Dim dr As SqlDataReader = cmd.ExecuteReader
        '    con.Close()
        '    dr.Close()

        'End Using

    End Sub

    Protected Function GetRequesterEmail(ByVal _InvoiceID As Integer) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String =
                " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail " +
                " FROM       dbo.aspnet_Membership INNER JOIN " +
                "            dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId INNER JOIN " +
                "            dbo.Table3_Invoice_PRrequestToPM ON dbo.aspnet_Users.UserName = dbo.Table3_Invoice_PRrequestToPM.WhoRequested " +
                " WHERE      (dbo.Table3_Invoice_PRrequestToPM.InvoiceID = @InvoiceID) "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim InvoiceID As SqlParameter = cmd.Parameters.Add("@InvoiceID", System.Data.SqlDbType.Int)
            InvoiceID.Value = _InvoiceID
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Return dr(0)
            End While
            con.Close()
            dr.Close()
        End Using

    End Function

    Protected Function GetSubject(ByVal _InvoiceID As Integer) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, RTRIM(dbo.Table3_Invoice.Invoice_No)  " +
                "                       AS Invoice_No " +
                " FROM         dbo.Table2_PONo INNER JOIN " +
                "                       dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN " +
                "                       dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " +
                "                       dbo.Table3_Invoice_PRrequestToPM ON dbo.Table3_Invoice.InvoiceID = dbo.Table3_Invoice_PRrequestToPM.InvoiceID INNER JOIN " +
                "                       dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID " +
                " WHERE     (dbo.Table3_Invoice_PRrequestToPM.InvoiceID = @InvoiceID) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim InvoiceID As SqlParameter = cmd.Parameters.Add("@InvoiceID", System.Data.SqlDbType.Int)
            InvoiceID.Value = _InvoiceID
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Return "PR Approved | " + dr(0) + " | " + dr(1) + " | " + dr(2) + " |"
            End While
            con.Close()
            dr.Close()
        End Using

    End Function

    Protected Function GetBody(ByVal _InvoiceID As Integer) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String =
                " SELECT     InvoiceID, rtrim(SiteReqNo) AS SiteReqNo " +
                " FROM dbo.Table3_Invoice_PRrequestToPM " +
                " WHERE     (InvoiceID = @InvoiceID) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim InvoiceID As SqlParameter = cmd.Parameters.Add("@InvoiceID", System.Data.SqlDbType.Int)
            InvoiceID.Value = _InvoiceID
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Return "This PRN is approved." + " <a href=" + """" + "http://pts.mercuryeng.ru/webforms/PRN_coverpage.aspx?InvoiceID=" + dr(0).ToString + "&PRN=" + dr(1).ToString + """" + " target=" + """" + "_blank" + """" + ">CLICK TO SEE COVER PAGE</a> "
            End While
            con.Close()
            dr.Close()
        End Using

    End Function

    Protected Sub BtnApproveAll_Click(sender As Object, e As EventArgs)

        For Each grRow As GridViewRow In GridViewPMapproval.Rows

            If grRow.RowType = DataControlRowType.DataRow Then

                If DataBinder.Eval(grRow.DataItem, "ApprovedOrNot") = False Then

                    If DataBinder.Eval(grRow.DataItem, "ProjectManager").tolower = Page.User.Identity.Name.ToLower Then
                        ' Approve This Item
                        ExecuteApproval(DataBinder.Eval(grRow.DataItem, "InvoiceID") _
                                        , DataBinder.Eval(grRow.DataItem, "ProjectID") _
                                        , DataBinder.Eval(grRow.DataItem, "ProjectName"))
                    End If

                End If

            End If

        Next

        GridViewPMapproval.DataBind()

    End Sub

    Protected Sub GridviewApprovalStatus_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim _sender As GridView = sender
        Dim _page As Page = Page

        If (e.CommandName = "Approval") Then
            Dim rowIndex As String = e.CommandArgument
            Dim GridviewApprovalStatus As GridView = _sender
            Dim row As GridViewRow = GridviewApprovalStatus.Rows(rowIndex)

            Dim LiteralApproved As Literal = DirectCast(row.FindControl("LiteralApproved"), Literal)
            Dim LiteralInvoiceId As Literal = DirectCast(row.FindControl("LiteralInvoiceId"), Literal)
            Dim LiteralUserName As Literal = DirectCast(row.FindControl("LiteralUserName"), Literal)
            Dim LiteralPosition As Literal = DirectCast(row.FindControl("LiteralPosition"), Literal)

            ' It is approval or NOT approval ?
            If LiteralApproved.Text = "0" Then
                ' APPROVE

                If LiteralPosition.Text.Trim().ToLower() <> "pm" Then

                    If PTS_MERCURY.helper.Table3_Invoice_AdditionalUserApprovals.CountItemsByInvoiceIdUserName(LiteralInvoiceId.Text.Trim(), LiteralUserName.Text.Trim()) = 0 Then

                        PTS_MERCURY.helper.Table3_Invoice_AdditionalUserApprovals.Insert(LiteralInvoiceId.Text.Trim(), LiteralUserName.Text.Trim())

                    End If

                Else

                    PTS_MERCURY.helper.Table3_Invoice_PRrequestToPM.ApproveForPM(LiteralInvoiceId.Text.Trim(), LiteralUserName.Text.Trim())

                End If

                'Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                '                               _page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                '                               ">You have approved successfully!</p>")
            ElseIf LiteralApproved.Text = "1" Then

                ' NOT APPROVE
                If LiteralPosition.Text.Trim().ToLower() <> "pm" Then
                    If PTS_MERCURY.helper.Table3_Invoice_AdditionalUserApprovals.CountItemsByInvoiceIdUserName(LiteralInvoiceId.Text.Trim(), LiteralUserName.Text.Trim()) > 0 Then

                        PTS_MERCURY.helper.Table3_Invoice_AdditionalUserApprovals.Delete(LiteralInvoiceId.Text.Trim(), LiteralUserName.Text.Trim())

                    End If

                Else
                    PTS_MERCURY.helper.Table3_Invoice_PRrequestToPM.RemoveApprovalForPM(LiteralInvoiceId.Text.Trim(), LiteralUserName.Text.Trim())

                End If
                'Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                '                               _page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                '                               ">You have removed your approval successfully!</p>")
            End If

            ' CHECK CONTRACT IF IT IS READY FOR PO
            'ContractView.Initiate_Checking_Contract_Or_Addendum_To_Insert_Or_Update_Po_Then_Finally_Execute( _
            '                                        Convert.ToInt32(LiteralContractID.Text), _
            '                                        0, _
            '                                        _podetailsforEmail)

        End If

        GridViewPMapproval.DataBind()

    End Sub

    Protected Sub GridviewApprovalStatus_RowDataBound(sender As Object, e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim _InvoiceID As Integer = DataBinder.Eval(e.Row.DataItem, "InvoiceID")

            Dim ImageButtonApproval As ImageButton = DirectCast(e.Row.FindControl("ImageButtonApproval"), ImageButton)

            ' Define imageURL as per approval status
            If DataBinder.Eval(e.Row.DataItem, "ApprovedOrNot") = 0 Then
                ImageButtonApproval.ImageUrl = "~/images/ContractNotApprove.png"
            ElseIf DataBinder.Eval(e.Row.DataItem, "ApprovedOrNot") = 1 Then
                ImageButtonApproval.ImageUrl = "~/images/ContractApprove.png"
            End If

                ' Diasable or enable ImageButton as per user
            If DataBinder.Eval(e.Row.DataItem, "ApprovalRequiredPersons").ToString.ToLower.Trim() = HttpContext.Current.User.Identity.Name.ToLower.Trim() Then
                ImageButtonApproval.Enabled = True
                ImageButtonApproval.CssClass = "icon-animated-wrench"
                ImageButtonApproval.OnClientClick = "return confirm('Are you sure to approve this payment request?');"
            ElseIf DataBinder.Eval(e.Row.DataItem, "ApprovalRequiredPersons").ToString.ToLower.Trim() <> HttpContext.Current.User.Identity.Name.ToLower.Trim() Then
                ImageButtonApproval.Enabled = False
            End If

        End If

    End Sub
End Class
