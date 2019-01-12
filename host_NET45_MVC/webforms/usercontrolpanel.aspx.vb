Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging

Partial Class usercontrolpanel
    Inherits System.Web.UI.Page

    Dim currentuser As String = Page.User.Identity.Name.ToLower()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Or IsPostBack Then
            SqlDataSourceSendEmail.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name
            SqlDataSourceSendEmail.UpdateParameters("UserName").DefaultValue = Page.User.Identity.Name
        End If

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If (Aggregate C In db.Table_UserPhotos Where C.UserName = currentuser Into Count()) > 0 Then
                ImageAvatar.ImageUrl = (From C In db.Table_UserPhotos Where C.UserName = currentuser).ToList()(0).Photo
            Else
                ImageAvatar.ImageUrl = String.Empty
            End If

            db.Dispose()

        End Using

    End Sub

    Protected Sub SendEmailPaymentReqCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ProvideParameters()
    End Sub

    Protected Sub FormViewSendEmail_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdatedEventArgs) Handles FormViewSendEmail.ItemUpdated
    End Sub

    Protected Sub SendEmailApprovalCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ProvideParameters()
    End Sub

    Protected Sub SendEmailContractCheckBox_CheckedChanged(sender As Object, e As System.EventArgs)
        ProvideParameters()
    End Sub

    Protected Sub SendEmailPaymentListCheckBox_CheckedChanged(sender As Object, e As System.EventArgs)
        ProvideParameters()
    End Sub

    Protected Sub ProvideParameters()
        Dim SendEmailPaymentReqCheckBox As CheckBox = FormViewSendEmail.FindControl("SendEmailPaymentReqCheckBox")
        If SendEmailPaymentReqCheckBox IsNot Nothing Then
            SqlDataSourceSendEmail.UpdateParameters("SendEmailPaymentReq").Type = TypeCode.Boolean
            SqlDataSourceSendEmail.UpdateParameters("SendEmailPaymentReq").DefaultValue = SendEmailPaymentReqCheckBox.Checked
        End If

        Dim SendEmailApprovalCheckBox As CheckBox = FormViewSendEmail.FindControl("SendEmailApprovalCheckBox")
        If SendEmailApprovalCheckBox IsNot Nothing Then
            SqlDataSourceSendEmail.UpdateParameters("SendEmailApproval").Type = TypeCode.Boolean
            SqlDataSourceSendEmail.UpdateParameters("SendEmailApproval").DefaultValue = SendEmailApprovalCheckBox.Checked
        End If

        Dim SendEmailContractCheckBox As CheckBox = FormViewSendEmail.FindControl("SendEmailContractCheckBox")
        If SendEmailContractCheckBox IsNot Nothing Then
            SqlDataSourceSendEmail.UpdateParameters("SendEmailContract").Type = TypeCode.Boolean
            SqlDataSourceSendEmail.UpdateParameters("SendEmailContract").DefaultValue = SendEmailContractCheckBox.Checked
        End If

        Dim SendEmailPaymentListCheckBox As CheckBox = FormViewSendEmail.FindControl("SendEmailPaymentListCheckBox")
        If SendEmailPaymentListCheckBox IsNot Nothing Then
            SqlDataSourceSendEmail.UpdateParameters("SendEmailPayments").Type = TypeCode.Boolean
            SqlDataSourceSendEmail.UpdateParameters("SendEmailPayments").DefaultValue = SendEmailPaymentListCheckBox.Checked
        End If

        SqlDataSourceSendEmail.Update()
        Dim Notification As New _GiveNotification
        Notification._GiveNotification(Page.Request.Headers("X-Requested-With"), Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + ">Updated</p>")

    End Sub

    Protected Sub Upload_Click(sender As Object, e As EventArgs)

        If FileUploadAvatar.HasFile Then

            If System.IO.Path.GetExtension(FileUploadAvatar.PostedFile.FileName).ToLower = ".jpeg" OrElse _
                System.IO.Path.GetExtension(FileUploadAvatar.PostedFile.FileName).ToLower = ".jpg" OrElse _
                System.IO.Path.GetExtension(FileUploadAvatar.PostedFile.FileName).ToLower = ".png" OrElse _
                System.IO.Path.GetExtension(FileUploadAvatar.PostedFile.FileName).ToLower = ".bmp" Then

                If Directory.Exists(Server.MapPath("~/images/user_photos/")) Then
                    Dim UniqueString1 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                    Dim _link1 As String = "~/images/user_photos/" + currentuser + UniqueString1 + _
                                                   System.IO.Path.GetExtension(FileUploadAvatar.PostedFile.FileName)

                    FileUploadAvatar.SaveAs(MapPath(_link1))

                    Dim _bitmap As System.Drawing.Bitmap = CreateThumbnail(Server.MapPath(_link1), 168, 168)

                    Dim UniqueString2 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                    Dim _link2 As String = "~/images/user_photos/" + currentuser + UniqueString2 + ".jpg"

                    _bitmap.Save(Server.MapPath(_link2), System.Drawing.Imaging.ImageFormat.Jpeg)
                    _bitmap.Dispose()

                    Try
                        System.IO.File.Delete(MapPath(_link1))
                    Catch ex As Exception

                    End Try

                    Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                        If (Aggregate C In db.Table_UserPhotos Where C.UserName = currentuser Into Count()) = 0 Then

                            Dim entity = New PTS_MERCURY.db.Table_UserPhotos
                            entity.UserName = currentuser
                            entity.Photo = _link2

                            db.Table_UserPhotos.Attach(entity)
                            db.Table_UserPhotos.Add(entity)
                            db.SaveChanges()

                        Else

                            Dim entity = (From C In db.Table_UserPhotos Where C.UserName = currentuser).ToList()(0)

                            entity.Photo = _link2

                            db.SaveChanges()

                        End If

                        db.Dispose()

                    End Using

                    _GiveNotification.Gritter_Error(Page, "Success", "Your avatar successfully changed! It will appear once you refresh page")

                End If

            Else

                ' give error message
                _GiveNotification.Gritter_Error(Page, "Error", "Photo format should be jpeg, bnp or bmp! " , "error")

            End If

        End If

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If (Aggregate C In db.Table_UserPhotos Where C.UserName = currentuser Into Count()) > 0 Then
                ImageAvatar.ImageUrl = (From C In db.Table_UserPhotos Where C.UserName = currentuser).ToList()(0).Photo
            Else
                ImageAvatar.ImageUrl = String.Empty
            End If

            db.Dispose()

        End Using

    End Sub

    Public Shared Function CreateThumbnail(lcFilename As String, lnWidth As Integer, lnHeight As Integer) As Bitmap

        Dim bmpOut As System.Drawing.Bitmap = Nothing

        Dim loBMP As New Bitmap(lcFilename)
        Dim loFormat As ImageFormat = loBMP.RawFormat

        Dim lnRatio As Decimal
        Dim lnNewWidth As Integer = 0
        Dim lnNewHeight As Integer = 0

        If loBMP.Width < lnWidth AndAlso loBMP.Height < lnHeight Then
            Return loBMP
        End If

        If loBMP.Width > loBMP.Height Then
            lnRatio = CDec(lnWidth) / loBMP.Width
            lnNewWidth = lnWidth
            Dim lnTemp As Decimal = loBMP.Height * lnRatio
            lnNewHeight = CInt(Math.Truncate(lnTemp))
        Else
            lnRatio = CDec(lnHeight) / loBMP.Height
            lnNewHeight = lnHeight
            Dim lnTemp As Decimal = loBMP.Width * lnRatio
            lnNewWidth = CInt(Math.Truncate(lnTemp))
        End If


        bmpOut = New Bitmap(lnNewWidth, lnNewHeight)
        Dim g As Graphics = Graphics.FromImage(bmpOut)
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
        g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight)
        g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight)

        loBMP.Dispose()

        Return bmpOut
    End Function
End Class
