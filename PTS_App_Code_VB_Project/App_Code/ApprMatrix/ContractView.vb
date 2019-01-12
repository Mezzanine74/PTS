Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports PTS.CoreTables
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.Security
Imports System.Web
Imports PTS_App_Code_VB_Project.PTS.CoreTables

Public Class ContractView

    Shared Function USER_IN_Table_Contract_User_Junction(ByVal _UserName As String, _
                                                         ByVal _ProjectID As Integer) As Boolean

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT COUNT(UserName)  FROM [Table_Contract_User_Junction] WHERE UserName = @UserName AND ProjectID = @ProjectID "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text
            'syntax for parameter adding
            Dim PrjID As SqlParameter = cmd.Parameters.Add("@ProjectID", Data.SqlDbType.Int)
            PrjID.Value = _ProjectID
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar)
            UserName.Value = _UserName

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

    End Function

    Shared Function SupplierSigned_MercurySigned(ByVal _ContractID As Integer) As Boolean

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = " SELECT COUNT(ContractID) FROM Table_Contracts WHERE ContractID = @ContractID AND SignByMercury = 1 AND SignByMercury = 1 "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text
            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            ContractID.Value = _ContractID

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

    End Function

    Shared Function SupplierSigned_MercurySigned_Addendum(ByVal _AddendumID As Integer) As Boolean

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = " SELECT COUNT(AddendumID) FROM Table_Addendums WHERE AddendumID = @AddendumID AND AddendumSignBySupplier = 1 AND AddendumSignByMercury = 1 "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text
            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = _AddendumID

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

    End Function

    Shared Function NewGeneration(ByVal _PrpjectID As Integer) As Boolean

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " select NewGeneration from Table1_Project where ProjectID = @ProjectID "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text
            'syntax for parameter adding
            Dim PrjID As SqlParameter = cmd.Parameters.Add("@ProjectID", Data.SqlDbType.Int)
            PrjID.Value = _PrpjectID
            Dim ReturnValue As Boolean = False
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                ReturnValue = dr(0)
            End While
            Return ReturnValue
            con.Close()
            con.Dispose()
            dr.Close()
        End Using

    End Function

    Shared Function ProduceEmailBodyForPoDetails(ByVal _PO_No As String, ByVal _usercontrol As UserControl) As String

        Dim SQLsourcer As SqlDataSource = _usercontrol.FindControl("SqlDataSourcePoToApprove")
        SQLsourcer.SelectParameters("PO_No").DefaultValue = _PO_No

        Dim GridViewPoToApprove As GridView = _usercontrol.FindControl("GridViewPoToApprove")
        GridViewPoToApprove.DataBind()

        Dim stringWrite As New System.IO.StringWriter()
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
        _usercontrol.RenderControl(htmlWrite)
        Return stringWrite.ToString

    End Function

    Shared Function GetContractCurrency(ByVal _AddendumID As Integer) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     RTRIM(dbo.Table_Contracts.ContractCurrency) AS ContractCurrency " + _
                          " FROM         dbo.Table_Contracts INNER JOIN " + _
                          "                       dbo.Table_Addendums ON dbo.Table_Contracts.ContractID = dbo.Table_Addendums.ContractID " + _
                          " WHERE     (dbo.Table_Addendums.AddendumID = @AddendumID) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text
            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = _AddendumID
            Dim ReturnValue As String = ""
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                ReturnValue = dr(0)
            End While
            Return ReturnValue
            con.Close()
            con.Dispose()
            dr.Close()
        End Using

    End Function

    Shared Sub UpdateAddendumPoExecuted(ByVal _AddendumID As Integer)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " UPDATE [Table_Addendums] SET [POexecuted] = 1 WHERE AddendumID = @AddendumID "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text
            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = _AddendumID
            cmd.ExecuteNonQuery()
            con.Close()
            con.Dispose()
        End Using

    End Sub

    Shared Function GetPoExecutionFromContract(ByVal _ContractID As Integer) As Boolean

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT [POexecuted] FROM [Table_Contracts] WHERE ContractID = @ContractID "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text
            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            ContractID.Value = _ContractID
            Dim ReturnValue As Boolean = False
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                ReturnValue = dr(0)
            End While
            Return ReturnValue
            con.Close()
            con.Dispose()
            dr.Close()
        End Using

    End Function

    Shared Function GetPoExecutionFromAddendum(ByVal _AddendumID As Integer) As Boolean

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT [POexecuted] FROM [Table_Addendums] WHERE AddendumID = @AddendumID "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text
            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = _AddendumID
            Dim ReturnValue As Boolean = False
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                ReturnValue = dr(0)
            End While
            Return ReturnValue
            con.Close()
            con.Dispose()
            dr.Close()
        End Using

    End Function

    Shared Function ContractReadyToTurnPo(ByVal _ContractID As Integer) As Boolean
        Dim _return As Boolean = False
        If _ContractID = 0 Then
            Return False
            Exit Function
        End If

        If AllRequiredPersonsApprovedContract(_ContractID, 0) _
          And AllParametersProvidedToPassApprovalStage(_ContractID, 2) Then
            ' Sign Contract On Behalf Of Mercury
            ' This is cancelled as per Larisa request
            'SignContractOnBeHalfOfMercury(_ContractID)
            _return = True
        Else
            _return = False
        End If
        Return _return
    End Function

    Shared Function AllRequiredPersonsApprovedContract(ByVal _ContractID As Integer, ByVal _Shift As Integer) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = "ApprovalStatusContract"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            ContractID.Value = _ContractID
            Dim Shift As SqlParameter = cmd.Parameters.Add("@Shift", Data.SqlDbType.Int)
            Shift.Value = _Shift

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Boolean = False
            While dr.Read
                If dr(0) > 0 Then
                    _return = False
                ElseIf dr(0) = 0 Then
                    _return = True
                End If
            End While
            Return _return

            con.Close()
            con.Dispose()
            dr.Close()
        End Using

    End Function

    Shared Function AllParametersProvidedToPassApprovalStage(ByVal _ContractID As Integer, ByVal _Shift As Integer) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "ContractReadyForApproval"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            ContractID.Value = _ContractID
            Dim Shift As SqlParameter = cmd.Parameters.Add("@Shift", Data.SqlDbType.Int)
            Shift.Value = _Shift

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Boolean = False
            While dr.Read
                If dr(0) > 0 Then
                    _return = False
                ElseIf dr(0) = 0 Then
                    _return = True
                End If
            End While
            Return _return

            con.Close()
            con.Dispose()
            dr.Close()
        End Using
    End Function

    Shared Sub InsertOrUpdatePoFromContract(ByVal _Project_ID As Integer _
                           , ByVal _ContractID As Integer _
                           , ByVal _AddendumID As Integer _
                           , ByVal _PersonCreated As String _
                           , Optional ByVal _usercontrol As UserControl = Nothing)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "InsertOrUpdatePoFromContract"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", Data.SqlDbType.Int)
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            Dim PersonCreated As SqlParameter = cmd.Parameters.Add("@PersonCreated", Data.SqlDbType.NVarChar, 50)
            Dim outputPO_No_out As SqlParameter = cmd.Parameters.Add("@PO_No_out", Data.SqlDbType.NVarChar, 11)
            outputPO_No_out.Direction = Data.ParameterDirection.Output
            Dim outputInsertOrUpdate As SqlParameter = cmd.Parameters.Add("@InsertOrUpdate", Data.SqlDbType.NVarChar, 100)
            outputInsertOrUpdate.Direction = Data.ParameterDirection.Output

            ' assign parameter values
            ProjectID.Value = _Project_ID
            ContractID.Value = _ContractID
            AddendumID.Value = _AddendumID
            PersonCreated.Value = _PersonCreated

            cmd.ExecuteNonQuery()

            con.Close()
            con.Dispose()

            ' This Stored Procedure re organize PO description as per contract No and Contrat Date
            Using adapter As New ApprovalMatrixTableAdapters.QueriesTableAdapter

                Try
                    adapter.UpdatePO_Descriptions_asPerContractDetails()

                Catch ex As Exception
                    Dim a As MyCommonTasks
                    ' a.SendEmailToAdmin("UpdatePO_Descriptions_asPerContractDetails doesnt work", "UpdatePO_Descriptions_asPerContractDetails doesnt work")
                End Try

                adapter.Dispose()

            End Using

            ' Produce PO details for email body
            Dim _EmailBodyForPoDetails As String = ""
            If Not IsDBNull(outputPO_No_out.Value) Then
                If IsNumeric(outputPO_No_out.Value.ToString) = True Then
                    _EmailBodyForPoDetails = "has been approved. " + Environment.NewLine + _
                    "If you need to raise PO, please visit " + " <a href=" + """" + "http://pts.mercuryeng.ru/Contractview.aspx" + """" + " target=" + """" + "_blank" + """" + ">contracts page</a> "
                Else
                    _EmailBodyForPoDetails = PTS_MERCURY.helper.Table2_PONo.ReturnHTMLfromURL(outputPO_No_out.Value.ToString)
                End If
            End If

            ' send email notification
            If Not IsDBNull(outputPO_No_out.Value) Then
                If IsNumeric(outputPO_No_out.Value.ToString) = True Then
                    'Parameter 4, Frame contract approved
                    SendEmailApprovalMatrix.Send(_ContractID, 4, outputPO_No_out.Value.ToString, outputInsertOrUpdate.Value.ToString.Trim, _EmailBodyForPoDetails)

                Else

                    If outputInsertOrUpdate.Value.ToString.ToLower.Trim = "insert" Or outputInsertOrUpdate.Value.ToString.ToLower.Trim = "update" Then
                        'Parameter 3, PO inserted or updated
                        SendEmailApprovalMatrix.Send(_ContractID, 3, outputPO_No_out.Value.ToString, outputInsertOrUpdate.Value.ToString.Trim, _EmailBodyForPoDetails)

                    ElseIf outputInsertOrUpdate.Value.ToString.ToLower.Trim = "insertpofromframecontractaddendum" Then
                        ' Parameter 5, PO inserted from Addendum to Frame Contract
                        SendEmailApprovalMatrix.Send(_AddendumID, 5, outputPO_No_out.Value.ToString, outputInsertOrUpdate.Value.ToString.Trim, _EmailBodyForPoDetails)

                    ElseIf outputInsertOrUpdate.Value.ToString.ToLower.Trim = "insertfromcontractnewpopolicy" Then
                        ' Parameter 12, This will raise new PO against contract. It is new logic in PTS
                        SendEmailApprovalMatrix.Send(_ContractID, 12, outputPO_No_out.Value.ToString, outputInsertOrUpdate.Value.ToString.Trim, _EmailBodyForPoDetails)

                    ElseIf outputInsertOrUpdate.Value.ToString.ToLower.Trim = "insertfromaddendumnewpopolicy" Then
                        ' Parameter 13, This will raise new PO against REGULAR TYPE addendum. It is new logic in PTS
                        SendEmailApprovalMatrix.Send(_AddendumID, 13, outputPO_No_out.Value.ToString, outputInsertOrUpdate.Value.ToString.Trim, _EmailBodyForPoDetails)

                    ElseIf outputInsertOrUpdate.Value.ToString.ToLower.Trim = "updatefromreplaceaddendumnewpopolicy" Then
                        ' Parameter 14, This will update existing PO which has been raised against contract before. It is new logic in PTS
                        SendEmailApprovalMatrix.Send(_AddendumID, 14, outputPO_No_out.Value.ToString, outputInsertOrUpdate.Value.ToString.Trim, _EmailBodyForPoDetails)

                    ElseIf outputInsertOrUpdate.Value.ToString.ToLower.Trim = "zerovalueaddendumexecuted" Then
                        ' Parameter 15, ZERO VALUE ADDENDUM executed
                        SendEmailApprovalMatrix.Send(_AddendumID, 15, outputPO_No_out.Value.ToString, outputInsertOrUpdate.Value.ToString.Trim, Nothing)

                    End If

                End If
            End If

        End Using
    End Sub

    Shared Function AddendumReadyToTurnPo(ByVal _AddendumID As Integer) As Boolean
        Dim _return As Boolean = False

        ' THIS IS CANCELLED, NEW LOGIC REQUIRES DIFFERENT
        ' if Contract Scenario is 0, then raise PO immediately, no approval required
        'If GetContractScenarioOverAddendum(_AddendumID) = 0 Then
        '  _return = True
        '  Return _return
        '  Exit Function
        'End If

        If AllRequiredPersonsApprovedAddendum(_AddendumID, 0) _
          And AllParametersProvidedToPassApprovalStageAddendum(_AddendumID, 2) Then
            ' Sign Addendum On Behalf Of Mercury
            ' this is cancelled as per Larisa request.
            'SignAddendumOnBeHalfOfMercury(_AddendumID)
            _return = True
        Else
            _return = False
        End If
        Return _return
    End Function

    Shared Function AllRequiredPersonsApprovedAddendum(ByVal _AddendumID As Integer, ByVal _Shift As Integer) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = "ApprovalStatusAddendum"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = _AddendumID
            Dim Shift As SqlParameter = cmd.Parameters.Add("@Shift", Data.SqlDbType.Int)
            Shift.Value = _Shift

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Boolean = False
            While dr.Read
                If dr(0) > 0 Then
                    _return = False
                ElseIf dr(0) = 0 Then
                    _return = True
                End If
            End While
            Return _return

            con.Close()
            con.Dispose()
            dr.Close()
        End Using

    End Function

    Shared Function AllParametersProvidedToPassApprovalStageAddendum(ByVal _AddendumID As Integer, ByVal _Shift As Integer) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "AddendumReadyForApproval"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = _AddendumID
            Dim Shift As SqlParameter = cmd.Parameters.Add("@Shift", Data.SqlDbType.Int)
            Shift.Value = _Shift

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Boolean = False
            While dr.Read
                If dr(0) > 0 Then
                    _return = False
                ElseIf dr(0) = 0 Then
                    _return = True
                End If
            End While
            Return _return

            con.Close()
            con.Dispose()
            dr.Close()
        End Using
    End Function

    Shared Function ScenarioNoForThisAddendum(ByVal _AddendumID As Integer) As Integer
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT dbo.Table_Addendums.Scenario " + _
                                      " FROM dbo.Table_Addendums " + _
                                      " WHERE (dbo.Table_Addendums.AddendumID = @AddendumID) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = _AddendumID
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Integer = 0
            While dr.Read
                _return = dr(0)
            End While
            Return _return
            con.Close()
            con.Dispose()
            dr.Close()
        End Using

    End Function

    Shared Function ScenarioNoForThisContract(ByVal _ContractID As Integer) As Integer
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT dbo.Table_Contracts.Scenario " + _
                                      " FROM dbo.Table_Contracts " + _
                                      " WHERE (dbo.Table_Contracts.ContractID = @ContractID) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            ContractID.Value = _ContractID
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Integer = 0
            While dr.Read
                _return = dr(0)
            End While
            Return _return
            con.Close()
            con.Dispose()
            dr.Close()
        End Using
    End Function

    Shared Function GetContractIDfromAddendumID(ByVal _AddendumID As Integer) As String

        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " SELECT ContractID FROM Table_Addendums WHERE AddendumID = @AddendumID "
        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = Data.CommandType.Text

        'syntax for parameter adding
        Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
        AddendumID.Value = _AddendumID
        Dim dr As SqlDataReader = cmd.ExecuteReader
        Dim returnvalue As Integer = 0
        While dr.Read
            If IsDBNull(dr(0)) Then
                returnvalue = 0
            Else
                returnvalue = dr(0)
            End If
        End While
        Return returnvalue
        con.Close()
        con.Dispose()
        dr.Close()

    End Function

    Shared Sub ApproveContract(ByVal _ContractID As String, ByVal _UserName As String)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " DELETE FROM [Table_Contract_UsersApprv] WHERE ContractID= @ContractID AND UserName= @UserName " + _
                                  " INSERT INTO [Table_Contract_UsersApprv] " + _
                                  "            ([ContractID] " + _
                                  "            ,[UserName] " + _
                                  "            ,[WhenApproved]) " + _
                                  "      VALUES " + _
                                  "            (@ContractID " + _
                                  "            ,@UserName " + _
                                  "            ,@WhenApproved) " + _
                                  " INSERT INTO [Table_Contract_UsersApprv_log] " + _
                                  "            ([ContractID] " + _
                                  "            ,[UserName] " + _
                                  "            ,[WhenAction] " + _
                                  "            ,[Exception] " + _
                                  "            ,[Who] " + _
                                  "            ,[EntryOrRemoval_1_Or_0]) " + _
                                  "      VALUES " + _
                                  "            (@ContractID " + _
                                  "            ,@UserName " + _
                                  "            ,@WhenApproved " + _
                                  "            ,0 " + _
                                  "            ,@Who " + _
                                  "            ,1) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            ContractID.Value = Convert.ToInt32(_ContractID)
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar)
            UserName.Value = _UserName
            Dim Who As SqlParameter = cmd.Parameters.Add("@Who", Data.SqlDbType.NVarChar)
            Who.Value = HttpContext.Current.User.Identity.Name.ToLower

            Dim WhenApproved As SqlParameter = cmd.Parameters.Add("@WhenApproved", Data.SqlDbType.SmallDateTime)
            WhenApproved.Value = LocalTime.GetTime

            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
        End Using

        If Roles.IsUserInRole("ContractLeadGirls") Then
            PTS_MERCURY.helper.EmailGenerator.LawyersApprovedContract.Send(_ContractID)
        End If

        Try
            ' Send email notifications
            Using db As New PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If db.ApprovalStatusContractAllApprovedEF(_ContractID).ToList()(0) = 1 Then
                    PTS_MERCURY.helper.EmailGenerator.EverybodyApprovedContract.Send(_ContractID)
                End If

            End Using

        Catch ex As Exception

        End Try

    End Sub

    Shared Sub RemoveApprovalContract(ByVal _ContractID As String, ByVal _UserName As String)
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " DELETE FROM [Table_Contract_UsersApprv] WHERE ContractID= @ContractID AND UserName= @UserName " + _
                                  " INSERT INTO [Table_Contract_UsersApprv_log] " + _
                                  "            ([ContractID] " + _
                                  "            ,[UserName] " + _
                                  "            ,[WhenAction] " + _
                                  "            ,[Exception] " + _
                                  "            ,[Who] " + _
                                  "            ,[EntryOrRemoval_1_Or_0]) " + _
                                  "      VALUES " + _
                                  "            (@ContractID " + _
                                  "            ,@UserName " + _
                                  "            ,@WhenApproved " + _
                                  "            ,0 " + _
                                  "            ,@Who " + _
                                  "            ,0) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            ContractID.Value = Convert.ToInt32(_ContractID)
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar)
            UserName.Value = _UserName
            Dim Who As SqlParameter = cmd.Parameters.Add("@Who", Data.SqlDbType.NVarChar)
            Who.Value = HttpContext.Current.User.Identity.Name.ToLower

            Dim WhenApproved As SqlParameter = cmd.Parameters.Add("@WhenApproved", Data.SqlDbType.SmallDateTime)
            WhenApproved.Value = LocalTime.GetTime


            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
        End Using
    End Sub

    Shared Sub Reject_ThisContractByContractGirls(ByVal _ContractID As String, ByVal _UserName As String, Optional ByVal _usercontrol As UserControl = Nothing)

        If Roles.IsUserInRole(_UserName, "ContractLeadGirls") Then
            _UserName = "lawyers"
        End If

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " DELETE FROM [Table_Contract_UsersApprv] WHERE ContractID = @ContractID AND UserName = @UserName " + _
                                  " INSERT INTO [Table_Contract_UsersApprv] " + _
                                  "            ([ContractID] " + _
                                  "            ,[UserName] " + _
                                  "            ,[WhenApproved] " + _
                                  "            ,[Exception]) " + _
                                  "      VALUES " + _
                                  "            (@ContractID " + _
                                  "            ,@UserName " + _
                                  "            ,@WhenApproved " + _
                                  "            ,1) " + _
                                  " INSERT INTO [Table_Contract_UsersApprv_log] " + _
                                  "            ([ContractID] " + _
                                  "            ,[UserName] " + _
                                  "            ,[WhenAction] " + _
                                  "            ,[Exception] " + _
                                  "            ,[Who] " + _
                                  "            ,[EntryOrRemoval_1_Or_0]) " + _
                                  "      VALUES " + _
                                  "            (@ContractID " + _
                                  "            ,@UserName " + _
                                  "            ,@WhenApproved " + _
                                  "            ,1 " + _
                                  "            ,@Who " + _
                                  "            ,1) "


            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            ContractID.Value = Convert.ToInt32(_ContractID)
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar)
            UserName.Value = _UserName
            Dim WhenApproved As SqlParameter = cmd.Parameters.Add("@WhenApproved", Data.SqlDbType.SmallDateTime)
            WhenApproved.Value = LocalTime.GetTime
            Dim Who As SqlParameter = cmd.Parameters.Add("@Who", Data.SqlDbType.NVarChar)
            Who.Value = HttpContext.Current.User.Identity.Name.ToLower

            Dim dr As SqlDataReader = cmd.ExecuteReader

            ' Send Email about rejection
            If _usercontrol IsNot Nothing Then
                SendEmailApprovalMatrix.Send(_ContractID, 6, Nothing, "rjC", ProduceHTMLforContractEmailBody(_usercontrol, Convert.ToInt32(_ContractID)))
            End If

            con.Close()
            dr.Close()
        End Using

        Try
            ' Send email notifications
            Using db As New PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If db.ApprovalStatusContractAllApprovedEF(_ContractID).ToList()(0) = 1 Then
                    PTS_MERCURY.helper.EmailGenerator.EverybodyApprovedContract.Send(_ContractID)
                End If

            End Using

        Catch ex As Exception

        End Try

    End Sub

    Shared Sub RemoveReject_ThisContractByContractGirls(ByVal _ContractID As String, ByVal _UserName As String, Optional ByVal _usercontrol As UserControl = Nothing)

        If Roles.IsUserInRole(_UserName, "ContractLeadGirls") Then
            _UserName = "lawyers"
        End If

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " DELETE FROM [Table_Contract_UsersApprv] WHERE ContractID = @ContractID AND UserName = @UserName " + _
                                  " INSERT INTO [Table_Contract_UsersApprv_log] " + _
                                  "            ([ContractID] " + _
                                  "            ,[UserName] " + _
                                  "            ,[WhenAction] " + _
                                  "            ,[Exception] " + _
                                  "            ,[Who] " + _
                                  "            ,[EntryOrRemoval_1_Or_0]) " + _
                                  "      VALUES " + _
                                  "            (@ContractID " + _
                                  "            ,@UserName " + _
                                  "            ,@WhenApproved " + _
                                  "            ,1 " + _
                                  "            ,@Who " + _
                                  "            ,0) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            ContractID.Value = Convert.ToInt32(_ContractID)
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar)
            UserName.Value = _UserName
            Dim WhenApproved As SqlParameter = cmd.Parameters.Add("@WhenApproved", Data.SqlDbType.SmallDateTime)
            WhenApproved.Value = LocalTime.GetTime
            Dim Who As SqlParameter = cmd.Parameters.Add("@Who", Data.SqlDbType.NVarChar)
            Who.Value = HttpContext.Current.User.Identity.Name.ToLower

            Dim dr As SqlDataReader = cmd.ExecuteReader

            ' Send Email about removal of rejection
            If _usercontrol IsNot Nothing Then
                SendEmailApprovalMatrix.Send(_ContractID, 10, Nothing, "rjCR", ProduceHTMLforContractEmailBody(_usercontrol, Convert.ToInt32(_ContractID)))
            End If

            con.Close()
            dr.Close()
        End Using
    End Sub

    Shared Sub ApproveAddendum(ByVal _AddendumID As String, ByVal _UserName As String)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " INSERT INTO [Table_Addendum_UsersApprv] " + _
                                  "            ([AddendumID] " + _
                                  "            ,[UserName] " + _
                                  "            ,[WhenApproved]) " + _
                                  "      VALUES " + _
                                  "            (@AddendumID " + _
                                  "            ,@UserName " + _
                                  "            ,@WhenApproved) " + _
                                  " INSERT INTO [Table_Addendum_UsersApprv_log] " + _
                                  "            ([AddendumID] " + _
                                  "            ,[UserName] " + _
                                  "            ,[WhenAction] " + _
                                  "            ,[Exception] " + _
                                  "            ,[Who] " + _
                                  "            ,[EntryOrRemoval_1_Or_0]) " + _
                                  "      VALUES " + _
                                  "            (@AddendumID " + _
                                  "            ,@UserName " + _
                                  "            ,@WhenApproved " + _
                                  "            ,0 " + _
                                  "            ,@Who " + _
                                  "            ,1) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = Convert.ToInt32(_AddendumID)
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar, 256)
            UserName.Value = _UserName
            Dim Who As SqlParameter = cmd.Parameters.Add("@Who", Data.SqlDbType.NVarChar)
            Who.Value = HttpContext.Current.User.Identity.Name.ToLower
            Dim WhenApproved As SqlParameter = cmd.Parameters.Add("@WhenApproved", Data.SqlDbType.SmallDateTime)
            WhenApproved.Value = LocalTime.GetTime

            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
        End Using

        If Roles.IsUserInRole("ContractLeadGirls") Then
            PTS_MERCURY.helper.EmailGenerator.LawyersApprovedAddendum.Send(_AddendumID)
        End If

        Try
            ' Send email notifications
            Using db As New PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If db.ApprovalStatusAddendumAllApprovedEF(_AddendumID).ToList()(0) = 1 Then
                    PTS_MERCURY.helper.EmailGenerator.EverybodyApprovedAddendum.Send(_AddendumID)
                End If

            End Using

        Catch ex As Exception

        End Try

    End Sub

    Shared Sub RemoveApprovalAddendum(ByVal _AddendumID As String, ByVal _UserName As String)
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " DELETE FROM [Table_Addendum_UsersApprv] WHERE AddendumID= @AddendumID AND UserName= @UserName " + _
                                  " INSERT INTO [Table_Addendum_UsersApprv_log] " + _
                                  "            ([AddendumID] " + _
                                  "            ,[UserName] " + _
                                  "            ,[WhenAction] " + _
                                  "            ,[Exception] " + _
                                  "            ,[Who] " + _
                                  "            ,[EntryOrRemoval_1_Or_0]) " + _
                                  "      VALUES " + _
                                  "            (@AddendumID " + _
                                  "            ,@UserName " + _
                                  "            ,@WhenApproved " + _
                                  "            ,0 " + _
                                  "            ,@Who " + _
                                  "            ,0) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = Convert.ToInt32(_AddendumID)
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar)
            UserName.Value = _UserName
            Dim Who As SqlParameter = cmd.Parameters.Add("@Who", Data.SqlDbType.NVarChar)
            Who.Value = HttpContext.Current.User.Identity.Name.ToLower
            Dim WhenApproved As SqlParameter = cmd.Parameters.Add("@WhenApproved", Data.SqlDbType.SmallDateTime)
            WhenApproved.Value = LocalTime.GetTime

            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
        End Using
    End Sub

    Shared Sub Reject_ThisAddendumByContractGirls(ByVal _AddendumID As String, ByVal _UserName As String, Optional ByVal _usercontrol As UserControl = Nothing)

        If Roles.IsUserInRole(_UserName, "ContractLeadGirls") Then
            _UserName = "lawyers"
        End If

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " DELETE FROM [Table_Addendum_UsersApprv] WHERE AddendumID= @AddendumID AND UserName= @UserName  " + _
                                  " INSERT INTO [Table_Addendum_UsersApprv] " + _
                                  "            ([AddendumID] " + _
                                  "            ,[UserName] " + _
                                  "            ,[WhenApproved] " + _
                                  "            ,[Exception]) " + _
                                  "      VALUES " + _
                                  "            (@AddendumID " + _
                                  "            ,@UserName " + _
                                  "            ,@WhenApproved " + _
                                  "            ,1) " + _
                                  " INSERT INTO [Table_Addendum_UsersApprv_log] " + _
                                  "            ([AddendumID] " + _
                                  "            ,[UserName] " + _
                                  "            ,[WhenAction] " + _
                                  "            ,[Exception] " + _
                                  "            ,[Who] " + _
                                  "            ,[EntryOrRemoval_1_Or_0]) " + _
                                  "      VALUES " + _
                                  "            (@AddendumID " + _
                                  "            ,@UserName " + _
                                  "            ,@WhenApproved " + _
                                  "            ,1 " + _
                                  "            ,@Who " + _
                                  "            ,1) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = Convert.ToInt32(_AddendumID)
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar)
            UserName.Value = _UserName
            Dim Who As SqlParameter = cmd.Parameters.Add("@Who", Data.SqlDbType.NVarChar)
            Who.Value = HttpContext.Current.User.Identity.Name.ToLower
            Dim WhenApproved As SqlParameter = cmd.Parameters.Add("@WhenApproved", Data.SqlDbType.SmallDateTime)
            WhenApproved.Value = LocalTime.GetTime

            Dim dr As SqlDataReader = cmd.ExecuteReader

            ' Send Email about rejection
            If _usercontrol IsNot Nothing Then
                SendEmailApprovalMatrix.Send(_AddendumID, 7, Nothing, "rjA", ProduceHTMLforAddendumEmailBody(_usercontrol, Convert.ToInt32(_AddendumID)))
            End If

            con.Close()
            dr.Close()
        End Using

        Try
            ' Send email notifications
            Using db As New PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If db.ApprovalStatusAddendumAllApprovedEF(_AddendumID).ToList()(0) = 1 Then
                    PTS_MERCURY.helper.EmailGenerator.EverybodyApprovedAddendum.Send(_AddendumID)
                End If

            End Using

        Catch ex As Exception

        End Try

    End Sub

    Shared Sub RemoveReject_ThisAddendumByContractGirls(ByVal _AddendumID As String, ByVal _UserName As String, Optional ByVal _usercontrol As UserControl = Nothing)

        If Roles.IsUserInRole(_UserName, "ContractLeadGirls") Then
            _UserName = "lawyers"
        End If

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " DELETE FROM [Table_Addendum_UsersApprv] WHERE AddendumID= @AddendumID AND UserName= @UserName  " + _
                                  " INSERT INTO [Table_Addendum_UsersApprv_log] " + _
                                  "            ([AddendumID] " + _
                                  "            ,[UserName] " + _
                                  "            ,[WhenAction] " + _
                                  "            ,[Exception] " + _
                                  "            ,[Who] " + _
                                  "            ,[EntryOrRemoval_1_Or_0]) " + _
                                  "      VALUES " + _
                                  "            (@AddendumID " + _
                                  "            ,@UserName " + _
                                  "            ,@WhenApproved " + _
                                  "            ,1 " + _
                                  "            ,@Who " + _
                                  "            ,0) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = Convert.ToInt32(_AddendumID)
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar)
            UserName.Value = _UserName
            Dim Who As SqlParameter = cmd.Parameters.Add("@Who", Data.SqlDbType.NVarChar)
            Who.Value = HttpContext.Current.User.Identity.Name.ToLower
            Dim WhenApproved As SqlParameter = cmd.Parameters.Add("@WhenApproved", Data.SqlDbType.SmallDateTime)
            WhenApproved.Value = LocalTime.GetTime


            Dim dr As SqlDataReader = cmd.ExecuteReader

            ' Send Email about removal of rejection
            If _usercontrol IsNot Nothing Then
                SendEmailApprovalMatrix.Send(_AddendumID, 11, Nothing, "rjAR", ProduceHTMLforAddendumEmailBody(_usercontrol, Convert.ToInt32(_AddendumID)))
            End If

            con.Close()
            dr.Close()
        End Using
    End Sub

    Shared Function SmallContract() As Integer

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT [SmallContract] FROM [dbo].[Table_Scenario] WHERE Scenario = @Scenario "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text
            Dim userName As String = HttpContext.Current.User.Identity.Name.ToLower.ToString

            'syntax for parameter adding
            Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario", Data.SqlDbType.NVarChar)
            Scenario.Value = userName + "Scenario1"
            Dim ReturnValue As Boolean
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                ReturnValue = dr(0)
            End While
            Return ReturnValue
            con.Close()
            dr.Close()
            con.Dispose()
        End Using

    End Function

    ' This class is being shared by ContractViewPage and Contract-Addendum details page
    Shared Sub GridviewApprovalStatus_RowDataBound(ByVal _row As GridViewRow)

        If _row.RowType = DataControlRowType.DataRow Then

            Try
                Dim LiteralWhichLawyer As Literal = DirectCast(_row.FindControl("LiteralWhichLawyer"), Literal)

                If DataBinder.Eval(_row.DataItem, "UserName").ToString.ToLower = "lawyers" Then
                    If Len(LiteralWhichLawyer.Text.Trim()) > 0 Then
                        LiteralWhichLawyer.Text = "<br/>(" + LiteralWhichLawyer.Text + ")"
                        LiteralWhichLawyer.Visible = True
                    End If
                End If

            Catch ex As Exception

            End Try


            Dim _ContractId As Integer = DataBinder.Eval(_row.DataItem, "ContractId")
            Dim _Comment As String = ""

            If DataBinder.Eval(_row.DataItem, "Rank") = 0 Then

                _row.Cells(0).BackColor = Drawing.Color.LightGray

            End If

            Try
                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                    Dim _user As String = "lawyers"
                    If (Aggregate C In db.Table_Contracts_UserRemarks Where C.ContractID = _ContractId And C.UserName = _user Into Count()) > 0 Then
                        '_Comment = (From C In db.Table_Contracts_UserRemarks Where C.ContractID = _ContractId And C.UserName = _user).ToList()(0).Remark.Trim
                        _Comment = (From C In db.Table_Contracts_UserRemarks Where C.ContractID = _ContractId And C.UserName = _user).FirstOrDefault.Remark.Trim
                    End If

                    db.Dispose()
                End Using

            Catch ex As Exception

            End Try

            Dim ImageButtonApproval As ImageButton = DirectCast(_row.FindControl("ImageButtonApproval"), ImageButton)
            Dim LinkButtonRejectContractGirls As LinkButton = DirectCast(_row.FindControl("LinkButtonRejectContractGirls"), LinkButton)

            ' Define imageURL as per approval status
            If DataBinder.Eval(_row.DataItem, "Approved") = 0 Then
                ImageButtonApproval.ImageUrl = "~/images/ContractNotApprove.png"
            ElseIf DataBinder.Eval(_row.DataItem, "Approved") = 1 And Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = False Then
                ImageButtonApproval.ImageUrl = "~/images/ContractApprove.png"
            ElseIf Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = True Then
                ImageButtonApproval.ImageUrl = "~/images/ContractNotApprove.png"
            End If

            ' if user in Role ContractLeadGirls, then activate <Lawyers>
            If HttpContext.Current.User.IsInRole("ContractLeadGirls") Then
                If DataBinder.Eval(_row.DataItem, "UserName").ToString.ToLower = "lawyers" Then
                    ImageButtonApproval.Enabled = True
                    If Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = True Then
                        LinkButtonRejectContractGirls.Text = "Not Agreed"
                        LinkButtonRejectContractGirls.Attributes.Add("class", "tooltip-success")
                        LinkButtonRejectContractGirls.Attributes.Add("data-rel", "tooltip")
                        LinkButtonRejectContractGirls.Attributes.Add("data-placement", "top")
                        LinkButtonRejectContractGirls.Attributes.Add("data-original-title", _Comment)
                    ElseIf Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = False Then
                        LinkButtonRejectContractGirls.Text = "Reject"
                    End If
                Else
                    ImageButtonApproval.Enabled = False
                End If
            Else
                If DataBinder.Eval(_row.DataItem, "UserName").ToString.ToLower = "lawyers" Then
                    If Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = True Then
                        LinkButtonRejectContractGirls.Text = "Not Agreed"
                        LinkButtonRejectContractGirls.Attributes.Add("class", "tooltip-success")
                        LinkButtonRejectContractGirls.Attributes.Add("data-rel", "tooltip")
                        LinkButtonRejectContractGirls.Attributes.Add("data-placement", "top")
                        LinkButtonRejectContractGirls.Attributes.Add("data-original-title", _Comment)
                        LinkButtonRejectContractGirls.Enabled = False
                    ElseIf Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = False Then
                        LinkButtonRejectContractGirls.Text = "Reject"
                        LinkButtonRejectContractGirls.Enabled = False
                    End If
                End If
                ' Diasable or enable ImageButton as per user
                If DataBinder.Eval(_row.DataItem, "UserName").ToString.ToLower = HttpContext.Current.User.Identity.Name.ToLower Then
                    ImageButtonApproval.Enabled = True
                ElseIf DataBinder.Eval(_row.DataItem, "UserName").ToString.ToLower <> HttpContext.Current.User.Identity.Name.ToLower Then
                    ImageButtonApproval.Enabled = False
                End If
            End If

            'Show or hide Reject button as per status
            If DataBinder.Eval(_row.DataItem, "UserName").ToString.ToLower = "lawyers" Then
                If DataBinder.Eval(_row.DataItem, "Approved") = 0 Then
                    ImageButtonApproval.Visible = True
                    If HttpContext.Current.User.IsInRole("ContractLeadGirls") Then
                        LinkButtonRejectContractGirls.Visible = True
                    Else
                        LinkButtonRejectContractGirls.Visible = False
                    End If
                ElseIf DataBinder.Eval(_row.DataItem, "Approved") = 1 And Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = False Then
                    ImageButtonApproval.Visible = True
                    LinkButtonRejectContractGirls.Visible = False
                ElseIf Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = True Then
                    ImageButtonApproval.Visible = False
                    If HttpContext.Current.User.IsInRole("ContractLeadGirls") Then
                        LinkButtonRejectContractGirls.Visible = True
                    Else
                        LinkButtonRejectContractGirls.Visible = True
                        LinkButtonRejectContractGirls.Enabled = False
                    End If
                End If
            End If

            If CreateDataReader.Create_Table_Contract(DataBinder.Eval(_row.DataItem, "ContractID")).POexecuted = True And _
                CreateDataReader.Create_Table_Contract(DataBinder.Eval(_row.DataItem, "ContractID")).Exceptional = False Then
                ' Disable controls to approval
                _row.Enabled = False
            End If

        End If

    End Sub

    ' This class should be updated if GridviewApprovalStatus_RowDataBound updated above
    Shared Sub DataListContractApprovals_ItemDataBound(ByVal _Item As DataListItem)

        If _Item.ItemType = DataControlRowType.DataRow Then
            Dim ImageButtonApproval As ImageButton = DirectCast(_Item.FindControl("ImageButtonApproval"), ImageButton)
            Dim LinkButtonRejectContractGirls As LinkButton = DirectCast(_Item.FindControl("LinkButtonRejectContractGirls"), LinkButton)

            ' Define imageURL as per approval status
            If DataBinder.Eval(_Item.DataItem, "Approved") = 0 And DataBinder.Eval(_Item.DataItem, "ApprovalRequired") = 1 Then
                ImageButtonApproval.ImageUrl = "~/images/ContractNotApprove.png"
            ElseIf DataBinder.Eval(_Item.DataItem, "Approved") = 1 And DataBinder.Eval(_Item.DataItem, "Exception") = 0 And DataBinder.Eval(_Item.DataItem, "ApprovalRequired") = 1 Then
                ImageButtonApproval.ImageUrl = "~/images/ContractApprove.png"
            ElseIf DataBinder.Eval(_Item.DataItem, "Exception") = 1 And DataBinder.Eval(_Item.DataItem, "ApprovalRequired") = 1 Then
                ImageButtonApproval.ImageUrl = "~/images/ContractNotApprove.png"
            ElseIf DataBinder.Eval(_Item.DataItem, "ApprovalRequired") = 0 Then
                ImageButtonApproval.Visible = False
            End If

            Exit Sub

            ' if user in Role ContractLeadGirls, then activate <Lawyers>
            If HttpContext.Current.User.IsInRole("ContractLeadGirls") Then
                If DataBinder.Eval(_Item.DataItem, "UserName").ToString.ToLower = "lawyers" Then
                    ImageButtonApproval.Enabled = True
                    If DataBinder.Eval(_Item.DataItem, "Exception") = 1 Then
                        LinkButtonRejectContractGirls.Text = "Not Agreed"
                    ElseIf DataBinder.Eval(_Item.DataItem, "Exception") = 0 Then
                        LinkButtonRejectContractGirls.Text = "Reject"
                    End If
                Else
                    ImageButtonApproval.Enabled = False
                End If
            Else
                If DataBinder.Eval(_Item.DataItem, "UserName").ToString.ToLower = "lawyers" Then
                    If DataBinder.Eval(_Item.DataItem, "Exception") = 1 Then
                        LinkButtonRejectContractGirls.Text = "Not Agreed"
                        LinkButtonRejectContractGirls.Enabled = False
                    ElseIf DataBinder.Eval(_Item.DataItem, "Exception") = 0 Then
                        LinkButtonRejectContractGirls.Text = "Reject"
                        LinkButtonRejectContractGirls.Enabled = False
                    End If
                End If
                ' Diasable or enable ImageButton as per user
                If DataBinder.Eval(_Item.DataItem, "UserName").ToString.ToLower = HttpContext.Current.User.Identity.Name.ToLower Then
                    ImageButtonApproval.Enabled = True
                ElseIf DataBinder.Eval(_Item.DataItem, "UserName").ToString.ToLower <> HttpContext.Current.User.Identity.Name.ToLower Then
                    ImageButtonApproval.Enabled = False
                End If
            End If

            ' Show or hide Reject button as per status
            If DataBinder.Eval(_Item.DataItem, "UserName").ToString.ToLower = "lawyers" Then
                If DataBinder.Eval(_Item.DataItem, "Approved") = 0 And DataBinder.Eval(_Item.DataItem, "ApprovalRequired") = 1 Then
                    ImageButtonApproval.Visible = True
                    LinkButtonRejectContractGirls.Visible = True
                ElseIf DataBinder.Eval(_Item.DataItem, "Approved") = 1 And DataBinder.Eval(_Item.DataItem, "Exception") = 0 And DataBinder.Eval(_Item.DataItem, "ApprovalRequired") = 1 Then
                    ImageButtonApproval.Visible = True
                    LinkButtonRejectContractGirls.Visible = False
                ElseIf DataBinder.Eval(_Item.DataItem, "Exception") = 1 And DataBinder.Eval(_Item.DataItem, "ApprovalRequired") = 1 Then
                    ImageButtonApproval.Visible = False
                    LinkButtonRejectContractGirls.Visible = True
                End If
            End If

            If CreateDataReader.Create_Table_Contract(DataBinder.Eval(_Item.DataItem, "ContractID")).POexecuted = True Then
                ' Disable controls to approval
                _Item.Enabled = False
            End If

        End If

    End Sub

    ' This class is being shared by ContractViewPage and Contract-Addendum details page
    Shared Sub GridviewApprovalStatus_RowCommand(ByVal _sender As GridView, _
                           ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs, _
                           ByVal _podetailsforEmail As UserControl, _
                           ByVal _page As Page, _
                           Optional _contractdetailsControl As UserControl = Nothing)

        Dim Notification As New _GiveNotification

        If (e.CommandName = "Approval") Then
            Dim rowIndex As String = e.CommandArgument
            Dim GridviewApprovalStatus As GridView = _sender
            Dim row As GridViewRow = GridviewApprovalStatus.Rows(rowIndex)

            Dim LiteralApproved As Literal = DirectCast(row.FindControl("LiteralApproved"), Literal)
            Dim LiteralContractID As Literal = DirectCast(row.FindControl("LiteralContractID"), Literal)
            Dim LiteralUserName As Literal = DirectCast(row.FindControl("LiteralUserName"), Literal)

            ' It is approval or NOT approval ?
            If LiteralApproved.Text = "0" Then
                ' APPROVE
                ContractView.ApproveContract(LiteralContractID.Text, LiteralUserName.Text)
                Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                               _page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                               ">You have approved successfully!</p>")
            ElseIf LiteralApproved.Text = "1" Then
                ' NOT APPROVE
                ContractView.RemoveApprovalContract(LiteralContractID.Text, LiteralUserName.Text)
                Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                               _page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                               ">You have removed your approval successfully!</p>")
            End If

            ' CHECK CONTRACT IF IT IS READY FOR PO
            ContractView.Initiate_Checking_Contract_Or_Addendum_To_Insert_Or_Update_Po_Then_Finally_Execute( _
                                                    Convert.ToInt32(LiteralContractID.Text), _
                                                    0, _
                                                    _podetailsforEmail)

        End If

        If (e.CommandName = "ApprovalByRejecting") Then
            Dim rowIndex As String = e.CommandArgument
            Dim GridviewApprovalStatus As GridView = _sender
            Dim row As GridViewRow = GridviewApprovalStatus.Rows(rowIndex)

            Dim LiteralRejectContractGirls As Literal = DirectCast(row.FindControl("LiteralRejectContractGirls"), Literal)
            Dim LiteralContractID As Literal = DirectCast(row.FindControl("LiteralContractID"), Literal)
            Dim LiteralUserName As Literal = DirectCast(row.FindControl("LiteralUserName"), Literal)

            ' It is rejected or not
            If LiteralRejectContractGirls.Text = "0" Then
                ' REJECT
                ContractView.Reject_ThisContractByContractGirls(LiteralContractID.Text, LiteralUserName.Text, _contractdetailsControl)
                Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                               _page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                               ">You have rejected successfully!</p>")

            ElseIf LiteralRejectContractGirls.Text = "1" Then
                ' REMOVE REJECT
                ContractView.RemoveReject_ThisContractByContractGirls(LiteralContractID.Text, LiteralUserName.Text)
                Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                               _page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                               ">You have removed your rejection successfully!</p>")

            End If

            ' CHECK CONTRACT IF IT IS READY FOR PO
            ContractView.Initiate_Checking_Contract_Or_Addendum_To_Insert_Or_Update_Po_Then_Finally_Execute( _
                                                    Convert.ToInt32(LiteralContractID.Text), _
                                                    0, _
                                                    _podetailsforEmail)

        End If

    End Sub

    ' This class is being shared by ContractViewPage and Contract-Addendum details page
    Shared Sub GridviewApprovalStatusAddendum_RowDataBound(ByVal _row As GridViewRow)

        If _row.RowType = DataControlRowType.DataRow Then

            Try
                Dim LiteralWhichLawyer As Literal = DirectCast(_row.FindControl("LiteralWhichLawyer"), Literal)

                If DataBinder.Eval(_row.DataItem, "UserName").ToString.ToLower = "lawyers" Then
                    If Len(LiteralWhichLawyer.Text.Trim()) > 0 Then
                        LiteralWhichLawyer.Text = "<br/>(" + LiteralWhichLawyer.Text + ")"
                        LiteralWhichLawyer.Visible = True
                    End If
                End If

            Catch ex As Exception

            End Try

            Dim _AddendumId As Integer = DataBinder.Eval(_row.DataItem, "AddendumId")
            Dim _Comment As String = ""

            If DataBinder.Eval(_row.DataItem, "Rank") = 0 Then

                _row.Cells(0).BackColor = Drawing.Color.LightGray

            End If

            Try
                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                    Dim _user As String = "lawyers"
                    If (Aggregate C In db.Table_Addendum_UserRemarks Where C.AddendumID = _AddendumId And C.UserName = _user Into Count()) > 0 Then
                        '_Comment = (From C In db.Table_Addendum_UserRemarks Where C.AddendumID = _AddendumId And C.UserName = _user).ToList()(0).Remark.Trim
                        _Comment = (From C In db.Table_Addendum_UserRemarks Where C.AddendumID = _AddendumId And C.UserName = _user).FirstOrDefault.Remark.Trim
                    End If

                    db.Dispose()
                End Using

            Catch ex As Exception

            End Try

            ' enable scriptmanager for postback
            'ScriptManager1.RegisterPostBackControl(DirectCast(_row.FindControl("ImageButtonApproval"), ImageButton))
            Dim ImageButtonApproval As ImageButton = DirectCast(_row.FindControl("ImageButtonApproval"), ImageButton)
            Dim LinkButtonRejectContractGirls As LinkButton = DirectCast(_row.FindControl("LinkButtonRejectContractGirls"), LinkButton)

            ' Define imageURL as per approval status
            If DataBinder.Eval(_row.DataItem, "Approved") = 0 Then
                ImageButtonApproval.ImageUrl = "~/images/ContractNotApprove.png"
            ElseIf DataBinder.Eval(_row.DataItem, "Approved") = 1 And Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = False Then
                ImageButtonApproval.ImageUrl = "~/images/ContractApprove.png"
            ElseIf Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = True Then
                ImageButtonApproval.ImageUrl = "~/images/ContractNotApprove.png"
            End If

            ' if user in Role ContractLeadGirls, then activate <Lawyers>
            If HttpContext.Current.User.IsInRole("ContractLeadGirls") Then
                If DataBinder.Eval(_row.DataItem, "UserName").ToString.ToLower = "lawyers" Then
                    ImageButtonApproval.Enabled = True
                    If Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = True Then
                        LinkButtonRejectContractGirls.Text = "Not Agreed"
                        LinkButtonRejectContractGirls.Attributes.Add("class", "tooltip-success")
                        LinkButtonRejectContractGirls.Attributes.Add("data-rel", "tooltip")
                        LinkButtonRejectContractGirls.Attributes.Add("data-placement", "top")
                        LinkButtonRejectContractGirls.Attributes.Add("data-original-title", _Comment)
                    ElseIf Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = False Then
                        LinkButtonRejectContractGirls.Text = "Reject"
                    End If
                Else
                    ImageButtonApproval.Enabled = False
                End If
            Else
                If DataBinder.Eval(_row.DataItem, "UserName").ToString.ToLower = "lawyers" Then
                    If Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = True Then
                        LinkButtonRejectContractGirls.Text = "Not Agreed"
                        LinkButtonRejectContractGirls.Attributes.Add("class", "tooltip-success")
                        LinkButtonRejectContractGirls.Attributes.Add("data-rel", "tooltip")
                        LinkButtonRejectContractGirls.Attributes.Add("data-placement", "top")
                        LinkButtonRejectContractGirls.Attributes.Add("data-original-title", _Comment)
                        LinkButtonRejectContractGirls.Enabled = False
                    ElseIf Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = False Then
                        LinkButtonRejectContractGirls.Text = "Reject"
                        LinkButtonRejectContractGirls.Enabled = False
                    End If
                End If
                ' Diasable or enable ImageButton as per user
                If DataBinder.Eval(_row.DataItem, "UserName").ToString.ToLower = HttpContext.Current.User.Identity.Name.ToLower Then
                    ImageButtonApproval.Enabled = True
                ElseIf DataBinder.Eval(_row.DataItem, "UserName").ToString.ToLower <> HttpContext.Current.User.Identity.Name.ToLower Then
                    ImageButtonApproval.Enabled = False
                End If
            End If

            ' Show or hide Reject button as per status
            If DataBinder.Eval(_row.DataItem, "UserName").ToString.ToLower = "lawyers" Then
                If DataBinder.Eval(_row.DataItem, "Approved") = 0 Then
                    ImageButtonApproval.Visible = True
                    If HttpContext.Current.User.IsInRole("ContractLeadGirls") Then
                        LinkButtonRejectContractGirls.Visible = True
                    Else
                        LinkButtonRejectContractGirls.Visible = False
                    End If
                ElseIf DataBinder.Eval(_row.DataItem, "Approved") = 1 And Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = False Then
                    ImageButtonApproval.Visible = True
                    LinkButtonRejectContractGirls.Visible = False
                ElseIf Convert.ToBoolean(DataBinder.Eval(_row.DataItem, "Exception")) = True Then
                    ImageButtonApproval.Visible = False
                    If HttpContext.Current.User.IsInRole("ContractLeadGirls") Then
                        LinkButtonRejectContractGirls.Visible = True
                    Else
                        LinkButtonRejectContractGirls.Visible = True
                        LinkButtonRejectContractGirls.Enabled = False
                    End If
                End If
            End If

            If CreateDataReader.Create_Table_Addendums(DataBinder.Eval(_row.DataItem, "AddendumID")).POexecuted = True And _
                CreateDataReader.Create_Table_Addendums(DataBinder.Eval(_row.DataItem, "AddendumID")).Exceptional = False Then
                ' Disable controls to approval
                _row.Enabled = False
            End If

        End If

    End Sub

    ' This class is being shared by ContractViewPage and Contract-Addendum details page
    Shared Sub GridviewApprovalStatusAddendum_RowCommand(ByVal _sender As GridView, _
                       ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs, _
                       ByVal _podetailsforEmail As UserControl, _
                       ByVal _page As Page, _
                       Optional _addendumdetailsControl As UserControl = Nothing)

        Dim Notification As New _GiveNotification

        If (e.CommandName = "Approval") Then
            Dim rowIndex As String = e.CommandArgument
            Dim GridviewApprovalStatus As GridView = _sender
            Dim row As GridViewRow = GridviewApprovalStatus.Rows(rowIndex)

            Dim LiteralApproved As Literal = DirectCast(row.FindControl("LiteralApproved"), Literal)
            Dim LiteralAddendumID As Literal = DirectCast(row.FindControl("LiteralAddendumID"), Literal)
            Dim LiteralUserName As Literal = DirectCast(row.FindControl("LiteralUserName"), Literal)

            ' It is approval or NOT approval ?
            If LiteralApproved.Text = "0" Then
                ' APPROVE
                ContractView.ApproveAddendum(LiteralAddendumID.Text, LiteralUserName.Text)
                Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                               _page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                               ">You have approved successfully!</p>")

            ElseIf LiteralApproved.Text = "1" Then
                ' NOT APPROVE
                ContractView.RemoveApprovalAddendum(LiteralAddendumID.Text, LiteralUserName.Text)
                Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                               _page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                               ">You have removed your approval successfully!</p>")

            End If

            ' CHECK ADDENDUM IF IT IS READY FOR PO
            ContractView.Initiate_Checking_Contract_Or_Addendum_To_Insert_Or_Update_Po_Then_Finally_Execute( _
                                                    CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LiteralAddendumID.Text)).ContractID, _
                                                    Convert.ToInt32(LiteralAddendumID.Text), _
                                                    _podetailsforEmail)

        End If

        If (e.CommandName = "ApprovalByRejecting") Then
            Dim rowIndex As String = e.CommandArgument
            Dim GridviewApprovalStatus As GridView = _sender
            Dim row As GridViewRow = GridviewApprovalStatus.Rows(rowIndex)

            Dim LiteralRejectContractGirls As Literal = DirectCast(row.FindControl("LiteralRejectContractGirls"), Literal)
            Dim LiteralAddendumID As Literal = DirectCast(row.FindControl("LiteralAddendumID"), Literal)
            Dim LiteralUserName As Literal = DirectCast(row.FindControl("LiteralUserName"), Literal)

            ' It is rejected or not
            If LiteralRejectContractGirls.Text = "0" Then
                ' REJECT
                ContractView.Reject_ThisAddendumByContractGirls(LiteralAddendumID.Text, LiteralUserName.Text, _addendumdetailsControl)
                Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                               _page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                               ">You have rejected successfully!</p>")

            ElseIf LiteralRejectContractGirls.Text = "1" Then
                ' REMOVE REJECT
                ContractView.RemoveReject_ThisAddendumByContractGirls(LiteralAddendumID.Text, LiteralUserName.Text)
                Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                               _page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                               ">You have removed your rejection successfully!</p>")

            End If

            ' CHECK ADDENDUM IF IT IS READY FOR PO
            ContractView.Initiate_Checking_Contract_Or_Addendum_To_Insert_Or_Update_Po_Then_Finally_Execute( _
                                                    CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LiteralAddendumID.Text)).ContractID, _
                                                    Convert.ToInt32(LiteralAddendumID.Text), _
                                                    _podetailsforEmail)

        End If

    End Sub

    Shared Sub InsertOrUpdatePOWithExceptionalApproval(ByVal _Project_ID As Integer _
                           , ByVal _ContractID As Integer _
                           , ByVal _AddendumID As Integer _
                           , ByVal _PersonCreated As String _
                           , Optional ByVal _usercontrol As UserControl = Nothing)

        ' UPDATE Exceptional = 1 before sending to InsertORUpdatePo
        If _AddendumID = 0 Then ' it is contract
            UpdateContractExceptional(_ContractID, True)
        Else ' it is addendum
            UpdateAddendumExceptional(_AddendumID, True)

        End If

        ' InsertOrUpdate Exceptional
        ContractView.InsertOrUpdatePoFromContract( _
                                                _Project_ID, _
                                                  _ContractID, _
                                                  _AddendumID, _
                                                  _PersonCreated, _
                                                  _usercontrol)

    End Sub

    Shared Sub UpdateContractExceptional(ByVal _ContractID As Integer, ByVal _Exceptional As Boolean)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " UPDATE Table_Contracts SET Exceptional = @Exceptional WHERE ContractID = @ContractID "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
            ContractID.Value = _ContractID
            Dim Exceptional As SqlParameter = cmd.Parameters.Add("@Exceptional", Data.SqlDbType.Bit)
            Exceptional.Value = _Exceptional

            cmd.ExecuteNonQuery()
            con.Close()
            con.Dispose()
        End Using

    End Sub

    Shared Sub UpdateAddendumExceptional(ByVal _AddendumID As Integer, ByVal _Exceptional As Boolean)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " UPDATE [Table_Addendums] SET [Exceptional] = @Exceptional WHERE AddendumID = @AddendumID "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
            AddendumID.Value = _AddendumID
            Dim Exceptional As SqlParameter = cmd.Parameters.Add("@Exceptional", Data.SqlDbType.Bit)
            Exceptional.Value = _Exceptional

            cmd.ExecuteNonQuery()
            con.Close()
            con.Dispose()
        End Using

    End Sub

    Shared Sub Initiate_Checking_Contract_Or_Addendum_To_Insert_Or_Update_Po_Then_Finally_Execute( _
        ByVal _ContractID As Integer, _
        ByVal _AddendumID As Integer, _
        ByVal _userControl As UserControl)

        If _AddendumID = 0 Then ' it is from Contract
            If ContractView.ContractReadyToTurnPo(_ContractID) Then
                If CreateDataReader.Create_Table_Contract(_ContractID).POexecuted = False Then
                    If CreateDataReader.Create_Table_Contract(_ContractID).Exceptional = False Then
                        If CreateDataReader.Create_Table_Contract(_ContractID).Scenario <> 0 Then
                            ' InsertOrUpdatePO
                            ' AddendumID parameter to be 0
                            ContractView.InsertOrUpdatePoFromContract( _
                              CreateDataReader.Create_Table_Contract(_ContractID).ProjectID, _
                              _ContractID, _
                              0, _
                              HttpContext.Current.User.Identity.Name.ToString.ToLower, _
                              _userControl)

                        End If
                    End If
                End If

                If CreateDataReader.Create_Table_Contract(_ContractID).POexecuted = True Then
                    If CreateDataReader.Create_Table_Contract(_ContractID).Exceptional = True Then
                        If CreateDataReader.Create_Table_Contract(_ContractID).Scenario <> 0 Then
                            ' Update Exceptional of Contract as 0
                            ' Because po raised earlier, then contract closed by aprovals and missing items
                            ' set exceptional FALSE
                            ContractView.UpdateContractExceptional(_ContractID, False)
                        End If
                    End If
                End If

            End If

        Else ' It is from Addendum

            If ContractView.AddendumReadyToTurnPo(_AddendumID) Then
                If CreateDataReader.Create_Table_Addendums(_AddendumID).POexecuted = False Then
                    If CreateDataReader.Create_Table_Addendums(_AddendumID).Exceptional = False Then
                        '(Scenario = 0 AND AddendumType = 3) OR ((Scenario > 0 AND AddendumType > 0 AND AddendumType < 3))
                        If ((ContractView.ScenarioNoForThisAddendum(_AddendumID) = 0 _
                        And CreateDataReader.Create_Table_Addendums(_AddendumID).AddendumTypes = 3) _
                        Or _
                        (ContractView.ScenarioNoForThisAddendum(_AddendumID) > 0 _
                        And CreateDataReader.Create_Table_Addendums(_AddendumID).AddendumTypes > 0 _
                        And CreateDataReader.Create_Table_Addendums(_AddendumID).AddendumTypes < 3)) Then
                            ' Then execute InsertOrUpdatePoFromContract
                            ContractView.InsertOrUpdatePoFromContract( _
                              CreateDataReader.Create_Table_Contract(_ContractID).ProjectID, _
                              _ContractID, _
                              _AddendumID, _
                              HttpContext.Current.User.Identity.Name.ToLower, _
                              _userControl)
                        End If
                    End If
                End If

                If CreateDataReader.Create_Table_Addendums(_AddendumID).POexecuted = True Then
                    If CreateDataReader.Create_Table_Addendums(_AddendumID).Exceptional = True Then
                        '(Scenario = 0 AND AddendumType = 3) OR ((Scenario > 0 AND AddendumType > 0 AND AddendumType < 3))
                        If ((ContractView.ScenarioNoForThisAddendum(_AddendumID) = 0 _
                        And CreateDataReader.Create_Table_Addendums(_AddendumID).AddendumTypes = 3) _
                        Or _
                        (ContractView.ScenarioNoForThisAddendum(_AddendumID) > 0 _
                        And CreateDataReader.Create_Table_Addendums(_AddendumID).AddendumTypes > 0 _
                        And CreateDataReader.Create_Table_Addendums(_AddendumID).AddendumTypes < 3)) Then
                            ' Update Exceptional of Addendum as 0
                            ' Because po raised earlier, then contract closed by aprovals and missing items
                            ' set exceptional FALSE
                            ContractView.UpdateAddendumExceptional(_AddendumID, False)
                        End If
                    End If
                End If
            End If

        End If
    End Sub

    Shared Function IsContractControlExceptional(ByVal _UserName As String, ByVal _ProjectID As Integer) As Boolean

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT count([UserName]) FROM [dbo].[Table_ContractControlExceptional] WHERE UserName = @UserName and ProjectID = @ProjectID "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar)
            UserName.Value = _UserName
            Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", Data.SqlDbType.Int)
            ProjectID.Value = _ProjectID

            If cmd.ExecuteScalar = 0 Then
                Return False
            Else
                Return True
            End If

            con.Close()
            con.Dispose()
        End Using

    End Function

    Shared Function PaymentTermsValidated(_advance As String, _interim As String, _shipment As String, _delivery As String, _retention As String) As Boolean

        Dim Advance As Decimal = 0
        Dim Interim As Decimal = 0
        Dim Shipment As Decimal = 0
        Dim Delivery As Decimal = 0
        Dim Retention As Decimal = 0

        If IsNumeric(_advance) Then
            Advance = Convert.ToDecimal(_advance)
        End If

        If IsNumeric(_interim) Then
            Interim = Convert.ToDecimal(_interim)
        End If

        If IsNumeric(_shipment) Then
            Shipment = Convert.ToDecimal(_shipment)
        End If

        If IsNumeric(_delivery) Then
            Delivery = Convert.ToDecimal(_delivery)
        End If

        If IsNumeric(_retention) Then
            Retention = Convert.ToDecimal(_retention)
        End If

        If (Advance + Interim + Shipment + Delivery + Retention) > 100 Then
            'wrong
            Return False
        Else
            Return True
        End If

    End Function

    Shared Function ProduceHTMLforContractEmailBody(ByVal _userControl As UserControl, ByVal _contractId As Integer) As String

        Dim sqls As SqlDataSource = _userControl.FindControl("SqlDataSourceContractEmailBody")
        Dim FormViewContractEmailBody As FormView = _userControl.FindControl("FormViewContractEmailBody")

        sqls.SelectParameters("ContractID").DefaultValue = _contractId

        Dim stringWrite As New System.IO.StringWriter()
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
        FormViewContractEmailBody.DataBind()
        _userControl.RenderControl(htmlWrite)

        Return stringWrite.ToString

    End Function

    Shared Function ProduceHTMLforAddendumEmailBody(ByVal _userControl As UserControl, ByVal _addendumID As Integer) As String

        Dim sqls As SqlDataSource = _userControl.FindControl("SqlDataSourceAddendumsEmailBody")
        Dim FormViewAddendumsEmailBody As FormView = _userControl.FindControl("FormViewAddendumsEmailBody")

        sqls.SelectParameters("AddendumID").DefaultValue = _addendumID

        Dim stringWrite As New System.IO.StringWriter()
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
        FormViewAddendumsEmailBody.DataBind()
        _userControl.RenderControl(htmlWrite)

        Return stringWrite.ToString

    End Function

    Shared Function ContractOrAddendumRejected(ByVal _ContractID As Integer, ByVal _AddendumID As Integer, ByVal _username As String) As Boolean

        If _ContractID > 0 And _AddendumID = 0 Then
            ' use contract
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " IF EXISTS (SELECT [ContractID] FROM [Table_Contract_UsersApprv] WHERE UserName = @UserName AND ContractID = @ContractID AND Exception = 1)" + _
                                        " Select 1" + _
                                        " ELSE" + _
                                        " Select 0"

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = Data.CommandType.Text
                'syntax for parameter adding
                Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
                ContractID.Value = _ContractID
                Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar)
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
                Dim sqlstring As String = " IF EXISTS (SELECT [AddendumID] FROM [Table_Addendum_UsersApprv] WHERE UserName = @UserName AND AddendumID = @AddendumID AND Exception = 1)" + _
                                        " Select 1" + _
                                        " ELSE" + _
                                        " Select 0"

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = Data.CommandType.Text
                'syntax for parameter adding
                Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
                AddendumID.Value = _AddendumID
                Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar)
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

End Class
