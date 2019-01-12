Imports System.Data.SqlClient
Partial Class contractNotes
  Inherits System.Web.UI.Page

  Protected Sub GridViewProjects_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewProjects.Load
    If Not IsPostBack Then
      GridViewProjects.Sort("SupplierName", SortDirection.Ascending)
    End If
  End Sub

  Protected Sub GridViewProjects_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewProjects.RowCommand
    If (e.CommandName = "Update") Then
      Dim index As Integer = Convert.ToInt32(e.CommandArgument)
      Dim row As GridViewRow = GridViewProjects.Rows(index)
      Dim TextBoxSupplierIDEdit As TextBox = DirectCast(row.FindControl("TextBoxSupplierIDEdit"), TextBox)

      If TextBoxSupplierIDEdit IsNot Nothing AndAlso Len(TextBoxSupplierIDEdit.Text) = 0 Then
        TextBoxSupplierIDEdit.Text = "0000000000"
      End If
    End If


  End Sub

  Protected Sub GridViewProjects_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewProjects.RowDataBound
    Dim LinkButtonEdit As LinkButton = DirectCast(e.Row.FindControl("LinkButtonEdit"), LinkButton)
    Dim LinkButtonDelete As LinkButton = DirectCast(e.Row.FindControl("LinkButtonDelete"), LinkButton)

    Dim TextBoxSupplierNameEdit As TextBox = DirectCast(e.Row.FindControl("TextBoxSupplierNameEdit"), TextBox)
    Dim TextBoxSupplierIDEdit As TextBox = DirectCast(e.Row.FindControl("TextBoxSupplierIDEdit"), TextBox)
    Dim LabelSupplierIDItem As Label = DirectCast(e.Row.FindControl("LabelSupplierIDItem"), Label)

        If Roles.IsUserInRole("ContractLeadGirls") Or Roles.IsUserInRole("ContractSupportGirl") Then
            ' in case of itemmode
            If LinkButtonDelete IsNot Nothing Then
                LinkButtonDelete.Visible = True
                LinkButtonEdit.Visible = True
            End If
        Else
            If User.Identity.Name.ToString.ToLower() = "savas" Then
                ' do nothing, all controls available
            Else
                ' in case of itemmode
                If LinkButtonDelete IsNot Nothing Then
                    LinkButtonDelete.Visible = False
                    LinkButtonEdit.Visible = False
                End If
            End If
        End If

    ' highlight edit row
    If TextBoxSupplierNameEdit IsNot Nothing Then
            e.Row.BackColor = System.Drawing.Color.LightSteelBlue
        End If

    If TextBoxSupplierIDEdit IsNot Nothing AndAlso TextBoxSupplierIDEdit.Text = "0000000000" Then
      TextBoxSupplierIDEdit.Text = ""
    End If

    If LabelSupplierIDItem IsNot Nothing AndAlso LabelSupplierIDItem.Text = "0000000000" Then
      LabelSupplierIDItem.Text = ""
    End If

  End Sub

  Protected Sub GridViewProjects_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewProjects.RowUpdating
  End Sub

  Protected Sub ImageButtonAddProject_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAddProject.Click
    If Convert.ToInt32(LabelFormViewVisibleStatus.Text) Mod 2 = 0 Then
      FormViewInsertProject.Visible = True
      LabelFormViewVisibleStatus.Text = Convert.ToString(Convert.ToInt32(LabelFormViewVisibleStatus.Text) + 1)
      ImageButtonAddProject.ImageUrl = "~/Images/minus.png"

      Exit Sub
    ElseIf Convert.ToInt32(LabelFormViewVisibleStatus.Text) Mod 2 = 1 Then
      FormViewInsertProject.Visible = False
      LabelFormViewVisibleStatus.Text = Convert.ToString(Convert.ToInt32(LabelFormViewVisibleStatus.Text) + 1)
      ImageButtonAddProject.ImageUrl = "~/Images/insert.png"
    End If
  End Sub

  Protected Sub FormViewInsertProject_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FormViewInsertProject.ItemInserted
    GridViewProjects.DataBind()
    ComboBoxSupplierName.DataBind()
    FormViewInsertProject.Visible = False
    ImageButtonAddProject.ImageUrl = "~/Images/insert.png"
  End Sub

  Protected Sub FormViewInsertProject_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormViewInsertProject.ItemInserting

    Dim TextBoxSupplierID As TextBox = FormViewInsertProject.FindControl("TextBoxSupplierID")
    If TextBoxSupplierID IsNot Nothing Then
      If Len(TextBoxSupplierID.Text) = 0 Then
        e.Values("SupplierID") = "0000000000"
      End If
    End If

  End Sub

  Protected Sub FormViewInsertProject_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormViewInsertProject.Load
    If Not IsPostBack Then
    End If
  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Not IsPostBack Then
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = "SELECT [ContractRequirements] FROM [Table_ContractRequirements]"
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    LabelNotes.Text = dr(0).ToString
                End While
                dr.Close()
                con.Close()
                con.Dispose()

            End Using
        End If

        If Not IsPostBack Then
            If Roles.IsUserInRole("ContractLeadGirls") Or Roles.IsUserInRole("ContractSupportGirl") Then
                ImageButtonAddProject.Visible = True
            Else
                If User.Identity.Name.ToString.ToLower() = "savas" Then
                    ' do nothing, all controls available
                    ImageButtonAddProject.Visible = True
                Else
                    ImageButtonAddProject.Visible = False
                End If
            End If
        End If

        If IsPostBack Or Not IsPostBack Then
            If Roles.IsUserInRole("ContractLeadGirls") Or Roles.IsUserInRole("ContractSupportGirl") Then
                ButtonEditNotes.Visible = True
            ElseIf User.Identity.Name.ToString.ToLower() = "savas" Then
                ButtonEditNotes.Visible = True
            End If
        End If

    End Sub

    Protected Sub ButtonEditNotes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEditNotes.Click
        EditorNotes.Visible = True
        ButtonUpdate.Visible = True
        EditorNotes.Content = LabelNotes.Text
    End Sub

    Protected Sub ButtonUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUpdate.Click
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "UPDATE [Table_ContractRequirements] SET [ContractRequirements] =" + "N'" + EditorNotes.Content.ToString.Replace("'", "'+N''''+N'") + "'" + " WHERE Id = 1"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            Try
                Dim dr As SqlDataReader = cmd.ExecuteReader
                dr.Close()
            Catch ex As Exception
            End Try
            con.Close()
            con.Dispose()

        End Using

        ' Refresh LabelNote
        Using conLabel As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            conLabel.Open()
            Dim sqlstringLabel As String = "SELECT [ContractRequirements] FROM [Table_ContractRequirements]"
            Dim cmdLabel As New SqlCommand(sqlstringLabel, conLabel)
            cmdLabel.CommandType = System.Data.CommandType.Text
            Dim drLabel As SqlDataReader = cmdLabel.ExecuteReader
            While drLabel.Read
                LabelNotes.Text = drLabel(0).ToString
            End While
            drLabel.Close()
            conLabel.Close()
            conLabel.Dispose()

        End Using

    ' hide update button and editor
    EditorNotes.Visible = False
    ButtonUpdate.Visible = False
  End Sub
End Class
