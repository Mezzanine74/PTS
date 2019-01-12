Imports System.IO
Imports System.Data.SqlClient
Imports System.Data

Partial Class SearchContractsFeed
    Inherits System.Web.UI.Page

    Protected Sub BtnGetData_Click(sender As Object, e As EventArgs) Handles BtnGetData.Click

        Using _adapter As New MercuryTableAdapters.Table_Contract_BrkdwnTableAdapter

            If _adapter.GetDataByContractID(TxContractID.Text.Trim) = 0 Then
                ' insert
                _adapter.Insert(TxContractID.Text.Trim, HTMLbrkDwn.Content.ToString, EditorHTMLcleanText.Text.ToString)
            ElseIf _adapter.GetDataByContractID(TxContractID.Text.Trim) = 1 Then
                ' update
                _adapter.Update(HTMLbrkDwn.Content.ToString, EditorHTMLcleanText.Text.ToString, TxContractID.Text)
            End If

            Dim _table As New Mercury.Table_Contract_BrkdwnDataTable
            Dim _row As Mercury.Table_Contract_BrkdwnRow

            _table = _adapter.GetHTMLByContractID(TxContractID.Text.Trim)

            For Each _row In _table
                LiteralbrkDwn.Text = _row.Contract_Brkdwn
                LiteralEditorHTMLcleanText.Text = _row.Contract_CleanText
            Next

            _table.Dispose()

            HTMLbrkDwn.Content = String.Empty
            EditorHTMLcleanText.Text = String.Empty
            _adapter.Dispose()
        End Using

        ' executing Supplier Details
        If PanelSupplierDetails.Visible = True Then
            Using _adapter As New MercuryTableAdapters.Table6_SupplierDetailsTableAdapter
                If _adapter.GetCountBySupplierID(TxSupplierID.Text.Trim) = 0 Then
                    ' insert
                    _adapter.Insert(TxSupplierID.Text.Trim, HTMLSupplierAdressDetailsENG.Text, HTMLSupplierAdressDetailsRUS.Text, HTMLSupplierBankingDetailsENG.Text, HTMLSupplierBankingDetailsRUS.Text)
                Else
                    ' update
                    _adapter.Update(HTMLSupplierAdressDetailsENG.Text, HTMLSupplierAdressDetailsRUS.Text, HTMLSupplierBankingDetailsENG.Text, HTMLSupplierBankingDetailsRUS.Text, TxSupplierID.Text.Trim)
                End If

                Dim _table As New Mercury.Table6_SupplierDetailsDataTable
                Dim _row As Mercury.Table6_SupplierDetailsRow

                _table = _adapter.GetDataBySupplierID(TxSupplierID.Text.Trim)

                For Each _row In _table
                    LiteralSupplierAdressDetailsENG.Text = _row.SupplierAdressDetails_ENG
                    LiteralSupplierAdressDetailsRUS.Text = _row.SupplierAdressDetails_RUS
                    LiteralSupplierBankingDetailsENG.Text = _row.SupplierBankDetails_ENG
                    LiteralSupplierBankingDetailsRUS.Text = _row.SupplierBankDetails_RUS
                Next

                _table.Dispose()

                HTMLSupplierAdressDetailsENG.Text = String.Empty
                HTMLSupplierAdressDetailsRUS.Text = String.Empty
                LiteralSupplierID.Text = TxSupplierID.Text.Trim
                HTMLSupplierBankingDetailsENG.Text = String.Empty
                HTMLSupplierBankingDetailsRUS.Text = String.Empty

                _adapter.Dispose()

                TxSupplierID.Text = String.Empty

            End Using
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Request.QueryString("ContractID") IsNot Nothing Then

                ' transfer ContractID to TxtBx
                TxContractID.Text = Request.QueryString("ContractID").ToString

                Dim SupplierID As String = ""
                SupplierID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(Request.QueryString("ContractID")).SupplierID

                ' show panel details or hide
                Using adapter As New MercuryTableAdapters.QueriesTableAdapterCommon
                    If adapter.GetCountSupplierDetailsBySupplierID(SupplierID) = 0 Then
                        PanelSupplierDetails.Visible = True
                        TxSupplierID.Text = SupplierID
                    Else
                        PanelSupplierDetails.Visible = False
                        TxSupplierID.Text = String.Empty
                    End If
                    adapter.Dispose()
                End Using

            Else

                Page.Visible = False
                For i = 1 To 20
                    Response.Write("PLEASE PROVIDE CONTRACTID QUERY STRING <br/>")
                Next

            End If
        End If

    End Sub
End Class
