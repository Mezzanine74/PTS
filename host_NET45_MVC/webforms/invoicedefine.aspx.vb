Imports AjaxControlToolkit
Imports System.IO

Partial Class invoicedefine
    Inherits System.Web.UI.Page

    Public Function GetDropDownListInvoiceType() As IQueryable(Of PTS_MERCURY.db.Table3_Invoice_Type)
        Return PTS_MERCURY.helper.Table3_Invoice_Type.GetDropDownListInvoice_Type()
    End Function

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' this section will help to stop user if invoice value exceeds advance payment of Contract. This will work only if it is first invoice of the PO
        Dim TextBoxAdvance As TextBox = FormViewInvoice.FindControl("TextBoxAdvance")
        Dim DropDownListPOselector As DropDownList = FormViewInvoice.FindControl("DropDownListPOselector")
        TextBoxAdvance.Text = ValidateInvoiceValueAgainstContractAdvance.GetAdvancePaymentValue(DropDownListPOselector.SelectedValue.ToString).Column1

        ' this section will help to stop user if invoice value exceeds advance payment of Contract. This will work only if it is first invoice of the PO
        Dim TextBoxAdvanceAddendum As TextBox = FormViewInvoice.FindControl("TextBoxAdvanceAddendum")
        TextBoxAdvanceAddendum.Text = ValidateInvoiceValueAgainstContractAdvance.GetAdvancePaymentValueFromAddendum(DropDownListPOselector.SelectedValue.ToString)

        If IsPostBack Or Not IsPostBack Then
            ' it provides query parameter for DropDownListProject
            TextBoxUserName.Text = Page.User.Identity.Name
        End If

        ' it transfer previous page onto here...
        If Not IsPostBack Then
            If Request.QueryString("PoNo") IsNot Nothing Then
                If Not String.IsNullOrEmpty(Request.QueryString("PoNo").ToString) Then

                    'Dim previousPageURL As String = Request.UrlReferrer.AbsolutePath.ToString
                    'Dim PageFileInfo As New System.IO.FileInfo(previousPageURL)
                    'Dim ContPlaceHold As ContentPlaceHolder = PreviousPage.Master.FindControl("MainContent")
                    'Dim UpdatePanel1 As UpdatePanel = ContPlaceHold.FindControl("UpdatePanel1")
                    'Dim FrmViewPO As FormView = UpdatePanel1.FindControl("FormViewPO")

                    Dim PoNo As String = Request.QueryString("PoNo").ToString
                    ' Project_ID
                    Dim ProjectID As String

                    Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                        Dim ta = (From C In db.Table2_PONo Where C.PO_No = PoNo.Trim)

                        ProjectID = (From C In db.Table2_PONo Where C.PO_No = PoNo).ToList()(0).Project_ID

                        db.Dispose()

                    End Using

                    Dim DrpPrjThis As DropDownList = FormViewInvoice.FindControl("DropDownListPrjID")
                    Dim DrpPrjPOselect As DropDownList = FormViewInvoice.FindControl("DropDownListPOselector")

                    DrpPrjThis.DataBind()
                    DrpPrjThis.SelectedValue = Convert.ToInt32(ProjectID)

                    DrpPrjPOselect.DataBind()

                    DrpPrjPOselect.SelectedValue = PoNo

                    '***************************************************************************************
                    'It feeds POsum+TotalInvoice+Outstanding textboxes
                    '***************************************************************************************
                    Dim GrdPOhistory1 As GridView = FormViewInvoice.FindControl("GridViewInvoiceHistoryperPO")
                    Dim DrpPOSumHolder1 As DropDownList = FormViewInvoice.FindControl("DropDownListPOSumHolder")
                    Dim TxtBoxPOSum1 As TextBox = FormViewInvoice.FindControl("TextBoxTotalPO")
                    Dim DrpTotInvoiceHolder1 As DropDownList = FormViewInvoice.FindControl("DropDownListTotalInvoiceHolder")
                    Dim TxtBoxInvoiceTotal1 As TextBox = FormViewInvoice.FindControl("TextBoxTotalInvoice")
                    Dim TxtBoxOutstanding1 As TextBox = FormViewInvoice.FindControl("TextBoxOutstanding")
                    Dim DrpToOutstanding1 As DropDownList = FormViewInvoice.FindControl("DropDownListOutstanding")
                    Dim DrpINN1 As DropDownList = FormViewInvoice.FindControl("DropDownListINNCarrier")

                    ' variable to work with progress bar and percentage
                    Dim LabelTotalInvoicePercent As Label = FormViewInvoice.FindControl("LabelTotalInvoicePercent")
                    Dim LabelTotalOutstandingPercent As Label = FormViewInvoice.FindControl("LabelTotalOutstandingPercent")
                    Dim ImageTotalInvoiceProgress As Image = FormViewInvoice.FindControl("ImageTotalInvoiceProgress")
                    Dim ImageTotalOutstandingProgress As Image = FormViewInvoice.FindControl("ImageTotalOutstandingProgress")


                    Dim X011 As TextBox = FormViewInvoice.FindControl("Invoice_NoTextBox")
                    Dim X021 As TextBox = FormViewInvoice.FindControl("Invoice_DateTransferTextBox")
                    Dim X031 As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")
                    Dim X041 As TextBox = FormViewInvoice.FindControl("NotesTextBox")
                    'Dim X051 As TextBox = FormViewInvoice.FindControl("DeadlineToPayTransferTextBox")
                    Dim X061 As GridView = FormViewInvoice.FindControl("GridViewInvoiceHistoryperPO")

                    ' it enables active controls
                    X011.Enabled = True
                    X021.Enabled = True
                    X031.Enabled = True
                    X041.Enabled = True
                    'X051.Enabled = True
                    X061.Enabled = True

                    'Invoice history run
                    GrdPOhistory1.DataBind()

                    'POSum restored in Dropdownlist as Value
                    DrpPOSumHolder1.DataBind()
                    'POSum value transfered from Dropdownlist into TextBox
                    TxtBoxPOSum1.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DrpPOSumHolder1.Items(0).Value))

                    'Total Invoice restored in Dropdownlist as Value
                    DrpTotInvoiceHolder1.DataBind()
                    'Total Invoice value transfered from Dropdownlist into TextBox
                    TxtBoxInvoiceTotal1.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DrpTotInvoiceHolder1.Items(0).Value))
                    LabelTotalInvoicePercent.Text = " " + Math.Round((Convert.ToDecimal(DrpTotInvoiceHolder1.SelectedValue) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) * 100), 2).ToString + " %"
                    ImageTotalInvoiceProgress.Width = Math.Round(Convert.ToDecimal(DrpTotInvoiceHolder1.SelectedValue) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) * 100, 2)


                    'Evaluates the Outstanding 
                    DrpToOutstanding1.DataBind()
                    'TxtBoxOutstanding1.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DrpToOutstanding1.SelectedValue))
                    TxtBoxOutstanding1.Text = DrpToOutstanding1.Items(0).Value
                    LabelTotalOutstandingPercent.Text = " " + Math.Round((Convert.ToDecimal(DrpToOutstanding1.SelectedValue) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) * 100), 2).ToString + " %"
                    ImageTotalOutstandingProgress.Width = Math.Round(Convert.ToDecimal(DrpToOutstanding1.SelectedValue) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) * 100, 2)


                    ' INN number drp runs
                    DrpINN1.DataBind()

                    'it focus on
                    'X011.Focus()

                    ' Show or Hide Invoice PDF control
                    Dim PanelInvoicePDF As Panel = FormViewInvoice.FindControl("PanelInvoicePDF")
                    If PTS.CoreTables.CreateDataReader.Create_Table1_Project(ProjectID).PR_Coverpage_Approval_Compulsory = True Then
                        ShowOrHidePDFcontrols(True)
                    ElseIf PTS.CoreTables.CreateDataReader.Create_Table1_Project(ProjectID).PR_Coverpage_Approval_Compulsory = False Then
                        ShowOrHidePDFcontrols(False)
                    End If

                End If
            End If

        End If

        ' it disables active controls
        If Request.QueryString("PoNo") Is Nothing Then
            If Not IsPostBack Then
                Dim X011 As TextBox = FormViewInvoice.FindControl("Invoice_NoTextBox")
                Dim X022 As TextBox = FormViewInvoice.FindControl("Invoice_DateTransferTextBox")
                Dim X033 As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")
                Dim X044 As TextBox = FormViewInvoice.FindControl("NotesTextBox")
                Dim X066 As GridView = FormViewInvoice.FindControl("GridViewInvoiceHistoryperPO")

                X011.Enabled = False
                X022.Enabled = False
                X033.Enabled = False
                X044.Enabled = False
                X066.Enabled = False
            End If
        End If
        ' it makes labelDoublicateInvoiceWarning invisible
        If Not IsPostBack Then
            Dim X09 As Label = FormViewInvoice.FindControl("LabelDoublicateInvoiceWarning")
            X09.Visible = False
        End If

        'If previous page is nothing and if it is not postback then it requery dropdownProject
        '_ then dropdownProject databound event resets control dropdownproject
        If Not IsPostBack Then
            If PreviousPage Is Nothing Then
                Dim DrpProject As DropDownList = FormViewInvoice.FindControl("DropDownListPrjID")
                DrpProject.DataBind()


            End If
        End If
        '*********************************************************************************

        If Not IsPostBack Then
            If Request.QueryString("PoNo") Is Nothing Then
                ShowOrHidePDFcontrols(False)
            End If
        End If

        ' If postback is coming from Invoice Date textbox, then it focus on Invoice Value textbox
        If IsPostBack Then
            Dim controllX As New String(Page.Request.Params.Get("__EVENTTARGET"))
            Dim TextInvoiceValueX As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")
            If (Not controllX Is Nothing) Or (controllX <> "") Then
                If controllX = "ctl00$MainContent$FormViewInvoice$Invoice_DateTransferTextBox" Then
                    TextInvoiceValueX.Focus()
                End If
            End If
        End If
        '*********************************************************************************

        ' If postback is coming from DropDownListPOselector, then it removes Select PO statement from first row
        If IsPostBack Then
            Dim DropDownPoselector As DropDownList = FormViewInvoice.FindControl("DropDownListPOselector")
            Dim controll1 As New String(Page.Request.Params.Get("__EVENTTARGET"))

            If (Not controll1 Is Nothing) Or (controll1 <> "") Then
                If controll1 = "ctl00$MainContent$FormViewInvoice$DropDownListPOselector" Then
                    If DropDownPoselector.Items(0).ToString = "Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO |" Then
                        DropDownPoselector.Items.RemoveAt(0)
                    End If
                End If
            End If
        End If
        '*********************************************************************************

        ' If postback is coming from DropDownListProject, then it removes Select Project statement from first row
        If IsPostBack Then
            Dim DropDownProject As DropDownList = FormViewInvoice.FindControl("DropDownListPrjID")
            Dim controll1 As New String(Page.Request.Params.Get("__EVENTTARGET"))

            If (Not controll1 Is Nothing) Or (controll1 <> "") Then
                If controll1 = "ctl00$MainContent$FormViewInvoice$DropDownListPrjID" Then
                    If DropDownProject.Items(0).ToString = "Select Project " Then
                        DropDownProject.Items.RemoveAt(0)
                    End If
                End If
            End If
        End If
        '*********************************************************************************

        'It runs requery for PO selector
        If Not IsPostBack Then
            'It runs requery for PO selector
            Dim DrpDwnPoSelctror As DropDownList = FormViewInvoice.FindControl("DropDownListPOselector")
            'DrpDwnPoSelctror.DataBind()
        End If

        '*********************************************************************************
        'It refresh invoice date again. It losts value after postback because it is readonly...
        If IsPostBack Then
            Dim controll1 As New String(Page.Request.Params.Get("__EVENTTARGET"))
            If (Not controll1 Is Nothing) Or (controll1 <> "") Then
                If controll1 = "ctl00$MainContent$FormViewInvoice$Invoice_DateTransferTextBox" Then
                    Dim GridInvDoublicate As GridView = FormViewInvoice.FindControl("GridViewInvoiceDoublicate")
                    'Dim DateTextBoxInvoice As TextBox = FormViewInvoice.FindControl("Invoice_DateTextBoxPostBack")
                    Dim DateTextBoxInvoiceHolder As TextBox = FormViewInvoice.FindControl("Invoice_DateTransferTextBox")
                    Dim InvoiceValueTextBox As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")
                    GridInvDoublicate.DataBind()
                    InvoiceValueTextBox.Focus()
                End If
            End If
        End If

        '*********************************************************************************
        'It checks invoice if it is doublicated
        If IsPostBack Then
            Dim controll10 As New String(Page.Request.Params.Get("__EVENTTARGET"))
            If (Not controll10 Is Nothing) Or (controll10 <> "") Then
                If controll10 = "ctl00$MainContent$FormViewInvoice$Invoice_NoTextBox" Then
                    Dim GridInvDoublicate As GridView = FormViewInvoice.FindControl("GridViewInvoiceDoublicate")
                    GridInvDoublicate.DataBind()
                End If
            End If
        End If
        '*********************************************************************************

        Dim GridViewInvoiceHistoryperPO As GridView = FormViewInvoice.FindControl("GridViewInvoiceHistoryperPO")
        If GridViewInvoiceHistoryperPO.Rows.Count = 0 Then
            GridViewInvoiceHistoryperPO.EmptyDataText = "<span class=" + """" + "label label-lg label-pink arrowed-right" + """" + ">No invoice defined yet</span>"
            'Response.Write(GridViewInvoiceHistoryperPO.Rows.Count.ToString)
        Else

        End If

        Dim RequiredFieldValidatorInvoiceType As RequiredFieldValidator = TryCast(FormViewInvoice.FindControl("RequiredFieldValidatorInvoiceType"), RequiredFieldValidator)

        If RequiredFieldValidatorInvoiceType IsNot Nothing Then
            If PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table2_PONo.IfPoCorrespondsToSubcontractorContractOrAddendum(DropDownListPOselector.SelectedValue.ToString.Trim()) Then
                RequiredFieldValidatorInvoiceType.Enabled = True
            Else
                RequiredFieldValidatorInvoiceType.Enabled = False
            End If
        End If

    End Sub

    Protected Sub SqlDataSourceInvoice_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceInvoice.Inserted

        Dim NewID As Integer = e.Command.Parameters("@id").Value
        TextBoxStoreInvoiceID.Text = NewID.ToString

        ' Insert PDF link to database
        Dim DropDownListPrjID As DropDownList = FormViewInvoice.FindControl("DropDownListPrjID")
        Dim TextBoxLinkToInvoicePDF As TextBox = FormViewInvoice.FindControl("TextBoxLinkToInvoicePDF")

        If PTS.CoreTables.CreateDataReader.Create_Table1_Project(DropDownListPrjID.SelectedValue).PR_Coverpage_Approval_Compulsory = True Then
            Using Adapter As New InvoicePDFTableAdapters.Table3_Invoice_PDFTableAdapter
                Adapter.Insert(NewID, TextBoxLinkToInvoicePDF.Text.Trim)
                Adapter.Dispose()
            End Using

        End If

        ' insert invoiceID and Invoice type to Invoice_Type_Junction table
        Dim DropDownListInvoiceType As DropDownList = TryCast(FormViewInvoice.FindControl("DropDownListInvoiceType"), DropDownList)
        If DropDownListInvoiceType.SelectedValue <> 0 Then
            PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table3_Invoice_Type_Junction.Insert(NewID, DropDownListInvoiceType.SelectedValue)
        End If

    End Sub

    Protected Sub DropDownListPrjID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        ' it rejects Marina from TNK-BP to Access Denied Page
        Dim DropDownListPrjID As DropDownList = FormViewInvoice.FindControl("DropDownListPrjID")
        If Page.User.Identity.Name.ToString = "marina" OrElse Page.User.Identity.Name.ToString = "n.komleva" AndAlso DropDownListPrjID.SelectedValue = 123 Then
            Response.Redirect("~/webforms/AccessDenied.aspx")
        End If

        ' variable to work with progress bar and percentage
        Dim LabelTotalInvoicePercent As Label = FormViewInvoice.FindControl("LabelTotalInvoicePercent")
        Dim LabelTotalOutstandingPercent As Label = FormViewInvoice.FindControl("LabelTotalOutstandingPercent")
        Dim ImageTotalInvoiceProgress As Image = FormViewInvoice.FindControl("ImageTotalInvoiceProgress")
        Dim ImageTotalOutstandingProgress As Image = FormViewInvoice.FindControl("ImageTotalOutstandingProgress")


        ' it makes labelDoublicateInvoiceWarning invisible
        If IsPostBack Then
            Dim X14 As Label = FormViewInvoice.FindControl("LabelDoublicateInvoiceWarning")
            X14.Visible = False
        End If


        'It runs requery for PO selector
        Dim DrpDwnPoSelctror As DropDownList = FormViewInvoice.FindControl("DropDownListPOselector")
        DrpDwnPoSelctror.DataBind()

        ' It resets invoice no
        Dim TextInvoiceno As TextBox = FormViewInvoice.FindControl("Invoice_NoTextBox")
        TextInvoiceno.Text = ""
        '***************************************************************************************
        ' It resets invoice date
        Dim TextInvoiceDateTransfer As TextBox = FormViewInvoice.FindControl("Invoice_DateTransferTextBox")
        TextInvoiceDateTransfer.Text = ""
        '***************************************************************************************
        ' It resets invoice value
        Dim TextInvoiceValue As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")
        TextInvoiceValue.Text = ""
        '***************************************************************************************
        ' It resets invoice value carrier
        Dim TextInvoiceValueCarrier As TextBox = FormViewInvoice.FindControl("TextBoxInvValueCarrier")
        TextInvoiceValueCarrier.Text = ""
        '***************************************************************************************
        ' It resets notes
        Dim TextNotes As TextBox = FormViewInvoice.FindControl("NotesTextBox")
        TextNotes.Text = ""
        '***************************************************************************************
        ' It resert po total
        Dim TextTotalpo As TextBox = FormViewInvoice.FindControl("TextBoxTotalPO")
        TextTotalpo.Text = ""
        '***************************************************************************************
        ' It resert po total invoiced
        Dim TextTotalinvoice As TextBox = FormViewInvoice.FindControl("TextBoxTotalInvoice")
        TextTotalinvoice.Text = ""
        LabelTotalInvoicePercent.Text = ""
        ImageTotalInvoiceProgress.Width = 0

        ' It resert outstanding
        Dim TextOutstanding As TextBox = FormViewInvoice.FindControl("TextBoxOutstanding")
        TextOutstanding.Text = ""
        LabelTotalOutstandingPercent.Text = ""
        ImageTotalOutstandingProgress.Width = 0

        '***************************************************************************************
        ' Focus on Drp PO Selector
        Dim DrpPoSelector As DropDownList = FormViewInvoice.FindControl("DropDownListPOselector")
        DrpPoSelector.Focus()
        '***************************************************************************************

        ' it enables active controls
        Dim X01 As TextBox = FormViewInvoice.FindControl("Invoice_NoTextBox")
        Dim X02 As TextBox = FormViewInvoice.FindControl("Invoice_DateTransferTextBox")
        Dim X03 As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")
        Dim X04 As TextBox = FormViewInvoice.FindControl("NotesTextBox")
        'Dim X05 As TextBox = FormViewInvoice.FindControl("DeadlineToPayTransferTextBox")
        Dim X06 As GridView = FormViewInvoice.FindControl("GridViewInvoiceHistoryperPO")

        X01.Enabled = False
        X02.Enabled = False
        X03.Enabled = False
        X04.Enabled = False
        'X05.Enabled = False
        X06.Enabled = False

        ' Show or Hide Invoice PDF control
        If PTS.CoreTables.CreateDataReader.Create_Table1_Project(sender.SelectedValue).PR_Coverpage_Approval_Compulsory = True Then
            ShowOrHidePDFcontrols(True)
        ElseIf PTS.CoreTables.CreateDataReader.Create_Table1_Project(sender.SelectedValue).PR_Coverpage_Approval_Compulsory = False Then
            ShowOrHidePDFcontrols(False)
        End If

    End Sub

    Protected Sub ShowOrHidePDFcontrols(ByVal _Show As Boolean)

        Dim LabelInvoicePDF As Label = FormViewInvoice.FindControl("LabelInvoicePDF")
        Dim FileUploadPDF As FileUpload = FormViewInvoice.FindControl("FileUploadPDF")
        Dim ButtonUploadPDF As LinkButton = FormViewInvoice.FindControl("ButtonUploadPDF")
        Dim RequiredFieldValidatorPDF As RequiredFieldValidator = FormViewInvoice.FindControl("RequiredFieldValidatorPDF")
        Dim TextBoxLinkToInvoicePDF As TextBox = FormViewInvoice.FindControl("TextBoxLinkToInvoicePDF")
        Dim ButtonWhatIsThis As LinkButton = FormViewInvoice.FindControl("ButtonWhatIsThis")

        TextBoxLinkToInvoicePDF.Text = String.Empty

        If _Show = True Then
            LabelInvoicePDF.Visible = True
            FileUploadPDF.Visible = True
            ButtonUploadPDF.Visible = True
            RequiredFieldValidatorPDF.Visible = True
            TextBoxLinkToInvoicePDF.Visible = True
            ButtonWhatIsThis.Visible = True

        ElseIf _Show = False Then
            LabelInvoicePDF.Visible = False
            FileUploadPDF.Visible = False
            ButtonUploadPDF.Visible = False
            RequiredFieldValidatorPDF.Visible = False
            TextBoxLinkToInvoicePDF.Visible = False
            ButtonWhatIsThis.Visible = False

        End If
    End Sub

    Protected Sub DropDownListPOselector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '***************************************************************************************
        'It feeds POsum+TotalInvoice+Outstanding textboxes
        '***************************************************************************************
        Dim GrdPOhistory As GridView = FormViewInvoice.FindControl("GridViewInvoiceHistoryperPO")
        Dim DrpPOSumHolder As DropDownList = FormViewInvoice.FindControl("DropDownListPOSumHolder")
        Dim TxtBoxPOSum As TextBox = FormViewInvoice.FindControl("TextBoxTotalPO")
        Dim DrpTotInvoiceHolder As DropDownList = FormViewInvoice.FindControl("DropDownListTotalInvoiceHolder")
        Dim TxtBoxInvoiceTotal As TextBox = FormViewInvoice.FindControl("TextBoxTotalInvoice")
        Dim TxtBoxOutstanding As TextBox = FormViewInvoice.FindControl("TextBoxOutstanding")
        Dim DrpToOutstanding As DropDownList = FormViewInvoice.FindControl("DropDownListOutstanding")
        Dim DrpINN As DropDownList = FormViewInvoice.FindControl("DropDownListINNCarrier")

        ' variable to work with progress bar and percentage
        Dim LabelTotalInvoicePercent As Label = FormViewInvoice.FindControl("LabelTotalInvoicePercent")
        Dim LabelTotalOutstandingPercent As Label = FormViewInvoice.FindControl("LabelTotalOutstandingPercent")
        Dim ImageTotalInvoiceProgress As Image = FormViewInvoice.FindControl("ImageTotalInvoiceProgress")
        Dim ImageTotalOutstandingProgress As Image = FormViewInvoice.FindControl("ImageTotalOutstandingProgress")



        Dim X01 As TextBox = FormViewInvoice.FindControl("Invoice_NoTextBox")
        Dim X02 As TextBox = FormViewInvoice.FindControl("Invoice_DateTransferTextBox")
        Dim X03 As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")
        Dim X04 As TextBox = FormViewInvoice.FindControl("NotesTextBox")
        'Dim X05 As TextBox = FormViewInvoice.FindControl("DeadlineToPayTransferTextBox")
        Dim X06 As GridView = FormViewInvoice.FindControl("GridViewInvoiceHistoryperPO")

        'Evaluates the Outstanding 
        DrpToOutstanding.DataBind()

        ' LabelDoublicateInvoiceWarning to be invisible
        Dim LblDblWrn As Label = FormViewInvoice.FindControl("LabelDoublicateInvoiceWarning")
        LblDblWrn.Visible = False

        If Convert.ToDecimal(DrpToOutstanding.SelectedValue) > 0 Then

            ' it enables active controls
            X01.Enabled = True
            X02.Enabled = True
            X03.Enabled = True
            X04.Enabled = True
            '   X05.Enabled = True
            X06.Enabled = True

            'Invoice history run
            GrdPOhistory.DataBind()
            'POSum restored in Dropdownlist as Value
            DrpPOSumHolder.DataBind()
            'POSum value transfered from Dropdownlist into TextBox
            TxtBoxPOSum.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DrpPOSumHolder.SelectedValue))

            'Total Invoice restored in Dropdownlist as Value
            DrpTotInvoiceHolder.DataBind()
            'Total Invoice value transfered from Dropdownlist into TextBox
            TxtBoxInvoiceTotal.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DrpTotInvoiceHolder.SelectedValue))
            LabelTotalInvoicePercent.Text = " " + Math.Round((Convert.ToDecimal(DrpTotInvoiceHolder.SelectedValue) / Convert.ToDecimal(DrpPOSumHolder.SelectedValue) * 100), 2).ToString + " %"
            ImageTotalInvoiceProgress.Width = Math.Round(Convert.ToDecimal(DrpTotInvoiceHolder.SelectedValue) / Convert.ToDecimal(DrpPOSumHolder.SelectedValue) * 100, 2)

            'Evaluates the Outstanding 
            'TxtBoxOutstanding.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DrpToOutstanding.SelectedValue))
            TxtBoxOutstanding.Text = DrpToOutstanding.SelectedValue
            LabelTotalOutstandingPercent.Text = " " + Math.Round((Convert.ToDecimal(DrpToOutstanding.SelectedValue) / Convert.ToDecimal(DrpPOSumHolder.SelectedValue) * 100), 2).ToString + " %"
            ImageTotalOutstandingProgress.Width = Math.Round(Convert.ToDecimal(DrpToOutstanding.SelectedValue) / Convert.ToDecimal(DrpPOSumHolder.SelectedValue) * 100, 2)
            ' INN number drp runs
            DrpINN.DataBind()
            '***************************************************************************************
            '***************************************************************************************

            ' It resets invoice no
            Dim TextInvoiceno As TextBox = FormViewInvoice.FindControl("Invoice_NoTextBox")
            TextInvoiceno.Text = ""
            '***************************************************************************************
            ' It resets invoice date
            Dim TextInvoiceDateTransfer As TextBox = FormViewInvoice.FindControl("Invoice_DateTransferTextBox")
            TextInvoiceDateTransfer.Text = ""
            '***************************************************************************************
            ' It resets Invoice Date PostBack Value
            'Dim DateTextBoxInvoice2 As TextBox = FormViewInvoice.FindControl("Invoice_DateTextBoxPostBack")
            'DateTextBoxInvoice2.Text = ""
            '***************************************************************************************
            ' It resets invoice value
            Dim TextInvoiceValue As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")
            TextInvoiceValue.Text = ""
            '***************************************************************************************
            ' It resets invoice value carrier
            Dim TextInvoiceValueCarrier As TextBox = FormViewInvoice.FindControl("TextBoxInvValueCarrier")
            TextInvoiceValueCarrier.Text = ""
            '***************************************************************************************
            ' It resets notes
            Dim TextNotes As TextBox = FormViewInvoice.FindControl("NotesTextBox")
            TextNotes.Text = ""
            '***************************************************************************************

            X01.Focus()

        Else

            ' it enables active controls
            X01.Enabled = False
            X02.Enabled = False
            X03.Enabled = False
            X04.Enabled = False
            'X05.Enabled = False
            X06.Enabled = False

            'Response.Write("<script type='text/javascript'>alert('PO closed! You can not define more invoce...');</script>")

            'Invoice history run
            GrdPOhistory.DataBind()
            'POSum restored in Dropdownlist as Value
            DrpPOSumHolder.DataBind()
            'POSum value transfered from Dropdownlist into TextBox
            TxtBoxPOSum.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DrpPOSumHolder.SelectedValue))

            'Total Invoice restored in Dropdownlist as Value
            DrpTotInvoiceHolder.DataBind()
            'Total Invoice value transfered from Dropdownlist into TextBox
            TxtBoxInvoiceTotal.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DrpTotInvoiceHolder.SelectedValue))
            LabelTotalInvoicePercent.Text = ""
            ImageTotalInvoiceProgress.Width = 0


            'Evaluates the Outstanding 
            'TxtBoxOutstanding.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(DrpToOutstanding.SelectedValue))
            TxtBoxOutstanding.Text = DrpToOutstanding.SelectedValue
            LabelTotalOutstandingPercent.Text = ""
            ImageTotalOutstandingProgress.Width = 0
            ' INN number drp runs
            DrpINN.DataBind()
            '***************************************************************************************
            '***************************************************************************************

            ' It resets invoice no
            Dim TextInvoiceno As TextBox = FormViewInvoice.FindControl("Invoice_NoTextBox")
            TextInvoiceno.Text = ""
            '***************************************************************************************
            ' It resets invoice date
            Dim TextInvoiceDateTransfer As TextBox = FormViewInvoice.FindControl("Invoice_DateTransferTextBox")
            TextInvoiceDateTransfer.Text = ""
            '***************************************************************************************
            ' It resets Invoice Date PostBack Value
            'Dim DateTextBoxInvoice2 As TextBox = FormViewInvoice.FindControl("Invoice_DateTextBoxPostBack")
            'DateTextBoxInvoice2.Text = ""
            '***************************************************************************************
            ' It resets invoice value
            Dim TextInvoiceValue As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")
            TextInvoiceValue.Text = ""
            '***************************************************************************************
            ' It resets notes
            Dim TextNotes As TextBox = FormViewInvoice.FindControl("NotesTextBox")
            TextNotes.Text = ""
            '***************************************************************************************
            ' focus on DrpPOSumHolder again
            Dim DrpPoSelect As DropDownList = FormViewInvoice.FindControl("DropDownListPOselector")
            DrpPoSelect.Focus()
        End If


    End Sub

    Protected Sub DropDownListPOselector_DataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        ' It resets "DropDownListPOselector" after DropDownListPrjID fires
        Dim DrpPoslctor As DropDownList = FormViewInvoice.FindControl("DropDownListPOselector")
        Dim controll As New String(Page.Request.Params.Get("__EVENTTARGET"))
        If (Not controll Is Nothing) Or (controll <> "") Then
            If controll = "ctl00$MainContent$FormViewInvoice$DropDownListPrjID" Then
                Dim lst As New ListItem("Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO |", "0")
                DrpPoslctor.Items.Insert(0, lst)
            End If
        End If

        'If previous page is nothing and if it is not postback then it resets dropdownposelector 
        If Not IsPostBack Then
            If PreviousPage Is Nothing Then
                Dim DrpPoslctor2 As DropDownList = FormViewInvoice.FindControl("DropDownListPOselector")
                Dim lst2 As New ListItem("Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO |", "0")
                DrpPoslctor2.Items.Insert(0, lst2)
            End If
        End If

        'If not coming from pocreate.aspx and if it is not postback then it resets dropdownposelector 
        If Not IsPostBack Then
            If Not PreviousPage Is Nothing Then
                Dim previousPageURL As String = Request.UrlReferrer.AbsolutePath.ToString
                Dim PageFileInfo As New System.IO.FileInfo(previousPageURL)
                If PageFileInfo.Name <> "pocreate.aspx" Then
                    Dim DrpPoslctor22 As DropDownList = FormViewInvoice.FindControl("DropDownListPOselector")
                    Dim lst22 As New ListItem("Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO | Select PO |", "0")
                    DrpPoslctor22.Items.Insert(0, lst22)
                End If
            End If
        End If

    End Sub

    Protected Sub DropDownListPrjID_DataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        'If previous page is nothing and if it is not postback then it resets dropdownProject 
        If Not IsPostBack Then
            If PreviousPage Is Nothing Then
                Dim DrpProject As DropDownList = FormViewInvoice.FindControl("DropDownListPrjID")
                Dim lst1 As New ListItem("Select Project ", "0")
                DrpProject.Items.Insert(0, lst1)
            End If
        End If

        'If not coming from pocreate.aspx and if it is not postback then it resets dropdownposelector 
        If Not IsPostBack Then
            If Not PreviousPage Is Nothing Then
                Dim previousPageURL As String = Request.UrlReferrer.AbsolutePath.ToString
                Dim PageFileInfo As New System.IO.FileInfo(previousPageURL)
                If PageFileInfo.Name <> "pocreate.aspx" Then
                    Dim DrpProjectt As DropDownList = FormViewInvoice.FindControl("DropDownListPrjID")
                    Dim lst11 As New ListItem("Select Project ", "0")
                    DrpProjectt.Items.Insert(0, lst11)
                End If
            End If
        End If

    End Sub

    Protected Sub InsertButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ' it defines UpdatedBy parameter as DateTime.Now
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        SqlDataSourceInvoice.InsertParameters("CreatedBy").DefaultValue = result.ToString
        SqlDataSourceInvoice.InsertParameters("CreatedBy").Type = TypeCode.DateTime

        SqlDataSourceInvoice.InsertParameters("PersonCreated").DefaultValue = Page.User.Identity.Name.ToString
        SqlDataSourceInvoice.InsertParameters("PersonCreated").Type = TypeCode.String
    End Sub

    Protected Sub FormViewInvoice_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormViewInvoice.ItemInserting

        Dim Invoice_DateTransferTextBox As TextBox = FormViewInvoice.FindControl("Invoice_DateTransferTextBox")
        Dim DropDownListPOselector As DropDownList = FormViewInvoice.FindControl("DropDownListPOselector")
        Dim InvoiceValueTextBox As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")

        ' check if invoice value validated against advance payment on contract.
        If (Convert.ToDecimal(InvoiceValueTextBox.Text) - ValidateInvoiceValueAgainstContractAdvance.GetAdvancePaymentValue(DropDownListPOselector.SelectedValue.ToString).Column1) > 0 Then
            e.Cancel = True

            Response.Redirect("~/webforms/invoicedefine.aspx")

            Exit Sub
        End If

        e.Values("Invoice_Date") = Convert.ToDateTime(Mid(Invoice_DateTransferTextBox.Text.ToString, 1, 2).ToString + "/" + Mid(Invoice_DateTransferTextBox.Text.ToString, 4, 2).ToString + "/" + Mid(Invoice_DateTransferTextBox.Text.ToString, 7, 4).ToString)
        e.Values("PO_No") = DropDownListPOselector.SelectedValue.ToString

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

    End Sub

    Protected Sub GridViewInvoiceDoublicate_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SqlDataSourceInvoiceDublicateCheck As SqlDataSource = FormViewInvoice.FindControl("SqlDataSourceInvoiceDublicateCheck")
        Dim Invoice_DateTransferTextBox As TextBox = FormViewInvoice.FindControl("Invoice_DateTransferTextBox")

        If Len(Invoice_DateTransferTextBox.Text.ToString) > 0 Then
            SqlDataSourceInvoiceDublicateCheck.SelectParameters("Invoice_Date").DefaultValue = Convert.ToDateTime(Mid(Invoice_DateTransferTextBox.Text.ToString, 1, 2).ToString + "/" + Mid(Invoice_DateTransferTextBox.Text.ToString, 4, 2).ToString + "/" + Mid(Invoice_DateTransferTextBox.Text.ToString, 7, 4).ToString)
            SqlDataSourceInvoiceDublicateCheck.SelectParameters("Invoice_Date").Type = TypeCode.DateTime
        Else
            SqlDataSourceInvoiceDublicateCheck.SelectParameters("Invoice_Date").DefaultValue = Convert.ToDateTime("23/07/1974")
            SqlDataSourceInvoiceDublicateCheck.SelectParameters("Invoice_Date").Type = TypeCode.DateTime
        End If

    End Sub

    Protected Sub GridViewInvoiceDoublicate_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextBoxDuplicateInvoiceRowCount As TextBox = FormViewInvoice.FindControl("TextBoxDuplicateInvoiceRowCount")
        Dim GridViewInvoiceDoublicate As GridView = FormViewInvoice.FindControl("GridViewInvoiceDoublicate")
        TextBoxDuplicateInvoiceRowCount.Text = GridViewInvoiceDoublicate.Rows.Count.ToString

        Dim LabelDoublicateInvoiceWarning As Label = FormViewInvoice.FindControl("LabelDoublicateInvoiceWarning")
        If GridViewInvoiceDoublicate.Rows.Count > 0 Then
            LabelDoublicateInvoiceWarning.Visible = True
        Else
            LabelDoublicateInvoiceWarning.Visible = False
        End If

    End Sub

    Protected Sub DropDownListPOselector_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DropDownListPOselector As DropDownList = FormViewInvoice.FindControl("DropDownListPOselector")
        Dim TextBoxPOselectorWordCount As TextBox = FormViewInvoice.FindControl("TextBoxPOselectorWordCount")
        TextBoxPOselectorWordCount.Text = DropDownListPOselector.SelectedValue.Count.ToString
    End Sub

    Protected Sub GridViewInvoiceDoublicate_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        If (e.CommandName = "OpenPdf") Then

            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)

        End If

    End Sub

    Protected Sub GridViewInvoiceDoublicate_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        'it defines type of PDF image if it exist or not.

        Dim GridViewInvoiceDoublicate As GridView = FormViewInvoice.FindControl("GridViewInvoiceDoublicate")

        If e.Row.RowType = DataControlRowType.DataRow Then

            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "AttachmentExist")) AndAlso DataBinder.Eval(e.Row.DataItem, "AttachmentExist") = True Then
                DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/pdf.bmp"
            Else
                DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/pdf_bw.bmp"
            End If

        End If

    End Sub

    Protected Sub Invoice_DateTransferTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ' it pass Outstanding value into Invoice Value for convinience
        Dim TInvoice_DateTransferTextBox As TextBox = FormViewInvoice.FindControl("Invoice_DateTransferTextBox")
        Dim TInvoice_NoTextBox As TextBox = FormViewInvoice.FindControl("Invoice_NoTextBox")
        Dim TxtBoxOutstanding7 As TextBox = FormViewInvoice.FindControl("TextBoxOutstanding")
        Dim TInvoiceValueTextBox As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")
        Dim NotesTextBox As TextBox = FormViewInvoice.FindControl("NotesTextBox")
        Dim DrpPOSumHolder1 As DropDownList = FormViewInvoice.FindControl("DropDownListPOSumHolder")

        If IsPostBack Then
            If TInvoice_DateTransferTextBox.Text <> "" And TInvoice_NoTextBox.Text <> "" And TInvoiceValueTextBox.Text = "" Then
                TInvoiceValueTextBox.Text = TxtBoxOutstanding7.Text
                ' Note section needs to be refreshed accordingly unless it is 100%
                If IsNumeric(TInvoiceValueTextBox.Text) Then
                    If Convert.ToDecimal(TInvoiceValueTextBox.Text) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) < 1 AndAlso Convert.ToDecimal(TInvoiceValueTextBox.Text) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) > 0 Then
                        NotesTextBox.Text = Math.Round(Convert.ToDecimal(TInvoiceValueTextBox.Text) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) * 100, 2).ToString + " % payment of total PO"
                    Else
                        NotesTextBox.Text = ""
                    End If
                End If

            End If
        End If
    End Sub

    Protected Sub Invoice_NoTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ' it pass Outstanding value into Invoice Value for convinience
        Dim TInvoice_DateTransferTextBox As TextBox = FormViewInvoice.FindControl("Invoice_DateTransferTextBox")
        Dim TInvoice_NoTextBox As TextBox = FormViewInvoice.FindControl("Invoice_NoTextBox")
        Dim TxtBoxOutstanding7 As TextBox = FormViewInvoice.FindControl("TextBoxOutstanding")
        Dim TInvoiceValueTextBox As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")
        Dim NotesTextBox As TextBox = FormViewInvoice.FindControl("NotesTextBox")
        Dim DrpPOSumHolder1 As DropDownList = FormViewInvoice.FindControl("DropDownListPOSumHolder")

        TInvoice_DateTransferTextBox.Focus()

        If IsPostBack Then
            If TInvoice_DateTransferTextBox.Text <> "" And TInvoice_NoTextBox.Text <> "" And TInvoiceValueTextBox.Text = "" Then
                TInvoiceValueTextBox.Text = TxtBoxOutstanding7.Text
                ' Note section needs to be refreshed accordingly unless it is 100%
                If IsNumeric(TInvoiceValueTextBox.Text) Then
                    If Convert.ToDecimal(TInvoiceValueTextBox.Text) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) < 1 AndAlso Convert.ToDecimal(TInvoiceValueTextBox.Text) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) > 0 Then
                        NotesTextBox.Text = Math.Round(Convert.ToDecimal(TInvoiceValueTextBox.Text) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) * 100, 2).ToString + " % payment of total PO"
                    Else
                        NotesTextBox.Text = ""
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub GridViewInvoiceDoublicate_Load(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub InvoiceValueTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ' it provides hint for user about invoice description
        Dim DrpPOSumHolder1 As DropDownList = FormViewInvoice.FindControl("DropDownListPOSumHolder")
        Dim DrpToOutstanding1 As DropDownList = FormViewInvoice.FindControl("DropDownListOutstanding")
        Dim LabelTotalInvoicePercent As Label = FormViewInvoice.FindControl("LabelTotalInvoicePercent")
        Dim LabelTotalOutstandingPercent As Label = FormViewInvoice.FindControl("LabelTotalOutstandingPercent")
        Dim InvoiceValueTextBox As TextBox = FormViewInvoice.FindControl("InvoiceValueTextBox")
        Dim NotesTextBox As TextBox = FormViewInvoice.FindControl("NotesTextBox")

        If IsNumeric(InvoiceValueTextBox.Text) Then
            If Convert.ToDecimal(InvoiceValueTextBox.Text) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) < 1 AndAlso Convert.ToDecimal(InvoiceValueTextBox.Text) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) > 0 Then
                NotesTextBox.Text = Math.Round(Convert.ToDecimal(InvoiceValueTextBox.Text) / Convert.ToDecimal(DrpPOSumHolder1.SelectedValue) * 100, 2).ToString + " % payment of total PO"
            Else
                NotesTextBox.Text = ""
            End If
        End If

    End Sub

    Protected Sub ButtonWhatIsThis_Click(sender As Object, e As EventArgs)

        ModalPopupExtenderPayReqNoHistory.Show()

    End Sub

    Protected Sub ButtonUploadPDF_Click(sender As Object, e As EventArgs)

        Dim FileToUpload As FileUpload = FormViewInvoice.FindControl("FileUploadPDF")
        Dim LabelInfo As Label = FormViewInvoice.FindControl("LabelInfoInvoice")
        Dim TextLink As TextBox = FormViewInvoice.FindControl("TextBoxLinkToInvoicePDF")

        If FileToUpload.HasFile Then
            If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) <> ".pdf" Then
                LabelInfo.ForeColor = System.Drawing.Color.Red
                LabelInfo.Text = "Please select PDF format file"
                TextLink.Text = ""
            Else
                If FileToUpload.PostedFile.ContentLength / 1000 > 1200 Then
                    ModalPopupExtender1.Show()
                    panEdit.CssClass = "PanelWarning"
                Else
                    Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                    FileToUpload.SaveAs(MapPath("~/REQUEST/_InvoicePDF/" + UniqueString1 + ".pdf"))
                    TextLink.Text = "~/REQUEST/_InvoicePDF/" + UniqueString1 + ".pdf"
                    LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                    LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"

                End If
            End If
        Else
            TextLink.Text = ""
            LabelInfo.ForeColor = System.Drawing.Color.Red
            LabelInfo.Text = "you did not specify any file"
        End If

    End Sub

End Class
