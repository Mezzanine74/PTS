Imports Microsoft.VisualBasic

Public Class ApprovalMatrixCreateDataReader

    Shared Function GetCountByUserRoleProjectID(ByVal _userName As String, ByVal _Role As String, ByVal _projectID As Integer) As Integer

        Using adapter As New ApprovalMatrixTableAdapters.Table_Approval_UserRolePrjectJunctionTableAdapter
            Return adapter.GetCountByUserRoleProjectID(_userName, _Role, _projectID)
            adapter.Dispose()
        End Using

    End Function

End Class
