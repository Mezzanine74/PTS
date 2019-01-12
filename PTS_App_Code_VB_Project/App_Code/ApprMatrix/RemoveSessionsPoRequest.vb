Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Web

Public Class SessionsPoRequest
    Shared Sub RemoveScenarioSession(ByVal _Scenario As String)
        Dim userName As String = HttpContext.Current.User.Identity.Name.ToLower.ToString

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " DELETE FROM Table_Scenario WHERE Scenario LIKE '%' + @Scenario + '%' "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario", Data.SqlDbType.NVarChar)
            Scenario.Value = userName + _Scenario
            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            con.Dispose()
            dr.Close()
        End Using

    End Sub

    Shared Sub RemoveALLScenarioSession()
        Dim userName As String = HttpContext.Current.User.Identity.Name.ToLower.ToString

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " DELETE FROM Table_Scenario WHERE Scenario LIKE '%' + @Scenario + '%' "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario", Data.SqlDbType.NVarChar)
            Scenario.Value = userName + "Scenario"
            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            con.Dispose()
            dr.Close()
        End Using

    End Sub

    Shared Function ScenarioSessionExist(ByVal _Scenario As String) As Boolean

        Dim userName As String = HttpContext.Current.User.Identity.Name.ToLower.ToString
        Dim _return As Boolean = False

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT COUNT(Scenario) FROM Table_Scenario WHERE Scenario LIKE '%' + @Scenario + '%' "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario", Data.SqlDbType.NVarChar)
            Scenario.Value = userName + _Scenario
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                If dr(0) > 0 Then
                    _return = True
                Else
                    _return = False
                End If
            End While

            con.Close()
            con.Dispose()
            dr.Close()
            Return _return
        End Using

    End Function

    Shared Sub DeleteOtherSessions(ByVal _Scenario As String)

        ' Delete Scenarios except _Scenario
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = _
              " DELETE FROM [Table_Scenario] WHERE (Scenario NOT LIKE N'%' + @Scenario + '%') AND (Scenario LIKE N'%' + @Scenario2 + '%') "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim userName As String = HttpContext.Current.User.Identity.Name.ToLower.ToString
            Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario", Data.SqlDbType.NVarChar)
            Scenario.Value = userName + _Scenario

            Dim Scenario2 As SqlParameter = cmd.Parameters.Add("@Scenario2", Data.SqlDbType.NVarChar)
            Scenario2.Value = userName + "Scenario"


            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            con.Dispose()
            dr.Close()

        End Using

    End Sub

    Shared Sub DeleteALLSessions(ByVal _Name As String)
        ' Delete Scenarios except _Scenario
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = _
              " DELETE FROM [Table_Scenario] WHERE Scenario LIKE N'%' + @Scenario + '%' "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario", Data.SqlDbType.NVarChar)
            Scenario.Value = _Name + "Scenario"

            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            con.Dispose()
            dr.Close()
        End Using

    End Sub

End Class
