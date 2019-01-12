Imports System.Data.SqlClient

Partial Class SupplierDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Using _adapter As New MercuryTableAdapters.Table6_SupplierDetailsTableAdapter

            Dim _table As New Mercury.Table6_SupplierDetailsDataTable
            Dim _row As Mercury.Table6_SupplierDetailsRow

            _table = _adapter.GetDataBySupplierID(Request.QueryString("SupplierID").ToString)

            For Each _row In _table
                LiteralHTMLSupplierAdressDetailsENG.Text = _row.SupplierAdressDetails_ENG
                LiteralHTMLSupplierAdressDetailsRUS.Text = _row.SupplierAdressDetails_RUS
                LiteralHTMLSupplierBankingDetailsENG.Text = _row.SupplierBankDetails_ENG
                LiteralHTMLSupplierBankingDetailsRUS.Text = _row.SupplierBankDetails_RUS
            Next

            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " SELECT RTRIM([SupplierName]) FROM [dbo].[Table6_Supplier] WHERE SupplierID = @SupplierID "
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text

                'syntax for parameter adding
                Dim SupplierID As SqlParameter = cmd.Parameters.Add("@SupplierID", System.Data.SqlDbType.NVarChar, 12)
                SupplierID.Value = Request.QueryString("SupplierID").ToString
                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    LiteralHeadSupplierDetails.Text = dr(0).ToString
                End While
                con.Close()
                dr.Close()
            End Using

            _table.Dispose()

            _adapter.Dispose()
        End Using

    End Sub
End Class
