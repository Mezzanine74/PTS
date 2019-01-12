Imports System.Data.SqlClient
Imports PTS_App_Code_VB_Project

Partial Class Content_webusercontrols_WebUserControlUserPhoto
    Inherits System.Web.UI.UserControl

    Public Property CssClass() As String

        Get
            Return ImageUserPhoto.CssClass
        End Get

        Set(value As String)

            ImageUserPhoto.CssClass = value

        End Set

    End Property

    Public Property UserName() As String

        Get
            Return String.Empty
        End Get

        Set(value As String)
            ImageUserPhoto.ImageUrl = PTSMainClass.GetPhoto(value)
            ImageUserPhoto.ToolTip = If(String.IsNullOrEmpty(PTSMainClass.GetNameSurnameFromUserName(value)), value, PTSMainClass.GetNameSurnameFromUserName(value))
        End Set

    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If String.IsNullOrEmpty(Me.CssClass) Then
            ImageUserPhoto.CssClass = "userphoto tooltip-success"
        End If

    End Sub
End Class
