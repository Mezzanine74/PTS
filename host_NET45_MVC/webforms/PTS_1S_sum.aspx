<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="PTS_1S_sum.aspx.vb" Inherits="PTS_1S_sum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Date" DataSourceID="ObjectDataSourcePTS_1S_Sum"
        Font-Size="10px" HeaderStyle-BackColor="#99CCFF" ShowFooter="True" >
        <Columns>
            <asp:TemplateField HeaderText="Date">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Date", "{0:dd/MM/yyyy}")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Payment Date">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("PaymentDateIndex", "{0:dd/MM/yyyy}")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Payment Amount 1S" FooterStyle-HorizontalAlign="Right" FooterStyle-BackColor="#FFCC99" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("PaymentAmount_1S", "{0:N2}")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Payment Amount PTS" FooterStyle-HorizontalAlign="Right" FooterStyle-BackColor="#FFCC99" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("PaymentAmount_PTS", "{0:N2}")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Diff" FooterStyle-HorizontalAlign="Right" FooterStyle-BackColor="#FFCC99" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("DifferenceInRealPayments", "{0:N2}")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#99CCFF"></HeaderStyle>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSourcePTS_1S_Sum" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="PTS_App_Code_VB_Project.MercuryTableAdapters.View_Payments_PTS_1S_SumTableAdapter">
    </asp:ObjectDataSource>
</asp:Content>

