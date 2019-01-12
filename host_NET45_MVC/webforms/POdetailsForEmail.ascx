<%@ Control Language="VB" AutoEventWireup="false" CodeFile="POdetailsForEmail.ascx.vb" Inherits="POdetailsForEmail" %>

<asp:GridView ID="GridViewPoToApprove" runat="server" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSourcePoToApprove" EnableModelValidation="True" 
    CellPadding="3"  >
    <Columns>

      <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" 
        SortExpression="PO_No"  HeaderStyle-Width="80px" ItemStyle-Width="80px">

<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="80px"></ItemStyle>
      </asp:BoundField>

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

    <HeaderStyle BackColor="LightSkyBlue" Font-Size="11px" />
    <RowStyle BackColor="GhostWhite" BorderColor="#CCCCCC" BorderStyle="Solid" 
      BorderWidth="1px" Font-Size="11px" />

  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourcePoToApprove" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand=" SELECT     (CASE WHEN Table2_PONo.Approved = 1 THEN N'Approved' WHEN Table2_PONo.Approved = 0 THEN N'NotApproved' WHEN Table2_PONo.Approved IS NULL 
                                      THEN N'Not Required' END) AS ApprovalStatus, RTRIM(dbo.Table2_PONo.PO_No) AS PO_No, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, 
                                      RTRIM(dbo.Table2_PONo.Description) AS Description, dbo.Table2_PONo.TotalPrice AS TotalValueWithVAT, RTRIM(dbo.Table2_PONo.PO_Currency) AS Currency, 
                                      dbo.Table2_PONo.VATpercent, RTRIM(dbo.Table7_CostCode.CostCode) + N' ' + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CodeDescription, 
                                      dbo.Table2_PONo.PO_Date, CASE WHEN RTRIM(dbo.Table_PersonRequestPo.NameSurname) IS NULL 
                                      THEN N'-' ELSE RTRIM(dbo.Table_PersonRequestPo.NameSurname) END AS RequestedBy
                    FROM         dbo.Table2_PONo INNER JOIN
                                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                                      dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode INNER JOIN
                                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID LEFT OUTER JOIN
                                      dbo.Table_PersonRequestPo ON dbo.Table1_Project.ProjectID = dbo.Table_PersonRequestPo.ProjectID AND 
                                      dbo.Table2_PONo.RequestedBy = dbo.Table_PersonRequestPo.UserName
                    WHERE     (RTRIM(dbo.Table2_PONo.PO_No) = @PO_No) ">
    <SelectParameters>
      <asp:Parameter Name="PO_No" Type="String"  />
    </SelectParameters>
  </asp:SqlDataSource>

