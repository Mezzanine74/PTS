Imports Microsoft.VisualBasic
Imports System.Web.UI

Public Class ContractRemarksFromUser

    Shared Function GetDataByContractUser(ByVal _ContractID As Integer, ByVal _username As String) As Integer

        Dim Adapter As New MercuryTableAdapters.Table_Contracts_UserRemarksTableAdapter

        Return Adapter.GetCountResult(_ContractID, _username)

    End Function

    Shared Sub UpdateByContractUser(ByVal _ContractID As Integer, ByVal _username As String, ByVal _remark As String, Optional ByVal _userControl As UserControl = Nothing)

        'Dim Adapter As New MercuryTableAdapters.Table_Contracts_UserRemarksTableAdapter
        'Dim Table As Mercury.Table_Contracts_UserRemarksDataTable
        'Dim _row As Mercury.Table_Contracts_UserRemarksRow

        'Table = Adapter.GetDataByContractIDUserName(_ContractID, _username)

        'For Each _row In Table
        '    _row.Remark = _remark
        'Next

        'Adapter.Update(Table)

        '_row = Nothing
        'Table = Nothing
        'Adapter = Nothing

        ' send email to approval users if this contract disagreed by lawyers
        If _username.ToLower = NameOfLeadLawyer.GetNameFromFunction And ContractView.ContractOrAddendumRejected(_ContractID, 0, "lawyers") Then
            If _userControl IsNot Nothing Then
                SendEmailApprovalMatrix.Send(_ContractID, 8, Nothing, "rjCU", ContractView.ProduceHTMLforContractEmailBody(_userControl, _ContractID))
            End If
        End If

    End Sub

    'Shared Sub DeleteDataByContractUser(ByVal _ContractID As Integer, ByVal _username As String)

    '    Dim Adapter As New MercuryTableAdapters.Table_Contracts_UserRemarksTableAdapter

    '    Adapter.Delete(_ContractID, _username)

    'End Sub

    Shared Sub InsertByContractUser(ByVal _ContractID As Integer, ByVal _username As String, ByVal _remark As String)

        Dim Adapter As New MercuryTableAdapters.Table_Contracts_UserRemarksTableAdapter
        Adapter.Insert(_ContractID, _username, _remark, DateTime.Now)
        Adapter = Nothing

    End Sub

    Shared Function GetDataByAddendumUser(ByVal _AddendumID As Integer, ByVal _username As String) As Integer

        Dim Adapter As New MercuryTableAdapters.Table_Addendum_UserRemarksTableAdapter

        Return Adapter.GetCountResult(_AddendumID, _username)

    End Function

    Shared Sub UpdateByAddendumUser(ByVal _AddendumID As Integer, ByVal _username As String, ByVal _remark As String, Optional ByVal _userControl As UserControl = Nothing)

        'Dim Adapter As New MercuryTableAdapters.Table_Addendum_UserRemarksTableAdapter
        'Dim Table As Mercury.Table_Addendum_UserRemarksDataTable
        'Dim _row As Mercury.Table_Addendum_UserRemarksRow

        'Table = Adapter.GetDataAddendumIDUserName(_AddendumID, _username)

        'For Each _row In Table
        '    _row.Remark = _remark
        'Next

        'Adapter.Update(Table)

        '_row = Nothing
        'Table = Nothing
        'Adapter = Nothing

        ' send email to approval users if this addendum disagreed by lawyers
        If _username.ToLower = NameOfLeadLawyer.GetNameFromFunction And ContractView.ContractOrAddendumRejected(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID).ContractID, _AddendumID, "lawyers") Then
            If _userControl IsNot Nothing Then
                SendEmailApprovalMatrix.Send(_AddendumID, 9, Nothing, "rjAU", ContractView.ProduceHTMLforAddendumEmailBody(_userControl, _AddendumID))
            End If
        End If

    End Sub

    Shared Sub InsertByAddendumUser(ByVal _AddendumID As Integer, ByVal _username As String, ByVal _remark As String)

        Dim Adapter As New MercuryTableAdapters.Table_Addendum_UserRemarksTableAdapter
        Adapter.Insert(_AddendumID, _username, _remark, DateTime.Now)
        Adapter = Nothing

    End Sub

    'Shared Sub DeleteDataByAddendumUser(ByVal _AddendumID As Integer, ByVal _username As String)

    '    Dim Adapter As New MercuryTableAdapters.Table_Addendum_UserRemarksTableAdapter

    '    Adapter.Delete(_AddendumID, _username)

    'End Sub


End Class
