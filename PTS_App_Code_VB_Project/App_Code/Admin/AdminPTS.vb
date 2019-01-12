Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class AdminPTS

    Shared Function IsUserInProject(ByVal _UserName As String, ByVal _ProjectID As Integer) As Boolean

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SP_GetIsUserInProject"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim UserParm As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar, 256)
            UserParm.Value = _UserName

            Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", Data.SqlDbType.SmallInt)
            ProjectID.Value = _ProjectID

            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read

                If dr(0) > 0 Then
                    Return True
                Else
                    Return False
                End If

            End While

            con.Close()
            dr.Close()

        End Using

    End Function

    Shared Sub AddUserToProject(ByVal _UserName As String, ByVal _ProjectID As Integer)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SP_AddUserToProject"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim UserParm As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar, 256)
            UserParm.Value = _UserName

            Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", Data.SqlDbType.SmallInt)
            ProjectID.Value = _ProjectID

            Dim dr As SqlDataReader = cmd.ExecuteReader

            con.Close()
            dr.Close()

        End Using

    End Sub

    Shared Sub RemoveUserFromProject(ByVal _UserName As String, ByVal _ProjectID As Integer)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SP_RemoveUserFromProject"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim UserParm As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar, 256)
            UserParm.Value = _UserName

            Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", Data.SqlDbType.SmallInt)
            ProjectID.Value = _ProjectID

            Dim dr As SqlDataReader = cmd.ExecuteReader

            con.Close()
            dr.Close()

        End Using

    End Sub


End Class
