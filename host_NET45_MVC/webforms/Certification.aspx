<%@ Page Title="" Language="VB" MasterPageFile="~/SiteCertificate.master" AutoEventWireup="false" CodeFile="Certification.aspx.vb" Inherits="Certification" %>

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

    <table >
        <tr>
            <td class="style3" >

            </td>
            <td class="style3" >

               <asp:ImageButton ID="ImageButtonDocument" runat="server" ImageUrl="~/Images/Cerf_Document.png" 
                    PostBackUrl="~/webforms/CertificationEnterDocument.aspx" ToolTip="Enter Certification Document" BorderColor="White" 
                    BorderWidth="1px" />

            </td>
            <td class="style7" >

                <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
                Enter Certification Document</div>

            </td>
            <td class="style3" >

            </td>
            <td class="style3" >

               <asp:ImageButton ID="ImageButtonDocumentEdit" runat="server" ImageUrl="~/Images/Cerf_Document_Edit.png" 
                    PostBackUrl="~/webforms/CertificationEditDocument.aspx" ToolTip="Edit Certification Document" BorderColor="White" 
                    BorderWidth="1px" />

            </td>
            <td class="style7" >

                <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
                Edit Certification Document</div>

            </td>

        </tr>

        <tr>
            <td class="style3" >

            </td>
            <td class="style3" >

               <asp:ImageButton ID="ImageButtonInvoice" runat="server" ImageUrl="~/Images/Cerf_Invoice.png" 
                    PostBackUrl="~/webforms/CertificationEnterInvoice.aspx" ToolTip="Enter Certification Invoice" BorderColor="White" 
                    BorderWidth="1px" />

            </td>
            <td class="style7" >

                <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
                Enter Certification Invoice</div>

            </td>
            <td class="style3" >

            </td>
            <td class="style3" >

               <asp:ImageButton ID="ImageButtonInvoiceEdit" runat="server" ImageUrl="~/Images/Cerf_Invoice_Edit.png" 
                    PostBackUrl="~/webforms/CertificationEditInvoice.aspx" ToolTip="Edit Certification Invoice" BorderColor="White" 
                    BorderWidth="1px" />

            </td>
            <td class="style7" >

                <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
                Enter Certification Invoice</div>

            </td>

        </tr>
        <tr>
            <td class="style3" >

            </td>
            <td class="style3" >

               <asp:ImageButton ID="ImageButtonPayment" runat="server" ImageUrl="~/Images/Cerf_Payment.png" 
                    PostBackUrl="~/webforms/CertificationEnterPayment.aspx" ToolTip="Enter Certification Payment" BorderColor="White" 
                    BorderWidth="1px" />

            </td>
            <td class="style7" >

                <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
                Enter Certification Payment</div>

            </td>
            <td class="style3" >

            </td>
            <td class="style3" >

               <asp:ImageButton ID="ImageButtonPaymentEdit" runat="server" ImageUrl="~/Images/Cerf_Payment_Edit.png" 
                    PostBackUrl="~/webforms/CertificationEditPayment.aspx" ToolTip="Edit Certification Payment" BorderColor="White" 
                    BorderWidth="1px" />

            </td>
            <td class="style7" >

                <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
                Edit Certification Payment</div>

            </td>

        </tr>

    </table>

</asp:Content>

