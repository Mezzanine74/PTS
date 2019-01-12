<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="SearchContractsNotEnteredItems.aspx.vb" Inherits="SearchContractsNotEnteredItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:Label ID="lbl" runat="server"></asp:Label>

    <asp:Button ID="Button1" runat="server" Text="R E F R E S H" CssClass="btn btn-mini" />
    <asp:Button ID="ButtonIgnore" runat="server" Text="IGNORE WHOLE LIST FROM SEARCH FEED BECAUSE THERE IS NO DATA IN THERE" CssClass="btn btn-mini btn-danger" OnClientClick="return confirm('Do you want to ignore all items')" OnClick="ButtonIgnore_Click" />
    <asp:GridView ID="GridViewNotEnteredItems" runat="server" AutoGenerateColumns="False" DataKeyNames="ContractID" DataSourceID="SqlDataSourceNotEnteredItems" Font-Size="12px" >
        <Columns>
            <asp:BoundField DataField="ContractID" HeaderText="ContractID" InsertVisible="False" ReadOnly="True" SortExpression="ContractID" ItemStyle-CssClass="LabelContract" ItemStyle-Width="80px" />
            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="True" SortExpression="ProjectName" HeaderStyle-Width="100px">
                <HeaderStyle Width="100px"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ContractDescription" HeaderText="Contract Description" ReadOnly="True" SortExpression="ContractDescription" ItemStyle-Width="300px"></asp:BoundField>
            <asp:BoundField DataField="ContractValue_withVAT" HeaderText="ContractValue with VAT" SortExpression="ContractValue_withVAT" HeaderStyle-Width="100px" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Width="100px"></HeaderStyle>

                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ContractCurrency" HeaderText="Currency" ReadOnly="True" SortExpression="ContractCurrency" ItemStyle-Width="80px" />
            <asp:BoundField DataField="VATpercent" HeaderText="VAT %" SortExpression="VATpercent" HeaderStyle-Width="50px">
                <HeaderStyle Width="50px"></HeaderStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Link" SortExpression="LinkToTemplatefile_DOC" ItemStyle-Width="200px">
                <ItemTemplate>

                    <asp:ImageButton ID="ImageButtonLink" runat="server" CommandName="OpenFile"
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToTemplatefile_DOC")%>' />

                    <asp:HyperLink ID="HyperlinkFeedLink" runat="server" Target="_blank">Go To Feed Page</asp:HyperLink>

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle Height="50px" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceNotEnteredItems" runat="server"
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
        SelectCommandType="StoredProcedure" SelectCommand="SP_SearchContractNotEnteredItems"></asp:SqlDataSource>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">

        <script type="text/javascript" src="http://cdn.jsdelivr.net/json2/0.1/json2.js"></script>

        <script type="text/javascript">
            $(document).ready(function () {
                //Scripts goes here

                $(".LabelContract").click(function (event) {

                    if (confirm('Are you sure to delete ?')) {

                        var _clickedItem = $(this)

                        var Table_Contracts_IgnoreSearchFeed = {};
                        Table_Contracts_IgnoreSearchFeed.ContractID = _clickedItem.text();

                        $.ajax({
                            type: "POST",
                            url: '<%= ResolveUrl("~/Pages/PTS/PageMethods/Table_Contracts_IgnoreSearchFeed.aspx/InsertTable_Contracts_IgnoreSearchFeed")%>',
                            data: '{Table_Contracts_IgnoreSearchFeed: ' + JSON.stringify(Table_Contracts_IgnoreSearchFeed) + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                $("body").append('<div id="loader" class="divcenterscreen">processing ...</div>');
                                $('#loader').show();

                                var x, y;

                                x = event.pageX;
                                y = event.pageY;

                                $('#loader').css({ top: y, left: x + x, position: 'absolute' });
                            },
                            complete: function () {
                                _clickedItem.closest("tr").fadeOut("slow");
                                $('#loader').hide();

                            },
                            success: function (response) {
                                //alert("User has been added successfully.");
                                //window.location.reload();
                            }
                        });

                    }

                })

            })
        </script>


</asp:Content>

