Imports Microsoft.VisualBasic

Public Class CalculateScenario

    Shared Function Calculate(ByVal _TotalPriceIncVAT As Decimal, ByVal _VAT As Decimal, ByVal _Currency As String) As Integer
        Dim _return As Integer = 0

        Dim TotalInEuroExcVAT As Decimal = 0

        If _Currency = "Rub" Then
            TotalInEuroExcVAT = _TotalPriceIncVAT / ((100 + _VAT) / 100) / (MaxExchRate.GetRubleEuro)
        ElseIf _Currency = "Dollar" Then
            TotalInEuroExcVAT = (_TotalPriceIncVAT / ((100 + _VAT) / 100)) * (MaxExchRate.GetRubleDollar) / (MaxExchRate.GetRubleEuro)
        ElseIf _Currency = "Euro" Then
            TotalInEuroExcVAT = (_TotalPriceIncVAT / ((100 + _VAT) / 100))
        End If

        TotalInEuroExcVAT = Math.Round(TotalInEuroExcVAT, 0)

        If TotalInEuroExcVAT > 0 AndAlso TotalInEuroExcVAT < 5000 Then
            _return = 1
        ElseIf TotalInEuroExcVAT >= 5000 AndAlso TotalInEuroExcVAT < 50000 Then
            _return = 2
        ElseIf TotalInEuroExcVAT >= 50000 AndAlso TotalInEuroExcVAT < 100000 Then
            _return = 3
        ElseIf TotalInEuroExcVAT >= 100000 AndAlso TotalInEuroExcVAT < 250000 Then
            _return = 4
        ElseIf TotalInEuroExcVAT >= 250000 AndAlso TotalInEuroExcVAT < 1000000 Then
            _return = 5
        ElseIf TotalInEuroExcVAT >= 1000000 AndAlso TotalInEuroExcVAT < 5000000 Then
            _return = 6
        ElseIf TotalInEuroExcVAT >= 5000000 Then
            _return = 7
        End If

        Return _return

    End Function

End Class
