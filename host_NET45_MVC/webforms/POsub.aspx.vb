Imports System.Data.SqlClient
Partial Class POsub
    Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    SqlDataSourceCostCode.SelectParameters("username").Type = TypeCode.String
    SqlDataSourceCostCode.SelectParameters("username").DefaultValue = Page.User.Identity.Name.ToString

    SqlDataSourceCostCode.SelectParameters("ProjectID").Type = TypeCode.Int32
    SqlDataSourceCostCode.SelectParameters("ProjectID").DefaultValue = Convert.ToInt32(Mid(Request.QueryString("PO_No"), 4, 3))

    BindDDL()

  End Sub

  Protected Sub LinkButtonInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButtonInsert.Click

    If Len(TextBoxCollectedDocument.Text) = 0 Then
      TextBoxCollectedDocument.Text = "0"
    End If

    If (Not CheckPoValue(Convert.ToDecimal(TextBoxPoTotal.Text), 0)) AndAlso (Not CheckCollectedDocumentValue(Convert.ToDecimal(TextBoxCollectedDocument.Text), 0)) Then
      ' Check if any Sub Po exist under this PO
      Dim SubPo_No As String = ""
      Dim MaxSubPoExtensionNo As Integer = 0
      If GridViewSubPo.Rows.Count = 0 Then
        ' There is no SubPo yet. Assign first sub PO value
        SubPo_No = Request.QueryString("PO_No").ToString + "/" + "01"
      ElseIf GridViewSubPo.Rows.Count > 0 Then
        ' Find the biggest Sub_Po no. Then assign next subPO number
                Using conSubPo As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                    conSubPo.Open()
                    Dim sqlstringSubPo As String = " SELECT TOP (1) RTRIM(dbo.Table2_PONo_Sub.SubPO_No) AS SubPO_No " + _
                                      " FROM         dbo.Table2_PONo_Sub INNER JOIN " + _
                                      " dbo.Table2_PONo ON dbo.Table2_PONo_Sub.PO_No = dbo.Table2_PONo.PO_No " + _
                                                    " WHERE dbo.Table2_PONo.PO_No = N'" + Request.QueryString("PO_No") + "'" + _
                                                    " ORDER BY SubPO_No DESC "
                    Dim cmdSubPo As New SqlCommand(sqlstringSubPo, conSubPo)
                    cmdSubPo.CommandType = System.Data.CommandType.Text
                    Dim drSubPo As SqlDataReader = cmdSubPo.ExecuteReader
                    While drSubPo.Read
                        MaxSubPoExtensionNo = Convert.ToInt32(Mid(drSubPo(0).ToString, 13, 2))
                    End While
                    conSubPo.Close()
                    drSubPo.Close()
                    conSubPo.Dispose()

                End Using
                If MaxSubPoExtensionNo < 9 AndAlso MaxSubPoExtensionNo > 0 Then
                    SubPo_No = Request.QueryString("PO_No").ToString + "/" + "0" + Convert.ToString((MaxSubPoExtensionNo + 1))
                Else
                    SubPo_No = Request.QueryString("PO_No").ToString + "/" + Convert.ToString((MaxSubPoExtensionNo + 1))
                End If
            End If

            ' insert Sub PO
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = " INSERT INTO [Table2_PONo_Sub] " +
                                          "            ([SubPO_No] " +
                                          "            ,[PO_No] " +
                                          "            ,[TotalPrice] " +
                                          "            ,[CostCode] " +
                                          "            ,[CollectedValue]) " +
                                          "      VALUES " +
                                          "            ( N'" + SubPo_No + "'" +
                                          "            , N'" + Request.QueryString("PO_No").ToString + "'" +
                                          "            , @TotalPrice" +
                                          "            , N'" + DropDownListCostCode.SelectedValue.ToString + "'" +
                                          "            , @CollectedValue) "

                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text

                'syntax for parameter adding
                Dim TotalPrice As SqlParameter = cmd.Parameters.Add("@TotalPrice", System.Data.SqlDbType.Decimal)
                TotalPrice.Value = Convert.ToDecimal(TextBoxPoTotal.Text)
                Dim CollectedValue As SqlParameter = cmd.Parameters.Add("@CollectedValue", System.Data.SqlDbType.Decimal)
                If Len(TextBoxCollectedDocument.Text) = 0 Then
                    CollectedValue.Value = 0
                Else
                    CollectedValue.Value = Convert.ToDecimal(TextBoxCollectedDocument.Text)
                End If

                Dim dr As SqlDataReader = cmd.ExecuteReader
                con.Close()
                dr.Close()
                con.Dispose()

            End Using

      GridViewPoInfo.DataBind()
      GridViewSubPo.DataBind()
      BindDDL()

      TextBoxPoTotal.Text = ""
      TextBoxCollectedDocument.Text = ""
      DropDownListCostCode.SelectedValue = "0"

    Else
      If CheckPoValue(Convert.ToDecimal(TextBoxPoTotal.Text), 0) Then
        message("You cannot add more than total PO value")
      End If
      If CheckCollectedDocumentValue(Convert.ToDecimal(TextBoxCollectedDocument.Text), 0) Then
        message("You cannot add more than total Collected value")
      End If
    End If

  End Sub

  Protected Sub GridViewSubPo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewSubPo.RowCommand

  End Sub

  Protected Sub GridViewSubPo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewSubPo.RowDataBound

    Dim MyTask As New MyCommonTasks
    MyTask.HoverEffectOnGridviewCells(sender, e.Row)

    Dim SqlDataSourceCostCodeEdit As SqlDataSource = DirectCast(e.Row.FindControl("SqlDataSourceCostCodeEdit"), SqlDataSource)
    Dim DropDownListCostCodeEdit As DropDownList = DirectCast(e.Row.FindControl("DropDownListCostCodeEdit"), DropDownList)
    If SqlDataSourceCostCodeEdit IsNot Nothing Then
      SqlDataSourceCostCodeEdit.SelectParameters("username").Type = TypeCode.String
      SqlDataSourceCostCodeEdit.SelectParameters("username").DefaultValue = Page.User.Identity.Name.ToString

      SqlDataSourceCostCodeEdit.SelectParameters("ProjectID").Type = TypeCode.Int32
      SqlDataSourceCostCodeEdit.SelectParameters("ProjectID").DefaultValue = Convert.ToInt32(Mid(Request.QueryString("PO_No"), 4, 3))
      DropDownListCostCodeEdit.SelectedValue = DataBinder.Eval(e.Row.DataItem, "CostCode").ToString
    End If
  End Sub

  Protected Sub GridViewSubPo_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridViewSubPo.RowDeleted
    GridViewPoInfo.DataBind()
    GridViewSubPo.DataBind()
    BindDDL()

  End Sub

  Protected Sub GridViewSubPo_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles GridViewSubPo.RowUpdated
    GridViewPoInfo.DataBind()
    BindDDL()

  End Sub

  Protected Sub GridViewSubPo_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewSubPo.RowUpdating

    Dim GridRow As GridViewRow = GridViewSubPo.Rows(e.RowIndex)
    Dim TextBoxTotalPrice As TextBox = DirectCast(GridRow.FindControl("TextBoxTotalPrice"), TextBox)
    Dim TextBoxCollectedValue As TextBox = DirectCast(GridRow.FindControl("TextBoxCollectedValue"), TextBox)
    Dim DropDownListCostCodeEdit As DropDownList = DirectCast(GridRow.FindControl("DropDownListCostCodeEdit"), DropDownList)

    If (Not CheckPoValue(Convert.ToDecimal(TextBoxTotalPrice.Text), e.OldValues("TotalPrice"))) AndAlso (Not CheckCollectedDocumentValue(Convert.ToDecimal(TextBoxCollectedValue.Text), e.OldValues("CollectedValue"))) Then
      SqlDataSourceSubPo.UpdateParameters("CostCode").Type = TypeCode.String
      SqlDataSourceSubPo.UpdateParameters("CostCode").DefaultValue = DropDownListCostCodeEdit.SelectedValue.ToString
    Else
      If CheckPoValue(Convert.ToDecimal(TextBoxTotalPrice.Text), e.OldValues("TotalPrice")) Then
        message("You cannot add more than total PO value")
      End If
      If CheckCollectedDocumentValue(Convert.ToDecimal(TextBoxCollectedValue.Text), e.OldValues("CollectedValue")) Then
        message("You cannot add more than total Collected value")
      End If

      e.Cancel = True
      TextBoxTotalPrice.Text = e.OldValues("TotalPrice")
      TextBoxCollectedValue.Text = e.OldValues("CollectedValue")

    End If

  End Sub

  Public Function CheckPoValue(ByVal NewValue As Decimal, ByVal OldValue As Decimal) As Boolean
    Dim StopInsertBecauseOfPoValue As Boolean = False
    If Math.Round(Convert.ToDecimal(DropDownListTotalPoValue.SelectedValue), 2) < (Math.Round(Convert.ToDecimal(DropDownListTotalSubPoValue.SelectedValue), 2) + Math.Round(Convert.ToDecimal(NewValue), 2) - Math.Round(Convert.ToDecimal(OldValue), 2)) Then
      StopInsertBecauseOfPoValue = True
    End If
    Return StopInsertBecauseOfPoValue
  End Function

  Public Function CheckCollectedDocumentValue(ByVal NewValue As Decimal, ByVal OldValue As Decimal) As Boolean
    Dim StopInsertBecauseOfCollectedDocs As Boolean = False
    If Math.Round(Convert.ToDecimal(DropDownListTotalCollectedDocuments.SelectedValue), 2) < (Math.Round(Convert.ToDecimal(DropDownListTotalSubPoCollectedDocuments.SelectedValue), 2) + Math.Round(Convert.ToDecimal(NewValue), 2) - Math.Round(Convert.ToDecimal(OldValue), 2)) Then
      StopInsertBecauseOfCollectedDocs = True
    End If
    Return StopInsertBecauseOfCollectedDocs
  End Function

  Public Sub message(ByVal Message As String)
    Dim BuildScript As New System.Text.StringBuilder
    Dim cs As ClientScriptManager = Page.ClientScript
    BuildScript.Append("<script>")
    BuildScript.Append(Environment.NewLine)
    BuildScript.Append("alert('" & Message & "');")
    BuildScript.Append(Environment.NewLine)
    BuildScript.Append("</" + "script>")
    cs.RegisterStartupScript(Me.[GetType](), "asd", BuildScript.ToString())
  End Sub

  Public Sub BindDDL()
    DropDownListTotalPoValue.DataBind()
    DropDownListTotalSubPoValue.DataBind()
    DropDownListTotalCollectedDocuments.DataBind()
    DropDownListTotalSubPoCollectedDocuments.DataBind()

    Dim OutstandingPoValue As Decimal = Convert.ToDecimal(Math.Round(Convert.ToDecimal(DropDownListTotalPoValue.SelectedValue), 2) - Math.Round(Convert.ToDecimal(DropDownListTotalSubPoValue.SelectedValue), 2))
    Dim outstandingCollectedValue As Decimal = Convert.ToDecimal(Math.Round(Convert.ToDecimal(DropDownListTotalCollectedDocuments.SelectedValue), 2) - Math.Round(Convert.ToDecimal(DropDownListTotalSubPoCollectedDocuments.SelectedValue), 2))

    If OutstandingPoValue > 0 Then
      labelOutstandingPOvalue.Text = "<span style=" + """" + "color: #DC143C; font-size: 11px" + """" + ">" + "<img alt=" + """" + """" + " src=" + """" + "Images/exclamation.png" + """" + " />" + " Outstanding Purchase Order value = <span style=" + """" + "font-weight: bold" + """" + ">" + String.Format("{0:#,##0.00}", OutstandingPoValue) + "</span></span>"
    ElseIf OutstandingPoValue = 0 Then
      labelOutstandingPOvalue.Text = "<span  style=" + """" + "color: #009900; font-size: 11px; font-weight: bold" + """" + ">" + "<img alt=" + """" + """" + " src=" + """" + "Images/GreenMark.png" + """" + " />" + "You successfully created all Sub Purchase Orders" + "</span>"
    End If

    If outstandingCollectedValue > 0 Then
      labelOutstandingCollectedValue.Text = "<span style=" + """" + "color: #DC143C; font-size: 11px" + """" + ">" + "<img alt=" + """" + """" + " src=" + """" + "Images/exclamation.png" + """" + " />" + "Outstanding collected document value = <span style=" + """" + "font-weight: bold" + """" + ">" + String.Format("{0:#,##0.00}", outstandingCollectedValue) + "</span></span>"
    ElseIf outstandingCollectedValue = 0 Then
      labelOutstandingCollectedValue.Text = "<span style=" + """" + "color: #009900; font-size: 11px; font-weight: bold" + """" + ">" + "<img alt=" + """" + """" + " src=" + """" + "Images/GreenMark.png" + """" + " />" + "All collected documents has been distributed to subPos successfully" + "</span>"
    End If
  End Sub

  Protected Sub GridViewPoInfo_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewPoInfo.RowDataBound
    Dim MyTask As New MyCommonTasks
    MyTask.HoverEffectOnGridviewCells(sender, e.Row)
  End Sub
End Class
