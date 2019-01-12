Imports Microsoft.VisualBasic

Public Class EnterPaymentsFrom1S

    Shared Sub Insert(ByVal _FinanceNo As String, _
                      ByVal _paymentDate As DateTime, _
                      ByVal _Sort As Integer, _
                      ByVal _info As String, _
                      ByVal _supplier As String, _
                      ByVal _payment As Decimal)

        Dim adapter As New MercuryTableAdapters.Table_Payments_1STableAdapter

        adapter.Insert(_FinanceNo, _paymentDate, _Sort, _info, _supplier, _payment)
        adapter.Dispose()

    End Sub

    Shared Sub Delete(ByVal _paymentDate As DateTime)

        Dim adapter As New MercuryTableAdapters.Table_Payments_1STableAdapter

        adapter.DeleteQuery(_paymentDate)
        adapter.Dispose()

    End Sub

End Class
