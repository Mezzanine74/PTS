
Partial Class POForeignCurrency
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load




    ' it will run for main Gridview itself
    If Not IsPostBack Then
      SqlDataSourcePOweek.SelectCommand = "  SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, RTRIM(dbo.Table2_PONo.PO_No) AS PO_No,   " + _
"                        dbo.Table2_PONo.PO_Date, dbo.Table2_PONo.TotalPrice AS TotalPriceWithVAT, dbo.View_QryW3.PoSumExcVAT AS TotalPriceExcVAT,   " + _
"                        RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency, View_PoInvoiceNull.IfNull, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName,   " + _
"                        CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) IS NULL   " + _
"                        THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) END)   " + _
"                        ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) IS NULL   " + _
"                        THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) END)   " + _
"                        END AS PaidWthVAT, CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) IS NULL   " + _
"                        THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) END)   " + _
"                        ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) IS NULL   " + _
"                        THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) END)   " + _
"                        END AS PendingWthVAT, CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) IS NULL   " + _
"                        THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) END)   " + _
"                        ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) IS NULL   " + _
"                        THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)   " + _
"                        = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) END)   " + _
"                        END AS BalanceWthVAT  " + _
"  FROM         dbo.Table1_Project INNER JOIN  " + _
"                        dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN  " + _
"                        View_PoInvoiceNull ON dbo.Table2_PONo.PO_No = View_PoInvoiceNull.PO_No INNER JOIN  " + _
"                        dbo.View_QryW3 ON dbo.Table2_PONo.PO_No = dbo.View_QryW3.PO_No INNER JOIN  " + _
"                        dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID  " + _
"  WHERE     (RTRIM(dbo.Table2_PONo.PO_Currency) <> 'Rub')  " + _
"                       AND (dbo.Table1_Project.ProjectID NOT IN (34, 37, 39, 42, 44, 45, 48, 49, 51, 52, 53, 54, 56, 57, 60, 68, 75, 77, 78, 80, 90)) " + _
"  ORDER BY PO_No "


    ElseIf DropDownListPrj.SelectedValue.ToString = "0" Then
      SqlDataSourcePOweek.SelectCommand = " SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, RTRIM(dbo.Table2_PONo.PO_No) AS PO_No,  " + _
"                       dbo.Table2_PONo.PO_Date, dbo.Table2_PONo.TotalPrice AS TotalPriceWithVAT, dbo.View_QryW3.PoSumExcVAT AS TotalPriceExcVAT,  " + _
"                       RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency, View_PoInvoiceNull.IfNull, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName,  " + _
"                       CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) END)  " + _
"                       END AS PaidWthVAT, CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) END)  " + _
"                       END AS PendingWthVAT, CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) END)  " + _
"                       END AS BalanceWthVAT " + _
" FROM         dbo.Table1_Project INNER JOIN " + _
"                       dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
"                       View_PoInvoiceNull ON dbo.Table2_PONo.PO_No = View_PoInvoiceNull.PO_No INNER JOIN " + _
"                       dbo.View_QryW3 ON dbo.Table2_PONo.PO_No = dbo.View_QryW3.PO_No INNER JOIN " + _
"                       dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID " + _
" WHERE       " + _
"                       (RTRIM(dbo.Table2_PONo.PO_Currency) <> 'Rub') " + _
"                       AND (dbo.Table1_Project.ProjectID NOT IN (34, 37, 39, 42, 44, 45, 48, 49, 51, 52, 53, 54, 56, 57, 60, 68, 75, 77, 78, 80, 90)) " + _
" ORDER BY PO_No "


    Else
      SqlDataSourcePOweek.SelectCommand = " SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, RTRIM(dbo.Table2_PONo.PO_No) AS PO_No,  " + _
"                       dbo.Table2_PONo.PO_Date, dbo.Table2_PONo.TotalPrice AS TotalPriceWithVAT, dbo.View_QryW3.PoSumExcVAT AS TotalPriceExcVAT,  " + _
"                       RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency, View_PoInvoiceNull.IfNull, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName,  " + _
"                       CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) END)  " + _
"                       END AS PaidWthVAT, CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) END)  " + _
"                       END AS PendingWthVAT, CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) END)  " + _
"                       END AS BalanceWthVAT " + _
" FROM         dbo.Table1_Project INNER JOIN " + _
"                       dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
"                       View_PoInvoiceNull ON dbo.Table2_PONo.PO_No = View_PoInvoiceNull.PO_No INNER JOIN " + _
"                       dbo.View_QryW3 ON dbo.Table2_PONo.PO_No = dbo.View_QryW3.PO_No INNER JOIN " + _
"                       dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID " + _
" WHERE     (dbo.Table1_Project.ProjectID = " + DropDownListPrj.SelectedValue.ToString + ")   " + _
"                       AND (RTRIM(dbo.Table2_PONo.PO_Currency) <> 'Rub') " + _
"                       AND (dbo.Table1_Project.ProjectID NOT IN (34, 37, 39, 42, 44, 45, 48, 49, 51, 52, 53, 54, 56, 57, 60, 68, 75, 77, 78, 80, 90)) " + _
" ORDER BY PO_No "


    End If

    SqlDataSourcePOweek.DataBind()
    GridViewPOweek.DataBind()

    ' it will run for Total part of Gridview

    If Not IsPostBack Then
      SqlDataSourceTotal.SelectCommand = " SELECT     TOP (100) PERCENT 0 AS projectID, SUM(dbo.Table2_PONo.TotalPrice) AS TotalPriceWithVAT, SUM(dbo.View_QryW3.PoSumExcVAT) AS TotalPriceExcVAT,  " + _
"                       RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency,  " + _
"                       SUM(CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) END) END)  " + _
"                       AS PaidWthVAT, SUM(CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) END) END)  " + _
"                       AS PendingWthVAT, SUM(CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) END) END)  " + _
"                       AS BalanceWthVAT " + _
" FROM         dbo.Table1_Project INNER JOIN " + _
"                       dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
"                       dbo.View_QryW3 ON dbo.Table2_PONo.PO_No = dbo.View_QryW3.PO_No INNER JOIN " + _
"                       dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID " + _
" WHERE      " + _
"                       (dbo.Table1_Project.ProjectID NOT IN (34, 37, 39, 42, 44, 45, 48, 49, 51, 52, 53, 54, 56, 57, 60, 68, 75, 77, 78, 80, 90)) " + _
" GROUP BY RTRIM(dbo.Table2_PONo.PO_Currency) " + _
" HAVING      (RTRIM(dbo.Table2_PONo.PO_Currency) <> 'Rub') "



    ElseIf DropDownListPrj.SelectedValue.ToString = "0" Then
      SqlDataSourceTotal.SelectCommand = " SELECT     TOP (100) PERCENT 0 AS projectID, SUM(dbo.Table2_PONo.TotalPrice) AS TotalPriceWithVAT, SUM(dbo.View_QryW3.PoSumExcVAT) AS TotalPriceExcVAT,  " + _
"                       RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency,  " + _
"                       SUM(CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) END) END)  " + _
"                       AS PaidWthVAT, SUM(CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) END) END)  " + _
"                       AS PendingWthVAT, SUM(CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) END) END)  " + _
"                       AS BalanceWthVAT " + _
" FROM         dbo.Table1_Project INNER JOIN " + _
"                       dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
"                       dbo.View_QryW3 ON dbo.Table2_PONo.PO_No = dbo.View_QryW3.PO_No INNER JOIN " + _
"                       dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID " + _
" WHERE      " + _
"                       (dbo.Table1_Project.ProjectID NOT IN (34, 37, 39, 42, 44, 45, 48, 49, 51, 52, 53, 54, 56, 57, 60, 68, 75, 77, 78, 80, 90)) " + _
" GROUP BY RTRIM(dbo.Table2_PONo.PO_Currency) " + _
" HAVING      (RTRIM(dbo.Table2_PONo.PO_Currency) <> 'Rub') "


    Else
      SqlDataSourceTotal.SelectCommand = " SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, SUM(dbo.Table2_PONo.TotalPrice) AS TotalPriceWithVAT, SUM(dbo.View_QryW3.PoSumExcVAT)  " + _
"                       AS TotalPriceExcVAT, RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency,  " + _
"                       SUM(CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPaidExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPaidExcVAT END) END) END)  " + _
"                       AS PaidWthVAT, SUM(CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.RublePendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.DollarPendingExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.EuroPendingExcVAT END) END) END)  " + _
"                       AS PendingWthVAT, SUM(CASE WHEN table6_supplier.vat_free = 1 THEN (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) END)  " + _
"                       ELSE (table2_pono.vatpercent + 100) / 100 * (CASE WHEN (CASE WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) IS NULL  " + _
"                       THEN 0 ELSE (CASE WHEN RTRIM(Table2_PONo.Po_Currency) = 'Rub' THEN View_QryW3.BalanceRubleExcVAT WHEN RTRIM(Table2_PONo.Po_Currency)  " + _
"                       = 'Dollar' THEN View_QryW3.BalanceDollarExcVAT WHEN RTRIM(Table2_PONo.Po_Currency) = 'Euro' THEN View_QryW3.BalanceEuroExcVAT END) END) END)  " + _
"                       AS BalanceWthVAT " + _
" FROM         dbo.Table1_Project INNER JOIN " + _
"                       dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN " + _
"                       dbo.View_QryW3 ON dbo.Table2_PONo.PO_No = dbo.View_QryW3.PO_No INNER JOIN " + _
"                       dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID " + _
" WHERE      " + _
"                       (dbo.Table1_Project.ProjectID NOT IN (34, 37, 39, 42, 44, 45, 48, 49, 51, 52, 53, 54, 56, 57, 60, 68, 75, 77, 78, 80, 90)) " + _
" GROUP BY dbo.Table1_Project.ProjectID, RTRIM(dbo.Table2_PONo.PO_Currency) " + _
" HAVING      (dbo.Table1_Project.ProjectID = " + DropDownListPrj.SelectedValue.ToString + ") AND (RTRIM(dbo.Table2_PONo.PO_Currency) <> 'Rub') "

    End If

    SqlDataSourceTotal.DataBind()
    GridViewTotalSum.DataBind()

  End Sub

  Protected Sub GridViewPOweek_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewPOweek.RowDataBound

    Dim MyTask As New MyCommonTasks
    MyTask.HoverEffectOnGridviewCells(sender, e.Row)

    'it defines CurrencyImage.
    If DirectCast(e.Row.FindControl("LabelCurrency"), Label) IsNot Nothing Then
      If DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text = "Rub" Then
        DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/ruble_icon_.bmp"
      ElseIf DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text = "Dollar" Then
        DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/dollar_icon_.bmp"
      ElseIf DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text = "Euro" Then
        DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/euro_icon_.bmp"
      End If
    End If

    ' it highlights row if PO has no invoice agains it
    If DirectCast(e.Row.FindControl("LabelIfNull"), Label) IsNot Nothing Then
      If DirectCast(e.Row.FindControl("LabelIfNull"), Label).Text = "null" Then
                e.Row.BackColor = System.Drawing.Color.OrangeRed
            End If
    End If

  End Sub

  Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.DataBound
    If Not IsPostBack Then
      Dim lst1 As New ListItem("ALL PROJECTS", "0")
      Me.DropDownListPrj.Items.Insert(0, lst1)
    End If
  End Sub

  Protected Sub DropDownListPrj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListPrj.SelectedIndexChanged
    SqlDataSourcePOweek.DataBind()
    GridViewPOweek.DataBind()
  End Sub

  Protected Sub GridViewTotalSum_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewTotalSum.RowDataBound
    'it defines CurrencyImage.
    If DirectCast(e.Row.FindControl("LabelCurrency"), Label) IsNot Nothing Then
      If DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text = "Rub" Then
        DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/ruble_icon_.bmp"
      ElseIf DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text = "Dollar" Then
        DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/dollar_icon_.bmp"
      ElseIf DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text = "Euro" Then
        DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/euro_icon_.bmp"
      End If
    End If
  End Sub

  Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
    GridViewToExcel.DataSource = SqlDataSourcePOweek
    GridViewToExcel.AllowPaging = False
    GridViewToExcel.DataBind()

    GridViewSumToExcel.DataSource = SqlDataSourceTotal
    GridViewSumToExcel.AllowPaging = False
    GridViewSumToExcel.DataBind()

    ' start to export Excel
    Response.Clear()
    Response.AddHeader("content-disposition", "attachment; filename=OpenPO_ForeignCurrency.xls")

    Response.Buffer = True
    Response.ContentType = "application/vnd.ms-excel"
    Response.Charset = ""
    Me.EnableViewState = False
    Dim oStringWriter As New System.IO.StringWriter
    Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)

    Dim oStringWriterSum As New System.IO.StringWriter
    Dim oHtmlTextWriterSum As New System.Web.UI.HtmlTextWriter(oStringWriterSum)

    GridViewToExcel.RenderControl(oHtmlTextWriter)
    GridViewSumToExcel.RenderControl(oHtmlTextWriterSum)

    Dim zoneId As String = "Russian Standard Time"
    Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
    Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

    Response.Write("<p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span lang=EN-US style='color:red;mso-ansi-language:EN-US'>Printed by " + String.Format("{0:dddd, MMMM d, yyyy, h:mm tt}", result) + " Moscow Russia Time Zone" + "<o:p></o:p></span></b></p>")

    Response.Write(oStringWriter.ToString())

    Response.Write(oStringWriterSum.ToString())

    Response.[End]()
  End Sub

  Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    Exit Sub
  End Sub

End Class
