<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="ProjectWeeklySummaryEmail.aspx.vb" Inherits="ProjectWeeklySummaryEmail" EnableEventValidation="false" %> 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


  <div style="text-align: center; width: 100%">
    <asp:Button ID="Button_Test" runat="server" Text="Test" />
    <rsweb:ReportViewer ID="ReportViewerDeliveryReport" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="true" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>
 </div>

  <div style="text-align: center; width: 100%">
    <rsweb:ReportViewer ID="ReportViewerSendEmailToManagement_PaymentsSinceJan" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="true" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>
 </div>

            <asp:GridView ID="GridViewContractNotification" runat="server" AutoGenerateColumns="False" EmptyDataText="There is no such contract" >
                <Columns>
                  <asp:BoundField DataField="ContractID" Visible="false" />
                  <asp:BoundField DataField="AddendumID" Visible="false" />
                  <asp:BoundField DataField="TypeOfDoc" HeaderText="Doc. Type" HeaderStyle-Width="100px" />
                  <asp:BoundField DataField="SupplierName" HeaderText="Client Name" HeaderStyle-Width="200px" />
                  <asp:BoundField DataField="ContractNo" HeaderText="Contract No" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                  <asp:BoundField DataField="ContractDate" HeaderText="Contract Date" HeaderStyle-Width="100px" DataFormatString="{0:dd/MM/yyyy}" />
                  <asp:BoundField DataField="ContractDescription" HeaderText="Contract Description" HeaderStyle-Width="250px" />
                  <asp:BoundField DataField="ContractValue_woVAT" HeaderText="Contract Value woVAT" HeaderStyle-Width="100px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                  <asp:BoundField DataField="ContractCurrency" HeaderText="Contract Currency" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                  <asp:BoundField DataField="ContractType" HeaderText="Contract Type" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                  <asp:BoundField DataField="SignBySupplier" HeaderText="Sign By Supplier" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="SignByMercury" HeaderText="Sign By Mercury" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <RowStyle Height="50px" />
                <HeaderStyle BackColor="#66ccff" Height="80px" />
        </asp:GridView>

</asp:Content>