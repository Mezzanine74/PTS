Partial Class ContractBrkdwn
    Inherits System.Web.UI.Page

    Dim FirstParameterNameOfDescription As String = ""
    Dim StartPosition As Integer
    Dim EndPosition As Integer
    Dim OccurenceOfSpace As Integer
    Dim sayac As Integer = 0
    Dim DonguBiter As Boolean = False

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Request.QueryString("ContractID") Is Nothing Then
            Page.Visible = False
            For i = 1 To 20
                Response.Write("ContractID should be provided as Query String! <br/>")
            Next
            Exit Sub
        End If

        Using Adapter As New MercuryTableAdapters.Table6_SupplierDetailsTableAdapter
            If Adapter.GetCountBySupplierID(PTS.CoreTables.CreateDataReader.Create_Table_Contract(Request.QueryString("ContractID")).SupplierID) > 0 Then
                LinkButtonSupplierDetails.Visible = True
            Else
                LinkButtonSupplierDetails.Text = "(Supplier details not available)"
                LinkButtonSupplierDetails.Font.Italic = True
                LinkButtonSupplierDetails.Enabled = False
            End If
        End Using

        Using _adapter As New MercuryTableAdapters.Table_Contract_BrkdwnTableAdapter
            Dim _table As New Mercury.Table_Contract_BrkdwnDataTable
            Dim _row As Mercury.Table_Contract_BrkdwnRow

            _table = _adapter.GetHTMLByContractID(Request.QueryString("ContractID").ToString)

            For Each _row In _table
                LiteralHTML.Text = _row.Contract_Brkdwn
            Next

            _adapter.Dispose()
            _table.Dispose()

        End Using

        Using _adapter2 As New MercuryTableAdapters.DataTableSerachContractTableAdapter
            Dim _table2 As New Mercury.DataTableSerachContractDataTable
            Dim _row2 As Mercury.DataTableSerachContractRow

            _table2 = _adapter2.GetDataContractID(Request.QueryString("ContractID").ToString)

            For Each _row2 In _table2
                LtrlProjectName.Text = _row2.ProjectName
                LiteralSupplierName.Text = _row2.SupplierName
                LiteralDescription.Text = _row2.ContractDescription
                LiteralContractValue.Text = _row2.ContractValue_withVAT
                LiteralCurrency.Text = _row2.ContractCurrency
            Next

            _adapter2.Dispose()
            _table2.Dispose()

        End Using

        HypLkContractDetails.NavigateUrl = "~/webforms/ContractDetails.aspx?ContractID=" + Request.QueryString("ContractID").ToString

        If Session("SearchParameter") IsNot Nothing Then
            LiteralSearchCriteria.Text = Session("SearchParameter").ToString + "<br/>"

            LiteralHTML.Text = SearchContract.HighlightSearchParameters(Session("SearchParameter").ToString, LiteralHTML.Text)

        End If

    End Sub

    Protected Sub ImageButtonExcel_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButtonExcel.Click

        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.ContentEncoding = System.Text.Encoding.Unicode
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())
        Response.Charset = ""
        Response.Write(LiteralHTML.Text)
        Response.[End]()

    End Sub

    Protected Sub LinkButtonSupplierDetails_Click(sender As Object, e As EventArgs) Handles LinkButtonSupplierDetails.Click

        Using _adapter As New MercuryTableAdapters.Table6_SupplierDetailsTableAdapter

            Dim _table As New Mercury.Table6_SupplierDetailsDataTable
            Dim _row As Mercury.Table6_SupplierDetailsRow

            _table = _adapter.GetDataBySupplierID(PTS.CoreTables.CreateDataReader.Create_Table_Contract(Request.QueryString("ContractID")).SupplierID)

            For Each _row In _table
                LiteralHTMLSupplierAdressDetailsENG.Text = _row.SupplierAdressDetails_ENG
                LiteralHTMLSupplierAdressDetailsRUS.Text = _row.SupplierAdressDetails_RUS
                LiteralHTMLSupplierBankingDetailsENG.Text = _row.SupplierBankDetails_ENG
                LiteralHTMLSupplierBankingDetailsRUS.Text = _row.SupplierBankDetails_RUS
            Next

            LiteralHeadSupplierDetails.Text = LiteralSupplierName.Text

            _table.Dispose()

            _adapter.Dispose()
        End Using


        ModalPopupExtenderSupplierDetails.Show()

    End Sub
End Class
