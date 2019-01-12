<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AutoApprovalByPTSAddendum.aspx.vb" Inherits="Pages_PTS_EmailBodies_AutoApprovalByPTSAddendum" %>

<%@ Register Src="~/Pages/PTS/UserControls/WebUserControlAddendumDetails.ascx" TagPrefix="usercontrol" TagName="WebUserControlAddendumDetails" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title></title>

<style>
    .table_ {
        border-style:solid;
        border-color:black;
        border-width:medium;
        margin:10px;
        background-color:#F8F9F9;
        color:#566573;
    }

    td {
        vertical-align:top;
    }

    .header_ {
        font-weight:bold;
        width:250px;
        padding:3px;
        border-bottom-style:solid;
        border-bottom-color:lightgray;
        border-bottom-width:1px;
    }

    .header_gridview {
        font-weight:bold;
        padding:3px;
        border-bottom-style:solid;
        border-bottom-color:lightgray;
        border-bottom-width:1px;
    }

    .second_ {
        width:250px;
        padding:3px;
        border-bottom-style:solid;
        border-bottom-color:lightgray;
        border-bottom-width:1px;
    }

    .second_gridview {
        padding:3px;
        border-bottom-style:solid;
        border-bottom-color:lightgray;
        border-bottom-width:1px;
    }


</style>

</head>
<body>
    <form id="form1" runat="server">
    <div>

        <usercontrol:WebUserControlAddendumDetails runat="server" ID="WebUserControlAddendumDetails"  />

    </div>
    </form>
</body>
</html>
