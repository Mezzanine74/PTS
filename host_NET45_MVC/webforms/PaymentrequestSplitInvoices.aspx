<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="PaymentrequestSplitInvoices.aspx.vb" Inherits="PaymentrequestSplitInvoices" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div style="float: left; margin-right:10px; width:350px;">
    <table class="table table-nonfluid">
        <tr>
            <td>
                InvoiceId >
            </td>
            <td>
                <asp:Literal ID="LiteralInvoiceId" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                PayReqNo >
            </td>
            <td>
                <asp:Literal ID="LiteralPayReqNo" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                Payment Status >
            </td>
            <td>
                <asp:Literal ID="LiteralPaid" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                Invoice No >
            </td>
            <td>
                <asp:Literal ID="LiteralInvoiceNo" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                Invoice Value Exc VAT >
            </td>
            <td>
                <span id="InvoiceValueExcVAT"><asp:Literal ID="LiteralInvoiceValueExcVAT" runat="server"></asp:Literal></span>&nbsp;
                             <asp:Literal ID="LiteralCurrency" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                VAT % > 
            </td>
            <td>
                <asp:Literal ID="LiteralVATPercent" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                VAT free >  
            </td>
            <td>
                <asp:Literal ID="LiteralVATFree" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                PayReqDate >  
            </td>
            <td>
                <asp:Literal ID="LiteralPayReqDate" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                Site Record No >  
            </td>
            <td>
                <asp:Literal ID="LiteralSiteRecordNo" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
        
    <asp:Label ID="LabelInfo" runat="server" Text="" ForeColor="Red"></asp:Label>

    </div>

    <div style="margin-left: 10px;">
    Into how many invoice you want to split > 
    <asp:DropDownList ID="DDLsplitnumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLsplitnumber_SelectedIndexChanged">
        <asp:ListItem Value="0" Text="select" Selected="True"></asp:ListItem>
        <asp:ListItem Value="2" Text="2" ></asp:ListItem>
        <asp:ListItem Value="3" Text="3" ></asp:ListItem>
    </asp:DropDownList>

    <asp:FileUpload ID="FileUploadInvoice" runat="server" />
    <asp:Label ID="LabelInvoiceUploadInfo" runat="server" CssClass="label label-danger inline" Visible="false">You didnt specify any file</asp:Label>
    <asp:LinkButton ID="LinkButtonInvoiceUpload" runat="server" CssClass="btn btn-lg btn-success" style="margin-top:5px!important;" OnClick="LinkButtonInvoiceUpload_Click" CausesValidation="false">
        Upload Invoice To Split
        <i class="ace-icon fa fa-upload "></i>
    </asp:LinkButton>

    <asp:HiddenField ID="HiddenInvoiceLink" runat="server"></asp:HiddenField>



        <br /><br />
        Total Splitted >  <asp:Literal ID="LiteralTotalSplitted" runat="server"></asp:Literal>
        <br />

        <asp:TextBox ID="TextBox1" runat="server" placeholder="Invoice Value Exc VAT 1" Visible="false" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" Display="Dynamic" Enabled="false" 
             ControlToValidate="TextBox1" ></asp:RequiredFieldValidator>

        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Enabled="false" 
            ControlToValidate="TextBox1"  Display="Dynamic"
            ErrorMessage="not valid number" 
            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
            </asp:RegularExpressionValidator>                            


        <asp:TextBox ID="TextBox2" runat="server" placeholder="Invoice Value Exc VAT 2" Visible="false" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required" Display="Dynamic" Enabled="false" 
             ControlToValidate="TextBox2" ></asp:RequiredFieldValidator>

        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Enabled="false"  
            ControlToValidate="TextBox2"  Display="Dynamic"
            ErrorMessage="not valid number" 
            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
            </asp:RegularExpressionValidator>                            

        <asp:TextBox ID="TextBox3" runat="server" placeholder="Invoice Value Exc VAT 3" Visible="false" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required" Display="Dynamic" Enabled="false" 
             ControlToValidate="TextBox3" ></asp:RequiredFieldValidator>

        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Enabled="false" 
            ControlToValidate="TextBox3"  Display="Dynamic"
            ErrorMessage="not valid number" 
            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
            </asp:RegularExpressionValidator>                            


    <asp:Button ID="ButtonSubmit" runat="server" CssClass="btn btn-mini btn-success" OnClick="ButtonSubmit_Click" Text="Split"/>

    </div>


<div style="text-align: center; width: 100%; display:none;" >
    <rsweb:ReportViewer ID="ReportViewerInvoiceCoverPage" runat="server"  ProcessingMode="remote" 
    ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
    ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
    ShowToolBar="false" ShowZoomControl="true" Visible="false" 
    SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
    <ServerReport
       DisplayName="CostReport" ReportPath="/"
          ReportServerUrl="http://localhost/ReportServer_SQLEXPRESS" />
    </rsweb:ReportViewer>
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" Runat="Server">

    <script type="text/javascript">

        $(function () {

            $("input[id*='TextBox1']").keyup(function () {
                var invValue = Math.round($("#InvoiceValueExcVAT").text() * 100) / 100;
                var Txt_1 = Math.round($("input[id*='TextBox1']").val() * 100) / 100 ; 

                var diff = Math.round((invValue - Txt_1) * 100) / 100;

                if (diff > 0) {
                    $("input[id*='TextBox2']").val(diff);
                }
                else {
                    $("input[id*='TextBox2']").val("error");
                }

            });

            $("input[id*='TextBox2']").keyup(function () {
                var invValue = Math.round($("#InvoiceValueExcVAT").text() * 100) / 100;
                var Txt_2 = Math.round($("input[id*='TextBox2']").val() * 100) / 100;

                var diff = Math.round((invValue - Txt_2) * 100) / 100;

                if (diff > 0) {
                    $("input[id*='TextBox1']").val(diff);
                }
                else {
                    $("input[id*='TextBox1']").val("error");
                }

            });

        })

    </script>



</asp:Content>

