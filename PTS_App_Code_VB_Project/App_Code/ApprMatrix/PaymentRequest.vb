Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class PaymentRequest

    Shared Function GetData(ByVal _payreqNo As Integer) As Mercury.Table_PayReq_UsersPayRequestRow

        Dim Adapter As New MercuryTableAdapters.Table_PayReq_UsersPayRequestTableAdapter
        Dim Table As Mercury.Table_PayReq_UsersPayRequestDataTable
        Dim _return As Mercury.Table_PayReq_UsersPayRequestRow

        Table = Adapter.GetDataByPayReqNo(_payreqNo)

        For Each _return In Table
            ' single value returns
            Return _return
            _return = Nothing
        Next

        Table = Nothing
        Adapter = Nothing

    End Function

    Shared Sub Insert(ByVal _payreqNo As Integer, ByVal _username As String)

        Dim Adapter As New MercuryTableAdapters.Table_PayReq_UsersPayRequestTableAdapter

        Adapter.InsertQuery(_payreqNo, _username, DateTime.Now)

        Adapter = Nothing

    End Sub

    Shared Sub Delete(ByVal _payreqNo As Integer)

        Dim Adapter As New MercuryTableAdapters.Table_PayReq_UsersPayRequestTableAdapter

        Adapter.DeleteByPayReqNo(_payreqNo)

        Adapter = Nothing

    End Sub

    Shared Function ShowRequestButton(ByVal _username As String) As Boolean

        If PTS.CoreTables.CreateDataReader.Create_aspnet_Users(_username).AuthorizedForPaymentRequest = True Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
