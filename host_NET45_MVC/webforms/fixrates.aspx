<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="fixrates.aspx.vb" Inherits="fix_ratesOsman22" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<div style="text-align: center; width: 100%">
    <rsweb:ReportViewer ID="ReportViewerFxRate" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    <ServerReport
       DisplayName="CostReport" ReportPath="/"
          ReportServerUrl="http://localhost/ReportServer_SQLEXPRESS" />
    </rsweb:ReportViewer>
 </div>

  <table >
   <tr >
    <td style="vertical-align: top;">

                  <asp:ImageButton ID="ImageButton1" runat="server" 
                ImageUrl="~/Images/Excel.jpg"  OnClick="ImageButton1_Click"
                ToolTip="Export To Excel" Width="20px" BorderStyle="Solid" 
                BorderWidth="1px" />

      <br /><br />
      <asp:GridView ID="GridViewFixRate" runat="server" AutoGenerateColumns="False"  CssClass="Grid"
        DataSourceID="ObjectDataSourceFixRates" EnableModelValidation="True" 
        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px">
        <Columns>
          <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" />

          <asp:BoundField DataField="EurosPaidWithVAT" HeaderText="Euros Paid Wth VAT" 
            HeaderStyle-Width="100px" ItemStyle-Width="100px"
            DataFormatString="{0:N2}" 
            ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="#F0F8FF" >
<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" BackColor="AliceBlue" Width="100px"></ItemStyle>
          </asp:BoundField>
          <asp:BoundField DataField="FixRate_Euro" HeaderText="FX Rate" 
            HeaderStyle-Width="100px" ItemStyle-Width="100px"
            ReadOnly="True" 
            DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" 
            ItemStyle-BackColor="#F0F8FF" >
<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" BackColor="AliceBlue" Width="100px"></ItemStyle>
          </asp:BoundField>
          <asp:BoundField DataField="DollarPaidWithVAT"  HeaderStyle-Width="100px" ItemStyle-Width="100px"
            HeaderText="USD Paid Wth VAT" DataFormatString="{0:N2}" 
            ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="#D9FFEC" >
<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" BackColor="#D9FFEC" Width="100px"></ItemStyle>
          </asp:BoundField>
          <asp:BoundField DataField="FixRate_Dollar"  HeaderStyle-Width="100px" ItemStyle-Width="100px"
            HeaderText="FX Rate"  ReadOnly="True" ItemStyle-BackColor="#D9FFEC"
            DataFormatString="{0:N4}" 
            ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" BackColor="#D9FFEC" Width="100px"></ItemStyle>
          </asp:BoundField>
          <asp:BoundField DataField="RublePaidWithVAT"  HeaderStyle-Width="100px" ItemStyle-Width="100px"
            HeaderText="Rub Paid Wth VAT" DataFormatString="{0:N2}" 
            ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="#E4AFAF" >

          </asp:BoundField>
          <asp:BoundField DataField="FixRate_Ruble"  HeaderStyle-Width="100px" ItemStyle-Width="100px"
            HeaderText="FX Rate"  ReadOnly="True" ItemStyle-BackColor="#E4AFAF"
            DataFormatString="{0:N4}" 
            ItemStyle-HorizontalAlign="Right">
          </asp:BoundField>

            <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px"
            ItemStyle-HorizontalAlign="Right"  ItemStyle-BackColor="#F0F8FF" HeaderText="Balance Euro Wth VAT"  >
              <ItemTemplate>
                <asp:HyperLink ID="HyperLinkBalanceEuro" runat="server"  
                NavigateUrl='<%# "~/BalanceForeignCurrency.aspx?ProjectId=" + Eval("ProjectID") + "&PO_Currency=Euro"%>'
                Text='<%# Bind("BalanceEuroWithVAT", "{0:###,###,###.00}")%>' Target="_blank" 
                  Font-Underline="True" ForeColor="Blue" onmouseover="this.style.cursor='hand'" >
                  </asp:HyperLink>
              </ItemTemplate>
            <HeaderStyle Width="100px"></HeaderStyle>


            </asp:TemplateField>

            <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px"
            ItemStyle-HorizontalAlign="Right"  ItemStyle-BackColor="#D9FFEC" HeaderText="Balance Dollar Wth VAT"  >
              <ItemTemplate>
                <asp:HyperLink ID="HyperLinkBalanceDollar" runat="server"  
                NavigateUrl='<%# "~/BalanceForeignCurrency.aspx?ProjectId=" + Eval("ProjectID") + "&PO_Currency=Dollar"%>'
                Text='<%# Bind("BalanceDollarWithVAT","{0:###,###,###.00}") %>' 
                  Target="_blank" Font-Underline="True" ForeColor="Blue" 
                  onmouseover="this.style.cursor='hand'" ></asp:HyperLink>
              </ItemTemplate>
            <HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" BackColor="#D9FFEC" Width="100px"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px"
            ItemStyle-HorizontalAlign="Right"  ItemStyle-BackColor="#E4AFAF" HeaderText="Balance Ruble Wth VAT"  >
              <ItemTemplate>
                <asp:HyperLink ID="HyperLinkBalanceRuble" runat="server"  
                NavigateUrl='<%# "~/BalanceForeignCurrency.aspx?ProjectId=" + Eval("ProjectID") + "&PO_Currency=Rub"%>'
                Text='<%# Bind("BalanceRubleWithVAT", "{0:###,###,###.00}")%>' 
                  Target="_blank" Font-Underline="True" ForeColor="Blue" 
                  onmouseover="this.style.cursor='hand'" ></asp:HyperLink>
              </ItemTemplate>
            <HeaderStyle Width="100px"></HeaderStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px">
              <ItemTemplate>
                <asp:ImageButton ID="ImageButtonExportToExcel" runat="server" CommandName="ExportToExcel" ImageUrl="~/images/Excel.jpg"
                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProjectID")%>' />
              </ItemTemplate>
            </asp:TemplateField>

        </Columns>
           <RowStyle  CssClass="GridItemNakladnaya" Height="30px"/>
           <HeaderStyle  CssClass="GridHeader"  Height="30px"/>
      </asp:GridView>
     
      <asp:ObjectDataSource ID="ObjectDataSourceFixRates" runat="server" 
          OldValuesParameterFormatString="original_{0}" 
          SelectMethod="GetData" 
          TypeName="PTS_App_Code_VB_Project.MercuryTableAdapters.DataTableFixRatesTableAdapter">
      </asp:ObjectDataSource>
     
    </td>
   </tr>
  </table>


</asp:Content>

