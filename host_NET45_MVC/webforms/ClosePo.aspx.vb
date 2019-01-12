Imports System.Data.SqlClient
Partial Class ClosePo
    Inherits System.Web.UI.Page



  Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click

    CloseThisPo(Request.QueryString("PO_No"), Page.User.Identity.Name.ToString.ToLower)

    labelInfo.Text = "<span style=" + """" + "color: #339900; font-size: 14pt; font-weight: bold" + """" + ">PO Closed successfully!</span>"

  End Sub

  Protected Sub CloseThisPo(ByVal _Po_No As String, ByVal _User As String)
    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    Using con
      con.Open()
      Dim sqlstring As String = "CloseThisPo"
      Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar, 11)
            PO_No.Value = _Po_No
            Dim UserName As SqlParameter = cmd.Parameters.Add("@User", System.Data.SqlDbType.NVarChar)
            UserName.Value = _User
      cmd.ExecuteNonQuery()
      con.Close()
      con.Dispose()
    End Using
  End Sub

End Class
