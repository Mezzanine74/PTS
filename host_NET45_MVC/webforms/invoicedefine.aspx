<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="invoicedefine.aspx.vb" Inherits="invoicedefine" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Define Invoice</title>
    <script type="text/javascript">

        function pageLoad() {
        }

    </script>
    
    <style type="text/css">
        #form1
        {
        }
        .style8
        {
        }
        .style10
        {
            text-align: right;
        }
        .style12
        {
             width: 240px;       
        }
        .style13
        {
            width: 96px;
            text-align: right;
        }
        .style14
        {
            width: 240px;
            text-align: left;
        }
        .style15
        {
            width: 107px;
        }
        .style16
        {
            width: 310px;
        }
        .style17
        {
        }
        .style18
        {
            width: 91px;
            text-align: right;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <asp:ModalPopupExtender ID="ModalPopupExtenderPayReqNoHistory" runat="server"
   TargetControlID="ButtonPayReqNoHistory"
   PopupControlID="PanelPayReqNoHistory"
   BackgroundCssClass="modalBackground"
   CancelControlID="btnCancelPayReqNoHistory"
   PopupDragHandleControlID="PanelPayReqNoHistory" >
  </asp:ModalPopupExtender>
  <asp:Panel ID="PanelPayReqNoHistory" runat="server" Style="display:none;" >
     <div style="text-align: right">
                <asp:Button ID="btnCancelPayReqNoHistory" runat="server" Text="X" 
                 Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066" 
                 OnClientClick="changeClass" />
     </div>

      <asp:Image ID="ImagePDFinstrcion" runat="server" ImageUrl="~/images/InvoicePDF_Instruction.png" />

  </asp:Panel>
  <asp:Button id="ButtonPayReqNoHistory"  runat="server" CssClass="hidepanel"/>
    
<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
 TargetControlID="lnkPopup"
 PopupControlID="panEdit"
 BackgroundCssClass="modalBackground"
 CancelControlID="btnCancel"
 PopupDragHandleControlID="panEdit" >
</asp:ModalPopupExtender>

<asp:Panel ID="panEdit" runat="server" 
          BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" 
          BackColor="White" CssClass="hidepanel" >
        <h2 style="text-align: center; color: #FF0000;">Large File</h2>
        <div style="text-align: center; font-size: 12px; color: #333333; font-weight: bold;">You are uploading a pdf file which is bigger than 1.2 MB in size.</div>
        <div style="text-align: center; font-size: 12px; color: #333333; font-weight: bold;">Please see how to optimize PDF files in <a style="font-size: 14px;" href="http://mercuryeng.org/HOW_TO_REDUCE_PDF_SIZE.htm" target="_blank">this link</a> . It is very straightforward process.</div>
        <div style="text-align: center; font-size: 12px; color: #333333; font-weight: bold;">Or you can visit this 
            <a href="http://smallpdf.com/" target="_blank">site</a> 
            to reduce your file size online</div>
        <div style="text-align: center; font-size: 12px; color: #333333; font-weight: bold;">Thanks for your understanding</div>
       <br />
       <table style="width: 100%">
        <tr>
         <td style="width: 50%; text-align: center;">
           <asp:Button ID="btnCancel" runat="server" Text="CLOSE" causesValidation = "false"
              Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="Red" />
         </td>
        </tr>
       </table>

</asp:Panel>
<a id="lnkPopup" runat="server"></a>
   
        <asp:FormView ID="FormViewInvoice" runat="server" DataKeyNames="InvoiceID" 
            DataSourceID="SqlDataSourceInvoice" DefaultMode="Insert" >
            <EditItemTemplate>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table >
                    <tr style="height: 50px">
                        <td class="style15">
                            <asp:Label ID="LabelPrjName" runat="server" 
                                AssociatedControlID="DropDownListPrjID" Text="Project Name:" 
                                ></asp:Label>
                        </td>
                        <td class="style17">
                            <asp:DropDownList ID="DropDownListPrjID" runat="server" AutoPostBack="True" 
                                DataSourceID="SqlDataSourcePrj" DataTextField="ProjectName" 
                                DataValueField="ProjectID"
                                ondatabound="DropDownListPrjID_DataBound" 
                                onselectedindexchanged="DropDownListPrjID_SelectedIndexChanged" 
                                Width="300px" >
                            </asp:DropDownList>
                        </td>
                        <td >
                             </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:DropDownList ID="DropDownListPOselector" runat="server" 
                                AutoPostBack="True" DataSourceID="SqlDataSourcePoSelector" 
                                DataTextField="POText1" DataValueField="PO_No" CssClass="form-group form-control ddl_fxfnt"
                                ondatabound="DropDownListPOselector_DataBound" 
                                onselectedindexchanged="DropDownListPOselector_SelectedIndexChanged" 
                                Width="900px"  
                                onprerender="DropDownListPOselector_PreRender" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style15">
                            
                            <asp:CompareValidator ID="CompareValidatorPoselector" runat="server"
                            ErrorMessage="Required"
                            ControlToValidate="TextBoxPOselectorWordCount"
                             ValueToCompare="1" Operator="NotEqual" Type="Integer" 
                                ValidationGroup="insertbutton" Display="Dynamic"></asp:CompareValidator>
                            
                        </td>
                        <td class="style17" colspan="2">
                             </td>
                        <td class="style16">
                             </td>
                        <td>
                             </td>
                        <td>
                             </td>
                        <td>
                             </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:Label ID="LabelDoublicateInvoiceWarning" runat="server" Font-Bold="True" 
                                ForeColor="Red" 
                                
                                Text="This Invoice has been defined before! Check Invoice Date and Number again!" 
                                 />
                            <asp:GridView ID="GridViewInvoiceDoublicate" runat="server" 
                                AutoGenerateColumns="False" 
                                DataKeyNames="SupplierID" 
                                DataSourceID="SqlDataSourceInvoiceDublicateCheck" CssClass="Grid" 
                                ondatabinding="GridViewInvoiceDoublicate_DataBinding" 
                                onprerender="GridViewInvoiceDoublicate_PreRender" 
                                onrowcommand="GridViewInvoiceDoublicate_RowCommand" 
                                onrowdatabound="GridViewInvoiceDoublicate_RowDataBound" 
                                onload="GridViewInvoiceDoublicate_Load" >
                                <Columns>
                                    <asp:TemplateField HeaderText="SupplierID" SortExpression="SupplierID" ControlStyle-Width="65" HeaderStyle-Width="65">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("SupplierID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ProjectName" SortExpression="ProjectName" ControlStyle-Width="70" HeaderStyle-Width="70">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO_No" SortExpression="PO_No" ControlStyle-Width="80" HeaderStyle-Width="80">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" SortExpression="Description" ControlStyle-Width="130" HeaderStyle-Width="130">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName" ControlStyle-Width="70" HeaderStyle-Width="70">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice_No" SortExpression="Invoice_No" ControlStyle-Width="80" HeaderStyle-Width="80">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("Invoice_No") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice_Date" SortExpression="Invoice_Date" ControlStyle-Width="80" HeaderStyle-Width="80">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" 
                                                Text='<%# Bind("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PDF" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="OpenPdf" 
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToInvoice") %>'
                                         CausesValidation="False" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency" SortExpression="Currency" ControlStyle-Width="51" HeaderStyle-Width="51">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req.Date"  ControlStyle-Width="80" HeaderStyle-Width="80">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8ReqDate" runat="server" Text='<%# Bind("PayReqDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved By"  ControlStyle-Width="80" HeaderStyle-Width="80">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8PersonApprove" runat="server" Text='<%# Bind("PersonApprove") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="Paymant Date"  ControlStyle-Width="80" HeaderStyle-Width="80">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8PaymentDate" runat="server" Text='<%# Bind("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                        
                                    
                                    
                                </Columns>
                                <RowStyle  CssClass="GridItemNakladnaya" />
                                <HeaderStyle  CssClass="GridHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:CompareValidator ID="CompareValidatorDublicatingIncoiveRowCount" 
                                runat="server" 
                                 ErrorMessage="Dublicating invoice!" 
                                ValueToCompare="0" ControlToValidate="TextBoxDuplicateInvoiceRowCount" 
                                Type="Integer" ValidationGroup="insertbutton" Display="Dynamic"></asp:CompareValidator>
                        </td>
                    </tr>
                </table>
                 <br />
                <table >
                    <tr>
                        <td class="style18">
                            <asp:Label ID="Label9" runat="server" AssociatedControlID="DropDownListInvoiceType" Text="Invoice Type" />
                        </td>
                        <td class="style12">
                        <asp:DropDownList ID="DropDownListInvoiceType" runat="server" 
                            SelectMethod="GetDropDownListInvoiceType"
                            AppendDataBoundItems="true"
                            ItemType="PTS_App_Code_VB_Project.PTS_MERCURY.db.Table3_Invoice_Type"
                            DataTextField="Type_name" DataValueField="Type_id">
                            <asp:ListItem Value="0">Select Invoice Type:</asp:ListItem>
                        </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceType" runat="server" 
                                ControlToValidate="DropDownListInvoiceType" InitialValue="0"
                                ErrorMessage="Required" ValidationGroup="insertbutton" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="style13">

                        </td>
                        <td class="style14">

                        </td>
                    </tr>
                    <tr>
                        <td class="style18">
                            <asp:Label ID="LabelInvoice_No" runat="server" 
                                AssociatedControlID="Invoice_NoTextBox" Text="Invoice_No:" 
                                 />
                        </td>
                        <td class="style12">
                            <asp:TextBox ID="Invoice_NoTextBox" runat="server" AutoPostBack="True" 
                                Text='<%# Bind("Invoice_No") %>'  
                                ontextchanged="Invoice_NoTextBox_TextChanged" 
                                ValidationGroup="invoiceno" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="Invoice_NoTextBox"  
                                ErrorMessage="Required" ValidationGroup="invoiceno" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertButton" runat="server" 
                                ControlToValidate="Invoice_NoTextBox"  
                                ErrorMessage="Required" ValidationGroup="insertbutton" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style13">
                            <asp:Label ID="Label2" runat="server" Text="PO Total Exc VAT: " ></asp:Label>
                        </td>
                        <td class="style14">
                            <asp:TextBox ID="TextBoxTotalPO" runat="server" ReadOnly="True" 
                                Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style18">
                            <asp:Label ID="LabelInvoice_Date" runat="server" AssociatedControlID="Invoice_DateTransferTextBox" Text="Invoice_Date:" />
                        </td>
                        <td class="style12">

                                        <asp:TextBox ID="Invoice_DateTransferTextBox" runat="server" 
                                            AutoPostBack="True"  CausesValidation="True" CssClass="add_datepicker"
                                            ontextchanged="Invoice_DateTransferTextBox_TextChanged" 
                                            ValidationGroup="invoicedate" />
<%--                            <asp:CalendarExtender ID="Invoice_DateTransferTextBox_CalendarExtender" 
                                runat="server" Format="dd/MM/yyyy"  CssClass="cal_Theme1"
                                TargetControlID="Invoice_DateTransferTextBox">
                            </asp:CalendarExtender>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="Invoice_DateTransferTextBox"  
                                ErrorMessage="Required" ValidationGroup="invoicedate" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertButton2" runat="server" 
                                ControlToValidate="Invoice_DateTransferTextBox"  
                                ErrorMessage="Required" ValidationGroup="insertbutton" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorInvDate" runat="server" 
                            ControlToValidate="Invoice_DateTransferTextBox"  
                            ErrorMessage="dd/mm/yyyy" 
                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                                ValidationGroup="invoicedate" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorInsertButton" runat="server" 
                            ControlToValidate="Invoice_DateTransferTextBox"  
                            ErrorMessage="dd/mm/yyyy" 
                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                                ValidationGroup="insertbutton" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td class="style13">
                            <asp:Label ID="Label3" runat="server" Text="Total Invoice Exc VAT: " 
                                 ForeColor="Blue"></asp:Label>
                        </td>
                        <td class="style14">
                            <asp:TextBox ID="TextBoxTotalInvoice" runat="server" ReadOnly="True" 
                                 Width="120px" BackColor="#E8E8FF" 
                                BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" ForeColor="Blue"></asp:TextBox>
                            <asp:Image ID="ImageTotalInvoiceProgress" runat="server" Height="10px" 
                                ImageUrl="~/Images/bar.bmp" Width="0px" />
                            <asp:Label ID="LabelTotalInvoicePercent" runat="server" Font-Bold="True" 
                                Font-Names="Segoe UI" Font-Size="12px" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style18">
                            <asp:Label ID="LabelInvoiceValue" runat="server"  Text="InvoiceValue Exc VAT:" 
                                 />
                        </td>
                        <td class="style12">
                            <asp:TextBox ID="InvoiceValueTextBox" runat="server" Text='<%# Bind("InvoiceValue") %>' 
                                CausesValidation="True" AutoPostBack="True"  ontextchanged="InvoiceValueTextBox_TextChanged" 
                                ValidationGroup="General" />
<%--                                            <asp:TextBoxWatermarkExtender ID="TextBoxTenderSearch_TextBoxWatermarkExtender" 
                                                runat="server" TargetControlID="InvoiceValueTextBox" 
                                                WatermarkText="Exc. VAT" WatermarkCssClass="TextBoxWaterMark">
                                            </asp:TextBoxWatermarkExtender>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="InvoiceValueTextBox"  
                                ErrorMessage="Required" ValidationGroup="General" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4InsertButton" runat="server" 
                                ControlToValidate="InvoiceValueTextBox"  
                                ErrorMessage="Required" ValidationGroup="insertbutton" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="InvoiceValueTextBox"  
                                ErrorMessage="Not Valid" 
                                ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                                ValidationGroup="General" Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2InsertButton" runat="server" 
                                ControlToValidate="InvoiceValueTextBox"  
                                ErrorMessage="Not Valid" 
                                ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                                ValidationGroup="insertbutton" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td class="style13">
                            <asp:Label ID="Label4" runat="server" Text="Outstanding Exc VAT:" 
                                 ForeColor="Blue"></asp:Label>
                        </td>
                        <td class="style14">
                            <asp:TextBox ID="TextBoxOutstanding" runat="server" ReadOnly="True" 
                                 Width="120px" ValidationGroup="General" 
                                BackColor="#E8E8FF" BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" 
                                ForeColor="Blue"></asp:TextBox>
                            <asp:Image ID="ImageTotalOutstandingProgress" runat="server" Height="10px" 
                                ImageUrl="~/Images/bar.bmp" Width="0px" />
                            <asp:Label ID="LabelTotalOutstandingPercent" runat="server" Font-Bold="True" 
                                Font-Names="Segoe UI" Font-Size="12px" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style18">
                            <asp:Label ID="LabelInvoicePDF" runat="server" AssociatedControlID="InvoiceValueTextBox"  Text="Invoice PDF:" />
                            <br />
                            <asp:LinkButton id="ButtonWhatIsThis"  runat="server" Text="(What Is This?)" OnClick="ButtonWhatIsThis_Click" Font-Size="10px" ForeColor="Red"/>
                        </td>
                        <td class="style12">
                            <asp:FileUpload ID="FileUploadPDF" runat="server"  />
                            <br />
                            <asp:LinkButton ID="ButtonUploadPDF" runat="server" CausesValidation="False" 
                                CssClass="btn btn-lg btn-pink"  OnClick="ButtonUploadPDF_Click" >
                                Upload
                            </asp:LinkButton>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPDF" runat="server" ControlToValidate="TextBoxLinkToInvoicePDF"  ErrorMessage="PDF Required" ValidationGroup="insertbutton" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="TextBoxLinkToInvoicePDF" CssClass="hidepanel" runat="server" ></asp:TextBox>
                            <asp:Label ID="LabelInfoInvoice" runat="server"  ForeColor="Red" style="font-weight: 700"></asp:Label>

                        </td>
                        <td class="style13">&nbsp;</td>
                        <td class="style14">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style18">
                            <asp:Label ID="LabelNotes" runat="server" AssociatedControlID="NotesTextBox" 
                                Text="Invoice Description:"   
                                ForeColor="Blue" />
                        </td>
                        <td  colspan="3">
                                
                                 <table >
                                    <tr>
                                        <td>
                            <asp:TextBox ID="NotesTextBox" runat="server" Text='<%# Bind("Notes") %>' 
                                 Height="50px" TextMode="MultiLine" Font-Bold="True" 
                                                Font-Italic="True" Font-Names="Segoe UI" ForeColor="Blue" 
                                                BackColor="#E8E8FF" BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" />
                                        </td>
                                        <td>
                                            <div style="background-color:#FFD600; color: #FF0000; font-size: 12px; font-weight: bold;"><<< the invoice definition terms goes here, e.g. "%", "advance", "payment", "percent"</div>
                                            <div style="background-color:#FFD600; color: #FF0000; font-size: 12px; font-weight: bold;"><<< automatic descriptions will popup for your convinience, you can revise if required.</div>
                                         </td>
                                    </tr>
                                </table>                               
                        </td>
                    </tr>
                    <tr>
                        <td class="style10" colspan="2">
                            <asp:CompareValidator ID="CompareValidator1InsertButton2" runat="server" 
                                BackColor="Yellow" BorderColor="#333333" ControlToCompare="TextBoxOutstanding" 
                                ControlToValidate="InvoiceValueTextBox"  
                                Display="Dynamic" ErrorMessage="Invoice Value Greater Than Outstanding!" 
                                Font-Bold="True" Operator="LessThanEqual" SetFocusOnError="True" Type="Double" 
                                ValidationGroup="insertbutton"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidatorCheckOutstanding" runat="server" 
                                BackColor="Yellow" BorderColor="#333333" ControlToCompare="TextBoxOutstanding" 
                                ControlToValidate="InvoiceValueTextBox"  
                                Display="Dynamic" ErrorMessage="Invoice Value Greater Than Outstanding!" 
                                Font-Bold="True" Operator="LessThanEqual" SetFocusOnError="True" Type="Double" 
                                ValidationGroup="General"></asp:CompareValidator>

                            <asp:CompareValidator ID="CompareValidatorAdvance" runat="server" 
                                BackColor="Yellow" BorderColor="#333333" ControlToCompare="TextBoxAdvance" 
                                ControlToValidate="InvoiceValueTextBox"  
                                Display="Dynamic" ErrorMessage="Invoice Value Bigger Than Advance Payment of Contract!" 
                                Font-Bold="True" Operator="LessThanEqual" SetFocusOnError="True" Type="Double" 
                                ValidationGroup="insertbutton"></asp:CompareValidator>

                            <asp:CompareValidator ID="CompareValidatorAdvanceAddendum" runat="server" 
                                BackColor="Yellow" BorderColor="#333333" ControlToCompare="TextBoxAdvanceAddendum" 
                                ControlToValidate="InvoiceValueTextBox"  
                                Display="Dynamic" ErrorMessage="Invoice Value Bigger Than Advance Payment of Addendum!" 
                                Font-Bold="True" Operator="LessThanEqual" SetFocusOnError="True" Type="Double" 
                                ValidationGroup="insertbutton"></asp:CompareValidator>

                            <asp:TextBox ID="TextBoxAdvance" runat="server" CssClass="hidepanel" />
                            <asp:TextBox ID="TextBoxAdvanceAddendum" runat="server" CssClass="hidepanel" />

                        </td>
                        <td class="style10" colspan="2">
                            <asp:CompareValidator ID="CompareValidatorGreaterThanZero" runat="server" 
                                BackColor="Yellow" BorderColor="#333333" 
                                ControlToValidate="InvoiceValueTextBox"  
                                Display="Dynamic" ErrorMessage="Invoice value must be greater than zero!" 
                                Font-Bold="True" Operator="GreaterThan" Type="Double" ValidationGroup="General" 
                                ValueToCompare="0"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator1InsertButton" runat="server" 
                                BackColor="Yellow" BorderColor="#333333" 
                                ControlToValidate="InvoiceValueTextBox"  
                                Display="Dynamic" ErrorMessage="Invoice value must be greater than zero!" 
                                Font-Bold="True" Operator="GreaterThan" Type="Double" 
                                ValidationGroup="insertbutton" ValueToCompare="0"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style10" colspan="4">
                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CssClass="btn btn-primary"
                                CommandName="Insert" onclick="InsertButton_Click" 
                                PostBackUrl="~/webforms/paymentrequest.aspx" Text="Insert" 
                                ValidationGroup="insertbutton" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style8" colspan="4">
                            <asp:GridView ID="GridViewInvoiceHistoryperPO" runat="server" 
                                AutoGenerateColumns="False" DataKeyNames="PO_No" 
                                DataSourceID="SqlDataSourceInvoiceHistoryPerPO" CssClass="Grid" 
                                 >
                                <Columns>
                                    <asp:TemplateField HeaderText="PO No" SortExpression="PO_No" ControlStyle-Width="80" HeaderStyle-Width="80">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Labelpono" runat="server" Text='<%# Eval("PO_No") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice_No" SortExpression="Invoice_No" ControlStyle-Width="80" HeaderStyle-Width="80">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Labelinvno" runat="server" Text='<%# Eval("Invoice_No") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice_Date" SortExpression="Invoice_Date" ControlStyle-Width="80" HeaderStyle-Width="80">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Labelinvdate" runat="server" Text='<%# Eval("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="InvoiceValue" SortExpression="InvoiceValue" ControlStyle-Width="80" HeaderStyle-Width="80">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Labelinvvalue" runat="server" Text='<%# Eval("InvoiceValue","{0:###,###,###.00}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO_Currency" SortExpression="PO_Currency" ControlStyle-Width="51" HeaderStyle-Width="51">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Labelcurrency" runat="server" Text='<%# Eval("PO_Currency") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UpdatedBy" SortExpression="UpdatedBy" ControlStyle-Width="80" HeaderStyle-Width="80">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LabelupdatedBy" runat="server" Text='<%# Eval("UpdatedBy","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle  CssClass="GridItemNakladnaya" />
                                <HeaderStyle  CssClass="GridHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>

                <asp:Panel ID="Panel1" runat="server" CssClass="hidepanel" >
                            <asp:SqlDataSource ID="SqlDataSourcePOSum" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SELECT [PO_No], [PoSumExcVAT] FROM [View_POsumCommon3] where ([PO_No]=@PO_No)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListPOselector" DefaultValue="0" 
                                        Name="PO_No" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSourceInvoicedPO" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SELECT [PO_No], case when [InvoiceSum] is null then 0 else [InvoiceSum] end AS InvoiceSum FROM [View_POsumCommon3] WHERE ([PO_No]=@PO_No)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListPOselector" DefaultValue="0" 
                                        Name="PO_No" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:DropDownList ID="DropDownListPOSumHolder" runat="server" 
                                DataSourceID="SqlDataSourcePOSum" DataTextField="PO_No" 
                                DataValueField="PoSumExcVAT" Height="21px" Width="188px">
                            </asp:DropDownList>
                            <br />
                            <asp:DropDownList ID="DropDownListTotalInvoiceHolder" runat="server" 
                                DataSourceID="SqlDataSourceInvoicedPO" DataTextField="PO_No" 
                                DataValueField="InvoiceSum" Height="20px" Width="173px">
                            </asp:DropDownList>
                            <br />
                            <asp:DropDownList ID="DropDownListOutstanding" runat="server" 
                                DataSourceID="SqlDataSourceOutstanding" DataTextField="PO_No" 
                                DataValueField="Outstanding" Height="16px" Width="174px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceOutstanding" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SELECT [PO_No], [Outstanding] FROM [View_POsumCommon3] WHERE ([PO_No]=@PO_No)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListPOselector" DefaultValue="0" 
                                        Name="PO_No" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlDataSourceInvoiceDublicateCheck" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SELECT * FROM [View_InvoiceDublicateCheck] WHERE ([SupplierID]=@SupplierID) AND ([Invoice_No]=@Invoice_No) AND ([Invoice_Date]=@Invoice_Date)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListINNCarrier" DefaultValue="0" 
                                        Name="SupplierID" PropertyName="SelectedValue" />
                                    <asp:ControlParameter ControlID="Invoice_NoTextBox" DefaultValue="_X_X" 
                                        Name="Invoice_No" PropertyName="Text" />
                                    <asp:Parameter Name="Invoice_Date" />

                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSourcePoSelector" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand=" SELECT     TOP (100) PERCENT dbo.Table2_PONo.PO_No, REPLACE(RIGHT('              ' + CAST(dbo.Table2_PONo.PO_No AS varchar(14)), 14) 
                      + '  ' + RIGHT('                                   ' + CAST(dbo.Table6_Supplier.SupplierName AS varchar(35)), 35) 
                      + '  ' + RIGHT('                                                            ' + CAST(dbo.Table2_PONo.Description AS varchar(60)), 60), ' ', '_') AS POText1, 
                      dbo.Table2_PONo.Approved
FROM         dbo.Table2_PONo INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID LEFT OUTER JOIN
                      dbo.Table3_Invoice ON dbo.Table2_PONo.PO_No = dbo.Table3_Invoice.PO_No
WHERE     (dbo.Table2_PONo.Project_ID = @ProjectID)
GROUP BY CONVERT(decimal(12, 2), 
                      (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table2_POno.TotalPrice ELSE dbo.Table2_POno.TotalPrice / ((100 + dbo.Table2_POno.VATpercent)
                       / 100) END)), dbo.Table2_PONo.PO_No, REPLACE(RIGHT('              ' + CAST(dbo.Table2_PONo.PO_No AS varchar(14)), 14) 
                      + '  ' + RIGHT('                                   ' + CAST(dbo.Table6_Supplier.SupplierName AS varchar(35)), 35) 
                      + '  ' + RIGHT('                                                            ' + CAST(dbo.Table2_PONo.Description AS varchar(60)), 60), ' ', '_'), 
                      dbo.Table2_PONo.Approved
HAVING      (CONVERT(decimal(12, 2), 
                      (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table2_POno.TotalPrice ELSE dbo.Table2_POno.TotalPrice / ((100 + dbo.Table2_POno.VATpercent)
                       / 100) END)) - SUM(CONVERT(decimal(12, 2), (CASE WHEN dbo.Table3_Invoice.InvoiceValue IS NULL 
                      THEN 0 ELSE dbo.Table3_Invoice.InvoiceValue END))) > 0) AND (ABS(CONVERT(decimal(12, 2), 
                      (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table2_POno.TotalPrice ELSE dbo.Table2_POno.TotalPrice / ((100 + dbo.Table2_POno.VATpercent)
                       / 100) END)) - SUM(CONVERT(decimal(12, 2), (CASE WHEN dbo.Table3_Invoice.InvoiceValue IS NULL 
                      THEN 0 ELSE dbo.Table3_Invoice.InvoiceValue END)))) / CONVERT(decimal(12, 2), 
                      (CASE WHEN dbo.Table6_Supplier.VAT_Free = 1 THEN dbo.Table2_POno.TotalPrice ELSE dbo.Table2_POno.TotalPrice / ((100 + dbo.Table2_POno.VATpercent)
                       / 100) END)) * 100 >= 0.0) AND (dbo.Table2_PONo.Approved <> 0 OR
                      dbo.Table2_PONo.Approved IS NULL)
ORDER BY dbo.Table2_PONo.PO_No DESC ">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListPrjID" DefaultValue="" 
                                        Name="ProjectID" PropertyName="SelectedValue" Type="Int16" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSourceInvoiceHistoryPerPO" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SELECT [PO_No], [Invoice_No], [Invoice_Date], [InvoiceValue], [PO_Currency], [UpdatedBy] FROM [View_InvoiceHistoryPerPO] WHERE ([PO_No] = @PO_No)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListPOselector" 
                                        DefaultValue="_" Name="PO_No" PropertyName="SelectedValue" 
                                        Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                                            <asp:DropDownList ID="DropDownListINNCarrier" runat="server" 
                                DataSourceID="SqlDataSourceINN" DataTextField="PO_No" 
                                DataValueField="SupplierID" Height="16px" Width="152px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceINN" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                SelectCommand="SELECT Table2_PONo.PO_No, Table6_Supplier.SupplierID FROM Table2_PONo INNER JOIN Table6_Supplier ON Table2_PONo.SupplierID = Table6_Supplier.SupplierID WHERE ([PO_No]=@PO_No)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListPOselector" DefaultValue="0" 
                                        Name="PO_No" PropertyName="SelectedValue" />
                                    <asp:ControlParameter ControlID="Invoice_NoTextBox" DefaultValue="_X_X" 
                                        Name="Invoice_No" PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <asp:TextBox ID="TextBoxInvValueCarrier" runat="server" Height="21px" 
                                 Width="134px"></asp:TextBox>
                            <asp:TextBox ID="TextBoxPOselectorWordCount" runat="server" 
                                 ></asp:TextBox>                                 
                            <asp:TextBox ID="TextBoxDuplicateInvoiceRowCount" runat="server" 
                                 ></asp:TextBox>                                                                  
                </asp:Panel>                
            </InsertItemTemplate>
            <ItemTemplate>
            </ItemTemplate>
        </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSourceInvoice" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SELECT * FROM [Table3_Invoice]" 
        
        InsertCommand="INSERT INTO Table3_Invoice( Invoice_No, Invoice_Date, PO_No, InvoiceValue, Notes, CreatedBy, PersonCreated) VALUES ( @Invoice_No, @Invoice_Date, @PO_No, @InvoiceValue, @Notes,  @CreatedBy, @PersonCreated);SELECT @ID=SCOPE_IDENTITY()">
        <InsertParameters>
            <asp:Parameter Name="CreatedBy" />
            <asp:Parameter Name="PersonCreated" />
            <asp:Parameter Direction="Output" Name="ID" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                
        SelectCommand=" SELECT * FROM (
                        -- ONLY NEW GENERATION PROJECTS WHERE USER HAS ACCESS
                        SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
                                              AS ProjectName, dbo.Table_Approval_UserRolePrjectJunction.UserName
                        FROM         dbo.Table1_Project INNER JOIN
                                              dbo.Table_Approval_UserRolePrjectJunction ON dbo.Table1_Project.ProjectID = dbo.Table_Approval_UserRolePrjectJunction.ProjectID
                        WHERE     (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.Table_Approval_UserRolePrjectJunction.UserName = @UserName) AND 
                                              (dbo.Table_Approval_UserRolePrjectJunction.RoleName = N'InitiateContractAndAddendum')

                        UNION ALL

                        -- ONLY NOT NEW GENERATION PROJECTS WHERE USER HAS ACCESS
                        SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID)) 
                                              AS ProjectName, dbo.aspnet_Users.UserName
                        FROM         dbo.Table1_Project INNER JOIN
                                              dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
                                              dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId
                        WHERE     (dbo.Table1_Project.CurrentStatus = 1) AND (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table1_Project.NewGeneration = 0)
                        ) AS Source
                        ORDER BY ProjectName ">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    <p>
        <asp:TextBox ID="TextBoxStoreInvoiceID" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="TextBoxUserName" runat="server" Visible="False"></asp:TextBox>
    </p>

    </asp:Content>

