<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WebUserControl_AddendumEmailBody.ascx.vb" Inherits="WebUserControl_AddendumEmailBody" %>

<style type="text/css">

    .table_format {
        border: thin solid #00BFFF;	
        background-color: #F8F8FF;
    }

        .table_format td {
            padding:6px;
        }

</style>

    <asp:FormView ID="FormViewAddendumsEmailBody" runat="server" 
        DataSourceID="SqlDataSourceAddendumsEmailBody" 
        DataKeyNames="AddendumID" EmptyDataText="Empty" DefaultMode="ReadOnly" Font-Size="11px">

        <ItemTemplate>
            <table >
                <tr>
                    <td style="vertical-align: top; width:380px;">
                        <table class="table_format" style="border: thin solid #00BFFF;	background-color: #F8F8FF;">
                            <tr class="styleTRhorizontal">
                                <td style="width:150px; padding:3px; font-weight:bold;">
                                    <asp:Label ID="LabelInsert17" runat="server" 
                                        Text="проект" Width="100px" />
                                </td>
                                <td style="width:175px; padding:3px; ">
                                    <asp:Label ID="TextBoxProjectName" runat="server" 
                                         Text='<%# Bind("ProjectName") %>'  />
                                </td>
                            </tr>
                            <tr class="styleTRhorizontal">
                                <td style="width:150px; padding:3px; font-weight:bold;">
                                    <asp:Label ID="LabelInsert18" runat="server"  
                                        Text="заказ номер" Width="100px" />
                                </td>
                                <td style="width:175px; padding:3px; ">
                                    <asp:Label ID="LabelPOno" runat="server"  
                                         />
                                </td>
                            </tr>
                            <tr class="styleTRhorizontal">
                                <td style="width:150px; padding:3px; font-weight:bold;">
                                    <asp:Label ID="LabelSupplier" runat="server"  
                                        Text="поставщик" Width="100px" />
                                </td>
                                <td style="width:175px; padding:3px; ">
                                    <asp:Label ID="LabelSupplierName" runat="server"  
                                      Text='<%# Bind("SupplierName") %>'  />
                                </td>
                            </tr>
                            <tr class="styleTRhorizontal">
                                <td style="width:150px; padding:3px; font-weight:bold;">
                                    <asp:Label ID="LabelAddendumTypes" runat="server"  
                                        Text="Тип дополнительного соглашения" Width="100px" />
                                </td>
                                <td style="width:175px; padding:3px; ">
                                    <asp:Label ID="LabelAddendumType" runat="server"  
                                      Text='<%# Bind("AddendumTypes") %>'  />
                                </td>
                            </tr>
                        </table>
                        <br />

                    </td>
                    <td >
                        <table class="table_format" style="border: thin solid #00BFFF;	background-color: #F8F8FF;">
                            <tr class="styleTRhorizontal">
                                <td style="width:175px; padding:3px; font-weight:bold;">
                                    <asp:Label ID="LabelInsert20" runat="server"  
                                        Text="Номер контракта" Width="120px" />
                                </td>
                                <td style="width:250px; padding:3px;">
                                    <asp:Label ID="LabelContractName" runat="server"  
                                    Text='<%# Bind("ContractNo") %>' />
                                </td>
                            </tr>
                            <tr class="styleTRhorizontal">
                                <td style="width:175px; padding:3px; font-weight:bold;">
                                    <asp:Label ID="LabelInsert21" runat="server"  
                                        Text="Дата контракта" Width="120px" />
                                </td>
                                <td style="width:250px; padding:3px;">
                                    <asp:Label ID="LabelContractDate" runat="server"  
                                    Text='<%# Bind("ContractDate","{0:dd/MM/yyyy}")%>' />
                                </td>
                            </tr>
                            <tr class="styleTRhorizontal">
                                <td style="width:175px; padding:3px; font-weight:bold;">
                                    <asp:Label ID="LabelInsert22" runat="server"  
                                        Text="Стоимость контракта с НДС" Width="120px" />
                                </td>
                                <td style="width:250px; padding:3px;">
                                    <asp:Label ID="LabelContractValue" runat="server"  
                                    Text='<%# Bind("ContractValue_withVAT","{0:###,###,###.00}") %>' />
                                </td>
                            </tr>
                            <tr class="styleTRhorizontal">
                                <td style="width:175px; padding:3px; font-weight:bold;">
                                    <asp:Label ID="LabelInsert23" runat="server"  
                                        Text="Контрактная валюта" Width="120px" />
                                </td>
                                <td style="width:250px; padding:3px;">
                                    <asp:Label ID="LabelCurrency" runat="server"  
                                     Text='<%# Bind("ContractCurrency") %>' />
                                </td>
                            </tr>
                            <tr class="styleTRhorizontal">
                                <td style="width:175px; padding:3px; font-weight:bold;">
                                    <asp:Label ID="LabelInsert24" runat="server"  
                                        Text="Форма контракта" Width="120px" />
                                </td>
                                <td style="width:250px; padding:3px;">
                                    <asp:Label ID="LabelContractType" runat="server"  
                                    Text='<%# Bind("ContractType") %>'/>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:175px; padding:3px; font-weight:bold;">
                                    <asp:Label ID="LabelInsert25" runat="server"  
                                        Text="Описание контракта" Width="120px" />
                                </td>
                                <td style="width:250px; padding:3px;">
                                    <asp:Label ID="LabelContractDescription" runat="server" 
                                     Text='<%# Bind("ContractDescription") %>'   />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />

            <table class="table_format" style="border: thin solid #00BFFF;	background-color: #F8F8FF;">
                <tr class="styleTRhorizontal">
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        
                        <asp:Label ID="LabelCostCode" runat="server"  
                            Text="Код затрат" Width="100px" />
                        
                    </td>
                    <td style="width:200px; padding:3px;">
                       <asp:Label ID="TextBoxCostCode" runat="server" 
                       Text='<%# Bind("CostCode") %>' ></asp:Label>

                       <br />

                    </td>
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        
                        <asp:Label ID="LabelInsert9" runat="server" Text="Дополнительное описание соглашения" Width="120px" />
                        
                    </td>
                    <td style="width:200px; padding:3px;">

                        <asp:Label ID="ContractDescriptionTextBox" runat="server" Text='<%# Bind("AddendumDescription") %>' TextMode="MultiLine" />

                    </td>
                </tr>
                <tr class="styleTRhorizontal">
                  <td style="width:150px; padding:3px; font-weight:bold;">
                    <asp:Label ID="LabelInsert4" runat="server"  
                      Text="Дополнительное соглашение нет" Width="100px" />
                  </td>
                  <td style="width:200px; padding:3px;">
                    <asp:Label ID="ContractNoTextBox" runat="server"  
                      Text='<%# Bind("AddendumNo") %>' />

                  </td>
                  <td style="width:150px; padding:3px; font-weight:bold;">

                    <asp:Label ID="LabelInsertStartDate" runat="server"  
                      Text="Дата начала" Width="100px" />
                  </td>
                  <td style="width:200px; padding:3px;">
                    <asp:Label ID="TextBoxStartDate" runat="server"  
                    Text='<%# Bind("StartDate","{0:dd/MM/yyyy}")%>' />
                  </td>
                </tr>
                <tr class="styleTRhorizontal">
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        
                        <asp:Label ID="LabelInsert5" runat="server"  
                            Text="Дата дополнительного соглашения" Width="100px" />
                        
                    </td>
                    <td style="width:200px; padding:3px;">
                        
                        <asp:Label ID="AddendumDateTextBox" runat="server"  
                        Text='<%# Bind("AddendumDate","{0:dd/MM/yyyy}")%>' />

                    </td>
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        
                        <asp:Label ID="LabelInsertFinishDate" runat="server"  
                          Text="Дата окончания" Width="100px" />
                    </td>
                    <td style="width:200px; padding:3px;">
                      <asp:Label ID="TextBoxFinishDate" runat="server"  
                      Text='<%# Bind("FinishDate","{0:dd/MM/yyyy}")%>' />
                    </td>
                </tr>
                <tr class="styleTRhorizontal">
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        
                        <asp:Label ID="LabelAddendumValue_WithVAT" runat="server"  
                            Text="Дополнительное соглашение Стоимость с НДС" Width="120px" />
                        
                    </td>
                    <td style="width:200px; padding:3px;">
                        <asp:Label ID="TextBoxAddendumValue_withVAT" runat="server" 
                           Text='<%# Bind("AddendumValue_WithVAT","{0:###,###,###.00}") %>' />
                    </td>
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        <asp:Label ID="LabelInsertAdvance" runat="server" Text="авансировать %" Width="100px" />
                    </td>
                    <td style="width:200px; padding:3px;">
                        <asp:Label ID="TextBoxAdvance" runat="server" Text='<%# Bind("Advance") %>' />
                    </td>
                </tr>
                <tr class="styleTRhorizontal">
                 <td style="width:150px; padding:3px; font-weight:bold;">
                         <asp:Label ID="LabelVATpercent" runat="server" Text="НДС %" Width="120px" />
                    </td>
                 <td style="width:200px; padding:3px;">
                        <%-- --------------------------------------------------Addendum Value WITH VAT --%>
                        <asp:Label ID="TextBoxVAT" runat="server" Text='<%# Bind("VATpercent") %>' />
                 </td>
                 <td style="width:150px; padding:3px; font-weight:bold;">
                     <asp:Label ID="LabelInterim" runat="server" Text="промежуточный %" Width="100px" />
                    </td>
                  <td style="width:200px; padding:3px;">
                      <asp:Label ID="TextBoxInterim" runat="server" Text='<%# Bind("Interim")%>' />
                    </td>
                </tr>
                <tr class="styleTRhorizontal">
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        <asp:Label ID="LabelBudgetValueWithVAT" runat="server" Text="Бюджетная стоимость с НДС" Width="120px" Visible="false" />
                    </td>
                    <td style="width:200px; padding:3px;">
                        <asp:Label ID="TextBoxBudgetValue" runat="server" Text='<%# Bind("Budget", "{0:###,###,###.00}")%>' Visible="false" />

                        <asp:ImageButton ID="ImageButtonBudgetPDF" runat="server" data-rel="tooltip" data-placement="top" class="tooltip-success" data-original-title="Budget Document"
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BudgetLinkToPDF")%>' Visible="false" 
                        CommandName="OpenBudgetPDF" />

                    </td>
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        <asp:Label ID="LabelInsertShipment" runat="server" Text="отгрузка %" Width="100px" />
                    </td>
                    <td style="width:200px; padding:3px;">
                        <asp:Label ID="TextBoxShipment" runat="server" Text='<%# Bind("Shipment")%>' />
                    </td>
                </tr>
                <tr class="styleTRhorizontal">
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        <asp:Label ID="LabelRequestedBy" runat="server" Text="Запрошенный" Width="100px" />
                    </td>
                    <td style="width:200px; padding:3px;">
                        <asp:Label ID="TextBoxRequestedBy" runat="server" Text='<%# Bind("RequestedBy") %>' />
                    </td>
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        <asp:Label ID="LabelInsertDelivery" runat="server" Text="Доставка %" Width="100px" />
                    </td>
                    <td style="width:200px; padding:3px;">
                        <asp:Label ID="TextBoxDelivery" runat="server" Text='<%# Bind("Delivery")%>' />
                    </td>
                </tr>
                <tr class="styleTRhorizontal">
                    <td style="width:150px; padding:3px; font-weight:bold;">&nbsp;</td>
                    <td style="width:200px; padding:3px;">
                        &nbsp;</td>
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        <asp:Label ID="LabelInsertRetention" runat="server" Text="удержание %" Width="100px" />
                    </td>
                    <td style="width:200px; padding:3px;">
                        <asp:Label ID="TextBoxAddendumRetention" runat="server" Text='<%# Bind("AddendumRetention") %>' />
                    </td>
                </tr>
                <tr class="styleTRhorizontal">
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        <asp:Label ID="LabelInsertPenalty" runat="server" Text="Наказание для нас" Width="100px" />
                    </td>
                    <td style="width:200px; padding:3px;">
                        <asp:Label ID="TextBoxPenalties" runat="server" Text='<%# Bind("Penalties") %>' />
                    </td>
                    <td style="width:150px; padding:3px; font-weight:bold;">заметка</td>
                    <td style="width:200px; padding:3px;">
                        <asp:Label ID="TextBoxPenaltyNote" runat="server" Text='<%# Bind("PenaltiesNote")%>' TextMode="MultiLine" Width="280px" />
                    </td>
                </tr>
                <tr class="styleTRhorizontal">
                    <td style="width:150px; padding:3px; font-weight:bold;">
                        <asp:Label ID="LabelInsertPenaltySupplier" runat="server" Text="Штраф за Поставщика" Width="100px" />
                    </td>
                    <td style="width:200px; padding:3px;"><%-- --------------------------------------------------Addendum Value WITH VAT --%>
                        <asp:Label ID="TextBoxPenaltiesSupplier" runat="server" Text='<%# Bind("PenaltiesToSupplier")%>' />
                    </td>
                    <td style="width:150px; padding:3px; font-weight:bold;">заметка</td>
                    <td style="width:200px; padding:3px;">
                        <asp:Label ID="TextBoxPenaltyToSupplierNote" runat="server" Text='<%# Bind("PenaltiesToSupplierNote")%>' TextMode="MultiLine" Width="280px" />
                    </td>
                </tr>
                <tr class="styleTRhorizontal">
                 <td style="width:150px; padding:3px; font-weight:bold;">
                     <asp:Label ID="LabelInsertDeliveryTerms" runat="server" Text="Условия доставки" Width="100px" />
                    </td>
                 <td style="width:200px; padding:3px;">
                        <%-- --------------------------------------------------------------Addendum VAT --%>
                        <asp:Label ID="TextBoxDeliveryTerms" runat="server" Text='<%# Bind("DeliveryTerms") %>' TextMode="MultiLine" Width="280px" />
                 </td>
                 <td style="width:150px; padding:3px; font-weight:bold;">
                     <asp:Label ID="LabelInsertGuarantePeriod" runat="server" Text="Гарантийный срок" Width="100px" />
                    </td>
                  <td style="width:200px; padding:3px;">
                      <asp:Label ID="TextBoxGuaranteePeriod" runat="server" Text='<%# Bind("GuaranteePeriod") %>' TextMode="MultiLine" />
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
    
    <asp:SqlDataSource ID="SqlDataSourceAddendumsEmailBody" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommand=" 
            declare @ContractLeadPerson nvarChar(30) = (select dbo.NameOfLeadLawyer())

            IF EXISTS (SELECT * FROM dbo.Table_Addendum_UsersApprv WHERE AddendumID = @AddendumID AND UserName = N'lawyers' AND Exception = 1 )
            BEGIN
	            IF EXISTS (SELECT * FROM dbo.Table_Addendum_UserRemarks WHERE AddendumID = @AddendumID AND UserName = @ContractLeadPerson)
	              BEGIN 

		            SELECT     dbo.Table_Addendums.AddendumID, dbo.Table_Addendums.ContractID, RTRIM(dbo.Table1_Project.ProjectName) + N' - ' + RTRIM(CONVERT(NvarChar(4), 
							              dbo.Table1_Project.ProjectID)) AS ProjectName, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, RTRIM(dbo.Table_Contracts.ContractNo) AS ContractNo, 
							              dbo.Table_Contracts.ContractDate, dbo.Table_Contracts.ContractValue_withVAT, RTRIM(dbo.Table_Contracts.ContractCurrency) AS ContractCurrency, 
							              RTRIM(dbo.Table_Contracts.ContractType) AS ContractType, RTRIM(dbo.Table_Contracts.ContractDescription) AS ContractDescription, 
							              RTRIM(dbo.Table_Addendums.AddendumNo) AS AddendumNo, dbo.Table_Addendums.AddendumDate, dbo.Table_Addendums.AddendumValue_WithVAT, 
							              dbo.Table_Addendums.VATpercent, dbo.Table_Addendums.Budget, RTRIM(dbo.Table_Addendums.AddendumDescription) AS AddendumDescription, 
							              RTRIM(dbo.Table_Addendums.CostCode) AS CostCode, CASE WHEN AddendumRetention IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), 
							              [AddendumRetention])) + N' %' END AS AddendumRetention, RTRIM(dbo.Table_Addendums.RequestedBy) AS RequestedBy, 
							              CASE WHEN Table_Addendums.Penalties = 1 THEN N'Yes' WHEN Table_Addendums.Penalties = 0 THEN N'No' WHEN Table_Addendums.Penalties IS NULL 
							              THEN N'-' END AS Penalties, CASE WHEN Table_Addendums.Advance IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), 
							              Table_Addendums.Advance)) + N' %' END AS Advance, CASE WHEN Table_Addendums.Interim IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), 
							              Table_Addendums.Interim)) + N' %' END AS Interim, CASE WHEN Table_Addendums.Shipment IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), 
							              Table_Addendums.Shipment)) + N' %' END AS Shipment, CASE WHEN Table_Addendums.Delivery IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15),
							               Table_Addendums.Delivery)) + N' %' END AS Delivery, dbo.Table_Addendums.StartDate, dbo.Table_Addendums.FinishDate, 
							              RTRIM(dbo.Table_Addendums.DeliveryTerms) AS DeliveryTerms, dbo.Table_Addendums.GuaranteePeriod, 
							              CASE WHEN Table_Addendums.AddendumTypes = 1 THEN N'Regular Addendum' WHEN Table_Addendums.AddendumTypes = 2 THEN N'Replace Addendum' WHEN Table_Addendums.AddendumTypes
							               = 3 THEN N'Zero Value Addendum' END AS AddendumTypes, RTRIM(REPLACE(dbo.Table_Addendums.AddendumLinkToTemplatefile_DOC, N'~', N'')) 
							              AS AddendumLinkToTemplatefile_DOC, RTRIM(REPLACE(dbo.Table_Addendums.AddendumLinkToPDFcopy, N'~', N'')) AS AddendumLinkToPDFcopy, 
							              RTRIM(dbo.Table_Addendums.PenaltiesNote) AS PenaltiesNote, CASE WHEN Table_Addendums.PenaltiesToSupplier IS NULL 
							              THEN N'-' WHEN Table_Addendums.PenaltiesToSupplier = 1 THEN N'Yes' WHEN Table_Addendums.PenaltiesToSupplier = 0 THEN N'No' END AS PenaltiesToSupplier,
							               RTRIM(dbo.Table_Addendums.PenaltiesToSupplierNote) AS PenaltiesToSupplierNote,N'The reason of why '+ @ContractLeadPerson + N' rejected this contract' AS ReasonTitle,
							              RTRIM(dbo.Table_Addendum_UserRemarks.Remark) AS Remark_From_Larisa, dbo.Table_Addendums.BudgetLinkToPDF
		            FROM         dbo.Table_Addendums INNER JOIN
							              dbo.Table_Contracts ON dbo.Table_Contracts.ContractID = dbo.Table_Addendums.ContractID INNER JOIN
							              dbo.Table1_Project ON dbo.Table1_Project.ProjectID = dbo.Table_Contracts.ProjectID INNER JOIN
							              dbo.Table6_Supplier ON dbo.Table6_Supplier.SupplierID = dbo.Table_Contracts.SupplierID LEFT OUTER JOIN
							              dbo.Table_Addendum_UserRemarks ON dbo.Table_Addendums.AddendumID = dbo.Table_Addendum_UserRemarks.AddendumID
		            WHERE     (dbo.Table_Addendums.AddendumID = @AddendumID) AND (dbo.Table_Addendum_UserRemarks.UserName = @ContractLeadPerson)
	              END

            END

            ELSE

		            SELECT     dbo.Table_Addendums.AddendumID, dbo.Table_Addendums.ContractID, RTRIM(dbo.Table1_Project.ProjectName) + N' - ' + RTRIM(CONVERT(NvarChar(4), 
							              dbo.Table1_Project.ProjectID)) AS ProjectName, RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, RTRIM(dbo.Table_Contracts.ContractNo) AS ContractNo, 
							              dbo.Table_Contracts.ContractDate, dbo.Table_Contracts.ContractValue_withVAT, RTRIM(dbo.Table_Contracts.ContractCurrency) AS ContractCurrency, 
							              RTRIM(dbo.Table_Contracts.ContractType) AS ContractType, RTRIM(dbo.Table_Contracts.ContractDescription) AS ContractDescription, 
							              RTRIM(dbo.Table_Addendums.AddendumNo) AS AddendumNo, dbo.Table_Addendums.AddendumDate, dbo.Table_Addendums.AddendumValue_WithVAT, 
							              dbo.Table_Addendums.VATpercent, dbo.Table_Addendums.Budget, RTRIM(dbo.Table_Addendums.AddendumDescription) AS AddendumDescription, 
							              RTRIM(dbo.Table_Addendums.CostCode) AS CostCode, CASE WHEN AddendumRetention IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), 
							              [AddendumRetention])) + N' %' END AS AddendumRetention, RTRIM(dbo.Table_Addendums.RequestedBy) AS RequestedBy, 
							              CASE WHEN Table_Addendums.Penalties = 1 THEN N'Yes' WHEN Table_Addendums.Penalties = 0 THEN N'No' WHEN Table_Addendums.Penalties IS NULL 
							              THEN N'-' END AS Penalties, CASE WHEN Table_Addendums.Advance IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), 
							              Table_Addendums.Advance)) + N' %' END AS Advance, CASE WHEN Table_Addendums.Interim IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), 
							              Table_Addendums.Interim)) + N' %' END AS Interim, CASE WHEN Table_Addendums.Shipment IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15), 
							              Table_Addendums.Shipment)) + N' %' END AS Shipment, CASE WHEN Table_Addendums.Delivery IS NULL THEN N'not provided' ELSE RTRIM(CONVERT(nVarChar(15),
							               Table_Addendums.Delivery)) + N' %' END AS Delivery, dbo.Table_Addendums.StartDate, dbo.Table_Addendums.FinishDate, 
							              RTRIM(dbo.Table_Addendums.DeliveryTerms) AS DeliveryTerms, dbo.Table_Addendums.GuaranteePeriod, 
							              CASE WHEN Table_Addendums.AddendumTypes = 1 THEN N'Regular Addendum' WHEN Table_Addendums.AddendumTypes = 2 THEN N'Replace Addendum' WHEN Table_Addendums.AddendumTypes
							               = 3 THEN N'Zero Value Addendum' END AS AddendumTypes, RTRIM(REPLACE(dbo.Table_Addendums.AddendumLinkToTemplatefile_DOC, N'~', N'')) 
							              AS AddendumLinkToTemplatefile_DOC, RTRIM(REPLACE(dbo.Table_Addendums.AddendumLinkToPDFcopy, N'~', N'')) AS AddendumLinkToPDFcopy, 
							              RTRIM(dbo.Table_Addendums.PenaltiesNote) AS PenaltiesNote, CASE WHEN Table_Addendums.PenaltiesToSupplier IS NULL 
							              THEN N'-' WHEN Table_Addendums.PenaltiesToSupplier = 1 THEN N'Yes' WHEN Table_Addendums.PenaltiesToSupplier = 0 THEN N'No' END AS PenaltiesToSupplier,
							               RTRIM(dbo.Table_Addendums.PenaltiesToSupplierNote) AS PenaltiesToSupplierNote, NULL AS ReasonTitle, 
							              N' ' AS Remark_From_Larisa, dbo.Table_Addendums.BudgetLinkToPDF
		            FROM         dbo.Table_Addendums INNER JOIN
							              dbo.Table_Contracts ON dbo.Table_Contracts.ContractID = dbo.Table_Addendums.ContractID INNER JOIN
							              dbo.Table1_Project ON dbo.Table1_Project.ProjectID = dbo.Table_Contracts.ProjectID INNER JOIN
							              dbo.Table6_Supplier ON dbo.Table6_Supplier.SupplierID = dbo.Table_Contracts.SupplierID
		            WHERE     (dbo.Table_Addendums.AddendumID = @AddendumID)
        ">

    <SelectParameters>

          <asp:Parameter DefaultValue="0" Name="AddendumID" type="Int32" />

    </SelectParameters>

    </asp:SqlDataSource>

