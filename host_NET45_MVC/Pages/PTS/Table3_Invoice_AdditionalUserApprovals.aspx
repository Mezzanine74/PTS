<%@ Page Title="" Language="VB" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Table3_Invoice_AdditionalUserApprovals.aspx.vb" Inherits="Pages_PTS_Table3_Invoice_AdditionalUserApprovals" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/assets/css/styles_PTS_MERCURY.css" />

</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:FormView runat="server" ID="FormviewTable3_Invoice_AdditionalUserApprovals" DataKeyNames="id" DefaultMode="Insert"
        ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table3_Invoice_AdditionalUserApprovals"
        OnCallingDataMethods="FormviewTable3_Invoice_AdditionalUserApprovals_CallingDataMethods"
        SelectMethod="FormviewTable3_Invoice_AdditionalUserApprovals_GetItem"
        OnItemInserting="FormviewTable3_Invoice_AdditionalUserApprovals_ItemInserting"
        InsertMethod="FormviewTable3_Invoice_AdditionalUserApprovals_InsertItem">
        <InsertItemTemplate>
            <fieldset>
                <legend>
                    <div style="display: inline-block">Table3_Invoice_AdditionalUserApprovals</div>
                </legend>
                <ol>
                    <div>


                        <label>Invoice Id</label>
                        <asp:DynamicControl runat="server" DataField="InvoiceId" Mode="Insert" ValidationGroup="Table3_Invoice_AdditionalUserApprovalsInsert" />

                        <label>User Name</label>
                        <asp:DropDownList ID="DropDownListUserName" runat="server" ValidationGroup="Table3_Invoice_AdditionalUserApprovalsInsert"
                            SelectMethod="GetDropDownListUserName"
                            AppendDataBoundItems="true"
                            DataTextField="UserName" DataValueField="UserName">
                            <asp:ListItem Value="">Select UserName:</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVDropDownListUserName" runat="server" ControlToValidate="DropDownListUserName" ErrorMessage="Required" Display="Dynamic" ValidationGroup="Table3_Invoice_AdditionalUserApprovalsInsert"></asp:RequiredFieldValidator>

                        <label>When Approved</label>
                        <asp:DynamicControl runat="server" DataField="WhenApproved" Mode="Insert" ValidationGroup="Table3_Invoice_AdditionalUserApprovalsInsert" DataFormatString="{0:dd/MM/yyyy}" />


                        <label></label>
                        <asp:Button ID="btnSave" runat="server" Text="Insert" CommandName="Insert" ValidationGroup="Table3_Invoice_AdditionalUserApprovalsInsert" CssClass="btn btn-mini btn-info" />
                    </div>
                </ol>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>


    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table3_Invoice_AdditionalUserApprovalsInsert" ForeColor="Red" />

    <asp:GridView runat="server" ID="GridviewTable3_Invoice_AdditionalUserApprovals" CssClass="table table-nonfluid table-hover" GridLines="None" DataKeyNames="id" AllowPaging="true"
        ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table3_Invoice_AdditionalUserApprovals"
        OnCallingDataMethods="GridviewTable3_Invoice_AdditionalUserApprovals_CallingDataMethods"
        SelectMethod="GridviewTable3_Invoice_AdditionalUserApprovals_GetData"
        UpdateMethod="GridviewTable3_Invoice_AdditionalUserApprovals_UpdateItem"
        DeleteMethod="GridviewTable3_Invoice_AdditionalUserApprovals_DeleteItem"
        OnRowUpdating="GridviewTable3_Invoice_AdditionalUserApprovals_RowUpdating"
        AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                <EditItemTemplate>
                    <div class="btn-group-vertical" role="group">
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CssClass="btn btn-minier btn-primary"
                            CommandName="Update" Text="Update" ValidationGroup="Table3_Invoice_AdditionalUserApprovalsUpdate"></asp:LinkButton>
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


            <asp:DynamicField DataField="InvoiceId" ValidationGroup="Table3_Invoice_AdditionalUserApprovalsUpdate" HeaderText="Invoice Id" />

            <asp:TemplateField HeaderText="User Name" HeaderStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="labelUserName" runat="server" Text="<%# PTS_App_Code_VB_Project.PTS_MERCURY.helper.aspnet_Users.GetRowByUserName(Item.UserName).UserName%>"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListUserName" runat="server"
                        SelectMethod="GetDropDownListUserName"
                        AppendDataBoundItems="true" SelectedValue="<%# Item.UserName%>"
                        DataTextField="UserName" DataValueField="UserName">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:DynamicField DataField="WhenApproved" ValidationGroup="Table3_Invoice_AdditionalUserApprovalsUpdate" HeaderText="When Approved" DataFormatString="{0:dd/MM/yyyy}" ApplyFormatInEditMode="true" />


        </Columns>
        <PagerSettings Position="TopAndBottom" />
        <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
        <HeaderStyle CssClass="headergridnew" />
    </asp:GridView>


    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table3_Invoice_AdditionalUserApprovalsUpdate" ForeColor="Red" />

</asp:Content>

