<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="Error.aspx.vb" Inherits="_Error2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

			<span style="color: #ff0000; font-size:30px;">Error Occured ):</span>
      <br /><br />
		<p style="font-size:14px;">
			Sorry for interruption. The copy of error information has been already sent to Admin via email.</p></

<div>
    
<br />

<asp:Panel ID="pnlClick" runat="server" >

    <div style="background-color:GhostWhite; height:30px; vertical-align:middle;">

    <div style="float:left; color:Black;padding:5px 5px 0 0">
      <asp:Label ID="lblMessage" runat="server" Text="Label"/>
      <asp:Image ID="imgArrows" runat="server" />
    </div>

    <div style="clear:both"></div>

    </div>

</asp:Panel>

<asp:Panel ID="pnlCollapsable" runat="server"  >
    <asp:Literal ID="LiteralErrorMessage" runat="server" ></asp:Literal>
</asp:Panel>

<asp:CollapsiblePanelExtender
    ID="CollapsiblePanelExtender1"
    runat="server"
    CollapseControlID="pnlClick"
    Collapsed="true"
    ExpandControlID="pnlClick"
    TextLabelID="lblMessage"
    CollapsedText="Show Details"
    ExpandedText="Hide Details"
    ImageControlID="imgArrows"
    CollapsedImage="~/images/downarrow.jpg"
    ExpandedImage="~/images/uparrow.jpg"
    ExpandDirection="Vertical"
    TargetControlID="pnlCollapsable"
    ScrollContents="false">
</asp:CollapsiblePanelExtender>
</div>


</asp:Content>

