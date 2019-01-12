Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic

Public Class YahooFinance

    Public Shared Function GetCruedOil() As String

        Try
            Using webClient = New System.Net.WebClient()


                Dim json = webClient.DownloadString("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%3D%22CLJ15.NYM%22&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys")

                Dim parser As New JavaScriptSerializer()
                Dim _rootObject As New YahooFinance.RootObject
                _rootObject = parser.Deserialize(Of YahooFinance.RootObject)(json)

                Dim _return As String = ""

                If Mid(_rootObject.query.results.quote.ChangeinPercent.ToString, 1, 1) = "+" Then
                    _return = "<a href=" + """" + "http://finance.yahoo.com/q?s=clj15.nym" + """" + " target=" + """" + "_blank" + """" + " style=" + """" + "color:blue;" + """" + ">Crude Oil </a>" + _rootObject.query.results.quote.LastTradePriceOnly.ToString + "<span style=" + """" + "color:green;" + """" + "> " + _rootObject.query.results.quote.ChangeinPercent.ToString + "</span>"
                ElseIf Mid(_rootObject.query.results.quote.ChangeinPercent.ToString, 1, 1) = "-" Then
                    _return = "<a href=" + """" + "http://finance.yahoo.com/q?s=clj15.nym" + """" + " target=" + """" + "_blank" + """" + " style=" + """" + "color:blue;" + """" + ">Crude Oil </a>" + _rootObject.query.results.quote.LastTradePriceOnly.ToString + "<span style=" + """" + "color:red;" + """" + "> " + _rootObject.query.results.quote.ChangeinPercent.ToString + "</span>"
                End If

                _rootObject = Nothing
                webClient.Dispose()

                Return _return

            End Using

        Catch ex As Exception

        End Try


    End Function

    Public Class Quote
        Public Property symbol() As String
            Get
                Return m_symbol
            End Get
            Set(value As String)
                m_symbol = value
            End Set
        End Property
        Private m_symbol As String
        Public Property Ask() As Object
            Get
                Return m_Ask
            End Get
            Set(value As Object)
                m_Ask = value
            End Set
        End Property
        Private m_Ask As Object
        Public Property AverageDailyVolume() As String
            Get
                Return m_AverageDailyVolume
            End Get
            Set(value As String)
                m_AverageDailyVolume = value
            End Set
        End Property
        Private m_AverageDailyVolume As String
        Public Property Bid() As Object
            Get
                Return m_Bid
            End Get
            Set(value As Object)
                m_Bid = value
            End Set
        End Property
        Private m_Bid As Object
        Public Property AskRealtime() As String
            Get
                Return m_AskRealtime
            End Get
            Set(value As String)
                m_AskRealtime = value
            End Set
        End Property
        Private m_AskRealtime As String
        Public Property BidRealtime() As String
            Get
                Return m_BidRealtime
            End Get
            Set(value As String)
                m_BidRealtime = value
            End Set
        End Property
        Private m_BidRealtime As String
        Public Property BookValue() As String
            Get
                Return m_BookValue
            End Get
            Set(value As String)
                m_BookValue = value
            End Set
        End Property
        Private m_BookValue As String
        Public Property Change_PercentChange() As String
            Get
                Return m_Change_PercentChange
            End Get
            Set(value As String)
                m_Change_PercentChange = value
            End Set
        End Property
        Private m_Change_PercentChange As String
        Public Property Change() As String
            Get
                Return m_Change
            End Get
            Set(value As String)
                m_Change = value
            End Set
        End Property
        Private m_Change As String
        Public Property Commission() As Object
            Get
                Return m_Commission
            End Get
            Set(value As Object)
                m_Commission = value
            End Set
        End Property
        Private m_Commission As Object
        Public Property Currency() As String
            Get
                Return m_Currency
            End Get
            Set(value As String)
                m_Currency = value
            End Set
        End Property
        Private m_Currency As String
        Public Property ChangeRealtime() As String
            Get
                Return m_ChangeRealtime
            End Get
            Set(value As String)
                m_ChangeRealtime = value
            End Set
        End Property
        Private m_ChangeRealtime As String
        Public Property AfterHoursChangeRealtime() As String
            Get
                Return m_AfterHoursChangeRealtime
            End Get
            Set(value As String)
                m_AfterHoursChangeRealtime = value
            End Set
        End Property
        Private m_AfterHoursChangeRealtime As String
        Public Property DividendShare() As String
            Get
                Return m_DividendShare
            End Get
            Set(value As String)
                m_DividendShare = value
            End Set
        End Property
        Private m_DividendShare As String
        Public Property LastTradeDate() As String
            Get
                Return m_LastTradeDate
            End Get
            Set(value As String)
                m_LastTradeDate = value
            End Set
        End Property
        Private m_LastTradeDate As String
        Public Property TradeDate() As Object
            Get
                Return m_TradeDate
            End Get
            Set(value As Object)
                m_TradeDate = value
            End Set
        End Property
        Private m_TradeDate As Object
        Public Property EarningsShare() As String
            Get
                Return m_EarningsShare
            End Get
            Set(value As String)
                m_EarningsShare = value
            End Set
        End Property
        Private m_EarningsShare As String
        Public Property ErrorIndicationreturnedforsymbolchangedinvalid() As Object
            Get
                Return m_ErrorIndicationreturnedforsymbolchangedinvalid
            End Get
            Set(value As Object)
                m_ErrorIndicationreturnedforsymbolchangedinvalid = value
            End Set
        End Property
        Private m_ErrorIndicationreturnedforsymbolchangedinvalid As Object
        Public Property EPSEstimateCurrentYear() As String
            Get
                Return m_EPSEstimateCurrentYear
            End Get
            Set(value As String)
                m_EPSEstimateCurrentYear = value
            End Set
        End Property
        Private m_EPSEstimateCurrentYear As String
        Public Property EPSEstimateNextYear() As String
            Get
                Return m_EPSEstimateNextYear
            End Get
            Set(value As String)
                m_EPSEstimateNextYear = value
            End Set
        End Property
        Private m_EPSEstimateNextYear As String
        Public Property EPSEstimateNextQuarter() As String
            Get
                Return m_EPSEstimateNextQuarter
            End Get
            Set(value As String)
                m_EPSEstimateNextQuarter = value
            End Set
        End Property
        Private m_EPSEstimateNextQuarter As String
        Public Property DaysLow() As String
            Get
                Return m_DaysLow
            End Get
            Set(value As String)
                m_DaysLow = value
            End Set
        End Property
        Private m_DaysLow As String
        Public Property DaysHigh() As String
            Get
                Return m_DaysHigh
            End Get
            Set(value As String)
                m_DaysHigh = value
            End Set
        End Property
        Private m_DaysHigh As String
        Public Property YearLow() As String
            Get
                Return m_YearLow
            End Get
            Set(value As String)
                m_YearLow = value
            End Set
        End Property
        Private m_YearLow As String
        Public Property YearHigh() As String
            Get
                Return m_YearHigh
            End Get
            Set(value As String)
                m_YearHigh = value
            End Set
        End Property
        Private m_YearHigh As String
        Public Property HoldingsGainPercent() As String
            Get
                Return m_HoldingsGainPercent
            End Get
            Set(value As String)
                m_HoldingsGainPercent = value
            End Set
        End Property
        Private m_HoldingsGainPercent As String
        Public Property AnnualizedGain() As Object
            Get
                Return m_AnnualizedGain
            End Get
            Set(value As Object)
                m_AnnualizedGain = value
            End Set
        End Property
        Private m_AnnualizedGain As Object
        Public Property HoldingsGain() As Object
            Get
                Return m_HoldingsGain
            End Get
            Set(value As Object)
                m_HoldingsGain = value
            End Set
        End Property
        Private m_HoldingsGain As Object
        Public Property HoldingsGainPercentRealtime() As String
            Get
                Return m_HoldingsGainPercentRealtime
            End Get
            Set(value As String)
                m_HoldingsGainPercentRealtime = value
            End Set
        End Property
        Private m_HoldingsGainPercentRealtime As String
        Public Property HoldingsGainRealtime() As Object
            Get
                Return m_HoldingsGainRealtime
            End Get
            Set(value As Object)
                m_HoldingsGainRealtime = value
            End Set
        End Property
        Private m_HoldingsGainRealtime As Object
        Public Property MoreInfo() As String
            Get
                Return m_MoreInfo
            End Get
            Set(value As String)
                m_MoreInfo = value
            End Set
        End Property
        Private m_MoreInfo As String
        Public Property OrderBookRealtime() As Object
            Get
                Return m_OrderBookRealtime
            End Get
            Set(value As Object)
                m_OrderBookRealtime = value
            End Set
        End Property
        Private m_OrderBookRealtime As Object
        Public Property MarketCapitalization() As Object
            Get
                Return m_MarketCapitalization
            End Get
            Set(value As Object)
                m_MarketCapitalization = value
            End Set
        End Property
        Private m_MarketCapitalization As Object
        Public Property MarketCapRealtime() As Object
            Get
                Return m_MarketCapRealtime
            End Get
            Set(value As Object)
                m_MarketCapRealtime = value
            End Set
        End Property
        Private m_MarketCapRealtime As Object
        Public Property EBITDA() As String
            Get
                Return m_EBITDA
            End Get
            Set(value As String)
                m_EBITDA = value
            End Set
        End Property
        Private m_EBITDA As String
        Public Property ChangeFromYearLow() As Object
            Get
                Return m_ChangeFromYearLow
            End Get
            Set(value As Object)
                m_ChangeFromYearLow = value
            End Set
        End Property
        Private m_ChangeFromYearLow As Object
        Public Property PercentChangeFromYearLow() As Object
            Get
                Return m_PercentChangeFromYearLow
            End Get
            Set(value As Object)
                m_PercentChangeFromYearLow = value
            End Set
        End Property
        Private m_PercentChangeFromYearLow As Object
        Public Property LastTradeRealtimeWithTime() As String
            Get
                Return m_LastTradeRealtimeWithTime
            End Get
            Set(value As String)
                m_LastTradeRealtimeWithTime = value
            End Set
        End Property
        Private m_LastTradeRealtimeWithTime As String
        Public Property ChangePercentRealtime() As String
            Get
                Return m_ChangePercentRealtime
            End Get
            Set(value As String)
                m_ChangePercentRealtime = value
            End Set
        End Property
        Private m_ChangePercentRealtime As String
        Public Property ChangeFromYearHigh() As Object
            Get
                Return m_ChangeFromYearHigh
            End Get
            Set(value As Object)
                m_ChangeFromYearHigh = value
            End Set
        End Property
        Private m_ChangeFromYearHigh As Object
        Public Property PercebtChangeFromYearHigh() As Object
            Get
                Return m_PercebtChangeFromYearHigh
            End Get
            Set(value As Object)
                m_PercebtChangeFromYearHigh = value
            End Set
        End Property
        Private m_PercebtChangeFromYearHigh As Object
        Public Property LastTradeWithTime() As String
            Get
                Return m_LastTradeWithTime
            End Get
            Set(value As String)
                m_LastTradeWithTime = value
            End Set
        End Property
        Private m_LastTradeWithTime As String
        Public Property LastTradePriceOnly() As String
            Get
                Return m_LastTradePriceOnly
            End Get
            Set(value As String)
                m_LastTradePriceOnly = value
            End Set
        End Property
        Private m_LastTradePriceOnly As String
        Public Property HighLimit() As Object
            Get
                Return m_HighLimit
            End Get
            Set(value As Object)
                m_HighLimit = value
            End Set
        End Property
        Private m_HighLimit As Object
        Public Property LowLimit() As Object
            Get
                Return m_LowLimit
            End Get
            Set(value As Object)
                m_LowLimit = value
            End Set
        End Property
        Private m_LowLimit As Object
        Public Property DaysRange() As String
            Get
                Return m_DaysRange
            End Get
            Set(value As String)
                m_DaysRange = value
            End Set
        End Property
        Private m_DaysRange As String
        Public Property DaysRangeRealtime() As String
            Get
                Return m_DaysRangeRealtime
            End Get
            Set(value As String)
                m_DaysRangeRealtime = value
            End Set
        End Property
        Private m_DaysRangeRealtime As String
        Public Property FiftydayMovingAverage() As String
            Get
                Return m_FiftydayMovingAverage
            End Get
            Set(value As String)
                m_FiftydayMovingAverage = value
            End Set
        End Property
        Private m_FiftydayMovingAverage As String
        Public Property TwoHundreddayMovingAverage() As String
            Get
                Return m_TwoHundreddayMovingAverage
            End Get
            Set(value As String)
                m_TwoHundreddayMovingAverage = value
            End Set
        End Property
        Private m_TwoHundreddayMovingAverage As String
        Public Property ChangeFromTwoHundreddayMovingAverage() As Object
            Get
                Return m_ChangeFromTwoHundreddayMovingAverage
            End Get
            Set(value As Object)
                m_ChangeFromTwoHundreddayMovingAverage = value
            End Set
        End Property
        Private m_ChangeFromTwoHundreddayMovingAverage As Object
        Public Property PercentChangeFromTwoHundreddayMovingAverage() As Object
            Get
                Return m_PercentChangeFromTwoHundreddayMovingAverage
            End Get
            Set(value As Object)
                m_PercentChangeFromTwoHundreddayMovingAverage = value
            End Set
        End Property
        Private m_PercentChangeFromTwoHundreddayMovingAverage As Object
        Public Property ChangeFromFiftydayMovingAverage() As Object
            Get
                Return m_ChangeFromFiftydayMovingAverage
            End Get
            Set(value As Object)
                m_ChangeFromFiftydayMovingAverage = value
            End Set
        End Property
        Private m_ChangeFromFiftydayMovingAverage As Object
        Public Property PercentChangeFromFiftydayMovingAverage() As Object
            Get
                Return m_PercentChangeFromFiftydayMovingAverage
            End Get
            Set(value As Object)
                m_PercentChangeFromFiftydayMovingAverage = value
            End Set
        End Property
        Private m_PercentChangeFromFiftydayMovingAverage As Object
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(value As String)
                m_Name = value
            End Set
        End Property
        Private m_Name As String
        Public Property Notes() As Object
            Get
                Return m_Notes
            End Get
            Set(value As Object)
                m_Notes = value
            End Set
        End Property
        Private m_Notes As Object
        Public Property Open() As String
            Get
                Return m_Open
            End Get
            Set(value As String)
                m_Open = value
            End Set
        End Property
        Private m_Open As String
        Public Property PreviousClose() As String
            Get
                Return m_PreviousClose
            End Get
            Set(value As String)
                m_PreviousClose = value
            End Set
        End Property
        Private m_PreviousClose As String
        Public Property PricePaid() As Object
            Get
                Return m_PricePaid
            End Get
            Set(value As Object)
                m_PricePaid = value
            End Set
        End Property
        Private m_PricePaid As Object
        Public Property ChangeinPercent() As String
            Get
                Return m_ChangeinPercent
            End Get
            Set(value As String)
                m_ChangeinPercent = value
            End Set
        End Property
        Private m_ChangeinPercent As String
        Public Property PriceSales() As Object
            Get
                Return m_PriceSales
            End Get
            Set(value As Object)
                m_PriceSales = value
            End Set
        End Property
        Private m_PriceSales As Object
        Public Property PriceBook() As Object
            Get
                Return m_PriceBook
            End Get
            Set(value As Object)
                m_PriceBook = value
            End Set
        End Property
        Private m_PriceBook As Object
        Public Property ExDividendDate() As Object
            Get
                Return m_ExDividendDate
            End Get
            Set(value As Object)
                m_ExDividendDate = value
            End Set
        End Property
        Private m_ExDividendDate As Object
        Public Property PERatio() As Object
            Get
                Return m_PERatio
            End Get
            Set(value As Object)
                m_PERatio = value
            End Set
        End Property
        Private m_PERatio As Object
        Public Property DividendPayDate() As Object
            Get
                Return m_DividendPayDate
            End Get
            Set(value As Object)
                m_DividendPayDate = value
            End Set
        End Property
        Private m_DividendPayDate As Object
        Public Property PERatioRealtime() As Object
            Get
                Return m_PERatioRealtime
            End Get
            Set(value As Object)
                m_PERatioRealtime = value
            End Set
        End Property
        Private m_PERatioRealtime As Object
        Public Property PEGRatio() As Object
            Get
                Return m_PEGRatio
            End Get
            Set(value As Object)
                m_PEGRatio = value
            End Set
        End Property
        Private m_PEGRatio As Object
        Public Property PriceEPSEstimateCurrentYear() As Object
            Get
                Return m_PriceEPSEstimateCurrentYear
            End Get
            Set(value As Object)
                m_PriceEPSEstimateCurrentYear = value
            End Set
        End Property
        Private m_PriceEPSEstimateCurrentYear As Object
        Public Property PriceEPSEstimateNextYear() As Object
            Get
                Return m_PriceEPSEstimateNextYear
            End Get
            Set(value As Object)
                m_PriceEPSEstimateNextYear = value
            End Set
        End Property
        Private m_PriceEPSEstimateNextYear As Object
        Public Property Symbol_() As String
            Get
                Return m_Symbol_
            End Get
            Set(value As String)
                m_Symbol_ = value
            End Set
        End Property
        Private m_Symbol_ As String
        Public Property SharesOwned() As Object
            Get
                Return m_SharesOwned
            End Get
            Set(value As Object)
                m_SharesOwned = value
            End Set
        End Property
        Private m_SharesOwned As Object
        Public Property ShortRatio() As Object
            Get
                Return m_ShortRatio
            End Get
            Set(value As Object)
                m_ShortRatio = value
            End Set
        End Property
        Private m_ShortRatio As Object
        Public Property LastTradeTime() As String
            Get
                Return m_LastTradeTime
            End Get
            Set(value As String)
                m_LastTradeTime = value
            End Set
        End Property
        Private m_LastTradeTime As String
        Public Property TickerTrend() As String
            Get
                Return m_TickerTrend
            End Get
            Set(value As String)
                m_TickerTrend = value
            End Set
        End Property
        Private m_TickerTrend As String
        Public Property OneyrTargetPrice() As Object
            Get
                Return m_OneyrTargetPrice
            End Get
            Set(value As Object)
                m_OneyrTargetPrice = value
            End Set
        End Property
        Private m_OneyrTargetPrice As Object
        Public Property Volume() As String
            Get
                Return m_Volume
            End Get
            Set(value As String)
                m_Volume = value
            End Set
        End Property
        Private m_Volume As String
        Public Property HoldingsValue() As Object
            Get
                Return m_HoldingsValue
            End Get
            Set(value As Object)
                m_HoldingsValue = value
            End Set
        End Property
        Private m_HoldingsValue As Object
        Public Property HoldingsValueRealtime() As Object
            Get
                Return m_HoldingsValueRealtime
            End Get
            Set(value As Object)
                m_HoldingsValueRealtime = value
            End Set
        End Property
        Private m_HoldingsValueRealtime As Object
        Public Property YearRange() As String
            Get
                Return m_YearRange
            End Get
            Set(value As String)
                m_YearRange = value
            End Set
        End Property
        Private m_YearRange As String
        Public Property DaysValueChange() As String
            Get
                Return m_DaysValueChange
            End Get
            Set(value As String)
                m_DaysValueChange = value
            End Set
        End Property
        Private m_DaysValueChange As String
        Public Property DaysValueChangeRealtime() As String
            Get
                Return m_DaysValueChangeRealtime
            End Get
            Set(value As String)
                m_DaysValueChangeRealtime = value
            End Set
        End Property
        Private m_DaysValueChangeRealtime As String
        Public Property StockExchange() As String
            Get
                Return m_StockExchange
            End Get
            Set(value As String)
                m_StockExchange = value
            End Set
        End Property
        Private m_StockExchange As String
        Public Property DividendYield() As Object
            Get
                Return m_DividendYield
            End Get
            Set(value As Object)
                m_DividendYield = value
            End Set
        End Property
        Private m_DividendYield As Object
        Public Property PercentChange() As String
            Get
                Return m_PercentChange
            End Get
            Set(value As String)
                m_PercentChange = value
            End Set
        End Property
        Private m_PercentChange As String
    End Class

    Public Class Results
        Public Property quote() As Quote
            Get
                Return m_quote
            End Get
            Set(value As Quote)
                m_quote = value
            End Set
        End Property
        Private m_quote As Quote
    End Class

    Public Class Query
        Public Property count() As Integer
            Get
                Return m_count
            End Get
            Set(value As Integer)
                m_count = value
            End Set
        End Property
        Private m_count As Integer
        Public Property created() As String
            Get
                Return m_created
            End Get
            Set(value As String)
                m_created = value
            End Set
        End Property
        Private m_created As String
        Public Property lang() As String
            Get
                Return m_lang
            End Get
            Set(value As String)
                m_lang = value
            End Set
        End Property
        Private m_lang As String
        Public Property results() As Results
            Get
                Return m_results
            End Get
            Set(value As Results)
                m_results = value
            End Set
        End Property
        Private m_results As Results
    End Class

    Public Class RootObject
        Public Property query() As Query
            Get
                Return m_query
            End Get
            Set(value As Query)
                m_query = value
            End Set
        End Property
        Private m_query As Query
    End Class


End Class