<%@ Page Title="" Language="VB" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Table7_CostCode.aspx.vb" Inherits="Pages_PTS_Table7_CostCode" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/assets/css/styles_PTS_MERCURY.css" />

</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:FormView runat="server" ID="FormviewTable7_CostCode" DataKeyNames="CostCode" DefaultMode="Insert"
        ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table7_CostCode"
        OnCallingDataMethods="FormviewTable7_CostCode_CallingDataMethods"
        SelectMethod="FormviewTable7_CostCode_GetItem"
        InsertMethod="FormviewTable7_CostCode_InsertItem">
        <InsertItemTemplate>
            <fieldset>
                <legend>
                    <div style="display: inline-block">Table7_CostCode</div>
                    <div style="display: inline-block; float: right;"><a href="_ListOfTables.aspx">List Of Tables</a></div>
                </legend>
                <ol>
                    <div>

                        <label>Cost Code</label>
                        <asp:DynamicControl runat="server" DataField="CostCode" Mode="Insert" ValidationGroup="Table7_CostCodeInsert" />

                        <label>Cost Division Description</label>
                        <asp:DropDownList ID="DropDownListCostDivisionDescription" runat="server" ValidationGroup="Table7_CostCodeInsert" Style="width: 200px;"
                            SelectMethod="GetDropDownListCostDivisionDescription"
                            AppendDataBoundItems="true" SelectedValue="<%# BindItem.CostVidisionID%>" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table7_CostDivision+Table7_CostDivision_For_DDL"
                            DataTextField="CostDivisionDescription" DataValueField="CostVidisionID">
                            <asp:ListItem Value="">Select CostDivisionDescription:</asp:ListItem>
                        </asp:DropDownList>

                        <label>Code Description</label>
                        <asp:TextBox ID="TextBoxCodeDescription" runat="server" Text='<%# Bind("CodeDescription")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table7_CostCodeInsert"></asp:TextBox>

                        <label>Updated By</label>
                        <asp:DynamicControl runat="server" DataField="UpdatedBy" Mode="Insert" ValidationGroup="Table7_CostCodeInsert" DataFormatString="{0:dd/MM/yyyy}" />

                        <label>Data Center</label>
                        <asp:DynamicControl runat="server" DataField="DataCenter" Mode="Insert" ValidationGroup="Table7_CostCodeInsert" />

                        <label>From Access</label>
                        <asp:DynamicControl runat="server" DataField="FromAccess" Mode="Insert" ValidationGroup="Table7_CostCodeInsert" />

                        <label>Type</label>
                        <asp:DynamicControl runat="server" DataField="Type" Mode="Insert" ValidationGroup="Table7_CostCodeInsert" />

                        <label>Note</label>
                        <asp:DynamicControl runat="server" DataField="Note" Mode="Insert" ValidationGroup="Table7_CostCodeInsert" />


                        <label></label>
                        <asp:Button ID="btnSave" runat="server" Text="Insert" CommandName="Insert" ValidationGroup="Table7_CostCodeInsert" CssClass="btn btn-mini btn-info" />
                    </div>
                </ol>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>


    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table7_CostCodeInsert" ForeColor="Red" />

    <asp:GridView runat="server" ID="GridviewTable7_CostCode" CssClass="table table-nonfluid table-hover" GridLines="None" DataKeyNames="CostCode" AllowPaging="true"
        ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table7_CostCode"
        OnCallingDataMethods="GridviewTable7_CostCode_CallingDataMethods"
        SelectMethod="GridviewTable7_CostCode_GetData"
        UpdateMethod="GridviewTable7_CostCode_UpdateItem"
        DeleteMethod="GridviewTable7_CostCode_DeleteItem"
        AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                <EditItemTemplate>
                    <div class="btn-group-vertical" role="group">
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CssClass="btn btn-minier btn-primary"
                            CommandName="Update" Text="Update" ValidationGroup="Table7_CostCodeUpdate"></asp:LinkButton>
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

            <asp:DynamicField DataField="CostCode" ValidationGroup="Table7_CostCodeUpdate" HeaderText="Cost Code" Visible="false"/>

            <asp:TemplateField HeaderText="Cost Code">
                <ItemTemplate>
                    <asp:Label ID="labelCostCode" runat="server" Text="<%# Item.CostCode%>"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="labelCostCode" runat="server" Text="<%# Item.CostCode%>"></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Cost Division Description" HeaderStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="labelCostDivisionDescription" runat="server" Text="<%# PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table7_CostDivision.GetRowByPrimaryKey(Item.CostVidisionID).CostDivisionDescription%>"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListCostDivisionDescription" runat="server"
                        SelectMethod="GetDropDownListCostDivisionDescription"
                        AppendDataBoundItems="true" SelectedValue="<%# BindItem.CostVidisionID%>" ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table7_CostDivision+Table7_CostDivision_For_DDL"
                        DataTextField="CostDivisionDescription" DataValueField="CostVidisionID">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <span class="span_small"><%# Item.CodeDescription%></span>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxCodeDescription" runat="server" Text='<%# Bind("CodeDescription")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table7_CostCodeUpdate"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:DynamicField DataField="UpdatedBy" ValidationGroup="Table7_CostCodeUpdate" HeaderText="Updated By" DataFormatString="{0:dd/MM/yyyy}" ApplyFormatInEditMode="true" />

            <asp:DynamicField DataField="DataCenter" ValidationGroup="Table7_CostCodeUpdate" HeaderText="Data Center" />

            <asp:DynamicField DataField="FromAccess" ValidationGroup="Table7_CostCodeUpdate" HeaderText="From Access" />

            <asp:DynamicField DataField="Type" ValidationGroup="Table7_CostCodeUpdate" HeaderText="Type" />

            <asp:DynamicField DataField="Note" ValidationGroup="Table7_CostCodeUpdate" HeaderText="Note" />


        </Columns>
        <PagerSettings Position="TopAndBottom" />
        <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
        <HeaderStyle CssClass="headergridnew" />
    </asp:GridView>


    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table7_CostCodeUpdate" ForeColor="Red" />

</asp:Content>

