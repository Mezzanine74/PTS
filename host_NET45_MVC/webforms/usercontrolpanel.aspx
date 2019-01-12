<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="usercontrolpanel.aspx.vb" Inherits="usercontrolpanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <br /> <br />
<table>
 <tr>
  <td>
  <asp:FormView ID="FormViewSendEmail" runat="server" 
    DataSourceID="SqlDataSourceSendEmail" EnableModelValidation="True" 
    DefaultMode="Edit">
    <EditItemTemplate>
    <table style="font-size: 10px; font-weight: bold">
       <tr>
        <td align="center" 
               style="background-color: #DCDCDC; height: 15px; font-size: 10px;" colspan="2">
           Email Notification Settings
        </td>
       </tr>        
      <tr>
       <td>
        I request to receive email notifications after new payment request drops to Pending List:
       </td>
       <td>
        <asp:CheckBox ID="SendEmailPaymentReqCheckBox" runat="server" 
          Checked='<%# Bind("SendEmailPaymentReq") %>' 
               oncheckedchanged="SendEmailPaymentReqCheckBox_CheckedChanged" 
               AutoPostBack="True" />
       </td>
      </tr>
      <tr>
       <td>
        I request to receive email notifications after any action on PendingApproval page:
       </td>
       <td>
        <asp:CheckBox ID="SendEmailApprovalCheckBox" runat="server" 
          Checked='<%# Bind("SendEmailApproval") %>' AutoPostBack="True" 
               oncheckedchanged="SendEmailApprovalCheckBox_CheckedChanged" />
       </td>
      </tr>
      <tr>
       <td>
        I request to receive email notifications on Contracts and Addendums page:
       </td>
       <td>
        <asp:CheckBox ID="SendEmailContractCheckBox" runat="server" 
          Checked='<%# Bind("SendEmailContract") %>' AutoPostBack="True" oncheckedchanged="SendEmailContractCheckBox_CheckedChanged" 
               />
       </td>
      </tr>
      <tr>
       <td>
        I request to receive email notifications for Payment List:
       </td>
       <td>
        <asp:CheckBox ID="SendEmailPaymentListCheckBox" runat="server" 
          Checked='<%# Bind("SendEmailPayments") %>' AutoPostBack="True" oncheckedchanged="SendEmailPaymentListCheckBox_CheckedChanged" 
               />
       </td>
      </tr>
    </table>
    </EditItemTemplate>
  </asp:FormView>  
  </td>
 </tr>
 <tr>
  <td style="text-align: right">
    <asp:Label ID="labelUpdateMessage" runat="server"  Visible="false"  Text="updated..."
        Font-Size="10px" Font-Italic="True" Font-Bold="True" ForeColor="#009900">
    </asp:Label>
  </td>
 </tr> 
</table>

  <br /><br /><br />
  <asp:SqlDataSource ID="SqlDataSourceSendEmail" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
    SelectCommand="SELECT [UserName]
      ,[SendEmailPaymentReq]
      ,[SendEmailApproval]
      ,(case when [SendEmailContract] is null then 0 else [SendEmailContract] end ) AS [SendEmailContract]
      ,SendEmailPayments
  FROM [aspnet_Users]
   WHERE UserName = @UserName"
  UpdateCommand = "UPDATE [aspnet_Users]
   SET [SendEmailPaymentReq] = @SendEmailPaymentReq
      ,[SendEmailApproval] = @SendEmailApproval
      ,[SendEmailContract] = @SendEmailContract
      ,[SendEmailPayments] = @SendEmailPayments
        WHERE UserName = @UserName">
      <SelectParameters>
       <asp:Parameter Name="UserName" Type="String"/>
      </SelectParameters>
      <UpdateParameters>
       <asp:Parameter Name="UserName" Type="String"/>
       <asp:Parameter Name="SendEmailPaymentReq" />
       <asp:Parameter Name="SendEmailApproval" />
       <asp:Parameter Name="SendEmailContract" />
       <asp:Parameter Name="SendEmailPayments" />
      </UpdateParameters>
   </asp:SqlDataSource>
  <asp:ChangePassword ID="ChangePassword1" runat="server" CssClass="LabelGeneral">
    <ChangePasswordTemplate>
      <table cellpadding="1" cellspacing="0" style="border-collapse:collapse; font-size: 10px;">
        <tr>
          <td>
            <table cellpadding="0">
              <tr>
                <td align="center" colspan="2" style="background-color: #DCDCDC; height: 15px;">
                  Change Your Password</td>
              </tr>
              <tr>
                <td align="right">
                  <asp:Label ID="CurrentPasswordLabel" runat="server" 
                    AssociatedControlID="CurrentPassword">Password:</asp:Label>
                </td>
                <td>
                  <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" CssClass="TextBoxGeneralRev"
                    Font-Size="10px"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" 
                    ControlToValidate="CurrentPassword" ErrorMessage="Password is required." 
                    ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                </td>
              </tr>
              <tr>
                <td align="right">
                  <asp:Label ID="NewPasswordLabel" runat="server" 
                    AssociatedControlID="NewPassword">New Password:</asp:Label>
                </td>
                <td>
                  <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" CssClass="TextBoxGeneralRev"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" 
                    ControlToValidate="NewPassword" ErrorMessage="New Password is required." 
                    ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                </td>
              </tr>
              <tr>
                <td align="right">
                  <asp:Label ID="ConfirmNewPasswordLabel" runat="server" 
                    AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                </td>
                <td>
                  <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" CssClass="TextBoxGeneralRev"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" 
                    ControlToValidate="ConfirmNewPassword" 
                    ErrorMessage="Confirm New Password is required." 
                    ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                </td>
              </tr>
              <tr>
                <td align="center" colspan="2">
                  <asp:CompareValidator ID="NewPasswordCompare" runat="server" 
                    ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                    Display="Dynamic" 
                    ErrorMessage="The Confirm New Password must match the New Password entry." 
                    ValidationGroup="ChangePassword1"></asp:CompareValidator>
                </td>
              </tr>
              <tr>
                <td align="center" colspan="2" style="color:Red;">
                  <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                </td>
              </tr>
              <tr>
                <td align="right">
                  <asp:Button ID="ChangePasswordPushButton" runat="server" 
                    CommandName="ChangePassword" Text="Change Password" 
                    ValidationGroup="ChangePassword1" CssClass="btn btn-mini"/>
                </td>
                <td>
                  <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" 
                    CommandName="Cancel" Text="Cancel" CssClass="btn btn-mini"  />
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </ChangePasswordTemplate>
  </asp:ChangePassword>

    <hr />

    <h5>AVATAR PHOTO</h5>

    <asp:Image ID="ImageAvatar" runat="server"/>

    <asp:FileUpload ID="FileUploadAvatar" runat="server" />
    <br />
    <asp:Button ID="Upload" runat="server" Text="Process My Photo" CssClass="btn btn-info btn-mini" OnClick="Upload_Click"/>

</asp:Content>

