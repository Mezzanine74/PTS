<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="PTS_1S_DeliveryDocComparisonGrid.aspx.vb" Inherits="PTS_1S_DeliveryDocComparisonGridREV" enableEventValidation ="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

                <asp:Button ID="ButtonUpdate" runat="server" Text="Update Records" CssClass="btn btn-success"  />

                <asp:Button ID="ButtonExcel" runat="server" Text="To Excel" OnClick="ButtonExcel_Click" CssClass="btn btn-purple" />

                <asp:DropDownList ID="DropDownListProject" runat="server" DataSourceID="SqlDataSourceProjects" DataTextField="ProjectName" DataValueField="ProjectID" AutoPostBack="true">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceProjects" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT * FROM (
                SELECT N'0' AS ProjectID, N'_Select Project' AS ProjectName
                UNION ALL
                SELECT Table1_Project.ProjectID, RTRIM(ProjectName) + N' ' + RTRIM(convert(nvarChar(5),Table1_Project.ProjectID)) AS ProjectName FROM (
                SELECT ProjectID FROM (
                SELECT ProjectID FROM View_DeliveryDocEntry1S
                UNION ALL
                SELECT ProjectID FROM View_DeliveryDocEntryPTS
                ) AS SrcPrjID
                GROUP BY ProjectID
                ) AS SourcePrjID
                INNER JOIN Table1_Project ON Table1_Project.ProjectID = SourcePrjID.ProjectID
                WHERE CurrentStatus = 1
                ) AS Source
                ORDER BY ProjectName ASC"></asp:SqlDataSource>

                <asp:DropDownList ID="DropDownListSupplier" runat="server" DataSourceID="SqlDataSourceSupplier" DataTextField="SupplierName" DataValueField="SupplierID" AutoPostBack="true" CssClass="ddl_fxfnt">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceSupplier" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                SelectCommand="
                SELECT SupplierID, SupplierName FROM (

                SELECT N'0' AS SupplierID, RIGHT('..................................................' + N'', 50) +
                 RIGHT('..................................................' + N'_ALL', 50) AS SupplierName

                UNION ALL

                SELECT AllINN.SupplierID, 
                Left(ISNULL(SupplierPTS.SupplierName,N'') + '..................................................', 50) +
                Left(ISNULL(Supplier1S.SupplierName1S,N'') + '..................................................', 50) AS SupplierName
                FROM         (SELECT     SupplierID
                                       FROM          (SELECT     SupplierID
                                                               FROM          dbo.View_DeliveryDocEntry1S
                                                               WHERE      (ProjectID = @ProjectID)
                                                               UNION ALL
                                                               SELECT     SupplierID
                                                               FROM         dbo.View_DeliveryDocEntryPTS
                                                               WHERE     (ProjectID = @ProjectID)) AS Source
                                       GROUP BY SupplierID) AS AllINN LEFT OUTER JOIN
                                          (SELECT     SupplierID, SupplierName
                                            FROM          dbo.View_DeliveryDocEntryPTS AS View_DeliveryDocEntryPTS_1
                                            WHERE      (ProjectID = @ProjectID)
                                            GROUP BY SupplierID, SupplierName) AS SupplierPTS ON AllINN.SupplierID = SupplierPTS.SupplierID LEFT OUTER JOIN
                                          (SELECT     SupplierID, SupplierName1S
                                            FROM          dbo.View_DeliveryDocEntry1S AS View_DeliveryDocEntry1S_1
                                            WHERE      (ProjectID = @ProjectID)
                                            GROUP BY SupplierID, SupplierName1S) AS Supplier1S ON AllINN.SupplierID = Supplier1S.SupplierID
                ) AS Source
                ORDER BY Source.SupplierName
                    ">
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownListProject" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                </SelectParameters>
                </asp:SqlDataSource>


    <table style="margin-top:5px;">
        <tr >
            <td>
            <asp:RadioButtonList ID="RadioButtonListCriteria" CssClass="table-condensed" runat="server" BackColor="#CECFCE" Font-Size="10px" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListCriteria_SelectedIndexChanged">
                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                <asp:ListItem Value="1">Matched</asp:ListItem>
                <asp:ListItem Value="2">Not Matched</asp:ListItem>
            </asp:RadioButtonList>
            </td>
            <td style="padding-left:5px;">
                <asp:CheckBoxList ID="CheckBoxListWeeks" runat="server" CssClass="table-condensed"  RepeatDirection="Horizontal" Font-Size="10px" AutoPostBack="true" OnSelectedIndexChanged="CheckBoxListWeeks_SelectedIndexChanged">
                    <asp:ListItem Value="1">This Week</asp:ListItem>
                    <asp:ListItem Value="2">Last Week</asp:ListItem>
                    <asp:ListItem Value="3">Two Weeks Ago</asp:ListItem>
                    <asp:ListItem Value="4">Older Than Two Weeks</asp:ListItem>
                    <asp:ListItem Value="5">Empty</asp:ListItem>
                </asp:CheckBoxList>

            </td>
        </tr>
    </table>



    <asp:GridView ID="GridViewPTS_1S_Comparison" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourcePTS_1S_ComparisonForGrid"
         Font-Size="10px" HeaderStyle-BackColor="#FF0066" HeaderStyle-ForeColor="White" HeaderStyle-Height="50px">
        <Columns>

            <asp:BoundField DataField="MatchStatus" HeaderText="MatchStatus" ReadOnly="True" SortExpression="MatchStatus" />

            <asp:TemplateField HeaderText =" Supplier 1S">
                <ItemTemplate>
                    <asp:Literal ID="LiteralSupplierName" runat="server" Text='<%# Eval("SupplierName1S")%>' ></asp:Literal>
                    <asp:HyperLink ID="HyperlinkPTS" runat="server" Target="_blank" >PTS</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DocDate1S" HeaderText="Document Date 1S" ReadOnly="True" SortExpression="DocDate1S" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="DocNo1S" HeaderText="Document No 1S" ReadOnly="True" SortExpression="DocNo1S" />
            <asp:BoundField DataField="DocValue1S" HeaderText="Document Value in Rub With VAT 1S" ReadOnly="True" SortExpression="DocValue1S" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1S" runat="server" />
                    <asp:Label ID="Label1S_ID" runat="server" Text='<%# Eval("ID_1S")%>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBoxPTS" runat="server" />
                    <asp:Label ID="LabelPTS_ID" runat="server" Text='<%# Eval("ID_PTS")%>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="DocValuePTS" HeaderText="Doc Value PTS in Rub With VAT" ReadOnly="True" SortExpression="DocValuePTS" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
            <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name PTS" ReadOnly="True" SortExpression="SupplierName" />
            <asp:BoundField DataField="DocNoPTS" HeaderText="Document No PTS" ReadOnly="True" SortExpression="DocNoPTS" />
            <asp:BoundField DataField="NakNo" HeaderText="Nakladnaya No PTS" ReadOnly="True" SortExpression="NakNo" />
            <asp:BoundField DataField="Nak_Date" HeaderText="Nakkladnaya Date PTS" ReadOnly="True" SortExpression="Nak_Date" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="Nak_Rub_WithVAT" HeaderText="Nakladnaya Value In Rub With VAT" ReadOnly="True" SortExpression="Nak_Rub_WithVAT" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
            <asp:BoundField DataField="AktNo" HeaderText="Akt No PTS" ReadOnly="True" SortExpression="AktNo" />
            <asp:BoundField DataField="AktDate" HeaderText="Akt Date PTS" ReadOnly="True" SortExpression="AktDate" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="AktValue" HeaderText="Akt Value in Rub With VAT" ReadOnly="True" SortExpression="AktValue" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="Inv_ContrNo" HeaderText="Invoice or Conttract No" ReadOnly="True" SortExpression="Inv_ContrNo" />
            <asp:BoundField DataField="PersonCreated" HeaderText="Person Created" ReadOnly="True" SortExpression="PersonCreated" />
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" ReadOnly="True" SortExpression="CreatedBy" />
            <asp:TemplateField HeaderText="Entry Week">
                <ItemTemplate>
                    <div class="hidepanel"><asp:Literal ID="LiteralEntryWeek" runat="server" Text='<%# Eval("CreatedBy")%>' ></asp:Literal></div>
                    <asp:Literal ID="LiteralEntryDescription" runat="server"  ></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

        <HeaderStyle BackColor="#FF0066" ForeColor="White" Height="50px"></HeaderStyle>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourcePTS_1S_ComparisonForGrid" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SP_Delivery_ReportServerSource_Rev" SelectCommandType="StoredProcedure">
        <SelectParameters>
<%--            <asp:ControlParameter ControlID="DropDownListProject" DefaultValue="0" Name="ProjectID" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="DropDownListSupplier" DefaultValue="0" Name="SupplierID" PropertyName="SelectedValue" Type="String" />--%>
            <asp:Parameter Name="ProjectID" Type="Int16" />
            <asp:Parameter Name="SupplierID" Type="String" />
            <asp:Parameter Name="GenerateMissingReport" Type="Boolean"  DefaultValue="False"/>
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

