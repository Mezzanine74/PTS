<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="ContractDetails.aspx.vb" Inherits="ContractDetails2" %>

<%@ Register src="POdetailsForEmail.ascx" tagname="SeperateControl" tagprefix="uc1" %>
<%@ Register src="WebUserControl_ContractEmailBody.ascx" tagname="SeperateControl2" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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
                <uc1:SeperateControl2 ID="WebUserControl_ContractEmailBody" runat="server" />
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td style="width:100px; text-align:center; font-size:10px;" >
                          <asp:Literal ID="LiteralDOCheading" runat="server" Visible="false">Contract Template</asp:Literal>
                            <br /><br />
                          <asp:ImageButton ID="HyperLinkDOC" runat="server" visible="false"></asp:ImageButton>
                        </td>
                        <td style="width:100px; text-align:center; font-size:10px;" >
                          <asp:Literal ID="LiteralPDFheading" runat="server" Visible="false" >Contract approved PDF</asp:Literal>
                            <br /><br />
                          <asp:ImageButton ID="HyperLinkPDF" runat="server" visible="false" ImageUrl="~/images/pdf.bmp"></asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center; " colspan="2">
                                <asp:Label ID="LabelPOstatus" runat="server" ForeColor="Red" Font-Size="Larger" Font-Bold="True"> </asp:Label>
                        </td>
                    </tr>
                </table>
                
                <br />

                    <%-- ----------------APPROVAL GRID -------------------------------------------------%>
                          <asp:Gridview ID="GridviewApprovalStatus" runat="server" 
                            DataSourceID="SqlDataSourceApprovalStatus" CssClass="GridviewApprovalStatus" 
                            AutoGenerateColumns="False" 
                            onrowcommand="GridviewApprovalStatus_RowCommand" 
                            onrowdatabound="GridviewApprovalStatus_RowDataBound" >
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
                                <asp:Literal id="LiteralContractID" runat="server" Text='<%# Bind("ContractID") %>' Visible="false"></asp:Literal>
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
                            SelectCommand="ApprovalStatusContract" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                              <asp:QueryStringParameter DefaultValue="0" Name="ContractID" 
                                QueryStringField="ContractID" />
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
                            SelectCommand="ContractReadyForApproval" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                              <asp:QueryStringParameter DefaultValue="0" Name="ContractID" 
                                QueryStringField="ContractID" />
                              <asp:Parameter DefaultValue="1" Name="Shift" Type="Int32" />
                            </SelectParameters>
                          </asp:SqlDataSource>
                    <%-- ----------------/REQUIRED ITEMS ------------------------------------------------%>

                        <br />

                            <asp:Panel ID="PanelNominated" runat="server" >
                            <asp:Label ID="LabelESTM_Approval" runat="server" Font-Size="12px"
                                 Text="" ForeColor="#FF3399" Width="300px">
                            </asp:Label>
                                <br />
                            <asp:DropDownList ID="DDLestm" runat="server" CssClass="DrpDwnListGeneral"
                                AutoPostBack="true" >
                                <asp:ListItem Value="0">No Response Yet</asp:ListItem>
                                <asp:ListItem Value="1">Confirmed</asp:ListItem>
                                <asp:ListItem Value="2">Not Confirmed</asp:ListItem>
                            </asp:DropDownList>
                                <hr />
                             <span Class="PanelNominated" >
                             Nominated Subcontractor, <br /> No Commercial Offer Required
                             </span>
                            </asp:Panel>

                        <br />

                        <asp:GridView ID="GridViewOffers" runat="server" AutoGenerateColumns="False" 
                           DataKeyNames="ContractID" DataSourceID="SqlDataSourceOffers" 
                           EnableModelValidation="True"  
                           CssClass="GridviewApprovalStatus" onrowcommand="GridViewOffers_RowCommand" 
                               onrowdatabound="GridViewOffers_RowDataBound"  >
                           <Columns>
                             <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" 
                             HeaderStyle-Width="150px" ItemStyle-Width="150px"
                               SortExpression="SupplierName" />
                             <asp:BoundField DataField="OfferValueWithVAT" HeaderText="Offer Value With VAT" 
                             HeaderStyle-Width="60px" ItemStyle-Width="60px"
                             DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" 
                               SortExpression="OfferValueWithVAT" />
                             <asp:BoundField DataField="Currency" SortExpression="Currency" />
                             <asp:TemplateField>
                              <ItemTemplate>
                              <asp:ImageButton ID="ImageButtonOfferZip" runat="server" CommandName="OpenZip" 
                              CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Attachment") %>' 
                              imageUrl="~/images/zipicon.png" CausesValidation="False" />
                              </ItemTemplate>
                             </asp:TemplateField>
                           </Columns>
                         </asp:GridView>
                         <asp:SqlDataSource ID="SqlDataSourceOffers" runat="server" 
                           ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                           SelectCommand="SELECT ContractID 
                                              ,[SupplierName]
                                              ,[OfferValueWithVAT]
                                              ,[Currency]
                                              ,[Attachment]
                                          FROM [Table_Contract_Offers] 
                                          INNER JOIN Table6_Supplier 
                                          ON Table6_Supplier.SupplierID = Table_Contract_Offers.SupplierID
                                          WHERE ContractID = @ContractID">
                           <SelectParameters>
                              <asp:QueryStringParameter DefaultValue="0" Name="ContractID" 
                                QueryStringField="ContractID" />
                           </SelectParameters>
                         </asp:SqlDataSource>

                        <%----------------------------------------------------------------- COMMERCIAL OFFERS --%>

            </td>
        </tr>
    </table>

</asp:Content>

