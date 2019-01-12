Imports Microsoft.VisualBasic
Imports AjaxControlToolkit
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class ShowModalPopUpMessage

    ' !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    ' !!!!!!!!  TO WORK WITH THIS, SCRIPT MANAGER SHOULD BE DEFINED ON CHILD PAGE !!!!!!!!!!!!!!!!!!!!!
    ' !!!!!!!!  WEBUSERCONTROL SHOULD BE PLACED IN MASTER PAGE BUT OUT OF ALL CONTROLPLACES !!!!!!!!!!!
    ' !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    Shared Sub Show(_page As Page, _message As String)

        Dim WebUserControl_PopupMessage As UserControl = _page.Master.FindControl("WebUserControl_PopupMessage")

        Dim labelMessage As Label = WebUserControl_PopupMessage.FindControl("labelMessage")
        Dim ModalPopupExtenderCommon As ModalPopupExtender = WebUserControl_PopupMessage.FindControl("ModalPopupExtenderCommon")

        labelMessage.Text = _message
        ModalPopupExtenderCommon.Show()

    End Sub

End Class
