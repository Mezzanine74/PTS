Imports Microsoft.VisualBasic
Imports System.Web.HttpContext
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls

Public Class PTSMainClass

    Shared Sub ProvideImageFromFile(ByVal _imageButton As ImageButton, ByVal _link As String)

        If _link IsNot Nothing Then
            Dim linktoDoc As String = _link.Trim()
            Dim ImageButtonItem As ImageButton = _imageButton

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(Current.Server.MapPath(linktoDoc))
            If file.Exists Then
                If ImageButtonItem IsNot Nothing Then
                    For i = 1 To Len(linktoDoc) - 1
                        If Mid(linktoDoc, Len(linktoDoc) - i, 1) = "." Then

                            Dim extension As String = Mid(linktoDoc, Len(linktoDoc) - i, i + 1)

                            If extension = ".xls" Then
                                ' excel old
                                ImageButtonItem.ImageUrl = "~/Images/excel.png"
                            ElseIf extension = ".xlsx" Then
                                ' excel new
                                ImageButtonItem.ImageUrl = "~/Images/excel.png"
                            ElseIf extension = ".doc" Then
                                ' word old
                                ImageButtonItem.ImageUrl = "~/Images/doc.png"
                            ElseIf extension = ".docx" Then
                                ' word new
                                ImageButtonItem.ImageUrl = "~/Images/doc.png"
                            ElseIf extension = ".pdf" Then
                                ' pdf
                                ImageButtonItem.ImageUrl = "~/Images/pdf.png"
                            ElseIf extension = ".zip" Then
                                ' zip
                                ImageButtonItem.ImageUrl = "~/Images/zipicon.png"
                            ElseIf extension = ".rar" Then
                                ' rar
                                ImageButtonItem.ImageUrl = "~/Images/rar.png"
                            End If

                        End If
                    Next
                End If
            Else
                ImageButtonItem.ImageUrl = "~/Images/ContractNotApprove.png"
                ImageButtonItem.Enabled = False
            End If
        End If

    End Sub

    Shared Function GetRubWithVATToPay(ByVal _PayReqNo As Integer, ByVal _Dollar As Decimal, ByVal _Euro As Decimal) As Decimal

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SP_GetRubleWithVATToPay"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim PayReqNo As SqlParameter = cmd.Parameters.Add("@PayReqNo", Data.SqlDbType.Int)
            PayReqNo.Value = _PayReqNo
            Dim Dollar As SqlParameter = cmd.Parameters.Add("@Dollar", Data.SqlDbType.Decimal)
            Dollar.Value = _Dollar
            Dim Euro As SqlParameter = cmd.Parameters.Add("@Euro", Data.SqlDbType.Decimal)
            Euro.Value = _Euro

            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read
                Return dr(0)
            End While

            con.Close()
            dr.Close()
        End Using

    End Function

    Shared Sub ExportGridExcel(ByVal _grid As GridView, ByVal _filenameWithoutExtension As String)

        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.ContentType = "application/vnd.xls"
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + _filenameWithoutExtension + ".xls")
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode
        HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())
        HttpContext.Current.Response.Charset = ""

        Dim swriter As New StringWriter()
        Dim hwriter As New HtmlTextWriter(swriter)

        Dim frm As New HtmlForm()
        _grid.Parent.Controls.Add(frm)
        frm.Attributes("runat") = "server"
        frm.Controls.Add(_grid)
        frm.RenderControl(hwriter)

        HttpContext.Current.Response.Write(swriter.ToString())
        HttpContext.Current.Response.[End]()

    End Sub

    Shared Function GetDollarFromDate(ByVal _Date As DateTime) As Decimal

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SP_GetExcRateFromDate"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim Date_ As SqlParameter = cmd.Parameters.Add("@Date", Data.SqlDbType.DateTime)
            Date_.Value = _Date

            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read

                Return dr(0)

            End While

            con.Close()
            dr.Close()
        End Using

    End Function

    Shared Function GetEuroFromDate(ByVal _Date As DateTime) As Decimal

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SP_GetExcRateFromDate"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim Date_ As SqlParameter = cmd.Parameters.Add("@Date", Data.SqlDbType.DateTime)
            Date_.Value = _Date

            Dim dr As SqlDataReader = cmd.ExecuteReader

            While dr.Read

                Return dr(1)

            End While

            con.Close()
            dr.Close()
        End Using

    End Function

    Shared Function FileExist(ByVal _link As String) As Boolean

        Dim path As String = Current.Server.MapPath(_link)
        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)

        If file.Exists Then
            Return True
        Else
            Return False
        End If

    End Function

    Shared Sub OpenFile(ByVal _page As Page, ByVal _link As String)

        Dim path As String = Current.Server.MapPath(_link)
        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
        If file.Exists Then
            Current.Response.Clear()
            Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
            Current.Response.AddHeader("Content-Length", file.Length.ToString())
            Current.Response.ContentType = "application/octet-stream"
            Current.Response.WriteFile(file.FullName)
            Current.Response.End()
            'Current.Response.Write("find in local file")
        Else
            _GiveNotification.Gritter_Error(_page, "Error", "File doesnt exist", "error")

        End If

    End Sub

    Shared Function GetNameSurnameFromUserName(ByVal _username As String) As String

        Try
            Dim email As String = PTS_MERCURY.db.QuickTables.aspnet_Membership(PTS_MERCURY.db.QuickTables.aspnet_Users(_username).UserId).LoweredEmail.Trim.ToLower

            Dim position As Integer = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase _
                                      (email).IndexOf("@")

            Return (Mid(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(email), 1, position)).Replace(".", " ")

        Catch ex As Exception

        End Try

    End Function

    Shared Function GetPhoto(ByVal _username As String) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SELECT RTRIM(Photo) AS Link FROM [dbo].[Table_UserPhotos] WHERE UserName = @UserName"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            'syntax for parameter adding
            Dim UserParm As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar, 256)
            UserParm.Value = _username
            Dim dr As SqlDataReader = cmd.ExecuteReader

            If Not dr.HasRows Then
                Return "~/assets/avatars/avatar2.png"
            End If

            While dr.Read

                Return dr(0).ToString

            End While


            con.Close()
            dr.Close()
        End Using

    End Function

End Class
