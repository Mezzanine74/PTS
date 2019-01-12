Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class Table8_ExchangeRates

        Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

        Shared Function CountItemsByDate(_Date As String) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table8_ExchangeRates Where C.Date = _Date Into Count()

            End Using

            Return _return

        End Function

        Shared Function GetRowByPrimaryKey(_Date As String) As PTS_MERCURY.db.Table8_ExchangeRates

            Dim _return As PTS_MERCURY.db.Table8_ExchangeRates

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItemsByDate(_Date) > 0 Then

                    _return = (From C In db.Table8_ExchangeRates Where C.Date = _Date).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function



    End Class

End Namespace
