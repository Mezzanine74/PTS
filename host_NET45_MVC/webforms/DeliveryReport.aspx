<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="DeliveryReport.aspx.vb" Inherits="DeliveryReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">

        function SetAutoCompleteWidth(source, EventArgs) {
            var target
            target = ((document.getElementBy) ? document.getElementById("AutoCompleteDiv") : document.all.AutoCompleteDiv);
            target.style.width = '500px';
        }

       </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


    <table class="table-condensed">
        <tr style="background-color:lightblue; font-size:12px;">
            <td>
                Supplier
            </td>
            <td>
                Start Date
            </td>
            <td>
                Finish Date
            </td>

        </tr>

        <tr>
            <td>
                <asp:TextBox ID="TextBoxSupplier" runat="server" Placeholder="Enter INN number or some text from Supplier Name" ></asp:TextBox>

                <div ID="AutoCompleteDiv" class="TextBoxGeneral" >
                </div>    
                                  
                <asp:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" 
                    CompletionInterval="0" CompletionListElementID="AutoCompleteDiv" 
                    CompletionSetCount="12" MinimumPrefixLength="0" 
                    ServiceMethod="SupplierEdit" onclientshown="SetAutoCompleteWidth" 
                    ServicePath="AutoComplete.asmx" TargetControlID="TextBoxSupplier">
                </asp:AutoCompleteExtender>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSupplier" runat="server" ErrorMessage="Required" ControlToValidate ="TextBoxSupplier" Display="Dynamic" ></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="TxtDate1" runat="server" CssClass="add_datepicker"  />
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDate1" runat="server" ErrorMessage="Required" ControlToValidate ="TxtDate1" Display="Dynamic" ></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorDate1" ControlToValidate="TxtDate1" Display="Dynamic"
                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" ></asp:RegularExpressionValidator>
            </td>
            <td>
                <asp:TextBox ID="TxtDate2" runat="server" CssClass="add_datepicker"  />

                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDate2" runat="server" ErrorMessage="Required" ControlToValidate ="TxtDate2" Display="Dynamic" ></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorDate2" ControlToValidate="TxtDate2" Display="Dynamic"
                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" ></asp:RegularExpressionValidator>
            </td>
            <td>
                <asp:Button ID="ButtonRun" runat="server" Text="Run Report" CssClass="btn btn-mini btn-default"/>
            </td>
            <td>
                <asp:Button ID="ButtonExportExcel" runat="server" Text="Export To Excel" CssClass="btn btn-mini btn-default" />
            </td>
        </tr>

    </table>

    <div style="text-align: center; width: 100%">
        <rsweb:ReportViewer ID="ReportViewerDelivery" runat="server"  ProcessingMode="remote" 
        ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
        ShowPageNavigationControls="true" ShowParameterPrompts="False" ShowPromptAreaButton="False"
        ShowToolBar="false" ShowZoomControl="true" Visible="false" 
        SizeToReportContent="True"  ZoomMode="FullPage"  AsyncRendering="False">
        </rsweb:ReportViewer>
    </div>


</asp:Content>

