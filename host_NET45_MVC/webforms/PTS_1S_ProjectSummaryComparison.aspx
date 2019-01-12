<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="PTS_1S_ProjectSummaryComparison.aspx.vb" Inherits="PTS_1S_ProjectSummaryComparison"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        #MainContent_GridViewProjectSummaryComparison td {
            padding:5px;
        }

        #MainContent_GridViewProjectSummaryComparison th {
            height:50px;
            background-color:lightyellow;
            padding:5px;
        }


    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:DropDownList ID="ddlProject" runat="server" DataSourceID="SqlDataSourcePrj"  AutoPostBack="true"
        DataTextField="ProjectName" DataValueField="ProjectID">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     TOP (100) PERCENT ProjectID, ProjectName
    FROM         (SELECT     0 AS ProjectID, N'_Select Project' AS ProjectName
                           UNION ALL
                           SELECT     TOP (100) PERCENT ProjectID, RTRIM(ProjectName) + N' - ' + RTRIM(CONVERT(nvarChar(10), ProjectID)) AS Expr1
                           FROM         dbo.Table1_Project
                           WHERE     (CurrentStatus = 1)) AS Source
    ORDER BY ProjectName"></asp:SqlDataSource>

    <hr />

    <asp:GridView ID="GridViewProjectSummaryComparison" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceProjectComparison" ShowFooter="True" >
        <Columns>
            <asp:BoundField DataField="SupplierID" HeaderText="Supplier ID" ReadOnly="True" SortExpression="SupplierID" />
            <asp:BoundField DataField="SupplierNamePTS" HeaderText="Supplier Name PTS" SortExpression="SupplierNamePTS" ItemStyle-Width="300px" />
            <asp:BoundField DataField="SupplierName1S" HeaderText="Supplier Name 1S" SortExpression="SupplierName1S" ItemStyle-Width="300px" />
            <asp:BoundField DataField="Sum_DocValuePTS" HeaderText="Sum of Doc Value PTS" ReadOnly="True" SortExpression="Sum_DocValuePTS" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="Sum_DocValue1S" HeaderText="Sum of Doc Value 1S" ReadOnly="True" SortExpression="Sum_DocValue1S" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
            <asp:TemplateField HeaderText="Diff (PTS - 1S)" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Literal ID="LiteralDiff" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="%" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle Font-Size="Large" Font-Bold="true" BackColor="LightGray" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceProjectComparison" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SP_Delivery_ProjectSummaryComparison" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlProject" DefaultValue="0" Type="Int16" Name="ProjectID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

