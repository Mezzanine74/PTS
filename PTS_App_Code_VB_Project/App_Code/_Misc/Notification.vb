Imports Microsoft.VisualBasic
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Web.HttpContext
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports AjaxControlToolkit
Imports System.Web.UI
Imports System.Web.UI.HtmlControls

Public Class _GiveNotification
    Public Sub _GiveNotification(ByVal _Ajax As String _
                                 , ByVal CurrentPage As Page, _
                                 ByVal Message As String)

        'If _Ajax = "XMLHttpRequest" Then
        '  Dim BuildScript As New System.Text.StringBuilder
        '  BuildScript.Append("<script>")
        '  BuildScript.Append(Environment.NewLine)
        '  BuildScript.Append("$.sticky('" + Message + "');")
        '  BuildScript.Append(Environment.NewLine)
        '  BuildScript.Append("</" + "script>")
        '  AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript _
        '  (CurrentPage, CurrentPage.[GetType](), "asd", BuildScript.ToString(), False)
        'Else
        '  Dim BuildScript As New System.Text.StringBuilder
        '  Dim cs As ClientScriptManager = CurrentPage.ClientScript
        '  BuildScript.Append("<script>")
        '  BuildScript.Append(Environment.NewLine)
        '  BuildScript.Append("$.sticky('" + Message + "');")
        '  BuildScript.Append(Environment.NewLine)
        '  BuildScript.Append("</" + "script>")
        '  cs.RegisterStartupScript(Me.[GetType](), "asd", BuildScript.ToString())
        'End If

        Gritter_Error(CurrentPage, "Notification", Message, "default")

    End Sub

    Shared Sub Gritter_Error(ByVal CurrentPage As Page, ByVal title As String, ByVal message As String, Optional ByVal position As String = "default")

        Dim input As HtmlGenericControl = New HtmlGenericControl("input")
        input.Attributes.Add("id", "gritter-light")
        input.Attributes.Add("checked", "")
        input.Attributes.Add("type", "checkbox")
        input.Attributes.Add("class", "ace ace-switch ace-switch-5")
        CurrentPage.Controls.Add(input)

        If position = "default" Then
            ScriptManager.RegisterClientScriptBlock(CurrentPage, GetType(Page), "alert", "$(function () { " + _
                    " $.gritter.add({ " + _
                    "     title: '" + title + "', " + _
                    "     text: '" + message + "', " + _
                    "     time: '" + "3000" + "', " + _
                    "     class_name: 'gritter-info gritter-top-center' + (!$('#gritter-light').get(0).checked ? ' gritter-light' : '') " + _
                    " }); " + _
                "});", True)

            ' dont use tis anymore. it might be used under Nakladnaya page etc. USE "error" instead
        ElseIf position = "center" Then
            ScriptManager.RegisterClientScriptBlock(CurrentPage, GetType(Page), "alert", "$(function () { " + _
                    " $.gritter.add({ " + _
                    "     title: '" + title + "', " + _
                    "     text: '" + message + "', " + _
                    "     time: '" + "3000" + "', " + _
                    "     class_name: 'gritter-error gritter-top-center' + (!$('#gritter-light').get(0).checked ? ' gritter-light' : '') " + _
                    " }); " + _
                "});", True)

        ElseIf position = "error" Then
            ScriptManager.RegisterClientScriptBlock(CurrentPage, GetType(Page), "alert", "$(function () { " + _
                    " $.gritter.add({ " + _
                    "     title: '" + title + "', " + _
                    "     text: '" + message + "', " + _
                    "     time: '" + "3000" + "', " + _
                    "     class_name: 'gritter-error gritter-top-center' + (!$('#gritter-light').get(0).checked ? ' gritter-light' : '') " + _
                    " }); " + _
                "});", True)

        ElseIf position = "success" Then
            ScriptManager.RegisterClientScriptBlock(CurrentPage, GetType(Page), "alert", "$(function () { " + _
                    " $.gritter.add({ " + _
                    "     title: '" + title + "', " + _
                    "     text: '" + message + "', " + _
                    "     time: '" + "3000" + "', " + _
                    "     class_name: 'gritter-success gritter-top-center' + (!$('#gritter-light').get(0).checked ? ' gritter-light' : '') " + _
                    " }); " + _
                "});", True)

        End If

    End Sub

End Class
