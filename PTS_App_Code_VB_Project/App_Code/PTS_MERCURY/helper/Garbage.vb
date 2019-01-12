Imports Microsoft.VisualBasic
Imports System.Web.UI

Namespace PTS_MERCURY.helper

    Public Class Garbage

        Shared Function GetQueryString(ByVal _id As String) As String

            Dim _return As String = 0

            If System.Web.HttpContext.Current.Request.QueryString(_id) IsNot Nothing Then
                _return = System.Web.HttpContext.Current.Request.QueryString(_id)
            End If

            Return _return

        End Function

        Structure QueryStringParameter
            Const AddendumId = "AddendumId"
            Const ContractId = "ContractId"
            Const PO_No = "PO_No"
        End Structure

        Structure MagicString
            Const DeliveryDocs = "DeliveryDocs"
            Const Nakladnaya = "Nakladnaya"
            Const Akt = "Akt"
            Const AktToNakladnaya = "AktToNakladnaya"
            Const LocalHostAdress = "http://10.8.35.6"
            Const PTSHostAdress = "http://pts.mercuryeng.ru"
        End Structure

        Shared Function GetControlThatCausedPostBack(ByVal page As Page) As Control
            'initialize a control and set it to null
            Dim ctrl As Control = Nothing

            'get the event target name and find the control
            Dim ctrlName As String = page.Request.Params.[Get]("__EVENTTARGET")
            If Not [String].IsNullOrEmpty(ctrlName) Then
                ctrl = page.FindControl(ctrlName)
            End If

            'return the control to the calling method
            Return ctrl

        End Function

    End Class

End Namespace

