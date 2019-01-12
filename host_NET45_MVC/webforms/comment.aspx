<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="comment.aspx.vb" Inherits="comment" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
  TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <style type="text/css">
    .TextBoxRemoveVerticalBar
    {}
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  
  <div>
   <asp:TextBox ID="textbox1" runat="server"></asp:TextBox>
   <asp:TextBox ID="textbox2" runat="server"></asp:TextBox>
   <asp:TextBox ID="textbox3" runat="server"></asp:TextBox>
   <asp:TextBox ID="textbox4" runat="server"></asp:TextBox>
   <asp:Button ID="buttonRedirect" runat="server" Text= "Redirect" />
  </div>

  <div>

<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
 TargetControlID="lnkPopup"
 PopupControlID="panEdit"
 BackgroundCssClass="modalBackground"
 CancelControlID="btnCancel"
 PopupDragHandleControlID="panEdit" >
</asp:ModalPopupExtender>

 

<asp:Panel ID="panEdit" runat="server" Height="180px" Width="400px" CssClass="ModalWindow">
        <h1>Edit</h1>
        <table width="100%">
            <tr>
                <td class="formtext" style="height: 23px; width: 150px;" align="left">
                    Fields1:
                </td>
                <td>
                    <asp:TextBox ID="txtFields1" runat="server"></asp:TextBox>
                </td>
            </tr>           
       </table>
       <br />
       <asp:Button ID="Button1" runat="server" Text="Update" />
       <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</asp:Panel>
<a id="lnkPopup" runat="server">Show Popup</a>

  </div>











  <asp:Label ID="Label1" 
            runat="server"  
            Text="Comments"  
            Font-Size="XX-Large"  
            Font-Names="Comic Sans MS">
   </asp:Label>

  <asp:HoverMenuExtender ID="HoverMenuExtender1"  
            runat="server"  
            TargetControlID="Label1"  
            PopupControlID="Panel1"  
            PopupPosition="Bottom">
  </asp:HoverMenuExtender>

  <asp:Panel    
            ID="Panel1"  
            runat="server"  
            Width="400"  
            BorderColor="Gray"
            BorderWidth="1px"  BackColor="White">  

            <div style="margin: 5px; text-align: center">
               <asp:TextBox ID="textBoxComments" runat="server" TextMode="MultiLine" 
                  Height="178px" Width="388px" CssClass="TextBoxRemoveVerticalBar" 
                 BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" ></asp:TextBox>
            </div>


            <div style="margin: 5px; text-align: center">
               <asp:Button ID="ButtonSendComment" runat="server" Text="Submit" 
                 Font-Size="10px" />
            </div>
  </asp:Panel>

 <asp:ModalPopupExtender ID="ModalPopupExtenderSent" runat="server"
 TargetControlID="lnkPopup"
 PopupControlID="PanelSent"
 BackgroundCssClass="modalBackground">
 </asp:ModalPopupExtender>

<asp:Panel ID="PanelSent" runat="server"  
    BackColor="White">
        <h1 style="color: #0000FF; margin: 10px">...has been sent!</h1>
</asp:Panel>

    <asp:Timer ID="TimerMessageSent" interval="700" runat="server" Enabled="False"></asp:Timer>














                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                 DataKeyNames="CommentID" DataSourceID="SqlDataSource1" 
                 EnableModelValidation="True">
                 <Columns>
                   <asp:BoundField DataField="CommentID" HeaderText="CommentID" 
                     InsertVisible="False" ReadOnly="True" SortExpression="CommentID" />
                   <asp:BoundField DataField="UserName" HeaderText="UserName" 
                     SortExpression="UserName" />

                  <asp:TemplateField ItemStyle-Width="600px" HeaderStyle-Width="600px">
                  <ItemTemplate>
                      <%# DataBinder.Eval(Container.DataItem, "Comment").Replace("\n", "<br/>")%>
                  </ItemTemplate>
                  </asp:TemplateField>


                   <asp:BoundField DataField="SentBy" HeaderText="SentBy" 
                     SortExpression="SentBy" />
                 </Columns>
               </asp:GridView>
               <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                 ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                 SelectCommand="SELECT TOP 5 [CommentID], [UserName], [Comment], [SentBy] FROM [Table_UserComments] ORDER BY CommentID DESC">
               </asp:SqlDataSource>


</asp:Content>

