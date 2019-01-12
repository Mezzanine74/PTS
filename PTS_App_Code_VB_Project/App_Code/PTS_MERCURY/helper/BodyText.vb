Imports Microsoft.VisualBasic
Imports System.Web

Namespace PTS_MERCURY.helper


    Public Class BodyTexts

        Shared Function GetTranslation() As IEnumerable(Of PTS_MERCURY.db.BodyText)

            Dim _return As IEnumerable(Of PTS_MERCURY.db.BodyText)

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = (From C In db.BodyTexts).ToList()

            End Using

            Return _return

        End Function

        Shared Function Ref(RefNo As String, Optional lang As Lang = Lang._null) As String

            ' Translation session bos ise n/a gonder
            Dim _return As String = "n/a"

            Dim Translation As IEnumerable(Of PTS_MERCURY.db.BodyText) = Nothing
            If HttpContext.Current.Session("Translation") IsNot Nothing Then
                Translation = HttpContext.Current.Session("Translation")
            End If

            If Translation IsNot Nothing Then

                ' Referansta ismen bleirtilmisse oncelik tani
                If lang = BodyTexts.Lang.eng Then
                    Return Translation.Where(Function(c) c.Ref.Equals(RefNo))(0).Eng
                End If

                If lang = BodyTexts.Lang.rus Then
                    Return Translation.Where(Function(c) c.Ref.Equals(RefNo))(0).Rus
                End If

                ' Dropdown List secmisse ikinci onceligi tani
                If HttpContext.Current.Session("ddl_lang") IsNot Nothing Then
                    If HttpContext.Current.Session("ddl_lang").ToString = lang.eng.ToString() Then
                        Return Translation.Where(Function(c) c.Ref.Equals(RefNo))(0).Eng
                    End If

                    If HttpContext.Current.Session("ddl_lang").ToString = lang.rus.ToString() Then
                        Return Translation.Where(Function(c) c.Ref.Equals(RefNo))(0).Rus
                    End If
                End If

                ' DEFAULT, hicbirsey secilmemisse default gonder
                Return Translation.Where(Function(c) c.Ref.Equals(RefNo))(0).Rus

            End If

            Return _return

        End Function

        Shared Function CountText() As Integer
            Dim _return As Integer
            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.BodyTexts Into Count()

            End Using

            Return _return

        End Function

        Enum Lang
            _null
            eng
            rus
        End Enum

    End Class

End Namespace
