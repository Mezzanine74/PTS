Imports PTS_App_Code_VB_Project

Partial Class _Error2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Request.QueryString("ErrorID") IsNot Nothing Then
            Using Adapter As New ErrorLogTableAdapters.Table_ErrorLogTableAdapter
                Dim table As New ErrorLog.Table_ErrorLogDataTable

                table = Adapter.GetDataByErrorID(Request.QueryString("ErrorID"))

                For Each _row As ErrorLog.Table_ErrorLogRow In table
                    LiteralErrorMessage.Text = _row.ErrorHTML
                Next

                table.Dispose()
                Adapter.Dispose()

            End Using
        End If

    End Sub
End Class
