<%@ Page Title="" Language="VB" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Table_Addendum_UsersApprv_IgnoreUser.aspx.vb" Inherits="Pages_PTS_Table_Addendum_UsersApprv_IgnoreUser" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/assets/css/styles_PTS_MERCURY.css" />

</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:FormView runat="server" ID="FormviewTable_Addendum_UsersApprv_IgnoreUser" DataKeyNames="id" DefaultMode="Insert"
        ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table_Addendum_UsersApprv_IgnoreUser"
        OnCallingDataMethods="FormviewTable_Addendum_UsersApprv_IgnoreUser_CallingDataMethods"
        SelectMethod="FormviewTable_Addendum_UsersApprv_IgnoreUser_GetItem"
        InsertMethod="FormviewTable_Addendum_UsersApprv_IgnoreUser_InsertItem">
        <InsertItemTemplate>
            <fieldset>
                <legend>
                    <div style="display: inline-block">Table_Addendum_UsersApprv_IgnoreUser</div>
                    <div style="display: inline-block; float: right;"><a href="_ListOfTables.aspx">List Of Tables</a></div>
                </legend>
                <ol>
                    <div>


                        <label>Addendum I D</label>
                        <asp:DynamicControl runat="server" DataField="AddendumID" Mode="Insert" ValidationGroup="Table_Addendum_UsersApprv_IgnoreUserInsert" />

                        <label>User Name</label>
                        <asp:DropDownList ID="DropDownListUserName" runat="server" ValidationGroup="Table_Addendum_UsersApprv_IgnoreUserInsert"
                            SelectMethod="GetDropDownListUserName"
                            AppendDataBoundItems="true" SelectedValue="<%# BindItem.UserName%>" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.aspnet_Users"
                            DataTextField="UserName" DataValueField="UserName">
                            <asp:ListItem Value="">Select UserName:</asp:ListItem>
                        </asp:DropDownList>


                        <label></label>
                        <asp:Button ID="btnSave" runat="server" Text="Insert" CommandName="Insert" ValidationGroup="Table_Addendum_UsersApprv_IgnoreUserInsert" CssClass="btn btn-mini btn-info" />
                    </div>
                </ol>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>


    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table_Addendum_UsersApprv_IgnoreUserInsert" ForeColor="Red" />

    <asp:GridView runat="server" ID="GridviewTable_Addendum_UsersApprv_IgnoreUser" CssClass="table table-nonfluid table-hover" GridLines="None" DataKeyNames="id" AllowPaging="true"
        ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table_Addendum_UsersApprv_IgnoreUser"
        OnCallingDataMethods="GridviewTable_Addendum_UsersApprv_IgnoreUser_CallingDataMethods"
        SelectMethod="GridviewTable_Addendum_UsersApprv_IgnoreUser_GetData"
        UpdateMethod="GridviewTable_Addendum_UsersApprv_IgnoreUser_UpdateItem"
        DeleteMethod="GridviewTable_Addendum_UsersApprv_IgnoreUser_DeleteItem"
        AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                <EditItemTemplate>
                    <div class="btn-group-vertical" role="group">
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CssClass="btn btn-minier btn-primary"
                            CommandName="Update" Text="Update" ValidationGroup="Table_Addendum_UsersApprv_IgnoreUserUpdate"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </div>
                </EditItemTemplate>
                <ItemTemplate>
                    <div class="btn-group-vertical" role="group">
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
                            CommandName="Edit" Text="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonDelete" runat="server" CssClass="btn btn-minier btn-danger" CausesValidation="false"
                            OnClientClick="return confirm('Are you sure you want to delete this record?');"
                            CommandName="Delete" Text="Delete"></asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:DynamicField DataField="AddendumID" ValidationGroup="Table_Addendum_UsersApprv_IgnoreUserUpdate" HeaderText="Addendum I D" />

            <asp:TemplateField HeaderText="User Name" HeaderStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="labelUserName" runat="server" Text="<%# PTS_App_Code_VB_Project.PTS_MERCURY.helper.aspnet_Users.GetRowByPrimaryKey(Item.UserName).UserName%>"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListUserName" runat="server"
                        SelectMethod="GetDropDownListUserName"
                        AppendDataBoundItems="true" SelectedValue="<%# BindItem.UserName%>" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.aspnet_Users"
                        DataTextField="UserName" DataValueField="UserName">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>


        </Columns>
        <PagerSettings Position="TopAndBottom" />
        <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
        <HeaderStyle CssClass="headergridnew" />
    </asp:GridView>


    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table_Addendum_UsersApprv_IgnoreUserUpdate" ForeColor="Red" />

</asp:Content>

