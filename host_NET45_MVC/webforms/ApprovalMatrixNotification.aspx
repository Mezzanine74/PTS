<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" EnableEventValidation="false"
    AutoEventWireup="false" CodeFile="ApprovalMatrixNotification.aspx.vb" Inherits="ApprovalMatrixNotification" MaintainScrollPositionOnPostback="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div>
        <asp:GridView ID="GridViewNotApprovedPerson" runat="server"
            AutoGenerateColumns="False" DataSourceID="SqlDataSourceNotApprovedPersons"
            EnableModelValidation="True">
            <Columns>
                <asp:TemplateField HeaderText="UserName" SortExpression="UserName">
                    <ItemTemplate>
                        <asp:Label ID="LabelNotApprovedPerson" runat="server"
                            Text='<%# Bind("UserName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceNotApprovedPersons" runat="server"
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
            SelectCommand="SELECT [UserName]  FROM [ApprMx].[View_Contract_Addendum_NotApprovedPersonsForEmailNotification] WHERE UserName <> N'ilya' "></asp:SqlDataSource>

        <asp:GridView ID="GridViewApprovedButNotExecutedContracts" runat="server"
            AutoGenerateColumns="False" DataSourceID="SqlDataSourceApprovedButNotExecutedContracts"
            EnableModelValidation="True">
            <Columns>
                <asp:TemplateField HeaderText="ContractID" SortExpression="ContractID">
                    <ItemTemplate>
                        <asp:Label ID="LabelApprovedButNotExecutedContract" runat="server"
                            Text='<%# Bind("ContractID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceApprovedButNotExecutedContracts" runat="server"
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
            SelectCommand=" SELECT [ContractID] FROM [ApprMx].[ContractsAllApproved_NotExecutedYet] "></asp:SqlDataSource>

        <asp:GridView ID="GridViewApprovedButNotExecutedAddendums" runat="server"
            AutoGenerateColumns="False" DataSourceID="SqlDataSourceApprovedButNotExecutedAddendums"
            EnableModelValidation="True">
            <Columns>
                <asp:TemplateField HeaderText="ContractID" SortExpression="ContractID">
                    <ItemTemplate>
                        <asp:Label ID="LabelApprovedButNotExecutedContract" runat="server"
                            Text='<%# Bind("ContractID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AddendumID" SortExpression="AddendumID">
                    <ItemTemplate>
                        <asp:Label ID="LabelApprovedButNotExecutedAddendum" runat="server"
                            Text='<%# Bind("AddendumID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceApprovedButNotExecutedAddendums" runat="server"
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
            SelectCommand=" SELECT [ContractID], [AddendumID] FROM [ApprMx].[AddendumsAllApproved_NotExecutedYet] "></asp:SqlDataSource>

        <asp:GridView ID="GridViewInsuranceDetails" runat="server" GridLines="None" AutoGenerateColumns="false">
            <Columns>

                <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" HeaderStyle-Width="150px" />

                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" HeaderStyle-Width="150px" />

                <asp:BoundField DataField="InsuranceFinish" HeaderText="InsuranceFinish" ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="150px" />

            </Columns>
        </asp:GridView>

    </div>

</asp:Content>

