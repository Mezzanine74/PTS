Imports Microsoft.VisualBasic

Public Class Table2_PONo

    Shared Function GetDataTable2_PONo_row(ByVal _PO_No As String) As Mercury.Table2_PONoRow

        Dim Table2_PONoTableAdapter As New MercuryTableAdapters.Table2_PONoTableAdapter
        Dim Table2_PONoDataTable As Mercury.Table2_PONoDataTable
        Dim _return As Mercury.Table2_PONoRow

        Table2_PONoDataTable = Table2_PONoTableAdapter.GetDataByPoNo(_PO_No)

        For Each _return In Table2_PONoDataTable
            ' single value returns
            Return _return
            _return = Nothing
        Next

        Table2_PONoTableAdapter = Nothing
        Table2_PONoDataTable = Nothing

    End Function

End Class
