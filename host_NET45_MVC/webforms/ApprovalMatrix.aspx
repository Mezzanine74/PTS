<%@ Page Title="" Language="VB" MasterPageFile="~/site.master"  EnableEventValidation="false" EnableViewState="true" 
AutoEventWireup="false" CodeFile="ApprovalMatrix.aspx.vb" Inherits="ApprovalMatrixRev334" MaintainScrollPositionOnPostback="True"   %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="POdetailsForEmail.ascx" tagname="SeperateControl" tagprefix="uc1" %>

<%@ Register src="WebUserControl_ContractEmailBody.ascx" tagname="SeperateControl2" tagprefix="uc1" %>

<%@ Register src="WebUserControl_AddendumEmailBody.ascx" tagname="SeperateControl3" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

        .style1
        {
            width: 69px;
            height:20px;
            background-color: #D4EAF8;
            text-align: center;
        }
        
        .style2
        {
            width: 69px;
            height:35px;
            background-color: #F2FAFD;
            text-align: center;       
        }

        .style3
        {
            width: 69px;
            height:35px;
            background-color: #F2FAFD;
            text-align: center;
            color:Gray;
            font-style:italic;
        }

        .pad_td {
            padding:7px;
        }

        .pad td {
            padding:5px;
        }

        .LabelNotification {
            padding:1px 2px 1px 2px;
            margin-left:2px;
            background-color:red;
            color:white;
            font-weight:bold;
            font-size:11px;
            border-radius:30px;
            box-shadow:1px 1px 1px gray;
            display:inline;
        }

        .pnl_newsup {
            margin:1px; padding:2px; font-size:8px; color:#E74C3C;
            background-color:#FDEDEC;border-color:Black;border-style:Solid; 
            border-width:1px; font-weight:bold;width:100px;text-align:center;
        }

        .min100 {
            min-width:115px !important;
        }

        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:HiddenField ID="PageUserName" runat="server" Value="osman" />

<div id="ModalRemarksAddendum" class="modal">
    <div class="modal-dialog">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Comments</h4>
            </div>
            <div class="modal-body">
            <%--START--%>
                <div style="background-color:white; padding:10px;">
                            <div data-bind="foreach: global.Addendums_UserRemarkModel.Me">
                                <div data-bind="visible: ShowOrHide">
                                    <textarea name="CommentEntry" data-bind="textinput: Remark" rows="2" cols="20" style="height:100px;width:300px;"></textarea>
                                    <input type="submit" value="Add Comment" class="btn btn-mini btn-success" data-bind="click: function () { global.Addendums_UserRemarkModel.Add($data) }" >
                                </div>
                            </div>

                          <hr>

                          <div style="max-height: 400px; overflow: scroll">
		                  <table id="AddendumComments" class="table table-condensed" >
                              <thead>
                                <tr>
                                    <th></th>
				                    <th >UserName</th>
                                    <th >Remark</th>
                                    <th >LastUpdate</th>
			                    </tr>
                              </thead>
			                <tbody data-bind="foreach: global.Addendums_UserRemarkListModel.Comments">
                                <tr data-bind="visible: ItemMode">
                                    <td>
                                        <div data-bind="visible: ShowOrHide">
                                            <button data-bind="click: function () { global.Addendums_UserRemarkModel.ChangeModeToEdit($data) } " class="btn btn-info btn-mini" style="width:65px;">Edit</button>
                                            <button data-bind="click: function () { if (confirm('do you want to delete')) global.Addendums_UserRemarkModel.Delete($data) } " class="btn btn-danger btn-mini" style="width:65px;">Delete</button>
                                        </div>
                                    </td>
				                    <td data-bind="text: UserName" ></td>
                                    <td data-bind="text: Remark" style="width:300px;"></td>
                                    <td data-bind="text: LastUpdate"></td>
			                    </tr>
                                <tr data-bind="visible: EditMode">
                                    <td>
                                        <button data-bind="click: function () { global.Addendums_UserRemarkModel.Update($data) } " class="btn btn-success btn-mini" style="width:65px;">Update</button>
                                    </td>
				                    <td data-bind="text: UserName" ></td>
                                    <td style="width:300px;">
                                        <textarea name="Remark" data-bind="textinput: Remark" rows="2" cols="20" style="height:100px;width:300px;"></textarea>
                                    </td>
                                    <td data-bind="text: LastUpdate"></td>
			                    </tr>
		                     </tbody>
		                  </table>
                          </div>

                </div>
            <%--FINISH--%>
            </div>
        </div>
    </div>
</div>

<div id="ModalRemarksContract" class="modal">
    <div class="modal-dialog">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Comments</h4>
            </div>
            <div class="modal-body">
            <%--START--%>
                <div style="background-color:white; padding:10px;">

                            <div data-bind="foreach: global.Contracts_UserRemarkModel.Me">
                                <div data-bind="visible: ShowOrHide">
                                    <textarea name="CommentEntry" data-bind="textinput: Remark" rows="2" cols="20" style="height:100px;width:300px;"></textarea>
                                    <input type="submit" value="Add Comment" class="btn btn-mini btn-success" data-bind="click: function () { global.Contracts_UserRemarkModel.Add($data) }" >
                                </div>
                            </div>

                          <hr>

                          <div style="max-height: 400px; overflow: scroll">

		                  <table id="MainContent_GridViewComments" class="table table-condensed" >
                              <thead>
                                <tr>
                                    <th></th>
				                    <th >UserName</th>
                                    <th >Remark</th>
                                    <th >LastUpdate</th>
			                    </tr>
                              </thead>
			                <tbody data-bind="foreach: global.Contracts_UserRemarkListModel.Comments">
                                <tr data-bind="visible: ItemMode">
                                    <td>
                                        <div data-bind="visible: ShowOrHide">
                                            <button data-bind="click: function () { global.Contracts_UserRemarkModel.ChangeModeToEdit($data) } " class="btn btn-info btn-mini" style="width:65px;">Edit</button>
                                            <button data-bind="click: function () { if (confirm('do you want to delete')) global.Contracts_UserRemarkModel.Delete($data) } " class="btn btn-danger btn-mini" style="width:65px;">Delete</button>
                                        </div>
                                    </td>
				                    <td data-bind="text: UserName" ></td>
                                    <td data-bind="text: Remark" style="width:300px;"></td>
                                    <td data-bind="text: LastUpdate"></td>
			                    </tr>
                                <tr data-bind="visible: EditMode">
                                    <td>
                                        <button data-bind="click: function () { global.Contracts_UserRemarkModel.Update($data) }" class="btn btn-success btn-mini" style="width:65px;">Update</button>
                                    </td>
				                    <td data-bind="text: UserName" ></td>
                                    <td style="width:300px;">
                                        <textarea name="Remark" data-bind="textinput: Remark" rows="2" cols="20" style="height:100px;width:300px;"></textarea>
                                    </td>
                                    <td data-bind="text: LastUpdate"></td>
			                    </tr>
		                     </tbody>
		                  </table>
                          </div> 

                  </div>
            <%--FINISH--%>
            </div>
        </div>
    </div>
</div>

   <asp:Panel ID="PanelHidden" runat="server" CssClass="hidepanel">
       <uc1:SeperateControl ID="POdetailsForEmail" runat="server" />
       <uc1:SeperateControl2 ID="WebUserControl_ContractEmailBody" runat="server" />
       <uc1:SeperateControl3 ID="WebUserControl_AddendumEmailBody" runat="server" />
   </asp:Panel>

    <asp:Literal ID="LiteralContractIDTransfer" runat="server" Visible="false" ></asp:Literal>
    <asp:Literal ID="LiteralAddendumIDTransfer" runat="server" Visible="false" ></asp:Literal>
    <asp:Literal ID="LiteralUserNameTransfer" runat="server" Visible="false" ></asp:Literal>

  <%-- MODAL POPUP REMARK --%>
  <asp:ModalPopupExtender ID="ModalPopupExtenderRemarkFromUser" runat="server"
   TargetControlID="ButtonRemarkFromUser" Drag="false"
   PopupControlID="PanelRemarkFromUser"
   BackgroundCssClass="modalBackground"
   CancelControlID="btnCancelRemarkFromUser" >
  </asp:ModalPopupExtender>

  <asp:Panel ID="PanelRemarkFromUser" runat="server" Style="display:none;" >
     <div style="text-align: right">
                <asp:Button ID="btnCancelRemarkFromUser" runat="server" Text="X" 
                 Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066" 
                 OnClientClick="changeClass" />
     </div>

       <table class="ContractRemarks" style="display:none;" >
        <tr>
         <td>
          <asp:Button Id="ButtonUpdate" runat="server" Text="Update My Comment" 
            BackColor="Lime" Font-Bold="True" Font-Size="12px" ForeColor="White"/>
         </td>
        </tr>
        <tr>
         <td>
          <asp:TextBox ID="TextBoxEditorNotes" runat="server" Width="600px" Height="500px" TextMode="MultiLine" ></asp:TextBox>
         </td>
        </tr>
       </table>

      <div style="background-color:white; border-color:black; border-style:solid; border-width:thick; padding:10px;">
          <asp:Panel ID="PanelEntry" runat="server">
          Enter your comment:
          <hr />
          <asp:TextBox ID="TextBoxUserEntry" runat="server" Width="300px" Height="100px" TextMode="MultiLine" ></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredValidator" runat="server" ControlToValidate="TextBoxUserEntry" ValidationGroup="commnet" ErrorMessage="required" Display="Dynamic"></asp:RequiredFieldValidator>
          <asp:Button Id="ButtonGetEntry" runat="server" Text="Comment" Font-Bold="True" Font-Size="12px" OnClick="ButtonGetEntry_Click" ValidationGroup="commnet" />

          <br />
          <br />
          </asp:Panel>
          Comments:

          <hr />

          <div style="max-height: 400px; overflow: scroll">

          <asp:GridView ID="GridViewComments" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSourceEntry" Font-Size="11px">
              <Columns>
                  <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" ItemStyle-CssClass="pad_td"/>
                  <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark"  ItemStyle-Width="300px"/>
                  <asp:BoundField DataField="LastUpdate" HeaderText="LastUpdate" SortExpression="LastUpdate" />
              </Columns>
           </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSourceEntry" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                SelectCommand="

                IF @Type = N'contract'
                BEGIN
                  SELECT [id]
                  ,[ContractID] AS ID_
                  ,[UserName]
                  ,[Remark]
                  ,[LastUpdate]
                  FROM [dbo].[Table_Contracts_UserRemarks]
                  WHERE ContractID = @id
                END

                IF @Type = N'addendum'
                BEGIN
                SELECT [id]
                      ,[AddendumID] AS ID_
                      ,[UserName]
                      ,[Remark]
                      ,[LastUpdate]
                  FROM [dbo].[Table_Addendum_UserRemarks]
                  WHERE AddendumID = @id
                END 

                ">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="id" Type="Int32" />
                    <asp:Parameter DefaultValue="-" Name="Type" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>

          </div> 

      </div>

   </asp:Panel>

   <asp:Button id="ButtonRemarkFromUser"  runat="server" CssClass="hidepanel"/>
  <%-- /MODAL POPUP REMARK --%>

                                <usercontrol:checkboxBootstrap ID="cbxAll" runat="server" Text="Show All Items" OnCheckChanged="cbxAll_CheckedChanged" AutoPostBack="true" />

                                <usercontrol:checkboxBootstrap ID="cbxOnlyMy" runat="server" Text="Show Only My Items" OnCheckChanged="cbxOnly_CheckedChanged" AutoPostBack="true" />

                                <asp:DropDownList ID="DropDownListPrj" runat="server" 
                                    DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                                    DataValueField="ProjectID" AutoPostBack="True" >
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                    SelectCommand="SELECT 0 AS ProjectID, N'_ALL PROJECTS' AS ProjectName UNION ALL SELECT [ProjectID], [ProjectName] FROM [Table1_Project] WHERE ([NewGeneration] = 1) ORDER BY [ProjectName]">
                                </asp:SqlDataSource>

     <hr />

        <asp:GridView ID="GridviewNotApprovedContractsOrAddendums" runat="server"
            DataSourceID="SqlDataSourceNotApprovedContractsOrAddendums"
            AutoGenerateColumns="False"
            DataKeyNames="ContractID, AddendumID"
            EnableModelValidation="True" 
            GridLines="None" CssClass="table table-nonfluid" >
            <EmptyDataTemplate>
                NO DATA
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField >

                    <HeaderTemplate>
                        <header>
                            <%= BodyTexts.Ref("mWVv/Gkf90+3DjgnlKVwkA")%>
                        </header>
                    </HeaderTemplate>
                    <ItemTemplate>
                                <span style="font-size:large; margin-left:100px;">
                                    <asp:Literal ID="LiteralSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Literal>
                                </span>
                    <table>
                        <tr>
                            <td style="padding:5px; width:100px">
                                <asp:Image ID="ImageLogo" runat="server" ImageUrl=<%# Eval("Logo") %> />
                                <asp:Image ID="ImageProjectClientContract" runat="server" ImageUrl=<%# Eval("ProjectClientContractLogo") %> Visible="false" />
                            </td>
                            <td style="padding:5px; width:220px; vertical-align:top;">
                                <asp:Panel CssClass="pnl_newsup" ID="panelNewSupplier" runat="server" Visible="false">
                                    <%= BodyTexts.Ref("qcBhmdE2a0efLQGscCaCiQ")%>
                                </asp:Panel>

                                <span style="font-size:initial; color:#ABB2B9;">
                                    <asp:Literal ID="LiteralContOrAddnNo" runat="server" Text='<%# Bind("ContOrAddnNo")%>'></asp:Literal>
                                </span>
                                <br />
                                <span style="font-size:medium; color:#B03A2E;">
                                    <asp:Literal ID="LiteralValueWithVAT" runat="server" Text='<%# Bind("Value_WithVAT", "{0:N2}")%>'></asp:Literal>
                                    <asp:Literal ID="LiteralCurrency" runat="server" Text='<%# Bind("Currency")%>'></asp:Literal>
                                </span>
                                <br />
                                <asp:HyperLink ID="HyperlinkLink" runat="server" Target="_blank" ForeColor="#3498DB" Font-Size="Medium">
                                           See Details
                                </asp:HyperLink>

                            </td>
                            <td style="padding:5px; font-size:12px; width:250px;">
                                <asp:Literal ID="LiteralDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Literal>
                            </td>
                        </tr>
                    </table>

                        <div class="hide">
                            <span id="sp_contractid" class="hide"><asp:Literal ID="LiteralContractID" runat="server" Text='<%# Bind("ContractID") %>' ></asp:Literal></span>
                            <span id="sp_addendumid" class="hide"><asp:Literal ID="LiteralAddendumID" runat="server" Text='<%# Bind("AddendumID") %>' ></asp:Literal></span>
                            <asp:HyperLink ID="HyperlinkPO" runat="server" Target="_blank" ForeColor="Blue"></asp:HyperLink>

    <%--                        <asp:Literal ID="LiteralBudget" runat="server" Text='<%# Bind("Budget", "{0:N2}")%>'></asp:Literal>--%>
                            <asp:Literal ID="LiteralReplaceBlock" runat="server"></asp:Literal>

                            <asp:Literal ID="LiteralCreatedBy" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Literal>

                        </div>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="79px" ItemStyle-HorizontalAlign="Center" >
                    <HeaderTemplate>
                        <header>
                            <%= BodyTexts.Ref("uuxYQefN/UiHiCi5zKu/OA")%>
                        </header>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table id="tblR5" runat="server" Class="table table-nonfluid min100">
                            <tr>
                                <td class="style1">
                                    <asp:Literal ID="LiteralUserRank5" runat="server" Text='<%# Bind("UserRank5") %>'></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:ImageButton ID="ImageButtonApprovalUserRank5" runat="server" CommandName="ApprovalUserRank5"
                                        CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Literal ID="LiteralWhenApprovedUserRank5" runat="server" Text="When Approved"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:LinkButton ID="LinkButtonRemarkUserRank5" runat="server"
                                        CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="RemarkUserRank5">Add Comment</asp:LinkButton>

                                    <%--<asp:Label ID="LabelNotificationNumberUserRank5" runat="server" Text="2" CssClass="LabelNotification"></asp:Label>--%>

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="79px" ItemStyle-HorizontalAlign="Center" >
                    <HeaderTemplate>
                        <header>
                            <%= BodyTexts.Ref("6a2DH6/KU0isLXkrPqZC0g")%>
                        </header>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table id="tblR10" runat="server" Class="table table-nonfluid min100">
                            <tr>
                                <td class="style1">
                                    <asp:Literal ID="LiteralUserRank10" runat="server" Text='<%# Bind("UserRank10") %>'></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:ImageButton ID="ImageButtonApprovalUserRank10" runat="server" CommandName="ApprovalUserRank10"
                                        CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Literal ID="LiteralWhenApprovedUserRank10" runat="server" Text="When Approved"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:LinkButton ID="LinkButtonRemarkUserRank10" runat="server"
                                        CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="RemarkUserRank10">Add Comment</asp:LinkButton>

                                    <%--                                            <asp:Label ID="LabelNotificationNumberUserRank10" runat="server" Text="2" CssClass="LabelNotification"></asp:Label>--%>

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="79px" ItemStyle-HorizontalAlign="Center" >
                    <HeaderTemplate>
                        <header>
                            <%= BodyTexts.Ref("4k1FiWiVPEyPFC3g2CjVpA")%>
                        </header>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <table id="tblR20" runat="server" Class="table table-nonfluid min100">
                            <tr>
                                <td class="style1">
                                    <asp:Literal ID="LiteralUserRank20" runat="server" Text='<%# Bind("UserRank20") %>'></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:ImageButton ID="ImageButtonApprovalUserRank20" runat="server" CommandName="ApprovalUserRank20"
                                        CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Literal ID="LiteralWhenApprovedUserRank20" runat="server" Text="When Approved"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:LinkButton ID="LinkButtonRemarkUserRank20" runat="server"
                                        CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="RemarkUserRank20">Add Comment</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="79px" ItemStyle-HorizontalAlign="Center" >
                    <HeaderTemplate>
                        <header>
                            <%= BodyTexts.Ref("y/s5vl4TP0SxC1Rc4+QREQ")%>
                        </header>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table id="tblR30" runat="server" Class="table table-nonfluid min100">
                            <tr>
                                <td class="style1">
                                    <span class="hidepanel">
                                        <asp:Literal ID="LiteralUserRank30" runat="server" Text='<%# Bind("UserRank30") %>'></asp:Literal></span>
                                    <asp:Literal ID="LiteralShowLawyersToClient" runat="server" Text="lawyers"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:ImageButton ID="ImageButtonApprovalUserRank30" runat="server" CommandName="ApprovalUserRank30"
                                        CommandArgument='<%#Container.DataItemIndex%>' CausesValidation="False" />
                                    <asp:LinkButton ID="LinkButtonRejectContractGirls" runat="server" CommandName="ApprovalByRejecting"
                                        CommandArgument='<%#Container.DataItemIndex%>' CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Literal ID="LiteralWhenApprovedUserRank30" runat="server" Text="When Approved"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:LinkButton ID="LinkButtonRemarkUserRank30" runat="server"
                                        CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="RemarkUserRank30">Add Comment</asp:LinkButton>

                                    <%--                                            <asp:Label ID="LabelNotificationNumberUserRank30" runat="server" Text="2" CssClass="LabelNotification"></asp:Label>--%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="79px" ItemStyle-HorizontalAlign="Center" >
                    <HeaderTemplate>
                        <header>
                            <%= BodyTexts.Ref("m+/LFWMEs0i3cpPEFRp/tw")%>
                        </header>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table id="tblR40" runat="server" Class="table table-nonfluid min100">
                            <tr>
                                <td class="style1">
                                    <asp:Literal ID="LiteralUserRank40" runat="server" Text='<%# Bind("UserRank40") %>'></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:ImageButton ID="ImageButtonApprovalUserRank40" runat="server" CommandName="ApprovalUserRank40"
                                        CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Literal ID="LiteralWhenApprovedUserRank40" runat="server" Text="When Approved"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:LinkButton ID="LinkButtonRemarkUserRank40" runat="server"
                                        CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="RemarkUserRank40">Add Comment</asp:LinkButton>

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="79px" ItemStyle-HorizontalAlign="Center" Visible="true" >
                    <HeaderTemplate>
                        <header>
                            <%= BodyTexts.Ref("UFU4S9GLlUGj3SzKuK2FMA")%>
                        </header>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table id="tblR50" runat="server" Class="table table-nonfluid min100">
                            <tr>
                                <td class="style1">
                                    <asp:Literal ID="LiteralUserRank50" runat="server" Text='<%# Bind("UserRank50") %>'></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:ImageButton ID="ImageButtonApprovalUserRank50" runat="server" CommandName="ApprovalUserRank50"
                                        CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Literal ID="LiteralWhenApprovedUserRank50" runat="server" Text="When Approved"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:LinkButton ID="LinkButtonRemarkUserRank50" runat="server"
                                        CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="RemarkUserRank50">Add Comment</asp:LinkButton>

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="79px" ItemStyle-HorizontalAlign="Center" >
                    <HeaderTemplate>
                        <header>
                            <%= BodyTexts.Ref("wY+czOVPL026uC9rcR4RgA")%>
                        </header>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table id="tblR60" runat="server" Class="table table-nonfluid min100">
                            <tr>
                                <td class="style1">
                                    <asp:Literal ID="LiteralUserRank60" runat="server" Text='<%# Bind("UserRank60") %>'></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:ImageButton ID="ImageButtonApprovalUserRank60" runat="server" CommandName="ApprovalUserRank60"
                                        CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Literal ID="LiteralWhenApprovedUserRank60" runat="server" Text="When Approved"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:LinkButton ID="LinkButtonRemarkUserRank60" runat="server"
                                        CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="RemarkUserRank60">Add Comment</asp:LinkButton>

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="79px" ItemStyle-HorizontalAlign="Center" >
                    <HeaderTemplate>
                        <header>
                            <%= BodyTexts.Ref("H8JQMoO6nkSbZQjWR8vNvw")%>
                        </header>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table id="tblR70" runat="server" Class="table table-nonfluid min100">
                            <tr>
                                <td class="style1">
                                    <asp:Literal ID="LiteralUserRank70" runat="server" Text='<%# Bind("UserRank70") %>'></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:ImageButton ID="ImageButtonApprovalUserRank70" runat="server" CommandName="ApprovalUserRank70"
                                        CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Literal ID="LiteralWhenApprovedUserRank70" runat="server" Text="When Approved"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:LinkButton ID="LinkButtonRemarkUserRank70" runat="server"
                                        CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="RemarkUserRank70">Add Comment</asp:LinkButton>

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="79px" ItemStyle-HorizontalAlign="Center"  > 
                    <HeaderTemplate>
                        <header>
                            <%= BodyTexts.Ref("cLq2IAIJkkOf/Lj94vCvwA")%>
                        </header>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table id="tblR80" runat="server" Class="table table-nonfluid min100">
                            <tr>
                                <td class="style1">
                                    <asp:Literal ID="LiteralUserRank80" runat="server" Text='<%# Bind("UserRank80") %>'></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:ImageButton ID="ImageButtonApprovalUserRank80" runat="server" CommandName="ApprovalUserRank80"
                                        CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Literal ID="LiteralWhenApprovedUserRank80" runat="server" Text="When Approved"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:LinkButton ID="LinkButtonRemarkUserRank80" runat="server"
                                        CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="RemarkUserRank80">Add Comment</asp:LinkButton>

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />
        </asp:GridView>

                          <asp:SqlDataSource ID="SqlDataSourceNotApprovedContractsOrAddendums" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand="[ApprMx].[SP_ApprovalMatrixGetData]" 
                            SelectCommandType="StoredProcedure">

                             <SelectParameters>
                              <asp:Parameter Name="UserName" DefaultValue="-" Type="String" />
                              <asp:Parameter Name="FilterType" DefaultValue="Show Only My Items" Type="String" />
                              <asp:ControlParameter  Type="Int32" ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" />
                             </SelectParameters>

                           </asp:SqlDataSource>

</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/knockout/3.4.2/knockout-debug.js"></script>

    <script src="/Scripts/KnockoutScripts/userCommentsApprMatrx.js"></script>

    <script type="text/javascript" src="http://cdn.jsdelivr.net/json2/0.1/json2.js"></script>

    <script type="text/javascript">
        $(function () {

            $("input[name*='ImageButtonApprovalUserRank']").on('click', function (e) {

                if (confirm('Are you sure to approve ?')) {

                    var _clickedItem = $(this)

                    var ApprovalParameters = {};
                    ApprovalParameters.UserName = _clickedItem.closest('tr').prev('tr').find('td:eq(0)').text().trim();
                    ApprovalParameters.ContractId = _clickedItem.closest('table').closest('td').closest('tr').find(('#sp_contractid')).text().trim();
                    ApprovalParameters.AddendumId = _clickedItem.closest('table').closest('td').closest('tr').find(('#sp_addendumid')).text().trim();

                    $.ajax({
                        type: "POST",
                        url: '<%= ResolveUrl("~/Pages/PTS/PageMethods/ApprovalMatrix.aspx/Approve")%>',
                        data: '{ApprovalParameters: ' + JSON.stringify(ApprovalParameters) + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {

                            _clickedItem.closest('table').closest('td').closest('tr').fadeOut("slow", function () {
                                $.gritter.add({
                                    title: 'Approved',
                                    text: 'Successfully Approved',
                                    time: '2000',
                                    class_name: 'gritter-info gritter-top-center'
                                })
                            });
                            //alert("User has been added successfully.");
                            //window.location.reload();
                        }
                    });

                }

                e.preventDefault();
                return false;
            })
        })
    </script>

</asp:Content>

