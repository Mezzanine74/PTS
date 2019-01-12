Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.Data.SqlClient

Public Class AddContractGirlsToEmailLoop
    Shared Sub Add(ByVal _mail As MailMessage)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = _
                " SELECT     RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail " + _
                " FROM         dbo.aspnet_Membership INNER JOIN " + _
                "                       dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId " + _
                " WHERE     (dbo.aspnet_Users.ContractGirlApprovalEmail = 1) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                _mail.To.Add(dr(0).ToString)
            End While

            con.Close()
            dr.Close()
            con.Dispose()

        End Using
    End Sub

End Class
