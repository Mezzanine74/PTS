<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="Paymentrequest.aspx.vb" Inherits="__paymentrequestACoptimizationZZ222" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Untitled Page</title>

    <style type="text/css">
        
        .style16
        {
            width: 259px;
        }        
        .style18
        {
            width: 110px;
        }
        .style19
        {
            width: 350px;
        }
        .style21
        {
            width: 150px;
        }
        .style22
        {
        }
        
        .style23
        {
        }
        
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    

<div id="myModal" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">The Last 5 Site Record No</h4>
            </div>
            <div class="modal-body">
                                        <asp:GridView ID="GridViewSiteReqNoHelper" runat="server" AutoGenerateColumns="False" 
                                            DataSourceID="SqlDataSourceSiteReqNoHelper" ShowHeader="false" CssClass="table table-striped table-bordered table-hover">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>

                                                        <asp:Label ID="LabelSiteRecordNo" runat="server" CssClass="label cursor_pointer label-success">pick me</asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SiteRecordNo" HeaderText="SiteRecordNo" SortExpression="SiteRecordNo" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="SqlDataSourceSiteReqNoHelper" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                            SelectCommand="
											select TOP 5 SiteRecordNo from (
											SELECT TOP 20 SiteRecordNo
											FROM         (SELECT     10000000 AS PayReqNo, Table3_Invoice_PRrequestToPM.SiteReqNo AS SiteRecordNo
																   FROM          Table3_Invoice INNER JOIN
																						  Table2_PONo ON Table2_PONo.PO_No = Table3_Invoice.PO_No INNER JOIN
																						  Table3_Invoice_PRrequestToPM ON Table3_Invoice.InvoiceID = Table3_Invoice_PRrequestToPM.InvoiceID LEFT OUTER JOIN
																						  Table4_PaymentRequest ON Table3_Invoice.InvoiceID = Table4_PaymentRequest.InvoiceID
																   WHERE      (Table2_PONo.Project_ID = @ProjectID) AND (Table4_PaymentRequest.InvoiceID IS NULL)
																   UNION ALL
																   SELECT     Table4_PaymentRequest_1.PayReqNo, Table4_PaymentRequest_1.SiteRecordNo
																   FROM         Table4_PaymentRequest AS Table4_PaymentRequest_1 INNER JOIN
																						 Table3_Invoice AS Table3_Invoice_1 ON Table3_Invoice_1.InvoiceID = Table4_PaymentRequest_1.InvoiceID INNER JOIN
																						 Table2_PONo AS Table2_PONo_1 ON Table2_PONo_1.PO_No = Table3_Invoice_1.PO_No
																   WHERE     (Table2_PONo_1.Project_ID = @ProjectID) AND (isnumeric(LEFT(Table4_PaymentRequest_1.SiteRecordNo, 1)) = 1)) AS Source
											GROUP BY SiteRecordNo, PayReqNo
											ORDER BY PayReqNo DESC

											) as SiteRecNoSource
											order by SiteRecordNo desc ">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="0" Name="ProjectID" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
            </div>
        </div>
    </div>
</div>

<div id="myModalPDFNote" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Why cant I see PDF upload control?</h4>
            </div>
            <div class="modal-body">

                <asp:Image ID="ImagePDFinstrcion" runat="server" ImageUrl="~/images/InvoicePDF_Instruction_PayReq.png" />

            </div>
        </div>
    </div>
</div>

    <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>

                       <asp:Label ID="Labeltest" runat="server" ></asp:Label>

    <rsweb:ReportViewer ID="ReportViewerFollowUpReportBySupplierExcVAT" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    </rsweb:ReportViewer>


        <asp:FormView ID="FormViewPayReq" runat="server" DataKeyNames="PayReqNo" CssClass="table" 
            DataSourceID="SqlDataSourcePayReq" DefaultMode="Insert">
            <EditItemTemplate>
            </EditItemTemplate>
            <InsertItemTemplate>

<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
 TargetControlID="lnkPopup"
 PopupControlID="panEdit"
 BackgroundCssClass="modalBackground"
 CancelControlID="btnCancel"
 PopupDragHandleControlID="panEdit" >
</asp:ModalPopupExtender>

<asp:Panel ID="panEdit" runat="server" 
          BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" 
          BackColor="White" CssClass="hidepanel" >
        <h2 style="text-align: center; color: #FF0000;">Large File</h2>
        <div style="text-align: center; font-size: 12px; color: #333333; font-weight: bold;">You are uploading a pdf file which is bigger than 1.2 MB in size.</div>
        <div style="text-align: center; font-size: 12px; color: #333333; font-weight: bold;">Please see how to optimize PDF files in <a style="font-size: 14px;" href="http://mercuryeng.org/HOW_TO_REDUCE_PDF_SIZE.htm" target="_blank">this link</a> . It is very straightforward process.</div>
        <div style="text-align: center; font-size: 12px; color: #333333; font-weight: bold;">Or you can visit this 
            <a href="http://smallpdf.com/" target="_blank">site</a> 
            to reduce your file size online</div>
        <div style="text-align: center; font-size: 12px; color: #333333; font-weight: bold;">Thanks for your understanding</div>
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

                    <asp:Label ID="LabelPayReqNoInsert" runat="server" Text='<%# Bind("PayReqNo") %>' CssClass="hidepanel" ></asp:Label>

                    <div class="form-horizontal">
                        <div class="col-xs-6">

                            <div class="form-group">
                                <div class="col-xs-6" >
                                    <asp:DropDownList ID="DropDownListProject" runat="server" AutoPostBack="True" 
                                         onselectedindexchanged="DropDownListProject_SelectedIndexChanged" CssClass="form-control" 
                                        OnLoad="DropDownListProject_Load" OnPreRender="DropDownListProject_PreRender">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-xs-12" >
                                    <asp:DropDownList ID="DropDownListInvoiceIDNotInPendingYet" runat="server" 
                                        AutoPostBack="True" CssClass="form-control ddl_fxfnt" 
                                        DataSourceID="SqlDataSourceInvoiceIdNotInPendingYet" 
                                        DataTextField="InvoiceIDNotInPendingYet" DataValueField="InvoiceID" 
                                        ondatabound="DropDownListInvoiceIDNotInPendingYet_DataBound" 
                                        onselectedindexchanged="DropDownListInvoiceIDNotInPendingYet_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                        ControlToValidate="DropDownListInvoiceIDNotInPendingYet" 
                                         ErrorMessage="Required" Operator="NotEqual" 
                                        ValueToCompare="0" Display="Dynamic" ></asp:CompareValidator>
                                    <asp:CompareValidator ID="CompareValidatorPMreq" runat="server" 
                                        ControlToValidate="DropDownListInvoiceIDNotInPendingYet" 
                                         ErrorMessage="Required" Operator="NotEqual" 
                                        ValueToCompare="0" ValidationGroup="SendReqToPM" Display="Dynamic" ></asp:CompareValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                    <asp:Label ID="LabelContractReference" runat="server" Text="Contract Reference: " 
                                    CssClass="col-xs-3 control-label" Visible="false" AssociatedControlID="TextBoxContractReference"  ></asp:Label>
                                <div class="col-xs-3" >
                                    <asp:TextBox ID="TextBoxContractReference" runat="server" Text="" Visible="false" CssClass="form-control" ></asp:TextBox>

                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-xs-6 col-md-offset-3" >

                                    <asp:label ID="labelsitereqnoHelper" runat="server" Text ="last 5 SiteRecordNo" CssClass="label label-success arrowed-in-right arrowed cursor_pointer" Visible="false"></asp:label>

                                    <asp:LinkButton ID="LinkButtonCoverPage" runat="server"   CausesValidation=false CssClass="label label-success arrowed-in-right arrowed cursor_pointer"
                                       onclick="LinkButtonCoverPage_Click" Visible="false" >Produce Cover Page</asp:LinkButton>

<%--                                    <asp:Label ID="LinkButtonPDFNote" runat="server" Text ="Why cant I see PDF upload control?" 
                                        CausesValidation="false" CssClass="label label-warning arrowed-in-right arrowed" ></asp:Label>--%>


                                    <span id="SpanWarning" runat="server" class="label label-warning cursor_pointer" style="display:none;">
												<i class="ace-icon fa fa-exclamation-triangle icon-animated-bell"></i>
												Why cant I see PDF upload control?
									</span>

                                </div>

                            </div>

                            <div class="form-group">
                                    <asp:Label ID="LabelSiteRecordNo" runat="server"  Text="SiteRecordNo"
                                         CssClass="col-xs-3 control-label" AssociatedControlID="SiteRecordNoTextBox"></asp:Label>
                                <div class="col-xs-3" >
                                    <asp:TextBox ID="SiteRecordNoTextBox" runat="server"  Text='<%# Bind("SiteRecordNo") %>' CssClass="form-control"/>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPMrequest" runat="server" 
                                            ControlToValidate="SiteRecordNoTextBox"  
                                            ErrorMessage="Required" Display="Dynamic" ValidationGroup="SendReqToPM">
                                        </asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-3 "  >
                                    <asp:Button ID="ButtonSendRequestToPM" runat="server" Visible="false" CssClass="btn btn-default" 
                                        Text="Send Request To PM" ValidationGroup="SendReqToPM" OnClick="ButtonSendRequestToPM_Click" />

                                    <asp:CheckBox ID="cbx_skippm" runat="server" Text="Skip PM" />

                                </div>
                            </div>

                            <div class="form-group">
                                    <asp:Label ID="LabelPayReqDate" runat="server" Text="PayReqDate" CssClass="col-xs-3 control-label"></asp:Label>
                                <div class="col-xs-3" >
                                        <asp:TextBox ID="PayReqDateTextBoxHolder" runat="server" CssClass="form-control add_datepicker" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPayReqDate" 
                                                runat="server" ControlToValidate="PayReqDateTextBoxHolder" 
                                                 ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPayReqDate" runat="server" 
                                        ControlToValidate="PayReqDateTextBoxHolder" Display="Dynamic"
                                        ErrorMessage="dd/mm/yyyy" 
                                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                        <asp:Label ID="LabelAttachRequest" runat="server" CssClass="col-xs-3 control-label" AssociatedControlID="FileUploadInvoice"
                                            Text="Attach Request"></asp:Label>
                                <div class="col-xs-3" >
                                        <asp:FileUpload ID="FileUploadInvoice" runat="server" CssClass="form-control"  />
                                        <asp:Label ID="LabelInfoInvoice" runat="server" ForeColor="Red" ></asp:Label>
                                </div>

                                <div class="col-xs-3" >
                                        <asp:Button ID="ButtonUploadInvoice" runat="server" CssClass="btn btn-light" 
                                            onclick="ButtonUploadInvoice_Click" Text="Upload" 
                                            CausesValidation="False" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                             ErrorMessage="Required" 
                                            ControlToValidate="TextBoxLinkToInvoice"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                        <asp:Label ID="LabelPaymentTerm" runat="server" Text="Payment Term" CssClass="col-xs-3 control-label" AssociatedControlID="DropDownListPaymentTerm"></asp:Label>
                                <div class="col-xs-3" >
                                        <asp:DropDownList ID="DropDownListPaymentTerm" runat="server" 
                                            CssClass="form-control" 
                                          ondatabound="DropDownListPaymentTerm_DataBound">
                                        </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-xs-offset-3 col-md-3">
                                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CssClass="btn btn-primary pull-right" 
                                    CommandName="Insert" Text="Insert" onclick="InsertButton_Click" />
                                </div>
                            </div>

                        </div>

                        <div class="col-xs-6">
                            <div class="form-group">
                                <div class="col-xs-12" >

                                    <div id="Div_InvoiceDetails" runat="server" visible="false" class="col-xs-12 col-sm-6 widget-container-col ui-sortable">
										<div class="widget-box widget-color-blue ui-sortable-handle">
											<!-- #section:custom/widget-box.options -->
											<div class="widget-header">
												<h5 class="widget-title bigger lighter">
													<i class="ace-icon fa fa-table"></i>
													Invoice &amp; Details
												</h5>
											</div>

											<!-- /section:custom/widget-box.options -->
											<div class="widget-body">
												<div class="widget-main no-padding">
													<table class="table table-striped table-bordered table-hover">

														<tbody>
															<tr>
																<td class="col-xs-3">Supplier Name</td>
																<td class="col-xs-5">
                                                                    <asp:Label ID="LabelSupplierName" runat="server" ></asp:Label>

																</td>
															</tr>

															<tr>
																<td class="">Description</td>
																<td>
                                                                    <asp:Label ID="LabelDescription" runat="server" ></asp:Label>

																</td>
															</tr>

															<tr>
																<td class="">Invoice No</td>
																<td>
                                                                    <asp:Label ID="LabelInvoiceNo" runat="server" ></asp:Label>

																</td>
															</tr>

															<tr>
																<td class="">Invoice Date</td>
																<td>
                                                                    <asp:Label ID="LabelInvoiceDate" runat="server" ></asp:Label>

																</td>
															</tr>

															<tr>
																<td class="">Invoice Value</td>
																<td>
                                                                    <asp:Label ID="LabelInvoiceValue" runat="server" ></asp:Label>
                                                                    <asp:Label ID="LabelCurrency" runat="server" ></asp:Label>

																</td>
															</tr>

														</tbody>
													</table>
												</div>
											</div>
										</div>
									</div>







                                </div>
                            </div>
                        </div>

                    </div>

                            <asp:HyperLink ID="HyperLinkProduceCoverPage" runat="server" Font-Size="10px" Target="_blank" ></asp:HyperLink>

                <asp:Panel ID="Panel1" runat="server" CssClass="hidepanel">        
                <asp:SqlDataSource ID="SqlDataSourceInvoiceIdNotInPendingYet" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="
IF (SELECT rtrim(@UserName)) = N'vusala.gadjieva' AND (SELECT @ProjectID) = 168 
	BEGIN
		SELECT     dbo.View_InvoiceIDNotInPendingYet1.InvoiceID, REPLACE(dbo.View_InvoiceIDNotInPendingYet1.InvoiceIDNotInPendingYet, ' ', '_') AS InvoiceIDNotInPendingYet, 
							  dbo.View_InvoiceIDNotInPendingYet1.ProjectID, RTRIM(dbo.Table7_CostCode.Type) AS Type, RTRIM(dbo.aspnet_Users.UserName) AS UserName
		FROM         dbo.View_InvoiceIDNotInPendingYet1 INNER JOIN
							  dbo.Table3_Invoice ON dbo.View_InvoiceIDNotInPendingYet1.InvoiceID = dbo.Table3_Invoice.InvoiceID INNER JOIN
							  dbo.Table2_PONo ON dbo.Table3_Invoice.PO_No = dbo.Table2_PONo.PO_No INNER JOIN
							  dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode INNER JOIN
							  dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN
							  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
							  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId
		WHERE     (RTRIM(dbo.aspnet_Users.UserName) = @UserName) AND (RTRIM(dbo.Table7_CostCode.Type) <> 'Finance') AND (dbo.View_InvoiceIDNotInPendingYet1.ProjectID = @ProjectID)

		UNION ALL

		SELECT     dbo.View_InvoiceIDNotInPendingYet1.InvoiceID, REPLACE(dbo.View_InvoiceIDNotInPendingYet1.InvoiceIDNotInPendingYet, ' ', '_') AS InvoiceIDNotInPendingYet, 
							  dbo.View_InvoiceIDNotInPendingYet1.ProjectID, RTRIM(dbo.Table7_CostCode.Type) AS Type, NULL AS UserName
		FROM         dbo.aspnet_UsersInRoles INNER JOIN
							  dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId INNER JOIN
							  dbo.aspnet_Users ON dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId INNER JOIN
							  dbo.View_InvoiceIDNotInPendingYet1 INNER JOIN
							  dbo.Table3_Invoice ON dbo.View_InvoiceIDNotInPendingYet1.InvoiceID = dbo.Table3_Invoice.InvoiceID INNER JOIN
							  dbo.Table2_PONo ON dbo.Table3_Invoice.PO_No = dbo.Table2_PONo.PO_No INNER JOIN
							  dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode INNER JOIN
							  dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID ON dbo.aspnet_Roles.RoleName = dbo.Table7_CostCode.Type
		WHERE     (dbo.Table7_CostCode.CostCode = '005')
		GROUP BY dbo.View_InvoiceIDNotInPendingYet1.InvoiceID, REPLACE(dbo.View_InvoiceIDNotInPendingYet1.InvoiceIDNotInPendingYet, ' ', '_'), 
							  dbo.View_InvoiceIDNotInPendingYet1.ProjectID, RTRIM(dbo.Table7_CostCode.Type)
		HAVING      (dbo.View_InvoiceIDNotInPendingYet1.ProjectID = @ProjectID)
	END
ELSE
	BEGIN
		SELECT     dbo.View_InvoiceIDNotInPendingYet1.InvoiceID, REPLACE(dbo.View_InvoiceIDNotInPendingYet1.InvoiceIDNotInPendingYet, ' ', '_') AS InvoiceIDNotInPendingYet, 
							  dbo.View_InvoiceIDNotInPendingYet1.ProjectID, RTRIM(dbo.Table7_CostCode.Type) AS Type, RTRIM(dbo.aspnet_Users.UserName) AS UserName
		FROM         dbo.View_InvoiceIDNotInPendingYet1 INNER JOIN
							  dbo.Table3_Invoice ON dbo.View_InvoiceIDNotInPendingYet1.InvoiceID = dbo.Table3_Invoice.InvoiceID INNER JOIN
							  dbo.Table2_PONo ON dbo.Table3_Invoice.PO_No = dbo.Table2_PONo.PO_No INNER JOIN
							  dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode INNER JOIN
							  dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN
							  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
							  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId
		WHERE     (RTRIM(dbo.aspnet_Users.UserName) = @UserName) AND (RTRIM(dbo.Table7_CostCode.Type) <> 'Finance') AND (dbo.View_InvoiceIDNotInPendingYet1.ProjectID = @ProjectID)

		UNION ALL

		SELECT     dbo.View_InvoiceIDNotInPendingYet1.InvoiceID, REPLACE(dbo.View_InvoiceIDNotInPendingYet1.InvoiceIDNotInPendingYet, ' ', '_') AS InvoiceIDNotInPendingYet, 
							  dbo.View_InvoiceIDNotInPendingYet1.ProjectID, RTRIM(dbo.Table7_CostCode.Type) AS Type, RTRIM(dbo.aspnet_Users.UserName) AS UserName
		FROM         dbo.aspnet_UsersInRoles INNER JOIN
							  dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId INNER JOIN
							  dbo.aspnet_Users ON dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId INNER JOIN
							  dbo.View_InvoiceIDNotInPendingYet1 INNER JOIN
							  dbo.Table3_Invoice ON dbo.View_InvoiceIDNotInPendingYet1.InvoiceID = dbo.Table3_Invoice.InvoiceID INNER JOIN
							  dbo.Table2_PONo ON dbo.Table3_Invoice.PO_No = dbo.Table2_PONo.PO_No INNER JOIN
							  dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode INNER JOIN
							  dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID ON dbo.aspnet_Roles.RoleName = dbo.Table7_CostCode.Type
		WHERE     (RTRIM(dbo.Table7_CostCode.Type) = 'Finance') AND (RTRIM(dbo.aspnet_Users.UserName) = @UserName) AND (dbo.View_InvoiceIDNotInPendingYet1.ProjectID = @ProjectID)
	END ">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListProject" Name="ProjectID" 
                            PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                            PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>                
                       
                               <asp:SqlDataSource ID="SqlDataSourceSupplierName" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SELECT Table3_Invoice.InvoiceID, Table6_Supplier.SupplierName FROM Table6_Supplier INNER JOIN Table2_PONo ON Table6_Supplier.SupplierID = Table2_PONo.SupplierID INNER JOIN Table3_Invoice ON Table2_PONo.PO_No = Table3_Invoice.PO_No WHERE (InvoiceID=@InvoiceID);">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListInvoiceIDNotInPendingYet" 
                                        Name="InvoiceID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSourceDescription" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                
                    SelectCommand="SELECT Table3_Invoice.InvoiceID, RTRIM(Table2_PONo.Description) AS Description FROM Table2_PONo INNER JOIN Table3_Invoice ON Table2_PONo.PO_No = Table3_Invoice.PO_No WHERE (Table3_Invoice.InvoiceID = @InvoiceID)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListInvoiceIDNotInPendingYet" 
                                        Name="InvoiceID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSourceInvoiceNo" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SELECT InvoiceID, Invoice_No FROM Table3_Invoice WHERE (InvoiceID=@InvoiceID);">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListInvoiceIDNotInPendingYet" 
                                        Name="InvoiceID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSourceInvoiceDate" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SELECT InvoiceID, Invoice_Date FROM Table3_Invoice WHERE (InvoiceID = @InvoiceID);">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListInvoiceIDNotInPendingYet" 
                                        Name="InvoiceID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSourceInvoiceValue" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SELECT InvoiceID, InvoiceValue FROM Table3_Invoice WHERE (InvoiceID = @InvoiceID)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListInvoiceIDNotInPendingYet" 
                                        Name="InvoiceID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSourceCurrency" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SELECT Table3_Invoice.InvoiceID, Table2_PONo.PO_Currency FROM Table2_PONo INNER JOIN Table3_Invoice ON Table2_PONo.PO_No = Table3_Invoice.PO_No WHERE (Table3_Invoice.InvoiceID = @InvoiceID)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListInvoiceIDNotInPendingYet" 
                                        Name="InvoiceID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>

             
                                            <asp:DropDownList ID="DropDownListSupplierName" 
                    runat="server" Height="16px" 
                                Width="152px" 
                                DataSourceID="SqlDataSourceSupplierName" DataTextField="SupplierName" 
                                DataValueField="InvoiceID" Visible="False">
                            </asp:DropDownList>

                            <asp:DropDownList ID="DropDownListDescription" runat="server" Height="16px" 
                                Width="155px" DataSourceID="SqlDataSourceDescription" 
                                DataTextField="Description" DataValueField="InvoiceID" 
                    Visible="False">
                            </asp:DropDownList>

                            <asp:DropDownList ID="DropDownListInvoiceNo" runat="server" Height="18px" 
                                Width="153px" DataSourceID="SqlDataSourceInvoiceNo" DataTextField="Invoice_No" 
                                DataValueField="InvoiceID" Visible="False">
                            </asp:DropDownList>

                            <asp:DropDownList ID="DropDownListInvoiceDate" runat="server" Height="16px" 
                                Width="148px" DataSourceID="SqlDataSourceInvoiceDate" 
                                DataTextField="Invoice_Date" DataValueField="InvoiceID" 
                    Visible="False">
                            </asp:DropDownList>

                            <asp:DropDownList ID="DropDownListInvoiceValue" runat="server" Height="16px" 
                                Width="153px" DataSourceID="SqlDataSourceInvoiceValue" 
                                DataTextField="InvoiceValue" DataValueField="InvoiceID" 
                    Visible="False">
                            </asp:DropDownList>

                            <asp:DropDownList ID="DropDownListCurrency" runat="server" Height="16px" 
                                Width="156px" DataSourceID="SqlDataSourceCurrency" DataTextField="PO_Currency" 
                                DataValueField="InvoiceID" Visible="False">
                            </asp:DropDownList>

                    <asp:TextBox ID="TextBoxLinkToInvoice" runat="server" Height="18px" 
                        Text='<%# Bind("LinkToInvoice") %>'  Width="954px"></asp:TextBox>
                <asp:TextBox ID="TextBoxPayReqDate" runat="server" Height="22px" 
                    Text='<%# Bind("PayReqDate") %>' Width="128px"></asp:TextBox>
                <asp:TextBox ID="InvoiceIDTextBox" runat="server" style="text-align: left" 
                    Text='<%# Bind("InvoiceID") %>' />                
                    <asp:TextBox ID="TextBoxStatus" Text='<%# Bind("Urgency") %>' runat="server" 
                    ></asp:TextBox>
                </asp:Panel>

            </InsertItemTemplate>
            <ItemTemplate>
            </ItemTemplate>
        </asp:FormView>

            
        <asp:SqlDataSource ID="SqlDataSourcePayReq" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
            SelectCommand="SELECT * FROM [Table4_PaymentRequest]" 
            InsertCommand="INSERT INTO Table4_PaymentRequest(InvoiceID, SiteRecordNo, PayReqDate, LinkToInvoice, CreatedBy, PersonCreated, Approved, PersonApprove, LastAction, PaymentTerm, ContractReference) VALUES (@InvoiceID, @SiteRecordNo, @PayReqDate, @LinkToInvoice, @CreatedBy, @PersonCreated, @Approved, @PersonApprove, @LastAction, @PaymentTerm, @ContractReference)">
            <InsertParameters>
            <asp:Parameter Name="CreatedBy" />
            <asp:Parameter Name="PersonCreated" />
            </InsertParameters>
            </asp:SqlDataSource>


</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">

<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=FormViewPayReq.FindControl("labelsitereqnoHelper").ClientID%>').click(function () {
            $("#myModal").modal('show');
        });

        $('#<%=FormViewPayReq.FindControl("SpanWarning").ClientID%>').click(function () {
            $("#myModalPDFNote").modal('show');
        });

    });
</script>

<script type="text/javascript">
    $(document).ready(function () {

        $('[data-siterecordno]').click(function () {
            $('#MainContent_FormViewPayReq_SiteRecordNoTextBox').val($(this).data("siterecordno")).focus();
            $("#myModal").modal('hide');
        });

    });
</script>


</asp:Content>
