<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WebUserControl_PopupMessage.ascx.vb" Inherits="WebUserControl_PopupMessage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

  <%-- MODAL POPUP COMMON --%>
  <asp:ModalPopupExtender ID="ModalPopupExtenderCommon" runat="server"
   TargetControlID="ButtonCommon"
   PopupControlID="PanelCommon"
   BackgroundCssClass="modalBackground"
   CancelControlID="btnCancelCommon"
   PopupDragHandleControlID="PanelCommon" >
  </asp:ModalPopupExtender>
  <asp:Panel ID="PanelCommon" runat="server" Style="display:none;" >
     <div style="text-align: right">
                <asp:Button ID="btnCancelCommon" runat="server" Text="X" 
                 Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066" 
                 OnClientClick="changeClass" />
     </div>
      
    <asp:Label ID="labelMessage" runat="server" Text=""></asp:Label>

  </asp:Panel>
  <asp:Button id="ButtonCommon"  runat="server" CssClass="hidepanel"/>
  <%-- /MODAL POPUP COMMON --%>
