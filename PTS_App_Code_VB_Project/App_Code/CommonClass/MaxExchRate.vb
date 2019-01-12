Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class MaxExchRate

    Shared Function GetRubleDollar() As Decimal
        Return GetRate().RubleDollar
    End Function

    Shared Function GetRubleEuro() As Decimal
        Return GetRate().RubleEuro
    End Function

    Shared Function GetReferringDollar(ByVal _date As DateTime) As Decimal
        Return GetReferringRate(_date).RubleDollar
    End Function

    Shared Function GetReferringEuro(ByVal _date As DateTime) As Decimal
        Return GetReferringRate(_date).RubleEuro
    End Function

    Shared Function GetRate() As _MaxExchRate
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT [RubbleDollar],[RubbleEuro] FROM [View_MaxExchangeRate] "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text
            Dim ReturnValue As New _MaxExchRate
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                ReturnValue.RubleDollar = dr(0)
                ReturnValue.RubleEuro = dr(1)
            End While
            Return ReturnValue
            con.Close()
            dr.Close()
        End Using
    End Function

    Shared Function GetReferringRate(ByVal _date As DateTime) As _ReferingExchRate
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " if exists (select RubbleDollar, RubbleEuro from Table8_ExchangeRates where Date > @Date - 1 and Date < @Date) " + _
                                        " 	select RubbleDollar, RubbleEuro from Table8_ExchangeRates where Date > @Date - 1 and Date < @Date " + _
                                        " else " + _
                                        " select top 1 RubbleDollar, RubbleEuro from View_MaxExchangeRate order by date desc "
            Dim cmd As New SqlCommand(sqlstring, con)

            Dim Date_ As SqlParameter = cmd.Parameters.Add("@Date", Data.SqlDbType.SmallDateTime)
            Date_.Value = _date

            cmd.CommandType = Data.CommandType.Text
            Dim ReturnValue As New _ReferingExchRate
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                ReturnValue.RubleDollar = dr(0)
                ReturnValue.RubleEuro = dr(1)
            End While
            Return ReturnValue
            con.Close()
            dr.Close()
        End Using
    End Function

    Public Class _MaxExchRate
        Friend RubleDollar As Decimal
        Friend RubleEuro As Decimal
    End Class

    Public Class _ReferingExchRate
        Friend RubleDollar As Decimal
        Friend RubleEuro As Decimal
    End Class


End Class
