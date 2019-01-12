Imports System
Imports System.IO
Imports System.Net
Imports System.Data.SqlClient

Partial Class links
    Inherits System.Web.UI.Page

  Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

    For i = 1 To 17219
      Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
      con.Open()
      Dim sqlstring As String = "select rtrim(LinkToInvoice) as LinkToInvoice from Table4_PaymentRequest where payreqno = " + i.ToString
      Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim pathItem As String = ""
            While dr.Read

                Dim con2 As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
                con2.Open()
                Dim sqlstring2 As String = ""
                pathItem = Server.MapPath(dr(0).ToString)
                Dim fileItem As System.IO.FileInfo = New System.IO.FileInfo(pathItem)

                If fileItem.Exists Then
                    sqlstring2 = "update Table4_PaymentRequest set AttachmentExist = N'true' where PayReqNo = " + i.ToString
                Else
                    sqlstring2 = "update Table4_PaymentRequest set AttachmentExist = N'false' where PayReqNo = " + i.ToString
                End If

                Dim cmd2 As New SqlCommand(sqlstring2, con2)
                cmd2.CommandType = System.Data.CommandType.Text
                Dim dr2 As SqlDataReader = cmd2.ExecuteReader
        con2.Close()
        dr2.Close()

      End While
      con.Close()
      dr.Close()

    Next

  End Sub
End Class
