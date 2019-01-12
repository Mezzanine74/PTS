<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="Default_2OsmanZOOTO"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Main Page</title>
    <script type="text/javascript">

        function pageLoad() {
        }

    </script>
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

         .style8
        {
            vertical-align: middle;
            }        

        .Notf {
            padding:2px 2px 2px 2px !important; 
            margin-left:2px !important;
            background-color:red !important;
            color:white !important;
            font-weight:bold !important;
            font-size:12px !important;
            border-radius:30px !important;
            box-shadow:1px 1px 1px gray !important;
            display:inline !important;
        }

        </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


                                    <span class="badge badge-success" style="width:23px; height:23px; font-size: 16px; padding-top:3px; border-radius: 100%;"><i class="ace-icon fa fa-comments fa-dollar"></i></span>
                                        <span style="color: rgb(154, 188, 50);"><asp:Literal runat="server" ID="LiteralDollar"></asp:Literal></span>

                                    <span class="badge badge-primary" style="width:23px; height:23px; font-size: 16px; padding-top:3px; border-radius: 100%;"><i class="ace-icon fa fa-comments fa-euro"></i></span>
                                        <span style="color: rgb(111, 179, 224);"><asp:Literal runat="server" ID="LiteralEuro"></asp:Literal></span>

    <asp:HoverMenuExtender ID="HoverMenuExtenderOthers"  
            runat="server"  
            TargetControlID="ImageButtonOthers"  
            PopupControlID="PanelOthers" PopupPosition="Top" HoverDelay="500" >
  </asp:HoverMenuExtender>

  <asp:Panel    
            ID="PanelOthers"  
            runat="server"  
            BorderColor="#000000" CssClass="hidepanel"
            BorderWidth="3px"  BackColor="White"> 

            <table>
             <tr>
              <td class="style3" >
                    <asp:ImageButton ID="ImageButtonComparePo" runat="server" visible="true"
                    ImageUrl="~/Images/Others.png" PostBackUrl="~/webforms/ComparePo.aspx" 
                    ToolTip="Compare Po" BorderColor="White" BorderWidth="1px" />
              </td>
              <td class="style7" >
                <div >  <span style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"><%= BodyTexts.Ref("fB7ggky2bUKq2Vhu1bCHkw")%></span> </div>
              </td>
             </tr>
             <tr>
              <td class="style3" >
                    <asp:ImageButton ID="ImageButtonFixRates" runat="server" visible="true"
                    ImageUrl="~/Images/Others.png" PostBackUrl="~/webforms/FixRates.aspx" 
                    ToolTip="Fix Rates" BorderColor="White" BorderWidth="1px" />
              </td>
              <td class="style7" >
                <div >  <span style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"><%= BodyTexts.Ref("6glt/3uyEUehY7mNa/F/DQ")%></span> </div>
              </td>
             </tr>
             <tr>
              <td class="style3" >
                    <asp:ImageButton ID="ImageButtonBalanceBreakDown" runat="server" visible="true"
                    ImageUrl="~/Images/BalanceBreakdown.png" PostBackUrl="~/webforms/ProjectBalanceBreakdown.aspx" 
                    ToolTip="Other 1" BorderColor="White" BorderWidth="1px" />
              </td>
              <td class="style7" >
                <div >  <span style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"><%= BodyTexts.Ref("oeyaGclgZEe20mK5urhAqw")%></span> </div>
              </td>
             </tr>
            </table>

  </asp:Panel>

    <asp:HoverMenuExtender ID="HoverMenuExtenderFollowUpReports"  
            runat="server"  
            TargetControlID="ImageButtonFollowUpReports"  
            PopupControlID="PanelFollowUpReports" PopupPosition="Top" 
            HoverDelay="500">
  </asp:HoverMenuExtender>

  <asp:Panel    
            ID="PanelFollowUpReports"  
            runat="server"  
            BorderColor="#000000" CssClass="hidepanel"
            BorderWidth="3px"  BackColor="White"> 

            <table>
             <tr>
              <td class="style3" >
                    <asp:ImageButton ID="ImageButtonFollowUpReportBackUp" runat="server" visible="true"
                    ImageUrl="~/Images/BackUp.png" PostBackUrl="~/webforms/FollowUpReport2.aspx" 
                    ToolTip="FollowUpReports Backups" BorderColor="White" BorderWidth="1px" />
              </td>
              <td class="style7" >
                <div >  <span style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;">
                <%= BodyTexts.Ref("1si+JfEv5Ea+NvZKRLM/MQ")%>
                </span> </div>
              </td>
             </tr>
             <tr>
              <td class="style3" >
                    <asp:ImageButton ID="ImageButtonFollowUpReportBySupplierWithVAT" runat="server" visible="true"
                    ImageUrl="~/Images/FollowUp_.bmp" PostBackUrl="~/webforms/FollowUpReportBySupplierWithVAT.aspx" 
                    ToolTip="Other 1" BorderColor="White" BorderWidth="1px" />
              </td>
              <td class="style7" >
                <div >  <span style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"><%= BodyTexts.Ref("iSMI3AXi1UC01FReoDDVDg")%> 
                <font style="color: #009933; background-color: #CCFFFF; font-weight: bold; font-size: 12px;"><%= BodyTexts.Ref("uqTxdKORn0q7E+ivUFvbyw")%></font>
                </span> </div>
              </td>

              <td class="style3" >
                    <asp:ImageButton ID="ImageButtonFURSupplierProjectIncVAT" runat="server" visible="true"
                    ImageUrl="~/Images/FollowUp_.bmp" PostBackUrl="~/webforms/FollowUpReportBySupplierProjectWithVAT.aspx" 
                    ToolTip="Other 1" BorderColor="White" BorderWidth="1px" />
              </td>
              <td class="style7" >
                <div >  <span style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"><%= BodyTexts.Ref("ZGaQrToN8k2IKXxZgeTRPA")%>
                <font style="color: #009933; background-color: #CCFFFF; font-weight: bold; font-size: 12px;"><%= BodyTexts.Ref("uqTxdKORn0q7E+ivUFvbyw")%></font>
                </span> </div>
              </td>

             </tr>
             <tr>
              <td class="style3" >
                    <asp:ImageButton ID="ImageButtonFollowUpReportBySupplierExcVAT" runat="server" visible="true"
                    ImageUrl="~/Images/FollowUp_.bmp" PostBackUrl="~/webforms/FollowUpReportBySupplierExcVAT.aspx" 
                    ToolTip="Other 1" BorderColor="White" BorderWidth="1px" />
              </td>
              <td class="style7" >
                <div >  <span style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"><%= BodyTexts.Ref("iSMI3AXi1UC01FReoDDVDg")%> 
                <font style="color: #FF0000; background-color: #FFFF00; font-size: 12px; font-weight: bold;"><%= BodyTexts.Ref("REdYTe8IIUm4V4jqvVASAg")%></font>
                </span> </div>
              </td>

              <td class="style3" >
                    <asp:ImageButton ID="ImageButtonFURSupplierProjectExcVAT" runat="server" visible="true"
                    ImageUrl="~/Images/FollowUp_.bmp" PostBackUrl="~/webforms/FollowUpReportBySupplierProjectExcVAT.aspx" 
                    ToolTip="Other 1" BorderColor="White" BorderWidth="1px" />
              </td>
              <td class="style7" >
                <div >  <span style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"><%= BodyTexts.Ref("ZGaQrToN8k2IKXxZgeTRPA")%>
                <font style="color: #FF0000; background-color: #FFFF00; font-size: 12px; font-weight: bold;"><%= BodyTexts.Ref("REdYTe8IIUm4V4jqvVASAg")%></font>
                </span> </div>
              </td>


             </tr>
             <tr>
              <td class="style3" >
                    <asp:ImageButton ID="ImageButtonFollowUpReportsWithVAT" runat="server" visible="true"
                    ImageUrl="~/Images/FollowUp_.bmp" PostBackUrl="~/webforms/FollowUpReportWithVAT.aspx" 
                    ToolTip="Fix Rates" BorderColor="White" BorderWidth="1px" />
              </td>
              <td class="style7" >
                <div >  <span style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;">
                <%= BodyTexts.Ref("3UNJZxIWnUiuuRwkYr82tw")%> 
                <font style="color: #009933; background-color: #CCFFFF; font-weight: bold; font-size: 12px;"><%= BodyTexts.Ref("uqTxdKORn0q7E+ivUFvbyw")%></font>
                </span> </div>
              </td>
             </tr>
             <tr>
              <td class="style3" >
                    <asp:ImageButton ID="ImageButtonFollowUpReportsExcVAT" runat="server" visible="true"
                    ImageUrl="~/Images/FollowUp_.bmp" PostBackUrl="~/webforms/FollowUpReport.aspx" 
                    ToolTip="Other 1" BorderColor="White" BorderWidth="1px" />
              </td>
              <td class="style7" >
                <div >  <span style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"><%= BodyTexts.Ref("3UNJZxIWnUiuuRwkYr82tw")%>
                <font style="color: #FF0000; background-color: #FFFF00; font-size: 12px; font-weight: bold;"><%= BodyTexts.Ref("REdYTe8IIUm4V4jqvVASAg")%></font>
                </span> </div>
              </td>
             </tr>
            </table>

  </asp:Panel>

    <asp:Timer ID="TimerMessageSent" interval="700" runat="server" Enabled="False"></asp:Timer>

    <asp:Timer ID="NotificationTimer" interval="20000" runat="server" >    </asp:Timer>
    <asp:Timer ID="PMTimer" interval="20000" runat="server" > </asp:Timer>
    <asp:Timer ID="Frametimer" interval="20000" runat="server" > </asp:Timer>
    
<%--    <table class="hide">
        <tr>
            <td>
                    <asp:GridView ID="GridViewMaxRate" runat="server" AutoGenerateColumns="False" DataKeyNames="Date" DataSourceID="SqlDataSourceMaxExcRate" Font-Size="10px">
                        <Columns>
                            <asp:BoundField DataField="Date" HeaderText="Date" ReadOnly="True" SortExpression="Date" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" >
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="RubbleDollar" HeaderText="RubbleDollar" SortExpression="RubbleDollar" ItemStyle-HorizontalAlign="Center"  >
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="RubbleEuro" HeaderText="RubbleEuro" SortExpression="RubbleEuro" ItemStyle-HorizontalAlign="Center"  >
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#99CCFF" />
                        <RowStyle Height="20px" />
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSourceMaxExcRate" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                        SelectCommand="SP_GetExcRateMax" SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>
            </td>
            <td style="padding-left:20px; font-size:small;">
                <asp:Literal ID="LiteralCrudeOil" runat="server" ></asp:Literal>
            </td>
        </tr>
    </table>--%>

<table>
    <tr>
        <td>

<table>
    <tr>
<%--       <td style="vertical-align:top;">



              <br />

                <script type="text/javascript"
	                src="http://www.oil-price.net/widgets/brent_crude_price_large/gen.php?lang=en">
                </script>
                <noscript> To get the BRENT <a href="http://www.oil-price.net/dashboard.php?lang=en#brent_crude_price_large">oil price</a>, please enable Javascript.
                </noscript>


        </td>--%>
        <td>

            <table >
                <tr>
                    <td class="style3" >

                                         </td>
                    <td class="style3" >

                                        <asp:ImageButton ID="ImageButtonPO" runat="server" ImageUrl="~/Images/NewPurchaseOrder_.bmp" 
                            PostBackUrl="~/webforms/pocreate.aspx" ToolTip="Create Purchase Order" BorderColor="White" 
                                            BorderWidth="1px" />


                    </td>
                    <td class="style7" >

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("Y0i4b4o8xEWsCCxrnDf8Qw")%></div>


                    </td>
                    <td class="style2" >
                        <asp:ImageButton ID="ImageButtonPOEdit" runat="server" ImageUrl="~/Images/Edit PurchaseOrder_.bmp" 
                            PostBackUrl="~/webforms/editpo.aspx" ToolTip="Edit Purchase Order" 
                            BorderColor="White" BorderWidth="1px" />

                    </td>
                    <td class="style7" >

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("aOfX+llnm0OCrn21IqoxvQ")%></div>


                    </td>
                    <td class="style2" >
                        <asp:ImageButton ID="ImageButtonPackingListToday" runat="server" 
                            ImageUrl="~/Images/PackingListToday.bmp" PostBackUrl="~/webforms/PackingListToday.aspx" 
                            ToolTip="Packing List Today" BorderColor="White" BorderWidth="1px" />

                         </td>
                    <td class="style7" >
                          <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;">
                              <%= BodyTexts.Ref("nwuBTlazpk2Zinp7G2jmVg")%></div>
                          </td>
                    <td style="text-align:left;">
                        <asp:UpdatePanel ID="UpdatePanelNotification" runat="server" updatemode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger controlid="NotificationTimer" eventname="Tick" />
                                    </Triggers>
                                    <ContentTemplate>

                                     <script type="text/javascript">
                                         Sys.Application.add_load(PTS_BindEvents);
                                     </script>


                                          <asp:HyperLink ID="HyperLinkNotification" runat="server" Target="_blank"  >_</asp:HyperLink>
                                   </ContentTemplate> 
                        </asp:UpdatePanel>

                           </td>
                </tr>
                <tr>
                    <td class="style3" >
                         </td>
                    <td class="style3" >
                        <asp:ImageButton ID="ImageButtonInvoice" runat="server" 
                            ImageUrl="~/Images/NewInvoice_.bmp" PostBackUrl="~/webforms/invoicedefine.aspx" 
                            ToolTip="Define New Invoice" BorderColor="White" BorderWidth="1px" />

                    </td>
                    <td class="style7" >

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("uxYlsqq0mkGYYpn8a7u4yA")%></div>


                    </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButtonInvoiceEdit" runat="server" 
                            ImageUrl="~/Images/Edit Invoice_.bmp" PostBackUrl="~/webforms/editinvoice.aspx" 
                            ToolTip="Edit Invoice" BorderColor="White" BorderWidth="1px" />

                    </td>
                    <td class="style7">

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("aOfX+llnm0OCrn21IqoxvQ")%></div>


                    </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButtonPayments" runat="server" 
                            ImageUrl="~/Images/Payment.bmp" PostBackUrl="~/webforms/Payments.aspx" 
                            ToolTip="Payment List" BorderWidth="1px" BorderColor="White"  />

                         </td>
                    <td class="style7" >
                     
                      
        <div >  <span style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> <%= BodyTexts.Ref("B0yPgHLCp0K2rReX/aajLQ")%></span> </div>                      

                          </td>
                    <td class="style8" rowspan="8" style="vertical-align:top!important;" >

                    </td>
                </tr>
                <tr>
                    <td class="style3" >
                         </td>
                    <td class="style3" >
                        <asp:ImageButton ID="ImageButtonPaymentReq" runat="server" 
                            ImageUrl="~/Images/NewPaymentRequest_.bmp" PostBackUrl="~/webforms/paymentrequest.aspx" 
                            ToolTip="Send Payment Request" BorderColor="White" BorderWidth="1px" />

                    </td>
                    <td class="style7" >

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("vU2YoQZln0+35yp6b/QOPA")%></div>


                    </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButtonPaymentReqEdit" runat="server" 
                            ImageUrl="~/Images/Edit PaymentRequest_.bmp" PostBackUrl="~/webforms/editpaymentreq.aspx" 
                            ToolTip="Edit Payment Request" BorderColor="White" BorderWidth="1px" />

                    </td>
                    <td class="style7">

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("aOfX+llnm0OCrn21IqoxvQ")%></div>


                    </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButtonFrameApproval" runat="server" 		
                            ImageUrl="~/Images/PM_Approval.png"  
                            ToolTip="Analytics" PostBackUrl="~/webforms/PoApprovalFrame.aspx" />
                         </td>
                    <td class="style7" >
                                          
                        <div >  
                            <span style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> <%= BodyTexts.Ref("x/PDAVzeykOrlNaRoArPxg")%></span>

                                <asp:UpdatePanel ID="UpdatePanelFramePO" runat="server" updatemode="Conditional" RenderMode="Inline">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger controlid="Frametimer" eventname="Tick" />
                                            </Triggers>
                                            <ContentTemplate>

                                             <script type="text/javascript">
                                                 Sys.Application.add_load(PTS_BindEvents);
                                             </script>

                                                <asp:HyperLink ID="HyperLinkFramePO" runat="server" Target="_blank" CssClass="badge badge-danger icon-animated-vertical" Text="test" NavigateUrl="~/PoApprovalFrame.aspx" ></asp:HyperLink> 
                                           </ContentTemplate> 
                                </asp:UpdatePanel>

                        </div>

                    </td>
                </tr>
                <tr>
                    <td class="style3" >
                         </td>
                    <td class="style3" >
                        <asp:ImageButton ID="ImageButtonPayLog" runat="server" 
                            ImageUrl="~/Images/Paid_.bmp" PostBackUrl="~/webforms/paylog.aspx" 
                            ToolTip="Enter Paid Items" BorderColor="White" BorderWidth="1px" />

                    </td>
                    <td class="style7" >

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("TDtJraVy40m0MVuTEUOjdQ")%></div>


                    </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButtonPayLogEdit" runat="server" 
                            ImageUrl="~/Images/Edit Paid_.bmp" PostBackUrl="~/webforms/editpaylog.aspx" 
                            ToolTip="Edit Paid Items" BorderColor="White" BorderWidth="1px" />

                    </td>
                    <td class="style7">

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("aOfX+llnm0OCrn21IqoxvQ")%></div>


                    </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButtonPM_Approval" runat="server" 
                            ImageUrl="~/Images/PM_Approval.png" PostBackUrl="~/webforms/PR_PMapproval.aspx" 
                            ToolTip="PM Approval" BorderColor="White" BorderWidth="1px" />

                         </td>
                    <td class="style7">

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("CWYNWVO3kEOfE7ye1CpIYA")%>

                        <asp:UpdatePanel ID="UpdatePanelPMcounter" runat="server" updatemode="Conditional" RenderMode="Inline">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger controlid="PMtimer" eventname="Tick" />
                                    </Triggers>
                                    <ContentTemplate>

                                     <script type="text/javascript">
                                         Sys.Application.add_load(PTS_BindEvents);
                                     </script>

                                        <asp:HyperLink ID="HyperLinkPMCounter" runat="server" Target="_blank" CssClass="badge badge-danger icon-animated-vertical" Text="test" NavigateUrl="~/PR_PMapproval.aspx" ></asp:HyperLink> 
                                   </ContentTemplate> 
                        </asp:UpdatePanel>

        </div>


                          </td>
                </tr>
                <tr>
                    <td class="style3" >
                           </td>
                    <td class="style3" >
                        <asp:ImageButton ID="ImageButtonPending" runat="server" 
                            ImageUrl="~/Images/Pending_.bmp" PostBackUrl="~/webforms/Pendinglist.aspx" 
                            ToolTip="Pending List" BorderColor="White" BorderWidth="1px" />

                          </td>
                    <td class="style7" >

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("VlJLLvQaHEu3lnTp0eL3nA")%></div>


                          </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButtonMonitoring" runat="server" 
                            ImageUrl="~/Images/Monitoring_.bmp" PostBackUrl="~/webforms/Monitoring.aspx" 
                            ToolTip="Monitoring Records" BorderColor="White" BorderWidth="1px" />

                         </td>
                    <td class="style7">

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("SzrFDgl6bEeRmYPkuEDxDw")%></div>


                         </td>
                    <td class="style2" >
                        <asp:ImageButton ID="ImageButtonPObreakdown" runat="server" 
                            ImageUrl="~/Images/PObreakdown.png" PostBackUrl="~/webforms/PObreakdown.aspx" 
                            ToolTip="PO breakdown" BorderColor="White" BorderWidth="1px" />

                         </td>
                    <td class="style7">

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("mIG1qK8GxEG5aoUAYQInRQ")%> </div>


                          </td>
                </tr>
                <tr>
                    <td class="style3" >
                         </td>
                    <td class="style3" >
                        <asp:ImageButton ID="ImageButtonOpen_Po" runat="server" 
                            ImageUrl="~/Images/open_po.bmp"  
                            PostBackUrl="~/webforms/open_po.aspx"
                            ToolTip="Open Po" BorderColor="White" BorderWidth="1px" />

                    </td>
                    <td class="style7" >

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("Tjfkr/nOOEiZJKyxvAF6gw")%></div>


                    </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButtonContract" runat="server"  BorderColor="White" BorderWidth="1px"
                            ImageUrl="~/Images/Contract_.bmp"  PostBackUrl="~/webforms/Contractview.aspx" 
                            ToolTip="Contract"  />                

                         </td>
                    <td class="style7">
            <FONT style="color: #808080; font-size: 10px; font-family: Arial;"><%= BodyTexts.Ref("q51PydLI9Eqp1qhKslGz1Q")%>
                        </FONT><FONT 
                        </td>
                
                    &nbsp;<td class="style2">
                        <asp:ImageButton ID="ImageButtonFollowUpReports" runat="server" 
                            ImageUrl="~/Images/FollowUp_.bmp"   
                            ToolTip="Follow Up Report" BorderColor="White" BorderWidth="1px" />

                          </td>
                    <td class="style7">

                        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("3UNJZxIWnUiuuRwkYr82tw")%> 
                            </div>

                           </td>
                </tr>
                <tr>
                    <td class="style3" >
                          </td>
                    <td class="style3" >

                        <asp:ImageButton ID="ImageButtonApprovalMatrix" runat="server" visible="true"
                            ImageUrl="~/Images/ApprovalMatrix.png" PostBackUrl="~/webforms/ApprovalMatrix.aspx" 
                            ToolTip="Approval Matrix" BorderColor="White" BorderWidth="1px" />
                              </td>
                    <td class="style7" >

                            <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;">
                               <asp:Literal ID="LiteralApprovalMatrix" runat="server" Text="Approval Matrix"></asp:Literal> 
                               <asp:Literal ID="LiteralUnreadComm" runat="server" ></asp:Literal>
                               <asp:Literal ID="LiteralUserMissingApprovals" runat="server" ></asp:Literal>
                            </div>


                         </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButtonMissingDocs" runat="server" visible="true"
                            ImageUrl="~/Images/MissingDocs.bmp" PostBackUrl="~/webforms/missingdocs.aspx" 
                            ToolTip="Missing Documents" BorderColor="White" BorderWidth="1px" />

                         </td>
                    <td class="style7">



        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
             <%= BodyTexts.Ref("a3N2hiZfq06xai7MapCEfg")%></div>



                        </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButtonNakladnaya" runat="server" 
                            ImageUrl="~/Images/Nak_Shot_Akt_.bmp"  
                            ToolTip="Nakladnaya-Shot Faktura-Akt" PostBackUrl="~/webforms/nakladnaya.aspx" 
                            BorderColor="White" BorderWidth="1px" />

                         </td>
                    <td class="style7">

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
            <%= BodyTexts.Ref("mlmUn7IQoUasRVE3sXwK3Q")%></div>


                    </td>
                </tr>
                <tr>
                    <td class="style3" >
                         </td>
                    <td class="style3" >
                        <asp:ImageButton ID="ImageButtonOthers" runat="server" visible="true"
                            ImageUrl="~/Images/Others.png"  
                            ToolTip="Others" BorderColor="White" BorderWidth="1px" />
                         </td>
                    <td class="style7" >

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"><span style="color: #808080; font-size: 14px; font-weight: bold; "><%= BodyTexts.Ref("EBZIpPrZa0e5a8p8quuHEQ")%></span></div>


                          </td>
                    <td class="style2"  >
                        <asp:ImageButton ID="ImageButtonSearch" runat="server" visible="true"
                            ImageUrl="~/Images/search_invoice_.bmp" PostBackUrl="~/webforms/Searchinvoice.aspx" 
                            ToolTip="Search Invoice" BorderColor="White" BorderWidth="1px" />

                         </td>
                    <td class="style7" >

        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;">
        Search 
        </div>


                          </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButtonContractVersusPo" runat="server" 
                            ImageUrl="~/Images/FxRates.png"  
                            ToolTip="Fx Rates" PostBackUrl="~/webforms/fixrates.aspx" BorderColor="White" 
                            BorderWidth="1px" />
                          </td>
                    <td class="style7">

                        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;"> 
                            <%= BodyTexts.Ref("6glt/3uyEUehY7mNa/F/DQ")%></div>

                           </td>
                </tr>
                <tr>
                    <td class="style3" >
                         &nbsp;</td>
                    <td class="style3" >
                        <asp:ImageButton ID="ImageButtonSearchContractBreakdown" runat="server" visible="false"
                            ImageUrl="~/Images/search_invoice_.bmp"  
                            ToolTip="Search Contract Breakdown" BorderColor="White" BorderWidth="1px" 
                            PostBackUrl="~/webforms/SearchContracts.aspx" />
                    </td>
                    <td class="style7" >
                            <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left; display:none;">
                            <%= BodyTexts.Ref("svQce+C9qkyPz4L7T7lzlw")%> 
                            </div>
                          </td>
                    <td class="style2"   >
                        <asp:ImageButton ID="ImageButtonIntAudit" runat="server" visible="true"
                            ImageUrl="~/Images/IntAudit.png" PostBackUrl="~/webforms/IntAudit.aspx" 
                            ToolTip="Internal Audit" BorderColor="White" BorderWidth="1px" />

                         </td>
                    <td class="style7" >

                    <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;">
                    <%= BodyTexts.Ref("iwJxva0y00OlyMzjNJrODw")%> 
                    </div>


                          </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButtonBudget" runat="server" 
                            ImageUrl="~/Images/Budget.bmp" PostBackUrl="~/webforms/costreport.aspx" 
                            ToolTip="Budget" BorderColor="White" BorderWidth="1px" />
                    </td>
                    <td class="style7">
        <div style="color: #808080; font-size: 10px; font-family: Arial; text-align: left;" id="Divreport" runat="server"> 
            </div>
                           </td>
                </tr>
                </table>

        </td>
    </tr>
</table>

        </td>
        <td style="vertical-align:top!important;">

<%--                    <span class="label label-success arrowed arrowed-right">online</span>

                        <asp:UpdatePanel ID="TimedPanel" runat="server" updatemode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
                                    </Triggers>
                                    <ContentTemplate>

                                         <script type="text/javascript">
                                             Sys.Application.add_load(PTS_BindEvents);
                                         </script>

                                        <asp:GridView ID="GridViewOnlineUsers" runat="server" AutoGenerateColumns="False" 
                                            DataSourceID="SqlDataSourceOnlineUsers" GridLines="None"
                                            ShowHeader="False" CssClass="table table-nonfluid borderless" >
                                            <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelUser" CssClass="hide" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                                            <usercontrol:ImageUserPhoto ID="userphoto" runat="server" UserName='<%# Bind("UserName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="hide">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelPageUserOn" runat="server" Text='<%# Bind("PageName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                          <asp:SqlDataSource ID="SqlDataSourceOnlineUsers" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                                            SelectCommand="SELECT dbo.aspnet_Users.UserName, 
                              CASE WHEN dbo.aspnet_Users.UserName = N'savas' THEN N'' ELSE REPLACE(View_UserLastVisitTime2.PageName, '.aspx', '') END AS PageName
                                FROM         dbo.aspnet_Users INNER JOIN
                                                      dbo.View_UserLastVisitTime2 ON dbo.aspnet_Users.UserName = dbo.View_UserLastVisitTime2.UserName INNER JOIN
                                                      dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId AND dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId
                              WHERE dbo.aspnet_Users.UserName <> N'nila.isaeva'
                                ORDER BY DATEDIFF(minute, dbo.View_UserLastVisitTime2.VisitTime, GETDATE())">
                          </asp:SqlDataSource>

                            <br />

                          <rsweb:ReportViewer ID="ReportViewerTotalUserToday" runat="server" ProcessingMode="Remote" 
                          ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowFindControls="False"
                          ShowPageNavigationControls="False" ShowParameterPrompts="False" ShowPromptAreaButton="False"
                          ShowToolBar="False" ShowZoomControl="False" Visible="false" 
                          SizeToReportContent="True" ZoomMode="FullPage"  AsyncRendering="False">
                          <ServerReport
                             DisplayName="MyReport" ReportPath="/karadumanco/reports/ReportGaugeTotalUserToday"
                                ReportServerUrl="https://rs2k801.discountasp.net/ReportServer" />
                          </rsweb:ReportViewer>

                                   </ContentTemplate> 
                        </asp:UpdatePanel>
--%>
        </td>
    </tr>
</table>


    <asp:SqlDataSource ID="SqlDataSourceAgeingInsert" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceFollowUp" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    InsertCommand = "INSERT INTO Table_FollowUpReportSummary
           ([ProjectID]
           ,[DayOfRun]
           ,[OverallPoTotalDollarExcVAT]
           ,[OverallPoTotalEuroExcVAT]
           ,[OverallPoTotalRubleExcVAT]
           ,[OverallDollarPaidExcVAT]
           ,[OverallEuroPaidExcVAT]
           ,[OverallRublePaidExcVAT]
           ,[OverallDollarPendingExcVAT]
           ,[OverallEuroPendingExcVAT]
           ,[OverallRublePendingExcVAT]
           ,[OverallDoneDollarPO]
           ,[OverallDoneEuroPO]
           ,[OverallDoneRublePO]
           ,[OverallPartialDollarPO]
           ,[OverallPartialEuroPO]
           ,[OverallPartialRublePO])
(SELECT ProjectID,GETDATE()
,OverallPoTotalDollarExcVAT
,OverallPoTotalEuroExcVAT
,OverallPoTotalRubleExcVAT
,OverallDollarPaidExcVAT
,OverallEuroPaidExcVAT
,OverallRublePaidExcVAT
,OverallDollarPendingExcVAT
,OverallEuroPendingExcVAT
,OverallRublePendingExcVAT
,OverallDoneDollarPO
,OverallDoneEuroPO
,OverallDoneRublePO
,OverallPartialDollarPO
,OverallPartialEuroPO
,OverallPartialRublePO
FROM View_CostCodeSummary1 
WHERE (ProjectID IN (SELECT ProjectID FROM Table1_Project WHERE Report = 'True'))
AND (OverallPoTotalDollarExcVAT IS NOT NULL))">
    </asp:SqlDataSource>

</asp:Content>

