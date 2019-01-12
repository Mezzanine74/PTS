Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class PaymentApproval

    Shared Function GetDataPaymentApprovalByUserNamePayReqNo(ByVal _payreqNo As Integer, ByVal _username As String) As Mercury.Table_PayReq_UsersApprvRow

        Dim Table_PayReq_UserApprv_Adapter As New MercuryTableAdapters.Table_PayReq_UsersApprvTableAdapter
        Dim Table_PayReq_UserApprv_Table As Mercury.Table_PayReq_UsersApprvDataTable
        Dim _return As Mercury.Table_PayReq_UsersApprvRow

        Table_PayReq_UserApprv_Table = Table_PayReq_UserApprv_Adapter.GetDatallPaymentApprovalByUserPayReqNo(_payreqNo, _username)

        For Each _return In Table_PayReq_UserApprv_Table
            ' single value returns
            Return _return
            _return = Nothing
        Next

    End Function

    Shared Sub InsertDataPaymentApprovalByUserNamePayReqNo(ByVal _payreqNo As Integer, ByVal _username As String)

        Dim Table_PayReq_UserApprv_Adapter As New MercuryTableAdapters.Table_PayReq_UsersApprvTableAdapter

        Table_PayReq_UserApprv_Adapter.Insert(_payreqNo, _username, LocalTime.GetTime)

    End Sub

    Shared Sub DeleteFromDataPaymentApprovalByUserNamePayReqNo(ByVal _payreqNo As Integer, ByVal _username As String)

        Dim Table_PayReq_UserApprv_Adapter As New MercuryTableAdapters.Table_PayReq_UsersApprvTableAdapter

        Table_PayReq_UserApprv_Adapter.Delete(_payreqNo, _username)

    End Sub

    Shared Function ShowApprovalButton(ByVal _username As String) As Boolean

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " IF EXISTS ( " + _
                                    " SELECT COUNT([UserName]) " + _
                                    " FROM [dbo].[Table_ApprovalPayment_Scenario_Users] " + _
                                    " WHERE UserName = @UserName  " + _
                                    " GROUP BY UserName ) " + _
                                    " 	SELECT 1  " + _
                                    " ELSE " + _
                                    " 	SELECT 0 "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim UserParm As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar, 256)
            UserParm.Value = _username
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                If dr(0) = 0 Then
                    Return False
                Else
                    Return True
                End If
            End While
            con.Close()
            dr.Close()
        End Using

    End Function

End Class
