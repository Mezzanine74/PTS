<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="EditSupplier.aspx.vb" Inherits="EditSupplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Edit Supplier</title>
    <script type="text/javascript">

        function pageLoad() {
        }

        function SetAutoCompleteWidth(source, EventArgs) {
            var target
            target = ((document.getElementBy) ? document.getElementById("AutoCompleteDiv") : document.all.AutoCompleteDiv);
            target.style.width = '500px';
        }

       </script>

    <link href="Styles.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:TextBox ID="TextBoxSupplierIDOuter" runat="server"
        Width="500px" AutoPostBack="True" Placeholder="Enter INN number or some text from Supplier Name"></asp:TextBox>

                        <div ID="AutoCompleteDiv" class="TextBoxGeneral" 
                            >
                        </div>    
                                  
                        <asp:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" 
                            CompletionInterval="0" CompletionListElementID="AutoCompleteDiv" 
                            CompletionSetCount="12" MinimumPrefixLength="0" 
                            ServiceMethod="SupplierEdit" onclientshown="SetAutoCompleteWidth" 
                            ServicePath="AutoComplete.asmx" TargetControlID="TextBoxSupplierIDOuter">
                        </asp:AutoCompleteExtender>
                    <br /> <br />
                    
                            <asp:GridView ID="GridViewEditSupplier" runat="server" AutoGenerateColumns="False" 
                                DataSourceID="SqlDataSourceEditSupplier"  CssClass="Grid" DataKeyNames="SupplierID"
                                AllowPaging="True" >
                                <Columns>

                                        <asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                                            <EditItemTemplate>
                                                <div class="btn-group-vertical" role="group" >
                                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CssClass="btn btn-minier btn-primary"
                                                        CommandName="Update" Text="Update"></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
                                                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                                </div>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <div class="btn-group-vertical" role="group">
                                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
                                                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButtonDelete" runat="server" CssClass="btn btn-minier btn-danger"
                                                        OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                        CommandName="Delete" Text="Delete"></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
        
                                    <asp:TemplateField HeaderText="SupplierID" SortExpression="SupplierID" >
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("SupplierID") %>' CssClass="LabelGeneral"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div>
                                            <font style="font-size:10px;">IP/&#1048;&#1055; : </font>
                                                <asp:CheckBox ID="CheckBoxPersonSupplier" runat="server" OnCheckedChanged="CheckBoxPersonSupplier_CheckedChanged"  AutoPostBack="true" />
                                            <asp:TextBox ID="TextBoxSupplierID" runat="server" Text='<%# Bind("SupplierID") %>' CssClass="TextBoxGeneralRev"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="TextBoxSupplierID_FilteredTextBoxExtender" 
                                                runat="server" FilterType="Numbers" TargetControlID="TextBoxSupplierID">
                                            </asp:FilteredTextBoxExtender>
                                            </div>
                                            <div>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorSupplierEnter" 
                                                runat="server" ControlToValidate="TextBoxSupplierID" CssClass="LabelGeneral" 
                                                SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>
                                             </div>
                                            <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSupplierID"  Display="Dynamic" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxSupplierID"></asp:RequiredFieldValidator>     
                                            </div>                                       
                                        </EditItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("SupplierName") %>' CssClass="LabelGeneral"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                         <div>
                                            <asp:TextBox ID="TextBoxSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'  CssClass="TextBoxGeneralRev"></asp:TextBox>
                                         </div>
                                            <br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorSupplierName" runat="server"  Display="Dynamic"
                                                ErrorMessage="Only latin letters allowed without any special characters" ControlToValidate="TextBoxSupplierName"
                                                CssClass="LabelGeneral" ValidationExpression="^[0-9a-zA-Z ]+$" > 
                                            </asp:RegularExpressionValidator>
                                         <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSupplierName"  Display="Dynamic" runat="server" 
                                                ErrorMessage="Required" ControlToValidate="TextBoxSupplierName"></asp:RequiredFieldValidator>     
                                        </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT_Free" SortExpression="VAT_Free">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("VAT_Free") %>' 
                                                Enabled="false" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("VAT_Free") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PhoneNumber" SortExpression="PhoneNumber1" ControlStyle-Width="65" HeaderStyle-Width="65">
                                        <ItemTemplate>
                                            <table >
                                                <tr >
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("PhoneNumber1") %>' CssClass="LabelGeneral"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("PhoneNumber2") %>' CssClass="LabelGeneral"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <table >
                                                <tr >
                                                    <td>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("PhoneNumber1") %>' CssClass="TextBoxGeneralRev"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("PhoneNumber2") %>' CssClass="TextBoxGeneralRev"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>

                                        </EditItemTemplate>

<ControlStyle Width="65px"></ControlStyle>

<HeaderStyle Width="65px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FaxNumber" SortExpression="FaxNumber1" ControlStyle-Width="65" HeaderStyle-Width="65">
                                        <ItemTemplate>
                                            <table >
                                                <tr >
                                                    <td>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("FaxNumber1") %>' CssClass="LabelGeneral"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                     <asp:Label ID="Label7" runat="server" Text='<%# Bind("FaxNumber2") %>' CssClass="LabelGeneral"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <table >
                                                <tr >
                                                    <td>
                                               <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("FaxNumber1") %>' CssClass="TextBoxGeneralRev"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                               <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("FaxNumber2") %>' CssClass="TextBoxGeneralRev"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EditItemTemplate>

<ControlStyle Width="65px"></ControlStyle>

<HeaderStyle Width="65px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WebAdress" SortExpression="WebAdress">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("WebAdress") %>' CssClass="LabelGeneral"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("WebAdress") %>' CssClass="TextBoxGeneralRev" ></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EmailAddress" SortExpression="EmailAddress">
                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("EmailAddress") %>' CssClass="LabelGeneral"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("EmailAddress") %>' CssClass="TextBoxGeneralRev"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Adress" SortExpression="Adress">
                                        <ItemTemplate>
                                            <asp:Label ID="Label10" runat="server" Text='<%# Bind("Adress") %>' CssClass="LabelGeneral"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("Adress") %>' CssClass="TextBoxGeneralRev" TextMode="MultiLine" Height="80"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <RowStyle  CssClass="GridItemNakladnaya" />
                                <HeaderStyle  CssClass="GridHeader" />
                                 <PagerStyle CssClass="pager" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSourceEditSupplier" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SELECT [Idname], [SupplierID], RTRIM([SupplierName]) AS [SupplierName], RTRIM([PhoneNumber1]) AS [PhoneNumber1], RTRIM([PhoneNumber2]) AS [PhoneNumber2], RTRIM([FaxNumber1]) AS [FaxNumber1] , RTRIM([FaxNumber2]) AS [FaxNumber2], RTRIM([WebAdress]) AS [WebAdress], RTRIM([EmailAddress]) AS [EmailAddress], RTRIM([Adress]) AS [Adress], [VAT_Free] FROM [View_EditSupplier] WHERE IDname=@IDname"
                                UpdateCommand="UPDATE Table6_Supplier SET SupplierName = @SupplierName, PhoneNumber1 = @PhoneNumber1, PhoneNumber2 = @PhoneNumber2, FaxNumber1 = @FaxNumber1, FaxNumber2 = @FaxNumber2, WebAdress = @WebAdress, EmailAddress = @EmailAddress, Adress = @Adress, VAT_Free = @VAT_Free WHERE SupplierID = @SupplierID"
                                DeleteCommand="DELETE FROM [View_EditSupplier] WHERE SupplierID=@SupplierID "> 
                                     <SelectParameters>
                                        <asp:ControlParameter ControlID="TextBoxSupplierIDOuter" DefaultValue="0" 
                                            Name="IDname" PropertyName="Text" Type="String" />
                                     </SelectParameters>
  
                            </asp:SqlDataSource>    
                        
</asp:Content>

