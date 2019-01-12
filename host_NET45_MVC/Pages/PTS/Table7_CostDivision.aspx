<%@ Page Title="" Language="VB" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Table7_CostDivision.aspx.vb" Inherits="Pages_PTS_Table7_CostDivision" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/assets/css/styles_PTS_MERCURY.css" />

</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:FormView runat="server" ID="FormviewTable7_CostDivision" DataKeyNames="CostVidisionID" DefaultMode="Insert"
        ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table7_CostDivision"
        OnCallingDataMethods="FormviewTable7_CostDivision_CallingDataMethods"
        SelectMethod="FormviewTable7_CostDivision_GetItem"
        InsertMethod="FormviewTable7_CostDivision_InsertItem">
        <InsertItemTemplate>
            <fieldset>
                <legend>
                    <div style="display: inline-block">Table7_CostDivision</div>
                    <div style="display: inline-block; float: right;"><a href="_ListOfTables.aspx">List Of Tables</a></div>
                </legend>
                <ol>
                    <div>

                        <label>Cost Vidision I D</label>
                        <asp:DynamicControl runat="server" DataField="CostVidisionID" Mode="Insert" ValidationGroup="Table7_CostDivisionInsert" />

                        <label>Cost Division Description</label>
                        <asp:TextBox ID="TextBoxCostDivisionDescription" runat="server" Text='<%# Bind("CostDivisionDescription")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table7_CostDivisionInsert"></asp:TextBox>

                        <label>Cost Division2 Description</label>
                        <asp:DropDownList ID="DropDownListCostDivision2Description" runat="server" ValidationGroup="Table7_CostDivisionInsert"
                            SelectMethod="GetDropDownListCostDivision2Description"
                            AppendDataBoundItems="true" SelectedValue="<%# BindItem.CostDivision2ID%>" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table7_CostDivision2+Table7_CostDivision2_For_DDL"
                            DataTextField="CostDivision2Description" DataValueField="CostDivision2ID">
                            <asp:ListItem Value="">Select CostDivision2Description:</asp:ListItem>
                        </asp:DropDownList>


                        <label></label>
                        <asp:Button ID="btnSave" runat="server" Text="Insert" CommandName="Insert" ValidationGroup="Table7_CostDivisionInsert" CssClass="btn btn-mini btn-info" />
                    </div>
                </ol>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>


    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table7_CostDivisionInsert" ForeColor="Red" />

    <asp:GridView runat="server" ID="GridviewTable7_CostDivision" CssClass="table table-nonfluid table-hover" GridLines="None" DataKeyNames="CostVidisionID" AllowPaging="true"
        ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table7_CostDivision"
        OnCallingDataMethods="GridviewTable7_CostDivision_CallingDataMethods"
        SelectMethod="GridviewTable7_CostDivision_GetData"
        UpdateMethod="GridviewTable7_CostDivision_UpdateItem"
        DeleteMethod="GridviewTable7_CostDivision_DeleteItem"
        AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                <EditItemTemplate>
                    <div class="btn-group-vertical" role="group">
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CssClass="btn btn-minier btn-primary"
                            CommandName="Update" Text="Update" ValidationGroup="Table7_CostDivisionUpdate"></asp:LinkButton>
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

            <asp:DynamicField DataField="CostVidisionID" ValidationGroup="Table7_CostDivisionUpdate" HeaderText="Cost Vidision I D" />

            <asp:TemplateField>
                <ItemTemplate>
                    <span class="span_small"><%# Item.CostDivisionDescription%></span>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxCostDivisionDescription" runat="server" Text='<%# Bind("CostDivisionDescription")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table7_CostDivisionUpdate"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Cost Division2 Description" HeaderStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="labelCostDivision2Description" runat="server" Text="<%# PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table7_CostDivision2.GetRowByPrimaryKey(Item.CostDivision2ID).CostDivision2Description%>"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListCostDivision2Description" runat="server"
                        SelectMethod="GetDropDownListCostDivision2Description"
                        AppendDataBoundItems="true" SelectedValue="<%# BindItem.CostDivision2ID%>" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table7_CostDivision2+Table7_CostDivision2_For_DDL"
                        DataTextField="CostDivision2Description" DataValueField="CostDivision2ID">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>


        </Columns>
        <PagerSettings Position="TopAndBottom" />
        <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
        <HeaderStyle CssClass="headergridnew" />
    </asp:GridView>


    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table7_CostDivisionUpdate" ForeColor="Red" />

</asp:Content>

