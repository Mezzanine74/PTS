Imports System.Data.SqlClient
Imports System.IO
Partial Class folderoptimization
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'FIND ALL FILES IN FOLDER
        Dim folders As String() = System.IO.Directory.GetDirectories(Server.MapPath("REQUEST\"))
        For Each folderName As String In folders

            Dim dir As New System.IO.DirectoryInfo(Server.MapPath(folderName.Replace("C:\HostingSpaces\savas\mercuryeng.org\wwwroot\", "")))
            For Each f As System.IO.FileInfo In dir.GetFiles("*.*")
                'Response.Write(f.Name.ToString)
                'Response.Write("<br/>")

                Dim cn As New System.Data.SqlClient.SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
                Dim cmd As New System.Data.SqlClient.SqlCommand()
                cmd.Connection = cn

                cmd.CommandText = "INSERT INTO [Table_F_FolderName]   ([FolderNames], [FileNames])  VALUES  (" + "'" + folderName.Replace("C:\HostingSpaces\savas\mercuryeng.org\wwwroot\REQUEST\", "") + "'" + ", " + "'" + "~/REQUEST/" + folderName.Replace("C:\HostingSpaces\savas\mercuryeng.org\wwwroot\REQUEST\", "") + "/" + f.Name.ToString + "'" + " )"
                cmd.CommandType = System.Data.CommandType.Text
                cn.Open()
                cmd.ExecuteNonQuery()
                cn.Close()

            Next
            'Exit For
        Next
        Response.Write("bitti aq")
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
        con.Open()
        Dim sqlstring As String = "SELECT TOP 50 rtrim([FileNames]) as Link  FROM [Table_F_FolderName] Where rtrim([FileNames]) like '%computershare%'"
        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim dr As SqlDataReader = cmd.ExecuteReader
        While dr.Read

            System.IO.File.Delete(Server.MapPath(dr(0).ToString))
            Response.Write(dr(0).ToString + " deleted")
            Response.Write("<br/>")

        End While

    End Sub

  Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
    Dim folders As String() = System.IO.Directory.GetDirectories(Server.MapPath("REQUEST\"))
    For Each folderName As String In folders
      Dim objFileInfo As FileInfo
      Dim objDir As DirectoryInfo = New DirectoryInfo(Server.MapPath(folderName.Replace("C:\HostingSpaces\savas\mercuryeng.org\wwwroot\", "")))
      Dim lngDirSize As Integer = 0
      For Each objFileInfo In objDir.GetFiles()
        lngDirSize = lngDirSize + objFileInfo.Length
      Next
      Response.Write(folderName + " > " + lngDirSize.ToString + "|")
    Next
  End Sub

  Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        'FIND ALL FILES IN FOLDER
            Dim dir As New System.IO.DirectoryInfo(Server.MapPath("~/"))
            For Each f As System.IO.FileInfo In dir.GetFiles("*.*")
                Response.Write(f.Name.ToString)
                Response.Write("<br/>")
            Next
  End Sub



End Class
