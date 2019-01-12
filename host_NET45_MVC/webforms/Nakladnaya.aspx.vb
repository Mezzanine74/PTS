Imports System.Data.SqlClient
Imports System.IO
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.Garbage

Partial Class NakladnayaCopy2
    Inherits System.Web.UI.Page

    Dim Notification As New _GiveNotification

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init


    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ' it provides query parameter for DropDownListSupplier
        If IsPostBack Or Not IsPostBack Then
            TextBoxUserName.Text = Page.User.Identity.Name
        End If
    End Sub

    Protected Function GetTextWarning() As String

        Return "You cannot ADD more than " + LabelMaxDocValue.Text + " Otherwise it exceeds Total Po Value in Ruble With VAT. " + _
        "<br/>" + _
        "If you need to enter this amount, you need to increase PO value on <a href=" + """" + "/webforms/editpo.aspx" + """" + " target=" + """" + "_blank" + """" + ">Edit Purchase Order</a> page."


    End Function

    Protected Sub DropDownListPrj_DataBound(sender As Object, e As System.EventArgs) Handles DropDownListPrj.DataBound
        If Not IsPostBack Then
            Dim lst1 As New ListItem("Select Project", "0")
            Me.DropDownListPrj.Items.Insert(0, lst1)
        End If

        If Request.QueryString("ProjectID") IsNot Nothing And Not IsPostBack Then
            DropDownListPrj.SelectedValue = Request.QueryString("ProjectID")
        End If

    End Sub

    Protected Sub DropDownListSupplier_DataBound(sender As Object, e As System.EventArgs) Handles DropDownListSupplier.DataBound
        Dim lst As New ListItem("ALL SUPPLIER", "")
        DropDownListSupplier.Items.Insert(0, lst)

        If Request.QueryString("SupplierID") IsNot Nothing And Not IsPostBack Then
            DropDownListSupplier.SelectedValue = Request.QueryString("SupplierID")
        End If

    End Sub

    Protected Sub GridViewNakladnaya_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewNakladnaya.RowCommand

        ' hide LabelWarningNaklSf if it is activated from serverside
        LabelWarningNaklSf.Visible = False
        LabelWarningAktSf.Visible = False
        LabelWarningSfNakl_Akt_Together.Visible = False
        LabelWarningNakl_Akt_Together_NotPossible.Visible = False
        LabelNaklErrorWarning.Visible = False
        LabelAktErrorWarning.Visible = False

        TextBoxShowHistoryNakladnaya.Visible = False
        TextBoxShowHistoryAkt.Visible = False
        TextBoxShowHistoryAktToNakladnaya.Visible = False

        If (e.CommandName = "Nakl") Then
            Dim PO_No As String = e.CommandArgument.ToString
            labelPOnoNakl.Text = PO_No.ToString
            'ModalPopupExtenderNakladnaya.Show()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnaya" + "').modal({}) });", True)

        End If

        If (e.CommandName = "NaklTotalValue") Then
            Dim PO_No As String = e.CommandArgument.ToString
            labelPOnoNakl.Text = PO_No.ToString
            'ModalPopupExtenderNakladnayaEdit.Show()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnayaEdit" + "').modal({}) });", True)

        End If

        If (e.CommandName = "Akt") Then
            Dim PO_No As String = e.CommandArgument.ToString
            labelPOnoAkt.Text = PO_No.ToString
            'ModalPopupExtenderAkt.Show()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAkt" + "').modal({}) });", True)
        End If

        If (e.CommandName = "AktTotalValue") Then
            Dim PO_No As String = e.CommandArgument.ToString
            labelPOnoAkt.Text = PO_No.ToString
            'ModalPopupExtenderAktEdit.Show()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAktEdit" + "').modal({}) });", True)
        End If

        If (e.CommandName = "Nakl_Akt") Then
            Dim PO_No As String = e.CommandArgument.ToString
            labelPOnoNakl_Akt_Together.Text = PO_No.ToString
            labelPOnoNakl.Text = PO_No.ToString
            ' bind DDLnakladnaya
            'ModalPopupExtenderNakl_Akt_Together.Show()

            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakl_Akt_Together" + "').modal({}) });", True)

        End If

        If (e.CommandName = "Nakl_AktTotalValue") Then
            Dim PO_No As String = e.CommandArgument.ToString
            labelPOnoNakl_Akt_Together.Text = PO_No.ToString
            labelPOnoNakl.Text = PO_No.ToString
            ' bind DDLnakladnaya
            'ModalPopupExtenderNakladnayaEdit.Show()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnayaEdit" + "').modal({}) });", True)
        End If

        If (e.CommandName = "closedocument") Then

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewNakladnaya.Rows(index)
            Dim TextBoxCloseDoc As TextBox = DirectCast(row.FindControl("TextBoxCloseDoc"), TextBox)

            Dim PO_No As String = row.Cells(0).Text.Trim
            Dim ClosingValue As Decimal = Convert.ToDecimal(TextBoxCloseDoc.Text)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim nakladnaya As New PTS_MERCURY.db.Table_PO_Nakladnaya

                nakladnaya.PO_No = PO_No
                nakladnaya.Inv_ContrNo = "PTS"
                nakladnaya.SF = True

                If Request.QueryString("DocNo") IsNot Nothing Then
                    nakladnaya.NakNo = Request.QueryString("DocNo").ToString
                Else
                    nakladnaya.NakNo = "PTS"
                End If

                If Request.QueryString("DocDate") IsNot Nothing Then
                    nakladnaya.Nak_Date = Request.QueryString("DocDate").ToString
                Else
                    nakladnaya.Nak_Date = Date.Today
                End If

                nakladnaya.Nak_Rub_WithVAT = ClosingValue
                nakladnaya.Comment = "this is entered by admin for 1S reconsoliation "
                nakladnaya.CreatedBy = DateTime.Now
                nakladnaya.PersonCreated = Page.User.Identity.Name.ToLower

                If ClosingValue > (GetPoTotalInRubleWithVAT(PO_No) * Flexibility(PO_No) - GetPoTotalCollectedDocRubleWithVATByPO(PO_No)) Then
                    ' give notification
                    LabelMaxDocValue.Text = String.Format("{0:#,##0.00}", _
                         (GetPoTotalInRubleWithVAT(PO_No) * Flexibility(PO_No) - GetPoTotalCollectedDocRubleWithVATByPO(PO_No))) + _
                     " Rub With VAT"
                    _GiveNotification.Gritter_Error(Me, "Your transaction is not executed! ", GetTextWarning(), "center")

                Else

                    db.Table_PO_Nakladnaya.Add(nakladnaya)
                    db.SaveChanges()

                    If Request.QueryString("_1Sid") IsNot Nothing Then

                        Dim matching_indexes1S As New PTS_MERCURY.db.Table_Delivery_MatchingIndexes
                        Dim PTS_Or_1S As String = "1S"
                        Dim Index_PTS_Or_1S As String = Request.QueryString("_1Sid").ToString
                        matching_indexes1S.PTS_Or_1S = PTS_Or_1S
                        matching_indexes1S.Index_PTS_Or_1S = Index_PTS_Or_1S

                        db.Table_Delivery_MatchingIndexes.Add(matching_indexes1S)
                        db.SaveChanges()

                        Dim matching_indexesPTS As New PTS_MERCURY.db.Table_Delivery_MatchingIndexes
                        PTS_Or_1S = "PTS"
                        Index_PTS_Or_1S = "Nkl_" + nakladnaya.ID_Nak.ToString.Trim
                        matching_indexesPTS.PTS_Or_1S = PTS_Or_1S
                        matching_indexesPTS.Index_PTS_Or_1S = Index_PTS_Or_1S

                        db.Table_Delivery_MatchingIndexes.Add(matching_indexesPTS)

                        db.SaveChanges()
                    End If

                End If

                db.Dispose()

            End Using

            GridViewNakladnaya.DataBind()

        End If


    End Sub

    Protected Sub GridViewNakladnaya_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewNakladnaya.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim p As String = DataBinder.Eval(e.Row.DataItem, "PO_No").ToString
            DirectCast(e.Row.FindControl("LiteralBalance"), Literal).Text = _
                "Balance : " + String.Format("{0: #,#.00}", GetPoTotalInRubleWithVAT(p) * Flexibility(p) - GetPoTotalCollectedDocRubleWithVATByPO(p))

            If Roles.IsUserInRole("DeliveryComparison") Then
                DirectCast(e.Row.FindControl("TextBoxCloseDoc"), TextBox).Text = _
                    GetPoTotalInRubleWithVAT(p) * Flexibility(p) - GetPoTotalCollectedDocRubleWithVATByPO(p)
                If (GetPoTotalInRubleWithVAT(p) * Flexibility(p) - GetPoTotalCollectedDocRubleWithVATByPO(p)) <> 0 Then
                    DirectCast(e.Row.FindControl("TextBoxCloseDoc"), TextBox).Visible = True
                Else
                    DirectCast(e.Row.FindControl("TextBoxCloseDoc"), TextBox).Visible = False
                End If
            Else
                DirectCast(e.Row.FindControl("TextBoxCloseDoc"), TextBox).Visible = False
            End If

            Dim LinkButtonClose As LinkButton = DirectCast(e.Row.FindControl("LinkButtonClose"), LinkButton)

            If Roles.IsUserInRole("DeliveryComparison") Then
                If (GetPoTotalInRubleWithVAT(p) * Flexibility(p) - GetPoTotalCollectedDocRubleWithVATByPO(p)) <> 0 Then
                    LinkButtonClose.Visible = True
                Else
                    LinkButtonClose.Visible = False
                End If
            Else
                LinkButtonClose.Visible = False
            End If

            'Provide value to parameter for PO details
            Dim SqlDataSourcePOdetail As SqlDataSource = _
              DirectCast(e.Row.FindControl("SqlDataSourcePOdetail"), SqlDataSource)
            SqlDataSourcePOdetail.SelectParameters("PO_No").DefaultValue = _
              DataBinder.Eval(e.Row.DataItem, "PO_No").ToString

            ' Check existitnce of nakladnaya and Akt. _Show and Hide Nakl and Akt buttons depending on the situation.
            Dim LinkButtonNakladnaya As LinkButton = DirectCast(e.Row.FindControl("LinkButtonNakladnaya"), LinkButton)
            Dim LinkButtonAkt As LinkButton = DirectCast(e.Row.FindControl("LinkButtonAkt"), LinkButton)
            Dim LinkButton_Nakl_Akt As LinkButton = DirectCast(e.Row.FindControl("LinkButton_Nakl_Akt"), LinkButton)

            If NumberOf_FREEnakl(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString) > 0 AndAlso Not AktExist(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString) Then
                ' This logic cancelled after meeting with Finance at 25/02/2014
                'LinkButtonAkt.Visible = False
            End If

            If NumberOf_FREEnakl(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString) = 0 AndAlso AktExist(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString) Then
                ' This logic cancelled after meeting with Finance at 25/02/2014
                'LinkButtonNakladnaya.Visible = False
            End If

            ' dont show total collected values if it is zero
            Dim LinkButtonNakladnayaTotalValue As LinkButton = DirectCast(e.Row.FindControl("LinkButtonNakladnayaTotalValue"), LinkButton)
            Dim LinkButtonAktTotalValue As LinkButton = DirectCast(e.Row.FindControl("LinkButtonAktTotalValue"), LinkButton)
            Dim LinkButton_Nakl_AktTotalValue As LinkButton = DirectCast(e.Row.FindControl("LinkButton_Nakl_AktTotalValue"), LinkButton)

            If Len(LinkButtonNakladnayaTotalValue.Text) = 0 Then
                LinkButtonNakladnayaTotalValue.Visible = False
            ElseIf Convert.ToDecimal(LinkButtonNakladnayaTotalValue.Text) = 0 Then
                LinkButtonNakladnayaTotalValue.Visible = False
            End If

            If Len(LinkButtonAktTotalValue.Text) = 0 Then
                LinkButtonAktTotalValue.Visible = False
            ElseIf Convert.ToDecimal(LinkButtonAktTotalValue.Text) = 0 Then
                LinkButtonAktTotalValue.Visible = False
            End If

            If Len(LinkButton_Nakl_AktTotalValue.Text) = 0 Then
                LinkButton_Nakl_AktTotalValue.Visible = False
            ElseIf Convert.ToDecimal(LinkButton_Nakl_AktTotalValue.Text) = 0 Then
                LinkButton_Nakl_AktTotalValue.Visible = False
            End If

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If DataBinder.Eval(e.Row.DataItem, "Status") = 0 Then
                ' do nothing
            ElseIf DataBinder.Eval(e.Row.DataItem, "Status") = 1 Then
                e.Row.BackColor = System.Drawing.Color.Pink
            ElseIf DataBinder.Eval(e.Row.DataItem, "Status") = 2 Then
                e.Row.BackColor = System.Drawing.Color.PaleGreen
            End If
        End If

        ' if supplier name is MERCURY, dont show to users unless he is savas
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DropDownListSupplier.SelectedValue = "7717148677" Then
                If Not Page.User.Identity.Name.ToLower.ToString = "savas" Then
                    Dim LinkButtonNakladnaya As LinkButton = DirectCast(e.Row.FindControl("LinkButtonNakladnaya"), LinkButton)
                    Dim LinkButtonAkt As LinkButton = DirectCast(e.Row.FindControl("LinkButtonAkt"), LinkButton)
                    Dim LinkButton_Nakl_Akt As LinkButton = DirectCast(e.Row.FindControl("LinkButton_Nakl_Akt"), LinkButton)


                    Dim LinkButtonNakladnayaTotalValue As LinkButton = DirectCast(e.Row.FindControl("LinkButtonNakladnayaTotalValue"), LinkButton)
                    Dim LinkButtonAktTotalValue As LinkButton = DirectCast(e.Row.FindControl("LinkButtonAktTotalValue"), LinkButton)
                    Dim LinkButton_Nakl_AktTotalValue As LinkButton = DirectCast(e.Row.FindControl("LinkButton_Nakl_AktTotalValue"), LinkButton)

                    LinkButtonNakladnaya.Enabled = False
                    LinkButtonAkt.Enabled = False
                    LinkButton_Nakl_Akt.Enabled = False

                    LinkButtonNakladnayaTotalValue.Enabled = False
                    LinkButtonAktTotalValue.Enabled = False
                    LinkButton_Nakl_AktTotalValue.Enabled = False
                End If
            End If
        End If

    End Sub

    Protected Sub ButtonInsertNakl_Click(sender As Object, e As System.EventArgs) Handles ButtonInsertNakl.Click

        ' Validation 1: Check PO for VAT
        If VATincluded(labelPOnoNakl.Text) Then
            If CheckBoxNakladnayaSF.Checked = False Then
                LabelWarningNaklSf.Visible = True
                'ModalPopupExtenderNakladnaya.Show()
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnaya" + "').modal({}) });", True)
                Exit Sub
            End If
        End If

        InsertNakladnaya()

    End Sub

    Protected Sub ButtonInsertAkt_Click(sender As Object, e As System.EventArgs) Handles ButtonInsertAkt.Click


        ' Validation 1: Check PO for VAT
        If VATincluded(labelPOnoAkt.Text) Then
            If CheckBoxAktSF.Checked = False Then
                LabelWarningAktSf.Visible = True
                'ModalPopupExtenderAkt.Show()
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAkt" + "').modal({}) });", True)
                Exit Sub
            End If
        End If

        InsertAkt()

    End Sub

    Protected Sub ButtonInsertNakl_Akt_Together_Click(sender As Object, e As System.EventArgs) Handles ButtonInsertNakl_Akt_Together.Click

        ' Validation 1: Check PO for VAT
        If VATincluded(labelPOnoNakl.Text) Then
            If CheckBoxSFNakl_Akt_Together.Checked = False Then
                LabelWarningSfNakl_Akt_Together.Visible = True
                'ModalPopupExtenderNakl_Akt_Together.Show()
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakl_Akt_Together" + "').modal({}) });", True)
                Exit Sub
            End If
        End If

        InsertNakl_Akt_Together()

    End Sub

    Protected Sub ButtonEditNakl_Click(sender As Object, e As System.EventArgs) Handles ButtonEditNakl.Click

        GridViewNaklEdit.DataBind()
        'ModalPopupExtenderNakladnayaEdit.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnayaEdit" + "').modal({}) });", True)
        'ModalPopupExtenderNakladnaya.Hide()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnaya" + "').modal('hide') });", True)

    End Sub

    Protected Sub ButtonEditAkt_Click(sender As Object, e As System.EventArgs) Handles ButtonEditAkt.Click

        GridViewAktEdit.DataBind()
        'ModalPopupExtenderAktEdit.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAktEdit" + "').modal({}) });", True)
        'ModalPopupExtenderAkt.Hide()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAkt" + "').modal('hide') });", True)

    End Sub

    Protected Sub ButtonEditAktToNakladnaya_Click(sender As Object, e As System.EventArgs) Handles ButtonEditAktToNakladnaya.Click

        GridViewAktToNakladnayaEdit.DataBind()
        'ModalPopupExtenderAktToNaklEdit.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAktToNakladnayaEdit" + "').modal({}) });", True)
        'ModalPopupExtenderNakladnayaEdit.Hide()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnayaEdit" + "').modal('hide') });", True)
        TextBoxShowHistoryAktToNakladnaya.Visible = False

    End Sub

    Protected Sub ButtonEditNakl_Akt_Together_Click(sender As Object, e As System.EventArgs) Handles ButtonEditNakl_Akt_Together.Click

        ButtonEditNakl_Click(Nothing, Nothing)

    End Sub

    Protected Sub ButtonSwitchToInsertNakl_Click(sender As Object, e As System.EventArgs) Handles ButtonSwitchToInsertNakl.Click

        'ModalPopupExtenderNakladnaya.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnaya" + "').modal({}) });", True)

        'ModalPopupExtenderNakladnayaEdit.Hide()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnayaEdit" + "').modal('hide') });", True)

    End Sub

    Protected Sub ButtonSwitchToNakladnayaEdit_Click(sender As Object, e As System.EventArgs) Handles ButtonSwitchToNakladnayaEdit.Click

        'ModalPopupExtenderNakladnayaEdit.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnayaEdit" + "').modal({}) });", True)
        GridViewNaklEdit.DataBind()
        'ModalPopupExtenderAktToNaklEdit.Hide()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAktToNakladnayaEdit" + "').modal('hide') });", True)
        TextBoxShowHistoryAktToNakladnaya.Visible = False

    End Sub

    Protected Sub ButtonSwitchToInsertNakladnaya__Click(sender As Object, e As System.EventArgs) Handles ButtonSwitchToInsertNakladnaya_.Click

        'ModalPopupExtenderNakladnaya.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnaya" + "').modal({}) });", True)
        'ModalPopupExtenderAktToNaklEdit.Hide()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAktToNakladnayaEdit" + "').modal('hide') });", True)

    End Sub

    Protected Sub ButtonSwitchToActInsert__Click(sender As Object, e As System.EventArgs) Handles ButtonSwitchToActInsert.Click

        'ModalPopupExtenderAkt.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAkt" + "').modal({}) });", True)
        'ModalPopupExtenderAktEdit.Hide()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAktEdit" + "').modal('hide') });", True)

    End Sub

    Protected Sub InsertNakladnaya()

        ' Check if entered Delivery doc value exceeds Total Po Value With VAT
        Dim _POno As String = labelPOnoNakl.Text
        Dim _POtotalRubleWithVAT As Decimal = GetPoTotalInRubleWithVAT(_POno)
        Dim _CollectedtotalRubleWithVAT As Decimal = GetPoTotalCollectedDocRubleWithVATByPO(_POno)
        Dim _NewValue As Decimal = Math.Round(Convert.ToDecimal(TextBoxNaklValue.Text), 2)
        Dim _OldValue As Decimal = 0.0
        Dim _AddingUp As Decimal = _NewValue - _OldValue

        If _POtotalRubleWithVAT < ((_CollectedtotalRubleWithVAT + _AddingUp) / Flexibility(_POno)) Then
            ' Show warning modal popup to user
            Dim a As New MyCommonTasks
            a.SendEmailToAdmin(_POno + " > " + _
               _POtotalRubleWithVAT.ToString + " > " + _CollectedtotalRubleWithVAT.ToString + " + " + _AddingUp.ToString + " > " + Page.User.Identity.Name.ToString, "")
            LabelMaxDocValue.Text = String.Format("{0:#,##0.00}", _
                 _POtotalRubleWithVAT * Flexibility(_POno) - _CollectedtotalRubleWithVAT) + _
             " Rub With VAT"
            'PnlDeliveryWarning.Visible = True
            _GiveNotification.Gritter_Error(Me, "Your transaction is not executed! ", GetTextWarning(), "center")

            Exit Sub
        End If

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " INSERT INTO [Table_PO_Nakladnaya] " + _
                                      "            ([PO_No] " + _
                                      "            ,[Inv_ContrNo] " + _
                                      "            ,[NakNo] " + _
                                      "            ,[Nak_Date] " + _
                                      "            ,[Nak_Rub_WithVAT] " + _
                                      "            ,[SF] " + _
                                      "            ,[CreatedBy] " + _
                                      "            ,[PersonCreated] " + _
                                      "            ,[PDF] " + _
                                      "            ,[Comment]) " + _
                                      "      VALUES " + _
                                      "            (@PO_No " + _
                                      "            ,@Inv_ContrNo " + _
                                      "            ,@NakNo " + _
                                      "            ,@Nak_Date " + _
                                      "            ,@Nak_Rub_WithVAT " + _
                                      "            ,@SF " + _
                                      "            ,@CreatedBy " + _
                                      "            ,@PersonCreated " + _
                                      "            ,@PDF " + _
                                      "            ,@Comment) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar)
            Dim Inv_ContrNo As SqlParameter = cmd.Parameters.Add("@Inv_ContrNo", System.Data.SqlDbType.NVarChar)
            Dim NakNo As SqlParameter = cmd.Parameters.Add("@NakNo", System.Data.SqlDbType.NVarChar)
            Dim Nak_Date As SqlParameter = cmd.Parameters.Add("@Nak_Date", System.Data.SqlDbType.DateTime)
            Dim Nak_Rub_WithVAT As SqlParameter = cmd.Parameters.Add("@Nak_Rub_WithVAT", System.Data.SqlDbType.Decimal)
            Dim SF As SqlParameter = cmd.Parameters.Add("@SF", System.Data.SqlDbType.Bit)
            Dim CreatedBy As SqlParameter = cmd.Parameters.Add("@CreatedBy", System.Data.SqlDbType.DateTime)
            Dim PersonCreated As SqlParameter = cmd.Parameters.Add("@PersonCreated", System.Data.SqlDbType.NVarChar)
            Dim PDF As SqlParameter = cmd.Parameters.Add("@PDF", System.Data.SqlDbType.NVarChar)
            Dim Comment As SqlParameter = cmd.Parameters.Add("@Comment", System.Data.SqlDbType.NVarChar)

            'Assign values to Parameters
            PO_No.Value = labelPOnoNakl.Text
            Inv_ContrNo.Value = TextBoxInvContNo.Text
            NakNo.Value = TextBoxNaklNo.Text
            Nak_Date.Value = TextBoxNaklDate.Text
            Nak_Rub_WithVAT.Value = TextBoxNaklValue.Text
            SF.Value = CheckBoxNakladnayaSF.Checked

            CreatedBy.Value = result
            PersonCreated.Value = Page.User.Identity.Name.ToString
            PDF.Value = HiddenNakladnayaPDFLink.Value
            Comment.Value = TextBoxComment.Text

            Dim dr As SqlDataReader = cmd.ExecuteReader

            ' SHOW notification about insert
            Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Inserted Successfully!</p>")

            ' RESET all controls in MODAL POPUP
            TextBoxInvContNo.Text = ""
            TextBoxNaklNo.Text = ""
            TextBoxNaklDate.Text = ""
            TextBoxNaklValue.Text = ""
            CheckBoxNakladnayaSF.Checked = False

            ' bind gridview
            GridViewNakladnaya.DataBind()
            GridViewNaklEdit.DataBind()

            con.Close()
            con.Dispose()
            dr.Close()

        End Using
    End Sub

    Protected Sub InsertAkt()

        ' Check if entered Delivery doc value exceeds Total Po Value With VAT
        Dim _POno As String = labelPOnoAkt.Text
        Dim _POtotalRubleWithVAT As Decimal = GetPoTotalInRubleWithVAT(_POno)
        Dim _CollectedtotalRubleWithVAT As Decimal = GetPoTotalCollectedDocRubleWithVATByPO(_POno)
        Dim _NewValue As Decimal = Math.Round(Convert.ToDecimal(TextBoxAktValue.Text), 2)
        Dim _OldValue As Decimal = 0.0
        Dim _AddingUp As Decimal = _NewValue - _OldValue

        If _POtotalRubleWithVAT < ((_CollectedtotalRubleWithVAT + _AddingUp) / Flexibility(_POno)) Then
            ' Show warning modal popup to user
            Dim a As New MyCommonTasks
            a.SendEmailToAdmin(_POno + " > " +
               _POtotalRubleWithVAT.ToString + " > " + _CollectedtotalRubleWithVAT.ToString + " + " + _AddingUp.ToString + " > " + Page.User.Identity.Name.ToString, "")
            LabelMaxDocValue.Text = String.Format("{0:#,##0.00}",
                 _POtotalRubleWithVAT * Flexibility(_POno) - _CollectedtotalRubleWithVAT) +
             " Rub With VAT"
            'PnlDeliveryWarning.Visible = True
            _GiveNotification.Gritter_Error(Me, "Your transaction is not executed! ", GetTextWarning(), "center")

            Exit Sub
        End If

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " INSERT INTO [Table_PO_Akt] " +
                                      "            ([PO_No] " +
                                      "            ,[AktNo] " +
                                      "            ,[Akt_Date] " +
                                      "            ,[Akt_Rub_WithVAT] " +
                                      "            ,[SF] " +
                                      "            ,[CreatedBy] " +
                                      "            ,[PersonCreated] " +
                                      "            ,[PDF] " +
                                      "            ,[Comment]) " +
                                      "      VALUES " +
                                      "            (@PO_No " +
                                      "            ,@AktNo " +
                                      "            ,@Akt_Date " +
                                      "            ,@Akt_Rub_WithVAT " +
                                      "            ,@SF " +
                                      "            ,@CreatedBy " +
                                      "            ,@PersonCreated " +
                                      "            ,@PDF " +
                                      "            ,@Comment) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar)
            Dim AktNo As SqlParameter = cmd.Parameters.Add("@AktNo", System.Data.SqlDbType.NVarChar)
            Dim Akt_Date As SqlParameter = cmd.Parameters.Add("@Akt_Date", System.Data.SqlDbType.DateTime)
            Dim Akt_Rub_WithVAT As SqlParameter = cmd.Parameters.Add("@Akt_Rub_WithVAT", System.Data.SqlDbType.Decimal)
            Dim SF As SqlParameter = cmd.Parameters.Add("@SF", System.Data.SqlDbType.Bit)
            Dim CreatedBy As SqlParameter = cmd.Parameters.Add("@CreatedBy", System.Data.SqlDbType.DateTime)
            Dim PersonCreated As SqlParameter = cmd.Parameters.Add("@PersonCreated", System.Data.SqlDbType.NVarChar)
            Dim PDF As SqlParameter = cmd.Parameters.Add("@PDF", System.Data.SqlDbType.NVarChar)
            Dim Comment As SqlParameter = cmd.Parameters.Add("@Comment", System.Data.SqlDbType.NVarChar)

            'Assign values to Parameters
            PO_No.Value = labelPOnoAkt.Text
            AktNo.Value = TextBoxAktNo.Text
            Akt_Date.Value = TextBoxAktDate.Text
            Akt_Rub_WithVAT.Value = TextBoxAktValue.Text
            SF.Value = CheckBoxAktSF.Checked

            CreatedBy.Value = result
            PersonCreated.Value = Page.User.Identity.Name.ToString
            PDF.Value = HiddenFieldAktLink.Value
            Comment.Value = TextBoxAktComment.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader

            ' SHOW notification about insert
            Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Inserted Successfully!</p>")

            ' RESET all controls in MODAL POPUP
            TextBoxAktNo.Text = ""
            TextBoxAktDate.Text = ""
            TextBoxAktValue.Text = ""
            CheckBoxAktSF.Checked = False

            ' bind gridview
            GridViewNakladnaya.DataBind()
            GridViewAktEdit.DataBind()

            con.Close()
            con.Dispose()
            dr.Close()

        End Using
    End Sub

    Protected Sub InsertNakl_Akt_Together()

        ' Check if entered Delivery doc value exceeds Total Po Value With VAT
        Dim _POno As String = labelPOnoNakl.Text
        Dim _POtotalRubleWithVAT As Decimal = GetPoTotalInRubleWithVAT(_POno)
        Dim _CollectedtotalRubleWithVAT As Decimal = GetPoTotalCollectedDocRubleWithVATByPO(_POno)
        Dim _NewValue As Decimal = Math.Round(Convert.ToDecimal(TextBoxNaklValueNakl_Akt_Together.Text), 2) _
            + Math.Round(Convert.ToDecimal(TextBoxAktValueNakl_Akt_Together.Text), 2)
        Dim _OldValue As Decimal = 0.0
        Dim _AddingUp As Decimal = _NewValue - _OldValue

        If _POtotalRubleWithVAT < ((_CollectedtotalRubleWithVAT + _AddingUp) / Flexibility(_POno)) Then
            ' Show warning modal popup to user
            Dim a As New MyCommonTasks
            a.SendEmailToAdmin(_POno + " > " +
               _POtotalRubleWithVAT.ToString + " > " + _CollectedtotalRubleWithVAT.ToString + " + " + _AddingUp.ToString + " > " + Page.User.Identity.Name.ToString, "")
            LabelMaxDocValue.Text = String.Format("{0:#,##0.00}",
                 _POtotalRubleWithVAT * Flexibility(_POno) - _CollectedtotalRubleWithVAT) +
             " Rub With VAT"
            'PnlDeliveryWarning.Visible = True
            _GiveNotification.Gritter_Error(Me, "Your transaction is not executed! ", GetTextWarning(), "center")
            Exit Sub
        End If

        Dim id_Nak As Integer = 0
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " INSERT INTO [Table_PO_Nakladnaya] " +
                                      "            ([PO_No] " +
                                      "            ,[Inv_ContrNo] " +
                                      "            ,[NakNo] " +
                                      "            ,[Nak_Date] " +
                                      "            ,[Nak_Rub_WithVAT] " +
                                      "            ,[SF] " +
                                      "            ,[CreatedBy] " +
                                      "            ,[PersonCreated] " +
                                      "            ,[PDF] " +
                                      "            ,[Comment]) " +
                                      "      VALUES " +
                                      "            (@PO_No " +
                                      "            ,@Inv_ContrNo " +
                                      "            ,@NakNo " +
                                      "            ,@Nak_Date " +
                                      "            ,@Nak_Rub_WithVAT " +
                                      "            ,@SF " +
                                      "            ,@CreatedBy " +
                                      "            ,@PersonCreated " +
                                      "            ,@PDF " +
                                      "            ,@Comment) " +
                                      " ;SELECT SCOPE_IDENTITY() as ID_Nak "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar)
            Dim Inv_ContrNo As SqlParameter = cmd.Parameters.Add("@Inv_ContrNo", System.Data.SqlDbType.NVarChar)
            Dim NakNo As SqlParameter = cmd.Parameters.Add("@NakNo", System.Data.SqlDbType.NVarChar)
            Dim Nak_Date As SqlParameter = cmd.Parameters.Add("@Nak_Date", System.Data.SqlDbType.DateTime)
            Dim Nak_Rub_WithVAT As SqlParameter = cmd.Parameters.Add("@Nak_Rub_WithVAT", System.Data.SqlDbType.Decimal)
            Dim SF As SqlParameter = cmd.Parameters.Add("@SF", System.Data.SqlDbType.Bit)
            Dim CreatedBy As SqlParameter = cmd.Parameters.Add("@CreatedBy", System.Data.SqlDbType.DateTime)
            Dim PersonCreated As SqlParameter = cmd.Parameters.Add("@PersonCreated", System.Data.SqlDbType.NVarChar)
            Dim PDF As SqlParameter = cmd.Parameters.Add("@PDF", System.Data.SqlDbType.NVarChar)
            Dim Comment As SqlParameter = cmd.Parameters.Add("@Comment", System.Data.SqlDbType.NVarChar)
            'Assign values to Parameters
            PO_No.Value = labelPOnoNakl.Text
            Inv_ContrNo.Value = TextBoxInvContNoNakl_Akt_Together.Text
            NakNo.Value = TextBoxNaklNoNakl_Akt_Together.Text
            Nak_Date.Value = TextBoxNaklDateNakl_Akt_Together.Text
            Nak_Rub_WithVAT.Value = TextBoxNaklValueNakl_Akt_Together.Text
            SF.Value = CheckBoxSFNakl_Akt_Together.Checked

            CreatedBy.Value = result
            PersonCreated.Value = Page.User.Identity.Name.ToString
            PDF.Value = HiddenFieldAktToNakladnayaPDFLink.Value
            Comment.Value = TextBoxNaklValueNakl_Akt_TogetherComment.Text

            Dim dr As SqlDataReader = cmd.ExecuteReader

            If dr.HasRows Then
                dr.Read()
                id_Nak = Convert.ToInt32(dr("ID_Nak"))
            End If

            con.Close()
            con.Dispose()
            dr.Close()

        End Using

        ' Start To insert Akt_To_Nakl
        Using conAkt As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            conAkt.Open()

            Dim sqlstringAkt As String = " INSERT INTO [Table_PO_Akt_To_Nak] " +
                                      "            ([ID_Nak] " +
                                      "            ,[AktNo] " +
                                      "            ,[Akt_Date] " +
                                      "            ,[Akt_Rub_WithVAT] " +
                                      "            ,[CreatedBy] " +
                                      "            ,[PersonCreated] " +
                                      "            ,[Comment]) " +
                                      "      VALUES " +
                                      "            (@ID_Nak " +
                                      "            ,@AktNo " +
                                      "            ,@Akt_Date " +
                                      "            ,@Akt_Rub_WithVAT " +
                                      "            ,@CreatedBy " +
                                      "            ,@PersonCreated " +
                                      "            ,@Comment) "

            Dim cmdAkt As New SqlCommand(sqlstringAkt, conAkt)
            cmdAkt.CommandType = System.Data.CommandType.Text

            Dim _ID_Nak As SqlParameter = cmdAkt.Parameters.Add("@ID_Nak", System.Data.SqlDbType.Int)
            Dim AktNo As SqlParameter = cmdAkt.Parameters.Add("@AktNo", System.Data.SqlDbType.NVarChar)
            Dim Akt_Date As SqlParameter = cmdAkt.Parameters.Add("@Akt_Date", System.Data.SqlDbType.DateTime)
            Dim Akt_Rub_WithVAT As SqlParameter = cmdAkt.Parameters.Add("@Akt_Rub_WithVAT", System.Data.SqlDbType.Decimal)
            Dim _CreatedBy As SqlParameter = cmdAkt.Parameters.Add("@CreatedBy", System.Data.SqlDbType.DateTime)
            Dim _PersonCreated As SqlParameter = cmdAkt.Parameters.Add("@PersonCreated", System.Data.SqlDbType.NVarChar)
            Dim Comment As SqlParameter = cmdAkt.Parameters.Add("@Comment", System.Data.SqlDbType.NVarChar)
            'Assign values to Parameters
            _ID_Nak.Value = id_Nak
            AktNo.Value = TextBoxAktNoNakl_Akt_Together.Text
            Akt_Date.Value = TextBoxAktDateNakl_Akt_Together.Text
            Akt_Rub_WithVAT.Value = TextBoxAktValueNakl_Akt_Together.Text
            _CreatedBy.Value = result
            _PersonCreated.Value = Page.User.Identity.Name.ToString
            Comment.Value = TextBoxCommentNakl_Akt_Together.Text

            Dim drAkt As SqlDataReader = cmdAkt.ExecuteReader

            conAkt.Close()
            conAkt.Dispose()
            drAkt.Close()

        End Using

        ' SHOW notification about insert
        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Inserted Successfully!</p>")

        ' RESET all controls in MODAL POPUP
        TextBoxInvContNoNakl_Akt_Together.Text = ""
        TextBoxNaklNoNakl_Akt_Together.Text = ""
        TextBoxNaklDateNakl_Akt_Together.Text = ""
        TextBoxNaklValueNakl_Akt_Together.Text = ""
        CheckBoxSFNakl_Akt_Together.Checked = False
        TextBoxAktNoNakl_Akt_Together.Text = ""
        TextBoxAktDateNakl_Akt_Together.Text = ""
        TextBoxAktValueNakl_Akt_Together.Text = ""

        ' bind gridview
        GridViewNakladnaya.DataBind()
        GridViewAktEdit.DataBind()
        GridViewNaklEdit.DataBind()

    End Sub

    Protected Function VATincluded(ByVal PO_No As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     dbo.View_PoSumWithVAT.PoSumWithVAT - dbo.View_PoSumExcVAT.PoSumExcVAT AS VAT " +
                                      " FROM         dbo.Table2_PONo INNER JOIN " +
                                      " dbo.View_PoSumExcVAT ON dbo.Table2_PONo.PO_No = dbo.View_PoSumExcVAT.PO_No INNER JOIN " +
                                      " dbo.View_PoSumWithVAT ON dbo.Table2_PONo.PO_No = dbo.View_PoSumWithVAT.PO_No " +
                                      " WHERE     (dbo.Table2_PONo.PO_No = @PO_No) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim _PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar)
            _PO_No.Value = PO_No.ToString
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim VATexist As Boolean = True
            While dr.Read
                If dr(0) > 0 Then
                    VATexist = True
                ElseIf dr(0) = 0 Then
                    VATexist = False
                End If
            End While
            Return VATexist
            con.Close()
            con.Dispose()
            dr.Close()

        End Using

    End Function

    Protected Function NaklExist(ByVal PO_No As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT count([ID_Nak]) FROM [Table_PO_Nakladnaya] WHERE PO_No = @PO_No "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim _PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar)
            _PO_No.Value = PO_No.ToString
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim Nklexist As Boolean = True
            While dr.Read
                If dr(0) > 0 Then
                    Nklexist = True
                ElseIf dr(0) = 0 Then
                    Nklexist = False
                End If
            End While
            Return Nklexist
            con.Close()
            con.Dispose()
            dr.Close()

        End Using
    End Function

    Protected Function AktExist(ByVal PO_No As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT count([ID_Akt]) FROM [Table_PO_Akt] WHERE PO_No = @PO_No "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim _PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar)
            _PO_No.Value = PO_No.ToString
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim Akexist As Boolean = True
            While dr.Read
                If dr(0) > 0 Then
                    Akexist = True
                ElseIf dr(0) = 0 Then
                    Akexist = False
                End If
            End While
            Return Akexist
            con.Close()
            con.Dispose()
            dr.Close()

        End Using
    End Function

    Protected Function NumberOf_FREEnakl(ByVal PO_No As String) As Integer
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT COUNT(dbo.Table_PO_Nakladnaya.ID_Nak) AS CountFreeNakl " +
                    " FROM dbo.Table_PO_Nakladnaya LEFT OUTER JOIN " +
                    " dbo.Table_PO_Akt_To_Nak ON dbo.Table_PO_Nakladnaya.ID_Nak = dbo.Table_PO_Akt_To_Nak.ID_Nak " +
                    " WHERE (dbo.Table_PO_Nakladnaya.PO_No = @PO_No) AND (dbo.Table_PO_Akt_To_Nak.ID_Nak IS NULL) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim _PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar)
            _PO_No.Value = PO_No.ToString
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim NofFREEnakl As Integer = 0
            While dr.Read
                NofFREEnakl = dr(0)
            End While
            Return NofFREEnakl
            con.Close()
            con.Dispose()
            dr.Close()

        End Using
    End Function

    Protected Sub GridViewNaklEdit_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewNaklEdit.RowCommand

        ' show Modal POPup explicitly
        'ModalPopupExtenderNakladnayaEdit.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnayaEdit" + "').modal({}) });", True)

        If (e.CommandName = "ShowHistory") Then
            Dim PayReqNo As Integer = e.CommandArgument.ToString
            TextBoxShowHistoryNakladnaya.Text = GetHistory(PayReqNo)
            TextBoxShowHistoryNakladnaya.Visible = True
        End If

        If (e.CommandName = "EditPDF") Then

            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnayaEdit" + "').modal({}) });", True)

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewNaklEdit.Rows(index)
            Dim FileToUpload As FileUpload = DirectCast(row.FindControl("FileToUpload"), FileUpload)
            Dim LabelInfo As Label = DirectCast(row.FindControl("LabelInfo"), Label)
            Dim HiddenFieldPDF As HiddenField = DirectCast(row.FindControl("HiddenFieldPDF"), HiddenField)

            If FileToUpload.HasFile Then

                If Path.GetExtension(FileToUpload.PostedFile.FileName) <> ".pdf" Then
                    LabelInfo.Visible = True
                    LabelInfo.Text = "Please select PDF format file"
                    LabelInfo.CssClass = "label label-danger inline"
                    Exit Sub
                End If

                Dim _linkadress As String = "~/REQUEST/" + MagicString.DeliveryDocs + "/" + MagicString.Nakladnaya + "/"
                Dim _directory As String = Server.MapPath(_linkadress)

                If Not Directory.Exists(_directory) Then
                    Directory.CreateDirectory(_directory)
                End If

                Dim _link As String = _linkadress + Guid.NewGuid().ToString().GetHashCode().ToString("x") + Guid.NewGuid().ToString().GetHashCode().ToString("x") +
                    Path.GetExtension(FileToUpload.PostedFile.FileName)

                FileToUpload.SaveAs(MapPath(_link))

                HiddenFieldPDF.Value = _link

                LabelInfo.Visible = True
                LabelInfo.Text = FileToUpload.PostedFile.FileName + " loaded..."
                LabelInfo.CssClass = "label label-success inline"

            Else

                LabelInfo.Visible = True
                LabelInfo.Text = "You didnt specify any file"
                LabelInfo.CssClass = "label label-danger inline"

            End If

        End If


    End Sub

    Protected Sub GridViewAktEdit_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewAktEdit.RowCommand

        ' show Modal POPup explicitly
        'ModalPopupExtenderAktEdit.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAktEdit" + "').modal({}) });", True)

        If (e.CommandName = "ShowHistory") Then
            Dim PayReqNo As Integer = e.CommandArgument.ToString
            TextBoxShowHistoryAkt.Text = GetHistory(PayReqNo)
            TextBoxShowHistoryAkt.Visible = True
        End If

        If (e.CommandName = "EditPDF") Then

            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAktEdit" + "').modal({}) });", True)

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewAktEdit.Rows(index)
            Dim FileToUpload As FileUpload = DirectCast(row.FindControl("FileToUpload"), FileUpload)
            Dim LabelInfo As Label = DirectCast(row.FindControl("LabelInfo"), Label)
            Dim HiddenFieldPDF As HiddenField = DirectCast(row.FindControl("HiddenFieldPDF"), HiddenField)

            If FileToUpload.HasFile Then

                If Path.GetExtension(FileToUpload.PostedFile.FileName) <> ".pdf" Then
                    LabelInfo.Visible = True
                    LabelInfo.Text = "Please select PDF format file"
                    LabelInfo.CssClass = "label label-danger inline"
                    Exit Sub
                End If

                Dim _linkadress As String = "~/REQUEST/" + MagicString.DeliveryDocs + "/" + MagicString.Akt + "/"
                Dim _directory As String = Server.MapPath(_linkadress)

                If Not Directory.Exists(_directory) Then
                    Directory.CreateDirectory(_directory)
                End If

                Dim _link As String = _linkadress + Guid.NewGuid().ToString().GetHashCode().ToString("x") + Guid.NewGuid().ToString().GetHashCode().ToString("x") +
                    Path.GetExtension(FileToUpload.PostedFile.FileName)

                FileToUpload.SaveAs(MapPath(_link))

                HiddenFieldPDF.Value = _link

                LabelInfo.Visible = True
                LabelInfo.Text = FileToUpload.PostedFile.FileName + " loaded..."
                LabelInfo.CssClass = "label label-success inline"

            Else

                LabelInfo.Visible = True
                LabelInfo.Text = "You didnt specify any file"
                LabelInfo.CssClass = "label label-danger inline"

            End If

        End If

    End Sub

    Protected Sub GridViewAktToNakladnayaEdit_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewAktToNakladnayaEdit.RowCommand

        ' show Modal POPup explicitly
        'ModalPopupExtenderAktToNaklEdit.Show()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAktToNakladnayaEdit" + "').modal({}) });", True)

        If (e.CommandName = "ShowHistory") Then
            Dim PayReqNo As Integer = e.CommandArgument.ToString
            TextBoxShowHistoryAktToNakladnaya.Text = GetHistory(PayReqNo)
            TextBoxShowHistoryAktToNakladnaya.Visible = True
        End If

    End Sub

    Protected Sub GridViewNaklEdit_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles GridViewNaklEdit.RowUpdated

        ' SHOW notification about Update
        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Updated Successfully!</p>")
        ' bind gridview
        GridViewNakladnaya.DataBind()

    End Sub

    Protected Sub GridViewAktEdit_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles GridViewAktEdit.RowUpdated

        ' SHOW notification about Update
        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Updated Successfully!</p>")
        ' bind gridview
        GridViewNakladnaya.DataBind()

    End Sub

    Protected Sub GridViewAktToNakladnayaEdit_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles GridViewAktToNakladnayaEdit.RowUpdated

        ' SHOW notification about Update
        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Updated Successfully!</p>")
        ' bind gridview
        GridViewNakladnaya.DataBind()

    End Sub

    Protected Sub GridViewNaklEdit_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridViewNaklEdit.RowDeleted

        ' SHOW notification about Update
        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Deleted Successfully!</p>")
        ' bind gridview
        GridViewNakladnaya.DataBind()

    End Sub

    Protected Sub GridViewAktEdit_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridViewAktEdit.RowDeleted

        ' SHOW notification about Update
        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Deleted Successfully!</p>")
        ' bind gridview
        GridViewNakladnaya.DataBind()
        GridViewNaklEdit.DataBind()

    End Sub

    Protected Sub GridViewAktToNakladnayaEdit_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridViewAktToNakladnayaEdit.RowDeleted

        ' SHOW notification about Update
        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Deleted Successfully!</p>")
        ' bind gridview
        GridViewNakladnaya.DataBind()
        GridViewNaklEdit.DataBind()

    End Sub
    Protected Sub GridViewNaklEdit_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewNaklEdit.RowUpdating

        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewNaklEdit.Rows(index)
        Dim TextBoxNak_Date As TextBox = DirectCast(row.FindControl("TextBoxNak_Date"), TextBox)
        Dim CheckBocSFedit As CheckBox = DirectCast(row.FindControl("CheckBocSFedit"), CheckBox)
        Dim LabelSFwarning As Label = DirectCast(row.FindControl("LabelSFwarning"), Label)

        If VATincluded(labelPOnoNakl.Text) Then
            If CheckBocSFedit.Checked = False Then
                LabelSFwarning.Visible = True
                e.Cancel = True
                Exit Sub
            End If
        End If

        ' Check if entered Delivery doc value exceeds Total Po Value With VAT
        Dim _POno As String = labelPOnoNakl.Text
        Dim _POtotalRubleWithVAT As Decimal = GetPoTotalInRubleWithVAT(_POno)
        Dim _CollectedtotalRubleWithVAT As Decimal = GetPoTotalCollectedDocRubleWithVATByPO(_POno)
        Dim TextBoxNak_Rub_WithVAT As TextBox = DirectCast(row.FindControl("TextBoxNak_Rub_WithVAT"), TextBox)
        Dim _NewValue As Decimal = Math.Round(Convert.ToDecimal(TextBoxNak_Rub_WithVAT.Text), 2)
        Dim _OldValue As Decimal = Math.Round(Convert.ToDecimal(e.OldValues("Nak_Rub_WithVAT")), 2)
        Dim _AddingUp As Decimal = _NewValue - _OldValue

        If _POtotalRubleWithVAT < ((_CollectedtotalRubleWithVAT + _AddingUp) / Flexibility(_POno)) Then
            ' Show warning modal popup to user
            Dim a As New MyCommonTasks
            a.SendEmailToAdmin(_POno + " > " +
               _POtotalRubleWithVAT.ToString + " > " + Page.User.Identity.Name.ToString, "")
            LabelMaxDocValue.Text = String.Format("{0:#,##0.00}",
                 _POtotalRubleWithVAT * Flexibility(_POno) - _CollectedtotalRubleWithVAT) +
             " Rub With VAT"
            'PnlDeliveryWarning.Visible = True
            _GiveNotification.Gritter_Error(Me, "Your transaction is not executed! ", GetTextWarning(), "center")

            Dim osman As New MyCommonTasks
            osman.SendEmailToAdmin("cancelled", "")


            'ModalPopupExtenderNakladnayaEdit.Hide()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnayaEdit" + "').modal('hide') });", True)
            e.Cancel = True
            Exit Sub
        End If

        ' Validation 1: AKT EXIST
        If AktExist(labelPOnoNakl.Text) Then
            Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">AKT EXIST. YOU MUST DELETE FIRST.</p>")
            e.Cancel = True
            Exit Sub
        End If

        e.NewValues("Nak_Date") = Mid(TextBoxNak_Date.Text.ToString, 7, 4).ToString + "/" +
                                            Mid(TextBoxNak_Date.Text.ToString, 4, 2).ToString + "/" +
                                            Mid(TextBoxNak_Date.Text.ToString, 1, 2).ToString

        Dim TextBoxNak_Comment As TextBox = DirectCast(row.FindControl("TextBoxNak_Comment"), TextBox)
        e.NewValues("Comment") = TextBoxNak_Comment.Text


    End Sub

    Protected Sub GridViewAktEdit_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewAktEdit.RowUpdating

        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewAktEdit.Rows(index)
        Dim TextBoxAkt_Date As TextBox = DirectCast(row.FindControl("TextBoxAkt_Date"), TextBox)
        Dim CheckBocSFedit As CheckBox = DirectCast(row.FindControl("CheckBocSFedit"), CheckBox)
        Dim LabelSFwarning As Label = DirectCast(row.FindControl("LabelSFwarning"), Label)

        If VATincluded(labelPOnoAkt.Text) Then
            If CheckBocSFedit.Checked = False Then
                LabelSFwarning.Visible = True
                e.Cancel = True
                Exit Sub
            End If
        End If

        ' Check if entered Delivery doc value exceeds Total Po Value With VAT
        Dim _POno As String = labelPOnoAkt.Text
        Dim _POtotalRubleWithVAT As Decimal = GetPoTotalInRubleWithVAT(_POno)
        Dim _CollectedtotalRubleWithVAT As Decimal = GetPoTotalCollectedDocRubleWithVATByPO(_POno)
        Dim TextBoxAkt_Rub_WithVAT As TextBox = DirectCast(row.FindControl("TextBoxAkt_Rub_WithVAT"), TextBox)
        Dim _NewValue As Decimal = Math.Round(Convert.ToDecimal(TextBoxAkt_Rub_WithVAT.Text), 2)
        Dim _OldValue As Decimal = Math.Round(Convert.ToDecimal(e.OldValues("Akt_Rub_WithVAT")), 2)
        Dim _AddingUp As Decimal = _NewValue - _OldValue

        If _POtotalRubleWithVAT < ((_CollectedtotalRubleWithVAT + _AddingUp) / Flexibility(_POno)) Then
            ' Show warning modal popup to user
            Dim a As New MyCommonTasks
            a.SendEmailToAdmin(_POno + " > " +
               _POtotalRubleWithVAT.ToString + " > " + _CollectedtotalRubleWithVAT.ToString + " + " + _AddingUp.ToString + " > " + Page.User.Identity.Name.ToString, "")
            LabelMaxDocValue.Text = String.Format("{0:#,##0.00}",
                 _POtotalRubleWithVAT * Flexibility(_POno) - _CollectedtotalRubleWithVAT) +
             " Rub With VAT"
            'PnlDeliveryWarning.Visible = True
            _GiveNotification.Gritter_Error(Me, "Your transaction is not executed! ", GetTextWarning(), "center")
            'ModalPopupExtenderAktEdit.Hide()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAktEdit" + "').modal('hide') });", True)
            e.Cancel = True
            Exit Sub
        End If

        e.NewValues("Akt_Date") = Mid(TextBoxAkt_Date.Text.ToString, 7, 4).ToString + "/" +
                                            Mid(TextBoxAkt_Date.Text.ToString, 4, 2).ToString + "/" +
                                            Mid(TextBoxAkt_Date.Text.ToString, 1, 2).ToString

        Dim TextBoxAkt_Comment As TextBox = DirectCast(row.FindControl("TextBoxAkt_Comment"), TextBox)
        e.NewValues("Comment") = TextBoxAkt_Comment.Text

    End Sub

    Protected Sub GridViewAktToNakladnayaEdit_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewAktToNakladnayaEdit.RowUpdating

        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim row As GridViewRow = GridViewAktToNakladnayaEdit.Rows(index)
        Dim TextBoxAkt_Date_ As TextBox = DirectCast(row.FindControl("TextBoxAkt_Date_"), TextBox)

        ' Check if entered Delivery doc value exceeds Total Po Value With VAT
        Dim _POno As String = labelPOnoNakl.Text
        Dim _POtotalRubleWithVAT As Decimal = GetPoTotalInRubleWithVAT(_POno)
        Dim _CollectedtotalRubleWithVAT As Decimal = GetPoTotalCollectedDocRubleWithVATByPO(_POno)
        Dim TextBoxAkt_Rub_WithVAT_ As TextBox = DirectCast(row.FindControl("TextBoxAkt_Rub_WithVAT_"), TextBox)
        Dim _NewValue As Decimal = Math.Round(Convert.ToDecimal(TextBoxAkt_Rub_WithVAT_.Text), 2)
        Dim _OldValue As Decimal = Math.Round(Convert.ToDecimal(e.OldValues("Akt_Rub_WithVAT")), 2)
        Dim _AddingUp As Decimal = _NewValue - _OldValue

        If _POtotalRubleWithVAT < ((_CollectedtotalRubleWithVAT + _AddingUp) / Flexibility(_POno)) Then
            ' Show warning modal popup to user
            Dim a As New MyCommonTasks
            a.SendEmailToAdmin(_POno + " > " +
               _POtotalRubleWithVAT.ToString + " > " + _CollectedtotalRubleWithVAT.ToString + " + " + _AddingUp.ToString + " > " + Page.User.Identity.Name.ToString, "")
            LabelMaxDocValue.Text = String.Format("{0:#,##0.00}",
                 _POtotalRubleWithVAT * Flexibility(_POno) - _CollectedtotalRubleWithVAT) +
             " Rub With VAT"
            'PnlDeliveryWarning.Visible = True
            _GiveNotification.Gritter_Error(Me, "Your transaction is not executed! ", GetTextWarning(), "center")
            'ModalPopupExtenderAktToNaklEdit.Hide()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAktToNakladnayaEdit" + "').modal('hide') });", True)
            e.Cancel = True
            Exit Sub
        End If

        e.NewValues("Akt_Date") = Mid(TextBoxAkt_Date_.Text.ToString, 7, 4).ToString + "/" +
                                            Mid(TextBoxAkt_Date_.Text.ToString, 4, 2).ToString + "/" +
                                            Mid(TextBoxAkt_Date_.Text.ToString, 1, 2).ToString

        Dim TextBoxAkt_Comment As TextBox = DirectCast(row.FindControl("TextBoxAkt_Comment"), TextBox)
        e.NewValues("Comment") = TextBoxAkt_Comment.Text

    End Sub

    Protected Sub GridViewNaklEdit_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewNaklEdit.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            If DirectCast(e.Row.FindControl("HypPDF"), HyperLink) IsNot Nothing Then
                If Not File.Exists(Server.MapPath(DataBinder.Eval(e.Row.DataItem, "PDF").ToString.Trim())) Then
                    DirectCast(e.Row.FindControl("HypPDF"), HyperLink).Visible = False
                End If
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButtonDelete As LinkButton = DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton)
            Dim ImageButtonFollowUpReports As Image = DirectCast(e.Row.FindControl("ImageButtonFollowUpReports"), Image)
            Dim PanelFollowUpReports As Panel = DirectCast(e.Row.FindControl("PanelFollowUpReports"), Panel)

            ' dont show DELETE botton if there is AKT against it.
            If IsAktExist(DataBinder.Eval(e.Row.DataItem, "ID_Nak").ToString) Then
                If LinkButtonDelete IsNot Nothing Then
                    LinkButtonDelete.Visible = False
                    ImageButtonFollowUpReports.Visible = True
                    PanelFollowUpReports.Visible = True
                End If
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButtonShowHistory As LinkButton = DirectCast(e.Row.FindControl("LinkButtonShowHistory"), LinkButton)
            If Not LinkButtonShowHistory Is Nothing Then
                If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PayReqNo")) AndAlso
                  Len(DataBinder.Eval(e.Row.DataItem, "PayReqNo").ToString()) > 0 Then
                    LinkButtonShowHistory.Visible = True
                Else
                    LinkButtonShowHistory.Visible = False
                End If
            End If
        End If

        ' disable row if 1S matchs
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButtonEdit As LinkButton = DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton)
            Dim LinkButtonDelete As LinkButton = DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton)
            Dim LabelMatchMessageItem As Label = DirectCast(e.Row.FindControl("LabelMatchMessageItem"), Label)
            Dim LabelMatchMessageEdit As Label = DirectCast(e.Row.FindControl("LabelMatchMessageEdit"), Label)

            Dim TextBoxInv_ContrNo As TextBox = DirectCast(e.Row.FindControl("TextBoxInv_ContrNo"), TextBox)
            Dim TextBoxNakNo As TextBox = DirectCast(e.Row.FindControl("TextBoxNakNo"), TextBox)
            Dim TextBoxNak_Date As TextBox = DirectCast(e.Row.FindControl("TextBoxNak_Date"), TextBox)
            Dim TextBoxNak_Rub_WithVAT As TextBox = DirectCast(e.Row.FindControl("TextBoxNak_Rub_WithVAT"), TextBox)
            Dim CheckBocSFedit As CheckBox = DirectCast(e.Row.FindControl("CheckBocSFedit"), CheckBox)

            If DataBinder.Eval(e.Row.DataItem, "MatchIn1S") = 1 Then

                If LinkButtonEdit IsNot Nothing Then
                    ' Edit button visible, Delete button invisible, Provide a lagel control and write the message on this label control
                    LinkButtonEdit.Visible = True
                    LinkButtonDelete.Visible = False
                    LabelMatchMessageItem.Visible = True

                    ' Edit Mode, enable only Comment control, disable all others
                End If

                If LabelMatchMessageEdit IsNot Nothing Then
                    LabelMatchMessageEdit.Visible = True

                    TextBoxInv_ContrNo.Enabled = False
                    TextBoxNakNo.Enabled = False
                    TextBoxNak_Date.Enabled = False
                    TextBoxNak_Rub_WithVAT.Enabled = False
                    CheckBocSFedit.Enabled = False

                End If

            Else
                If LinkButtonEdit IsNot Nothing Then
                    ' Edit button visible, Delete button invisible, Provide a lagel control and write the message on this label control
                    LinkButtonEdit.Visible = True
                    LinkButtonDelete.Visible = True
                    LabelMatchMessageItem.Visible = False

                    ' Edit Mode, enable only Comment control, disable all others
                End If

                If LabelMatchMessageEdit IsNot Nothing Then
                    LabelMatchMessageEdit.Visible = False

                    TextBoxInv_ContrNo.Enabled = True
                    TextBoxNakNo.Enabled = True
                    TextBoxNak_Date.Enabled = True
                    TextBoxNak_Rub_WithVAT.Enabled = True
                    CheckBocSFedit.Enabled = True
                End If

            End If
        End If

    End Sub

    Protected Function IsAktExist(ByVal _Id_nak As Integer) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String =
              "SELECT count([ID_Nak]) FROM [Table_PO_Akt_To_Nak] where Id_nak = @Id_nak"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim Id_nak As SqlParameter = cmd.Parameters.Add("@Id_nak", System.Data.SqlDbType.Int)
            Id_nak.Value = _Id_nak
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Boolean = True
            While dr.Read
                If dr(0) > 0 Then
                    _return = True
                Else
                    _return = False
                End If
            End While
            Return _return
            con.Close()
            con.Dispose()
            dr.Close()

        End Using
    End Function

    Protected Function GetHistory(ByVal PayReqNo As Integer) As String
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String =
              " SELECT  [CollectionHistory] FROM [Table5_PayLog] where PayReqNo = @PayReqNo "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim _PayReqNo As SqlParameter = cmd.Parameters.Add("@PayReqNo", System.Data.SqlDbType.Int)
            _PayReqNo.Value = PayReqNo
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As String = ""
            While dr.Read
                _return = dr(0).ToString
            End While
            Return _return
            con.Close()
            con.Dispose()
            dr.Close()

        End Using

    End Function

    Protected Sub GridViewAktEdit_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewAktEdit.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            If DirectCast(e.Row.FindControl("HypPDF"), HyperLink) IsNot Nothing Then
                If Not File.Exists(Server.MapPath(DataBinder.Eval(e.Row.DataItem, "PDF").ToString.Trim())) Then
                    DirectCast(e.Row.FindControl("HypPDF"), HyperLink).Visible = False
                End If
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButtonShowHistory As LinkButton = DirectCast(e.Row.FindControl("LinkButtonShowHistory"), LinkButton)
            If Not LinkButtonShowHistory Is Nothing Then
                If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PayReqNo")) AndAlso
                  Len(DataBinder.Eval(e.Row.DataItem, "PayReqNo").ToString()) > 0 Then
                    LinkButtonShowHistory.Visible = True
                Else
                    LinkButtonShowHistory.Visible = False
                End If
            End If
        End If

        ' disable row if 1S matchs
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButtonEdit As LinkButton = DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton)
            Dim LinkButtonDelete As LinkButton = DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton)
            Dim LabelMatchMessageItem As Label = DirectCast(e.Row.FindControl("LabelMatchMessageItem"), Label)
            Dim LabelMatchMessageEdit As Label = DirectCast(e.Row.FindControl("LabelMatchMessageEdit"), Label)

            Dim TextBoxInv_AktNo As TextBox = DirectCast(e.Row.FindControl("TextBoxInv_AktNo"), TextBox)
            Dim TextBoxAkt_Date As TextBox = DirectCast(e.Row.FindControl("TextBoxAkt_Date"), TextBox)
            Dim TextBoxAkt_Rub_WithVAT As TextBox = DirectCast(e.Row.FindControl("TextBoxAkt_Rub_WithVAT"), TextBox)
            Dim CheckBocSFedit As CheckBox = DirectCast(e.Row.FindControl("CheckBocSFedit"), CheckBox)

            If DataBinder.Eval(e.Row.DataItem, "MatchIn1S") = 1 Then

                If LinkButtonEdit IsNot Nothing Then
                    ' Edit button visible, Delete button invisible, Provide a lagel control and write the message on this label control
                    LinkButtonEdit.Visible = True
                    LinkButtonDelete.Visible = False
                    LabelMatchMessageItem.Visible = True

                    ' Edit Mode, enable only Comment control, disable all others
                End If

                If LabelMatchMessageEdit IsNot Nothing Then
                    LabelMatchMessageEdit.Visible = True

                    TextBoxInv_AktNo.Enabled = False
                    TextBoxAkt_Date.Enabled = False
                    TextBoxAkt_Rub_WithVAT.Enabled = False
                    CheckBocSFedit.Enabled = False

                End If

            Else
                If LinkButtonEdit IsNot Nothing Then
                    ' Edit button visible, Delete button invisible, Provide a lagel control and write the message on this label control
                    LinkButtonEdit.Visible = True
                    LinkButtonDelete.Visible = True
                    LabelMatchMessageItem.Visible = False

                    ' Edit Mode, enable only Comment control, disable all others
                End If

                If LabelMatchMessageEdit IsNot Nothing Then
                    LabelMatchMessageEdit.Visible = False

                    TextBoxInv_AktNo.Enabled = True
                    TextBoxAkt_Date.Enabled = True
                    TextBoxAkt_Rub_WithVAT.Enabled = True
                    CheckBocSFedit.Enabled = True

                End If

            End If
        End If

    End Sub

    Protected Sub GridViewAktToNakladnayaEdit_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewAktToNakladnayaEdit.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButtonShowHistory As LinkButton = DirectCast(e.Row.FindControl("LinkButtonShowHistory"), LinkButton)
            If Not LinkButtonShowHistory Is Nothing Then
                If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PayReqNo")) AndAlso
                  Len(DataBinder.Eval(e.Row.DataItem, "PayReqNo").ToString()) > 0 Then
                    LinkButtonShowHistory.Visible = True
                Else
                    LinkButtonShowHistory.Visible = False
                End If
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButtonEdit As LinkButton = DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton)
            Dim LinkButtonDelete As LinkButton = DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton)
            Dim LabelMatchMessageItem As Label = DirectCast(e.Row.FindControl("LabelMatchMessageItem"), Label)
            Dim LabelMatchMessageEdit As Label = DirectCast(e.Row.FindControl("LabelMatchMessageEdit"), Label)

            Dim TextBoxInv_AktNo_ As TextBox = DirectCast(e.Row.FindControl("TextBoxInv_AktNo_"), TextBox)
            Dim TextBoxAkt_Date_ As TextBox = DirectCast(e.Row.FindControl("TextBoxAkt_Date_"), TextBox)
            Dim TextBoxAkt_Rub_WithVAT_ As TextBox = DirectCast(e.Row.FindControl("TextBoxAkt_Rub_WithVAT_"), TextBox)

            If DataBinder.Eval(e.Row.DataItem, "MatchIn1S") = 1 Then

                If LinkButtonEdit IsNot Nothing Then
                    ' Edit button visible, Delete button invisible, Provide a lagel control and write the message on this label control
                    LinkButtonEdit.Visible = True
                    LinkButtonDelete.Visible = False
                    LabelMatchMessageItem.Visible = True

                    ' Edit Mode, enable only Comment control, disable all others
                End If

                If LabelMatchMessageEdit IsNot Nothing Then
                    LabelMatchMessageEdit.Visible = True

                    TextBoxInv_AktNo_.Enabled = False
                    TextBoxAkt_Date_.Enabled = False
                    TextBoxAkt_Rub_WithVAT_.Enabled = False

                End If

            Else
                If LinkButtonEdit IsNot Nothing Then
                    ' Edit button visible, Delete button invisible, Provide a lagel control and write the message on this label control
                    LinkButtonEdit.Visible = True
                    LinkButtonDelete.Visible = True
                    LabelMatchMessageItem.Visible = False

                    ' Edit Mode, enable only Comment control, disable all others
                End If

                If LabelMatchMessageEdit IsNot Nothing Then
                    LabelMatchMessageEdit.Visible = False

                    TextBoxInv_AktNo_.Enabled = True
                    TextBoxAkt_Date_.Enabled = True
                    TextBoxAkt_Rub_WithVAT_.Enabled = True

                End If

            End If
        End If

    End Sub

    Protected Function GetPoTotalInRubleWithVAT(ByVal _PO_No As String) As Decimal

        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        Using con
            con.Open()
            Dim sqlstring As String = "GetPoTotalInRubleWithVAT"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar, 11)
            PO_No.Value = _PO_No
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Decimal = 0
            While dr.Read
                _return = dr(0)
            End While
            con.Close()
            con.Dispose()
            dr.Close()
            Return _return
        End Using

    End Function

    Protected Function GetPoTotalCollectedDocRubleWithVATByPO(ByVal _PO_No As String) As Decimal

        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        Using con
            con.Open()
            Dim sqlstring As String = "GetPoTotalCollectedDocRubleWithVATByPO"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar, 11)
            PO_No.Value = _PO_No
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As Decimal = 0
            While dr.Read
                _return = dr(0)
            End While
            con.Close()
            con.Dispose()
            dr.Close()
            Return _return
        End Using

    End Function

    Protected Function GetPoCurrency(ByVal _PO_No As String) As String

        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        Using con
            con.Open()
            Dim sqlstring As String = "Select RTRIM(PO_Currency) FROM Table2_PONo WHERE Po_No = @PO_No"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar, 11)
            PO_No.Value = _PO_No
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As String = ""
            While dr.Read
                _return = dr(0)
            End While
            con.Close()
            con.Dispose()
            dr.Close()
            Return _return
        End Using

    End Function

    Protected Function Flexibility(ByVal _PO_No As String) As Decimal

        Dim _return As Decimal = 1

        If GetPoCurrency(_PO_No).ToLower <> "rub" Then
            ' use 17% flexibility
            _return = 1.17
        End If

        Return _return

    End Function

    Protected Sub DropDownListPrj_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString())

        nameValues.Remove("SupplierID")

        nameValues.Set("ProjectID", DropDownListPrj.SelectedValue.ToString)
        Dim url As String = Request.Url.AbsolutePath
        Dim updatedQueryString As String = "?" + nameValues.ToString()
        Response.Redirect(url + updatedQueryString)

    End Sub

    Protected Sub DropDownListSupplier_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString())
        nameValues.Set("SupplierID", DropDownListSupplier.SelectedValue.ToString.Trim)

        If DropDownListSupplier.SelectedIndex = 0 Then
            nameValues.Remove("SupplierID")
        End If

        Dim url As String = Request.Url.AbsolutePath
        Dim updatedQueryString As String = "?" + nameValues.ToString()
        Response.Redirect(url + updatedQueryString)

    End Sub

    Protected Sub LinkButtonNakladnayaPDF_Click(sender As Object, e As EventArgs)

        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakladnaya" + "').modal({}) });", True)

        If FileUploadNakladnayaPDF.HasFile Then

            If Path.GetExtension(FileUploadNakladnayaPDF.PostedFile.FileName) <> ".pdf" Then
                LabelInfo.Visible = True
                LabelInfo.Text = "Please select PDF format file"
                LabelInfo.CssClass = "label label-danger inline"
                Exit Sub
            End If

            Dim _linkadress As String = "~/REQUEST/" + MagicString.DeliveryDocs + "/" + MagicString.Nakladnaya + "/"
            Dim _directory As String = Server.MapPath(_linkadress)

            If Not Directory.Exists(_directory) Then
                Directory.CreateDirectory(_directory)
            End If

            Dim _link As String = _linkadress + Guid.NewGuid().ToString().GetHashCode().ToString("x") + Guid.NewGuid().ToString().GetHashCode().ToString("x") + _
                Path.GetExtension(FileUploadNakladnayaPDF.PostedFile.FileName)

            FileUploadNakladnayaPDF.SaveAs(MapPath(_link))

            HiddenNakladnayaPDFLink.Value = _link

            LabelInfo.Visible = True
            LabelInfo.Text = FileUploadNakladnayaPDF.PostedFile.FileName + " loaded..."
            LabelInfo.CssClass = "label label-success inline"

        Else

            LabelInfo.Visible = True
            LabelInfo.Text = "You didnt specify any file"
            LabelInfo.CssClass = "label label-danger inline"

        End If

    End Sub

    Protected Sub LinkButtonAktPDF_Click(sender As Object, e As EventArgs)

        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalAkt" + "').modal({}) });", True)

        If FileUploadAkt.HasFile Then

            If Path.GetExtension(FileUploadAkt.PostedFile.FileName) <> ".pdf" Then
                LabelInfoAkt.Visible = True
                LabelInfoAkt.Text = "Please select PDF format file"
                LabelInfoAkt.CssClass = "label label-danger inline"
                Exit Sub
            End If

            Dim _linkadress As String = "~/REQUEST/" + MagicString.DeliveryDocs + "/" + MagicString.Akt + "/"
            Dim _directory As String = Server.MapPath(_linkadress)

            If Not Directory.Exists(_directory) Then
                Directory.CreateDirectory(_directory)
            End If

            Dim _link As String = _linkadress + Guid.NewGuid().ToString().GetHashCode().ToString("x") + Guid.NewGuid().ToString().GetHashCode().ToString("x") + _
                Path.GetExtension(FileUploadAkt.PostedFile.FileName)

            FileUploadAkt.SaveAs(MapPath(_link))

            HiddenFieldAktLink.Value = _link

            LabelInfoAkt.Visible = True
            LabelInfoAkt.Text = FileUploadAkt.PostedFile.FileName + " loaded..."
            LabelInfoAkt.CssClass = "label label-success inline"

        Else

            LabelInfoAkt.Visible = True
            LabelInfoAkt.Text = "You didnt specify any file"
            LabelInfoAkt.CssClass = "label label-danger inline"

        End If

    End Sub

    Protected Sub LinkButtonAktToNakladnayaPDF_Click(sender As Object, e As EventArgs)

        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalNakl_Akt_Together" + "').modal({}) });", True)

        If FileUploadAkToNakladnaya.HasFile Then

            If Path.GetExtension(FileUploadAkToNakladnaya.PostedFile.FileName) <> ".pdf" Then
                LabelInfoAktToNakladnaya.Visible = True
                LabelInfoAktToNakladnaya.Text = "Please select PDF format file"
                LabelInfoAktToNakladnaya.CssClass = "label label-danger inline"
                Exit Sub
            End If

            Dim _linkadress As String = "~/REQUEST/" + MagicString.DeliveryDocs + "/" + MagicString.AktToNakladnaya + "/"
            Dim _directory As String = Server.MapPath(_linkadress)

            If Not Directory.Exists(_directory) Then
                Directory.CreateDirectory(_directory)
            End If

            Dim _link As String = _linkadress + Guid.NewGuid().ToString().GetHashCode().ToString("x") + Guid.NewGuid().ToString().GetHashCode().ToString("x") + _
                Path.GetExtension(FileUploadAkToNakladnaya.PostedFile.FileName)

            FileUploadAkToNakladnaya.SaveAs(MapPath(_link))

            HiddenFieldAktToNakladnayaPDFLink.Value = _link

            LabelInfoAktToNakladnaya.Visible = True
            LabelInfoAktToNakladnaya.Text = FileUploadAkToNakladnaya.PostedFile.FileName + " loaded..."
            LabelInfoAktToNakladnaya.CssClass = "label label-success inline"

        Else

            LabelInfoAktToNakladnaya.Visible = True
            LabelInfoAktToNakladnaya.Text = "You didnt specify any file"
            LabelInfoAktToNakladnaya.CssClass = "label label-danger inline"

        End If

    End Sub
End Class
