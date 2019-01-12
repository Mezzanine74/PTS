Imports System.IO
Imports System.Data.SqlClient
Imports System.Data

Partial Class SearchContractsFeed2
    Inherits System.Web.UI.Page

    Protected Sub BtnGetData_Click(sender As Object, e As EventArgs) Handles BtnGetData.Click

        Using _adapter As New MercuryTableAdapters.Table_Contract_BrkdwnTableAdapter
            _adapter.UpdateContractCleanText(EditorHTMLcleanText.Text.ToString, TxContractID.Text.Trim)
            _adapter.Dispose()
            EditorHTMLcleanText.Text = String.Empty
            TxContractID.Focus()
        End Using

        'If _adapter.GetDataByContractID(TxContractID.Text.Trim) = 0 Then
        '    ' insert
        '    _adapter.Insert(TxContractID.Text.Trim, HTMLDescENG.Content.ToString, HTMLDescRUS.Content.ToString, HTMLbrkDwn.Content.ToString)
        'ElseIf _adapter.GetDataByContractID(TxContractID.Text.Trim) = 1 Then
        '    ' update
        '    _adapter.Update(HTMLDescENG.Content.ToString, HTMLDescRUS.Content.ToString, HTMLbrkDwn.Content.ToString, TxContractID.Text)
        'End If


        'Dim _table As New Mercury.Table_Contract_BrkdwnDataTable
        'Dim _row As Mercury.Table_Contract_BrkdwnRow

        '_table = _adapter.GetHTMLByContractID(TxContractID.Text.Trim)

        'For Each _row In _table
        '    LiteralDescENG.Text = _row.Contract_DescriptionENG
        '    LiteralDescRUS.Text = _row.Contract_DescriptionRUS
        '    LiteralbrkDwn.Text = _row.Contract_Brkdwn
        'Next


        '_table.Dispose()

        'HTMLDescENG.Content = "N/A"
        'HTMLDescRUS.Content = "N/A"
        'HTMLbrkDwn.Content = String.Empty

    End Sub

End Class
