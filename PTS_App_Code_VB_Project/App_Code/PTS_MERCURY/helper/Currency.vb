Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class Currency

        Public Class Currency_DDL

            Property Currency As String
            Property Currency_Description As String

        End Class

        Shared Function GetCurrency_DDL() As IQueryable(Of Currency_DDL)

            Dim A As New List(Of Currency_DDL)

            A.Add(New Currency_DDL With {.Currency = "Rub", .Currency_Description = "Ruble"})
            A.Add(New Currency_DDL With {.Currency = "Dollar", .Currency_Description = "Dollar"})
            A.Add(New Currency_DDL With {.Currency = "Euro", .Currency_Description = "Euro"})

            Return A.AsQueryable().Select(Function(e) New Currency_DDL With {.Currency = e.Currency, .Currency_Description = e.Currency_Description}).OrderBy(Function(e) e.Currency_Description)

        End Function

        Shared Function CountItemsByCurrency(_Currency As String) As Integer

            Dim _return As Integer = 0

            _return = Aggregate C In GetCurrency_DDL() Where C.Currency = _Currency Into Count()

            Return _return

        End Function

        Shared Function GetRowByPrimaryKey(_Currency As String) As Currency_DDL

            Dim _return As Currency_DDL

            If CountItemsByCurrency(_Currency) > 0 Then

                _return = (From C In GetCurrency_DDL() Where C.Currency = _Currency).ToList()(0)

            Else

                _return = Nothing

            End If

            Return _return

        End Function

    End Class

End Namespace
