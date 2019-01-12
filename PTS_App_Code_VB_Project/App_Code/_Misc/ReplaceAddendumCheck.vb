Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.VisualBasic

Public Class ReplaceAddendumCheck


    Shared Function ReplaceAddendumTotalInvoiceRelation() As DataTable

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SP_ReplaceAddendumTotalInvoiceRelation"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            ContractID.Value = 2987
            Dim dr As SqlDataReader = cmd.ExecuteReader

            Dim dt As New DataTable()
            dt.Load(dr)
            Return dt

            con.Close()
            dr.Close()
        End Using

    End Function


    ' This class to handle all required methods and functions to achieve REPLACE ADDENDUM check


    ' 1) First Entry
    ' ********************
    ' New PO Policy
    '   Take Contract ID and Check if it is new PO Policy via ProjectID
    '   Get Total Invoice With VAT via ContractID via POno
    '   Return Total Invoice Value
    ' Old PO Policy
    '   Take Contract ID and Check if it is old PO Policy via ProjectID
    '   Get Total Invoice With VAT via ContractID via POno
    '   Return Total Invoice Value


    ' 2) Before PO Update
    ' Entire Replace Addendum should be frozen ?


    ' 3) When Insert Invoice, Pending Replace Addendum to be Checked


    Shared Function GetTotalInvoice_WithVAT_AgainstPO(ByVal POno As String) As Decimal

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = _
            " SELECT     ISNULL(SUM(ISNULL(CASE WHEN VAT_Free = 0 THEN CONVERT(decimal(12, 2),  " + _
            "            dbo.Table3_Invoice.InvoiceValue * (dbo.Table2_PONo.VATpercent + 100) / 100) WHEN VAT_Free = 1 THEN dbo.Table3_Invoice.InvoiceValue END, 0)), 0)  " + _
            "            AS TotalInvoiceWithVAT " + _
            " FROM       dbo.Table2_PONo INNER JOIN " + _
            "            dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID LEFT OUTER JOIN " + _
            "            dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No " + _
            " WHERE      (dbo.Table2_PONo.PO_No = @PO_No) And dbo.Table2_PONo.PO_No <> N'PO-183-0089' "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim _PO As SqlParameter = cmd.Parameters.Add("@PO_No", Data.SqlDbType.NVarChar, 11)
            _PO.Value = POno
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Return dr(0)
            End While
            con.Close()
            dr.Close()
        End Using

    End Function

    Shared Function IsReplaceAddendumAllowed(ByVal _AddendumID As Integer) As Boolean

        If PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID).AddendumTypes = 2 Then
            If PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID).POexecuted = False Then

                If PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID).AddendumValue_WithVAT < _
                    GetTotalInvoice_WithVAT_AgainstPO(PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID).ContractID).PO_No) Then
                    Return False
                Else
                    Return True
                End If
            End If
        Else
            Return True
        End If

    End Function


End Class
