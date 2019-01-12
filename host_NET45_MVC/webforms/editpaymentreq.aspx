<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="editpaymentreq.aspx.vb" Inherits="editpaymentreqFASTERXXXrandomZZ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Edit Payment Request</title>    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
                    <asp:ModalPopupExtender ID="ModalPopupExtenderDeleteWarning" runat="server"
                     TargetControlID="ButtonDeleteWarning"
                     PopupControlID="PanelDeleteWarning"
                     BackgroundCssClass="modalBackground"
                     CancelControlID="btnCancel"
                     PopupDragHandleControlID="PanelDeleteWarning" >
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="PanelDeleteWarning" runat="server" Style="display:none;" >
                                        <div style="text-align: right">
                                                         <asp:Button ID="Button1" runat="server" Text="X" 
                                                            Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066" 
                                                            OnClientClick="changeClass" />
                                        </div>
                                        <table style="background-color: #FFFFFF; padding: 10px; font-size: large; color: #333333">
                                         <tr>
                                          <td>
                                              <img alt="" src="~/images/alert.png" />                                           
                                          </td>
                                          <td>
                                                There is a <b style="color: #FF0000">payment</b> referring to this payment request.
                                                <br />
                                                In order to delete this item, related payment needs to be deleted from database
                                                <br />
                                                <font style="color:Red">However you are not authorized to perform this transaction.</font>
                                                <br />
                                                Please contact to Finance (<a href="mailto:julia.serik@mercuryeng.ru?Subject=Payment needs to be deleted from PTS">send email</a>).
                                          </td>
                                         </tr>
                                        </table>
                    </asp:Panel>
                    <asp:Button id="ButtonDeleteWarning"  runat="server" CssClass="hidepanel"/>


    <asp:Label ID="LabelLinkTransferFromCommandEvent" runat="server" Text="" CssClass="hidepanel"></asp:Label>

<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
 TargetControlID="lnkPopup"
 PopupControlID="panEdit"
 BackgroundCssClass="modalBackground"
 CancelControlID="btnCancel"
 PopupDragHandleControlID="panEdit" >
</asp:ModalPopupExtender>

<asp:Panel ID="panEdit" runat="server" 
          BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" 
          BackColor="White" Style="display:none;" >
        <h2 style="text-align: center; color: #FF0000;">Large File</h2>
        <div style="text-align: center; font-size: 12px; color: #333333; font-weight: bold;">You are uploading a pdf file which is bigger than 1.2 MB in size.</div>
        <div style="text-align: center; font-size: 12px; color: #333333; font-weight: bold;">Please study how to optimize PDF files in <a style="font-size: 14px;" href="http://pts.mercuryeng.ru/HOW_TO_REDUCE_PDF_SIZE.htm" target="_blank">this link</a> . It is very straightforward process.</div>
       <br />
       <table style="width: 100%">
        <tr>
         <td style="width: 50%; text-align: center;">
           <asp:Button ID="btnCancel" runat="server" Text="CLOSE" causesValidation = "false"
              Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="Red" />
         </td>
        </tr>
       </table>

</asp:Panel>
<a id="lnkPopup" runat="server"></a>


            <div class="btn-grp">

                    <asp:DropDownList ID="DropDownListPrj" runat="server" 
                        DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                        DataValueField="ProjectID" Width="267px" AutoPostBack="True" >
                    </asp:DropDownList>

                   <asp:DropDownList ID="DropDownListSupplier" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSourceSupplier" 
                        DataTextField="SupplierName" DataValueField="SupplierID" Width="267px">
                    </asp:DropDownList>

            </div>
                    <asp:SqlDataSource ID="SqlDataSourceSupplier" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="SELECT     TOP (100) PERCENT RTRIM(dbo.Table6_Supplier.SupplierID) AS SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, dbo.Table1_Project.ProjectID
FROM         dbo.Table6_Supplier INNER JOIN
                      dbo.Table2_PONo ON dbo.Table6_Supplier.SupplierID = dbo.Table2_PONo.SupplierID INNER JOIN
                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN
                      dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID
GROUP BY dbo.Table6_Supplier.SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName), dbo.Table1_Project.ProjectID
HAVING      (dbo.Table1_Project.ProjectID = @ProjectID)
ORDER BY SupplierName">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
    
    <br />
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        
        SelectCommand=" IF @UserName = N'dzera'
						BEGIN
							-- GIVE ACCESS TO DZERA FOR EACH PROJECT
							SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
												  AS ProjectName, dbo.aspnet_Users.UserName
							FROM         dbo.Table1_Project INNER JOIN
												  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
												  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId
							WHERE     (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.aspnet_Users.UserName = @UserName)
							ORDER BY ProjectName 
						END

						IF @UserName <> N'dzera'
						BEGIN
							SELECT * FROM (
							-- ONLY NEW GENERATION PROJECTS WHERE USER HAS ACCESS
                            SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
                                                  AS ProjectName, dbo.Table_Approval_UserRolePrjectJunction.UserName
                            FROM         dbo.Table1_Project INNER JOIN
                                                  dbo.Table_Approval_UserRolePrjectJunction ON dbo.Table1_Project.ProjectID = dbo.Table_Approval_UserRolePrjectJunction.ProjectID
                            WHERE     (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.Table_Approval_UserRolePrjectJunction.UserName = @UserName) AND 
                                                  (dbo.Table_Approval_UserRolePrjectJunction.RoleName = N'InitiateContractAndAddendum')

							UNION ALL

							-- ONLY NOT NEW GENERATION PROJECTS WHERE USER HAS ACCESS
							SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
												  AS ProjectName, dbo.aspnet_Users.UserName
							FROM         dbo.Table1_Project INNER JOIN
												  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
												  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId
							WHERE     (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table1_Project.NewGeneration = 0)

							UNION ALL

							-- THIS IS SPECIAFALLY DESIGNED FOR ROMAn HOWEVER IT MAY BE USED FOR ALL OTHERS
							-- IT GIVE ACCESS TO ORDINARY USERS FOR APPROVAL MATRIX MANIPULATION PAGES
							SELECT     dbo.Table_ApprUserExcpAccessToActiveUsers.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
												  AS ProjectName, dbo.Table_ApprUserExcpAccessToActiveUsers.UserName
							FROM         dbo.Table_ApprUserExcpAccessToActiveUsers INNER JOIN
												  dbo.Table1_Project ON dbo.Table_ApprUserExcpAccessToActiveUsers.ProjectID = dbo.Table1_Project.ProjectID
							WHERE     (dbo.Table_ApprUserExcpAccessToActiveUsers.UserName = @UserName)

							) AS Source
							ORDER BY ProjectName 
						END ">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:Label ID="Message" runat="server" CssClass="LabelMessage"></asp:Label>
    <asp:Label ID="LabelFileName" runat="server" CssClass="LabelGeneral"></asp:Label>


    <asp:GridView ID="GridViewEditPaymentReq" runat="server" AutoGenerateColumns="False" GridLines="None" 
        DataSourceID="SqlDataSourceEditPaymentReq" CssClass="table table-nonfluid table-hover"  AllowSorting="True"
         DataKeyNames="PayReqNo" AllowPaging="True" PageSize="20" PagerSettings-Position="TopAndBottom">
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
            
      <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" 
        SortExpression="PO_No" HeaderStyle-Width="100px" ItemStyle-Width="100px">
      </asp:BoundField>

      <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" 
        SortExpression="Description" HeaderStyle-Width="130px" ItemStyle-Width="130px">
      </asp:BoundField>

      <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ReadOnly="True" 
        SortExpression="SupplierName" HeaderStyle-Width="100px" ItemStyle-Width="100px">
      </asp:BoundField>

      <asp:BoundField DataField="Invoice_No" HeaderText="Invoice_No" ReadOnly="True" 
        SortExpression="Invoice_No" HeaderStyle-Width="80px" ItemStyle-Width="80px">
      </asp:BoundField>

      <asp:BoundField DataField="InvoiceValue" HeaderText="InvoiceValue Exc VAT" ReadOnly="True" 
        SortExpression="InvoiceValue" HeaderStyle-Width="100" ItemStyle-Width="100" 
        DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right">
      </asp:BoundField>

      <asp:BoundField DataField="PO_Currency" HeaderText="PO_Currency" ReadOnly="True" 
        SortExpression="PO_Currency" HeaderStyle-Width="51px" ItemStyle-Width="51px">
      </asp:BoundField>

            <asp:TemplateField HeaderText="SiteRecordNo" SortExpression="SiteRecordNo" ControlStyle-Width="120" HeaderStyle-Width="120" ItemStyle-Width="120">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxSiteRecordEdit" runat="server" Text='<%# Bind("SiteRecordNo") %>' CssClass=" TextBoxGeneralRev"></asp:TextBox>

<%--                                        <asp:DropDownList ID="DropDownListActivityCode" 
                                        runat="server" visible="false"
                                        DataSourceID="SqlDataSourceActivityCode" DataTextField="CostCode" 
                                        DataValueField="CostCode" 
                                        selectedvalue='<%# Bind("ActivityCode") %>' AppendDataBoundItems="True" >
                                        </asp:DropDownList>

                                        <asp:SqlDataSource ID="SqlDataSourceActivityCode" runat="server" 
                                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                          SelectCommand="SELECT rtrim(CostCode) as CostCode FROM Table_ActivityCodeDSP3 ORDER BY CodeID ASC">
                                        </asp:SqlDataSource>--%>

<%--                  <div style="display:none!important;"><asp:label ID="LabelContractReference" runat="server" Text="Contract Reference" Visible="false"></asp:label></div>
                  <div style="display:none!important;"><asp:TextBox ID="TextBoxContractReference" runat="server" Text='<%# Bind("ContractReference") %>' CssClass="TextBoxGeneralRev" Visible="false"></asp:TextBox></div>--%>

                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("SiteRecordNo") %>'></asp:Label>
                    <div style="display:none!important;">
                    <asp:Label ID="LabelActivityCode" runat="server" Text='<%# Bind("ActivityCode") %>' ForeColor="#6666FF"></asp:Label>
                    </div>
                    <div style="display:none!important;"><asp:label ID="LabelContractReferenceItem" runat="server" Text='<%# Bind("ContractReference") %>'  Visible="false" 
                    ForeColor="#FF6600" Font-Italic="True" Font-Size="10px"></asp:label></div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PayReqDate" SortExpression="PayReqDate" HeaderStyle-Width="100" ControlStyle-Width="100" ItemStyle-Width="100">
                <EditItemTemplate>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPayReqDate" ControlToValidate="TextBoxPayReqDateShown"
                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="TextBoxPayReqDateShown" runat="server" Text='<%# Bind("PayReqDate","{0:dd/MM/yyyy}") %>' CssClass="TextBoxGeneralRev add_datepicker"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPayReqDate" runat="server" 
                    ErrorMessage="Required" ControlToValidate="TextBoxPayReqDateShown"></asp:RequiredFieldValidator>

                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("PayReqDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes" SortExpression="Notes" ControlStyle-Width="50" HeaderStyle-Width="50" ItemStyle-Width="50">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxNoteEdit" runat="server" Text='<%# Bind("Notes") %>' TextMode="MultiLine" CssClass="TextBoxGeneralRevMultiline"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="PDF" ItemStyle-Width="120" HeaderStyle-Width="120" >
                <EditItemTemplate>
                    <asp:Label ID="LabelInvoiceEdit" runat="server" Text='<%# Bind("LinkToInvoice") %>'  CssClass="hidepanel"></asp:Label>
                    <asp:Label ID="LabelLinkToInvoiceToTransfer" runat="server" Text="" CssClass="hidepanel"></asp:Label>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:ImageButton ID="ImageButtonEdit" runat="server" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToInvoice") %>' CommandName="OpenPdfEdit" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUploadInvoice" runat="server" CssClass="LabelGeneral" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                                <asp:Button ID="ButtonUploadInvoice" runat="server" CausesValidation="False" 
                                    CssClass="ButtonGeneral" Text="Upload" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="UploadInvoice" />
                                
                            </td>
                        </tr>


                        <tr>
                            <td>
                                
                            </td>
                        </tr>


                    </table>

                </EditItemTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButtonItem" runat="server" CommandName="OpenPdfItem"
                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToInvoice") %>'/>

                    &nbsp;

                    <asp:HyperLink ID="HypCvr" runat="server" Target="_blank">Cover Page</asp:HyperLink>

                    <asp:HyperLink ID="HyperLinkSplit" runat="server" CssClass="btn btn-mini btn-danger" Target="_blank" NavigateUrl='<%# Eval("InvoiceId", "~/PaymentrequestSplitInvoices.aspx?InvoiceId={0}")%>' >Split Invoice</asp:HyperLink>

                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />

    </asp:GridView>


    <asp:SqlDataSource ID="SqlDataSourceEditPaymentReq" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        UpdateCommand="UPDATE_Table4_PaymentRequest" UpdateCommandType="StoredProcedure"
        DeleteCommand="DELETE_Table4_PaymentRequest" DeleteCommandType="StoredProcedure" >
        <SelectParameters>
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>
                 <asp:DropDownList ID="DropDownListFinanceCheck" runat="server" Visible="false"
                  DataSourceID="SqlDataSourceFinanceCheck" DataTextField="PayReqNo" DataValueField="PayReqNo"    
                 ></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceFinanceCheck" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>">
                </asp:SqlDataSource>

</asp:Content>