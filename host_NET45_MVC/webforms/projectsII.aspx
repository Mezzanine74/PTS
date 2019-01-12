<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="projectsII.aspx.vb" Inherits="projectsII" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <asp:Label ID="LabelKeepProjectID" runat="server" CssClass="hidepanel" ></asp:Label>
  <asp:Label ID="LabelFormViewVisibleStatus" runat="server" Text="0"  CssClass="hidepanel"></asp:Label>
  <asp:Label ID="LabelKeepAddendumDate" runat="server" CssClass="hidepanel" ></asp:Label>
  <asp:Label ID="LabelKeepCashInDate" runat="server" CssClass="hidepanel" ></asp:Label>
<table>
  <tr>
    <td>
      <asp:ImageButton ID="ImageButtonAddProject" runat="server" Visible="false"
        CausesValidation="False" ImageUrl="~/Images/insert.png" />
    </td>
    <td>
      <asp:FormView ID="FormViewInsertProject" runat="server" 
        DataKeyNames="ProjectID" DataSourceID="SqlDataSourceInsertProject" 
        DefaultMode="Insert" EnableModelValidation="True" Visible="false">
        <InsertItemTemplate>
          <asp:Panel ID="pnlLogin" runat="server" DefaultButton="InsertButton">
            <table>
              <tr>
                <td style="width: 80px; text-align: center;">
                  <asp:Label ID="LabelProjectNo" runat="server" CssClass="LabelGeneral" 
                    Text="Project No"></asp:Label>
                </td>
                <td style="width: 200px; text-align: center;">
                  <asp:Label ID="LabelProjectName" runat="server" CssClass="LabelGeneral" 
                    Text="Project Name"></asp:Label>
                </td>
                <td style="width: 80px; text-align: center;">
                  <asp:Label ID="LabelCurrent" runat="server" CssClass="LabelGeneral" 
                    Text="Current"></asp:Label>
                </td>
                <td style="width: 100px; text-align: center;">
                  <asp:Label ID="LabelType" runat="server" CssClass="LabelGeneral" 
                    Text="Project Type"></asp:Label>
                </td>
                <td style="width: 80px; text-align: center;">
                  <asp:Label ID="LabelReporting" runat="server" CssClass="LabelGeneral" 
                    Text="Reporting"></asp:Label>
                </td>
                <td style="width: 80px; text-align: center;">
                  <asp:Label ID="LabelBackUp" runat="server" CssClass="LabelGeneral" 
                    Text="BackUp"></asp:Label>
                </td>
                <td style="width: 80px; text-align: center;">
                  <asp:Label ID="LabelPOcreate" runat="server" CssClass="LabelGeneral" 
                    Text="POcreate"></asp:Label>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td style="text-align: center; background-color: #F5F5F5;">
                  <asp:TextBox ID="TextBoxProjectNo" runat="server" 
                    Text='<%# Bind("ProjectID") %>' Width="50px" />
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="TextBoxProjectNo" Display="Dynamic" ErrorMessage="!"></asp:RequiredFieldValidator>
                </td>
                <td style="text-align: center; background-color: #F5F5F5;">
                  <asp:TextBox ID="TextBoxProjectName" runat="server" 
                    Text='<%# Bind("ProjectName") %>' Width="150px" />
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="TextBoxProjectName" Display="Dynamic" ErrorMessage="!"></asp:RequiredFieldValidator>
                </td>
                <td style="text-align: center; background-color: #F5F5F5;">
                  <asp:CheckBox ID="CheckBoxCurrent" runat="server" 
                    Checked='<%# Bind("CurrentStatus") %>' />
                </td>
                <td style="text-align: center; background-color: #F5F5F5;">
                  <asp:DropDownList ID="DropDownListProjectType" runat="server" 
                    DataSourceID="SqlDataSourceProjectType" DataTextField="Type" 
                    DataValueField="Type" Width="90px">
                  </asp:DropDownList>
                  <asp:SqlDataSource ID="SqlDataSourceProjectType" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="SELECT RTRIM([Type]) AS [Type] FROM [Table1_Project] GROUP BY [Type]">
                  </asp:SqlDataSource>
                </td>
                <td style="text-align: center; background-color: #F5F5F5;">
                  <asp:CheckBox ID="CheckBoxReporting" runat="server" 
                    Checked='<%# Bind("Report") %>' />
                </td>
                <td style="text-align: center; background-color: #F5F5F5;">
                  <asp:CheckBox ID="CheckBoxBackUp" runat="server" 
                    Checked='<%# Bind("BackUpRequired") %>' />
                </td>
                <td style="text-align: center; background-color: #F5F5F5;">
                  <asp:CheckBox ID="CheckBoxPOcreate" runat="server" 
                    Checked='<%# Bind("POcreate") %>' />
                </td>
                <td>
                  <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                    CommandName="Insert" CssClass="ButtonEdit" 
                    onmouseover="this.style.cursor='hand'" Text="Insert" />
                </td>
              </tr>
            </table>
          </asp:Panel>
        </InsertItemTemplate>
      </asp:FormView>
      <asp:SqlDataSource ID="SqlDataSourceInsertProject" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        InsertCommand="INSERT INTO [Table1_Project] ([ProjectID], [ProjectName], [CurrentStatus], [Type], [Report], [BackUpRequired], [POcreate]) VALUES (@ProjectID, @ProjectName, @CurrentStatus, @Type, @Report, @BackUpRequired, @POcreate)">
        <InsertParameters>
          <asp:Parameter Name="ProjectID" Type="Int16" />
          <asp:Parameter Name="ProjectName" Type="String" />
          <asp:Parameter Name="CurrentStatus" Type="Boolean" />
          <asp:Parameter Name="Type" Type="String" />
          <asp:Parameter Name="Report" Type="Boolean" />
          <asp:Parameter Name="BackUpRequired" Type="Boolean" />
          <asp:Parameter Name="POcreate" Type="Boolean" />
        </InsertParameters>
      </asp:SqlDataSource>
    </td>
  </tr>
</table>

<table width=100% >
 <tr>
  <td>
      <table width=100% >
                  <tr>
                          <td width=100%>
                                  <table align=center>
                                      <tr>
                                            <td >
                                              <asp:DropDownList ID="DropDownListProject" runat="server"  AutoPostBack="true"
                                                DataSourceID="SqlDataSourceProjectID" DataTextField="ProjectName" Font-Size="11px"
                                                DataValueField="ProjectID">
                                              </asp:DropDownList>
                                              <asp:SqlDataSource ID="SqlDataSourceProjectID" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                                                SelectCommand="SELECT     TOP (100) PERCENT dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName
                                                FROM         dbo.Table1_Project INNER JOIN
                                                                      dbo.Table_Prj_User_Junction ON dbo.Table1_Project.ProjectID = dbo.Table_Prj_User_Junction.ProjectID INNER JOIN
                                                                      dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId
                                                WHERE     (dbo.aspnet_Users.UserName = @UserName) AND (dbo.Table1_Project.Report = 1)
                                                ORDER BY ProjectName">
                                                <SelectParameters>
                                                  <asp:ControlParameter ControlID="TextBoxUserName" Name="UserName" 
                                                    PropertyName="Text" />
                                                </SelectParameters>
                                              </asp:SqlDataSource>
                                                <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="hidepanel" ></asp:TextBox>

                                            </td>
                                      </tr>
                                  </table>
                          </td>
                  </tr>
      </table>    
  </td>
 </tr>
 <tr>
  <td>
      <table width=100% >
                  <tr>
                          <td width=100%>
                                  <table align=center>
                                      <tr>
                                            <td >

  <asp:GridView ID="GridViewProjects" runat="server" AllowPaging="True" 
    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ProjectID" PagerSettings-Position="TopAndBottom"
    DataSourceID="SqlDataSourceProjects" EnableModelValidation="True" 
    CssClass="Grid" PageSize="10">
    <Columns>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral"
                        CommandName="Update" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                        CommandName="Edit" Text="Edit" ></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                    OnClientClick="return confirm('Are you sure you want to delete this record?');"
                    CommandName="Delete" Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

      <asp:TemplateField>
       <HeaderTemplate>
       </HeaderTemplate>
       <ItemTemplate>
        <table>
         <tr>
          <td style="width: 75px; background-color: #C0C0C0; color: #FFFFFF;">Project No
          </td>
          <td style="width: 200px; background-color: #C0C0C0; color: #FFFFFF;">Project Name
          </td>
          <td style="width: 60px; background-color: #C0C0C0; color: #FFFFFF;">Current Status
          </td>
          <td style="width: 70px; background-color: #C0C0C0; color: #FFFFFF;">Type
          </td>
          <td style="width: 70px; background-color: #C0C0C0; color: #FFFFFF;">Report
          </td>
          <td style="width: 70px; background-color: #C0C0C0; color: #FFFFFF;">BackUp Required
          </td>
          <td style="width: 70px; background-color: #C0C0C0; color: #FFFFFF;">PO Create
          </td>
          <td style="width: 120px; background-color: #C0C0C0; color: #FFFFFF;">Day Since Last Action
          </td>
          <td style="width: 120px; background-color: #C0C0C0; color: #FFFFFF;">Insurance Start
          </td>
          <td style="width: 70px; background-color: #C0C0C0; color: #FFFFFF;">Insurance Finish
          </td>
         </tr>
         <tr>
          <td style="width: 75px"><asp:Label ID="LabelProjectIDItem" runat="server" Text='<%# Bind("ProjectID") %>'></asp:Label>
          </td>
          <td style="width: 200px"><asp:Label ID="LabelProjectNameItem" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
          </td>
          <td style="width: 60px"> <asp:CheckBox ID="CheckBoxCurrentStatusItem" runat="server" 
            Checked='<%# Bind("CurrentStatus") %>' Visible="false" />
          <asp:Image ID="ImageCurrentStatus" runat="server" ImageUrl="~/Images/BlueDot60.png" />
          </td>
          <td style="width: 70px"><asp:Label ID="LabelTypeItem" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
          </td>
          <td style="width: 70px"><asp:CheckBox ID="CheckBoxReportRequiredItem" runat="server" Checked='<%# Bind("Report") %>' 
            Visible="false" />
          <asp:Image ID="ImageReport" runat="server" ImageUrl="~/Images/BlueDot60.png" />
          </td>
          <td style="width: 70px"><asp:CheckBox ID="CheckBoxBackUpRequiredItem" runat="server" 
            Checked='<%# Bind("BackUpRequired") %>' Visible="false" />
          <asp:Image ID="ImageBackUpRequired" runat="server" ImageUrl="~/Images/BlueDot60.png" />
          </td>
          <td style="width: 70px"><asp:CheckBox ID="CheckBoxPOcreateItem" runat="server" 
            Checked='<%# Bind("POCreate") %>' Visible="false" />
          <asp:Image ID="ImagePOcreate" runat="server" ImageUrl="~/Images/BlueDot60.png" />
          </td>
          <td style="width: 120px"><asp:Label ID="LabelDaySinceLastActionEdit" runat="server" Text='<%# Bind("DaySinceLastAction")%>'  ></asp:Label>
          </td>
          <td style="width: 120px"><asp:Label ID="LabelInsuranceStart" runat="server" Text='<%# Bind("InsuranceStart","{0:dd/MM/yyyy}")%>'  ></asp:Label>
          </td>
          <td style="width: 70px"><asp:Label ID="LabelInsuranceFinish" runat="server" Text='<%# Bind("InsuranceFinish","{0:dd/MM/yyyy}")%>'  ></asp:Label>
          </td>
         </tr>
         <tr>
          <td style="width: 75px">
              <asp:Image ID="ImageLogo" runat="server" ImageUrl='<%# Bind("Logo")%>'  />
          </td>
          <td style="width: 200px; background-color: #C0C0C0; color: #FFFFFF;">Total Cash In Inc. VAT
          </td>
          <td style="width: 60px">
          </td>
          <td style="width: 70px">
          </td>
          <td style="width: 70px">
          </td>
          <td style="width: 70px; background-color: #C0C0C0; color: #FFFFFF;">Completion Date
          </td>
          <td style="width: 70px; background-color: #C0C0C0; color: #FFFFFF;">Contract Currency
          </td>
          <td style="width: 120px; background-color: #C0C0C0; color: #FFFFFF;">Contract Amount Exc. VAT
          </td>
          <td style="width: 120px; background-color: #C0C0C0; color: #FFFFFF;">Current Amount Exc. VAT
          </td>
          <td style="width: 70px; background-color: #C0C0C0; color: #FFFFFF;">Margin
          </td>
         </tr>
         <tr style="line-height: 20px; height: 20px;">
          <td style="width: 75px">
          </td>
          <td style="width: 200px"><asp:Label ID="LabelTotalCashIn" runat="server" ></asp:Label>
                          <asp:DropDownList ID="DropDownListTotalCashIn" runat="server" 
                              DataSourceID="SqlDataSourceTotalCashIn" DataTextField="TotalCashIn"
                               DataValueField="TotalCashIn" CssClass="hidepanel"></asp:DropDownList>

                          <asp:SqlDataSource ID="SqlDataSourceTotalCashIn" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                          SelectCommand="SELECT SUM(CashInAmount) AS TotalCashIn FROM
                                         Table_ProjectCashIn
                                         WHERE ProjectID=@ProjectID">
                           <SelectParameters>
                            <asp:Parameter Name="ProjectID" Type="Int32" />
                           </SelectParameters>
                          </asp:SqlDataSource>
          </td>
          <td style="width: 60px">
          </td>
          <td style="width: 70px">
          </td>
          <td style="width: 70px">
          </td>
          <td style="width: 70px"><asp:Label ID="LabelCompletionDate" runat="server" Text='<%# Bind("CompletionDate","{0:dd/MM/yyyy}")%>'  ></asp:Label>
          </td>
          <td style="width: 70px"><asp:Label ID="LabelContractCurrency" runat="server" Text='<%# Bind("ContractCurrency")%>'  ></asp:Label>
          </td>
          <td style="width: 120px"><asp:Label ID="LabelContractAmount" runat="server" Text='<%# Bind("ContractAmount","{0:###,###,###.00}")%>'  ></asp:Label>
          </td>
          <td style="width: 120px"><asp:Label ID="LabelCurrentAmount" runat="server" ></asp:Label>
                          <asp:DropDownList ID="DropDownListCurrentAmount" runat="server" 
                              DataSourceID="SqlDataSourceCurrentAmount" DataTextField="CurrenctAmount"
                               DataValueField="CurrenctAmount" CssClass="hidepanel"></asp:DropDownList>

                          <asp:SqlDataSource ID="SqlDataSourceCurrentAmount" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                          SelectCommand="SELECT SUM(ContractAmount) AS CurrenctAmount FROM
                                        (
                                        SELECT     ContractAmount
                                        FROM         dbo.Table1_Project
                                        WHERE     (ProjectID = @ProjectID)

                                        UNION ALL

                                        SELECT     SUM(AddendumAmount) AS Expr1
                                        FROM         dbo.Table_ProjectAddendum
                                        WHERE     (ProjectID = @ProjectID)
                                        ) AS DataSource1">
                           <SelectParameters>
                            <asp:Parameter Name="ProjectID" Type="Int32" />
                           </SelectParameters>
                          </asp:SqlDataSource>
          </td>
          <td style="width: 70px"><asp:Label ID="LabelMargin" runat="server" Text='<%# Bind("Margin")%>'  ></asp:Label>
          </td>
         </tr>
        </table>
       </ItemTemplate>
       <EditItemTemplate>
       <table>
         <tr>
          <td style="width: 75px; background-color: #708090; color: #FFFFFF;">Project No
          </td>
          <td style="width: 200px; background-color: #708090; color: #FFFFFF;">Project Name
          </td>
          <td style="width: 60px; background-color: #708090; color: #FFFFFF;">Current Status
          </td>
          <td style="width: 70px; background-color: #708090; color: #FFFFFF;">Type
          </td>
          <td style="width: 70px; background-color: #708090; color: #FFFFFF;">Report
          </td>
          <td style="width: 70px; background-color: #708090; color: #FFFFFF;">BackUp Required
          </td>
          <td style="width: 70px; background-color: #708090; color: #FFFFFF;">PO Create
          </td>
          <td style="width: 120px; background-color: #708090; color: #FFFFFF;">Day Since Last Action
          </td>
          <td style="width: 120px; background-color: #708090; color: #FFFFFF;">Insurance Start
          </td>
          <td style="width: 70px; background-color: #708090; color: #FFFFFF;">Insurance Finish
          </td>
         </tr>
         <tr>
          <td style="width: 75px"><asp:TextBox ID="TextBoxProjectIDEdit" runat="server" Text='<%# Bind("ProjectID") %>' Width="70px" CssClass="TextBoxGeneralRev"></asp:TextBox>
          </td>
          <td style="width: 200px"><asp:TextBox ID="TextBoxProjectNameEdit" runat="server" Text='<%# Bind("ProjectName") %>' Width="180px" CssClass="TextBoxGeneralRev"></asp:TextBox>
          </td>
          <td style="width: 60px">          <asp:CheckBox ID="CheckBoxCurrentStatusEdit" runat="server" 
            Checked='<%# Bind("CurrentStatus") %>' />
          </td>
          <td style="width: 70px">                    <asp:DropDownList ID="DropDownListProjectType" runat="server" 
                        selectedvalue='<%# Bind("Type") %>' AppendDataBoundItems="True"  Width="60px"  Font-Size="11px"
                        DataSourceID="SqlDataSourceProjectType" DataTextField="Type" DataValueField="Type">
                    </asp:DropDownList>

                    <asp:SqlDataSource ID="SqlDataSourceProjectType" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="SELECT RTRIM([Type]) AS [Type] FROM [Table1_Project] GROUP BY [Type]">
                    </asp:SqlDataSource>
          </td>
          <td style="width: 70px"><asp:CheckBox ID="CheckBoxReportRequiredEdit" runat="server" Checked='<%# Bind("Report") %>' />
          </td>
          <td style="width: 70px"><asp:CheckBox ID="CheckBoxBackUpRequiredEdit" runat="server" 
            Checked='<%# Bind("BackUpRequired") %>' />
          </td>
          <td style="width: 70px"><asp:CheckBox ID="CheckBoxPOcreateEdit" runat="server" 
            Checked='<%# Bind("POCreate") %>' />
          </td>
          <td style="width: 120px"><asp:Label ID="LabelDaySinceLastActionItem" runat="server" Text='<%# Bind("DaySinceLastAction")%>'  ></asp:Label>
          </td>
          <td style="width: 120px"><asp:RegularExpressionValidator ID="RegularExpressionValidatorInsuranceStart" ControlToValidate="TextBoxInsuranceStart" Display="Dynamic"
                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" ></asp:RegularExpressionValidator>

                    <asp:TextBox ID="TextBoxInsuranceStart" runat="server"  CssClass="TextBoxGeneralRev add_datepicker"
                     Text='<%# Bind("InsuranceStart","{0:dd/MM/yyyy}")%>' Width="65px"></asp:TextBox>


          </td>
          <td style="width: 70px"><asp:RegularExpressionValidator ID="RegularExpressionValidatorInsuranceFinish" ControlToValidate="TextBoxInsuranceFinish" Display="Dynamic"
                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" ></asp:RegularExpressionValidator>

                    <asp:TextBox ID="TextBoxInsuranceFinish" runat="server"  CssClass="TextBoxGeneralRev add_datepicker"
                    Text='<%# Bind("InsuranceFinish","{0:dd/MM/yyyy}")%>'  Width="65px"></asp:TextBox>

          </td>
         </tr>
         <tr>
          <td style="width: 75px">
          </td>
          <td style="width: 200px; background-color: #708090; color: #FFFFFF;">Total Cash In Inc. VAT
          </td>
          <td style="width: 60px">
          </td>
          <td style="width: 70px">
          </td>
          <td style="width: 70px">
          </td>
          <td style="width: 70px; background-color: #708090; color: #FFFFFF;">Completion Date
          </td>
          <td style="width: 70px; background-color: #708090; color: #FFFFFF;">Contract Currency
          </td>
          <td style="width: 120px; background-color: #708090; color: #FFFFFF;">Contract Amount Exc. VAT
          </td>
          <td style="width: 120px; background-color: #708090; color: #FFFFFF;">Current Amount Exc. VAT
          </td>
          <td style="width: 70px; background-color: #708090; color: #FFFFFF;">Margin
          </td>
         </tr>
         <tr>
          <td style="width: 75px">
          </td>
          <td style="width: 200px"><asp:Label ID="LabelTotalCashInEdit" runat="server" ></asp:Label>
                          <asp:DropDownList ID="DropDownListTotalCashInEdit" runat="server" 
                              DataSourceID="SqlDataSourceTotalCashInEdit" DataTextField="TotalCashIn"
                               DataValueField="TotalCashIn" CssClass="hidepanel"></asp:DropDownList>

                          <asp:SqlDataSource ID="SqlDataSourceTotalCashInEdit" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                          SelectCommand="SELECT SUM(CashInAmount) AS TotalCashIn FROM
                                         Table_ProjectCashIn
                                         WHERE ProjectID=@ProjectID">
                           <SelectParameters>
                            <asp:Parameter Name="ProjectID" Type="Int32" />
                           </SelectParameters>
                          </asp:SqlDataSource>
          </td>
          <td style="width: 60px">
          </td>
          <td style="width: 70px">
          </td>
          <td style="width: 70px">
          </td>
          <td style="width: 70px"><asp:RegularExpressionValidator ID="RegularExpressionValidatorCompletetion" ControlToValidate="TextBoxCompletion" Display="Dynamic"
                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" ></asp:RegularExpressionValidator>

                    <asp:TextBox ID="TextBoxCompletion" runat="server"  CssClass="TextBoxGeneralRev add_datepicker"
                     Text='<%# Bind("CompletionDate","{0:dd/MM/yyyy}")%>'  Width="65px"></asp:TextBox>

          </td>
          <td style="width: 70px"><asp:DropDownList ID="DropDownListContractCurrency" runat="server"  Width="65px"
                AppendDataBoundItems="True"   
                selectedvalue='<%# Bind("ContractCurrency") %>' Font-Size="11px">
                <asp:ListItem Value="">-</asp:ListItem>
                <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                <asp:ListItem Value="Euro">Euro</asp:ListItem>
               </asp:DropDownList>
          </td>
          <td style="width: 120px"><asp:RegularExpressionValidator ID="RegularExpressionValidatorContractAmount" ControlToValidate="TextBoxContractAmount" Display="Dynamic"
               runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
              <asp:TextBox ID="TextBoxContractAmount" width="65px" runat="server" Text='<%# Bind("ContractAmount") %>'  CssClass="TextBoxGeneralRev"></asp:TextBox>
          </td>
          <td style="width: 120px"><asp:Label ID="LabelContractCurrentAmountEdit" runat="server" ></asp:Label>
                          <asp:DropDownList ID="DropDownListCurrentAmountEdit" runat="server" 
                              DataSourceID="SqlDataSourceCurrentAmountEdit" DataTextField="CurrenctAmount"
                               DataValueField="CurrenctAmount" CssClass="hidepanel"></asp:DropDownList>

                          <asp:SqlDataSource ID="SqlDataSourceCurrentAmountEdit" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                          SelectCommand="SELECT SUM(ContractAmount) AS CurrenctAmount FROM
                                        (
                                        SELECT     ContractAmount
                                        FROM         dbo.Table1_Project
                                        WHERE     (ProjectID = @ProjectID)

                                        UNION ALL

                                        SELECT     SUM(AddendumAmount) AS Expr1
                                        FROM         dbo.Table_ProjectAddendum
                                        WHERE     (ProjectID = @ProjectID)
                                        ) AS DataSource1">
                           <SelectParameters>
                            <asp:Parameter Name="ProjectID" Type="Int32" />
                           </SelectParameters>
                          </asp:SqlDataSource>
          </td>
          <td style="width: 70px"><asp:TextBox ID="TextBoxMargin" runat="server"  Width="65px"
               Text='<%# Bind("Margin") %>'  CssClass="TextBoxGeneralRev"></asp:TextBox>
              <asp:RegularExpressionValidator ID="RegularExpressionMargin" ControlToValidate="TextBoxMargin" Display="Dynamic"
               runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
          </td>
         </tr>
         <tr style="border-color: #C0C0C0; border-top-style: solid; border-top-width: 4px; ">
          <td colspan="5">
          <table>
          <tr>
           <td>
            <asp:FormView ID="FormViewInsertAddendum" runat="server" DataKeyNames="AddendumID"  
              DataSourceID="SqlDataSourceInsertAddendum" EnableModelValidation="True" 
               DefaultMode="Insert" oniteminserted="FormViewInsertAddendum_ItemInserted" >
              <InsertItemTemplate>
               <asp:Panel runat="server" ID="pnlLogin" DefaultButton="InsertButton">
                  <table>
                    <tr style="background-color: #F08080; color: #FFFFFF">
                    <td style="width: 200px; text-align: center;">
                          Addendum Description
                    </td>
                    <td style="width: 70px; text-align: center;">
                          Addendum Date
                    </td>
                    <td style="width: 70px; text-align: center;">
                          Addendum Amount Exc. VAT
                    </td>
                    <td>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 200px; text-align: center; background-color: #F5F5F5;">
                      <asp:TextBox ID="TextBoxAddendumDescription" runat="server"  Width="150px"
                        Text='<%# Bind("AddendumDescription") %>'  CssClass="TextBoxGeneralRev"/>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddendumDescription" runat="server"  ValidationGroup="InsertAddendum"
                        ErrorMessage="!" ControlToValidate="TextBoxAddendumDescription" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 70px;  text-align: center; background-color: #F5F5F5;">
                      <asp:RegularExpressionValidator ID="RegularExpressionValidatorAddendumDate" ControlToValidate="TextBoxAddendumDate" Display="Dynamic"
                      runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                      ValidationGroup="InsertAddendum"></asp:RegularExpressionValidator>

                      <asp:TextBox ID="TextBoxAddendumDate" runat="server"  CssClass="TextBoxGeneralRev add_datepicker"
                       Text='<%# Bind("AddendumDate","{0:dd/MM/yyyy}")%>'  Width="65px"></asp:TextBox>

                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddendumDate" runat="server"  ValidationGroup="InsertAddendum"
                        ErrorMessage="!" ControlToValidate="TextBoxAddendumDate" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 70px; text-align: center; background-color: #F5F5F5;">
                      <asp:TextBox ID="TextBoxAddendumAmount" runat="server"  Width="65px"
                        Text='<%# Bind("AddendumAmount") %>'  CssClass="TextBoxGeneralRev"/>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddendumAmount" runat="server"  ValidationGroup="InsertAddendum"
                        ErrorMessage="!" ControlToValidate="TextBoxAddendumAmount" Display="Dynamic"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidatorAddendumAmount" ControlToValidate="TextBoxAddendumAmount" Display="Dynamic" ValidationGroup="InsertAddendum"
                        runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                     <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                      CommandName="Insert" Text="Insert" CssClass="ButtonEdit" 
                        onmouseover="this.style.cursor='hand'" onclick="InsertButton_Click" ValidationGroup="InsertAddendum" />
                    </td>
                  </tr>
                  </table>
               </asp:Panel>
              </InsertItemTemplate>
            </asp:FormView>
            <asp:SqlDataSource ID="SqlDataSourceInsertAddendum" runat="server" 
              ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>">
            </asp:SqlDataSource>
           </td>
          </tr>
          <tr>
           <td>
             <asp:GridView ID="GridViewAddendum" runat="server" 
               DataSourceID="SqlDataSourceAddendum" EnableModelValidation="True" 
               AutoGenerateColumns="False" DataKeyNames="AddendumID" CssClass="Grid" 
                   onrowcommand="GridViewAddendum_RowCommand" 
                   onrowupdating="GridViewAddendum_RowUpdating" 
               onrowdeleted="GridViewAddendum_RowDeleted" 
               onrowupdated="GridViewAddendum_RowUpdated">
               <Columns>
                  <asp:TemplateField ShowHeader="False">
                      <EditItemTemplate>
                          <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral" ValidationGroup="EditAddendum"
                              CommandName="Update" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" Text="Update"></asp:LinkButton>
                          <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                              CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                              CommandName="Edit" Text="Edit"></asp:LinkButton>
                          <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                          OnClientClick="return confirm('Are you sure you want to delete this record?');"
                          CommandName="Delete" Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>
                      </ItemTemplate>
                  </asp:TemplateField>                 
                 <asp:TemplateField HeaderText="Addendum Description" 
                   SortExpression="AddendumDescription" HeaderStyle-Width="150px" ControlStyle-Width="150px">
                   <EditItemTemplate>
                     <asp:TextBox ID="TextBox2" runat="server" Width="130px"
                       Text='<%# Bind("AddendumDescription") %>'></asp:TextBox>
                   </EditItemTemplate>
                   <ItemTemplate>
                     <asp:Label ID="Label3" runat="server" Text='<%# Bind("AddendumDescription") %>'></asp:Label>
                   </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Addendum Date" SortExpression="AddendumDate" HeaderStyle-Width="70px" ControlStyle-Width="70px">
                   <EditItemTemplate>
                     <asp:TextBox ID="TextBoxAddendumDate" runat="server" Text='<%# Bind("AddendumDate","{0:dd/MM/yyyy}") %>' Width="65px" CssClass="add_datepicker"></asp:TextBox>

                      <asp:RegularExpressionValidator ID="RegularExpressionValidatorAddendumDate" ControlToValidate="TextBoxAddendumDate" Display="Dynamic"
                      runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                      ValidationGroup="EditAddendum"></asp:RegularExpressionValidator>

                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddendumDate" runat="server"  ValidationGroup="EditAddendum"
                        ErrorMessage="!" ControlToValidate="TextBoxAddendumDate" Display="Dynamic"></asp:RequiredFieldValidator>

                   </EditItemTemplate>
                   <ItemTemplate>
                     <asp:Label ID="Label4" runat="server" Text='<%# Bind("AddendumDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                   </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Addendum Amount" SortExpression="AddendumAmount" HeaderStyle-Width="70px" ControlStyle-Width="70px">
                   <EditItemTemplate>
                     <asp:TextBox ID="TextBoxAddendumAmount" runat="server" Text='<%# Bind("AddendumAmount") %>' Width="65px"></asp:TextBox>

                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddendumAmount" runat="server"  ValidationGroup="EditAddendum"
                        ErrorMessage="!" ControlToValidate="TextBoxAddendumAmount" Display="Dynamic"></asp:RequiredFieldValidator>

                      <asp:RegularExpressionValidator ID="RegularExpressionValidatorAddendumAmount" ControlToValidate="TextBoxAddendumAmount" Display="Dynamic"
                        runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"
                        ValidationGroup="EditAddendum"></asp:RegularExpressionValidator>

                   </EditItemTemplate>
                   <ItemTemplate>
                     <asp:Label ID="Label5" runat="server" Text='<%# Bind("AddendumAmount","{0:###,###,###.00}") %>'></asp:Label>
                   </ItemTemplate>
                 </asp:TemplateField>
               </Columns>
              <RowStyle  CssClass="GridItemNakladnaya" />
              <HeaderStyle  CssClass="GridHeader" />
             </asp:GridView>
             <asp:SqlDataSource ID="SqlDataSourceAddendum" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
               SelectCommand="SELECT [AddendumID]
                          ,[ProjectID]
                          ,RTRIM([AddendumDescription]) AS AddendumDescription
                          ,[AddendumDate]
                          ,[AddendumAmount]
                      FROM [Table_ProjectAddendum]
                      WHERE ProjectID = @ProjectID"
               UpdateCommand = "UPDATE [Table_ProjectAddendum]
                                 SET [ProjectID] = @ProjectID
                                    ,[AddendumDescription] = @AddendumDescription
                                    ,[AddendumDate] = @AddendumDate
                                    ,[AddendumAmount] = @AddendumAmount
                               WHERE AddendumID = @AddendumID"
               DeleteCommand = "DELETE FROM [Table_ProjectAddendum] WHERE AddendumID = @AddendumID">
               <SelectParameters>
                <asp:Parameter Name="ProjectID" Type="Int32" />
               </SelectParameters>
             </asp:SqlDataSource>
           </td>
          </tr>
          </table>

          </td>
          <td colspan="5">

          <table>
          <tr>
           <td>
            <asp:FormView ID="FormViewInsertForecast" runat="server" DataKeyNames="ForecastID"  
              DataSourceID="SqlDataSourceInsertForecast" EnableModelValidation="True" 
               DefaultMode="Insert" oniteminserted="FormViewInsertForecast_ItemInserted">
              <InsertItemTemplate>
               <asp:Panel runat="server" ID="pnlLogin" DefaultButton="InsertButton">
                  <table>
                    <tr style="background-color: #F08080; color: #FFFFFF">
                    <td style="width: 70px; text-align: center;">
                          ForecastType
                    </td>
                    <td style="width: 100px; text-align: center;">
                          Forecast Year Month
                    </td>
                    <td style="width: 70px; text-align: center;">
                          Forecast Amount Exc. VAT
                    </td>
                    <td>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 70px; text-align: center; background-color: #F5F5F5;">
                       <asp:DropDownList ID="DropDownListForecastType" runat="server"  Width="65px"  Font-Size="11px"
                        selectedvalue='<%# Bind("ForecastType") %>' DataSourceID="SqlDataSourceForecastType"
                         DataValueField="ForecastTypeID" DataTextField="ForecastTypeDescription">
                       </asp:DropDownList>

                        <asp:SqlDataSource ID="SqlDataSourceForecastType" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                          SelectCommand="SELECT ForecastTypeID, RTRIM(ForecastTypeDescription) AS ForecastTypeDescription
                          FROM Table_ForecastType">
                        </asp:SqlDataSource>
                    </td>
                    <td style="width: 100px;  text-align: center; background-color: #F5F5F5;">

                     <asp:DropDownList ID="DropDownListForecastYearMonth" runat="server"  Width="90px"
                        selectedvalue='<%# Bind("ForecastYearMonth") %>'  Font-Size="11px">
                        <asp:ListItem Value="X">Select</asp:ListItem>
                        <asp:ListItem Value="201001">2010-JAN</asp:ListItem>
                        <asp:ListItem Value="201002">2010-FEB</asp:ListItem>
                        <asp:ListItem Value="201003">2010-MARCH</asp:ListItem>
                        <asp:ListItem Value="201004">2010-APRIL</asp:ListItem>
                        <asp:ListItem Value="201005">2010-MAY</asp:ListItem>
                        <asp:ListItem Value="201006">2010-JUNE</asp:ListItem>
                        <asp:ListItem Value="201007">2010-JULY</asp:ListItem>
                        <asp:ListItem Value="201008">2010-AUG</asp:ListItem>
                        <asp:ListItem Value="201009">2010-SEP</asp:ListItem>
                        <asp:ListItem Value="201010">2010-OCT</asp:ListItem>
                        <asp:ListItem Value="201011">2010-NOV</asp:ListItem>
                        <asp:ListItem Value="201012">2010-DEC</asp:ListItem>
                        <asp:ListItem Value="201101">2011-JAN</asp:ListItem>
                        <asp:ListItem Value="201102">2011-FEB</asp:ListItem>
                        <asp:ListItem Value="201103">2011-MARCH</asp:ListItem>
                        <asp:ListItem Value="201104">2011-APRIL</asp:ListItem>
                        <asp:ListItem Value="201105">2011-MAY</asp:ListItem>
                        <asp:ListItem Value="201106">2011-JUNE</asp:ListItem>
                        <asp:ListItem Value="201107">2011-JULY</asp:ListItem>
                        <asp:ListItem Value="201108">2011-AUG</asp:ListItem>
                        <asp:ListItem Value="201109">2011-SEP</asp:ListItem>
                        <asp:ListItem Value="201110">2011-OCT</asp:ListItem>
                        <asp:ListItem Value="201111">2011-NOV</asp:ListItem>
                        <asp:ListItem Value="201112">2011-DEC</asp:ListItem>
                        <asp:ListItem Value="201201">2012-JAN</asp:ListItem>
                        <asp:ListItem Value="201202">2012-FEB</asp:ListItem>
                        <asp:ListItem Value="201203">2012-MARCH</asp:ListItem>
                        <asp:ListItem Value="201204">2012-APRIL</asp:ListItem>
                        <asp:ListItem Value="201205">2012-MAY</asp:ListItem>
                        <asp:ListItem Value="201206">2012-JUNE</asp:ListItem>
                        <asp:ListItem Value="201207">2012-JULY</asp:ListItem>
                        <asp:ListItem Value="201208">2012-AUG</asp:ListItem>
                        <asp:ListItem Value="201209">2012-SEP</asp:ListItem>
                        <asp:ListItem Value="201210">2012-OCT</asp:ListItem>
                        <asp:ListItem Value="201211">2012-NOV</asp:ListItem>
                        <asp:ListItem Value="201212">2012-DEC</asp:ListItem>
                     </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidatorForecastYearMonth" runat="server" ValidationGroup="InsertForecast"
                                ControlToValidate="DropDownListForecastYearMonth" Display="Dynamic"
                                CssClass="LabelGeneral" ErrorMessage="Required" Operator="NotEqual" 
                                ValueToCompare="X"></asp:CompareValidator>
                    </td>
                    <td style="width: 70px; text-align: center; background-color: #F5F5F5;">
                      <asp:TextBox ID="TextBoxForecastAmount" runat="server"  Width="65px" CssClass="TextBoxGeneralRev"
                        Text='<%# Bind("ForecastAmount") %>' />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorForecastAmount" runat="server"  ValidationGroup="InsertForecast"
                        ErrorMessage="!" ControlToValidate="TextBoxForecastAmount" Display="Dynamic"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidatorForecastAmount" ControlToValidate="TextBoxForecastAmount" Display="Dynamic" ValidationGroup="InsertForecast"
                        runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                     <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                      CommandName="Insert" Text="Insert" CssClass="ButtonEdit" 
                        onmouseover="this.style.cursor='hand'" ValidationGroup="InsertForecast" 
                        onclick="InsertButton_Click1" />
                    </td>
                  </tr>
                  </table>
               </asp:Panel>
              </InsertItemTemplate>
            </asp:FormView>
            <asp:SqlDataSource ID="SqlDataSourceInsertForecast" runat="server" 
              ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>">
            </asp:SqlDataSource>
           </td>
          </tr>
          <tr>
           <td>
             <asp:GridView ID="GridViewForecast" runat="server" 
               DataSourceID="SqlDataSourceForecastEdit" EnableModelValidation="True" 
               AutoGenerateColumns="False" DataKeyNames="ForecastID" CssClass="Grid" 
                   onrowcommand="GridViewForecast_RowCommand" 
                   onrowupdating="GridViewForecast_RowUpdating">
               <Columns>
                  <asp:TemplateField ShowHeader="False">
                      <EditItemTemplate>
                          <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral" ValidationGroup="EditForecast"
                              CommandName="Update" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" Text="Update"></asp:LinkButton>
                          <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                              CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                              CommandName="Edit" Text="Edit"></asp:LinkButton>
                          <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                          OnClientClick="return confirm('Are you sure you want to delete this record?');"
                          CommandName="Delete" Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>
                      </ItemTemplate>
                  </asp:TemplateField>                 
                 <asp:TemplateField HeaderText="Forecast Type" 
                   SortExpression="ForecastType" HeaderStyle-Width="120px" ControlStyle-Width="120px">
                   <EditItemTemplate>
                       <asp:DropDownList ID="DropDownListForecastType" runat="server" Width="100px"  Font-Size="11px"
                        selectedvalue='<%# Bind("ForecastType") %>' DataSourceID="SqlDataSourceForecastType"
                         DataValueField="ForecastTypeID" DataTextField="ForecastTypeDescription"
                         AppendDataBoundItems="True">
                       </asp:DropDownList>

                        <asp:SqlDataSource ID="SqlDataSourceForecastType" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                          SelectCommand="SELECT ForecastTypeID, RTRIM(ForecastTypeDescription) AS ForecastTypeDescription
                          FROM Table_ForecastType">
                        </asp:SqlDataSource>
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:DropDownList ID="DropDownListForecastType" runat="server"  Enabled="false" Width="100px"
                        selectedvalue='<%# Bind("ForecastType") %>' DataSourceID="SqlDataSourceForecastType"  Font-Size="11px"
                         DataValueField="ForecastTypeID" DataTextField="ForecastTypeDescription"
                         AppendDataBoundItems="True">
                       </asp:DropDownList>

                        <asp:SqlDataSource ID="SqlDataSourceForecastType" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>"
                          SelectCommand="SELECT ForecastTypeID, RTRIM(ForecastTypeDescription) AS ForecastTypeDescription
                          FROM Table_ForecastType">
                        </asp:SqlDataSource>
                   </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Forecast Year Month" SortExpression="ForecastYearMonth" HeaderStyle-Width="100px" ControlStyle-Width="100px">
                   <EditItemTemplate>
                     <asp:DropDownList ID="DropDownListForecastYearMonth" runat="server"  Width="90px" 
                        selectedvalue='<%# Bind("ForecastYearMonth") %>' AppendDataBoundItems="True" Font-Size="11px">
                        <asp:ListItem Value="201102">2011-FEB</asp:ListItem>
                        <asp:ListItem Value="201103">2011-MARCH</asp:ListItem>
                        <asp:ListItem Value="201104">2011-APRIL</asp:ListItem>
                        <asp:ListItem Value="201105">2011-MAY</asp:ListItem>
                        <asp:ListItem Value="201106">2011-JUNE</asp:ListItem>
                        <asp:ListItem Value="201107">2011-JULY</asp:ListItem>
                        <asp:ListItem Value="201108">2011-AUG</asp:ListItem>
                        <asp:ListItem Value="201109">2011-SEP</asp:ListItem>
                        <asp:ListItem Value="201110">2011-OCT</asp:ListItem>
                        <asp:ListItem Value="201111">2011-NOV</asp:ListItem>
                        <asp:ListItem Value="201112">2011-DEC</asp:ListItem>
                        <asp:ListItem Value="201201">2012-JAN</asp:ListItem>
                        <asp:ListItem Value="201202">2012-FEB</asp:ListItem>
                        <asp:ListItem Value="201203">2012-MARCH</asp:ListItem>
                        <asp:ListItem Value="201204">2012-APRIL</asp:ListItem>
                        <asp:ListItem Value="201205">2012-MAY</asp:ListItem>
                        <asp:ListItem Value="201206">2012-JUNE</asp:ListItem>
                        <asp:ListItem Value="201207">2012-JULY</asp:ListItem>
                        <asp:ListItem Value="201208">2012-AUG</asp:ListItem>
                        <asp:ListItem Value="201209">2012-SEP</asp:ListItem>
                        <asp:ListItem Value="201210">2012-OCT</asp:ListItem>
                        <asp:ListItem Value="201211">2012-NOV</asp:ListItem>
                        <asp:ListItem Value="201212">2012-DEC</asp:ListItem>
                     </asp:DropDownList>
                   </EditItemTemplate>
                   <ItemTemplate>
                     <asp:DropDownList ID="DropDownListForecastYearMonth" runat="server"  Width="90px" Enabled="false"
                        selectedvalue='<%# Bind("ForecastYearMonth") %>' AppendDataBoundItems="True"  Font-Size="11px">
                        <asp:ListItem Value="201102">2011-FEB</asp:ListItem>
                        <asp:ListItem Value="201103">2011-MARCH</asp:ListItem>
                        <asp:ListItem Value="201104">2011-APRIL</asp:ListItem>
                        <asp:ListItem Value="201105">2011-MAY</asp:ListItem>
                        <asp:ListItem Value="201106">2011-JUNE</asp:ListItem>
                        <asp:ListItem Value="201107">2011-JULY</asp:ListItem>
                        <asp:ListItem Value="201108">2011-AUG</asp:ListItem>
                        <asp:ListItem Value="201109">2011-SEP</asp:ListItem>
                        <asp:ListItem Value="201110">2011-OCT</asp:ListItem>
                        <asp:ListItem Value="201111">2011-NOV</asp:ListItem>
                        <asp:ListItem Value="201112">2011-DEC</asp:ListItem>
                        <asp:ListItem Value="201201">2012-JAN</asp:ListItem>
                        <asp:ListItem Value="201202">2012-FEB</asp:ListItem>
                        <asp:ListItem Value="201203">2012-MARCH</asp:ListItem>
                        <asp:ListItem Value="201204">2012-APRIL</asp:ListItem>
                        <asp:ListItem Value="201205">2012-MAY</asp:ListItem>
                        <asp:ListItem Value="201206">2012-JUNE</asp:ListItem>
                        <asp:ListItem Value="201207">2012-JULY</asp:ListItem>
                        <asp:ListItem Value="201208">2012-AUG</asp:ListItem>
                        <asp:ListItem Value="201209">2012-SEP</asp:ListItem>
                        <asp:ListItem Value="201210">2012-OCT</asp:ListItem>
                        <asp:ListItem Value="201211">2012-NOV</asp:ListItem>
                        <asp:ListItem Value="201212">2012-DEC</asp:ListItem>
                     </asp:DropDownList>
                   </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Forecast Amount Exc. VAT" SortExpression="ForecastAmount" HeaderStyle-Width="70px" ControlStyle-Width="70px">
                   <EditItemTemplate>
                     <asp:TextBox ID="TextBoxForecastAmount" runat="server" Text='<%# Bind("ForecastAmount") %>' Width="65px"  CssClass="TextBoxGeneralRev"></asp:TextBox>

                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorForecastAmount" runat="server"  ValidationGroup="EditForecast"
                        ErrorMessage="!" ControlToValidate="TextBoxForecastAmount" Display="Dynamic"></asp:RequiredFieldValidator>

                      <asp:RegularExpressionValidator ID="RegularExpressionValidatorForecastAmount" ControlToValidate="TextBoxForecastAmount" Display="Dynamic"
                        runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"
                        ValidationGroup="EditForecast"></asp:RegularExpressionValidator>

                   </EditItemTemplate>
                   <ItemTemplate>
                     <asp:Label ID="Label5" runat="server" Text='<%# Bind("ForecastAmount","{0:###,###,###.00}") %>'></asp:Label>
                   </ItemTemplate>
                 </asp:TemplateField>
               </Columns>
              <RowStyle  CssClass="GridItemNakladnaya" />
              <HeaderStyle  CssClass="GridHeader" />
             </asp:GridView>
             <asp:SqlDataSource ID="SqlDataSourceForecastEdit" runat="server" 
               ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
               SelectCommand="SELECT [ForecastID]
                          ,[ProjectID]
                          ,RTRIM([ForecastType]) AS ForecastType
                          ,[ForecastYearMonth]
                          ,[ForecastAmount]
                      FROM [Table_ProjectForecast]
                      WHERE ProjectID = @ProjectID"
               UpdateCommand = "UPDATE [Table_ProjectForecast]
                                 SET [ProjectID] = @ProjectID
                                    ,[ForecastType] = @ForecastType
                                    ,[ForecastYearMonth] = @ForecastYearMonth
                                    ,[ForecastAmount] = @ForecastAmount
                               WHERE ForecastID = @ForecastID"
               DeleteCommand = "DELETE FROM [Table_ProjectForecast] WHERE ForecastID = @ForecastID">
               <SelectParameters>
                <asp:Parameter Name="ProjectID" Type="Int32" />
               </SelectParameters>
             </asp:SqlDataSource>
           </td>
          </tr>
          </table>
          </td>
         </tr>
         <tr style="border-color: #C0C0C0; border-top-style: solid; border-top-width: 4px; ">
           <td colspan="10">
              <table>
                        <tr>
                         <td>
                          <asp:FormView ID="FormViewCashIn" runat="server" DataKeyNames="CashInID"  
                            DataSourceID="SqlDataSourceInsertCashIn" EnableModelValidation="True" 
                             DefaultMode="Insert" oniteminserted="FormViewInsertCashIn_ItemInserted" >
                            <InsertItemTemplate>
                             <asp:Panel runat="server" ID="pnlLogin" DefaultButton="InsertButton">
                                <table>
                                  <tr style="background-color: #F08080; color: #FFFFFF">
                                  <td style="width: 200px; text-align: center;">
                                        Cash In Description
                                  </td>
                                  <td style="width: 70px; text-align: center;">
                                        Cash In Date
                                  </td>
                                  <td style="width: 70px; text-align: center;">
                                        Cash In Amount Inc. VAT
                                  </td>
                                  <td>
                                  </td>
                                </tr>
                                <tr>
                                  <td style="width: 200px; text-align: center; background-color: #F5F5F5;">
                                    <asp:TextBox ID="TextBoxCashInDescription" runat="server"  Width="150px"
                                      Text='<%# Bind("CashInDescription") %>'  CssClass="TextBoxGeneralRev"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCashInDescription" runat="server"  ValidationGroup="InsertCashIn"
                                      ErrorMessage="!" ControlToValidate="TextBoxCashInDescription" Display="Dynamic"></asp:RequiredFieldValidator>
                                  </td>
                                  <td style="width: 70px;  text-align: center; background-color: #F5F5F5;">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorCashInDate" ControlToValidate="TextBoxCashInDate" Display="Dynamic"
                                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                                    ValidationGroup="InsertCashIn"></asp:RegularExpressionValidator>

                                    <asp:TextBox ID="TextBoxCashInDate" runat="server"  CssClass="TextBoxGeneralRev add_datepicker"
                                     Text='<%# Bind("CashInDate","{0:dd/MM/yyyy}")%>'  Width="65px"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCashInDate" runat="server"  ValidationGroup="InsertCashIn"
                                      ErrorMessage="!" ControlToValidate="TextBoxCashInDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                  </td>
                                  <td style="width: 70px; text-align: center; background-color: #F5F5F5;">
                                    <asp:TextBox ID="TextBoxCashInAmount" runat="server"  Width="65px"
                                      Text='<%# Bind("CashInAmount") %>'  CssClass="TextBoxGeneralRev"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCashInAmount" runat="server"  ValidationGroup="InsertCashIn"
                                      ErrorMessage="!" ControlToValidate="TextBoxCashInAmount" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorCashInAmount" ControlToValidate="TextBoxCashInAmount" Display="Dynamic" ValidationGroup="InsertCashIn"
                                      runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"></asp:RegularExpressionValidator>
                                  </td>
                                  <td>
                                   <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                                    CommandName="Insert" Text="Insert" CssClass="ButtonEdit" 
                                      onmouseover="this.style.cursor='hand'" onclick="InsertButton_ClickCashIn" ValidationGroup="InsertCashIn" />
                                  </td>
                                </tr>
                                </table>
                             </asp:Panel>
                            </InsertItemTemplate>
                          </asp:FormView>
                          <asp:SqlDataSource ID="SqlDataSourceInsertCashIn" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>">
                          </asp:SqlDataSource>
                         </td>
                        </tr>
                        <tr>
                         <td>
                           <asp:GridView ID="GridViewCashIn" runat="server"
                             DataSourceID="SqlDataSourceCashIn" EnableModelValidation="True" 
                             AutoGenerateColumns="False" DataKeyNames="CashInID" CssClass="Grid" 
                                 onrowcommand="GridViewCashIn_RowCommand" 
                                 onrowupdating="GridViewCashIn_RowUpdating" 
                             onrowdeleted="GridViewCashIn_RowDeleted" 
                             onrowupdated="GridViewCashIn_RowUpdated">
                             <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True"  CssClass="LabelGeneral" ValidationGroup="EditCashIn"
                                            CommandName="Update" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" Text="Update"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False"  CssClass="LabelGeneral"
                                            CommandName="Edit" Text="Edit"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonDelete" Runat="server" 
                                        OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                        CommandName="Delete" Text="Delete" CssClass="LabelGeneral"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>                 
                               <asp:TemplateField HeaderText="Cash In Description" 
                                 SortExpression="CashInDescription" HeaderStyle-Width="150px" ControlStyle-Width="150px">
                                 <EditItemTemplate>
                                   <asp:TextBox ID="TextBoxCashInDescription" runat="server" Width="130px"
                                     Text='<%# Bind("CashInDescription") %>'></asp:TextBox>
                                 </EditItemTemplate>
                                 <ItemTemplate>
                                   <asp:Label ID="LabelCashInDescription" runat="server" Text='<%# Bind("CashInDescription") %>'></asp:Label>
                                 </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Cash In Date" SortExpression="CashInDate" HeaderStyle-Width="70px" ControlStyle-Width="70px">
                                 <EditItemTemplate>
                                   <asp:TextBox ID="TextBoxCashInDate" runat="server" Text='<%# Bind("CashInDate","{0:dd/MM/yyyy}") %>' Width="65px" CssClass="add_datepicker"></asp:TextBox>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorCashInDate" ControlToValidate="TextBoxCashInDate" Display="Dynamic"
                                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                                    ValidationGroup="EditCashIn"></asp:RegularExpressionValidator>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCashInDate" runat="server"  ValidationGroup="EditCashIn"
                                      ErrorMessage="!" ControlToValidate="TextBoxCashInDate" Display="Dynamic"></asp:RequiredFieldValidator>

                                 </EditItemTemplate>
                                 <ItemTemplate>
                                   <asp:Label ID="LabelCashInDate" runat="server" Text='<%# Bind("CashInDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                 </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Cash In Amount" SortExpression="CashInAmount" HeaderStyle-Width="70px" ControlStyle-Width="70px">
                                 <EditItemTemplate>
                                   <asp:TextBox ID="TextBoxCashInAmount" runat="server" Text='<%# Bind("CashInAmount") %>' Width="65px"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCashInAmount" runat="server"  ValidationGroup="EditCashIn"
                                      ErrorMessage="!" ControlToValidate="TextBoxCashInAmount" Display="Dynamic"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorCashInAmount" ControlToValidate="TextBoxCashInAmount" Display="Dynamic"
                                      runat="server" ErrorMessage="Wrong format"  ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)"
                                      ValidationGroup="EditCashIn"></asp:RegularExpressionValidator>

                                 </EditItemTemplate>
                                 <ItemTemplate>
                                   <asp:Label ID="LabelCashInAmount" runat="server" Text='<%# Bind("CashInAmount","{0:###,###,###.00}") %>'></asp:Label>
                                 </ItemTemplate>
                               </asp:TemplateField>
                             </Columns>
                            <RowStyle  CssClass="GridItemNakladnaya" />
                            <HeaderStyle  CssClass="GridHeader" />
                           </asp:GridView>
                           <asp:SqlDataSource ID="SqlDataSourceCashIn" runat="server" 
                             ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                             SelectCommand="SELECT [CashInID]
                                        ,[ProjectID]
                                        ,RTRIM([CashInDescription]) AS CashInDescription
                                        ,[CashInDate]
                                        ,[CashInAmount]
                                    FROM [Table_ProjectCashIn]
                                    WHERE ProjectID = @ProjectID
				    ORDER BY CashInID"
                             UpdateCommand = "UPDATE [Table_ProjectCashIn]
                                               SET [ProjectID] = @ProjectID
                                                  ,[CashInDescription] = @CashInDescription
                                                  ,[CashInDate] = @CashInDate
                                                  ,[CashInAmount] = @CashInAmount
                                             WHERE CashInID = @CashInID"
                             DeleteCommand = "DELETE FROM [Table_ProjectCashIn] WHERE CashInID = @CashInID">
                             <SelectParameters>
                              <asp:Parameter Name="ProjectID" Type="Int32" />
                             </SelectParameters>
                           </asp:SqlDataSource>
                         </td>
                        </tr>
                        </table>
           </td>
         </tr>
       </table>
       </EditItemTemplate>
      </asp:TemplateField>

    </Columns>

<PagerSettings Position="TopAndBottom"></PagerSettings>
        <pagerstyle  horizontalalign="Center" CssClass="pager" />
        <RowStyle  CssClass="GridItemNakladnaya" />
        <HeaderStyle  CssClass="GridHeader" />
        <PagerStyle CssClass="pager" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceProjects" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT [ProjectID]
,Rtrim([ProjectName]) AS ProjectName
,[CurrentStatus]
, Rtrim([Type]) AS Type
, [Report]
, [BackUpRequired]
, [InsuranceStart]
, [InsuranceFinish]
, [POcreate], [CompletionDate], RTRIM([ContractCurrency]) AS ContractCurrency, [ContractAmount], [Margin]
, (case when t1.DaySinceLastAction is null then -1 else t1.DaySinceLastAction end) as DaySinceLastAction
,RTRIM(Logo) AS Logo
 FROM [Table1_Project] LEFT OUTER JOIN
 (
SELECT  dbo.Table1_Project.ProjectID AS ProjectIDD ,DATEDIFF(day, 
                      CASE WHEN maxpo_date > maxpaymentdate THEN maxpo_date WHEN maxpo_date < maxpaymentdate THEN maxpaymentdate WHEN maxpo_date = maxpaymentdate
                       THEN maxpaymentdate END, GETDATE()) AS DaySinceLastAction
FROM         dbo.Table1_Project LEFT OUTER JOIN
                      View_SIL_MinPOdatePerProject ON dbo.Table1_Project.ProjectID = View_SIL_MinPOdatePerProject.ProjectID LEFT OUTER JOIN
                      View_SILmAxPaymentDatePerProject ON dbo.Table1_Project.ProjectID = View_SILmAxPaymentDatePerProject.ProjectID LEFT OUTER JOIN
                      View_SILMaxPoDatePerProject ON dbo.Table1_Project.ProjectID = View_SILMaxPoDatePerProject.ProjectID
WHERE     (View_SIL_MinPOdatePerProject.MinPO_Date IS NOT NULL)
 ) as t1 ON dbo.Table1_Project.ProjectID = t1.ProjectIDD
 WHERE ProjectID = @ProjectID"
    DeleteCommand="DELETE FROM [Table1_Project] WHERE ProjectID = @ProjectID">
  <SelectParameters>
          <asp:ControlParameter ControlID="DropDownListProject" Name="ProjectID" 
            PropertyName="SelectedValue" />
  </SelectParameters>
  </asp:SqlDataSource>
                                            </td>
                                      </tr>
                                  </table>
                          </td>
                  </tr>
      </table>    
  </td>
 </tr>
</table>

</asp:Content>

