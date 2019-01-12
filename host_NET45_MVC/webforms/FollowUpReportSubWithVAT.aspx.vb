Imports System.Data.SqlClient
Partial Class _Nakl_FollowUpReportDeliveryFinanceFasterSubRCWithVATrev
    Inherits System.Web.UI.Page

    Protected Overrides ReadOnly Property PageStatePersister() As PageStatePersister
        Get
            Return New SessionPageStatePersister(Me)
        End Get
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Or Not IsPostBack Then

            labelCurrency1.Text = "In " + Request.QueryString("Currency")
            labelProjectName.Text = getProjectName(Request.QueryString("ProjectID"))
        End If

        ' it provides query parameter for DropDownListSupplier
        If IsPostBack Or Not IsPostBack Then
            TextBoxUserName.Text = Page.User.Identity.Name
        End If

        If Not IsPostBack Then
            Try
                RenderReport.Render("HTML", ReportViewer_, "_Nakl_FollowUpReportSubWithVATrev", _
                                    "Currency", _
                                    Request.QueryString("Currency").ToString, _
                                    "ProjectID", _
                                    Request.QueryString("ProjectID").ToString, _
                                    "CostCode", _
                                    Request.QueryString("CostCode").ToString)
            Catch ex As Exception
                ' do nothing
            End Try
        End If

    End Sub

    Protected Function getProjectName(ByVal ProjectID As Integer) As String
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SELECT RTRIM(ProjectName) AS ProjectName FROM dbo.Table1_Project WHERE  ProjectID = " + ProjectID.ToString
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim returnProjectName As String = ""
            While dr.Read
                returnProjectName = dr(0).ToString
            End While
            con.Close()
            dr.Close()
            con.Dispose()
            Return returnProjectName

        End Using
    End Function

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        'If Request.QueryString("Currency") = "Ruble" Then
        '  GridViewRubleToExcel.DataSource = SqlDataSourceRubleToExcel
        '  SqlDataSourceRubleToExcel.DataBind()
        '  GridViewRubleToExcel.DataBind()
        '  ExportToExcel("Report.xls", GridViewRubleToExcel)
        'ElseIf Request.QueryString("Currency") = "Dollar" Then
        '  GridViewDollarToExcel.DataSource = SqlDataSourceDollarToExcel
        '  SqlDataSourceDollarToExcel.DataBind()
        '  GridViewDollarToExcel.DataBind()
        '  ExportToExcel("Report.xls", GridViewDollarToExcel)
        'ElseIf Request.QueryString("Currency") = "Euro" Then
        '  GridViewEuroToExcel.DataSource = SqlDataSourceEuroToExcel
        '  SqlDataSourceEuroToExcel.DataBind()
        '  GridViewEuroToExcel.DataBind()
        '  ExportToExcel("Report.xls", GridViewEuroToExcel)
        'End If

        ExportToExcel()

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub

    Private Sub ExportToExcel()

        RenderReport.Render("Excel", ReportViewer_, "_Nakl_FollowUpReportSubWithVATrev", _
                            "Currency", _
                            Request.QueryString("Currency").ToString, _
                            "ProjectID", _
                            Request.QueryString("ProjectID").ToString, _
                            "CostCode", _
                            Request.QueryString("CostCode").ToString)

        Exit Sub

        'Response.Clear()
        'Dim PrjName As String = getProjectName(Request.QueryString("ProjectID"))
        'Dim Currency As String = Request.QueryString("Currency")
        'Dim CostCode As String = Request.QueryString("CostCode")
        'If CostCode = "" Then
        '    Response.AddHeader("content-disposition", "attachment; filename=Report " + PrjName + " in " + Currency + " for ALL Cost Code " + ".xls")

        'Else
        '    Response.AddHeader("content-disposition", "attachment; filename=Report " + PrjName + " in " + Currency + " for Cost Code " + CostCode + ".xls")
        'End If

        'Response.Buffer = True
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.Charset = ""
        'Me.EnableViewState = False
        'Dim oStringWriter As New System.IO.StringWriter
        'Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)

        'If Request.QueryString("Currency") = "Ruble" Then
        '    GridViewRubleToExcel.RenderControl(oHtmlTextWriter)
        'ElseIf Request.QueryString("Currency") = "Dollar" Then
        '    GridViewDollarToExcel.RenderControl(oHtmlTextWriter)
        'ElseIf Request.QueryString("Currency") = "Euro" Then
        '    GridViewEuroToExcel.RenderControl(oHtmlTextWriter)
        'End If

        'Dim zoneId As String = "Russian Standard Time"
        'Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        'Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        'If CostCode = "" Then
        '    Response.Write("<p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span lang=EN-US style='color:red;mso-ansi-language:EN-US'>" + PrjName + " in " + Currency + " for ALL Cost Code " + ", printed by " + String.Format("{0:dddd, MMMM d, yyyy, h:mm tt}", result) + "<o:p></o:p></span></b></p>")
        'Else
        '    Response.Write("<p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span lang=EN-US style='color:red;mso-ansi-language:EN-US'>" + PrjName + " in " + Currency + " for Cost Code " + CostCode + ", printed by " + String.Format("{0:dddd, MMMM d, yyyy, h:mm tt}", result) + "<o:p></o:p></span></b></p>")
        'End If
        'Response.Write(oStringWriter.ToString())
        'Response.[End]()

    End Sub

    Protected Sub ImageButtonOld_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonOld.Click

        'If labelWhichCurrency.Text = "" Then
        'GridViewFollowUpRuble.DataBind()
        'GridViewFollowUpDollar.DataBind()
        'GridViewFollowUpEuro.DataBind()
        'GridViewCostSummaryRuble.DataBind()
        'GridViewCostSummaryDollar.DataBind()
        'GridViewCostSummaryEuro.DataBind()
        'GridViewRubleToExcel.DataBind()
        'GridViewDollarToExcel.DataBind()
        'GridViewEuroToExcel.DataBind()
        'LabelProjectName.Text = "Report For " + DropDownListPrj.SelectedItem.Text + " in " + DropDownListCurrency.SelectedItem.Text
        'labelWhichCurrency.Text = DropDownListCurrency.SelectedItem.Text
        'End If

        If Request.QueryString("Currency") = "Ruble" Then
            GridViewRubleToExcel.DataSource = SqlDataSourceRubleToExcel
            SqlDataSourceRubleToExcel.DataBind()
            GridViewRubleToExcel.DataBind()
            ExportToExcelOld("Report.xls", GridViewRubleToExcel)
        ElseIf Request.QueryString("Currency") = "Dollar" Then
            GridViewDollarToExcel.DataSource = SqlDataSourceDollarToExcel
            SqlDataSourceDollarToExcel.DataBind()
            GridViewDollarToExcel.DataBind()
            ExportToExcelOld("Report.xls", GridViewDollarToExcel)
        ElseIf Request.QueryString("Currency") = "Euro" Then
            GridViewEuroToExcel.DataSource = SqlDataSourceEuroToExcel
            SqlDataSourceEuroToExcel.DataBind()
            GridViewEuroToExcel.DataBind()
            ExportToExcelOld("Report.xls", GridViewEuroToExcel)
        End If

    End Sub

    Private Sub ExportToExcelOld(ByVal strFileName As String, ByVal dg As GridView)

        Response.Clear()
        Response.ClearHeaders()
        Dim PrjName As String = getProjectName(Request.QueryString("ProjectID"))
        Dim Currency As String = Request.QueryString("Currency")
        Dim CostCode As String = Request.QueryString("CostCode")
        If CostCode = "" Then
            Response.AddHeader("content-disposition", "attachment; filename=Report " + PrjName + " in " + Currency + " for ALL Cost Code " + ".xls")

        Else
            Response.AddHeader("content-disposition", "attachment; filename=Report " + PrjName + " in " + Currency + " for Cost Code " + CostCode + ".xls")
        End If

        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        Me.EnableViewState = False
        Dim oStringWriter As New System.IO.StringWriter
        Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)

        If Request.QueryString("Currency") = "Ruble" Then
            GridViewRubleToExcel.RenderControl(oHtmlTextWriter)
        ElseIf Request.QueryString("Currency") = "Dollar" Then
            GridViewDollarToExcel.RenderControl(oHtmlTextWriter)
        ElseIf Request.QueryString("Currency") = "Euro" Then
            GridViewEuroToExcel.RenderControl(oHtmlTextWriter)
        End If

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        If CostCode = "" Then
            Response.Write("<p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span lang=EN-US style='color:red;mso-ansi-language:EN-US'>" + PrjName + " in " + Currency + " for ALL Cost Code " + ", printed by " + String.Format("{0:dddd, MMMM d, yyyy, h:mm tt}", result) + "<o:p></o:p></span></b></p>")
        Else
            Response.Write("<p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span lang=EN-US style='color:red;mso-ansi-language:EN-US'>" + PrjName + " in " + Currency + " for Cost Code " + CostCode + ", printed by " + String.Format("{0:dddd, MMMM d, yyyy, h:mm tt}", result) + "<o:p></o:p></span></b></p>")
        End If
        Response.Write(oStringWriter.ToString())
        Response.[End]()

    End Sub

End Class