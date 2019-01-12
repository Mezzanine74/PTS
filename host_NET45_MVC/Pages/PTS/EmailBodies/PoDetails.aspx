<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PoDetails.aspx.vb" Inherits="Pages_PTS_EmailBodies_PoDetails" %>

<%@ Register Src="~/webforms/POdetailsForEmail.ascx" TagPrefix="usercontrol" TagName="POdetailsForEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <usercontrol:POdetailsForEmail runat="server" ID="POdetailsForEmail" />
    </div>
    </form>
</body>
</html>
