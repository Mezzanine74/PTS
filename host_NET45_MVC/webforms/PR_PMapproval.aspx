<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PR_PMapproval.aspx.vb" Inherits="PR_PMapproval" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<div style="text-align: center; width: 100%; display:none;" >
    <rsweb:ReportViewer ID="ReportViewerInvoiceCoverPage" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    <ServerReport
       DisplayName="CostReport" ReportPath="/"
          ReportServerUrl="http://localhost/ReportServer_SQLEXPRESS" />
    </rsweb:ReportViewer>
</div>

    <asp:Button ID="BtnApproveAll" runat="server" Text="Approve All My Items" CssClass="btn btn-success btn-lg icon-animated-wrench" OnClick="BtnApproveAll_Click" Visible="false"/>

<script>
    $(document).ready(function () {
        $("MainContent_BtnApproveAll").animate({ left: '250px' });
    });
</script>

    <asp:GridView ID="GridViewPMapproval" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourcePMapproval" GridLines="None" 
        CssClass="table table-nonfluid table-hover" EmptyDataText="N O   R E L E V A N T   D A T A   F O R   Y O U" >
        <Columns>
            <asp:BoundField DataField="InvoiceID" HeaderText="InvoiceID" ReadOnly="True" SortExpression="InvoiceID" Visible="false" />
            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="True" SortExpression="ProjectName" ItemStyle-Width="80" HeaderStyle-Width="80" />
            <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ReadOnly="True" SortExpression="SupplierName" ItemStyle-Width="150" HeaderStyle-Width="150" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" ItemStyle-Width="250" HeaderStyle-Width="250"/>
            <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" SortExpression="PO_No" ItemStyle-Width="80" HeaderStyle-Width="80"/>
            <asp:BoundField DataField="Invoice_No" HeaderText="Invoice No" ReadOnly="True" SortExpression="Invoice_No" ItemStyle-Width="80" HeaderStyle-Width="80"/>
            <asp:BoundField DataField="Invoice_Date" HeaderText="Invoice Date" ReadOnly="True" SortExpression="Invoice_Date" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="80" HeaderStyle-Width="80"/>
            <asp:BoundField DataField="InvoiceValue_ExcVAT" HeaderText="Invoice Value Exc VAT" ReadOnly="True" SortExpression="InvoiceValue_ExcVAT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" ItemStyle-Width="100" HeaderStyle-Width="100"/>
            <asp:BoundField DataField="PO_Currency" ReadOnly="True" SortExpression="PO_Currency" ItemStyle-Width="50" HeaderStyle-Width="50"/>
            <asp:BoundField DataField="SiteReqNo" HeaderText="Site Req No" ReadOnly="True" SortExpression="SiteReqNo" ItemStyle-Width="80" HeaderStyle-Width="80"/>
            <asp:TemplateField HeaderText=" PDF " ItemStyle-Width="50" HeaderStyle-Width="50">
                <ItemTemplate>
                 <asp:HyperLink ID="ImageButtonPdf" runat="server" Target="_blank" ></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="PM" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50" HeaderStyle-Width="50">
                <ItemTemplate>
                    <usercontrol:ImageUserPhoto ID="ImageUserPM" runat="server" UserName='<%# Eval("ProjectManager")%>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="From" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50" HeaderStyle-Width="50">
                <ItemTemplate>
                    <usercontrol:ImageUserPhoto ID="ImageUserRQ" runat="server" UserName='<%# Eval("WhoRequested")%>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Approved Or Not" SortExpression="ApprovedOrNot" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" HeaderStyle-Width="100" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="ImageButtonApproval" runat="server" 
                        CommandName="Approval" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False"/>
                    <asp:Label ID="LabelInvoiceID" runat="server" CssClass="hidepanel " Text='<%# Eval("InvoiceID")%>' ></asp:Label>
                    <asp:Label ID="LabelProjectID" runat="server" CssClass="hidepanel" Text='<%# Eval("ProjectID")%>' ></asp:Label>
                    <asp:Label ID="LabelProjectName" runat="server" CssClass="hidepanel" Text='<%# Eval("ProjectName")%>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="WhenApproved" HeaderText="When Approved" ReadOnly="True" SortExpression="WhenApproved" ItemStyle-Width="120" HeaderStyle-Width="120" Visible="false"/>
            <asp:BoundField DataField="WhoApproved" HeaderText="Who Approved" ReadOnly="True" SortExpression="WhoApproved" ItemStyle-Width="120" HeaderStyle-Width="120" Visible="false"/>

            <asp:TemplateField>
                <ItemTemplate>

                                          <asp:Gridview ID="GridviewApprovalStatus" runat="server" 
                                            DataSourceID="SqlDataSourceApprovalStatus" CssClass="table table-nonfluid borderless"
                                            AutoGenerateColumns="False" onrowcommand="GridviewApprovalStatus_RowCommand"
                                               onrowdatabound="GridviewApprovalStatus_RowDataBound" GridLines="None" >
                                            <Columns>
                                              <asp:TemplateField >
                                                <ItemTemplate>
                                                 <asp:Literal id="LiteralUserName" runat="server" Text='<%# Bind("ApprovalRequiredPersons")%>'></asp:Literal>
                                                </ItemTemplate>
                                              </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Approved" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                <%-- LiteralApproved and LiteralContractID should stay here, they are being used in RowCommand --%>
                                                <asp:Literal id="LiteralApproved" runat="server" Text='<%# Bind("ApprovedOrNot")%>' Visible="false"></asp:Literal>
                                                <asp:Literal id="LiteralInvoiceId" runat="server" Text='<%# Bind("InvoiceID")%>' Visible="false"></asp:Literal>
                                                <asp:Literal id="LiteralPosition" runat="server" Text='<%# Bind("Position")%>' Visible="false"></asp:Literal>

                                                <asp:ImageButton ID="ImageButtonApproval" runat="server" CommandName="Approval" 
                                                CommandArgument='<%# Container.DataItemIndex %>' 
                                                 CausesValidation="False" />

                                                </ItemTemplate>
                                              </asp:TemplateField>

                                              <asp:TemplateField HeaderText="When Approved" Visible="false">
                                                <ItemTemplate>
                                                 <%--<asp:Literal id="LiteralWhenApproved" runat="server" Text='<%# Bind("WhenApproved", "{0:dd/MM/yyyy}")%>'></asp:Literal>--%>
                                                 <asp:Literal id="LiteralWhenApproved" runat="server" Text="coming in later"></asp:Literal>
                                                </ItemTemplate>
                                              </asp:TemplateField>
                                            </Columns>
                                               <HeaderStyle CssClass="hide" />
                                          </asp:Gridview>
                                          <asp:SqlDataSource ID="SqlDataSourceApprovalStatus" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                            SelectCommand="SELECT * FROM View_PaymentRequestApprovalStatus WHERE InvoiceId = @InvoiceId" SelectCommandType="Text">
                                            <SelectParameters>
                                              <asp:Parameter DefaultValue="0" Name="InvoiceId" Type="Int32" />
                                            </SelectParameters>
                                          </asp:SqlDataSource>

                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourcePMapproval" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" 

        IF @UserName = N'savas'
        BEGIN
SELECT     Table3_Invoice_PRrequestToPM.InvoiceID, RTRIM(Table1_Project.ProjectName) AS ProjectName, RTRIM(Table6_Supplier.SupplierName) AS SupplierName, 
                      CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR
                      dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description) + ' / ' + RTRIM(dbo.Table3_Invoice.Notes)
                       END AS Description, RTRIM(Table3_Invoice.Invoice_No) AS Invoice_No, Table3_Invoice.Invoice_Date, Table3_Invoice.InvoiceValue AS InvoiceValue_ExcVAT, 
                      RTRIM(Table2_PONo.PO_Currency) AS PO_Currency, Table3_Invoice_PRrequestToPM.SiteReqNo, RTRIM(Table1_Project.ProjectManager) AS ProjectManager, 
                      RTRIM(Table3_Invoice_PRrequestToPM.WhoRequested) AS WhoRequested, Table3_Invoice_PRrequestToPM.ApprovedOrNot, 
                      Table3_Invoice_PRrequestToPM.WhenApproved, Table3_Invoice_PRrequestToPM.WhoApproved, Table1_Project.ProjectID, Table2_PONo.PO_No, 
                      Table3_Invoice.InvoiceID AS Expr1
FROM         Table1_Project INNER JOIN
                      Table2_PONo ON Table1_Project.ProjectID = Table2_PONo.Project_ID INNER JOIN
                      Table3_Invoice ON Table2_PONo.PO_No = Table3_Invoice.PO_No INNER JOIN
                      Table3_Invoice_PRrequestToPM ON Table3_Invoice.InvoiceID = Table3_Invoice_PRrequestToPM.InvoiceID INNER JOIN
                      Table6_Supplier ON Table2_PONo.SupplierID = Table6_Supplier.SupplierID LEFT OUTER JOIN
                      Table4_PaymentRequest ON Table3_Invoice.InvoiceID = Table4_PaymentRequest.InvoiceID
WHERE     (Table4_PaymentRequest.InvoiceID IS NULL) 
            ORDER BY Table3_Invoice_PRrequestToPM.ApprovedOrNot
        END

        ELSE

        BEGIN

            select 
			
			
			InvoiceID, ProjectName, SupplierName, Description, Invoice_No, Invoice_Date, InvoiceValue_ExcVAT, PO_Currency, SiteReqNo, ProjectManager, WhoRequested, ApprovedOrNot, WhenApproved, WhoApproved, ProjectID, PO_No
			
			
			
			 from (
            SELECT     dbo.Table3_Invoice_PRrequestToPM.InvoiceID, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, RTRIM(dbo.Table6_Supplier.SupplierName) 
                                  AS SupplierName, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR
                                  dbo.Table3_Invoice.Notes IS NULL THEN RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description) + ' / ' + RTRIM(dbo.Table3_Invoice.Notes)
                                   END AS Description, RTRIM(dbo.Table3_Invoice.Invoice_No) AS Invoice_No, dbo.Table3_Invoice.Invoice_Date, 
                                  dbo.Table3_Invoice.InvoiceValue AS InvoiceValue_ExcVAT, RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency, dbo.Table3_Invoice_PRrequestToPM.SiteReqNo, 
                                  RTRIM(dbo.Table1_Project.ProjectManager) AS ProjectManager, RTRIM(dbo.Table3_Invoice_PRrequestToPM.WhoRequested) AS WhoRequested, 
                                  dbo.Table3_Invoice_PRrequestToPM.ApprovedOrNot, dbo.Table3_Invoice_PRrequestToPM.WhenApproved, dbo.Table3_Invoice_PRrequestToPM.WhoApproved, dbo.Table1_Project.ProjectID, dbo.Table2_PONo.PO_No
            FROM         dbo.Table1_Project INNER JOIN
                                  dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN
                                  dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN
                                  dbo.Table3_Invoice_PRrequestToPM ON dbo.Table3_Invoice.InvoiceID = dbo.Table3_Invoice_PRrequestToPM.InvoiceID INNER JOIN
                                  dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                                  dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
                                  dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId LEFT OUTER JOIN
                                  dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID
            WHERE     (dbo.Table4_PaymentRequest.InvoiceID IS NULL) AND (dbo.aspnet_Users.UserName = @UserName)

            UNION ALL

            SELECT     dbo.Table3_Invoice_PRrequestToPM.InvoiceID, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, RTRIM(dbo.Table6_Supplier.SupplierName) 
                                  AS SupplierName, CASE WHEN LEN(RTRIM(dbo.Table3_Invoice.Notes)) = 0 OR dbo.Table3_Invoice.Notes IS NULL THEN 
					              RTRIM(dbo.Table2_PONo.Description) ELSE RTRIM(dbo.Table2_PONo.Description) + ' / ' + RTRIM(dbo.Table3_Invoice.Notes) END AS Description,
					              RTRIM(dbo.Table3_Invoice.Invoice_No) AS Invoice_No, dbo.Table3_Invoice.Invoice_Date, 
                                  dbo.Table3_Invoice.InvoiceValue AS InvoiceValue_ExcVAT, RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency, dbo.Table3_Invoice_PRrequestToPM.SiteReqNo, 
                                  RTRIM(dbo.Table1_Project.ProjectManager) AS ProjectManager, RTRIM(dbo.Table3_Invoice_PRrequestToPM.WhoRequested) AS WhoRequested, 
                                  dbo.Table3_Invoice_PRrequestToPM.ApprovedOrNot, dbo.Table3_Invoice_PRrequestToPM.WhenApproved, dbo.Table3_Invoice_PRrequestToPM.WhoApproved, dbo.Table1_Project.ProjectID, dbo.Table2_PONo.PO_No
            FROM         dbo.Table1_Project INNER JOIN
                                  dbo.Table2_PONo ON dbo.Table1_Project.ProjectID = dbo.Table2_PONo.Project_ID INNER JOIN
                                  dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN
                                  dbo.Table3_Invoice_PRrequestToPM ON dbo.Table3_Invoice.InvoiceID = dbo.Table3_Invoice_PRrequestToPM.InvoiceID INNER JOIN
                                  dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID LEFT OUTER JOIN
                                  dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID
            WHERE dbo.Table1_Project.ProjectManager = @UserName  AND (dbo.Table4_PaymentRequest.InvoiceID IS NULL) 
			
			) AS Source
		GROUP BY InvoiceID, ProjectName, SupplierName, Description, Invoice_No, Invoice_Date, InvoiceValue_ExcVAT, PO_Currency, SiteReqNo, ProjectManager, WhoRequested, ApprovedOrNot, WhenApproved, WhoApproved, ProjectID, PO_No
        HAVING (InvoiceID not in (60534,60592,60598,60599,60637,60597,60446))
	    ORDER BY ApprovedOrNot

      END

">
        <SelectParameters>
            <asp:Parameter DefaultValue="_" Name="UserName" Type="string" />
        </SelectParameters>
    </asp:SqlDataSource>


</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server" ID="ContentScripts">

        <script type="text/javascript">
            $(document).ready(function () {
                //Scripts goes here
                $('[data-highligh]').toggle("highlight").toggle("highlight").toggle("highlight").toggle("highlight").toggle("highlight").toggle("highlight");
            })
        </script>

</asp:Content>