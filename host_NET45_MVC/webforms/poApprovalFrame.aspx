<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="poApprovalFrame.aspx.vb" Inherits="poApprovalFrame" %>

<%@ Register src="POdetailsForEmail.ascx" tagname="SeperateControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <asp:Panel ID="panelHideUserControl" runat="server" CssClass="hidepanel" >
   <uc1:SeperateControl ID="POdetailsForEmail" runat="server" />
  </asp:Panel>

  <asp:GridView ID="GridViewFrameApproval" runat="server" AutoGenerateColumns="False" CssClass="table table-nonfluid table-hover"
    DataSourceID="SqlDataSourceApprovaFrameContract" EnableModelValidation="True" GridLines="None" >
    <Columns>
      <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" 
        ReadOnly="True" SortExpression="ProjectName" />
      <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" 
        SortExpression="PO_No" />
      <asp:BoundField DataField="TotalPrice" HeaderText="Total Price With VAT" ReadOnly="True" 
        HeaderStyle-Width="100px" ItemStyle-Width="100px"
        SortExpression="TotalPrice" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
      <asp:BoundField DataField="PO_Currency" ReadOnly="True" 
        SortExpression="PO_Currency" />
      <asp:BoundField DataField="VATpercent" HeaderText="VAT %" ReadOnly="True" 
        SortExpression="VATpercent" ItemStyle-HorizontalAlign="Center" />
      <asp:TemplateField ItemStyle-Width="50px">
       <ItemTemplate>
         <asp:HyperLink 
              ID="HyperLinkFramContract" 
              runat="server" 
              Target="_blank" 
              NavigateUrl='<%# Eval("FrameContractID","~/ContractDetails.aspx?ContractID={0}") %>'
              >See Frame Contract
         </asp:HyperLink>
       </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Approval">
       <ItemTemplate>
                    <asp:DataList ID="DataListFrameApproval" runat="server"  
                        DataSourceID="SqlDataSourceFrameApproval" RepeatDirection="Horizontal" 
                      onitemdatabound="DataListFrameApproval_ItemDataBound" onitemcommand="DataListFrameApproval_ItemCommand" >
                      <ItemTemplate>
                          <table >
                              <tr>
                                  <td style="width:50px" >
                                    <asp:Literal id="LiteralUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Literal>
                                    <asp:Literal id="LiteralPONo" runat="server" Text='<%# Bind("PO_No") %>' Visible="false" ></asp:Literal>
                                  </td>
                                  <td>
                                    <asp:Literal id="LiteralApproval" runat="server" Text='<%# Bind("Approval") %>' Visible="false" ></asp:Literal>
                                    <asp:ImageButton ID="ImageButtonApproval" runat="server" CommandName="Approval" CausesValidation="False" />
                                  </td>
                                  <td style="width:100px" >
                                   <asp:Literal id="LiteralWhenApproved" runat="server" Text='<%# Bind("WhenApproved") %>'></asp:Literal>
                                  </td>
                              </tr>
                          </table>
                      </ItemTemplate>
                    </asp:DataList>
                    <asp:SqlDataSource ID="SqlDataSourceFrameApproval" runat="server" 
                             ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                             SelectCommand=" SELECT     FramContractPOApprovalUsers.UserName, dbo.Table_PO_FrContApprovalUsers.WhenApproved, CASE WHEN Table_PO_FrContApprovalUsers.UserName IS NULL 
                      THEN 0 ELSE 1 END AS Approval, FramContractPOApprovalUsers.PO_No
FROM         (SELECT     RTRIM(dbo.Table2_PONo.PO_No) AS PO_No, RTRIM(dbo.aspnet_Users.UserName) AS UserName
                       FROM          dbo.aspnet_Users CROSS JOIN
                                              dbo.Table2_PONo
                       WHERE      (dbo.Table2_PONo.FrameContractPO = 1) AND (dbo.Table2_PONo.Approved <> 1) AND (dbo.aspnet_Users.ApproveFramePo = 1)) 
                      AS FramContractPOApprovalUsers LEFT OUTER JOIN
                      dbo.Table_PO_FrContApprovalUsers ON FramContractPOApprovalUsers.PO_No = dbo.Table_PO_FrContApprovalUsers.PO_No AND 
                      FramContractPOApprovalUsers.UserName = dbo.Table_PO_FrContApprovalUsers.UserName
WHERE     (FramContractPOApprovalUsers.PO_No = @PO_No) ">
                            <SelectParameters>
                                <asp:Parameter Name="PO_No" Type="String" />
                            </SelectParameters>
                    </asp:SqlDataSource>
       </ItemTemplate>
      </asp:TemplateField>
    </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceApprovaFrameContract" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand=" 
SELECT     RTRIM(Table1_Project.ProjectName) AS ProjectName, RTRIM(Table2_PONo.PO_No) AS PO_No, Table2_PONo.FrameContractID, Table2_PONo.TotalPrice, 
                      Table2_PONo.PO_Currency, Table2_PONo.VATpercent
FROM         Table1_Project INNER JOIN
                      Table2_PONo ON Table1_Project.ProjectID = Table2_PONo.Project_ID INNER JOIN
                          (SELECT     derivedtbl_1_1.ProjectID
                            FROM          (SELECT     Table_Approval_UserPositionPrjJunction.ProjectID, Table_Approval_UserPositionPrjJunction.UserName
                                                    FROM          Table_Approval_UserPositionPrjJunction INNER JOIN
                                                                           Table_Approval_PositionEmployee ON 
                                                                           Table_Approval_UserPositionPrjJunction.PositionID = Table_Approval_PositionEmployee.PositionID
                                                    WHERE      (Table_Approval_PositionEmployee.PositionName IN (N'project manager', N'cost controller', N'finance director'))
                                                    UNION ALL
                                                    SELECT     ProjectID, UserName
                                                    FROM         Table_Approval_UserRolePrjectJunction
                                                    WHERE     (RoleName = N'InitiateContractAndAddendum')) AS derivedtbl_1_1 INNER JOIN
                                                       (SELECT     Project_ID
                                                         FROM          Table2_PONo AS Table2_PONo_1
                                                         WHERE      (FrameContractPO = 1) AND (Approved = 0)) AS derivedtbl_2 ON derivedtbl_1_1.ProjectID = derivedtbl_2.Project_ID
                            WHERE      (derivedtbl_1_1.UserName = @username)) AS derivedtbl_1 ON Table1_Project.ProjectID = derivedtbl_1.ProjectID
WHERE     (Table2_PONo.FrameContractPO = 1) AND (Table2_PONo.Approved <> 1)
GROUP BY RTRIM(Table1_Project.ProjectName), RTRIM(Table2_PONo.PO_No), Table2_PONo.FrameContractID, Table2_PONo.TotalPrice, Table2_PONo.PO_Currency, 
                      Table2_PONo.VATpercent
">
      <SelectParameters>
          <asp:Parameter name="username" Type="String"/>
      </SelectParameters>
  </asp:SqlDataSource>

<asp:Panel ID="PanelHide" runat="server" CssClass="hidepanel">
  <asp:GridView ID="GridViewPoToApprove" runat="server" AutoGenerateColumns="False" 
      DataSourceID="SqlDataSourcePoToApprove" EnableModelValidation="True" CssClass="Grid" >
      <Columns>

        <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" ReadOnly="True" 
          SortExpression="ApprovalStatus"  HeaderStyle-Width="80px" 
          ItemStyle-Width="80px">
  <HeaderStyle Width="80px"></HeaderStyle>

  <ItemStyle Width="80px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" 
          SortExpression="PO_No"  HeaderStyle-Width="80px" ItemStyle-Width="80px"/>

        <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" 
          ReadOnly="True" SortExpression="SupplierName"  HeaderStyle-Width="150px" 
          ItemStyle-Width="150px">
        <HeaderStyle Width="150px" />
        <ItemStyle Width="150px" />
        </asp:BoundField>
        <asp:BoundField DataField="Description" HeaderText="Description" 
          ReadOnly="True" SortExpression="Description" HeaderStyle-Width="250px" 
          ItemStyle-Width="250px">
        <HeaderStyle Width="250px" />
        <ItemStyle Width="250px" />
        </asp:BoundField>
        <asp:BoundField DataField="TotalValueWithVAT" HeaderText="Total Value With VAT" 
          SortExpression="TotalValueWithVAT" HeaderStyle-Width="60px" 
          ItemStyle-Width="60px" DataFormatString="{0:N2}" 
          ItemStyle-HorizontalAlign="Right" >
        <HeaderStyle Width="60px" />
        <ItemStyle HorizontalAlign="Right" Width="60px" />
        </asp:BoundField>
        <asp:BoundField DataField="Currency" HeaderText="Currency" ReadOnly="True" 
          SortExpression="Currency" />
        <asp:BoundField DataField="VATpercent" HeaderText="VATpercent" 
          SortExpression="VATpercent" />
        <asp:BoundField DataField="CodeDescription" HeaderText="CodeDescription" ReadOnly="True" 
          SortExpression="CodeDescription"  HeaderStyle-Width="100px" 
          ItemStyle-Width="100px">
  <HeaderStyle Width="100px"></HeaderStyle>

  <ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="PO_Date" HeaderText="PO_Date" ReadOnly="True" 
          SortExpression="PO_Date"  DataFormatString="{0:dd/MM/yyyy}"/>

        <asp:BoundField DataField="RequestedBy" HeaderText="RequestedBy" 
          ReadOnly="True" SortExpression="RequestedBy" HeaderStyle-Width="80px" 
          ItemStyle-Width="80px" >
  <HeaderStyle Width="80px"></HeaderStyle>

  <ItemStyle Width="80px"></ItemStyle>
        </asp:BoundField>

      </Columns>
          <RowStyle  CssClass="GridItemNakladnaya" />
          <HeaderStyle  CssClass="GridHeader" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourcePoToApprove" runat="server" 
      ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
      SelectCommand=" SELECT     (CASE WHEN Table2_PONo.Approved = 1 THEN N'Approved' WHEN Table2_PONo.Approved = 0 THEN N'NotApproved' WHEN Table2_PONo.Approved IS
                         NULL THEN N'Not Required' END) AS ApprovalStatus, RTRIM(dbo.Table2_PONo.PO_No) AS PO_No, RTRIM(dbo.Table6_Supplier.SupplierName) 
                        AS SupplierName, RTRIM(dbo.Table2_PONo.Description) AS Description, dbo.Table2_PONo.TotalPrice AS TotalValueWithVAT, 
                        RTRIM(dbo.Table2_PONo.PO_Currency) AS Currency, dbo.Table2_PONo.VATpercent, RTRIM(dbo.Table7_CostCode.CostCode) 
                        + N' ' + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CodeDescription, (dbo.Table2_PONo.PO_Date) AS PO_Date, 
                        RTRIM(dbo.Table_PersonRequestPo.NameSurname) AS RequestedBy
  FROM         dbo.Table2_PONo INNER JOIN
                        dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                        dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode INNER JOIN
                        dbo.Table_PersonRequestPo ON dbo.Table2_PONo.RequestedBy = dbo.Table_PersonRequestPo.UserName
  WHERE     (RTRIM(dbo.Table2_PONo.PO_No) = @PO_No) ">
      <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="PO_No" />
      </SelectParameters>
    </asp:SqlDataSource>
</asp:Panel>

</asp:Content>

