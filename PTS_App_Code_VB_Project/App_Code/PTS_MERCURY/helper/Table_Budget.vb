Imports Microsoft.VisualBasic
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Text

Namespace PTS_MERCURY.helper

    Public Class Table_Budget

        Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table_Budget Into Count()) > 0 Then
                _return = (Aggregate C In db.Table_Budget Into Max(C.BudgetID))
            End If

            Return _return + 1

        End Function

        Shared Function CountItemsByBudgetID(_BudgetID As Int32) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_Budget Where C.BudgetID = _BudgetID Into Count()

            End Using

            Return _return

        End Function

        Shared Function GetRowByPrimaryKey(_BudgetID As Int32) As PTS_MERCURY.db.Table_Budget

            Dim _return As PTS_MERCURY.db.Table_Budget

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItemsByBudgetID(_BudgetID) > 0 Then

                    _return = (From C In db.Table_Budget Where C.BudgetID = _BudgetID).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function

        Partial Public Class Table_Budget_ToGrid
            Public Property BudgetID As Integer
            Public Property ProjectID As Short
            Public Property CostCode As String
            Public Property Budget As Nullable(Of Decimal)
            Public Property PlannedToSpend As Nullable(Of Decimal)
            Public Property PlannedToSpendCO As Nullable(Of Decimal)
            Public Property Currency As String
            Public Property UpdatedPlannedRevenue As Nullable(Of Decimal)
            Public Property OriginalBOQ As Nullable(Of Decimal)
            Public Property VCO As Nullable(Of Decimal)
            Public Property CostToGoBOQ As Nullable(Of Decimal)
            Public Property CostToGoVCO As Nullable(Of Decimal)
        End Class

        Public Class BudgetParameters
            Property budget As Decimal
            Property exceedingBudget As Boolean = False
            Property totalPO As Decimal
            Property nonexecutedContractAddendum As Decimal
            Property oldValue As Decimal
            Property newValue As Decimal
            Property HowMuchExceeds As Decimal
        End Class

        Shared Function GetBudgetParameters(_projectid As Integer, _costcode As String, _oldvalue As Decimal, _newvalue As Decimal) As BudgetParameters

            Dim _return As New BudgetParameters

            _return.oldValue = _oldvalue
            _return.newValue = _newvalue

            Dim _budget As Decimal = 0.0
            Dim _currency As String = ""

            If PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(_projectid).ContractCurrency IsNot Nothing Then
                _currency = PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(_projectid).ContractCurrency.Trim()
            End If

            Dim _totalcommitmentExcVat As Decimal = 0.0
            Dim _nonexecutedContractAddendum As Decimal = 0.0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If (Aggregate C In db.Table_Budget Where C.ProjectID = _projectid And C.CostCode = _costcode Into Count()) > 0 Then
                    If Not (From C In db.Table_Budget Where C.ProjectID = _projectid And C.CostCode = _costcode).ToList()(0).Budget.HasValue Then
                        _budget = 0.0
                    Else
                        _budget = (From C In db.Table_Budget Where C.ProjectID = _projectid And C.CostCode = _costcode).ToList()(0).Budget
                    End If

                End If

                Dim _comittedcost = (From C In db.View_CostCodeSummary0 Where C.ProjectID = _projectid And C.CostCode = _costcode Select C.TotalPoTotalDollarExcVAT, C.TotalPoTotalEuroExcVAT, C.TotalPoTotalRubleExcVAT)

                If _comittedcost.ToList().Count() > 0 Then

                    If _currency.ToLower() = "rub" Then
                        _totalcommitmentExcVat = _comittedcost.ToList()(0).TotalPoTotalRubleExcVAT
                    ElseIf _currency.ToLower() = "dollar" Then
                        _totalcommitmentExcVat = _comittedcost.ToList()(0).TotalPoTotalDollarExcVAT
                    ElseIf _currency.ToLower() = "euro" Then
                        _totalcommitmentExcVat = _comittedcost.ToList()(0).TotalPoTotalEuroExcVAT
                    End If

                End If

                Try
                    If _currency.ToLower() = "rub" Then
                        _nonexecutedContractAddendum = helper.View_ContractsAddendumsNonExecutedTotal.GetView_ContractsAddendumsNonExecutedTotal(_projectid, _costcode).TotalNotExcecutedValueExcVAT_Rub
                    ElseIf _currency.ToLower() = "dollar" Then
                        _nonexecutedContractAddendum = helper.View_ContractsAddendumsNonExecutedTotal.GetView_ContractsAddendumsNonExecutedTotal(_projectid, _costcode).TotalNotExcecutedValueExcVAT_Dollar
                    ElseIf _currency.ToLower() = "euro" Then
                        _nonexecutedContractAddendum = helper.View_ContractsAddendumsNonExecutedTotal.GetView_ContractsAddendumsNonExecutedTotal(_projectid, _costcode).TotalNotExcecutedValueExcVAT_Euro
                    End If
                Catch ex As Exception
                    _nonexecutedContractAddendum = 0
                End Try

            End Using

            _return.budget = _budget
            _return.totalPO = _totalcommitmentExcVat
            _return.nonexecutedContractAddendum = _nonexecutedContractAddendum

            _return.HowMuchExceeds = _totalcommitmentExcVat + _nonexecutedContractAddendum + _newvalue - _oldvalue - _budget

            If _budget <> 0 AndAlso _return.HowMuchExceeds > 0 Then
                _return.exceedingBudget = True
            End If

            Return _return

            _return = Nothing

        End Function

        Shared Function GetBudgetEmailControlFailed(_page As Page, _projectId As Short, _costcode As String, _oldValue As Decimal, _newValue As Decimal) As Boolean

            Return False

            ' these postback cause invalid budget failer
            If PTS_MERCURY.helper.Garbage.GetControlThatCausedPostBack(_page) IsNot Nothing Then
                If PTS_MERCURY.helper.Garbage.GetControlThatCausedPostBack(_page).ID.IndexOf("DropDownListProject") > -1 Then
                    Return False
                    Exit Function
                End If
            End If

            Dim _return As Boolean = False

            Dim _budgetemailcontrol As Boolean = False

            Try
                _budgetemailcontrol = PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(_projectId).BudgetEmailControl
            Catch ex As Exception
                ' ignore exception
            End Try



            Dim LiteralTest As New Literal()
            Dim content As ContentPlaceHolder
            content = _page.Master.FindControl("MainContent")

            If _page.IsPostBack = True Then
                If _budgetemailcontrol = True Then

                    If _costcode <> "0" Then

                        Dim _TotalPrice As Decimal = 0

                        If IsNumeric(_newValue) = True Then
                            _TotalPrice = _newValue
                        End If

                        Dim _budgetparameters As New PTS_MERCURY.helper.Table_Budget.BudgetParameters

                        _budgetparameters = PTS_MERCURY.helper.Table_Budget.GetBudgetParameters(_projectId, _costcode, _oldValue, _newValue)

                        Dim _htmlMessage As String = ""

                        Try
                            _htmlMessage = "            <table class=" + """" + "table" + """" + " border=" + """" + "1" + """" + "> " + _
                                            "                <thead> " + _
                                            "                    <tr> " + _
                                            "                        <td> " + _
                                            "                            Cost Code " + _
                                            "                        </td> " + _
                                            "                        <td> " + _
                                            "                            Budget Exc VAT " + _
                                            "                        </td> " + _
                                            "                        <td> " + _
                                            "                            Total PO Exc VAT " + _
                                            "                        </td> " + _
                                            "                        <td> " + _
                                            "                            Not Executed Contract and Addendum Exc VAT " + _
                                            "                        </td> " + _
                                            "                        <td> " + _
                                            "                            Old Value " + _
                                            "                        </td> " + _
                                            "                        <td> " + _
                                            "                            New Value " + _
                                            "                        </td> " + _
                                            "                        <td> " + _
                                            "                            How Much Exceeding " + _
                                            "                        </td> " + _
                                            "                    </tr> " + _
                                            "                </thead> " + _
                                            "                <tbody> " + _
                                            "                    <tr> " + _
                                            "                        <td> " + _
                                            "                            " + _costcode + " " + _
                                            "                        </td> " + _
                                            "                        <td> " + _
                                            "                            " + String.Format("{0:N2}", _budgetparameters.budget) + " " + _
                                            "                        </td> " + _
                                            "                        <td> " + _
                                            "                            " + String.Format("{0:N2}", _budgetparameters.totalPO) + " " + _
                                            "                        </td> " + _
                                            "                        <td> " + _
                                            "                            " + String.Format("{0:N2}", _budgetparameters.nonexecutedContractAddendum) + " " + _
                                            "                        </td> " + _
                                            "                        <td> " + _
                                            "                            " + String.Format("{0:N2}", _budgetparameters.oldValue) + " " + _
                                            "                        </td> " + _
                                            "                        <td> " + _
                                            "                            " + String.Format("{0:N2}", _budgetparameters.newValue) + " " + _
                                            "                        </td> " + _
                                            "                        <td> " + _
                                            "                            " + String.Format("{0:N2}", _budgetparameters.HowMuchExceeds) + " " + _
                                            "                        </td> " + _
                                            "                    </tr> " + _
                                            "                </tbody> " + _
                                            "            </table> "

                        Catch ex As Exception

                        End Try

                        If _budgetparameters.budget = 0 Then

                            LiteralTest.Text = _returnModal("There is no budget defined for this Cost Code. You cannot proceed!" + _htmlMessage)
                            content.Controls.Add(LiteralTest)
                            ScriptManager.RegisterClientScriptBlock(_page, GetType(Page), "alert", "$(function () { $('#" + "ModalGridError" + "').modal({}) });", True)
                            _return = True
                            'PTS_MERCURY.helper.EmailGenerator.BudgetControlEmailGenerator.Send(_page.User.Identity.Name.ToLower(), _projectId, _costcode, PTS_MERCURY.helper.EmailGenerator.BudgetControlEmailGenerator.TypeOfNotification.BudgetValueZero)

                        ElseIf _budgetparameters.exceedingBudget = True Then

                            LiteralTest.Text = _returnModal("You are exceeding budget for this Cost Code. You cannot proceed!" + _htmlMessage)
                            content.Controls.Add(LiteralTest)
                            ScriptManager.RegisterClientScriptBlock(_page, GetType(Page), "alert", "$(function () { $('#" + "ModalGridError" + "').modal({}) });", True)
                            _return = True
                            'PTS_MERCURY.helper.EmailGenerator.BudgetControlEmailGenerator.Send(_page.User.Identity.Name.ToLower(), _projectId, _costcode, PTS_MERCURY.helper.EmailGenerator.BudgetControlEmailGenerator.TypeOfNotification.BudgetExceedingLimit)

                        Else
                            _return = False

                        End If

                    End If
                Else
                    _return = False
                End If
            End If

            Return _return

        End Function

        Shared Function _returnModal(_message As String) As String

            Dim _stringBuilder As New StringBuilder
            _stringBuilder.Append("<div id=" + """" + "ModalGridError" + """" + " class=" + """" + "modal" + """" + ">")
            _stringBuilder.Append("    <div class=" + """" + "modal-dialog modal-dialog-center" + """" + ">")
            _stringBuilder.Append("        <div class=" + """" + "modal-content modal_inlineBlock" + """" + " >")
            _stringBuilder.Append("            <div class=" + """" + "modal-header" + """" + ">")
            _stringBuilder.Append("                <button type=" + """" + "button" + """" + " class=" + """" + "close" + """" + " data-dismiss=" + """" + "modal" + """" + " aria-hidden=" + """" + "true" + """" + ">&times;</button>")
            _stringBuilder.Append("                <h4 class=" + """" + "modal-title" + """" + ">Error!</h4>")
            _stringBuilder.Append("")
            _stringBuilder.Append("            </div>")
            _stringBuilder.Append("            <div class=" + """" + "modal-body" + """" + " style=" + """" + "width:100%; margin:10px; " + """" + ">")
            _stringBuilder.Append("                <div style=" + """" + " overflow: auto;" + """" + ">")
            _stringBuilder.Append("			        " + _message + "")
            _stringBuilder.Append("                </div>")
            _stringBuilder.Append("            </div>")
            _stringBuilder.Append("        </div>")
            _stringBuilder.Append("    </div>")
            _stringBuilder.Append("</div>")

            Return _stringBuilder.ToString()

        End Function




    End Class
End Namespace

