Imports Microsoft.VisualBasic
Imports System.Net


Namespace PTS_MERCURY.helper

Public Class Table2_PONo

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

        Shared Function CountItems(_PO_no As String) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table2_PONo Where C.PO_No = _PO_no Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_PO_No As String) As PTS_MERCURY.db.Table2_PONo

            Dim _return As PTS_MERCURY.db.Table2_PONo

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItems(_PO_No) > 0 Then

                    _return = (From C In db.Table2_PONo Where C.PO_No = _PO_No).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function


        Shared Function GetDropDownListProject_ID() As IQueryable(Of db.Table2_PONo)

            Return From C In db.Table2_PONo

        End Function

        Shared Function ReturnHTMLfromURL(PO_No As String) As String

            Dim myClient As New WebClient()
            Dim _return As String = ""
            Dim variable As String = ""

            'Dim _localhost As String = HttpContext.Current.Request.Url.Scheme + System.Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host + (IIf(HttpContext.Current.Request.Url.IsDefaultPort, "", ":" + HttpContext.Current.Request.Url.Port.ToString()))
            Dim _localhost As String = PTS_MERCURY.helper.Garbage.MagicString.LocalHostAdress

            _return = myClient.DownloadString(_localhost + "/Pages/PTS/EmailBodies/" + "PoDetails" + ".aspx?" + PTS_MERCURY.helper.Garbage.QueryStringParameter.PO_No + "=" + PO_No)

            Return _return

        End Function

        Shared Function IfPoCorrespondsToSubcontractorContractOrAddendum(po_no As String) As Boolean

            Dim _return As Boolean = False

            Dim A As Integer = Aggregate C In db.Table_Contracts Where C.PO_No = po_no And C.FrameContract = False And C.ContractType = "sub" Into Count()

            If A > 0 Then
                _return = True
                Return _return
            End If

            Dim B As Integer = Aggregate C In db.Table_Contracts Join D In db.Table_Addendums On C.ContractID Equals D.ContractID
                                Where D.PO_No = po_no And C.FrameContract = False And C.ContractType = "sub" Into Count()

            If B > 0 Then
                _return = True
                Return _return
            End If

            Return _return

        End Function


    End Class
End Namespace

