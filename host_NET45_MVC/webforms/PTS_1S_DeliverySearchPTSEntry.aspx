<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PTS_1S_DeliverySearchPTSEntry.aspx.vb" Inherits="PTS_1S_DeliverySearchPTSEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:Panel ID="panel" runat="server" DefaultButton="BtnSearch">
        Supplier
        <asp:TextBox ID="TextBoxSupplierINN" runat="server" PlaceHolder="Supplier ID" AutoPostBack="true" OnTextChanged="BtnSearch_Click"></asp:TextBox>
        Doc No
        <asp:TextBox ID="TextBoxDocNo" runat="server" PlaceHolder="Doc No" AutoPostBack="true" OnTextChanged="BtnSearch_Click"></asp:TextBox>
        Doc Date
        <asp:TextBox ID="TextBoxDocDate" runat="server" PlaceHolder="Doc Date" CssClass="add_datepicker" AutoPostBack="true" OnTextChanged="BtnSearch_Click"></asp:TextBox>

        <asp:Button ID="BtnSearch" runat="server" Text="S E A R C H" CssClass="btn btn-mini btn-info" />
    </asp:Panel>

    <hr />

    <asp:GridView ID="GridViewSearchResult" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceSearchDeliveryDocInPTS" EmptyDataText="  N O    R E S U L T !  "
         GridLines="None" CssClass="table table-nonfluid table-hover">
        <Columns>
            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="True" SortExpression="ProjectName" HeaderStyle-Width="100px" />
            <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ReadOnly="True" SortExpression="SupplierName" HeaderStyle-Width="100px" />
            <asp:BoundField DataField="PO_No" HeaderText="PO No" ReadOnly="True" SortExpression="PO_No" HeaderStyle-Width="100px" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="Inv_ContrNo" HeaderText="Inv ContrNo" ReadOnly="True" SortExpression="Inv_ContrNo" HeaderStyle-Width="80px" />
            <asp:BoundField DataField="NakNo" HeaderText="Nak No" ReadOnly="True" SortExpression="NakNo" HeaderStyle-Width="80px" />
            <asp:BoundField DataField="Nak_Date" HeaderText="Nak Date" ReadOnly="True" SortExpression="Nak_Date" HeaderStyle-Width="80px" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="Nak_Rub_WithVAT" HeaderText="Nak Rub WithVAT" ReadOnly="True" SortExpression="Nak_Rub_WithVAT" HeaderStyle-Width="80px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
            <asp:BoundField DataField="AktNo" HeaderText="Akt No" ReadOnly="True" SortExpression="AktNo" HeaderStyle-Width="80px" />
            <asp:BoundField DataField="AktDate" HeaderText="Akt Date" ReadOnly="True" SortExpression="AktDate" HeaderStyle-Width="80px" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="AktValue" HeaderText="Akt Value" ReadOnly="True" SortExpression="AktValue" HeaderStyle-Width="80px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
            <asp:BoundField DataField="TotalDocumentValue" HeaderText="Total Document Value" ReadOnly="True" SortExpression="TotalDocumentValue" HeaderStyle-Width="80px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
            <asp:BoundField DataField="PersonCreated" HeaderText="Person Created" ReadOnly="True" SortExpression="PersonCreated" HeaderStyle-Width="80px" />
            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" ReadOnly="True" SortExpression="CreatedBy" HeaderStyle-Width="80px" />
            <asp:TemplateField HeaderStyle-Width="70">
                <ItemTemplate>
                    <asp:HyperLink ID="HyLinkToComparison" runat="server" Target="_blank" NavigateUrl="~/addendumenterNew.aspx" Text="Details" CssClass="btn btn-mini"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceSearchDeliveryDocInPTS" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SP_DeliverySearchPTSEntry" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxSupplierINN" Name="SupplierID" PropertyName="Text" Type="String" ConvertEmptyStringToNull="false"/>
            <asp:ControlParameter ControlID="TextBoxDocNo" Name="DocNo" PropertyName="Text" Type="String" ConvertEmptyStringToNull="false"/>
            <asp:Parameter Name="DocDate" Type="DateTime"  />
        </SelectParameters>
    </asp:SqlDataSource>


</asp:Content>

