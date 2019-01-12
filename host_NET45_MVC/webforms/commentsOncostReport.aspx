<%@ Page Title="" Language="VB" MaintainScrollPositionOnPostback="true" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="commentsOncostReport.aspx.vb" Inherits="commentsOncostReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <asp:Label ID="labelTest" runat="server" CssClass="hidepanel" ></asp:Label>

<table width=100% >
 <tr>
     <td width=100%>
       <table align=center>
         <tr>
           <td>
            <table>
              <tr>
                <td style="font-size: 10px; font-weight: bold">
                  <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-mini btn-default" ValidationGroup="Comment">Add your comment</asp:LinkButton>
                </td>
                <td>
                  <asp:TextBox ID="TextBoxCostCodeComments" runat="server" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Size="12px" Height="47px" 
                    TextMode="MultiLine" Width="447px" ValidationGroup="Comment"></asp:TextBox>
                </td>
                <td>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidatorCostCodeComments" 
                    runat="server" ControlToValidate="TextBoxCostCodeComments" Display="Dynamic" 
                    ErrorMessage="Required" Font-Size="10px" ValidationGroup="Comment"></asp:RequiredFieldValidator>
                </td>
              </tr>
            </table>
           </td>
         </tr>
         <tr>
           <td>
             <div style="text-align: center"><asp:Label ID="LabelDefinition" runat="server" 
                 Text="Label" Font-Bold="True" Font-Size="12px"></asp:Label></div>
            <asp:GridView ID="GridViewCostCodeComments" runat="server" AutoGenerateColumns="False" 
              DataKeyNames="CommentID" DataSourceID="SqlDataSourceCostCodeComments" 
              EnableModelValidation="True" EmptyDataText="No comment yet" 
              AllowPaging="True" Font-Size="11px">
              <AlternatingRowStyle BackColor="#EFEFEF" />
              <Columns>
                  <asp:TemplateField>
                  <ItemTemplate>
                      <asp:LinkButton ID="LinkButtonEdit" Runat="server" CssClass="btn btn-minier btn-success"
                      CommandName="Edit" Text="Edit" ></asp:LinkButton>
                      <asp:LinkButton ID="LinkButtonDelete" Runat="server" CssClass="btn btn-minier"
                      OnClientClick="return confirm('Are you sure you want to delete this record?');"
                      CommandName="Delete" Text="Delete" ></asp:LinkButton>
                  </ItemTemplate>
                  <EditItemTemplate>
                      <asp:LinkButton ID="LinkButton2" Runat="server" CssClass="btn btn-minier btn-success"
                      Text="Update"  CommandName="Update"></asp:LinkButton>
                      <asp:LinkButton ID="LinkButton4" Runat="server" CssClass="btn btn-minier"
                      Text="Cancel"  CommandName="Cancel"></asp:LinkButton>
                  </EditItemTemplate>
                  </asp:TemplateField>

                <asp:BoundField DataField="UserName" HeaderText="UserName"  ReadOnly ="true"
                  SortExpression="UserName" />
                  <asp:TemplateField HeaderText="Comment" SortExpression="Comment" HeaderStyle-Width="400px">
                    <EditItemTemplate>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Comment") %>' 
                      Width="350px" Height="200px" TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
                    </ItemTemplate>

          <HeaderStyle Width="400px"></HeaderStyle>
                  </asp:TemplateField>
              </Columns>
              <EmptyDataRowStyle BackColor="Red" ForeColor="White" />
              <HeaderStyle  CssClass="GridHeader" />
              <PagerSettings Position="TopAndBottom" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourceCostCodeComments" runat="server" 
              ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
              SelectCommand="SELECT [CommentID], [UserName], [Comment] FROM [Table_CommentsCostReport] WHERE ([BudgetID] = @BudgetID)
               ORDER BY CommentID DESC"
              InsertCommand="INSERT INTO [Table_CommentsCostReport]
                     ([BudgetID]
                     ,[UserName]
                     ,[Comment])
               VALUES
                     (@BudgetID
                     ,@UserName
                     ,@Comment)"
              UpdateCommand = " UPDATE Table_CommentsCostReport SET Comment = @Comment WHERE CommentID = @CommentID "
              DeleteCommand = " DELETE FROM Table_CommentsCostReport WHERE CommentID = @CommentID " >
              <SelectParameters>
                <asp:ControlParameter ControlID="labelTest" Name="BudgetID" PropertyName="Text" 
                  Type="Int32" />
              </SelectParameters>
              <InsertParameters>
                <asp:ControlParameter ControlID="labelTest" Name="BudgetID" PropertyName="Text" 
                  Type="Int32" />
                <asp:ControlParameter ControlID="TextBoxCostCodeComments" Name="Comment" PropertyName="Text" 
                  Type="String" />
                <asp:Parameter Name="UserName" Type="String" />
              </InsertParameters>
            </asp:SqlDataSource>
             <asp:DropDownList ID="DropDownListProjectName" runat="server" CssClass="hidepanel"
               DataSourceID="SqlDataSourceProjectName" DataTextField="ProjectName" 
               DataValueField="ProjectName">
             </asp:DropDownList>
             <asp:DropDownList ID="DropDownListCostDescription" runat="server"  CssClass="hidepanel"
               DataSourceID="SqlDataSourceCostDescription" DataTextField="CodeDescription" 
               DataValueField="CodeDescription">
             </asp:DropDownList>
             <asp:SqlDataSource ID="SqlDataSourceCostDescription" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
               SelectCommand="SELECT RTRIM([CodeDescription]) AS CodeDescription FROM [Table7_CostCode] WHERE ([CostCode] = @CostCode)">
               <SelectParameters>
                 <asp:QueryStringParameter Name="CostCode" QueryStringField="CostCode" 
                   Type="String" />
               </SelectParameters>
             </asp:SqlDataSource>
             <asp:SqlDataSource ID="SqlDataSourceProjectName" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
               SelectCommand="SELECT RTRIM([ProjectName]) AS ProjectName FROM [Table1_Project] WHERE ([ProjectID] = @ProjectID)">
               <SelectParameters>
                 <asp:QueryStringParameter Name="ProjectID" QueryStringField="ProjectID" 
                   Type="Int16" />
               </SelectParameters>
             </asp:SqlDataSource>
           </td>
         </tr>
        </table>
      </td>
 </tr>
</table>    
</asp:Content>

