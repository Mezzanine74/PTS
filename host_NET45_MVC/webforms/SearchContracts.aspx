<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="SearchContracts.aspx.vb" Inherits="SearchContracts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:Panel Id="pnlerch" runat="server" DefaultButton="ButtonSearch" style="margin: 0 auto; width:1048px; text-align:center; ">
            <asp:TextBox ID="TextBoxSearch" runat="server" BorderColor="#0099FF" BorderStyle="Solid" BorderWidth="2px" Font-Size="Large" ForeColor="#0066FF" Height="30px" Width="558px"></asp:TextBox>
        &nbsp;
            <asp:Button ID="ButtonSearch" runat="server" Text="Search" CssClass="btn btn-success"/>
        <br />
            <asp:RadioButtonList ID="RadioButtonListSearchMode" runat="server" RepeatDirection="Horizontal" style="margin: 0 auto;" AutoPostBack="true">
                <asp:ListItem Selected="True" Value="0">Similar Match</asp:ListItem>
                <asp:ListItem Value="1">Exact Match</asp:ListItem>
            </asp:RadioButtonList>
    </asp:Panel>

    <br />

    <div style="width:1048px; margin:0 auto;">
        <asp:GridView ID="GridViewSearch" runat="server" AutoGenerateColumns="False" DataKeyNames="ContractID" 
            DataSourceID="ObjectDataSourceSearch" CssClass="GridviewSearchContract">
            <Columns>
                <asp:BoundField DataField="ContractID" HeaderText="ID" ReadOnly="True" SortExpression="ContractID" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperlinkToHTMLSource" runat="server" Target="_blank" CssClass="LinkBrkDwn"
                            NavigateUrl='<%# "~/ContractBrkdwn.aspx?ContractID=" & Eval("ContractID")%>'>See Breakdown</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" ReadOnly="True" SortExpression="ProjectName" 
                    HeaderStyle-Width="150px" ItemStyle-Width="150px"/>
                <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ReadOnly="True" SortExpression="SupplierName" 
                    HeaderStyle-Width="200px" ItemStyle-Width="200px" />
                <asp:BoundField DataField="ContractDescription" HeaderText="ContractDescription" ReadOnly="True" SortExpression="ContractDescription" 
                    HeaderStyle-Width="300px" ItemStyle-Width="300px" />
                <asp:BoundField DataField="ContractDate" HeaderText="ContractDate" SortExpression="ContractDate" 
                    DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="ContractValue_withVAT" HeaderText="ContractValue withVAT" 
                    SortExpression="ContractValue_withVAT" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" 
                    HeaderStyle-Width="60px" ItemStyle-Width="60px" />
                <asp:BoundField DataField="ContractCurrency" HeaderText="Currency" ReadOnly="True" SortExpression="ContractCurrency" />
            </Columns>
        </asp:GridView>
    </div>
    
    <asp:ObjectDataSource ID="ObjectDataSourceSearch" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSearchResult" 
        TypeName="PTS_App_Code_VB_Project.MercuryTableAdapters.DataTableSerachContractTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxSearch" 
                DefaultValue="imposible_value" Name="parameter" 
                PropertyName="Text" Type="String" />
            <asp:Parameter Name="SearchMode" DefaultValue="0" Type="Int16" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />

</asp:Content>

