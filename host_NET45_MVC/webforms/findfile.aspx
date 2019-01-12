<%@ Page Language="VB" AutoEventWireup="false" CodeFile="findfile.aspx.vb" Inherits="findfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <asp:DropDownList ID="DropDownList1" runat="server" 
             
            Width="365px">
   <asp:ListItem>REQUEST\Bain - 46\</asp:ListItem>
<asp:ListItem>REQUEST\C&W - 31\</asp:ListItem>
<asp:ListItem>REQUEST\Coca - Cola - 48\</asp:ListItem>
<asp:ListItem>REQUEST\Data Center\</asp:ListItem>
<asp:ListItem>REQUEST\De Beers - 35\</asp:ListItem>
<asp:ListItem>REQUEST\DELL - 65\</asp:ListItem>
<asp:ListItem>REQUEST\DELL New\</asp:ListItem>
<asp:ListItem>REQUEST\Design office\</asp:ListItem>
<asp:ListItem>REQUEST\Deutsche bank - 33\</asp:ListItem>
<asp:ListItem>REQUEST\Finance Dep\</asp:ListItem>
<asp:ListItem>REQUEST\GE - 66\</asp:ListItem>
<asp:ListItem>REQUEST\GE-NEW\</asp:ListItem>
<asp:ListItem>REQUEST\Google\</asp:ListItem>
<asp:ListItem>REQUEST\HP\</asp:ListItem>
<asp:ListItem>REQUEST\Human Resources\</asp:ListItem>
<asp:ListItem>REQUEST\IB\</asp:ListItem>
<asp:ListItem>REQUEST\IT Management - 93\</asp:ListItem>
<asp:ListItem>REQUEST\Jonson & Jonson -55\</asp:ListItem>
<asp:ListItem>REQUEST\K & N - 45\</asp:ListItem>
<asp:ListItem>REQUEST\Kaspiy\</asp:ListItem>
<asp:ListItem>REQUEST\KAZIMIR - 61\</asp:ListItem>
<asp:ListItem>REQUEST\K-N- new\</asp:ListItem>
<asp:ListItem>REQUEST\Legal Dep-97\</asp:ListItem>
<asp:ListItem>REQUEST\Michael Page - 56\</asp:ListItem>
<asp:ListItem>REQUEST\Morgan Stanley - 54\</asp:ListItem>
<asp:ListItem>REQUEST\Moscow Office-90\</asp:ListItem>
<asp:ListItem>REQUEST\NIKE - 38\</asp:ListItem>
<asp:ListItem>REQUEST\Office Managment-95\</asp:ListItem>
<asp:ListItem>REQUEST\ORIFLAME\</asp:ListItem>
<asp:ListItem>REQUEST\Procurement Dep -94\</asp:ListItem>
<asp:ListItem>REQUEST\Regus Ciyidel-44\</asp:ListItem>
<asp:ListItem>REQUEST\Regus Embankment\</asp:ListItem>
<asp:ListItem>REQUEST\ROCHE - 42\</asp:ListItem>
<asp:ListItem>REQUEST\ROCHE - 60\</asp:ListItem>
<asp:ListItem>REQUEST\ROLF - 57\</asp:ListItem>
<asp:ListItem>REQUEST\Rus Finans - 52\</asp:ListItem>
<asp:ListItem>REQUEST\SALYM - 59\</asp:ListItem>
<asp:ListItem>REQUEST\Sberbank Canteen\</asp:ListItem>
<asp:ListItem>REQUEST\Shell new -68\</asp:ListItem>
<asp:ListItem>REQUEST\Shell\</asp:ListItem>
<asp:ListItem>REQUEST\Shlumberger 26\</asp:ListItem>
<asp:ListItem>REQUEST\Soni ericsson 53\</asp:ListItem>
<asp:ListItem>REQUEST\Swiss otel\</asp:ListItem>
<asp:ListItem>REQUEST\Tender Dep\</asp:ListItem>
<asp:ListItem>REQUEST\TMF - 69\</asp:ListItem>
<asp:ListItem>REQUEST\Wyeth - 62\</asp:ListItem>

        </asp:DropDownList>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     REPLACE(PATH, N'REQUESTS', N'REQUEST') AS PATH
FROM         dbo.View_ProblemliDosyaLinkleri
GROUP BY PATH"></asp:SqlDataSource>
        <asp:Button ID="Button1" runat="server" Text="Button" />

    <br />
        
        <asp:ListBox ID="ListBox1" runat="server" Height="788px" Width="332px">
        </asp:ListBox>


    
    </div>
    </form>
</body>
</html>
