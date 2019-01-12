Imports System.Data.OleDb
Imports System.Data
Imports System.Data.SqlClient

Partial Class EnterAllInOneCreateRegularPO
    Inherits System.Web.UI.Page

    Protected Sub ButtonEnter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEnter.Click

        For Each row As GridViewRow In GridViewFinance.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim POprj As String = ""
                Dim POsortNo As String = ""
                Dim PONo As String = ""

                Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    con.Open()
                    Dim sqlstring As String = "GetPoNoFromMakerNewGeneration"
                    Dim cmd As New SqlCommand(sqlstring, con)
                    cmd.CommandType = System.Data.CommandType.StoredProcedure

                    'syntax for parameter adding
                    Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.SmallInt)
                    ProjectID.Value = row.Cells(0).Text
                    Dim dr As SqlDataReader = cmd.ExecuteReader
                    While dr.Read
                        PONo = dr(0).ToString
                    End While
                    con.Close()
                    dr.Close()
                End Using

                Dim InvoiceID As Integer = 0
                Dim PayReqNo As Integer = 0

                'Instance
                Dim zoneId As String = "Russian Standard Time"
                Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
                Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

                'SqlDataSourcePoNo.SelectParameters("PrjID").DefaultValue = row.Cells(0).Text
                'SqlDataSourcePoNo.SelectParameters("PrjID").Type = TypeCode.Int32
                'DropDownListPoNo.DataBind()

                '' PO first part done
                'If Len(row.Cells(0).Text) = 1 Then
                '  POprj = "00" + row.Cells(0).Text
                'ElseIf Len(row.Cells(0).Text) = 2 Then
                '  POprj = "0" + row.Cells(0).Text
                'ElseIf Len(row.Cells(0).Text) = 3 Then
                '  POprj = row.Cells(0).Text
                'End If

                '' PO second part done
                'If Len(DropDownListPoNo.SelectedItem.Text) = 1 Then
                '  POsortNo = "000" + DropDownListPoNo.SelectedItem.Text
                'ElseIf Len(DropDownListPoNo.SelectedItem.Text) = 2 Then
                '  POsortNo = "00" + DropDownListPoNo.SelectedItem.Text
                'ElseIf Len(DropDownListPoNo.SelectedItem.Text) = 3 Then
                '  POsortNo = "0" + DropDownListPoNo.SelectedItem.Text
                'ElseIf Len(DropDownListPoNo.SelectedItem.Text) = 4 Then
                '  POsortNo = DropDownListPoNo.SelectedItem.Text
                'End If

                '' POnoCombine
                'PONo = "PO-" + POprj + "-" + POsortNo

                ' Assing all parameters for TABLEPO
                SqlDataSourceInsertPO.InsertParameters("PO_No").DefaultValue = PONo
                SqlDataSourceInsertPO.InsertParameters("PO_No").Type = TypeCode.String
                SqlDataSourceInsertPO.InsertParameters("Project_ID").DefaultValue = Convert.ToInt32(row.Cells(0).Text)
                SqlDataSourceInsertPO.InsertParameters("Project_ID").Type = TypeCode.Int32
                SqlDataSourceInsertPO.InsertParameters("SupplierID").DefaultValue = row.Cells(7).Text
                SqlDataSourceInsertPO.InsertParameters("SupplierID").Type = TypeCode.String
                SqlDataSourceInsertPO.InsertParameters("Description").DefaultValue = row.Cells(2).Text
                SqlDataSourceInsertPO.InsertParameters("Description").Type = TypeCode.String
                SqlDataSourceInsertPO.InsertParameters("TotalPrice").DefaultValue = Convert.ToDecimal(row.Cells(4).Text)
                SqlDataSourceInsertPO.InsertParameters("TotalPrice").Type = TypeCode.Decimal
                SqlDataSourceInsertPO.InsertParameters("PO_Currency").DefaultValue = Convert.ToString(row.Cells(8).Text.Trim())
                SqlDataSourceInsertPO.InsertParameters("PO_Currency").Type = TypeCode.String
                SqlDataSourceInsertPO.InsertParameters("VATpercent").DefaultValue = Convert.ToInt32(row.Cells(5).Text)
                SqlDataSourceInsertPO.InsertParameters("VATpercent").Type = TypeCode.Int32
                SqlDataSourceInsertPO.InsertParameters("CostCode").DefaultValue = row.Cells(1).Text
                SqlDataSourceInsertPO.InsertParameters("CostCode").Type = TypeCode.String
                SqlDataSourceInsertPO.InsertParameters("PO_Date").DefaultValue = Convert.ToDateTime(row.Cells(3).Text + " 00:00:00")
                SqlDataSourceInsertPO.InsertParameters("PO_Date").Type = TypeCode.DateTime
                SqlDataSourceInsertPO.InsertParameters("CreatedBy").DefaultValue = result
                SqlDataSourceInsertPO.InsertParameters("CreatedBy").Type = TypeCode.DateTime
                SqlDataSourceInsertPO.InsertParameters("PersonCreated").DefaultValue = "PTS"
                SqlDataSourceInsertPO.InsertParameters("PersonCreated").Type = TypeCode.String

                ' insert into TAPLEPO
                SqlDataSourceInsertPO.Insert()

                ' Increase POsortNo 
                'SqlDataSourceIncreasePOsortNo.InsertParameters("PrjID").DefaultValue = Convert.ToInt32(row.Cells(0).Text)
                'SqlDataSourceIncreasePOsortNo.InsertParameters("PrjID").Type = TypeCode.Int32
                'SqlDataSourceIncreasePOsortNo.InsertParameters("POSortNo").DefaultValue = Convert.ToInt32(DropDownListPoNo.SelectedValue + 1)
                'SqlDataSourceIncreasePOsortNo.InsertParameters("POSortNo").Type = TypeCode.Int32

                'SqlDataSourceIncreasePOsortNo.Insert()

                ' prepare parameters for TABLEINVOICE
                SqlDataSourceInsertInvoice.InsertParameters("Invoice_No").DefaultValue = "PTS"
                SqlDataSourceInsertInvoice.InsertParameters("Invoice_No").Type = TypeCode.String
                SqlDataSourceInsertInvoice.InsertParameters("Invoice_Date").DefaultValue = Convert.ToDateTime(row.Cells(3).Text + " 00:00:00")
                SqlDataSourceInsertInvoice.InsertParameters("Invoice_Date").Type = TypeCode.DateTime
                SqlDataSourceInsertInvoice.InsertParameters("PO_No").DefaultValue = PONo
                SqlDataSourceInsertInvoice.InsertParameters("PO_No").Type = TypeCode.String
                SqlDataSourceInsertInvoice.InsertParameters("InvoiceValue").DefaultValue = Convert.ToDecimal(row.Cells(6).Text)
                SqlDataSourceInsertInvoice.InsertParameters("InvoiceValue").Type = TypeCode.Decimal
                SqlDataSourceInsertInvoice.InsertParameters("CreatedBy").DefaultValue = result
                SqlDataSourceInsertInvoice.InsertParameters("CreatedBy").Type = TypeCode.DateTime
                SqlDataSourceInsertInvoice.InsertParameters("PersonCreated").DefaultValue = "PTS"
                SqlDataSourceInsertInvoice.InsertParameters("PersonCreated").Type = TypeCode.String

                ' insert into table TABLEINVOICE
                SqlDataSourceInsertInvoice.Insert()

                ' take invoiceID for TABLEPAYMENTREQUEST
                SqlDataSourceInvoiceID.SelectParameters("PO_No").DefaultValue = PONo
                SqlDataSourceInvoiceID.SelectParameters("PO_No").Type = TypeCode.String

                DropDownListInvoiceID.DataBind()
                InvoiceID = DropDownListInvoiceID.SelectedValue

                ' prepare parameters for TABLEPAYMENTREQUEST
                SqlDataSourceInsertPaymentRequest.InsertParameters("SiteRecordNo").DefaultValue = "PTS"
                SqlDataSourceInsertPaymentRequest.InsertParameters("SiteRecordNo").Type = TypeCode.String
                SqlDataSourceInsertPaymentRequest.InsertParameters("InvoiceID").DefaultValue = InvoiceID
                SqlDataSourceInsertPaymentRequest.InsertParameters("InvoiceID").Type = TypeCode.Decimal
                SqlDataSourceInsertPaymentRequest.InsertParameters("PayReqDate").DefaultValue = Convert.ToDateTime(row.Cells(3).Text + " 00:00:00")
                SqlDataSourceInsertPaymentRequest.InsertParameters("PayReqDate").Type = TypeCode.DateTime
                SqlDataSourceInsertPaymentRequest.InsertParameters("Approved").DefaultValue = "Approved"
                SqlDataSourceInsertPaymentRequest.InsertParameters("Approved").Type = TypeCode.String
                SqlDataSourceInsertPaymentRequest.InsertParameters("PersonApprove").DefaultValue = "PTS"
                SqlDataSourceInsertPaymentRequest.InsertParameters("PersonApprove").Type = TypeCode.String
                SqlDataSourceInsertPaymentRequest.InsertParameters("LastAction").DefaultValue = result
                SqlDataSourceInsertPaymentRequest.InsertParameters("LastAction").Type = TypeCode.DateTime
                SqlDataSourceInsertPaymentRequest.InsertParameters("CreatedBy").DefaultValue = result
                SqlDataSourceInsertPaymentRequest.InsertParameters("CreatedBy").Type = TypeCode.DateTime
                SqlDataSourceInsertPaymentRequest.InsertParameters("PersonCreated").DefaultValue = "PTS"
                SqlDataSourceInsertPaymentRequest.InsertParameters("PersonCreated").Type = TypeCode.String
                SqlDataSourceInsertPaymentRequest.InsertParameters("AttachmentExist").DefaultValue = False
                SqlDataSourceInsertPaymentRequest.InsertParameters("AttachmentExist").Type = TypeCode.Boolean


                ' insert parameters into tablepaymentrequest
                SqlDataSourceInsertPaymentRequest.Insert()

                ' find the related PayReqNo
                SqlDataSourcePayReqNo.SelectParameters("InvoiceID").DefaultValue = InvoiceID
                SqlDataSourcePayReqNo.SelectParameters("InvoiceID").Type = TypeCode.Int32

                DropDownListPayReqNo.DataBind()
                PayReqNo = DropDownListPayReqNo.SelectedValue

                ' prepare parameters for TABLEPAYLOG
                SqlDataSourcePayLog.InsertParameters("PayReqNo").DefaultValue = PayReqNo
                SqlDataSourcePayLog.InsertParameters("PayReqNo").Type = TypeCode.Int32
                SqlDataSourcePayLog.InsertParameters("FinanceNo").DefaultValue = "PTS"
                SqlDataSourcePayLog.InsertParameters("FinanceNo").Type = TypeCode.String
                SqlDataSourcePayLog.InsertParameters("PaymentDate").DefaultValue = Convert.ToDateTime(row.Cells(3).Text + " 00:00:00")
                SqlDataSourcePayLog.InsertParameters("PaymentDate").Type = TypeCode.DateTime
                SqlDataSourcePayLog.InsertParameters("Amount").DefaultValue = Convert.ToDecimal(row.Cells(4).Text)
                SqlDataSourcePayLog.InsertParameters("Amount").Type = TypeCode.Decimal
                SqlDataSourcePayLog.InsertParameters("Currency").DefaultValue = "Rub"
                SqlDataSourcePayLog.InsertParameters("Currency").Type = TypeCode.String
                SqlDataSourcePayLog.InsertParameters("CreatedBy").DefaultValue = result
                SqlDataSourcePayLog.InsertParameters("CreatedBy").Type = TypeCode.DateTime
                SqlDataSourcePayLog.InsertParameters("PersonCreated").DefaultValue = "PTS"
                SqlDataSourcePayLog.InsertParameters("PersonCreated").Type = TypeCode.String
                SqlDataSourcePayLog.InsertParameters("RubbleDollar").DefaultValue = PTSMainClass.GetDollarFromDate(Convert.ToDateTime(row.Cells(3).Text + " 00:00:00"))
                SqlDataSourcePayLog.InsertParameters("RubbleDollar").Type = TypeCode.Decimal
                SqlDataSourcePayLog.InsertParameters("RubbleEuro").DefaultValue = PTSMainClass.GetEuroFromDate(Convert.ToDateTime(row.Cells(3).Text + " 00:00:00"))
                SqlDataSourcePayLog.InsertParameters("RubbleEuro").Type = TypeCode.Decimal

                ' insert parameters into tablepaylog
                SqlDataSourcePayLog.Insert()

            End If
        Next
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim connString As String = ConfigurationManager.ConnectionStrings("xls").ConnectionString
        '' Create the connection object
        'Dim oledbConn As OleDbConnection = New OleDbConnection(connString)

        '' Open connection
        'oledbConn.Open()

        '' Create OleDbCommand object and select data from worksheet Sheet1
        'Dim cmd As OleDbCommand = New OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn)

        '' Create new OleDbDataAdapter
        'Dim oleda As OleDbDataAdapter = New OleDbDataAdapter()

        'oleda.SelectCommand = cmd

        '' Create a DataSet which will hold the data extracted from the worksheet.
        'Dim ds As DataSet = New DataSet()

        '' Fill the DataSet from the data extracted from the worksheet.
        'oleda.Fill(ds, "Finance")

        '' Bind the data to the GridView
        'GridViewFinance.DataSource = ds.Tables(0).DefaultView
        'GridViewFinance.DataBind()

        '' Close connection
        'oledbConn.Close()

    End Sub

    Protected Sub GridViewFinance_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewFinance.RowDataBound
    End Sub
End Class
