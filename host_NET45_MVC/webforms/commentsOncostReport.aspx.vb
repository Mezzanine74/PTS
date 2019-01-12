Imports System.Data.SqlClient
Partial Class commentsOncostReport
    Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Request.QueryString("ProjectID") IsNot Nothing AndAlso _
      Request.QueryString("CostCode") IsNot Nothing AndAlso _
      Request.QueryString("Currency") IsNot Nothing Then

      ' labelDefinition
      DropDownListProjectName.DataBind()
      DropDownListCostDescription.DataBind()
      LabelDefinition.Text = DropDownListProjectName.SelectedValue.ToString + " (" + Request.QueryString("CostCode").ToString + " - " + DropDownListCostDescription.SelectedValue.ToString + ")"

      ' check if budget exist for this item or not.
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

                con.Open()
                Dim sqlstring As String = "SELECT count(BudgetID) AS CountBudget FROM [Table_Budget] WHERE (ProjectID = @ProjectID ) AND (CostCode = @CostCode) AND (Currency = @Currency) "
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text

                'syntax for parameter adding
                Dim ProjectID As SqlParameter = cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
                ProjectID.Value = Request.QueryString("ProjectID")
                Dim CostCode As SqlParameter = cmd.Parameters.Add("@CostCode", System.Data.SqlDbType.NChar, 20)
                CostCode.Value = Request.QueryString("CostCode").ToString
                Dim Currency As SqlParameter = cmd.Parameters.Add("@Currency", System.Data.SqlDbType.NChar, 10)
                Currency.Value = Request.QueryString("Currency").ToString

                Dim dr As SqlDataReader = cmd.ExecuteReader

                While dr.Read
                    If dr(0) > 0 Then
                        'select BudgetID
                        Using con2 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                            con2.Open()
                            Dim sqlstring2 As String = "SELECT BudgetID FROM [Table_Budget] WHERE (ProjectID = @ProjectID ) AND (CostCode = @CostCode) AND (Currency = @Currency) "

                            Dim cmd2 As New SqlCommand(sqlstring2, con2)
                            cmd2.CommandType = System.Data.CommandType.Text

                            'syntax for parameter adding
                            Dim ProjectID2 As SqlParameter = cmd2.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
                            ProjectID2.Value = Request.QueryString("ProjectID")
                            Dim CostCode2 As SqlParameter = cmd2.Parameters.Add("@CostCode", System.Data.SqlDbType.NChar, 20)
                            CostCode2.Value = Request.QueryString("CostCode").ToString
                            Dim Currency2 As SqlParameter = cmd2.Parameters.Add("@Currency", System.Data.SqlDbType.NChar, 10)
                            Currency2.Value = Request.QueryString("Currency").ToString
                            Dim dr2 As SqlDataReader = cmd2.ExecuteReader
                            While dr2.Read
                                labelTest.Text = dr2(0).ToString
                            End While

                            con2.Close()
                            dr2.Close()

                        End Using

                    Else
                        ' add default budget 
                        Using con2 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                            con2.Open()
                            Dim sqlstring2 As String = " INSERT INTO [Table_Budget] " +
                                "            ([ProjectID] " +
                                "            ,[CostCode] " +
                                "            ,[Budget] " +
                                "            ,[PlannedToSpend] " +
                                "            ,[Currency]) " +
                                "      VALUES " +
                                "            (@ProjectID " +
                                "            ,@CostCode " +
                                "            ,0 " +
                                "            ,0 " +
                                "            ,@Currency);SELECT @ID=SCOPE_IDENTITY() "

                            Dim cmd2 As New SqlCommand(sqlstring2, con2)
                            cmd2.CommandType = System.Data.CommandType.Text

                            'syntax for parameter adding
                            Dim ProjectID2 As SqlParameter = cmd2.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int)
                            ProjectID2.Value = Request.QueryString("ProjectID")
                            Dim CostCode2 As SqlParameter = cmd2.Parameters.Add("@CostCode", System.Data.SqlDbType.NChar, 20)
                            CostCode2.Value = Request.QueryString("CostCode").ToString
                            Dim Currency2 As SqlParameter = cmd2.Parameters.Add("@Currency", System.Data.SqlDbType.NChar, 10)
                            Currency2.Value = Request.QueryString("Currency").ToString

                            Dim ID As SqlParameter = cmd2.Parameters.Add("@ID", System.Data.SqlDbType.Int)
                            ID.Direction = System.Data.ParameterDirection.Output

                            Dim dr2 As SqlDataReader = cmd2.ExecuteReader

                            labelTest.Text = ID.Value.ToString

                            con2.Close()
                            dr2.Close()

                        End Using
                    End If
                End While

                con.Close()
                con.Dispose()
                dr.Close()

            End Using

        End If
  End Sub

  Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
    SqlDataSourceCostCodeComments.InsertParameters("UserName").DefaultValue = Page.User.Identity.Name.ToString
    SqlDataSourceCostCodeComments.Insert()
    GridViewCostCodeComments.DataBind()
    TextBoxCostCodeComments.Text = ""
  End Sub

  Protected Sub GridViewCostCodeComments_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewCostCodeComments.RowDataBound
    If e.Row.RowType = DataControlRowType.DataRow Then
      Dim LinkEdit As LinkButton = DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton)
      Dim LinkDelete As LinkButton = DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton)

      If LinkEdit IsNot Nothing Then
        If DataBinder.Eval(e.Row.DataItem, "UserName").ToString.ToLower.Replace(" ", "") = Page.User.Identity.Name.ToString.ToLower.Replace(" ", "") Then
          LinkEdit.Visible = True
          LinkDelete.Visible = True
        ElseIf DataBinder.Eval(e.Row.DataItem, "UserName").ToString.ToLower <> Page.User.Identity.Name.ToString.ToLower Then
          LinkEdit.Visible = False
          LinkDelete.Visible = False
        End If
      End If

    End If
  End Sub
End Class
