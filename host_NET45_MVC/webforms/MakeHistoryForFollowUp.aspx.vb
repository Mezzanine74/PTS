Imports System.Data.SqlClient

Partial Class MakeHistoryForFollowUp
  Inherits System.Web.UI.Page

  Protected Sub ButtonRun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRun.Click

    'Dim StartDate As DateTime = Convert.ToDateTime(TextBoxStart.Text)
    'Dim StartDate As DateTime = Convert.ToDateTime(Mid(DropDown1.SelectedItem.Text.ToString, 7, 4).ToString + "-" + Mid(DropDown1.SelectedItem.Text.ToString, 4, 2).ToString + "-" + Mid(DropDown1.SelectedItem.Text.ToString, 1, 2).ToString + " " + "00" + ":" + "00" + ":" + "00")
    Dim StartDate As DateTime
    'If DropDown1 Is Nothing Then
    StartDate = Convert.ToDateTime(TextBoxStart.Text)
    'Else
    'StartDate = Convert.ToDateTime(Mid(DropDown1.SelectedItem.Text.ToString, 7, 4).ToString + "-" + Mid(DropDown1.SelectedItem.Text.ToString, 4, 2).ToString + "-" + Mid(DropDown1.SelectedItem.Text.ToString, 1, 2).ToString + " " + "00" + ":" + "00" + ":" + "00")
    'End If


    ' cancel this later...
    StartDate = StartDate.AddDays(1)
    ' .... cancel this later

    Dim FinishDate As DateTime = Convert.ToDateTime(TextBoxFinish.Text)

    Dim cn As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    Dim cmd As New System.Data.SqlClient.SqlCommand()
    cmd.Connection = cn
    cn.Open()
    cmd.CommandType = System.Data.CommandType.Text

    While StartDate <= FinishDate
      If StartDate.DayOfWeek.ToString = "Monday" OrElse StartDate.DayOfWeek.ToString = "Tuesday" OrElse StartDate.DayOfWeek.ToString = "Wednesday" OrElse StartDate.DayOfWeek.ToString = "Thursday" OrElse StartDate.DayOfWeek.ToString = "Friday" OrElse StartDate.DayOfWeek.ToString = "Saturday" OrElse StartDate.DayOfWeek.ToString = "Sunday" Then
        ' produce SQL
        Dim DayOfRun As String = "'" + Mid(StartDate.ToString, 7, 4).ToString + "-" + Mid(StartDate.ToString, 4, 2).ToString + "-" + Mid(StartDate.ToString, 1, 2).ToString + " " + "00" + ":" + "00" + ":" + "00" + "'"
        Dim ProjectID As String = TextBoxProjectID.Text
        Dim POnoHint As String = TextBoxPOKey.Text
        Dim SqlToGo As String = ""

        SqlToGo = " DELETE FROM [Table_View_QryW0_history] " + _
" INSERT INTO [Table_View_QryW0_history] " + _
"            ([ProjectID] " + _
"            ,[PO_No] " + _
"            ,[PoSumExcVAT] " + _
"            ,[PO_Currency] " + _
"            ,[PO_Date]) " + _
" ( " + _
" SELECT     dbo.Table1_Project.ProjectID, dbo.Table2_PONo.PO_No, dbo.View_PoSumExcVAT.PoSumExcVAT, dbo.View_PoSumExcVAT.PO_Currency,  " + _
"                       dbo.Table2_PONo.PO_Date " + _
" FROM         dbo.Table1_Project INNER JOIN " + _
"                       dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID LEFT OUTER JOIN " + _
"                       dbo.View_PoSumExcVAT ON dbo.Table2_PONo.PO_No = dbo.View_PoSumExcVAT.PO_No " + _
" WHERE     (dbo.Table2_PONo.PO_Date < CONVERT(DATETIME, " + DayOfRun + ", 102)) AND (dbo.Table1_Project.ProjectID = " + ProjectID + ") " + _
" ) " + _
"  " + _
" DELETE FROM [Table_View_QryW1_history] " + _
" INSERT INTO [Table_View_QryW1_history] " + _
"            ([PO_No] " + _
"            ,[DollarPendingExcVAT] " + _
"            ,[EuroPendingExcVAT] " + _
"            ,[RublePendingExcVAT]) " + _
" ( " + _
" SELECT PO_No, SUM(DollarPendingExcVAT) AS DollarPendingExcVAT, SUM(EuroPendingExcVAT) AS EuroPendingExcVAT, SUM(RublePendingExcVAT) AS RublePendingExcVAT FROM " + _
" (SELECT     dbo.Table2_PONo.PO_No, CONVERT(decimal(12, 2),  " + _
"                       CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN dbo.Table3_Invoice.InvoiceValue / dbo.Table8_ExchangeRates.RubbleDollar ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency " + _
"                        = N'Dollar' THEN dbo.Table3_Invoice.InvoiceValue ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN dbo.Table3_Invoice.InvoiceValue * dbo.Table8_ExchangeRates.RubbleEuro " + _
"                        / dbo.Table8_ExchangeRates.RubbleDollar END) END) END) AS DollarPendingExcVAT, CONVERT(decimal(12, 2),  " + _
"                       CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN dbo.Table3_Invoice.InvoiceValue / dbo.Table8_ExchangeRates.RubbleEuro ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency " + _
"                        = N'Dollar' THEN dbo.Table3_Invoice.InvoiceValue * dbo.Table8_ExchangeRates.RubbleDollar / dbo.Table8_ExchangeRates.RubbleEuro ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency " + _
"                        = N'Euro' THEN dbo.Table3_Invoice.InvoiceValue END) END) END) AS EuroPendingExcVAT, CONVERT(decimal(12, 2),  " + _
"                       CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN dbo.Table3_Invoice.InvoiceValue ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN dbo.Table3_Invoice.InvoiceValue " + _
"                        * dbo.Table8_ExchangeRates.RubbleDollar ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN dbo.Table3_Invoice.InvoiceValue * dbo.Table8_ExchangeRates.RubbleEuro " + _
"                        END) END) END) AS RublePendingExcVAT " + _
" FROM         dbo.Table2_PONo INNER JOIN " + _
"                       dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " + _
"                       dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID LEFT OUTER JOIN " + _
"                       dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo CROSS JOIN " + _
"                       dbo.Table8_ExchangeRates " + _
" WHERE     (dbo.Table2_PONo.PO_Date < CONVERT(DATETIME, " + DayOfRun + ", 102)) AND (dbo.Table5_PayLog.PaymentDate IS NULL) AND  " + _
"                       (dbo.Table8_ExchangeRates.Date = CONVERT(DATETIME, " + DayOfRun + ", 102)) AND (dbo.Table4_PaymentRequest.PayReqDate < CONVERT(DATETIME,  " + _
"                       " + DayOfRun + ", 102)) AND (dbo.Table2_PONo.PO_No LIKE N'%PO-" + POnoHint + "%') " + _
" UNION ALL                       " + _
" SELECT     dbo.Table2_PONo.PO_No, SUM(CONVERT(decimal(12, 2),  " + _
"                       CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN dbo.Table3_Invoice.InvoiceValue / dbo.Table8_ExchangeRates.RubbleDollar ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency " + _
"                        = N'Dollar' THEN dbo.Table3_Invoice.InvoiceValue ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN dbo.Table3_Invoice.InvoiceValue * dbo.Table8_ExchangeRates.RubbleEuro " + _
"                        / dbo.Table8_ExchangeRates.RubbleDollar END) END) END)) AS DollarPendingExcVAT, SUM(CONVERT(decimal(12, 2),  " + _
"                       CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN dbo.Table3_Invoice.InvoiceValue / dbo.Table8_ExchangeRates.RubbleEuro ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency " + _
"                        = N'Dollar' THEN dbo.Table3_Invoice.InvoiceValue * dbo.Table8_ExchangeRates.RubbleDollar / dbo.Table8_ExchangeRates.RubbleEuro ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency " + _
"                        = N'Euro' THEN dbo.Table3_Invoice.InvoiceValue END) END) END)) AS EuroPendingExcVAT, SUM(CONVERT(decimal(12, 2),  " + _
"                       CASE WHEN dbo.Table2_PONo.PO_Currency = N'Rub' THEN dbo.Table3_Invoice.InvoiceValue ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Dollar' THEN dbo.Table3_Invoice.InvoiceValue " + _
"                        * dbo.Table8_ExchangeRates.RubbleDollar ELSE (CASE WHEN dbo.Table2_PONo.PO_Currency = N'Euro' THEN dbo.Table3_Invoice.InvoiceValue * dbo.Table8_ExchangeRates.RubbleEuro " + _
"                        END) END) END)) AS RublePendingExcVAT " + _
" FROM         dbo.Table2_PONo INNER JOIN " + _
"                       dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " + _
"                       dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID LEFT OUTER JOIN " + _
"                       dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo CROSS JOIN " + _
"                       dbo.Table8_ExchangeRates " + _
" WHERE     (dbo.Table2_PONo.PO_Date < CONVERT(DATETIME, " + DayOfRun + ", 102)) AND (dbo.Table5_PayLog.PaymentDate > CONVERT(DATETIME,  " + _
"                       " + DayOfRun + ", 102)) AND (dbo.Table8_ExchangeRates.Date = CONVERT(DATETIME, " + DayOfRun + ", 102)) AND  " + _
"                       (dbo.Table4_PaymentRequest.PayReqDate < CONVERT(DATETIME, " + DayOfRun + ", 102)) " + _
" GROUP BY dbo.Table2_PONo.PO_No " + _
" HAVING      (dbo.Table2_PONo.PO_No LIKE N'%PO-" + POnoHint + "%') " + _
"                       ) AS DataSource1 " + _
" GROUP BY PO_No " + _
" ) " + _
"  " + _
"  " + _
" DELETE FROM [Table_View_QryW2_history] " + _
" INSERT INTO [Table_View_QryW2_history] " + _
"            ([PO_No] " + _
"            ,[DollarPaidExcVAT] " + _
"            ,[EuroPaidExcVAT] " + _
"            ,[RublePaidExcVAT] " + _
"            ,[DoneRublePO] " + _
"            ,[DoneEuroPO] " + _
"            ,[DoneDollarPO] " + _
"            ,[PartialRublePO] " + _
"            ,[PartialDollarPO] " + _
"            ,[PartialEuroPO]) " + _
" ( " + _
" SELECT     dbo.Table2_PONo.PO_No, SUM(CONVERT(decimal(12, 2),  " + _
"                       (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN (CASE WHEN dbo.Table5_PayLog.Currency = N'Rub' THEN dbo.Table5_PayLog.Amount / dbo.Table8_ExchangeRates.RubbleDollar " + _
"                        ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Dollar' THEN dbo.Table5_PayLog.Amount ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Euro' THEN dbo.Table5_PayLog.Amount " + _
"                        * dbo.Table8_ExchangeRates.RubbleEuro / dbo.Table8_ExchangeRates.RubbleDollar END) END) END)  " + _
"                       ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Rub' THEN dbo.Table5_PayLog.Amount / dbo.Table8_ExchangeRates.RubbleDollar / ((100 + dbo.Table2_POno.VATpercent) " + _
"                        / 100) ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Dollar' THEN dbo.Table5_PayLog.Amount / ((100 + dbo.Table2_POno.VATpercent) / 100)  " + _
"                       ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Euro' THEN dbo.Table5_PayLog.Amount * dbo.Table8_ExchangeRates.RubbleEuro / dbo.Table8_ExchangeRates.RubbleDollar " + _
"                        / ((100 + dbo.Table2_POno.VATpercent) / 100) END) END) END) END))) AS DollarPaidExcVAT, SUM(CONVERT(decimal(12, 2),  " + _
"                       (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN (CASE WHEN dbo.Table5_PayLog.Currency = N'Rub' THEN dbo.Table5_PayLog.Amount / dbo.Table8_ExchangeRates.RubbleEuro " + _
"                        ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Dollar' THEN dbo.Table5_PayLog.Amount * dbo.Table8_ExchangeRates.RubbleDollar / dbo.Table8_ExchangeRates.RubbleEuro " + _
"                        ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Euro' THEN dbo.Table5_PayLog.Amount END) END) END)  " + _
"                       ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Rub' THEN dbo.Table5_PayLog.Amount / dbo.Table8_ExchangeRates.RubbleEuro / ((100 + dbo.Table2_POno.VATpercent) " + _
"                        / 100)  " + _
"                       ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Dollar' THEN dbo.Table5_PayLog.Amount * dbo.Table8_ExchangeRates.RubbleDollar / dbo.Table8_ExchangeRates.RubbleEuro " + _
"                        / ((100 + dbo.Table2_POno.VATpercent) / 100)  " + _
"                       ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Euro' THEN dbo.Table5_PayLog.Amount / ((100 + dbo.Table2_POno.VATpercent) / 100) END) END) END) END)))  " + _
"                       AS EuroPaidExcVAT, SUM(CONVERT(decimal(12, 2),  " + _
"                       (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN (CASE WHEN dbo.Table5_PayLog.Currency = N'Rub' THEN dbo.Table5_PayLog.Amount ELSE (CASE WHEN dbo.Table5_PayLog.Currency " + _
"                        = N'Dollar' THEN dbo.Table5_PayLog.Amount * dbo.Table8_ExchangeRates.RubbleDollar ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Euro' THEN dbo.Table5_PayLog.Amount " + _
"                        * dbo.Table8_ExchangeRates.RubbleEuro END) END) END)  " + _
"                       ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Rub' THEN dbo.Table5_PayLog.Amount / ((100 + dbo.Table2_POno.VATpercent) / 100)  " + _
"                       ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Dollar' THEN dbo.Table5_PayLog.Amount * dbo.Table8_ExchangeRates.RubbleDollar / ((100 + dbo.Table2_POno.VATpercent) " + _
"                        / 100)  " + _
"                       ELSE (CASE WHEN dbo.Table5_PayLog.Currency = N'Euro' THEN dbo.Table5_PayLog.Amount * dbo.Table8_ExchangeRates.RubbleEuro / ((100 + dbo.Table2_POno.VATpercent) " + _
"                        / 100) END) END) END) END))) AS RublePaidExcVAT,Null,Null,Null,Null,Null,Null " + _
" FROM         dbo.Table2_PONo INNER JOIN " + _
"                       dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN " + _
"                       dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID INNER JOIN " + _
"                       dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo INNER JOIN " + _
"                       dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN " + _
"                       dbo.Table8_ExchangeRates ON dbo.Table5_PayLog.PaymentDate = dbo.Table8_ExchangeRates.Date " + _
" WHERE     (dbo.Table2_PONo.PO_Date < CONVERT(DATETIME, " + DayOfRun + ", 102)) AND (dbo.Table5_PayLog.PaymentDate < CONVERT(DATETIME,  " + _
"                       " + DayOfRun + ", 102)) AND (dbo.Table2_PONo.PO_No  LIKE '%PO-" + POnoHint + "-%') " + _
" GROUP BY dbo.Table2_PONo.PO_No " + _
" ) " + _
"  " + _
" DELETE FROM [Table_View_QryW3_history] " + _
" INSERT INTO [Table_View_QryW3_history] " + _
"            ([PO_No] " + _
"            ,[DollarPendingExcVAT] " + _
"            ,[EuroPendingExcVAT] " + _
"            ,[RublePendingExcVAT] " + _
"            ,[DollarPaidExcVAT] " + _
"            ,[EuroPaidExcVAT] " + _
"            ,[RublePaidExcVAT] " + _
"            ,[PoSumExcVAT] " + _
"            ,[PO_Currency] " + _
"            ,[PoTotalDollarExcVAT] " + _
"            ,[PoTotalEuroExcVAT] " + _
"            ,[PoTotalRubleExcVAT] " + _
"            ,[BalanceDollarExcVAT] " + _
"            ,[BalanceEuroExcVAT] " + _
"            ,[BalanceRubleExcVAT] " + _
"            ,[DoneRublePO] " + _
"            ,[DoneEuroPO] " + _
"            ,[DoneDollarPO] " + _
"            ,[PartialRublePO] " + _
"            ,[PartialDollarPO] " + _
"            ,[PartialEuroPO]) " + _
" ( " + _
" SELECT     Table_View_QryW0_history.PO_No, Table_View_QryW1_history.DollarPendingExcVAT, Table_View_QryW1_history.EuroPendingExcVAT,  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT, Table_View_QryW2_history.DollarPaidExcVAT, Table_View_QryW2_history.EuroPaidExcVAT,  " + _
"                       Table_View_QryW2_history.RublePaidExcVAT, null, null,  " + _
"                       CASE WHEN Table_View_QryW0_history.PO_Currency = N'Rub' AND Table_View_QryW1_history.RublePendingExcVAT IS NULL AND  " + _
"                       Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT / Table8_ExchangeRates.RubbleDollar WHEN Table_View_QryW0_history.PO_Currency = N'Rub' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT / Table8_ExchangeRates.RubbleDollar WHEN Table_View_QryW0_history.PO_Currency = N'Rub' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW2_history.DollarPaidExcVAT + (Table_View_QryW0_history.PoSumExcVAT - Table_View_QryW2_history.RublePaidExcVAT)  " + _
"                       / Table8_ExchangeRates.RubbleDollar WHEN Table_View_QryW0_history.PO_Currency = N'Rub' AND Table_View_QryW1_history.RublePendingExcVAT IS NULL  " + _
"                       AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW2_history.DollarPaidExcVAT + (Table_View_QryW0_history.PoSumExcVAT - Table_View_QryW2_history.RublePaidExcVAT)  " + _
"                       / Table8_ExchangeRates.RubbleDollar WHEN Table_View_QryW0_history.PO_Currency = N'Dollar' AND Table_View_QryW1_history.RublePendingExcVAT IS NULL " + _
"                        AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT WHEN Table_View_QryW0_history.PO_Currency = N'Dollar' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT WHEN Table_View_QryW0_history.PO_Currency = N'Dollar' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT WHEN Table_View_QryW0_history.PO_Currency = N'Dollar' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT WHEN Table_View_QryW0_history.PO_Currency = N'Euro' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT * Table8_ExchangeRates.RubbleEuro / Table8_ExchangeRates.RubbleDollar WHEN Table_View_QryW0_history.PO_Currency " + _
"                        = N'Euro' AND Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT * Table8_ExchangeRates.RubbleEuro / Table8_ExchangeRates.RubbleDollar WHEN Table_View_QryW0_history.PO_Currency " + _
"                        = N'Euro' AND Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW2_history.DollarPaidExcVAT + (Table_View_QryW0_history.PoSumExcVAT - Table_View_QryW2_history.EuroPaidExcVAT)  " + _
"                       * Table8_ExchangeRates.RubbleEuro / Table8_ExchangeRates.RubbleDollar WHEN Table_View_QryW0_history.PO_Currency = N'Euro' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW2_history.DollarPaidExcVAT + (Table_View_QryW0_history.PoSumExcVAT - Table_View_QryW2_history.EuroPaidExcVAT)  " + _
"                       * Table8_ExchangeRates.RubbleEuro / Table8_ExchangeRates.RubbleDollar END AS PoTotalDollarExcVAT,  " + _
"                       CASE WHEN Table_View_QryW0_history.PO_Currency = N'Rub' AND Table_View_QryW1_history.RublePendingExcVAT IS NULL AND  " + _
"                       Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT / Table8_ExchangeRates.RubbleEuro WHEN Table_View_QryW0_history.PO_Currency = N'Rub' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT / Table8_ExchangeRates.RubbleEuro WHEN Table_View_QryW0_history.PO_Currency = N'Rub' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW2_history.EuroPaidExcVAT + (Table_View_QryW0_history.PoSumExcVAT - Table_View_QryW2_history.RublePaidExcVAT)  " + _
"                       / Table8_ExchangeRates.RubbleEuro WHEN Table_View_QryW0_history.PO_Currency = N'Rub' AND Table_View_QryW1_history.RublePendingExcVAT IS NULL  " + _
"                       AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW2_history.EuroPaidExcVAT + (Table_View_QryW0_history.PoSumExcVAT - Table_View_QryW2_history.RublePaidExcVAT)  " + _
"                       / Table8_ExchangeRates.RubbleEuro WHEN Table_View_QryW0_history.PO_Currency = N'Dollar' AND Table_View_QryW1_history.RublePendingExcVAT IS NULL  " + _
"                       AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT * Table8_ExchangeRates.RubbleDollar / Table8_ExchangeRates.RubbleEuro WHEN Table_View_QryW0_history.PO_Currency " + _
"                        = N'Dollar' AND Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT * Table8_ExchangeRates.RubbleDollar / Table8_ExchangeRates.RubbleEuro WHEN Table_View_QryW0_history.PO_Currency " + _
"                        = N'Dollar' AND Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW2_history.EuroPaidExcVAT + (Table_View_QryW0_history.PoSumExcVAT - Table_View_QryW2_history.DollarPaidExcVAT)  " + _
"                       * Table8_ExchangeRates.RubbleDollar / Table8_ExchangeRates.RubbleEuro WHEN Table_View_QryW0_history.PO_Currency = N'Dollar' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW2_history.EuroPaidExcVAT + (Table_View_QryW0_history.PoSumExcVAT - Table_View_QryW2_history.DollarPaidExcVAT)  " + _
"                       * Table8_ExchangeRates.RubbleDollar / Table8_ExchangeRates.RubbleEuro WHEN Table_View_QryW0_history.PO_Currency = N'Euro' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT WHEN Table_View_QryW0_history.PO_Currency = N'Euro' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT WHEN Table_View_QryW0_history.PO_Currency = N'Euro' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT WHEN Table_View_QryW0_history.PO_Currency = N'Euro' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT END AS PoTotalEuroExcVAT, CASE WHEN Table_View_QryW0_history.PO_Currency = N'Rub' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT WHEN Table_View_QryW0_history.PO_Currency = N'Rub' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT WHEN Table_View_QryW0_history.PO_Currency = N'Rub' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT WHEN Table_View_QryW0_history.PO_Currency = N'Rub' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT WHEN Table_View_QryW0_history.PO_Currency = N'Dollar' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT * Table8_ExchangeRates.RubbleDollar WHEN Table_View_QryW0_history.PO_Currency = N'Dollar' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT * Table8_ExchangeRates.RubbleDollar WHEN Table_View_QryW0_history.PO_Currency = N'Dollar' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW2_history.RublePaidExcVAT + (Table_View_QryW0_history.PoSumExcVAT - Table_View_QryW2_history.DollarPaidExcVAT)  " + _
"                       * Table8_ExchangeRates.RubbleDollar WHEN Table_View_QryW0_history.PO_Currency = N'Dollar' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW2_history.RublePaidExcVAT + (Table_View_QryW0_history.PoSumExcVAT - Table_View_QryW2_history.DollarPaidExcVAT)  " + _
"                       * Table8_ExchangeRates.RubbleDollar WHEN Table_View_QryW0_history.PO_Currency = N'Euro' AND Table_View_QryW1_history.RublePendingExcVAT IS NULL  " + _
"                       AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT * Table8_ExchangeRates.RubbleEuro WHEN Table_View_QryW0_history.PO_Currency = N'Euro' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NULL  " + _
"                       THEN Table_View_QryW0_history.PoSumExcVAT * Table8_ExchangeRates.RubbleEuro WHEN Table_View_QryW0_history.PO_Currency = N'Euro' AND  " + _
"                       Table_View_QryW1_history.RublePendingExcVAT IS NOT NULL AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW2_history.RublePaidExcVAT + (Table_View_QryW0_history.PoSumExcVAT - Table_View_QryW2_history.EuroPaidExcVAT)  " + _
"                       * Table8_ExchangeRates.RubbleEuro WHEN Table_View_QryW0_history.PO_Currency = N'Euro' AND Table_View_QryW1_history.RublePendingExcVAT IS NULL  " + _
"                       AND Table_View_QryW2_history.RublePaidExcVAT IS NOT NULL  " + _
"                       THEN Table_View_QryW2_history.RublePaidExcVAT + (Table_View_QryW0_history.PoSumExcVAT - Table_View_QryW2_history.EuroPaidExcVAT)  " + _
"                       * Table8_ExchangeRates.RubbleEuro END AS PoTotalRubleExcVAT,null,null,null,null,null,null,null,null,null " + _
" FROM         Table_View_QryW0_history LEFT OUTER JOIN " + _
"                       Table_View_QryW1_history ON Table_View_QryW0_history.PO_No = Table_View_QryW1_history.PO_No LEFT OUTER JOIN " + _
"                       Table_View_QryW2_history ON Table_View_QryW0_history.PO_No = Table_View_QryW2_history.PO_No CROSS JOIN " + _
"                       dbo.Table8_ExchangeRates " + _
" WHERE     (dbo.Table8_ExchangeRates.Date = CONVERT(DATETIME, " + DayOfRun + ", 102)) AND (Table_View_QryW0_history.PO_No  LIKE '%PO-" + POnoHint + "-%') " + _
" ) " + _
"  " + _
" INSERT INTO [Table_HistoryFollowUpTotal] " + _
"            ([ProjectID] " + _
"            ,[DayOfRun] " + _
"            ,[PoTotalDollarExcVAT] " + _
"            ,[PoTotalEuroExcVAT] " + _
"            ,[PoTotalRubleExcVAT] " + _
"            ,[DollarPaidExcVAT] " + _
"            ,[EuroPaidExcVAT] " + _
"            ,[RublePaidExcVAT] " + _
"            ,[DollarPendingExcVAT] " + _
"            ,[EuroPendingExcVAT] " + _
"            ,[RublePendingExcVAT]) " + _
" ( " + _
" SELECT     dbo.Table1_Project.ProjectID, " + _
"            CONVERT(DATETIME, " + DayOfRun + ", 102) AS DateOfRun, " + _
"            SUM(Table_View_QryW3_history.PoTotalDollarExcVAT) AS PoTotalDollarExcVAT, " + _
"            SUM(Table_View_QryW3_history.PoTotalEuroExcVAT) AS PoTotalEuroExcVAT, " + _
"            SUM(Table_View_QryW3_history.PoTotalRubleExcVAT) AS PoTotalRubleExcVAT, " + _
"            SUM(Table_View_QryW3_history.DollarPaidExcVAT) AS DollarPaidExcVAT, " + _
"            SUM(Table_View_QryW3_history.EuroPaidExcVAT) AS EuroPaidExcVAT, " + _
"            SUM(Table_View_QryW3_history.RublePaidExcVAT) AS RublePaidExcVAT, " + _
"            SUM(Table_View_QryW3_history.DollarPendingExcVAT) AS DollarPendingExcVAT, " + _
"            SUM(Table_View_QryW3_history.EuroPendingExcVAT) AS EuroPendingExcVAT, " + _
"            SUM(Table_View_QryW3_history.RublePendingExcVAT) AS RublePendingExcVAT " + _
" FROM         dbo.Table2_PONo LEFT OUTER JOIN " + _
"                       Table_View_QryW3_history ON dbo.Table2_PONo.PO_No = Table_View_QryW3_history.PO_No RIGHT OUTER JOIN " + _
"                       dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID " + _
" WHERE dbo.Table1_Project.ProjectID IN (" + ProjectID + ") " + _
" GROUP BY dbo.Table1_Project.ProjectID " + _
" ) "

        cmd.CommandText = SqlToGo
        cmd.ExecuteNonQuery()

        StartDate = StartDate.AddDays(1)

      Else
        ' increase StartDate by one day
        StartDate = StartDate.AddDays(1)
      End If
    End While

    cn.Close()

  End Sub

  Protected Sub GridViewSendToAction_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewSendToAction.RowCommand
    If (e.CommandName = "SendToAction") Then
      Dim indexItem As Integer = Convert.ToInt32(e.CommandArgument)
      Dim rowItem As GridViewRow = GridViewSendToAction.Rows(indexItem)

      Dim LabelPrjID As Label = DirectCast(rowItem.FindControl("LabelPrjID"), Label)
      Dim LabelPrjName As Label = DirectCast(rowItem.FindControl("LabelPrjName"), Label)
      Dim LabelMinDate As Label = DirectCast(rowItem.FindControl("LabelMinDate"), Label)
      Dim LabelMaxDate As Label = DirectCast(rowItem.FindControl("LabelMaxDate"), Label)

      SqlDataSource1.SelectParameters("ProjectID").DefaultValue = Convert.ToInt32(LabelPrjID.Text)
      DropDown1.DataBind()

      TextBoxProjectID.Text = LabelPrjID.Text

      If Len(LabelPrjID.Text) = 2 Then
        TextBoxPOKey.Text = "0" + LabelPrjID.Text
      ElseIf Len(LabelPrjID.Text) = 1 Then
        TextBoxPOKey.Text = "00" + LabelPrjID.Text
      ElseIf Len(LabelPrjID.Text) = 3 Then
        TextBoxPOKey.Text = LabelPrjID.Text
      End If

      TextBoxStart.Text = Mid(LabelMinDate.Text.ToString, 7, 4).ToString + "-" + Mid(LabelMinDate.Text.ToString, 4, 2).ToString + "-" + Mid(LabelMinDate.Text.ToString, 1, 2).ToString + " " + "00" + ":" + "00" + ":" + "00"
      TextBoxFinish.Text = Mid(LabelMaxDate.Text.ToString, 7, 4).ToString + "-" + Mid(LabelMaxDate.Text.ToString, 4, 2).ToString + "-" + Mid(LabelMaxDate.Text.ToString, 1, 2).ToString + " " + "00" + ":" + "00" + ":" + "00"

    End If
  End Sub
End Class
