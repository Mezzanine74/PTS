Imports System.Data.SqlClient

Partial Class PaymentsNotes
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    If Not IsPostBack Then
      EditorNotes.Content = getContent()
    End If
  End Sub

  Protected Function getContent() As String
    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    con.Open()
    Dim sqlstring As String = " SELECT Notes FROM Table_PaymentListNotes WHERE id = 1"
    Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim dr As SqlDataReader = cmd.ExecuteReader
        Dim Notes As String = ""
        While dr.Read
            Notes = dr(0).ToString
        End While
        con.Close()
        dr.Close()
        Return Notes
    End Function

    Protected Sub ButtonUpdate_Click(sender As Object, e As System.EventArgs) Handles ButtonUpdate.Click
        UpdateContent()
        EditorNotes.Content = getContent()
    End Sub

    Protected Sub UpdateContent()
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " UPDATE Table_PaymentListNotes SET Notes = @Notes WHERE id = 1"
        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim Notes As SqlParameter = cmd.Parameters.Add("@Notes", System.Data.SqlDbType.NVarChar)
        Notes.Value = EditorNotes.Content

    Dim dr As SqlDataReader = cmd.ExecuteReader
    con.Close()
    dr.Close()

    Dim Notification As New _GiveNotification
    Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Updated Successfully!</p>")


  End Sub

End Class
