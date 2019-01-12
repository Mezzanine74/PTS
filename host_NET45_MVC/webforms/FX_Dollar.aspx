<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FX_Dollar.aspx.vb" Inherits="FX_Dollar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .gridviewFx td {
           padding: 10px;
          }

        .gridviewFx th {
           padding: 10px;
          }

    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridViewFxRate" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceFXDollar" Font-Size="55px" CssClass="gridviewFx">
            <Columns>
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                <asp:BoundField DataField="Yandex" HeaderText="Yandex" SortExpression="Yandex" ItemStyle-ForeColor="Blue" />
                <asp:BoundField DataField="Citibank" HeaderText="Citibank" SortExpression="Citibank" ItemStyle-ForeColor="Red" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Literal ID="LtR" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceFXDollar" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
            SelectCommand="SELECT TOP 300 [Date], [Yandex], [Citibank] FROM [Table_FX_Dollar] WHERE [Date] > N'2015-08-16' ORDER BY Date DESC"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
