Imports System.Data.SqlClient
Imports System.IO

Partial Class FollowUpReport
  Inherits System.Web.UI.Page

  Dim POcounter As Integer = 0
  Dim POrowColor As Integer = 0
  Dim POnoHolder As String = ""
  Dim CostCodecounter As Integer = 0
  Dim CostCoderowColor As Integer = 0
  Dim CostCodeHolder As String = ""

  Dim OverallPendingExcVAT As Decimal = 0.0
  Dim OverallPaidExcVAT As Decimal = 0.0
  Dim OverallPoTotalExcVAT As Decimal = 0.0
  Dim VATpaid As Decimal = 0.0
  Dim OutstandingVAT As Decimal = 0.0

  Dim TotalEuroWithVAT As Decimal = 0.0
  Dim TotalDollarWithVAT As Decimal = 0.0
  Dim TotalRubleWithVAT As Decimal = 0.0
  Dim TotalRubleExcVAT As Decimal = 0.0

  Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
    ' it provides Select Project Statement for DDL
    If Not IsPostBack Then
      Dim lst As New ListItem("Select Project", "0")
      DropDownListPrj.Items.Insert(0, lst)
    End If
  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    If IsPostBack Or Not IsPostBack Then
      'ScriptManager1.RegisterPostBackControl(ImageButton1)
    End If





    ' it provides query parameter for DropDownListSupplier
    If IsPostBack Or Not IsPostBack Then
      TextBoxUserName.Text = Page.User.Identity.Name
    End If

    ' it removes SELECT PROJECT after posback
    If IsPostBack Then
      Dim controll1 As New String(Page.Request.Params.Get("__EVENTTARGET"))
      If (Not controll1 Is Nothing) Or (controll1 <> "") Then
        If controll1 = "ctl00$MainContent$DropDownListPrj" Then
          If Me.DropDownListPrj.Items(0).ToString = "Select Project" Then
            Me.DropDownListPrj.Items.RemoveAt(0)
          End If
        End If
      End If
    End If

    ' it will start BackUp for FollowUp Reports.
    If IsPostBack Then
      Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
      con.Open()
      Dim sqlstring As String = "SELECT ProjectID, RTRIM(ProjectName) AS ProjectName FROM [Table1_Project] WHERE BackUpRequired = 1"
      Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
      While dr.Read

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim DateOfBackUp As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " 00:00:00" + "'"
        Dim InstanceOfBackup As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'"


        SqlDataSourceBackUpControl.SelectCommand = "SELECT [BackUPID] FROM [Table_BackUpFUreports] " + _
                                                  " WHERE ProjectID= " + dr(0).ToString + " AND DateOfBackUp = " + DateOfBackUp
        DropDownListBackUpControl.DataBind()

        If DropDownListBackUpControl.Items.Count = 0 Then
          ' it doenst exist, so create Excel backup

          ' it provides criteria for Excel Output
          LabelProjectIDforExcel.Text = dr(0).ToString

          GridViewEuroToExcel.DataSource = SqlDataSourceEuroToExcel
          ' reset global variables before databind
          POcounter = 0
          POrowColor = 0
          POnoHolder = ""
          CostCodecounter = 0
          CostCoderowColor = 0
          CostCodeHolder = ""

          OverallPendingExcVAT = 0.0
          OverallPaidExcVAT = 0.0
          OverallPoTotalExcVAT = 0.0
          VATpaid = 0.0
          OutstandingVAT = 0.0

          GridViewEuroToExcel.DataBind()

          Dim strFilePath As String = String.Empty

          strFilePath = Server.MapPath("~/FollowUpReports/") + dr(1).ToString + " " + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " EURO" + ".xls"
          Dim PathToBackUpFile As String = "'" + "~/FollowUpReports/" + dr(1).ToString + " " + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " EURO" + ".xls" + "'"

          Dim oStringWriter As New System.IO.StringWriter()
          Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(oStringWriter)
          Dim objStreamWriter As StreamWriter = File.AppendText(strFilePath)
          GridViewEuroToExcel.RenderControl(oHtmlTextWriter)
          objStreamWriter.WriteLine(oStringWriter.ToString())
          objStreamWriter.Close()

          ' it is registered to database table 
          SqlDataSourceRegisterToDatabase.InsertCommand = "INSERT INTO [Table_BackUpFUreports] " + _
                                            " ([ProjectID] " + _
                                            " ,[DateOfBackUp] " + _
                                            " ,[InstanceOfBackup] " + _
                                            " ,[ReportCurrency] " + _
                                            " ,[PathToBackUpFile]) " + _
                                            " VALUES (" + dr(0).ToString + "," + DateOfBackUp.ToString + "," + InstanceOfBackup.ToString + "," + "'" + "EURO" + "'" + "," + PathToBackUpFile.ToString + ") "
          SqlDataSourceRegisterToDatabase.Insert()
        Else
          ' Excel copy exist, so resume loop
        End If

      End While
      con.Close()
    End If

    ' it will produce Excel output
    If IsPostBack Then
      Dim zoneId As String = "Russian Standard Time"
      Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
      Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

      Dim DateOfBackUp As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " 00:00:00" + "'"
      Dim InstanceOfBackup As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'"

      SqlDataSourceBackUpControlPendingList.SelectCommand = "SELECT [BackUPID] FROM [Table_BackUpPendingList] " + _
                                                " WHERE DateOfBackUp = " + DateOfBackUp
      DropDownListBackUpControlPendingList.DataBind()

      If DropDownListBackUpControlPendingList.Items.Count = 0 Then
        ' it doenst exist, so create Excel backup

        GridViewPendingListToExcel.DataSource = SqlDataSourcePendingExcel
        ' reset global variables before databind
        'POcounter = 0
        'POrowColor = 0
        'POnoHolder = ""
        'CostCodecounter = 0
        'CostCoderowColor = 0
        'CostCodeHolder = ""

        'OverallPendingExcVAT = 0.0
        'OverallPaidExcVAT = 0.0
        'OverallPoTotalExcVAT = 0.0
        'VATpaid = 0.0
        'OutstandingVAT = 0.0

        GridViewPendingListToExcel.DataBind()

        Dim strFilePathPendingList As String = String.Empty

        strFilePathPendingList = Server.MapPath("~/PendingListBackUp/PendingList") + " " + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + ".xls"
        Dim PathToBackUpFile As String = "'" + "~/PendingListBackUp/PendingList" + " " + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + ".xls" + "'"

        Dim oStringWriter As New System.IO.StringWriter()
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(oStringWriter)
        Dim objStreamWriter As StreamWriter = File.AppendText(strFilePathPendingList)
        GridViewPendingListToExcel.RenderControl(oHtmlTextWriter)
        objStreamWriter.WriteLine(oStringWriter.ToString())
        objStreamWriter.Close()

        ' it is registered to database table 
        SqlDataSourceRegisterToDatabase.InsertCommand = "INSERT INTO [Table_BackUpPendingList] " + _
                                          " ([DateOfBackUp] " + _
                                          " ,[InstanceOfBackup] " + _
                                          " ,[PathToBackUpFile]) " + _
                                          " VALUES (" + DateOfBackUp.ToString + "," + InstanceOfBackup.ToString + "," + PathToBackUpFile.ToString + ") "
        SqlDataSourceRegisterToDatabase.Insert()
      Else
        ' Excel copy exist, no need to pendinglist backup
      End If

    End If


  End Sub

  Protected Sub SqlDataSourceRubleToExcel_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceRubleToExcel.Selecting
    e.Command.CommandTimeout = 120
  End Sub

  Protected Sub SqlDataSourceDollarToExcel_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceDollarToExcel.Selecting
    e.Command.CommandTimeout = 120
  End Sub

  Protected Sub SqlDataSourceEuroToExcel_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceEuroToExcel.Selecting
    e.Command.CommandTimeout = 120
  End Sub

  Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

  End Sub

  Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    Exit Sub
  End Sub

  Protected Sub GridViewRubleToExcel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewRubleToExcel.RowDataBound
    Dim LabelCostCode As Label = DirectCast(e.Row.FindControl("LabelCostCode"), Label)
    Dim LabelPOno As Label = DirectCast(e.Row.FindControl("LabelPO_No"), Label)
    Dim LabelInvoice_No As Label = DirectCast(e.Row.FindControl("LabelInvoice_No"), Label)
    Dim LabelInvoice_Date As Label = DirectCast(e.Row.FindControl("LabelInvoice_Date"), Label)
    Dim LabelInvoiceValue As Label = DirectCast(e.Row.FindControl("LabelInvoiceValue"), Label)
    Dim LabelSiteRecordNo As Label = DirectCast(e.Row.FindControl("LabelSiteRecordNo"), Label)
    Dim LabelPaymentDate As Label = DirectCast(e.Row.FindControl("LabelPaymentDate"), Label)
    Dim LabelRublePendingExcVAT As Label = DirectCast(e.Row.FindControl("LabelRublePendingExcVAT"), Label)
    Dim LabelRublePaidExcVAT As Label = DirectCast(e.Row.FindControl("LabelRublePaidExcVAT"), Label)
    Dim LabelOrderValueRuble As Label = DirectCast(e.Row.FindControl("LabelOrderValueRuble"), Label)
    Dim LabelVATpaidRuble As Label = DirectCast(e.Row.FindControl("LabelVATpaidRuble"), Label)
    Dim LabelOutstandingVATRuble As Label = DirectCast(e.Row.FindControl("LabelOutstandingVATRuble"), Label)
    Dim LabelStatusNote As Label = DirectCast(e.Row.FindControl("LabelStatusNote"), Label)

    If e.Row.RowType = DataControlRowType.Header Then
      Dim i As Integer = 0
      For i = 0 To e.Row.Cells.Count - 1
        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#191970")
                e.Row.Cells(i).ForeColor = System.Drawing.Color.White
            Next
        End If

        If LabelInvoice_No IsNot Nothing Then
            If CostCodecounter = 0 Then
                CostCodeHolder = LabelCostCode.Text
                CostCodecounter = 1
            Else
                If CostCodeHolder <> LabelCostCode.Text Then
                    CostCodeHolder = LabelCostCode.Text
                    CostCoderowColor = CostCoderowColor + 1
                End If
            End If
            If CostCoderowColor Mod 2 = 0 Then
                e.Row.Cells(1).BackColor = System.Drawing.ColorTranslator.FromHtml("#6666FF")
            ElseIf CostCoderowColor Mod 2 = 1 Then
                e.Row.Cells(1).BackColor = System.Drawing.ColorTranslator.FromHtml("#6699FF")
            End If
        End If

        If LabelInvoice_No IsNot Nothing Then
            If POcounter = 0 Then
                POnoHolder = LabelPOno.Text
                POcounter = 1
                POrowColor = 0
            Else
                If POnoHolder <> LabelPOno.Text Then
                    POnoHolder = LabelPOno.Text
                    POrowColor = POrowColor + 1
                End If
            End If
            Dim i As Integer = 0
            For i = 2 To e.Row.Cells.Count - 1
                If POrowColor Mod 2 = 0 Then
                    e.Row.Cells(i).BackColor = System.Drawing.Color.Lavender
                ElseIf POrowColor Mod 2 = 1 Then
                    e.Row.Cells(i).BackColor = System.Drawing.Color.LightSteelBlue
                End If
            Next
        End If

        ' it sums all row figures for footer
        If LabelInvoice_No IsNot Nothing AndAlso LabelInvoice_No.Text <> "" Then
            OverallPendingExcVAT = OverallPendingExcVAT + Convert.ToDecimal(LabelRublePendingExcVAT.Text)
            OverallPaidExcVAT = OverallPaidExcVAT + Convert.ToDecimal(LabelRublePaidExcVAT.Text)
            OverallPoTotalExcVAT = OverallPoTotalExcVAT + Convert.ToDecimal(LabelOrderValueRuble.Text)
            VATpaid = VATpaid + Convert.ToDecimal(LabelVATpaidRuble.Text)
            OutstandingVAT = OutstandingVAT + Convert.ToDecimal(LabelOutstandingVATRuble.Text)
        ElseIf LabelInvoice_No IsNot Nothing AndAlso LabelInvoice_No.Text = "" Then
            OverallPoTotalExcVAT = OverallPoTotalExcVAT + Convert.ToDecimal(LabelOrderValueRuble.Text)
        End If

        Dim LabelFooterTitle As Label = DirectCast(e.Row.FindControl("LabelFooterTitle"), Label)
        Dim LabelRublePendingExcVATtotal As Label = DirectCast(e.Row.FindControl("LabelRublePendingExcVATtotal"), Label)
        Dim LabelRublePaidExcVATtotal As Label = DirectCast(e.Row.FindControl("LabelRublePaidExcVATtotal"), Label)
        Dim LabelOrderValueRubletotal As Label = DirectCast(e.Row.FindControl("LabelOrderValueRubletotal"), Label)
        Dim LabelVATpaidRubletotal As Label = DirectCast(e.Row.FindControl("LabelVATpaidRubletotal"), Label)
        Dim LabelOutstandingVATRubletotal As Label = DirectCast(e.Row.FindControl("LabelOutstandingVATRubletotal"), Label)

        ' it assigns footer values
        If LabelFooterTitle IsNot Nothing Then
            LabelRublePendingExcVATtotal.Text = Convert.ToString(OverallPendingExcVAT)
            LabelRublePaidExcVATtotal.Text = Convert.ToString(OverallPaidExcVAT)
            LabelOrderValueRubletotal.Text = Convert.ToString(OverallPoTotalExcVAT)
            LabelVATpaidRubletotal.Text = Convert.ToString(VATpaid)
            LabelOutstandingVATRubletotal.Text = Convert.ToString(OutstandingVAT)
        End If

        ' it colors footer
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim i As Integer = 0
            For i = 0 To e.Row.Cells.Count - 1
                e.Row.Cells(i).BackColor = System.Drawing.Color.Black
                e.Row.Cells(i).ForeColor = System.Drawing.Color.White
                e.Row.Cells(i).Font.Bold = True
            Next
        End If

        If LabelInvoice_No IsNot Nothing AndAlso LabelInvoice_No.Text = "" Then

            LabelInvoice_No.Text = "Open"
            LabelInvoice_Date.Text = "Open"
            LabelInvoiceValue.Text = "Open"
            LabelSiteRecordNo.Text = "Open"
            LabelPaymentDate.Text = "Open"
            LabelRublePendingExcVAT.Text = "Open"
            LabelRublePaidExcVAT.Text = "Open"
            LabelVATpaidRuble.Text = "Open"
            LabelOutstandingVATRuble.Text = "Open"
            LabelStatusNote.Text = "Open"

            LabelInvoice_No.Font.Italic = True
            LabelInvoice_Date.Font.Italic = True
            LabelInvoiceValue.Font.Italic = True
            LabelSiteRecordNo.Font.Italic = True
            LabelPaymentDate.Font.Italic = True
            LabelRublePendingExcVAT.Font.Italic = True
            LabelRublePaidExcVAT.Font.Italic = True
            LabelVATpaidRuble.Font.Italic = True
            LabelOutstandingVATRuble.Font.Italic = True
            LabelStatusNote.Font.Italic = True

            LabelInvoice_No.ForeColor = System.Drawing.Color.Gray
            LabelInvoice_Date.ForeColor = System.Drawing.Color.Gray
            LabelInvoiceValue.ForeColor = System.Drawing.Color.Gray
            LabelSiteRecordNo.ForeColor = System.Drawing.Color.Gray
            LabelPaymentDate.ForeColor = System.Drawing.Color.Gray
            LabelRublePendingExcVAT.ForeColor = System.Drawing.Color.Gray
            LabelRublePaidExcVAT.ForeColor = System.Drawing.Color.Gray
            LabelOrderValueRuble.Font.Bold = True
            LabelVATpaidRuble.ForeColor = System.Drawing.Color.Gray
            LabelOutstandingVATRuble.ForeColor = System.Drawing.Color.Gray
            LabelStatusNote.ForeColor = System.Drawing.Color.Gray

        End If

    End Sub

    Protected Sub GridViewDollarToExcel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewDollarToExcel.RowDataBound
        Dim LabelCostCode As Label = DirectCast(e.Row.FindControl("LabelCostCode"), Label)
        Dim LabelPOno As Label = DirectCast(e.Row.FindControl("LabelPO_No"), Label)
        Dim LabelInvoice_No As Label = DirectCast(e.Row.FindControl("LabelInvoice_No"), Label)
        Dim LabelInvoice_Date As Label = DirectCast(e.Row.FindControl("LabelInvoice_Date"), Label)
        Dim LabelInvoiceValue As Label = DirectCast(e.Row.FindControl("LabelInvoiceValue"), Label)
        Dim LabelSiteRecordNo As Label = DirectCast(e.Row.FindControl("LabelSiteRecordNo"), Label)
        Dim LabelPaymentDate As Label = DirectCast(e.Row.FindControl("LabelPaymentDate"), Label)
        Dim LabelDollarPendingExcVAT As Label = DirectCast(e.Row.FindControl("LabelDollarPendingExcVAT"), Label)
        Dim LabelDollarPaidExcVAT As Label = DirectCast(e.Row.FindControl("LabelDollarPaidExcVAT"), Label)
        Dim LabelOrderValueDollar As Label = DirectCast(e.Row.FindControl("LabelOrderValueDollar"), Label)
        Dim LabelVATpaidDollar As Label = DirectCast(e.Row.FindControl("LabelVATpaidDollar"), Label)
        Dim LabelOutstandingVATDollar As Label = DirectCast(e.Row.FindControl("LabelOutstandingVATDollar"), Label)
        Dim LabelStatusNote As Label = DirectCast(e.Row.FindControl("LabelStatusNote"), Label)

        If e.Row.RowType = DataControlRowType.Header Then
            Dim i As Integer = 0
            For i = 0 To e.Row.Cells.Count - 1
                e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#191970")
                e.Row.Cells(i).ForeColor = System.Drawing.Color.White
            Next
        End If

        If LabelInvoice_No IsNot Nothing Then
            If CostCodecounter = 0 Then
                CostCodeHolder = LabelCostCode.Text
                CostCodecounter = 1
            Else
                If CostCodeHolder <> LabelCostCode.Text Then
                    CostCodeHolder = LabelCostCode.Text
                    CostCoderowColor = CostCoderowColor + 1
                End If
            End If
            If CostCoderowColor Mod 2 = 0 Then
                e.Row.Cells(1).BackColor = System.Drawing.ColorTranslator.FromHtml("#6666FF")
            ElseIf CostCoderowColor Mod 2 = 1 Then
                e.Row.Cells(1).BackColor = System.Drawing.ColorTranslator.FromHtml("#6699FF")
            End If
        End If

        If LabelInvoice_No IsNot Nothing Then
            If POcounter = 0 Then
                POnoHolder = LabelPOno.Text
                POcounter = 1
                POrowColor = 0
            Else
                If POnoHolder <> LabelPOno.Text Then
                    POnoHolder = LabelPOno.Text
                    POrowColor = POrowColor + 1
                End If
            End If
            Dim i As Integer = 0
            For i = 2 To e.Row.Cells.Count - 1
                If POrowColor Mod 2 = 0 Then
                    e.Row.Cells(i).BackColor = System.Drawing.Color.Lavender
                ElseIf POrowColor Mod 2 = 1 Then
                    e.Row.Cells(i).BackColor = System.Drawing.Color.LightSteelBlue
                End If
            Next
        End If

        ' it sums all row figures for footer
        If LabelInvoice_No IsNot Nothing AndAlso LabelInvoice_No.Text <> "" Then
            OverallPendingExcVAT = OverallPendingExcVAT + Convert.ToDecimal(LabelDollarPendingExcVAT.Text)
            OverallPaidExcVAT = OverallPaidExcVAT + Convert.ToDecimal(LabelDollarPaidExcVAT.Text)
            OverallPoTotalExcVAT = OverallPoTotalExcVAT + Convert.ToDecimal(LabelOrderValueDollar.Text)
            VATpaid = VATpaid + Convert.ToDecimal(LabelVATpaidDollar.Text)
            OutstandingVAT = OutstandingVAT + Convert.ToDecimal(LabelOutstandingVATDollar.Text)
        ElseIf LabelInvoice_No IsNot Nothing AndAlso LabelInvoice_No.Text = "" Then
            OverallPoTotalExcVAT = OverallPoTotalExcVAT + Convert.ToDecimal(LabelOrderValueDollar.Text)
        End If

        Dim LabelFooterTitle As Label = DirectCast(e.Row.FindControl("LabelFooterTitle"), Label)
        Dim LabelDollarPendingExcVATtotal As Label = DirectCast(e.Row.FindControl("LabelDollarPendingExcVATtotal"), Label)
        Dim LabelDollarPaidExcVATtotal As Label = DirectCast(e.Row.FindControl("LabelDollarPaidExcVATtotal"), Label)
        Dim LabelOrderValueDollartotal As Label = DirectCast(e.Row.FindControl("LabelOrderValueDollartotal"), Label)
        Dim LabelVATpaidDollartotal As Label = DirectCast(e.Row.FindControl("LabelVATpaidDollartotal"), Label)
        Dim LabelOutstandingVATDollartotal As Label = DirectCast(e.Row.FindControl("LabelOutstandingVATDollartotal"), Label)

        ' it assigns footer values
        If LabelFooterTitle IsNot Nothing Then
            LabelDollarPendingExcVATtotal.Text = Convert.ToString(OverallPendingExcVAT)
            LabelDollarPaidExcVATtotal.Text = Convert.ToString(OverallPaidExcVAT)
            LabelOrderValueDollartotal.Text = Convert.ToString(OverallPoTotalExcVAT)
            LabelVATpaidDollartotal.Text = Convert.ToString(VATpaid)
            LabelOutstandingVATDollartotal.Text = Convert.ToString(OutstandingVAT)
        End If

        ' it colors footer
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim i As Integer = 0
            For i = 0 To e.Row.Cells.Count - 1
                e.Row.Cells(i).BackColor = System.Drawing.Color.Black
                e.Row.Cells(i).ForeColor = System.Drawing.Color.White
                e.Row.Cells(i).Font.Bold = True
            Next
        End If

        If LabelInvoice_No IsNot Nothing AndAlso LabelInvoice_No.Text = "" Then
            LabelInvoice_No.Text = "Open"
            LabelInvoice_Date.Text = "Open"
            LabelInvoiceValue.Text = "Open"
            LabelSiteRecordNo.Text = "Open"
            LabelPaymentDate.Text = "Open"
            LabelDollarPendingExcVAT.Text = "Open"
            LabelDollarPaidExcVAT.Text = "Open"
            LabelVATpaidDollar.Text = "Open"
            LabelOutstandingVATDollar.Text = "Open"
            LabelStatusNote.Text = "Open"

            LabelInvoice_No.Font.Italic = True
            LabelInvoice_Date.Font.Italic = True
            LabelInvoiceValue.Font.Italic = True
            LabelSiteRecordNo.Font.Italic = True
            LabelPaymentDate.Font.Italic = True
            LabelDollarPendingExcVAT.Font.Italic = True
            LabelDollarPaidExcVAT.Font.Italic = True
            LabelVATpaidDollar.Font.Italic = True
            LabelOutstandingVATDollar.Font.Italic = True
            LabelStatusNote.Font.Italic = True

            LabelInvoice_No.ForeColor = System.Drawing.Color.Gray
            LabelInvoice_Date.ForeColor = System.Drawing.Color.Gray
            LabelInvoiceValue.ForeColor = System.Drawing.Color.Gray
            LabelSiteRecordNo.ForeColor = System.Drawing.Color.Gray
            LabelPaymentDate.ForeColor = System.Drawing.Color.Gray
            LabelDollarPendingExcVAT.ForeColor = System.Drawing.Color.Gray
            LabelDollarPaidExcVAT.ForeColor = System.Drawing.Color.Gray
            LabelOrderValueDollar.Font.Bold = True
            LabelVATpaidDollar.ForeColor = System.Drawing.Color.Gray
            LabelOutstandingVATDollar.ForeColor = System.Drawing.Color.Gray
            LabelStatusNote.ForeColor = System.Drawing.Color.Gray

        End If
    End Sub

    Protected Sub GridViewEuroToExcel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewEuroToExcel.RowDataBound
        Dim LabelCostCode As Label = DirectCast(e.Row.FindControl("LabelCostCode"), Label)
        Dim LabelPOno As Label = DirectCast(e.Row.FindControl("LabelPO_No"), Label)
        Dim LabelInvoice_No As Label = DirectCast(e.Row.FindControl("LabelInvoice_No"), Label)
        Dim LabelInvoice_Date As Label = DirectCast(e.Row.FindControl("LabelInvoice_Date"), Label)
        Dim LabelInvoiceValue As Label = DirectCast(e.Row.FindControl("LabelInvoiceValue"), Label)
        Dim LabelSiteRecordNo As Label = DirectCast(e.Row.FindControl("LabelSiteRecordNo"), Label)
        Dim LabelPaymentDate As Label = DirectCast(e.Row.FindControl("LabelPaymentDate"), Label)
        Dim LabelEuroPendingExcVAT As Label = DirectCast(e.Row.FindControl("LabelEuroPendingExcVAT"), Label)
        Dim LabelEuroPaidExcVAT As Label = DirectCast(e.Row.FindControl("LabelEuroPaidExcVAT"), Label)
        Dim LabelOrderValueEuro As Label = DirectCast(e.Row.FindControl("LabelOrderValueEuro"), Label)
        Dim LabelVATpaidEuro As Label = DirectCast(e.Row.FindControl("LabelVATpaidEuro"), Label)
        Dim LabelOutstandingVATEuro As Label = DirectCast(e.Row.FindControl("LabelOutstandingVATEuro"), Label)
        Dim LabelStatusNote As Label = DirectCast(e.Row.FindControl("LabelStatusNote"), Label)

        If e.Row.RowType = DataControlRowType.Header Then
            Dim i As Integer = 0
            For i = 0 To e.Row.Cells.Count - 1
                e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#191970")
                e.Row.Cells(i).ForeColor = System.Drawing.Color.White
            Next
        End If

        If LabelInvoice_No IsNot Nothing Then
            If CostCodecounter = 0 Then
                CostCodeHolder = LabelCostCode.Text
                CostCodecounter = 1
            Else
                If CostCodeHolder <> LabelCostCode.Text Then
                    CostCodeHolder = LabelCostCode.Text
                    CostCoderowColor = CostCoderowColor + 1
                End If
            End If
            If CostCoderowColor Mod 2 = 0 Then
                e.Row.Cells(1).BackColor = System.Drawing.ColorTranslator.FromHtml("#6666FF")
            ElseIf CostCoderowColor Mod 2 = 1 Then
                e.Row.Cells(1).BackColor = System.Drawing.ColorTranslator.FromHtml("#6699FF")
            End If
        End If

        If LabelInvoice_No IsNot Nothing Then
            If POcounter = 0 Then
                POnoHolder = LabelPOno.Text
                POcounter = 1
                POrowColor = 0
            Else
                If POnoHolder <> LabelPOno.Text Then
                    POnoHolder = LabelPOno.Text
                    POrowColor = POrowColor + 1
                End If
            End If
            Dim i As Integer = 0
            For i = 2 To e.Row.Cells.Count - 1
                If POrowColor Mod 2 = 0 Then
                    e.Row.Cells(i).BackColor = System.Drawing.Color.Lavender
                ElseIf POrowColor Mod 2 = 1 Then
                    e.Row.Cells(i).BackColor = System.Drawing.Color.LightSteelBlue
                End If
            Next
        End If

        ' it sums all row figures for footer
        If LabelInvoice_No IsNot Nothing AndAlso LabelInvoice_No.Text <> "" Then
            OverallPendingExcVAT = OverallPendingExcVAT + Convert.ToDecimal(LabelEuroPendingExcVAT.Text)
            OverallPaidExcVAT = OverallPaidExcVAT + Convert.ToDecimal(LabelEuroPaidExcVAT.Text)
            OverallPoTotalExcVAT = OverallPoTotalExcVAT + Convert.ToDecimal(LabelOrderValueEuro.Text)
            VATpaid = VATpaid + Convert.ToDecimal(LabelVATpaidEuro.Text)
            OutstandingVAT = OutstandingVAT + Convert.ToDecimal(LabelOutstandingVATEuro.Text)
        ElseIf LabelInvoice_No IsNot Nothing AndAlso LabelInvoice_No.Text = "" Then
            OverallPoTotalExcVAT = OverallPoTotalExcVAT + Convert.ToDecimal(LabelOrderValueEuro.Text)
        End If

        Dim LabelFooterTitle As Label = DirectCast(e.Row.FindControl("LabelFooterTitle"), Label)
        Dim LabelEuroPendingExcVATtotal As Label = DirectCast(e.Row.FindControl("LabelEuroPendingExcVATtotal"), Label)
        Dim LabelEuroPaidExcVATtotal As Label = DirectCast(e.Row.FindControl("LabelEuroPaidExcVATtotal"), Label)
        Dim LabelOrderValueEurototal As Label = DirectCast(e.Row.FindControl("LabelOrderValueEurototal"), Label)
        Dim LabelVATpaidEurototal As Label = DirectCast(e.Row.FindControl("LabelVATpaidEurototal"), Label)
        Dim LabelOutstandingVATEurototal As Label = DirectCast(e.Row.FindControl("LabelOutstandingVATEurototal"), Label)

        ' it assigns footer values
        If LabelFooterTitle IsNot Nothing Then
            LabelEuroPendingExcVATtotal.Text = Convert.ToString(OverallPendingExcVAT)
            LabelEuroPaidExcVATtotal.Text = Convert.ToString(OverallPaidExcVAT)
            LabelOrderValueEurototal.Text = Convert.ToString(OverallPoTotalExcVAT)
            LabelVATpaidEurototal.Text = Convert.ToString(VATpaid)
            LabelOutstandingVATEurototal.Text = Convert.ToString(OutstandingVAT)
        End If

        ' it colors footer
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim i As Integer = 0
            For i = 0 To e.Row.Cells.Count - 1
                e.Row.Cells(i).BackColor = System.Drawing.Color.Black
                e.Row.Cells(i).ForeColor = System.Drawing.Color.White
                e.Row.Cells(i).Font.Bold = True
            Next
        End If

        If LabelInvoice_No IsNot Nothing AndAlso LabelInvoice_No.Text = "" Then
            LabelInvoice_No.Text = "Open"
            LabelInvoice_Date.Text = "Open"
            LabelInvoiceValue.Text = "Open"
            LabelSiteRecordNo.Text = "Open"
            LabelPaymentDate.Text = "Open"
            LabelEuroPendingExcVAT.Text = "Open"
            LabelEuroPaidExcVAT.Text = "Open"
            LabelVATpaidEuro.Text = "Open"
            LabelOutstandingVATEuro.Text = "Open"
            LabelStatusNote.Text = "Open"

            LabelInvoice_No.Font.Italic = True
            LabelInvoice_Date.Font.Italic = True
            LabelInvoiceValue.Font.Italic = True
            LabelSiteRecordNo.Font.Italic = True
            LabelPaymentDate.Font.Italic = True
            LabelEuroPendingExcVAT.Font.Italic = True
            LabelEuroPaidExcVAT.Font.Italic = True
            LabelVATpaidEuro.Font.Italic = True
            LabelOutstandingVATEuro.Font.Italic = True
            LabelStatusNote.Font.Italic = True

            LabelInvoice_No.ForeColor = System.Drawing.Color.Gray
            LabelInvoice_Date.ForeColor = System.Drawing.Color.Gray
            LabelInvoiceValue.ForeColor = System.Drawing.Color.Gray
            LabelSiteRecordNo.ForeColor = System.Drawing.Color.Gray
            LabelPaymentDate.ForeColor = System.Drawing.Color.Gray
            LabelEuroPendingExcVAT.ForeColor = System.Drawing.Color.Gray
            LabelEuroPaidExcVAT.ForeColor = System.Drawing.Color.Gray
            LabelOrderValueEuro.Font.Bold = True
            LabelVATpaidEuro.ForeColor = System.Drawing.Color.Gray
            LabelOutstandingVATEuro.ForeColor = System.Drawing.Color.Gray
            LabelStatusNote.ForeColor = System.Drawing.Color.Gray

        End If
    End Sub

    Protected Sub GridViewPendingListToExcel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewPendingListToExcel.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            Dim i As Integer = 0
            For i = 0 To e.Row.Cells.Count - 1
                e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#191970")
                e.Row.Cells(i).ForeColor = System.Drawing.Color.White
            Next
        End If

        Dim LabelApproved_ As Label = DirectCast(e.Row.FindControl("LabelApproved_"), Label)
        Dim LabelStatus As Label = DirectCast(e.Row.FindControl("LabelStatus"), Label)

        If LabelStatus IsNot Nothing Then
            If LabelStatus.Text = "Urgent" Then
                Dim i As Integer = 0
                For i = 0 To e.Row.Cells.Count - 1
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#87CEEB")
                Next
            ElseIf LabelStatus.Text = "Hold" Then
                Dim i As Integer = 0
                For i = 0 To e.Row.Cells.Count - 1
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#D7D7D7")
                Next
            End If
        End If

        If LabelApproved_ IsNot Nothing Then
            If LabelApproved_.Text = "Approved" Then
                e.Row.Cells(0).BackColor = System.Drawing.Color.Lime
                e.Row.Cells(0).ForeColor = System.Drawing.Color.Red
                e.Row.Cells(0).Font.Bold = True
            End If
        End If

        'it sums for footer
        Dim LabelEuroWithVAT As Label = DirectCast(e.Row.FindControl("LabelEuroWithVAT"), Label)
        Dim LabelDollarWithVAT As Label = DirectCast(e.Row.FindControl("LabelDollarWithVAT"), Label)
        Dim LabelRubleWithVAT As Label = DirectCast(e.Row.FindControl("LabelRubleWithVAT"), Label)
        Dim LabelRubleExcVAT As Label = DirectCast(e.Row.FindControl("LabelRubleExcVAT"), Label)

        If LabelEuroWithVAT IsNot Nothing Then
            TotalEuroWithVAT = TotalEuroWithVAT + Convert.ToDecimal(LabelEuroWithVAT.Text)
            TotalDollarWithVAT = TotalDollarWithVAT + Convert.ToDecimal(LabelDollarWithVAT.Text)
            TotalRubleWithVAT = TotalRubleWithVAT + Convert.ToDecimal(LabelRubleWithVAT.Text)
            TotalRubleExcVAT = TotalRubleExcVAT + Convert.ToDecimal(LabelRubleExcVAT.Text)
        End If

        Dim LabelEuroWithVATFooter As Label = DirectCast(e.Row.FindControl("LabelEuroWithVATFooter"), Label)
        Dim LabelDollarWithVATFooter As Label = DirectCast(e.Row.FindControl("LabelDollarWithVATFooter"), Label)
        Dim LabelRubleWithVATFooter As Label = DirectCast(e.Row.FindControl("LabelRubleWithVATFooter"), Label)
        Dim LabelRubleExcVATFooter As Label = DirectCast(e.Row.FindControl("LabelRubleExcVATFooter"), Label)

        If e.Row.RowType = DataControlRowType.Footer Then
            LabelEuroWithVATFooter.Text = TotalEuroWithVAT.ToString
            LabelDollarWithVATFooter.Text = TotalDollarWithVAT.ToString
            LabelRubleWithVATFooter.Text = TotalRubleWithVAT.ToString
            LabelRubleExcVATFooter.Text = TotalRubleExcVAT.ToString

            Dim i As Integer = 0
            For i = 0 To e.Row.Cells.Count - 1
                e.Row.Cells(i).BackColor = System.Drawing.Color.Black
                e.Row.Cells(i).ForeColor = System.Drawing.Color.White
                e.Row.Cells(i).Font.Bold = True
      Next
    End If

  End Sub
End Class

