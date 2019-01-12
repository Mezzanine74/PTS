Imports System.Data.SqlClient
Partial Class AddendumClientData
  Inherits System.Web.UI.Page

  Protected Sub FormViewAddendumClientData_ItemDeleted(sender As Object, e As System.Web.UI.WebControls.FormViewDeletedEventArgs) Handles FormViewAddendumClientData.ItemDeleted
    ChangeModeAccordingly(sender)
  End Sub

  Protected Sub FormViewAddendumClientData_ItemInserted(sender As Object, e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FormViewAddendumClientData.ItemInserted
    ChangeModeAccordingly(sender)
  End Sub

  Protected Sub FormViewAddendumClientData_ItemInserting(sender As Object, e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormViewAddendumClientData.ItemInserting
    ' Insert aLL required parameters here
    e.Values("AddendumID") = Request.QueryString("AddendumID")

    Dim TextBoxStartDate As TextBox = FormViewAddendumClientData.FindControl("TextBoxStartDate")
    If String.IsNullOrEmpty(TextBoxStartDate.Text) Then
      e.Values("ProjectStartDate") = Nothing
    Else
      e.Values("ProjectStartDate") = Convert.ToDateTime(TextBoxStartDate.Text)
    End If

    Dim TextBoxFinishDate As TextBox = FormViewAddendumClientData.FindControl("TextBoxFinishDate")
    If String.IsNullOrEmpty(TextBoxFinishDate.Text) Then
      e.Values("ProjectFinishDate") = Nothing
    Else
      e.Values("ProjectFinishDate") = Convert.ToDateTime(TextBoxFinishDate.Text)
    End If

    Dim TextBoxAddendumAmountWithVAT As TextBox = _
      FormViewAddendumClientData.FindControl("TextBoxAddendumAmountWithVAT")
    If String.IsNullOrEmpty(TextBoxAddendumAmountWithVAT.Text) Then
      e.Values("AddendumAmountWithVAT") = Nothing
    Else
      e.Values("AddendumAmountWithVAT") = Convert.ToDecimal(TextBoxAddendumAmountWithVAT.Text)
    End If

    Dim TextBoxPaymentTermsReCurrency As TextBox = _
      FormViewAddendumClientData.FindControl("TextBoxPaymentTermsReCurrency")
    If String.IsNullOrEmpty(TextBoxPaymentTermsReCurrency.Text) Then
      e.Values("PaymentTermsReCurrency") = Nothing
    Else
      e.Values("PaymentTermsReCurrency") = TextBoxPaymentTermsReCurrency.Text
    End If

    Dim TextBoxAdvancePercent As TextBox = _
      FormViewAddendumClientData.FindControl("TextBoxAdvancePercent")
    If String.IsNullOrEmpty(TextBoxAdvancePercent.Text) Then
      e.Values("AdvancePercent") = Nothing
    Else
      e.Values("AdvancePercent") = Convert.ToInt32(TextBoxAdvancePercent.Text)
    End If

    Dim TextBoxRetentionPercent As TextBox = _
          FormViewAddendumClientData.FindControl("TextBoxRetentionPercent")
    If String.IsNullOrEmpty(TextBoxRetentionPercent.Text) Then
      e.Values("RetentionPercent") = Nothing
    Else
      e.Values("RetentionPercent") = Convert.ToInt32(TextBoxRetentionPercent.Text)
    End If

    Dim TextBoxRetentionTerms As TextBox = _
      FormViewAddendumClientData.FindControl("TextBoxRetentionTerms")
    If String.IsNullOrEmpty(TextBoxRetentionTerms.Text) Then
      e.Values("RetentionTerms") = Nothing
    Else
      e.Values("RetentionTerms") = TextBoxRetentionTerms.Text
    End If

    Dim TextBoxRetentionTermsComments As TextBox = _
  FormViewAddendumClientData.FindControl("TextBoxRetentionTermsComments")
    If String.IsNullOrEmpty(TextBoxRetentionTermsComments.Text) Then
      e.Values("RetentionTermsAddComment") = Nothing
    Else
      e.Values("RetentionTermsAddComment") = TextBoxRetentionTermsComments.Text
    End If

    Dim TextBoxValidatedBOQmarginPercent As TextBox = _
          FormViewAddendumClientData.FindControl("TextBoxValidatedBOQmarginPercent")
    If String.IsNullOrEmpty(TextBoxValidatedBOQmarginPercent.Text) Then
      e.Values("ValidatedBOQmarginPercent") = Nothing
    Else
      e.Values("ValidatedBOQmarginPercent") = Convert.ToInt32(TextBoxValidatedBOQmarginPercent.Text)
    End If

    Dim TextBoxIVexpectedSubmissionDate As TextBox = _
      FormViewAddendumClientData.FindControl("TextBoxIVexpectedSubmissionDate")
    If String.IsNullOrEmpty(TextBoxIVexpectedSubmissionDate.Text) Then
      e.Values("IVexpectedSubmissionDate") = Nothing
    Else
      e.Values("IVexpectedSubmissionDate") = Convert.ToDateTime(TextBoxIVexpectedSubmissionDate.Text)
    End If

    Dim TextBoxIVexpectedApprovalDate As TextBox = _
        FormViewAddendumClientData.FindControl("TextBoxIVexpectedApprovalDate")
    If String.IsNullOrEmpty(TextBoxIVexpectedApprovalDate.Text) Then
      e.Values("IvexpectedApprovalDate") = Nothing
    Else
      e.Values("IvexpectedApprovalDate") = Convert.ToDateTime(TextBoxIVexpectedApprovalDate.Text)
    End If

    Dim TextBoxExpectedPaymentDate As TextBox = _
        FormViewAddendumClientData.FindControl("TextBoxExpectedPaymentDate")
    If String.IsNullOrEmpty(TextBoxExpectedPaymentDate.Text) Then
      e.Values("ExpectedPaymentDate") = Nothing
    Else
      e.Values("ExpectedPaymentDate") = Convert.ToDateTime(TextBoxExpectedPaymentDate.Text)
    End If

    Dim CheckBoxPenaltyClause As CheckBox = _
    FormViewAddendumClientData.FindControl("CheckBoxPenaltyClause")
    If Not CheckBoxPenaltyClause.Checked Then
      e.Values("PenaltyClause") = False
    Else
      e.Values("PenaltyClause") = True
    End If

    Dim TextBoxPenaltyTerms As TextBox = _
    FormViewAddendumClientData.FindControl("TextBoxPenaltyTerms")
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

  Protected Sub FormViewAddendumClientData_ItemUpdated(sender As Object, e As System.Web.UI.WebControls.FormViewUpdatedEventArgs) Handles FormViewAddendumClientData.ItemUpdated
    ChangeModeAccordingly(sender)
  End Sub

  Protected Sub FormViewAddendumClientData_ItemUpdating(sender As Object, e As System.Web.UI.WebControls.FormViewUpdateEventArgs) Handles FormViewAddendumClientData.ItemUpdating
    ' Update aLL required parameters here

    Dim TextBoxStartDate As TextBox = FormViewAddendumClientData.FindControl("TextBoxStartDate")
    If String.IsNullOrEmpty(TextBoxStartDate.Text) Then
      e.NewValues("ProjectStartDate") = Nothing
    Else
      e.NewValues("ProjectStartDate") = Convert.ToDateTime(TextBoxStartDate.Text)
    End If

    Dim TextBoxFinishDate As TextBox = FormViewAddendumClientData.FindControl("TextBoxFinishDate")
    If String.IsNullOrEmpty(TextBoxFinishDate.Text) Then
      e.NewValues("ProjectFinishDate") = Nothing
    Else
      e.NewValues("ProjectFinishDate") = Convert.ToDateTime(TextBoxFinishDate.Text)
    End If

    Dim TextBoxAddendumAmountWithVAT As TextBox = _
      FormViewAddendumClientData.FindControl("TextBoxAddendumAmountWithVAT")
    If String.IsNullOrEmpty(TextBoxAddendumAmountWithVAT.Text) Then
      e.NewValues("AddendumAmountWithVAT") = Nothing
    Else
      e.NewValues("AddendumAmountWithVAT") = Convert.ToDecimal(TextBoxAddendumAmountWithVAT.Text)
    End If

    Dim TextBoxPaymentTermsReCurrency As TextBox = _
      FormViewAddendumClientData.FindControl("TextBoxPaymentTermsReCurrency")
    If String.IsNullOrEmpty(TextBoxPaymentTermsReCurrency.Text) Then
      e.NewValues("PaymentTermsReCurrency") = Nothing
    Else
      e.NewValues("PaymentTermsReCurrency") = TextBoxPaymentTermsReCurrency.Text
    End If

    Dim TextBoxAdvancePercent As TextBox = _
      FormViewAddendumClientData.FindControl("TextBoxAdvancePercent")
    If String.IsNullOrEmpty(TextBoxAdvancePercent.Text) Then
      e.NewValues("AdvancePercent") = Nothing
    Else
      e.NewValues("AdvancePercent") = Convert.ToInt32(TextBoxAdvancePercent.Text)
    End If

    Dim TextBoxRetentionPercent As TextBox = _
          FormViewAddendumClientData.FindControl("TextBoxRetentionPercent")
    If String.IsNullOrEmpty(TextBoxRetentionPercent.Text) Then
      e.NewValues("RetentionPercent") = Nothing
    Else
      e.NewValues("RetentionPercent") = Convert.ToInt32(TextBoxRetentionPercent.Text)
    End If

    Dim TextBoxRetentionTerms As TextBox = _
      FormViewAddendumClientData.FindControl("TextBoxRetentionTerms")
    If String.IsNullOrEmpty(TextBoxRetentionTerms.Text) Then
      e.NewValues("RetentionTerms") = Nothing
    Else
      e.NewValues("RetentionTerms") = TextBoxRetentionTerms.Text
    End If

    Dim TextBoxRetentionTermsComments As TextBox = _
  FormViewAddendumClientData.FindControl("TextBoxRetentionTermsComments")
    If String.IsNullOrEmpty(TextBoxRetentionTermsComments.Text) Then
      e.NewValues("RetentionTermsAddComment") = Nothing
    Else
      e.NewValues("RetentionTermsAddComment") = TextBoxRetentionTermsComments.Text
    End If

    Dim TextBoxValidatedBOQmarginPercent As TextBox = _
          FormViewAddendumClientData.FindControl("TextBoxValidatedBOQmarginPercent")
    If String.IsNullOrEmpty(TextBoxValidatedBOQmarginPercent.Text) Then
      e.NewValues("ValidatedBOQmarginPercent") = Nothing
    Else
      e.NewValues("ValidatedBOQmarginPercent") = Convert.ToInt32(TextBoxValidatedBOQmarginPercent.Text)
    End If

    Dim TextBoxIVexpectedSubmissionDate As TextBox = _
      FormViewAddendumClientData.FindControl("TextBoxIVexpectedSubmissionDate")
    If String.IsNullOrEmpty(TextBoxIVexpectedSubmissionDate.Text) Then
      e.NewValues("IVexpectedSubmissionDate") = Nothing
    Else
      e.NewValues("IVexpectedSubmissionDate") = Convert.ToDateTime(TextBoxIVexpectedSubmissionDate.Text)
    End If

    Dim TextBoxIVexpectedApprovalDate As TextBox = _
        FormViewAddendumClientData.FindControl("TextBoxIVexpectedApprovalDate")
    If String.IsNullOrEmpty(TextBoxIVexpectedApprovalDate.Text) Then
      e.NewValues("IvexpectedApprovalDate") = Nothing
    Else
      e.NewValues("IvexpectedApprovalDate") = Convert.ToDateTime(TextBoxIVexpectedApprovalDate.Text)
    End If

    Dim TextBoxExpectedPaymentDate As TextBox = _
        FormViewAddendumClientData.FindControl("TextBoxExpectedPaymentDate")
    If String.IsNullOrEmpty(TextBoxExpectedPaymentDate.Text) Then
      e.NewValues("ExpectedPaymentDate") = Nothing
    Else
      e.NewValues("ExpectedPaymentDate") = Convert.ToDateTime(TextBoxExpectedPaymentDate.Text)
    End If

    Dim CheckBoxPenaltyClause As CheckBox = _
    FormViewAddendumClientData.FindControl("CheckBoxPenaltyClause")
    If Not CheckBoxPenaltyClause.Checked Then
      e.NewValues("PenaltyClause") = False
    Else
      e.NewValues("PenaltyClause") = True
    End If

    Dim TextBoxPenaltyTerms As TextBox = _
    FormViewAddendumClientData.FindControl("TextBoxPenaltyTerms")
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

  Protected Sub FormViewAddendumClientData_Load(sender As Object, e As System.EventArgs) Handles FormViewAddendumClientData.Load

    If Not IsPostBack Then
      ChangeModeAccordingly(sender)
    End If

  End Sub

  Protected Sub ChangeModeAccordingly(ByVal FormViewToManuplate As FormView)

    Dim LinkButtonEdit As LinkButton = FormViewToManuplate.FindControl("LinkButtonEdit")
    Dim LinkButtonDelete As LinkButton = FormViewToManuplate.FindControl("LinkButtonDelete")

    If Page.User.IsInRole("AddClientData") Then
      ' define which mode to OPEN
      If AddendumExist(Request.QueryString("AddendumID")) = True Then
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

  Protected Function AddendumExist(ByVal AddendumID_ As Integer) As Boolean
    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    con.Open()
    Dim sqlstring As String = " SELECT count([AddendumID]) FROM [Table_AddendumClientData] WHERE AddendumID = @AddendumID "
    Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text

        'syntax for parameter adding
        Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", System.Data.SqlDbType.Int)
        AddendumID.Value = AddendumID_
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
    FormViewAddendumClientData.ChangeMode(FormViewMode.Edit)
  End Sub
End Class
