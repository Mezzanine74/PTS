Imports System.Data.SqlClient

Partial Class PoRequest
    Inherits System.Web.UI.Page
    Dim Notification As New _GiveNotification
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not (Roles.IsUserInRole("LawyerOnSite") Or Roles.IsUserInRole("ContractLeadGirls") Or Roles.IsUserInRole("InitiateContractAndAddendum")) Then
            ' user not to allow
            Response.Redirect("~/webforms/AccessDenied.aspx")
            Exit Sub

        End If

        ' Check CheckBoxes to enable or disable Validation Controls on DropdownList CostCode
        If CheckBoxFrameContract.Checked = False Then
            CompareValidatorCostCodeBudget.Enabled = True
            HighlightCheckBox(CheckBoxFrameContract, 0)
        End If

        SqlDataSourceProject.SelectParameters("UserName").DefaultValue = _
            Page.User.Identity.Name.ToLower.ToString
        SqlDataSourceCostCode.SelectParameters("UserName").DefaultValue = _
            Page.User.Identity.Name.ToLower.ToString

        If Session("_ProjectID") IsNot Nothing Then
            DropDownListProject.SelectedValue = Convert.ToInt16(Session("_ProjectID"))
            _DropDownListProject_SelectedIndexChanged(Convert.ToInt16(Session("_ProjectID")))
            Session.Remove("_ProjectID")
        End If

        If CheckBoxFrameContract.Checked = True Then
            CompareValidatorPoValue.Enabled = False
        Else
            CompareValidatorPoValue.Enabled = True
        End If


        Dim _newvalue As Decimal = 0.0

        _newvalue = Math.Round(IIf(IsNumeric(TotalPriceTextBox.Text), TotalPriceTextBox.Text, 0) / ((100 + (IIf(IsNumeric(TextBoxVATpercent.Text), TextBoxVATpercent.Text, 0))) / 100), 2)

        If PTS_MERCURY.helper.Table_Budget.GetBudgetEmailControlFailed(Page, _
                                                                       IIf(DropDownListProject.SelectedIndex > 0, DropDownListProject.SelectedValue, 0), _
                                                                       DropDownListCostCode.SelectedValue.ToString().Trim(), _
                                                                       0, _
                                                                       _newvalue) = _
                                                                   True Then

            ButtonSendRequest.Enabled = False

        Else

            ButtonSendRequest.Enabled = True

        End If


    End Sub

    Protected Sub ImageButtonRemoveSession_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonRemoveSession.Click
        ' remove all scenario sessions
        SessionsPoRequest.RemoveALLScenarioSession()
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

        ' CHECK Scenario Sessions if there exist or not
        ' Show or Hide Panels depends on Scenario sessions
        If SessionsPoRequest.ScenarioSessionExist("Scenario-1") OrElse _
            SessionsPoRequest.ScenarioSessionExist("Scenario1") OrElse _
             SessionsPoRequest.ScenarioSessionExist("Scenario2") OrElse _
              SessionsPoRequest.ScenarioSessionExist("Scenario3") OrElse _
               SessionsPoRequest.ScenarioSessionExist("Scenario4") OrElse _
                SessionsPoRequest.ScenarioSessionExist("Scenario5") OrElse _
                 SessionsPoRequest.ScenarioSessionExist("Scenario6") OrElse _
                  SessionsPoRequest.ScenarioSessionExist("Scenario7") Then
            ShowHidePanels("Session")
        Else
            ShowHidePanels("POrequest")
        End If

        If SessionsPoRequest.ScenarioSessionExist("Scenario-1") Then
            ImageButtonFrameContract.Visible = True
        Else
            ImageButtonFrameContract.Visible = False
        End If ' end Scenario -1 

        If SessionsPoRequest.ScenarioSessionExist("Scenario1") Then
            ImageButtonContinueScenario1.Visible = True
            If ContractView.SmallContract() = True Then
                ImageButtonContinueScenario1.PostBackUrl = "~/webforms/contractenterNew.aspx"
            ElseIf ContractView.SmallContract() = False Then
                ImageButtonContinueScenario1.PostBackUrl = "~/webforms/pocreateNew.aspx"
            End If
        Else
            ImageButtonContinueScenario1.Visible = False
        End If ' end Scenario 1 

        If SessionsPoRequest.ScenarioSessionExist("Scenario2") Then
            ImageButtonContinueScenario2.Visible = True
        Else
            ImageButtonContinueScenario2.Visible = False
        End If ' end Scenario 2

        If SessionsPoRequest.ScenarioSessionExist("Scenario3") Then
            ImageButtonContinueScenario3.Visible = True
        Else
            ImageButtonContinueScenario3.Visible = False
        End If ' end Scenario 3 

        If SessionsPoRequest.ScenarioSessionExist("Scenario4") Then
            ImageButtonContinueScenario4.Visible = True
        Else
            ImageButtonContinueScenario4.Visible = False
        End If ' end Scenario 4 

        If SessionsPoRequest.ScenarioSessionExist("Scenario5") Then
            ImageButtonContinueScenario5.Visible = True
        Else
            ImageButtonContinueScenario5.Visible = False
        End If ' end Scenario 5 

        If SessionsPoRequest.ScenarioSessionExist("Scenario6") Then
            ImageButtonContinueScenario6.Visible = True
        Else
            ImageButtonContinueScenario6.Visible = False
        End If ' end Scenario 6

        If SessionsPoRequest.ScenarioSessionExist("Scenario7") Then
            ImageButtonContinueScenario7.Visible = True
        Else
            ImageButtonContinueScenario7.Visible = False
        End If ' end Scenario 7 

    End Sub

    Protected Sub DropDownListProject_DataBound(sender As Object, e As System.EventArgs) Handles DropDownListProject.DataBound
        Dim lst As New ListItem("Select Project", "0")
        DropDownListProject.Items.Insert(0, lst)
    End Sub

    Protected Sub DropDownListProject_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListProject.SelectedIndexChanged

        _DropDownListProject_SelectedIndexChanged(DropDownListProject.SelectedValue)

    End Sub

    Protected Sub _DropDownListProject_SelectedIndexChanged(ByVal _DropDownListSelectedItem As Integer)

        ' reset cost Code error
        TextBoxCostCodeError.Text = "Valid"

        If RequestedByRequired(_DropDownListSelectedItem) = True Then
            DropDownListRequestedBy.Visible = True
            LabelRequestedBy.Visible = True
            CompareValidatorRequested.Enabled = True
        Else
            DropDownListRequestedBy.Visible = False
            LabelRequestedBy.Visible = False
            CompareValidatorRequested.Enabled = False
        End If

    End Sub

    Protected Sub DropDownListCostCode_SelectedIndexChanged(sender As Object, e As System.EventArgs)

        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " SELECT     RTRIM(Type) AS Type" + _
                                                " FROM Table1_Project" + _
                                                " WHERE     ProjectID =" + "'" + DropDownListProject.SelectedValue.ToString + "'"
        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim dr As SqlDataReader = cmd.ExecuteReader
        While dr.Read
            Dim ProjectType As String
            ProjectType = dr(0).ToString()
            If ProjectType = "DataCenter" Then
                If DropDownListCostCode.SelectedValue.ToString = "0" Then
                    TextBoxCostCodeError.Text = "Valid"
                    DropDownListCurrency.Enabled = True
                ElseIf Len(DropDownListCostCode.SelectedValue.ToString) = 10 Then
                    TextBoxCostCodeError.Text = "Valid"
                    DropDownListCurrency.Enabled = True
                ElseIf Len(DropDownListCostCode.SelectedValue.ToString) = 1 AndAlso Not IsNumeric(DropDownListCostCode.SelectedValue.ToString) Then
                    TextBoxCostCodeError.Text = "NotValid"
                    CompareValidatorCostCode.Validate()
                    DropDownListCurrency.Enabled = True
                ElseIf Len(DropDownListCostCode.SelectedValue.ToString) > 1 AndAlso Len(DropDownListCostCode.SelectedValue.ToString) < 10 Then
                    If Roles.IsUserInRole("Finance") Then
                        If IsNumeric(DropDownListCostCode.SelectedValue.ToString) Then
                            TextBoxCostCodeError.Text = "Valid"
                            DropDownListCurrency.SelectedValue = "Rub"
                            DropDownListCurrency.Enabled = False
                        ElseIf Not IsNumeric(DropDownListCostCode.SelectedValue.ToString) Then
                            TextBoxCostCodeError.Text = "NotValid"
                            CompareValidatorCostCode.Validate()
                            DropDownListCurrency.Enabled = True
                        End If
                    Else
                        TextBoxCostCodeError.Text = "NotValid"
                        CompareValidatorCostCode.Validate()
                        DropDownListCurrency.Enabled = True
                    End If
                End If
            Else
                TextBoxCostCodeError.Text = "Valid"

                If GetCostCodeType(DropDownListCostCode.SelectedValue.ToString) = "Finance" Then
                    DropDownListCurrency.SelectedValue = "Rub"
                    DropDownListCurrency.Enabled = False
                Else
                    DropDownListCurrency.Enabled = True
                End If

            End If
        End While
        con.Close()
        dr.Close()

    End Sub

    Protected Sub DropDownListRequestedBy_DataBound(sender As Object, e As System.EventArgs)
        Dim lst2 As New ListItem("Select Person", "0")
        DropDownListRequestedBy.Items.Insert(0, lst2)
    End Sub

    Protected Sub ButtonSendRequest_Click(sender As Object, e As EventArgs)

        Dim TotalPriceIncVAT As Decimal = Convert.ToDecimal(TotalPriceTextBox.Text)
        Dim VAT As Decimal = Convert.ToDecimal(TextBoxVATpercent.Text)

        ' Calculate Scenario on different class to use both on Addendum and Contract Page
        Dim _Scenario As Integer =
          CalculateScenario.Calculate(TotalPriceIncVAT, VAT, DropDownListCurrency.SelectedValue.ToString)

        If CheckBoxFrameContract.Checked Then
            ' Scenario -1
            If SessionsPoRequest.ScenarioSessionExist("Scenario-1") Then
                Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Dublicating Scenario!</p>")
                Exit Sub
            End If
            AssignSessionVariables("Scenario-1")
            SessionsPoRequest.DeleteOtherSessions("Scenario-1")
            Response.Redirect("~/webforms/contractenterNew.aspx")
        ElseIf _Scenario = 1 And CheckBoxSmallContract.Checked = False Then
            ' Scenario 1
            If SessionsPoRequest.ScenarioSessionExist("Scenario1") Then
                Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Dublicating Scenario!</p>")
                Exit Sub
            End If
            AssignSessionVariables("Scenario1")
            SessionsPoRequest.DeleteOtherSessions("Scenario1")
            Response.Redirect("~/webforms/pocreateNew.aspx")
        ElseIf _Scenario = 1 And CheckBoxSmallContract.Checked = True Then
            ' Scenario 1
            If SessionsPoRequest.ScenarioSessionExist("Scenario1") Then
                Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Dublicating Scenario!</p>")
                Exit Sub
            End If
            AssignSessionVariables("Scenario1")
            SessionsPoRequest.DeleteOtherSessions("Scenario1")
            Response.Redirect("~/webforms/contractenterNew.aspx")
        ElseIf _Scenario = 2 Then
            If SessionsPoRequest.ScenarioSessionExist("Scenario2") Then
                Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Dublicating Scenario!</p>")
                Exit Sub
            End If
            AssignSessionVariables("Scenario2")
            SessionsPoRequest.DeleteOtherSessions("Scenario2")
            Response.Redirect("~/webforms/contractenterNew.aspx")
            'Response.Write("Scenario 2 >" + TotalInEuroExcVAT.ToString)
        ElseIf _Scenario = 3 Then
            If SessionsPoRequest.ScenarioSessionExist("Scenario3") Then
                Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Dublicating Scenario!</p>")
                Exit Sub
            End If
            AssignSessionVariables("Scenario3")
            SessionsPoRequest.DeleteOtherSessions("Scenario3")
            Response.Redirect("~/webforms/contractenterNew.aspx")
            'Response.Write("Scenario 3 >" + TotalInEuroExcVAT.ToString)
        ElseIf _Scenario = 4 Then
            If SessionsPoRequest.ScenarioSessionExist("Scenario4") Then
                Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Dublicating Scenario!</p>")
                Exit Sub
            End If
            AssignSessionVariables("Scenario4")
            SessionsPoRequest.DeleteOtherSessions("Scenario4")
            Response.Redirect("~/webforms/contractenterNew.aspx")
            'Response.Write("Scenario 4 >" + TotalInEuroExcVAT.ToString)
        ElseIf _Scenario = 5 Then
            If SessionsPoRequest.ScenarioSessionExist("Scenario5") Then
                Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Dublicating Scenario!</p>")
                Exit Sub
            End If
            AssignSessionVariables("Scenario5")
            SessionsPoRequest.DeleteOtherSessions("Scenario5")
            Response.Redirect("~/webforms/contractenterNew.aspx")
            'Response.Write("Scenario 5 >" + TotalInEuroExcVAT.ToString)
        ElseIf _Scenario = 6 Then
            If SessionsPoRequest.ScenarioSessionExist("Scenario6") Then
                Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Dublicating Scenario!</p>")
                Exit Sub
            End If
            AssignSessionVariables("Scenario6")
            SessionsPoRequest.DeleteOtherSessions("Scenario6")
            Response.Redirect("~/webforms/contractenterNew.aspx")
            'Response.Write("Scenario 6 >" + TotalInEuroExcVAT.ToString)
        ElseIf _Scenario = 7 Then
            If SessionsPoRequest.ScenarioSessionExist("Scenario7") Then
                Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Dublicating Scenario!</p>")
                Exit Sub
            End If
            AssignSessionVariables("Scenario7")
            SessionsPoRequest.DeleteOtherSessions("Scenario7")
            Response.Redirect("~/webforms/contractenterNew.aspx")
            'Response.Write("Scenario 7 >" + TotalInEuroExcVAT.ToString)
        End If

    End Sub

    Protected Sub AssignSessionVariables(ByVal _Scenario As String)

        ' Assign Values
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " INSERT INTO [Table_Scenario] " +
                            "            ([Scenario] " +
                            "            ,[ProjectID] " +
                            "            ,[TotalPrice] " +
                            "            ,[VAT] " +
                            "            ,[Currency] " +
                            "            ,[CostCode] " +
                            "            ,[RequestedBy] " +
                            "            ,[Nominated] " +
                            "            ,[FrameContract] " +
                            "            ,[SmallContract]) " +
                            "      VALUES " +
                            "            (@Scenario " +
                            "            ,@ProjectID " +
                            "            ,@TotalPrice " +
                            "            ,@VAT " +
                            "            ,@Currency " +
                            "            ,@CostCode  " +
                            "            ,@RequestedBy " +
                            "            ,@Nominated " +
                            "            ,@FrameContract " +
                            "            ,@SmallContract) "

        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text

        'syntax for parameter adding
        Dim userName As String = Page.User.Identity.Name.ToLower.ToString
        Dim Scenario As SqlParameter = cmd.Parameters.Add("@Scenario", System.Data.SqlDbType.NVarChar)
        Scenario.Value = userName + _Scenario

        Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.SmallInt)
        ProjectID.Value = DropDownListProject.SelectedValue

        Dim TotalPrice As SqlParameter = cmd.Parameters.Add("@TotalPrice", System.Data.SqlDbType.Decimal)
        TotalPrice.Value = TotalPriceTextBox.Text

        Dim VAT As SqlParameter = cmd.Parameters.Add("@VAT", System.Data.SqlDbType.Decimal)
        VAT.Value = TextBoxVATpercent.Text

        Dim Currency As SqlParameter = cmd.Parameters.Add("@Currency", System.Data.SqlDbType.NVarChar)
        Currency.Value = DropDownListCurrency.SelectedValue

        Dim CostCode As SqlParameter = cmd.Parameters.Add("@CostCode", System.Data.SqlDbType.NVarChar)
        CostCode.Value = DropDownListCostCode.SelectedValue

        Dim RequestedBy As SqlParameter = cmd.Parameters.Add("@RequestedBy", System.Data.SqlDbType.NVarChar)
        If Not String.IsNullOrEmpty(DropDownListRequestedBy.SelectedValue.ToString) Then
            RequestedBy.Value = DropDownListRequestedBy.SelectedValue
        Else
            RequestedBy.Value = DBNull.Value
        End If

        Dim Nominated As SqlParameter = cmd.Parameters.Add("@Nominated", System.Data.SqlDbType.Bit)
        Nominated.Value = CheckBoxNominated.Checked

        Dim FrameContract As SqlParameter = cmd.Parameters.Add("@FrameContract", System.Data.SqlDbType.SmallInt)
        FrameContract.Value = CheckBoxFrameContract.Checked

        Dim SmallContract As SqlParameter = cmd.Parameters.Add("@SmallContract", System.Data.SqlDbType.Bit)
        If CheckBoxSmallContract.Checked = True Then
            SmallContract.Value = True
        Else
            SmallContract.Value = False
        End If

        Dim dr As SqlDataReader = cmd.ExecuteReader
        con.Close()
        dr.Close()

    End Sub

    Protected Function GetCostCodeType(ByVal _CostCode As String) As String
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = "SELECT Rtrim([Type]) AS Type FROM [Table7_CostCode] WHERE CostCode = @CostCode"
        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        'syntax for parameter adding
        Dim CostCode As SqlParameter = cmd.Parameters.Add("@CostCode", System.Data.SqlDbType.NVarChar)
        CostCode.Value = _CostCode
        Dim ReturnValue As String = ""
        Dim dr As SqlDataReader = cmd.ExecuteReader
        While dr.Read
            ReturnValue = dr(0).ToString
        End While
        Return ReturnValue
        con.Close()
        dr.Close()
    End Function

    Protected Function RequestedByRequired(ByVal projectID_ As Integer) As Boolean
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " select RequestedRequired from Table1_Project where ProjectID = @ProjectID "
        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        'syntax for parameter adding
        Dim PrjID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
        PrjID.Value = projectID_
        Dim ReturnValue As Boolean
        Dim dr As SqlDataReader = cmd.ExecuteReader
        While dr.Read
            ReturnValue = dr(0)
        End While
        Return ReturnValue
        con.Close()
        dr.Close()
    End Function

    Protected Sub ShowHidePanels(ByVal _Status As String)
        If _Status = "Session" Then
            PanelExistingSession.Visible = True
            PanelPOrequest.Visible = False
        ElseIf _Status = "POrequest" Then
            PanelExistingSession.Visible = False
            PanelPOrequest.Visible = True
        End If
    End Sub

    Protected Sub CheckBoxFrameContract_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxFrameContract.CheckedChanged
        ' if Checked, PO value, VAT and CostCode to be frozen as ZERO. Validation controls also to be deactivated
        Dim _Checkbox As CheckBox = sender
        If _Checkbox.Checked Then
            ' PO value to be ZERO and frozen
            TotalPriceTextBox.Text = "0.00"
            TotalPriceTextBox.Enabled = False

            ' VAT to be ZERO and frozen
            TextBoxVATpercent.Text = "0"
            TextBoxVATpercent.Enabled = False

            ' CostCode value to be ZERO and frozen
            DropDownListCostCode.SelectedValue = "0"
            DropDownListCostCode.Enabled = True
            CompareValidatorCostCodeBudget.Enabled = True

            HighlightCheckBox(_Checkbox, 1)

        Else
            ' PO value to be ZERO and frozen
            TotalPriceTextBox.Text = String.Empty
            TotalPriceTextBox.Enabled = True

            ' VAT to be ZERO and frozen
            TextBoxVATpercent.Text = String.Empty
            TextBoxVATpercent.Enabled = True

            ' CostCode value to be ZERO and frozen
            DropDownListCostCode.SelectedValue = "0"
            DropDownListCostCode.Enabled = True
            CompareValidatorCostCodeBudget.Enabled = True

            HighlightCheckBox(_Checkbox, 0)

        End If

    End Sub

    Protected Sub HighlightCheckBox(ByVal _CheckBox As CheckBox, ByVal a As Integer)
        If a = 1 Then
            _CheckBox.BackColor = System.Drawing.Color.Lime
            _CheckBox.BorderStyle = BorderStyle.Solid
            _CheckBox.BorderColor = System.Drawing.Color.Red
            _CheckBox.BorderWidth = 5

        ElseIf a = 0 Then
            _CheckBox.BackColor = System.Drawing.Color.White
            _CheckBox.BorderStyle = BorderStyle.None

        End If
    End Sub

    Protected Sub CheckBoxNominated_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxNominated.CheckedChanged

        ' if Checked, highlight
        Dim _Checkbox As CheckBox = sender
        If _Checkbox.Checked Then

            HighlightCheckBox(_Checkbox, 1)

        Else

            HighlightCheckBox(_Checkbox, 0)

        End If

    End Sub

    Protected Sub CheckBoxSmallContract_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxSmallContract.CheckedChanged

        ' if Checked, highlight
        Dim _Checkbox As CheckBox = sender
        If _Checkbox.Checked Then

            HighlightCheckBox(_Checkbox, 1)

        Else

            HighlightCheckBox(_Checkbox, 0)

        End If

    End Sub

End Class
