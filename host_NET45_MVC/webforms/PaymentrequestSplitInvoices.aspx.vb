Imports System.IO

Partial Class PaymentrequestSplitInvoices
    Inherits System.Web.UI.Page

    Dim InvoiceIdAsQueryString As Integer = 0

    Dim _invoiceid As Integer = 0
    Dim _payreqno As Integer = 0
    Dim _paid As Boolean = False
    Dim _invoiceno As String = ""
    Dim _payreqdate As String = ""
    Dim _siterecordno As String = ""
    Dim _invValueExcVAT As Decimal = 0
    Dim _currency As String = ""
    Dim _vat As Decimal = 0
    Dim _vatFree As Boolean = False
    Dim _supplierName As String = ""
    Dim _invoiceDate As String = ""
    Dim _poNo As String = ""
    Dim _ProjectName As String = ""
    Dim _CostCode As String = ""
    Dim _Description As String = ""
    Dim _PM_SignaturesLink As String = ""
    Dim _WhenApproved As String = ""
    Dim _vatCoeff As Decimal = 1
    Dim _invoiceIdArray(2) As Integer
    Dim _destinationFileArray(2) As String
    Dim _invoice_type As Integer = 0
    Dim _invoice_note As String = ""


    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

        If Page.Request.QueryString("invoiceid") IsNot Nothing AndAlso IsNumeric(Page.Request.QueryString("invoiceid")) Then
            InvoiceIdAsQueryString = Page.Request.QueryString("invoiceid")
        End If

        _invoiceid = InvoiceIdAsQueryString

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            _payreqno = PTS_MERCURY.helper.Table4_PaymentRequest.GetRowByPrimaryKeyByInvoiceId(_invoiceid).PayReqNo.ToString().Trim()
            _payreqdate = PTS_MERCURY.helper.Table4_PaymentRequest.GetRowByPrimaryKeyByInvoiceId(_invoiceid).PayReqDate
            _siterecordno = PTS_MERCURY.helper.Table4_PaymentRequest.GetRowByPrimaryKeyByInvoiceId(_invoiceid).SiteRecordNo.Trim()
        Catch ex As Exception

        End Try

        Try
            If PTS_MERCURY.helper.Table5_PayLog.GetRowByPrimaryKey(_payreqno).PayReqNo > 0 Then
                _paid = True
            End If
        Catch ex As Exception

        End Try

        Try
            _invoiceno = PTS_MERCURY.helper.Table3_Invoice.GetRowByPrimaryKey(_invoiceid).Invoice_No.Trim()

            Try
                _invoice_type = PTS_MERCURY.helper.Table3_Invoice_Type_Junction.GetRowByPrimaryKey(_invoiceid).Type_id
            Catch ex As Exception

            End Try

            Try
                _invoice_note = PTS_MERCURY.helper.Table3_Invoice.GetRowByPrimaryKey(_invoiceid).Notes.Trim()
            Catch ex As Exception

            End Try

            _invValueExcVAT = PTS_MERCURY.helper.Table3_Invoice.GetRowByPrimaryKey(_invoiceid).InvoiceValue
            _invoiceDate = PTS_MERCURY.helper.Table3_Invoice.GetRowByPrimaryKey(_invoiceid).Invoice_Date
            _poNo = PTS_MERCURY.helper.Table3_Invoice.GetRowByPrimaryKey(_invoiceid).PO_No
            Dim _projectid As Integer = PTS_MERCURY.helper.Table2_PONo.GetRowByPrimaryKey(_poNo).Project_ID
            _ProjectName = PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(_projectid).ProjectName.Trim()
            Dim _l As String = PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(_projectid).PM_SignaturesLink.Trim()
            _PM_SignaturesLink = "file://C:\host_NET45" + _l.Replace("~", "").Replace("/", "\")
            Dim _c As String = PTS_MERCURY.helper.Table2_PONo.GetRowByPrimaryKey(_poNo).CostCode.Trim()
            Dim _cd As String = PTS_MERCURY.helper.Table7_CostCode.GetRowByPrimaryKey(_c).CodeDescription.Trim()
            _CostCode = _c + " - " + _cd
            _Description = PTS_MERCURY.helper.Table2_PONo.GetRowByPrimaryKey(_poNo).Description.Trim() + " / Splitted invoice"
            _currency = PTS_MERCURY.helper.Table2_PONo.GetRowByPrimaryKey(_poNo).PO_Currency
            _vat = PTS_MERCURY.helper.Table2_PONo.GetRowByPrimaryKey(_poNo).VATpercent
            Dim _supplierid As String = PTS_MERCURY.helper.Table2_PONo.GetRowByPrimaryKey(_poNo).SupplierID
            _vatFree = PTS_MERCURY.helper.Table6_Supplier.GetRowByPrimaryKey(_supplierid).VAT_Free
            _supplierName = PTS_MERCURY.helper.Table6_Supplier.GetRowByPrimaryKey(_supplierid).SupplierName.Trim()

            If _vatFree Then
                _vatCoeff = 1
            ElseIf Not _vatFree Then
                _vatCoeff = (100 + _vat) / 100
            End If

        Catch ex As Exception

        End Try

        LiteralInvoiceId.Text = _invoiceid.ToString()
        LiteralPayReqNo.Text = _payreqno.ToString()
        LiteralPaid.Text = If(_paid = True, "paid", "not paid")
        LiteralInvoiceNo.Text = _invoiceno
        LiteralPayReqDate.Text = _payreqdate
        LiteralSiteRecordNo.Text = _siterecordno
        LiteralInvoiceValueExcVAT.Text = _invValueExcVAT.ToString()
        LiteralCurrency.Text = _currency
        LiteralVATPercent.Text = _vat
        LiteralVATFree.Text = _vatFree.ToString()

        ' decide if invoice needs to be uploaded

        Dim path As String = Server.MapPath(PTS_MERCURY.helper.Table3_Invoice_PDF.GetPDFlink(_invoiceid))
        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
        If file.Exists Then
            HiddenInvoiceLink.Value = PTS_MERCURY.helper.Table3_Invoice_PDF.GetPDFlink(_invoiceid)

            'fileupload and related validations to be invisible
            FileUploadInvoice.Visible = False
            LabelInvoiceUploadInfo.Visible = False
            LinkButtonInvoiceUpload.Visible = False
        Else
            'fileupload and related validations to be invisible
            FileUploadInvoice.Visible = True
            LabelInvoiceUploadInfo.Visible = True
            LinkButtonInvoiceUpload.Visible = True

        End If

    End Sub

    Protected Sub DDLsplitnumber_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim _ddl As DropDownList = sender

        If IsNumeric(_ddl.SelectedValue) Then

            Dim _loop As Integer = _ddl.SelectedValue

            TextBox1.Visible = False
            TextBox2.Visible = False
            TextBox3.Visible = False

            RegularExpressionValidator1.Enabled = False
            RegularExpressionValidator2.Enabled = False
            RegularExpressionValidator3.Enabled = False

            RequiredFieldValidator1.Enabled = False
            RequiredFieldValidator2.Enabled = False
            RequiredFieldValidator3.Enabled = False

            If _ddl.SelectedValue > 0 Then

                If _loop = 2 Then
                    TextBox1.Visible = True
                    TextBox2.Visible = True
                    TextBox3.Visible = False

                    RegularExpressionValidator1.Enabled = True
                    RegularExpressionValidator2.Enabled = True
                    RegularExpressionValidator3.Enabled = False

                    RequiredFieldValidator1.Enabled = True
                    RequiredFieldValidator2.Enabled = True
                    RequiredFieldValidator3.Enabled = False
                ElseIf _loop = 3 Then
                    TextBox1.Visible = True
                    TextBox2.Visible = True
                    TextBox3.Visible = True

                    RegularExpressionValidator1.Enabled = True
                    RegularExpressionValidator2.Enabled = True
                    RegularExpressionValidator3.Enabled = True

                    RequiredFieldValidator1.Enabled = True
                    RequiredFieldValidator2.Enabled = True
                    RequiredFieldValidator3.Enabled = True

                End If

            End If
        End If


    End Sub

    Protected Sub ButtonSubmit_Click(sender As Object, e As EventArgs)

        If HiddenInvoiceLink.Value.ToString().Length() = 0 Then
            LabelInfo.Text = " ATTACHMENT MISSING"
            Exit Sub
        End If

        If IsNumeric(DDLsplitnumber.SelectedValue) Then

            Dim _totalSplit As Integer = DDLsplitnumber.SelectedValue
            If _totalSplit > 0 Then

                ' validation goes here
                Dim _totalinvoice As Decimal = 0

                If _totalSplit = 2 Then
                    _totalinvoice = CDec(TextBox1.Text) + CDec(TextBox2.Text)
                ElseIf _totalSplit = 3 Then
                    _totalinvoice = CDec(TextBox1.Text) + CDec(TextBox2.Text) + CDec(TextBox3.Text)
                End If

                LiteralTotalSplitted.Text = _totalinvoice.ToString()

                If _totalinvoice > _invValueExcVAT Then
                    LabelInfo.Text = " Total split amount is bigger than invoice value"
                    Exit Sub
                ElseIf _totalinvoice < _invValueExcVAT Then
                    LabelInfo.Text = " Total split amount is less than invoice value"
                    Exit Sub
                ElseIf _paid Then
                    LabelInfo.Text = " Paid invoices cannot be processed"
                    Exit Sub
                ElseIf _payreqno = 0 Then
                    LabelInfo.Text = " Payment request doest exist. It is needed to proceed."
                    Exit Sub
                Else
                    LabelInfo.Text = ""

                    ' proceed split
                    For i = 1 To _totalSplit
                        CombinedPDFLink(i)
                        DeletePaymentRequest()
                        DeleteInvoice()
                        InsertInvoice(i)
                        InsertPaymentRequest(i)
                    Next

                    Dim _supplierid As String = PTS_MERCURY.helper.Table2_PONo.GetRowByPrimaryKey(_poNo).SupplierID
                    Dim _projectid As Integer = PTS_MERCURY.helper.Table2_PONo.GetRowByPrimaryKey(_poNo).Project_ID
                    Response.Redirect("~/webforms/editpaymentreq.aspx?ProjectId=" + _projectid.ToString().Trim() + "&SupplierId=" + _supplierid)

                End If

            End If
        End If
    End Sub

    Protected Sub CombinedPDFLink(_splitno As Integer)

        Dim _splitValue As Decimal = 0

        If _splitno = 1 Then
            _splitValue = Convert.ToDecimal(TextBox1.Text)
        ElseIf _splitno = 2 Then
            _splitValue = Convert.ToDecimal(TextBox2.Text)
        ElseIf _splitno = 3 Then
            _splitValue = Convert.ToDecimal(TextBox3.Text)
        End If

        ' uploaded invoice 
        Dim oldFile As String = Server.MapPath(HiddenInvoiceLink.Value)

        ' new address for new files
        Dim newFile1 As String = Server.MapPath("~/REQUEST/_InvoicePDF/" + Guid.NewGuid().ToString().GetHashCode().ToString("x") + Guid.NewGuid().ToString().GetHashCode().ToString("x") + ".pdf")

        Dim Point1 As New System.Drawing.Point(150, 600)

        ' write text on pdf
        InvoicePDFTables.AddTextToPdf(oldFile, newFile1, "Splitted Invoice! Pay " + String.Format("{0:N2}", _splitValue * _vatCoeff) + " " + _currency.Trim() + " only !!!", Point1)

        ' generate cover page for splits
        Dim _Path1 As String = "REQUEST/_CoverPagePDF/" + Guid.NewGuid().ToString().GetHashCode().ToString("x") + Guid.NewGuid().ToString().GetHashCode().ToString("x") + ".pdf"

        ' Process cover page.
        RenderReport.Render("discPDF", ReportViewerInvoiceCoverPage, "PR_CoverPagesSplitInvoice", _
                            "SupplierName", _supplierName, _
                            "Invoice_No", _invoiceno + " /split#" + _splitno.ToString(), _
                            "Invoice_Date", _invoiceDate, _
                            "PO_No", _poNo, _
                            "ProjectName", _ProjectName, _
                            "CostCode", _CostCode, _
                            _Path1, _
                            "Description", _Description, _
                            "InvoiceValueExcVAT", String.Format("{0:N2}", _splitValue), _
                            "VAT", String.Format("{0:N2}", _splitValue * _vatCoeff - _splitValue), _
                            "InvoiceValue_WithVAT", String.Format("{0:N2}", _splitValue * _vatCoeff), _
                            "PO_Currency", _currency, _
                            "ProjectID", "", _
                            "SiteReqNo", _siterecordno + " /split#" + _splitno.ToString(), _
                            "PM_SignaturesLink", _PM_SignaturesLink, _
                            "WhenApproved", "", _
                            )

        Dim anArrayOfPDFs() As String = {Server.MapPath("~/" + _Path1), newFile1}

        ' Determine destination file name
        Dim DestinationFileName1 As String
        If Directory.Exists(Server.MapPath("~/REQUEST/") + _ProjectName + "/") Then
            DestinationFileName1 = "~/REQUEST/" + _ProjectName + "/" + _invoiceid.ToString + Guid.NewGuid().ToString().GetHashCode().ToString("x") + ".pdf"
        Else
            Directory.CreateDirectory(Server.MapPath("~/REQUEST/") + _ProjectName)
            DestinationFileName1 = "~/REQUEST/" + _ProjectName + "/" + _invoiceid.ToString + Guid.NewGuid().ToString().GetHashCode().ToString("x") + ".pdf"
        End If

        InvoicePDFTables.MergeFiles(Server.MapPath(DestinationFileName1), anArrayOfPDFs)

        _destinationFileArray(_splitno - 1) = DestinationFileName1

    End Sub

    Sub DeletePaymentRequest()

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If (Aggregate C In db.Table4_PaymentRequest Where C.PayReqNo = _payreqno Into Count()) > 0 Then
                db.Table4_PaymentRequest.Remove((From C In db.Table4_PaymentRequest Where C.PayReqNo = _payreqno).ToList()(0))

                db.SaveChanges()
            End If

            db.Dispose()
        End Using

    End Sub

    Sub DeleteInvoice()

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If (Aggregate C In db.Table3_Invoice Where C.InvoiceID = _invoiceid Into Count()) > 0 Then
                db.Table3_Invoice.Remove((From C In db.Table3_Invoice Where C.InvoiceID = _invoiceid).ToList()(0))

                db.SaveChanges()
            End If

            db.Dispose()
        End Using

    End Sub

    Sub InsertInvoice(_splitno As Integer)

        Dim _splitValue As Decimal = 0

        If _splitno = 1 Then
            _splitValue = Convert.ToDecimal(TextBox1.Text)
        ElseIf _splitno = 2 Then
            _splitValue = Convert.ToDecimal(TextBox2.Text)
        ElseIf _splitno = 3 Then
            _splitValue = Convert.ToDecimal(TextBox3.Text)
        End If

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim asdf As New MyCommonTasks

            Dim a As New PTS_MERCURY.db.Table3_Invoice With {.Invoice_No = Concatenate(_invoiceno.Trim(), " /split#" + _splitno.ToString(), 30), _
                                                             .InvoiceValue = _splitValue, _
                                                             .Invoice_Date = Convert.ToDateTime(Mid(_invoiceDate, 7, 4) + "/" + Mid(_invoiceDate, 4, 2) + "/" + Mid(_invoiceDate, 1, 2)), _
                                                             .PO_No = _poNo, _
                                                             .Notes = Concatenate(IIf(_invoice_note.Length() > 0, _invoice_note, ""), " /#" + _splitno.ToString(), 150), _
                                                             .CreatedBy = DateTime.Now, _
                                                             .PersonCreated = Page.User.Identity.Name.ToLower()}

            db.Table3_Invoice.Attach(a)
            db.Table3_Invoice.Add(a)
            db.SaveChanges()

            _invoiceIdArray(_splitno - 1) = a.InvoiceID

            db.Dispose()

            ' process invoice type
            If _invoice_type > 0 Then
                PTS_MERCURY.helper.Table3_Invoice_Type_Junction.Insert(_invoiceIdArray(_splitno - 1), _invoice_type)
            End If

        End Using

    End Sub

    Sub InsertPaymentRequest(_splitno As Integer)

        Dim _payreqdateDateFormat As DateTime = (Mid(_payreqdate, 7, 4) + "/" + Mid(_payreqdate, 4, 2) + "/" + Mid(_payreqdate, 1, 2))

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim a As New PTS_MERCURY.db.Table4_PaymentRequest With {.SiteRecordNo = Concatenate(_siterecordno, " /#" + _splitno.ToString(), 12), _
                                                                    .InvoiceID = _invoiceIdArray(_splitno - 1), _
                                                                    .PayReqDate = _payreqdateDateFormat, _
                                                                    .LinkToInvoice = _destinationFileArray(_splitno - 1), _
                                                                    .Approved = True, _
                                                                    .PersonApprove = "PTS Split", _
                                                                    .CreatedBy = DateTime.Now, _
                                                                    .PersonCreated = Page.User.Identity.Name.ToLower(), _
                                                                    .AttachmentChange = False, _
                                                                    .AttachmentExist = True}

            db.Table4_PaymentRequest.Attach(a)
            db.Table4_PaymentRequest.Add(a)
            Try
                db.SaveChanges()
            Catch ex As Exception
                '
            End Try

            db.Dispose()


        End Using

    End Sub

    Protected Sub LinkButtonInvoiceUpload_Click(sender As Object, e As EventArgs)

        If FileUploadInvoice.HasFile Then

            If Path.GetExtension(FileUploadInvoice.PostedFile.FileName) <> ".pdf" Then
                LabelInvoiceUploadInfo.Visible = True
                LabelInvoiceUploadInfo.Text = "Please select PDF format file"
                LabelInvoiceUploadInfo.CssClass = "label label-danger inline"
                Exit Sub
            End If

            Dim _linkadress As String = "~/REQUEST/_InvoicePDF/"
            Dim _directory As String = Server.MapPath(_linkadress)

            If Not Directory.Exists(_directory) Then
                Directory.CreateDirectory(_directory)
            End If

            Dim _link As String = _linkadress + Guid.NewGuid().ToString().GetHashCode().ToString("x") + Guid.NewGuid().ToString().GetHashCode().ToString("x") + _
                Path.GetExtension(FileUploadInvoice.PostedFile.FileName)

            FileUploadInvoice.SaveAs(MapPath(_link))

            HiddenInvoiceLink.Value = _link

            LabelInvoiceUploadInfo.Visible = True
            LabelInvoiceUploadInfo.Text = FileUploadInvoice.PostedFile.FileName + " loaded..."
            LabelInvoiceUploadInfo.CssClass = "label label-success inline"

        Else

            LabelInvoiceUploadInfo.Visible = True
            LabelInvoiceUploadInfo.Text = "You didnt specify any file"
            LabelInvoiceUploadInfo.CssClass = "label label-danger inline"

        End If

    End Sub

    Protected Function Concatenate(text1 As String, text2 As String, allowed_length As Integer) As String


        Dim _return As String = ""

        Dim _newLength As Integer = text1.Trim().Length + text2.Trim().Length

        If _newLength > allowed_length Then

            _return = Mid(text1, 1, text1.Trim().Length - (_newLength - allowed_length + 2)) + "..." + text2

        Else

            _return = text1 + text2

        End If

        Return _return

    End Function



End Class
