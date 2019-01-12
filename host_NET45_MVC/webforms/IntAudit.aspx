<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="IntAudit.aspx.vb" Inherits="IntAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style2
        {
            width: 50px;
            text-align: left;
            height: 50px;
        }
        
         .style3
        {
            width: 50px;
            text-align: center;
            height: 50px;
        }
        
         .style7
        {
            width: 200px;
            vertical-align: middle;
            }        
        
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <h3>INTERNAL AUDIT</h3>

    <table >
        <tr>
            <td class="style3" >

            </td>
            <td class="style3" >

               <asp:ImageButton ID="ImageButtonPTS_1S" runat="server" ImageUrl="~/Images/PTS_1S.png" 
                    PostBackUrl="~/webforms/PTS_1S.aspx" ToolTip="Payments PTS versus 1S" BorderColor="White" 
                    BorderWidth="1px" />

            </td>
            <td class="style7" >

                <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
                Payments PTS versus 1S</div>

            </td>
        </tr>

        <tr>
            <td class="style3" >

            </td>
            <td class="style3" >

               <asp:ImageButton ID="ImageButtonPTS_1S_Sum" runat="server" ImageUrl="~/Images/PTS_1S_sum.png" 
                    PostBackUrl="~/webforms/PTS_1S_Sum.aspx" ToolTip="Payments PTS versus 1S Summation" BorderColor="White" 
                    BorderWidth="1px" />

            </td>
            <td class="style7" >

                <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
                Payments PTS versus 1S Summation</div>

            </td>
        </tr>

        <tr>
            <td class="style3" >

            </td>
            <td class="style3" >

               <asp:ImageButton ID="ImageButtonCertificate" runat="server" ImageUrl="~/Images/Certificate.png" 
                    PostBackUrl="~/Certification.aspx" ToolTip="Certification" BorderColor="White" 
                    BorderWidth="1px" />

            </td>
            <td class="style7" >

                <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
                Certification</div>

            </td>
        </tr>

    </table>

</asp:Content>

