Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

Public Class Table5_PayLog

Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities



        Shared Function MaxId() As Int32

            Dim _return As Int32 = 0

            If (Aggregate C In db.Table5_PayLog Into Count()) > 0 Then
                _return = (Aggregate C In db.Table5_PayLog Into Max(C.PayReqNo))
            End If

            Return _return + 1

        End Function


        Shared Function CountItems() As Integer 

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table5_PayLog Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_PayReqNo As Int32) As PTS_MERCURY.db.Table5_PayLog

            Dim _return As PTS_MERCURY.db.Table5_PayLog

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If CountItems() > 0 Then

              _return = (From C In db.Table5_PayLog Where C.PayReqNo = _PayReqNo).ToList()(0)

            Else

               _return = Nothing

            End If

            End Using

            Return _return

        End Function


        Shared Function GetDropDownListFinanceNo() As IQueryable(Of db.Table5_PayLog)

            Return From C In db.Table5_PayLog

        End Function

    End Class
End Namespace

