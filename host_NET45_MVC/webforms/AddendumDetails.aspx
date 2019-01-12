<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="AddendumDetails.aspx.vb" Inherits="AddendumDetails2" %>

<%@ Register src="POdetailsForEmail.ascx" tagname="SeperateControl" tagprefix="uc1" %>

<%@ Register src="WebUserControl_AddendumEmailBody.ascx" tagname="SeperateControl2" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

        .styleTRhorizontal
        {
            border-color:#E6E6FA;
            border-bottom-style:solid;
            border-width:1px;
        }
        
     </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div id="ModalFileManager" class="modal">
        <div class="modal-dialog modal-dialog-center">
            <div class="modal-content modal_inlineBlock">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Manage Files</h4>
                </div>
                <div class="modal-body" style="width:800px;" >
                    <asp:Panel ID="panelContainer" runat="server">
                        <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Visible="true" Height="300" >
                            <Settings RootFolder="~/CONTRACT\_doNotDeleteThis" ThumbnailFolder="~/Thumb/" EnableMultiSelect="true" />
                            <SettingsFileList View="Details"></SettingsFileList>
                            <SettingsEditing AllowDownload="true"/>
                            <SettingsUpload Enabled="false" AdvancedModeSettings-EnableMultiSelect="true"></SettingsUpload>
                            <SettingsFolders visible="false" />
                            <ClientSideEvents SelectedFileOpened="function(s, e) {
	                            e.file.Download();
	                            e.processOnServer = false;
                            }" />
                        </dx:ASPxFileManager>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

<asp:panel ID="PanelToAddendums"  runat="server" CssClass="hidepanel">
   <uc1:SeperateControl ID="POdetailsForEmail" runat="server" />
</asp:panel>

    <asp:Label ID="LabelHooverToolTipContract" runat="server" CssClass="label label-inverse label-xlg"></asp:Label>

    <hr />

<table>
    <tr>
        <td>
            <uc1:SeperateControl2 ID="WebUserControl_AddendumEmailBody" runat="server" />
        </td>
        <td style="vertical-align:top;">

            <table>
                            <tr>
                                    <td style="width:100px; text-align:center; font-size:10px;" >
                                      <asp:Literal ID="LiteralDOCheading" runat="server" Visible="false">Addendum Template</asp:Literal>
                                      <asp:ImageButton ID="HyperLinkDOC" runat="server" visible="false" ></asp:ImageButton>
                                    </td>
                                    <td style="width:100px; text-align:center; font-size:10px;" >
                                      <asp:Literal ID="LiteralPDFheading" runat="server" Visible="false" >Addendum approved PDF</asp:Literal>
                                      <asp:ImageButton ID="HyperLinkPDF" runat="server" visible="false" ImageUrl="~/images/pdf.bmp" ></asp:ImageButton>
                                    </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align:center;">
                                    <asp:Label ID="LabelPOstatus" runat="server" ForeColor="Red" Font-Size="Larger" Font-Bold="True" >
                                    </asp:Label>
                                </td>
                            </tr>
            </table>

            <br />
            
                    <%-- ----------------APPROVAL GRID -------------------------------------------------%>

                          <asp:Gridview ID="GridviewApprovalStatusAddendum" runat="server" 
                            DataSourceID="SqlDataSourceApprovalStatus" CssClass="GridviewApprovalStatus" 
                            AutoGenerateColumns="False" EmptyDataText="Approval is not required!" 
                            EmptyDataRowStyle-ForeColor="Red" 
                            onrowcommand="GridviewApprovalStatusAddendum_RowCommand" 
                            onrowdatabound="GridviewApprovalStatusAddendum_RowDataBound">
                            <Columns>
                              <asp:TemplateField >
                                <ItemTemplate>
                                 <asp:Literal id="LiteralUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Literal>
                                 <asp:Literal id="LiteralWhichLawyer" Visible="false" runat="server" Text='<%# Bind("WhoApprovedFromLawyers")%>'></asp:Literal>
                                </ItemTemplate>
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Approved" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <%-- LiteralApproved and LiteralContractID should stay here, they are being used in RowCommand --%>
                                <asp:Literal id="LiteralApproved" runat="server" Text='<%# Bind("Approved") %>' Visible="false"></asp:Literal>
                                <asp:Literal id="LiteralAddendumID" runat="server" Text='<%# Bind("AddendumID") %>' Visible="false"></asp:Literal>
                                <asp:Literal id="LiteralRejectContractGirls" runat="server" Text='<%# Bind("Exception") %>' Visible="false"></asp:Literal>
                                <asp:ImageButton ID="ImageButtonApproval" runat="server" CommandName="Approval" 
                                CommandArgument='<%# Container.DataItemIndex %>' 
                                 CausesValidation="False" />
                                <asp:LinkButton ID="LinkButtonRejectContractGirls" runat="server" CommandName="ApprovalByRejecting" 
                                CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False"  />
                                </ItemTemplate>
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="When Approved">
                                <ItemTemplate>
                                 <asp:Literal id="LiteralWhenApproved" runat="server" Text='<%# Bind("WhenApproved") %>'></asp:Literal>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:Gridview>
                          <asp:SqlDataSource ID="SqlDataSourceApprovalStatus" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="ApprovalStatusAddendum" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                              <asp:QueryStringParameter DefaultValue="0" Name="AddendumID" 
                                QueryStringField="AddendumID" />
                              <asp:Parameter DefaultValue="1" Name="Shift" Type="Int32" />
                            </SelectParameters>
                          </asp:SqlDataSource>

                    <%-- ----------------/APPROVAL GRID -------------------------------------------------%>

                    <br />

                    <%-- ----------------REQUIRED ITEMS -------------------------------------------------%>

                          <asp:Gridview ID="GridviewMissingItemsForApproval" runat="server" 
                            CssClass="GridviewMissingBeforeApproval" DataSourceID="SqlDataSourceMissingItemsForApproval"
                            AutoGenerateColumns="False" >
                            <Columns>
                              <asp:TemplateField HeaderText="Required Items Before Approval" ItemStyle-Width="120px">
                                <ItemTemplate>
                                 <asp:Literal id="LiteralTitle" runat="server" Text='<%# "• " + Eval("Title") %>'></asp:Literal>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:Gridview>
                          <asp:SqlDataSource ID="SqlDataSourceMissingItemsForApproval" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="AddendumReadyForApproval" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                              <asp:QueryStringParameter DefaultValue="0" Name="AddendumID" 
                                QueryStringField="AddendumID" />
                              <asp:Parameter DefaultValue="1" Name="Shift" Type="Int32" />
                            </SelectParameters>
                          </asp:SqlDataSource>

                    <%-- ----------------/REQUIRED ITEMS ------------------------------------------------%>


        </td>
    </tr>
</table>
    
</asp:Content>

