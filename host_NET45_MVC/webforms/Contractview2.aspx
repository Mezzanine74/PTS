<%@ Page Title="" Language="VB" MasterPageFile="~/site.master"  MaintainScrollPositionOnPostback="true" 
AutoEventWireup="false" EnableEventValidation ="false" CodeFile="Contractview2.aspx.vb" Inherits="contractView_ApprovalMatrixPTM3SSTZORT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="POdetailsForEmail.ascx" tagname="SeperateControl" tagprefix="uc1" %>

<%@ Register src="WebUserControl_ContractEmailBody.ascx" tagname="SeperateControl2" tagprefix="uc1" %>

<%@ Register src="WebUserControl_AddendumEmailBody.ascx" tagname="SeperateControl3" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <title>View Contracts</title>

  <script type="text/javascript">
      function pageLoad() {
      }
      function SetAutoCompleteWidth(source, EventArgs) {
          var target
          target = ((document.getElementBy) ? document.getElementById("AutoCompleteDiv") : document.all.AutoCompleteDiv);
          target.style.width = '441px';
      }

      function SetAutoCompleteWidthSearch(source, EventArgs) {
          var a
          a = document.getElementById('MainContent_AutoCompleteDivSearch');
          a.style.width = '730px';
      }

      function SetContextKey() {

          $find('MainContent_AutoCompleteExtenderSearch').set_contextKey($get('MainContent_TextBoxUserName').value);

      }

    </script>

    <style type="text/css">

        .float_left{
            float:left;
        }

        .div_font
        {
            font-family:Consolas;
            margin: 0px;
            font-size:small;
            background-color: White;
            cursor: default;
            overflow-y: auto;
            overflow-x: hidden;
            max-height:500px;
            text-align: left;
            border: 1px solid #777;
            z-index:10000;
        }

        div > .panel {
            padding-bottom: 0px!important;
            margin-bottom: 3px!important;
        }

        div > .panel-heading {
          padding: 3px 4px!important;
          font-weight:bold!important;
        }

        div > .panel-body {
            padding:4px;
        }

        div > .panel-body > .table {
            margin-bottom:0px;
        }

        div > .panel-body > .table > thead > tr > td {
            font-size:smaller!important;
        }

        div > .panel-body > .table > tbody > tr > td {
            font-size:smaller!important;
        }

        div > .panel-body > .table > thead > tr > td, 
        div > .panel-body > .table > tbody > tr > td, 
        div > .panel-body > .table > tfoot > tr > td {
            padding:4px;
        }

        div > .table.table-nonfluid.borderless{
            margin-bottom:0px!important;
        }

        div > .table.table-nonfluid.borderless > tbody > tr > td{
            padding:1px!important;
            font-size:10px !important;
        }

        div > .panel-body > .table > tbody > tr > td > .alert {
            margin-bottom:0px!important;
            padding:4px !important;
            font-size:11px !important;
        }
        
        .LawyerComment {
            padding:2px 2px 2px 2px;
            background-color: #F2F4F4;
            border-width:thin;
            border-style:solid;
        }

        .pnl_newsup {
            margin:1px; padding:2px; font-size:8px; color:#E74C3C;
            background-color:#FDEDEC;border-color:Black;border-style:Solid; 
            border-width:1px; font-weight:bold;width:100px;text-align:center;
        }

        .showMoreLess {
            text-align:right; clear:both;
        }

        .showMoreLess a {
            color:blue; font-weight:bold; cursor:pointer;
        }

        .showMoreLessAdd {
            text-align:right; clear:both;
        }

        .showMoreLessAdd a {
            color:blue; font-weight:bold; cursor:pointer;
        }

        .HooverToolTip{
            padding:3px;
            background-color:black;
            color:white;
            font-size:10px!important;
        }

        .dx-tag-container > .dx-texteditor-input{
            border:none !important;
        }

        .btn-lg-search{
            padding:10px !important;
            font-size:20px !important;
            margin:10px !important;
        }

        .td_fl_mng {
            width:25px; border-color:lightgray; border-width:thin; border-style:solid;
        }

    </style>

   <link href="/assets/css/jquery.tagsinput.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:HiddenField ID="HiddenButtonExceptionalApproveShowOrNot" runat="server" value=""/>
    <asp:HiddenField ID="hiddenSearchAdvanceMode" runat="server" Value="0" />

                        <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Visible="false">
                            <Settings RootFolder="~/" ThumbnailFolder="~/Thumb/" EnableMultiSelect="false" />
                            <SettingsFileList View="Details"></SettingsFileList>
                            <SettingsEditing AllowCopy="false" AllowCreate="false" AllowDownload="true"/>
                            <SettingsUpload Enabled="false"></SettingsUpload>
                            <SettingsFolders visible="false" />
                        </dx:ASPxFileManager>


    <div id="ModalFileManager" class="modal">
        <div class="modal-dialog modal-dialog-center">
            <div class="modal-content modal_inlineBlock">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Manage Files</h4>
                </div>
                <div class="modal-body" style="width:800px;" >
                    <asp:Panel ID="panelContainer" runat="server">

                    </asp:Panel>
                    <asp:HiddenField ID="hiddenFileManagerUploadContractDOCMode" runat="server" Value="0" />
                    <asp:HiddenField ID="hiddenFileManagerUploadContractPDFMode" runat="server" Value="0" />
                    <asp:HiddenField ID="hiddenFileManagerUploadAddendumDOCMode" runat="server" Value="0" />
                    <asp:HiddenField ID="hiddenFileManagerUploadAddendumPDFMode" runat="server" Value="0" />

                    <asp:HiddenField ID="HiddenFieldfileManager_Link" runat="server" Value="~/webforms" />
                    <asp:HiddenField ID="HiddenFieldfileManager_Editable" runat="server" Value="0" />
                </div>
            </div>
        </div>
    </div>

    <div id="divContractSearch" class="modal">
        <div class="modal-dialog modal-dialog-center">
            <div class="modal-content modal_inlineBlock">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Enter Search Options</h4>
                </div>
                <div class="modal-body" style="width: 1500px;">

                    <table class="table">
                        <thead>
                            <tr>
                                <td>Project
                                </td>
                                <td>Supplier
                                </td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <div id="inputProjects" style="width: 500px;"></div>
                                    <asp:HiddenField ID="hiddenSearchInputProjects" runat="server" />
                                </td>
                                <td>
                                    <div id="inputSuppliers" style="width: 500px;"></div>
                                    <asp:HiddenField ID="hiddenSearchInputSuppliers" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>

                    <table class="table">
                        <thead>
                            <tr>
                                <td>Entry Interval
                                </td>
                                <td>Approval Interval
                                </td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBoxEntryInterval" runat="server" CssClass="add_daterangepicker" Style="margin-bottom: 2px;"
                                        Width="160px" OnTextChanged="TextBoxEntryInterval_TextChanged"></asp:TextBox>
<%--                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('.add_daterangepicker').on('apply.daterangepicker', function (ev, picker) {
                                                __doPostBack('ctl00$MainContent$TextBoxEntryInterval', '')
                                                console.log("i am ready");
                                            });
                                        });
                                    </script>--%>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxApprovalInterval" runat="server" CssClass="add_daterangepicker" 
                                        Width="160px"></asp:TextBox>

                                </td>
                            </tr>
                        </tbody>
                    </table>

                    <table class="table">

                        <thead>
                            <tr>
                                <td>Контактный номер
                                </td>
                                <td>Контактный дата
                                </td>
                                <td>Описание
                                </td>
                                <td>Тип
                                </td>
                                <td>Подпись
                                </td>
                                <td>валюта
                                </td>
                                <td>advance
                                </td>
                                <td>удержание
                                </td>
                                <td>документы
                                </td>
                            </tr>
                        </thead>

                        <tbody>
                            <tr style="font-size:11px;">
                                <td>
                                    <asp:TextBox ID="textBoxSearchInputContractNo" runat="server" placeholder="contract no"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="textBoxSearchInputContractDate" runat="server" placeholder="contract date" CssClass="add_datepicker"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="textBoxSearchInputContractDescription" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    <div style="margin-bottom:3px;">
                                        <span style="width:40px !important; display:inline-block;">Ser</span>
                                        <asp:CheckBox ID="cbxSearchInputContractTypeSer" runat="server" Checked="true" />
                                    </div>

                                    <div style="margin-bottom:3px;">
                                        <span style="width:40px !important; display:inline-block;">Sub</span>
                                        <asp:CheckBox ID="cbxSearchInputContractTypeSub" runat="server" Checked="true" />
                                    </div>

                                    <div style="margin-bottom:3px;">
                                        <span style="width:40px !important; display:inline-block;">Sup</span>
                                        <asp:CheckBox ID="cbxSearchInputContractTypeSup" runat="server" Checked="true" />
                                    </div>
                                </td>
                                <td>
                                    <%--<usercontrol:checkboxBootstrap ID="checkBoxSearchInputSignBysupplier" runat="server" Text="signed by supplier" />--%>
                                    <div style="margin-bottom:3px;">
                                        <span style="width:80px !important; display:inline-block;">by supplier</span>
                                        <asp:DropDownList ID="ddlBoxSearchInputSignBysupplier" runat="server">
                                            <asp:ListItem value="-">-</asp:ListItem>
                                            <asp:ListItem value="1">yes</asp:ListItem>
                                            <asp:ListItem value="0">no</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <%--<usercontrol:checkboxBootstrap ID="checkBoxSearchInputSignByMercury" runat="server" Text="signed by us" />--%>
                                    <div>
                                        <span style="width:80px !important; display:inline-block;">by us</span>
                                        <asp:DropDownList ID="ddlSearchInputSignByMercury" runat="server">
                                            <asp:ListItem value="-">-</asp:ListItem>
                                            <asp:ListItem value="1">yes</asp:ListItem>
                                            <asp:ListItem value="0">no</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>

                                    <div style="margin-bottom:3px;">
                                        <span style="width:50px !important; display:inline-block;">Ruble</span>
                                        <asp:CheckBox ID="cbxSearchInputCurrencyRuble" runat="server" Checked="true" />
                                    </div>

                                    <div style="margin-bottom:3px;">
                                        <span style="width:50px !important; display:inline-block;">Dollar</span>
                                        <asp:CheckBox ID="cbxSearchInputCurrencyDollar" runat="server" Checked="true" />
                                    </div>

                                    <div style="margin-bottom:3px;">
                                        <span style="width:50px !important; display:inline-block;">Euro</span>
                                        <asp:CheckBox ID="cbxSearchInputCurrencyEuro" runat="server" Checked="true" />
                                    </div>

                                </td>
                                <td>
                                    <div style="margin-bottom:3px;">
                                        <span style="width:50px !important; display:inline-block;">Advance</span>
                                        <asp:DropDownList ID="ddlBoxSearchInputAdvance" runat="server">
                                            <asp:ListItem value="-">-</asp:ListItem>
                                            <asp:ListItem value="1">yes</asp:ListItem>
                                            <asp:ListItem value="0">no</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </td>
                                <td>
                                    <div style="margin-bottom:3px;">
                                        <span style="width:50px !important; display:inline-block;">Retention</span>
                                        <asp:DropDownList ID="ddlSearchInputRetention" runat="server">
                                            <asp:ListItem value="-">-</asp:ListItem>
                                            <asp:ListItem value="1">yes</asp:ListItem>
                                            <asp:ListItem value="0">no</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>
                                    <div style="margin-bottom:3px;">
                                        <span style="width:50px !important; display:inline-block;">Draft</span>
                                        <asp:DropDownList ID="ddlBoxSearchInputDraft" runat="server">
                                            <asp:ListItem value="-">-</asp:ListItem>
                                            <asp:ListItem value="1">yes</asp:ListItem>
                                            <asp:ListItem value="0">no</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div style="margin-bottom:3px;">
                                        <span style="width:50px !important; display:inline-block;">Final</span>
                                        <asp:DropDownList ID="ddlSearchInputFinal" runat="server">
                                            <asp:ListItem value="-">-</asp:ListItem>
                                            <asp:ListItem value="1">yes</asp:ListItem>
                                            <asp:ListItem value="0">no</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                        </tbody>

                    </table>
                    <div style="float:right;">
                        <asp:Button ID="btnSearchAdvance" runat="server" CssClass="btn btn-lg-search btn-success" Text="Search" OnClick="btnSearchAdvance_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>

<div id="ModalRaisePo" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">!</h4>
            </div>
            <div class="modal-body">

                Total Po under this Frame Contract already exceeds Budget.
                <br />
                You cannot proceed.
                <br />
                <asp:HyperLink ID="HyperlinkFrameBudgetModal" runat="server" style="color:red;" target="_blank" >See breakdown for details</asp:HyperLink>
            </div>
        </div>
    </div>
</div>

<asp:panel ID="PanelToAddendums"  runat="server" CssClass="hidepanel">
   <uc1:SeperateControl ID="POdetailsForEmail" runat="server" />
   <uc1:SeperateControl2 ID="WebUserControl_ContractEmailBody" runat="server" />
   <uc1:SeperateControl3 ID="WebUserControl_AddendumEmailBody" runat="server" />
   <asp:Literal ID="LiteralCounter" runat="server" Text="0" ></asp:Literal>
   <asp:Label ID="LabelProjectName" runat="server" ></asp:Label>
   <asp:Label ID="LabelGridViewPagingStatus" runat="server" ></asp:Label>   
   <asp:Label ID="LabelGridViewPageSize" runat="server" ></asp:Label>         
   <asp:Label ID="LabelGridViewPageNumber" runat="server" ></asp:Label>      
   <asp:Label ID="LabelPOno" runat="server" ></asp:Label>
   <asp:Label ID="LabelSupplierNameV" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractNo" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractDate" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractValue" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractValueWithVAT" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractCurrencyV" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractType" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractDescription" runat="server" ></asp:Label>
   <asp:Label ID="LabelContractID" runat="server" ></asp:Label>   
   <asp:DropDownList ID="DDLforSupplierNameCheck" runat="server"
    DataSourceID="SqlDataSourceforSupplierNameCheck" DataTextField="SupplierName" DataValueField="SupplierName" >
   </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceforSupplierNameCheck" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                     SelectCommand="SELECT RTRIM(SupplierName) AS SupplierName FROM Table6_Supplier WHERE SupplierID = @SupplierID">
                    <SelectParameters>
                        <asp:Parameter Name="SupplierID" />
                    </SelectParameters>
                </asp:SqlDataSource>
   <asp:Label ID="LabelEditModeIndex" runat="server" ></asp:Label>   
   <asp:SqlDataSource ID="SqlDataSourcePoDublication" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>">
   </asp:SqlDataSource>
   <asp:DropDownList ID="DropDownListPoDublication" runat="server"
    DataTextField="PO_No" DataValueField="PO_No" DataSourceID="SqlDataSourcePoDublication" >
   </asp:DropDownList>
</asp:panel>

    <table>
        <tr>
            <td style="width: 350px; background-color: #F8F9F9; padding: 2px;">
                <asp:DropDownList ID="DropDownListPrjID" runat="server" AutoPostBack="True" Style="margin-bottom: 2px; width: 320px;"
                    DataSourceID="SqlDataSourcePrj"
                    DataTextField="ProjectName" DataValueField="ProjectID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourcePrj" runat="server"
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="
                        SELECT     ProjectID, ProjectName, UserName
                        FROM         (         SELECT     -1 AS ProjectID, N'__Выбрать проект' AS ProjectName, NULL AS UserName
                                               UNION ALL
                                               SELECT     0 AS ProjectID, N'_ВСЕ ПРОЕКТЫ' AS ProjectName, NULL AS UserName
                                               UNION ALL
                                               SELECT     TOP (100) PERCENT Table1_Project.ProjectID, RTRIM(Table1_Project.ProjectName) AS ProjectName, aspnet_Users.UserName
                                               FROM         Table1_Project INNER JOIN
                                                                     Table_Prj_User_Junction ON Table1_Project.ProjectID = Table_Prj_User_Junction.ProjectID INNER JOIN
                                                                     aspnet_Users ON Table_Prj_User_Junction.UserID = aspnet_Users.UserId INNER JOIN
                                                                     Table_Contracts ON Table1_Project.ProjectID = Table_Contracts.ProjectID
                                               GROUP BY Table1_Project.ProjectID, Table1_Project.ProjectName, aspnet_Users.UserName
                                               HAVING      (aspnet_Users.UserName = @UserName)) AS DataSource1
                        ORDER BY ProjectName 
                    ">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName"
                            PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <asp:DropDownList ID="DropDownListSupplier" runat="server" AutoPostBack="True" style="width: 320px;"
                    DataSourceID="SqlDataSourceSupplier"
                    DataTextField="SupplierName" DataValueField="SupplierID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceSupplier" runat="server"
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="
                    SELECT     TOP (100) PERCENT dbo.Table_Contracts.ProjectID, dbo.Table_Contracts.SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName) 
                      AS SupplierName
                        FROM         dbo.Table_Contracts INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table_Contracts.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                      dbo.Table1_Project ON dbo.Table_Contracts.ProjectID = dbo.Table1_Project.ProjectID
                        GROUP BY dbo.Table_Contracts.ProjectID, dbo.Table_Contracts.SupplierID, RTRIM(dbo.Table6_Supplier.SupplierName)
                       HAVING      (dbo.Table_Contracts.ProjectID = @ProjectID)
                        ORDER BY SupplierName">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListPrjID" Name="ProjectID"
                            PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </td>
            <td style="width: 164px; background-color: #F8F9F9; padding: 2px;">

            </td>
            <td style="width: 304px; background-color: #F8F9F9; padding: 2px;">
                <asp:TextBox ID="TextBoxSearch" runat="server" OnTextChanged="TextBoxSearch_TextChanged"
                    Width="300px" Style="font-family: Consolas" 
                    AutoPostBack="true" onkeyup="SetContextKey()"></asp:TextBox>
                <div id="AutoCompleteDivSearch" runat="server" class="div_font"></div>

                <asp:AutoCompleteExtender ID="AutoCompleteExtenderSearch" runat="server"
                    CompletionInterval="0" CompletionListElementID="AutoCompleteDivSearch"
                    CompletionSetCount="12" MinimumPrefixLength="0" UseContextKey="true"
                    OnClientShown="SetAutoCompleteWidthSearch" ServiceMethod="SearchContractNo"
                    ServicePath="AutoComplete.asmx" TargetControlID="TextBoxSearch">
                </asp:AutoCompleteExtender>

            </td>
            <td style="background-color:#F8F9F9;">
                <table>
                    <tr>
                        <td style="width:32px; padding:2px;">
                            <asp:ImageButton ID="ImageButton1" runat="server" BorderColor="#666666" 
                                BorderStyle="Solid" BorderWidth="1px" ImageUrl="~/Images/Excel.jpg" 
                                ToolTip="Export To Excel" Width="20px" />
                        </td>
                        <td style="width:60px;padding:2px;">
                        <label><%= BodyTexts.Ref("bOGHuvlOtUK+HEZc2Zxp9g")%></label>
                             <asp:DropDownList ID="DDLpageSize" runat="server" AutoPostBack="true" >
                                                    <asp:ListItem Value="ALL">-</asp:ListItem>                            
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="40">40</asp:ListItem>
                                                    <asp:ListItem Value="60">60</asp:ListItem>
                             </asp:DropDownList>
                        </td>
                        <td style="text-align: right;padding:2px;">
                           <asp:Label ID="LabelTheLatestContractNo" runat="server" ></asp:Label>
                        </td>
                        <td style="width: 60px;padding:2px;">
                            <asp:ImageButton ID="ImageButtonNotes" runat="server" data-rel="tooltip" ToolTip="List Of Suppliers"
                              ImageUrl="~/Images/Notes.png" PostBackUrl="~/webforms/contractnotes.aspx" />
                        </td>
                        <td style="padding:2px;">
                            <a ID="btnSearchContract" style="cursor:pointer; font-size:xx-large;" class="btn btn-lg btn-success hide">Search</a>
                            <asp:Panel ID="panelInsurancearning" runat="server" Visible="false">

                              <div>
                                  <asp:Label ID="LabelInsurance" runat="server" BackColor="Yellow" 
                                  Font-Bold="True" ForeColor="Red" Text="Insurance alert!"></asp:Label>
                              </div>
                              <div style="OVERFLOW:auto; WIDTH:500px; HEIGHT:45px">
                                      <asp:DataList ID="DataListInsurance" runat="server" 
                                        DataSourceID="SqlDataSourceInsurance" Font-Size="10px" ItemStyle-Wrap="False" 
                                        RepeatDirection="Horizontal">
                                        <ItemTemplate>
                                          <table>
                                            <td>
                                              <div style="padding: 2px; background-color: #CC0000; text-align: right; margin-bottom: 1px; font-weight: bold; color: #FFFFFF;">
                                                <asp:Label ID="LabelProjects" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                                              </div>
                                            </td>
                                            <td>
                                              <div style="padding: 2px; background-color: #CC0000; text-align: right; margin-bottom: 1px; font-weight: bold; color: #FFFFFF;">
                                                <asp:Label ID="LabelDayToGo" runat="server" Text='<%# Bind("DayToGo") %>'></asp:Label>
                                              </div>
                                            </td>
                                          </table>
                                        </ItemTemplate>
                                      </asp:DataList>
                                      <asp:SqlDataSource ID="SqlDataSourceInsurance" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                                        SelectCommand=" SELECT     RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, RTRIM(CONVERT(nvarchar(3), CASE WHEN dbo.Table1_ProjectInsurCertf.InsuranceFinish IS NOT NULL 
                                      THEN datediff(day, getdate(), dbo.Table1_ProjectInsurCertf.InsuranceFinish) END)) + ' day left' AS DayToGo
                                    FROM         dbo.Table1_Project INNER JOIN
                                      dbo.Table1_ProjectInsurCertf ON dbo.Table1_Project.ProjectID = dbo.Table1_ProjectInsurCertf.ProjectID
                                    WHERE     (CASE WHEN dbo.Table1_ProjectInsurCertf.InsuranceFinish IS NOT NULL THEN datediff(day, getdate(), dbo.Table1_ProjectInsurCertf.InsuranceFinish) END <= 15) AND 
                                      (dbo.Table1_Project.InsuranceFinish IS NOT NULL) AND (CASE WHEN dbo.Table1_ProjectInsurCertf.InsuranceFinish IS NOT NULL THEN datediff(day, getdate(), 
                                      dbo.Table1_ProjectInsurCertf.InsuranceFinish) END >= 0) "
                                        ></asp:SqlDataSource>
                              </div>
                            </asp:Panel>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

          <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="hidepanel"></asp:TextBox>
        
    <asp:GridView ID="GridViewShowContract" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSourceForContractGrid" EnableModelValidation="True" GridLines="None"
        CssClass="table table-nonfluid" AllowSorting="True" DataKeyNames="ContractID" >
        <Columns>

<asp:TemplateField HeaderStyle-CssClass="hide" >
                <HeaderTemplate>
                        <table style="color: #FFFFFF;">
                            <tr>
                                <td style="width:40px;">
                                </td>
                                <td style="width:20px;">
                                </td>
                                <td style="width:80px; text-align: center;">
                                    <%= BodyTexts.Ref("33HRnsxsc0aaZpvXTRqzvw")%>
                                </td>
                                <td style="width:80px; text-align: center;">
                                <%= BodyTexts.Ref("b/QwmB2AWEmXNrVeVHPG7g")%>
                                
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table> 
                                    </td>
                                    </tr>
                                    </table>                                
                                
                                </td>
                                <td style="width:80px; text-align: center;">
                                    <%= BodyTexts.Ref("aUlY16DSxEuv5OYTuG/x+g")%>
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table> 
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:135px; text-align: center;">
                                    <%= BodyTexts.Ref("WCc0K46nMU6P4KVjfJyRyQ")%>
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderSupplierASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderSupplierASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderSupplierDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderSupplierDESC_Click" />
                                                </td>
                                            </tr>
                                        </table> 
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:130px; text-align: center;">
                                    ContractDescription
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderDescriptionASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderDescriptionASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderDescriptionDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderDescriptionDESC_Click" />
                                                </td>
                                            </tr>
                                        </table> 
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:50px; text-align: center;">
                                    Contract Type
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderContractTypeASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderContractTypeASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderContractTypeDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderContractTypeDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                    
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:50px; text-align: center;">
                                    Signed By Supplier
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderSignedBySupplierASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" 
                                                        onclick="ImageButtonHeaderSignedBySupplierASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderSignedBySupplierDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" 
                                                        onclick="ImageButtonHeaderSignedBySupplierDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                      
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:50px; text-align: center;">
                                    Signed By mercury
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderSignedByMercuryASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderSignedByMercuryASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderSignedByMercuryDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" 
                                                        onclick="ImageButtonHeaderSignedByMercuryDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                      
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:50px; text-align: center; display: none;">
                                    Collected By Supplier
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderCollectedBySupplierASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" 
                                                        onclick="ImageButtonHeaderCollectedBySupplierASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderCollectedBySupplierDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" 
                                                        onclick="ImageButtonHeaderCollectedBySupplierDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                        
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:100px; text-align: center;">
                                    <asp:Label ID="LabelLarisaTakHatela" runat="server" ></asp:Label>
                                </td>
                                <td style="width:85px; text-align: center;">
                                    Contract Value
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderContractValueASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderContractValueASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderContractValueDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderContractValueDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                       
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:50px; text-align: center;">
                                </td>
                                <td style="width:50px; text-align: center;">
                                    Retention
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderRetentionASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderRetentionASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderRetentionDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderRetentionDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                     
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:50px; text-align: center;">
                                    Archived
                                    
                                    <table width=100% >
                                    <tr>
                                    <td width=100% >
                                        <table align=center>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderArchivedASC" runat="server" 
                                                        ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderArchivedASC_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonHeaderArchivedDESC" runat="server" 
                                                        ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderArchivedDESC_Click" />
                                                </td>
                                            </tr>
                                        </table>                                      
                                    </td>
                                    </tr>
                                    </table>                                        
                                    
                                </td>
                                <td style="width:100px; text-align: center;">
                                    Note
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="width: 50px; text-align: center; display: none;">
                                                DOC
                                            </td>
                                            <td style="width: 50px; text-align: center;">
                                                PDF
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                        </table>
                </HeaderTemplate>

                <ItemTemplate>
                        <asp:Panel ID="panelInItem" runat="server" >
                         <asp:HoverMenuExtender ID="HoverMenuExtenderItem" runat="server" 
                          PopupPosition="Top" PopupControlID="panelInItemX" 
                          TargetControlID="panelInItem" >
                         </asp:HoverMenuExtender>
                             <asp:Label ID="LabelPoStatus" runat="server" ></asp:Label>
                             <asp:Label ID="LabelReadyForPo" runat="server" ForeColor="Red" Visible="false" ></asp:Label>
                        <table>		

                            <tr>
                              <td>
                                <asp:Panel ID="panelInItemX" runat="server">
                                  <asp:Label ID="LabelHooverToolTipContract" runat="server" CssClass="HooverToolTip"></asp:Label>
                                </asp:Panel>
                              </td>
                            </tr>	

                        </table>

                            <asp:Panel ID="PanelCommonInformationR1Item" runat="server" CssClass="panel panel-info " Width="1200px">
                                <div class="panel-heading">
                                    <%= BodyTexts.Ref("Cz1u1YxQmUO8D6vQzUaGEA")%>
                                </div>

                                <div class="panel-body">
                                    
                                    <table class="table" >
                                        <thead>
                                            <td>
                                                
                                            </td>
                                            <td>
                                                #
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("Mu+drePgpEK6h8XzFfTOJg")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("b/QwmB2AWEmXNrVeVHPG7g")%> <br />
                                                <asp:ImageButton ID="ImageButtonHeaderContractNoASC" runat="server" 
                                                ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderContractNoASC_Click" />
                                                &nbsp;
                                                <asp:ImageButton ID="ImageButtonHeaderContractNoDESC" runat="server" 
                                                ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderContractNoDESC_Click" />

                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("aUlY16DSxEuv5OYTuG/x+g")%> <br />
                                                <asp:ImageButton ID="ImageButtonHeaderContractDateASC" runat="server" 
                                                ImageUrl="~/Images/ASC.JPG" onclick="ImageButtonHeaderContractDateASC_Click" />
                                                &nbsp;
                                                <asp:ImageButton ID="ImageButtonHeaderContractDateDESC" runat="server" 
                                                ImageUrl="~/Images/DESC.JPG" onclick="ImageButtonHeaderContractDateDESC_Click" />

                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("WCc0K46nMU6P4KVjfJyRyQ")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("foHRgj5mokmXEh62sTOoiQ")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("ofeMLaQHw0mejHavxFcTrg")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("vXdfZGm13kewiqmHmdWdyQ")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("hL8TNg+mj02+XJTLz0GqUA")%>
                                            </td>
                                            <td style="display:none;">
                                                <%= BodyTexts.Ref("9jcfRxS3WEqqHigdLtr6BQ")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("1epQY1LCBkqFks1Ea7MVfw")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("69H0Rzyc2k6HgOxJbJQKug")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("mB+gkXNFaEegQ/Gm8GRgsA")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("AG8NVnjS50yHEBirA9v9Jw")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("mXA5GI226kq6JCs9nCrlaQ")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("nEM0zCjdzkGup5oo1REa6g")%>
                                            </td>
                                        </thead>
                                        
                                        <tr>
                                            <td style="width:75px;">
                                                <div class="btn-group-vertical" role="group">
                                                            <asp:Button ID="ButtonEdit" runat="server" CausesValidation="False" 
                                                                CommandName="Edit" CssClass="btn btn-minier btn-success" onmouseover="this.style.cursor='hand'" 
                                                                Text="Edit"  />

                                                            <asp:Button ID="ButtonDelete" runat="server" CausesValidation="False" 
                                                                CommandName="Delete" CssClass="btn btn-minier btn-danger"
                                                                onmouseover="this.style.cursor='hand'" Text="Delete" />
                                                </div>
                                            </td>
                                            <td style="width:20px;">
                                                <asp:Label ID="LabelSortNumber" runat="server" Font-Bold="True" 
                                                    Font-Size="12px" ForeColor="#CC0000"></asp:Label>
                                            </td>
                                            <td style="width:80px;">
                                                <table style="width:80px; text-align: center;">
                                                    <tr>
                                                        <td style="padding-bottom:3px;">
                                                            <asp:Label ID="LabelProjectNameItem" runat="server" ></asp:Label>
                                                            <asp:Image ID="ImageLogo" runat="server" ImageUrl="~/images/logo/183-JTI.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LabelPOnoItem" runat="server" Text='<%# Bind("PO_No") %>' 
                                                              BackColor="#FFFF66" Visible="false"></asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <div class="btn-group-vertical">

                                                            <asp:HyperLink ID="HyperlinkPoNo" runat="server" 
                                                                NavigateUrl='<%# "~/invoicedefine.aspx?PoNo=" + Eval("PO_No") %>'
                                                                Text='<%# Bind("PO_No") %>' Visible="false" 
                                                                Target="_blank" ></asp:HyperLink>

                                                                <div id="Div_RaisePO" runat="server">
                                                                    <asp:LinkButton ID="LinkButtonRaisePO" runat="server" 
                                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" 
                                                                        CommandName="RaisePoForFrameContract" Visible="false" CssClass="btn btn-minier btn-primary"
                                                                        PostBackUrl="~/webforms/pocreateNew.aspx">Raise PO</asp:LinkButton>

                                                                </div>

                                                            <asp:LinkButton ID="LinkButtonAddAddendum" runat="server" 
                                                                CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" 
                                                                CommandName="AddAddendum" CssClass="btn btn-minier btn-success" >Add Addendum</asp:LinkButton>

                                                            <asp:LinkButton ID="LinkButtonNumberOfAddendum" runat="server" 
                                                                CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" 
                                                                CommandName="ShowAddendum" CssClass=" btn btn-minier btn-danger" ></asp:LinkButton>
                                                            <asp:Label ID="LabelHideShow" runat="server" Text="0" Visible="false" ></asp:Label> 

                                                             <asp:HyperLink ID="HyperlinkAddClientData" runat="server"  CssClass="btn btn-minier btn-success"
                                                             Target="_blank" Visible="false"  >
                                                             </asp:HyperLink>

                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                            <td style="width:80px; "">
                                                <asp:Label ID="LabelContractNoItem" runat="server" 
                                                    Text='<%# Bind("ContractNo") %>'></asp:Label>
                                                <asp:Label ID="LabelContractIDitem" runat="server" Font-Bold="true" ForeColor="DarkBlue" Font-Size="Medium"
                                                    Text='<%# Bind("ContractID") %>' visible="false" ></asp:Label>
                                                <asp:Label ID="LabelCvWthVAT" runat="server" Font-Bold="true" ForeColor="red" Font-Size="Medium"
                                                    visible="false" ></asp:Label>
                                                <asp:Label ID="LabelRowIndexOnGridviewContract" runat="server"  Text="test"
                                                    visible="false"></asp:Label>
                                                <asp:Label ID="LabelCreatedBy" runat="server"  Text='<%# Bind("CreatedBy") %>' 
                                                    visible="false"></asp:Label>
                                                <asp:Label ID="LabelUpdatedBy" runat="server"  Text='<%# Bind("UpdatedBy") %>' 
                                                    visible="false"></asp:Label>
                                                <asp:Label ID="LabelPersonCreated" runat="server"  Text='<%# Bind("PersonCreated") %>' 
                                                    visible="false"></asp:Label>
                                                <asp:Label ID="LabelPersonUpdated" runat="server"  Text='<%# Bind("PersonUpdated") %>' 
                                                    visible="false"></asp:Label>
                                            </td>
                                            <td style="width:80px; ">
                                                <asp:Label ID="LabelContractDateItem" runat="server" 
                                                    Text='<%# Bind("ContractDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </td>
                                            <td style="width:100px; ">
                                                <asp:Label ID="LabelSupplierName" runat="server"
                                                    Text='<%# Bind("SupplierName") %>'></asp:Label>
                                                <asp:Image ID="ImagePersonOrNot" runat="server" />
                                            </td>
                                            <td style="width:100px; ">
                                                <asp:Label ID="LabelContractDescItem" runat="server" 
                                                    Text='<%# Bind("ContractDescription") %>'></asp:Label>
                                            </td>
                                            <td style="width:50px; ">
                                                <asp:Label ID="LabelContractTypeItem" runat="server" 
                                                    Text='<%# Bind("ContractType") %>'></asp:Label>
                                            </td>
                                            <td style="width:50px; ">
                                                <usercontrol:checkboxBootstrap ID="CheckBoxSignBySupplier" runat="server" 
                                                    Checked='<%# Bind("SignBySupplier") %>' _Disable="true" />
                                            </td>
                                            <td style="width:50px; ">
                                                <usercontrol:checkboxBootstrap ID="CheckBoxSignByMercury" runat="server" 
                                                    Checked='<%# Bind("SignByMercury") %>' _Disable="true"/>
                                            </td>
                                            <td style="display:none;">

                                            </td>
                                            <td style="width:100px; ">
                                                <asp:Label ID="LabelContractGivenTo" runat="server" Text='<%# Bind("ContractGivenTo") %>'></asp:Label>
                                            </td>
                                            <td style="width:85px; font-size:10px; text-align:right;">
                                                <span style="font-style:italic; color:Gray;">
                                                  <asp:literal ID="LiteralContractValueExcVATItem" Text="Exc. VAT" runat="server" ></asp:literal>
                                                </span>
                                                <asp:Label ID="LabelContractValueItem" runat="server" 
                                                    Text='<%# Bind("ContractValue_woVAT", "{0:###,###,###.00}") %>'></asp:Label>
                                                <br />

                                                <span style="font-style:italic; color:Gray;">
                                                <asp:literal ID="LiteralContractValueWithVATItem" Text="With VAT"  runat="server" ></asp:literal>
                                                </span>
                                                <asp:literal ID="LiteralContractValueWithVATItemValue" Text='<%# Bind("ContractValue_withVAT", "{0:###,###,###.00}") %>'  runat="server" >
                                                </asp:literal>
                                                <br />

                                                <span style="font-style:italic; color:Gray;">
                                                <asp:literal ID="LiteralVATItem" Text="VAT %"  runat="server" ></asp:literal>
                                                </span>
                                                <asp:literal ID="LiteralVATItemValue" Text='<%# Bind("VATpercent", "{0:###,###,###.00}") %>' runat="server" >
                                                </asp:literal>

                                                <%--this should be removed from data table as well--%>
                                                <div class="hide">
                                                    <span 
                                                        style="font-style:italic; color:Gray;">
                                                    <asp:literal ID="LiteralBudgetTitle" Text="Budget With VAT"  runat="server" ></asp:literal>
                                                    </span>
                                                    <asp:literal ID="LiteralBudgetValue" Text='<%# Bind("Budget", "{0:###,###,###.00}")%>'  runat="server" >
                                                    </asp:literal>
                                                    <br />
                                                                <asp:ImageButton ID="ImageButtonBudgetPDF" runat="server" data-rel="tooltip" data-placement="top" class="tooltip-success" data-original-title="Budget Document"
                                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BudgetLinkToPDF")%>' 
                                                                    CommandName="OpenBudgetPDF" />
                                                </div>
                                                <%--------------this should be removed from data table as well--%>
                                            </td>
                                            <td style="width:50px; ">

                                                    <asp:Label ID="LabelContractCurrency" runat="server"  Visible="false"
                                                        Text='<%# Bind("ContractCurrency") %>'></asp:Label>
                                                <asp:Image ID="ImageCurrency" runat="server" />
                                            </td>
                                            <td style="width:50px; ">
                                                <asp:Label ID="LabelRetention" runat="server" Text='<%# Bind("Retention") %>'></asp:Label>
                                            </td>
                                            <td style="width:50px; ">
                                                <usercontrol:checkboxBootstrap ID="CheckBoxArchivedByMercury" runat="server" 
                                                        Checked='<%# Bind("ArchivedByMercury") %>' _Disable="true" />
                                            </td>
                                            <td style="width:100px; ">
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Note") %>'></asp:Label>
                                            </td>
                                            <td>



                                                <table>
                                                    <tr>
                                                        <td style="width:25px; border-color:lightgray; border-width:thin; border-style:solid;">
                                                            <asp:ImageButton ID="ImageButtonLinkToTemplatefile_DOC" runat="server" 
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToTemplatefile_DOC") %>' 
                                                            CommandName="OpenDOC" />

                                                        </td>
                                                        <td style="width:25px; border-color:lightgray; border-width:thin; border-style:solid;">
                                                            <asp:ImageButton ID="ImageButtonLinkToPDFcopy" runat="server" 
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToPDFcopy") %>' 
                                                            CommandName="OpenPDF" />

                                                        </td>
                                                    </tr>
                                                </table>



                                            </td>
                                        </tr>
                                    </table>

                                    <table class="table hide" id="tblCommonNewGN" runat="server" >
                                          <tr>
                                           <td style="width:100px;">
                                             <asp:Button ID="ButtonExceptionalApprove" runat="server" CausesValidation="False" CommandName="ExceptionalApproveContract"  Visible="false"
                                              CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn btn-minier" />
                                              <br />
                                                    <usercontrol:ImageUserPhoto ID="ImageRequestedBy" runat="server" UserName='<%# Eval("RequestedBy")%>' />
                                           </td>
                                           <td style="width:150px; vertical-align: top;">
                                          <asp:Gridview ID="GridviewMissingItemsForApproval" runat="server" 
                                            CssClass="GridviewMissingBeforeApproval" DataSourceID="SqlDataSourceMissingItemsForApproval"
                                            AutoGenerateColumns="False" >
                                            <Columns>
                                              <asp:TemplateField HeaderText="Требуемые товары до их утверждения" ItemStyle-Width="120px">
                                                <ItemTemplate>
                                                 <asp:Literal id="LiteralTitle" runat="server" Text='<%# "• " + Eval("Title") %>'></asp:Literal>
                                                </ItemTemplate>
                                              </asp:TemplateField>
                                            </Columns>
                                          </asp:Gridview>
                                          <asp:SqlDataSource ID="SqlDataSourceMissingItemsForApproval" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                            SelectCommand="ContractReadyForApproval" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                              <asp:Parameter DefaultValue="0" Name="ContractID" Type="Int32" />
                                              <asp:Parameter DefaultValue="1" Name="Shift" Type="Int32" />
                                            </SelectParameters>
                                          </asp:SqlDataSource>
                                        <%------------------------------------------------------------ MISSING ITEMS FOR APPROVAL --%>
                                           </td>
                                           <td style="width:250px;vertical-align: top;">
                                          <asp:Gridview ID="GridviewApprovalStatus" runat="server" 
                                            DataSourceID="SqlDataSourceApprovalStatus" CssClass="table table-nonfluid borderless"
                                            AutoGenerateColumns="False" onrowcommand="GridviewApprovalStatus_RowCommand" 
                                               onrowdatabound="GridviewApprovalStatus_RowDataBound" GridLines="None" >
                                            <Columns>
                                              <asp:TemplateField >
                                                <ItemTemplate>
                                                 <asp:Literal id="LiteralUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Literal>
                                                 <asp:Literal id="LiteralWhichLawyer" Visible="false" runat="server" Text='<%# Bind("WhoApprovedFromLawyers")%>'></asp:Literal>
                                                </ItemTemplate>
                                              </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Approved" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                <%-- LiteralApproved and LiteralContractID should stay here, they are being used in RowCommand --%>
                                                <asp:Literal id="LiteralApproved" runat="server" Text='<%# Bind("Approved") %>' Visible="false"></asp:Literal>
                                                <asp:Literal id="LiteralContractID" runat="server" Text='<%# Bind("ContractID") %>' Visible="false"></asp:Literal>
                                                <asp:Literal id="LiteralRejectContractGirls" runat="server" Text='<%# Bind("Exception") %>' Visible="false"></asp:Literal>

                                                <asp:ImageButton ID="ImageButtonApproval" runat="server" CommandName="Approval" 
                                                CommandArgument='<%# Container.DataItemIndex %>' 
                                                 CausesValidation="False" />

                                                <asp:LinkButton ID="LinkButtonRejectContractGirls" runat="server" CommandName="ApprovalByRejecting" 
                                                CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False" />
                                                </ItemTemplate>
                                              </asp:TemplateField>

                                              <asp:TemplateField HeaderText="When Approved">
                                                <ItemTemplate>
                                                 <asp:Literal id="LiteralWhenApproved" runat="server" Text='<%# Bind("WhenApproved", "{0:dd/MM/yyyy}")%>'></asp:Literal>
                                                </ItemTemplate>
                                              </asp:TemplateField>
                                            </Columns>
                                               <HeaderStyle CssClass="hide" />
                                          </asp:Gridview>
                                              <div id="DivLawyerComment" runat="server" class="LawyerComment"><asp:Label ID="LabelLawyerComment" runat="server" Text='<%# Bind("Remark")%>'></asp:Label></div>  
                                          <asp:SqlDataSource ID="SqlDataSourceApprovalStatus" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                            SelectCommand="ApprovalStatusContract" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                              <asp:Parameter DefaultValue="0" Name="ContractID" Type="Int32" />
                                              <asp:Parameter DefaultValue="1" Name="Shift" Type="Int32" />
                                            </SelectParameters>
                                          </asp:SqlDataSource>
                                        <%----------------------------------------------------------------- APPROVAL MATRIX --%>
                                           </td>
                                           <td style="width:250px;;vertical-align: top;">
                                        <asp:GridView ID="GridViewOffers" runat="server" AutoGenerateColumns="False" 
                                           DataKeyNames="ContractID" DataSourceID="SqlDataSourceOffers" 
                                           EnableModelValidation="True"  
                                           CssClass="GridviewApprovalStatus" onrowcommand="GridViewOffers_RowCommand" 
                                               onrowdatabound="GridViewOffers_RowDataBound"  >
                                           <Columns>
                                             <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" 
                                             HeaderStyle-Width="150px" ItemStyle-Width="150px"
                                               SortExpression="SupplierName" />
                                             <asp:BoundField DataField="OfferValueWithVAT" HeaderText="Offer Value With VAT" 
                                             HeaderStyle-Width="60px" ItemStyle-Width="60px"
                                             DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" 
                                               SortExpression="OfferValueWithVAT" />
                                             <asp:BoundField DataField="Currency" SortExpression="Currency" />
                                             <asp:TemplateField>
                                              <ItemTemplate>
                                              <asp:ImageButton ID="ImageButtonOfferZip" runat="server" CommandName="OpenZip" 
                                              CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Attachment") %>' 
                                              imageUrl="~/images/zipicon.png" CausesValidation="False" />
                                              </ItemTemplate>
                                             </asp:TemplateField>
                                           </Columns>
                                         </asp:GridView>
                                         <asp:SqlDataSource ID="SqlDataSourceOffers" runat="server" 
                                           ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                           SelectCommand="SELECT ContractID 
                                                              ,[SupplierName]
                                                              ,[OfferValueWithVAT]
                                                              ,[Currency]
                                                              ,[Attachment]
                                                          FROM [Table_Contract_Offers] 
                                                          INNER JOIN Table6_Supplier 
                                                          ON Table6_Supplier.SupplierID = Table_Contract_Offers.SupplierID
                                                          WHERE ContractID = @ContractID">
                                           <SelectParameters>
                                             <asp:Parameter DefaultValue="0" Name="ContractID" Type="Int32" />
                                           </SelectParameters>
                                         </asp:SqlDataSource>

                                        <%----------------------------------------------------------------- COMMERCIAL OFFERS --%>
                                           </td>
                                           <td style="padding-left:10px;">
<%--                                            <asp:Panel ID="PanelNominated" runat="server" >
                                            <asp:Label ID="LabelESTM_Approval" runat="server" Font-Size="10px"
                                                 Text="" ForeColor="#FF3399" Width="300px">
                                            </asp:Label>
                                                <br />
                                            <asp:DropDownList ID="DDLestm" runat="server" CssClass="DrpDwnListGeneral"
                                                SelectedValue='<%# Bind("NominatedApprovedByESTM")%>' AutoPostBack="true" OnSelectedIndexChanged="DDLestm_SelectedIndexChanged">
                                                <asp:ListItem Value="0">No Response Yet</asp:ListItem>
                                                <asp:ListItem Value="1">Confirmed</asp:ListItem>
                                                <asp:ListItem Value="2">Not Confirmed</asp:ListItem>
                                            </asp:DropDownList>
                                                <hr />
                                             <span title="" class="label label-success label-lg tooltip-success" data-rel="tooltip" data-placement="top" 
                                                 data-original-title="No Commercial Offer Required" >
                                             Nominated Subcontractor
                                             </span>
                                            </asp:Panel>--%>

                                            <asp:Panel ID="PanelFrameContract" runat="server" >
                                             <span Class="PanelFrameContract" >
                                              <asp:label ID="LabelFrameContract" runat="server" CssClass="label label-success label-xlg tooltip-success" Text="Frame Contract" data-rel="tooltip" data-placement="top" ToolTip="Frame contracts has no value. You can create as many PO as you want with the buttom on the left - Raise PO"></asp:label>
                                             </span>
                                            </asp:Panel>
                                           </td>
                                           <td style="padding-left:10px;">
                                                <asp:Panel CssClass="pnl_newsup" ID="panelNewSupplier" runat="server" Visible="false">
                                                    <%= BodyTexts.Ref("qcBhmdE2a0efLQGscCaCiQ")%>
                                                </asp:Panel>
                                           </td>
                                          </tr>
                                    </table>

                                </div>

                            </asp:Panel>

      <!---- COMMERCIAL TERMS ---------------------------------------------------------->

                            <asp:Panel ID="PanelCommercialTermsContractItem" runat="server" CssClass="panel panel-info float_left hide" Width="1000px">
                                <div class="panel-heading">
                                    <%= BodyTexts.Ref("1lBVf/JgM0yopXMK71U1xA")%>
                                    <span style="float:right;"><asp:literal ID="LiteralCostCode" Text='<%# Bind("CostCode")%>' runat="server" ></asp:literal></span>
                                </div>

                                <div class="panel-body">

                                    <table class="table">
                                        <thead>
                                            <td>
                                                <%= BodyTexts.Ref("hEhO9xKZVke1gtHcr6u2Gg")%>:	
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("eOZry6gjB0yqlsMmUT7/aw")%>:	
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("WBn92MUF20iopsBEY2+rxA")%>:	
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>:
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("uzuez9RR302+6q9tOPvnJw")%>:	
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("76jVcY8TbUSVUcJGrpnfhg")%>:	
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("Vh6I6dyPyU+d+3zCSzOyhQ")%>:	
                                            </td>
                                        </thead>
                                        <tr>
                                            <td style="width:75px;">
                                                <asp:literal ID="LiteralStartDate" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy}") %>' runat="server" ></asp:literal>
                                            </td>
                                            <td style="width:75px;">
                                                <asp:literal ID="LiteralFinishDate" Text='<%# Bind("FinishDate", "{0:dd/MM/yyyy}") %>' runat="server" ></asp:literal>
                                            </td>
                                            <td style="width:75px;">
                                                <asp:literal ID="LiteralPenalties" Text='<%# Bind("Penalties") %>' runat="server" ></asp:literal>
                                            </td>
                                            <td style="width:200px;">
                                                <asp:literal ID="LiteralPenaltyNote" Text='<%# Bind("PenaltiesNote")%>' runat="server" ></asp:literal>
                                            </td>
                                            <td style="width:75px;">
                                                <asp:Literal ID="LiteralPenaltiesToSupplier" runat="server" Text='<%# Bind("PenaltiesToSupplier")%>'></asp:Literal>
                                            </td>
                                            <td style="width:200px;">
                                                <asp:Literal ID="LiteralPenaltyNoteToSupplier" runat="server" Text='<%# Bind("PenaltiesToSupplierNote")%>'></asp:Literal>
                                            </td>
                                            <td style="width:150px;">
                                                <asp:literal ID="LiteralDeliveryTerms" Text='<%# Bind("DeliveryTerms") %>' runat="server" ></asp:literal>
                                            </td>
                                            <td style="width:100px;">
                                                <asp:literal ID="LiteralGuaranteePeriod" Text='<%# Bind("GuaranteePeriod") %>' runat="server" ></asp:literal>
                                            </td>
                                        </tr>

                                    </table>

                                </div>

                                <div class="panel-heading">
                                    <%= BodyTexts.Ref("IeKLbyowHECapdUmB+qKrw")%>
                                </div>

                                <div class="panel-body">

                                  <table class="table">

                                      <thead>
                                          <td>
                                              <%= BodyTexts.Ref("wt6Qt9QUh0GDaiAKFZhS0A")%>
                                          </td>
                                          <td>
                                              <%= BodyTexts.Ref("L4img0LG+06x0/+AqzBnkQ")%>
                                          </td>
                                          <td>
                                              <%= BodyTexts.Ref("3faC83SQyUS74LvrVeBEgA")%>
                                          </td>
                                          <td>
                                              <%= BodyTexts.Ref("CdFoIWOhUUq43gPj9azb9Q")%>
                                          </td>
                                          <td>
                                              <%= BodyTexts.Ref("v4yTZj0Pi0CtAif6PQDZ7w")%>
                                          </td>
                                      </thead>

                                      <tr>
                                          <td>
                                              <asp:Literal ID="LiteralAdvance" runat="server" Text='<%# Bind("Advance", "{0:###,###,###.0} %")%>' ></asp:Literal>
                                          </td>
                                          <td>
                                              <asp:Literal ID="LiteralInterim" runat="server" Text='<%# Bind("Interim", "{0:###,###,###.0} %")%>' ></asp:Literal>
                                          </td>
                                          <td>
                                              <asp:Literal ID="LiteralShipment" runat="server" Text='<%# Bind("Shipment", "{0:###,###,###.0} %")%>' ></asp:Literal>
                                          </td>
                                          <td>
                                              <asp:Literal ID="LiteralDelivery" runat="server" Text='<%# Bind("Delivery", "{0:###,###,###.0} %")%>' ></asp:Literal>
                                          </td>
                                          <td>
                                              <asp:Literal ID="LiteralRetention" runat="server" Text='<%# Bind("Retention", "{0:###,###,###.0} %")%>' ></asp:Literal>
                                          </td>
                                      </tr>

                                  </table>

                                </div>

                            </asp:Panel>

      <!---------------------------------------------------------- COMMERCIAL TERMS ---->

<%--                    <asp:Panel ID="PanelTagContract" CssClass="panel panel-info float_left " Width="200px" runat="server" Visible="false">
                        <div class="panel-heading">
                            Tags
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="TxtBoxTagsContract" runat="server"></asp:TextBox>
                        </div>
                        <asp:HiddenField ID="HdContractId" runat="server" Value='<%# Eval("ContractID")%>' />
                    </asp:Panel>--%>

                            <!-- Client Contract Additional Details -->
                            <asp:Panel ID="PanelClientContractAdditionalDetails" runat="server" Visible="false" CssClass="panel panel-info " Width="710px">
                                <div class="panel-heading">
                                    <%= BodyTexts.Ref("VP2VuGvsMkepSyxNTrK95w")%>
                                </div>

                                <div class="panel-body">
                                    <table class="table">
                                        <thead>
                                            <td style="width:250px;">
                                                <%= BodyTexts.Ref("76jVcY8TbUSVUcJGrpnfhg")%>
                                            </td>
                                            <td style="width:80px;">
                                                <%= BodyTexts.Ref("RpoOSVLThUeJmnFtjELvww")%>
                                            </td>
                                            <td style="width:80px;">
                                                <%= BodyTexts.Ref("Kd8jnLnalkSmye2nfayPRw")%>
                                            </td>
                                            <td style="width:50px;">
                                                <%= BodyTexts.Ref("d5ZE9E74GUS3dwP/bQT1vg")%>
                                            </td>
                                            <td style="width:250px;">
                                                <%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>
                                            </td>
                                        </thead>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="LiteralDeliveryTermsClient" runat="server" Text='<%# Eval("DeliveryTerms")%>'></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="LiteralCompletionDate" runat="server" ></asp:Literal>
                                            </td>
                                            <td>
                                                <div><usercontrol:checkboxBootstrap ID="CheckBoxAktOfWorkItem" runat="server" Disable="true" BootstrapType="3"/></div>
                                                <div><asp:ImageButton ID="ImageButtonClientContractAktToWorkContract" runat="server" CommandName="ClientContractAktToWork"  Visible="false" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" /></div>
                                            </td>
                                            <td>
                                                <asp:Literal ID="LiteralPenaltyClient" runat="server" Text='<%# Eval("Penalties")%>'></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="LiteralPenaltyNotesClient" runat="server" Text='<%# Eval("PenaltiesNote")%>'></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </asp:Panel>
                            <!-- Client Contract Additional Details -->

    <asp:GridView ID="GridViewShowAddendum" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSourceShowAddendum" EnableModelValidation="True" GridLines="None"
        CssClass="table table-nonfluid"  DataKeyNames="AddendumID" onrowdatabound="GridViewShowAddendum_RowDataBound" 
                            onrowupdating="GridViewShowAddendum_RowUpdating" 
                            onrowcommand="GridViewShowAddendum_RowCommand" 
                            onrowdeleting="GridViewShowAddendum_RowDeleting" 
                            onrowupdated="GridViewShowAddendum_RowUpdated" 
                            onrowdeleted="GridViewShowAddendum_RowDeleted" 
                            onrowcreated="GridViewShowAddendum_RowCreated" >
        <Columns>
            <asp:templatefield>

                <ItemTemplate>
                        <asp:Panel ID="panelInItemAddendum" runat="server" >
                         <asp:HoverMenuExtender ID="HoverMenuExtenderItem" runat="server" 
                          PopupPosition="Top" PopupControlID="panelInItemXAddendum" 
                          TargetControlID="panelInItemAddendum" >
                         </asp:HoverMenuExtender>
                             <asp:Literal ID="LiteralReplaceBlock" runat="server"></asp:Literal>
                             <asp:Label ID="LabelPoStatus" runat="server" ></asp:Label>
                        <table >		
                            <tr>
                              <td>
                                <asp:Panel ID="panelInItemXAddendum" runat="server">
                                   <asp:Label ID="LabelHooverToolTipAddendum" runat="server" CssClass="label label-inverse label-xlg" ></asp:Label>
                                </asp:Panel>
                              </td>
                            </tr>	
                        </table >		

                            <asp:Panel ID="PanelCommonInformationAddItem" runat="server" CssClass="panel panel-info " Width="1200px">
                                <div class="panel-heading">
                                    <%= BodyTexts.Ref("Cz1u1YxQmUO8D6vQzUaGEA")%>
                                </div>

                                <div class="panel-body">

                                    <table class="table" >
                                        <thead>
                                            <td>
                                                
                                            </td>
                                            <td>
                                                #
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("Mu+drePgpEK6h8XzFfTOJg")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("b/QwmB2AWEmXNrVeVHPG7g")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("aUlY16DSxEuv5OYTuG/x+g")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("WCc0K46nMU6P4KVjfJyRyQ")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("foHRgj5mokmXEh62sTOoiQ")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("ofeMLaQHw0mejHavxFcTrg")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("vXdfZGm13kewiqmHmdWdyQ")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("hL8TNg+mj02+XJTLz0GqUA")%>
                                            </td>
                                            <td style="display:none;">
                                                <%= BodyTexts.Ref("9jcfRxS3WEqqHigdLtr6BQ")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("1epQY1LCBkqFks1Ea7MVfw")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("69H0Rzyc2k6HgOxJbJQKug")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("mB+gkXNFaEegQ/Gm8GRgsA")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("AG8NVnjS50yHEBirA9v9Jw")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("mXA5GI226kq6JCs9nCrlaQ")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("nEM0zCjdzkGup5oo1REa6g")%>
                                            </td>
                                        </thead>

                                        <tr>
                                            <td style="width:75px;">
                                                <div class="btn-group-vertical" role="group">
                                                      <asp:Button ID="ButtonEdit" runat="server" CausesValidation="False" 
                                                        CommandName="Edit" CssClass="btn btn-minier btn-success " 
                                                        onmouseover="this.style.cursor='hand'" Text="Edit"  />


                                                      <asp:Button ID="ButtonDelete" runat="server" CausesValidation="False" 
                                                        CommandName="Delete" CssClass="btn btn-minier btn-danger" 
                                                        OnClientClick="return confirm('Are you sure you want to delete this record?');" 
                                                        onmouseover="this.style.cursor='hand'" Text="Delete"  />
                                                </div>
                                            </td>
                                            <td style="width:20px;">
                                                    <asp:Label ID="LabelSortNumber" runat="server" class="AddendumColumn"></asp:Label>
                                                  <br />
                                                 <asp:Button ID="ButtonExceptionalApprove" runat="server" CausesValidation="False" CommandName="ExceptionalApproveAddendum"  
                                                  OnClientClick="return confirm('Are you sure you want to approve this addendum EXCEPTIONALLY?');" Visible="false"
                                                  CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Width="50px" Height="40px" CssClass="wrap"
                                                  onmouseover="this.style.cursor='hand'" Text="Exceptional Approve"  />
                                            </td>
                                            <td style="width:80px;">
                                                    <asp:Label ID="LabelPOnoItem" runat="server" BackColor="#FFFF66" 
                                                      Text='<%# Bind("PO_No") %>' Visible="false"></asp:Label>

                                                  <asp:HyperLink ID="HyperlinkPoNo" runat="server" 
                                                  NavigateUrl='<%# "~/invoicedefine.aspx?PoNo=" + Eval("PO_No") %>'
                                                  Text='<%# Bind("PO_No") %>' Visible="false"
                                                  Target="_blank" ></asp:HyperLink>

                                                    <br />
                                                    <asp:HyperLink ID="HyperlinkAddAddendumData" runat="server" Target="_blank" CssClass="btn btn-minier btn-success">
                                                  </asp:HyperLink>
                                            </td>
                                            <td style="width:80px; "">
                                                <asp:Label ID="LabelAddendumNoItem" runat="server" 
                                                  Text='<%# Bind("AddendumNo") %>'></asp:Label>
                                                <asp:Label ID="LabelAddendumIDitem" runat="server" 
                                                  Text='<%# Bind("AddendumID") %>' visible="false"></asp:Label>
                                                <asp:Label ID="LabelCreatedBy" runat="server" Text='<%# Bind("CreatedBy") %>' 
                                                  visible="false"></asp:Label>
                                                <asp:Label ID="LabelUpdatedBy" runat="server" Text='<%# Bind("UpdatedBy") %>' 
                                                  visible="false"></asp:Label>
                                                <asp:Label ID="LabelPersonCreated" runat="server" 
                                                  Text='<%# Bind("PersonCreated") %>' visible="false"></asp:Label>
                                                <asp:Label ID="LabelPersonUpdated" runat="server" 
                                                  Text='<%# Bind("PersonUpdated") %>' visible="false"></asp:Label>
                                            </td>
                                            <td style="width:80px; ">
                                                <asp:Label ID="LabelAddendumDateItem" runat="server" 
                                                  Text='<%# Bind("AddendumDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </td>
                                            <td style="width:100px; ">

                                            </td>
                                            <td style="width:100px; ">
                                                <asp:Label ID="LabelAddendumDescItem" runat="server" 
                                                  Text='<%# Bind("AddendumDescription") %>'></asp:Label>
                                            </td>
                                            <td style="width:50px; ">

                                            </td>
                                            <td style="width:50px; ">
                                                <usercontrol:checkboxBootstrap ID="CheckboxBootstrapAddSuppSgn" runat="server" 
                                                    Checked='<%# Bind("AddendumSignBySupplier")%>' _Disable="true"/>

                                            </td>
                                            <td style="width:50px; ">
                                                <usercontrol:checkboxBootstrap ID="CheckboxBootstrapAddMercSgn" runat="server" 
                                                    Checked='<%# Bind("AddendumSignByMercury")%>' _Disable="true"/>

                                            </td>
                                            <td style="display:none;">
                                            </td>
                                            <td style="width:100px; ">
                                                <asp:Label ID="LabelAddendumGivenTo" runat="server" 
                                                  Text='<%# Bind("AddendumGivenTo") %>'></asp:Label>
                                            </td>
                                            <td style="width:85px; font-size:10px; text-align:right;">
                                                <span style="font-style:italic; color:Gray;">
                                                 <asp:literal ID="LiteralAddendumValueExcVATItem" Text="Exc. VAT"  runat="server" ></asp:literal>
                                                </span>
                                                <asp:Label ID="LabelContractValueItem" runat="server" 
                                                  Text='<%# Bind("AddendumValue_woVAT", "{0:###,###,###.00}") %>'></asp:Label>
                                                    <br />

                                                <span style="font-style:italic; color:Gray;">
                                                 <asp:literal ID="LiteralAddendumValueWithVATItem" Text="With VAT"  runat="server" ></asp:literal>
                                                </span>
                                                <asp:literal ID="LiteralAddendumValueWithVATItemValue" Text='<%# Bind("AddendumValue_withVAT", "{0:###,###,###.00}") %>'  runat="server" >
                                                </asp:literal>
                                                <br />

                                                <span style="font-style:italic; color:Gray;">
                                                 <asp:literal ID="LiteralVATItem" Text="VAT %"  runat="server" ></asp:literal>
                                                </span>
                                                <asp:literal ID="LiteralVATItemValue" Text='<%# Bind("VATpercent", "{0:###,###,###.00}") %>' runat="server" >
                                                </asp:literal>

                                                <%--this should be remove from data table as well--%>
                                                <div>
                                                    <span 
                                                        style="font-style:italic; color:Gray;">
                                                    <asp:literal ID="LiteralBudgetTitle" Text="Budget With VAT"  runat="server" ></asp:literal>
                                                    </span>
                                                    <asp:literal ID="LiteralBudgetValue" Text='<%# Bind("Budget", "{0:###,###,###.00}")%>'  runat="server" >
                                                    </asp:literal>

                                                <br />
                                                            <asp:ImageButton ID="ImageButtonBudgetPDF" runat="server" data-rel="tooltip" data-placement="top" class="tooltip-success" data-original-title="Budget Document"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BudgetLinkToPDF")%>' 
                                                                CommandName="OpenBudgetPDF" />
                                                </div>
                                                <%--------------this should be remove from data table as well--%>

                                            </td>
                                            <td style="width:50px; ">

                                            </td>
                                            <td style="width:50px; ">
                                                <asp:Label ID="LabelRetention" runat="server" 
                                                  Text='<%# Bind("AddendumRetention") %>'></asp:Label>
                                            </td>
                                            <td style="width:50px; ">
                                                <usercontrol:checkboxBootstrap ID="CheckboxBootstrapAddArch" runat="server" 
                                                    Checked='<%# Bind("AddendumArchivedByMercury")%>' _Disable="true"/>

                                            </td>
                                            <td style="width:100px; ">
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("AddendumNote") %>'></asp:Label>
                                            </td>
                                            <td>

                                                <table>
                                                    <tr>
                                                        <td class="td_fl_mng">
                                                          <asp:ImageButton ID="ImageButtonLinkToTemplatefile_DOC" runat="server" 
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AddendumLinkToTemplatefile_DOC") %>' 
                                                            CommandName="OpenDOC" />

                                                        </td>
                                                        <td class="td_fl_mng">
                                                          <asp:ImageButton ID="ImageButtonLinkToPDFcopy" runat="server" 
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AddendumLinkToPDFcopy") %>' 
                                                            CommandName="OpenPDF" />
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
				                        </table>

                                <table id="tblAddCommonNewGN" runat="server" class="table hide">
                                 <tr>
                                <td>
                                  <usercontrol:ImageUserPhoto ID="ImageRequestedBy" runat="server" UserName='<%# Eval("RequestedBy")%>' />
                                </td>
                                   <td style="padding-left: 150px; vertical-align: top;">
                                  <asp:Gridview ID="GridviewMissingItemsForApproval" runat="server" 
                                    CssClass="GridviewMissingBeforeApproval" DataSourceID="SqlDataSourceMissingItemsForApproval"
                                    AutoGenerateColumns="False" >
                                    <Columns>
                                      <asp:TemplateField HeaderText="Required Items Before Approval" ItemStyle-Width="120px">
                                        <ItemTemplate>
                                         <asp:Literal id="LiteralTitle" runat="server" Text='<%# "• " + Eval("Title") %>'></asp:Literal>
                                        </ItemTemplate>
                                      </asp:TemplateField>
                                    </Columns>
                                  </asp:Gridview>
                                  <asp:SqlDataSource ID="SqlDataSourceMissingItemsForApproval" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                    SelectCommand="AddendumReadyForApproval" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                      <asp:Parameter DefaultValue="0" Name="AddendumID" Type="Int32" />
                                      <asp:Parameter DefaultValue="1" Name="Shift" Type="Int32" />
                                    </SelectParameters>
                                  </asp:SqlDataSource>
                                <%------------------------------------------------------------ MISSING ITEMS FOR APPROVAL --%>
                                   </td>
                                   <td style="padding-left: 50px; vertical-align: top;">
                                  <asp:Gridview ID="GridviewApprovalStatusAddendum" runat="server" 
                                    DataSourceID="SqlDataSourceApprovalStatus" CssClass="table table-nonfluid borderless" GridLines="None"
                                    AutoGenerateColumns="False" onrowcommand="GridviewApprovalStatusAddendum_RowCommand" 
                                       onrowdatabound="GridviewApprovalStatusAddendum_RowDataBound">
                                    <Columns>
                                      <asp:TemplateField >
                                        <ItemTemplate>
                                         <asp:Literal id="LiteralUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Literal>
                                         <asp:Literal id="LiteralWhichLawyer" Visible="false" runat="server" Text='<%# Bind("WhoApprovedFromLawyers")%>'></asp:Literal>
                                        </ItemTemplate>
                                      </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Approved" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <%-- LiteralApproved and LiteralContractID should stay here, they are being used in RowCommand --%>
                                        <asp:Literal id="LiteralApproved" runat="server" Text='<%# Bind("Approved") %>' Visible="false"></asp:Literal>
                                        <asp:Literal id="LiteralAddendumID" runat="server" Text='<%# Bind("AddendumID") %>' Visible="false"></asp:Literal>
                                        <asp:Literal id="LiteralRejectContractGirls" runat="server" Text='<%# Bind("Exception") %>' Visible="false"></asp:Literal>
                                        <asp:ImageButton ID="ImageButtonApproval" runat="server" CommandName="Approval" 
                                        CommandArgument='<%# Container.DataItemIndex %>' 
                                         CausesValidation="False" />
                                        <asp:LinkButton ID="LinkButtonRejectContractGirls" runat="server" CommandName="ApprovalByRejecting" 
                                        CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="False"  />
                                        </ItemTemplate>
                                      </asp:TemplateField>

                                      <asp:TemplateField HeaderText="When Approved">
                                        <ItemTemplate>
                                         <asp:Literal id="LiteralWhenApproved" runat="server" Text='<%# Bind("WhenApproved", "{0:dd/MM/yyyy}")%>'></asp:Literal>
                                        </ItemTemplate>
                                      </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="hide"/>
                                  </asp:Gridview>
                                  <asp:SqlDataSource ID="SqlDataSourceApprovalStatus" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                    SelectCommand="ApprovalStatusAddendum" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                      <asp:Parameter DefaultValue="0" Name="AddendumID" Type="Int32" />
                                      <asp:Parameter DefaultValue="1" Name="Shift" Type="Int32" />
                                    </SelectParameters>
                                  </asp:SqlDataSource>
                                <%----------------------------------------------------------------- APPROVAL MATRIX --%>
                                   </td>
                                   <td style="padding-left:10px;">
                                    <asp:Panel ID="PanelAddendumType" runat="server" Class="PanelAddendumType">
                                      <asp:Label ID="LiteralAddendumType" runat="server" CssClass="label label-info label-xlg tooltip-success" data-rel="tooltip" data-placement="top"  ></asp:Label>
                                    </asp:Panel>
                                   </td>
                                 </tr>
                                </table>

                                </div>

                            </asp:Panel>


                            <asp:Panel ID="PanelCommercialTermsAddendumItem" runat="server" CssClass="panel panel-info float_left hide" Width="1000px">
                                <div class="panel-heading">
                                    <%= BodyTexts.Ref("1lBVf/JgM0yopXMK71U1xA")%>
                                    <span style="float:right;"><asp:literal ID="LiteralCostCode" Text='<%# Bind("CostCode")%>' runat="server" ></asp:literal></span>
                                </div>

                                <div class="panel-body">

                                    <table class="table">
                                        <thead>
                                            <td>
                                                <%= BodyTexts.Ref("hEhO9xKZVke1gtHcr6u2Gg")%>:	
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("eOZry6gjB0yqlsMmUT7/aw")%>:	
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("WBn92MUF20iopsBEY2+rxA")%>:	
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>:
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("uzuez9RR302+6q9tOPvnJw")%>:	
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>:
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("76jVcY8TbUSVUcJGrpnfhg")%>:	
                                            </td>
                                            <td>
                                                <%= BodyTexts.Ref("Vh6I6dyPyU+d+3zCSzOyhQ")%>:	
                                            </td>
                                        </thead>
                                        <tr>
                                            <td style="width:75px;">
                                              <asp:literal ID="LiteralStartDate" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy}") %>' runat="server" ></asp:literal>
                                            </td>
                                            <td style="width:75px;">
                                              <asp:literal ID="LiteralFinishDate" Text='<%# Bind("FinishDate", "{0:dd/MM/yyyy}") %>' runat="server" ></asp:literal>
                                            </td>
                                            <td style="width:75px;">
                                              <asp:literal ID="LiteralPenalties" Text='<%# Bind("Penalties") %>' runat="server" ></asp:literal>
                                            </td>
                                            <td style="width:200px;">
                                              <asp:literal ID="LiteralPenaltyNote" Text='<%# Bind("PenaltiesNote")%>' runat="server" ></asp:literal>
                                            </td>
                                            <td style="width:75px;">
                                              <asp:Literal ID="LiteralPenaltiesToSupplier" runat="server" Text='<%# Bind("PenaltiesToSupplier")%>'></asp:Literal>
                                            </td>
                                            <td style="width:200px;">
                                              <asp:Literal ID="LiteralPenaltyNoteToSupplier" runat="server" Text='<%# Bind("PenaltiesToSupplierNote")%>'></asp:Literal>
                                            </td>
                                            <td style="width:150px;">
                                              <asp:literal ID="LiteralDeliveryTerms" Text='<%# Bind("DeliveryTerms") %>' runat="server" ></asp:literal>
                                            </td>
                                            <td style="width:100px;">
                                              <asp:literal ID="LiteralGuaranteePeriod" Text='<%# Bind("GuaranteePeriod") %>' runat="server" ></asp:literal>
                                            </td>
                                        </tr>

                                    </table>

                                </div>

                                <div class="panel-heading">
                                    <%= BodyTexts.Ref("IeKLbyowHECapdUmB+qKrw")%>
                                </div>

                                <div class="panel-body">

                                  <table class="table">

                                      <thead>
                                          <td>
                                              <%= BodyTexts.Ref("wt6Qt9QUh0GDaiAKFZhS0A")%>
                                          </td>
                                          <td>
                                              <%= BodyTexts.Ref("L4img0LG+06x0/+AqzBnkQ")%>
                                          </td>
                                          <td>
                                              <%= BodyTexts.Ref("3faC83SQyUS74LvrVeBEgA")%>
                                          </td>
                                          <td>
                                              <%= BodyTexts.Ref("CdFoIWOhUUq43gPj9azb9Q")%>
                                          </td>
                                          <td>
                                              <%= BodyTexts.Ref("v4yTZj0Pi0CtAif6PQDZ7w")%>
                                          </td>
                                      </thead>

                                      <tr>
                                          <td>
                                            <asp:Literal ID="LiteralAdvance" runat="server" Text='<%# Bind("Advance", "{0:###,###,###.0} %")%>'></asp:Literal>
                                          </td>
                                          <td>
                                            <asp:Literal ID="LiteralInterim" runat="server" Text='<%# Bind("Interim", "{0:###,###,###.0} %")%>'></asp:Literal>
                                          </td>
                                          <td>
                                            <asp:Literal ID="LiteralShipment" runat="server" Text='<%# Bind("Shipment", "{0:###,###,###.0} %")%>'></asp:Literal>
                                          </td>
                                          <td>
                                            <asp:Literal ID="LiteralDelivery" runat="server" Text='<%# Bind("Delivery", "{0:###,###,###.0} %")%>'></asp:Literal>
                                          </td>
                                          <td>
                                            <asp:Literal ID="LiteralRetention" runat="server" Text='<%# Bind("AddendumRetention", "{0:###,###,###.0} %")%>'></asp:Literal>
                                          </td>
                                      </tr>

                                  </table>

                                </div>

                            </asp:Panel>

                    <asp:Panel ID="PanelTagAddendum" CssClass="panel panel-info float_left " Width="200px" runat="server" Visible="false">
                        <div class="panel-heading">
                            Tags
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="TxtBoxTagsAddendum" runat="server"></asp:TextBox>
                        </div>
                        <asp:HiddenField ID="HdAddendumId" runat="server" Value='<%# Eval("AddendumId")%>' />
                    </asp:Panel>                 

                            <!-- Client Contract Additional Details -->
                            <asp:Panel ID="PanelClientAddendumAdditionalDetails" runat="server" Visible="false" CssClass="panel panel-info " Width="710px">
                                <div class="panel-heading">
                                    <%= BodyTexts.Ref("VP2VuGvsMkepSyxNTrK95w")%>
                                </div>

                                <div class="panel-body">
                                    <table class="table">
                                        <thead>
                                            <td style="width:250px;">
                                                <%= BodyTexts.Ref("76jVcY8TbUSVUcJGrpnfhg")%>
                                            </td>
                                            <td style="width:80px;">
                                                <%= BodyTexts.Ref("RpoOSVLThUeJmnFtjELvww")%>
                                            </td>
                                            <td style="width:80px;">
                                                <%= BodyTexts.Ref("Kd8jnLnalkSmye2nfayPRw")%>
                                            </td>
                                            <td style="width:50px;">
                                                <%= BodyTexts.Ref("d5ZE9E74GUS3dwP/bQT1vg")%>
                                            </td>
                                            <td style="width:250px;">
                                                <%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>
                                            </td>
                                        </thead>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="LiteralDeliveryTermsClient" runat="server" Text='<%# Eval("DeliveryTerms")%>'></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="LiteralCompletionDate" runat="server" ></asp:Literal>
                                            </td>
                                            <td>
                                                <usercontrol:checkboxBootstrap ID="CheckBoxAktOfWorkItem" runat="server" Disable="true" BootstrapType="3"/>
                                                <br /><br />
                                                <asp:ImageButton ID="ImageButtonClientContractAktToWorkAddendum" runat="server" CommandName="ClientAddendumAktToWork"  Visible="false" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                            </td>
                                            <td>
                                                <asp:Literal ID="LiteralPenaltyClient" runat="server" Text='<%# Eval("Penalties")%>'></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="LiteralPenaltyNotesClient" runat="server" Text='<%# Eval("PenaltiesNote")%>'></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </asp:Panel>
                            <!-- Client Contract Additional Details -->


                        <div class="showMoreLessAdd" >
                            <a class="icon-animated-vertical"> > > > Показать больше...</a>
                        </div>

                    </asp:Panel>                 

                </ItemTemplate>

                <EditItemTemplate>
                        <div style="text-align: center"><asp:Label ID="LabelScenarioOutOfRange" runat="server" Text=""  ForeColor="#FF0066" Font-Bold="True" Font-Size="12px" /></div>
                            <asp:Label ID="LabelPoNoBeforeEdit" runat="server" CssClass="hidepanel" ></asp:Label>
                            <asp:DropDownList ID="DropDownListPOnoEdit" runat="server" DataSourceID="SqlDataSourcePoNo" DataTextField="Information" 
                              DataValueField="PO_No" Font-Names="Consolas" Font-Size="11px" Width="1120px" BackColor="#FFFF66">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourcePoNo" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
SelectCommand="SELECT N'' As PO_No, 'Select Po No' as Information

UNION ALL

SELECT     dbo.Table2_PONo.PO_No, REPLACE(RTRIM(dbo.Table2_PONo.PO_No) 
                      + N' ' + RIGHT('00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000' + CAST(dbo.Table2_PONo.Description
                       AS varchar(140)), 140) + N' &gt; ' + CASE LEN(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT))) WHEN 7 THEN LEFT(rtrim(CONVERT(nchar(20), 
                      dbo.View_PoSumExcVAT.PoSumExcVAT)), 1) + ',' + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 6) 
                      WHEN 8 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 2) + ',' + RIGHT(rtrim(CONVERT(nchar(20), 
                      dbo.View_PoSumExcVAT.PoSumExcVAT)), 6) WHEN 9 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 3) 
                      + ',' + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 6) WHEN 10 THEN LEFT(rtrim(CONVERT(nchar(20), 
                      dbo.View_PoSumExcVAT.PoSumExcVAT)), 1) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 2, 3) 
                      + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 5, 3) + '.' + + RIGHT(rtrim(CONVERT(nchar(20), 
                      dbo.View_PoSumExcVAT.PoSumExcVAT)), 2) WHEN 11 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 2) 
                      + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 3, 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), 
                      dbo.View_PoSumExcVAT.PoSumExcVAT)), 6, 3) + '.' + + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 2) 
                      WHEN 12 THEN LEFT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), 
                      dbo.View_PoSumExcVAT.PoSumExcVAT)), 4, 3) + ',' + + SUBSTRING(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 7, 3) 
                      + '.' + + RIGHT(rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)), 2) ELSE rtrim(CONVERT(nchar(20), dbo.View_PoSumExcVAT.PoSumExcVAT)) 
                      END + N' ' + RTRIM(dbo.View_PoSumExcVAT.PO_Currency) + N' exc.VAT', ' ', '_') AS Information
FROM         dbo.Table2_PONo INNER JOIN
                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                      dbo.View_PoSumExcVAT ON dbo.Table2_PONo.PO_No = dbo.View_PoSumExcVAT.PO_No INNER JOIN
                      dbo.Table_Contracts ON dbo.Table6_Supplier.SupplierID = dbo.Table_Contracts.SupplierID INNER JOIN
                      dbo.Table_Addendums ON dbo.Table_Contracts.ContractID = dbo.Table_Addendums.ContractID
WHERE     (dbo.Table2_PONo.Project_ID = @Project_ID) AND (dbo.Table_Addendums.AddendumID = @AddendumID)">
<SelectParameters>
 <asp:Parameter Name="Project_ID" Type="Int32" />
 <asp:Parameter Name="AddendumID" Type="Int32"/>
</SelectParameters>
</asp:SqlDataSource>
<asp:Label ID="LabelWarningPoDublication" runat="server"></asp:Label>

                        <table style="background-color: #E6E6FA">
                            <tr>
                                <td style="width:40px;">
                                    <div class="btn-group-vertical" role="group" >
                                                <asp:Button ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update" CssClass="btn btn-minier btn-primary" Text="Update"  />

                                                <asp:Button ID="ButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel" CssClass="btn btn-minier btn-danger " Text="Cancel" />
                                    </div>
                                </td>
                                <td style="width:20px;"></td>
                                <td style="width:80px; text-align: center;">
                                    <div>
                                        <asp:DropDownList ID="DDLContractID" runat="server" CssClass="EditContract" DataSourceID="SqlDataSourceContractID" DataTextField="ContractNo" DataValueField="ContractID" Font-Size="9px" Height="20px" width="75px">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSourceContractID" runat="server" ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" SelectCommand="SELECT     dbo.Table_Contracts.ContractID, RTRIM(dbo.Table_Contracts.ContractNo) AS ContractNo,
                                                                                     dbo.Table1_Project.ProjectID
                                                                                    FROM         dbo.Table_Contracts INNER JOIN
                                                                                    dbo.Table1_Project ON dbo.Table_Contracts.ProjectID = dbo.Table1_Project.ProjectID
                                                                                    WHERE     (dbo.Table1_Project.ProjectID = @ProjectID)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="DropDownListPrjID" Name="ProjectID" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </td>
                                <td style="width:80px; text-align: center;">

                                   <asp:Panel ID="HideFromUser" runat="server" CssClass="ContractEditTitles">
                                    <asp:Literal ID="LiteralAddenumID" runat="server" Text='<%# Bind("AddendumID") %>'></asp:Literal>
                                    <asp:Literal ID="LiteralPOexecuted" runat="server" Text='<%# Bind("POexecuted") %>'></asp:Literal>
                                   </asp:Panel>

                                    <asp:Label ID="LabelAddendumNoEdit" runat="server" CssClass="ContractEditTitles"  >No</asp:Label>
                                    <br />

                                    <asp:TextBox ID="TextBoxAddendumNoEdit" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("AddendumNo") %>' width="75px"></asp:TextBox>
                                    <asp:Label ID="LabelScenario" runat="server" CssClass="hidepanel" Text='<%# Bind("Scenario") %>'></asp:Label>
                                </td>
                                <td style="width:80px; text-align: center;">
                                    <asp:Label ID="LabelDateEdit" runat="server" CssClass="ContractEditTitles"  >Date</asp:Label>
                                    <br />

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorContractDate" runat="server" ControlToValidate="TextBoxAddendumDateEdit" Display="Dynamic" ErrorMessage="dd/mm/yyyy" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="TextBoxAddendumDateEdit" runat="server" CssClass="EditContract add_datepicker" Text='<%# Bind("AddendumDate", "{0:dd/MM/yyyy}")%>' width="75px"></asp:TextBox>

                                </td>
                                <td style="width:135px; text-align: left;"></td>
                                <td style="width:130px; text-align: center;">
                                    <asp:Label ID="LabelDescriptionEdit" runat="server" CssClass="ContractEditTitles"  >Description</asp:Label>
                                    <br />

                                    <asp:TextBox ID="TextBoxAddendumDescriptionEdit" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("AddendumDescription") %>' TextMode="MultiLine" width="125px"></asp:TextBox>
                                </td>
                                <td style="width:50px; text-align: center;"></td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelSignSup" runat="server" CssClass="ContractEditTitles"  >Sign Sup.</asp:Label>
                                    <br />

                                    <asp:CheckBox ID="SignBySupplierCheckBoxEdit" runat="server" Checked='<%# Bind("AddendumSignBySupplier") %>' />
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelSignMercEdit" runat="server" CssClass="ContractEditTitles"  >Sign Merc.</asp:Label>
                                    <br />

                                    <asp:CheckBox ID="SignByMercuryCheckBoxEdit" runat="server" Checked='<%# Bind("AddendumSignByMercury") %>' />
                                </td>
                                <td style="width:50px; text-align: center; display: none;">
                                    <asp:CheckBox ID="CollectionBySupplierCheckBoxEdit" runat="server" Checked='<%# Bind("AddendumCollectionBySupplier") %>' />
                                </td>
                                <td style="width:100px; text-align: center;">
                                    <asp:Label ID="LabelGivenToEdit" runat="server" CssClass="ContractEditTitles"  >Given To</asp:Label>
                                    <br />

                                    <asp:TextBox ID="TextBoxAddendumGivenTo" runat="server" CssClass="EditContract" Height="40px" Text='<%# Bind("AddendumGivenTo") %>' TextMode="MultiLine" width="75px"></asp:TextBox>
                                </td>
                                <td style="width:85px; text-align: center;">
                                    <span class="ContractEditTitles"><asp:Literal ID="LiteralLiteralAddendumValueExcVAT" runat="server" Text="Exc. VAT"></asp:Literal></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorContractValue" runat="server" ControlToValidate="TextBoxAddendumValueEdit" Display="Dynamic" ErrorMessage="Wrong format" ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="TextBoxAddendumValueEdit" runat="server" CssClass="EditContract" Text='<%# Bind("AddendumValue_woVAT") %>' width="75px"></asp:TextBox>
                                    <br />
                                    <span class="ContractEditTitles"><asp:Literal ID="LiteralAddendumValueWithVAT" runat="server" Text="With VAT"></asp:Literal></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorAddendumValueWithVATEdit" runat="server" ControlToValidate="TextBoxAddendumValueWithVATEdit" Display="Dynamic" ErrorMessage="Wrong format" ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddendumValueWithVATEdit" runat="server" ControlToValidate="TextBoxAddendumValueWithVATEdit" Display="Dynamic" ErrorMessage="Required">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TextBoxAddendumValueWithVATEdit" runat="server" CssClass="EditContract" Text='<%# Bind("AddendumValue_WithVAT") %>' width="75px"></asp:TextBox>
                                    <br />
                                    <span class="ContractEditTitles"><asp:Literal ID="LiteralVAT" runat="server" Text="VAT %"></asp:Literal></span>
                                    <asp:FilteredTextBoxExtender ID="TextBoxVATpercent_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="TextBoxVAT">
                                    </asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxVAT" runat="server" ControlToValidate="TextBoxVAT" Display="Dynamic" ErrorMessage="Required">
                                    </asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidatorTextBoxVAT" runat="server" ControlToValidate="TextBoxVAT" Display="Dynamic" ErrorMessage="range to be 0-20" MaximumValue="20" MinimumValue="0" Type="Double">
                                    </asp:RangeValidator>
                                    <asp:TextBox ID="TextBoxVAT" runat="server" CssClass="EditContract" Text='<%# Bind("VATpercent") %>' width="75px">
                                    </asp:TextBox>

                                    <%--this to be removed completely later on--%>
                                    <div class="hidden">
                                        <span class="ContractEditTitles"><asp:literal ID="LiteralBudgetEdit" Text="Budget With VAT" runat="server" ></asp:literal></span>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorBudgetEdit" 
                                            runat="server" ControlToValidate="TextBoxBudget" Display="Dynamic" 
                                            ErrorMessage="Wrong format" 
                                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="TextBoxBudget" runat="server" 
                                            CssClass="EditContract" Text='<%# Bind("Budget")%>' width="75px"></asp:TextBox>

                                        <br />

                                        <div>
                                            <asp:Label ID="labelBudgetInfo" runat="server" CssClass="LabelGeneral"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="LabelBudgetTitle" runat="server" Text="Budget File" Width="100px" CssClass="ContractEditTitles" />
                                        </div>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LabelBudgetDeleteDoc" runat="server" ForeColor="#3366FF" Text="Delete Budget File?" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBoxBudgetDeleteDoc" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="TextBoxBudgetLinkPDF" runat="server" CssClass="hidepanel" Text='<%# Bind("BudgetLinkToPDF")%>' />
                                            <asp:FileUpload ID="FileUploadBudgetPDF" runat="server" CssClass="EditContract" Width="80px" />
                                        </div>
                                        <div>
                                            <asp:Button ID="ButtonBudgetPDFUpload" runat="server" CausesValidation="False" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="UploadBudgetPDFAddendum" CssClass="DDLEditContract" Text="Upload Budget PDF" />
                                        </div>
                                    </div>
                                    <%--this to be removed completely later on--%>

                                </td>
                                <td style="width:50px; text-align: center;"></td>
                                <td style="width:20px; text-align: center;">
                                    <asp:Label ID="LabelRetentionEdit" runat="server" CssClass="ContractEditTitles"  >Retention</asp:Label>
                                    <br />
                                  <asp:TextBox ID="TextBoxRetention" runat="server" CssClass="EditContract" Text='<%# Bind("AddendumRetention")%>' width="50px" />
                                  <asp:RangeValidator ID="RangeValidatorRetention" runat="server" ControlToValidate="TextBoxRetention" 
                                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                                      MinimumValue="0" Type="Double">
                                  </asp:RangeValidator>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorRetention" runat="server" ControlToValidate="TextBoxRetention" 
                                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                                  </asp:RegularExpressionValidator>


                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelArchievedEdit" runat="server" CssClass="ContractEditTitles"  >Archived</asp:Label>
                                    <br />

                                    <asp:CheckBox ID="ArchivedByMercuryCheckBoxEdit" runat="server" Checked='<%# Bind("AddendumArchivedByMercury") %>' />
                                </td>
                                <td style="width:100px; text-align: center;">
                                    <asp:Label ID="LabelNoteEdit" runat="server" CssClass="ContractEditTitles"  >Note</asp:Label>
                                    <br />

                                    <asp:TextBox ID="TextBoxNoteEdit" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("AddendumNote") %>' TextMode="MultiLine" width="115px"></asp:TextBox>
                                </td>
                                <td>
                                                <table>
                                                    <tr>
                                                        <td class="td_fl_mng">
                                                            <asp:ImageButton ID="ImageButtonLinkToTemplatefile_DOC" runat="server" 
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AddendumLinkToTemplatefile_DOC") %>' 
                                                                CommandName="OpenDOCedit" />

                                                        </td>
                                                        <td class="td_fl_mng">
                                                            <asp:ImageButton ID="ImageButtonLinkToPDFcopy" runat="server" 
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AddendumLinkToPDFcopy") %>' 
                                                                CommandName="OpenPDFedit" />
                                                        </td>
                                                    </tr>
                                                </table>

                                        <asp:TextBox ID="LinkToTemplatefile_DOCTextBoxEdit" runat="server" Visible="false" Text='<%# Bind("AddendumLinkToTemplatefile_DOC") %>' />
                                        <asp:TextBox ID="LinkToPDFcopyTextBoxEdit" runat="server" Visible="false" Text='<%# Bind("AddendumLinkToPDFcopy") %>' />

                                </td>
                            </tr>

                        </table>

      <!---- COMMERCIAL TERMS EDIT MODE-------------------------------------------------------->
      <asp:Panel ID="PanelCommercialTermsAddendumEdit" runat="server" >
      <table style="background-color: #CCCCCC">
       <tr>
           <th colspan="12" style="text-align:center; color:Red;">
               <asp:Literal ID="LiteralCommercialRoleTitle" runat="server">
        </asp:Literal>
           </th>
           <tr>
               <td class="CommercialTitles"><%= BodyTexts.Ref("hEhO9xKZVke1gtHcr6u2Gg")%>: </td>
               <td style="padding:2px;">
                   <asp:TextBox ID="TextBoxStartDate" runat="server" CssClass="EditContract add_datepicker" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy}")%>' width="75px"></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidatorStartDate" runat="server" ControlToValidate="TextBoxStartDate" Display="Dynamic" ErrorMessage="dd/mm/yyyy" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}">
                   </asp:RegularExpressionValidator>
               </td>
               <td class="CommercialTitles"><%= BodyTexts.Ref("WBn92MUF20iopsBEY2+rxA")%>: </td>
               <td style="padding:2px;">
                   <asp:DropDownList ID="DropDownListPenalty" runat="server" CssClass="EditContract" >
                       <asp:ListItem Value="">-</asp:ListItem>
                       <asp:ListItem Value="1">Yes</asp:ListItem>
                       <asp:ListItem Value="0">No</asp:ListItem>
                   </asp:DropDownList>
               </td>
               <td class="CommercialTitles" rowspan="2"><%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>:</td>
               <td rowspan="2">
                   <asp:TextBox ID="TextBoxPenaltyNote" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("PenaltiesNote")%>' TextMode="MultiLine" width="125px">
                                    </asp:TextBox>
                   <br />
                   <asp:Label ID="LabelValidationPenaltyMercuryNote" runat="server" ForeColor="Red" ></asp:Label>
               </td>
               <td class="CommercialTitles" rowspan="2"><%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>:</td>
               <td rowspan="2">
                   <asp:TextBox ID="TextBoxPenaltyNoteSupplier" runat="server" CssClass="EditContract" Height="40" 
                       Text='<%# Bind("PenaltiesToSupplierNote")%>' TextMode="MultiLine" width="125px"></asp:TextBox>
                   <br />
                   <asp:Label ID="LabelValidationPenaltySupplierNote" runat="server" ForeColor="Red" ></asp:Label>
               </td>
               <td class="CommercialTitles" rowspan="2"><%= BodyTexts.Ref("76jVcY8TbUSVUcJGrpnfhg")%>: </td>
               <td rowspan="2" style="padding:2px; width:150px;">
                   <asp:TextBox ID="TextBoxDeliveryTerms" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("DeliveryTerms") %>' TextMode="MultiLine" width="125px">
                   </asp:TextBox>
               </td>
               <td class="CommercialTitles" rowspan="2"><%= BodyTexts.Ref("Vh6I6dyPyU+d+3zCSzOyhQ")%>: </td>
               <td rowspan="2" style="padding:2px; width:150px;">
                   <asp:TextBox ID="TextBoxGuaranteePeriod" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("GuaranteePeriod") %>' TextMode="MultiLine" width="125px">
                                    </asp:TextBox>
               </td>
           </tr>
           <tr>
               <td class="CommercialTitles"><%= BodyTexts.Ref("eOZry6gjB0yqlsMmUT7/aw")%>: </td>
               <td style="padding:2px;">
                   <asp:TextBox ID="TextBoxFinishDate" runat="server" CssClass="EditContract add_datepicker" Text='<%# Bind("FinishDate", "{0:dd/MM/yyyy}")%>' width="75px"></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidatorFinishDate" runat="server" ControlToValidate="TextBoxFinishDate" Display="Dynamic" ErrorMessage="dd/mm/yyyy" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}">
                                    </asp:RegularExpressionValidator>
               </td>
               <td class="CommercialTitles"><%= BodyTexts.Ref("uzuez9RR302+6q9tOPvnJw")%>:</td>
               <td style="padding:2px;">
                   <asp:DropDownList ID="DropDownListPenaltyToSupplier" runat="server" CssClass="EditContract" >
                       <asp:ListItem Value="">-</asp:ListItem>
                       <asp:ListItem Value="1">Yes</asp:ListItem>
                       <asp:ListItem Value="0">No</asp:ListItem>
                   </asp:DropDownList>
               </td>
           </tr>
       </tr>
          <tr>
              <td class="CommercialTitles"><%= BodyTexts.Ref("wt6Qt9QUh0GDaiAKFZhS0A")%>: </td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxAdvance" runat="server" CssClass="EditContract" Text='<%# Bind("Advance") %>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorAdvance" runat="server" ControlToValidate="TextBoxAdvance" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorAdvance" runat="server" ControlToValidate="TextBoxAdvance" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td class="CommercialTitles"><%= BodyTexts.Ref("L4img0LG+06x0/+AqzBnkQ")%>:</td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxInterim" runat="server" CssClass="EditContract" Text='<%# Bind("Interim")%>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorInterim" runat="server" ControlToValidate="TextBoxInterim" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorInterim" runat="server" ControlToValidate="TextBoxInterim" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td class="CommercialTitles"><%= BodyTexts.Ref("3faC83SQyUS74LvrVeBEgA")%>:</td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxShipment" runat="server" CssClass="EditContract" Text='<%# Bind("Shipment")%>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorShipment" runat="server" ControlToValidate="TextBoxShipment" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorShipment" runat="server" ControlToValidate="TextBoxShipment" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td class="CommercialTitles"><%= BodyTexts.Ref("CdFoIWOhUUq43gPj9azb9Q")%>:</td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxDelivery" runat="server" CssClass="EditContract" Text='<%# Bind("Delivery")%>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorDelivery" runat="server" ControlToValidate="TextBoxDelivery" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorDelivery" runat="server" ControlToValidate="TextBoxDelivery" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td >
                  <asp:Label ID="LabelPaymentTermsValidationNotification" runat="server" ForeColor="Red" Font-Bold="true" Visible="false">
                      Total of Payment Terms exceeds 100%. Please check it again.
                  </asp:Label>
              </td>
              <td >

              </td>
              <td >

              </td>
              <td >

              </td>
          </tr>
      </table>
      </asp:Panel>
      <!---------------------------------------------------------- COMMERCIAL TERMS EDIT MODE---->
                
                            <!-- Client Contract Additional Details -->
                            <asp:Panel ID="PanelClientAddendumAdditionalDetailsEdit" runat="server" Visible="false" CssClass="panel panel-info " Width="550px">
                                <div class="panel-heading">
                                    <%= BodyTexts.Ref("VP2VuGvsMkepSyxNTrK95w")%>
                                </div>

                                <div class="panel-body">
                                    <table class="table">
                                        <thead>
                                            <td style="width:250px;">
                                                <%= BodyTexts.Ref("76jVcY8TbUSVUcJGrpnfhg")%>
                                            </td>
                                            <td style="width:80px;">
                                                <%= BodyTexts.Ref("RpoOSVLThUeJmnFtjELvww")%>
                                            </td>
                                            <td style="width:80px;">
                                                <%= BodyTexts.Ref("Kd8jnLnalkSmye2nfayPRw")%>
                                            </td>
                                            <td style="width:50px;">
                                                <%= BodyTexts.Ref("d5ZE9E74GUS3dwP/bQT1vg")%>
                                            </td>
                                            <td style="width:250px;">
                                                <%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>
                                            </td>
                                        </thead>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBoxDeliveryTermEdit" runat="server"
                                                    CssClass="TextBoxContract" Height="75px"
                                                    Text='<%# Bind("DeliveryTerms")%>' TextMode="MultiLine"
                                                    Width="200px" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxCompletionDateEdit" runat="server" Width="80px"
                                                    CssClass="TextBoxGeneralRev add_datepicker"  />

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCompletionDateEdit" runat="server"
                                                    ControlToValidate="TextBoxCompletionDateEdit"
                                                    ErrorMessage="dd/mm/yyyy" Display="Dynamic"
                                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <usercontrol:checkboxBootstrap ID="CheckBoxAktOfWorkEdit" runat="server" BootstrapType="3"/>
                                                <br /><br />
                                                <asp:FileUpload ID="FileUploadAktOfWorkAddendum" runat="server" CssClass="btn btn-minier"/>
                                                <asp:Button ID="ButtonFileUpload" runat="server" CssClass="btn btn-mini btn-success" Text="upload file" 
                                                    CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="AktOfWorkAddendum" />
                                                <asp:Label ID="LabelLinkAktOfWorkAddendum" runat="server" CssClass="hide"></asp:Label>
                                                <usercontrol:checkboxBootstrap ID="CheckBoxDeleteAddendumAktOfWork" runat="server" Text="Delete" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckBoxPenaltyToMercuryEdit" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxPenaltyToMercuryEdit_CheckedChanged1"
                                                    Checked='<%# Bind("Penalties")%>' />

                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxPenaltyNotesClientEdit" runat="server"
                                                    CssClass="TextBoxContract" Height="75px"
                                                    Text='<%# Bind("PenaltiesNote")%>' TextMode="MultiLine"
                                                    Width="200px" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPenaltyMercuryNoteEdit" runat="server" ErrorMessage="Required" Display="Dynamic" 
                                                CssClass="LabelGeneral" ControlToValidate="TextBoxPenaltyNotesClientEdit" ></asp:RequiredFieldValidator>


                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </asp:Panel>
                            <!-- Client Contract Additional Details -->


                </EditItemTemplate>

            </asp:templatefield>
        </Columns>

    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceShowAddendum" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand=" 
SELECT  Table_Addendums.AddendumID, Table_Addendums.ContractID, RTRIM(Table_Addendums.PO_No) AS PO_No, RTRIM(Table_Addendums.AddendumNo) 
        AS AddendumNo, Table_Addendums.AddendumDate, Table_Addendums.AddendumValue_woVAT, Table_Addendums.AddendumValue_WithVAT, 
        Table_Addendums.VATpercent, RTRIM(Table_Addendums.AddendumDescription) AS AddendumDescription, 
        ISNULL(RTRIM(Table_Addendums.AddendumLinkToTemplatefile_DOC),N'') AS AddendumLinkToTemplatefile_DOC, Table_Addendums.AddendumSignBySupplier, 
        Table_Addendums.AddendumSignByMercury, Table_Addendums.AddendumCollectionBySupplier, RTRIM(Table_Addendums.AddendumGivenTo) 
        AS AddendumGivenTo, ISNULL(RTRIM(Table_Addendums.AddendumLinkToPDFcopy),N'') AS AddendumLinkToPDFcopy, Table_Addendums.AddendumArchivedByMercury, 
        Table_Addendums.UpdatedBy, Table_Addendums.AddendumRetention, RTRIM(Table_Addendums.AddendumNote) AS AddendumNote, Table_Addendums.CreatedBy, 
        RTRIM(Table_Addendums.PersonCreated) AS PersonCreated, RTRIM(Table_Addendums.PersonUpdated) AS PersonUpdated, Table_Addendums.AttachmentExist, 
        Table1_Project.NewGeneration, Table_Addendums.RequestedBy, Table_Addendums.Penalties, Table_Addendums.PenaltiesNote, Table_Addendums.Budget, 
        Table_Addendums.BudgetLinkToPDF, Table_Addendums.Advance, Table_Addendums.StartDate, Table_Addendums.FinishDate, Table_Addendums.DeliveryTerms, 
        Table_Addendums.GuaranteePeriod, Table_Addendums.AddendumTypes, Table_Addendums.Scenario, Table_Addendums.POexecuted, 
        Table_Addendums.PenaltiesToSupplier, Table_Addendums.PenaltiesToSupplierNote, Table_Addendums.Interim, Table_Addendums.Shipment, 
        Table_Addendums.Delivery, ISNULL(RTRIM(Table7_CostCode.CostCode) + N' - ' + RTRIM(Table7_CostCode.CodeDescription), N'') AS CostCode, 
        Table_Addendums.Exceptional
		, Table_Contracts.ProjectID
		, Table_Addendums.AddendumLinkToTemplatefile_DOC
					  , ISNULL(UserRoles.[Commercial_Items], 0) AS Commercial_Items
					  , ISNULL(UserRoles.[ESTM], 0) AS ESTM
					  , ISNULL(UserRoles.[InitiateContractAndAddendum], 0) AS InitiateContractAndAddendum
					  , ISNULL(UserRoles.[ContractLeadGirls], 0) AS ContractLeadGirls
					  , ISNULL(UserRoles.[LawyerOnSite], 0) AS LawyerOnSite
					  , ISNULL(UserRoles.[ContractSupportGirl], 0) AS ContractSupportGirl
					  , ISNULL(ContractControlExceptional.ContractControlExceptional, 0) AS ContractControlExceptional
					  , ISNULL(Contract_User_Junction.Contract_User_Junction, 0) AS Contract_User_Junction
					  , CASE WHEN LawyersCanEnterContract.ProjectID IS NOT NULL THEN 1 ELSE 0 END AS LawyersCanEnterContract
FROM    Table_Addendums INNER JOIN
        Table_Contracts ON Table_Addendums.ContractID = Table_Contracts.ContractID INNER JOIN
        Table1_Project ON Table_Contracts.ProjectID = Table1_Project.ProjectID LEFT OUTER JOIN
        Table7_CostCode ON Table_Addendums.CostCode = Table7_CostCode.CostCode
					  LEFT OUTER JOIN (
						SELECT [ProjectID]
							  ,[UserName]
							  ,[Commercial_Items]
							  ,[ESTM]
							  ,[InitiateContractAndAddendum]
							  ,[ContractLeadGirls]
							  ,[LawyerOnSite]
							  ,[ContractSupportGirl]
						  FROM [ApprMx].[View_Approval_UserRolePrjectMatrix]
						WHERE UserName = @UserName 
					  ) AS UserRoles ON UserRoles.ProjectID = Table_Contracts.ProjectID
					  LEFT OUTER JOIN (
						SELECT     ProjectID, COUNT(ProjectID) AS ContractControlExceptional
						FROM         Table_ContractControlExceptional
						WHERE     (UserName = @UserName)
						GROUP BY ProjectID
					  ) AS ContractControlExceptional ON ContractControlExceptional.ProjectID = Table_Contracts.ProjectID
					  LEFT OUTER JOIN (
						SELECT ProjectID, Count(ProjectID) AS Contract_User_Junction 
						FROM Table_Contract_User_Junction 
						WHERE UserName = @UserName 
						GROUP BY ProjectID
					  ) AS Contract_User_Junction ON Contract_User_Junction.ProjectID = Table_Contracts.ProjectID
					  LEFT OUTER JOIN Table1_Project_LawyersCanEnterContract LawyersCanEnterContract ON LawyersCanEnterContract.ProjectID = Table_Contracts.ProjectID
WHERE   (Table_Addendums.ContractID = @ContractID) 
"

        UpdateCommand="UPDATE [Table_Addendums]
                                   SET [ContractID] = @ContractID
                                      ,[PO_No] = @PO_No
                                      ,[AddendumNo] = @AddendumNo
                                      ,[AddendumDate] = @AddendumDate
                                      ,[AddendumValue_woVAT] = @AddendumValue_woVAT
                                      ,[AddendumValue_WithVAT] = @AddendumValue_WithVAT
                                      ,[VATpercent] = @VATpercent
                                      ,[AddendumDescription] = @AddendumDescription
                                      ,[AddendumLinkToTemplatefile_DOC] = @AddendumLinkToTemplatefile_DOC
                                      ,[AddendumSignBySupplier] = @AddendumSignBySupplier
                                      ,[AddendumSignByMercury] = @AddendumSignByMercury
                                      ,[AddendumCollectionBySupplier] = @AddendumCollectionBySupplier
                                      ,[AddendumGivenTo] = @AddendumGivenTo
                                      ,[AddendumLinkToPDFcopy] = @AddendumLinkToPDFcopy
                                      ,[AddendumArchivedByMercury] = @AddendumArchivedByMercury
                                      ,[UpdatedBy] = @UpdatedBy
                                      ,[PersonUpdated] = @PersonUpdated
                                      ,[AddendumRetention] = @AddendumRetention
                                      ,[AddendumNote] = @AddendumNote
                                      ,[AttachmentExist] = @AttachmentExist
                                      , Penalties = @Penalties 
                                      , PenaltiesNote = @PenaltiesNote 
                                      , Budget = @Budget
                                      , BudgetLinkToPDF = @BudgetLinkToPDF 
                                      , Advance = @Advance 
                                      , StartDate = @StartDate 
                                      , FinishDate = @FinishDate 
                                      , DeliveryTerms = @DeliveryTerms 
                                      , GuaranteePeriod = @GuaranteePeriod 
                                      , PenaltiesToSupplier = @PenaltiesToSupplier 
                                      , PenaltiesToSupplierNote = @PenaltiesToSupplierNote 
                                      , Interim = @Interim
                                      , Shipment = @Shipment
                                      , Delivery = @Delivery
                                 WHERE [AddendumID] = @AddendumID"
        DeleteCommand="
                                ALTER TABLE Table_Addendum_UsersApprv DISABLE TRIGGER AfterDelete_Table_Addendum_UsersApprv
                                ALTER TABLE Table_Addendum_UserRemarks DISABLE TRIGGER Table_Addendum_UserRemarks_delete

                                DELETE Table_Addendums WHERE (AddendumID= @AddendumID)

                                ALTER TABLE Table_Addendum_UsersApprv ENABLE TRIGGER AfterDelete_Table_Addendum_UsersApprv
                                ALTER TABLE Table_Addendum_UserRemarks ENABLE TRIGGER Table_Addendum_UserRemarks_delete " 
                            ondeleted="SqlDataSourceShowAddendum_Deleted">
                         <SelectParameters>
                                <asp:Parameter Name="ContractID" />
                                <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" PropertyName="Text" DefaultValue="-" />
                        </SelectParameters>   
    </asp:SqlDataSource>

                        <div class="showMoreLess" >
                            <a class="icon-animated-vertical"> > > > Показать больше...</a>
                        </div>

                        </asp:Panel>
                </ItemTemplate>

                <EditItemTemplate>
                        <div style="text-align: center"><asp:Label ID="LabelScenarioOutOfRange" runat="server" Text=""  ForeColor="#FF0066" Font-Bold="True" Font-Size="12px" /></div>
                        <div style="text-align: center"><asp:Label ID="LabelProjectSupplierComtability" runat="server" Visible="false" Text="Project and Supplier must be compatible"  ForeColor="#FF0066" Font-Bold="True" Font-Size="12px" /></div>
                        <table>
                            <asp:Label ID="LabelPoNoBeforeEdit" runat="server" Visible="false" ></asp:Label>
                            <asp:DropDownList ID="DropDownListPOnoEdit" runat="server" DataSourceID="SqlDataSourcePoNo" DataTextField="Information" 
                              DataValueField="PO_No" Font-Names="Consolas" Font-Size="11px" Width="1120px"  BackColor="#FFFF66">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourcePoNo" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
SelectCommand="SELECT N'' As PO_No, 'Select Po No' as Information

UNION ALL

SELECT
rtrim(Table2_PONo.PO_No) AS PO_No,REPLACE(rtrim(Table2_PONo.PO_No)+N' '+RIGHT('00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000' + CAST(dbo.Table2_PONo.Description AS varchar(140)), 140)
+N' &gt; '+ case LEN(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)))
when 7 then LEFT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),1)+ ','+ RIGHT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),6) 
when 8 then LEFT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),2)+ ','+ RIGHT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),6) 
when 9 then LEFT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),3)+ ','+ RIGHT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),6) 
when 10 then LEFT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),1)+ ','+
	+ SUBSTRING(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),2,3)+ ','+
	+ SUBSTRING(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),5,3)+ '.'+	
	+ RIGHT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),2) 
when 11 then LEFT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),2)+ ','+
	+ SUBSTRING(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),3,3)+ ','+
	+ SUBSTRING(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),6,3)+ '.'+	
	+ RIGHT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),2)	
when 12 then LEFT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),3)+ ','+
	+ SUBSTRING(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),4,3)+ ','+
	+ SUBSTRING(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),7,3)+ '.'+	
	+ RIGHT(rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT)),2)	
else rtrim(CONVERT(nchar(20),dbo.View_PoSumExcVAT.PoSumExcVAT))
end +N' ' +RTRIM(dbo.View_PoSumExcVAT.PO_Currency) + N' exc.VAT', ' ', '_')
                       AS Information
FROM         dbo.Table2_PONo INNER JOIN
                      dbo.Table1_Project ON dbo.Table2_PONo.Project_ID = dbo.Table1_Project.ProjectID INNER JOIN
                      dbo.Table6_Supplier ON dbo.Table2_PONo.SupplierID = dbo.Table6_Supplier.SupplierID INNER JOIN
                      dbo.View_PoSumExcVAT ON dbo.Table2_PONo.PO_No = dbo.View_PoSumExcVAT.PO_No
WHERE Project_ID= @Project_ID AND dbo.Table6_Supplier.SupplierID = @SupplierID ">
<SelectParameters>
 <asp:Parameter Name="Project_ID" Type="Int32" />
 <asp:Parameter Name="SupplierID" Type="String"/>
</SelectParameters>
</asp:SqlDataSource>
<asp:Label ID="LabelWarningPoDublication" runat="server"></asp:Label>
                            <tr>
                                <td style="width:40px;">
                                    <div class="btn-group-vertical" role="group" >
                                                <asp:Button ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update" CssClass="btn btn-minier btn-primary" Text="Update"  />

                                                <asp:Button ID="ButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel" CssClass="btn btn-minier btn-danger " Text="Cancel" />
                                    </div>
                                </td>

                                <td style="width:20px;">

                                </td>
                                <td style="width:80px; text-align: center;">
                                    <div>
                                        <asp:DropDownList ID="DDLProjectEdit" runat="server"  
                                             CssClass="EditContract" 
                                            DataSourceID="SqlDataSourcePrjEdit" DataTextField="ProjectName" 
                                            DataValueField="ProjectID" Font-Size="9px" Height="20px" 
                                             width="75px">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSourcePrjEdit" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" >
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                                                    PropertyName="Text" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                    <div>
                                        <asp:Label ID="LabelPOnoEdit" runat="server" Text='<%# Bind("PO_No") %>'></asp:Label>
                                        <asp:Label  ID="LabelScenario" runat="server" Text='<%# Bind("Scenario") %>' CssClass="hidepanel" ></asp:Label>
                                    </div>
                                </td>
                                <td style="width:80px; text-align: center;">

                                  <asp:Panel ID="PanelHideFromUser" runat="server" CssClass="hidepanel">
                                    <asp:Literal ID="LiteralContractID" runat="server" Text='<%# Bind("ContractID") %>'></asp:Literal>
                                    <asp:Literal ID="LiteralPOexecuted" runat="server" Text='<%# Bind("POexecuted") %>'></asp:Literal>
                                  </asp:Panel>

                                    <asp:Label ID="LabelContractNoEdit" runat="server" CssClass="ContractEditTitles"  >Contract No</asp:Label>
                                    <br />
                                    <asp:TextBox ID="TextBoxContractNoEdit" runat="server" CssClass="EditContract" 
                                        Text='<%# Bind("ContractNo") %>' width="75px"></asp:TextBox>
                                </td>
                                <td style="width:80px; text-align: center;">
                                    <asp:Label ID="LabelContractDateEdit" runat="server" CssClass="ContractEditTitles"  >Contract Date</asp:Label>
                                    <br />

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorContractDate" 
                                        runat="server" ControlToValidate="TextBoxContractDateEdit" Display="Dynamic" 
                                        ErrorMessage="dd/mm/yyyy" 
                                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="TextBoxContractDateEdit" runat="server" 
                                        CssClass="EditContract add_datepicker" Text='<%# Bind("ContractDate", "{0:dd/MM/yyyy}")%>' 
                                        width="75px"></asp:TextBox>

                                </td>
                                <td style="width:135px; text-align: left;">
                                        <asp:TextBox ID="TextBoxPO_Count" runat="server" Text='<%# Bind("PO_Count") %>' CssClass="hidepanel" ></asp:TextBox>
                                    <div>
                                      <asp:CompareValidator ID="CompareValidatorSupplier" runat="server" ErrorMessage=""
                                       ValueToCompare="INN Number is not 10 digit or Supplier does not exist"
                                       ControlToValidate="TextBoxToValidate" Display="Dynamic" Operator="NotEqual"></asp:CompareValidator>
                                    </div>

                                    <asp:Label ID="LabelSupplierIDEdit" runat="server" CssClass="ContractEditTitles"  >Supplier INN</asp:Label>
                                    <br />

                                    <asp:TextBox ID="SupplierIDTextBox" runat="server" AutoPostBack="true"
                                        CssClass="EditContract" ontextchanged="SupplierIDTextBox_TextChanged" 
                                        Text='<%# Bind("SupplierID") %>' width="115px" CausesValidation="True" />
                                    <div ID="AutoCompleteDiv">
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSupplierIDTextBox" ValidationGroup="PO_count"
                                        runat="server" ControlToValidate="SupplierIDTextBox" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" 
                                        CompletionInterval="0" CompletionListElementID="AutoCompleteDiv" 
                                        CompletionSetCount="12" MinimumPrefixLength="0" 
                                        onclientshown="SetAutoCompleteWidth" ServiceMethod="SupplierEdit" 
                                        ServicePath="AutoComplete.asmx" TargetControlID="SupplierIDTextBox">
                                    </asp:AutoCompleteExtender>
                                    <div>
                                        <asp:Label ID="LabelSupplierNameInEdit" runat="server" CssClass="LabelGeneral"></asp:Label>
                                        <asp:TextBox ID="TextBoxToValidate" runat="server" Visible="false" ></asp:TextBox>
                                    </div>
                                </td>
                                <td style="width:130px; text-align: center;">
                                    <asp:Label ID="LabelContractDescriptionEdit" runat="server" CssClass="ContractEditTitles"  >Description</asp:Label>
                                    <br />

                                    <asp:TextBox ID="TextBoxContractDescriptionEdit" runat="server" 
                                        CssClass="EditContract" Height="40" Text='<%# Bind("ContractDescription") %>' 
                                        TextMode="MultiLine" width="125px"></asp:TextBox>
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelTypeEdit" runat="server" CssClass="ContractEditTitles"  >Type</asp:Label>
                                    <br />

                                    <asp:DropDownList ID="DropDownListContractTypeEdit" runat="server" 
                                        AppendDataBoundItems="True" CssClass="EditContract" 
                                        selectedvalue='<%# Bind("ContractType") %>' width="45px">
                                        <asp:ListItem Value="Sub">Sub</asp:ListItem>
                                        <asp:ListItem Value="Sup">Sup</asp:ListItem>
                                        <asp:ListItem Value="Ser">Ser</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelSignedSupplierEdit" runat="server" CssClass="ContractEditTitles"  >Sign Sup.</asp:Label>
                                    <br />

                                    <asp:CheckBox ID="SignBySupplierCheckBoxEdit" runat="server" 
                                        Checked='<%# Bind("SignBySupplier") %>' />
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelSignMercEdit" runat="server" CssClass="ContractEditTitles"  >Sign Merc.</asp:Label>
                                    <br />

                                    <asp:CheckBox ID="SignByMercuryCheckBoxEdit" runat="server" 
                                        Checked='<%# Bind("SignByMercury") %>' />
                                </td>
                                <td style="width:50px; text-align: center; display: none;">
                                    <asp:CheckBox ID="CollectionBySupplierCheckBoxEdit" runat="server" 
                                        Checked='<%# Bind("CollectionBySupplier") %>' />
                                </td>

                                <td style="width:100px; text-align: center;">
                                    <asp:Label ID="LabelGivenToEdit" runat="server" CssClass="ContractEditTitles"  >Given To</asp:Label>
                                    <br />

                                   <asp:TextBox ID="TextBoxContractGivenTo" width="75px" Height="40" 
                                    TextMode="MultiLine" runat="server" Text='<%# Bind("ContractGivenTo") %>' CssClass="EditContract">
                                   </asp:TextBox>
                                </td>

                                <td style="width:85px; font-size:10px;">

                                    <span class="ContractEditTitles"><asp:literal ID="LiteralContractValueExcVAT" Text="Contract Value Exc. VAT" runat="server" ></asp:literal></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorContractValue" 
                                        runat="server" ControlToValidate="TextBoxContractValueEdit" Display="Dynamic" 
                                        ErrorMessage="Wrong format" 
                                        ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="TextBoxContractValueEdit" runat="server" 
                                        CssClass="EditContract" Text='<%# Bind("ContractValue_woVAT") %>' width="75px"></asp:TextBox>
                                    <br />

                                    <span class="ContractEditTitles"><asp:literal ID="LiteralContractValueWithVAT" Text="Contract Value With VAT"  runat="server" ></asp:literal></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorContractValueWithVATEdit" 
                                        runat="server" ControlToValidate="TextBoxContractValueWithVATEdit" Display="Dynamic" 
                                        ErrorMessage="Wrong format" 
                                        ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorContractValueWithVATEdit" runat="server" 
                                            ControlToValidate="TextBoxContractValueWithVATEdit" 
                                            Display="Dynamic" ErrorMessage="Required">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TextBoxContractValueWithVATEdit" runat="server" 
                                        CssClass="EditContract" Text='<%# Bind("ContractValue_withVAT") %>' width="75px"></asp:TextBox>
                                    <br />

                                    <span class="ContractEditTitles"><asp:literal ID="LiteralVAT" Text="VAT %"  runat="server" ></asp:literal></span>
                                    <asp:FilteredTextBoxExtender ID="TextBoxVATpercent_FilteredTextBoxExtender" 
                                        runat="server" FilterType="Numbers" TargetControlID="TextBoxVAT">
                                    </asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxVAT" runat="server" 
                                        ControlToValidate="TextBoxVAT" Display="Dynamic"
                                        ErrorMessage="Required">
                                    </asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidatorTextBoxVAT" runat="server"  Display="Dynamic"
                                        ErrorMessage="range to be 0-20" ControlToValidate="TextBoxVAT" 
                                        MaximumValue="20" MinimumValue="0" Type="Double">
                                    </asp:RangeValidator>
                                    <asp:TextBox ID="TextBoxVAT" runat="server" 
                                        CssClass="EditContract" Text='<%# Bind("VATpercent") %>' width="75px">
                                    </asp:TextBox>

                                    <%--this to be removed completely later on--%>
                                    <div class="hidden">
                                        <span class="ContractEditTitles"><asp:literal ID="LiteralBudgetEdit" Text="Budget With VAT" runat="server" ></asp:literal></span>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorBudgetEdit" 
                                            runat="server" ControlToValidate="TextBoxBudget" Display="Dynamic" 
                                            ErrorMessage="Wrong format" 
                                            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="TextBoxBudget" runat="server" 
                                            CssClass="EditContract" Text='<%# Bind("Budget")%>' width="75px"></asp:TextBox>

                                        <br />

                                        <div>
                                            <asp:Label ID="labelBudgetInfo" runat="server" CssClass="LabelGeneral"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="LabelBudgetTitle" runat="server" Text="Budget File" Width="100px" CssClass="ContractEditTitles" />
                                        </div>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LabelBudgetDeleteDoc" runat="server" ForeColor="#3366FF" Text="Delete Budget File?" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBoxBudgetDeleteDoc" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="TextBoxBudgetLinkPDF" runat="server" CssClass="hidepanel" Text='<%# Bind("BudgetLinkToPDF")%>' />
                                            <asp:FileUpload ID="FileUploadBudgetPDF" runat="server" CssClass="EditContract" Width="80px" />
                                        </div>
                                        <div>
                                            <asp:Button ID="ButtonBudgetPDFUpload" runat="server" CausesValidation="False" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="UploadBudgetPDF" CssClass="DDLEditContract" Text="Upload Budget PDF" />
                                        </div>
                                    </div>
                                    <%--this to be removed completely later on--%>

                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelCurrencyEdit" runat="server" CssClass="ContractEditTitles"  >Currency</asp:Label>
                                    <br />

                                    <asp:DropDownList ID="DropDownListCurrencyEdit" runat="server" 
                                        AppendDataBoundItems="True" CssClass="EditContract" 
                                        selectedvalue='<%# Bind("ContractCurrency") %>'>
                                        <asp:ListItem Value="">-</asp:ListItem>
                                        <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                                        <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                                        <asp:ListItem Value="Euro">Euro</asp:ListItem>
                                        <asp:ListItem Value="GBP">GBP</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width:20px; text-align: center;">
                                    <asp:Label ID="LabelRetentionEdit" runat="server" CssClass="ContractEditTitles"  >Retention</asp:Label>
                                    <br />
                                  <asp:TextBox ID="TextBoxRetention" runat="server" CssClass="EditContract" Text='<%# Bind("Retention")%>' width="50px" />
                                  <asp:RangeValidator ID="RangeValidatorRetention" runat="server" ControlToValidate="TextBoxRetention" 
                                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                                      MinimumValue="0" Type="Double">
                                  </asp:RangeValidator>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorRetention" runat="server" ControlToValidate="TextBoxRetention" 
                                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                                  </asp:RegularExpressionValidator>
                                </td>
                                <td style="width:50px; text-align: center;">
                                    <asp:Label ID="LabelArchivedEdit" runat="server" CssClass="ContractEditTitles"  >Archived</asp:Label>
                                    <br />

                                    <asp:CheckBox ID="ArchivedByMercuryCheckBoxEdit" runat="server" 
                                        Checked='<%# Bind("ArchivedByMercury") %>' />
                                </td>
                                <td style="width:100px; text-align: center;">
                                    <asp:Label ID="LabelNote" runat="server" CssClass="ContractEditTitles"  >Note</asp:Label>
                                    <br />

                                    <asp:TextBox ID="TextBoxNoteEdit" runat="server" CssClass="EditContract" 
                                        Height="40" Text='<%# Bind("Note") %>' TextMode="MultiLine" width="115px"></asp:TextBox>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td class="td_fl_mng">
                                                            <asp:ImageButton ID="ImageButtonLinkToTemplatefile_DOC" runat="server" 
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToTemplatefile_DOC") %>' 
                                                                CommandName="OpenDOCedit" />

                                            </td>
                                            <td class="td_fl_mng">
                                                            <asp:ImageButton ID="ImageButtonLinkToPDFcopy" runat="server" 
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LinkToPDFcopy") %>' 
                                                                CommandName="OpenPDFedit" />

                                            </td>
                                        </tr>
                                    </table>


                                                        <asp:TextBox ID="LinkToTemplatefile_DOCTextBoxEdit" runat="server" 
                                                            Text='<%# Bind("LinkToTemplatefile_DOC") %>' CssClass="hidepanel" />
                                                        <asp:TextBox ID="LinkToPDFcopyTextBoxEdit" runat="server" CssClass="hidepanel" 
                                                            Text='<%# Bind("LinkToPDFcopy") %>' />   

                                </td>
                            </tr>
                        </table>

      <!---- COMMERCIAL TERMS EDIT MODE-------------------------------------------------------->
      <asp:Panel ID="PanelCommercialTermsContractEdit" runat="server" >
      <table style="background-color: #CCCCCC">
       <tr>
           <th colspan="12" style="text-align:center; color:Red;">
               <asp:Literal ID="LiteralCommercialRoleTitle" runat="server">
        </asp:Literal>
           </th>
           <tr>
               <td class="CommercialTitles"><%= BodyTexts.Ref("hEhO9xKZVke1gtHcr6u2Gg")%>: </td>
               <td style="padding:2px;">
                   <asp:TextBox ID="TextBoxStartDate" runat="server" CssClass="EditContract add_datepicker" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy}")%>' width="75px"></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidatorStartDate" runat="server" ControlToValidate="TextBoxStartDate" Display="Dynamic" ErrorMessage="dd/mm/yyyy" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}">
                                    </asp:RegularExpressionValidator>
               </td>
               <td class="CommercialTitles"><%= BodyTexts.Ref("WBn92MUF20iopsBEY2+rxA")%>: </td>
               <td style="padding:2px;">
                   <asp:DropDownList ID="DropDownListPenalty" runat="server" CssClass="EditContract" >
                       <asp:ListItem Value="">-</asp:ListItem>
                       <asp:ListItem Value="1">Yes</asp:ListItem>
                       <asp:ListItem Value="0">No</asp:ListItem>
                   </asp:DropDownList>
               </td>
               <td class="CommercialTitles" rowspan="2"><%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>:</td>
               <td rowspan="2">
                   <asp:TextBox ID="TextBoxPenaltyNote" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("PenaltiesNote")%>' TextMode="MultiLine" width="125px">
                                    </asp:TextBox>
                   <br />
                   <asp:Label ID="LabelValidationPenaltyMercuryNote" runat="server" ForeColor="Red" ></asp:Label>
               </td>
               <td class="CommercialTitles" rowspan="2"><%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>:</td>
               <td rowspan="2">
                   <asp:TextBox ID="TextBoxPenaltyNoteSupplier" runat="server" CssClass="EditContract" Height="40" 
                       Text='<%# Bind("PenaltiesToSupplierNote")%>' TextMode="MultiLine" width="125px"></asp:TextBox>
                   <br />
                   <asp:Label ID="LabelValidationPenaltySupplierNote" runat="server" ForeColor="Red" ></asp:Label>
               </td>
               <td class="CommercialTitles" rowspan="2"><%= BodyTexts.Ref("76jVcY8TbUSVUcJGrpnfhg")%>: </td>
               <td rowspan="2" style="padding:2px; width:150px;">
                   <asp:TextBox ID="TextBoxDeliveryTerms" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("DeliveryTerms") %>' TextMode="MultiLine" width="125px">
                   </asp:TextBox>
               </td>
               <td class="CommercialTitles" rowspan="2"><%= BodyTexts.Ref("Vh6I6dyPyU+d+3zCSzOyhQ")%>: </td>
               <td rowspan="2" style="padding:2px; width:150px;">
                   <asp:TextBox ID="TextBoxGuaranteePeriod" runat="server" CssClass="EditContract" Height="40" Text='<%# Bind("GuaranteePeriod") %>' TextMode="MultiLine" width="125px">
                                    </asp:TextBox>
               </td>
           </tr>
           <tr>
               <td class="CommercialTitles"><%= BodyTexts.Ref("eOZry6gjB0yqlsMmUT7/aw")%>: </td>
               <td style="padding:2px;">
                   <asp:TextBox ID="TextBoxFinishDate" runat="server" CssClass="EditContract add_datepicker" Text='<%# Bind("FinishDate", "{0:dd/MM/yyyy}")%>' width="75px"></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidatorFinishDate" runat="server" ControlToValidate="TextBoxFinishDate" Display="Dynamic" ErrorMessage="dd/mm/yyyy" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}">
                                    </asp:RegularExpressionValidator>
               </td>
               <td class="CommercialTitles"><%= BodyTexts.Ref("uzuez9RR302+6q9tOPvnJw")%>:</td>
               <td style="padding:2px;">
                   <asp:DropDownList ID="DropDownListPenaltyToSupplier" runat="server" CssClass="EditContract" >
                       <asp:ListItem Value="">-</asp:ListItem>
                       <asp:ListItem Value="1">Yes</asp:ListItem>
                       <asp:ListItem Value="0">No</asp:ListItem>
                   </asp:DropDownList>
               </td>
           </tr>
       </tr>
          <tr>
              <td class="CommercialTitles"><%= BodyTexts.Ref("wt6Qt9QUh0GDaiAKFZhS0A")%>: </td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxAdvance" runat="server" CssClass="EditContract" Text='<%# Bind("Advance") %>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorAdvance" runat="server" ControlToValidate="TextBoxAdvance" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorAdvance" runat="server" ControlToValidate="TextBoxAdvance" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td class="CommercialTitles"><%= BodyTexts.Ref("L4img0LG+06x0/+AqzBnkQ")%>:</td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxInterim" runat="server" CssClass="EditContract" Text='<%# Bind("Interim")%>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorInterim" runat="server" ControlToValidate="TextBoxInterim" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorInterim" runat="server" ControlToValidate="TextBoxInterim" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td class="CommercialTitles"><%= BodyTexts.Ref("3faC83SQyUS74LvrVeBEgA")%>:</td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxShipment" runat="server" CssClass="EditContract" Text='<%# Bind("Shipment")%>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorShipment" runat="server" ControlToValidate="TextBoxShipment" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorShipment" runat="server" ControlToValidate="TextBoxShipment" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td class="CommercialTitles"><%= BodyTexts.Ref("CdFoIWOhUUq43gPj9azb9Q")%>:</td>
              <td style="padding:2px;">
                  <asp:TextBox ID="TextBoxDelivery" runat="server" CssClass="EditContract" Text='<%# Bind("Delivery")%>' width="50px" />
                  <asp:RangeValidator ID="RangeValidatorDelivery" runat="server" ControlToValidate="TextBoxDelivery" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="range to be 0-100" MaximumValue="100" 
                      MinimumValue="0" Type="Double">
                  </asp:RangeValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorDelivery" runat="server" ControlToValidate="TextBoxDelivery" 
                      CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="not valid number" 
                      ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                  </asp:RegularExpressionValidator>
              </td>
              <td >
                  <asp:Label ID="LabelPaymentTermsValidationNotification" runat="server" ForeColor="Red" Font-Bold="true" Visible="false">
                  </asp:Label>
              </td>
              <td >

              </td>
              <td >

              </td>
              <td >

              </td>
          </tr>
      </table>
      </asp:Panel>
      <!---------------------------------------------------------- COMMERCIAL TERMS EDIT MODE---->

                            <!-- Client Contract Additional Details -->
                            <asp:Panel ID="PanelClientContractAdditionalDetailsEdit" runat="server" Visible="false" CssClass="panel panel-info " Width="710px">
                                <div class="panel-heading">
                                    <%= BodyTexts.Ref("VP2VuGvsMkepSyxNTrK95w")%>
                                </div>

                                <div class="panel-body">
                                    <table class="table">
                                        <thead>
                                            <td style="width:250px;">
                                                <%= BodyTexts.Ref("76jVcY8TbUSVUcJGrpnfhg")%>
                                            </td>
                                            <td style="width:80px;">
                                                <%= BodyTexts.Ref("RpoOSVLThUeJmnFtjELvww")%>
                                            </td>
                                            <td style="width:80px;">
                                                <%= BodyTexts.Ref("Kd8jnLnalkSmye2nfayPRw")%>
                                            </td>
                                            <td style="width:50px;">
                                                <%= BodyTexts.Ref("d5ZE9E74GUS3dwP/bQT1vg")%>
                                            </td>
                                            <td style="width:250px;">
                                                <%= BodyTexts.Ref("b7cyUgbKckK/KGTaEbD5kA")%>
                                            </td>
                                        </thead>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBoxDeliveryTermEdit" runat="server"
                                                    CssClass="TextBoxGeneralRevMultiline" 
                                                    Text='<%# Bind("DeliveryTerms")%>' TextMode="MultiLine"
                                                    Width="200px" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxCompletionDateEdit" runat="server" Width="80px"
                                                    CssClass="TextBoxGeneralRev add_datepicker"  />

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCompletionDateEdit" runat="server"
                                                    ControlToValidate="TextBoxCompletionDateEdit"
                                                    ErrorMessage="dd/mm/yyyy" Display="Dynamic"
                                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>

                                            </td>
                                            <td>

                                                <usercontrol:checkboxBootstrap ID="CheckBoxAktOfWorkEdit" runat="server" BootstrapType="3"/>
                                                <br /><br />
                                                <asp:FileUpload ID="FileUploadAktOfWorkContract" runat="server" CssClass="btn btn-minier"/>
                                                <asp:Button ID="ButtonFileUpload" runat="server" CssClass="btn btn-mini btn-success" Text="upload file" 
                                                    CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="AktOfWorkContract" />
                                                <asp:Label ID="LabelLinkAktOfWorkContract" runat="server" CssClass="hide"></asp:Label>
                                                <usercontrol:checkboxBootstrap ID="CheckBoxDeleteContractAktOfWork" runat="server" Text="Delete" />

                                                

                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckBoxPenaltyToMercuryEdit" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxPenaltyToMercuryEdit_CheckedChanged"
                                                    Checked='<%# Bind("Penalties")%>' />

                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxPenaltyNotesClientEdit" runat="server"
                                                    CssClass="TextBoxGeneralRevMultiline"  
                                                    Text='<%# Bind("PenaltiesNote")%>' TextMode="MultiLine"
                                                    Width="200px" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPenaltyMercuryNoteEdit" runat="server" ErrorMessage="Required" Display="Dynamic" 
                                                CssClass="LabelGeneral" ControlToValidate="TextBoxPenaltyNotesClientEdit" ></asp:RequiredFieldValidator>


                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </asp:Panel>
                            <!-- Client Contract Additional Details -->


                </EditItemTemplate>
</asp:TemplateField>

        </Columns>
        <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceForContractGrid" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="ContractGrid_Select" SelectCommandType="StoredProcedure"

        UpdateCommand="UPDATE Table_Contracts
                              SET  ProjectID= @ProjectID
                                   ,PO_No= @PO_No
                                   ,ContractNo= @ContractNo
                                   ,ContractDate= @ContractDate
                                   ,ContractValue_woVAT= @ContractValue_woVAT
                                   ,ContractCurrency= @ContractCurrency
                                   ,SupplierID= @SupplierID
                                   ,ContractDescription= @ContractDescription
                                   ,ContractType= @ContractType
                                   ,LinkToTemplatefile_DOC= @LinkToTemplatefile_DOC
                                   ,SignBySupplier= @SignBySupplier
                                   ,SignByMercury= @SignByMercury
                                   ,CollectionBySupplier= @CollectionBySupplier
                                   ,ContractGivenTo= @ContractGivenTo
                                   ,LinkToPDFcopy= @LinkToPDFcopy
                                   ,ArchivedByMercury= @ArchivedByMercury
                                   ,Retention= @Retention
                                   ,Note= @Note
                                   ,UpdatedBy= @UpdatedBy
                                   ,PersonUpdated= @PersonUpdated
                                   ,[AttachmentExist] = @AttachmentExist
                                   ,[ContractValue_withVAT] = @ContractValue_withVAT
                                   ,[VATpercent] = @VATpercent
                                   ,Penalties = @Penalties
                                   ,PenaltiesNote = @PenaltiesNote
                                   ,PenaltiesToSupplier = @PenaltiesToSupplier
                                   ,PenaltiesToSupplierNote = @PenaltiesToSupplierNote
                                   ,Budget = @Budget
                                   ,BudgetLinkToPDF = @BudgetLinkToPDF
                                   ,Advance = @Advance
                                   ,StartDate = @StartDate 
                                   ,FinishDate = @FinishDate 
                                   ,DeliveryTerms = @DeliveryTerms 
                                   ,GuaranteePeriod = @GuaranteePeriod 
                                   ,Interim= @Interim
                                   ,Shipment= @Shipment
                                   ,Delivery= @Delivery
                                WHERE (ContractID= @ContractID)"
        DeleteCommand="
                                ALTER TABLE Table_Contract_UsersApprv DISABLE TRIGGER AfterDelete_Table_Contract_UsersApprv
                                ALTER TABLE Table_Contracts_UserRemarks DISABLE TRIGGER Table_Contracts_UserRemarks_delete

                                DELETE Table_Contracts WHERE (ContractID= @ContractID)

                                ALTER TABLE [Table_Contract_UsersApprv] ENABLE TRIGGER AfterDelete_Table_Contract_UsersApprv
                                ALTER TABLE Table_Contracts_UserRemarks ENABLE TRIGGER Table_Contracts_UserRemarks_delete " >

                         <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownListPrjID" Name="ProjectID" PropertyName="Text" />
                            <asp:ControlParameter ControlID="DropDownListSupplier" Name="SupplierID" PropertyName="Text" />
                            <asp:ControlParameter ControlID="TextBoxEntryInterval" Name="entrytimeinterval" PropertyName="Text" DefaultValue="-" />
                            <asp:ControlParameter ControlID="TextBoxApprovalInterval" Name="approvaltimeinterval" PropertyName="Text" DefaultValue="-" />
                            <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" PropertyName="Text" DefaultValue="-" />
                            <asp:ControlParameter ControlID="hiddenSearchAdvanceMode" Name="SearchAdvanceMode" PropertyName="Value" DefaultValue="0" />
                            <asp:ControlParameter ControlID="hiddenSearchInputProjects" Name="SearchInputProjects" PropertyName="Value" DefaultValue="-" />
                            <asp:ControlParameter ControlID="hiddenSearchInputSuppliers" Name="SearchInputSuppliers" PropertyName="Value" DefaultValue="-" />
                            <asp:ControlParameter ControlID="textBoxSearchInputContractNo" Name="SearchInputContractNo" PropertyName="Text" DefaultValue="-" />
                            <asp:ControlParameter ControlID="textBoxSearchInputContractDate" Name="SearchInputContractDate" PropertyName="Text" DefaultValue="-" />
                            <asp:ControlParameter ControlID="textBoxSearchInputContractDescription" Name="SearchInputContractDescription" PropertyName="Text" DefaultValue="-" />
                            <asp:ControlParameter ControlID="ddlBoxSearchInputSignBysupplier" Name="SearchInputSignBysupplier" PropertyName="Text" DefaultValue="-" />
                            <asp:ControlParameter ControlID="ddlSearchInputSignByMercury" Name="SearchInputSignByMercury" PropertyName="Text" DefaultValue="-" />
                            <asp:ControlParameter ControlID="ddlBoxSearchInputAdvance" Name="SearchInputAdvance" PropertyName="Text" DefaultValue="-" />
                            <asp:ControlParameter ControlID="ddlSearchInputRetention" Name="SearchInputRetention" PropertyName="Text" DefaultValue="-" />
                            <asp:ControlParameter ControlID="ddlBoxSearchInputDraft" Name="SearchInputDraft" PropertyName="Text" DefaultValue="-" />
                            <asp:ControlParameter ControlID="ddlSearchInputFinal" Name="SearchInputFinal" PropertyName="Text" DefaultValue="-" />
                            <asp:ControlParameter ControlID="cbxSearchInputContractTypeSer" Name="SearchInputContractTypeSer" PropertyName="Checked" DefaultValue="1" />
                            <asp:ControlParameter ControlID="cbxSearchInputContractTypeSub" Name="SearchInputContractTypeSub" PropertyName="Checked" DefaultValue="1" />
                            <asp:ControlParameter ControlID="cbxSearchInputContractTypeSup" Name="SearchInputContractTypeSup" PropertyName="Checked" DefaultValue="1" />
                            <asp:ControlParameter ControlID="cbxSearchInputCurrencyRuble" Name="SearchInputCurrencyRuble" PropertyName="Checked" DefaultValue="1" />
                            <asp:ControlParameter ControlID="cbxSearchInputCurrencyDollar" Name="SearchInputCurrencyDollar" PropertyName="Checked" DefaultValue="1" />
                            <asp:ControlParameter ControlID="cbxSearchInputCurrencyEuro" Name="SearchInputCurrencyEuro" PropertyName="Checked" DefaultValue="1" />


                        </SelectParameters>   
    </asp:SqlDataSource>

        <asp:Panel runat="server" ID="panelexcel" CssClass="hidepanel">

<asp:GridView ID="GridViewExcel" runat="server" AutoGenerateColumns="False" 
         EnableModelValidation="True" 
        CssClass="Grid" HeaderStyle-BackColor="#3333CC" HeaderStyle-ForeColor="White">
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="LabelSortNumber" runat="server" Font-Bold="True" ForeColor="#CC0000" Font-Size="12px"></asp:Label>
                    <asp:Label ID="LabelID" runat="server" Text='<%# Bind("ID") %>' Visible = "false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PO_No" SortExpression="PO_No" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelPO_No" runat="server" Text='<%# Bind("PO_No")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ProjectID" SortExpression="ProjectID" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelProjectID" runat="server" Text='<%# Bind("ProjectID")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ProjectName" SortExpression="ProjectName" ControlStyle-Width="100" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="LabelProjectName" runat="server" Text='<%# Bind("ProjectName")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ContractNo" SortExpression="ContractNoToExcel" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelContractNoToExcel" runat="server" Text='<%# Bind("ContractNoToExcel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Add Or Not" SortExpression="AddendumOrNot" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelAddendumOrNot" runat="server" Text='<%# Bind("AddendumOrNot") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ContractNo" SortExpression="ContractNo" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("ContractNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ContractDate" SortExpression="ContractDate" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("ContractDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="StartDate" SortExpression="StartDate" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelStartDate" runat="server" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FinishDate" SortExpression="FinishDate" ControlStyle-Width="80" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="LabelFinishDate" runat="server" Text='<%# Bind("FinishDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName" ControlStyle-Width="120" HeaderStyle-Width="120">
                <ItemTemplate>
                    <asp:Label ID="LabelSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ContractDescription"  SortExpression="ContractDescription" ControlStyle-Width="300" HeaderStyle-Width="300">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ContractDescription") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contract Type" SortExpression="ContractType"  HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ContractType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Signed By Supplier" SortExpression="SignBySupplier"  HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBoxSignBySupplier" runat="server" 
                        Checked='<%# Bind("SignBySupplier") %>' Enabled="false"  Visible="False" />
                    <asp:Label ID="LabelSignBySupplier" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Signed By Mercury" SortExpression="SignByMercury"  HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBoxSignByMercury" runat="server"  Visible="False"
                        Checked='<%# Bind("SignByMercury") %>' Enabled="false" />
                    <asp:Label ID="LabelSignByMercury" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Given To" SortExpression="ContractGivenTo"  HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="LabelContractGivenTo" runat="server" 
                        Text='<%# Bind("ContractGivenTo") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Contract Value with VAT"  SortExpression="ContractValue_withVAT" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="LabelWithVAT" runat="server" Text='<%# Bind("ContractValue_withVAT")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Contract Value woVAT"  SortExpression="ContractValue_woVAT" ControlStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("ContractValue_woVAT") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText=""  SortExpression="ContractCurrency">
                <ItemTemplate>
                                <asp:Label ID="LabelContractCurrency" runat="server" Text='<%# Bind("ContractCurrency") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Retention" SortExpression="Retention"  ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="LabelRetention" runat="server" Text='<%# Bind("Retention") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Archived"  SortExpression="ArchivedByMercury"  HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBoxArchivedByMercury" runat="server" 
                        Checked='<%# Bind("ArchivedByMercury") %>' Enabled="false"  Visible="False" />
                    <asp:Label ID="LabelArchivedByMercury" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Note" SortExpression="Note" ControlStyle-Width="100" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Note") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle  CssClass="GridItemNakladnaya" />
        <HeaderStyle  CssClass="GridHeader" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceExcel" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="

                    IF @ProjectID <> 0        
					  SELECT DataSource3.*, Table1_Project.ProjectName FROM
                      (
                      SELECT RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, DataSource2.*
                      FROM         dbo.Table6_Supplier INNER JOIN
                      (SELECT * FROM
                      (
						SELECT  CASE WHEN Table_Contracts.FrameContract = 1 THEN N'Frame Contract' ELSE Table2_PONo.PO_No END AS PO_No, CONVERT(nchar(5), Table_Contracts.ContractID) + N' _ ' AS ID, Table_Contracts.ProjectID, RTRIM(Table_Contracts.ContractNo) AS ContractNo, 
											  Table_Contracts.ContractDate, Table_Contracts.SupplierID, RTRIM(Table_Contracts.ContractDescription) AS ContractDescription, RTRIM(Table_Contracts.ContractType) 
											  AS ContractType, Table_Contracts.SignBySupplier, Table_Contracts.SignByMercury, RTRIM(Table_Contracts.ContractGivenTo) AS ContractGivenTo, 
											  Table_Contracts.ContractValue_woVAT, Table_Contracts.ContractValue_withVAT, RTRIM(Table_Contracts.ContractCurrency) AS ContractCurrency, 
											  Table_Contracts.Retention, Table_Contracts.ArchivedByMercury, RTRIM(Table_Contracts.Note) AS Note, RTRIM(Table_Contracts.ContractNo) AS ContractNoToExcel, 
											  N'' AS AddendumOrNot, Table_Contracts.StartDate, Table_Contracts.FinishDate
						FROM         Table_Contracts LEFT OUTER JOIN
                      Table2_PONo ON Table_Contracts.PO_No = Table2_PONo.PO_No
                      UNION ALL

						SELECT     Table2_PONo.PO_No, CONVERT(nchar(5), Table_Contracts.ContractID) + 'A' + CONVERT(nchar(5), Table_Addendums.AddendumID) AS ID, Table_Contracts.ProjectID, 
											  RTRIM(Table_Addendums.AddendumNo) AS AddendumNo, Table_Addendums.AddendumDate, Table_Contracts.SupplierID, 
											  RTRIM(Table_Addendums.AddendumDescription) AS AddendumDescription, RTRIM(Table_Contracts.ContractType) AS ContractType, 
											  Table_Addendums.AddendumSignBySupplier, Table_Addendums.AddendumSignByMercury, RTRIM(Table_Addendums.AddendumGivenTo) AS AddendumGivenTo, 
											  Table_Addendums.AddendumValue_woVAT, Table_Addendums.AddendumValue_WithVAT, RTRIM(Table_Contracts.ContractCurrency) AS ContractCurrency, 
											  Table_Addendums.AddendumRetention, Table_Addendums.AddendumArchivedByMercury, RTRIM(Table_Addendums.AddendumNote) AS AddendumNote, 
											  RTRIM(Table_Contracts.ContractNo) AS ContractNoToExcel, N'Add' AS AddendumOrNot, Table_Addendums.StartDate, Table_Addendums.FinishDate
						FROM         Table_Contracts INNER JOIN
											  Table_Addendums ON Table_Contracts.ContractID = Table_Addendums.ContractID LEFT OUTER JOIN
											  Table2_PONo ON Table_Addendums.PO_No = Table2_PONo.PO_No
					  ) As DataSource1) AS DataSource2 ON dbo.Table6_Supplier.SupplierID = DataSource2.SupplierID
                      ) AS DataSource3
					  LEFT OUTER JOIN Table1_Project ON DataSource3.ProjectID = Table1_Project.ProjectID
                      WHERE     (DataSource3.ProjectID =@ProjectID) AND (SupplierID LIKE '%' + @SupplierID + '%')
                      ORDER BY ID ASC
        
                    IF @ProjectID = 0        
					  SELECT DataSource3.*, Table1_Project.ProjectName FROM
                      (
                      SELECT RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName, DataSource2.*
                      FROM         dbo.Table6_Supplier INNER JOIN
                      (SELECT * FROM
                      (
						SELECT  CASE WHEN Table_Contracts.FrameContract = 1 THEN N'Frame Contract' ELSE Table2_PONo.PO_No END AS PO_No, CONVERT(nchar(5), Table_Contracts.ContractID) + N' _ ' AS ID, Table_Contracts.ProjectID, RTRIM(Table_Contracts.ContractNo) AS ContractNo, 
											  Table_Contracts.ContractDate, Table_Contracts.SupplierID, RTRIM(Table_Contracts.ContractDescription) AS ContractDescription, RTRIM(Table_Contracts.ContractType) 
											  AS ContractType, Table_Contracts.SignBySupplier, Table_Contracts.SignByMercury, RTRIM(Table_Contracts.ContractGivenTo) AS ContractGivenTo, 
											  Table_Contracts.ContractValue_woVAT, Table_Contracts.ContractValue_withVAT, RTRIM(Table_Contracts.ContractCurrency) AS ContractCurrency, 
											  Table_Contracts.Retention, Table_Contracts.ArchivedByMercury, RTRIM(Table_Contracts.Note) AS Note, RTRIM(Table_Contracts.ContractNo) AS ContractNoToExcel, 
											  N'' AS AddendumOrNot, Table_Contracts.StartDate, Table_Contracts.FinishDate
						FROM         Table_Contracts LEFT OUTER JOIN
											  Table2_PONo ON Table_Contracts.PO_No = Table2_PONo.PO_No
                      UNION ALL

						SELECT     Table2_PONo.PO_No, CONVERT(nchar(5), Table_Contracts.ContractID) + 'A' + CONVERT(nchar(5), Table_Addendums.AddendumID) AS ID, Table_Contracts.ProjectID, 
											  RTRIM(Table_Addendums.AddendumNo) AS AddendumNo, Table_Addendums.AddendumDate, Table_Contracts.SupplierID, 
											  RTRIM(Table_Addendums.AddendumDescription) AS AddendumDescription, RTRIM(Table_Contracts.ContractType) AS ContractType, 
											  Table_Addendums.AddendumSignBySupplier, Table_Addendums.AddendumSignByMercury, RTRIM(Table_Addendums.AddendumGivenTo) AS AddendumGivenTo, 
											  Table_Addendums.AddendumValue_woVAT, Table_Addendums.AddendumValue_WithVAT, RTRIM(Table_Contracts.ContractCurrency) AS ContractCurrency, 
											  Table_Addendums.AddendumRetention, Table_Addendums.AddendumArchivedByMercury, RTRIM(Table_Addendums.AddendumNote) AS AddendumNote, 
											  RTRIM(Table_Contracts.ContractNo) AS ContractNoToExcel, N'Add' AS AddendumOrNot, Table_Addendums.StartDate, Table_Addendums.FinishDate
						FROM         Table_Contracts INNER JOIN
											  Table_Addendums ON Table_Contracts.ContractID = Table_Addendums.ContractID LEFT OUTER JOIN
											  Table2_PONo ON Table_Addendums.PO_No = Table2_PONo.PO_No
                      ) As DataSource1) AS DataSource2 ON dbo.Table6_Supplier.SupplierID = DataSource2.SupplierID
                      ) AS DataSource3
					  LEFT OUTER JOIN Table1_Project ON DataSource3.ProjectID = Table1_Project.ProjectID
                      WHERE     (SupplierID LIKE '%' + @SupplierID + '%')
                      ORDER BY ID ASC

                     ">
                         <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownListPrjID" Name="ProjectID" PropertyName="Text" />
                            <asp:ControlParameter ControlID="DropDownListSupplier" Name="SupplierID" PropertyName="Text" ConvertEmptyStringToNull="False" />
                        </SelectParameters>   
    </asp:SqlDataSource>
        </asp:Panel>


  <%-- MODAL POPUP PAYREQNO History --%>
  <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server"
   TargetControlID="ButtonSearch"
   PopupControlID="PanelSearch"
   BackgroundCssClass="modalBackground"
   CancelControlID="btnCancelSearch"
   PopupDragHandleControlID="PanelSearch" >
  </asp:ModalPopupExtender>
  <asp:Panel ID="PanelSearch" runat="server" Style="display:none;" >
     <div style="text-align: right">
                <asp:Button ID="btnCancelSearch" runat="server" Text="X" 
                 Font-Bold="True" Font-Size="16px" ForeColor="White" BackColor="#FF0066" 
                 OnClientClick="changeClass" />
     </div>

     <asp:Button ID="ButtonResult" runat="server" Text="asdfasdasdf" OnClick="ButtonResult_Click" />

  </asp:Panel>
  <asp:Button id="ButtonSearch"  runat="server" CssClass="hidepanel"/>
    <%-- /MODAL POPUP PAYREQNO History --%>  

</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server" ID="ContentPlaceHolderScript">

  <script src="/assets/js/jquery.tagsinput.js"></script>
  <script src="../Scripts/Webforms/contractview.js"></script>

</asp:Content>

