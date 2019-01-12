<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WebUserControl_ContractEmailBody.ascx.vb" Inherits="WebUserControl_ContractEmailBody" %>

<style type="text/css">

    .table_format {
        border: thin solid #00BFFF;	
        background-color: #F8F8FF;
    }

        .table_format td {
            padding:6px;
        }

        .pnl_newsup {
            margin:1px; padding:2px; font-size:8px; color:#E74C3C;
            background-color:#FDEDEC;border-color:Black;border-style:Solid; 
            border-width:1px; font-weight:bold;width:100px;text-align:center;
        }

</style>

    <asp:FormView ID="FormViewContractEmailBody" runat="server"  
        DataSourceID="SqlDataSourceContractEmailBody" AllowPaging="True" 
        DataKeyNames="ContractID" DefaultMode="ReadOnly" Font-Size="11px">

       <ItemTemplate>
        <table>
        <tr>
        <td>
        <table class="table_format">
                <tr>
                    <td style="width:150px; font-weight:bold;">
                            <asp:Label ID="LabelProject" runat="server" 
                            Text="проект" Width="100px" />
                    </td>
                    <td style="width:200px; ">
                            <asp:Label ID="TextBoxProjectName" runat="server"  
                            Text='<%# Bind("ProjectName") %>' />

                    </td>
                </tr>
                <tr>
                    <td style="width:150px; font-weight:bold;">
                        
                        <asp:Label ID="LabelSupplierName" runat="server"  
                            Text="поставщик" Width="100px" />
                    </td>
                    <td style="width:200px; ">

                       <asp:Label ID="x" runat="server"   Text='<%# Bind("SupplierID") %>'/>
                                
                    </td>
                </tr>
            </table>

        </td>
        <td>
            <asp:panel id="panelNewSupplier" CssClass="pnl_newsup" runat="server" Visible="false">
                <%= BodyTexts.Ref("qcBhmdE2a0efLQGscCaCiQ")%>
            </asp:panel>

        </td>
        </tr>
        </table>
        
            <br />
        
            <table class="table_format">
                <tr>
                    <td style="width:150px;  font-weight:bold;">
                        
                        <asp:Label ID="LabelInsert4" runat="server"  
                            Text="Номер контракта" Width="100px" />
                        
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="ContractNoTextBox" runat="server"  
                            Text='<%# Bind("ContractNo") %>' />
                    </td>
                    <td style="width:150px;  font-weight:bold;">
                    </td>
                    <td style="width:200px;  ">

                    </td>
                </tr>
                <tr>
                    <td style="width:150px;  font-weight:bold;">
                        
                        <asp:Label ID="LabelInsert5" runat="server"  
                            Text="Дата контракта" Width="100px" />
                        
                    </td>
                    <td style="width:200px;  ">
                        
                        <asp:Label ID="ContractDateTextBox" runat="server" 
                         Text='<%# Bind("ContractDate","{0:dd/MM/yyyy}")%>' />

                    </td>
                    <td style="width:150px;  font-weight:bold;">
                        
                        <asp:Label ID="LabelInsertStartDate" runat="server"  
                          Text="Дата начала" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxStartDate" runat="server"  
                        Text='<%# Bind("StartDate","{0:dd/MM/yyyy}")%>'/>

                    </td>
                </tr>
                <tr>
                    <td style="width:150px;  font-weight:bold;">
                        
                        <asp:Label ID="LabelInsert6" runat="server"  
                            Text="Стоимость контракта с НДС" Width="120px" />
                        
                    </td>
                    <td style="width:200px;  ">
                        
                        <asp:Label ID="TextBoxContractValue_withVAT" runat="server" 
                             Text='<%# Bind("ContractValue_withVAT","{0:###,###,###.00}") %>' />

                        
                    </td>
                    <td style="width:150px;  font-weight:bold;">
                      <asp:Label ID="LabelInsertFinishDate" runat="server"  
                        Text="Дата окончания" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxFinishDate" runat="server"  
                        Text='<%# Bind("FinishDate","{0:dd/MM/yyyy}")%>'/>

                    </td>
                </tr>
                <tr>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelVAT" runat="server"  
                            Text="НДС %" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxVAT" runat="server" 
                             Text='<%# Bind("VATpercent") %>' />
                    </td>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelInsertAdvance" runat="server" Text="авансировать %" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxAdvance" runat="server" Text='<%# Bind("Advance") %>' />
                    </td>
                </tr>
                <tr>
                    <td style="width:150px;  font-weight:bold; ">
                        <asp:Label ID="LabelInsertBudget" runat="server" Text="Бюджет с НДС" Width="100px" Visible="false" />
                    </td>
                    <td style="width:200px; ">
                        <asp:Label ID="TextBoxBudgetValue" runat="server" Text='<%# Bind("Budget", "{0:###,###,###.00}")%>' Visible="false" />

                        <asp:ImageButton ID="ImageButtonBudgetPDF" runat="server" data-rel="tooltip" data-placement="top" class="tooltip-success" data-original-title="Budget Document"
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BudgetLinkToPDF")%>' Visible="false"
                        CommandName="OpenBudgetPDF" />

                    </td>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelInsertInterim" runat="server" Text="промежуточный %" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxInterim" runat="server" Text='<%# Bind("Interim")%>' />

                    </td>
                </tr>
                <tr>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelInsert7" runat="server" Text="Контрактная валюта" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="ContractCurrencyTextBox" runat="server" Text='<%# Bind("ContractCurrency") %>' />
                    </td>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelInsertShipment" runat="server" Text="отгрузка %" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxShipment" runat="server" Text='<%# Bind("Shipment")%>' />
                    </td>
                </tr>
                <tr>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelInsertCostCode" runat="server" Text="Код затрат" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxCostCode" runat="server" Text='<%# Bind("CostCode") %>' />
                    </td>
                    <td style="width:150px;  font-weight:bold;">

                        <asp:Label ID="LabelInsertDelivery" runat="server" Text="Доставка %" Width="100px" />

                    </td>
                        <td style="width:200px;  ">

                            <asp:Label ID="TextBoxDelivery" runat="server" Text='<%# Bind("Delivery")%>' />

                        </td>
                </tr>
                <tr>
                    <td style="width:150px;  font-weight:bold;">&nbsp;</td>
                    <td style="width:200px;  ">&nbsp;</td>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelInsertRetention" runat="server" Text="удержание %" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxRetention" runat="server" Text='<%# Bind("Retention") %>' />
                    </td>
                </tr>
                <tr>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelInsertPenalty" runat="server" Text="Наказание К нам" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxPenalties" runat="server" Text='<%# Bind("Penalties") %>' />
                    </td>
                    <td style="width:150px;  font-weight:bold;">
                        Заметка</td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxPenaltyNote" runat="server" Text='<%# Bind("PenaltiesNote")%>' />
                    </td>
                </tr>
                <tr>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelInsertPenaltySupplier" runat="server" Text="Штраф за Поставщика" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxPenaltiesToSupplier" runat="server" Text='<%# Bind("PenaltiesToSupplier")%>' />
                    </td>
                    <td style="width:150px;  font-weight:bold;">
                        Заметка</td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxPenaltyToSupplierNote" runat="server" Text='<%# Bind("PenaltiesToSupplierNote")%>' />
                    </td>
                </tr>
                <tr>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelRequestedBy" runat="server" Text="Запрошенный" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxRequestedBy" runat="server" Text='<%# Bind("RequestedBy") %>' />
                    </td>
                    <td style="width:150px;  font-weight:bold;">&nbsp;</td>
                    <td style="width:200px;  ">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelInsert8" runat="server" Text="Форма контракта" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="ContractTypeTextBox" runat="server" Text='<%# Bind("ContractType") %>' />
                    <td style="width:150px;  font-weight:bold;">

                      <asp:Label ID="LabelInsertDeliveryTerms" runat="server" 
                         Text="Условия доставки" Width="100px" />

                    </td>
                        <td style="width:200px;  ">
                        <asp:Label ID="TextBoxDeliveryTerms" runat="server" 
                             Text='<%# Bind("DeliveryTerms") %>' />
                            <br />
                    </td>
                </tr>
                <tr>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelInsert9" runat="server" Text="Описание контракта" Width="120px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="ContractDescriptionTextBox" runat="server" Text='<%# Bind("ContractDescription") %>' />
                    </td>
                    <td style="width:150px;  font-weight:bold;">
                        <asp:Label ID="LabelInsertGuarantePeriod" runat="server" Text="Гарантийный срок" Width="100px" />
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="TextBoxGuaranteePeriod" runat="server" Text='<%# Bind("GuaranteePeriod") %>' />
                    </td>
                </tr>
                <tr id="ClientAdditionalData" visible="false" runat="server">
                    <td style="width:150px;  font-weight:bold;">
                        Дата завершения 
                    </td>
                    <td style="width:200px;">
                        <asp:Label ID="LabelCompletionDate" runat="server"  />
                    </td>
                    <td style="width:150px;  font-weight:bold;">
                        Акт работы
                    </td>
                    <td style="width:200px;  ">
                        <asp:Label ID="LabelAktOfWork" runat="server"  />
                    </td>

                </tr>
                <tr style="font-weight:bold; background-color: #FFFF00; border-bottom-style: dashed; border-bottom-width:thick; border-bottom-color: #000000">
                    <td colspan="4">
                        <asp:Label ID="LabelReasonTitle" runat="server" Text='<%# Bind("ReasonTitle")%>' />
                    </td>
                </tr>
                <tr >
                    <td colspan="4">
                        <asp:Label ID="LabelLarisaComments" runat="server" Text='<%# Bind("Remark_From_Larisa")%>' />
                    </td>
                </tr>
            </table>

       </ItemTemplate>

    </asp:FormView>

    <asp:SqlDataSource ID="SqlDataSourceContractEmailBody" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand="

declare @ContractLeadPerson nvarChar(30) = (select dbo.NameOfLeadLawyer())

IF EXISTS (SELECT * FROM dbo.Table_Contract_UsersApprv WHERE ContractID = @ContractID AND UserName = N'lawyers' AND Exception = 1 )
BEGIN
	IF EXISTS (SELECT * FROM dbo.Table_Contracts_UserRemarks WHERE ContractID = @ContractID AND UserName = @ContractLeadPerson)
	  BEGIN 
		SELECT     dbo.Table_Contracts.ContractID, RTRIM(dbo.Table1_Project.ProjectName) + N' - ' + CONVERT(NvarChar(4), dbo.Table_Contracts.ProjectID) AS ProjectName, 
							  dbo.Table_Contracts.PO_No, dbo.Table_Contracts.ContractNo, dbo.Table_Contracts.ContractDate, dbo.Table_Contracts.ContractValue_woVAT, 
							  dbo.Table_Contracts.ContractValue_withVAT, dbo.Table_Contracts.VATpercent, dbo.Table_Contracts.Budget, dbo.Table_Contracts.SignBySupplier, 
							  dbo.Table_Contracts.SentToSupplier, RTRIM(REPLACE(dbo.Table_Contracts.LinkToTemplatefile_DOC, N'~', N'')) AS LinkToTemplatefile_DOC, 
							  dbo.Table_Contracts.ContractType, dbo.Table_Contracts.ContractDescription, dbo.Table6_Supplier.SupplierName AS SupplierID, 
							  dbo.Table_Contracts.ContractCurrency, dbo.Table_Contracts.SignByMercury, dbo.Table_Contracts.CollectionBySupplier, 
							  RTRIM(REPLACE(dbo.Table_Contracts.LinkToPDFcopy, N'~', N'')) AS LinkToPDFcopy, dbo.Table_Contracts.ArchivedByMercury, CASE WHEN Retention IS NULL 
							  THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), [Retention])) + N' %' END AS Retention, dbo.Table_Contracts.Note, dbo.Table_Contracts.CostCode, 
							  dbo.Table_Contracts.RequestedBy, CASE WHEN Advance IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), [Advance])) + N' %' END AS Advance, 
							  CASE WHEN Interim IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), [Interim])) + N' %' END AS Interim, CASE WHEN Shipment IS NULL 
							  THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), [Shipment])) + N' %' END AS Shipment, CASE WHEN Delivery IS NULL 
							  THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), [Delivery])) + N' %' END AS Delivery, dbo.Table_Contracts.DeliveryTerms, 
							  dbo.Table_Contracts.GuaranteePeriod, dbo.Table_Contracts.StartDate, dbo.Table_Contracts.FinishDate, CASE WHEN [Penalties] IS NULL 
							  THEN N'-' WHEN [Penalties] = 1 THEN N'Yes' WHEN [Penalties] = 0 THEN N'No' END AS Penalties, RTRIM(dbo.Table_Contracts.PenaltiesNote) AS PenaltiesNote, 
							  CASE WHEN [PenaltiesToSupplier] IS NULL 
							  THEN N'-' WHEN [PenaltiesToSupplier] = 1 THEN N'Yes' WHEN [PenaltiesToSupplier] = 0 THEN N'No' END AS PenaltiesToSupplier, 
							  RTRIM(dbo.Table_Contracts.PenaltiesToSupplierNote) AS PenaltiesToSupplierNote, 
                              N'The reason of why '+ @ContractLeadPerson + N' rejected this contract' AS ReasonTitle,
                              RTRIM(dbo.Table_Contracts_UserRemarks.Remark) AS Remark_From_Larisa, dbo.Table_Contracts.BudgetLinkToPDF
		FROM         dbo.Table_Contracts INNER JOIN
							  dbo.Table6_Supplier ON dbo.Table6_Supplier.SupplierID = dbo.Table_Contracts.SupplierID INNER JOIN
							  dbo.Table1_Project ON dbo.Table1_Project.ProjectID = dbo.Table_Contracts.ProjectID LEFT OUTER JOIN
							  dbo.Table_Contracts_UserRemarks ON dbo.Table_Contracts.ContractID = dbo.Table_Contracts_UserRemarks.ContractID
		WHERE     (dbo.Table_Contracts.ContractID = @ContractID) AND (dbo.Table_Contracts_UserRemarks.UserName = @ContractLeadPerson)
	  END

END

ELSE

SELECT     dbo.Table_Contracts.ContractID, RTRIM(dbo.Table1_Project.ProjectName) + N' - ' + CONVERT(NvarChar(4), dbo.Table_Contracts.ProjectID) AS ProjectName, 
                      dbo.Table_Contracts.PO_No, dbo.Table_Contracts.ContractNo, dbo.Table_Contracts.ContractDate, dbo.Table_Contracts.ContractValue_woVAT, 
                      dbo.Table_Contracts.ContractValue_withVAT, dbo.Table_Contracts.VATpercent, dbo.Table_Contracts.Budget, dbo.Table_Contracts.SignBySupplier, 
                      dbo.Table_Contracts.SentToSupplier, RTRIM(REPLACE(dbo.Table_Contracts.LinkToTemplatefile_DOC, N'~', N'')) AS LinkToTemplatefile_DOC, 
                      dbo.Table_Contracts.ContractType, dbo.Table_Contracts.ContractDescription, dbo.Table6_Supplier.SupplierName AS SupplierID, 
                      dbo.Table_Contracts.ContractCurrency, dbo.Table_Contracts.SignByMercury, dbo.Table_Contracts.CollectionBySupplier, 
                      RTRIM(REPLACE(dbo.Table_Contracts.LinkToPDFcopy, N'~', N'')) AS LinkToPDFcopy, dbo.Table_Contracts.ArchivedByMercury, CASE WHEN Retention IS NULL 
                      THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), [Retention])) + N' %' END AS Retention, dbo.Table_Contracts.Note, dbo.Table_Contracts.CostCode, 
                      dbo.Table_Contracts.RequestedBy, CASE WHEN Advance IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), [Advance])) + N' %' END AS Advance, 
                      CASE WHEN Interim IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), [Interim])) + N' %' END AS Interim, CASE WHEN Shipment IS NULL 
                      THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), [Shipment])) + N' %' END AS Shipment, CASE WHEN Delivery IS NULL 
                      THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), [Delivery])) + N' %' END AS Delivery, dbo.Table_Contracts.DeliveryTerms, 
                      dbo.Table_Contracts.GuaranteePeriod, dbo.Table_Contracts.StartDate, dbo.Table_Contracts.FinishDate, CASE WHEN [Penalties] IS NULL 
                      THEN N'-' WHEN [Penalties] = 1 THEN N'Yes' WHEN [Penalties] = 0 THEN N'No' END AS Penalties, RTRIM(dbo.Table_Contracts.PenaltiesNote) AS PenaltiesNote, 
                      CASE WHEN [PenaltiesToSupplier] IS NULL 
                      THEN N'-' WHEN [PenaltiesToSupplier] = 1 THEN N'Yes' WHEN [PenaltiesToSupplier] = 0 THEN N'No' END AS PenaltiesToSupplier, 
                      RTRIM(dbo.Table_Contracts.PenaltiesToSupplierNote) AS PenaltiesToSupplierNote, 
                      NULL AS ReasonTitle,
                      N' ' AS Remark_From_Larisa, dbo.Table_Contracts.BudgetLinkToPDF
FROM         dbo.Table_Contracts INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table6_Supplier.SupplierID = dbo.Table_Contracts.SupplierID INNER JOIN
                      dbo.Table1_Project ON dbo.Table1_Project.ProjectID = dbo.Table_Contracts.ProjectID
WHERE     (dbo.Table_Contracts.ContractID = @ContractID)

        " >

        <SelectParameters>

          <asp:Parameter DefaultValue="0" Name="ContractID" type="String" />

        </SelectParameters>
    </asp:SqlDataSource>

