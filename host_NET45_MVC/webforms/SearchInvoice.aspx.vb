Partial Class SearchInvoice
  Inherits System.Web.UI.Page

  Dim FirstParameterNameOfDescription As String = ""

  Dim StartStatus As String = "Start"
  Dim FlyBy1 As String = ""

  Dim StartStatusCostCode As String = "Start"
  Dim FlyBy1CostCode As String = ""

  Dim i As Integer = 0
  Dim StringBuilder_DescriptionSQL As String = ""
  Dim StringBuilderForSupplierType As String = ""
  Dim StringBuilderForInvoiceNo As String = ""
  Dim StringBuilderForPayReqNo As String = ""
  Dim StringBuilderForFinanceNo As String = ""

  Dim iCostcode As Integer = 0
  Dim StringBuilderForCostcode As String = ""

  Dim StartPosition As Integer
  Dim EndPosition As Integer
  Dim OccurenceOfSpace As Integer
  Dim sayac As Integer = 0
  Dim DonguBiter As Boolean = False

  Dim StartPosition2 As Integer
  Dim EndPosition2 As Integer
  Dim OccurenceOfSpace2 As Integer
  Dim sayac2 As Integer = 0
  Dim DonguBiter2 As Boolean = False

  Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Me.Form.DefaultButton = Me.ButtonSearch.UniqueID
    If Not IsPostBack Then
      Session.RemoveAll()
    End If
  End Sub


  Protected Sub GridViewMonitor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewMonitor.Load
    If IsPostBack Then
      SqlDataSourceMonitor.SelectParameters.Clear()
      SqlDataSourceMonitor.SelectCommand = " SELECT      " + _
                                                                          " PO_No " + _
                                                                          " ,Description " + _
                                                                          " ,POtotalprice " + _
                                                                          " ,VATpercent " + _
                                                                          " ,PO_Currency " + _
                                                                          " ,PO_Date " + _
                                                                          " ,Invoice_No " + _
                                                                          " ,Invoice_Date " + _
                                                                          " ,Invoice_value " + _
                                                                          " ,SiteRecordNo " + _
                                                                          " ,PayReqDate " + _
                                                                          " ,LinkToInvoice " + _
                                                                          " ,Urgency " + _
                                                                          " ,PersonApprove " + _
                                                                          " ,FinanceNo " + _
                                                                          " ,PaymentDate " + _
                                                                          " ,Payment_amount " + _
                                                                          " ,Payment_currency " + _
                                                                          " ,ProjectID " + _
                                                                          " ,SupplierID " + _
                                                                          " ,CostCode " + _
                                                                          " ,SupplierName " + _
                                                                          " ,PrjNumberName " + _
                                                                          " ,SupplierNumberName " + _
                                                                          " ,ProjectName " + _
                                                                          " ,AttachmentExist " + _
                                                                          " ,CostCode_ " + _
                                                                          " FROM   dbo.View_SearchingInvoice "

      StringBuilder_DescriptionSQL = " HAVING Description LIKE '%' + @"

      '..........1
      ' Define DESCRIPTION SQL parameter
      While DonguBiter = False

        If sayac = 0 Then
          StartPosition = 0
        Else
          StartPosition = OccurenceOfSpace
        End If

        EndPosition = InStr(StartPosition + 1, Trim(TextBoxSearch.Text), " ")
        OccurenceOfSpace = EndPosition
        sayac = sayac + 1

        If EndPosition = 0 Then
          Dim DescriptionParameter_Name_FirstLoop As String = Mid(Trim(TextBoxSearch.Text), StartPosition + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition).ToString + sayac.ToString
          Dim DescriptionParameter_Value_FirstLoop As String = Mid(Trim(TextBoxSearch.Text), StartPosition + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition).ToString
          If sayac = 1 Then
            SqlDataSourceMonitor.SelectParameters.Add(DescriptionParameter_Name_FirstLoop, TypeCode.String.ToString())
            SqlDataSourceMonitor.SelectParameters(DescriptionParameter_Name_FirstLoop).DefaultValue = DescriptionParameter_Value_FirstLoop
            SqlDataSourceMonitor.SelectParameters(DescriptionParameter_Name_FirstLoop).Type = TypeCode.String
            FirstParameterNameOfDescription = DescriptionParameter_Name_FirstLoop
            StringBuilder_DescriptionSQL = StringBuilder_DescriptionSQL + DescriptionParameter_Name_FirstLoop + " + '%'"
          Else
            SqlDataSourceMonitor.SelectParameters.Add(DescriptionParameter_Name_FirstLoop, TypeCode.String.ToString())
            SqlDataSourceMonitor.SelectParameters(DescriptionParameter_Name_FirstLoop).DefaultValue = DescriptionParameter_Value_FirstLoop
            SqlDataSourceMonitor.SelectParameters(DescriptionParameter_Name_FirstLoop).Type = TypeCode.String
            StringBuilder_DescriptionSQL = StringBuilder_DescriptionSQL + " AND Description LIKE '%' + @" + DescriptionParameter_Name_FirstLoop + " + '%'"
          End If
          DonguBiter = True
        Else
          Dim DescriptionParameter_Name_OtherLoops As String = Mid(Trim(TextBoxSearch.Text), StartPosition + 1, EndPosition - StartPosition - 1).ToString + sayac.ToString
          Dim DescriptionParameter_Value_OtherLoops As String = Mid(Trim(TextBoxSearch.Text), StartPosition + 1, EndPosition - StartPosition - 1).ToString
          If sayac = 1 Then
            SqlDataSourceMonitor.SelectParameters.Add(DescriptionParameter_Name_OtherLoops, TypeCode.String.ToString())
            SqlDataSourceMonitor.SelectParameters(DescriptionParameter_Name_OtherLoops).DefaultValue = DescriptionParameter_Value_OtherLoops
            SqlDataSourceMonitor.SelectParameters(DescriptionParameter_Name_OtherLoops).Type = TypeCode.String
            StringBuilder_DescriptionSQL = StringBuilder_DescriptionSQL + DescriptionParameter_Name_OtherLoops + " + '%'"
          Else
            SqlDataSourceMonitor.SelectParameters.Add(DescriptionParameter_Name_OtherLoops, TypeCode.String.ToString())
            SqlDataSourceMonitor.SelectParameters(DescriptionParameter_Name_OtherLoops).DefaultValue = DescriptionParameter_Value_OtherLoops
            SqlDataSourceMonitor.SelectParameters(DescriptionParameter_Name_OtherLoops).Type = TypeCode.String
            StringBuilder_DescriptionSQL = StringBuilder_DescriptionSQL + " AND Description LIKE '%' + @" + DescriptionParameter_Name_OtherLoops + " + '%'"
          End If
        End If
      End While

      '..........2
      ' Define SUPPLIER TYPE SQL parameter
      Dim li As DataListItem
      For Each li In DataListSupplierType.Items
        Dim CheckBoxSupplierType As CheckBox = DirectCast(li.FindControl("CheckBoxSupplierType"), CheckBox)
        If CheckBoxSupplierType.Checked = True Then
          ' open session as SupplierTypeID and assign value 1
          Session(DataListSupplierType.DataKeys(li.ItemIndex).ToString) = 1
          If i = 0 Then
            StringBuilderForSupplierType = " WHERE (SupplierTypeId IN (" + DataListSupplierType.DataKeys(li.ItemIndex).ToString
          Else
            StringBuilderForSupplierType = StringBuilderForSupplierType + "," + DataListSupplierType.DataKeys(li.ItemIndex).ToString
          End If
          i = i + 1
        ElseIf CheckBoxSupplierType.Checked = False AndAlso Session(DataListSupplierType.DataKeys(li.ItemIndex).ToString) IsNot Nothing Then
          Session(DataListSupplierType.DataKeys(li.ItemIndex).ToString) = 0
        End If
      Next

      StringBuilderForSupplierType = StringBuilderForSupplierType + "))"
      If StringBuilderForSupplierType = "))" Then
        StringBuilderForSupplierType = ""
      End If

      '..........3
      ' Define COST CODE SQL parameter
      Dim liCostCode As DataListItem
      For Each liCostCode In DataListCostCode.Items
        Dim CheckBoxCostCode As CheckBox = DirectCast(liCostCode.FindControl("CheckBoxCostCode"), CheckBox)
        If CheckBoxCostCode.Checked = True Then
          ' open session as SupplierTypeID and assign value 1
          Session(DataListCostCode.DataKeys(liCostCode.ItemIndex).ToString) = 1
          If iCostcode = 0 Then
            StringBuilderForCostcode = " (CostCode_ IN (" + "N'" + DataListCostCode.DataKeys(liCostCode.ItemIndex).ToString + "'"
          Else
            StringBuilderForCostcode = StringBuilderForCostcode + "," + "N'" + DataListCostCode.DataKeys(liCostCode.ItemIndex).ToString + "'"
          End If
          iCostcode = iCostcode + 1
        ElseIf CheckBoxCostCode.Checked = False AndAlso Session(DataListCostCode.DataKeys(liCostCode.ItemIndex).ToString) IsNot Nothing Then
          Session(DataListCostCode.DataKeys(liCostCode.ItemIndex).ToString) = 0
        End If
      Next

      StringBuilderForCostcode = StringBuilderForCostcode + "))"
      If StringBuilderForCostcode = "))" Then
        StringBuilderForCostcode = ""
      End If

      '..........4
      ' Define INVOICE NO SQL parameter
      If Not String.IsNullOrEmpty(TextBoxInvoiceNo.Text) Then
        SqlDataSourceMonitor.SelectParameters.Add("Invoice_No_FromTextBox", TypeCode.String.ToString())
        SqlDataSourceMonitor.SelectParameters("Invoice_No_FromTextBox").DefaultValue = TextBoxInvoiceNo.Text
        StringBuilderForInvoiceNo = " (Invoice_No LIKE '%' + @" + "Invoice_No_FromTextBox" + " + '%'" + " ) "
      End If

      '..........5
      ' Define PAY_REQ_NO SQL parameter
      If Not String.IsNullOrEmpty(TextBoxSiteRecordNo.Text) Then
        SqlDataSourceMonitor.SelectParameters.Add("PayReqNo_FromTextBox", TypeCode.String.ToString())
        SqlDataSourceMonitor.SelectParameters("PayReqNo_FromTextBox").DefaultValue = TextBoxSiteRecordNo.Text
        StringBuilderForPayReqNo = " (SiteRecordNo LIKE '%' + @" + "PayReqNo_FromTextBox" + " + '%'" + " ) "
      End If

      '..........6
      ' Define FINANCE_NO SQL parameter
      If Not String.IsNullOrEmpty(TextBoxFinanceNo.Text) Then
        SqlDataSourceMonitor.SelectParameters.Add("FinanceNo_FromTextBox", TypeCode.String.ToString())
        SqlDataSourceMonitor.SelectParameters("FinanceNo_FromTextBox").DefaultValue = TextBoxFinanceNo.Text
        StringBuilderForFinanceNo = " (FinanceNo LIKE '%' + @" + "FinanceNo_FromTextBox" + " + '%'" + " ) "
      End If

      ' Dont convert EmptyString to NULL for Description textbox if at least one criteria provided.
      If String.IsNullOrEmpty(TextBoxSearch.Text) AndAlso _
        ( _
          Not String.IsNullOrEmpty(StringBuilderForSupplierType) _
          OrElse Not String.IsNullOrEmpty(StringBuilderForCostcode) _
          OrElse Not String.IsNullOrEmpty(StringBuilderForInvoiceNo) _
          OrElse Not String.IsNullOrEmpty(StringBuilderForPayReqNo) _
          OrElse Not String.IsNullOrEmpty(StringBuilderForFinanceNo) _
          ) Then
        SqlDataSourceMonitor.SelectParameters(FirstParameterNameOfDescription).ConvertEmptyStringToNull = False
        'Response.Write("SqlDataSourceMonitor.SelectParameters(FirstParameterNameOfDescription).ConvertEmptyStringToNull = False")
      End If

      ' Add all parameters together
      If Not String.IsNullOrEmpty(StringBuilderForSupplierType) Then
        SqlDataSourceMonitor.SelectCommand = SqlDataSourceMonitor.SelectCommand + StringBuilderForSupplierType
      End If

            SqlDataSourceMonitor.SelectCommand = SqlDataSourceMonitor.SelectCommand + _
                                                                                                                                              " GROUP BY  " + _
                                                                                                                                              " PO_No " + _
                                                                                                                                              " ,Description " + _
                                                                                                                                              " ,POtotalprice " + _
                                                                                                                                              " ,VATpercent " + _
                                                                                                                                              " ,PO_Currency " + _
                                                                                                                                              " ,PO_Date " + _
                                                                                                                                              " ,Invoice_No " + _
                                                                                                                                              " ,Invoice_Date " + _
                                                                                                                                              " ,Invoice_value " + _
                                                                                                                                              " ,SiteRecordNo " + _
                                                                                                                                              " ,PayReqDate " + _
                                                                                                                                              " ,LinkToInvoice " + _
                                                                                                                                              " ,Urgency " + _
                                                                                                                                              " ,PersonApprove " + _
                                                                                                                                              " ,FinanceNo " + _
                                                                                                                                              " ,PaymentDate " + _
                                                                                                                                              " ,Payment_amount " + _
                                                                                                                                              " ,Payment_currency " + _
                                                                                                                                              " ,ProjectID " + _
                                                                                                                                              " ,SupplierID " + _
                                                                                                                                              " ,CostCode " + _
                                                                                                                                              " ,SupplierName " + _
                                                                                                                                              " ,PrjNumberName " + _
                                                                                                                                              " ,SupplierNumberName " + _
                                                                                                                                              " ,ProjectName " + _
                                                                                                                                              " ,AttachmentExist " + _
                                                                                                                                              " ,CostCode_ "

      SqlDataSourceMonitor.SelectCommand = SqlDataSourceMonitor.SelectCommand + StringBuilder_DescriptionSQL

      If Not String.IsNullOrEmpty(StringBuilderForCostcode) Then
        SqlDataSourceMonitor.SelectCommand = SqlDataSourceMonitor.SelectCommand + " AND " + StringBuilderForCostcode
      End If
      If Not String.IsNullOrEmpty(StringBuilderForInvoiceNo) Then
        SqlDataSourceMonitor.SelectCommand = SqlDataSourceMonitor.SelectCommand + " AND " + StringBuilderForInvoiceNo
      End If
      If Not String.IsNullOrEmpty(StringBuilderForPayReqNo) Then
        SqlDataSourceMonitor.SelectCommand = SqlDataSourceMonitor.SelectCommand + " AND " + StringBuilderForPayReqNo
      End If
      If Not String.IsNullOrEmpty(StringBuilderForFinanceNo) Then
        SqlDataSourceMonitor.SelectCommand = SqlDataSourceMonitor.SelectCommand + " AND " + StringBuilderForFinanceNo
      End If

      'Response.Write(SqlDataSourceMonitor.SelectCommand)
    End If
  End Sub

  Protected Sub GridViewMonitor_PreRender(sender As Object, e As System.EventArgs) Handles GridViewMonitor.PreRender

  End Sub


  Protected Sub GridViewMonitor_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewMonitor.RowCommand
    If (e.CommandName = "OpenPdf") Then
      ' Retrieve the row index stored in the CommandArgument property.
      Dim index As Integer = Convert.ToInt32(e.CommandArgument)

      ' Retrieve the row that contains the button 
      ' from the Rows collection.
      Dim row As GridViewRow = GridViewMonitor.Rows(index)

      ' Add code here to add the item to the shopping cart.

      Dim LinkInvoice As Label = DirectCast(row.FindControl("LabelLinkInvoice"), Label)

      ' It reloads files
      Dim path As String = Server.MapPath(LinkInvoice.Text)
      Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)

      If file.Exists Then
        Response.Clear()
        Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
        Response.AddHeader("Content-Length", file.Length.ToString())
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(file.FullName)
        Response.End()
      End If

    End If

  End Sub

  Protected Sub GridViewMonitor_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewMonitor.RowCreated

  End Sub

  Protected Sub GridViewMonitor_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewMonitor.RowDataBound


    Dim MyTask As New MyCommonTasks
    MyTask.HoverEffectOnGridviewCells(sender, e.Row)

    If e.Row.RowType = DataControlRowType.EmptyDataRow Then
      If Not IsPostBack Then
        e.Row.Visible = False
      End If
    End If

    'it defines type of PDF image if it exist or not.
    If DirectCast(e.Row.FindControl("LabelLinkInvoice"), Label) IsNot Nothing Then
      Dim path As String = Server.MapPath(DirectCast(e.Row.FindControl("LabelLinkInvoice"), Label).Text.ToString)
      Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
      If file.Exists Then
        DirectCast(e.Row.FindControl("ImageButtonPdf"), ImageButton).ImageUrl = "~/Images/pdf.bmp"
      Else
        DirectCast(e.Row.FindControl("ImageButtonPdf"), ImageButton).ImageUrl = "~/Images/pdf_bw.bmp"
      End If
    End If


    'Select Case TextBox1.Text
    '    Case "a"
    'Label1.text = "You entered A"
    '    Case "e"
    'Label1.Text = "You entered E"
    '    Case "i"
    'Label1.Text = "You entered I"
    '    Case "o"
    'Label1.Text = "You entered O"
    '    Case "u"
    'Label1.Text = "You entered U"
    'End Select

    '************

    '*************
    ' PASTE LATER HERE
    Dim LabelDescription As Label = DirectCast(e.Row.FindControl("LabelDescription"), Label)
    If LabelDescription IsNot Nothing AndAlso Not String.IsNullOrEmpty(TextBoxSearch.Text) Then
      While DonguBiter2 = False

        If sayac2 = 0 Then
          StartPosition2 = 0
        Else
          StartPosition2 = OccurenceOfSpace2
        End If

        EndPosition2 = InStr(StartPosition2 + 1, Trim(TextBoxSearch.Text), " ")
        OccurenceOfSpace2 = EndPosition2
        sayac2 = sayac2 + 1

        If EndPosition2 = 0 Then
          Dim TextOne As String = Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString
          If sayac2 = 1 Then
            LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextOne)
          Else
            Select Case sayac2
              Case 2
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextOne)
              Case 3
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextOne)
              Case 4
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextOne)
              Case 5
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextOne)
              Case 6
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextOne)
              Case 7
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextOne)
              Case 8
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextOne)
              Case 9
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextOne)
              Case 10
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextOne)
            End Select
          End If
          DonguBiter2 = True
        Else
          Dim TextTwo As String = Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString
          If sayac2 = 1 Then
            LabelDescription.Text = ReturnMarkedText(1, LabelDescription.Text, TextTwo)
          Else
            Select Case sayac2
              Case 2
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextTwo)
              Case 3
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextTwo)
              Case 4
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextTwo)
              Case 5
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextTwo)
              Case 6
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextTwo)
              Case 7
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextTwo)
              Case 8
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextTwo)
              Case 9
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextTwo)
              Case 10
                LabelDescription.Text = ReturnMarkedText(sayac2, LabelDescription.Text, TextTwo)
            End Select
          End If
        End If
      End While
      sayac2 = 0
      DonguBiter2 = False
    End If

    ' __________________

    ' it will highlight InvoiceNo if selected
    Dim Label11invnoo As Label = DirectCast(e.Row.FindControl("Label11invnoo"), Label)
    If Label11invnoo IsNot Nothing Then
      If Not String.IsNullOrEmpty(TextBoxInvoiceNo.Text) Then
        Label11invnoo.Text = ReturnMarkedText(-2, Label11invnoo.Text, TextBoxInvoiceNo.Text)
      End If
    End If

    ' it will highlight PayReqNo if selected
    Dim LabelSiteRecordNo As Label = DirectCast(e.Row.FindControl("LabelSiteRecordNo"), Label)
    If LabelSiteRecordNo IsNot Nothing Then
      If Not String.IsNullOrEmpty(TextBoxSiteRecordNo.Text) Then
        LabelSiteRecordNo.Text = ReturnMarkedText(-1, LabelSiteRecordNo.Text, TextBoxSiteRecordNo.Text)
      End If
    End If

    ' it will highlight FinanceNo if selected
    Dim LabelFinanceNo As Label = DirectCast(e.Row.FindControl("LabelFinanceNo"), Label)
    If LabelFinanceNo IsNot Nothing Then
      If Not String.IsNullOrEmpty(TextBoxFinanceNo.Text) Then
        LabelFinanceNo.Text = ReturnMarkedText(0, LabelFinanceNo.Text, TextBoxFinanceNo.Text)
      End If
    End If

    ' It fixes column width problem
    Dim Label1 As Label = DirectCast(e.Row.FindControl("LabelDescription"), Label)
    If Label1 IsNot Nothing Then
      If Label1 IsNot Nothing Then
        Label1.Text = Label1.Text.Replace(",", "," + " ")
      End If
    End If

    'it defines CurrencyImage.
    If DirectCast(e.Row.FindControl("LabelCurrency"), Label) IsNot Nothing Then
      If InStr(DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text, "Rub") > 0 Then
        DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/ruble_icon_.bmp"
      ElseIf InStr(DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text, "Dollar") > 0 Then
        DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/dollar_icon_.bmp"
      ElseIf InStr(DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text, "Euro") > 0 Then
        DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/euro_icon_.bmp"
      End If
    End If

  End Sub

  Public Function ReturnMarkedText(ByVal NumberOf As Integer, ByVal TextOf As String, ByVal TextHighlight As String) As String
    Select Case NumberOf
      Case -2
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #99FF66" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
      Case -1
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #A8FFFF" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
      Case 0
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFFF00" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
      Case 1
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFFF00;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
      Case 2
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #ADFF2F;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
      Case 3
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFB6C1;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
      Case 4
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #87CEFA;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
      Case 5
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #DA70D6;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
      Case 6
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FF0000;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
      Case 7
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #8FBC8B;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
      Case 8
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #1E90FF;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
      Case 9
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FF8C00;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
      Case 10
        TextOf = Regex.Replace(TextOf, TextHighlight, "<span style=" + """" + "border: 1px solid #E6E6FA; padding-top: 1px; padding-bottom: 1px; background-color: #FFD700;" + """" + ">" & TextHighlight & "</span>", RegexOptions.IgnoreCase)
    End Select
    Return TextOf
  End Function

  Protected Sub SqlDataSourceMonitor_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceMonitor.Selecting
    e.Command.CommandTimeout = 120
  End Sub

  Protected Sub ButtonSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSearch.Click
    GridViewMonitor.Sort("PO_Date", SortDirection.Descending)
    DataListSupplierType.DataBind()
    DataListCostCode.DataBind()
  End Sub

  Protected Sub ButtonSelectCategory_Click(sender As Object, e As System.EventArgs) Handles ButtonSelectCategory.Click
    'ModalPopupExtender1.Show()
    'DataListSupplierType.DataSource = SqlDataSourceSupplierType
    'DataListSupplierType.DataBind()
  End Sub

  Protected Sub DataListSupplierType_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataListSupplierType.ItemCreated
  End Sub

  Protected Sub DataListSupplierType_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataListSupplierType.ItemDataBound
    If e.Item.ItemType = ListItemType.Item _
  OrElse e.Item.ItemType = ListItemType.AlternatingItem _
  OrElse e.Item.ItemType = ListItemType.Separator Then

      Dim LabelSupplierType As Label = DirectCast(e.Item.FindControl("LabelSupplierType"), Label)
      Dim HyperLinkSupplierByType As HyperLink = DirectCast(e.Item.FindControl("HyperLinkSupplierByType"), HyperLink)

      If StartStatus = "Start" Then
        StartStatus = "Started"
        FlyBy1 = DataBinder.Eval(e.Item.DataItem, "SupplierDiscipline").ToString()
        DirectCast(e.Item.FindControl("LabelSubTittle"), Label).Text = _
          "<div style=" + """" + "background-color: #333333; color: #FFFFFF; height: 18px; text-align: center;" + """" + ">" + FlyBy1 + "</div>"
      Else
        If DataBinder.Eval(e.Item.DataItem, "SupplierDiscipline") <> FlyBy1 Then
          DirectCast(e.Item.FindControl("LabelSubTittle"), Label).Text = _
          "<div style=" + """" + "background-color: #333333; color: #FFFFFF; height: 18px; text-align: center;" + """" + ">" + DataBinder.Eval(e.Item.DataItem, "SupplierDiscipline") + "</div>"
          FlyBy1 = DataBinder.Eval(e.Item.DataItem, "SupplierDiscipline")
        End If
      End If

      ' Rebind selected items from session
      If Not Session(DataBinder.Eval(e.Item.DataItem, "SupplierTypeId").ToString) Is Nothing AndAlso Session(DataBinder.Eval(e.Item.DataItem, "SupplierTypeId").ToString) = 1 Then
        DirectCast(e.Item.FindControl("CheckBoxSupplierType"), CheckBox).Checked = True
                LabelSupplierType.BackColor = System.Drawing.Color.Chartreuse
            End If

            ' disable Supplier Type if doesnt correspondense to any supplier
            If DataBinder.Eval(e.Item.DataItem, "CountOfSupplier") = 0 Then
                DirectCast(e.Item.FindControl("CheckBoxSupplierType"), CheckBox).Enabled = False
                LabelSupplierType.ForeColor = System.Drawing.Color.Gray
                LabelSupplierType.Font.Italic = True
            End If

            ' Show count of supplier as Hyperlink
            LabelSupplierType.Text = LabelSupplierType.Text
            If DataBinder.Eval(e.Item.DataItem, "CountOfSupplier") = 0 Then
                ' nothing
            Else
                HyperLinkSupplierByType.Text = " (" + DataBinder.Eval(e.Item.DataItem, "CountOfSupplier").ToString + ")"
                HyperLinkSupplierByType.Attributes.Add("onclick", "javascript:w= window.open('SuppliersByType.aspx?SupplierTypeId=" _
                                       + DataBinder.Eval(e.Item.DataItem, "SupplierTypeId").ToString _
                                       + "','SupplierByType','left=300,top=150,width=300,height=300,toolbar=0,resizable=0,scrollbars=yes');")
            End If

        End If

    End Sub

    Protected Sub ButtonSelectCostCode_Click(sender As Object, e As System.EventArgs) Handles ButtonSelectCostCode.Click
        'ModalPopupExtenderCostCode.Show()
    End Sub

    Protected Sub DataListCostCode_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataListCostCode.ItemDataBound
        If e.Item.ItemType = ListItemType.Item _
OrElse e.Item.ItemType = ListItemType.AlternatingItem _
OrElse e.Item.ItemType = ListItemType.Separator Then

            Dim LabelCostCode As Label = DirectCast(e.Item.FindControl("LabelCostCode"), Label)

            If StartStatusCostCode = "Start" Then
                StartStatusCostCode = "Started"
                FlyBy1CostCode = DataBinder.Eval(e.Item.DataItem, "CostDivisionDescription").ToString()
                DirectCast(e.Item.FindControl("LabelCostDivisionDescription"), Label).Text =
          "<div style=" + """" + "background-color: #333333; color: #FFFFFF; height: 18px; text-align: center;" + """" + ">" + FlyBy1CostCode + "</div>"
            Else
                If DataBinder.Eval(e.Item.DataItem, "CostDivisionDescription") <> FlyBy1CostCode Then
                    DirectCast(e.Item.FindControl("LabelCostDivisionDescription"), Label).Text =
          "<div style=" + """" + "background-color: #333333; color: #FFFFFF; height: 18px; text-align: center;" + """" + ">" + DataBinder.Eval(e.Item.DataItem, "CostDivisionDescription") + "</div>"
                    FlyBy1CostCode = DataBinder.Eval(e.Item.DataItem, "CostDivisionDescription")
                End If
            End If

            ' Rebind selected items from session
            If Not Session(DataBinder.Eval(e.Item.DataItem, "CostCode").ToString) Is Nothing AndAlso Session(DataBinder.Eval(e.Item.DataItem, "CostCode").ToString) = 1 Then
                DirectCast(e.Item.FindControl("CheckBoxCostCode"), CheckBox).Checked = True
                LabelCostCode.BackColor = System.Drawing.Color.Chartreuse
            End If

      ' highlight cost code part
      LabelCostCode.Text = "<font style=" + """" + "font-weight: bold" + """" + ">" + Mid(LabelCostCode.Text, 1, InStr(LabelCostCode.Text, " ")) + "</font>" _
        + Mid(LabelCostCode.Text, InStr(LabelCostCode.Text, " "), Len(LabelCostCode.Text) - InStr(LabelCostCode.Text, " ") + 1)

    End If
  End Sub

    Protected Sub ButtonExportExcel_Click(sender As Object, e As EventArgs)

        ExportToExcel.Export(gvFiles, SqlDataSourceMonitor, "Search Results")

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
    End Sub

End Class
