Imports System.Data.SqlClient

Partial Class podetails
  Inherits System.Web.UI.Page


  Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    'ScriptManager1.SupportsPartialRendering = False
  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    GridViewMonitor.DataBind()
  End Sub

  Protected Sub GridViewMonitor_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewMonitor.PreRender
    ' Define SelectCommand statement based on PrjID and CostCode querystring parameter
    If Request.QueryString("ProjectID") IsNot Nothing AndAlso Request.QueryString("CostCode") IsNot Nothing Then

            SqlDataSourceMonitor.SelectCommand = " SELECT * FROM ( " + _
      " SELECT     dbo.Table2_PONo_Sub.SubPO_No AS PO_No, RTRIM(dbo.Table2_PONo.Description) AS Description, dbo.Table2_PONo_Sub.TotalPrice AS POtotalprice,   " + _
      "                                                dbo.Table2_PONo.VATpercent, dbo.Table2_PONo.PO_Currency, dbo.Table2_PONo.PO_Date, NULL AS Invoice_No, NULL AS Invoice_Date, NULL    " + _
      "                                                AS Invoice_value, NULL AS SiteRecordNo, NULL AS PayReqDate, NULL AS LinkToInvoice, NULL AS Urgency, NULL AS PersonApprove, NULL    " + _
      "                                                AS FinanceNo, NULL AS PaymentDate, NULL AS Payment_amount, NULL AS Payment_currency, RTRIM(dbo.Table2_PONo_Sub.CostCode) AS CostCode,    " + _
      "                                                RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, N'false' AS AttachmentExist   " + _
      "                         FROM          dbo.Table2_PONo_Sub INNER JOIN   " + _
      "                                                dbo.Table2_PONo ON dbo.Table2_PONo_Sub.PO_No = dbo.Table2_PONo.PO_No INNER JOIN   " + _
      "                                                dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN   " + _
      "                                                dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN   " + _
      "                                                dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN   " + _
      "                                                dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId   " + _
      "                         WHERE      (dbo.Table2_PONo.PO_No IN   " + _
      "                                                    (SELECT     Table2_PONo_1.PO_No   " + _
      "                                                      FROM          dbo.Table2_PONo AS Table2_PONo_1 INNER JOIN   " + _
      "                                                                                 (SELECT     PO_No, SUM(TotalPrice) AS TotalPrice   " + _
      "                                                                                   FROM          dbo.Table2_PONo_Sub AS Table2_PONo_Sub_1   " + _
      "                                                                                   GROUP BY PO_No) AS DataSource1 ON DataSource1.PO_No = Table2_PONo_1.PO_No AND    " + _
      "                                                                             Table2_PONo_1.TotalPrice = DataSource1.TotalPrice)) AND  (dbo.Table1_Project.ProjectID = " + Request.QueryString("ProjectID").ToString() + ") AND (RTRIM(dbo.aspnet_Users.UserName) = N'" + Page.User.Identity.Name.ToString + "') AND (dbo.Table2_PONo_Sub.CostCode = N'" + Request.QueryString("CostCode").ToString() + "')  " + _
      "  " + _
      "  " + _
      " UNION ALL " + _
      "  " + _
      " SELECT     TOP (100) PERCENT dbo.Table2_PONo.PO_No, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR " + _
      "                       dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(Table2_PONo.Description) ELSE RTRIM(Table2_PONo.Description) + ' / ' + RTRIM(dbo.Table3_Invoice.Notes)  " + _
      "                       END AS Description, CONVERT(decimal(12, 2), dbo.Table2_PONo.TotalPrice) AS POtotalprice, dbo.Table2_PONo.VATpercent, dbo.Table2_PONo.PO_Currency,  " + _
      "                       dbo.Table2_PONo.PO_Date, dbo.Table3_Invoice.Invoice_No, dbo.Table3_Invoice.Invoice_Date, CONVERT(decimal(12, 2), dbo.Table3_Invoice.InvoiceValue)  " + _
      "                       AS Invoice_value, RTRIM(dbo.Table4_PaymentRequest.SiteRecordNo) AS SiteRecordNo, dbo.Table4_PaymentRequest.PayReqDate,  " + _
      "                       RTRIM(dbo.Table4_PaymentRequest.LinkToInvoice) AS LinkToInvoice, RTRIM(dbo.Table4_PaymentRequest.Urgency) AS Urgency,  " + _
      "                       RTRIM(dbo.Table4_PaymentRequest.PersonApprove) AS PersonApprove, RTRIM(dbo.Table5_PayLog.FinanceNo) AS FinanceNo, dbo.Table5_PayLog.PaymentDate,  " + _
      "                       CONVERT(decimal(12, 2), dbo.Table5_PayLog.Amount) AS Payment_amount, RTRIM(dbo.Table5_PayLog.Currency) AS Payment_currency,  " + _
      "                       RTRIM(CONVERT(nvarchar(20), dbo.Table7_CostCode.CostCode)) + N' - ' + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode,  " + _
      "                       RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, CASE WHEN AttachmentExist IS NULL  " + _
      "                       THEN N'False' ELSE AttachmentExist END AS AttachmentExist " + _
      " FROM         dbo.View_SubPoEqualToTotalPo RIGHT OUTER JOIN " + _
      "                       dbo.Table6_Supplier INNER JOIN " + _
      "                       dbo.Table2_PONo ON dbo.Table6_Supplier.SupplierID = dbo.Table2_PONo.SupplierID INNER JOIN " + _
      "                       dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode ON  " + _
      "                       dbo.View_SubPoEqualToTotalPo.PO_No = dbo.Table2_PONo.PO_No LEFT OUTER JOIN " + _
      "                       dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No RIGHT OUTER JOIN " + _
      "                       dbo.Table1_Project INNER JOIN " + _
      "                       dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN " + _
      "                       dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId ON  " + _
      "                       dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID LEFT OUTER JOIN " + _
      "                       dbo.Table5_PayLog RIGHT OUTER JOIN " + _
      "                       dbo.Table4_PaymentRequest ON dbo.Table5_PayLog.PayReqNo = dbo.Table4_PaymentRequest.PayReqNo ON  " + _
      "                       dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID " + _
      " WHERE     (dbo.Table1_Project.ProjectID = " + Request.QueryString("ProjectID").ToString() + ") AND (RTRIM(dbo.aspnet_Users.UserName) = N'" + Page.User.Identity.Name.ToString + "') AND (dbo.Table2_PONo.CostCode = N'" + Request.QueryString("CostCode").ToString() + "') AND  " + _
      "                       (dbo.View_SubPoEqualToTotalPo.PO_No IS NULL) " + _
      "                       ) As DataSourceALL " + _
      " ORDER BY PO_No                       "
    ElseIf Request.QueryString("PoNo") IsNot Nothing Then

            SqlDataSourceMonitor.SelectCommand = " SELECT     TOP (100) PERCENT dbo.Table2_PONo.PO_No, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR " + _
      "                       dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description)  " + _
      "                       + ' / ' + RTRIM(dbo.Table3_Invoice.Notes) END AS Description, CONVERT(decimal(12, 2), dbo.Table2_PONo.TotalPrice) AS POtotalprice,  " + _
      "                       dbo.Table2_PONo.VATpercent, dbo.Table2_PONo.PO_Currency, dbo.Table2_PONo.PO_Date, dbo.Table3_Invoice.Invoice_No,  " + _
      "                       dbo.Table3_Invoice.Invoice_Date, CONVERT(decimal(12, 2), dbo.Table3_Invoice.InvoiceValue) AS Invoice_value,  " + _
      "                       RTRIM(dbo.Table4_PaymentRequest.SiteRecordNo) AS SiteRecordNo, dbo.Table4_PaymentRequest.PayReqDate,  " + _
      "                       RTRIM(dbo.Table4_PaymentRequest.LinkToInvoice) AS LinkToInvoice, RTRIM(dbo.Table4_PaymentRequest.Urgency) AS Urgency,  " + _
      "                       RTRIM(dbo.Table4_PaymentRequest.PersonApprove) AS PersonApprove, RTRIM(dbo.Table5_PayLog.FinanceNo) AS FinanceNo,  " + _
      "                       dbo.Table5_PayLog.PaymentDate, CONVERT(decimal(12, 2), dbo.Table5_PayLog.Amount) AS Payment_amount, RTRIM(dbo.Table5_PayLog.Currency)  " + _
      "                       AS Payment_currency, RTRIM(CONVERT(nvarchar(20), dbo.Table7_CostCode.CostCode)) + N' - ' + RTRIM(dbo.Table7_CostCode.CodeDescription)  " + _
      "                       AS CostCode, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName,  " + _
      "                       CASE WHEN AttachmentExist IS NULL THEN N'False' ELSE AttachmentExist END AS AttachmentExist " + _
      " FROM         dbo.Table1_Project LEFT OUTER JOIN " + _
      "                       dbo.Table6_Supplier INNER JOIN " + _
      "                       dbo.Table2_PONo ON dbo.Table6_Supplier.SupplierID = dbo.Table2_PONo.SupplierID INNER JOIN " + _
      "                       dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode LEFT OUTER JOIN " + _
      "                       dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No ON  " + _
      "                       dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID LEFT OUTER JOIN " + _
      "                       dbo.Table5_PayLog RIGHT OUTER JOIN " + _
      "                       dbo.Table4_PaymentRequest ON dbo.Table5_PayLog.PayReqNo = dbo.Table4_PaymentRequest.PayReqNo ON  " + _
      "                       dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID " + _
      " WHERE     (dbo.Table2_PONo.PO_No = N'" + Request.QueryString("PONo").ToString() + "') " + _
      " ORDER BY dbo.Table2_PONo.PO_No "


    End If
  End Sub

  Protected Sub GridViewMonitor_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewMonitor.RowCommand
    If (e.CommandName.ToLower = "openpdf") Then

    End If
  End Sub

  Protected Sub GridViewMonitor_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewMonitor.RowDataBound

    Dim MyTask As New MyCommonTasks
    MyTask.HoverEffectOnGridviewCells(sender, e.Row)

    If e.Row.RowType = DataControlRowType.DataRow Then
      'it defines type of PDF image if it exist or not.
      If DataBinder.Eval(e.Row.DataItem, "AttachmentExist") = True Then
        DirectCast(e.Row.FindControl("ImageButtonPdf"), HyperLink).ImageUrl = "~/Images/pdf.bmp"
                DirectCast(e.Row.FindControl("ImageButtonPdf"), HyperLink).NavigateUrl = _
                  "~/webforms/showFile.aspx?link=" + Replace(DataBinder.Eval(e.Row.DataItem, "LinkToInvoice").ToString, "~", "")
      Else
        DirectCast(e.Row.FindControl("ImageButtonPdf"), HyperLink).ImageUrl = "~/Images/pdf_bw.bmp"
      End If
    End If

    ' It fixes column width problem
    If e.Row.RowType = DataControlRowType.DataRow Then
      Dim Label1 As Label = DirectCast(e.Row.FindControl("Label2"), Label)

      If Label1 IsNot Nothing Then
        Label1.Text = Label1.Text.Replace(",", "," + " ")
      End If
    End If

    ' it provide hyperlinkURL for POsub
    If DirectCast(e.Row.FindControl("HyperLinkSubPo"), HyperLink) IsNot Nothing Then
      Dim QueryStrings As String = "?Po_No=" + Mid(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString, 1, 11)
      If User.IsInRole("EnterPurchaseOrder") Then
        DirectCast(e.Row.FindControl("HyperLinkSubPo"), HyperLink).Attributes.Add("onclick", "javascript:w= window.open('POsub.aspx" + QueryStrings + "','POsub','left=100,top=100,width=750,height=600,toolbar=0,resizable=0,scrollbars=yes');")
      Else

      End If
      ' highlight row Color if it is sub PO
      If InStr(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString, "/") > 0 Then
                e.Row.BackColor = System.Drawing.Color.Yellow
            End If

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If IsNotApproved(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString) Then
                e.Row.BackColor = System.Drawing.Color.Black
                e.Row.ForeColor = System.Drawing.Color.White
                e.Row.Enabled = False
                e.Row.Cells(10).Text = "NOT APPROVED PO"
                e.Row.Cells(10).BackColor = System.Drawing.Color.Red
            End If
    End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim LabelSupplierID As Label = DirectCast(e.Row.FindControl("LabelSupplierID"), Label)
            Dim pono As String = ""
            If Request.QueryString("POno") IsNot Nothing Then
                pono = Request.QueryString("POno")

                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                    LabelSupplierID.Text = (From C In db.Table2_PONo Where C.PO_No = pono).ToList()(0).SupplierID

                End Using

            End If


        End If

  End Sub

  Protected Function IsNotApproved(ByVal POno As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT CASE WHEN Approved IS NULL THEN 1 ELSE  Approved END AS Approved FROM Table2_POno WHERE PO_No = @PO_No "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim poNo_ As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar, 15)
            poNo_.Value = POno
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim returnValue As Boolean
            While dr.Read
                If dr(0) = False Then
                    returnValue = True
                Else
                    returnValue = False
                End If
            End While
            Return returnValue
            con.Close()
            dr.Close()
            con.Dispose()

        End Using
    End Function

  Protected Sub SqlDataSourceMonitor_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceMonitor.Selected
  End Sub

  Protected Sub SqlDataSourceMonitor_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceMonitor.Selecting

  End Sub
End Class
