Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.HttpBrowserCapabilities
Imports System.IO
Imports PTS_App_Code_VB_Project
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper

Partial Class site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

        If Page.User.Identity.Name.ToLower <> "savas" Then
            DDL_Lang.Visible = False
        End If


        If Not IsPostBack Then
            If Not Request.IsAuthenticated And Page.AppRelativeVirtualPath().Trim().IndexOf("login") = -1 Then

                TakeKeysToSession()

                Dim AllKeys As String = ""

                For Each key As String In Request.QueryString.AllKeys
                    If key <> "ReturnUrl" Then
                        AllKeys = AllKeys + "&" + key + "=" + Request.QueryString(key)
                    End If
                Next

                Response.Redirect("~/login.aspx" + "?ReturnUrl=" + Page.AppRelativeVirtualPath().Trim().Replace("~/", String.Empty) + AllKeys)

            ElseIf Not Request.IsAuthenticated And Page.AppRelativeVirtualPath().Trim().IndexOf("login") <> -1 Then

                TakeKeysToSession()

                If Request.QueryString("username") IsNot Nothing AndAlso Request.QueryString("password") IsNot Nothing Then
                    If Membership.ValidateUser(Request.QueryString("username"), Request.QueryString("password")) Then
                        FormsAuthentication.RedirectFromLoginPage(Request.QueryString("username"), False)
                    End If
                End If

            End If
        End If

        If Roles.IsUserInRole("asteros") And Page.AppRelativeVirtualPath().Trim().IndexOf("login") = -1 Then

            Response.Redirect("~/Pages/AsterosOrders/default.aspx")

        End If


        ' Translation kontrol kismi

        ' bunu buradan sonra kaldir, hata verdigi icin burada
        If Not IsPostBack Then
            Session("Translation") = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.GetTranslation()
        End If

        If Session("ddl_lang") Is Nothing Then
            ' DEFAULT language ENG
            Session("ddl_lang") = BodyTexts.Lang.rus
        End If

        If Not IsPostBack Then
            If Session("ddl_lang") IsNot Nothing Then
                ddl_lang.SelectedValue = Session("ddl_lang").ToString()
            End If
        End If

        If Session("CountBodyText") Is Nothing Then
            Session("CountBodyText") = BodyTexts.CountText()
            Dim Translation As IEnumerable(Of PTS_MERCURY.db.BodyText) = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.GetTranslation()
            Session("Translation") = Translation
        End If

        If Session("CountBodyText") IsNot Nothing Then
            Try
                If BodyTexts.CountText() > Session("CountBodyText") Then
                    Dim Translation As IEnumerable(Of PTS_MERCURY.db.BodyText) = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.GetTranslation()
                    Session("Translation") = Translation
                End If
            Catch ex As Exception

            End Try
        End If

    End Sub

    Protected Sub TakeKeysToSession()
        '******************************************************************************
        '******************************************************************************

        Session.Remove("_preserve_allkeys")

        Dim AllKeys As String = ""

        For Each key As String In Request.QueryString.AllKeys
            If key <> "ReturnUrl" Then
                If key.ToLower <> "username" Then
                    If key.ToLower <> "password" Then
                        AllKeys = AllKeys + "&" + key + "=" + Request.QueryString(key)
                    End If
                End If
            End If
        Next

        Session("_preserve_allkeys") = AllKeys

        '******************************************************************************
        '******************************************************************************
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.IsAuthenticated And Session("_preserve_allkeys") IsNot Nothing Then

            Dim url As String = Request.Url.AbsolutePath
            Dim updatedQueryString As String = "?" + Session("_preserve_allkeys")

            Session.Remove("_preserve_allkeys")

            Response.Redirect(url + updatedQueryString)

        End If

        Dim sPagePath As String = System.Web.HttpContext.Current.Request.Url.AbsolutePath
        Dim oFileInfo As System.IO.FileInfo = New System.IO.FileInfo(sPagePath)
        Dim sPageName As String = oFileInfo.Name

        Page.Title = "PTS- " + sPageName.Replace(".aspx", String.Empty)

        oFileInfo = Nothing

        If Request.IsAuthenticated Then

            If Not LoginStatus.LogoutText.IndexOf("ace-icon fa fa-power-off") > 0 Then
                LoginStatus.LogoutText = "<i class=" + """" + "ace-icon fa fa-power-off" + """" + "></i>" + LoginStatus.LogoutText
            End If

            LiteralWho.Text = Page.User.Identity.Name.ToLower
        Else
            If Not LoginStatus.LoginText.IndexOf("ace-icon fa fa-power-off") > 0 Then
                LoginStatus.LoginText = "<i class=" + """" + "ace-icon fa fa-power-off" + """" + "></i>" + LoginStatus.LoginText
            End If
        End If

        If Page.User.Identity.Name.ToLower = "auditers" Then
            If HttpContext.Current.Request.Url.AbsolutePath.ToLower <> "/contractview.aspx" Then
                Page.Visible = False
                Response.Write("<h1>You cannot go this page. Sorry!</h1>")

            End If
        End If

        ' XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        ' XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX THIS CODE FOLLOW USER  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        ' XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        Dim mycommantask As New MyCommonTasks
        mycommantask.UpdateFollowUsersTable(Page, SqlDataSourceFollowUsers)
        mycommantask = Nothing

        ' XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        ' XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX //THIS CODE FOLLOW USER  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        ' XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        ImageUser.UserName = Page.User.Identity.Name.ToString.Trim

    End Sub

    Protected Sub ddl_lang_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim ddl As DropDownList = sender

        Session("ddl_lang") = ddl.SelectedValue.ToString.ToLower().Trim

        'Dim ctrl As Control = Nothing
        'Dim ctrlPostback As String = Page.Request.Params.[Get]("__EVENTTARGET")

        ''Dim asdf As New MyCommonTasks
        ''asdf.SendEmailToAdmin(ctrlPostback.ToString(), "")

        ''If ctrlPostback.Trim.Length > 0 Then
        ''    ctrl = Page.FindControl(ctrlPostback)
        ''End If

        ''If ctrl IsNot Nothing Then

        ''    If ctrl.ClientID.ToString() = "ddl_lang" Then
        ''        Dim ddl As DropDownList = ctrl
        ''        Session("ddl_lang") = ddl.SelectedValue.ToString.ToLower().Trim

        ''    End If

        ''End If

    End Sub
End Class