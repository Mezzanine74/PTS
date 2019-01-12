<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="z.aspx.vb" Inherits="z" %>

<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="fb-like" data-href="http://mercuryeng.ru/staff/IT_Department/52" data-layout="standard" data-action="like" data-show-faces="true" data-share="false"></div>

    <asp:GridView ID="GridViewMaxRate" runat="server" AutoGenerateColumns="False" DataKeyNames="Date" DataSourceID="SqlDataSourceMaxExcRate" Font-Size="10px">
        <Columns>
            <asp:BoundField DataField="Date" HeaderText="Date" ReadOnly="True" SortExpression="Date" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="RubbleDollar" HeaderText="RubbleDollar" SortExpression="RubbleDollar" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="RubbleEuro" HeaderText="RubbleEuro" SortExpression="RubbleEuro" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
        </Columns>
        <HeaderStyle BackColor="#99CCFF" />
        <RowStyle Height="20px" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceMaxExcRate" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="SP_GetExcRateMax" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

    <asp:Label ID="LabelUserName" runat="server" CssClass="label label-default"></asp:Label>
    <asp:Label ID="LabelEmail" runat="server" CssClass="label label-default"></asp:Label>
    <asp:Label ID="LabelPass" runat="server" CssClass="label label-danger"></asp:Label>

    <table>
        <tr>
            <td style="border-color: yellowgreen; padding: 5px; border-style: solid;">

                <table>
                    <tr>
                        <td class="LabelGeneral">User Name</td>
                        <td>
                            <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="form-group input-sm"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelGeneral">Password</td>
                        <td>
                            <asp:TextBox ID="TextBoxPassword" runat="server" CssClass="form-group input-sm"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelGeneral">Email</td>
                        <td>
                            <asp:TextBox ID="TextBoxEmailAdress" runat="server" CssClass="form-group input-sm"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="ButtonEnterUser" runat="server" Text="Enter User" CssClass="btn btn-mini" />
                        </td>
                    </tr>
                </table>

            </td>
            <td style="border-color: yellowgreen; padding: 5px; border-style: solid;">

                <table>
                    <tr>
                        <td>User Name:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxUserNamePAsswordChange" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>New Password:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxNewPassword" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="ButtonChangePassword" runat="server" Text="Change Password" OnClick="ButtonChangePassword_Click"
                                CssClass="btn btn-mini" />
                        </td>
                        <td></td>
                    </tr>
                </table>

            </td>
            <td style="border-color: yellowgreen; padding: 5px; border-style: solid;">

                <asp:DropDownList ID="DropDownListUserToDelete" runat="server" OnSelectedIndexChanged="DropDownListUserToDelete_SelectedIndexChanged1"
                    DataSourceID="SqlDataSourceUserToDelete" DataTextField="UserName"
                    DataValueField="UserName" AutoPostBack="True">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceUserToDelete" runat="server"
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                    SelectCommand="SELECT [UserId], RTRIM([UserName]) AS [UserName] FROM [vw_aspnet_Users] ORDER BY [UserName]"></asp:SqlDataSource>

                <asp:Button ID="ButtonDeleteUser" runat="server" Text="Delete User" CssClass="btn btn-mini"
                    OnClientClick="return confirm('Are you sure you want to delete this User?');" />

                <asp:Button ID="ButtonGiveFullAccessForSpecificUser" runat="server" Text="Give Full Access" CssClass="btn btn-mini"
                    OnClientClick="return confirm('Are you sure you want to give full access to this user?');" />

                <asp:Button ID="ButtonDeleteProjectsFromUser" runat="server" Text="Remove All Projects" CssClass="btn btn-mini"
                    OnClientClick="return confirm('Are you sure to remove all projects from this user?');" />

                <asp:Button ID="ButtonGiveFullProjectForRequiredPersons" runat="server" Text="Give Full Access For Required People"
                    OnClientClick="return confirm('Are you sure?');" CssClass="btn btn-mini" />

                <asp:SqlDataSource ID="SqlDataSourceCommon" runat="server"
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"></asp:SqlDataSource>

                <asp:SqlDataSource ID="SqlDataSourcePrjCount" runat="server"
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                    SelectCommand="SELECT COUNT(ProjectID) AS CountOfPrj  FROM dbo.Table1_Project"></asp:SqlDataSource>

            </td>
        </tr>

    </table>

    <br />

    <table>
        <tr>
            <td>

                <asp:Button ID="ButtonProcessRolesProjectsForUser" runat="server" Text=" PROCESS DATA " CssClass="btn btn-mini" />

                <br />

                <h3>Create Role</h3>
                <asp:TextBox ID="TextBoxCreateRole" runat="server"></asp:TextBox>
                <asp:Button ID="ButtonCreateRole" runat="server" Text="Create Role" CssClass="btn btn-mini btn-info" OnClick="ButtonCreateRole_Click" />


                <br />

                <asp:DataList ID="DataListRoles" runat="server" Font-Size="Smaller"
                    DataKeyField="RoleName" DataSourceID="SqlDataSourceRole" 
                    RepeatColumns="3" RepeatDirection="Vertical" OnItemDataBound="DataListRoles_ItemDataBound">
                    <ItemTemplate>
                        <div>
                            <asp:CheckBox ID="CheckBoxUserRole" runat="server" />
                            <asp:Label ID="LabelRoleName" runat="server" Text='<%# Bind("RoleName")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                    <SeparatorStyle BorderColor="#003366" BorderStyle="Solid" BorderWidth="2px" />
                </asp:DataList>

                <asp:SqlDataSource ID="SqlDataSourceRole" runat="server"
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                    SelectCommand="SELECT RTRIM(RoleName) AS RoleName
                                    FROM dbo.aspnet_Roles ORDER BY RoleName"></asp:SqlDataSource>
            </td>
            <td>
                <asp:DataList ID="DataListProject" runat="server" Font-Size="Smaller"
                    DataKeyField="ProjectID" DataSourceID="SqlDataSourceProject" RepeatColumns="5"
                    RepeatDirection="Vertical" OnItemDataBound="DataListProject_ItemDataBound">
                    <ItemTemplate>
                        <div>
                            <asp:CheckBox ID="CheckBoxUserProject" runat="server" />
                            <asp:Label ID="LabelProjectID" runat="server" Text='<%# Bind("ProjectID")%>' CssClass="hidepanel"></asp:Label>
                            <asp:Label ID="LabelProject" runat="server" Text='<%# Bind("ProjectName")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                    <SeparatorStyle BorderColor="#003366" BorderStyle="Solid" BorderWidth="2px" />
                </asp:DataList>

                <asp:SqlDataSource ID="SqlDataSourceProject" runat="server"
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                    SelectCommand=" SELECT ProjectID, RTRIM(ProjectName) + N' - ' + CONVERT(nvarChar(5), ProjectID) AS ProjectName
                                    FROM dbo.Table1_Project
                                    ORDER BY ProjectID DESC "></asp:SqlDataSource>

            </td>
        </tr>
    </table>


    <br />
    <br />

    <asp:GridView ID="GridViewDBconnectionNumber" runat="server" AutoGenerateColumns="False"
        DataSourceID="SqlDataSourceDBconnectionnumber" EnableModelValidation="True"
        CssClass="Grid" HeaderStyle-Height="20px" HeaderStyle-BackColor="#99CCFF">
        <Columns>
            <asp:BoundField DataField="TotalConnection" HeaderText="TotalConnection"
                ReadOnly="True" SortExpression="TotalConnection" />
            <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceDBconnectionnumber" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="SELECT TOP (24) SUM(NumberOfConnection) AS TotalConnection, osman AS Time
FROM         dbo.Table_DB_NumberOfConnection
GROUP BY osman
ORDER BY Time DESC"></asp:SqlDataSource>



</asp:Content>

