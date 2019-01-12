Imports System.Data
Imports System.IO
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Data.SqlClient
Imports PTS_App_Code_VB_Project.PTS.CoreTables

Partial Class ApprovalMatrixRev33422
    Inherits System.Web.UI.Page

    Dim Notification As New _GiveNotification
    Dim a As New MyCommonTasks

    Protected Function GetRankPosition(ByVal _rank As String) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " select RTRIM(positionName) from Table_Approval_PositionEmployee WHERE Rank = @Rank "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim Rank As SqlParameter = cmd.Parameters.Add("@Rank", System.Data.SqlDbType.Int)
            Rank.Value = Convert.ToInt32(_rank)

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _return As String = ""

            While dr.Read
                _return = dr(0)
            End While

            Return _return
            con.Close()
            dr.Close()
            con.Dispose()
        End Using

    End Function

    Protected Sub GridviewNotApprovedContractsOrAddendums_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridviewNotApprovedContractsOrAddendums.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            'If DataBinder.Eval(e.Row.DataItem, "ContractId") = 6030 Then
            '    e.Row.Enabled = False
            '    e.Row.Attributes.Add("style", "background-image: url('http://pts.mercuryeng.ru/images/temporarily_frozen.png');opacity: 0.4")
            'End If

        End If

        ' highlighting Frame Contracts
        Dim LiteralValueWithVAT As Literal = DirectCast(e.Row.FindControl("LiteralValueWithVAT"), Literal)
        Dim LiteralCurrency As Literal = DirectCast(e.Row.FindControl("LiteralCurrency"), Literal)
        If LiteralValueWithVAT IsNot Nothing And DataBinder.Eval(e.Row.DataItem, "Value_WithVAT") = 0 Then
            LiteralValueWithVAT.Text = "Frame Contract"
            LiteralCurrency.Text = String.Empty
        End If

        ' budget not provided
        'Dim LiteralBudget As Literal = DirectCast(e.Row.FindControl("LiteralBudget"), Literal)
        'If LiteralBudget IsNot Nothing And IsDBNull(DataBinder.Eval(e.Row.DataItem, "Budget")) Then
        '    LiteralBudget.Text = "not provided"
        'End If

        ' CANCELLED AFTER TRANSLATION
        ' Providing Header position names
        'If e.Row.RowType = DataControlRowType.Header Then
        '    For i = 5 To 16
        '        If IsNumeric(Replace(e.Row.Cells(i).Text, "UserRank", "")) Then
        '            e.Row.Cells(i).Text = GetRankPosition(Replace(e.Row.Cells(i).Text, "UserRank", ""))
        '        End If
        '    Next
        'End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            ' show or hide tables for performance issue
            Dim tblR1 As HtmlTable = DirectCast(e.Row.FindControl("tblR1"), HtmlTable)
            Dim tblR2 As HtmlTable = DirectCast(e.Row.FindControl("tblR2"), HtmlTable)
            Dim tblR3 As HtmlTable = DirectCast(e.Row.FindControl("tblR3"), HtmlTable)
            Dim tblR4 As HtmlTable = DirectCast(e.Row.FindControl("tblR4"), HtmlTable)
            Dim tblR5 As HtmlTable = DirectCast(e.Row.FindControl("tblR5"), HtmlTable)
            Dim tblR6 As HtmlTable = DirectCast(e.Row.FindControl("tblR6"), HtmlTable)
            Dim tblR7 As HtmlTable = DirectCast(e.Row.FindControl("tblR7"), HtmlTable)
            Dim tblR8 As HtmlTable = DirectCast(e.Row.FindControl("tblR8"), HtmlTable)

            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "UserRank1")) Then tblR1.Visible = False
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "UserRank2")) Then tblR2.Visible = False
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "UserRank3")) Then tblR3.Visible = False
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "UserRank4")) Then tblR4.Visible = False
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "UserRank5")) Then tblR5.Visible = False
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "UserRank6")) Then tblR6.Visible = False
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "UserRank7")) Then tblR7.Visible = False
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "UserRank8")) Then tblR8.Visible = False

            'Dim SqlDataSourceApprovalStatus As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceApprovalStatus"), SqlDataSource)
            'SqlDataSourceApprovalStatus.SelectParameters("ContractID").DefaultValue = DataBinder.Eval(e.Row.DataItem, "ContractID")

            ' provide Hyperlink navigation URL
            Dim HyperlinkLink As HyperLink = DirectCast(e.Row.FindControl("HyperlinkLink"), HyperLink)
            If DataBinder.Eval(e.Row.DataItem, "AddendumID") > 0 Then
                ' it is addendum
                HyperlinkLink.NavigateUrl = "~/webforms/AddendumDetails.aspx?AddendumId=" + DataBinder.Eval(e.Row.DataItem, "AddendumID").ToString
                HyperlinkLink.Text = BodyTexts.Ref("RLtVwWgwTkK3dqhLx0hXBg")
            Else
                ' it is contract
                HyperlinkLink.NavigateUrl = "~/webforms/ContractDetails.aspx?ContractId=" + DataBinder.Eval(e.Row.DataItem, "ContractID").ToString
                HyperlinkLink.Text = BodyTexts.Ref("PPrzvFFD5UWwcTtESrll1g")
            End If

            ' Highlighting First cell of addendum lines
            If DataBinder.Eval(e.Row.DataItem, "AddendumID") > 0 Then
                e.Row.Cells(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#F4F6F6")
            End If

            '' Provide SqlDataSource select parameter for GridviewAddendum
            'Dim SqlDataSourceAddendums As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceAddendums"), SqlDataSource)
            'SqlDataSourceAddendums.SelectParameters("ContractID").DefaultValue = DataBinder.Eval(e.Row.DataItem, "ContractID")

            ' provide pending day
            Dim LiteralCreatedBy As Literal = DirectCast(e.Row.FindControl("LiteralCreatedBy"), Literal)
            Dim _today As DateTime = DateTime.Today
            Dim _createdBy As DateTime = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "CreatedBy"))
            If Math.Truncate(_today.Subtract(_createdBy).TotalDays + 1) = 0 Then
                LiteralCreatedBy.Text = "today"
            Else
                LiteralCreatedBy.Text = Math.Truncate(_today.Subtract(_createdBy).TotalDays + 1).ToString + BodyTexts.Ref("GzOtW12sHkSt+9pzu7JMDQ")
            End If

            ' Providing Images to ImageButtons
            Dim LiteralUserRank1 As Literal = DirectCast(e.Row.FindControl("LiteralUserRank1"), Literal)
            Dim LiteralUserRank2 As Literal = DirectCast(e.Row.FindControl("LiteralUserRank2"), Literal)
            Dim LiteralUserRank3 As Literal = DirectCast(e.Row.FindControl("LiteralUserRank3"), Literal)
            Dim LiteralUserRank4 As Literal = DirectCast(e.Row.FindControl("LiteralUserRank4"), Literal)
            Dim LiteralUserRank5 As Literal = DirectCast(e.Row.FindControl("LiteralUserRank5"), Literal)
            Dim LiteralUserRank6 As Literal = DirectCast(e.Row.FindControl("LiteralUserRank6"), Literal)
            Dim LiteralUserRank7 As Literal = DirectCast(e.Row.FindControl("LiteralUserRank7"), Literal)
            Dim LiteralUserRank8 As Literal = DirectCast(e.Row.FindControl("LiteralUserRank8"), Literal)

            Dim ImageButtonApprovalUserRank1 As ImageButton = DirectCast(e.Row.FindControl("ImageButtonApprovalUserRank1"), ImageButton)
            Dim ImageButtonApprovalUserRank2 As ImageButton = DirectCast(e.Row.FindControl("ImageButtonApprovalUserRank2"), ImageButton)
            Dim ImageButtonApprovalUserRank3 As ImageButton = DirectCast(e.Row.FindControl("ImageButtonApprovalUserRank3"), ImageButton)
            Dim ImageButtonApprovalUserRank4 As ImageButton = DirectCast(e.Row.FindControl("ImageButtonApprovalUserRank4"), ImageButton)
            Dim ImageButtonApprovalUserRank5 As ImageButton = DirectCast(e.Row.FindControl("ImageButtonApprovalUserRank5"), ImageButton)
            Dim ImageButtonApprovalUserRank6 As ImageButton = DirectCast(e.Row.FindControl("ImageButtonApprovalUserRank6"), ImageButton)
            Dim ImageButtonApprovalUserRank7 As ImageButton = DirectCast(e.Row.FindControl("ImageButtonApprovalUserRank7"), ImageButton)
            Dim ImageButtonApprovalUserRank8 As ImageButton = DirectCast(e.Row.FindControl("ImageButtonApprovalUserRank8"), ImageButton)

            Dim LiteralWhenApprovedUserRank1 As Literal = DirectCast(e.Row.FindControl("LiteralWhenApprovedUserRank1"), Literal)
            Dim LiteralWhenApprovedUserRank2 As Literal = DirectCast(e.Row.FindControl("LiteralWhenApprovedUserRank2"), Literal)
            Dim LiteralWhenApprovedUserRank3 As Literal = DirectCast(e.Row.FindControl("LiteralWhenApprovedUserRank3"), Literal)
            Dim LiteralWhenApprovedUserRank4 As Literal = DirectCast(e.Row.FindControl("LiteralWhenApprovedUserRank4"), Literal)
            Dim LiteralWhenApprovedUserRank5 As Literal = DirectCast(e.Row.FindControl("LiteralWhenApprovedUserRank5"), Literal)
            Dim LiteralWhenApprovedUserRank6 As Literal = DirectCast(e.Row.FindControl("LiteralWhenApprovedUserRank6"), Literal)
            Dim LiteralWhenApprovedUserRank7 As Literal = DirectCast(e.Row.FindControl("LiteralWhenApprovedUserRank7"), Literal)
            Dim LiteralWhenApprovedUserRank8 As Literal = DirectCast(e.Row.FindControl("LiteralWhenApprovedUserRank8"), Literal)

            Dim LinkButtonRejectContractGirls As LinkButton = DirectCast(e.Row.FindControl("LinkButtonRejectContractGirls"), LinkButton)

            Dim ContractID As Int32 = DataBinder.Eval(e.Row.DataItem, "ContractID")
            Dim AddendumID As Int32 = DataBinder.Eval(e.Row.DataItem, "AddendumID")
            Dim POexecuted As Int32 = DataBinder.Eval(e.Row.DataItem, "POexecuted")
            Dim Exceptional As Int32 = DataBinder.Eval(e.Row.DataItem, "Exceptional")

            ' Check user approved or not, then provide image URL
            AssignValuesToControls(ContractID, AddendumID, LiteralUserRank1, ImageButtonApprovalUserRank1, LiteralWhenApprovedUserRank1, Nothing)
            AssignValuesToControls(ContractID, AddendumID, LiteralUserRank2, ImageButtonApprovalUserRank2, LiteralWhenApprovedUserRank2, Nothing)
            AssignValuesToControls(ContractID, AddendumID, LiteralUserRank3, ImageButtonApprovalUserRank3, LiteralWhenApprovedUserRank3, LinkButtonRejectContractGirls)
            AssignValuesToControls(ContractID, AddendumID, LiteralUserRank4, ImageButtonApprovalUserRank4, LiteralWhenApprovedUserRank4, Nothing)
            AssignValuesToControls(ContractID, AddendumID, LiteralUserRank5, ImageButtonApprovalUserRank5, LiteralWhenApprovedUserRank5, Nothing)
            AssignValuesToControls(ContractID, AddendumID, LiteralUserRank6, ImageButtonApprovalUserRank6, LiteralWhenApprovedUserRank6, Nothing)
            AssignValuesToControls(ContractID, AddendumID, LiteralUserRank7, ImageButtonApprovalUserRank7, LiteralWhenApprovedUserRank7, Nothing)
            AssignValuesToControls(ContractID, AddendumID, LiteralUserRank8, ImageButtonApprovalUserRank8, LiteralWhenApprovedUserRank8, Nothing)

            ' Provide Project Logo for Images
            Dim ImageLogo As Image = DirectCast(e.Row.FindControl("ImageLogo"), Image)
            ImageLogo.ImageUrl = CreateDataReader.Create_Table1_Project(CreateDataReader.Create_Table_Contract(DataBinder.Eval(e.Row.DataItem, "ContractID")).ProjectID).Logo


            ' Disable contract and addendum if Po executed without any exception
            If DataBinder.Eval(e.Row.DataItem, "ContractID") > 0 And DataBinder.Eval(e.Row.DataItem, "AddendumID") = 0 Then
                ' it is contract
                If CreateDataReader.Create_Table_Contract(DataBinder.Eval(e.Row.DataItem, "ContractID")).POexecuted = True And
                    CreateDataReader.Create_Table_Contract(DataBinder.Eval(e.Row.DataItem, "ContractID")).Exceptional = False Then
                    ' Disable controls to approval
                    For i = 1 To 8
                        e.Row.Cells(i).Enabled = False
                    Next
                End If
            End If

            If DataBinder.Eval(e.Row.DataItem, "ContractID") > 0 And DataBinder.Eval(e.Row.DataItem, "AddendumID") > 0 Then
                ' it is addendum
                If CreateDataReader.Create_Table_Addendums(DataBinder.Eval(e.Row.DataItem, "AddendumID")).POexecuted = True And
                    CreateDataReader.Create_Table_Addendums(DataBinder.Eval(e.Row.DataItem, "AddendumID")).Exceptional = False Then
                    ' Disable controls to approval
                    For i = 1 To 8
                        e.Row.Cells(i).Enabled = False
                    Next
                End If
            End If

            ' Provide Hyperlink to PO if exist
            If DataBinder.Eval(e.Row.DataItem, "ContractID") > 0 And DataBinder.Eval(e.Row.DataItem, "AddendumID") = 0 Then
                If CreateDataReader.Create_Table_Contract(DataBinder.Eval(e.Row.DataItem, "ContractID")).POexecuted = True Then
                    Dim PO As String = CreateDataReader.Create_Table_Contract(DataBinder.Eval(e.Row.DataItem, "ContractID")).PO_No
                    Dim HyperlinkPO As HyperLink = DirectCast(e.Row.FindControl("HyperlinkPO"), HyperLink)
                    HyperlinkPO.Text = PO
                    HyperlinkPO.NavigateUrl = "~/webforms/PO.aspx?po=" + PO
                    HyperlinkPO.BackColor = System.Drawing.Color.Yellow
                End If
            End If

            ' display Add Comment controls
            Dim LinkButtonRemarkUserRank1 As LinkButton = DirectCast(e.Row.FindControl("LinkButtonRemarkUserRank1"), LinkButton)
            Dim LinkButtonRemarkUserRank2 As LinkButton = DirectCast(e.Row.FindControl("LinkButtonRemarkUserRank2"), LinkButton)
            Dim LinkButtonRemarkUserRank3 As LinkButton = DirectCast(e.Row.FindControl("LinkButtonRemarkUserRank3"), LinkButton)
            Dim LinkButtonRemarkUserRank4 As LinkButton = DirectCast(e.Row.FindControl("LinkButtonRemarkUserRank4"), LinkButton)
            Dim LinkButtonRemarkUserRank5 As LinkButton = DirectCast(e.Row.FindControl("LinkButtonRemarkUserRank5"), LinkButton)
            Dim LinkButtonRemarkUserRank6 As LinkButton = DirectCast(e.Row.FindControl("LinkButtonRemarkUserRank6"), LinkButton)
            Dim LinkButtonRemarkUserRank7 As LinkButton = DirectCast(e.Row.FindControl("LinkButtonRemarkUserRank7"), LinkButton)
            Dim LinkButtonRemarkUserRank8 As LinkButton = DirectCast(e.Row.FindControl("LinkButtonRemarkUserRank8"), LinkButton)

            DisplayAddCommentControls(ContractID, AddendumID, LiteralUserRank1, DataBinder.Eval(e.Row.DataItem, "CountUnreadComments_UR_1"), LinkButtonRemarkUserRank1, POexecuted, Exceptional)
            DisplayAddCommentControls(ContractID, AddendumID, LiteralUserRank2, DataBinder.Eval(e.Row.DataItem, "CountUnreadComments_UR_2"), LinkButtonRemarkUserRank2, POexecuted, Exceptional)
            DisplayAddCommentControls(ContractID, AddendumID, LiteralUserRank3, DataBinder.Eval(e.Row.DataItem, "CountUnreadComments_UR_3"), LinkButtonRemarkUserRank3, POexecuted, Exceptional)
            DisplayAddCommentControls(ContractID, AddendumID, LiteralUserRank4, DataBinder.Eval(e.Row.DataItem, "CountUnreadComments_UR_4"), LinkButtonRemarkUserRank4, POexecuted, Exceptional)
            DisplayAddCommentControls(ContractID, AddendumID, LiteralUserRank5, DataBinder.Eval(e.Row.DataItem, "CountUnreadComments_UR_5"), LinkButtonRemarkUserRank5, POexecuted, Exceptional)
            DisplayAddCommentControls(ContractID, AddendumID, LiteralUserRank6, DataBinder.Eval(e.Row.DataItem, "CountUnreadComments_UR_6"), LinkButtonRemarkUserRank6, POexecuted, Exceptional)
            DisplayAddCommentControls(ContractID, AddendumID, LiteralUserRank7, DataBinder.Eval(e.Row.DataItem, "CountUnreadComments_UR_7"), LinkButtonRemarkUserRank7, POexecuted, Exceptional)
            DisplayAddCommentControls(ContractID, AddendumID, LiteralUserRank8, DataBinder.Eval(e.Row.DataItem, "CountUnreadComments_UR_8"), LinkButtonRemarkUserRank8, POexecuted, Exceptional)

            'Enable Or Disable REJECT > based on comment existense. 
            'Be careful on "see comment" and "add comment" text, otherwise it will not work
            If LinkButtonRemarkUserRank3.Text.ToLower = "see comment" Then
                If Page.User.IsInRole("ContractLeadGirls") Then
                    LinkButtonRejectContractGirls.Enabled = True
                Else
                    LinkButtonRejectContractGirls.Enabled = False
                End If
            ElseIf LinkButtonRemarkUserRank3.Text.ToLower = "add comment" Then
                If Page.User.IsInRole("ContractLeadGirls") Then
                    LinkButtonRejectContractGirls.Enabled = False
                Else
                    LinkButtonRejectContractGirls.Visible = False
                End If
            End If

            ' Show New Supplier Warning
            If DataBinder.Eval(e.Row.DataItem, "ContractID") > 0 And DataBinder.Eval(e.Row.DataItem, "AddendumID") = 0 Then
                Dim SupplierID As String = PTS.CoreTables.CreateDataReader.Create_Table_Contract(DataBinder.Eval(e.Row.DataItem, "ContractID")).SupplierID
                Dim ProjectID As Integer = PTS.CoreTables.CreateDataReader.Create_Table_Contract(DataBinder.Eval(e.Row.DataItem, "ContractID")).ProjectID

                Dim panelNewSupplier As Panel = DirectCast(e.Row.FindControl("panelNewSupplier"), Panel)

                Using adapter As New ApprovalMatrixTableAdapters.QueriesTableAdapter

                    If adapter.GetTotalPoNumberUnderThisSupplier(SupplierID) = 0 And ProjectID <> 999 Then
                        panelNewSupplier.Visible = True
                    Else
                        panelNewSupplier.Visible = False
                    End If

                    adapter.Dispose()

                End Using

            End If

        End If

        ' add here REPLACE ADDENDUM logic
        Dim LiteralReplaceBlock As Literal = DirectCast(e.Row.FindControl("LiteralReplaceBlock"), Literal)
        If LiteralReplaceBlock IsNot Nothing Then
            Dim _AddendumID_ As Integer = DataBinder.Eval(e.Row.DataItem, "AddendumID")
            If PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID_).AddendumTypes = 2 Then
                If PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID_).POexecuted = False Then
                    If PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID_).AddendumValue_WithVAT <
                        ReplaceAddendumCheck.GetTotalInvoice_WithVAT_AgainstPO(PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_AddendumID_).ContractID).PO_No) Then
                        e.Row.Enabled = False
                        LiteralReplaceBlock.Text = "<h5>THIS REPLACE ADDENDUM CANNOT BE PROCEED.<br/>IT EXCEEDS TOTAL INVOICED VALUE UNDER " +
                            PTS.CoreTables.CreateDataReader.Create_Table_Contract(DataBinder.Eval(e.Row.DataItem, "ContractID")).PO_No + "</h5>"
                    Else
                        LiteralReplaceBlock.Text = ""
                    End If
                End If
            Else
                LiteralReplaceBlock.Text = ""
            End If
        End If

        ' Hide Mercury Office contracts for unnecessary people
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim prjid As Int32 = 175
        '    If DataBinder.Eval(e.Row.DataItem, "ProjectID") = prjid Then
        '        Dim _UserOfficeContractsPositionId As Integer = DataBinder.Eval(e.Row.DataItem, "UserOfficeContractsPositionId")

        '        If _UserOfficeContractsPositionId > 0 Then
        '            If Not (_UserOfficeContractsPositionId = 4 Or _UserOfficeContractsPositionId = 5) Then
        '                e.Row.Visible = False
        '            End If
        '        Else
        '            If Not (Roles.IsUserInRole("ContractLeadGirls") = True Or Page.User.Identity.Name.ToLower = "savas") Then
        '                e.Row.Visible = True
        '            End If
        '        End If
        '    End If
        'End If

    End Sub

    Protected Function ShowOrHideRemarkEntry(userName As String, pageUserName As String) As String

        If (userName = pageUserName) Then
            Return "true"
        End If

        If (userName = "lawyers" And Roles.IsUserInRole("ContractLeadGirls") = True) Then
            Return "true"
        End If

        Return "false"

    End Function

    Protected Sub DisplayAddCommentControls(ByVal _ContractID As Integer,
                                             ByVal _AddendumID As Integer,
                                             ByVal _LiteralUserRank As Literal,
                                             ByVal _CountUnreadComments As Integer,
                                             ByVal _LinkButtonRemarkUserRank As LinkButton,
                                             Optional POexecuted As Int32 = 0,
                                             Optional Exceptional As Int32 = 0)

        If _LiteralUserRank.Text.Trim().Length = 0 Then
            Exit Sub
        End If

        Dim _CurrentUser As String = ""

        If Roles.IsUserInRole("ContractLeadGirls") = True Then
            _CurrentUser = NameOfLeadLawyer.GetNameFromFunction
        Else
            _CurrentUser = Page.User.Identity.Name.ToString.ToLower
        End If

        If _LiteralUserRank.Text.ToLower = "lawyers" Then
            _LiteralUserRank.Text = NameOfLeadLawyer.GetNameFromFunction
        End If

        If String.IsNullOrEmpty(_LiteralUserRank.Text) Then
            _LinkButtonRemarkUserRank.Visible = False
        End If

        If _ContractID > 0 And _AddendumID = 0 Then
            ' Contract

            'Dim _C_or_A As String = ""
            'Dim FromWho As String = If(_LiteralUserRank.Text = NameOfLeadLawyer.GetNameFromFunction, "lawyers", _LiteralUserRank.Text)
            'Dim ForWho As String = If(Roles.IsUserInRole("ContractLeadGirls") = True, "lawyers", Page.User.Identity.Name.ToString.ToLower)
            'Dim Cnt As Integer = 0
            'Dim CountOfUnreadComment As String = ""

            'Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
            '    _C_or_A = "C"
            '    Dim _Count = Aggregate C In db.Notf_ContractCommentsUnRead Where C.C_or_A = _C_or_A And C.C_or_A_id = _ContractID And C.ForWho = ForWho And C.FromWho = FromWho Into Count()
            '    Cnt = _Count
            '    CountOfUnreadComment = _Count.ToString
            'End Using

            Dim ContractExecuted As Boolean = If(POexecuted = 1 And Exceptional = 0, True, False)

            'Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            '    Dim _executed = (From C In db.Table_Contracts Where C.ContractID = _ContractID Select New With {.POexecuted = C.POexecuted, .Exceptional = C.Exceptional}).ToList()(0)

            '    ContractExecuted = If(_executed.POexecuted = True And _executed.Exceptional = False, True, False)

            'End Using

            If ContractRemarksFromUser.GetDataByContractUser(_ContractID, _LiteralUserRank.Text) > 0 Then
                'Be careful on "see comment" and "add comment" text, otherwise REJECT button wont be disabled
                If Roles.IsUserInRole("ContractLeadGirls") = True Then
                    If _LiteralUserRank.Text <> "lawyers" Then
                        If _CountUnreadComments = 0 Then
                            _LinkButtonRemarkUserRank.Text = "See Comment"
                            _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksContract').modal('show'); global.Contracts_UserRemarkListModel.getComments(" + _ContractID.ToString + "); global.Contracts_UserRemarkModel.CreateMe('0', '" + _ContractID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                            _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.Red
                        Else
                            If ContractExecuted Then
                                _LinkButtonRemarkUserRank.Text = "See Comment"
                                _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksContract').modal('show'); global.Contracts_UserRemarkListModel.getComments(" + _ContractID.ToString + "); global.Contracts_UserRemarkModel.CreateMe('0', '" + _ContractID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                                _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.Green
                            Else
                                _LinkButtonRemarkUserRank.Text = "<span style=" + """" + "color:red;" + """" + ">See Comment</span><span class=" + """" + "LabelNotification" + """" + ">" + _CountUnreadComments.ToString + "</span>"
                                _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksContract').modal('show'); global.Contracts_UserRemarkListModel.getComments(" + _ContractID.ToString + "); global.Contracts_UserRemarkModel.CreateMe('0', '" + _ContractID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                            End If
                        End If
                    Else
                        _LinkButtonRemarkUserRank.Text = "See Comment"
                        _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksContract').modal('show'); global.Contracts_UserRemarkListModel.getComments(" + _ContractID.ToString + "); global.Contracts_UserRemarkModel.CreateMe('0', '" + _ContractID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                        _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.Red
                    End If
                Else
                    If _LiteralUserRank.Text <> Page.User.Identity.Name.ToString Then
                        If _CountUnreadComments = 0 Then
                            _LinkButtonRemarkUserRank.Text = "See Comment"
                            _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksContract').modal('show'); global.Contracts_UserRemarkListModel.getComments(" + _ContractID.ToString + "); global.Contracts_UserRemarkModel.CreateMe('0', '" + _ContractID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                            _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.Red
                        Else
                            If ContractExecuted Then
                                _LinkButtonRemarkUserRank.Text = "See Comment"
                                _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksContract').modal('show'); global.Contracts_UserRemarkListModel.getComments(" + _ContractID.ToString + "); global.Contracts_UserRemarkModel.CreateMe('0', '" + _ContractID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                                _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.Green
                            Else
                                _LinkButtonRemarkUserRank.Text = "<span style=" + """" + "color:red;" + """" + ">See Comment</span><span class=" + """" + "LabelNotification" + """" + ">" + _CountUnreadComments.ToString + "</span>"
                                _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksContract').modal('show'); global.Contracts_UserRemarkListModel.getComments(" + _ContractID.ToString + "); global.Contracts_UserRemarkModel.CreateMe('0', '" + _ContractID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                            End If
                        End If

                    Else
                        _LinkButtonRemarkUserRank.Text = "See Comment"
                        _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksContract').modal('show'); global.Contracts_UserRemarkListModel.getComments(" + _ContractID.ToString + "); global.Contracts_UserRemarkModel.CreateMe('0', '" + _ContractID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                        _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.Red
                    End If
                End If
            Else
                If _LiteralUserRank.Text.ToString.ToLower = _CurrentUser Then
                    'Be careful on "see comment" and "add comment" text, otherwise REJECT button wont be disabled
                    _LinkButtonRemarkUserRank.Text = "Add Comment"
                    _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksContract').modal('show'); global.Contracts_UserRemarkListModel.getComments(" + _ContractID.ToString + "); global.Contracts_UserRemarkModel.CreateMe('0', '" + _ContractID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                    _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.LightSkyBlue
                Else
                    _LinkButtonRemarkUserRank.Visible = False
                End If
            End If

        ElseIf _ContractID > 0 And _AddendumID > 0 Then
            ' Addendum

            'Dim _C_or_A As String = ""
            'Dim FromWho As String = If(_LiteralUserRank.Text = NameOfLeadLawyer.GetNameFromFunction, "lawyers", _LiteralUserRank.Text)
            'Dim ForWho As String = If(Roles.IsUserInRole("ContractLeadGirls") = True, "lawyers", Page.User.Identity.Name.ToString.ToLower)
            'Dim CountOfUnreadComment As String = ""

            'Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
            '    _C_or_A = "A"
            '    Dim _Count = Aggregate C In db.Notf_AddendumCommentsUnRead Where C.C_or_A = _C_or_A And C.C_or_A_id = _AddendumID And C.ForWho = ForWho And C.FromWho = FromWho Into Count()
            '    CountOfUnreadComment = _Count.ToString
            'End Using

            Dim AddendumExecuted As Boolean = If(POexecuted = 1 And Exceptional = 0, True, False)

            'Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            '    Dim _executed = (From C In db.Table_Addendums Where C.AddendumID = _AddendumID Select New With {.POexecuted = C.POexecuted, .Exceptional = C.Exceptional}).ToList()(0)

            '    AddendumExecuted = If(_executed.POexecuted = True And _executed.Exceptional = False, True, False)

            'End Using

            If ContractRemarksFromUser.GetDataByAddendumUser(_AddendumID, _LiteralUserRank.Text) > 0 Then
                'Be careful on "see comment" and "add comment" text, otherwise REJECT button wont be disabled
                If Roles.IsUserInRole("ContractLeadGirls") = True Then
                    If _LiteralUserRank.Text <> "lawyers" Then
                        If _CountUnreadComments = 0 Then
                            _LinkButtonRemarkUserRank.Text = "See Comment"
                            _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksAddendum').modal('show'); global.Addendums_UserRemarkListModel.getComments(" + _AddendumID.ToString + "); global.Addendums_UserRemarkModel.CreateMe('0', '" + _AddendumID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                            _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.Red
                        Else
                            If AddendumExecuted Then
                                _LinkButtonRemarkUserRank.Text = "See Comment"
                                _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksAddendum').modal('show'); global.Addendums_UserRemarkListModel.getComments(" + _AddendumID.ToString + "); global.Addendums_UserRemarkModel.CreateMe('0', '" + _AddendumID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                                _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.Green
                            Else
                                _LinkButtonRemarkUserRank.Text = "<span style=" + """" + "color:red;" + """" + ">See Comment</span><span class=" + """" + "LabelNotification" + """" + ">" + _CountUnreadComments.ToString + "</span>"
                            End If
                        End If
                    Else
                        _LinkButtonRemarkUserRank.Text = "See Comment"
                        _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksAddendum').modal('show'); global.Addendums_UserRemarkListModel.getComments(" + _AddendumID.ToString + "); global.Addendums_UserRemarkModel.CreateMe('0', '" + _AddendumID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                        _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.Red
                    End If
                Else
                    If _LiteralUserRank.Text <> Page.User.Identity.Name.ToString Then
                        If _CountUnreadComments = 0 Then
                            _LinkButtonRemarkUserRank.Text = "See Comment"
                            _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksAddendum').modal('show'); global.Addendums_UserRemarkListModel.getComments(" + _AddendumID.ToString + "); global.Addendums_UserRemarkModel.CreateMe('0', '" + _AddendumID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                            _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.Red
                        Else
                            If AddendumExecuted Then
                                _LinkButtonRemarkUserRank.Text = "See Comment"
                                _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksAddendum').modal('show'); global.Addendums_UserRemarkListModel.getComments(" + _AddendumID.ToString + "); global.Addendums_UserRemarkModel.CreateMe('0', '" + _AddendumID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                                _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.Green
                            Else
                                _LinkButtonRemarkUserRank.Text = "<span style=" + """" + "color:red;" + """" + ">See Comment</span><span class=" + """" + "LabelNotification" + """" + ">" + _CountUnreadComments.ToString + "</span>"
                            End If
                        End If

                    Else
                        _LinkButtonRemarkUserRank.Text = "See Comment"
                        _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksAddendum').modal('show'); global.Addendums_UserRemarkListModel.getComments(" + _AddendumID.ToString + "); global.Addendums_UserRemarkModel.CreateMe('0', '" + _AddendumID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                        _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.Red
                    End If
                End If
            Else
                If _LiteralUserRank.Text.ToString.ToLower = _CurrentUser Then
                    'Be careful on "see comment" and "add comment" text, otherwise REJECT button wont be disabled
                    _LinkButtonRemarkUserRank.Text = "Add Comment"
                    _LinkButtonRemarkUserRank.Attributes.Add("onclick", "$('#ModalRemarksAddendum').modal('show'); global.Addendums_UserRemarkListModel.getComments(" + _AddendumID.ToString + "); global.Addendums_UserRemarkModel.CreateMe('0', '" + _AddendumID.ToString + "', '" + _LiteralUserRank.Text + "', '', 'lastUpdate', '" + Page.User.Identity.Name.ToLower + "', " + ShowOrHideRemarkEntry(_LiteralUserRank.Text, Page.User.Identity.Name.ToLower) + ") ")
                    _LinkButtonRemarkUserRank.ForeColor = System.Drawing.Color.LightSkyBlue
                Else
                    _LinkButtonRemarkUserRank.Visible = False
                End If
            End If

        End If

        _LinkButtonRemarkUserRank.Enabled = False

    End Sub

    Protected Sub AssignValuesToControls(ByVal _ContractID As Integer, _
                                         ByVal _AddendumID As Integer, _
                                         ByVal _LiteralUserRank As Literal, _
                                         ByVal _ImageButtonApproval As ImageButton, _
                                         ByVal _LiteralWhenApproved As Literal, _
                                         Optional ByVal _LinkButtonRejectContractGirls As LinkButton = Nothing)

        If _LiteralUserRank.Text.Trim().Length = 0 Then
            Exit Sub
        End If

        ' if user in Role ContractLeadGirls, then activate <Lawyers>
        If Page.User.IsInRole("ContractLeadGirls") Then
            If _LiteralUserRank.Text = "lawyers" Then
                _ImageButtonApproval.Enabled = True
                _ImageButtonApproval.CssClass = "icon-animated-vertical"
                If ContractView.ContractOrAddendumRejected(_ContractID, _AddendumID, _LiteralUserRank.Text) = True Then
                    _LinkButtonRejectContractGirls.Text = "Not Agreed"
                    _ImageButtonApproval.Visible = False
                ElseIf UserApproved(_ContractID, _AddendumID, _LiteralUserRank.Text).Approved = False Then
                    _LinkButtonRejectContractGirls.Text = "Reject"
                ElseIf UserApproved(_ContractID, _AddendumID, _LiteralUserRank.Text).Approved = True Then
                    _LinkButtonRejectContractGirls.Visible = False
                End If
            Else
                _ImageButtonApproval.Enabled = False
            End If
        Else
            If _LiteralUserRank.Text = "lawyers" Then
                If ContractView.ContractOrAddendumRejected(_ContractID, _AddendumID, _LiteralUserRank.Text) = True Then
                    _LinkButtonRejectContractGirls.Text = "Not Agreed"
                    _LinkButtonRejectContractGirls.Enabled = False
                    _ImageButtonApproval.Visible = False
                ElseIf UserApproved(_ContractID, _AddendumID, _LiteralUserRank.Text).Approved = False Then
                    _LinkButtonRejectContractGirls.Text = "Reject"
                    _LinkButtonRejectContractGirls.Enabled = False
                ElseIf UserApproved(_ContractID, _AddendumID, _LiteralUserRank.Text).Approved = True Then
                    _LinkButtonRejectContractGirls.Visible = False
                End If
            End If
            ' Diasable or enable ImageButton as per user
            If _LiteralUserRank.Text.ToLower = Page.User.Identity.Name.ToLower Then
                _ImageButtonApproval.Enabled = True
                _ImageButtonApproval.CssClass = "icon-animated-vertical"
            ElseIf _LiteralUserRank.Text.ToLower <> Page.User.Identity.Name.ToLower Then
                _ImageButtonApproval.Enabled = False
            End If
        End If

        If Len(_LiteralUserRank.Text) = 0 Then
            ' dont show anything, this user is not required in approval
            _ImageButtonApproval.Visible = False
            _LiteralWhenApproved.Visible = False
            Exit Sub
        End If

        If UserApproved(_ContractID, _AddendumID, _LiteralUserRank.Text).Approved = True Then
            _ImageButtonApproval.ImageUrl = "http://pts.mercuryeng.ru/images/ContractApprove.png"
            _LiteralWhenApproved.Text = UserApproved(_ContractID, _AddendumID, _LiteralUserRank.Text).WhenApproved
        Else
            _ImageButtonApproval.ImageUrl = "http://pts.mercuryeng.ru/images/ContractNotApprove.png"
            _LiteralWhenApproved.Text = ""
        End If

    End Sub

    Protected Class UserApprovalStatus

        Friend Approved As Boolean
        Friend WhenApproved As Date
        Friend Exceptional As Boolean

    End Class

    Protected Function UserApproved(ByVal _ContractID As Integer, ByVal _AddendumID As Integer, ByVal _UserName As String) As UserApprovalStatus

        Dim _return As New UserApprovalStatus

        If Len(_UserName) = 0 Then
            ' user approval not required
            _return.Approved = 0
            _return.WhenApproved = Nothing
            _return.Exceptional = 0
            Return _return
            _return = Nothing
            Exit Function
        End If

        If _AddendumID > 0 Then
            ' it is Addendum
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " IF NOT EXISTS ( " + _
                                        " SELECT     WhenApproved, Exception " + _
                                        " FROM         dbo.Table_Addendum_UsersApprv " + _
                                        " WHERE     (UserName = @UserName) AND (AddendumID = @AddendumID) AND Exception = 0 ) " + _
                                        "  " + _
                                        " 	BEGIN " + _
                                        " 		SELECT 0 AS Approved, NULL AS WhenApproved, 0 AS Exception " + _
                                        " 	END " + _
                                        " ELSE " + _
                                        "  " + _
                                        " 	BEGIN " + _
                                        " 		SELECT     1 AS Approved, WhenApproved, Exception " + _
                                        " 		FROM         dbo.Table_Addendum_UsersApprv " + _
                                        " 		WHERE     (UserName = @UserName) AND (AddendumID = @AddendumID) AND Exception = 0 " + _
                                        " 	END "

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                'syntax for parameter adding
                Dim UserParm As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 256)
                UserParm.Value = _UserName
                Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", System.Data.SqlDbType.Int)
                AddendumID.Value = _AddendumID
                Dim dr As SqlDataReader = cmd.ExecuteReader

                While dr.Read
                    _return.Approved = dr(0)
                    If IsDBNull(dr(1)) Then
                        _return.WhenApproved = Nothing
                    Else
                        _return.WhenApproved = dr(1)
                    End If
                    _return.Exceptional = dr(2)
                End While

                Return _return
                _return = Nothing
                con.Close()
                dr.Close()
                con.Dispose()
                Exit Function
            End Using
        End If

        If _ContractID > 0 And _AddendumID = 0 Then
            ' it is Contract
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " IF NOT EXISTS ( " + _
                                        " SELECT     WhenApproved, Exception " + _
                                        " FROM         dbo.Table_Contract_UsersApprv " + _
                                        " WHERE     (UserName = @UserName) AND (ContractID = @ContractID) AND Exception = 0 ) " + _
                                        "  " + _
                                        " 	BEGIN " + _
                                        " 		SELECT 0 AS Approved, NULL AS WhenApproved, 0 AS Exception " + _
                                        " 	END " + _
                                        " ELSE " + _
                                        "  " + _
                                        " 	BEGIN " + _
                                        " 		SELECT     1 AS Approved, WhenApproved, Exception " + _
                                        " 		FROM         dbo.Table_Contract_UsersApprv " + _
                                        " 		WHERE     (UserName = @UserName) AND (ContractID = @ContractID) AND Exception = 0 " + _
                                        " 	END "

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                'syntax for parameter adding
                Dim UserParm As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 256)
                UserParm.Value = _UserName
                Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
                ContractID.Value = _ContractID
                Dim dr As SqlDataReader = cmd.ExecuteReader

                While dr.Read
                    _return.Approved = dr(0)
                    If IsDBNull(dr(1)) Then
                        _return.WhenApproved = Nothing
                    Else
                        _return.WhenApproved = dr(1)
                    End If
                    _return.Exceptional = dr(2)
                End While

                Return _return
                _return = Nothing
                con.Close()
                dr.Close()
                con.Dispose()
                Exit Function
            End Using
        End If
    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim CxAll As CheckBox = cbxAll.FindControl("CheckBox")
        Dim CxOnly As CheckBox = cbxOnlyMy.FindControl("CheckBox")

        cbxAll.Text = BodyTexts.Ref("6/KXqKPwbEabBpXg/8uncA")
        cbxOnlyMy.Text = BodyTexts.Ref("y+qDObcHYUeHR3yoPdy5CQ")

        PageUserName.Value = Page.User.Identity.Name.ToLower()

        If Page.IsPostBack = False Then
            CxOnly.Checked = True
        End If

        If Page.User.Identity.Name.ToString.ToLower = "thomas.mcdonnell" Then
            ' if user is General Director, show him only his items.
            If Not IsPostBack Then
                SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("FilterType").DefaultValue = "Show Only My Items"
                CxOnly.Checked = True
                CxAll.Checked = False
            End If
            SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("username").DefaultValue = "thomas.mcdonnell"

            'ElseIf Page.User.Identity.Name.ToString.ToLower = "eoin.vaughan" Or Page.User.Identity.Name.ToString.ToLower = "ronan.lynch" _
            '    Or Page.User.Identity.Name.ToString.ToLower = "alan.slattery" Then

            '    SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("FilterType").DefaultValue = "Show Only My Items"
            '    cbxOnlyMy.Visible = False
            '    cbxAll.Visible = False
            '    SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("username").DefaultValue = Page.User.Identity.Name.ToString.ToLower

        Else

            SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("username").DefaultValue = Page.User.Identity.Name.ToString.ToLower

            'If User.IsInRole("ContractLeadGirls") And Page.User.Identity.Name.ToString.ToLower <> "savas" Then
            '    SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("username").DefaultValue = "lawyers"
            'Else
            '    SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("username").DefaultValue = Page.User.Identity.Name.ToString.ToLower
            'End If
        End If

    End Sub

    Protected Sub GridviewNotApprovedContractsOrAddendums_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridviewNotApprovedContractsOrAddendums.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim grd As GridView = sender
        Dim row As GridViewRow = grd.Rows(index)

        Dim LiteralContractID As Literal = CType(row.FindControl("LiteralContractID"), Literal)
        Dim LiteralAddendumID As Literal = CType(row.FindControl("LiteralAddendumID"), Literal)

        ' REMOVED, it is in Web Method (~/Pages/PTS/PageMethods/ApprovalMatrix.aspx/Approve)
        'Select Case e.CommandName
        '    Case "ApprovalUserRank1"
        '        ApproveOrDisapprove(LiteralContractID, LiteralAddendumID, CType(row.FindControl("LiteralUserRank1"), Literal))
        '    Case "ApprovalUserRank2"
        '        ApproveOrDisapprove(LiteralContractID, LiteralAddendumID, CType(row.FindControl("LiteralUserRank2"), Literal))
        '    Case "ApprovalUserRank3"
        '        ApproveOrDisapprove(LiteralContractID, LiteralAddendumID, CType(row.FindControl("LiteralUserRank3"), Literal))
        '    Case "ApprovalUserRank4"
        '        ApproveOrDisapprove(LiteralContractID, LiteralAddendumID, CType(row.FindControl("LiteralUserRank4"), Literal))
        '    Case "ApprovalUserRank5"
        '        ApproveOrDisapprove(LiteralContractID, LiteralAddendumID, CType(row.FindControl("LiteralUserRank5"), Literal))
        '    Case "ApprovalUserRank6"
        '        ApproveOrDisapprove(LiteralContractID, LiteralAddendumID, CType(row.FindControl("LiteralUserRank6"), Literal))
        '    Case "ApprovalUserRank7"
        '        ApproveOrDisapprove(LiteralContractID, LiteralAddendumID, CType(row.FindControl("LiteralUserRank7"), Literal))
        '    Case "ApprovalUserRank8"
        '        ApproveOrDisapprove(LiteralContractID, LiteralAddendumID, CType(row.FindControl("LiteralUserRank8"), Literal))
        '    Case "ApprovalUserRank9"
        '        ApproveOrDisapprove(LiteralContractID, LiteralAddendumID, CType(row.FindControl("LiteralUserRank9"), Literal))
        '    Case "ApprovalUserRank10"
        '        ApproveOrDisapprove(LiteralContractID, LiteralAddendumID, CType(row.FindControl("LiteralUserRank10"), Literal))
        '    Case "ApprovalUserRank11"
        '        ApproveOrDisapprove(LiteralContractID, LiteralAddendumID, CType(row.FindControl("LiteralUserRank11"), Literal))
        '    Case "ApprovalUserRank12"
        '        ApproveOrDisapprove(LiteralContractID, LiteralAddendumID, CType(row.FindControl("LiteralUserRank12"), Literal))
        'End Select

        Select Case e.CommandName
            Case "RemarkUserRank1"
                ExecuteUserRemark(LiteralContractID.Text, LiteralAddendumID.Text, "UserRank1", row)
            Case "RemarkUserRank2"
                ExecuteUserRemark(LiteralContractID.Text, LiteralAddendumID.Text, "UserRank2", row)
            Case "RemarkUserRank3"
                ExecuteUserRemark(LiteralContractID.Text, LiteralAddendumID.Text, "UserRank3", row)
            Case "RemarkUserRank4"
                ExecuteUserRemark(LiteralContractID.Text, LiteralAddendumID.Text, "UserRank4", row)
            Case "RemarkUserRank5"
                ExecuteUserRemark(LiteralContractID.Text, LiteralAddendumID.Text, "UserRank5", row)
            Case "RemarkUserRank6"
                ExecuteUserRemark(LiteralContractID.Text, LiteralAddendumID.Text, "UserRank6", row)
            Case "RemarkUserRank7"
                ExecuteUserRemark(LiteralContractID.Text, LiteralAddendumID.Text, "UserRank7", row)
            Case "RemarkUserRank8"
                ExecuteUserRemark(LiteralContractID.Text, LiteralAddendumID.Text, "UserRank8", row)
        End Select

        If (e.CommandName = "ApprovalByRejecting") Then
            If Convert.ToInt32(LiteralContractID.Text) > 0 And Convert.ToInt32(LiteralAddendumID.Text) = 0 Then
                ' It is contract
                Dim LiteralUserRank3 As Literal = CType(row.FindControl("LiteralUserRank3"), Literal)

                ' It is rejected or not
                If ContractView.ContractOrAddendumRejected(Convert.ToInt32(LiteralContractID.Text), Convert.ToInt32(LiteralAddendumID.Text), "lawyers") = False Then
                    ' REJECT
                    ContractView.Reject_ThisContractByContractGirls(LiteralContractID.Text, LiteralUserRank3.Text, WebUserControl_ContractEmailBody)
                    Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                                   Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                                   ">You have rejected successfully!</p>")

                ElseIf ContractView.ContractOrAddendumRejected(Convert.ToInt32(LiteralContractID.Text), Convert.ToInt32(LiteralAddendumID.Text), "lawyers") = True Then
                    ' REMOVE REJECT
                    ContractView.RemoveReject_ThisContractByContractGirls(LiteralContractID.Text, LiteralUserRank3.Text, WebUserControl_ContractEmailBody)
                    Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                                   Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                                   ">You have removed your rejection successfully!</p>")

                End If

                ' CHECK CONTRACT IF IT IS READY FOR PO
                If ContractView.ContractReadyToTurnPo(Convert.ToInt32(LiteralContractID.Text)) _
                  And ContractView.ScenarioNoForThisContract(Convert.ToInt32(LiteralContractID.Text)) > 0 Then

                    ' AddendumID to be 0
                    ContractView.InsertOrUpdatePoFromContract( _
                      CreateDataReader.Create_Table_Contract(Convert.ToInt32(LiteralContractID.Text)).ProjectID, _
                      Convert.ToInt32(LiteralContractID.Text), _
                      0, _
                      HttpContext.Current.User.Identity.Name.ToLower, _
                      POdetailsForEmail)

                End If
                ' ......./ END OF CHECK CONTRACT IF IT IS READY FOR PO
            End If

            If Convert.ToInt32(LiteralContractID.Text) > 0 And Convert.ToInt32(LiteralAddendumID.Text) > 0 Then
                ' It is addendum
                Dim LiteralUserRank3 As Literal = CType(row.FindControl("LiteralUserRank3"), Literal)

                ' It is rejected or not
                If ContractView.ContractOrAddendumRejected(Convert.ToInt32(LiteralContractID.Text), Convert.ToInt32(LiteralAddendumID.Text), "lawyers") = False Then
                    ' REJECT
                    ContractView.Reject_ThisAddendumByContractGirls(LiteralAddendumID.Text, LiteralUserRank3.Text, WebUserControl_AddendumEmailBody)
                    Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                                   Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                                   ">You have rejected successfully!</p>")

                ElseIf ContractView.ContractOrAddendumRejected(Convert.ToInt32(LiteralContractID.Text), Convert.ToInt32(LiteralAddendumID.Text), "lawyers") = True Then
                    ' REMOVE REJECT
                    ContractView.RemoveReject_ThisAddendumByContractGirls(LiteralAddendumID.Text, LiteralUserRank3.Text, WebUserControl_AddendumEmailBody)
                    Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                                   Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                                   ">You have removed your rejection successfully!</p>")

                End If

                ' CHECK ADDENDUM IF IT IS READY FOR PO
                Dim ContractID As Integer = CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LiteralAddendumID.Text)).ContractID
                If ContractView.AddendumReadyToTurnPo(Convert.ToInt32(LiteralAddendumID.Text)) _
                  And _
                  ( _
                      (ContractView.ScenarioNoForThisAddendum(Convert.ToInt32(LiteralAddendumID.Text)) = 0 _
                       And CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LiteralAddendumID.Text)).AddendumTypes = 3) _
                      Or _
                      (ContractView.ScenarioNoForThisAddendum(Convert.ToInt32(LiteralAddendumID.Text)) > 0 _
                       And CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LiteralAddendumID.Text)).AddendumTypes > 0 _
                       And CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LiteralAddendumID.Text)).AddendumTypes < 3) _
                   ) _
                    And ContractView.GetPoExecutionFromAddendum(Convert.ToInt32(LiteralAddendumID.Text)) = False Then

                    ContractView.InsertOrUpdatePoFromContract( _
                      CreateDataReader.Create_Table_Contract(ContractID).ProjectID, _
                      ContractID, _
                      Convert.ToInt32(LiteralAddendumID.Text), _
                      HttpContext.Current.User.Identity.Name.ToLower, _
                      POdetailsForEmail)

                End If
                ' ......./ END OF CHECK ADDENDUM IF IT IS READY FOR PO
            End If
        End If

        Me.DataBind()
    End Sub

    Protected Sub ExecuteUserRemark(ByVal _ContractID As Integer, ByVal _AddendumID As Integer, ByVal _userRank As String, _
                                    ByVal _row As GridViewRow)

        ' inputs
        'contractID
        'addendumId
        'LiteralUserRank

        LiteralAddendumIDTransfer.Text = _AddendumID.ToString
        LiteralContractIDTransfer.Text = _ContractID.ToString
        Dim _string As String = CType(_row.FindControl("Literal" + _userRank), Literal).Text ' This is userName
        LiteralUserNameTransfer.Text = _string

        Dim _CurrentUser As String = ""

        If Roles.IsUserInRole("ContractLeadGirls") = True Then
            _CurrentUser = NameOfLeadLawyer.GetNameFromFunction
        Else
            _CurrentUser = Page.User.Identity.Name.ToString.ToLower()
        End If

        If CType(_row.FindControl("Literal" + _userRank), Literal).Text.ToLower = "lawyers" Then
            _string = NameOfLeadLawyer.GetNameFromFunction
        Else
            _string = CType(_row.FindControl("Literal" + _userRank), Literal).Text
        End If


        If _AddendumID = 0 Then
            SqlDataSourceEntry.SelectParameters("Type").DefaultValue = "contract"
            SqlDataSourceEntry.SelectParameters("id").DefaultValue = _ContractID
        Else
            SqlDataSourceEntry.SelectParameters("Type").DefaultValue = "addendum"
            SqlDataSourceEntry.SelectParameters("id").DefaultValue = _AddendumID
        End If

        If CheckIfUserInApprovalMatrix(_row) Then

            PanelEntry.Visible = True

        Else

            PanelEntry.Visible = False

        End If

        GridViewComments.DataBind()
        ModalPopupExtenderRemarkFromUser.Show()

        ' Insert unread comments to table for this user
        If _ContractID > 0 And _AddendumID = 0 Then
            ' Contract

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim FromWho As String = If(_string = NameOfLeadLawyer.GetNameFromFunction, "lawyers", _string)
                Dim ForWho As String = If(Roles.IsUserInRole("ContractLeadGirls") = True, "lawyers", Page.User.Identity.Name.ToString.ToLower)

                db.Database.ExecuteSqlCommand( _
                " INSERT INTO [dbo].[Table_Contract_CommentsRead] " + _
                "            ([C_Or_A] " + _
                "            ,[id_comment] " + _
                "            ,[FromWho] " + _
                "            ,[ReadWho] " + _
                "            ,[WhenRead]) " + _
                " Select [C_or_A] " + _
                "       ,[id_comment] " + _
                "       ,[FromWho] " + _
                "       ,[ForWho] " + _
                "       ,{0} " + _
                "   FROM [ApprMx].[Notf_ContractCommentsUnRead] " + _
                " WHERE C_or_A = N'C' and C_or_A_id = {1} and ForWho = {2}", _
                DateTime.Now, _ContractID, ForWho)

            End Using

        ElseIf _ContractID > 0 And _AddendumID > 0 Then
            ' Addendum

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim FromWho As String = If(_string = NameOfLeadLawyer.GetNameFromFunction, "lawyers", _string)
                Dim ForWho As String = If(Roles.IsUserInRole("ContractLeadGirls") = True, "lawyers", Page.User.Identity.Name.ToString.ToLower)

                db.Database.ExecuteSqlCommand( _
                " INSERT INTO [dbo].[Table_Contract_CommentsRead] " + _
                "            ([C_Or_A] " + _
                "            ,[id_comment] " + _
                "            ,[FromWho] " + _
                "            ,[ReadWho] " + _
                "            ,[WhenRead]) " + _
                " Select [C_or_A] " + _
                "       ,[id_comment] " + _
                "       ,[FromWho] " + _
                "       ,[ForWho] " + _
                "       ,{0} " + _
                "   FROM [ApprMx].[Notf_AddendumCommentsUnRead] " + _
                " WHERE C_or_A = N'A' and C_or_A_id = {1} and ForWho = {2} ", _
                DateTime.Now, _AddendumID, ForWho)

            End Using

        End If


    End Sub

    Protected Function CheckIfUserInApprovalMatrix(ByVal _row As GridViewRow) As Boolean

        Dim what As Boolean = False

        For i = 1 To 8

            If CType(_row.FindControl("LiteralUserRank" + i.ToString()), Literal).Text.ToLower = Page.User.Identity.Name.ToString.ToLower Then
                what = True
            End If

        Next

        If Roles.IsUserInRole("ContractLeadGirls") = True Then

            what = True

        End If

        Return what

    End Function

    Protected Sub ApproveOrDisapprove(ByVal LiteralContractID As Literal _
                                      , ByVal LiteralAddendumID As Literal _
                                      , ByVal LiteralUserRank As Literal)

        If Convert.ToInt32(LiteralContractID.Text) > 0 And Convert.ToInt32(LiteralAddendumID.Text) = 0 Then
            ' It is contract
            ' It is approval or NOT approval ?
            If ContractOrAddendumApprovedByUser(Convert.ToInt32(LiteralContractID.Text), Convert.ToInt32(LiteralAddendumID.Text), LiteralUserRank.Text) = False Then
                ' APPROVE
                ContractView.ApproveContract(LiteralContractID.Text, LiteralUserRank.Text)
                Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                               Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                               ">You have approved successfully!</p>")
            ElseIf ContractOrAddendumApprovedByUser(Convert.ToInt32(LiteralContractID.Text), Convert.ToInt32(LiteralAddendumID.Text), LiteralUserRank.Text) = True Then
                ' NOT APPROVE
                ContractView.RemoveApprovalContract(LiteralContractID.Text, LiteralUserRank.Text)
                Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                               Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                               ">You have removed your approval successfully!</p>")
            End If

            ' CHECK CONTRACT IF IT IS READY FOR PO
            If ContractView.ContractReadyToTurnPo(Convert.ToInt32(LiteralContractID.Text)) _
              And ContractView.ScenarioNoForThisContract(Convert.ToInt32(LiteralContractID.Text)) <> 0 _
               And ContractView.GetPoExecutionFromContract(Convert.ToInt32(LiteralContractID.Text)) = False Then

                ' AddendumID to be 0
                ContractView.InsertOrUpdatePoFromContract( _
                  CreateDataReader.Create_Table_Contract(Convert.ToInt32(LiteralContractID.Text)).ProjectID, _
                  Convert.ToInt32(LiteralContractID.Text), _
                  0, _
                  HttpContext.Current.User.Identity.Name.ToLower, _
                  POdetailsForEmail)

            End If
            ' ......./ END OF CHECK CONTRACT IF IT IS READY FOR PO

        ElseIf Convert.ToInt32(LiteralContractID.Text) > 0 And Convert.ToInt32(LiteralAddendumID.Text) > 0 Then
            ' it is addendum
            ' It is approval or NOT approval ?
            If ContractOrAddendumApprovedByUser(Convert.ToInt32(LiteralContractID.Text), Convert.ToInt32(LiteralAddendumID.Text), LiteralUserRank.Text) = False Then
                ' APPROVE
                ContractView.ApproveAddendum(LiteralAddendumID.Text, LiteralUserRank.Text)
                Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                               Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                               ">You have approved successfully!</p>")
            ElseIf ContractOrAddendumApprovedByUser(Convert.ToInt32(LiteralContractID.Text), Convert.ToInt32(LiteralAddendumID.Text), LiteralUserRank.Text) = True Then
                ' NOT APPROVE
                ContractView.RemoveApprovalAddendum(LiteralAddendumID.Text, LiteralUserRank.Text)
                Notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                                               Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                                               ">You have removed your approval successfully!</p>")
            End If

            ' CHECK ADDENDUM IF IT IS READY FOR PO
            Dim ContractID As Integer = CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LiteralAddendumID.Text)).ContractID
            If ContractView.AddendumReadyToTurnPo(Convert.ToInt32(LiteralAddendumID.Text)) _
              And _
              ( _
                  (ContractView.ScenarioNoForThisAddendum(Convert.ToInt32(LiteralAddendumID.Text)) = 0 _
                   And CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LiteralAddendumID.Text)).AddendumTypes = 3) _
                  Or _
                  (ContractView.ScenarioNoForThisAddendum(Convert.ToInt32(LiteralAddendumID.Text)) > 0 _
                   And CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LiteralAddendumID.Text)).AddendumTypes > 0 _
                   And CreateDataReader.Create_Table_Addendums(Convert.ToInt32(LiteralAddendumID.Text)).AddendumTypes < 3) _
               ) _
                And ContractView.GetPoExecutionFromAddendum(Convert.ToInt32(LiteralAddendumID.Text)) = False Then

                ContractView.InsertOrUpdatePoFromContract( _
                  CreateDataReader.Create_Table_Contract(ContractID).ProjectID, _
                  ContractID, _
                  Convert.ToInt32(LiteralAddendumID.Text), _
                  HttpContext.Current.User.Identity.Name.ToLower, _
                  POdetailsForEmail)

            End If
            ' ......./ END OF CHECK ADDENDUM IF IT IS READY FOR PO
        End If
    End Sub

    Protected Function ContractOrAddendumApprovedByUser(ByVal _ContractID As Integer, ByVal _AddendumID As Integer, ByVal _username As String) As Boolean

        If _ContractID > 0 And _AddendumID = 0 Then
            ' use contract
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " IF EXISTS (SELECT [ContractID] FROM [Table_Contract_UsersApprv] WHERE UserName = @UserName AND ContractID = @ContractID)" + _
                                        " Select 1" + _
                                        " ELSE" + _
                                        " Select 0"

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                'syntax for parameter adding
                Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
                ContractID.Value = _ContractID
                Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar)
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
                Dim sqlstring As String = " IF EXISTS (SELECT [AddendumID] FROM [Table_Addendum_UsersApprv] WHERE UserName = @UserName AND AddendumID = @AddendumID)" + _
                                        " Select 1" + _
                                        " ELSE" + _
                                        " Select 0"

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                'syntax for parameter adding
                Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", System.Data.SqlDbType.Int)
                AddendumID.Value = _AddendumID
                Dim UserName As SqlParameter = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar)
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

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Protected Sub ButtonUpdate_Click(sender As Object, e As System.EventArgs) Handles ButtonUpdate.Click

        'Dim ContractID As Integer = Convert.ToInt32(LiteralContractIDTransfer.Text)
        'Dim AddendumID As Integer = Convert.ToInt32(LiteralAddendumIDTransfer.Text)
        'Dim UserName As String = LiteralUserNameTransfer.Text
        ''Dim Remark As String = EditorNotes.Content.ToString.Replace("'", "'+N''''+N'")
        'Dim Remark As String = TextBoxEditorNotes.Text.Trim

        'If Roles.IsUserInRole("ContractLeadGirls") = True Then
        '    UserName = NameOfLeadLawyer.GetNameFromFunction
        'End If

        ''Update Or Insert Or Delete the comments
        'If ContractID > 0 And AddendumID = 0 Then
        '    ' Contract
        '    If ContractRemarksFromUser.GetDataByContractUser(ContractID, UserName) IsNot Nothing Then
        '        ' There is comment, update or delete
        '        If String.IsNullOrEmpty(Remark) Then
        '            '' delete
        '            'ContractRemarksFromUser.DeleteDataByContractUser(ContractID, UserName)
        '        Else
        '            '' update
        '            'ContractRemarksFromUser.UpdateByContractUser(ContractID, UserName, Remark, WebUserControl_ContractEmailBody)
        '        End If
        '    Else
        '        '' There is no comment, insert
        '        'ContractRemarksFromUser.InsertByContractUser(ContractID, UserName, Remark)
        '    End If

        'ElseIf ContractID > 0 And AddendumID > 0 Then
        '    ' Addendum
        '    If ContractRemarksFromUser.GetDataByAddendumUser(AddendumID, UserName) IsNot Nothing Then
        '        ' There is comment, update or delete
        '        If String.IsNullOrEmpty(Remark) Then
        '            '' delete
        '            'ContractRemarksFromUser.DeleteDataByAddendumUser(AddendumID, UserName)
        '        Else
        '            '' update
        '            'ContractRemarksFromUser.UpdateByAddendumUser(AddendumID, UserName, Remark, WebUserControl_AddendumEmailBody)
        '        End If

        '    Else
        '        '' There is no comment, insert
        '        'ContractRemarksFromUser.InsertByAddendumUser(AddendumID, UserName, Remark)
        '    End If

        'End If

        'GridviewNotApprovedContractsOrAddendums.DataBind()

    End Sub

    Protected Sub SqlDataSourceNotApprovedContractsOrAddendums_Selecting(sender As Object, e As SqlDataSourceSelectingEventArgs) Handles SqlDataSourceNotApprovedContractsOrAddendums.Selecting
        e.Command.CommandTimeout = 200
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender


    End Sub

    Protected Sub Page_PreRenderComplete(sender As Object, e As EventArgs) Handles Me.PreRenderComplete


    End Sub

    Protected Sub ButtonGetEntry_Click(sender As Object, e As EventArgs)

        Dim ContractID As Integer = Convert.ToInt32(LiteralContractIDTransfer.Text)
        Dim AddendumID As Integer = Convert.ToInt32(LiteralAddendumIDTransfer.Text)
        'Dim UserName As String = LiteralUserNameTransfer.Text
        Dim UserName As String = Page.User.Identity.Name.ToString.ToLower
        Dim Remark As String = TextBoxUserEntry.Text.Trim

        If Roles.IsUserInRole("ContractLeadGirls") = True Then
            UserName = NameOfLeadLawyer.GetNameFromFunction.ToString.ToLower
        End If

        If ContractID > 0 And AddendumID = 0 Then
            ' Only Insert allowed at the moment
            ContractRemarksFromUser.InsertByContractUser(ContractID, UserName, Remark)
        ElseIf ContractID > 0 And AddendumID > 0 Then
            ' Only Insert allowed at the moment
            ContractRemarksFromUser.InsertByAddendumUser(AddendumID, UserName, Remark)
        End If

        GridViewComments.DataBind()
        ModalPopupExtenderRemarkFromUser.Show()
        GridviewNotApprovedContractsOrAddendums.DataBind()

    End Sub

    Protected Sub cbxAll_CheckedChanged(sender As Object, e As EventArgs)

        Dim c As UserControl = sender
        Dim Cbx As CheckBox = cbxAll.FindControl("CheckBox")

        If Cbx.Checked = True Then
            Dim c2 As CheckBox = cbxOnlyMy.FindControl("CheckBox")
            c2.Checked = False

            BindMatrix("Show All Items")

        Else
            Cbx.Checked = True

        End If

    End Sub

    Protected Sub cbxOnly_CheckedChanged(sender As Object, e As EventArgs)

        Dim c As UserControl = sender
        Dim Cbx As CheckBox = cbxOnlyMy.FindControl("CheckBox")

        If Cbx.Checked = True Then
            Dim c2 As CheckBox = cbxAll.FindControl("CheckBox")
            c2.Checked = False

            BindMatrix("Show Only My Items")

        Else

            Cbx.Checked = True

        End If

    End Sub

    Protected Sub BindMatrix(ByVal _criteria As String)

        SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("FilterType").DefaultValue = _criteria

        SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("username").DefaultValue = Page.User.Identity.Name.ToString.ToLower

        'If User.IsInRole("ContractLeadGirls") And Page.User.Identity.Name.ToString.ToLower <> "savas" Then
        '    SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("username").DefaultValue = "lawyers"
        'Else
        '    SqlDataSourceNotApprovedContractsOrAddendums.SelectParameters("username").DefaultValue = Page.User.Identity.Name.ToString.ToLower
        'End If

    End Sub

End Class
