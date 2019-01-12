<%@ Page Title="" Language="VB" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Table3_Invoice_AdditionalApprovalProjectUserMatrix.aspx.vb" Inherits="Pages_PTS_Table3_Invoice_AdditionalApprovalProjectUserMatrix" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/assets/css/styles_PTS_MERCURY.css" />

</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:FormView runat="server" ID="FormviewTable3_Invoice_AdditionalApprovalProjectUserMatrix" DataKeyNames="id" DefaultMode="Insert"
        ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table3_Invoice_AdditionalApprovalProjectUserMatrix"
        OnCallingDataMethods="FormviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_CallingDataMethods"
        SelectMethod="FormviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_GetItem"
        OnItemInserting="FormviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_ItemInserting"
        InsertMethod="FormviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_InsertItem">
        <InsertItemTemplate>
            <fieldset>
                <legend>
                    <div style="display: inline-block">Table3_Invoice_AdditionalApprovalProjectUserMatrix</div>
                </legend>
                <ol>
                    <div>


                        <label>Project Name</label>
                        <asp:DropDownList ID="DropDownListProjectName" runat="server" ValidationGroup="Table3_Invoice_AdditionalApprovalProjectUserMatrixInsert"
                            SelectMethod="GetDropDownListProjectName"
                            AppendDataBoundItems="true"
                            DataTextField="ProjectName" DataValueField="ProjectID">
                            <asp:ListItem Value="">Select ProjectName:</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVDropDownListProjectName" runat="server" ControlToValidate="DropDownListProjectName" ErrorMessage="Required" Display="Dynamic" ValidationGroup="Table3_Invoice_AdditionalApprovalProjectUserMatrixInsert"></asp:RequiredFieldValidator>

                        <label>User Name</label>
                        <asp:DropDownList ID="DropDownListUserName" runat="server" ValidationGroup="Table3_Invoice_AdditionalApprovalProjectUserMatrixInsert"
                            SelectMethod="GetDropDownListUserName"
                            AppendDataBoundItems="true"
                            DataTextField="UserName" DataValueField="UserName">
                            <asp:ListItem Value="">Select UserName:</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVDropDownListUserName" runat="server" ControlToValidate="DropDownListUserName" ErrorMessage="Required" Display="Dynamic" ValidationGroup="Table3_Invoice_AdditionalApprovalProjectUserMatrixInsert"></asp:RequiredFieldValidator>


                        <label></label>
                        <asp:Button ID="btnSave" runat="server" Text="Insert" CommandName="Insert" ValidationGroup="Table3_Invoice_AdditionalApprovalProjectUserMatrixInsert" CssClass="btn btn-mini btn-info" />
                    </div>
                </ol>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>


    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table3_Invoice_AdditionalApprovalProjectUserMatrixInsert" ForeColor="Red" />

    <asp:GridView runat="server" ID="GridviewTable3_Invoice_AdditionalApprovalProjectUserMatrix" CssClass="table table-nonfluid table-hover" GridLines="None" DataKeyNames="id" AllowPaging="true"
        ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table3_Invoice_AdditionalApprovalProjectUserMatrix"
        OnCallingDataMethods="GridviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_CallingDataMethods"
        SelectMethod="GridviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_GetData"
        UpdateMethod="GridviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_UpdateItem"
        DeleteMethod="GridviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_DeleteItem"
        OnRowUpdating="GridviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_RowUpdating"
        AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                <EditItemTemplate>
                    <div class="btn-group-vertical" role="group">
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CssClass="btn btn-minier btn-primary"
                            CommandName="Update" Text="Update" ValidationGroup="Table3_Invoice_AdditionalApprovalProjectUserMatrixUpdate"></asp:LinkButton>
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


            <asp:TemplateField HeaderText="Project Name" HeaderStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="labelProjectName" runat="server" Text="<%# PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(Item.ProjectId).ProjectName%>"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListProjectName" runat="server"
                        SelectMethod="GetDropDownListProjectName"
                        AppendDataBoundItems="true" SelectedValue="<%# Item.ProjectId%>"
                        DataTextField="ProjectName" DataValueField="ProjectID">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

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


        </Columns>
        <PagerSettings Position="TopAndBottom" />
        <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
        <HeaderStyle CssClass="headergridnew" />
    </asp:GridView>


    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table3_Invoice_AdditionalApprovalProjectUserMatrixUpdate" ForeColor="Red" />

</asp:Content>

