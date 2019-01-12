Imports System.Data.SqlClient
Partial Class KPIs
  Inherits System.Web.UI.Page

  Protected Sub DropDownListProject_DataBound(sender As Object, e As System.EventArgs)
    Dim DropDownListProject As DropDownList = sender
    Dim lst2 As New ListItem("Select Project", "0")
    DropDownListProject.Items.Insert(0, lst2)
  End Sub

  Protected Sub FormViewKPIs_ItemDeleted(sender As Object, e As System.Web.UI.WebControls.FormViewDeletedEventArgs) Handles FormViewKPIs.ItemDeleted
    ChangeModeAccordingly(sender)
  End Sub

  Protected Sub FormViewKPIs_ItemInserted(sender As Object, e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FormViewKPIs.ItemInserted
    ChangeModeAccordingly(sender)
  End Sub

  Protected Sub FormViewKPIs_ItemInserting(sender As Object, e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormViewKPIs.ItemInserting
    ' Insert aLL required parameters here

    Dim DropDownListProject As DropDownList = FormViewKPIs.FindControl("DropDownListProject")
    If DropDownListProject.SelectedValue = 0 Then
      e.Cancel = True
      Exit Sub
    Else
      e.Values("ProjectID") = DropDownListProject.SelectedValue
    End If

    Dim TextBoxProjectManager As TextBox = FormViewKPIs.FindControl("TextBoxProjectManager")
    If String.IsNullOrEmpty(TextBoxProjectManager.Text) Then
      e.Values("ProjectManager") = Nothing
    Else
      e.Values("ProjectManager") = TextBoxProjectManager.Text
    End If

    Dim TextBoxStartDate As TextBox = FormViewKPIs.FindControl("TextBoxStartDate")
    If String.IsNullOrEmpty(TextBoxStartDate.Text) Then
      e.Values("ProjectStartDate") = Nothing
    Else
      e.Values("ProjectStartDate") = Convert.ToDateTime(TextBoxStartDate.Text)
    End If

    Dim TextBoxFinishDate As TextBox = FormViewKPIs.FindControl("TextBoxFinishDate")
    If String.IsNullOrEmpty(TextBoxFinishDate.Text) Then
      e.Values("ProjectFinishDate") = Nothing
    Else
      e.Values("ProjectFinishDate") = Convert.ToDateTime(TextBoxFinishDate.Text)
    End If

    Dim TextBoxCurrentProjectEndDate As TextBox = FormViewKPIs.FindControl("TextBoxCurrentProjectEndDate")
    If String.IsNullOrEmpty(TextBoxCurrentProjectEndDate.Text) Then
      e.Values("CurrentProjectEndDate") = Nothing
    Else
      e.Values("CurrentProjectEndDate") = Convert.ToDateTime(TextBoxCurrentProjectEndDate.Text)
    End If

    Dim TextBoxContractAmountWithVAT As TextBox = FormViewKPIs.FindControl("TextBoxContractAmountWithVAT")
    If String.IsNullOrEmpty(TextBoxContractAmountWithVAT.Text) Then
      e.Values("ContractAmountWithVAT") = Nothing
    Else
      e.Values("ContractAmountWithVAT") = Convert.ToDecimal(TextBoxContractAmountWithVAT.Text)
    End If

    Dim TextBoxAdvancePercent As TextBox = FormViewKPIs.FindControl("TextBoxAdvancePercent")
    If String.IsNullOrEmpty(TextBoxAdvancePercent.Text) Then
      e.Values("AdvancePercent") = Nothing
    Else
      e.Values("AdvancePercent") = Convert.ToDecimal(TextBoxAdvancePercent.Text)
    End If

    Dim TextBoxPaymentTerms As TextBox = FormViewKPIs.FindControl("TextBoxPaymentTerms")
    If String.IsNullOrEmpty(TextBoxPaymentTerms.Text) Then
      e.Values("PaymentTerms") = Nothing
    Else
      e.Values("PaymentTerms") = Convert.ToString(TextBoxPaymentTerms.Text)
    End If

    Dim TextBoxExchangeRate As TextBox = FormViewKPIs.FindControl("TextBoxExchangeRate")
    If String.IsNullOrEmpty(TextBoxExchangeRate.Text) Then
      e.Values("ExchangeRate") = Nothing
    Else
      e.Values("ExchangeRate") = Convert.ToDecimal(TextBoxExchangeRate.Text)
    End If

    Dim TextBoxRetentionPercent As TextBox = FormViewKPIs.FindControl("TextBoxRetentionPercent")
    If String.IsNullOrEmpty(TextBoxRetentionPercent.Text) Then
      e.Values("RetentionPercent") = Nothing
    Else
      e.Values("RetentionPercent") = Convert.ToDecimal(TextBoxRetentionPercent.Text)
    End If

    Dim TextBoxRetentionTerms As TextBox = FormViewKPIs.FindControl("TextBoxRetentionTerms")
    If String.IsNullOrEmpty(TextBoxRetentionTerms.Text) Then
      e.Values("RetentionTerms") = Nothing
    Else
      e.Values("RetentionTerms") = Convert.ToDecimal(TextBoxRetentionTerms.Text)
    End If

    Dim CheckBoxPenaltyClause As CheckBox = _
    FormViewKPIs.FindControl("CheckBoxPenaltyClause")
    If Not CheckBoxPenaltyClause.Checked Then
      e.Values("PenaltyClause") = False
    Else
      e.Values("PenaltyClause") = True
    End If

    Dim TextBoxPenaltyTerms As TextBox = _
FormViewKPIs.FindControl("TextBoxPenaltyTerms")
    If String.IsNullOrEmpty(TextBoxPenaltyTerms.Text) Then
      e.Values("PenaltyTerms") = Nothing
    Else
      e.Values("PenaltyTerms") = TextBoxPenaltyTerms.Text
    End If

    Dim TextBoxOriginalBOQmargin As TextBox = FormViewKPIs.FindControl("TextBoxOriginalBOQmargin")
    If String.IsNullOrEmpty(TextBoxOriginalBOQmargin.Text) Then
      e.Values("OriginalBOQMargin") = Nothing
    Else
      e.Values("OriginalBOQMargin") = Convert.ToDecimal(TextBoxOriginalBOQmargin.Text)
    End If

    Dim TextBoxValidatedBOQmargin As TextBox = FormViewKPIs.FindControl("TextBoxValidatedBOQmargin")
    If String.IsNullOrEmpty(TextBoxValidatedBOQmargin.Text) Then
      e.Values("ValidatedBOQMargin") = Nothing
    Else
      e.Values("ValidatedBOQMargin") = Convert.ToDecimal(TextBoxValidatedBOQmargin.Text)
    End If

    Dim TextBoxCurrentBOQmargin As TextBox = FormViewKPIs.FindControl("TextBoxCurrentBOQmargin")
    If String.IsNullOrEmpty(TextBoxCurrentBOQmargin.Text) Then
      e.Values("CurrentBOQMargin") = Nothing
    Else
      e.Values("CurrentBOQMargin") = Convert.ToDecimal(TextBoxCurrentBOQmargin.Text)
    End If

    Dim TextBoxCurrentIVwithoutVAT As TextBox = FormViewKPIs.FindControl("TextBoxCurrentIVwithoutVAT")
    If String.IsNullOrEmpty(TextBoxCurrentIVwithoutVAT.Text) Then
      e.Values("CurrentIVwithOutVAT") = Nothing
    Else
      e.Values("CurrentIVwithOutVAT") = Convert.ToDecimal(TextBoxCurrentIVwithoutVAT.Text)
    End If

    Dim TextBoxCurrentEVwithoutVAT As TextBox = FormViewKPIs.FindControl("TextBoxCurrentEVwithoutVAT")
    If String.IsNullOrEmpty(TextBoxCurrentEVwithoutVAT.Text) Then
      e.Values("CurrentEVwithOutVAT") = Nothing
    Else
      e.Values("CurrentEVwithOutVAT") = Convert.ToDecimal(TextBoxCurrentEVwithoutVAT.Text)
    End If

    Dim TextBoxCashInwithVAT As TextBox = FormViewKPIs.FindControl("TextBoxCashInwithVAT")
    If String.IsNullOrEmpty(TextBoxCashInwithVAT.Text) Then
      e.Values("CashIninclVat") = Nothing
    Else
      e.Values("CashIninclVat") = Convert.ToDecimal(TextBoxCashInwithVAT.Text)
    End If

    Dim TextBoxCashOutwithVAT As TextBox = FormViewKPIs.FindControl("TextBoxCashOutwithVAT")
    If String.IsNullOrEmpty(TextBoxCashOutwithVAT.Text) Then
      e.Values("CashOutinclVat") = Nothing
    Else
      e.Values("CashOutinclVat") = Convert.ToDecimal(TextBoxCashOutwithVAT.Text)
    End If

    ' THIS PART CAME LATER
    ' THIS PART CAME LATER
    ' THIS PART CAME LATER
    ' THIS PART CAME LATER
    ' THIS PART CAME LATER


    Dim TextBoxRetentionValue As TextBox = FormViewKPIs.FindControl("TextBoxRetentionValue")
    If String.IsNullOrEmpty(TextBoxRetentionValue.Text) Then
      e.Values("RetentionValuinclVAT") = Nothing
    Else
      e.Values("RetentionValuinclVAT") = Convert.ToDecimal(TextBoxRetentionValue.Text)
    End If

    Dim TextBoxRetentionDueDate As TextBox = FormViewKPIs.FindControl("TextBoxRetentionDueDate")
    If String.IsNullOrEmpty(TextBoxRetentionDueDate.Text) Then
      e.Values("RetentionDueDate") = Nothing
    Else
      e.Values("RetentionDueDate") = Convert.ToDateTime(TextBoxRetentionDueDate.Text)
    End If

    Dim TextBoxWarrantyPeriod As TextBox = FormViewKPIs.FindControl("TextBoxWarrantyPeriod")
    If String.IsNullOrEmpty(TextBoxWarrantyPeriod.Text) Then
      e.Values("WarrantyPeriod") = Nothing
    Else
      e.Values("WarrantyPeriod") = Convert.ToString(TextBoxWarrantyPeriod.Text)
    End If

    Dim TextBoxProjectCompletePercent As TextBox = FormViewKPIs.FindControl("TextBoxProjectCompletePercent")
    If String.IsNullOrEmpty(TextBoxProjectCompletePercent.Text) Then
      e.Values("ProjectCompletionPercent") = Nothing
    Else
      e.Values("ProjectCompletionPercent") = Convert.ToDecimal(TextBoxProjectCompletePercent.Text)
    End If

    Dim TextBoxCommentProjectName As TextBox = FormViewKPIs.FindControl("TextBoxCommentProjectName")
    If String.IsNullOrEmpty(TextBoxCommentProjectName.Text) Then
      e.Values("NoteProjectName") = Nothing
    Else
      e.Values("NoteProjectName") = Convert.ToString(TextBoxCommentProjectName.Text)
    End If

    Dim TextBoxCommentProjectManager As TextBox = FormViewKPIs.FindControl("TextBoxCommentProjectManager")
    If String.IsNullOrEmpty(TextBoxCommentProjectManager.Text) Then
      e.Values("NoteProjectManager") = Nothing
    Else
      e.Values("NoteProjectManager") = Convert.ToString(TextBoxCommentProjectManager.Text)
    End If

    Dim TextBoxCommentProjectStartDate As TextBox = FormViewKPIs.FindControl("TextBoxCommentProjectStartDate")
    If String.IsNullOrEmpty(TextBoxCommentProjectStartDate.Text) Then
      e.Values("NoteProjectStartDate") = Nothing
    Else
      e.Values("NoteProjectStartDate") = Convert.ToString(TextBoxCommentProjectStartDate.Text)
    End If

    Dim TextBoxCommentProjectFinishDate As TextBox = FormViewKPIs.FindControl("TextBoxCommentProjectFinishDate")
    If String.IsNullOrEmpty(TextBoxCommentProjectFinishDate.Text) Then
      e.Values("NoteProjectFinishDate") = Nothing
    Else
      e.Values("NoteProjectFinishDate") = Convert.ToString(TextBoxCommentProjectFinishDate.Text)
    End If

    Dim TextBoxCommentProjectCurrentEndDate As TextBox = FormViewKPIs.FindControl("TextBoxCommentProjectCurrentEndDate")
    If String.IsNullOrEmpty(TextBoxCommentProjectCurrentEndDate.Text) Then
      e.Values("NoteCurrentProjectEndDate") = Nothing
    Else
      e.Values("NoteCurrentProjectEndDate") = Convert.ToString(TextBoxCommentProjectCurrentEndDate.Text)
    End If

    Dim TextBoxCommentContractAmountIncVAT As TextBox = FormViewKPIs.FindControl("TextBoxCommentContractAmountIncVAT")
    If String.IsNullOrEmpty(TextBoxCommentContractAmountIncVAT.Text) Then
      e.Values("NoteContractAmountWithVAT") = Nothing
    Else
      e.Values("NoteContractAmountWithVAT") = Convert.ToString(TextBoxCommentContractAmountIncVAT.Text)
    End If

    Dim TextBoxCommentAdvancePercent As TextBox = FormViewKPIs.FindControl("TextBoxCommentAdvancePercent")
    If String.IsNullOrEmpty(TextBoxCommentAdvancePercent.Text) Then
      e.Values("NoteAdvancePercent") = Nothing
    Else
      e.Values("NoteAdvancePercent") = Convert.ToString(TextBoxCommentAdvancePercent.Text)
    End If

    Dim TextBoxCommentExchangeRate As TextBox = FormViewKPIs.FindControl("TextBoxCommentExchangeRate")
    If String.IsNullOrEmpty(TextBoxCommentExchangeRate.Text) Then
      e.Values("NoteExchangeRate") = Nothing
    Else
      e.Values("NoteExchangeRate") = Convert.ToString(TextBoxCommentExchangeRate.Text)
    End If

    Dim TextBoxCommentRetentionPercent As TextBox = FormViewKPIs.FindControl("TextBoxCommentRetentionPercent")
    If String.IsNullOrEmpty(TextBoxCommentRetentionPercent.Text) Then
      e.Values("NoteRetentionPercent") = Nothing
    Else
      e.Values("NoteRetentionPercent") = Convert.ToString(TextBoxCommentRetentionPercent.Text)
    End If

    Dim TextBoxCommentRetentionValueIncVAT As TextBox = FormViewKPIs.FindControl("TextBoxCommentRetentionValueIncVAT")
    If String.IsNullOrEmpty(TextBoxCommentRetentionValueIncVAT.Text) Then
      e.Values("NoteRetentionValueIncVAT") = Nothing
    Else
      e.Values("NoteRetentionValueIncVAT") = Convert.ToString(TextBoxCommentRetentionValueIncVAT.Text)
    End If

    Dim TextBoxCommentRetentionTerms As TextBox = FormViewKPIs.FindControl("TextBoxCommentRetentionTerms")
    If String.IsNullOrEmpty(TextBoxCommentRetentionTerms.Text) Then
      e.Values("NoteRetentionTerms") = Nothing
    Else
      e.Values("NoteRetentionTerms") = Convert.ToString(TextBoxCommentRetentionTerms.Text)
    End If

    Dim TextBoxCommentRetentionDueDate As TextBox = FormViewKPIs.FindControl("TextBoxCommentRetentionDueDate")
    If String.IsNullOrEmpty(TextBoxCommentRetentionDueDate.Text) Then
      e.Values("NoteRetentionDueDate") = Nothing
    Else
      e.Values("NoteRetentionDueDate") = Convert.ToString(TextBoxCommentRetentionDueDate.Text)
    End If

    Dim TextBoxCommentWarrantyPeriod As TextBox = FormViewKPIs.FindControl("TextBoxCommentWarrantyPeriod")
    If String.IsNullOrEmpty(TextBoxCommentWarrantyPeriod.Text) Then
      e.Values("NoteWarrantyPeriod") = Nothing
    Else
      e.Values("NoteWarrantyPeriod") = Convert.ToString(TextBoxCommentWarrantyPeriod.Text)
    End If

    Dim TextBoxCommentPenaltyClause As TextBox = FormViewKPIs.FindControl("TextBoxCommentPenaltyClause")
    If String.IsNullOrEmpty(TextBoxCommentPenaltyClause.Text) Then
      e.Values("NotePenaltyClause") = Nothing
    Else
      e.Values("NotePenaltyClause") = Convert.ToString(TextBoxCommentPenaltyClause.Text)
    End If

    Dim TextBoxCommentPenaltyTerms As TextBox = FormViewKPIs.FindControl("TextBoxCommentPenaltyTerms")
    If String.IsNullOrEmpty(TextBoxCommentPenaltyTerms.Text) Then
      e.Values("NotePenaltyTerms") = Nothing
    Else
      e.Values("NotePenaltyTerms") = Convert.ToString(TextBoxCommentPenaltyTerms.Text)
    End If

    Dim TextBoxCommentOriginalBOQMargin As TextBox = FormViewKPIs.FindControl("TextBoxCommentOriginalBOQMargin")
    If String.IsNullOrEmpty(TextBoxCommentOriginalBOQMargin.Text) Then
      e.Values("NoteOriginalBOQMargin") = Nothing
    Else
      e.Values("NoteOriginalBOQMargin") = Convert.ToString(TextBoxCommentOriginalBOQMargin.Text)
    End If

    Dim TextBoxCommentValidatedBOQmargin As TextBox = FormViewKPIs.FindControl("TextBoxCommentValidatedBOQmargin")
    If String.IsNullOrEmpty(TextBoxCommentValidatedBOQmargin.Text) Then
      e.Values("NoteValidatedBOQMargin") = Nothing
    Else
      e.Values("NoteValidatedBOQMargin") = Convert.ToString(TextBoxCommentValidatedBOQmargin.Text)
    End If

    Dim TextBoxCommentCurrentBOQmargin As TextBox = FormViewKPIs.FindControl("TextBoxCommentCurrentBOQmargin")
    If String.IsNullOrEmpty(TextBoxCommentCurrentBOQmargin.Text) Then
      e.Values("NoteCurrentBOQMargin") = Nothing
    Else
      e.Values("NoteCurrentBOQMargin") = Convert.ToString(TextBoxCommentCurrentBOQmargin.Text)
    End If

    Dim TextBoxCommentCurrentIVexcVAT As TextBox = FormViewKPIs.FindControl("TextBoxCommentCurrentIVexcVAT")
    If String.IsNullOrEmpty(TextBoxCommentCurrentIVexcVAT.Text) Then
      e.Values("NoteCurrentIVwithOutVAT") = Nothing
    Else
      e.Values("NoteCurrentIVwithOutVAT") = Convert.ToString(TextBoxCommentCurrentIVexcVAT.Text)
    End If

    Dim TextBoxCommentCurrentEVexcVAT As TextBox = FormViewKPIs.FindControl("TextBoxCommentCurrentEVexcVAT")
    If String.IsNullOrEmpty(TextBoxCommentCurrentEVexcVAT.Text) Then
      e.Values("NoteCurrentEVwithOutVAT") = Nothing
    Else
      e.Values("NoteCurrentEVwithOutVAT") = Convert.ToString(TextBoxCommentCurrentEVexcVAT.Text)
    End If

    Dim TextBoxCommentProjectCompletionPercent As TextBox = FormViewKPIs.FindControl("TextBoxCommentProjectCompletionPercent")
    If String.IsNullOrEmpty(TextBoxCommentProjectCompletionPercent.Text) Then
      e.Values("NoteProjectCompletionPercent") = Nothing
    Else
      e.Values("NoteProjectCompletionPercent") = Convert.ToString(TextBoxCommentProjectCompletionPercent.Text)
    End If

    Dim TextBoxCommentCashInExcVAT As TextBox = FormViewKPIs.FindControl("TextBoxCommentCashInExcVAT")
    If String.IsNullOrEmpty(TextBoxCommentCashInExcVAT.Text) Then
      e.Values("NoteCashIninclVat") = Nothing
    Else
      e.Values("NoteCashIninclVat") = Convert.ToString(TextBoxCommentCashInExcVAT.Text)
    End If

    Dim TextBoxCommentCashOutExcVAT As TextBox = FormViewKPIs.FindControl("TextBoxCommentCashOutExcVAT")
    If String.IsNullOrEmpty(TextBoxCommentCashOutExcVAT.Text) Then
      e.Values("NoteCashOutinclVat") = Nothing
    Else
      e.Values("NoteCashOutinclVat") = Convert.ToString(TextBoxCommentCashOutExcVAT.Text)
    End If
  End Sub

  Protected Sub FormViewKPIs_ItemUpdated(sender As Object, e As System.Web.UI.WebControls.FormViewUpdatedEventArgs) Handles FormViewKPIs.ItemUpdated
    ChangeModeAccordingly(sender)
  End Sub

  Protected Sub FormViewKPIs_ItemUpdating(sender As Object, e As System.Web.UI.WebControls.FormViewUpdateEventArgs) Handles FormViewKPIs.ItemUpdating
    ' Insert aLL required parameters here

    Dim DropDownListProject As DropDownList = FormViewKPIs.FindControl("DropDownListProject")
    If DropDownListProject.SelectedValue = 0 Then
      e.Cancel = True
      Exit Sub
    Else
      e.NewValues("ProjectID") = DropDownListProject.SelectedValue
    End If

    Dim TextBoxProjectManager As TextBox = FormViewKPIs.FindControl("TextBoxProjectManager")
    If String.IsNullOrEmpty(TextBoxProjectManager.Text) Then
      e.NewValues("ProjectManager") = Nothing
    Else
      e.NewValues("ProjectManager") = TextBoxProjectManager.Text
    End If

    Dim TextBoxStartDate As TextBox = FormViewKPIs.FindControl("TextBoxStartDate")
    If String.IsNullOrEmpty(TextBoxStartDate.Text) Then
      e.NewValues("ProjectStartDate") = Nothing
    Else
      e.NewValues("ProjectStartDate") = Convert.ToDateTime(TextBoxStartDate.Text)
    End If

    Dim TextBoxFinishDate As TextBox = FormViewKPIs.FindControl("TextBoxFinishDate")
    If String.IsNullOrEmpty(TextBoxFinishDate.Text) Then
      e.NewValues("ProjectFinishDate") = Nothing
    Else
      e.NewValues("ProjectFinishDate") = Convert.ToDateTime(TextBoxFinishDate.Text)
    End If

    Dim TextBoxCurrentProjectEndDate As TextBox = FormViewKPIs.FindControl("TextBoxCurrentProjectEndDate")
    If String.IsNullOrEmpty(TextBoxCurrentProjectEndDate.Text) Then
      e.NewValues("CurrentProjectEndDate") = Nothing
    Else
      e.NewValues("CurrentProjectEndDate") = Convert.ToDateTime(TextBoxCurrentProjectEndDate.Text)
    End If

    Dim TextBoxContractAmountWithVAT As TextBox = FormViewKPIs.FindControl("TextBoxContractAmountWithVAT")
    If String.IsNullOrEmpty(TextBoxContractAmountWithVAT.Text) Then
      e.NewValues("ContractAmountWithVAT") = Nothing
    Else
      e.NewValues("ContractAmountWithVAT") = Convert.ToDecimal(TextBoxContractAmountWithVAT.Text)
    End If

    Dim TextBoxAdvancePercent As TextBox = FormViewKPIs.FindControl("TextBoxAdvancePercent")
    If String.IsNullOrEmpty(TextBoxAdvancePercent.Text) Then
      e.NewValues("AdvancePercent") = Nothing
    Else
      e.NewValues("AdvancePercent") = Convert.ToDecimal(TextBoxAdvancePercent.Text)
    End If

    Dim TextBoxPaymentTerms As TextBox = FormViewKPIs.FindControl("TextBoxPaymentTerms")
    If String.IsNullOrEmpty(TextBoxPaymentTerms.Text) Then
      e.NewValues("PaymentTerms") = Nothing
    Else
      e.NewValues("PaymentTerms") = TextBoxPaymentTerms.Text
    End If

    Dim TextBoxExchangeRate As TextBox = FormViewKPIs.FindControl("TextBoxExchangeRate")
    If String.IsNullOrEmpty(TextBoxExchangeRate.Text) Then
      e.NewValues("ExchangeRate") = Nothing
    Else
      e.NewValues("ExchangeRate") = Convert.ToDecimal(TextBoxExchangeRate.Text)
    End If

    Dim TextBoxRetentionPercent As TextBox = FormViewKPIs.FindControl("TextBoxRetentionPercent")
    If String.IsNullOrEmpty(TextBoxRetentionPercent.Text) Then
      e.NewValues("RetentionPercent") = Nothing
    Else
      e.NewValues("RetentionPercent") = Convert.ToDecimal(TextBoxRetentionPercent.Text)
    End If

    Dim TextBoxRetentionTerms As TextBox = FormViewKPIs.FindControl("TextBoxRetentionTerms")
    If String.IsNullOrEmpty(TextBoxRetentionTerms.Text) Then
      e.NewValues("RetentionTerms") = Nothing
    Else
      e.NewValues("RetentionTerms") = TextBoxRetentionTerms.Text
    End If

    Dim CheckBoxPenaltyClause As CheckBox = _
    FormViewKPIs.FindControl("CheckBoxPenaltyClause")
    If Not CheckBoxPenaltyClause.Checked Then
      e.NewValues("PenaltyClause") = False
    Else
      e.NewValues("PenaltyClause") = True
    End If

    Dim TextBoxPenaltyTerms As TextBox = _
FormViewKPIs.FindControl("TextBoxPenaltyTerms")
    If String.IsNullOrEmpty(TextBoxPenaltyTerms.Text) Then
      e.NewValues("PenaltyTerms") = Nothing
    Else
      e.NewValues("PenaltyTerms") = TextBoxPenaltyTerms.Text
    End If

    Dim TextBoxOriginalBOQmargin As TextBox = FormViewKPIs.FindControl("TextBoxOriginalBOQmargin")
    If String.IsNullOrEmpty(TextBoxOriginalBOQmargin.Text) Then
      e.NewValues("OriginalBOQMargin") = Nothing
    Else
      e.NewValues("OriginalBOQMargin") = Convert.ToDecimal(TextBoxOriginalBOQmargin.Text)
    End If

    Dim TextBoxValidatedBOQmargin As TextBox = FormViewKPIs.FindControl("TextBoxValidatedBOQmargin")
    If String.IsNullOrEmpty(TextBoxValidatedBOQmargin.Text) Then
      e.NewValues("ValidatedBOQMargin") = Nothing
    Else
      e.NewValues("ValidatedBOQMargin") = Convert.ToDecimal(TextBoxValidatedBOQmargin.Text)
    End If

    Dim TextBoxCurrentBOQmargin As TextBox = FormViewKPIs.FindControl("TextBoxCurrentBOQmargin")
    If String.IsNullOrEmpty(TextBoxCurrentBOQmargin.Text) Then
      e.NewValues("CurrentBOQMargin") = Nothing
    Else
      e.NewValues("CurrentBOQMargin") = Convert.ToDecimal(TextBoxCurrentBOQmargin.Text)
    End If

    Dim TextBoxCurrentIVwithoutVAT As TextBox = FormViewKPIs.FindControl("TextBoxCurrentIVwithoutVAT")
    If String.IsNullOrEmpty(TextBoxCurrentIVwithoutVAT.Text) Then
      e.NewValues("CurrentIVwithOutVAT") = Nothing
    Else
      e.NewValues("CurrentIVwithOutVAT") = Convert.ToDecimal(TextBoxCurrentIVwithoutVAT.Text)
    End If

    Dim TextBoxCurrentEVwithoutVAT As TextBox = FormViewKPIs.FindControl("TextBoxCurrentEVwithoutVAT")
    If String.IsNullOrEmpty(TextBoxCurrentEVwithoutVAT.Text) Then
      e.NewValues("CurrentEVwithOutVAT") = Nothing
    Else
      e.NewValues("CurrentEVwithOutVAT") = Convert.ToDecimal(TextBoxCurrentEVwithoutVAT.Text)
    End If

    Dim TextBoxCashInwithVAT As TextBox = FormViewKPIs.FindControl("TextBoxCashInwithVAT")
    If String.IsNullOrEmpty(TextBoxCashInwithVAT.Text) Then
      e.NewValues("CashIninclVat") = Nothing
    Else
      e.NewValues("CashIninclVat") = Convert.ToDecimal(TextBoxCashInwithVAT.Text)
    End If

    Dim TextBoxCashOutwithVAT As TextBox = FormViewKPIs.FindControl("TextBoxCashOutwithVAT")
    If String.IsNullOrEmpty(TextBoxCashOutwithVAT.Text) Then
      e.NewValues("CashOutinclVat") = Nothing
    Else
      e.NewValues("CashOutinclVat") = Convert.ToDecimal(TextBoxCashOutwithVAT.Text)
    End If

    ' ALL OF THOSE CAME LATER
    ' ALL OF THOSE CAME LATER
    ' ALL OF THOSE CAME LATER
    ' ALL OF THOSE CAME LATER
    ' ALL OF THOSE CAME LATER
    ' ALL OF THOSE CAME LATER

    Dim TextBoxRetentionValue As TextBox = FormViewKPIs.FindControl("TextBoxRetentionValue")
    If String.IsNullOrEmpty(TextBoxRetentionValue.Text) Then
      e.NewValues("RetentionValuinclVAT") = Nothing
    Else
      e.NewValues("RetentionValuinclVAT") = Convert.ToDecimal(TextBoxRetentionValue.Text)
    End If

    Dim TextBoxRetentionDueDate As TextBox = FormViewKPIs.FindControl("TextBoxRetentionDueDate")
    If String.IsNullOrEmpty(TextBoxRetentionDueDate.Text) Then
      e.NewValues("RetentionDueDate") = Nothing
    Else
      e.NewValues("RetentionDueDate") = Convert.ToDateTime(TextBoxRetentionDueDate.Text)
    End If

    Dim TextBoxWarrantyPeriod As TextBox = FormViewKPIs.FindControl("TextBoxWarrantyPeriod")
    If String.IsNullOrEmpty(TextBoxWarrantyPeriod.Text) Then
      e.NewValues("WarrantyPeriod") = Nothing
    Else
      e.NewValues("WarrantyPeriod") = Convert.ToString(TextBoxWarrantyPeriod.Text)
    End If

    Dim TextBoxProjectCompletePercent As TextBox = FormViewKPIs.FindControl("TextBoxProjectCompletePercent")
    If String.IsNullOrEmpty(TextBoxProjectCompletePercent.Text) Then
      e.NewValues("ProjectCompletionPercent") = Nothing
    Else
      e.NewValues("ProjectCompletionPercent") = Convert.ToDecimal(TextBoxProjectCompletePercent.Text)
    End If

    Dim TextBoxCommentProjectName As TextBox = FormViewKPIs.FindControl("TextBoxCommentProjectName")
    If String.IsNullOrEmpty(TextBoxCommentProjectName.Text) Then
      e.NewValues("NoteProjectName") = Nothing
    Else
      e.NewValues("NoteProjectName") = Convert.ToString(TextBoxCommentProjectName.Text)
    End If

    Dim TextBoxCommentProjectManager As TextBox = FormViewKPIs.FindControl("TextBoxCommentProjectManager")
    If String.IsNullOrEmpty(TextBoxCommentProjectManager.Text) Then
      e.NewValues("NoteProjectManager") = Nothing
    Else
      e.NewValues("NoteProjectManager") = Convert.ToString(TextBoxCommentProjectManager.Text)
    End If

    Dim TextBoxCommentProjectStartDate As TextBox = FormViewKPIs.FindControl("TextBoxCommentProjectStartDate")
    If String.IsNullOrEmpty(TextBoxCommentProjectStartDate.Text) Then
      e.NewValues("NoteProjectStartDate") = Nothing
    Else
      e.NewValues("NoteProjectStartDate") = Convert.ToString(TextBoxCommentProjectStartDate.Text)
    End If

    Dim TextBoxCommentProjectFinishDate As TextBox = FormViewKPIs.FindControl("TextBoxCommentProjectFinishDate")
    If String.IsNullOrEmpty(TextBoxCommentProjectFinishDate.Text) Then
      e.NewValues("NoteProjectFinishDate") = Nothing
    Else
      e.NewValues("NoteProjectFinishDate") = Convert.ToString(TextBoxCommentProjectFinishDate.Text)
    End If

    Dim TextBoxCommentProjectCurrentEndDate As TextBox = FormViewKPIs.FindControl("TextBoxCommentProjectCurrentEndDate")
    If String.IsNullOrEmpty(TextBoxCommentProjectCurrentEndDate.Text) Then
      e.NewValues("NoteCurrentProjectEndDate") = Nothing
    Else
      e.NewValues("NoteCurrentProjectEndDate") = Convert.ToString(TextBoxCommentProjectCurrentEndDate.Text)
    End If

    Dim TextBoxCommentContractAmountIncVAT As TextBox = FormViewKPIs.FindControl("TextBoxCommentContractAmountIncVAT")
    If String.IsNullOrEmpty(TextBoxCommentContractAmountIncVAT.Text) Then
      e.NewValues("NoteContractAmountWithVAT") = Nothing
    Else
      e.NewValues("NoteContractAmountWithVAT") = Convert.ToString(TextBoxCommentContractAmountIncVAT.Text)
    End If

    Dim TextBoxCommentAdvancePercent As TextBox = FormViewKPIs.FindControl("TextBoxCommentAdvancePercent")
    If String.IsNullOrEmpty(TextBoxCommentAdvancePercent.Text) Then
      e.NewValues("NoteAdvancePercent") = Nothing
    Else
      e.NewValues("NoteAdvancePercent") = Convert.ToString(TextBoxCommentAdvancePercent.Text)
    End If

    Dim TextBoxCommentExchangeRate As TextBox = FormViewKPIs.FindControl("TextBoxCommentExchangeRate")
    If String.IsNullOrEmpty(TextBoxCommentExchangeRate.Text) Then
      e.NewValues("NoteExchangeRate") = Nothing
    Else
      e.NewValues("NoteExchangeRate") = Convert.ToString(TextBoxCommentExchangeRate.Text)
    End If

    Dim TextBoxCommentRetentionPercent As TextBox = FormViewKPIs.FindControl("TextBoxCommentRetentionPercent")
    If String.IsNullOrEmpty(TextBoxCommentRetentionPercent.Text) Then
      e.NewValues("NoteRetentionPercent") = Nothing
    Else
      e.NewValues("NoteRetentionPercent") = Convert.ToString(TextBoxCommentRetentionPercent.Text)
    End If

    Dim TextBoxCommentRetentionValueIncVAT As TextBox = FormViewKPIs.FindControl("TextBoxCommentRetentionValueIncVAT")
    If String.IsNullOrEmpty(TextBoxCommentRetentionValueIncVAT.Text) Then
      e.NewValues("NoteRetentionValueIncVAT") = Nothing
    Else
      e.NewValues("NoteRetentionValueIncVAT") = Convert.ToString(TextBoxCommentRetentionValueIncVAT.Text)
    End If

    Dim TextBoxCommentRetentionTerms As TextBox = FormViewKPIs.FindControl("TextBoxCommentRetentionTerms")
    If String.IsNullOrEmpty(TextBoxCommentRetentionTerms.Text) Then
      e.NewValues("NoteRetentionTerms") = Nothing
    Else
      e.NewValues("NoteRetentionTerms") = Convert.ToString(TextBoxCommentRetentionTerms.Text)
    End If

    Dim TextBoxCommentRetentionDueDate As TextBox = FormViewKPIs.FindControl("TextBoxCommentRetentionDueDate")
    If String.IsNullOrEmpty(TextBoxCommentRetentionDueDate.Text) Then
      e.NewValues("NoteRetentionDueDate") = Nothing
    Else
      e.NewValues("NoteRetentionDueDate") = Convert.ToString(TextBoxCommentRetentionDueDate.Text)
    End If

    Dim TextBoxCommentWarrantyPeriod As TextBox = FormViewKPIs.FindControl("TextBoxCommentWarrantyPeriod")
    If String.IsNullOrEmpty(TextBoxCommentWarrantyPeriod.Text) Then
      e.NewValues("NoteWarrantyPeriod") = Nothing
    Else
      e.NewValues("NoteWarrantyPeriod") = Convert.ToString(TextBoxCommentWarrantyPeriod.Text)
    End If

    Dim TextBoxCommentPenaltyClause As TextBox = FormViewKPIs.FindControl("TextBoxCommentPenaltyClause")
    If String.IsNullOrEmpty(TextBoxCommentPenaltyClause.Text) Then
      e.NewValues("NotePenaltyClause") = Nothing
    Else
      e.NewValues("NotePenaltyClause") = Convert.ToString(TextBoxCommentPenaltyClause.Text)
    End If

    Dim TextBoxCommentPenaltyTerms As TextBox = FormViewKPIs.FindControl("TextBoxCommentPenaltyTerms")
    If String.IsNullOrEmpty(TextBoxCommentPenaltyTerms.Text) Then
      e.NewValues("NotePenaltyTerms") = Nothing
    Else
      e.NewValues("NotePenaltyTerms") = Convert.ToString(TextBoxCommentPenaltyTerms.Text)
    End If

    Dim TextBoxCommentOriginalBOQMargin As TextBox = FormViewKPIs.FindControl("TextBoxCommentOriginalBOQMargin")
    If String.IsNullOrEmpty(TextBoxCommentOriginalBOQMargin.Text) Then
      e.NewValues("NoteOriginalBOQMargin") = Nothing
    Else
      e.NewValues("NoteOriginalBOQMargin") = Convert.ToString(TextBoxCommentOriginalBOQMargin.Text)
    End If

    Dim TextBoxCommentValidatedBOQmargin As TextBox = FormViewKPIs.FindControl("TextBoxCommentValidatedBOQmargin")
    If String.IsNullOrEmpty(TextBoxCommentValidatedBOQmargin.Text) Then
      e.NewValues("NoteValidatedBOQMargin") = Nothing
    Else
      e.NewValues("NoteValidatedBOQMargin") = Convert.ToString(TextBoxCommentValidatedBOQmargin.Text)
    End If

    Dim TextBoxCommentCurrentBOQmargin As TextBox = FormViewKPIs.FindControl("TextBoxCommentCurrentBOQmargin")
    If String.IsNullOrEmpty(TextBoxCommentCurrentBOQmargin.Text) Then
      e.NewValues("NoteCurrentBOQMargin") = Nothing
    Else
      e.NewValues("NoteCurrentBOQMargin") = Convert.ToString(TextBoxCommentCurrentBOQmargin.Text)
    End If

    Dim TextBoxCommentCurrentIVexcVAT As TextBox = FormViewKPIs.FindControl("TextBoxCommentCurrentIVexcVAT")
    If String.IsNullOrEmpty(TextBoxCommentCurrentIVexcVAT.Text) Then
      e.NewValues("NoteCurrentIVwithOutVAT") = Nothing
    Else
      e.NewValues("NoteCurrentIVwithOutVAT") = Convert.ToString(TextBoxCommentCurrentIVexcVAT.Text)
    End If

    Dim TextBoxCommentCurrentEVexcVAT As TextBox = FormViewKPIs.FindControl("TextBoxCommentCurrentEVexcVAT")
    If String.IsNullOrEmpty(TextBoxCommentCurrentEVexcVAT.Text) Then
      e.NewValues("NoteCurrentEVwithOutVAT") = Nothing
    Else
      e.NewValues("NoteCurrentEVwithOutVAT") = Convert.ToString(TextBoxCommentCurrentEVexcVAT.Text)
    End If

    Dim TextBoxCommentProjectCompletionPercent As TextBox = FormViewKPIs.FindControl("TextBoxCommentProjectCompletionPercent")
    If String.IsNullOrEmpty(TextBoxCommentProjectCompletionPercent.Text) Then
      e.NewValues("NoteProjectCompletionPercent") = Nothing
    Else
      e.NewValues("NoteProjectCompletionPercent") = Convert.ToString(TextBoxCommentProjectCompletionPercent.Text)
    End If

    Dim TextBoxCommentCashInExcVAT As TextBox = FormViewKPIs.FindControl("TextBoxCommentCashInExcVAT")
    If String.IsNullOrEmpty(TextBoxCommentCashInExcVAT.Text) Then
      e.NewValues("NoteCashIninclVat") = Nothing
    Else
      e.NewValues("NoteCashIninclVat") = Convert.ToString(TextBoxCommentCashInExcVAT.Text)
    End If

    Dim TextBoxCommentCashOutExcVAT As TextBox = FormViewKPIs.FindControl("TextBoxCommentCashOutExcVAT")
    If String.IsNullOrEmpty(TextBoxCommentCashOutExcVAT.Text) Then
      e.NewValues("NoteCashOutinclVat") = Nothing
    Else
      e.NewValues("NoteCashOutinclVat") = Convert.ToString(TextBoxCommentCashOutExcVAT.Text)
    End If


  End Sub



  Protected Sub ChangeModeAccordingly(ByVal FormViewToManuplate As FormView)

    Dim LinkButtonEdit As LinkButton = FormViewToManuplate.FindControl("LinkButtonEdit")
    Dim LinkButtonDelete As LinkButton = FormViewToManuplate.FindControl("LinkButtonDelete")

    If Page.User.IsInRole("AddClientData") Then
      ' define which mode to OPEN
      If ProjectKPIExist(Request.QueryString("ProjectID")) = True Then
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

  Protected Sub FormViewKPIs_Load(sender As Object, e As System.EventArgs) Handles FormViewKPIs.Load
    If Not IsPostBack Then
      ChangeModeAccordingly(sender)
    End If
  End Sub

  Protected Function GetProjectCurrency(ByVal ProjectID_ As Integer) As String
    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    con.Open()
    Dim sqlstring As String = " SELECT rtrim(ContractCurrency) as ContractCurrency FROM [Table1_Project] WHERE ProjectID = @ProjectID "
    Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text

        'syntax for parameter adding
        Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
        ProjectID.Value = ProjectID_
        Dim dr As SqlDataReader = cmd.ExecuteReader
        Dim returnValue As String = ""
        While dr.Read
            returnValue = dr(0).ToString
        End While
        Return returnValue
        con.Close()
        dr.Close()
    End Function


    Protected Function ProjectKPIExist(ByVal ProjectID_ As Integer) As Boolean
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " SELECT count([ProjectID]) FROM [Table_KPIs] WHERE ProjectID = @ProjectID "
        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text

        'syntax for parameter adding
        Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
        ProjectID.Value = ProjectID_
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
    FormViewKPIs.ChangeMode(FormViewMode.Edit)
  End Sub

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    If Not IsPostBack Then
      SqlDataSourceKPIs.UpdateParameters("RetentionValuinclVAT").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("RetentionDueDate").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("WarrantyPeriod").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("ProjectCompletionPercent").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteProjectName").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteProjectManager").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteProjectStartDate").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteProjectFinishDate").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteCurrentProjectEndDate").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteContractAmountWithVAT").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteAdvancePercent").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NotePaymentTerms").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteExchangeRate").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteRetentionPercent").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteRetentionValueIncVAT").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteRetentionTerms").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteRetentionDueDate").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteWarrantyPeriod").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NotePenaltyClause").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NotePenaltyTerms").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteOriginalBOQMargin").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteValidatedBOQMargin").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteCurrentBOQMargin").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteCurrentIVwithOutVAT").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteCurrentEVwithOutVAT").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteProjectCompletionPercent").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteCashIninclVat").DefaultValue = Nothing
      SqlDataSourceKPIs.UpdateParameters("NoteCashOutinclVat").DefaultValue = Nothing

      SqlDataSourceKPIs.InsertParameters("RetentionValuinclVAT").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("RetentionDueDate").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("WarrantyPeriod").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("ProjectCompletionPercent").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteProjectName").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteProjectManager").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteProjectStartDate").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteProjectFinishDate").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteCurrentProjectEndDate").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteContractAmountWithVAT").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteAdvancePercent").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NotePaymentTerms").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteExchangeRate").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteRetentionPercent").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteRetentionValueIncVAT").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteRetentionTerms").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteRetentionDueDate").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteWarrantyPeriod").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NotePenaltyClause").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NotePenaltyTerms").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteOriginalBOQMargin").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteValidatedBOQMargin").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteCurrentBOQMargin").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteCurrentIVwithOutVAT").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteCurrentEVwithOutVAT").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteProjectCompletionPercent").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteCashIninclVat").DefaultValue = Nothing
      SqlDataSourceKPIs.InsertParameters("NoteCashOutinclVat").DefaultValue = Nothing

    End If
  End Sub

  Protected Sub FormViewKPIs_PreRender(sender As Object, e As System.EventArgs) Handles FormViewKPIs.PreRender

    If IsPostBack OrElse Not IsPostBack Then
      Dim LabelContractCurrency As Literal = FormViewKPIs.FindControl("LabelContractCurrency")
      If LabelContractCurrency IsNot Nothing Then
        LabelContractCurrency.Text = GetProjectCurrency(Request.QueryString("ProjectID"))
      End If
    End If

  End Sub

  Protected Sub DataListKPIs_add_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataListKPIs_add.ItemDataBound
    If e.Item.ItemType = ListItemType.Header Then
      Dim DataListKPIs_add As DataList = sender
      Dim HyperLinkNewAdd As HyperLink = DirectCast(e.Item.FindControl("HyperLinkNewAdd"), HyperLink)
      If HyperLinkNewAdd IsNot Nothing Then
                HyperLinkNewAdd.NavigateUrl = "~/webforms/KPIs_add.aspx?ProjectID=" + Request.QueryString("ProjectID") + "&AddNo=0"
      End If
    End If
  End Sub
End Class
