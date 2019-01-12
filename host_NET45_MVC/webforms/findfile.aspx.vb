
Partial Class findfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        'FIND ALL FILES IN FOLDER
        Dim dir As New System.IO.DirectoryInfo(Server.MapPath(DropDownList1.SelectedValue))
        For Each f As System.IO.FileInfo In dir.GetFiles("ÐÎ*.pdf")
            'For Each f As System.IO.FileInfo In dir.GetFiles("ÐÎ-75-002.pdf")
            'For Each f As System.IO.FileInfo In dir.GetFiles("ZZ*.pdf")

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(Server.MapPath(DropDownList1.SelectedValue) + f.Name.Replace("ÐÎ", "PO"))

            If file.Exists Then
            Else
                f.MoveTo(Server.MapPath(DropDownList1.SelectedValue) + f.Name.Replace("ÐÎ", "PO"))
                ListBox1.Items.Add(f.Name + " moved to > " + f.Name.Replace("ÐÎ", "PO"))

            End If

        Next
    End Sub
End Class
