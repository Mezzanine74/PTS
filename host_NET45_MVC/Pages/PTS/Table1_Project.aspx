<%@ Page Title="" Language="VB" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Table1_Project.aspx.vb" Inherits="Pages_PTS_Table1_Project" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">

<link rel="stylesheet" href="/assets/css/styles_PTS_MERCURY.css" />

</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:FormView runat="server" ID="FormviewTable1_Project" DataKeyNames="ProjectID" DefaultMode="Insert"
ItemType = "PTS_App_Code_VB_Project.PTS_MERCURY.db.Table1_Project"
OnCallingDataMethods = "FormviewTable1_Project_CallingDataMethods"
SelectMethod = "FormviewTable1_Project_GetItem"
InsertMethod="FormviewTable1_Project_InsertItem">
<InsertItemTemplate>
<fieldset>
<legend><div style="display:inline-block">Table1_Project</div><div style="display:inline-block; float:right;"><a href="_ListOfTables.aspx">List Of Tables</a></div></legend>
<ol>
<div>


<label>Project Name</label>
<asp:TextBox ID="TextBoxProjectName" runat="server" Text='<%# Bind("ProjectName")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table1_ProjectInsert" ></asp:TextBox>

<label>Project Manager</label>
<asp:TextBox ID="TextBoxProjectManager" runat="server" Text='<%# Bind("ProjectManager")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table1_ProjectInsert" ></asp:TextBox>

<label>Project Adress</label>
<asp:TextBox ID="TextBoxProjectAdress" runat="server" Text='<%# Bind("ProjectAdress")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table1_ProjectInsert" ></asp:TextBox>

<label>P Memail</label>
<asp:TextBox ID="TextBoxPM_email" runat="server" Text='<%# Bind("PM_email")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table1_ProjectInsert" ></asp:TextBox>

<label>Current Status</label>
<asp:DynamicControl runat="server" DataField="CurrentStatus" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Updated By</label>
<asp:DynamicControl runat="server" DataField="UpdatedBy" Mode="Insert" ValidationGroup="Table1_ProjectInsert" DataFormatString = "{0:dd/MM/yyyy}" />

<label>From Access</label>
<asp:DynamicControl runat="server" DataField="FromAccess" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Type</label>
<asp:DynamicControl runat="server" DataField="Type" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Report</label>
<asp:DynamicControl runat="server" DataField="Report" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Back Up Required</label>
<asp:DynamicControl runat="server" DataField="BackUpRequired" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Insurance Start</label>
<asp:DynamicControl runat="server" DataField="InsuranceStart" Mode="Insert" ValidationGroup="Table1_ProjectInsert" DataFormatString = "{0:dd/MM/yyyy}" />

<label>Insurance Finish</label>
<asp:DynamicControl runat="server" DataField="InsuranceFinish" Mode="Insert" ValidationGroup="Table1_ProjectInsert" DataFormatString = "{0:dd/MM/yyyy}" />

<label>P Ocreate</label>
<asp:DynamicControl runat="server" DataField="POcreate" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Completion Date</label>
<asp:DynamicControl runat="server" DataField="CompletionDate" Mode="Insert" ValidationGroup="Table1_ProjectInsert" DataFormatString = "{0:dd/MM/yyyy}" />

<label>Contract Currency</label>
<asp:DynamicControl runat="server" DataField="ContractCurrency" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Contract Amount</label>
<asp:DynamicControl runat="server" DataField="ContractAmount" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Margin</label>
<asp:DynamicControl runat="server" DataField="Margin" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Nakladnaya Enabled</label>
<asp:DynamicControl runat="server" DataField="NakladnayaEnabled" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Back Color</label>
<asp:DynamicControl runat="server" DataField="BackColor" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Fore Color</label>
<asp:DynamicControl runat="server" DataField="ForeColor" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Pending Summary</label>
<asp:DynamicControl runat="server" DataField="PendingSummary" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Approve Required</label>
<asp:DynamicControl runat="server" DataField="ApproveRequired" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Requested Required</label>
<asp:DynamicControl runat="server" DataField="RequestedRequired" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Dont Show Contract Po Assistance</label>
<asp:DynamicControl runat="server" DataField="DontShowContractPoAssistance" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Change Order Signed Exc V A T</label>
<asp:DynamicControl runat="server" DataField="ChangeOrderSignedExcVAT" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Send Emails Non Zero Balance</label>
<asp:DynamicControl runat="server" DataField="SendEmailsNonZeroBalance" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>New Generation</label>
<asp:DynamicControl runat="server" DataField="NewGeneration" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Logo</label>
<asp:TextBox ID="TextBoxLogo" runat="server" Text='<%# Bind("Logo")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table1_ProjectInsert" ></asp:TextBox>

<label>New Po Policy</label>
<asp:DynamicControl runat="server" DataField="NewPoPolicy" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Po Maker New Generation</label>
<asp:DynamicControl runat="server" DataField="PoMakerNewGeneration" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>P R Coverpage Approval Allowed</label>
<asp:DynamicControl runat="server" DataField="PR_Coverpage_Approval_Allowed" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>P R Coverpage Approval Compulsory</label>
<asp:DynamicControl runat="server" DataField="PR_Coverpage_Approval_Compulsory" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>P M Signatures Link</label>
<asp:TextBox ID="TextBoxPM_SignaturesLink" runat="server" Text='<%# Bind("PM_SignaturesLink")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table1_ProjectInsert" ></asp:TextBox>

<label>Auto C C Approval</label>
<asp:DynamicControl runat="server" DataField="AutoCCApproval" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />

<label>Extra Approval If No Signed Client Contract</label>
<asp:DynamicControl runat="server" DataField="ExtraApprovalIfNoSignedClientContract" Mode="Insert" ValidationGroup="Table1_ProjectInsert" />


<label></label>
<asp:Button ID="btnSave" runat="server" Text="Insert" CommandName="Insert" ValidationGroup="Table1_ProjectInsert" CssClass="btn btn-mini btn-info" />
</div>
</ol>
</fieldset>
</InsertItemTemplate>
</asp:FormView>


<asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table1_ProjectInsert" ForeColor="Red" />

<asp:GridView runat="server" ID="GridviewTable1_Project" CssClass="table table-nonfluid table-hover" GridLines="None" DataKeyNames="ProjectID" AllowPaging="true"
ItemType = "PTS_App_Code_VB_Project.PTS_MERCURY.db.Table1_Project"
OnCallingDataMethods = "GridviewTable1_Project_CallingDataMethods"
SelectMethod = "GridviewTable1_Project_GetData"
UpdateMethod="GridviewTable1_Project_UpdateItem"
DeleteMethod="GridviewTable1_Project_DeleteItem"
AutoGenerateColumns="false">
<Columns>
<asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
<EditItemTemplate>
<div class="btn-group-vertical" role="group">
<asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CssClass="btn btn-minier btn-primary"
CommandName="Update" Text="Update" ValidationGroup="Table1_ProjectUpdate"></asp:LinkButton>
<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
CommandName="Cancel" Text="Cancel"></asp:LinkButton>
</div>
</EditItemTemplate>
<ItemTemplate>
<div class="btn-group-vertical" role="group">
<asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
CommandName="Edit" Text="Edit"></asp:LinkButton>
<asp:LinkButton ID="LinkButtonDelete" runat="server" CssClass="btn btn-minier btn-danger" CausesValidation="false"
OnClientClick = "return confirm('Are you sure you want to delete this record?');"
CommandName="Delete" Text="Delete"></asp:LinkButton>
</div>
</ItemTemplate>
</asp:TemplateField>


            <asp:TemplateField >
                <ItemTemplate>
                       <span class="span_small"><%# Item.ProjectName%></span>
                </ItemTemplate>
                <EditItemTemplate>
                       <asp:TextBox ID="TextBoxProjectName" runat="server" Text='<%# Bind("ProjectName")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table1_ProjectUpdate" ></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField >
                <ItemTemplate>
                       <span class="span_small"><%# Item.ProjectManager%></span>
                </ItemTemplate>
                <EditItemTemplate>
                       <asp:TextBox ID="TextBoxProjectManager" runat="server" Text='<%# Bind("ProjectManager")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table1_ProjectUpdate" ></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField >
                <ItemTemplate>
                       <span class="span_small"><%# Item.ProjectAdress%></span>
                </ItemTemplate>
                <EditItemTemplate>
                       <asp:TextBox ID="TextBoxProjectAdress" runat="server" Text='<%# Bind("ProjectAdress")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table1_ProjectUpdate" ></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField >
                <ItemTemplate>
                       <span class="span_small"><%# Item.PM_email%></span>
                </ItemTemplate>
                <EditItemTemplate>
                       <asp:TextBox ID="TextBoxPM_email" runat="server" Text='<%# Bind("PM_email")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table1_ProjectUpdate" ></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

<asp:DynamicField DataField="CurrentStatus" ValidationGroup="Table1_ProjectUpdate" HeaderText="Current Status" />

<asp:DynamicField DataField="UpdatedBy" ValidationGroup="Table1_ProjectUpdate" HeaderText="Updated By" DataFormatString="{0:dd/MM/yyyy}" ApplyFormatInEditMode="true" />

<asp:DynamicField DataField="FromAccess" ValidationGroup="Table1_ProjectUpdate" HeaderText="From Access" />

<asp:DynamicField DataField="Type" ValidationGroup="Table1_ProjectUpdate" HeaderText="Type" />

<asp:DynamicField DataField="Report" ValidationGroup="Table1_ProjectUpdate" HeaderText="Report" />

<asp:DynamicField DataField="BackUpRequired" ValidationGroup="Table1_ProjectUpdate" HeaderText="Back Up Required" />

<asp:DynamicField DataField="InsuranceStart" ValidationGroup="Table1_ProjectUpdate" HeaderText="Insurance Start" DataFormatString="{0:dd/MM/yyyy}" ApplyFormatInEditMode="true" />

<asp:DynamicField DataField="InsuranceFinish" ValidationGroup="Table1_ProjectUpdate" HeaderText="Insurance Finish" DataFormatString="{0:dd/MM/yyyy}" ApplyFormatInEditMode="true" />

<asp:DynamicField DataField="POcreate" ValidationGroup="Table1_ProjectUpdate" HeaderText="P Ocreate" />

<asp:DynamicField DataField="CompletionDate" ValidationGroup="Table1_ProjectUpdate" HeaderText="Completion Date" DataFormatString="{0:dd/MM/yyyy}" ApplyFormatInEditMode="true" />

<asp:DynamicField DataField="ContractCurrency" ValidationGroup="Table1_ProjectUpdate" HeaderText="Contract Currency" />

<asp:DynamicField DataField="ContractAmount" ValidationGroup="Table1_ProjectUpdate" HeaderText="Contract Amount" />

<asp:DynamicField DataField="Margin" ValidationGroup="Table1_ProjectUpdate" HeaderText="Margin" />

<asp:DynamicField DataField="NakladnayaEnabled" ValidationGroup="Table1_ProjectUpdate" HeaderText="Nakladnaya Enabled" />

<asp:DynamicField DataField="BackColor" ValidationGroup="Table1_ProjectUpdate" HeaderText="Back Color" />

<asp:DynamicField DataField="ForeColor" ValidationGroup="Table1_ProjectUpdate" HeaderText="Fore Color" />

<asp:DynamicField DataField="PendingSummary" ValidationGroup="Table1_ProjectUpdate" HeaderText="Pending Summary" />

<asp:DynamicField DataField="ApproveRequired" ValidationGroup="Table1_ProjectUpdate" HeaderText="Approve Required" />

<asp:DynamicField DataField="RequestedRequired" ValidationGroup="Table1_ProjectUpdate" HeaderText="Requested Required" />

<asp:DynamicField DataField="DontShowContractPoAssistance" ValidationGroup="Table1_ProjectUpdate" HeaderText="Dont Show Contract Po Assistance" />

<asp:DynamicField DataField="ChangeOrderSignedExcVAT" ValidationGroup="Table1_ProjectUpdate" HeaderText="Change Order Signed Exc V A T" />

<asp:DynamicField DataField="SendEmailsNonZeroBalance" ValidationGroup="Table1_ProjectUpdate" HeaderText="Send Emails Non Zero Balance" />

<asp:DynamicField DataField="NewGeneration" ValidationGroup="Table1_ProjectUpdate" HeaderText="New Generation" />

            <asp:TemplateField >
                <ItemTemplate>
                       <span class="span_small"><%# Item.Logo%></span>
                </ItemTemplate>
                <EditItemTemplate>
                       <asp:TextBox ID="TextBoxLogo" runat="server" Text='<%# Bind("Logo")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table1_ProjectUpdate" ></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

<asp:DynamicField DataField="NewPoPolicy" ValidationGroup="Table1_ProjectUpdate" HeaderText="New Po Policy" />

<asp:DynamicField DataField="PoMakerNewGeneration" ValidationGroup="Table1_ProjectUpdate" HeaderText="Po Maker New Generation" />

<asp:DynamicField DataField="PR_Coverpage_Approval_Allowed" ValidationGroup="Table1_ProjectUpdate" HeaderText="P R Coverpage Approval Allowed" />

<asp:DynamicField DataField="PR_Coverpage_Approval_Compulsory" ValidationGroup="Table1_ProjectUpdate" HeaderText="P R Coverpage Approval Compulsory" />

            <asp:TemplateField >
                <ItemTemplate>
                       <span class="span_small"><%# Item.PM_SignaturesLink%></span>
                </ItemTemplate>
                <EditItemTemplate>
                       <asp:TextBox ID="TextBoxPM_SignaturesLink" runat="server" Text='<%# Bind("PM_SignaturesLink")%>' TextMode="MultiLine" Rows="5" ValidationGroup="Table1_ProjectUpdate" ></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

<asp:DynamicField DataField="AutoCCApproval" ValidationGroup="Table1_ProjectUpdate" HeaderText="Auto C C Approval" />

<asp:DynamicField DataField="ExtraApprovalIfNoSignedClientContract" ValidationGroup="Table1_ProjectUpdate" HeaderText="Extra Approval If No Signed Client Contract" />


</Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />
</asp:GridView>


<asp:ValidationSummary runat="server" ShowModelStateErrors="true" ValidationGroup="Table1_ProjectUpdate" ForeColor="Red" />

</asp:Content>

