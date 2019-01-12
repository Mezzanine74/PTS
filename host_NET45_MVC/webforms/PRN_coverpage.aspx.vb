Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.Net

Partial Class PRN_coverpage3
    Inherits System.Web.UI.Page

    Dim _ProjectID As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

                con.Open()
                Dim sqlstring As String = " SELECT     RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, RTRIM(dbo.Table3_Invoice.Invoice_No) AS Invoice_No,  " + _
          "                       dbo.Table3_Invoice.Invoice_Date, RTRIM(dbo.Table2_PONo.PO_No) AS PO_No, RTRIM(CONVERT(nVarChar(50), dbo.Table1_Project.ProjectID))  " + _
          "                       + N'-' + RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, RTRIM(dbo.Table7_CostCode.CostCode)  " + _
          "                       + N'-' + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR " + _
          "                       dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description)  " + _
          "                       + ' / ' + RTRIM(dbo.Table3_Invoice.Notes) END AS Description, dbo.Table3_Invoice.InvoiceValue AS InvoiceValueExcVAT,  " + _
          "                       (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) " + _
          "                        / 100) END) - dbo.Table3_Invoice.InvoiceValue AS VAT,  " + _
          "                       (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue ELSE dbo.Table3_Invoice.InvoiceValue * ((100 + dbo.Table2_PONo.VATpercent) " + _
          "                        / 100) END) AS InvoiceValue_WithVAT, RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency, dbo.Table1_Project.ProjectID AS ProjectID " + _
          " FROM         dbo.Table6_Supplier INNER JOIN " + _
          "                       dbo.Table2_PONo ON dbo.Table6_Supplier.SupplierID = dbo.Table2_PONo.SupplierID INNER JOIN " + _
          "                       dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN " + _
          "                       dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " + _
          "                       dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode " + _
          " WHERE     (dbo.Table3_Invoice.InvoiceID = @InvoiceID) "

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                Dim InvoiceID As SqlParameter = cmd.Parameters.Add("@InvoiceID", System.Data.SqlDbType.Int)
                InvoiceID.Value = Convert.ToInt32(Request.QueryString("InvoiceID"))

                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    LabelCompany.Text = dr("SupplierName").ToString
                    LabelInvoiceNo.Text = dr("Invoice_No").ToString
                    LabelInvoiceDate.Text = String.Format("{0:dd/MM/yyyy}", dr("Invoice_Date"))
                    LabelPRN.Text = Request.QueryString("PRN")
                    LabelPO_No.Text = dr("PO_No").ToString
                    LabelProject.Text = dr("ProjectName").ToString
                    LabelCostCode.Text = dr("CostCode").ToString
                    LabelDescription.Text = dr("Description").ToString
                    LabelAmountExcVAT.Text = String.Format("{0:#,##0.00}", dr("InvoiceValueExcVAT"))
                    LabelVAT.Text = String.Format("{0:#,##0.00}", dr("VAT"))
                    LabelAmountWithVAT.Text = String.Format("{0:#,##0.00}", dr("InvoiceValue_WithVAT"))
                    LabelCurrency1.Text = dr("PO_Currency").ToString
                    LabelCurrency2.Text = dr("PO_Currency").ToString
                    LabelCurrency3.Text = dr("PO_Currency").ToString
                    _ProjectID = dr("ProjectID")
                End While

                dr.Close()
                con.Close()
                con.Dispose()

            End Using

        End If

        ' Provide Signature and TimeStamp if approved
        Using Adapter As New MercuryTableAdapters.Table3_Invoice_PRrequestToPMTableAdapter
            Dim table As New Mercury.Table3_Invoice_PRrequestToPMDataTable
            table = Adapter.GetDataByInvoiceID(Request.QueryString("InvoiceID"))
            For Each _row As Mercury.Table3_Invoice_PRrequestToPMRow In table
                If _row.ApprovedOrNot = True Then
                    ImagePMsignature.Visible = True
                    ImagePMsignature.ImageUrl = PTS.CoreTables.CreateDataReader.Create_Table1_Project(_ProjectID).PM_SignaturesLink
                    LabelSignatureTimeStamp.Visible = True
                    LabelSignatureTimeStamp.Text = "Electronically approved at " + _row.WhenApproved
                Else
                    ImagePMsignature.Visible = False
                    LabelSignatureTimeStamp.Visible = False
                End If
            Next
            table.Dispose()
            Adapter.Dispose()
        End Using


        'Dim path As String = Server.MapPath("SiteAnalytics.pdf")

        'Dim client As New WebClient()

        'Dim buffer As [Byte]() = client.DownloadData(path)




        'If buffer IsNot Nothing Then

        '    Response.ContentType = "application/pdf"

        '    Response.AddHeader("content-length", buffer.Length.ToString())

        '    Response.BinaryWrite(buffer)

        'End If

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Response.ContentType = "application/pdf"

        Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf")

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Dim sw As New StringWriter()

        Dim hw As New HtmlTextWriter(sw)

        Me.Page.RenderControl(hw)

        Dim sr As New StringReader(sw.ToString())

        Dim pdfDoc As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)

        Dim htmlparser As New HTMLWorker(pdfDoc)

        PdfWriter.GetInstance(pdfDoc, Response.OutputStream)

        pdfDoc.Open()

        htmlparser.Parse(sr)

        pdfDoc.Close()

        Response.Write(pdfDoc)

        Response.[End]()
    End Sub

End Class
