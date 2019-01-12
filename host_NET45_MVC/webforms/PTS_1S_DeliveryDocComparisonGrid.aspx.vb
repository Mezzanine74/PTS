Imports System.IO

Partial Class PTS_1S_DeliveryDocComparisonGridREV
    Inherits System.Web.UI.Page

    Dim SupplierID1 As String = ""
    Dim SupplierID2 As String = ""
    Dim Counter As Integer = 0

    Protected Sub GridViewPTS_1S_Comparison_DataBound(sender As Object, e As EventArgs) Handles GridViewPTS_1S_Comparison.DataBound

        Session("excelcommand") = Nothing

    End Sub

    Protected Sub GridViewPTS_1S_Comparison_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewPTS_1S_Comparison.RowDataBound

        ' Divide Match Status
        e.Row.Cells(1).Style.Add("BORDER-LEFT", "#FF0066 5px solid")

        ' Divide 1S and PTS
        e.Row.Cells(5).Style.Add("BORDER-LEFT", "#FF0066 5px solid")
        e.Row.Cells(7).Style.Add("BORDER-LEFT", "#FF0066 5px solid")

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim HyperlinkPTS As HyperLink = DirectCast(e.Row.FindControl("hyperlinkPTS"), HyperLink)
            If Session("excelcommand") IsNot Nothing Then
                HyperlinkPTS.Visible = False
            Else
                If Not Roles.IsUserInRole("DeliveryComparison") Then
                    HyperlinkPTS.Visible = False
                Else

                    If Len(DataBinder.Eval(e.Row.DataItem, "ID_1S").ToString.Trim) <> 0 And DataBinder.Eval(e.Row.DataItem, "MatchID") = 8 And DataBinder.Eval(e.Row.DataItem, "ID_1S_Match") = 0 Then
                        ' show hyperlink
                        HyperlinkPTS.Visible = True

                        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                            Dim projectID As Integer = 0
                            Dim SupplierID As String = "0"

                            Try
                                SupplierID = DataBinder.Eval(e.Row.DataItem, "SupplierID")
                                projectID = DataBinder.Eval(e.Row.DataItem, "ProjectID")

                            Catch ex As Exception

                            End Try

                            Dim count_ = (Aggregate C In db.Table2_PONo Where C.Project_ID = projectID And C.SupplierID = SupplierID Into Count())

                            If count_ = 0 Then
                                HyperlinkPTS.NavigateUrl = "~/webforms/Nakladnaya.aspx?ProjectID=" + DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString + _
                                    "&SupplierID=" + DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString + _
                                    "&_1Sid=" + DataBinder.Eval(e.Row.DataItem, "ID_1S").ToString + _
                                    "&DocDate=" + Mid(DataBinder.Eval(e.Row.DataItem, "DocDate1S").ToString, 1, 10) + _
                                    "&DocNo=" + DataBinder.Eval(e.Row.DataItem, "DocNo1S").ToString
                                HyperlinkPTS.CssClass = "btn btn-mini btn-danger"

                            Else
                                HyperlinkPTS.NavigateUrl = "~/webforms/Nakladnaya.aspx?ProjectID=" + DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString + _
                                    "&SupplierID=" + DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString + _
                                    "&_1Sid=" + DataBinder.Eval(e.Row.DataItem, "ID_1S").ToString + _
                                    "&DocDate=" + Mid(DataBinder.Eval(e.Row.DataItem, "DocDate1S").ToString, 1, 10) + _
                                    "&DocNo=" + DataBinder.Eval(e.Row.DataItem, "DocNo1S").ToString
                                HyperlinkPTS.CssClass = "btn btn-mini btn-success"

                            End If


                        End Using

                    Else
                        HyperlinkPTS.Visible = False
                    End If

                End If
            End If

            ' Visible or Invisible for checkboxes
            Dim CheckBox1S As CheckBox = DirectCast(e.Row.FindControl("CheckBox1S"), CheckBox)
            Dim CheckBoxPTS As CheckBox = DirectCast(e.Row.FindControl("CheckBoxPTS"), CheckBox)

            If CheckBox1S IsNot Nothing Then

                If Session("excelcommand") IsNot Nothing Then
                    ' if excel command, dont show checkboxes. but their marks will be processed below
                    CheckBoxPTS.Visible = False
                    CheckBox1S.Visible = False

                Else

                    If Len(DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString.Trim) = 0 Then
                        CheckBoxPTS.Visible = False
                    Else
                        CheckBoxPTS.Visible = True
                    End If
                    If Len(DataBinder.Eval(e.Row.DataItem, "ID_1S").ToString.Trim) = 0 Then
                        CheckBox1S.Visible = False
                    Else
                        CheckBox1S.Visible = True
                    End If

                End If

            End If
            ' END Visible or Invisible for checkboxes

            ' Seperate Different projects With Different Color
            If e.Row.RowIndex = 0 Then
                SupplierID1 = DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString.Trim
                Counter = Counter + 1

                If Counter Mod 2 = 0 Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffe5ef")
                Else
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff4f8 ")
                End If
            Else
                If DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString.Trim = SupplierID1 Then
                    If Counter Mod 2 = 0 Then
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffe5ef")
                    Else
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff4f8 ")
                    End If
                Else
                    Counter = Counter + 1
                    SupplierID1 = DataBinder.Eval(e.Row.DataItem, "SupplierID").ToString.Trim
                    If Counter Mod 2 = 0 Then
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffe5ef")
                    Else
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff4f8 ")
                    End If
                End If

            End If
            ' END Seperate Different projects With Different Color

            ' Check or Uncheck 
            If CheckBox1S IsNot Nothing Then
                Using Adapter As New DeliveryTableAdapters.Table_Delivery_MatchingIndexesTableAdapter
                    If Adapter.GetCountOfId("1S", DataBinder.Eval(e.Row.DataItem, "ID_1S").ToString) = 0 Then
                        ' Checkbox to be unchecked
                        CheckBox1S.Checked = False
                    ElseIf Adapter.GetCountOfId("1S", DataBinder.Eval(e.Row.DataItem, "ID_1S").ToString) > 0 Then
                        ' Checkbox to be checked
                        CheckBox1S.Checked = True
                        ' Highlight the Cell
                        e.Row.Cells(5).BackColor = System.Drawing.Color.Lime
                    End If
                End Using
            End If

            If CheckBoxPTS IsNot Nothing Then
                Using Adapter As New DeliveryTableAdapters.Table_Delivery_MatchingIndexesTableAdapter
                    If Adapter.GetCountOfId("PTS", DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString) = 0 Then
                        ' Checkbox to be unchecked
                        CheckBoxPTS.Checked = False
                    ElseIf Adapter.GetCountOfId("PTS", DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString) > 0 Then
                        ' Checkbox to be checked
                        CheckBoxPTS.Checked = True
                        ' Highlight the Cell
                        e.Row.Cells(6).BackColor = System.Drawing.Color.Lime
                    End If
                End Using
            End If
            ' END Check or Uncheck 

            ' Show or Hide Rows as percriteria
            If CheckBox1S IsNot Nothing Then
                Dim C1S As Integer = 0
                Dim CPTS As Integer = 0
                If CheckBox1S.Checked = True Then
                    C1S = 1
                Else
                    C1S = 0
                End If
                If CheckBoxPTS.Checked = True Then
                    CPTS = 1
                Else
                    CPTS = 0
                End If

                If (C1S + CPTS) = 0 Then
                    ' None of them checked
                    If RadioButtonListCriteria.SelectedValue = 0 Then
                        ' Show all items
                        e.Row.Visible = True
                    ElseIf RadioButtonListCriteria.SelectedValue = 1 Then
                        ' Show matched items only
                        e.Row.Visible = False
                    ElseIf RadioButtonListCriteria.SelectedValue = 2 Then
                        ' Show Unmatched items
                        e.Row.Visible = True
                    End If

                Else
                    ' at least one of them checked
                    If RadioButtonListCriteria.SelectedValue = 0 Then
                        ' Show all items
                        e.Row.Visible = True
                    ElseIf RadioButtonListCriteria.SelectedValue = 1 Then
                        ' Show matched items only
                        e.Row.Visible = True
                    ElseIf RadioButtonListCriteria.SelectedValue = 2 Then
                        ' Show Unmatched items
                        e.Row.Visible = False
                    End If
                End If

            End If
            ' END Show or Hide Rows as percriteria

            ' enable or disable Checkboxes depending to Role
            If User.IsInRole("DeliveryComparison") Then
                CheckBox1S.Enabled = True
                CheckBoxPTS.Enabled = True
            Else
                CheckBox1S.Enabled = False
                CheckBoxPTS.Enabled = False
            End If

            If Session("excelcommand") IsNot Nothing Then
                ' if excel command, do nothing on this
            Else

                'highligh Document Value and Date if it matches
                If (CheckBox1S.Visible = True And CheckBox1S.Checked = False) And (CheckBoxPTS.Visible = True And CheckBoxPTS.Checked = False) Then

                    ' Col 2 (Doc Date)
                    Dim _dateMatch1 As Boolean = False
                    If e.Row.Cells(2).Text.Trim.ToLower = e.Row.Cells(11).Text.Trim.ToLower Then
                        _dateMatch1 = True
                    End If

                    Dim _dateMatch2 As Boolean = False
                    If e.Row.Cells(2).Text.Trim.ToLower = e.Row.Cells(14).Text.Trim.ToLower Then
                        _dateMatch2 = True
                    End If

                    ' order is important
                    If _dateMatch1 Then
                        e.Row.Cells(2).Text = e.Row.Cells(2).Text + "<img src=" + """" + "images/GreenMark.png" + """" + " />"
                        e.Row.Cells(11).Text = e.Row.Cells(11).Text + "<img src=" + """" + "images/GreenMark.png" + """" + " />"
                    End If

                    If _dateMatch2 Then
                        e.Row.Cells(2).Text = e.Row.Cells(2).Text + "<img src=" + """" + "images/GreenMark.png" + """" + " />"
                        e.Row.Cells(14).Text = e.Row.Cells(14).Text + "<img src=" + """" + "images/GreenMark.png" + """" + " />"
                    End If

                    ' Col 3 (Doc No)
                    If e.Row.Cells(3).Text.Trim.ToLower = e.Row.Cells(9).Text.Trim.ToLower Then
                        e.Row.Cells(3).Text = e.Row.Cells(3).Text + "<img src=" + """" + "images/GreenMark.png" + """" + " />"
                        e.Row.Cells(9).Text = e.Row.Cells(9).Text + "<img src=" + """" + "images/GreenMark.png" + """" + " />"
                    End If

                    ' Col 4 (Doc Value)
                    If e.Row.Cells(4).Text.Trim.ToLower = e.Row.Cells(7).Text.Trim.ToLower Then
                        e.Row.Cells(4).Text = e.Row.Cells(4).Text + "<img src=" + """" + "images/GreenMark.png" + """" + " />"
                        e.Row.Cells(7).Text = e.Row.Cells(7).Text + "<img src=" + """" + "images/GreenMark.png" + """" + " />"
                    End If

                End If
            End If

            ' Extract PO Currency
            Dim Currency As String = ""
            If DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString.IndexOf("Nkl_Akt_") <> -1 Then
                Dim Nkl_Akt_ As Int32 = Mid(DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString, DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString.IndexOf("Nkl_Akt_") + Len("Nkl_Akt_") + 1, Len(DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString) - Len("Nkl_Akt_"))

                Try
                    Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
                        Currency = (From Akt_To_Nakl In db.Table_PO_Akt_To_Nak Join Nakl In db.Table_PO_Nakladnaya On Akt_To_Nakl.ID_Nak Equals Nakl.ID_Nak
                                    Join PO In db.Table2_PONo On Nakl.PO_No Equals PO.PO_No Where Akt_To_Nakl.ID_Akt_To_Nak = Nkl_Akt_
                                    Select New With {.Currency = PO.PO_Currency}).FirstOrDefault.Currency.ToString

                    End Using

                Catch ex As Exception

                End Try

            ElseIf DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString.IndexOf("Nkl_") <> -1 Then
                Dim Nkl_ As Int32 = Mid(DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString, DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString.IndexOf("Nkl_") + Len("Nkl_") + 1, Len(DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString) - Len("Nkl_"))

                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
                    Try
                        Currency = (From Nakl In db.Table_PO_Nakladnaya Join PO In db.Table2_PONo On Nakl.PO_No Equals PO.PO_No Where Nakl.ID_Nak = Nkl_
                                    Select New With {.Currency = PO.PO_Currency}).FirstOrDefault.Currency.ToString

                    Catch ex As Exception

                    End Try
                End Using

            ElseIf DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString.IndexOf("Akt_") <> -1 Then
                Dim Akt_ As Int32 = Mid(DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString, DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString.IndexOf("Akt_") + Len("Akt_") + 1, Len(DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString) - Len("Akt_"))

                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
                    Try
                        Currency = (From Akt In db.Table_PO_Akt Join PO In db.Table2_PONo On Akt.PO_No Equals PO.PO_No Where Akt.ID_Akt = Akt_
                                    Select New With {.Currency = PO.PO_Currency}).FirstOrDefault.Currency.ToString
                    Catch ex As Exception

                    End Try

                End Using

            End If

            If Session("excelcommand") IsNot Nothing Then
                ' if excel command, dont use images, instead use text on SupplierName
                If Currency.Trim.ToLower = "euro" Then
                    e.Row.Cells(8).Text = e.Row.Cells(8).Text + "<span style=" + """" + "color:red; font-weight: bold;" + """" + ">- in EURO</span>"

                ElseIf Currency.Trim.ToLower = "dollar" Then
                    e.Row.Cells(8).Text = e.Row.Cells(8).Text + "<span style=" + """" + "color:red; font-weight: bold;" + """" + ">- in DOLLAR</span>"

                End If

            Else
                If Currency.Trim.ToLower = "euro" Then
                    e.Row.Cells(8).Text = e.Row.Cells(8).Text + "<img src=" + """" + "images/Euro_.png" + """" + " />"

                ElseIf Currency.Trim.ToLower = "dollar" Then
                    e.Row.Cells(8).Text = e.Row.Cells(8).Text + "<img src=" + """" + "images/dollar_.png" + """" + " />"

                End If

            End If

            ' Provide Entry Week
            Dim LiteralEntryDescription As Literal = DirectCast(e.Row.FindControl("LiteralEntryDescription"), Literal)
            Dim LiteralEntryWeek As Literal = DirectCast(e.Row.FindControl("LiteralEntryWeek"), Literal)
            If Not String.IsNullOrEmpty(LiteralEntryWeek.Text) Then
                'Dim _date As Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "CreatedBy"))
                Dim _date As Date = DateTime.Now
                Dim _dateEntry As Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "CreatedBy"))

                Dim _DayNumberOfWeek = Convert.ToInt16(_date.DayOfWeek)
                Dim TheLastSunday As Date = _date.AddDays(-If(_DayNumberOfWeek = 0, 7, _DayNumberOfWeek))
                Dim TwoSundayBefore As Date = _date.AddDays(-7 - If(_DayNumberOfWeek = 0, 7, _DayNumberOfWeek))
                Dim ThreeSundayBefore As Date = _date.AddDays(-7 - 7 - If(_DayNumberOfWeek = 0, 7, _DayNumberOfWeek))

                If _dateEntry > TheLastSunday Then
                    LiteralEntryDescription.Text = "This Week"

                ElseIf _dateEntry > TwoSundayBefore And _dateEntry <= TheLastSunday Then
                    LiteralEntryDescription.Text = "Last Week"

                ElseIf _dateEntry > ThreeSundayBefore And _dateEntry <= TwoSundayBefore Then
                    LiteralEntryDescription.Text = "Two Weeks Ago"

                ElseIf _dateEntry <= ThreeSundayBefore Then
                    LiteralEntryDescription.Text = "Older Than Two Weeks"

                End If

            End If


            Dim _tick As Boolean = False
            For Each _Item As ListItem In CheckBoxListWeeks.Items
                If _Item.Selected Then
                    _tick = True
                End If
            Next

            If Not _tick Then
                ' do nothing 
            Else
                ' make all row invisible first
                e.Row.Visible = False
                For Each _Item As ListItem In CheckBoxListWeeks.Items
                    If _Item.Selected Then
                        If _Item.Value = 1 Then
                            If LiteralEntryDescription.Text = "This Week" Then
                                e.Row.Visible = True
                            End If
                        End If
                        If _Item.Value = 2 Then
                            If LiteralEntryDescription.Text = "Last Week" Then
                                e.Row.Visible = True
                            End If
                        End If
                        If _Item.Value = 3 Then
                            If LiteralEntryDescription.Text = "Two Weeks Ago" Then
                                e.Row.Visible = True
                            End If
                        End If
                        If _Item.Value = 4 Then
                            If LiteralEntryDescription.Text = "Older Than Two Weeks" Then
                                e.Row.Visible = True
                            End If
                        End If
                        If _Item.Value = 5 Then
                            If LiteralEntryDescription.Text = "" Then
                                e.Row.Visible = True
                            End If
                        End If

                    End If
                Next

            End If

            If Request.QueryString("ID_PTS") IsNot Nothing Then

                If DataBinder.Eval(e.Row.DataItem, "ID_PTS").ToString.ToLower = Request.QueryString("ID_PTS").ToLower Then
                    e.Row.BackColor = System.Drawing.Color.LightBlue
                End If

            End If

        End If

    End Sub

    Protected Sub HideOrShowRow(ByVal _row As GridViewRow)



    End Sub

    Protected Sub RadioButtonListCriteria_SelectedIndexChanged(sender As Object, e As EventArgs)

        GridViewPTS_1S_Comparison.DataBind()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Request.QueryString("ProjectID") IsNot Nothing Then
            SqlDataSourcePTS_1S_ComparisonForGrid.SelectParameters("ProjectID").DefaultValue = Request.QueryString("ProjectID")

        Else
            If Not IsPostBack Then
                SqlDataSourcePTS_1S_ComparisonForGrid.SelectParameters("ProjectID").DefaultValue = 0
            Else
                SqlDataSourcePTS_1S_ComparisonForGrid.SelectParameters("ProjectID").DefaultValue = DropDownListProject.SelectedValue
            End If


        End If

        If Request.QueryString("SupplierID") IsNot Nothing Then
            SqlDataSourcePTS_1S_ComparisonForGrid.SelectParameters("SupplierID").DefaultValue = Request.QueryString("SupplierID")

        Else
            If Not IsPostBack Then
                SqlDataSourcePTS_1S_ComparisonForGrid.SelectParameters("SupplierID").DefaultValue = 0
            Else
                SqlDataSourcePTS_1S_ComparisonForGrid.SelectParameters("SupplierID").DefaultValue = DropDownListSupplier.SelectedValue
            End If


        End If

        GridViewPTS_1S_Comparison.DataBind()

    End Sub

    Protected Sub CheckBoxListWeeks_SelectedIndexChanged(sender As Object, e As EventArgs)

        GridViewPTS_1S_Comparison.DataBind()

    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit

    End Sub

    Protected Sub Page_PreLoad(sender As Object, e As EventArgs) Handles Me.PreLoad

        If GetPostBackControl.GetPostBackControlId(Page).ToLower = "buttonupdate" Then
            ProcesGridviewMarks()
        End If


    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender



    End Sub

    Protected Sub SqlDataSourcePTS_1S_ComparisonForGrid_Load(sender As Object, e As EventArgs) Handles SqlDataSourcePTS_1S_ComparisonForGrid.Load

    End Sub

    Protected Sub DropDownListSupplier_Load(sender As Object, e As EventArgs) Handles DropDownListSupplier.Load



    End Sub

    Protected Sub DropDownListSupplier_PreRender(sender As Object, e As EventArgs) Handles DropDownListSupplier.PreRender
        If Request.QueryString("SupplierID") IsNot Nothing Then

            DropDownListSupplier.SelectedValue = Request.QueryString("SupplierID")

        End If
    End Sub

    Protected Sub DropDownListProject_PreRender(sender As Object, e As EventArgs) Handles DropDownListProject.PreRender
        If Request.QueryString("ProjectID") IsNot Nothing Then

            DropDownListProject.SelectedValue = Request.QueryString("ProjectID")

        End If

    End Sub

    Protected Sub ButtonExcel_Click(sender As Object, e As EventArgs)

        Session("excelcommand") = "start"

        GridViewPTS_1S_Comparison.DataBind()

        PTSMainClass.ExportGridExcel(GridViewPTS_1S_Comparison, "comparison")

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Protected Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click



    End Sub

    Protected Sub ProcesGridviewMarks()

        For Each gvr As GridViewRow In GridViewPTS_1S_Comparison.Rows
            If gvr.RowType = DataControlRowType.DataRow Then
                Dim CheckBox1S As CheckBox = DirectCast(gvr.FindControl("CheckBox1S"), CheckBox)
                Dim CheckBoxPTS As CheckBox = DirectCast(gvr.FindControl("CheckBoxPTS"), CheckBox)
                Dim Label1S_ID As Label = DirectCast(gvr.FindControl("Label1S_ID"), Label)
                Dim LabelPTS_ID As Label = DirectCast(gvr.FindControl("LabelPTS_ID"), Label)

                If CheckBox1S IsNot Nothing Then
                    If CheckBox1S.Checked = True Then
                        ' Check if already exist in database
                        Using Adapter As New DeliveryTableAdapters.Table_Delivery_MatchingIndexesTableAdapter
                            If Adapter.GetCountOfId("1S", Label1S_ID.Text.Trim) > 0 Then
                                ' do nothing
                            ElseIf Adapter.GetCountOfId("1S", Label1S_ID.Text.Trim) = 0 Then
                                ' insert data
                                'Response.Write("insert" + Adapter.GetCountOfId("1S", Label1S_ID.Text.Trim) + " | ")
                                Adapter.Insert("1S", Label1S_ID.Text.Trim)
                            End If
                            Adapter.Dispose()
                        End Using

                    ElseIf CheckBox1S.Checked = False Then
                        ' Check if already exist in database
                        Using Adapter As New DeliveryTableAdapters.Table_Delivery_MatchingIndexesTableAdapter
                            If Adapter.GetCountOfId("1S", Label1S_ID.Text.Trim) > 0 Then
                                ' delete
                                'Response.Write("delete" + Adapter.GetCountOfId("1S", Label1S_ID.Text.Trim) + " | ")
                                Adapter.DeleteIndex("1S", Label1S_ID.Text.Trim)
                            ElseIf Adapter.GetCountOfId("1S", Label1S_ID.Text.Trim) = 0 Then
                                ' do nothing 
                            End If
                            Adapter.Dispose()
                        End Using

                    End If

                End If

                If CheckBoxPTS IsNot Nothing Then
                    If CheckBoxPTS.Checked = True Then
                        ' Check if already exist in database
                        Using Adapter As New DeliveryTableAdapters.Table_Delivery_MatchingIndexesTableAdapter
                            If Adapter.GetCountOfId("PTS", LabelPTS_ID.Text.Trim) > 0 Then
                                ' do nothing
                            ElseIf Adapter.GetCountOfId("PTS", LabelPTS_ID.Text.Trim) = 0 Then
                                ' insert data
                                'Response.Write("insert" + Adapter.GetCountOfId("PTS", LabelPTS_ID.Text.Trim) + " | ")
                                Adapter.Insert("PTS", LabelPTS_ID.Text.Trim)
                            End If
                            Adapter.Dispose()
                        End Using

                    ElseIf CheckBoxPTS.Checked = False Then
                        ' Check if already exist in database
                        Using Adapter As New DeliveryTableAdapters.Table_Delivery_MatchingIndexesTableAdapter
                            If Adapter.GetCountOfId("PTS", LabelPTS_ID.Text.Trim) > 0 Then
                                ' delete
                                'Response.Write("delete" + Adapter.GetCountOfId("PTS", LabelPTS_ID.Text.Trim) + " | ")
                                Adapter.DeleteIndex("PTS", LabelPTS_ID.Text.Trim)
                            ElseIf Adapter.GetCountOfId("PTS", LabelPTS_ID.Text.Trim) = 0 Then
                                ' do nothing 
                            End If
                            Adapter.Dispose()
                        End Using

                    End If

                End If

            End If
        Next


    End Sub


End Class
