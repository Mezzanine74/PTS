Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Partial Class PendinglistRev
    Inherits System.Web.UI.Page

    Dim TotalEuroWithVAT As Decimal = 0.0
    Dim TotalDollarWithVAT As Decimal = 0.0
    Dim TotalRubleWithVAT As Decimal = 0.0
    Dim TotalRubleExcVAT As Decimal = 0.0
    Dim sendemail As New MyCommonTasks
    Dim Notification As New _GiveNotification


    'Protected Overrides ReadOnly Property PageStatePersister() As PageStatePersister
    '  Get
    '    Return New SessionPageStatePersister(Me)
    '  End Get
    'End Property


    Protected Sub GridViewPendingList_Init(sender As Object, e As System.EventArgs) Handles GridViewPendingList.Init
        'EnableViewState = False
    End Sub

    Protected Sub GridViewPendingList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewPendingList.Load
        If Not IsPostBack Then
            'GridViewPendingList.Sort("PO_Currency", SortDirection.Ascending)
            'GridViewPendingList.Sort("EuroWithVAT", SortDirection.Ascending)
        End If
    End Sub

    Protected Sub GridViewPendingList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewPendingList.RowCommand

        If (e.CommandName = "OpenPdf") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
        End If

    End Sub

    Protected Sub GridViewPendingList_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewPendingList.RowCreated


    End Sub

    Protected Sub GridViewPendingList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewPendingList.RowDataBound

        'Dim MyTask As New MyCommonTasks
        'MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        ' highlights rows as per status
        Dim row As GridViewRow


        If e.Row.RowType = DataControlRowType.DataRow Then

            ' check if not edit mode
            'If GridViewPendingApproval.EditIndex = -1 Then
            Dim LblStatus As String = DataBinder.Eval(e.Row.DataItem, "Urgency").ToString
            Dim LblApproval As String = DataBinder.Eval(e.Row.DataItem, "Approved").ToString

            If (LblApproval IsNot Nothing) AndAlso LblApproval = "Not Approved" Then
                'e.Row.BackColor = System.Drawing.Color.White
            End If
            If (LblStatus IsNot Nothing) AndAlso LblStatus = "Urgent" Then
                'row.BackColor = System.Drawing.Color.Yellow
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#87CEEB")
            End If
            If (LblStatus IsNot Nothing) AndAlso LblStatus = "Hold" Then
                'row.BackColor = System.Drawing.Color.Silver
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D7D7D7")
            End If
            If (LblStatus IsNot Nothing) AndAlso LblStatus = "-" Then
                'e.Row.BackColor = System.Drawing.Color.White
            End If
            If (LblApproval IsNot Nothing) AndAlso LblApproval = "Approved" Then
                'row.BackColor = System.Drawing.Color.GreenYellow
                'e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#BFFFBF")
                e.Row.Cells(0).BackColor = System.Drawing.Color.Lime
                e.Row.Cells(0).ForeColor = System.Drawing.Color.Red
                e.Row.Cells(0).Font.Bold = True
            End If
            If (LblApproval IsNot Nothing) AndAlso LblApproval = "Rejected" Then
                'row.BackColor = System.Drawing.Color.OrangeRed
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFB7B7")
            End If
            'End If

            'it defines type of PDF image if it exist or not.
            If e.Row.RowType = DataControlRowType.DataRow Then
                If DataBinder.Eval(e.Row.DataItem, "AttachmentExist") = True Then
                    DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/pdf.bmp"
                Else
                    DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/pdf_bw.bmp"
                End If
            End If

            'it defines CurrencyImage.
            If e.Row.RowType = DataControlRowType.DataRow Then
                If DataBinder.Eval(e.Row.DataItem, "PO_Currency").ToString = "Rub" Then
                    DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/ruble_icon_.bmp"
                ElseIf DataBinder.Eval(e.Row.DataItem, "PO_Currency").ToString = "Dollar" Then
                    DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/dollar_icon_.bmp"
                ElseIf DataBinder.Eval(e.Row.DataItem, "PO_Currency").ToString = "Euro" Then
                    DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/euro_icon_.bmp"
                End If
            End If

        End If

        ' It fixes column width problem
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(3).Text = e.Row.Cells(3).Text.Replace(",", "," + " ")
        End If

        ' it specifies pdf changed or not
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DataBinder.Eval(e.Row.DataItem, "AttachmentChange") = True Then
                Dim LabelAttachmentChanged As Label = DirectCast(e.Row.FindControl("LabelAttachmentChanged"), Label)
                If LabelAttachmentChanged IsNot Nothing Then
                    If Roles.IsUserInRole("Finance") Then
                        LabelAttachmentChanged.Text = "<span style=" + """" + "color: #FFFFFF" + """" + ">Attachment has been changed by </span><span style=" + """" + "color: #FFFFFF; font-size: 12pt; font-weight: bold" + """" + ">" + DataBinder.Eval(e.Row.DataItem, "PersonChangedAttachment") + " </span><span style=" + """" + "color: #FFFFFF" + """" + ">on " + DataBinder.Eval(e.Row.DataItem, "AttachmentChangeWhen") + "</span>"
                        e.Row.Cells(12).BackColor = System.Drawing.Color.Red
                    End If
                End If
            End If
        End If

        ' DE-ACTIVATED, being produced on Back End
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '  Dim LabelOSafterPayment As Literal = DirectCast(e.Row.FindControl("LabelOSafterPayment"), Literal)
        '  If LabelOSafterPayment IsNot Nothing Then
        '    LabelOSafterPayment.Text = String.Format("{0:#,##0.00}", DataBinder.Eval(e.Row.DataItem, "TotalBalanceWithVAT") _
        '      - DataBinder.Eval(e.Row.DataItem, "PendingWithVAT"))
        '  End If
        'End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' provide banned hyperlink
            Dim HyperlinkBannedPo As HyperLink = DirectCast(e.Row.FindControl("HyperlinkBannedPo"), HyperLink)
            If Len(DataBinder.Eval(e.Row.DataItem, "Diff_Paid_Collect").ToString) > 0 _
              AndAlso DataBinder.Eval(e.Row.DataItem, "Diff_Paid_Collect") > 0 Then
                HyperlinkBannedPo.Text = "There is more payment than collected documents!! Click to see details"
                HyperlinkBannedPo.Attributes.Add("onclick", "javascript:w= window.open('PObreakdownByPo.aspx?PO_No=" _
                                               + DataBinder.Eval(e.Row.DataItem, "PO_No").ToString _
                                               + "','Banned_Po','');")
                e.Row.Cells(5).BorderColor = System.Drawing.Color.Red
                e.Row.Cells(5).BorderStyle = BorderStyle.Solid
                e.Row.Cells(5).BorderWidth = 2
                e.Row.Cells(5).BackColor = System.Drawing.Color.Yellow
            Else
                HyperlinkBannedPo.Visible = False
            End If
        End If


    End Sub

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Or Not IsPostBack Then
            If Roles.IsUserInRole("Finance") Then
                DropDownListNumberOfChangedAttachment.DataBind()
                If DropDownListNumberOfChangedAttachment.SelectedValue > 0 Then
                    SpanNumberOfChangedAttachment.Visible = True
                    LiteralAttachmentText.Text = DropDownListNumberOfChangedAttachment.SelectedValue.ToString + " PDF changed!"
                Else
                    SpanNumberOfChangedAttachment.Visible = False
                End If
            End If
        End If

        If IsPostBack Or Not IsPostBack Then
            Dim UpdateExchangeRates As New MyCommonTasks
            UpdateExchangeRates.UpdateExchangeRate()
        End If

        ' Provide total figures

        Dim _SumALLpendingApproved As SumALLpendingApproved = New SumALLpendingApproved
        Dim _SumALLpending As SumALLpending = New SumALLpending

        _SumALLpendingApproved = GetSumAllPendingApproved()
        _SumALLpending = GetSumAllPending()

        LabelTotalEuroWithVATApproved.Text = String.Format("{0:#,##0.00}", _SumALLpendingApproved.EuroWithVAT)
        LabelTotalDollarWithVATApproved.Text = String.Format("{0:#,##0.00}", _SumALLpendingApproved.DollarWithVAT)
        LabelTotalRubleWithVATApproved.Text = String.Format("{0:#,##0.00}", _SumALLpendingApproved.RubleWithVAT)

        LabelTotalEuroWithVAT.Text = String.Format("{0:#,##0.00}", _SumALLpending.EuroWithVAT)
        LabelTotalDollarWithVAT.Text = String.Format("{0:#,##0.00}", _SumALLpending.DollarWithVAT)
        LabelTotalRubleWithVAT.Text = String.Format("{0:#,##0.00}", _SumALLpending.RubleWithVAT)

        _SumALLpendingApproved = Nothing
        _SumALLpending = Nothing

        If Page.User.Identity.Name.ToLower = "elmira.shabaeva" OrElse Page.User.Identity.Name.ToLower = "mariya.podobueva" OrElse Page.User.Identity.Name.ToLower = "natalia.larionova" Then
            GridViewPendingList.Columns(14).Visible = True
        Else
            GridViewPendingList.Columns(14).Visible = False
        End If

    End Sub

    Protected Sub Page_PreRenderComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRenderComplete
    End Sub

    Protected Sub SqlDataSourcePendingList_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourcePendingList.Selecting
        e.Command.CommandTimeout = 60
    End Sub

    Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
        If Not IsPostBack Then
            Dim lst0 As New ListItem("SELECT PROJECT", "-1")
            Dim lst1 As New ListItem("ALL PROJECTS", "0")
            DropDownListPrj.Items.Insert(0, lst0)
            DropDownListPrj.Items.Insert(1, lst1)
        End If

    End Sub

    Protected Sub DropDownListSupplier_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListSupplier.DataBound
        'If Not IsPostBack Then
        '  Dim lst1 As New ListItem("ALL SUPPLIERS", "0")
        '  DropDownListSupplier.Items.Insert(0, lst1)
        'End If
    End Sub

    Protected Function GetSumAllPending() As SumALLpending
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = "SPDataSourceSumALLpending"

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure

            Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
            If Not IsPostBack Then
                ProjectID.Value = Convert.ToInt32(0)
            Else
                Try
                    ProjectID.Value = Convert.ToInt32(DropDownListPrj.SelectedValue.ToString)
                Catch ex As Exception
                    Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Something went wrong. Please refresh page. I am still investigating what cause this error</p>")
                    sendemail.SendEmailToAdmin("pendinglist Error", "DropDownListPrj.SelectedValue:<" + DropDownListPrj.SelectedValue.ToString + ">")
                End Try
            End If

            Dim SupplierID As SqlParameter = cmd.Parameters.Add("@SupplierID", System.Data.SqlDbType.NVarChar)
            If Not IsPostBack Then
                SupplierID.Value = Convert.ToString("0")
            Else
                Try
                    SupplierID.Value = Convert.ToString(DropDownListSupplier.SelectedValue.ToString)
                Catch ex As Exception
                    Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Something went wrong. Please refresh page. I am still investigating what cause this error</p>")
                    sendemail.SendEmailToAdmin("pendinglist Error", "DropDownListSupplier.SelectedValue:<" + DropDownListSupplier.SelectedValue.ToString + ">")
                End Try
            End If

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _SumALLpending As New SumALLpending
            While dr.Read

                If IsDBNull(dr(0)) Then
                    _SumALLpending.DollarWithVAT = 0.0
                Else
                    _SumALLpending.DollarWithVAT = dr(0)
                End If

                If IsDBNull(dr(1)) Then
                    _SumALLpending.EuroWithVAT = 0.0
                Else
                    _SumALLpending.EuroWithVAT = dr(1)
                End If

                If IsDBNull(dr(2)) Then
                    _SumALLpending.RubleWithVAT = 0.0
                Else
                    _SumALLpending.RubleWithVAT = dr(2)
                End If

            End While
            Return _SumALLpending
            con.Close()
            con.Dispose()
            dr.Close()
        End Using

    End Function

    Protected Function GetSumAllPendingApproved() As SumALLpendingApproved

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = "SPDataSourceSumALLpendingApproval"

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure

            Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
            If Not IsPostBack Then
                ProjectID.Value = Convert.ToInt32(0)
            Else
                Try
                    ProjectID.Value = Convert.ToInt32(DropDownListPrj.SelectedValue.ToString)
                Catch ex As Exception
                    Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Something went wrong. Please refresh page. I am still investigating what cause this error</p>")
                    sendemail.SendEmailToAdmin("pendinglist Error", "DropDownListPrj.SelectedValue:<" + DropDownListPrj.SelectedValue.ToString + ">")
                End Try
            End If

            Dim SupplierID As SqlParameter = cmd.Parameters.Add("@SupplierID", System.Data.SqlDbType.NVarChar)
            If Not IsPostBack Then
                SupplierID.Value = Convert.ToString("0")
            Else
                Try
                    SupplierID.Value = Convert.ToString(DropDownListSupplier.SelectedValue.ToString)
                Catch ex As Exception
                    Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Something went wrong. Please refresh page. I am still investigating what cause this error</p>")
                    sendemail.SendEmailToAdmin("pendinglist Error", "DropDownListSupplier.SelectedValue:<" + DropDownListSupplier.SelectedValue.ToString + ">")
                End Try
            End If

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim _SumALLpendingApproved As New SumALLpendingApproved
            While dr.Read

                If IsDBNull(dr(0)) Then
                    _SumALLpendingApproved.DollarWithVAT = 0.0
                Else
                    _SumALLpendingApproved.DollarWithVAT = dr(0)
                End If

                If IsDBNull(dr(1)) Then
                    _SumALLpendingApproved.EuroWithVAT = 0.0
                Else
                    _SumALLpendingApproved.EuroWithVAT = dr(1)
                End If

                If IsDBNull(dr(2)) Then
                    _SumALLpendingApproved.RubleWithVAT = 0.0
                Else
                    _SumALLpendingApproved.RubleWithVAT = dr(2)
                End If

            End While
            Return _SumALLpendingApproved
            con.Close()
            con.Dispose()
            dr.Close()
        End Using
    End Function

    Public Class SumALLpending

        Friend DollarWithVAT As Decimal
        Friend EuroWithVAT As Decimal
        Friend RubleWithVAT As Decimal

    End Class

    Public Class SumALLpendingApproved

        Friend DollarWithVAT As Decimal
        Friend EuroWithVAT As Decimal
        Friend RubleWithVAT As Decimal

    End Class

    Protected Sub ButtonExcel_Click1(sender As Object, e As EventArgs)

        RenderReport.Render("excel", ReportViewerPendingToExcel, _
            "PendingToExcel", "ProjectID", DropDownListPrj.SelectedValue, _
            "SupplierID", DropDownListSupplier.SelectedValue)

    End Sub
End Class
