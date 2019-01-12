<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="JTI.aspx.vb" Inherits="JTI" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


            <asp:Button ID="ButtonExcel" runat="server" Text="E X P O R T   TO   E X C E L" CssClass="btn btn-lg btn-default"
                OnClick="ButtonExcel_Click" />

            <asp:Button ID="ButtonExcelWeeklyPayment" runat="server" Text="E X P O R T Weekly Payment Breakdown" CssClass="btn btn-lg btn-default"
                OnClick="ButtonExcelWeeklyPayment_Click"/>


    <asp:GridView ID="GridViewJTI" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceJTI" Font-Size="12px">
        <Columns>
            <asp:BoundField DataField="TypeOf" HeaderText="TypeOf" ReadOnly="True" SortExpression="TypeOf" />
            <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" SortExpression="PO_No" />
            <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ReadOnly="True" SortExpression="SupplierName" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
            <asp:BoundField DataField="Currency" HeaderText="Currency" ReadOnly="True" SortExpression="Currency" />
            <asp:BoundField DataField="TotalWithVAT" HeaderText="TotalWithVAT" ReadOnly="True" SortExpression="TotalWithVAT" />
            <asp:BoundField DataField="CostToGo" HeaderText="CostToGo" ReadOnly="True" SortExpression="CostToGo" />
            <asp:BoundField DataField="TotalPlannedCost" HeaderText="TotalPlannedCost" ReadOnly="True" SortExpression="TotalPlannedCost" />
            <asp:BoundField DataField="PaidToDate" HeaderText="PaidToDate" ReadOnly="True" SortExpression="PaidToDate" />
            <asp:BoundField DataField="ToBePaid" HeaderText="ToBePaid" ReadOnly="True" SortExpression="ToBePaid" />
            <asp:BoundField DataField="Checking" HeaderText="Checking" ReadOnly="True" SortExpression="Checking" />
            <asp:BoundField DataField="CurrentPOValueWithVAT" HeaderText="CurrentPOValueWithVAT" ReadOnly="True" SortExpression="CurrentPOValueWithVAT" />
            <asp:BoundField DataField="Current_PO_Currency" HeaderText="Current_PO_Currency" ReadOnly="True" SortExpression="Current_PO_Currency" />
            <asp:BoundField DataField="Current_PaidToDate" HeaderText="Current_PaidToDate" ReadOnly="True" SortExpression="Current_PaidToDate" />
            <asp:BoundField DataField="Difference" HeaderText="Difference" ReadOnly="True" SortExpression="Difference" />
        </Columns>
        <HeaderStyle BackColor="LightBlue" Height="50px" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceJTI" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SP_JTI_ExcelOutput" 
        SelectCommandType="StoredProcedure"></asp:SqlDataSource>

<div style="text-align: center; width: 100%">
    <rsweb:ReportViewer ID="ReportViewer_" runat="server"  
    ProcessingMode="remote"     ShowCredentialPrompts="False" 
    ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="True" ShowParameterPrompts="False" 
    ShowPromptAreaButton="False"
    ShowToolBar="False" ShowZoomControl="False" Visible="false" 
    SizeToReportContent="True" ZoomMode="PageWidth"  AsyncRendering="False">
    </rsweb:ReportViewer>
 </div>

</asp:Content>

