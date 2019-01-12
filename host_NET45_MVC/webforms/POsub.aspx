<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="POsub.aspx.vb" Inherits="POsub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <asp:GridView ID="GridViewPoInfo" runat="server" DataSourceID="SqlDataSourcePoInfo" 
    EnableModelValidation="True" AutoGenerateColumns="False" DataKeyNames="PO_No" CssClass="Grid" >
            <Columns>
              <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" 
                 HeaderStyle-Width="80px" ItemStyle-Width="80px"/>

              <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ReadOnly="True" 
                 HeaderStyle-Width="100px" ItemStyle-Width="100px"/>

              <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" 
                 HeaderStyle-Width="200px" ItemStyle-Width="200px"/>

              <asp:BoundField DataField="CostCode" HeaderText="CostCode" ReadOnly="True" 
                 HeaderStyle-Width="80px" ItemStyle-Width="80px"/>

              <asp:BoundField DataField="CodeDescription" HeaderText="CodeDescription" ReadOnly="True" 
                 HeaderStyle-Width="100px" ItemStyle-Width="100px"/>

              <asp:BoundField DataField="TotalPrice" HeaderText="Total Po Value With VAT" ReadOnly="True" 
                 HeaderStyle-Width="80px" ItemStyle-Width="80px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>

              <asp:BoundField DataField="PO_Currency" HeaderText="" ReadOnly="True" />

              <asp:BoundField DataField="CollectedValue" HeaderText="Collected Documents In Rub With.VAT" ReadOnly="True" 
                 HeaderStyle-Width="80px" ItemStyle-Width="80px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
            </Columns>
        <RowStyle  CssClass="GridItemNakladnaya" />
        <HeaderStyle  CssClass="GridHeader" />
  </asp:GridView>
  
  <asp:SqlDataSource ID="SqlDataSourcePoInfo" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand=" SELECT     dbo.Table2_PONo.PO_No, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, RTRIM(dbo.Table2_PONo.Description) AS Description, 
                      RTRIM(dbo.Table2_PONo.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CodeDescription) AS CodeDescription, dbo.Table2_PONo.TotalPrice, 
                      RTRIM(dbo.Table2_PONo.PO_Currency) AS PO_Currency, dbo.View_PO_CollectedDocsRubWithVAT.SumCollected AS CollectedValue
FROM         dbo.Table6_Supplier INNER JOIN
                      dbo.Table2_PONo ON dbo.Table6_Supplier.SupplierID = dbo.Table2_PONo.SupplierID INNER JOIN
                      dbo.Table7_CostCode ON dbo.Table2_PONo.CostCode = dbo.Table7_CostCode.CostCode LEFT OUTER JOIN
                      dbo.View_PO_CollectedDocsRubWithVAT ON dbo.Table2_PONo.PO_No = dbo.View_PO_CollectedDocsRubWithVAT.PO_No LEFT OUTER JOIN
                      dbo.Table4_PaymentRequest LEFT OUTER JOIN
                      dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo RIGHT OUTER JOIN
                      dbo.Table3_Invoice ON dbo.Table4_PaymentRequest.InvoiceID = dbo.Table3_Invoice.InvoiceID ON 
                      dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No
GROUP BY dbo.Table2_PONo.PO_No, RTRIM(dbo.Table6_Supplier.SupplierName), RTRIM(dbo.Table2_PONo.Description), RTRIM(dbo.Table2_PONo.CostCode), 
                      RTRIM(dbo.Table7_CostCode.CodeDescription), dbo.Table2_PONo.TotalPrice, RTRIM(dbo.Table2_PONo.PO_Currency), 
                      dbo.View_PO_CollectedDocsRubWithVAT.SumCollected
HAVING      (dbo.Table2_PONo.PO_No = @PO_No) ">
    <SelectParameters>
      <asp:QueryStringParameter DefaultValue="0" Name="PO_No" 
        QueryStringField="PO_No" />
    </SelectParameters>
  </asp:SqlDataSource>
  

  <asp:SqlDataSource ID="SqlDataSourceCostCode" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand=" SELECT *
                                                        FROM
                                                        (SELECT * FROM
                                                        (SELECT TOP 1 RTRIM([CostCode]) AS CostCode
                                                              ,rtrim(CostCode) + replicate(char(160),12-len(CostCode)) + RTRIM(CodeDescription) AS CostCode_Description
                                                          FROM [dbo].[Table7_CostCode]
                                                          ORDER BY [CostCode] ASC) AS A

                                                        UNION ALL

                                                        SELECT     RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 12 - LEN(dbo.Table7_CostCode.CostCode)) 
                                                                              + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description
                                                        FROM         dbo.aspnet_UsersInRoles INNER JOIN
                                                                              dbo.aspnet_Users ON dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId INNER JOIN
                                                                              dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId INNER JOIN
                                                                              dbo.Table7_CostCode ON dbo.aspnet_Roles.RoleName = dbo.Table7_CostCode.Type
                                                        WHERE     (dbo.aspnet_Users.UserName = @Username) AND (dbo.Table7_CostCode.Type = N'Finance')

                                                        UNION ALL

                                                        SELECT * FROM
                                                        (SELECT     RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 12 - LEN(dbo.Table7_CostCode.CostCode)) 
                      + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description
FROM         dbo.Table_Budget INNER JOIN
                      dbo.Table7_CostCode ON dbo.Table_Budget.CostCode = dbo.Table7_CostCode.CostCode INNER JOIN
                      dbo.Table7_CostDivision ON dbo.Table7_CostCode.CostVidisionID = dbo.Table7_CostDivision.CostVidisionID
WHERE     (dbo.Table_Budget.ProjectID = @ProjectID) AND (dbo.Table7_CostCode.Type <> N'Finance')
GROUP BY dbo.Table7_CostCode.CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 12 - LEN(dbo.Table7_CostCode.CostCode)) 
                      + RTRIM(dbo.Table7_CostCode.CodeDescription)) AS B) AS C
                                                        ORDER BY [CostCode] ASC ">
    <SelectParameters>
      <asp:Parameter Name="ProjectID" />
      <asp:Parameter Name="username" />
    </SelectParameters>
  </asp:SqlDataSource>

  <br />

  <table style="padding: 3px; margin: 3px; width:70%; font-size: 10px; background-color: #CCCCCC;">
    <tr>
     <td colspan=4 style="text-align: center; font-weight: bold; font-size: 12px;">
      You can create your SUB-purchase Orders by using this section
     </td>
    </tr>
    <tr>
      <td>
        SubPo Total</td>
      <td>
        Cost Code</td>
      <td>
        Collected Doc</td>
      <td>
      </td>
    </tr>
    <tr>
      <td>
        <asp:TextBox ID="TextBoxPoTotal" runat="server" Font-Size="10px"></asp:TextBox>
      </td>
      <td>

  <asp:DropDownList ID="DropDownListCostCode" runat="server" 
    DataSourceID="SqlDataSourceCostCode" DataTextField="CostCode_Description" 
    DataValueField="CostCode" Font-Size="10px">
  </asp:DropDownList>
      </td>
      <td>
        <asp:TextBox ID="TextBoxCollectedDocument" runat="server" Font-Size="10px"></asp:TextBox>
      </td>
      <td>

        <asp:LinkButton ID="LinkButtonInsert" runat="server" CssClass="btn btn-mini"
          ValidationGroup="InsertSubPo">Insert</asp:LinkButton>
  
      </td>
    </tr>
    <tr>
      <td>
         <asp:RequiredFieldValidator ID="RequiredFieldValidatorTotalPrice" 
           runat="server"  Display="Dynamic"
         ErrorMessage="Required" ControlToValidate="TextBoxPoTotal" 
           ValidationGroup="InsertSubPo"></asp:RequiredFieldValidator>
         <asp:RegularExpressionValidator ID="RegularExpressionValidatorTotalPrice" 
           ControlToValidate="TextBoxPoTotal" Display="Dynamic"
         runat="server" ErrorMessage="Wrong format"  
           ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
           ValidationGroup="InsertSubPo"></asp:RegularExpressionValidator>
      </td>
      <td>
        <asp:CompareValidator ID="CompareValidatorCostCode" runat="server" 
          ControlToValidate="DropDownListCostCode" ErrorMessage="Select CostCode" 
          Operator="NotEqual" ValidationGroup="InsertSubPo" ValueToCompare="0"></asp:CompareValidator>
      </td>
      <td>
       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
          ControlToValidate="TextBoxCollectedDocument" Display="Dynamic"
       runat="server" ErrorMessage="Wrong format"  
          ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
          ValidationGroup="InsertSubPo"></asp:RegularExpressionValidator>
      </td>
      <td>
      </td>
    </tr>
  </table>
<br />
<asp:label id="labelOutstandingPOvalue" runat="server"  ></asp:label>
  <br />
<asp:label id="labelOutstandingCollectedValue" runat="server" ></asp:label>

  <asp:GridView ID="GridViewSubPo" runat="server" DataSourceID="SqlDataSourceSubPo" 
    EnableModelValidation="True" AutoGenerateColumns="False" 
    DataKeyNames="SubPO_No" CssClass="Grid" EmptyDataText="No Sub Po defined yet" >
            <Columns>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral"
                        CommandName="Update" Text="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                    OnClientClick="return confirm('Are you sure you want to delete this record?');"
                    CommandName="Delete" Text="Delete" CssClass="LabelGeneral" CausesValidation="False"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:BoundField DataField="SubPO_No" HeaderText="SubPO_No" ReadOnly="True" 
                 HeaderStyle-Width="100px" ItemStyle-Width="100px"/>



            <asp:TemplateField HeaderText="TotalPrice" SortExpression="TotalPrice" ItemStyle-HorizontalAlign="Right" ControlStyle-Width="100" HeaderStyle-Width="100">
                <EditItemTemplate>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorTotalPrice" ControlToValidate="TextBoxTotalPrice" Display="Dynamic"
                    runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="TextBoxTotalPrice" runat="server" Text='<%# Bind("TotalPrice") %>' CssClass="FontSizeBGcolorForEditControls"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTotalPrice" runat="server"  Display="Dynamic"
                    ErrorMessage="Required" ControlToValidate="TextBoxTotalPrice"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelTotalPriceItem" runat="server" Text='<%# Bind("TotalPrice","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>



            <asp:TemplateField HeaderText="Code" SortExpression="CostCode" >
                <EditItemTemplate>
                        <asp:DropDownList ID="DropDownListCostCodeEdit" runat="server"  
                            DataSourceID="SqlDataSourceCostCodeEdit" DataTextField="CostCode_Description" 
                            DataValueField="CostCode"  Font-Size="10px"
                            AutoPostBack="False" BackColor="#CCFFFF">
                        </asp:DropDownList>

  <asp:SqlDataSource ID="SqlDataSourceCostCodeEdit" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand=" SELECT *
                                                        FROM
                                                        (
                                                        SELECT     RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 12 - LEN(dbo.Table7_CostCode.CostCode)) 
                                                                              + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description
                                                        FROM         dbo.aspnet_UsersInRoles INNER JOIN
                                                                              dbo.aspnet_Users ON dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_UsersInRoles.UserId = dbo.aspnet_Users.UserId INNER JOIN
                                                                              dbo.aspnet_Roles ON dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId AND dbo.aspnet_UsersInRoles.RoleId = dbo.aspnet_Roles.RoleId INNER JOIN
                                                                              dbo.Table7_CostCode ON dbo.aspnet_Roles.RoleName = dbo.Table7_CostCode.Type
                                                        WHERE     (dbo.aspnet_Users.UserName = @Username) AND (dbo.Table7_CostCode.Type = N'Finance')

                                                        UNION ALL

                                                        SELECT * FROM
                                                        (SELECT     RTRIM(dbo.Table7_CostCode.CostCode) AS CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 12 - LEN(dbo.Table7_CostCode.CostCode)) 
                      + RTRIM(dbo.Table7_CostCode.CodeDescription) AS CostCode_Description
FROM         dbo.Table_Budget INNER JOIN
                      dbo.Table7_CostCode ON dbo.Table_Budget.CostCode = dbo.Table7_CostCode.CostCode INNER JOIN
                      dbo.Table7_CostDivision ON dbo.Table7_CostCode.CostVidisionID = dbo.Table7_CostDivision.CostVidisionID
WHERE     (dbo.Table_Budget.ProjectID = @ProjectID) AND (dbo.Table7_CostCode.Type <> N'Finance')
GROUP BY dbo.Table7_CostCode.CostCode, RTRIM(dbo.Table7_CostCode.CostCode) + REPLICATE(CHAR(160), 12 - LEN(dbo.Table7_CostCode.CostCode)) 
                      + RTRIM(dbo.Table7_CostCode.CodeDescription)) AS B) AS C
                                                        ORDER BY [CostCode] ASC ">
    <SelectParameters>
      <asp:Parameter Name="ProjectID" />
      <asp:Parameter Name="username" />
    </SelectParameters>
  </asp:SqlDataSource>                        
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelItemCostCode" runat="server" Text='<%# Bind("CostCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>




            <asp:TemplateField HeaderText="CollectedValue" SortExpression="CollectedValue" ItemStyle-HorizontalAlign="Right" ControlStyle-Width="100" HeaderStyle-Width="100">
                <EditItemTemplate>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorCollectedValue" ControlToValidate="TextBoxCollectedValue" Display="Dynamic"
                    runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="TextBoxCollectedValue" runat="server" Text='<%# Bind("CollectedValue") %>' CssClass="FontSizeBGcolorForEditControls"></asp:TextBox>

                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelCollectedValueItem" runat="server" Text='<%# Bind("CollectedValue","{0:###,###,###.00}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>



            </Columns>
        <RowStyle  CssClass="GridItemNakladnaya" />
        <HeaderStyle  CssClass="GridHeader" />
  </asp:GridView>

  <asp:SqlDataSource ID="SqlDataSourceSubPo" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand=" SELECT [SubPO_No]
                    ,[TotalPrice]
                    ,RTRIM([CostCode]) AS CostCode
                    ,[CollectedValue]
                      FROM [Table2_PONo_Sub]
                    WHERE PO_No = @PO_No
                    ORDER BY [SubPO_No] " 
    DeleteCommand="DELETE FROM [Table2_PONo_Sub]
                   WHERE [SubPO_No] = @SubPO_No"
    UpdateCommand=" UPDATE [Table2_PONo_Sub]
                   SET [TotalPrice] = @TotalPrice
                      ,[CostCode] = @CostCode
                      ,[CollectedValue] = @CollectedValue
                   WHERE [SubPO_No] = @SubPO_No " >
    <SelectParameters>
      <asp:QueryStringParameter DefaultValue="0" Name="PO_No" 
        QueryStringField="PO_No" />
    </SelectParameters>
    <UpdateParameters>
      <asp:Parameter Name="CostCode" />
    </UpdateParameters>
  </asp:SqlDataSource>
  
  <asp:Panel ID="panelHidden" runat="server" CssClass="hidepanel" >
      <asp:DropDownList ID="DropDownListTotalPoValue" runat="server"
       DataSourceID="SqlDataSourceTotalPoValue" DataTextField="TotalPrice"
       DataValueField="TotalPrice">
      </asp:DropDownList>
      <asp:SqlDataSource ID="SqlDataSourceTotalPoValue" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" SELECT  TotalPrice
                        FROM dbo.Table2_PONo
                        WHERE PO_No = @PO_No " >
        <SelectParameters>
          <asp:QueryStringParameter DefaultValue="0" Name="PO_No" 
            QueryStringField="PO_No" />
        </SelectParameters>
      </asp:SqlDataSource>

      <asp:DropDownList ID="DropDownListTotalSubPoValue" runat="server"
       DataSourceID="SqlDataSourceTotalSuPoValue" DataTextField="TotalPrice"
       DataValueField="TotalPrice">
      </asp:DropDownList>
      <asp:SqlDataSource ID="SqlDataSourceTotalSuPoValue" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" SELECT     (CASE WHEN SUM(dbo.Table2_PONo_Sub.TotalPrice) IS NULL THEN 0 ELSE SUM(dbo.Table2_PONo_Sub.TotalPrice) END) AS TotalPrice
    FROM         dbo.Table2_PONo INNER JOIN
                          dbo.Table2_PONo_Sub ON dbo.Table2_PONo.PO_No = dbo.Table2_PONo_Sub.PO_No
                        WHERE Table2_PONo.PO_No = @PO_No " >
        <SelectParameters>
          <asp:QueryStringParameter DefaultValue="0" Name="PO_No" 
            QueryStringField="PO_No" />
        </SelectParameters>
      </asp:SqlDataSource>

      <asp:DropDownList ID="DropDownListTotalCollectedDocuments" runat="server"
       DataSourceID="SqlDataSourceTotalCollectedDocuments" DataTextField="CollectedValue"
       DataValueField="CollectedValue">
      </asp:DropDownList>
      <asp:SqlDataSource ID="SqlDataSourceTotalCollectedDocuments" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" SELECT     dbo.Table2_PONo.PO_No, SUM(CASE WHEN SumCollected IS NULL THEN 0 ELSE SumCollected END) AS CollectedValue
                        FROM         dbo.View_PO_CollectedDocsRubWithVAT RIGHT OUTER JOIN
                                              dbo.Table2_PONo ON dbo.View_PO_CollectedDocsRubWithVAT.PO_No = dbo.Table2_PONo.PO_No
                        GROUP BY dbo.Table2_PONo.PO_No
                        HAVING      (dbo.Table2_PONo.PO_No = @PO_No) " >
        <SelectParameters>
          <asp:QueryStringParameter DefaultValue="0" Name="PO_No" 
            QueryStringField="PO_No" />
        </SelectParameters>
      </asp:SqlDataSource>

      <asp:DropDownList ID="DropDownListTotalSubPoCollectedDocuments" runat="server"
       DataSourceID="SqlDataSourceTotalSubPoCollectedDocuments" DataTextField="CollectedValue"
       DataValueField="CollectedValue">
      </asp:DropDownList>
      <asp:SqlDataSource ID="SqlDataSourceTotalSubPoCollectedDocuments" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" SELECT     (CASE WHEN SUM(dbo.Table2_PONo_Sub.CollectedValue) IS NULL THEN 0 ELSE SUM(dbo.Table2_PONo_Sub.CollectedValue) END) AS CollectedValue
                        FROM         dbo.Table2_PONo INNER JOIN
                                              dbo.Table2_PONo_Sub ON dbo.Table2_PONo.PO_No = dbo.Table2_PONo_Sub.PO_No
                        WHERE     (dbo.Table2_PONo.PO_No = @PO_No) " >
        <SelectParameters>
          <asp:QueryStringParameter DefaultValue="0" Name="PO_No" 
            QueryStringField="PO_No" />
        </SelectParameters>
      </asp:SqlDataSource>
  </asp:Panel>

</asp:Content>

