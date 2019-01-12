Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration

Public Class EditPaymentRequest

    Public Sub UpdatePaymentRequest(ByVal PayReqNo As Integer, _
                                         ByVal SiteRecordNo As String, _
                                         ByVal ActivityCode As String, _
                                         ByVal PayReqDate As DateTime, _
                                         ByVal Notes As String, _
                                         ByVal LinkToInvoice As String)

        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)

        Dim com As New SqlCommand("UPDATE_Table4_PaymentRequest", con)

        com.CommandType = CommandType.StoredProcedure


        If Notes = "" OrElse Notes Is Nothing Then
            Notes = ""
        End If

        If ActivityCode = "" OrElse ActivityCode Is Nothing Then
            ActivityCode = ""
        End If

        If SiteRecordNo = "" OrElse SiteRecordNo Is Nothing Then
            SiteRecordNo = ""
        End If


        ' Add parameters
        com.Parameters.AddWithValue("@PayReqNo", PayReqNo)
        com.Parameters.AddWithValue("@SiteRecordNo", SiteRecordNo)
        com.Parameters.AddWithValue("@ActivityCode", ActivityCode)
        com.Parameters.AddWithValue("@PayReqDate", PayReqDate)
        com.Parameters.AddWithValue("@Notes", Notes)
        com.Parameters.AddWithValue("@LinkToInvoice", LinkToInvoice)


        'com.Parameters.Add("@PayReqNo", SqlDbType.Int, 6).Value = PayReqNo
        'com.Parameters.Add("@SiteRecordNo", SqlDbType.Char, 15).Value = SiteRecordNo
        'com.Parameters.Add("@ActivityCode", SqlDbType.Char, 10).Value = ActivityCode
        'com.Parameters.Add("@PayReqDate", SqlDbType.SmallDateTime).Value = PayReqDate
        'com.Parameters.Add("@Notes", SqlDbType.Char, 40).Value = Notes
        'com.Parameters.Add("@LinkToInvoice", SqlDbType.Char, 90).Value = LinkToInvoice

        ' Execute command
        Using con
            con.Open()
            com.ExecuteNonQuery()
        End Using

    End Sub

    Public Sub DeletePaymentRequest(ByVal PayReqNo As Integer)

        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
        Dim com As New SqlCommand("DELETE_Table4_PaymentRequest", con)

        com.CommandType = CommandType.StoredProcedure

        ' Add parameters
        com.Parameters.AddWithValue("@PayReqNo", PayReqNo)

        ' Execute command
        Using con
            con.Open()
            com.ExecuteNonQuery()
        End Using

    End Sub


    Public Function BindPaymentRequest(ByVal startRowIndex As Integer, ByVal maximumRows As Integer, ByVal ProjectID As Integer, ByVal SupplierID As String) As DataTable
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
        con.Open()
        Dim com As New SqlCommand("GET_Table4_PaymentRequest", con)
        com.CommandType = CommandType.StoredProcedure
        com.Parameters.Add("@RowIndex", SqlDbType.Int, 4).Value = startRowIndex
        com.Parameters.Add("@MaxRows", SqlDbType.Int, 4).Value = maximumRows
        com.Parameters.Add("@ProjectID", SqlDbType.Int, 4).Value = ProjectID
        com.Parameters.Add("@SupplierID", SqlDbType.NChar, 12).Value = SupplierID
        Dim ada As New SqlDataAdapter(com)
        Dim dt As New DataTable()
        ada.Fill(dt)
        Return dt
    End Function

    Public Function GetPaymentRequestCount(ByVal ProjectID As Integer, ByVal SupplierID As String) As Integer
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
        con.Open()
        Dim com As New SqlCommand("GetPaymentRequestCount", con)
        com.CommandType = CommandType.StoredProcedure
        com.Parameters.Add("@ProjectID", SqlDbType.Int, 4).Value = ProjectID
        com.Parameters.Add("@SupplierID", SqlDbType.NChar, 12).Value = SupplierID
        Dim dr As SqlDataReader = com.ExecuteReader()
        Dim count As Integer
        While dr.Read()
            If dr("PaymentRequestCount") IsNot Nothing Then
                Integer.TryParse(dr("PaymentRequestCount").ToString(), count)
            End If
        End While
        Return count
    End Function


End Class
