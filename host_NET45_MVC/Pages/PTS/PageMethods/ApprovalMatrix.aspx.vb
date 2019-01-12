Imports System.Web.Services
Imports System.Data.SqlClient
Imports PTS_App_Code_VB_Project.PTS.CoreTables

Partial Class Pages_PTS_PageMethods_ApprovalMatrix
    Inherits System.Web.UI.Page

    Class ApprovalParameters
        Property UserName As String
        Property ContractId As Integer
        Property AddendumId As Integer
    End Class

    Public Shared Function ContractOrAddendumApprovedByUser(ByVal _ContractID As Integer, ByVal _AddendumID As Integer, ByVal _username As String) As Boolean

        If _ContractID > 0 And _AddendumID = 0 Then
            ' use contract
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " IF EXISTS (SELECT [ContractID] FROM [Table_Contract_UsersApprv] WHERE UserName = @UserName AND ContractID = @ContractID)" + _
                                        " Select 1" + _
                                        " ELSE" + _
                                        " Select 0"

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                'syntax for parameter adding
                Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
                ContractID.Value = _ContractID
                Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar)
                UserName.Value = _username

                Dim ReturnValue As Boolean = False
                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    If dr(0) > 0 Then
                        ReturnValue = True
                    End If
                End While
                Return ReturnValue
                con.Close()
                con.Dispose()
                dr.Close()
            End Using
        ElseIf _ContractID > 0 And _AddendumID > 0 Then
            'use addendum
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " IF EXISTS (SELECT [AddendumID] FROM [Table_Addendum_UsersApprv] WHERE UserName = @UserName AND AddendumID = @AddendumID)" + _
                                        " Select 1" + _
                                        " ELSE" + _
                                        " Select 0"

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                'syntax for parameter adding
                Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", System.Data.SqlDbType.Int)
                AddendumID.Value = _AddendumID
                Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar)
                UserName.Value = _username

                Dim ReturnValue As Boolean = False
                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    If dr(0) > 0 Then
                        ReturnValue = True
                    End If
                End While
                Return ReturnValue
                con.Close()
                con.Dispose()
                dr.Close()
            End Using
        End If

    End Function

    <WebMethod> _
    Public Shared Sub Approve(ApprovalParameters As ApprovalParameters)

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim a As New ApprovalParameters

            a.UserName = ApprovalParameters.UserName
            a.ContractId = ApprovalParameters.ContractId
            a.AddendumId = ApprovalParameters.AddendumId

            ' THIS PART CAME FROM APPROVAL MATRIX (ApproveOrDisapprove)
            If Convert.ToInt32(a.ContractId) > 0 And Convert.ToInt32(a.AddendumId) = 0 Then
                ' It is contract
                ' It is approval or NOT approval ?
                If ContractOrAddendumApprovedByUser(Convert.ToInt32(a.ContractId), Convert.ToInt32(a.AddendumId), a.UserName) = False Then
                    ' APPROVE
                    ContractView.ApproveContract(a.ContractId, a.UserName)
                    'Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                    '                               Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                    '                               ">You have approved successfully!</p>")
                ElseIf ContractOrAddendumApprovedByUser(Convert.ToInt32(a.ContractId), Convert.ToInt32(a.AddendumId), a.UserName) = True Then
                    ' NOT APPROVE
                    ContractView.RemoveApprovalContract(a.ContractId, a.UserName)
                    'Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                    '                               Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                    '                               ">You have removed your approval successfully!</p>")
                End If

                ' CHECK CONTRACT IF IT IS READY FOR PO
                If ContractView.ContractReadyToTurnPo(Convert.ToInt32(a.ContractId)) _
                  And ContractView.ScenarioNoForThisContract(Convert.ToInt32(a.ContractId)) <> 0 _
                   And ContractView.GetPoExecutionFromContract(Convert.ToInt32(a.ContractId)) = False Then

                    ' AddendumID to be 0
                    ContractView.InsertOrUpdatePoFromContract( _
                      CreateDataReader.Create_Table_Contract(Convert.ToInt32(a.ContractId)).ProjectID, _
                      Convert.ToInt32(a.ContractId), _
                      0, _
                      HttpContext.Current.User.Identity.Name.ToLower)

                End If
                ' ......./ END OF CHECK CONTRACT IF IT IS READY FOR PO

            ElseIf Convert.ToInt32(a.ContractId) > 0 And Convert.ToInt32(a.AddendumId) > 0 Then
                ' it is addendum
                ' It is approval or NOT approval ?
                If ContractOrAddendumApprovedByUser(Convert.ToInt32(a.ContractId), Convert.ToInt32(a.AddendumId), a.UserName) = False Then
                    ' APPROVE
                    ContractView.ApproveAddendum(a.AddendumId, a.UserName)
                    'Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                    '                               Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                    '                               ">You have approved successfully!</p>")
                ElseIf ContractOrAddendumApprovedByUser(Convert.ToInt32(a.ContractId), Convert.ToInt32(a.AddendumId), a.UserName) = True Then
                    ' NOT APPROVE
                    ContractView.RemoveApprovalAddendum(a.AddendumId, a.UserName)
                    'Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                    '                               Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                    '                               ">You have removed your approval successfully!</p>")
                End If

                ' CHECK ADDENDUM IF IT IS READY FOR PO
                Dim ContractID As Integer = CreateDataReader.Create_Table_Addendums(Convert.ToInt32(a.AddendumId)).ContractID
                If ContractView.AddendumReadyToTurnPo(Convert.ToInt32(a.AddendumId)) _
                  And _
                  ( _
                      (ContractView.ScenarioNoForThisAddendum(Convert.ToInt32(a.AddendumId)) = 0 _
                       And CreateDataReader.Create_Table_Addendums(Convert.ToInt32(a.AddendumId)).AddendumTypes = 3) _
                      Or _
                      (ContractView.ScenarioNoForThisAddendum(Convert.ToInt32(a.AddendumId)) > 0 _
                       And CreateDataReader.Create_Table_Addendums(Convert.ToInt32(a.AddendumId)).AddendumTypes > 0 _
                       And CreateDataReader.Create_Table_Addendums(Convert.ToInt32(a.AddendumId)).AddendumTypes < 3) _
                   ) _
                    And ContractView.GetPoExecutionFromAddendum(Convert.ToInt32(a.AddendumId)) = False Then

                    ContractView.InsertOrUpdatePoFromContract( _
                      CreateDataReader.Create_Table_Contract(ContractID).ProjectID, _
                      ContractID, _
                      Convert.ToInt32(a.AddendumId), _
                      HttpContext.Current.User.Identity.Name.ToLower)

                End If
                ' ......./ END OF CHECK ADDENDUM IF IT IS READY FOR PO
            End If
            ' THIS PART CAME FROM APPROVAL MATRIX (ApproveOrDisapprove)

        End Using

    End Sub

End Class
