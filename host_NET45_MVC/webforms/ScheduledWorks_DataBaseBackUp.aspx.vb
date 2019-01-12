Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient
Imports System.IO

Partial Class _Nakl_ScheduledWorks_DataBaseBackUp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If IsPostBack Or Not IsPostBack Then
            If Page.User.IsInRole("admin") Then
            Else
                Response.Redirect("~/webforms/AccessDenied.aspx")
                Exit Sub
            End If
        End If

        ButtonExecute_Click(Nothing, Nothing)

    End Sub

    Protected Sub BackUpTable_PendingReportCnvEuro()
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "BackUpTable_PendingReportCnvEuro"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            cmd.ExecuteNonQuery()
            con.Close()
        End Using
    End Sub

    Protected Sub BackUpTable_PendingReportOrgCurry()
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "BackUpTable_PendingReportOrgCurry"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            cmd.ExecuteNonQuery()
            con.Close()
        End Using
    End Sub

    Protected Function FileExist(ByVal path As String) As Boolean
        Dim BackUpfile As System.IO.FileInfo = New System.IO.FileInfo(path)
        Dim Exist_ As New Boolean
        If BackUpfile.Exists Then
            Exist_ = True
        Else
            Exist_ = False
        End If
        Return Exist_
    End Function

    Protected Sub SqlDataSourceAgeingInsert_Inserting(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceAgeingInsert.Inserting
        e.Command.CommandTimeout = 500
    End Sub

    Protected Sub SqlDataSourceFollowUp_Inserting(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceFollowUp.Inserting
        e.Command.CommandTimeout = 500
    End Sub

    Protected Sub ButtonExecute_Click(sender As Object, e As EventArgs)

        Dim thehour As Integer = Convert.ToInt16(TextBoxTime.Text.Trim)

        ' IF YOU NEED TO GO HISTORY, YOU CAN CHANGE this Value as Minus. All procedure designed so
        Dim theshiftofday = TextBoxShift.Text.Trim
        ' ---------------------------------------------------------------------------------------

        Dim label1 As String = ""
        Dim label2 As String = ""
        Dim label3 As String = ""
        Dim label4 As String = ""
        Dim label1ForeColor As String = ""
        Dim label2ForeColor As String = ""
        Dim label3ForeColor As String = ""
        Dim label4ForeColor As String = ""

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)
        result = result.AddDays(theshiftofday)
        If result.Hour > thehour Then
            ' GO
            ' check backup
            Dim year As String = result.Year.ToString

            Dim month As String = ""
            If Len(result.Month.ToString) = 1 Then
                month = "0" + result.Month.ToString
            Else
                month = result.Month.ToString
            End If

            Dim day As String = ""
            If Len(result.Day.ToString) = 1 Then
                day = "0" + result.Day.ToString
            Else
                day = result.Day.ToString
            End If

            Dim path As String = Server.MapPath("~/SqlDatabase_Backups/SQL2008_794282_mercury_" + year + "_" + month + "_" + day + ".bak")

            If result.DayOfWeek.ToString = "Saturday" OrElse result.DayOfWeek.ToString = "Sunday" Then

                ' do nothing
                label1 = "BackUp doesnt required on SATURDAY and SUNDAY"
                label1ForeColor = "Red"
            Else
                ' BACKUP
                If FileExist(path) Then
                    label1 = path + " EXIST" + " > NO NEED TO CREATE BACKUP"
                    label1ForeColor = "Red"
                Else
                    Try
                        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                            con.Open()
                            Dim sqlstring As String = "BackupDatabase"
                            Dim cmd As New SqlCommand(sqlstring, con)
                            cmd.CommandTimeout = 200
                            cmd.CommandType = System.Data.CommandType.StoredProcedure

                            Dim _ShiftOfDay As SqlParameter = cmd.Parameters.Add("@ShiftOfDay", System.Data.SqlDbType.Int)
                            _ShiftOfDay.Value = theshiftofday

                            Dim dr As SqlDataReader = cmd.ExecuteReader
                            con.Close()
                            dr.Close()
                        End Using
                        If FileExist(path) Then
                            label1 = path + " has been created successfully!"
                            label1ForeColor = "Green"
                        Else
                            label1 = "Backup has not been created successfully! Something is wrong"
                            label1ForeColor = "Red"
                        End If
                    Catch ex As Exception
                        label1 = "Backup has not been created successfully! Exception Thrown!!"
                        label1ForeColor = "Red"
                    End Try

                    Try
                        ' BackUp Table_PendingReportCnvEuro
                        BackUpTable_PendingReportCnvEuro()
                    Catch ex As Exception

                    End Try

                    Try
                        ' BackUp Table_PendingReportOrgCurry
                        BackUpTable_PendingReportOrgCurry()

                    Catch ex As Exception

                    End Try

                End If

                ' backup IKEA report
                If Not System.IO.File.Exists(Server.MapPath("~/REQUEST/CostReportBackups/IKEA Cost Report " + year + "_" + month + "_" + day + ".xls")) Then
                    Try
                        RenderReport.Render("disc", _Nakl_CostReportInEuroWthSubPoWthPaidIKEA, "_Nakl_CostReportInEuroWthSubPoWthPaidIKEA", "excel", 1, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "REQUEST/CostReportBackups/IKEA Cost Report " + year + "_" + month + "_" + day + ".xls")
                    Catch ex As Exception

                    End Try
                End If

                If Not System.IO.File.Exists(Server.MapPath("~/REQUEST/CostReportBackups/BLD5 VTB Cost Report " + year + "_" + month + "_" + day + ".xls")) Then
                    Try
                        RenderReport.Render("disc", _Nakl_CostReportInEuroWthSubPoWthPaidIKEA, "_Nakl_CostReportInEuroWthSubPoWthPaid_BLD5", "excel", 1, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "REQUEST/CostReportBackups/BLD5 VTB Cost Report " + year + "_" + month + "_" + day + ".xls")
                    Catch ex As Exception

                    End Try
                End If


            End If

            ' AGEING
            Dim zoneId2Ageing As String = "Russian Standard Time"
            Dim tzi2Ageing As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId2Ageing)
            Dim result2Ageing As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi2Ageing)
            result2Ageing = result2Ageing.AddDays(theshiftofday)
            If result2Ageing.DayOfWeek.ToString = "Sunday" Then
                Dim LastMonday As String = "'" + Mid(result2Ageing.AddDays(1).ToString, 4, 2).ToString + "/" + Mid(result2Ageing.AddDays(1).ToString, 1, 2).ToString + "/" + Mid(result2Ageing.AddDays(1).ToString, 7, 4).ToString + "'"
                SqlDataSourceAgeingInsert.InsertCommand = " INSERT INTO [Table_Ageing] " +
        "            ([ProjectID] " +
        "            ,[DayOfRun] " +
        "            ,[PO_Total] " +
        "            ,[Paid] " +
        "            ,[Pending] " +
        "            ,[Balance]) " +
        " (SELECT [ProjectID] " +
        "       ," + LastMonday.ToString + " AS DayOfRun " +
        "       ,(CASE WHEN [OverallPoTotalEuroExcVAT] IS NULL THEN 0 ELSE [OverallPoTotalEuroExcVAT] END) AS PO_Total " +
        "       ,(CASE WHEN [OverallEuroPaidExcVAT] IS NULL THEN 0 ELSE [OverallEuroPaidExcVAT] END) AS Paid " +
        "       ,(CASE WHEN [OverallEuroPendingExcVAT] IS NULL THEN 0 ELSE [OverallEuroPendingExcVAT] END) AS Pending " +
        "       ,(CASE WHEN [OverallBalanceEuroExcVAT] IS NULL THEN 0 ELSE  [OverallBalanceEuroExcVAT] END) AS Balance " +
        "   FROM [dbo].[View_CostCodeSummary1] " +
        "   WHERE ProjectID IN  " +
        "   (SELECT     ProjectID " +
        " FROM         dbo.Table1_Project " +
        " WHERE     (Report = 1))) "
                Try
                    SqlDataSourceAgeingInsert.Insert()
                    label3 = "Ageing updated Successfully"
                    label3ForeColor = "Green"
                Catch
                    label3 = "Ageing has NOT been updated !!"
                    label3ForeColor = "Red"
                End Try
            Else
                label3 = "TODAY IS NOT SUNDAY"
                label3ForeColor = "Red"
            End If

            ' it feeds FollowUp History table
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = "SP_FeedFollowUpSummary"
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.StoredProcedure

                'syntax for parameter adding
                Dim _DayOfRun As SqlParameter = cmd.Parameters.Add("@Now_", System.Data.SqlDbType.DateTime)
                _DayOfRun.Value = result
                Dim dr As SqlDataReader = cmd.ExecuteReader

                label4 = "FollowUp History table updated Successfully"
                label4ForeColor = "Green"

                con.Close()
                dr.Close()
            End Using

            '      Dim zoneIdFollowUp As String = "Russian Standard Time"
            '      Dim tziFollowUp As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneIdFollowUp)

            '      Dim resultFollowUp As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tziFollowUp)
            '      resultFollowUp = resultFollowUp.AddDays(theshiftofday)
            '      Dim DayOfRun As String = "'" + Mid(resultFollowUp.ToString, 7, 4).ToString + "-" + Mid(resultFollowUp.ToString, 4, 2).ToString + "-" + Mid(resultFollowUp.ToString, 1, 2).ToString + " 00:00:00" + "'"
            '      SqlDataSourceFollowUp.InsertCommand = " INSERT INTO Table_FollowUpReportSummary " + _
            '"            ([ProjectID] " + _
            '"            ,[DayOfRun] " + _
            '"            ,[OverallPoTotalDollarExcVAT] " + _
            '"            ,[OverallPoTotalEuroExcVAT] " + _
            '"            ,[OverallPoTotalRubleExcVAT] " + _
            '"            ,[OverallDollarPaidExcVAT] " + _
            '"            ,[OverallEuroPaidExcVAT] " + _
            '"            ,[OverallRublePaidExcVAT] " + _
            '"            ,[OverallDollarPendingExcVAT] " + _
            '"            ,[OverallEuroPendingExcVAT] " + _
            '"            ,[OverallRublePendingExcVAT] " + _
            '"            ,[OverallDoneDollarPO] " + _
            '"            ,[OverallDoneEuroPO] " + _
            '"            ,[OverallDoneRublePO] " + _
            '"            ,[OverallPartialDollarPO] " + _
            '"            ,[OverallPartialEuroPO] " + _
            '"            ,[OverallPartialRublePO] " + _
            '"            ,[OverallDollarPaidWthVAT] " + _
            '"            ,[OverallEuroPaidWthVAT] " + _
            '"            ,[OverallRublePaidWthVAT] " + _
            '"            ,OverallPoTotalEuroWithVAT " + _
            '"            ,OverallTotalCollectedDocDollarExcVAT " + _
            '"            ,OverallTotalCollectedDocEuroExcVAT " + _
            '"            ,OverAllTotalCollectedDocRubleExcVAT) " + _
            '" (SELECT     dbo.View_CostCodeSummary1.ProjectID, " + DayOfRun + ", dbo.View_CostCodeSummary1.OverallPoTotalDollarExcVAT,  " + _
            '"                       dbo.View_CostCodeSummary1.OverallPoTotalEuroExcVAT, dbo.View_CostCodeSummary1.OverallPoTotalRubleExcVAT,  " + _
            '"                       dbo.View_CostCodeSummary1.OverallDollarPaidExcVAT, dbo.View_CostCodeSummary1.OverallEuroPaidExcVAT,  " + _
            '"                       dbo.View_CostCodeSummary1.OverallRublePaidExcVAT, dbo.View_CostCodeSummary1.OverallDollarPendingExcVAT,  " + _
            '"                       dbo.View_CostCodeSummary1.OverallEuroPendingExcVAT, dbo.View_CostCodeSummary1.OverallRublePendingExcVAT, 0 AS OverallDoneDollarPO,  " + _
            '"                       0 AS OverallDoneEuroPO, 0 AS OverallDoneRublePO, 0 AS OverallPartialDollarPO, 0 AS OverallPartialEuroPO, 0 AS OverallPartialRublePO,  " + _
            '"                       dbo.View_CostCodeSummary1.OverallDollarPaidExcVAT + dbo.View_CostCodeSummary2.VATpaidDollar AS OverallDollarPaidWthVAT,  " + _
            '"                       dbo.View_CostCodeSummary1.OverallEuroPaidExcVAT + dbo.View_CostCodeSummary2.VATpaidEuro AS OverallEuroPaidWthVAT,  " + _
            '"                       dbo.View_CostCodeSummary1.OverallRublePaidExcVAT + dbo.View_CostCodeSummary2.VATpaidRuble AS OverallRublePaidWthVAT,  " + _
            '"                       dbo.View_CostCodeSummary1_WithVAT.OverallPoTotalEuroWithVAT, OverallTotalCollected.OverallTotalCollectedDocDollarExcVAT,  " + _
            '"                       OverallTotalCollected.OverallTotalCollectedDocEuroExcVAT, OverallTotalCollected.OverAllTotalCollectedDocRubleExcVAT " + _
            '" FROM         dbo.View_CostCodeSummary1 INNER JOIN " + _
            '"                       dbo.View_CostCodeSummary2 ON dbo.View_CostCodeSummary1.ProjectID = dbo.View_CostCodeSummary2.ProjectID INNER JOIN " + _
            '"                       dbo.View_CostCodeSummary1_WithVAT ON dbo.View_CostCodeSummary1.ProjectID = dbo.View_CostCodeSummary1_WithVAT.ProjectID INNER JOIN " + _
            '"                           (SELECT     ProjectID, SUM(TotalCollectedDocDollarExcVAT) AS OverallTotalCollectedDocDollarExcVAT, SUM(TotalCollectedDocEuroExcVAT)  " + _
            '"                                                    AS OverallTotalCollectedDocEuroExcVAT, SUM(TotalCollectedDocRubleExcVAT) AS OverAllTotalCollectedDocRubleExcVAT " + _
            '"                             FROM          dbo.View_CostCodeSummary0 " + _
            '"                             GROUP BY ProjectID) AS OverallTotalCollected ON dbo.View_CostCodeSummary1.ProjectID = OverallTotalCollected.ProjectID " + _
            '" WHERE     (dbo.View_CostCodeSummary1.ProjectID IN " + _
            '"                           (SELECT     ProjectID " + _
            '"                             FROM          dbo.Table1_Project " + _
            '"                             WHERE      (Report = 'True'))) AND (dbo.View_CostCodeSummary1.OverallPoTotalDollarExcVAT IS NOT NULL)) "

            '      'Try
            '      SqlDataSourceFollowUp.Insert()
            '      label4 = "FollowUp History table updated Successfully"
            '      label4ForeColor = "Green"

            ' FOLLOWUP_BACKUP
            If result2Ageing.DayOfWeek.ToString = "Saturday" OrElse result2Ageing.DayOfWeek.ToString = "Sunday" Then
                ' do nothing
                label2 = "No need for FollowUp_Back on Saturday and Sunday"
                label2ForeColor = "Red"
            Else
                'Try
                Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    con.Open()
                    Dim sqlstring As String = "FollowUp_Backup"
                    Dim cmd As New SqlCommand(sqlstring, con)
                    cmd.CommandTimeout = 200
                    cmd.CommandType = System.Data.CommandType.StoredProcedure
                    Dim _ShiftOfDay As SqlParameter = cmd.Parameters.Add("@ShiftOfDay", System.Data.SqlDbType.Int)
                    _ShiftOfDay.Value = theshiftofday
                    Dim dr As SqlDataReader = cmd.ExecuteReader
                    con.Close()
                    dr.Close()
                End Using
                label2 = "FollowUp_Back  has been created successfully!"
                label2ForeColor = "Green"
                'Catch ex As Exception
                'label2 = "FollowUp_Back has not been created successfully! Exception Thrown!!"
                'label2ForeColor = "Red"
                'End Try

                ' Copy the latest PO table into FOLLOW UP database.
                Using conCOPY As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    conCOPY.Open()
                    Dim sqlstringCOPY As String = "UpdateFollowUPBackUpPO_SupplierTables"
                    Dim cmdCOPY As New SqlCommand(sqlstringCOPY, conCOPY)
                    cmdCOPY.CommandTimeout = 200
                    cmdCOPY.CommandType = System.Data.CommandType.StoredProcedure
                    Dim drCOPY As SqlDataReader = cmdCOPY.ExecuteReader
                    conCOPY.Close()
                    drCOPY.Close()
                End Using

            End If

            'Catch
            'label4 = "FollowUp History table has NOT been updated !!!"
            'label4ForeColor = "Red"

            'label2 = "FollowUp_Back has not been updated Because of Exception in FollowUp History !!"
            'label2ForeColor = "Red"
            'End Try

            'send email
            Dim Task As New MyCommonTasks
            Dim subject As String = "BackUp Notification " + DateTime.Now.ToString
            Dim body As String = "    <table> " + _
      "      <tr> " + _
      "       <td style=" + """" + "height: 30px" + """" + "> " + _
      "        <span id=" + """" + "ctl00_MainContent_Label5" + """" + " style=" + """" + "font-size:10px;" + """" + ">" + DateTime.Now.ToString + "</span> " + _
      "        </td> " + _
      "      </tr> " + _
      "      <tr> " + _
      "       <td style=" + """" + "height: 30px" + """" + "> " + _
      "        <span id=" + """" + "ctl00_MainContent_Label1" + """" + " style=" + """" + "color:" + label1ForeColor + ";font-size:10px;" + """" + ">" + label1 + "</span> " + _
      "        </td> " + _
      "      </tr> " + _
      "      <tr> " + _
      "       <td style=" + """" + "height: 30px" + """" + "> " + _
      "        <span id=" + """" + "ctl00_MainContent_Label2" + """" + " style=" + """" + "color:" + label2ForeColor + ";font-size:10px;" + """" + ">" + label2 + "</span> " + _
      "        </td> " + _
      "      </tr> " + _
      "      <tr> " + _
      "       <td style=" + """" + "height: 30px" + """" + "> " + _
      "        <span id=" + """" + "ctl00_MainContent_Label3" + """" + " style=" + """" + "color:" + label3ForeColor + ";font-size:10px;" + """" + ">" + label3 + "</span> " + _
      "        </td> " + _
      "      </tr> " + _
      "      <tr> " + _
      "       <td style=" + """" + "height: 30px" + """" + "> " + _
      "        <span id=" + """" + "ctl00_MainContent_Label4" + """" + " style=" + """" + "color:" + label4ForeColor + ";font-size:10px;" + """" + ">" + label4 + "</span> " + _
      "        </td> " + _
      "      </tr> " + _
      "    </table> "

            Try
                Task.SendEmailToAdmin(subject, body, Nothing, True)
            Catch ex As Exception
                body = "<h4>EMAIL NOT WORKING !!</h4>" + "<br />" + body
            End Try

            LabelInfo.Text = body
        Else
            'Too early
            label1 = "TOO EARLY_+" + result.Hour.ToString
            label1ForeColor = "Red"
            LabelInfo.Text = "        <span id=" + """" + "ctl00_MainContent_Label1" + """" + " style=" + """" + "color:" + label1ForeColor + ";font-size:10px;" + """" + ">" + label1 + "</span> "
        End If

    End Sub
End Class
