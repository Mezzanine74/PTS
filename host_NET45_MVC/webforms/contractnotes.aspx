<%@ Page Title="" culture="ru-RU" uiCulture="ru-RU" MaintainScrollPositionOnPostback="true"
Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="contractnotes.aspx.vb" Inherits="contractNotes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
  TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


  <asp:Label ID="LabelKeepProjectID" runat="server" CssClass="hidepanel" ></asp:Label>
  <asp:Label ID="LabelFormViewVisibleStatus" runat="server" Text="0"  CssClass="hidepanel"></asp:Label>

<table>
 <tr>
  <td>

  <td/>
  <td>
        <asp:ImageButton ID="ImageButtonAddProject" runat="server" 
          ImageUrl="~/Images/insert.png" CausesValidation="False" />
  </td>
  <td>
  <asp:FormView ID="FormViewInsertProject" runat="server" Visible= "false"
    DataSourceID="SqlDataSourceInsertProject" EnableModelValidation="True" DefaultMode="Insert">
    <InsertItemTemplate>
     <asp:Panel runat="server" ID="pnlLogin" DefaultButton="InsertButton">
        <table>
          <tr>
          <td style="width: 200px; text-align: center;">
                <asp:Label ID="LabelSupplierName" runat="server" Text="Supplier Name" CssClass="LabelGeneral" ></asp:Label>
          </td>
          <td style="width: 200px; text-align: center;">
                <asp:Label ID="LabelSupplierID" runat="server" Text="Supplier ID" CssClass="LabelGeneral" ></asp:Label>
          </td>
          <td style="width: 200px; text-align: center;">
                <asp:Label ID="LabelDescription" runat="server" Text="Description" CssClass="LabelGeneral" ></asp:Label>
          </td>
          <td style="width: 200px; text-align: center;">
                <asp:Label ID="LabelNotes" runat="server" Text="Notes" CssClass="LabelGeneral" ></asp:Label>
          </td>
          <td>
          </td>
        </tr>
        <tr>
          <td style="text-align: center; background-color: #F5F5F5;">
            <asp:TextBox ID="TextBoxSupplierName" runat="server" Width="150px"
              Text='<%# Bind("SupplierName") %>' />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="!" ControlToValidate="TextBoxSupplierName" Display="Dynamic"></asp:RequiredFieldValidator>
          </td>
          <td style="text-align: center; background-color: #F5F5F5;">
            <asp:TextBox ID="TextBoxSupplierID" runat="server"  Width="150px"
              Text='<%# Bind("SupplierID") %>' />
          </td>
          <td style="text-align: center; background-color: #F5F5F5;">
            <asp:TextBox ID="TextBoxDescription" runat="server"  Width="150px"
              Text='<%# Bind("Description") %>' />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="!" ControlToValidate="TextBoxDescription" Display="Dynamic"></asp:RequiredFieldValidator>
          </td>
          <td style="text-align: center; background-color: #F5F5F5;">
            <asp:TextBox ID="TextBoxNotes" runat="server"  Width="150px"
              Text='<%# Bind("Notes") %>' />

          </td>
          <td>
             <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
              CommandName="Insert" Text="Insert" CssClass="ButtonEdit" onmouseover="this.style.cursor='hand'" />
          </td>
        </tr>
        </table>
     </asp:Panel>
    </InsertItemTemplate>
  </asp:FormView>
  <asp:SqlDataSource ID="SqlDataSourceInsertProject" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    InsertCommand="INSERT INTO [Table_ContractNotes]
           ([SupplierName]
           ,[SupplierID]
           ,[Description]
           ,[Notes])
     VALUES
           (@SupplierName
           ,@SupplierID
           ,@Description
           ,@Notes)" >
    <InsertParameters>
      <asp:Parameter Name="SupplierName" Type="String" />
      <asp:Parameter Name="SupplierID" Type="String" />
      <asp:Parameter Name="Description" Type="String" />
      <asp:Parameter Name="Notes" Type="String" />
    </InsertParameters>
  </asp:SqlDataSource>
 </td>
 </tr>
</table>

      <asp:Button Id="ButtonUpdate" runat="server" Text="Update" Visible="false" 
        BackColor="Lime" Font-Bold="True" Font-Size="12px" ForeColor="White"/>
      <cc1:Editor ID="EditorNotes" runat="server"
        Width="450px" Height="600px" Visible="false"/>

<table>
 <tr>
  <td style="vertical-align: top">


   <asp:ComboBox ID="ComboBoxSupplierName" runat="server" 
		DataSourceID="SqlDataSourceSupplierName" 
    DataTextField="SupplierName" 
		AutoCompleteMode="SuggestAppend"
		DataValueField="SupplierID" 
		MaxLength="0" 
    style="display: inline;" AutoPostBack="True" Font-Size="10px" Width="300px">
   </asp:ComboBox>

        <asp:SqlDataSource ID="SqlDataSourceSupplierName" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
            SelectCommand="SELECT NULL AS SupplierID, N' All Suppliers' AS SupplierName UNION ALL SELECT [SupplierName], [SupplierName] FROM [Table_ContractNotes] ORDER BY [SupplierName]">
        </asp:SqlDataSource>

  <asp:GridView ID="GridViewProjects" runat="server" AllowPaging="True" 
    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" PagerSettings-Position="TopAndBottom"
    DataSourceID="SqlDataSourceProjects" EnableModelValidation="True" 
    CssClass="Grid" PageSize="100">
    <Columns>
            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="400px">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="btn btn-mini"
                        CommandName="Update" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"  CssClass="btn btn-mini"
                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>

                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="btn btn-mini"
                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                    OnClientClick="return confirm('Are you sure you want to delete this record?');"
                    CommandName="Delete" Text="Delete" CssClass="btn btn-mini"></asp:LinkButton>

                </ItemTemplate>
            </asp:TemplateField>
      <asp:TemplateField HeaderText="Supplier Name" SortExpression="SupplierName" ControlStyle-Width="200" HeaderStyle-Width="200">
        <EditItemTemplate>
          <asp:TextBox ID="TextBoxSupplierNameEdit" runat="server" Text='<%# Bind("SupplierName") %>'></asp:TextBox>
        </EditItemTemplate>
        <ItemTemplate>
          <asp:Label ID="LabelSupplierNameItem" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Supplier ID" SortExpression="SupplierID" ControlStyle-Width="100" HeaderStyle-Width="100">
        <EditItemTemplate>
          <asp:TextBox ID="TextBoxSupplierIDEdit" runat="server" Text='<%# Bind("SupplierID") %>'></asp:TextBox>
        </EditItemTemplate>
        <ItemTemplate>
          <asp:Label ID="LabelSupplierIDItem" runat="server" Text='<%# Bind("SupplierID") %>'></asp:Label>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Description" SortExpression="Description" ControlStyle-Width="200" HeaderStyle-Width="200">
        <EditItemTemplate>
          <asp:TextBox ID="TextBoxDescriptionEdit" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
        </EditItemTemplate>
        <ItemTemplate>
          <asp:Label ID="LabelDescriptionItem" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Notes" SortExpression="Notes" ControlStyle-Width="200" HeaderStyle-Width="200">
        <EditItemTemplate>
          <asp:TextBox ID="TextBoxNotesEdit" runat="server" Text='<%# Bind("Notes") %>'></asp:TextBox>
        </EditItemTemplate>
        <ItemTemplate>
          <asp:Label ID="LabelNotesItem" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
        <pagerstyle  horizontalalign="Center" CssClass="pager" />
        <RowStyle  CssClass="GridItemNakladnaya" />
        <HeaderStyle  CssClass="GridHeader" />
        <PagerStyle CssClass="pager2" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceProjects" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT [Id]
      ,rtrim([SupplierName]) AS SupplierName
      ,rtrim([SupplierID]) AS SupplierID
      ,rtrim([Description]) AS Description
      ,rtrim([Notes]) AS Notes
  FROM [Table_ContractNotes]
  WHERE SupplierName LIKE N'%' + @SupplierName + N'%'"
  UpdateCommand = "UPDATE [Table_ContractNotes]
                   SET [SupplierName] = @SupplierName
                      ,[SupplierID] = @SupplierID
                      ,[Description] = @Description
                      ,[Notes] = @Notes
                 WHERE Id = @Id "
    DeleteCommand="DELETE FROM [Table_ContractNotes] WHERE Id = @Id">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ComboBoxSupplierName" Name="SupplierName" 
                                    PropertyName="SelectedValue" Type="String" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
  </asp:SqlDataSource>
  </td>
  <td style="vertical-align: top;">
<div id="DivNotes" runat="server" 
style="border: 1px solid #808080; padding: 2px; margin: 2px; vertical-align: top; background-color: #F8F8FF;">
<asp:Button ID="ButtonEditNotes" runat="server" Text="Edit" BackColor="Lime" 
    Font-Bold="True" Font-Size="12px" ForeColor="White" Visible="False" />
 <asp:Label ID="LabelNotes" runat="server" ></asp:Label>
</div>
  </td>
 </tr>
</table>

</asp:Content>

