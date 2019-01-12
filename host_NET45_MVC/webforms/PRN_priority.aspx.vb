Imports System.Data.SqlClient
Partial Class PaymentTermsPriority
  Inherits System.Web.UI.Page

  Protected Sub GridViewPaymentTerms_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewPaymentTerms.RowCreated
    Dim DropDownListPriority As DropDownList = DirectCast(e.Row.FindControl("DropDownListPriority"), DropDownList)
    If e.Row.RowType = DataControlRowType.DataRow Then
      ' POPULATES DropDownList Priority
      If DataBinder.Eval(e.Row.DataItem, "Priority") = 0 Then
        ' dont assign value 
        PopulateDDL_priority(DropDownListPriority, True)
        DropDownListPriority.SelectedValue = 0
      Else
        ' populate and assign value
        PopulateDDL_priority(DropDownListPriority, False)
        DropDownListPriority.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Priority")
      End If
    End If

  End Sub

  Protected Sub GridViewPaymentTerms_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewPaymentTerms.RowDataBound

    Dim MyTask As New MyCommonTasks
    MyTask.HoverEffectOnGridviewCells(sender, e.Row)

    Dim DropDownListPriority As DropDownList = DirectCast(e.Row.FindControl("DropDownListPriority"), DropDownList)
    If e.Row.RowType = DataControlRowType.DataRow Then
      ' POPULATES DropDownList Priority
      If DataBinder.Eval(e.Row.DataItem, "Priority") = 0 Then
        ' dont assign value 
        'PopulateDDL_priority(DropDownListPriority, True)
        'DropDownListPriority.SelectedValue = 0
      Else
        ' populate and assign value
        'PopulateDDL_priority(DropDownListPriority, False)
        'DropDownListPriority.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Priority")
      End If
    End If

  End Sub

  Protected Sub PopulateDDL_priority(ByVal DDL_toPopulate As DropDownList, ByVal NulOrNot As Boolean)

    If NulOrNot = True Then
      Dim lst1 As New ListItem(0, 0)
      DDL_toPopulate.Items.Insert(0, lst1)
      Dim lst2 As New ListItem(Session("MaxPriorityNumber") + 1, Session("MaxPriorityNumber") + 1)
      DDL_toPopulate.Items.Insert(1, lst2)
    Else
      For i = 0 To Session("MaxPriorityNumber")
        If i = 0 Then
          Dim lst1 As New ListItem(0, 0)
          DDL_toPopulate.Items.Insert(0, lst1)
        Else
          Dim lst2 As New ListItem(i.ToString, Convert.ToInt32(i))
          DDL_toPopulate.Items.Insert(i, lst2)
        End If
      Next
    End If

  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    If IsPostBack Or Not IsPostBack Then
      TextBoxUserName.Text = Page.User.Identity.Name
    End If

    If Not String.IsNullOrEmpty(DropDownListPrj.SelectedValue.ToString) Then
      ' if project selected, find Max PriorityNumber
      Session("MaxPriorityNumber") = GetMaxPriorityNumber(DropDownListPrj.SelectedValue)
    End If
    'Response.Write(MaxPriorityNumber.ToString)

  End Sub

  Protected Function GetMaxPriorityNumber(ByVal ProjectID_ As Integer) As Integer
    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    con.Open()
    Dim sqlstring As String = " SELECT     (CASE WHEN MAX(dbo.Table4_PaymentRequest.Priority) IS NULL THEN 0 ELSE MAX(dbo.Table4_PaymentRequest.Priority) END) AS MaxPriority " + _
" FROM         dbo.Table2_PONo INNER JOIN " + _
"                       dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " + _
"                       dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID LEFT OUTER JOIN " + _
"                       dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo " + _
" WHERE     (dbo.Table2_PONo.Project_ID = @ProjectID) "

    Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text

        'syntax for parameter adding
        Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
        ProjectID.Value = ProjectID_
        Dim dr As SqlDataReader = cmd.ExecuteReader
        Dim returnValue As Integer = 0
        While dr.Read
            returnValue = dr(0)
        End While
        Return returnValue
        con.Close()
        dr.Close()
    End Function

    Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
        Dim lst1 As New ListItem("ALL Project", 0)
        Me.DropDownListPrj.Items.Insert(0, lst1)
    End Sub

    Protected Sub DropDownListPriority_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim ddl As DropDownList = DirectCast(sender, DropDownList)
        Dim row As GridViewRow = DirectCast(ddl.Parent.Parent, GridViewRow)
        Dim idx As Integer = row.RowIndex
        Dim LabelPayReqNo As Label = DirectCast(row.FindControl("LabelPayReqNo"), Label)

        ' UPDATE Priority accordingly
        If ddl.SelectedValue = 0 Then
            UpdatePriority(Convert.ToInt32(LabelPayReqNo.Text), 0, True)
            Response.Write(ddl.SelectedValue.ToString)
        Else
            UpdatePriority(Convert.ToInt32(LabelPayReqNo.Text), ddl.SelectedValue, False)
            Response.Write(ddl.SelectedValue.ToString)
        End If

    End Sub

    Protected Sub UpdatePriority(ByVal PayReqNo_ As Integer, ByVal Priority_ As Integer, ByVal IsNULPriority As Boolean)

        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()

        Dim sqlstring As String = ""

        If IsNULPriority = True Then
            sqlstring = " UPDATE [Table4_PaymentRequest] SET [Priority] = null WHERE PayReqNo = @PayReqNo "
        ElseIf IsNULPriority = False Then
            sqlstring = " UPDATE [Table4_PaymentRequest] SET [Priority] = @Priority WHERE PayReqNo = @PayReqNo "
        End If

        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text

        'syntax for parameter adding
        Dim PayReqNo As SqlParameter = cmd.Parameters.Add("@PayReqNo", System.Data.SqlDbType.Int)
        PayReqNo.Value = PayReqNo_

        Dim Priority As SqlParameter = cmd.Parameters.Add("@Priority", System.Data.SqlDbType.Int)
        Priority.Value = Priority_

    Dim dr As SqlDataReader = cmd.ExecuteReader
    con.Close()
    dr.Close()

  End Sub

End Class
