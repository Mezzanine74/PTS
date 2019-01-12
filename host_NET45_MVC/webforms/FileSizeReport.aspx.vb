Imports System.Data.SqlClient
Partial Class FileSizeReport
    Inherits System.Web.UI.Page

  Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
    If e.Row.RowType = DataControlRowType.DataRow Then



    End If
  End Sub

  Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click


    For i = 1 To 300
      Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
      con.Open()
      Dim sqlstring As String = "Select RTRIM(AddendumLinkToPDFcopy) AS AddendumLinkToPDFcopy FROM [Table_Addendums] WHERE AddendumID=" + i.ToString
      Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                Try
                    Dim fs As New System.IO.FileInfo(Server.MapPath(dr(0).ToString))
                    Dim fileLength As Integer = Convert.ToInt32(fs.Length)
                    Dim con2 As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
                    con2.Open()
                    Dim sqlstring2 As String = "INSERT INTO [Table_PDFsizeAddendum] ([AddendumID], FileSize) VALUES (" + i.ToString + " ," + fileLength.ToString + ")"
                    Dim cmd2 As New SqlCommand(sqlstring2, con2)
                    cmd2.CommandType = System.Data.CommandType.Text
                    Dim dr2 As SqlDataReader = cmd2.ExecuteReader
          con2.Close()
          dr2.Close()

        Catch ex As Exception

        End Try

      End While

      con.Close()
      dr.Close()

    Next

  End Sub
End Class
