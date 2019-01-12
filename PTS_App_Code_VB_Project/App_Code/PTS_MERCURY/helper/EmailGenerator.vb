Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net
Imports System.Web
Imports System.Text

Namespace PTS_MERCURY.helper.EmailGenerator

    Public Class SmtpClient_RussianEncoded
        Inherits SmtpClient

        Public Overloads Sub Send(message As MailMessage)
            message.BodyEncoding = System.Text.Encoding.UTF8
            message.SubjectEncoding = System.Text.Encoding.UTF8

            MyBase.Send(message)
        End Sub

    End Class

    Public Class EverybodyApprovedContract

        Shared Sub Send(_contractid As Integer)

            ' sender (savas)
            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", FromText(_contractid))
            Dim mm1 As New MailMessage()
            mm1.From = MailFrom
            mm1.Body = ""

            ' body
            AddBody(_contractid, mm1)

            ' receiver ()
            AddReceivers(_contractid, mm1)

            ' subject
            AddSubject(_contractid, mm1)

            ' attachment
            mm1.IsBodyHtml = True

            Dim smtp As New SmtpClient_RussianEncoded

            smtp.Send(mm1)

        End Sub

        Shared Sub AddReceivers(_contractid As Integer, ByVal mm As MailMessage)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A = db.SP_EmailListEverybodyApprovedContract(_contractid).ToList()

                For Each item In A
                    mm.To.Add(item)
                Next

            End Using

        End Sub

        Shared Sub AddSubject(_contractid As Integer, ByVal mm As MailMessage)

            mm.Subject = BodyTexts.Ref("iDlIBf4yVkme6jp5OIJRbQ")

        End Sub

        Shared Function ReturnHTMLfromURL(_contractid As Integer) As String

            Dim e = New EmailGeneratorHelper()
            Return e.getEverybodyApprovedContractEmailBody(_contractid)
            e = Nothing

        End Function

        Shared Sub AddBody(_contractid As Integer, ByVal mm As MailMessage)

            Dim strbuilder As New StringBuilder

            strbuilder.Append(ReturnHTMLfromURL(_contractid))

            mm.Body = strbuilder.ToString()

        End Sub

        Shared Function FromText(_contractid As Integer) As String

            Return BodyTexts.Ref("uEtqkoSDqEGHL9AeFneImQ")

        End Function

    End Class

    Public Class EverybodyApprovedAddendum

        Shared Sub Send(_Addendumid As Integer)

            Dim _contractId As Integer = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Addendums.GetRowByPrimaryKey(_Addendumid).ContractID

            ' sender (savas)
            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", FromText(_Addendumid))
            Dim mm1 As New MailMessage()
            mm1.From = MailFrom
            mm1.Body = ""

            ' body
            AddBody(_Addendumid, mm1)

            ' receiver ()
            AddReceivers(_contractId, mm1)

            ' subject
            AddSubject(_Addendumid, mm1)

            ' attachment
            mm1.IsBodyHtml = True

            Dim smtp As New SmtpClient_RussianEncoded

            smtp.Send(mm1)

        End Sub

        Shared Sub AddReceivers(_contractId As Integer, ByVal mm As MailMessage)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A = db.SP_EmailListEverybodyApprovedAddendum(_contractId).ToList()

                For Each item In A
                    mm.To.Add(item)
                Next

            End Using

        End Sub

        Shared Sub AddSubject(_Addendumid As Integer, ByVal mm As MailMessage)

            mm.Subject = BodyTexts.Ref("iDlIBf4yVkme6jp5OIJRbQ")

        End Sub

        Shared Function ReturnHTMLfromURL(_Addendumid As Integer) As String

            Dim e = New EmailGeneratorHelper()
            Return e.getEverybodyApprovedAddendumEmailBody(_Addendumid)
            e = Nothing

        End Function

        Shared Sub AddBody(_Addendumid As Integer, ByVal mm As MailMessage)

            Dim strbuilder As New StringBuilder

            strbuilder.Append(ReturnHTMLfromURL(_Addendumid))

            mm.Body = strbuilder.ToString()

        End Sub

        Shared Function FromText(_Addendumid As Integer) As String

            Return BodyTexts.Ref("qrCqfVCpQk+B5X2Lbn9f1w")

        End Function

    End Class

    Public Class LawyersApprovedContract

        Shared Sub Send(_contractid As Integer)

            ' sender (savas)
            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", FromText(_contractid))
            Dim mm1 As New MailMessage()
            mm1.From = MailFrom
            mm1.Body = ""

            ' body
            AddBody(_contractid, mm1)

            ' receiver ()
            AddReceivers(_contractid, mm1)

            ' subject
            AddSubject(_contractid, mm1)

            ' attachment
            mm1.IsBodyHtml = True

            Dim smtp As New SmtpClient_RussianEncoded

            smtp.Send(mm1)

        End Sub

        Shared Sub AddReceivers(_contractid As Integer, ByVal mm As MailMessage)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A = db.SP_EmailListLawyersApprovedContract(_contractid).ToList()

                For Each item In A
                    mm.To.Add(item)
                Next

            End Using

        End Sub

        Shared Sub AddSubject(_contractid As Integer, ByVal mm As MailMessage)

            mm.Subject = BodyTexts.Ref("8I0VIYbOTkOxHfxjdLRuxA")

        End Sub

        Shared Function ReturnHTMLfromURL(_contractid As Integer) As String

            Dim e = New EmailGeneratorHelper()
            Return e.getLawyersApprovedContractEmailBody(_contractid)
            e = Nothing

        End Function

        Shared Sub AddBody(_contractid As Integer, ByVal mm As MailMessage)

            Dim strbuilder As New StringBuilder

            strbuilder.Append(ReturnHTMLfromURL(_contractid))

            mm.Body = strbuilder.ToString()

        End Sub

        Shared Function FromText(_contractid As Integer) As String

            Return BodyTexts.Ref("uEtqkoSDqEGHL9AeFneImQ")

        End Function

    End Class

    Public Class LawyersApprovedAddendum

        Shared Sub Send(_Addendumid As Integer)

            ' sender (savas)
            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", FromText(_Addendumid))
            Dim mm1 As New MailMessage()
            mm1.From = MailFrom
            mm1.Body = ""

            ' body
            AddBody(_Addendumid, mm1)

            ' receiver ()
            AddReceivers(_Addendumid, mm1)

            ' subject
            AddSubject(_Addendumid, mm1)

            ' attachment
            mm1.IsBodyHtml = True

            Dim smtp As New SmtpClient_RussianEncoded

            smtp.Send(mm1)

        End Sub

        Shared Sub AddReceivers(_Addendumid As Integer, ByVal mm As MailMessage)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A = db.SP_EmailListLawyersApprovedAddendum(_Addendumid).ToList()

                For Each item In A
                    mm.To.Add(item)
                Next

            End Using

        End Sub

        Shared Sub AddSubject(_Addendumid As Integer, ByVal mm As MailMessage)

            mm.Subject = BodyTexts.Ref("6vbngQ61d0eSLQRBoA9vRQ")

        End Sub

        Shared Function ReturnHTMLfromURL(_Addendumid As Integer) As String

            Dim e = New EmailGeneratorHelper()
            Return e.getLawyersApprovedAddendumEmailBody(_Addendumid)
            e = Nothing

        End Function

        Shared Sub AddBody(_Addendumid As Integer, ByVal mm As MailMessage)

            Dim strbuilder As New StringBuilder

            strbuilder.Append(ReturnHTMLfromURL(_Addendumid))

            mm.Body = strbuilder.ToString()

        End Sub

        Shared Function FromText(_Addendumid As Integer) As String

            Return BodyTexts.Ref("qrCqfVCpQk+B5X2Lbn9f1w")

        End Function


    End Class

    Public Class AutoApprovalOnApprovalMatrixEmailGenerator

        Shared Sub Send(_contractid As Integer, _addendumid As Integer)

            Dim _TypeOfNotification As TypeOfNotification

            If _contractid > 0 And _addendumid = 0 Then
                _TypeOfNotification = AutoApprovalOnApprovalMatrixEmailGenerator.TypeOfNotification.TimeLimitApprovalContract
            ElseIf _contractid > 0 And _addendumid > 0 Then
                _TypeOfNotification = AutoApprovalOnApprovalMatrixEmailGenerator.TypeOfNotification.TimeLimitApprovalAddendum
            End If

            ' sender (savas)
            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", FromText(_contractid, _addendumid, _TypeOfNotification))
            Dim mm1 As New MailMessage()
            mm1.From = MailFrom
            mm1.Body = ""

            ' body
            AddBody(_contractid, _addendumid, _TypeOfNotification, mm1)

            ' receiver ()
            AddReceivers(_contractid, _addendumid, _TypeOfNotification, mm1)

            ' subject
            AddSubject(_contractid, _addendumid, _TypeOfNotification, mm1)

            ' attachment

            mm1.IsBodyHtml = True

            Dim smtp As New SmtpClient_RussianEncoded

            smtp.Send(mm1)

        End Sub

        Shared Sub AddReceivers(_contractid As Integer, _addendumid As Integer, ByVal TypeOfNotification As TypeOfNotification, ByVal mm As MailMessage)

            mm.To.Add("savas.karaduman@mercuryeng.ru")

            ' remove this later on
            Exit Sub

            ' add autoapproved persons
            PTS_MERCURY.helper.View_CheckTolerance.GetUsersEmailAutoApprovedByPTS(_contractid, _addendumid, mm)

            ' add not approved persons
            PTS_MERCURY.helper.View_CheckTolerance.GetUsersEmailNotApproved(_contractid, _addendumid, mm)

            mm.To.Add("savas.karaduman@mercuryeng.ru")

        End Sub

        Shared Sub AddSubject(_contractid As Integer, _addendumid As Integer, ByVal TypeOfNotification As TypeOfNotification, ByVal mm As MailMessage)

            Dim _contractno As String = ""
            Try
                _contractno = PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(_contractid).ContractNo.Trim()
            Catch ex As Exception
                _contractno = ""
            End Try

            Dim _addendumno As String = ""
            Try
                _addendumno = PTS_MERCURY.helper.Table_Addendums.GetRowByPrimaryKey(_addendumid).AddendumNo.Trim()
            Catch ex As Exception
                _addendumno = ""
            End Try

            Dim _suppliername As String = PTS_MERCURY.helper.Table_Contracts.GetSupplierName(_contractid)

            Dim _projectname As String = PTS_MERCURY.helper.Table_Contracts.GetProjectName(_contractid)

            If TypeOfNotification = AutoApprovalOnApprovalMatrixEmailGenerator.TypeOfNotification.TimeLimitApprovalContract Then
                mm.Subject = "Project: <" + _projectname + ">" + "Contract No: <" + _contractno + ">" + " Supplier Name: <" + _suppliername + ">"
            End If

            If TypeOfNotification = AutoApprovalOnApprovalMatrixEmailGenerator.TypeOfNotification.TimeLimitApprovalAddendum Then
                mm.Subject = "Project: <" + _projectname + ">" + "Addendum No: <" + _addendumno + ">" + " Supplier Name: <" + _suppliername + ">"
            End If

        End Sub

        Shared Function ReturnHTMLfromURL(_contractid As Integer, _addendumid As Integer, ByVal TypeOfNotification As TypeOfNotification) As String

            Dim myClient As New WebClient()
            Dim _return As String = ""
            Dim variable As String = ""

            If TypeOfNotification = AutoApprovalOnApprovalMatrixEmailGenerator.TypeOfNotification.TimeLimitApprovalContract Then
                variable = "AutoApprovalByPTSContract"
            ElseIf TypeOfNotification = AutoApprovalOnApprovalMatrixEmailGenerator.TypeOfNotification.TimeLimitApprovalAddendum Then
                variable = "AutoApprovalByPTSAddendum"
            End If

            Dim _localhost As String = PTS_MERCURY.helper.Garbage.MagicString.LocalHostAdress

            ' important, provide this
            myClient.Encoding = System.Text.Encoding.UTF8
            _return = myClient.DownloadString(_localhost + "/Pages/PTS/EmailBodies/" + variable + ".aspx?ContractId=" + _contractid.ToString() + "" + "&AddendumId=" + _addendumid.ToString())

            Return _return

        End Function

        Shared Sub AddBody(_contractid As Integer, _addendumid As Integer, ByVal TypeOfNotification As TypeOfNotification, ByVal mm As MailMessage)

            Dim strbuilder As New StringBuilder
            Dim _C_A As String = ""

            If _addendumid > 0 Then
                _C_A = "addendum"
            Else
                _C_A = "contract"
            End If

            strbuilder.Append("Dear All дловоылФФФФ,")
            strbuilder.Append("<br/><br/>")
            strbuilder.Append("PTS automatically approved following " + _C_A + " on behalf of ")
            strbuilder.Append("<br/><br/>")
            strbuilder.Append(PTS_MERCURY.helper.View_CheckTolerance.GetUsersAutoApprovedByPTS(_contractid, _addendumid))
            strbuilder.Append("<br/>")
            strbuilder.Append("Regards")
            strbuilder.Append("<br/>")
            strbuilder.Append("PTS")
            strbuilder.Append("<br/><br/>")
            strbuilder.Append(ReturnHTMLfromURL(_contractid, _addendumid, TypeOfNotification))

            mm.Body = strbuilder.ToString()

        End Sub

        Shared Function FromText(_contractid As Integer, _addendumid As Integer, ByVal TypeOfNotification As TypeOfNotification) As String

            Dim _return As String = ""

            If TypeOfNotification = AutoApprovalOnApprovalMatrixEmailGenerator.TypeOfNotification.TimeLimitApprovalContract Then
                _return = "Contract Auto Approval by PTS"
            End If

            If TypeOfNotification = AutoApprovalOnApprovalMatrixEmailGenerator.TypeOfNotification.TimeLimitApprovalAddendum Then
                _return = "Addendum Auto Approval by PTS"
            End If

            Return _return

        End Function

        Public Enum TypeOfNotification
            TimeLimitApprovalContract
            TimeLimitApprovalAddendum
        End Enum

    End Class

    Public Class BudgetControlEmailGenerator

        Shared Sub Send(_username As String, _projectid As Short, _costcode As String, _TypeOfNotification As TypeOfNotification)

            Dim username As String = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(PTS_MERCURY.helper.View_GetFullUserNameFromUserName.GetUserName(_username))

            Dim _ProjectName As String = ""
            _ProjectName = _projectid.ToString() + " - " + PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(_projectid).ProjectName.Trim()

            Dim _costcodedescription As String = ""
            Try
                _costcodedescription = _costcode + " - " + PTS_MERCURY.helper.Table7_CostCode.GetRowByPrimaryKey(_costcode).CodeDescription.Trim()
            Catch ex As Exception
                _costcodedescription = ""
            End Try

            ' sender (savas)
            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", FromText(_TypeOfNotification))
            Dim mm1 As New MailMessage()
            mm1.From = MailFrom
            mm1.Body = ""

            ' body
            AddBody(username, _projectid, _costcodedescription, _TypeOfNotification, mm1)

            ' receiver ()
            AddReceivers(_projectid, _username, _TypeOfNotification, mm1)

            ' subject
            AddSubject(_ProjectName, _costcodedescription, _TypeOfNotification, mm1)

            ' attachment

            mm1.IsBodyHtml = True

            Dim smtp As New SmtpClient_RussianEncoded

            smtp.Send(mm1)

        End Sub

        Shared Sub AddReceivers(_projectid As Integer, _username As String, ByVal TypeOfNotification As TypeOfNotification, ByVal mm As MailMessage)

            mm.To.Add("savas.karaduman@mercuryeng.ru")

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A = From C In db.aspnet_Users
                        Join D In db.aspnet_Membership On C.UserId Equals D.UserId
                        Join E In db.Table_Approval_UserPositionPrjJunction On C.UserName Equals E.UserName
                        Join F In db.Table_Approval_PositionEmployee On E.PositionID Equals F.PositionID
                        Where E.ProjectID = _projectid And (F.PositionName = "General Director" OrElse F.PositionName = "Project Manager" OrElse F.PositionName = "Cost Controller" OrElse F.PositionName = "Finance Director" OrElse F.PositionName = "Operational Manager" OrElse F.PositionName = "Commercial Manager") Select New With {D.LoweredEmail}

                If A.ToList().Count() > 0 Then

                    For i = 0 To A.ToList().Count() - 1

                        mm.To.Add(A.ToList()(i).LoweredEmail)

                    Next

                End If

                Dim B = (From C In db.aspnet_Users Join D In db.aspnet_Membership On C.UserId Equals D.UserId Where C.UserName = _username Select New With {D.LoweredEmail})

                If B.ToList().Count() > 0 Then

                    For i = 0 To B.ToList().Count() - 1

                        mm.To.Add(B.ToList()(i).LoweredEmail)

                    Next

                End If


            End Using


        End Sub

        Shared Sub AddSubject(_projectName As String, _costcodedescription As String, ByVal TypeOfNotification As TypeOfNotification, ByVal mm As MailMessage)

            If TypeOfNotification = BudgetControlEmailGenerator.TypeOfNotification.BudgetValueZero Then
                mm.Subject = "No budget defined for costcode > " + _costcodedescription + " under Project >" + _projectName
            End If

            If TypeOfNotification = BudgetControlEmailGenerator.TypeOfNotification.BudgetExceedingLimit Then
                mm.Subject = "Budget is exceeding limits for costcode > " + _costcodedescription + " under Project >" + _projectName
            End If

        End Sub

        Shared Function ReturnHTMLfromURL(_contractid As Integer, _addendumid As Integer, ByVal TypeOfNotification As TypeOfNotification) As String

            'Dim myClient As New WebClient()
            'Dim _return As String = ""
            'Dim variable As String = ""

            'If TypeOfNotification = AutoApprovalOnApprovalMatrixEmailGenerator.TypeOfNotification.TimeLimitApprovalContract Then
            '    variable = "AutoApprovalByPTSContract"
            'ElseIf TypeOfNotification = AutoApprovalOnApprovalMatrixEmailGenerator.TypeOfNotification.TimeLimitApprovalAddendum Then
            '    variable = "AutoApprovalByPTSAddendum"
            'End If

            'Dim _localhost As String = HttpContext.Current.Request.Url.Scheme + System.Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host + (IIf(HttpContext.Current.Request.Url.IsDefaultPort, "", ":" + HttpContext.Current.Request.Url.Port.ToString()))

            '_return = myClient.DownloadString(_localhost + "/Pages/PTS/EmailBodies/" + variable + ".aspx?ContractId=" + _contractid.ToString() + "" + "&AddendumId=" + _addendumid.ToString())

            'Return _return


        End Function

        Shared Sub AddBody(_username As String, _projectid As Short, _costcodedescription As String, ByVal TypeOfNotification As TypeOfNotification, ByVal mm As MailMessage)

            Dim _ProjectName As String = ""

            _ProjectName = _projectid.ToString() + " - " + PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(_projectid).ProjectName.Trim()

            Dim _costcontroller As String = ""
            Dim _costcontrollerEmail As String = ""

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A = (From C In db.Table_Approval_UserPositionPrjJunction Join D In db.Table_Approval_PositionEmployee On C.PositionID Equals D.PositionID Where C.ProjectID = _projectid And D.PositionName = "Cost Controller" Select New With {C.UserName})

                If A.ToList().Count() > 0 Then
                    _costcontroller = A.ToList()(0).UserName.Trim()
                    _costcontrollerEmail = (From C In db.aspnet_Users Join D In db.aspnet_Membership On C.UserId Equals D.UserId Where C.UserName = _costcontroller Select New With {D.LoweredEmail}).ToList()(0).LoweredEmail.Trim()
                    _costcontroller = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(PTS_MERCURY.helper.View_GetFullUserNameFromUserName.GetUserName(_costcontroller))
                Else
                    _costcontroller = "Not Defined"
                    _costcontrollerEmail = "Not Defined"
                End If

            End Using

            Dim strbuilder As New StringBuilder

            If TypeOfNotification = BudgetControlEmailGenerator.TypeOfNotification.BudgetValueZero Then
                strbuilder.Append("Dear All,")
                strbuilder.Append("<br/><br/>")
                strbuilder.Append("<b>" + _username + "</b> is trying to define a new PO for cost code <b>" + _costcodedescription + "</b> under project <b>" + _ProjectName + "</b>.")
                strbuilder.Append(" However budget value is not defined for this cost code <b>" + _costcodedescription + "</b> yet.")
                strbuilder.Append("<br/><br/>")
                strbuilder.Append("Transaction is terminated.")
                strbuilder.Append("<br/><br/>")
                strbuilder.Append("In order to proceed, you should contact to Cost Controller")
                strbuilder.Append("<br/>")
                strbuilder.Append("According to PTS, Cost Controller for <b>" + _ProjectName + "</b> is <b>" + "<a href=" + """" + "mailto:" + _costcontrollerEmail + "?Subject=Budget not defined" + """" + " target=" + """" + "_top" + """" + ">" + _costcontroller + "</a>" + "</b>")
                strbuilder.Append("<br/><br/>")
                strbuilder.Append("Regards")
                strbuilder.Append("<br/>")
                strbuilder.Append("PTS")
                strbuilder.Append("<br/><br/>")
                'strbuilder.Append(ReturnHTMLfromURL(_contractid, _addendumid, TypeOfNotification))
            End If

            If TypeOfNotification = BudgetControlEmailGenerator.TypeOfNotification.BudgetExceedingLimit Then

                strbuilder.Append("Dear All,")
                strbuilder.Append("<br/><br/>")
                strbuilder.Append("<b>" + _username + "</b> is trying to define a new PO for cost code <b>" + _costcodedescription + "</b> under project <b>" + _ProjectName + "</b>.")
                strbuilder.Append(" However budget is exceeding limits for this cost code <b>" + _costcodedescription + "</b>.")
                strbuilder.Append("<br/><br/>")
                strbuilder.Append("Transaction is terminated.")
                strbuilder.Append("<br/><br/>")
                strbuilder.Append("In order to proceed, you should contact to Cost Controller")
                strbuilder.Append("<br/>")
                strbuilder.Append("According to PTS, Cost Controller for <b>" + _ProjectName + "</b> is <b>" + "<a href=" + """" + "mailto:" + _costcontrollerEmail + "?Subject=Budget is exceeding limits" + """" + " target=" + """" + "_top" + """" + ">" + _costcontroller + "</a>" + "</b>")
                strbuilder.Append("<br/><br/>")
                strbuilder.Append("Regards")
                strbuilder.Append("<br/>")
                strbuilder.Append("PTS")
                strbuilder.Append("<br/><br/>")
                'strbuilder.Append(ReturnHTMLfromURL(_contractid, _addendumid, TypeOfNotification))
            End If

            mm.Body = strbuilder.ToString()

        End Sub

        Shared Function FromText(ByVal TypeOfNotification As TypeOfNotification) As String

            Dim _return As String = ""

            If TypeOfNotification = BudgetControlEmailGenerator.TypeOfNotification.BudgetValueZero Then
                _return = "PTS Budget Notification"
            End If

            If TypeOfNotification = BudgetControlEmailGenerator.TypeOfNotification.BudgetExceedingLimit Then
                _return = "PTS Budget Notification"
            End If

            Return _return

        End Function

        Public Enum TypeOfNotification
            BudgetValueZero
            BudgetExceedingLimit
        End Enum

    End Class

    Public Class FrameBudgetEmailGenerator

        Class Parameters
            Property projectId As Integer = 0
            Property contractid As Integer = 0
            Property projectname As String = ""
        End Class

        Shared Sub Send(_username As String, _framecontractid As Integer, _TypeOfNotification As TypeOfNotification)

            Dim _parameters As New Parameters

            Dim username As String = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(PTS_MERCURY.helper.View_GetFullUserNameFromUserName.GetUserName(_username))

            _parameters.contractid = _framecontractid
            _parameters.projectId = PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(_framecontractid).ProjectID
            _parameters.projectname = _parameters.projectId.ToString() + " - " + PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(_parameters.projectId).ProjectName.Trim()

            'Dim _costcodedescription As String = ""
            'Try
            '    _costcodedescription = _costcode + " - " + PTS_MERCURY.helper.Table7_CostCode.GetRowByPrimaryKey(_costcode).CodeDescription.Trim()
            'Catch ex As Exception
            '    _costcodedescription = ""
            'End Try

            ' sender (savas)
            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", FromText(_TypeOfNotification))
            Dim mm1 As New MailMessage()
            mm1.From = MailFrom
            mm1.Body = ""

            ' body
            AddBody(username, _parameters.contractid, _parameters.projectId, _TypeOfNotification, mm1)

            ' receiver ()
            AddReceivers(_parameters.projectId, _username, _TypeOfNotification, mm1)

            ' subject
            AddSubject(_parameters.projectname, "", _TypeOfNotification, mm1)

            ' attachment

            mm1.IsBodyHtml = True

            Dim smtp As New SmtpClient_RussianEncoded

            smtp.Send(mm1)

        End Sub

        Shared Sub AddReceivers(_projectid As Integer, _username As String, ByVal TypeOfNotification As TypeOfNotification, ByVal mm As MailMessage)

            mm.To.Add("savas.karaduman@mercuryeng.ru")

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A = From C In db.aspnet_Users
                        Join D In db.aspnet_Membership On C.UserId Equals D.UserId
                        Join E In db.Table_Approval_UserPositionPrjJunction On C.UserName Equals E.UserName
                        Join F In db.Table_Approval_PositionEmployee On E.PositionID Equals F.PositionID
                        Where E.ProjectID = _projectid And (F.PositionName = "General Director" OrElse F.PositionName = "Project Manager" OrElse F.PositionName = "Cost Controller" OrElse F.PositionName = "Finance Director" OrElse F.PositionName = "Operational Manager" OrElse F.PositionName = "Commercial Manager") Select New With {D.LoweredEmail}

                If A.ToList().Count() > 0 Then

                    For i = 0 To A.ToList().Count() - 1

                        mm.To.Add(A.ToList()(i).LoweredEmail)

                    Next

                End If

                Dim B = (From C In db.aspnet_Users Join D In db.aspnet_Membership On C.UserId Equals D.UserId Where C.UserName = _username Select New With {D.LoweredEmail})

                If B.ToList().Count() > 0 Then

                    For i = 0 To B.ToList().Count() - 1

                        mm.To.Add(B.ToList()(i).LoweredEmail)

                    Next

                End If


            End Using


        End Sub

        Shared Sub AddSubject(_projectName As String, _costcodedescription As String, ByVal TypeOfNotification As TypeOfNotification, ByVal mm As MailMessage)

            If TypeOfNotification = TypeOfNotification.BudgetexceedWarning Then
                mm.Subject = "Frame Contract Budget Exceeding under Project >" + _projectName
            End If

        End Sub

        Shared Function ReturnHTMLfromURL(_contractid As Integer, _addendumid As Integer, ByVal TypeOfNotification As TypeOfNotification) As String

            'Dim myClient As New WebClient()
            'Dim _return As String = ""
            'Dim variable As String = ""

            'If TypeOfNotification = AutoApprovalOnApprovalMatrixEmailGenerator.TypeOfNotification.TimeLimitApprovalContract Then
            '    variable = "AutoApprovalByPTSContract"
            'ElseIf TypeOfNotification = AutoApprovalOnApprovalMatrixEmailGenerator.TypeOfNotification.TimeLimitApprovalAddendum Then
            '    variable = "AutoApprovalByPTSAddendum"
            'End If

            'Dim _localhost As String = HttpContext.Current.Request.Url.Scheme + System.Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host + (IIf(HttpContext.Current.Request.Url.IsDefaultPort, "", ":" + HttpContext.Current.Request.Url.Port.ToString()))

            '_return = myClient.DownloadString(_localhost + "/Pages/PTS/EmailBodies/" + variable + ".aspx?ContractId=" + _contractid.ToString() + "" + "&AddendumId=" + _addendumid.ToString())

            'Return _return


        End Function

        Shared Sub AddBody(_username As String, _contractid As Integer, _projectid As Short, ByVal TypeOfNotification As TypeOfNotification, ByVal mm As MailMessage)

            Dim _ProjectName As String = ""

            _ProjectName = _projectid.ToString() + " - " + PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(_projectid).ProjectName.Trim()

            Dim _costcontroller As String = ""
            Dim _costcontrollerEmail As String = ""

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim A = (From C In db.Table_Approval_UserPositionPrjJunction Join D In db.Table_Approval_PositionEmployee On C.PositionID Equals D.PositionID Where C.ProjectID = _projectid And D.PositionName = "Cost Controller" Select New With {C.UserName})

                If A.ToList().Count() > 0 Then
                    _costcontroller = A.ToList()(0).UserName.Trim()
                    _costcontrollerEmail = (From C In db.aspnet_Users Join D In db.aspnet_Membership On C.UserId Equals D.UserId Where C.UserName = _costcontroller Select New With {D.LoweredEmail}).ToList()(0).LoweredEmail.Trim()
                    _costcontroller = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(PTS_MERCURY.helper.View_GetFullUserNameFromUserName.GetUserName(_costcontroller))
                Else
                    _costcontroller = "Not Defined"
                    _costcontrollerEmail = "Not Defined"
                End If

            End Using

            Dim strbuilder As New StringBuilder

            If TypeOfNotification = TypeOfNotification.BudgetexceedWarning Then
                strbuilder.Append("Dear All,")
                strbuilder.Append("<br/><br/>")
                strbuilder.Append("TOTAL PO VALUE exceeds 90% of Budget of " + " <a href=" + """" + "http://pts.mercuryeng.ru/contractdetails.aspx?ContractID=" + _contractid.ToString + """" + " target=" + """" + "_blank" + """" + ">this frame contract </a> " + " for Project <b>" + _ProjectName + "</b>")
                strbuilder.Append("<br/><br/>")
                strbuilder.Append("According to PTS, Cost Controller for <b>" + _ProjectName + "</b> is <b>" + "<a href=" + """" + "mailto:" + _costcontrollerEmail + "?Subject=Frame Contract Budget" + """" + " target=" + """" + "_top" + """" + ">" + _costcontroller + "</a>" + "</b>")
                strbuilder.Append("<br/><br/>")
                strbuilder.Append("Regards")
                strbuilder.Append("<br/>")
                strbuilder.Append("PTS")
                strbuilder.Append("<br/><br/>")
                'strbuilder.Append(ReturnHTMLfromURL(_contractid, _addendumid, TypeOfNotification))
            End If

            mm.Body = strbuilder.ToString()

        End Sub

        Shared Function FromText(ByVal TypeOfNotification As TypeOfNotification) As String

            Dim _return As String = ""

            If TypeOfNotification = TypeOfNotification.BudgetexceedWarning Then
                _return = "Frame Contract Budget"
            End If

            Return _return

        End Function

        Public Enum TypeOfNotification
            BudgetexceedWarning
        End Enum

    End Class

End Namespace
