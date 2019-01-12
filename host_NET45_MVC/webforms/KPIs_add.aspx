<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="KPIs_add.aspx.vb" Inherits="KPIs_add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <style type="text/css">
    .style1
    {
      font-size: 10px; 
      font-weight: bold; 
      color: #808080; 
      padding: 10px;
      border-bottom-color:#F0F8FF;
      border-bottom-style:solid ;
      border-bottom-width:1px;
    }
    
    .style2
    {
      font-size: 10px; 
      font-weight: bold; 
      color: #808080; 
      padding: 2px;
      text-align:center;
    }   

    .style4
    {
      font-size: 10px; 
    	width:200px;      
      font-weight: bold; 
      color: #6495ED; 
      padding: 1px;
      border-bottom-color:#F0F8FF;
      border-bottom-style:solid ;
      border-bottom-width:1px;
    }   

    
    .style3
    {
    	width:200px;
    	}     
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


                    <asp:DataList ID="DataListKPIs_add" runat="server" 
                        DataSourceID="SqlDataSourceKPIs_add" RepeatDirection="Horizontal"
                        Font-Size="10px">
                      <HeaderTemplate>
                       <span style="color: #666699; font-size: 11px; font-weight:bold;">Addendums </span>
                                       <asp:HyperLink ID="HyperLinkNewAdd" runat="server" Target="_self"  
                                              ForeColor="Red" BackColor="Yellow" Font-Bold="True" Text= "Create New" >
                                       </asp:HyperLink>
                      </HeaderTemplate>
                      <ItemTemplate>
                          <table >
                              <tr>
                                  <td >
                                          <div style="padding: 2px; width: 15px; background-color: #666699; text-align: center; margin-bottom: 1px; color: #FFFFFF;">
                                               <asp:HyperLink ID="HyperLinkKPIS_add" runat="server" Target="_self"  forecolor="White"
                                               NavigateURL='<%# Bind("NavigateURL") %>'
                                                Text='<%# Bind("AddNo") %>'  >
                                             </asp:HyperLink>
                                          </div>
                                  </td>
                              </tr>
                          </table>
                      </ItemTemplate>
                    </asp:DataList>
                    <asp:SqlDataSource ID="SqlDataSourceKPIs_add" runat="server" 
                             ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                             SelectCommand=" SELECT     RTRIM(dbo.Table_KPIs_add.addNo) AS addNo, 
                                                          N'~/KPIs_add.aspx?ProjectID=' + RTRIM(CONVERT(nvarChar(10), dbo.Table_KPIs.ProjectID)) 
                                                          + N'&' + N'AddNo=' + RTRIM(CONVERT(nvarChar(10), dbo.Table_KPIs_add.addNo)) AS NavigateURL
                                                          FROM         dbo.Table_KPIs INNER JOIN
                                                            dbo.Table_KPIs_add ON dbo.Table_KPIs.idKPI = dbo.Table_KPIs_add.idKPI
                                                          WHERE     (dbo.Table_KPIs.ProjectID = @ProjectID) ORDER BY AddNo ">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="0" Name="ProjectID" 
                                  QueryStringField="ProjectID" Type="Int32" />
                            </SelectParameters>
                    </asp:SqlDataSource>



  <asp:FormView ID="FormViewKPIs" runat="server" DataKeyNames="idKPI_add" 
    DataSourceID="SqlDataSourceKPIs" EnableModelValidation="True"  >
    <InsertItemTemplate>
      <table>

       <tr>
        <td class="style1">
        </td>
        <td>
          <asp:LinkButton ID="LinkButtonInsert" runat="server" 
            CssClass="ButtonEditAddendum" CommandName="Insert">Insert</asp:LinkButton>
        </td>
        <td class="style2">
          Comments
        </td>
       </tr>


       <tr>
        <td class="style1">
         Project Name:
        </td>
        <td>
                        <asp:DropDownList ID="DropDownListProject" runat="server" 
                            Height="20px" CssClass="DrpDwnListGeneral" 
                          DataSourceID="SqlDataSourceProjectName" DataTextField="ProjectName" 
                          DataValueField="idKPI" >
                        </asp:DropDownList>

                        <asp:SqlDataSource ID="SqlDataSourceProjectName" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                          SelectCommand=" SELECT     dbo.Table_KPIs.idKPI, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName
                                                        FROM         dbo.Table_KPIs INNER JOIN
                                                                              dbo.Table1_Project ON dbo.Table_KPIs.ProjectID = dbo.Table1_Project.ProjectID
                                                        WHERE     (dbo.Table_KPIs.ProjectID = @ProjectID) ">
                          <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="0" Name="ProjectID" 
                              QueryStringField="ProjectID" Type="Int32" />
                          </SelectParameters>
                        </asp:SqlDataSource>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentProjectName" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Add No:
        </td>
        <td>
         <asp:TextBox Id="TextBoxAddNo" runat="server" CssClass="TextBoxGeneral" ></asp:TextBox>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentAddNo" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Start Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxStartDate" runat="server" CssClass="TextBoxGeneral" ></asp:TextBox>
              <asp:CalendarExtender ID="CalendarExtenderTextBoxStartDate" 
                   runat="server" Format="dd/MM/yyyy"  CssClass="cal_Theme1"
                   TargetControlID="TextBoxStartDate">
              </asp:CalendarExtender>

              <asp:RegularExpressionValidator ID="RegularExpressionTextBoxStartDate" runat="server" 
                         ControlToValidate="TextBoxStartDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentProjectStartDate" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Finish Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxFinishDate" runat="server" CssClass="TextBoxGeneral" ></asp:TextBox>
              <asp:CalendarExtender ID="CalendarExtenderTextBoxFinishDate" 
                   runat="server" Format="dd/MM/yyyy"  CssClass="cal_Theme1"
                   TargetControlID="TextBoxFinishDate">
              </asp:CalendarExtender>

              <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxFinishDate" runat="server" 
                         ControlToValidate="TextBoxFinishDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentProjectFinishDate" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Current Project End Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxCurrentProjectEndDate" runat="server" CssClass="TextBoxGeneral" ></asp:TextBox>
              <asp:CalendarExtender ID="CalendarExtenderCurrentProjectEndDate"
                   runat="server" Format="dd/MM/yyyy"  CssClass="cal_Theme1"
                   TargetControlID="TextBoxCurrentProjectEndDate">
              </asp:CalendarExtender>

              <asp:RegularExpressionValidator ID="RegularExpressionValidatorCurrentProjectEndDate" runat="server" 
                         ControlToValidate="TextBoxCurrentProjectEndDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentProjectCurrentEndDate" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
          Contract Currency:
        </td>
        <td>
          <asp:Literal ID="LabelContractCurrency" runat="server" ></asp:Literal>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Contract Amount With VAT:
        </td>
        <td>
         <asp:TextBox Id="TextBoxContractAmountWithVAT" runat="server" 
         CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxContractAmountWithVAT" runat="server" 
                       ControlToValidate="TextBoxContractAmountWithVAT" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentContractAmountIncVAT" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Advance Percent:
        </td>
        <td>
         <asp:TextBox Id="TextBoxAdvancePercent" runat="server" CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorAdvancePercent" runat="server" 
                       ControlToValidate="TextBoxAdvancePercent" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="RangeValidatorTextBoxAdvancePercent" runat="server"  
                            Display="Dynamic" ErrorMessage="range to be 0-99" ControlToValidate="TextBoxAdvancePercent" 
                             CssClass="LabelGeneral" MaximumValue="99" MinimumValue="0" Type="Double">
                             </asp:RangeValidator>                             
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentAdvancePercent" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Payment Terms:
        </td>
        <td>

        </td>
        <td>
         <asp:TextBox Id="TextBoxPaymentTerms" runat="server" 
           TextMode="MultiLine" Height="50px"   CssClass="TextBoxGeneral" ></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Exchange Rate:
        </td>
        <td>
         <asp:TextBox Id="TextBoxExchangeRate" runat="server" CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorExchangeRate" runat="server" 
                       ControlToValidate="TextBoxExchangeRate" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentExchangeRate" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Retention Percent:
        </td>
        <td>
         <asp:TextBox Id="TextBoxRetentionPercent" runat="server" CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorRetentionPercent" runat="server" 
                       ControlToValidate="TextBoxRetentionPercent" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="RangeValidatorTextBoxRetentionPercent" runat="server"  
                            Display="Dynamic" ErrorMessage="range to be 0-99" ControlToValidate="TextBoxRetentionPercent" 
                             CssClass="LabelGeneral" MaximumValue="99" MinimumValue="0" Type="Double">
                             </asp:RangeValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentRetentionPercent" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1" bgcolor="#99CCFF">
          Retention Value inc VAT</td>
        <td>
          <asp:TextBox ID="TextBoxRetentionValue" runat="server" 
            CssClass="TextBoxGeneral" ></asp:TextBox>
          <asp:RegularExpressionValidator ID="RegularExpressionValidatorRetentionValue" 
            runat="server" ControlToValidate="TextBoxRetentionValue" 
            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Not Valid" 
            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
            ValidationGroup="General"></asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentRetentionValueIncVAT" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Retention Terms:
        </td>
        <td>
         <asp:TextBox Id="TextBoxRetentionTerms" runat="server" 
           TextMode="MultiLine" Height="50px"   CssClass="TextBoxGeneral" ></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="TextBoxCommentRetentionTerms" runat="server" 
              CssClass="TextBoxGeneral" Height="50px" 
              TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1" bgcolor="#99CCFF">
          Retention Due Date</td>
        <td>
          <asp:TextBox ID="TextBoxRetentionDueDate" runat="server" 
            CssClass="TextBoxGeneral"  ></asp:TextBox>
          <asp:CalendarExtender ID="TextBoxRetentionDueDate_CalendarExtender" 
            runat="server" CssClass="cal_Theme1" Format="dd/MM/yyyy" 
            TargetControlID="TextBoxRetentionDueDate">
          </asp:CalendarExtender>
          <asp:RegularExpressionValidator ID="RegularExpressionValidatorRetentionDueDate" 
            runat="server" ControlToValidate="TextBoxRetentionDueDate" 
            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="dd/mm/yyyy" 
            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentRetentionDueDate" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

        <tr>
          <td bgcolor="#99CCFF" class="style1">
            Warranty Period</td>
          <td>
            <asp:TextBox ID="TextBoxWarrantyPeriod" runat="server" 
              CssClass="TextBoxGeneral" Height="50px" 
              TextMode="MultiLine"></asp:TextBox>
          </td>
          <td>
            <asp:TextBox ID="TextBoxCommentWarrantyPeriod" runat="server" 
              CssClass="TextBoxGeneral" Height="50px" 
              TextMode="MultiLine"></asp:TextBox>
          </td>
        </tr>

       <tr>
        <td class="style1">
         Penalty Clause:
        </td>
        <td>
          <asp:CheckBox ID="CheckBoxPenaltyClause" runat="server" />
        </td>
        <td>
            <asp:TextBox ID="TextBoxCommentPenaltyClause" runat="server" 
              CssClass="TextBoxGeneral" Height="50px" 
              TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Penalty Terms:
        </td>
        <td>
         <asp:TextBox Id="TextBoxPenaltyTerms" runat="server" 
           TextMode="MultiLine" Height="50px"   CssClass="TextBoxGeneral" ></asp:TextBox>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentPenaltyTerms" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Original BOQ Margin %:
        </td>
        <td>
         <asp:TextBox Id="TextBoxOriginalBOQmargin" runat="server" CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorOriginalBOQmargin" runat="server" 
                       ControlToValidate="TextBoxOriginalBOQmargin" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentOriginalBOQMargin" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Validated BOQ Margin %:
        </td>
        <td>
         <asp:TextBox Id="TextBoxValidatedBOQmargin" runat="server" CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorValidatedBOQmargin" runat="server" 
                       ControlToValidate="TextBoxValidatedBOQmargin" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentValidatedBOQmargin" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Current BOQ Margin %:
        </td>
        <td>
         <asp:TextBox Id="TextBoxCurrentBOQmargin" runat="server" CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorCurrentBOQmargin" runat="server" 
                       ControlToValidate="TextBoxCurrentBOQmargin" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentCurrentBOQmargin" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Current IV Without VAT:
        </td>
        <td>
         <asp:TextBox Id="TextBoxCurrentIVwithoutVAT" runat="server" 
         CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorCurrentIVwithoutVAT" runat="server" 
                       ControlToValidate="TextBoxCurrentIVwithoutVAT" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentCurrentIVexcVAT" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Current EV Without VAT:
        </td>
        <td>
         <asp:TextBox Id="TextBoxCurrentEVwithoutVAT" runat="server" 
         CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorCurrentEVwithoutVAT" runat="server" 
                       ControlToValidate="TextBoxCurrentEVwithoutVAT" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentCurrentEVexcVAT" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>


       <tr>
        <td class="style1" bgcolor="#66CCFF">
          Project Completion Percent</td>
        <td>
          <asp:TextBox ID="TextBoxProjectCompletePercent" runat="server" 
            CssClass="TextBoxGeneral" ></asp:TextBox>
          <asp:RegularExpressionValidator ID="RegularExpressionValidatorProjectPercent" 
            runat="server" ControlToValidate="TextBoxProjectCompletePercent" 
            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Not Valid" 
            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
            ValidationGroup="General"></asp:RegularExpressionValidator>
          <asp:RangeValidator ID="RangeValidatorTextBoxAdvancePercent0" runat="server" 
            ControlToValidate="TextBoxProjectCompletePercent" CssClass="LabelGeneral" 
            Display="Dynamic" ErrorMessage="range to be 0-99" MaximumValue="99" 
            MinimumValue="0" Type="Double"></asp:RangeValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentProjectCompletionPercent" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Cash In With VAT:
        </td>
        <td>
         <asp:TextBox Id="TextBoxCashInwithVAT" runat="server" 
         CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorCashInwithVAT" runat="server" 
                       ControlToValidate="TextBoxCashInwithVAT" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
            <asp:TextBox ID="TextBoxCommentCashInExcVAT" runat="server" 
              CssClass="TextBoxGeneral" Height="50px" 
              TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Cash Out With VAT:
        </td>
        <td>
         <asp:TextBox Id="TextBoxCashOutwithVAT" runat="server" 
         CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorCashOutwithVAT" runat="server" 
                       ControlToValidate="TextBoxCashOutwithVAT" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentCashOutExcVAT" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

      </table>
    </InsertItemTemplate>


    <%-- --------------------------------------------------------------------------------------------------------- --%>
    <%-- --------------------------------------------------------------------------------------------------------- --%>
    <%-- --------------------------------------------------EDIT MODE--------------------------------------- --%>
    <%-- --------------------------------------------------------------------------------------------------------- --%>
    <%-- --------------------------------------------------------------------------------------------------------- --%>

    <EditItemTemplate>
      <table style="background-color: #F0F8FF">

       <tr>
        <td class="style1">
        </td>
        <td>
          <asp:LinkButton ID="LinkButtonInsert" runat="server" 
            CssClass="ButtonDeleteAddendum" CommandName="Update">Update</asp:LinkButton>
          <asp:LinkButton ID="LinkButtonDelete" runat="server" 
            OnClientClick="return confirm('Are you sure you want to delete this record?');"
            CssClass="ButtonDeleteAddendum" CommandName="Delete">Delete</asp:LinkButton>
        </td>
        <td class="style2">
          Comments
        </td>
       </tr>


       <tr>
        <td class="style1">
         Project Name:
        </td>
        <td>
                        <asp:DropDownList ID="DropDownListProject" runat="server" 
                            Height="20px" CssClass="DrpDwnListGeneral" 
                          DataSourceID="SqlDataSourceProjectName" DataTextField="ProjectName" 
                          DataValueField="idKPI" ondatabound="DropDownListProject_DataBound"
                          SelectedValue='<%# Bind("idKPI") %>' ></asp:DropDownList>

                        <asp:SqlDataSource ID="SqlDataSourceProjectName" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                          SelectCommand=" SELECT     dbo.Table_KPIs.idKPI, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName
                                                        FROM         dbo.Table_KPIs INNER JOIN
                                                        dbo.Table1_Project ON dbo.Table_KPIs.ProjectID = dbo.Table1_Project.ProjectID ">
                        </asp:SqlDataSource>

                        <asp:CompareValidator ID="CompareValidatorProjectName" runat="server" 
                          ErrorMessage="Required" CssClass="LabelGeneral"  Display="Dynamic"
                          ControlToValidate="DropDownListProject" ValueToCompare="0" 
                          Operator="NotEqual"></asp:CompareValidator>
        </td>
        <td class="style4">
          <asp:TextBox ID="TextBoxCommentProjectName" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteProjectName") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Add No:
        </td>
        <td>
         <asp:TextBox Id="TextBoxAddNo" runat="server" CssClass="TextBoxGeneral" Text='<%# Bind("AddNo") %>'   ></asp:TextBox>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentAddNo" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteAddNo") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Start Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxStartDate" runat="server" 
           Text='<%# Bind("ProjectStartDate","{0:dd/MM/yyyy}") %>' CssClass="TextBoxGeneral" ></asp:TextBox>
              <asp:CalendarExtender ID="CalendarExtenderTextBoxStartDate" 
                   runat="server" Format="dd/MM/yyyy"  CssClass="cal_Theme1"
                   TargetControlID="TextBoxStartDate">
              </asp:CalendarExtender>

              <asp:RegularExpressionValidator ID="RegularExpressionTextBoxStartDate" runat="server" 
                         ControlToValidate="TextBoxStartDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentProjectStartDate" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteProjectStartDate") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Finish Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxFinishDate" runat="server" 
         Text='<%# Bind("ProjectFinishDate","{0:dd/MM/yyyy}") %>' CssClass="TextBoxGeneral" ></asp:TextBox>
              <asp:CalendarExtender ID="CalendarExtenderTextBoxFinishDate" 
                   runat="server" Format="dd/MM/yyyy"  CssClass="cal_Theme1"
                   TargetControlID="TextBoxFinishDate">
              </asp:CalendarExtender>

              <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxFinishDate" runat="server" 
                         ControlToValidate="TextBoxFinishDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentProjectFinishDate" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteProjectFinishDate") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Current Project End Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxCurrentProjectEndDate" runat="server" CssClass="TextBoxGeneral" 
         Text='<%# Bind("CurrentProjectEndDate","{0:dd/MM/yyyy}") %>' ></asp:TextBox>
              <asp:CalendarExtender ID="CalendarExtenderCurrentProjectEndDate"
                   runat="server" Format="dd/MM/yyyy"  CssClass="cal_Theme1"
                   TargetControlID="TextBoxCurrentProjectEndDate">
              </asp:CalendarExtender>

              <asp:RegularExpressionValidator ID="RegularExpressionValidatorCurrentProjectEndDate" runat="server" 
                         ControlToValidate="TextBoxCurrentProjectEndDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentProjectCurrentEndDate" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteCurrentProjectEndDate") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
          Contract Currency:
        </td>
        <td class="style4">
          <asp:Literal ID="LabelContractCurrency" runat="server" ></asp:Literal>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Contract Amount With VAT:
        </td>
        <td>
         <asp:TextBox Id="TextBoxContractAmountWithVAT" runat="server" 
         CssClass="TextBoxGeneral" Text='<%# Bind("ContractAmountWithVAT") %>' ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxContractAmountWithVAT" runat="server" 
                       ControlToValidate="TextBoxContractAmountWithVAT" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentContractAmountIncVAT" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteContractAmountWithVAT") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Advance Percent:
        </td>
        <td>
         <asp:TextBox Id="TextBoxAdvancePercent" runat="server" CssClass="TextBoxGeneral" 
         Text='<%# Bind("AdvancePercent") %>' ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorAdvancePercent" runat="server" 
                       ControlToValidate="TextBoxAdvancePercent" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="RangeValidatorTextBoxAdvancePercent" runat="server"  
                            Display="Dynamic" ErrorMessage="range to be 0-99" ControlToValidate="TextBoxAdvancePercent" 
                             CssClass="LabelGeneral" MaximumValue="99" MinimumValue="0" Type="Double">
                             </asp:RangeValidator>                             
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentAdvancePercent" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteAdvancePercent") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Payment Terms:
        </td>
        <td>
          &nbsp;</td>
        <td>
          <asp:TextBox ID="TextBoxPaymentTerms" runat="server" CssClass="TextBoxGeneral" 
            Height="50px" Text='<%# Bind("PaymentTerms") %>' TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Exchange Rate:
        </td>
        <td>
         <asp:TextBox Id="TextBoxExchangeRate" runat="server" CssClass="TextBoxGeneral" 
         Text='<%# Bind("ExchangeRate") %>' ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorExchangeRate" runat="server" 
                       ControlToValidate="TextBoxExchangeRate" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentExchangeRate" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteExchangeRate") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Retention Percent:
        </td>
        <td>
         <asp:TextBox Id="TextBoxRetentionPercent" runat="server" CssClass="TextBoxGeneral" 
         Text='<%# Bind("RetentionPercent") %>' ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorRetentionPercent" runat="server" 
                       ControlToValidate="TextBoxRetentionPercent" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="RangeValidatorTextBoxRetentionPercent" runat="server"  
                            Display="Dynamic" ErrorMessage="range to be 0-99" ControlToValidate="TextBoxRetentionPercent" 
                             CssClass="LabelGeneral" MaximumValue="99" MinimumValue="0" Type="Double">
                             </asp:RangeValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentRetentionPercent" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteRetentionPercent") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1" bgcolor="#99CCFF">
          Retention Value inc VAT</td>
        <td>
          <asp:TextBox ID="TextBoxRetentionValue" runat="server" 
            CssClass="TextBoxGeneral" Text='<%# Bind("RetentionValuinclVAT") %>'></asp:TextBox>
          <asp:RegularExpressionValidator ID="RegularExpressionValidatorRetentionValue" 
            runat="server" ControlToValidate="TextBoxRetentionValue" 
            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Not Valid" 
            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
            ValidationGroup="General"></asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentRetentionValueIncVAT" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteRetentionValueIncVAT") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

        <tr>
          <td class="style1">
            Retention Terms:
          </td>
          <td>
            <asp:TextBox ID="TextBoxRetentionTerms" runat="server" 
              CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("RetentionTerms") %>' 
              TextMode="MultiLine"></asp:TextBox>
          </td>
          <td>
            <asp:TextBox ID="TextBoxCommentRetentionTerms" runat="server" 
              CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteRetentionTerms") %>' 
              TextMode="MultiLine"></asp:TextBox>
          </td>
        </tr>

       <tr>
        <td class="style1" bgcolor="#99CCFF">
          Retention Due Date</td>
        <td>
          <asp:TextBox ID="TextBoxRetentionDueDate" runat="server" 
            CssClass="TextBoxGeneral" 
            Text='<%# Bind("RetentionDueDate","{0:dd/MM/yyyy}") %>'></asp:TextBox>
          <asp:CalendarExtender ID="TextBoxRetentionDueDate_CalendarExtender" 
            runat="server" CssClass="cal_Theme1" Format="dd/MM/yyyy" 
            TargetControlID="TextBoxRetentionDueDate">
          </asp:CalendarExtender>
          <asp:RegularExpressionValidator ID="RegularExpressionValidatorRetentionDueDate" 
            runat="server" ControlToValidate="TextBoxRetentionDueDate" 
            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="dd/mm/yyyy" 
            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"></asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentRetentionDueDate" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteRetentionDueDate") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

        <tr>
          <td bgcolor="#99CCFF" class="style1">
            Warranty Period</td>
          <td>
            <asp:TextBox ID="TextBoxWarrantyPeriod" runat="server" 
              CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("WarrantyPeriod") %>' 
              TextMode="MultiLine"></asp:TextBox>
          </td>
          <td>
            <asp:TextBox ID="TextBoxCommentWarrantyPeriod" runat="server" 
              CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteWarrantyPeriod") %>' 
              TextMode="MultiLine"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td class="style1">
            Penalty Clause:
          </td>
          <td>
            <asp:CheckBox ID="CheckBoxPenaltyClause" runat="server" 
              Checked='<%# DataBinder.Eval(Container.DataItem, "PenaltyClause") %>' />
          </td>
          <td>
            <asp:TextBox ID="TextBoxCommentPenaltyClause" runat="server" 
              CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NotePenaltyClause") %>' 
              TextMode="MultiLine"></asp:TextBox>
          </td>
        </tr>

       <tr>
        <td class="style1">
         Penalty Terms:
        </td>
        <td>
         <asp:TextBox Id="TextBoxPenaltyTerms" runat="server" 
         Text='<%# Bind("PenaltyTerms") %>' 
           TextMode="MultiLine" Height="50px"   CssClass="TextBoxGeneral" ></asp:TextBox>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentPenaltyTerms" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NotePenaltyTerms") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Original BOQ Margin %:
        </td>
        <td>
         <asp:TextBox Id="TextBoxOriginalBOQmargin" runat="server" CssClass="TextBoxGeneral" 
         Text='<%# Bind("OriginalBOQMargin") %>' ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorOriginalBOQmargin" runat="server" 
                       ControlToValidate="TextBoxOriginalBOQmargin" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentOriginalBOQMargin" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteOriginalBOQMargin") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Validated BOQ Margin %:
        </td>
        <td>
         <asp:TextBox Id="TextBoxValidatedBOQmargin" runat="server" CssClass="TextBoxGeneral" 
         Text='<%# Bind("ValidatedBOQMargin") %>'  ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorValidatedBOQmargin" runat="server" 
                       ControlToValidate="TextBoxValidatedBOQmargin" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentValidatedBOQmargin" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteValidatedBOQMargin") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Current BOQ Margin %:
        </td>
        <td>
         <asp:TextBox Id="TextBoxCurrentBOQmargin" runat="server" CssClass="TextBoxGeneral" 
         Text='<%# Bind("CurrentBOQMargin") %>'  ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorCurrentBOQmargin" runat="server" 
                       ControlToValidate="TextBoxCurrentBOQmargin" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentCurrentBOQmargin" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteCurrentBOQMargin") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Current IV Without VAT:
        </td>
        <td>
         <asp:TextBox Id="TextBoxCurrentIVwithoutVAT" runat="server" 
         Text='<%# Bind("CurrentIVwithOutVAT") %>' 
         CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorCurrentIVwithoutVAT" runat="server" 
                       ControlToValidate="TextBoxCurrentIVwithoutVAT" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentCurrentIVexcVAT" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteCurrentIVwithOutVAT") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Current EV Without VAT:
        </td>
        <td>
         <asp:TextBox Id="TextBoxCurrentEVwithoutVAT" runat="server" 
         Text='<%# Bind("CurrentEVwithOutVAT") %>' 
         CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorCurrentEVwithoutVAT" runat="server" 
                       ControlToValidate="TextBoxCurrentEVwithoutVAT" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentCurrentEVexcVAT" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteCurrentEVwithOutVAT") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1" bgcolor="#66CCFF">
          Project Completion Percent</td>
        <td>
          <asp:TextBox ID="TextBoxProjectCompletePercent" runat="server" 
            CssClass="TextBoxGeneral" Text='<%# Bind("ProjectCompletionPercent") %>'></asp:TextBox>
          <asp:RegularExpressionValidator ID="RegularExpressionValidatorProjectPercent" 
            runat="server" ControlToValidate="TextBoxProjectCompletePercent" 
            CssClass="LabelGeneral" Display="Dynamic" ErrorMessage="Not Valid" 
            ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
            ValidationGroup="General"></asp:RegularExpressionValidator>
          <asp:RangeValidator ID="RangeValidatorTextBoxAdvancePercent0" runat="server" 
            ControlToValidate="TextBoxProjectCompletePercent" CssClass="LabelGeneral" 
            Display="Dynamic" ErrorMessage="range to be 0-99" MaximumValue="99" 
            MinimumValue="0" Type="Double"></asp:RangeValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentProjectCompletionPercent" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteProjectCompletionPercent") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

        <tr>
          <td class="style1">
            Cash In With VAT:
          </td>
          <td>
            <asp:TextBox ID="TextBoxCashInwithVAT" runat="server" CssClass="TextBoxGeneral" 
              Text='<%# Bind("CashIninclVat") %>'></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorCashInwithVAT" 
              runat="server" ControlToValidate="TextBoxCashInwithVAT" CssClass="LabelGeneral" 
              Display="Dynamic" ErrorMessage="Not Valid" 
              ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
              ValidationGroup="General">
                  </asp:RegularExpressionValidator>
          </td>
          <td>
            <asp:TextBox ID="TextBoxCommentCashInExcVAT" runat="server" 
              CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteCashIninclVat") %>' 
              TextMode="MultiLine"></asp:TextBox>
          </td>
        </tr>

       <tr>
        <td class="style1">
         Cash Out With VAT:
        </td>
        <td>
         <asp:TextBox Id="TextBoxCashOutwithVAT" runat="server" 
         Text='<%# Bind("CashOutinclVat") %>' 
         CssClass="TextBoxGeneral" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorCashOutwithVAT" runat="server" 
                       ControlToValidate="TextBoxCashOutwithVAT" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCommentCashOutExcVAT" runat="server" 
            CssClass="TextBoxGeneral" Height="50px" Text='<%# Bind("NoteCashOutinclVat") %>' 
            TextMode="MultiLine"></asp:TextBox>
        </td>
       </tr>

      </table>
    </EditItemTemplate>

    <%-- --------------------------------------------------------------------------------------------------------- --%>
    <%-- --------------------------------------------------------------------------------------------------------- --%>
    <%-- --------------------------------------------------ITEM MODE--------------------------------------- --%>
    <%-- --------------------------------------------------------------------------------------------------------- --%>
    <%-- --------------------------------------------------------------------------------------------------------- --%>

    <ItemTemplate>
          <table>

       <tr>
        <td class="style1">
        </td>
        <td>
          <asp:LinkButton ID="LinkButtonEdit" runat="server" 
            CssClass="ButtonDeleteAddendum" onclick="LinkButtonEdit_Click" >Edit</asp:LinkButton>
          <asp:LinkButton ID="LinkButtonDelete" runat="server" 
            OnClientClick="return confirm('Are you sure you want to delete this record?');"
            CssClass="ButtonDeleteAddendum" CommandName="Delete">Delete</asp:LinkButton>
        </td>
        <td class="style2">
          Comments
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Name:
        </td>
        <td>
                        <asp:DropDownList ID="DropDownListProject" runat="server" 
                            Height="20px" CssClass="DrpDwnListGeneral"  Enabled="false"
                          DataSourceID="SqlDataSourceProjectName" DataTextField="ProjectName" 
                          DataValueField="idKPI" ondatabound="DropDownListProject_DataBound"
                          SelectedValue='<%# Bind("idKPI") %>' ></asp:DropDownList>

                        <asp:SqlDataSource ID="SqlDataSourceProjectName" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                          SelectCommand=" SELECT     dbo.Table_KPIs.idKPI, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName
                                                        FROM         dbo.Table_KPIs INNER JOIN
                                                        dbo.Table1_Project ON dbo.Table_KPIs.ProjectID = dbo.Table1_Project.ProjectID ">
                        </asp:SqlDataSource>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteProjectName" runat="server" 
           Text='<%# Bind("NoteProjectName") %>'  ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Add No:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralAddNo" runat="server" 
           Text='<%# Bind("AddNo") %>'  ></asp:Literal>
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteAddNo" runat="server" 
           Text='<%# Bind("NoteAddNo") %>'  ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Start Date:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralStartDate" runat="server" 
           Text='<%# Bind("ProjectStartDate","{0:dd/MM/yyyy}") %>' ></asp:Literal>
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteProjectStartDate" runat="server" 
           Text='<%# Bind("NoteProjectStartDate") %>'  ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Finish Date:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralFinishDate" runat="server" 
         Text='<%# Bind("ProjectFinishDate","{0:dd/MM/yyyy}") %>' ></asp:Literal>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteProjectFinishDate" runat="server" 
           Text='<%# Bind("NoteProjectFinishDate") %>'  ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Current Project End Date:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralCurrentProjectEndDate" runat="server" 
         Text='<%# Bind("CurrentProjectEndDate","{0:dd/MM/yyyy}") %>' ></asp:Literal>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteCurrentProjectEndDate" runat="server" 
         Text='<%# Bind("NoteCurrentProjectEndDate") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
          Contract Currency:
        </td>
        <td class="style4">
          <asp:Literal ID="LabelContractCurrency" runat="server" ></asp:Literal>
        </td>
        <td class="style4">
        </td>
       </tr>

       <tr>
        <td class="style1">
         Contract Amount With VAT:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralContractAmountWithVAT" runat="server" 
          Text='<%# Bind("ContractAmountWithVAT","{0:N2}") %>' ></asp:Literal>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteContractAmountWithVAT" runat="server" 
          Text='<%# Bind("NoteContractAmountWithVAT") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Advance Percent:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralAdvancePercent" runat="server"  
         Text='<%# Bind("AdvancePercent") %>' ></asp:Literal>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteAdvancePercent" runat="server"  
         Text='<%# Bind("NoteAdvancePercent") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Payment Terms:
        </td>
        <td class="style4">

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralPaymentTerms" runat="server" Text='<%# Bind("PaymentTerms") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Exchange Rate:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralExchangeRate" runat="server"  
         Text='<%# Bind("ExchangeRate") %>' ></asp:Literal>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteExchangeRate" runat="server"  
         Text='<%# Bind("NoteExchangeRate") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Retention Percent:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralRetentionPercent" runat="server"  
         Text='<%# Bind("RetentionPercent") %>' ></asp:Literal>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteRetentionPercent" runat="server"  
         Text='<%# Bind("NoteRetentionPercent") %>' ></asp:Literal>
        </td>
       </tr>


       <tr>
        <td class="style1" bgcolor="#99CCFF">
          Retention Value inc VAT</td>
        <td class="style4">
         <asp:Literal Id="LiteralRetentionValuinclVAT" runat="server"  
         Text='<%# Bind("RetentionValuinclVAT","{0:N2}") %>' ></asp:Literal>
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteRetentionValueIncVAT" runat="server"  
         Text='<%# Bind("NoteRetentionValueIncVAT") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Retention Terms:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralRetentionTerms" runat="server" Text='<%# Bind("RetentionTerms") %>' ></asp:Literal>
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteRetentionTerms" runat="server"  
         Text='<%# Bind("NoteRetentionTerms") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1" bgcolor="#99CCFF">
          Retention Due Date</td>
        <td class="style4">
         <asp:Literal Id="LiteralRetentionDueDate" runat="server"  
         Text='<%# Bind("RetentionDueDate","{0:dd/MM/yyyy}") %>' ></asp:Literal>
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteRetentionDueDate" runat="server"  
         Text='<%# Bind("NoteRetentionDueDate") %>' ></asp:Literal>
        </td>
       </tr>

        <tr>
          <td bgcolor="#99CCFF" class="style1">
            Warranty Period</td>
        <td class="style4">
         <asp:Literal Id="LiteralWarrantyPeriod" runat="server"  
         Text='<%# Bind("WarrantyPeriod") %>' ></asp:Literal>
          </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteWarrantyPeriod" runat="server"  
         Text='<%# Bind("NoteWarrantyPeriod") %>' ></asp:Literal>
          </td>
        </tr>

       <tr>
        <td class="style1">
         Penalty Clause:
        </td>
        <td class="style4">
          <asp:CheckBox ID="CheckBoxPenaltyClause" runat="server" enabled="false"
          Checked='<%# DataBinder.Eval(Container.DataItem, "PenaltyClause") %>'  />
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNotePenaltyClause" runat="server"  
         Text='<%# Bind("NotePenaltyClause") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Penalty Terms:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralPenaltyTerms" runat="server" Text='<%# Bind("PenaltyTerms") %>' ></asp:Literal>
        </td>
         <td class="style4">
         <asp:Literal Id="LiteralNotePenaltyTerms" runat="server"  
         Text='<%# Bind("NotePenaltyTerms") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Original BOQ Margin %:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralOriginalBOQmargin" runat="server"  Text='<%# Bind("OriginalBOQMargin") %>' ></asp:Literal>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteOriginalBOQMargin" runat="server"  Text='<%# Bind("NoteOriginalBOQMargin") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Validated BOQ Margin %:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralValidatedBOQmargin" runat="server" Text='<%# Bind("ValidatedBOQMargin") %>'  ></asp:Literal>
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteValidatedBOQMargin" runat="server" Text='<%# Bind("NoteValidatedBOQMargin") %>'  ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Current BOQ Margin %:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralCurrentBOQmargin" runat="server" Text='<%# Bind("CurrentBOQMargin") %>'  ></asp:Literal>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteCurrentBOQMargin" runat="server" Text='<%# Bind("NoteCurrentBOQMargin") %>'  ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Current IV Without VAT:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralCurrentIVwithoutVAT" runat="server" Text='<%# Bind("CurrentIVwithOutVAT","{0:N2}") %>' ></asp:Literal>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteCurrentIVwithOutVAT" runat="server" Text='<%# Bind("NoteCurrentIVwithOutVAT") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Current EV Without VAT:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralCurrentEVwithoutVAT" runat="server" Text='<%# Bind("CurrentEVwithOutVAT","{0:N2}") %>' ></asp:Literal>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteCurrentEVwithOutVAT" runat="server" Text='<%# Bind("NoteCurrentEVwithOutVAT") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1" bgcolor="#66CCFF">
          Project Completion Percent</td>
        <td class="style4">
         <asp:Literal Id="LiteralProjectCompletionPercent" runat="server" Text='<%# Bind("ProjectCompletionPercent") %>' ></asp:Literal>
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteProjectCompletionPercent" runat="server" Text='<%# Bind("NoteProjectCompletionPercent") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Cash In With VAT:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralCashInwithVAT" runat="server" Text='<%# Bind("CashIninclVat","{0:N2}") %>' ></asp:Literal>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteCashIninclVat" runat="server" Text='<%# Bind("NoteCashIninclVat") %>' ></asp:Literal>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Cash Out With VAT:
        </td>
        <td class="style4">
         <asp:Literal Id="LiteralCashOutwithVAT" runat="server" Text='<%# Bind("CashOutinclVat","{0:N2}") %>' ></asp:Literal>

        </td>
        <td class="style4">
         <asp:Literal Id="LiteralNoteCashOutinclVat" runat="server" Text='<%# Bind("NoteCashOutinclVat") %>' ></asp:Literal>
        </td>
       </tr>

      </table>
    </ItemTemplate>

  </asp:FormView>
  <asp:SqlDataSource ID="SqlDataSourceKPIs" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT 
       [Table_KPIs_add].idKPI_add
      ,[Table_KPIs_add].idKPI
      ,[Table_KPIs_add].addNo
      ,[Table_KPIs_add].[ProjectStartDate]
      ,[Table_KPIs_add].[ProjectFinishDate]
      ,[Table_KPIs_add].[CurrentProjectEndDate]
      ,[Table_KPIs_add].[ContractAmountWithVAT]
      ,[Table_KPIs_add].[AdvancePercent]
      ,[Table_KPIs_add].[PaymentTerms]
      ,[Table_KPIs_add].[ExchangeRate]
      ,[Table_KPIs_add].[RetentionPercent]
      ,[Table_KPIs_add].[RetentionTerms]
      ,[Table_KPIs_add].[PenaltyClause]
      ,[Table_KPIs_add].[PenaltyTerms]
      ,[Table_KPIs_add].[OriginalBOQMargin]
      ,[Table_KPIs_add].[ValidatedBOQMargin]
      ,[Table_KPIs_add].[CurrentBOQMargin]
      ,[Table_KPIs_add].[CurrentIVwithOutVAT]
      ,[Table_KPIs_add].[CurrentEVwithOutVAT]
      ,[Table_KPIs_add].[CashIninclVat]
      ,[Table_KPIs_add].[CashOutinclVat]
      ,[Table_KPIs_add].[RetentionValuinclVAT]
      ,[Table_KPIs_add].[RetentionDueDate]
      ,[Table_KPIs_add].[WarrantyPeriod]
      ,[Table_KPIs_add].[ProjectCompletionPercent]
      ,[Table_KPIs_add].[NoteProjectName]
      ,[Table_KPIs_add].[NoteAddNo]
      ,[Table_KPIs_add].[NoteProjectStartDate]
      ,[Table_KPIs_add].[NoteProjectFinishDate]
      ,[Table_KPIs_add].[NoteCurrentProjectEndDate]
      ,[Table_KPIs_add].[NoteContractAmountWithVAT]
      ,[Table_KPIs_add].[NoteAdvancePercent]
      ,[Table_KPIs_add].[NotePaymentTerms]
      ,[Table_KPIs_add].[NoteRetentionDueDate]
      ,[Table_KPIs_add].[NoteWarrantyPeriod]
      ,[Table_KPIs_add].[NoteExchangeRate]
      ,[Table_KPIs_add].[NoteRetentionPercent]
      ,[Table_KPIs_add].[NoteRetentionValueIncVAT]
      ,[Table_KPIs_add].[NoteRetentionTerms]
      ,[Table_KPIs_add].[NotePenaltyClause]
      ,[Table_KPIs_add].[NotePenaltyTerms]
      ,[Table_KPIs_add].[NoteOriginalBOQMargin]
      ,[Table_KPIs_add].[NoteValidatedBOQMargin]
      ,[Table_KPIs_add].[NoteCurrentBOQMargin]
      ,[Table_KPIs_add].[NoteCurrentIVwithOutVAT]
      ,[Table_KPIs_add].[NoteCurrentEVwithOutVAT]
      ,[Table_KPIs_add].[NoteProjectCompletionPercent]
      ,[Table_KPIs_add].[NoteCashIninclVat]
      ,[Table_KPIs_add].[NoteCashOutinclVat]
  FROM [Table_KPIs_add]
   Inner Join [Table_KPIs] ON [Table_KPIs_add].idKPI = [Table_KPIs].idKPI
  WHERE (ProjectID = @ProjectID) AND (addNo = @addNo) "
    
    InsertCommand=" INSERT INTO [Table_KPIs_add]
           ([idKPI]
           ,addNo
           ,[ProjectStartDate]
           ,[ProjectFinishDate]
           ,[CurrentProjectEndDate]
           ,[ContractAmountWithVAT]
           ,[AdvancePercent]
           ,[PaymentTerms]
           ,[ExchangeRate]
           ,[RetentionPercent]
           ,[RetentionTerms]
           ,[PenaltyClause]
           ,[PenaltyTerms]
           ,[OriginalBOQMargin]
           ,[ValidatedBOQMargin]
           ,[CurrentBOQMargin]
           ,[CurrentIVwithOutVAT]
           ,[CurrentEVwithOutVAT]
           ,[CashIninclVat]
           ,[CashOutinclVat]
            ,[RetentionValuinclVAT]
            ,[RetentionDueDate]
            ,[WarrantyPeriod]
            ,[ProjectCompletionPercent]
            ,[NoteProjectName]
            ,[NoteAddNo]
            ,[NoteProjectStartDate]
            ,[NoteProjectFinishDate]
            ,[NoteCurrentProjectEndDate]
            ,[NoteContractAmountWithVAT]
            ,[NoteAdvancePercent]
            ,[NotePaymentTerms]
            ,[NoteRetentionDueDate]
            ,[NoteWarrantyPeriod]
            ,[NoteExchangeRate]
            ,[NoteRetentionPercent]
            ,[NoteRetentionValueIncVAT]
            ,[NoteRetentionTerms]
            ,[NotePenaltyClause]
            ,[NotePenaltyTerms]
            ,[NoteOriginalBOQMargin]
            ,[NoteValidatedBOQMargin]
            ,[NoteCurrentBOQMargin]
            ,[NoteCurrentIVwithOutVAT]
            ,[NoteCurrentEVwithOutVAT]
            ,[NoteProjectCompletionPercent]
            ,[NoteCashIninclVat]
            ,[NoteCashOutinclVat])
     VALUES
           (@idKPI
           ,@addNo
           ,@ProjectStartDate
           ,@ProjectFinishDate
           ,@CurrentProjectEndDate
           ,@ContractAmountWithVAT
           ,@AdvancePercent
           ,@PaymentTerms
           ,@ExchangeRate
           ,@RetentionPercent
           ,@RetentionTerms
           ,@PenaltyClause
           ,@PenaltyTerms
           ,@OriginalBOQMargin
           ,@ValidatedBOQMargin
           ,@CurrentBOQMargin
           ,@CurrentIVwithOutVAT
           ,@CurrentEVwithOutVAT
           ,@CashIninclVat
           ,@CashOutinclVat
          ,@RetentionValuinclVAT
          ,@RetentionDueDate
          ,@WarrantyPeriod
          ,@ProjectCompletionPercent
          ,@NoteProjectName
          ,@NoteAddNo
          ,@NoteProjectStartDate
          ,@NoteProjectFinishDate
          ,@NoteCurrentProjectEndDate
          ,@NoteContractAmountWithVAT
          ,@NoteAdvancePercent
          ,@NotePaymentTerms
          ,@NoteRetentionDueDate
          ,@NoteWarrantyPeriod
          ,@NoteExchangeRate
          ,@NoteRetentionPercent
          ,@NoteRetentionValueIncVAT
          ,@NoteRetentionTerms
          ,@NotePenaltyClause
          ,@NotePenaltyTerms
          ,@NoteOriginalBOQMargin
          ,@NoteValidatedBOQMargin
          ,@NoteCurrentBOQMargin
          ,@NoteCurrentIVwithOutVAT
          ,@NoteCurrentEVwithOutVAT
          ,@NoteProjectCompletionPercent
          ,@NoteCashIninclVat
          ,@NoteCashOutinclVat) " 
      
      UpdateCommand = " UPDATE Table_KPIs_add
   SET idKPI = @idKPI
      ,addNo = @addNo
      ,ProjectStartDate = @ProjectStartDate 
      ,ProjectFinishDate = @ProjectFinishDate 
      ,CurrentProjectEndDate = @CurrentProjectEndDate 
      ,ContractAmountWithVAT = @ContractAmountWithVAT 
      ,AdvancePercent = @AdvancePercent 
      ,PaymentTerms = @PaymentTerms
      ,ExchangeRate = @ExchangeRate 
      ,RetentionPercent = @RetentionPercent 
      ,RetentionTerms = @RetentionTerms
      ,PenaltyClause = @PenaltyClause 
      ,PenaltyTerms = @PenaltyTerms 
      ,OriginalBOQMargin = @OriginalBOQMargin 
      ,ValidatedBOQMargin = @ValidatedBOQMargin 
      ,CurrentBOQMargin = @CurrentBOQMargin 
      ,CurrentIVwithOutVAT = @CurrentIVwithOutVAT 
      ,CurrentEVwithOutVAT = @CurrentEVwithOutVAT 
      ,CashIninclVat = @CashIninclVat 
      ,CashOutinclVat = @CashOutinclVat 
      ,RetentionValuinclVAT = @RetentionValuinclVAT
      ,RetentionDueDate = @RetentionDueDate
      ,WarrantyPeriod = @WarrantyPeriod
      ,ProjectCompletionPercent = @ProjectCompletionPercent
      ,NoteProjectName = @NoteProjectName
      ,NoteAddNo = @NoteAddNo
      ,NoteProjectStartDate = @NoteProjectStartDate
      ,NoteProjectFinishDate = @NoteProjectFinishDate
      ,NoteCurrentProjectEndDate = @NoteCurrentProjectEndDate
      ,NoteContractAmountWithVAT = @NoteContractAmountWithVAT
      ,NoteAdvancePercent = @NoteAdvancePercent
      ,NotePaymentTerms = @NotePaymentTerms
      ,NoteRetentionDueDate = @NoteRetentionDueDate
      ,NoteWarrantyPeriod = @NoteWarrantyPeriod
      ,NoteExchangeRate = @NoteExchangeRate
      ,NoteRetentionPercent = @NoteRetentionPercent
      ,NoteRetentionValueIncVAT = @NoteRetentionValueIncVAT
      ,NoteRetentionTerms = @NoteRetentionTerms
      ,NotePenaltyClause = @NotePenaltyClause
      ,NotePenaltyTerms = @NotePenaltyTerms
      ,NoteOriginalBOQMargin = @NoteOriginalBOQMargin
      ,NoteValidatedBOQMargin = @NoteValidatedBOQMargin
      ,NoteCurrentBOQMargin = @NoteCurrentBOQMargin
      ,NoteCurrentIVwithOutVAT = @NoteCurrentIVwithOutVAT
      ,NoteCurrentEVwithOutVAT = @NoteCurrentEVwithOutVAT
      ,NoteProjectCompletionPercent = @NoteProjectCompletionPercent
      ,NoteCashIninclVat = @NoteCashIninclVat
      ,NoteCashOutinclVat = @NoteCashOutinclVat
 WHERE idKPI_add = @idKPI_add "  
        
       DeleteCommand = " DELETE FROM Table_KPIs_add WHERE idKPI_add = @idKPI_add " >
    <SelectParameters>
      <asp:QueryStringParameter DefaultValue="0" Name="ProjectID" 
        QueryStringField="ProjectID" Type="Int32" />
      <asp:QueryStringParameter DefaultValue="0" Name="addNo" 
        QueryStringField="addNo" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="RetentionValuinclVAT"   />
        <asp:Parameter Name="RetentionDueDate"   />
        <asp:Parameter Name="WarrantyPeriod"   />
        <asp:Parameter Name="ProjectCompletionPercent"   />
        <asp:Parameter Name="NoteProjectName"   />
        <asp:Parameter Name="NoteAddNo"   />
        <asp:Parameter Name="NoteProjectStartDate"   />
        <asp:Parameter Name="NoteProjectFinishDate"   />
        <asp:Parameter Name="NoteCurrentProjectEndDate"   />
        <asp:Parameter Name="NoteContractAmountWithVAT"   />
        <asp:Parameter Name="NoteAdvancePercent"   />
        <asp:Parameter Name="NotePaymentTerms"   />
        <asp:Parameter Name="NoteRetentionDueDate"   />
        <asp:Parameter Name="NoteWarrantyPeriod"   />
        <asp:Parameter Name="NoteExchangeRate"   />
        <asp:Parameter Name="NoteRetentionPercent"   />
        <asp:Parameter Name="NoteRetentionValueIncVAT"   />
        <asp:Parameter Name="NoteRetentionTerms"   />
        <asp:Parameter Name="NotePenaltyClause"   />
        <asp:Parameter Name="NotePenaltyTerms"   />
        <asp:Parameter Name="NoteOriginalBOQMargin"   />
        <asp:Parameter Name="NoteValidatedBOQMargin"   />
        <asp:Parameter Name="NoteCurrentBOQMargin"   />
        <asp:Parameter Name="NoteCurrentIVwithOutVAT"   />
        <asp:Parameter Name="NoteCurrentEVwithOutVAT"   />
        <asp:Parameter Name="NoteProjectCompletionPercent"   />
        <asp:Parameter Name="NoteCashIninclVat"   />
        <asp:Parameter Name="NoteCashOutinclVat"   />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="RetentionValuinclVAT"   />
        <asp:Parameter Name="RetentionDueDate"   />
        <asp:Parameter Name="WarrantyPeriod"   />
        <asp:Parameter Name="ProjectCompletionPercent"   />
        <asp:Parameter Name="NoteProjectName"   />
        <asp:Parameter Name="NoteAddNo"   />
        <asp:Parameter Name="NoteProjectStartDate"   />
        <asp:Parameter Name="NoteProjectFinishDate"   />
        <asp:Parameter Name="NoteCurrentProjectEndDate"   />
        <asp:Parameter Name="NoteContractAmountWithVAT"   />
        <asp:Parameter Name="NoteAdvancePercent"   />
        <asp:Parameter Name="NotePaymentTerms"   />
        <asp:Parameter Name="NoteRetentionDueDate"   />
        <asp:Parameter Name="NoteWarrantyPeriod"   />
        <asp:Parameter Name="NoteExchangeRate"   />
        <asp:Parameter Name="NoteRetentionPercent"   />
        <asp:Parameter Name="NoteRetentionValueIncVAT"   />
        <asp:Parameter Name="NoteRetentionTerms"   />
        <asp:Parameter Name="NotePenaltyClause"   />
        <asp:Parameter Name="NotePenaltyTerms"   />
        <asp:Parameter Name="NoteOriginalBOQMargin"   />
        <asp:Parameter Name="NoteValidatedBOQMargin"   />
        <asp:Parameter Name="NoteCurrentBOQMargin"   />
        <asp:Parameter Name="NoteCurrentIVwithOutVAT"   />
        <asp:Parameter Name="NoteCurrentEVwithOutVAT"   />
        <asp:Parameter Name="NoteProjectCompletionPercent"   />
        <asp:Parameter Name="NoteCashIninclVat"   />
        <asp:Parameter Name="NoteCashOutinclVat"   />
    </InsertParameters>
  </asp:SqlDataSource>
</asp:Content>

