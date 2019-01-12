Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Web.UI.WebControls
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Public Class ContractAndAddendumUpdateNotification

    Shared Sub SendContractUpdateNotification(ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs, ByVal row As GridViewRow)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            Dim _ContractNoOld As String = ""
            Dim _ContractNoNew As String = ""
            Dim _ContractDateOld As DateTime = Date.Today
            Dim _ContractDateNew As DateTime = Date.Today

            Dim _ContractID As Decimal = Convert.ToDecimal(CType(row.FindControl("LiteralContractID"), Literal).Text)

            If IsDBNull(e.OldValues("ContractNo")) Then
                _ContractNoOld = Nothing
            Else
                _ContractNoOld = e.OldValues("ContractNo")
            End If

            If IsDBNull(e.NewValues("ContractNo")) Then
                _ContractNoNew = Nothing
            Else
                _ContractNoNew = e.NewValues("ContractNo")
            End If

            If IsDBNull(e.OldValues("ContractDate")) Then
                _ContractDateOld = ""
            Else
                _ContractDateOld = Convert.ToDateTime(e.OldValues("ContractDate")).ToString("dd/MM/yyyy")
            End If

            If IsDBNull(e.NewValues("ContractDate")) Then
                _ContractDateNew = ""
            Else
                _ContractDateNew = Convert.ToDateTime(e.NewValues("ContractDate")).ToString("dd/MM/yyyy")
            End If

            If (_ContractNoOld = _ContractNoNew) And (_ContractDateOld = _ContractDateNew) Then
                con.Close()
                con.Dispose()
                Exit Sub
            End If

            con.Open()
            Dim sqlstring As String = "ContractUpdateEmailListGenerator"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            ContractID.Value = _ContractID

            Dim dr As SqlDataReader = cmd.ExecuteReader

            Dim _FromText As String = "Contract Updated"

            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", _FromText)
            Dim mm1 As New MailMessage()

            If (_ContractNoOld <> _ContractNoNew) And (_ContractDateOld <> _ContractDateNew) Then
                mm1.Subject = "Contract No and Contract Date Changed"
            ElseIf (_ContractNoOld = _ContractNoNew) And (_ContractDateOld <> _ContractDateNew) Then
                mm1.Subject = "Contract Date Changed"
            ElseIf (_ContractNoOld <> _ContractNoNew) And (_ContractDateOld = _ContractDateNew) Then
                mm1.Subject = "Contract No Changed"
            ElseIf (_ContractNoOld = _ContractNoNew) And (_ContractDateOld = _ContractDateNew) Then
                mm1.Subject = "nothing changed"
            End If

            mm1.Body = " <a href=" + """" + "http://pts.mercuryeng.ru/contractdetails.aspx?ContractID=" + _ContractID.ToString + """" + " target=" + """" + "_blank" + """" + ">SEE CONTRACT DETAILS</a> " + _
                Environment.NewLine + _
            "	<table border=" + """" + "1" + """" + " cellpadding=" + """" + "1" + """" + " cellspacing=" + """" + "1" + """" + " style=" + """" + "width: 500px;" + """" + "> " + _
"		<thead style=" + """" + "background-color:Aqua;" + """" + "> " + _
"			<tr> " + _
"				<th scope=" + """" + "col" + """" + "> " + _
"					&nbsp;</th> " + _
"				<th scope=" + """" + "col" + """" + "> " + _
"					Old Value</th> " + _
"				<th scope=" + """" + "col" + """" + "> " + _
"					New Value</th> " + _
"			</tr> " + _
"		</thead> " + _
"		<tbody> " + _
"			<tr> " + _
"				<td> " + _
"					Contract No</td> " + _
"				<td> " + _
"					" + _ContractNoOld + "</td> " + _
"				<td> " + _
"					" + _ContractNoNew + "</td> " + _
"			</tr> " + _
"			<tr> " + _
"				<td> " + _
"					Contract Date</td> " + _
"				<td> " + _
"					" + _ContractDateOld + "</td> " + _
"				<td> " + _
"					" + _ContractDateNew + "</td> " + _
"			</tr> " + _
"		</tbody> " + _
"	</table> "

            mm1.From = MailFrom

            While dr.Read
                If dr(0).ToString.Length <> 0 Then
                    mm1.To.Add(dr(0).ToString)
                End If
            End While

            mm1.To.Add("sk@mercuryeng.ru")

            mm1.IsBodyHtml = True

            Dim smtp As New SmtpClient_RussianEncoded
            'Try
            smtp.Send(mm1)
            'Catch ex As Exception
            'End Try
            con.Close()
            con.Dispose()
        End Using
    End Sub

    Shared Sub SendAddendumUpdateNotification(ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs, ByVal row As GridViewRow)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            Dim _AddendumNoOld As String = ""
            Dim _AddendumNoNew As String = ""
            Dim _AddendumDateOld As DateTime = Date.Today
            Dim _AddendumDateNew As DateTime = Date.Today

            Dim _AddendumID As Decimal = Convert.ToDecimal(CType(row.FindControl("LiteralAddenumID"), Literal).Text)

            If IsDBNull(e.OldValues("AddendumNo")) Then
                _AddendumNoOld = Nothing
            Else
                _AddendumNoOld = e.OldValues("AddendumNo")
            End If

            If IsDBNull(e.NewValues("AddendumNo")) Then
                _AddendumNoNew = Nothing
            Else
                _AddendumNoNew = e.NewValues("AddendumNo")
            End If

            If IsDBNull(e.OldValues("AddendumDate")) Then
                _AddendumDateOld = ""
            Else
                _AddendumDateOld = Convert.ToDateTime(e.OldValues("AddendumDate")).ToString("dd/MM/yyyy")
            End If

            If IsDBNull(e.NewValues("AddendumDate")) Then
                _AddendumDateNew = ""
            Else
                _AddendumDateNew = Convert.ToDateTime(e.NewValues("AddendumDate")).ToString("dd/MM/yyyy")
            End If

            If (_AddendumNoOld = _AddendumNoNew) And (_AddendumDateOld = _AddendumDateNew) Then
                con.Close()
                con.Dispose()
                Exit Sub
            End If

            con.Open()
            Dim sqlstring As String = "AddendumUpdateEmailListGenerator"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = _AddendumID

            Dim dr As SqlDataReader = cmd.ExecuteReader

            Dim _FromText As String = "Addendum Updated"

            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", _FromText)
            Dim mm1 As New MailMessage()

            If (_AddendumNoOld <> _AddendumNoNew) And (_AddendumDateOld <> _AddendumDateNew) Then
                mm1.Subject = "Addendum No and Addendum Date Changed"
            ElseIf (_AddendumNoOld = _AddendumNoNew) And (_AddendumDateOld <> _AddendumDateNew) Then
                mm1.Subject = "Addendum Date Changed"
            ElseIf (_AddendumNoOld <> _AddendumNoNew) And (_AddendumDateOld = _AddendumDateNew) Then
                mm1.Subject = "Addendum No Changed"
            ElseIf (_AddendumNoOld = _AddendumNoNew) And (_AddendumDateOld = _AddendumDateNew) Then
                mm1.Subject = "nothing changed"
            End If

            mm1.Body = " <a href=" + """" + "http://pts.mercuryeng.ru/addendumdetails.aspx?AddendumID=" + _AddendumID.ToString + """" + " target=" + """" + "_blank" + """" + ">SEE ADDENDUM DETAILS</a> " + _
                Environment.NewLine + _
            "	<table border=" + """" + "1" + """" + " cellpadding=" + """" + "1" + """" + " cellspacing=" + """" + "1" + """" + " style=" + """" + "width: 500px;" + """" + "> " + _
"		<thead style=" + """" + "background-color:Aqua;" + """" + "> " + _
"			<tr> " + _
"				<th scope=" + """" + "col" + """" + "> " + _
"					&nbsp;</th> " + _
"				<th scope=" + """" + "col" + """" + "> " + _
"					Old Value</th> " + _
"				<th scope=" + """" + "col" + """" + "> " + _
"					New Value</th> " + _
"			</tr> " + _
"		</thead> " + _
"		<tbody> " + _
"			<tr> " + _
"				<td> " + _
"					Addendum No</td> " + _
"				<td> " + _
"					" + _AddendumNoOld + "</td> " + _
"				<td> " + _
"					" + _AddendumNoNew + "</td> " + _
"			</tr> " + _
"			<tr> " + _
"				<td> " + _
"					Addendum Date</td> " + _
"				<td> " + _
"					" + _AddendumDateOld + "</td> " + _
"				<td> " + _
"					" + _AddendumDateNew + "</td> " + _
"			</tr> " + _
"		</tbody> " + _
"	</table> "

            mm1.From = MailFrom

            While dr.Read
                If dr(0).ToString.Length <> 0 Then
                    mm1.To.Add(dr(0).ToString)
                End If
            End While

            mm1.To.Add("sk@mercuryeng.ru")

            mm1.IsBodyHtml = True

            Dim smtp As New SmtpClient_RussianEncoded
            'Try
            smtp.Send(mm1)
            'Catch ex As Exception
            'End Try
            con.Close()
            con.Dispose()
        End Using
    End Sub
End Class
