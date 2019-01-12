<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Admin_Default" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="StyleAdmin.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

                                <asp:DropDownList ID="DropDownListPrj" runat="server" 
                                    DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                                    DataValueField="ProjectID" AutoPostBack="True" Font-Size="10px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                    SelectCommand="SELECT 0 AS ProjectID, N'_ALL PROJECTS' AS ProjectName UNION ALL SELECT [ProjectID], [ProjectName] FROM [Table1_Project] WHERE ([NewGeneration] = 1) ORDER BY [ProjectName]">
                                </asp:SqlDataSource>

    <br /><br />
    
    <asp:GridView ID="GridViewProjects" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID" DataSourceID="ObjectDataSourceProject">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="LabelProjectID" runat="server" Text='<%# Eval("ProjectID")%>' CssClass="Admin_project" ></asp:Label>

                    &nbsp;&nbsp;

                    <asp:Label ID="LabelProjectName" runat="server" Text='<%# Eval("ProjectName")%>' CssClass="Admin_project" ></asp:Label>
                    <br />

                    <table>
                        <tr>
                            <td>
                                <asp:FormView ID="FormViewTable_Approval_UserPositionPrjJunction" runat="server" DataKeyNames="id" DataSourceID="ObjectDataSourceTable_Approval_UserPositionPrjJunction"
                                     DefaultMode="Insert" OnDataBound="FormViewTable_Approval_UserPositionPrjJunction_DataBound" >
                                    <InsertItemTemplate>
                                        UserName:
                                        <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' />
                                        <br />
                                        PositionID:
                                        <asp:DropDownList ID="DropDownListPositionID" runat="server" DataSourceID="ObjectDataSourcePositionID" Enabled="true"
                                            DataTextField="PositionName" DataValueField="PositionID" selectedvalue='<%# Bind("PositionID")%>' >

                                        </asp:DropDownList> 
                                        <asp:ObjectDataSource ID="ObjectDataSourcePositionID" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.ApprovalMatrixManageUsersTableAdapters.Table_Approval_PositionEmployeeTableAdapter" >
                                        </asp:ObjectDataSource>

                                        <br />
                                        ProjectID:
                                        <asp:TextBox ID="ProjectIDTextBox" runat="server" Text='<%# Bind("ProjectID") %>' ReadOnly="true"  />
                                        <br />
                                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                                        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                    </InsertItemTemplate>
                                </asp:FormView>

                                <br />

                                <div style="font-size:large; font-weight:bold; text-align:center; color:red;">Table_Approval_UserPositionPrjJunction</div>
                                <asp:GridView ID="GridViewTable_Approval_UserPositionPrjJunction" runat="server" 
                                    AutoGenerateColumns="False" 
                                    DataKeyNames="id" 
                                    DataSourceID="ObjectDataSourceTable_Approval_UserPositionPrjJunction" >
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                CommandName="Delete" Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral"
                                                    CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                        <asp:BoundField DataField="PositionID" HeaderText="PositionID" SortExpression="PositionID" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownListPositionID" runat="server" DataSourceID="ObjectDataSourcePositionID" Enabled="false"
                                                    DataTextField="PositionName" DataValueField="PositionID" selectedvalue='<%# Bind("PositionID")%>' >

                                                </asp:DropDownList> 
                                                <asp:ObjectDataSource ID="ObjectDataSourcePositionID" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.ApprovalMatrixManageUsersTableAdapters.Table_Approval_PositionEmployeeTableAdapter" >
                                                </asp:ObjectDataSource>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownListPositionID" runat="server" DataSourceID="ObjectDataSourcePositionID" Enabled="true"
                                                    DataTextField="PositionName" DataValueField="PositionID" selectedvalue='<%# Bind("PositionID")%>' >

                                                </asp:DropDownList> 
                                                <asp:ObjectDataSource ID="ObjectDataSourcePositionID" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.ApprovalMatrixManageUsersTableAdapters.Table_Approval_PositionEmployeeTableAdapter" >
                                                </asp:ObjectDataSource>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                    </Columns>
                                </asp:GridView>

                                <asp:ObjectDataSource ID="ObjectDataSourceTable_Approval_UserPositionPrjJunction" runat="server" 
                                    DeleteMethod="Delete" InsertMethod="Insert" 
                                    OldValuesParameterFormatString="original_{0}" 
                                    SelectMethod="GetDataBy" TypeName="PTS_App_Code_VB_Project.ApprovalMatrixManageUsersTableAdapters.Table_Approval_UserPositionPrjJunctionTableAdapter" 
                                    UpdateMethod="Update">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Original_id" Type="Int32" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="UserName" Type="String" />
                                        <asp:Parameter Name="PositionID" Type="Int16" />
                                        <asp:Parameter Name="ProjectID" Type="Int16" />
                                    </InsertParameters>
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="ProjectID" Type="Int16" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="UserName" Type="String" />
                                        <asp:Parameter Name="PositionID" Type="Int16" />
                                        <asp:Parameter Name="ProjectID" Type="Int16" />
                                        <asp:Parameter Name="Original_id" Type="Int32" />
                                    </UpdateParameters>
                                </asp:ObjectDataSource>

                            </td>
                            <td>

                                <asp:GridView ID="GridViewTable_Approval_PositionEmployee" runat="server" AutoGenerateColumns="False" DataKeyNames="PositionID" DataSourceID="ObjectDataSourceTable_Approval_PositionEmployee">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                CommandName="Delete" Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral"
                                                    CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PositionID" HeaderText="PositionID" InsertVisible="False" ReadOnly="True" SortExpression="PositionID" />
                                        <asp:BoundField DataField="PositionName" HeaderText="PositionName" SortExpression="PositionName" />
                                        <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                                    </Columns>
                                </asp:GridView>

                                <asp:ObjectDataSource ID="ObjectDataSourceTable_Approval_PositionEmployee" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.ApprovalMatrixManageUsersTableAdapters.Table_Approval_PositionEmployeeTableAdapter" UpdateMethod="Update">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Original_PositionID" Type="Int16" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="PositionName" Type="String" />
                                        <asp:Parameter Name="Rank" Type="Int16" />
                                    </InsertParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="PositionName" Type="String" />
                                        <asp:Parameter Name="Rank" Type="Int16" />
                                        <asp:Parameter Name="Original_PositionID" Type="Int16" />
                                    </UpdateParameters>
                                </asp:ObjectDataSource>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:5px; background-color:gray; margin:5px;"></td>
                        </tr>
                        <tr>
                            <td>

                                <asp:FormView ID="FormViewTable_PersonRequestPo" runat="server" DataKeyNames="id" DataSourceID="ObjectDataSourceTable_PersonRequestPo" OnDataBound="FormViewTable_PersonRequestPo_DataBound"
                                     DefaultMode="Insert" >
                                    <InsertItemTemplate>
                                        ProjectID:
                                        <asp:TextBox ID="ProjectIDTextBox" runat="server" Text='<%# Bind("ProjectID") %>' ReadOnly="true" />
                                        <br />
                                        UserName:
                                        <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' />
                                        <br />
                                        Email:
                                        <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
                                        <br />
                                        NameSurname:
                                        <asp:TextBox ID="NameSurnameTextBox" runat="server" Text='<%# Bind("NameSurname") %>' />
                                        <br />
                                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                                        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                    </InsertItemTemplate>
                                </asp:FormView>

                                <div style="font-size:large; font-weight:bold; text-align:center; color:red;">Table_PersonRequestPo</div>
                                <asp:GridView ID="GridViewTable_PersonRequestPo" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ObjectDataSourceTable_PersonRequestPo">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                CommandName="Delete" Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral"
                                                    CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                        <asp:BoundField DataField="NameSurname" HeaderText="NameSurname" SortExpression="NameSurname" />
                                    </Columns>
                                </asp:GridView>

                                <asp:ObjectDataSource ID="ObjectDataSourceTable_PersonRequestPo" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataBy" TypeName="PTS_App_Code_VB_Project.ApprovalMatrixManageUsersTableAdapters.Table_PersonRequestPoTableAdapter" UpdateMethod="Update">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Original_id" Type="Int32" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="ProjectID" Type="Int16" />
                                        <asp:Parameter Name="UserName" Type="String" />
                                        <asp:Parameter Name="Email" Type="String" />
                                        <asp:Parameter Name="NameSurname" Type="String" />
                                    </InsertParameters>
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="ProjectID" Type="Int16" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="ProjectID" Type="Int16" />
                                        <asp:Parameter Name="UserName" Type="String" />
                                        <asp:Parameter Name="Email" Type="String" />
                                        <asp:Parameter Name="NameSurname" Type="String" />
                                        <asp:Parameter Name="Original_id" Type="Int32" />
                                    </UpdateParameters>
                                </asp:ObjectDataSource>

                            </td>
                            <td>
                                <asp:GridView ID="GridViewTable_ContractControlExceptional" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ObjectDataSourceTable_ContractControlExceptional" >
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                CommandName="Delete" Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral"
                                                    CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSourceTable_ContractControlExceptional" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByProjectID" TypeName="PTS_App_Code_VB_Project.ApprovalMatrixManageUsersTableAdapters.Table_ContractControlExceptionalTableAdapter" UpdateMethod="Update">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Original_id" Type="Int32" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="UserName" Type="String" />
                                        <asp:Parameter Name="ProjectID" Type="Int16" />
                                    </InsertParameters>
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="ProjectID" Type="Int16" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="UserName" Type="String" />
                                        <asp:Parameter Name="ProjectID" Type="Int16" />
                                        <asp:Parameter Name="Original_id" Type="Int32" />
                                    </UpdateParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:5px; background-color:gray; margin:5px;"></td>
                        </tr>
                        <tr>
                            <td>
                                
                                <div style="font-size:large; font-weight:bold; text-align:center; color:red;">Table_Approval_Scn_Prj_Users</div>
                                <asp:GridView ID="GridViewTable_Approval_Scn_Prj_Users" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ObjectDataSourceTable_Approval_Scn_Prj_Users" >
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                CommandName="Delete" Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral"
                                                    CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                        <asp:BoundField DataField="Scenario" HeaderText="Scenario" SortExpression="Scenario" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                    </Columns>
                                </asp:GridView>

                                <asp:ObjectDataSource ID="ObjectDataSourceTable_Approval_Scn_Prj_Users" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByProjectID" TypeName="PTS_App_Code_VB_Project.ApprovalMatrixManageUsersTableAdapters.Table_Approval_Scn_Prj_UsersTableAdapter" UpdateMethod="Update">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Original_id" Type="Int32" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="UserName" Type="String" />
                                        <asp:Parameter Name="Scenario" Type="Int16" />
                                        <asp:Parameter Name="ProjectID" Type="Int16" />
                                    </InsertParameters>
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="ProjectID" Type="Int16" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="UserName" Type="String" />
                                        <asp:Parameter Name="Scenario" Type="Int16" />
                                        <asp:Parameter Name="ProjectID" Type="Int16" />
                                        <asp:Parameter Name="Original_id" Type="Int32" />
                                    </UpdateParameters>
                                </asp:ObjectDataSource>

                            </td>
                            <td>
                                <asp:GridView ID="GridViewTable_Approval_Scenario" runat="server" AutoGenerateColumns="False" DataKeyNames="Scenario" DataSourceID="ObjectDataSourceTable_Approval_Scenario" >
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                CommandName="Delete" Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral"
                                                    CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Scenario" HeaderText="Scenario" ReadOnly="True" SortExpression="Scenario" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                    </Columns>
                                </asp:GridView>

                                <asp:ObjectDataSource ID="ObjectDataSourceTable_Approval_Scenario" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.ApprovalMatrixManageUsersTableAdapters.Table_Approval_ScenarioTableAdapter" UpdateMethod="Update">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Original_Scenario" Type="Int16" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="Scenario" Type="Int16" />
                                        <asp:Parameter Name="Description" Type="String" />
                                    </InsertParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="Description" Type="String" />
                                        <asp:Parameter Name="Original_Scenario" Type="Int16" />
                                    </UpdateParameters>
                                </asp:ObjectDataSource>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:5px; background-color:gray; margin:5px;"></td>
                        </tr>
                        <tr>
                            <td>

                                <asp:FormView ID="FormViewTable_Approval_UserRolePrjectJunction" runat="server" DataKeyNames="id" DataSourceID="ObjectDataSourceTable_Approval_UserRolePrjectJunction"
                                    OnDataBound="FormViewTable_Approval_UserRolePrjectJunction_DataBound" DefaultMode="Insert">
                                    <InsertItemTemplate>
                                        UserName:
                                        <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' />
                                        <br />
                                        RoleName:
                                        <asp:TextBox ID="RoleNameTextBox" runat="server" Text='<%# Bind("RoleName") %>' />
                                        <br />
                                        ProjectID:
                                        <asp:TextBox ID="ProjectIDTextBox" runat="server" Text='<%# Bind("ProjectID") %>' ReadOnly="true" />
                                        <br />
                                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                                        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                    </InsertItemTemplate>
                                </asp:FormView>

                                <div style="font-size:large; font-weight:bold; text-align:center; color:red;">Table_Approval_UserRolePrjectJunction</div>
                                <asp:GridView ID="GridViewTable_Approval_UserRolePrjectJunction" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ObjectDataSourceTable_Approval_UserRolePrjectJunction" >
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                CommandName="Delete" Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral"
                                                    CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                        <asp:BoundField DataField="RoleName" HeaderText="RoleName" SortExpression="RoleName" />
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                    </Columns>
                                </asp:GridView>

                                <asp:ObjectDataSource ID="ObjectDataSourceTable_Approval_UserRolePrjectJunction" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByProjectID" TypeName="PTS_App_Code_VB_Project.ApprovalMatrixManageUsersTableAdapters.Table_Approval_UserRolePrjectJunctionTableAdapter" UpdateMethod="Update">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Original_id" Type="Int32" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="UserName" Type="String" />
                                        <asp:Parameter Name="RoleName" Type="String" />
                                        <asp:Parameter Name="ProjectID" Type="Int16" />
                                    </InsertParameters>
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="ProjectID" Type="Int16" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="UserName" Type="String" />
                                        <asp:Parameter Name="RoleName" Type="String" />
                                        <asp:Parameter Name="ProjectID" Type="Int16" />
                                        <asp:Parameter Name="Original_id" Type="Int32" />
                                    </UpdateParameters>
                                </asp:ObjectDataSource>

                            </td>
                            <td>

                            </td>
                        </tr>

                    </table>

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSourceProject" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetDataByProjectID" 
        TypeName="PTS_App_Code_VB_Project.ApprovalMatrixManageUsersTableAdapters.Table_ProjectTableAdapter">
        <SelectParameters>
            <%-- Use this paremter after placing DDL for projects
            <asp:ControlParameter ControlID="GridView1" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
            --%>

            <asp:ControlParameter ControlID="DropDownListPrj" Type="Int16" Name="ProjectID" />
        </SelectParameters>
    </asp:ObjectDataSource>
       
</asp:Content>

