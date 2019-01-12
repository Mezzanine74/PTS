Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class aspnet_Users

        Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

        Shared Function CountItems(_username As String) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.aspnet_Users Where C.UserName Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetUserByName(_username As String) As PTS_MERCURY.db.aspnet_Users

            Dim _return As PTS_MERCURY.db.aspnet_Users

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItems(_username) > 0 Then

                    _return = (From C In db.aspnet_Users Where C.UserName = _username).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function


        Shared Function CountItems() As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.aspnet_Users Into Count()

            End Using

            Return _return

        End Function

        Shared Function GetRowByUserName(_UserName As String) As PTS_MERCURY.db.aspnet_Users

            Dim _return As PTS_MERCURY.db.aspnet_Users

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
                If CountItems() > 0 Then

                    _return = (From C In db.aspnet_Users Where C.UserName = _UserName).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function

        Shared Function CountItemsByUserName(_username As String) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.aspnet_Users Where C.UserName = _username Into Count()

            End Using

            Return _return

        End Function

        Shared Function GetRowByPrimaryKey(_username As String) As PTS_MERCURY.db.aspnet_Users

            Dim _return As PTS_MERCURY.db.aspnet_Users

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItemsByUserName(_username) > 0 Then

                    _return = (From C In db.aspnet_Users Where C.UserName = _username).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function

        Shared Function GetDropDownListUserName() As IQueryable(Of db.aspnet_Users)

            Return From C In db.aspnet_Users Order By C.UserName

        End Function


    End Class

End Namespace

