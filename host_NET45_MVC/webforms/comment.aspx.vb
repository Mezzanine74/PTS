Imports System.Data.SqlClient
Partial Class comment
    Inherits System.Web.UI.Page

  Protected Sub ButtonSendComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSendComment.Click

    Dim zoneId As String = "Russian Standard Time"
    Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
    Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)
    Dim InstanceOfSent As String = "'" + Mid(result.ToString, 7, 4).ToString + "-" + Mid(result.ToString, 4, 2).ToString + "-" + Mid(result.ToString, 1, 2).ToString + " " + Mid(result.ToString, 12, 2).ToString + ":" + Mid(result.ToString, 15, 2).ToString + ":" + Mid(result.ToString, 18, 2).ToString + "'"

    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    con.Open()
    Dim sqlstring As String = " INSERT INTO [dbo].[Table_UserComments] " + _
"            ([UserName] " + _
"            ,[Comment] " + _
"            ,[SentBy]) " + _
"      VALUES " + _
"            ( '" + Page.User.Identity.Name.ToString + "' " + _
"            , @Comment " + _
"            ," + InstanceOfSent + ") "
    Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim Comment As SqlParameter = cmd.Parameters.Add("@Comment", System.Data.SqlDbType.NVarChar)
        Comment.Value = textBoxComments.Text
    Dim dr As SqlDataReader = cmd.ExecuteReader

    If dr.RecordsAffected > 0 Then
      GridView1.DataBind()
      ModalPopupExtenderSent.Show()
      textBoxComments.Text = ""
      TimerMessageSent.Enabled = True
    End If

    con.Close()
    dr.Close()

  End Sub

  Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
    'If e.Row.RowType = DataControlRowType.DataRow Then
    'Dim labelOlesine As Label = DirectCast(e.Row.FindControl("labelOlesine"), Label)
    'labelOlesine.Text = labelOlesine.Text.Replace("\n\r", Environment.NewLine)
    'End If
  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    'ModalPopupExtender1.show()
  End Sub

  Protected Sub buttonRedirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonRedirect.Click
        Response.Redirect("~/webforms/comment2.aspx?Bir=" + textbox1.Text + "&Iki=" + textbox2.Text + "&Uc=" + textbox3.Text + "&Dort=" + textbox4.Text)
  End Sub

  Protected Sub TimerMessageSent_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerMessageSent.Tick
    ModalPopupExtenderSent.Hide()
    TimerMessageSent.Enabled = False
  End Sub
End Class
