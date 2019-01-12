<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="MatchBryanToPo.aspx.vb" Inherits="MatchBryanToPo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


                            <asp:DropDownList ID="DropDownListCP" runat="server" 
                                CssClass="DrpDwnListGeneral" Autopostback="true" >
                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                <asp:ListItem Value="C">Contract</asp:ListItem>
                                <asp:ListItem Value="P">Payments</asp:ListItem>
                            </asp:DropDownList>                            

  <asp:DropDownList ID="DropDownListSupplierName" runat="server" Autopostback="true" 
    DataSourceID="SqlDataSourceSupplier" DataTextField="SupplierName" 
    DataValueField="SupplierName" CssClass="DrpDwnListGeneral">
  </asp:DropDownList>
  <asp:SqlDataSource ID="SqlDataSourceSupplier" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT SupplierName from (
SELECT N'' As SupplierName 

UNION ALL

SELECT SupplierName from (
SELECT case when Table_BryanMaster_SavasVersion.SupplierId Is null then
      rtrim(Table_BryanMaster_SavasVersion.SupplierName)
      else
      rtrim(Table6_Supplier.SupplierName)
      end as SupplierName
  FROM [Table_BryanMaster_SavasVersion]
  left join Table6_Supplier on Table6_Supplier.SupplierID = [Table_BryanMaster_SavasVersion].supplierid
) as DataSource1
group by SupplierName 
) AS DataSource2
ORDER BY SupplierName asc"></asp:SqlDataSource>
  <br /> <br />
  <asp:GridView ID="GridViewBryanToPo" runat="server" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSourceBryanFiles" EnableModelValidation="True"
    CssClass="Grid" DataKeyNames="id" EmptyDataText="No data available" >
    <Columns>
      <asp:CommandField ShowEditButton="True"></asp:CommandField>

      <asp:TemplateField >
       <HeaderTemplate>
       
       <table>
        <tr>
         <td style="width: 200px">
         Reference
         </td>
         <td style="width: 60px">
         CP
         <td style="width: 200px">
         Description
         <td style="width: 200px">
         SupplierName
         </td>
         <td style="width: 100px">
         PO_No
         </td>
         <td style="width: 100px">
         Contract Value excVAT Euro
         </td>
         <td style="width: 100px">
         Contract Value wthVAT Euro
         </td>
         <td style="width: 100px">
         Payments excVAT Euro
         </td>
         <td style="width: 100px">
         Note
         </td>
        </tr>
       </table>
       
       </HeaderTemplate>
       
       <itemTemplate>
       <table>
        <tr>
         <td style="width: 200px">
            <asp:label ID="labelreference" runat="server" 
              Text='<%# Bind("Reference") %>'  />
         </td>
         <td style="width: 60px">
            <asp:label ID="labelCP" runat="server" 
              Text='<%# Bind("CP") %>'  />
         <td style="width: 200px">
            <asp:label ID="labelDescription" runat="server" 
              Text='<%# Bind("Description") %>'  />
         <td style="width: 200px">
            <asp:label ID="labelSupplierName" runat="server" 
              Text='<%# Bind("SupplierName") %>'  />
         </td>
         <td style="width: 100px">
            <asp:label ID="labelPoNoItem" runat="server" 
              Text='<%# Bind("PO_No") %>'  />
         </td>
         <td style="width: 100px; text-align: right;">
            <asp:label ID="label2" runat="server" 
              Text='<%# Bind("Contract_Value_excVAT_Euro","{0:###,###,###.00}") %>'  />
         </td>
         <td style="width: 100px; text-align: right;">
            <asp:label ID="label3" runat="server" 
              Text='<%# Bind("Contract_Value_wthVAT_Euro","{0:###,###,###.00}") %>'  />
         </td>
         <td style="width: 100px; text-align: right;">
            <asp:label ID="label4" runat="server" 
              Text='<%# Bind("Payments_excVAT_Euro","{0:###,###,###.00}") %>'  />
         </td>
         <td style="width: 100px">
            <asp:label ID="label5" runat="server" 
              Text='<%# Bind("Note") %>'  />
         </td>
        </tr>
       </table>
       </itemTemplate>
       
       <editItemTemplate>
       <table>
        <tr>
         <td style="width: 200px">
            <asp:label ID="labelreference" runat="server" 
              Text='<%# Bind("Reference") %>'  />
         </td>
         <td style="width: 60px">
            <asp:label ID="labelCP" runat="server" 
              Text='<%# Bind("CP") %>'  />
         </td>
         <td style="width: 200px">
            <asp:label ID="labelDescription" runat="server" 
              Text='<%# Bind("Description") %>'  />
         </td>
         <td style="width: 200px">
            <asp:label ID="labelSupplierName" runat="server" 
              Text='<%# Bind("SupplierName") %>'  />
         </td>
         <td style="width: 100px">
            <asp:label ID="label1" runat="server" 
              Text='<%# Bind("PO_No") %>'   />
         </td>
         <td style="width: 100px; text-align: right;">
            <asp:label ID="label2" runat="server"  BackColor="#FFFF66"
              Text='<%# Bind("Contract_Value_excVAT_Euro","{0:###,###,###.00}") %>'  />
         </td>
         <td style="width: 100px; text-align: right;">
            <asp:label ID="label3" runat="server" 
              Text='<%# Bind("Contract_Value_wthVAT_Euro","{0:###,###,###.00}") %>'  />
         </td>
         <td style="width: 100px; text-align: right;">
            <asp:label ID="label4" runat="server" 
              Text='<%# Bind("Payments_excVAT_Euro","{0:###,###,###.00}") %>'  />
         </td>
         <td style="width: 100px">
            <asp:label ID="label5" runat="server" 
              Text='<%# Bind("Note") %>'  />
         </td>
        </tr>
        <tr>
        <td colspan="9">
                            <asp:DropDownList ID="DropDownListPOnoEdit" runat="server" DataSourceID="SqlDataSourcePoNo" DataTextField="Information" 
                              DataValueField="PO_No" Font-Names="Consolas" Font-Size="11px" Width="1120px" BackColor="#FFFF66">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourcePoNo" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
SelectCommand=" SELECT PO_No, Information  FROM (

SELECT N'' As PO_No, 'Select Po No' as Information, 0 as PoTotalEuroExcVAT

UNION ALL

SELECT            RTRIM(dbo.Table2_PONo.PO_No) AS PO_No, 

                      rtrim(dbo.Table3_Invoice.Invoice_No) + REPLICATE(N'_',20-LEN(rtrim(dbo.Table3_Invoice.Invoice_No))) 
                      
                      +
                      
                      N' | '  + REPLICATE(N'_',13-LEN(rtrim(CONVERT(nvarChar(11), dbo.Table3_Invoice.Invoice_Date)))) + rtrim(CONVERT(nvarChar(11), dbo.Table3_Invoice.Invoice_Date))
                      
                      + 
                      
                      REPLACE(RTRIM(dbo.Table2_PONo.PO_No) + N' ' + RIGHT('000000000000000000000000000000000000000000000000000000000000000000000000000'
                       + CAST(dbo.Table2_PONo.Description AS varchar(75)), 75)   
                      
                      + 
                      
                      REPLICATE(N'_',15-LEN(rtrim(CASE LEN(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT))) 
                      WHEN 7 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 1) + ',' + RIGHT(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3.PoTotalEuroExcVAT)), 6) WHEN 8 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 2) 
                      + ',' + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 6) WHEN 9 THEN LEFT(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3.PoTotalEuroExcVAT)), 3) + ',' + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 6) 
                      WHEN 10 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 1) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3.PoTotalEuroExcVAT)), 2, 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 5, 3) 
                      + '.' + + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 2) WHEN 11 THEN LEFT(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3.PoTotalEuroExcVAT)), 2) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 3, 3) 
                      + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 6, 3) + '.' + + RIGHT(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3.PoTotalEuroExcVAT)), 2) WHEN 12 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 3) 
                      + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 4, 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3.PoTotalEuroExcVAT)), 7, 3) + '.' + + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 2) 
                      ELSE rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)) END))) + rtrim(CASE LEN(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT))) 
                      WHEN 7 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 1) + ',' + RIGHT(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3.PoTotalEuroExcVAT)), 6) WHEN 8 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 2) 
                      + ',' + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 6) WHEN 9 THEN LEFT(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3.PoTotalEuroExcVAT)), 3) + ',' + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 6) 
                      WHEN 10 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 1) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3.PoTotalEuroExcVAT)), 2, 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 5, 3) 
                      + '.' + + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 2) WHEN 11 THEN LEFT(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3.PoTotalEuroExcVAT)), 2) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 3, 3) 
                      + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 6, 3) + '.' + + RIGHT(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3.PoTotalEuroExcVAT)), 2) WHEN 12 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 3) 
                      + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 4, 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3.PoTotalEuroExcVAT)), 7, 3) + '.' + + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)), 2) 
                      ELSE rtrim(CONVERT(nchar(20), dbo.View_QryW3.PoTotalEuroExcVAT)) END)
                      
                   
                      +
                      
                      'Euro Exc. VAT'
                      
                      +
                                          
                       N' | ' + CASE LEN(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3_PoDetail.OrderValueEuro))) WHEN 7 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)), 1) 
                      + ',' + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)), 6) WHEN 8 THEN LEFT(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3_PoDetail.OrderValueEuro)), 2) + ',' + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)), 6) 
                      WHEN 9 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)), 3) + ',' + RIGHT(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3_PoDetail.OrderValueEuro)), 6) WHEN 10 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)), 1) 
                      + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)), 2, 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3_PoDetail.OrderValueEuro)), 5, 3) + '.' + + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)), 2) 
                      WHEN 11 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)), 2) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3_PoDetail.OrderValueEuro)), 3, 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)), 6, 3) 
                      + '.' + + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)), 2) WHEN 12 THEN LEFT(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3_PoDetail.OrderValueEuro)), 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)), 4, 3) 
                      + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)), 7, 3) + '.' + + RIGHT(rtrim(CONVERT(nchar(20), 
                      dbo.View_QryW3_PoDetail.OrderValueEuro)), 2) ELSE rtrim(CONVERT(nchar(20), dbo.View_QryW3_PoDetail.OrderValueEuro)) 
                      END 
                      
                      +
                      
                      N' | ',N' ',N'_') AS information, dbo.View_QryW3.PoTotalEuroExcVAT 

						
                      
FROM         dbo.Table2_PONo INNER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No INNER JOIN
                      dbo.View_QryW3 ON dbo.Table2_PONo.PO_No = dbo.View_QryW3.PO_No INNER JOIN
                      dbo.View_QryW3_PoDetail ON dbo.Table3_Invoice.InvoiceID = dbo.View_QryW3_PoDetail.InvoiceID INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID
WHERE     (dbo.Table6_Supplier.SupplierName = @SupplierName) AND ((dbo.Table2_PONo.Project_ID = 108) OR
                      (dbo.Table2_PONo.Project_ID = 140)) AND 
                      dbo.Table2_PONo.PO_No NOT IN (select PO_No from Table_BryanMaster_SavasVersion
                                                    Where LEN(rtrim(po_no)) > 0)
                        ) AS DataSource1 
                        ORDER BY PoTotalEuroExcVAT ASC">
                        <SelectParameters>
                         <asp:Parameter Name="SupplierName" Type="String" />
                        </SelectParameters>
                      </asp:SqlDataSource>
        </td>
        </tr>
       </table>
       </editItemTemplate>
      </asp:TemplateField>
    </Columns>
        <RowStyle  CssClass="GridItemNakladnaya" />
        <HeaderStyle  CssClass="GridHeader" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceBryanFiles" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand= "  SELECT top 5 [id]  
                 ,Table_BryanMaster_SavasVersion.SupplierID 
                 ,(N'_'+[Reference]) AS Reference  
                 ,[CP] 
                 ,[Description] 
                 ,case when 
                 Table_BryanMaster_SavasVersion.SupplierId Is null then 
                 rtrim(Table_BryanMaster_SavasVersion.SupplierName) 
                 else 
                 rtrim(Table6_Supplier.SupplierName) 
                 end as SupplierName 
                 ,[PO_No] 
                 ,[Contract_Value_excVAT_Euro] 
                 ,[Contract_Value_wthVAT_Euro] 
                 ,[Payments_excVAT_Euro] 
                 ,[Note] 
             FROM [Table_BryanMaster_SavasVersion] 
             left join Table6_Supplier on Table6_Supplier.SupplierID = [Table_BryanMaster_SavasVersion].supplierid 
             WHERE (( case when Table_BryanMaster_SavasVersion.SupplierId Is null then rtrim(Table_BryanMaster_SavasVersion.SupplierName) else rtrim(Table6_Supplier.SupplierName) end ) LIKE '%' + @SupplierName + '%') 
             AND (CP LIKE '%' + @CP + '%') aND (len(PO_No)=0 OR PO_No is null )
           order by SupplierName asc "
    UpdateCommand=" UPDATE Table_BryanMaster_SavasVersion
                    SET  PO_No=@PO_No WHERE id=@id ">
      <SelectParameters>
          <asp:ControlParameter ControlID="DropDownListCP" DefaultValue="-" Name="CP" 
              PropertyName="SelectedValue" ConvertEmptyStringToNull="False" />
          <asp:ControlParameter ControlID="DropDownListSupplierName" DefaultValue="-" 
              Name="SupplierName" PropertyName="SelectedValue" ConvertEmptyStringToNull="False" />
      </SelectParameters>
   </asp:SqlDataSource>

</asp:Content>

