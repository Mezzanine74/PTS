Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class ValidateInvoiceValueAgainstContractAdvance

    Shared Function GetAdvancePaymentValue(ByVal _PO_No As String) As Mercury.ValidateInvoiceValueAgainstContractAdvanceRow

        Using _adapter As New MercuryTableAdapters.ValidateInvoiceValueAgainstContractAdvanceTableAdapter
            Dim _table As Mercury.ValidateInvoiceValueAgainstContractAdvanceDataTable
            Dim _return As Mercury.ValidateInvoiceValueAgainstContractAdvanceRow

            _table = _adapter.GetData(_PO_No)

            For Each _return In _table
                ' single value returns
                Return _return
                _return = Nothing
            Next

            _adapter.Dispose()

        End Using

    End Function

    Shared Function GetAdvancePaymentValueFromAddendum(ByVal _PO_No As String) As Decimal

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "ValidateInvoiceValueAgainstAddendumAdvance"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim poNo As SqlParameter = cmd.Parameters.Add("@PO_No", Data.SqlDbType.NVarChar, 11)
            poNo.Value = _PO_No
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                Return dr(0)
            End While

            con.Close()
            dr.Close()
        End Using

    End Function

End Class
