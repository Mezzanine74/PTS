Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class OpenPo

    Shared Function GetOpen(ByVal _po_no As String) As Decimal

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     dbo.View_PoSumExcVAT.PoSumExcVAT - (CASE WHEN TotalInvoice IS NULL THEN 0 ELSE TotalInvoice END) AS Open_PO " + _
" FROM         (SELECT     PO_No, SUM(InvoiceValue) AS TotalInvoice " + _
"                        FROM          dbo.Table3_Invoice " + _
"                        GROUP BY PO_No) AS TotalInvoice RIGHT OUTER JOIN " + _
"                       dbo.View_PoSumExcVAT ON TotalInvoice.PO_No = dbo.View_PoSumExcVAT.PO_No " + _
" WHERE     (dbo.View_PoSumExcVAT.PO_No = @PO_No) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim UserParm As SqlParameter = cmd.Parameters.Add("@PO_No", Data.SqlDbType.NVarChar)
            UserParm.Value = _po_no
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                Return dr(0)
            End While

            con.Close()
            dr.Close()
        End Using

    End Function

End Class
