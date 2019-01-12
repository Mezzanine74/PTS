<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WebUserControlAddendumDetails.ascx.vb" Inherits="Pages_PTS_UserControls_WebUserControlAddendumDetails" %>

<asp:HiddenField ID="addendumid" runat="server" />

<asp:FormView runat="server" ID="FormviewAddendumDetails" DataKeyNames="Id" DefaultMode="ReadOnly"
    ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.View_AddendumDetails"
    OnCallingDataMethods="FormviewAddendumDetails_CallingDataMethods"
    SelectMethod="FormviewAddendumDetails_GetItem" EmptyDataText="No Addendum Found">
    <ItemTemplate>

        <span style="background-color:black; color:white; padding:3px;"><%# Item.CreationInfo%></span>

        <table>
            <tr>
                <td>

                    <table class="table_">
                        <tr>
                            <td class="header_">
                                <%# Item.ProjectNameTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.ProjectName%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.PoNoTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.PO_No%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.SupplierNameTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.SupplierName%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.AddendumTypeTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.AddendumType%>
                            </td>
                        </tr>

                    </table>

                    <table class="table_">
                        <tr>
                            <td class="header_">
                                <%# Item.CostCodeTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.CodeDescription%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.AddnNoTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.AddendumNo%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.AddnDateTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.AddendumDate%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.AddendumValueTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.AddendumValue_WithVAT%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.VATpercentTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.VATpercent%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.BudgetValueWithVATTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.Budget%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.RequestedByTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.RequestedBy%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.PenaltyToMercuryTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.Penalties%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.PenaltyToSupplierTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.PenaltiesToSupplier%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.DeliveryTermsTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.DeliveryTerms%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.CompletionDateTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.CompletionDate%>
                            </td>
                        </tr>
                    </table>

                    <asp:GridView ID="GridviewApprovalStatusAddendum" runat="server" AutoGenerateColumns="false" DataSourceID="SqlDataSourceApprovalStatusAddendum" CssClass="table_" GridLines="None" OnRowDataBound="GridviewApprovalStatusAddendum_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="UserName" HeaderText="User Name" HeaderStyle-CssClass="header_gridview" ItemStyle-CssClass="second_gridview" />
                            <asp:BoundField DataField="Approved" HeaderText="Approved" HeaderStyle-CssClass="header_gridview" ItemStyle-CssClass="second_gridview" />
                            <asp:BoundField DataField="WhenApproved" HeaderText="When Approved" HeaderStyle-CssClass="header_gridview" ItemStyle-CssClass="second_gridview" />
                        </Columns>
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSourceApprovalStatusAddendum" runat="server" SelectCommandType="StoredProcedure" SelectCommand="ApprovalStatusAddendum" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="addendumid" Name="AddendumId" Type="Int32" />
                            <asp:Parameter Name="Shift" Type="Int16" DefaultValue="1" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </td>
                <td>

                    <table class="table_">
                        <tr>
                            <td class="header_">
                                <%# Item.ContractNoTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.ContractNo%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.ContractDateTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.ContractDate%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.ContractValueTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.ContractValue_withVAT%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.ContractCurrencyTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.ContractCurrency%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.ContractTypeTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.ContractType%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.ContractDescriptionTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.ContractDescription%>
                            </td>
                        </tr>
                    </table>


                    <table class="table_">
                        <tr>
                            <td class="header_">
                                <%# Item.AddnDescriptionTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.AddendumDescription%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.StartDateTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.StartDate%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.FinishDateTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.FinishDate%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.AdvanceTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.Advance%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.InterimTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.Interim%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.ShipmentTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.Shipment%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.DeliveryTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.Delivery%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.RetentionTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.Retention%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.PenallyToMercuryNoteTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.PenaltiesNote%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.PenaltyToSupplierNoteTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.PenaltiesToSupplierNote%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.GuaranteePeriodTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.GuaranteePeriod%>
                            </td>
                        </tr>
                        <tr>
                            <td class="header_">
                                <%# Item.AktOfWorkTitle%>
                            </td>
                            <td class="second_">
                                <%# Item.AktOfWork%>
                            </td>
                        </tr>
                    </table>


                </td>
            </tr>
        </table>

    </ItemTemplate>
</asp:FormView>


