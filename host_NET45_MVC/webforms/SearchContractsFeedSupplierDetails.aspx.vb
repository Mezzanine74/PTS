
Partial Class SearchContractsFeedSupplierDetails
    Inherits System.Web.UI.Page

    Protected Sub BtnGetData_Click(sender As Object, e As EventArgs) Handles BtnGetData.Click

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

        End Using

        TxSupplierID.Text = String.Empty
        TxSupplierID.Focus()

    End Sub
End Class
