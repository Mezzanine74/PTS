Imports System.Data.SqlClient
Partial Class ContractClientData
  Inherits System.Web.UI.Page

  Protected Sub DropDownListProject_DataBound(sender As Object, e As System.EventArgs)
    Dim DropDownListProject As DropDownList = sender
    Dim lst2 As New ListItem("Select Project", "0")
    DropDownListProject.Items.Insert(0, lst2)
  End Sub

  Protected Sub FormViewContractClientData_ItemDeleted(sender As Object, e As System.Web.UI.WebControls.FormViewDeletedEventArgs) Handles FormViewContractClientData.ItemDeleted
    ChangeModeAccordingly(sender)
  End Sub

  Protected Sub FormViewContractClientData_ItemInserted(sender As Object, e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FormViewContractClientData.ItemInserted
    ChangeModeAccordingly(sender)
  End Sub

  Protected Sub FormViewContractClientData_ItemInserting(sender As Object, e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormViewContractClientData.ItemInserting
    ' Insert aLL required parameters here
    e.Values("ContractID") = Request.QueryString("ContractID")

    Dim DropDownListProject As DropDownList = FormViewContractClientData.FindControl("DropDownListProject")
    If DropDownListProject.SelectedValue = 0 Then
      e.Cancel = True
      Exit Sub
    Else
      e.Values("ProjectID_PTS") = DropDownListProject.SelectedValue
    End If

    Dim TextBoxProjectManager As TextBox = FormViewContractClientData.FindControl("TextBoxProjectManager")
    If String.IsNullOrEmpty(TextBoxProjectManager.Text) Then
      e.Values("ProjectManager") = Nothing
    Else
      e.Values("ProjectManager") = TextBoxProjectManager.Text
    End If

    Dim TextBoxStartDate As TextBox = FormViewContractClientData.FindControl("TextBoxStartDate")
    If String.IsNullOrEmpty(TextBoxStartDate.Text) Then
      e.Values("ProjectStartDate") = Nothing
    Else
      e.Values("ProjectStartDate") = Convert.ToDateTime(TextBoxStartDate.Text)
    End If

    Dim TextBoxFinishDate As TextBox = FormViewContractClientData.FindControl("TextBoxFinishDate")
    If String.IsNullOrEmpty(TextBoxFinishDate.Text) Then
      e.Values("ProjectFinishDate") = Nothing
    Else
      e.Values("ProjectFinishDate") = Convert.ToDateTime(TextBoxFinishDate.Text)
    End If

    Dim TextBoxContractAmountWithVAT As TextBox = _
      FormViewContractClientData.FindControl("TextBoxContractAmountWithVAT")
    If String.IsNullOrEmpty(TextBoxContractAmountWithVAT.Text) Then
      e.Values("ContractAmountWithVAT") = Nothing
    Else
      e.Values("ContractAmountWithVAT") = Convert.ToDecimal(TextBoxContractAmountWithVAT.Text)
    End If

    Dim TextBoxPaymentTermsReCurrency As TextBox = _
      FormViewContractClientData.FindControl("TextBoxPaymentTermsReCurrency")
    If String.IsNullOrEmpty(TextBoxPaymentTermsReCurrency.Text) Then
      e.Values("PaymentTermsReCurrency") = Nothing
    Else
      e.Values("PaymentTermsReCurrency") = TextBoxPaymentTermsReCurrency.Text
    End If

    Dim TextBoxAdvancePercent As TextBox = _
      FormViewContractClientData.FindControl("TextBoxAdvancePercent")
    If String.IsNullOrEmpty(TextBoxAdvancePercent.Text) Then
      e.Values("AdvancePercent") = Nothing
    Else
      e.Values("AdvancePercent") = Convert.ToInt32(TextBoxAdvancePercent.Text)
    End If

    Dim TextBoxRetentionPercent As TextBox = _
          FormViewContractClientData.FindControl("TextBoxRetentionPercent")
    If String.IsNullOrEmpty(TextBoxRetentionPercent.Text) Then
      e.Values("RetentionPercent") = Nothing
    Else
      e.Values("RetentionPercent") = Convert.ToInt32(TextBoxRetentionPercent.Text)
    End If

    Dim TextBoxRetentionTerms As TextBox = _
      FormViewContractClientData.FindControl("TextBoxRetentionTerms")
    If String.IsNullOrEmpty(TextBoxRetentionTerms.Text) Then
      e.Values("RetentionTerms") = Nothing
    Else
      e.Values("RetentionTerms") = TextBoxRetentionTerms.Text
    End If

    Dim TextBoxRetentionTermsComments As TextBox = _
  FormViewContractClientData.FindControl("TextBoxRetentionTermsComments")
    If String.IsNullOrEmpty(TextBoxRetentionTermsComments.Text) Then
      e.Values("RetentionTermsAddComment") = Nothing
    Else
      e.Values("RetentionTermsAddComment") = TextBoxRetentionTermsComments.Text
    End If

    Dim TextBoxValidatedBOQmarginPercent As TextBox = _
          FormViewContractClientData.FindControl("TextBoxValidatedBOQmarginPercent")
    If String.IsNullOrEmpty(TextBoxValidatedBOQmarginPercent.Text) Then
      e.Values("ValidatedBOQmarginPercent") = Nothing
    Else
      e.Values("ValidatedBOQmarginPercent") = Convert.ToInt32(TextBoxValidatedBOQmarginPercent.Text)
    End If

    Dim TextBoxIVexpectedSubmissionDate As TextBox = _
      FormViewContractClientData.FindControl("TextBoxIVexpectedSubmissionDate")
    If String.IsNullOrEmpty(TextBoxIVexpectedSubmissionDate.Text) Then
      e.Values("IVexpectedSubmissionDate") = Nothing
    Else
      e.Values("IVexpectedSubmissionDate") = Convert.ToDateTime(TextBoxIVexpectedSubmissionDate.Text)
    End If

    Dim TextBoxIVexpectedApprovalDate As TextBox = _
        FormViewContractClientData.FindControl("TextBoxIVexpectedApprovalDate")
    If String.IsNullOrEmpty(TextBoxIVexpectedApprovalDate.Text) Then
      e.Values("IvexpectedApprovalDate") = Nothing
    Else
      e.Values("IvexpectedApprovalDate") = Convert.ToDateTime(TextBoxIVexpectedApprovalDate.Text)
    End If

    Dim TextBoxExpectedPaymentDate As TextBox = _
        FormViewContractClientData.FindControl("TextBoxExpectedPaymentDate")
    If String.IsNullOrEmpty(TextBoxExpectedPaymentDate.Text) Then
      e.Values("ExpectedPaymentDate") = Nothing
    Else
      e.Values("ExpectedPaymentDate") = Convert.ToDateTime(TextBoxExpectedPaymentDate.Text)
    End If

    Dim CheckBoxPenaltyClause As CheckBox = _
    FormViewContractClientData.FindControl("CheckBoxPenaltyClause")
    If Not CheckBoxPenaltyClause.Checked Then
      e.Values("PenaltyClause") = False
    Else
      e.Values("PenaltyClause") = True
    End If

    Dim TextBoxPenaltyTerms As TextBox = _
    FormViewContractClientData.FindControl("TextBoxPenaltyTerms")
    If String.IsNullOrEmpty(TextBoxPenaltyTerms.Text) Then
      e.Values("PenaltyTerms") = Nothing
    Else
      e.Values("PenaltyTerms") = TextBoxPenaltyTerms.Text
    End If

    Dim zoneId As String = "Russian Standard Time"
    Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
    Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

    e.Values("CreatedBy") = result
    e.Values("PersonCreated") = Page.User.Identity.Name.ToString


  End Sub

  Protected Sub FormViewContractClientData_ItemUpdated(sender As Object, e As System.Web.UI.WebControls.FormViewUpdatedEventArgs) Handles FormViewContractClientData.ItemUpdated
    ChangeModeAccordingly(sender)
  End Sub

  Protected Sub ChangeModeAccordingly(ByVal FormViewToManuplate As FormView)

    Dim LinkButtonEdit As LinkButton = FormViewToManuplate.FindControl("LinkButtonEdit")
    Dim LinkButtonDelete As LinkButton = FormViewToManuplate.FindControl("LinkButtonDelete")

    If Page.User.IsInRole("AddClientData") Then
      ' define which mode to OPEN
      If ContractExist(Request.QueryString("ContractID")) = True Then
        FormViewToManuplate.ChangeMode(FormViewMode.ReadOnly)
        If LinkButtonEdit IsNot Nothing Then
          LinkButtonEdit.Visible = True
          LinkButtonDelete.Visible = True
        End If
      Else
        FormViewToManuplate.ChangeMode(FormViewMode.Insert)
      End If
    Else
      FormViewToManuplate.ChangeMode(FormViewMode.ReadOnly)
      If LinkButtonEdit IsNot Nothing Then
        LinkButtonEdit.Visible = False
        LinkButtonDelete.Visible = False
      End If
    End If


  End Sub

  Protected Sub FormViewContractClientData_ItemUpdating(sender As Object, e As System.Web.UI.WebControls.FormViewUpdateEventArgs) Handles FormViewContractClientData.ItemUpdating
    ' Update aLL required parameters here

    Dim DropDownListProject As DropDownList = FormViewContractClientData.FindControl("DropDownListProject")
    If DropDownListProject.SelectedValue = 0 Then
      e.Cancel = True
      Exit Sub
    Else
      e.NewValues("ProjectID_PTS") = DropDownListProject.SelectedValue
    End If

    Dim TextBoxProjectManager As TextBox = FormViewContractClientData.FindControl("TextBoxProjectManager")
    If String.IsNullOrEmpty(TextBoxProjectManager.Text) Then
      e.NewValues("ProjectManager") = Nothing
    Else
      e.NewValues("ProjectManager") = TextBoxProjectManager.Text
    End If

    Dim TextBoxStartDate As TextBox = FormViewContractClientData.FindControl("TextBoxStartDate")
    If String.IsNullOrEmpty(TextBoxStartDate.Text) Then
      e.NewValues("ProjectStartDate") = Nothing
    Else
      e.NewValues("ProjectStartDate") = Convert.ToDateTime(TextBoxStartDate.Text)
    End If

    Dim TextBoxFinishDate As TextBox = FormViewContractClientData.FindControl("TextBoxFinishDate")
    If String.IsNullOrEmpty(TextBoxFinishDate.Text) Then
      e.NewValues("ProjectFinishDate") = Nothing
    Else
      e.NewValues("ProjectFinishDate") = Convert.ToDateTime(TextBoxFinishDate.Text)
    End If

    Dim TextBoxContractAmountWithVAT As TextBox = _
      FormViewContractClientData.FindControl("TextBoxContractAmountWithVAT")
    If String.IsNullOrEmpty(TextBoxContractAmountWithVAT.Text) Then
      e.NewValues("ContractAmountWithVAT") = Nothing
    Else
      e.NewValues("ContractAmountWithVAT") = Convert.ToDecimal(TextBoxContractAmountWithVAT.Text)
    End If

    Dim TextBoxPaymentTermsReCurrency As TextBox = _
      FormViewContractClientData.FindControl("TextBoxPaymentTermsReCurrency")
    If String.IsNullOrEmpty(TextBoxPaymentTermsReCurrency.Text) Then
      e.NewValues("PaymentTermsReCurrency") = Nothing
    Else
      e.NewValues("PaymentTermsReCurrency") = TextBoxPaymentTermsReCurrency.Text
    End If

    Dim TextBoxAdvancePercent As TextBox = _
      FormViewContractClientData.FindControl("TextBoxAdvancePercent")
    If String.IsNullOrEmpty(TextBoxAdvancePercent.Text) Then
      e.NewValues("AdvancePercent") = Nothing
    Else
      e.NewValues("AdvancePercent") = Convert.ToInt32(TextBoxAdvancePercent.Text)
    End If

    Dim TextBoxRetentionPercent As TextBox = _
          FormViewContractClientData.FindControl("TextBoxRetentionPercent")
    If String.IsNullOrEmpty(TextBoxRetentionPercent.Text) Then
      e.NewValues("RetentionPercent") = Nothing
    Else
      e.NewValues("RetentionPercent") = Convert.ToInt32(TextBoxRetentionPercent.Text)
    End If

    Dim TextBoxRetentionTerms As TextBox = _
      FormViewContractClientData.FindControl("TextBoxRetentionTerms")
    If String.IsNullOrEmpty(TextBoxRetentionTerms.Text) Then
      e.NewValues("RetentionTerms") = Nothing
    Else
      e.NewValues("RetentionTerms") = TextBoxRetentionTerms.Text
    End If

    Dim TextBoxRetentionTermsComments As TextBox = _
  FormViewContractClientData.FindControl("TextBoxRetentionTermsComments")
    If String.IsNullOrEmpty(TextBoxRetentionTermsComments.Text) Then
      e.NewValues("RetentionTermsAddComment") = Nothing
    Else
      e.NewValues("RetentionTermsAddComment") = TextBoxRetentionTermsComments.Text
    End If

    Dim TextBoxValidatedBOQmarginPercent As TextBox = _
          FormViewContractClientData.FindControl("TextBoxValidatedBOQmarginPercent")
    If String.IsNullOrEmpty(TextBoxValidatedBOQmarginPercent.Text) Then
      e.NewValues("ValidatedBOQmarginPercent") = Nothing
    Else
      e.NewValues("ValidatedBOQmarginPercent") = Convert.ToInt32(TextBoxValidatedBOQmarginPercent.Text)
    End If

    Dim TextBoxIVexpectedSubmissionDate As TextBox = _
      FormViewContractClientData.FindControl("TextBoxIVexpectedSubmissionDate")
    If String.IsNullOrEmpty(TextBoxIVexpectedSubmissionDate.Text) Then
      e.NewValues("IVexpectedSubmissionDate") = Nothing
    Else
      e.NewValues("IVexpectedSubmissionDate") = Convert.ToDateTime(TextBoxIVexpectedSubmissionDate.Text)
    End If

    Dim TextBoxIVexpectedApprovalDate As TextBox = _
        FormViewContractClientData.FindControl("TextBoxIVexpectedApprovalDate")
    If String.IsNullOrEmpty(TextBoxIVexpectedApprovalDate.Text) Then
      e.NewValues("IvexpectedApprovalDate") = Nothing
    Else
      e.NewValues("IvexpectedApprovalDate") = Convert.ToDateTime(TextBoxIVexpectedApprovalDate.Text)
    End If

    Dim TextBoxExpectedPaymentDate As TextBox = _
        FormViewContractClientData.FindControl("TextBoxExpectedPaymentDate")
    If String.IsNullOrEmpty(TextBoxExpectedPaymentDate.Text) Then
      e.NewValues("ExpectedPaymentDate") = Nothing
    Else
      e.NewValues("ExpectedPaymentDate") = Convert.ToDateTime(TextBoxExpectedPaymentDate.Text)
    End If

    Dim CheckBoxPenaltyClause As CheckBox = _
    FormViewContractClientData.FindControl("CheckBoxPenaltyClause")
    If Not CheckBoxPenaltyClause.Checked Then
      e.NewValues("PenaltyClause") = False
    Else
      e.NewValues("PenaltyClause") = True
    End If

    Dim TextBoxPenaltyTerms As TextBox = _
    FormViewContractClientData.FindControl("TextBoxPenaltyTerms")
    If String.IsNullOrEmpty(TextBoxPenaltyTerms.Text) Then
      e.NewValues("PenaltyTerms") = Nothing
    Else
      e.NewValues("PenaltyTerms") = TextBoxPenaltyTerms.Text
    End If

    Dim zoneId As String = "Russian Standard Time"
    Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
    Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

    e.NewValues("UpdatedBy") = result
    e.NewValues("PersonUpdated") = Page.User.Identity.Name.ToString

  End Sub

  Protected Sub FormViewContractClientData_Load(sender As Object, e As System.EventArgs) Handles FormViewContractClientData.Load
    If Not IsPostBack Then
      ChangeModeAccordingly(sender)
    End If
  End Sub

  Protected Function ContractExist(ByVal ContractID_ As Integer) As Boolean
    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    con.Open()
    Dim sqlstring As String = " SELECT count([ContractID]) FROM [Table_ContractClientData] WHERE ContractID = @ContractID "
    Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text

        'syntax for parameter adding
        Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
        ContractID.Value = ContractID_
    Dim dr As SqlDataReader = cmd.ExecuteReader
    Dim returnValue As Boolean = False
    While dr.Read
      If dr(0) = 0 Then
        returnValue = False
      ElseIf dr(0) = 1 Then
        returnValue = True
      End If
    End While
    Return returnValue
    con.Close()
    dr.Close()
  End Function

  Protected Sub LinkButtonEdit_Click(sender As Object, e As System.EventArgs)
    FormViewContractClientData.ChangeMode(FormViewMode.Edit)
  End Sub

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

  End Sub
End Class
