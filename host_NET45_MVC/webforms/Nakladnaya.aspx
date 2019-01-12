<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="Nakladnaya.aspx.vb" Inherits="NakladnayaCopy2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

        .styleNakltr
        {
          height:30px!important;
        }
        
        .styleDescWidth
        {
          width:100px!important;
        }

     </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<h1><asp:Label ID="Label1" runat="server" ></asp:Label></h1>

<asp:Label ID="LabelMaxDocValue" runat="server" CssClass="hide"></asp:Label>

    <%-- MODAL POPUP NAKL_AKT_TOGETHER --%>

<div id="ModalNakl_Akt_Together" class="modal">
    <div class="modal-dialog modal-dialog-center">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Nakladnaya and Akt</h4>
            </div>
            <div class="modal-body">

                 <asp:Label ID="labelPOnoNakl_Akt_Together" runat="server" Text="Test" CssClass="label label-danger arrowed-in-right arrowed-in-left arrowed-in " ></asp:Label>

                 <table class="table-condensed" >
                  <tr>
                   <td colspan="2" style="text-align: right">
                    <asp:Button ID="ButtonEditNakl_Akt_Together" runat="server" Text="EDIT" CssClass="btn btn-info btn-mini"/>
                   </td>
                  </tr>
                     <tr>
                         <td></td>
                         <td>
                             <asp:FileUpload ID="FileUploadAkToNakladnaya" runat="server" />
                             <asp:Label ID="LabelInfoAktToNakladnaya" runat="server" ></asp:Label>
                             <asp:LinkButton ID="LinkButtonAktToNakladnayaPDF" runat="server" CssClass="btn btn-lg btn-success" OnClick="LinkButtonAktToNakladnayaPDF_Click" style="margin-top:5px!important;">
                                            Upload Document
                                            <i class="ace-icon fa fa-upload "></i>
                             </asp:LinkButton>

                            <asp:HiddenField ID="HiddenFieldAktToNakladnayaPDFLink" runat="server"></asp:HiddenField>

                         </td>
                         <td></td>
                     </tr>
                  <tr>
                   <td>
                       <%-- NAKLADNAYA GOES HERE --%>
                       <table class="table-condensed" >
                        <tr >
                         <td >
                          Invoice No or Contract No:
                         </td>
                         <td>
                          <asp:TextBox ID="TextBoxInvContNoNakl_Akt_Together" runat="server"></asp:TextBox>
                         </td>
                         <td>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvContNoNakl_Akt_Together" runat="server" 
                             ErrorMessage="Required" ControlToValidate="TextBoxInvContNoNakl_Akt_Together" Display="Dynamic"
                             ValidationGroup="Nakl_Akt_Together">
                           </asp:RequiredFieldValidator>
                          </td>
                        </tr>
                        <tr >
                         <td >
                         Nakladnaya No:
                         </td>
                         <td>
                          <asp:TextBox ID="TextBoxNaklNoNakl_Akt_Together" runat="server"></asp:TextBox>
                         </td>
                         <td>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidatorNakNoNakl_Akt_Together" runat="server" 
                             ErrorMessage="Required" ControlToValidate="TextBoxNaklNoNakl_Akt_Together" Display="Dynamic"
                             ValidationGroup="Nakl_Akt_Together">
                           </asp:RequiredFieldValidator>
                         </td>
                        </tr>
                        <tr >
                         <td >
                         Nakladnaya Date
                         </td>
                         <td>
                          <asp:TextBox ID="TextBoxNaklDateNakl_Akt_Together" runat="server" CssClass="add_datepicker"></asp:TextBox>
                         </td>
                         <td>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidatorNaklDateNakl_Akt_Together" runat="server" 
                             ErrorMessage="Required" ControlToValidate="TextBoxNaklDateNakl_Akt_Together" Display="Dynamic"
                             ValidationGroup="Nakl_Akt_Together">
                           </asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidatorPOdateNakl_Akt_Together" 
                           ControlToValidate="TextBoxNaklDateNakl_Akt_Together" Display="Dynamic"
                            runat="server" ErrorMessage="dd/mm/yyyy"  
                            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                            ValidationGroup="Nakl_Akt_Together"></asp:RegularExpressionValidator>
                         </td>
                        </tr>
                        <tr >
                         <td >
                          Nakladnaya Value in Ruble With VAT
                         </td>
                         <td>
                          <asp:TextBox ID="TextBoxNaklValueNakl_Akt_Together" runat="server"></asp:TextBox>
                         </td>
                         <td>
<%--                          <asp:RegularExpressionValidator ID="RegularExpressionValidatorTotalPriceNakl_Akt_Together" 
                            ControlToValidate="TextBoxNaklValueNakl_Akt_Together" Display="Dynamic"
                            runat="server" ErrorMessage="Wrong format"  
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                             ValidationGroup="Nakl_Akt_Together"></asp:RegularExpressionValidator>--%>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidatorNaklValueNakl_Akt_Together" runat="server" 
                             ErrorMessage="Required" ControlToValidate="TextBoxNaklValueNakl_Akt_Together" Display="Dynamic"
                             ValidationGroup="Nakl_Akt_Together">
                           </asp:RequiredFieldValidator>
                         </td>
                        </tr>
                           <tr >
                               <td >Comment</td>
                               <td>
                                   <asp:TextBox ID="TextBoxNaklValueNakl_Akt_TogetherComment" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
                               </td>
                               <td>&nbsp;</td>
                           </tr>
                       </table>
                   </td>
                   <td>
                       <%-- AKT GOES HERE --%>
                       <table class="table-condensed" >
                        <tr >
                         <td >
                         Akt No:
                         </td>
                         <td>
                          <asp:TextBox ID="TextBoxAktNoNakl_Akt_Together" runat="server"></asp:TextBox>
                         </td>
                         <td>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidatorAktNoNakl_Akt_Together" runat="server" 
                             ErrorMessage="Required" ControlToValidate="TextBoxAktNoNakl_Akt_Together" Display="Dynamic"
                             ValidationGroup="Nakl_Akt_Together">
                           </asp:RequiredFieldValidator>
                         </td>
                        </tr>
                        <tr >
                         <td >
                         Akt Date
                         </td>
                         <td>
                          <asp:TextBox ID="TextBoxAktDateNakl_Akt_Together" runat="server" CssClass="add_datepicker"></asp:TextBox>
                         </td>
                         <td>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidatorAktDateNakl_Akt_Together" runat="server" 
                             ErrorMessage="Required" ControlToValidate="TextBoxAktDateNakl_Akt_Together" Display="Dynamic"
                             ValidationGroup="Nakl_Akt_Together">
                           </asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidatorAktDateNakl_Akt_Together" 
                           ControlToValidate="TextBoxAktDateNakl_Akt_Together" Display="Dynamic"
                            runat="server" ErrorMessage="dd/mm/yyyy"  
                            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                            ValidationGroup="Nakl_Akt_Together"></asp:RegularExpressionValidator>
                         </td>
                        </tr>
                        <tr >
                         <td >
                          Akt Value in Ruble With VAT
                         </td>
                         <td>
                          <asp:TextBox ID="TextBoxAktValueNakl_Akt_Together" runat="server"></asp:TextBox>
                         </td>
                         <td>
<%--                          <asp:RegularExpressionValidator ID="RegularExpressionValidatorAktValueNakl_Akt_Together" 
                            ControlToValidate="TextBoxAktValueNakl_Akt_Together" Display="Dynamic"
                            runat="server" ErrorMessage="Wrong format"  
                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                             ValidationGroup="Nakl_Akt_Together"></asp:RegularExpressionValidator>--%>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidatorAktValueNakl_Akt_Together" runat="server" 
                             ErrorMessage="Required" ControlToValidate="TextBoxAktValueNakl_Akt_Together" Display="Dynamic"
                             ValidationGroup="Nakl_Akt_Together">
                           </asp:RequiredFieldValidator>
                         </td>
                        </tr>
                           <tr >
                               <td >Comment</td>
                               <td>
                                   <asp:TextBox ID="TextBoxCommentNakl_Akt_Together" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
                               </td>
                               <td>&nbsp;</td>
                           </tr>
                       </table>
                   </td>
                  </tr>
                  <tr>
                   <td >
                    Shot Faktura:
                    <asp:CheckBox ID="CheckBoxSFNakl_Akt_Together" runat="server"  />
                   </td>
                   <td>
                    <asp:Label id="LabelWarningSfNakl_Akt_Together" runat="server" Text="Required" 
                     Visible="false" ForeColor="Red"></asp:Label>
                   </td>
                  </tr>
                  <tr>
                   <td ColSpan="2" >
                    <asp:Label id="LabelWarningNakl_Akt_Together_NotPossible" runat="server" 
                     Text="There is seperate AKT and NAKLADNAYA. To proceed, you must delete." 
                     Visible="false" ForeColor="Red">
                    </asp:Label>
                     <asp:Button ID="ButtonInsertNakl_Akt_Together" runat="server" Text="INSERT" CssClass="btn btn-mini btn-success"
                      ValidationGroup="Nakl_Akt_Together" />
                   </td>
                  </tr>
                 </table>

            </div>
        </div>
    </div>
</div>

    <%-- /MODAL POPUP NAKL_AKT_TOGETHER --%>
    <%-- MODAL POPUP NAKLADNAYA --%>

<div id="ModalNakladnaya" class="modal">
    <div class="modal-dialog modal-dialog-center">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Nakladnaya</h4>
            </div>
            <div class="modal-body">

                 <asp:Label ID="labelPOnoNakl" runat="server" Text="Test" CssClass="label label-danger arrowed-in-right arrowed-in-left arrowed-in " ></asp:Label>
                 <table class="table-condensed">
                  <tr>
                   <td colspan="2" style="text-align: right">
                        <asp:Button ID="ButtonEditNakl" runat="server" Text="EDIT" CssClass="btn btn-mini btn-info" />
                   </td>
                  </tr>
                     <tr>
                         <td></td>
                         <td>
                             <asp:FileUpload ID="FileUploadNakladnayaPDF" runat="server" />
                             <asp:Label ID="LabelInfo" runat="server" CssClass="label label-danger inline" Visible="false">You didnt specify any file</asp:Label>
                             <asp:LinkButton ID="LinkButtonNakladnayaPDF" runat="server" CssClass="btn btn-lg btn-success" OnClick="LinkButtonNakladnayaPDF_Click" style="margin-top:5px!important;">
                                            Upload Document
                                            <i class="ace-icon fa fa-upload "></i>
                             </asp:LinkButton>

                            <asp:HiddenField ID="HiddenNakladnayaPDFLink" runat="server"></asp:HiddenField>

                         </td>
                         <td></td>
                     </tr>
                  <tr >
                   <td >
                    Invoice No or Contract No:
                   </td>
                   <td>
                    <asp:TextBox ID="TextBoxInvContNo" runat="server"></asp:TextBox>
                   </td>
                   <td>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvContNo" runat="server" 
                       ErrorMessage="Required" ControlToValidate="TextBoxInvContNo" Display="Dynamic"
                       ValidationGroup="Nakladnaya">
                     </asp:RequiredFieldValidator>
                    </td>
                  </tr>
                  <tr >
                   <td >
                   Shot Faktura:
                   </td>
                   <td>
                    <asp:CheckBox ID="CheckBoxNakladnayaSF" runat="server"  />
                   </td>
                   <td>
                    <asp:Label id="LabelWarningNaklSf" runat="server" Text="Required" 
                     Visible="false" ForeColor="Red"></asp:Label>
                   </td>
                  </tr>
                  <tr >
                   <td >
                   Nakladnaya No:
                   </td>
                   <td>
                    <asp:TextBox ID="TextBoxNaklNo" runat="server"></asp:TextBox>
                   </td>
                   <td>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorNakNo" runat="server" 
                       ErrorMessage="Required" ControlToValidate="TextBoxNaklNo" Display="Dynamic"
                       ValidationGroup="Nakladnaya">
                     </asp:RequiredFieldValidator>
                   </td>
                  </tr>
                  <tr >
                   <td >
                   Nakladnaya Date
                   </td>
                   <td>
                    <asp:TextBox ID="TextBoxNaklDate" runat="server" CssClass="add_datepicker"></asp:TextBox>
                   </td>
                   <td>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorNaklDate" runat="server" 
                       ErrorMessage="Required" ControlToValidate="TextBoxNaklDate" Display="Dynamic"
                       ValidationGroup="Nakladnaya">
                     </asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidatorPOdate" 
                     ControlToValidate="TextBoxNaklDate" Display="Dynamic"
                      runat="server" ErrorMessage="dd/mm/yyyy"  
                      ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                      ValidationGroup="Nakladnaya"></asp:RegularExpressionValidator>
                   </td>
                  </tr>
                  <tr >
                   <td >
                    Nakladnaya Value in Ruble With VAT
                   </td>
                   <td>
                    <asp:TextBox ID="TextBoxNaklValue" runat="server"></asp:TextBox>
                   </td>
                   <td>
<%--                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorTotalPrice" 
                      ControlToValidate="TextBoxNaklValue" Display="Dynamic"
                      runat="server" ErrorMessage="Wrong format"  
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="Nakladnaya"></asp:RegularExpressionValidator>--%>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorNaklValue" runat="server" 
                       ErrorMessage="Required" ControlToValidate="TextBoxNaklValue" Display="Dynamic"
                       ValidationGroup="Nakladnaya">
                     </asp:RequiredFieldValidator>
                   </td>
                  </tr>
                     <tr >
                         <td >Comment</td>
                         <td>
                             <asp:TextBox ID="TextBoxComment" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
                         </td>
                         <td>&nbsp;</td>
                     </tr>
                  <tr>
                  <td ColSpan="3" >
                    <asp:Label id="LabelNaklErrorWarning" runat="server" 
                     Text="There is AKT. To proceed, you must delete." 
                     Visible="false" ForeColor="Red">
                    </asp:Label>
                        <asp:Button ID="ButtonInsertNakl" runat="server" Text="INSERT" CssClass="btn btn-mini btn-success"
                        ValidationGroup="Nakladnaya" />
                  </td>
                  </tr>
                 </table>


            </div>
        </div>
    </div>
</div>

  <%-- /MODAL POPUP NAKLADNAYA --%> 
  <%-- MODAL POPUP NAKLADNAYA E D I T --%>

<div id="ModalNakladnayaEdit" class="modal">
    <div class="modal-dialog modal-dialog-center">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Nakladnaya Edit</h4>
            </div>
            <div class="modal-body" style="width:1000px; margin:10px; ">

                  <asp:Button ID="ButtonEditAktToNakladnaya" runat="server" Text="Edit Akt To Nakladnaya" CssClass="btn btn-mini btn-default" />
                  <asp:Button ID="ButtonSwitchToInsertNakl" runat="server" Text="Switch To Insert Mode" CssClass="btn btn-mini btn-default" />

                    <br /><br />

              <asp:UpdatePanel ID="updatePanelNaklEdit" runat="server">

                  <Triggers>
                    <asp:PostBackTrigger ControlID="GridViewNaklEdit" />
                  </Triggers>

                  <ContentTemplate>

                 <script type="text/javascript">
                     Sys.Application.add_load(PTS_BindEvents);
                 </script>


                  <div style=" height: 600px; overflow: auto;">

                  <asp:GridView ID="GridViewNaklEdit" runat="server" AllowPaging="False" 
                    AutoGenerateColumns="False" DataKeyNames="ID_Nak" CssClass="table" GridLines="None" 
                    DataSourceID="SqlDataSourceNakladnayaEdit" EnableModelValidation="True" >
                    <Columns>
                      <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="btn btn-minier btn-success "
                                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                                    OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                    CommandName="Delete" Text="Delete" CssClass="btn btn-minier btn-danger "></asp:LinkButton>

                                    <asp:Label ID="LabelMatchMessageItem" runat="server" Visible="false" ForeColor="red"
                                        Text="This item is marked as MATCH to 1S. You cannot change it unless finance authorize. You can change only comments section."></asp:Label>

                                  <asp:Image id="ImageButtonFollowUpReports" runat="server" Visible="false"
                                    ImageUrl="~/images/qmDelete.png" >
                                  </asp:Image>

                                    <asp:HoverMenuExtender ID="HoverMenuExtenderFollowUpReports"  
                                            runat="server"  
                                            TargetControlID="ImageButtonFollowUpReports"  
                                            PopupControlID="PanelFollowUpReports" PopupPosition="Top" 
                                            HoverDelay="500">
                                  </asp:HoverMenuExtender>

                                  <asp:Panel    
                                            ID="PanelFollowUpReports"  Visible="false"
                                            runat="server"  
                                            BorderColor="#000000" CssClass="hidepanel"
                                            BorderWidth="3px"  BackColor="White"> 

                                  <asp:Image ID="ImageAktToNaklWarning" runat="server" ImageUrl="~/images/AktToNakladnayaWarning.png" />

                                  </asp:Panel>

                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="btn btn-minier btn-default "
                                        CommandName="Update" Text="Update" ValidationGroup="NaklEdit"></asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False"  CssClass="btn btn-minier btn-success "
                                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>

                                    <asp:Label ID="LabelMatchMessageEdit" runat="server" ForeColor="Red" Visible="false" 
                                        Text="This item is marked as MATCH to 1S. You cannot change it unless finance authorize. You can change only comments section."></asp:Label>

                                </EditItemTemplate>

                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Inv_ContrNo">
                       <ItemTemplate>
                        <asp:Literal ID="LabelInv_ContrNo" runat="server" Text='<%# Bind("Inv_ContrNo") %>' ></asp:Literal>
                         &nbsp;
                         <asp:LinkButton ID="LinkButtonShowHistory" runat="server" CausesValidation="false"  CssClass="LabelGeneral"
                         CommandName="ShowHistory" Text="Show History" 
                         CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PayReqNo") %>' ></asp:LinkButton>
                       </ItemTemplate>
                       <EditItemTemplate>
                        <asp:TextBox ID="TextBoxInv_ContrNo" runat="server" Text='<%# Bind("Inv_ContrNo") %>' CssClass="TextBoxGeneralRev"  Width="90px" ></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorInv_ContrNo" runat="server"  Display="Dynamic"
                         ErrorMessage="Required" ControlToValidate="TextBoxInv_ContrNo" ValidationGroup="NaklEdit">
                        </asp:RequiredFieldValidator>
                       </EditItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="NakNo">
                       <ItemTemplate>
                        <asp:Literal ID="LabelNakNo" runat="server" Text='<%# Bind("NakNo") %>' ></asp:Literal>
                       </ItemTemplate>
                       <EditItemTemplate>
                        <asp:TextBox ID="TextBoxNakNo" runat="server" Text='<%# Bind("NakNo") %>' CssClass="TextBoxGeneralRev"  Width="90px"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNakNo" runat="server"  Display="Dynamic"
                         ErrorMessage="Required" ControlToValidate="TextBoxNakNo" ValidationGroup="NaklEdit">
                        </asp:RequiredFieldValidator>
                       </EditItemTemplate>

                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Nak_Date">
                       <ItemTemplate>
                        <asp:Literal ID="LabelNak_Date" runat="server" Text='<%# Bind("Nak_Date","{0:dd/MM/yyyy}") %>' ></asp:Literal>
                       </ItemTemplate>
                       <EditItemTemplate>
                        <asp:TextBox ID="TextBoxNak_Date" runat="server" Text='<%# Bind("Nak_Date","{0:dd/MM/yyyy}") %>' CssClass="add_datepicker TextBoxGeneralRev"  Width="90px" ></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNak_Date" runat="server"  Display="Dynamic"
                         ErrorMessage="Required" ControlToValidate="TextBoxNak_Date" ValidationGroup="NaklEdit">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorNakDate" ControlToValidate="TextBoxNak_Date" Display="Dynamic"
                        runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                        ValidationGroup="NaklEdit">
                        </asp:RegularExpressionValidator>
                       </EditItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Nak_Rub_WithVAT">
                       <ItemTemplate>
                        <asp:Literal ID="LabelNak_Rub_WithVAT" runat="server" Text='<%# Bind("Nak_Rub_WithVAT","{0:###,###,###.00}") %>' ></asp:Literal>
                       </ItemTemplate>
                       <EditItemTemplate>
                        <asp:TextBox ID="TextBoxNak_Rub_WithVAT" runat="server" Text='<%# Bind("Nak_Rub_WithVAT") %>' CssClass="TextBoxGeneralRev"  Width="90px"  ></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNak_Rub_WithVAT" runat="server"  Display="Dynamic"
                         ErrorMessage="Required" ControlToValidate="TextBoxNak_Rub_WithVAT" ValidationGroup="NaklEdit">
                        </asp:RequiredFieldValidator>
<%--                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorNak_Rub_WithVAT" ControlToValidate="TextBoxNak_Rub_WithVAT" Display="Dynamic"
                        runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" ValidationGroup="NaklEdit">
                        </asp:RegularExpressionValidator>--%>
                       </EditItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="SF">
                       <ItemTemplate>
                        <asp:CheckBox ID="CheckBocSF" runat="server" Checked='<%# Bind("SF") %>' 
                        Enabled="false" ></asp:CheckBox>
                       </ItemTemplate>
                       <EditItemTemplate>
                        <asp:CheckBox ID="CheckBocSFedit" runat="server" Checked='<%# Bind("SF") %>' ></asp:CheckBox>
                        <br />
                        <asp:Label ID="LabelSFwarning" runat="server" ForeColor="Red" Text="required" Visible="false" ></asp:Label>
                       </EditItemTemplate>
                      </asp:TemplateField>

                        <asp:TemplateField HeaderText="PDF">
                            <ItemTemplate>

                                <asp:Hyperlink ID="HypPDF" runat="server" ForeColor="Red" Target="_blank" NavigateUrl='<%# Eval("PDF", "~/ShowFile.aspx?Link={0}")%>' >see PDF</asp:Hyperlink>

                            </ItemTemplate>
                            <EditItemTemplate>

                                <asp:FileUpload ID="FileToUpload" runat="server" />
                                <asp:Label ID="LabelInfo" runat="server" Visible="false"></asp:Label>
                                <asp:Button ID="ButtonUploadPDF" runat="server" CausesValidation="False" 
                                    CssClass="btn btn-mini btn-success" Text="Upload" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="EditPDF" />

                                <asp:Hiddenfield ID="HiddenFieldPDF" runat="server" Value='<%# Bind("PDF")%>'  ></asp:Hiddenfield>

                            </EditItemTemplate>
                        </asp:TemplateField>

                      <asp:TemplateField HeaderText="Comment">
                       <ItemTemplate>
                        <asp:Literal ID="LabelNak_Comment" runat="server" Text='<%# Bind("Comment")%>' ></asp:Literal>
                       </ItemTemplate>
                       <EditItemTemplate>
                        <asp:TextBox ID="TextBoxNak_Comment" runat="server" Text='<%# Bind("Comment")%>' Width="90px"
                            TextMode="MultiLine" Height="100px"></asp:TextBox>
                        <br />
                       </EditItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Entered By">
                       <ItemTemplate>
                        <asp:Literal ID="LiteralCreatedBy" runat="server" Text='<%# Bind("CreatedBy")%>' ></asp:Literal>
                       </ItemTemplate>
                       <EditItemTemplate>
                       </EditItemTemplate>
                      </asp:TemplateField>

                    </Columns>

                  </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSourceNakladnayaEdit" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                        SelectCommand=" 
							                declare @TempTable_Indexes as table
							                (Id_Nakl_Match int)

							                DELETE FROM @TempTable_Indexes
							                INSERT INTO @TempTable_Indexes 
							                SELECT Id_Nakl_Match FROM (
						                        SELECT 
						                        CONVERT(Int, SUBSTRING(Index_PTS_Or_1S , LEN(N'Nkl_') + 1 , LEN(RTRIM(Index_PTS_Or_1S)) - LEN(N'Nkl_'))) AS Id_Nakl_Match
						                        FROM dbo.Table_Delivery_MatchingIndexes  
						                        WHERE PTS_Or_1S = N'PTS' AND ISNUMERIC(SUBSTRING(Index_PTS_Or_1S , LEN(N'Nkl_') + 1 , LEN(RTRIM(Index_PTS_Or_1S)) - LEN(N'Nkl_'))) = 1 AND Index_PTS_Or_1S LIKE N'%Nkl_%'

								                UNION ALL

						                        SELECT 
						                        CONVERT(Int, SUBSTRING(Index_PTS_Or_1S , LEN(N'Nkl_Akt_') + 1 , LEN(RTRIM(Index_PTS_Or_1S)) - LEN(N'Nkl_Akt_'))) AS Id_Nakl_Match
						                        FROM dbo.Table_Delivery_MatchingIndexes  
						                        WHERE PTS_Or_1S = N'PTS' AND ISNUMERIC(SUBSTRING(Index_PTS_Or_1S , LEN(N'Nkl_Akt_') + 1 , LEN(RTRIM(Index_PTS_Or_1S)) - LEN(N'Nkl_Akt_'))) = 1 AND Index_PTS_Or_1S LIKE N'%Nkl_Akt_%'
							                ) AS Source
							                GROUP BY Id_Nakl_Match

							                SELECT						 [ID_Nak]
														                ,[Inv_ContrNo]
														                ,[NakNo]
														                ,[Nak_Date]
														                ,[Nak_Rub_WithVAT]
														                ,[SF]
														                ,[CreatedBy]
														                ,[PersonCreated]
														                ,[PayReqNo]
                                                                        ,PDF
														                ,RTRIM(Comment) AS Comment
														                , CASE WHEN Id_Nakl_Match IS NULL THEN 0 ELSE 1 END AS MatchIn1S
													                FROM [Table_PO_Nakladnaya] 

							                LEFT OUTER JOIN (

											                SELECT Id_Nakl_Match FROM @TempTable_Indexes

									                   ) AS Nakl_Match ON Nakl_Match.Id_Nakl_Match = Table_PO_Nakladnaya.ID_Nak 
							                WHERE [Table_PO_Nakladnaya].PO_No = @PO_No "

                        UpdateCommand= " UPDATE [Table_PO_Nakladnaya]
                                           SET [Inv_ContrNo] = @Inv_ContrNo
                                              ,[NakNo] = @NakNo
                                              ,[Nak_Date] = @Nak_Date
                                              ,[Nak_Rub_WithVAT] = @Nak_Rub_WithVAT
                                              ,[SF] = @SF
                                              ,[PDF] = @PDF
                                              ,[Comment] = @Comment
                                         WHERE ID_Nak = @ID_Nak " 
                         
                        DeleteCommand= "DELETE FROM [Table_PO_Nakladnaya] WHERE ID_Nak = @ID_Nak " >
                              <SelectParameters>
                                  <asp:ControlParameter ControlID="labelPOnoNakl" Name="PO_No" 
                                      PropertyName="Text" />
                              </SelectParameters>
                    </asp:SqlDataSource>


                     <asp:TextBox ID="TextBoxShowHistoryNakladnaya" runat="server" Height="204px" 
                      Width="670px" TextMode="MultiLine" Visible="false">
                     </asp:TextBox>

                  </div>

                  </ContentTemplate>
              </asp:UpdatePanel>

            </div>
        </div>
    </div>
</div>

    <%-- /MODAL POPUP NAKLADNAYA E D I T --%>  
    <%-- MODAL POPUP AKT_TO_NAKLADNAYA E D I T --%>

<div id="ModalAktToNakladnayaEdit" class="modal">
    <div class="modal-dialog modal-dialog-center">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Akt To Nakladnaya Edit</h4>
            </div>
            <div class="modal-body" style="width:1000px; margin:10px; ">

                  <asp:Button ID="ButtonSwitchToNakladnayaEdit" runat="server" Text="Switch To Edit Nakladnaya" CssClass="btn btn-mini btn-default" />
                  <asp:Button ID="ButtonSwitchToInsertNakladnaya_" runat="server" Text="Switch To Insert Nakladnaya" CssClass="btn btn-mini btn-default" />
                 <br /><br />

              <asp:UpdatePanel ID="updatePanelAktToNakladnayaEdit" runat="server">

                  <ContentTemplate>

                 <script type="text/javascript">
                     Sys.Application.add_load(PTS_BindEvents);
                 </script>

                  <div style=" height: 600px; overflow: auto;">

                      <asp:GridView ID="GridViewAktToNakladnayaEdit" runat="server" AllowPaging="false" 
                        AutoGenerateColumns="False" DataKeyNames="ID_Akt_To_Nak" CssClass="table" GridLines="None"
                        DataSourceID="SqlDataSourceAktToNakladnayaEdit" EnableModelValidation="True" >
                        <Columns>
                          <asp:TemplateField HeaderStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="btn btn-minier btn-success"
                                            CommandName="Edit" Text="Edit"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                                        OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                        CommandName="Delete" Text="Delete" CssClass="btn btn-minier btn-danger" ></asp:LinkButton>

                                        <asp:Label ID="LabelMatchMessageItem" runat="server" ForeColor="Red" Visible="false" 
                                            Text="This item is marked as MATCH to 1S. You cannot change it unless finance authorize. You can change only comments section."></asp:Label>

                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="btn btn-minier btn-default"
                                            CommandName="Update" Text="Update" ValidationGroup="AktToNakladnayaEdit"></asp:LinkButton>

                                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False"  CssClass="btn btn-minier btn-default"
                                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>

                                        <asp:Label ID="LabelMatchMessageEdit" runat="server" ForeColor="Red" Visible="false" 
                                            Text="This item is marked as MATCH to 1S. You cannot change it unless finance authorize. You can change only comments section."></asp:Label>

                                    </EditItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Inv_ContrNo">
                           <ItemTemplate>
                            <asp:Literal ID="LabelInv_ContrNo_" runat="server" Text='<%# Bind("Inv_ContrNo") %>' ></asp:Literal>
                             &nbsp;
                             <asp:LinkButton ID="LinkButtonShowHistory" runat="server" CausesValidation="false"  CssClass="LabelGeneral"
                             CommandName="ShowHistory" Text="Show History" 
                             CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PayReqNo") %>' ></asp:LinkButton>
                           </ItemTemplate>
                           <EditItemTemplate>
                            <asp:Literal ID="LabelInv_ContrNo_" runat="server" Text='<%# Bind("Inv_ContrNo") %>' ></asp:Literal>
                           </EditItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="NakNo">
                           <ItemTemplate>
                            <asp:Literal ID="LabelNakNo_" runat="server" Text='<%# Bind("NakNo") %>' ></asp:Literal>
                           </ItemTemplate>
                           <EditItemTemplate>
                            <asp:Literal ID="LabelNakNo_" runat="server" Text='<%# Bind("NakNo") %>' ></asp:Literal>
                           </EditItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Nak_Date">
                           <ItemTemplate>
                            <asp:Literal ID="LabelNak_Date_" runat="server" Text='<%# Bind("Nak_Date","{0:dd/MM/yyyy}") %>' ></asp:Literal>
                           </ItemTemplate>
                           <EditItemTemplate>
                            <asp:Literal ID="LabelNak_Date_" runat="server" Text='<%# Bind("Nak_Date","{0:dd/MM/yyyy}") %>' ></asp:Literal>
                           </EditItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="AktNo">
                           <ItemTemplate>
                            <asp:Literal ID="LabelInv_AktNo_" runat="server" Text='<%# Bind("AktNo") %>' ></asp:Literal>
                           </ItemTemplate>
                           <EditItemTemplate>
                            <asp:TextBox ID="TextBoxInv_AktNo_" runat="server" Text='<%# Bind("AktNo") %>' CssClass="TextBoxGeneralRev"  Width="90px" ></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAktNo_" runat="server"  Display="Dynamic"
                             ErrorMessage="Required" ControlToValidate="TextBoxInv_AktNo_" ValidationGroup="AktToNakladnayaEdit">
                            </asp:RequiredFieldValidator>
                           </EditItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Akt_Date">
                           <ItemTemplate>
                            <asp:Literal ID="LabelAkt_Date_" runat="server" Text='<%# Bind("Akt_Date","{0:dd/MM/yyyy}") %>' ></asp:Literal>
                           </ItemTemplate>
                           <EditItemTemplate>
                            <asp:TextBox ID="TextBoxAkt_Date_" runat="server" Text='<%# Bind("Akt_Date","{0:dd/MM/yyyy}") %>' CssClass="TextBoxGeneralRev add_datepicker" Width="90px" ></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAkt_Date_" runat="server"  Display="Dynamic"
                             ErrorMessage="Required" ControlToValidate="TextBoxAkt_Date_" ValidationGroup="AktToNakladnayaEdit">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorAktDate_" ControlToValidate="TextBoxAkt_Date_" Display="Dynamic"
                            runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                            ValidationGroup="AktToNakladnayaEdit">
                            </asp:RegularExpressionValidator>
                           </EditItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Akt Rub WithVAT">
                           <ItemTemplate>
                            <asp:Literal ID="LabelAkt_Rub_WithVAT_" runat="server" Text='<%# Bind("Akt_Rub_WithVAT","{0:###,###,###.00}") %>' ></asp:Literal>
                           </ItemTemplate>
                           <EditItemTemplate>
                            <asp:TextBox ID="TextBoxAkt_Rub_WithVAT_" runat="server" Text='<%# Bind("Akt_Rub_WithVAT") %>' CssClass="TextBoxGeneralRev" Width="90px" ></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAkt_Rub_WithVAT_" runat="server"  Display="Dynamic"
                             ErrorMessage="Required" ControlToValidate="TextBoxAkt_Rub_WithVAT_" ValidationGroup="AktToNakladnayaEdit">
                            </asp:RequiredFieldValidator>
<%--                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorAkt_Rub_WithVAT_" ControlToValidate="TextBoxAkt_Rub_WithVAT_" Display="Dynamic"
                            runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" ValidationGroup="AktToNakladnayaEdit">
                            </asp:RegularExpressionValidator>--%>
                           </EditItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Comment">
                           <ItemTemplate>
                            <asp:Literal ID="LabelAkt_Comment" runat="server" Text='<%# Bind("Comment")%>' ></asp:Literal>
                           </ItemTemplate>
                           <EditItemTemplate>
                            <asp:TextBox ID="TextBoxAkt_Comment" runat="server" Text='<%# Bind("Comment")%>' 
                                TextMode="MultiLine" Height="100px" Width="100px" ></asp:TextBox>
                            <br />
                           </EditItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Entered By">
                           <ItemTemplate>
                            <asp:Literal ID="LiteralCreatedBy" runat="server" Text='<%# Bind("CreatedBy")%>' ></asp:Literal>
                           </ItemTemplate>
                           <EditItemTemplate>
                           </EditItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" />
                          </asp:TemplateField>

                        </Columns>
                      </asp:GridView>

                        <asp:SqlDataSource ID="SqlDataSourceAktToNakladnayaEdit" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                            SelectCommand=" 
                            SELECT 
                                              dbo.Table_PO_Akt_To_Nak.ID_Akt_To_Nak
                                            , dbo.Table_PO_Nakladnaya.Inv_ContrNo
                                            , dbo.Table_PO_Nakladnaya.NakNo
                                            , dbo.Table_PO_Nakladnaya.Nak_Date
                                            , dbo.Table_PO_Akt_To_Nak.AktNo
                                            , dbo.Table_PO_Akt_To_Nak.Akt_Date
                                            , dbo.Table_PO_Akt_To_Nak.Akt_Rub_WithVAT
                                            , dbo.Table_PO_Akt_To_Nak.CreatedBy
                                            , dbo.Table_PO_Akt_To_Nak.PersonCreated
                                            , dbo.Table_PO_Akt_To_Nak.PayReqNo
                                            , RTRIM(dbo.Table_PO_Akt_To_Nak.Comment) AS Comment
				                            , CASE WHEN Id_Nakl_Match IS NULL THEN 0 ELSE 1 END AS MatchIn1S
                                            FROM  dbo.Table_PO_Nakladnaya 
                                            INNER JOIN dbo.Table_PO_Akt_To_Nak ON dbo.Table_PO_Nakladnaya.ID_Nak = dbo.Table_PO_Akt_To_Nak.ID_Nak

                            LEFT OUTER JOIN (

						                            SELECT 
						                            CONVERT(Int, SUBSTRING(Index_PTS_Or_1S , LEN(N'Nkl_Akt_') + 1 , LEN(RTRIM(Index_PTS_Or_1S)) - LEN(N'Nkl_Akt_'))) AS Id_Nakl_Match
						                            FROM dbo.Table_Delivery_MatchingIndexes  
						                            WHERE PTS_Or_1S = N'PTS' AND ISNUMERIC(SUBSTRING(Index_PTS_Or_1S , LEN(N'Nkl_Akt_') + 1 , LEN(RTRIM(Index_PTS_Or_1S)) - LEN(N'Nkl_Akt_'))) = 1 AND Index_PTS_Or_1S LIKE N'%Nkl_Akt_%'
		                               ) AS Nakl_Match ON Nakl_Match.Id_Nakl_Match = Table_PO_Akt_To_Nak.ID_Nak 
                                            WHERE (dbo.Table_PO_Nakladnaya.PO_No = @PO_No) "

                            UpdateCommand= " UPDATE [Table_PO_Akt_To_Nak]
                                               SET [AktNo] = @AktNo
                                                  ,[Akt_Date] = @Akt_Date
                                                  ,[Akt_Rub_WithVAT] = @Akt_Rub_WithVAT
                                                  ,[Comment] = @Comment
                                             WHERE ID_Akt_To_Nak = @ID_Akt_To_Nak " 
                         
                            DeleteCommand= "DELETE FROM [Table_PO_Akt_To_Nak] WHERE ID_Akt_To_Nak = @ID_Akt_To_Nak " >
                                  <SelectParameters>
                                      <asp:ControlParameter ControlID="labelPOnoNakl" Name="PO_No" 
                                          PropertyName="Text" />
                                  </SelectParameters>
                        </asp:SqlDataSource>

                         <asp:TextBox ID="TextBoxShowHistoryAktToNakladnaya" runat="server" Height="204px" 
                          Width="670px" TextMode="MultiLine" Visible="false">
                         </asp:TextBox>

                  </div>

                  </ContentTemplate>
              </asp:UpdatePanel>

            </div>
        </div>
    </div>
</div>

    <%-- /MODAL POPUP AKT_TO_NAKLADNAYA E D I T --%>    
    <%-- MODAL POPUP AKT --%>

<div id="ModalAkt" class="modal">
    <div class="modal-dialog modal-dialog-center">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Akt</h4>
            </div>
            <div class="modal-body">

             <asp:Label ID="labelPOnoAkt" runat="server" Text="Test" CssClass="label label-danger arrowed-in-right arrowed-in-left arrowed-in " ></asp:Label>

             <table class="table-condensed">
              <tr>
               <td colspan="2" style="text-align: right">
                  <asp:Button ID="ButtonEditAkt" runat="server" Text="EDIT" CssClass="btn btn-mini btn-info"  />
               </td>
              </tr>
                     <tr>
                         <td></td>
                         <td>
                             <asp:FileUpload ID="FileUploadAkt" runat="server" />
                             <asp:Label ID="LabelInfoAkt" runat="server" ></asp:Label>
                             <asp:LinkButton ID="LinkButtonAktPDF" runat="server" CssClass="btn btn-lg btn-success" OnClick="LinkButtonAktPDF_Click" style="margin-top:5px!important;">
                                            Upload Document
                                            <i class="ace-icon fa fa-upload "></i>
                             </asp:LinkButton>

                            <asp:HiddenField ID="HiddenFieldAktLink" runat="server"></asp:HiddenField>

                         </td>
                         <td></td>
                     </tr>
              <tr >
               <td >
               Shot Faktura:
               </td>
               <td>
                <asp:CheckBox ID="CheckBoxAktSF" runat="server"  />
               </td>
               <td>
                <asp:Label id="LabelWarningAktSf" runat="server" Text="Required" 
                 Visible="false" ForeColor="Red"></asp:Label>
               </td>
              </tr>
              <tr >
               <td >
               Akt No:
               </td>
               <td>
                <asp:TextBox ID="TextBoxAktNo" runat="server"></asp:TextBox>
               </td>
               <td>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidatorAktNo" runat="server" 
                   ErrorMessage="Required" ControlToValidate="TextBoxAktNo" Display="Dynamic"
                   ValidationGroup="Akt">
                 </asp:RequiredFieldValidator>
               </td>
              </tr>
              <tr >
               <td >
               Akt Date
               </td>
               <td>
                <asp:TextBox ID="TextBoxAktDate" runat="server" CssClass="add_datepicker"></asp:TextBox>
               </td>
               <td>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidatorAktDate" runat="server" 
                   ErrorMessage="Required" ControlToValidate="TextBoxAktDate" Display="Dynamic"
                   ValidationGroup="Akt">
                 </asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidatorAktDate" 
                 ControlToValidate="TextBoxAktDate" Display="Dynamic"
                  runat="server" ErrorMessage="dd/mm/yyyy"  
                  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                  ValidationGroup="Akt"></asp:RegularExpressionValidator>
               </td>
              </tr>
              <tr >
               <td >
                Akt Value in Ruble With VAT
               </td>
               <td>
                <asp:TextBox ID="TextBoxAktValue" runat="server"></asp:TextBox>
               </td>
               <td>
<%--                <asp:RegularExpressionValidator ID="RegularExpressionValidatorAktValue" 
                  ControlToValidate="TextBoxAktValue" Display="Dynamic"
                  runat="server" ErrorMessage="Wrong format"  
                  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                   ValidationGroup="Akt"></asp:RegularExpressionValidator>--%>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidatorAktValue" runat="server" 
                   ErrorMessage="Required" ControlToValidate="TextBoxAktValue" Display="Dynamic"
                   ValidationGroup="Akt">
                 </asp:RequiredFieldValidator>
               </td>
              </tr>
                 <tr >
                     <td >Comment</td>
                     <td>
                         <asp:TextBox ID="TextBoxAktComment" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
                     </td>
                     <td>&nbsp;</td>
                 </tr>
              <tr>
              <td ColSpan="3" style="text-align: right">
                <asp:Label id="LabelAktErrorWarning" runat="server" 
                 Text="There is NAKLADNAYA. To proceed, you must delete." 
                 Visible="false" ForeColor="Red">
                </asp:Label>
                    <asp:Button ID="ButtonInsertAkt" runat="server" Text="INSERT" CssClass="btn btn-mini btn-success" 
                    ValidationGroup="Akt" />
              </td>
              </tr>
             </table>

            </div>
        </div>
    </div>
</div>

    <%-- /MODAL POPUP AKT --%>    
    <%-- MODAL POPUP AKT E D I T --%>

<div id="ModalAktEdit" class="modal">
    <div class="modal-dialog modal-dialog-center">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Akt</h4>
            </div>
            <div class="modal-body" style="width:1000px; margin:10px;">

              <asp:Button ID="ButtonSwitchToActInsert" runat="server" Text="Switch To Insert Akt" CssClass="btn btn-mini btn-default" />

              <br /><br />

              <asp:UpdatePanel ID="updatePanelAktEdit" runat="server">

                  <Triggers>
                    <asp:PostBackTrigger ControlID="GridViewAktEdit" />
                  </Triggers>

                  <ContentTemplate>

                 <script type="text/javascript">
                     Sys.Application.add_load(PTS_BindEvents);
                 </script>

              <div style=" height: 600px; overflow: auto;">

                  <asp:GridView ID="GridViewAktEdit" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" DataKeyNames="ID_Akt" CssClass="table" GridLines="None"
                    DataSourceID="SqlDataSourceAktEdit" EnableModelValidation="True" >
                    <Columns>
                      <asp:TemplateField HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="btn btn-minier btn-success "
                                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButtonDelete" Runat="server" CssClass="btn btn-minier btn-danger"
                                    OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                    CommandName="Delete" Text="Delete" ></asp:LinkButton>

                                    <asp:Label ID="LabelMatchMessageItem" runat="server" ForeColor="Red" Visible="false" 
                                        Text="This item is marked as MATCH to 1S. You cannot change it unless finance authorize. You can change only comments section."></asp:Label>

                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="btn btn-minier btn-default"
                                        CommandName="Update" Text="Update" ValidationGroup="AktEdit"></asp:LinkButton>

                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False"  CssClass="btn btn-minier btn-default"
                                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>

                                    <asp:Label ID="LabelMatchMessageEdit" runat="server" ForeColor="Red" Visible="false" 
                                        Text="This item is marked as MATCH to 1S. You cannot change it unless finance authorize. You can change only comments section."></asp:Label>

                                </EditItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="AktNo">
                       <ItemTemplate>
                        <asp:Literal ID="LabelInv_AktNo" runat="server" Text='<%# Bind("AktNo") %>' ></asp:Literal>
                         &nbsp;
                         <asp:LinkButton ID="LinkButtonShowHistory" runat="server" CausesValidation="false"  CssClass="LabelGeneral"
                         CommandName="ShowHistory" Text="Show History" 
                         CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PayReqNo") %>' ></asp:LinkButton>
                       </ItemTemplate>
                       <EditItemTemplate>
                        <asp:TextBox ID="TextBoxInv_AktNo" runat="server" Text='<%# Bind("AktNo") %>' CssClass="TextBoxGeneralRev" Width="90px" ></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAktNo" runat="server"  Display="Dynamic"
                         ErrorMessage="Required" ControlToValidate="TextBoxInv_AktNo" ValidationGroup="AktEdit">
                        </asp:RequiredFieldValidator>
                       </EditItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Akt_Date">
                       <ItemTemplate>
                        <asp:Literal ID="LabelAkt_Date" runat="server" Text='<%# Bind("Akt_Date","{0:dd/MM/yyyy}") %>' ></asp:Literal>
                       </ItemTemplate>
                       <EditItemTemplate>
                        <asp:TextBox ID="TextBoxAkt_Date" runat="server" Text='<%# Bind("Akt_Date","{0:dd/MM/yyyy}") %>' CssClass="add_datepicker TextBoxGeneralRev" Width="90px"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAkt_Date" runat="server"  Display="Dynamic"
                         ErrorMessage="Required" ControlToValidate="TextBoxAkt_Date" ValidationGroup="AktEdit">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorAktDate" ControlToValidate="TextBoxAkt_Date" Display="Dynamic"
                        runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                        ValidationGroup="AktEdit">
                        </asp:RegularExpressionValidator>
                       </EditItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Akt Rub WithVAT">
                       <ItemTemplate>
                        <asp:Literal ID="LabelAkt_Rub_WithVAT" runat="server" Text='<%# Bind("Akt_Rub_WithVAT","{0:###,###,###.00}") %>' ></asp:Literal>
                       </ItemTemplate>
                       <EditItemTemplate>
                        <asp:TextBox ID="TextBoxAkt_Rub_WithVAT" runat="server" Text='<%# Bind("Akt_Rub_WithVAT") %>' CssClass="TextBoxGeneralRev" Width="90px"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAkt_Rub_WithVAT" runat="server"  Display="Dynamic"
                         ErrorMessage="Required" ControlToValidate="TextBoxAkt_Rub_WithVAT" ValidationGroup="AktEdit">
                        </asp:RequiredFieldValidator>
<%--                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorAkt_Rub_WithVAT" ControlToValidate="TextBoxAkt_Rub_WithVAT" Display="Dynamic"
                        runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" ValidationGroup="AktEdit">
                        </asp:RegularExpressionValidator>--%>
                       </EditItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="SF">
                       <ItemTemplate>
                        <asp:CheckBox ID="CheckBocSF" runat="server" Checked='<%# Bind("SF") %>' 
                         Enabled="false" ></asp:CheckBox>
                       </ItemTemplate>
                       <EditItemTemplate>
                        <asp:CheckBox ID="CheckBocSFedit" runat="server" Checked='<%# Bind("SF") %>' Width="90px"></asp:CheckBox>
                        <br />
                        <asp:Label ID="LabelSFwarning" runat="server" ForeColor="Red" Text="required" Visible="false" ></asp:Label>
                       </EditItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                      </asp:TemplateField>

                        <asp:TemplateField HeaderText="PDF">
                            <ItemTemplate>

                                <asp:Hyperlink ID="HypPDF" runat="server" ForeColor="Red" Target="_blank" NavigateUrl='<%# Eval("PDF", "~/ShowFile.aspx?Link={0}")%>' >see PDF</asp:Hyperlink>

                            </ItemTemplate>
                            <EditItemTemplate>

                                <asp:FileUpload ID="FileToUpload" runat="server" />
                                <asp:Label ID="LabelInfo" runat="server" Visible="false"></asp:Label>
                                <asp:Button ID="ButtonUploadPDF" runat="server" CausesValidation="False" 
                                    CssClass="btn btn-mini btn-success" Text="Upload" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="EditPDF" />

                                <asp:Hiddenfield ID="HiddenFieldPDF" runat="server" Value='<%# Bind("PDF")%>'  ></asp:Hiddenfield>

                            </EditItemTemplate>
                        </asp:TemplateField>


                      <asp:TemplateField HeaderText="Comment">
                       <ItemTemplate>
                        <asp:Literal ID="LabelAkt_Comment" runat="server" Text='<%# Bind("Comment")%>' ></asp:Literal>
                       </ItemTemplate>
                       <EditItemTemplate>
                        <asp:TextBox ID="TextBoxAkt_Comment" runat="server" Text='<%# Bind("Comment")%>' 
                            TextMode="MultiLine" Height="100px" Width="100px" ></asp:TextBox>
                        <br />
                       </EditItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Entered By">
                       <ItemTemplate>
                        <asp:Literal ID="LiteralCreatedBy" runat="server" Text='<%# Bind("CreatedBy")%>' ></asp:Literal>
                       </ItemTemplate>
                       <EditItemTemplate>
                       </EditItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                      </asp:TemplateField>

                    </Columns>
                  </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSourceAktEdit" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                        SelectCommand=" 
                        SELECT						 [ID_Akt]
                                                    ,[AktNo]
                                                    ,[Akt_Date]
                                                    ,[Akt_Rub_WithVAT]
                                                    ,[SF]
                                                    ,[CreatedBy]
                                                    ,[PersonCreated]
                                                    ,[PayReqNo]
                                                    ,[PDF]
                                                    ,RTRIM(Comment) AS Comment
							                        , CASE WHEN Id_Akt_Match IS NULL THEN 0 ELSE 1 END AS MatchIn1S
                                                FROM [Table_PO_Akt] 
                        LEFT OUTER JOIN (

						                        SELECT 
						                        CONVERT(Int, SUBSTRING(Index_PTS_Or_1S , LEN(N'Akt_') + 1 , LEN(RTRIM(Index_PTS_Or_1S)) - LEN(N'Akt_'))) AS Id_Akt_Match
						                        FROM dbo.Table_Delivery_MatchingIndexes  
						                        WHERE PTS_Or_1S = N'PTS' AND ISNUMERIC(SUBSTRING(Index_PTS_Or_1S , LEN(N'Akt_') + 1 , LEN(RTRIM(Index_PTS_Or_1S)) - LEN(N'Akt_'))) = 1 AND Index_PTS_Or_1S LIKE N'%Akt_%'
		                           ) AS Akt_Match ON Akt_Match.Id_Akt_Match = [Table_PO_Akt].ID_Akt
                                                WHERE PO_No = @PO_No "

                        UpdateCommand= " UPDATE [Table_PO_Akt]
                                           SET [AktNo] = @AktNo
                                              ,[Akt_Date] = @Akt_Date
                                              ,[Akt_Rub_WithVAT] = @Akt_Rub_WithVAT
                                              ,[SF] = @SF
                                              ,[PDF] = @PDF
                                              ,Comment = @Comment
                                         WHERE ID_Akt = @ID_Akt " 
                         
                        DeleteCommand= "DELETE FROM [Table_PO_Akt] WHERE ID_Akt = @ID_Akt " >
                              <SelectParameters>
                                  <asp:ControlParameter ControlID="labelPOnoAkt" Name="PO_No" 
                                      PropertyName="Text" />
                              </SelectParameters>
                    </asp:SqlDataSource>

                 <asp:TextBox ID="TextBoxShowHistoryAkt" runat="server" Height="204px" 
                  Width="670px" TextMode="MultiLine" Visible="false">
                 </asp:TextBox>

              </div>

                  </ContentTemplate>
              </asp:UpdatePanel>


            </div>

        </div>
    </div>
</div>

    <%-- /MODAL POPUP AKT E D I T --%>

<div style="margin-bottom:10px;">

  <asp:DropDownList ID="DropDownListPrj" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPrj_SelectedIndexChanged"
    DataSourceID="SqlDataSourcePrj" 
    DataTextField="ProjectName" DataValueField="ProjectID" >
  </asp:DropDownList>

    <asp:DropDownList ID="DropDownListSupplier" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSupplier_SelectedIndexChanged"
      DataSourceID="SqlDataSourceSupplier" 
      DataTextField="SupplierName" DataValueField="SupplierID" >
    </asp:DropDownList>

    <asp:hyperlink ID="HyperlinkDocumentSummary" runat="server" CssClass="btn btn-xs btn-default"
    Target="_blank"  NavigateUrl="~/DeliveryPackingList.aspx" >See weekly packing list</asp:hyperlink>

    <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="hidepanel"></asp:TextBox>

    <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID,
                       (RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5),dbo.Table1_Project.ProjectID))) as ProjectName, dbo.aspnet_Users.UserName 
                       FROM         dbo.Table1_Project 
                       INNER JOIN  dbo.Table_Prj_User_Junction 
                       ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID 
                       INNER JOIN  dbo.aspnet_Users 
                       ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId 
                       WHERE  (Table1_Project.CurrentStatus = 1) 
                       AND (dbo.aspnet_Users.UserName = @UserName) 
                       ORDER BY dbo.Table1_Project.ProjectName">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceSupplier" runat="server" 
      ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
      SelectCommand="SELECT RTRIM(Table6_Supplier.SupplierID) AS SupplierID, 
                     RTRIM(Table6_Supplier.SupplierName) AS SupplierName, 
                     Table1_Project.ProjectID 
                     FROM Table6_Supplier 
                     INNER JOIN Table2_PONo 
                     ON Table6_Supplier.SupplierID = Table2_PONo.SupplierID 
                     INNER JOIN Table1_Project 
                     ON Table2_PONo.Project_ID = Table1_Project.ProjectID 
                     GROUP BY Table6_Supplier.SupplierID, 
                     RTRIM(Table6_Supplier.SupplierName), 
                     Table1_Project.ProjectID 
                     HAVING (Table1_Project.ProjectID = @ProjectID) ORDER BY SupplierName">
     <SelectParameters>
      <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" Name="ProjectID" 
       PropertyName="SelectedValue" />
     </SelectParameters>
    </asp:SqlDataSource>

</div>

  <asp:GridView ID="GridViewNakladnaya" runat="server" AllowPaging="True" 
    AutoGenerateColumns="False" DataKeyNames="PO_No" CssClass="Grid"
    DataSourceID="SqlDataSourceGridNakladnaya" EnableModelValidation="True" 
    PagerSettings-Position="TopAndBottom" >
    <Columns>
      <asp:BoundField DataField="PO_No" HeaderText="PO_No" ReadOnly="True" 
        SortExpression="PO_No" />
      <asp:BoundField DataField="Description" HeaderText="Description" 
        ReadOnly="True" SortExpression="Description" 
        ControlStyle-Width="300px" HeaderStyle-Width="300px" 
        ItemStyle-CssClass="FontSizeMiddle">

<ControlStyle Width="300px"></ControlStyle>

<HeaderStyle Width="300px"></HeaderStyle>
      </asp:BoundField>

      <asp:TemplateField>
       <ItemTemplate>
        <span class="span_title">Po Total In Ruble With VAT</span>
        <br />
        <span class="span_detail"><asp:Literal ID="LiteralPOtotalRubleWithVAT" runat="server" 
        Text='<%# Bind("PoTotalRubleWithVAT","{0:###,###,###.00}") %>'></asp:Literal></span>
        <br /><br />
        <span class="span_title">Total Paid In Ruble With VAT</span>
        <br />
        <span class="span_detail"><asp:Literal ID="LiteralPaidRubleWithVAT" runat="server" 
        Text='<%# Bind("RublePaidWithVAT","{0:###,###,###.00}") %>'></asp:Literal></span>
       </ItemTemplate>
      </asp:TemplateField>

      <asp:TemplateField>
       <ItemTemplate>
	<table class="TableNakladnayaSep">

        <asp:Literal ID="LiteralBalance" runat="server" ></asp:Literal>
        <asp:TextBox ID="TextBoxCloseDoc" runat="server" CssClass="TextBoxGeneralRev"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorOriginalValue" runat="server" ErrorMessage="Required" Display="Dynamic" 
                                                     ControlToValidate="TextBoxCloseDoc" ></asp:RequiredFieldValidator>

<%--                                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCashOriginal" runat="server" 
                                                    ControlToValidate="TextBoxCloseDoc"  Display="Dynamic"
                                                    ErrorMessage="not valid number" 
                                                    ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                                                    </asp:RegularExpressionValidator>                            --%>


   <tr>
    <td>
        <asp:LinkButton ID="LinkButtonClose" runat="server" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
        OnClientClick="return confirm('Are you sure you want to close this document?');" CommandName="closedocument" CssClass="btn btn-mini btn-danger">Close</asp:LinkButton>
    </td>
    <td class="span_title">
     Total Collected In Ruble With VAT
    </td>
   </tr>
	 <tr style="height: 20px">
	  <td>
        <asp:LinkButton ID="LinkButtonNakladnaya" runat="server" 
        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PO_No") %>' 
        CommandName="Nakl" ForeColor="#000099">Nakl</asp:LinkButton>
	  </td>
	  <td style="text-align:right;">
        <asp:LinkButton ID="LinkButtonNakladnayaTotalValue" runat="server" 
        Text='<%# Bind("TotalCollectedNaklOnlyInRubleWithVAT","{0:###,###,###.00}") %>'
        enabled="true" ForeColor="#000099"
        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PO_No") %>' 
        CommandName="NaklTotalValue"></asp:LinkButton>
	  </td>
	 </tr>
	 <tr style="height: 20px">
	  <td>
        <asp:LinkButton ID="LinkButtonAkt" runat="server" 
        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PO_No") %>' 
        CommandName="Akt" ForeColor="#000099">Akt</asp:LinkButton>
	  </td>
	  <td style="text-align:right;">
        <asp:LinkButton ID="LinkButtonAktTotalValue" runat="server" 
        Text='<%# Bind("TotalCollectedActInRubleWithVAT","{0:###,###,###.00}") %>'
        enabled="true" ForeColor="#000099"
        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PO_No") %>' 
        CommandName="AktTotalValue"></asp:LinkButton>
	  </td>
	 </tr>
	 <tr style="height: 20px">
	  <td>
        <asp:LinkButton ID="LinkButton_Nakl_Akt" runat="server" 
        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PO_No") %>' 
        CommandName="Nakl_Akt" ForeColor="#000099">Nakl+Akt</asp:LinkButton>
	  </td>
	  <td style="text-align:right;">
        <asp:LinkButton ID="LinkButton_Nakl_AktTotalValue" runat="server" 
        Text='<%# Bind("TotalCollectedNakl_AktInRubleWithVAT","{0:###,###,###.00}") %>'
        enabled="true"
        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PO_No") %>' 
        CommandName="Nakl_AktTotalValue" ForeColor="#000099"></asp:LinkButton>
	  </td>
	 </tr>
	</table>
       </ItemTemplate>
      </asp:TemplateField>


      <asp:TemplateField HeaderText="Po Details">
        <ItemTemplate>
          <asp:GridView ID="GridViewPodetail" runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSourcePOdetail" EnableModelValidation="True"
            CssClass="_GridNakladnayaPodetails">
            <Columns>
              <asp:BoundField DataField="Invoice_No" HeaderText="Invoice_No" 
                SortExpression="Invoice_No" ControlStyle-Width="100px" 
                HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right"/>
              <asp:BoundField DataField="Invoice_Date" HeaderText="Invoice_Date" 
                SortExpression="Invoice_Date" ControlStyle-Width="100px" 
                HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:dd/MM/yyyy}"/>
              <asp:BoundField DataField="InvoiceValue" HeaderText="InvoiceValue" 
                SortExpression="InvoiceValue" ControlStyle-Width="100px" 
                HeaderStyle-Width="100px"
                DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
              <asp:BoundField DataField="Notes" HeaderText="Invoice Notes" 
                SortExpression="Notes" ControlStyle-Width="200px" 
                HeaderStyle-Width="200px"/>
              <asp:BoundField DataField="SiteRecordNo" HeaderText="SiteRecordNo" 
                SortExpression="SiteRecordNo" ControlStyle-Width="100px" 
                HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right"/>
              <asp:BoundField DataField="PaymentDate" HeaderText="PaymentDate" 
                SortExpression="PaymentDate" ControlStyle-Width="100px" 
                HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:dd/MM/yyyy}"/>
              <asp:BoundField DataField="Amount" HeaderText="Payment Ruble With VAT" 
                SortExpression="Amount" ControlStyle-Width="100px"
                HeaderStyle-Width="100px"
                DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
            </Columns>
          </asp:GridView>
          <asp:SqlDataSource ID="SqlDataSourcePOdetail" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     dbo.Table3_Invoice.Invoice_No, dbo.Table3_Invoice.Invoice_Date, dbo.Table3_Invoice.InvoiceValue, dbo.Table3_Invoice.Notes, 
                      dbo.Table4_PaymentRequest.SiteRecordNo, dbo.Table5_PayLog.PaymentDate, dbo.Table5_PayLog.Amount
                      FROM         dbo.Table3_Invoice LEFT JOIN
                      dbo.Table4_PaymentRequest ON dbo.Table3_Invoice.InvoiceID = dbo.Table4_PaymentRequest.InvoiceID 
                      LEFT JOIN
                      dbo.Table5_PayLog ON dbo.Table4_PaymentRequest.PayReqNo = dbo.Table5_PayLog.PayReqNo
                      WHERE     (dbo.Table3_Invoice.PO_No = @PO_No)">
            <SelectParameters>
              <asp:Parameter Name="PO_No" />
            </SelectParameters>
          </asp:SqlDataSource>
        </ItemTemplate>
       
      </asp:TemplateField>
    </Columns>
        <HeaderStyle CssClass="GridHeader" />
        <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceGridNakladnaya" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SPnakladnaya_gridview" SelectCommandType="StoredProcedure">
    <SelectParameters>
      <asp:ControlParameter ControlID="DropDownListPrj" DefaultValue="0" 
        Name="ProjectID" PropertyName="SelectedValue" />
      <asp:ControlParameter ControlID="DropDownListSupplier" DefaultValue="0" 
        Name="SupplierID" PropertyName="SelectedValue" />
    </SelectParameters>
  </asp:SqlDataSource>



</asp:Content>