<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Vote.aspx.vb" Inherits="Vote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div height="70px">
    
    <br />

    <div style="text-align:center;">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Xmas_header.jpg" />
    </div>



      <br />

    <div style="text-align:center;">
       <h3 >Hello <asp:Literal ID="LiteralUser" runat="server"></asp:Literal></h3>
    </div>

    <div style="text-align:center;">
       <h3 >would you like to attend christmas party of 2012-2013 ?</h3>
    </div>

    <br />

    <div style="text-align:center;">
        <asp:ImageButton ID="ImageButtonYES" runat="server" ImageUrl="~/images/YES.png" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="ImageButtonNO" runat="server" ImageUrl="~/images/NO.png" />
    </div>
    
     <br />

    <div style="text-align:center;">
        <asp:Label ID="LabelStatus" runat="server" ></asp:Label>
    </div>

    <br /> 

    <hr />
    <div style="text-align:center;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/mercuryLogo.gif" Height="70px" />
        <br />
        <br />
        <asp:Button ID="ButtonResults" runat="server" Text="Send me Results" Visible="false" />
    </div>


    </div>

    <asp:panel ID="Hidepanel" runat="server" style="display:none" >
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" EnableModelValidation="True" 
        Font-Size="12px">
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" 
                SortExpression="UserName" />
            <asp:BoundField DataField="Sex" HeaderText="Sex" ReadOnly="True" 
                SortExpression="Sex" />
            <asp:BoundField DataField="LoweredEmail" HeaderText="Email" 
                ReadOnly="True" SortExpression="LoweredEmail" />
            <asp:BoundField DataField="yes" HeaderText="yes" ReadOnly="True" 
                SortExpression="yes" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="no" HeaderText="no" ReadOnly="True" HeaderStyle-Width="100px"
                SortExpression="no" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Vote_Time" HeaderText="Vote_Time" ReadOnly="True" 
                SortExpression="Vote_Time"  />
        </Columns>
        <HeaderStyle BackColor="Red" ForeColor="White" Height="20px" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" SELECT     TOP (100) PERCENT UPPER(REPLACE(LEFT(dbo.aspnet_Membership.Email, PATINDEX('%@%', dbo.aspnet_Membership.Email) - 1), '.', ' ')) AS UserName, 
                      RTRIM(dbo.aspnet_Membership.LoweredEmail) AS LoweredEmail, CASE WHEN Vote_Xmas = 1 THEN N'YES' WHEN Vote_Xmas IS NULL 
                      THEN N'-' ELSE N'' END AS yes, CASE WHEN Vote_Xmas = 0 THEN N'NO' WHEN Vote_Xmas IS NULL THEN N'-' ELSE N'' END AS no, 
                      dbo.aspnet_Users.Vote_Time, dbo.aspnet_Membership.Sex
FROM         dbo.aspnet_Membership INNER JOIN
                      dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId AND dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId
WHERE     (dbo.aspnet_Users.SendInvitation = 1)
ORDER BY UserName "
        ></asp:SqlDataSource>

</asp:panel>


    </form>
</body>
</html>
