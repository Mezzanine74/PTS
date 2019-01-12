<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="CostReportBackups.aspx.vb" Inherits="CostReportBackups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

            <asp:GridView ID="GridViewCostReportBackups" runat="server" AutoGenerateColumns="false" EmptyDataText="No files uploaded" CssClass="table table-nonfluid table-hover" GridLines="None" >
                <Columns>
                    <asp:BoundField DataField="Text" HeaderText="File Name" ItemStyle-Width="200" />
                    <asp:TemplateField ItemStyle-Width="100" >
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />
            </asp:GridView>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" Runat="Server">
</asp:Content>

