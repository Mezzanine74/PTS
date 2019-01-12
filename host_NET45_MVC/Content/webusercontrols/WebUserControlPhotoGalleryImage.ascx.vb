Imports System.Data.SqlClient

Partial Class Content_webusercontrols_WebUserControlUserPhoto
    Inherits System.Web.UI.UserControl

    Public PhotoLink As String = ""
    Public ThumbnailsLink As String = ""
    Public TitleName As String = ""

    Public Property PhotoSource() As String

        Get
            Return String.Empty
        End Get

        Set(value As String)

            PhotoLink = value.Replace("~/", "").Replace("~\", "")

        End Set

    End Property

    Public Property Thumbnails() As String

        Get
            Return String.Empty
        End Get

        Set(value As String)

            ThumbnailsLink = value.Replace("~/", "").Replace("~\", "")

        End Set

    End Property

    Public Property Title() As String

        Get
            Return String.Empty
        End Get

        Set(value As String)

            TitleName = value

        End Set

    End Property

End Class
