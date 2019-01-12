<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="ContractClientData.aspx.vb" Inherits="ContractClientData" %>

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
    
    .style3
    {
    	width:200px;
    	}     
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <asp:FormView ID="FormViewContractClientData" runat="server" DataKeyNames="ContractID" 
    DataSourceID="SqlDataSourceContractClientData" EnableModelValidation="True"  >
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
                          DataValueField="ProjectID" ondatabound="DropDownListProject_DataBound">
                        </asp:DropDownList>

                        <asp:SqlDataSource ID="SqlDataSourceProjectName" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                          SelectCommand="SELECT [ProjectID], rtrim([ProjectName]) as ProjectName  FROM [Table1_Project]
                                                          WHERE CurrentStatus = 1
                                                          ORDER By ProjectName ASC ">
                        </asp:SqlDataSource>

                        <asp:CompareValidator ID="CompareValidatorProjectName" runat="server" 
                          ErrorMessage="Required" CssClass="LabelGeneral"  Display="Dynamic"
                          ControlToValidate="DropDownListProject" ValueToCompare="0" 
                          Operator="NotEqual"></asp:CompareValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Manager:
        </td>
        <td>
         <asp:TextBox Id="TextBoxProjectManager" runat="server" CssClass="TextBoxGeneralRev" ></asp:TextBox>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Start Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxStartDate" runat="server" CssClass="TextBoxGeneralRev add_datepicker" ></asp:TextBox>

              <asp:RegularExpressionValidator ID="RegularExpressionTextBoxStartDate" runat="server" 
                         ControlToValidate="TextBoxStartDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Finish Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxFinishDate" runat="server" CssClass="TextBoxGeneralRev add_datepicker" ></asp:TextBox>

              <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxFinishDate" runat="server" 
                         ControlToValidate="TextBoxFinishDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
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
         CssClass="TextBoxGeneralRev" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxContractAmountWithVAT" runat="server" 
                       ControlToValidate="TextBoxContractAmountWithVAT" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Payment Terms Re Currency:
        </td>
        <td>
         <asp:TextBox Id="TextBoxPaymentTermsReCurrency" runat="server" 
          CssClass="TextBoxGeneralRev" TextMode="MultiLine" Height="50px" ></asp:TextBox>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Advance Percent:
        </td>
        <td>
         <asp:TextBox Id="TextBoxAdvancePercent" runat="server" CssClass="TextBoxGeneralRev" ></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTextBoxAdvancePercent" 
                                runat="server" FilterType="Numbers" TargetControlID="TextBoxAdvancePercent">
                            </asp:FilteredTextBoxExtender>
                            <asp:RangeValidator ID="RangeValidatorTextBoxAdvancePercent" runat="server"  
                            Display="Dynamic" ErrorMessage="range to be 0-99" ControlToValidate="TextBoxAdvancePercent" 
                             CssClass="LabelGeneral" MaximumValue="99" MinimumValue="0" Type="Integer">
                             </asp:RangeValidator>                             
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Retention Percent:
        </td>
        <td>
         <asp:TextBox Id="TextBoxRetentionPercent" runat="server" CssClass="TextBoxGeneralRev" ></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTextBoxRetentionPercent" 
                                runat="server" FilterType="Numbers" TargetControlID="TextBoxRetentionPercent">
                            </asp:FilteredTextBoxExtender>
                            <asp:RangeValidator ID="RangeValidatorTextBoxRetentionPercent" runat="server"  
                            Display="Dynamic" ErrorMessage="range to be 0-99" ControlToValidate="TextBoxRetentionPercent" 
                             CssClass="LabelGeneral" MaximumValue="99" MinimumValue="0" Type="Integer">
                             </asp:RangeValidator>
        </td>
        <td>
        </td>
       </tr>
       
       <tr>
        <td class="style1">
         Retention Terms:
        </td>
        <td>
         <asp:TextBox Id="TextBoxRetentionTerms" runat="server" 
          TextMode="MultiLine"  Height="50px"  CssClass="TextBoxGeneralRev" ></asp:TextBox>
        </td>
        <td class="style2">
         <asp:TextBox Id="TextBoxRetentionTermsComments" runat="server" 
           TextMode="MultiLine"  Height="50px"   CssClass="TextBoxGeneralRev" ></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Validated BOQ margin Percent:
        </td>
        <td>
         <asp:TextBox Id="TextBoxValidatedBOQmarginPercent" runat="server" CssClass="TextBoxGeneralRev" ></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTextBoxValidatedBOQmarginPercent" 
                                runat="server" FilterType="Numbers" TargetControlID="TextBoxValidatedBOQmarginPercent">
                            </asp:FilteredTextBoxExtender>
                            <asp:RangeValidator ID="RangeValidatorTextBoxValidatedBOQmarginPercent" runat="server"  
                            Display="Dynamic" ErrorMessage="range to be 0-99" ControlToValidate="TextBoxValidatedBOQmarginPercent" 
                             CssClass="LabelGeneral" MaximumValue="99" MinimumValue="0" Type="Integer">
                             </asp:RangeValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         IV expected Submission Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxIVexpectedSubmissionDate" runat="server" CssClass="TextBoxGeneralRev add_datepicker" ></asp:TextBox>

              <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxIVexpectedSubmissionDate" runat="server" 
                         ControlToValidate="TextBoxIVexpectedSubmissionDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         IV expected Approval Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxIVexpectedApprovalDate" runat="server" CssClass="TextBoxGeneralRev add_datepicker" ></asp:TextBox>

              <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxIVexpectedApprovalDate" runat="server" 
                         ControlToValidate="TextBoxIVexpectedApprovalDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Expected Payment Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxExpectedPaymentDate" runat="server" CssClass="TextBoxGeneralRev add_datepicker" ></asp:TextBox>

              <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxExpectedPaymentDate" runat="server" 
                         ControlToValidate="TextBoxExpectedPaymentDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
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
        </td>
       </tr>

       <tr>
        <td class="style1">
         Penalty Terms:
        </td>
        <td>
         <asp:TextBox Id="TextBoxPenaltyTerms" runat="server" 
           TextMode="MultiLine" Height="50px"   CssClass="TextBoxGeneralRev" ></asp:TextBox>
        </td>
        <td>
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
                          DataValueField="ProjectID" ondatabound="DropDownListProject_DataBound"
                          SelectedValue='<%# Bind("ProjectID_PTS") %>' ></asp:DropDownList>

                        <asp:SqlDataSource ID="SqlDataSourceProjectName" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                          SelectCommand="SELECT [ProjectID], rtrim([ProjectName]) as ProjectName  FROM [Table1_Project]
                                                          WHERE CurrentStatus = 1
                                                          ORDER By ProjectName ASC ">
                        </asp:SqlDataSource>

                        <asp:CompareValidator ID="CompareValidatorProjectName" runat="server" 
                          ErrorMessage="Required" CssClass="LabelGeneral"  Display="Dynamic"
                          ControlToValidate="DropDownListProject" ValueToCompare="0" 
                          Operator="NotEqual"></asp:CompareValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Manager:
        </td>
        <td>
         <asp:TextBox Id="TextBoxProjectManager" runat="server" 
           Text='<%# Bind("ProjectManager") %>'  CssClass="TextBoxGeneralRev" ></asp:TextBox>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Start Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxStartDate" runat="server" 
           Text='<%# Bind("ProjectStartDate","{0:dd/MM/yyyy}") %>' CssClass="TextBoxGeneralRev add_datepicker" ></asp:TextBox>

              <asp:RegularExpressionValidator ID="RegularExpressionTextBoxStartDate" runat="server" 
                         ControlToValidate="TextBoxStartDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Finish Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxFinishDate" runat="server" 
         Text='<%# Bind("ProjectFinishDate","{0:dd/MM/yyyy}") %>' CssClass="TextBoxGeneralRev add_datepicker" ></asp:TextBox>

              <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxFinishDate" runat="server" 
                         ControlToValidate="TextBoxFinishDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
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
          Text='<%# Bind("ContractAmountWithVAT") %>'  CssClass="TextBoxGeneralRev" ></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxContractAmountWithVAT" runat="server" 
                       ControlToValidate="TextBoxContractAmountWithVAT" CssClass="LabelGeneral" 
                       ErrorMessage="Not Valid" 
                       ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)" 
                       ValidationGroup="General" Display="Dynamic">
                  </asp:RegularExpressionValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Payment Terms Re Currency:
        </td>
        <td>
         <asp:TextBox Id="TextBoxPaymentTermsReCurrency" runat="server" 
           Text='<%# Bind("PaymentTermsReCurrency") %>'  CssClass="TextBoxGeneralRev" TextMode="MultiLine" Height="50px" ></asp:TextBox>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Advance Percent:
        </td>
        <td>
         <asp:TextBox Id="TextBoxAdvancePercent" runat="server" 
           Text='<%# Bind("AdvancePercent") %>'  CssClass="TextBoxGeneralRev" ></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTextBoxAdvancePercent" 
                                runat="server" FilterType="Numbers" TargetControlID="TextBoxAdvancePercent">
                            </asp:FilteredTextBoxExtender>
                            <asp:RangeValidator ID="RangeValidatorTextBoxAdvancePercent" runat="server"  
                            Display="Dynamic" ErrorMessage="range to be 0-99" ControlToValidate="TextBoxAdvancePercent" 
                             CssClass="LabelGeneral" MaximumValue="99" MinimumValue="0" Type="Integer">
                             </asp:RangeValidator>                             
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Retention Percent:
        </td>
        <td>
         <asp:TextBox Id="TextBoxRetentionPercent" runat="server" 
            Text='<%# Bind("RetentionPercent") %>'    CssClass="TextBoxGeneralRev" ></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTextBoxRetentionPercent" 
                                runat="server" FilterType="Numbers" TargetControlID="TextBoxRetentionPercent">
                            </asp:FilteredTextBoxExtender>
                            <asp:RangeValidator ID="RangeValidatorTextBoxRetentionPercent" runat="server"  
                            Display="Dynamic" ErrorMessage="range to be 0-99" ControlToValidate="TextBoxRetentionPercent" 
                             CssClass="LabelGeneral" MaximumValue="99" MinimumValue="0" Type="Integer">
                             </asp:RangeValidator>
        </td>
        <td>
        </td>
       </tr>
       
       <tr>
        <td class="style1">
         Retention Terms:
        </td>
        <td>
         <asp:TextBox Id="TextBoxRetentionTerms" runat="server"  Text='<%# Bind("RetentionTerms") %>'  
          TextMode="MultiLine"  Height="50px"  CssClass="TextBoxGeneralRev" ></asp:TextBox>
        </td>
        <td class="style2">
         <asp:TextBox Id="TextBoxRetentionTermsComments" runat="server" 
         Text='<%# Bind("RetentionTermsAddComment") %>'  
           TextMode="MultiLine"  Height="50px"   CssClass="TextBoxGeneralRev" ></asp:TextBox>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Validated BOQ margin Percent:
        </td>
        <td>
         <asp:TextBox Id="TextBoxValidatedBOQmarginPercent" runat="server" 
              Text='<%# Bind("ValidatedBOQmarginPercent") %>'     CssClass="TextBoxGeneralRev" ></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTextBoxValidatedBOQmarginPercent" 
                                runat="server" FilterType="Numbers" TargetControlID="TextBoxValidatedBOQmarginPercent">
                            </asp:FilteredTextBoxExtender>
                            <asp:RangeValidator ID="RangeValidatorTextBoxValidatedBOQmarginPercent" runat="server"  
                            Display="Dynamic" ErrorMessage="range to be 0-99" ControlToValidate="TextBoxValidatedBOQmarginPercent" 
                             CssClass="LabelGeneral" MaximumValue="99" MinimumValue="0" Type="Integer">
                             </asp:RangeValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         IV expected Submission Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxIVexpectedSubmissionDate" runat="server" 
            Text='<%# Bind("IVexpectedSubmissionDate","{0:dd/MM/yyyy}") %>'  CssClass="TextBoxGeneralRev add_datepicker" ></asp:TextBox>

              <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxIVexpectedSubmissionDate" runat="server" 
                         ControlToValidate="TextBoxIVexpectedSubmissionDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         IV expected Approval Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxIVexpectedApprovalDate" runat="server" 
             Text='<%# Bind("IvexpectedApprovalDate","{0:dd/MM/yyyy}") %>'   CssClass="TextBoxGeneralRev add_datepicker" ></asp:TextBox>

              <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxIVexpectedApprovalDate" runat="server" 
                         ControlToValidate="TextBoxIVexpectedApprovalDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Expected Payment Date:
        </td>
        <td>
         <asp:TextBox Id="TextBoxExpectedPaymentDate" runat="server" 
            Text='<%# Bind("ExpectedPaymentDate","{0:dd/MM/yyyy}") %>'   CssClass="TextBoxGeneralRev add_datepicker" ></asp:TextBox>

              <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxExpectedPaymentDate" runat="server" 
                         ControlToValidate="TextBoxExpectedPaymentDate" CssClass="LabelGeneral" 
                         ErrorMessage="dd/mm/yyyy" 
                         ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" 
                         Display="Dynamic">
              </asp:RegularExpressionValidator>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Penalty Clause:
        </td>
        <td>
          <asp:CheckBox ID="CheckBoxPenaltyClause" 
              Checked='<%# DataBinder.Eval(Container.DataItem, "PenaltyClause") %>' 
              runat="server" />
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Penalty Terms:
        </td>
        <td>
         <asp:TextBox Id="TextBoxPenaltyTerms" runat="server" 
         Text='<%# Bind("PenaltyTerms") %>' 
           TextMode="MultiLine" ></asp:TextBox>
        </td>
        <td>
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
                          DataValueField="ProjectID" ondatabound="DropDownListProject_DataBound"
                          SelectedValue='<%# Bind("ProjectID_PTS") %>' ></asp:DropDownList>

                        <asp:SqlDataSource ID="SqlDataSourceProjectName" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                          SelectCommand="SELECT [ProjectID], rtrim([ProjectName]) as ProjectName  FROM [Table1_Project]
                                                          WHERE CurrentStatus = 1
                                                          ORDER By ProjectName ASC ">
                        </asp:SqlDataSource>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Manager:
        </td>
        <td>
         <asp:Label Id="LabelProjectManager" runat="server" 
           Text='<%# Bind("ProjectManager") %>' CssClass="TextBoxGeneralRev" ></asp:Label>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Start Date:
        </td>
        <td>
         <asp:Label Id="TextBoxStartDate" runat="server" 
           Text='<%# Bind("ProjectStartDate","{0:dd/MM/yyyy}") %>' CssClass="TextBoxGeneralRev" ></asp:Label>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Project Finish Date:
        </td>
        <td>
         <asp:Label Id="TextBoxFinishDate" runat="server" 
         Text='<%# Bind("ProjectFinishDate","{0:dd/MM/yyyy}") %>' CssClass="TextBoxGeneralRev" ></asp:Label>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Contract Amount With VAT:
        </td>
        <td>
         <asp:label Id="TextBoxContractAmountWithVAT" runat="server" 
          Text='<%# Bind("ContractAmountWithVAT","{0:N2}") %>'  CssClass="TextBoxGeneralRev" ></asp:label>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Payment Terms Re Currency:
        </td>
        <td>
         <asp:label Id="TextBoxPaymentTermsReCurrency" runat="server" 
           Text='<%# Bind("PaymentTermsReCurrency") %>'  CssClass="TextBoxGeneralRev" TextMode="MultiLine" Height="50px" ></asp:label>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Advance Percent:
        </td>
        <td>
         <asp:label Id="TextBoxAdvancePercent" runat="server" 
           Text='<%# Bind("AdvancePercent") %>'  CssClass="TextBoxGeneralRev" ></asp:label>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Retention Percent:
        </td>
        <td>
         <asp:label Id="TextBoxRetentionPercent" runat="server" 
            Text='<%# Bind("RetentionPercent") %>'    CssClass="TextBoxGeneralRev" ></asp:label>
        </td>
        <td>
        </td>
       </tr>
       
       <tr>
        <td class="style1">
         Retention Terms:
        </td>
        <td>
         <asp:label Id="TextBoxRetentionTerms" runat="server"  Text='<%# Bind("RetentionTerms") %>'  
          CssClass="TextBoxGeneralRev" ></asp:label>
        </td>
        <td class="style3">
         <asp:label Id="TextBoxRetentionTermsComments" runat="server" 
            Text='<%# Bind("RetentionTermsAddComment") %>'  CssClass="TextBoxGeneralRev" ></asp:label>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Validated BOQ margin Percent:
        </td>
        <td>
         <asp:label Id="TextBoxValidatedBOQmarginPercent" runat="server" 
              Text='<%# Bind("ValidatedBOQmarginPercent") %>'     CssClass="TextBoxGeneralRev" ></asp:label>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         IV expected Submission Date:
        </td>
        <td>
         <asp:label Id="TextBoxIVexpectedSubmissionDate" runat="server" 
            Text='<%# Bind("IVexpectedSubmissionDate","{0:dd/MM/yyyy}") %>'  CssClass="TextBoxGeneralRev" ></asp:label>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         IV expected Approval Date:
        </td>
        <td>
         <asp:label Id="TextBoxIVexpectedApprovalDate" runat="server" 
             Text='<%# Bind("IvexpectedApprovalDate","{0:dd/MM/yyyy}") %>'   CssClass="TextBoxGeneralRev" ></asp:label>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Expected Payment Date:
        </td>
        <td>
         <asp:label Id="TextBoxExpectedPaymentDate" runat="server" 
            Text='<%# Bind("ExpectedPaymentDate","{0:dd/MM/yyyy}") %>'   CssClass="TextBoxGeneralRev" ></asp:label>
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Penalty Clause:
        </td>
        <td>
          <asp:CheckBox ID="CheckBoxPenaltyClause"  Enabled="false"
              Checked='<%# DataBinder.Eval(Container.DataItem, "PenaltyClause") %>' 
              runat="server" />
        </td>
        <td>
        </td>
       </tr>

       <tr>
        <td class="style1">
         Penalty Terms:
        </td>
        <td>
         <asp:label Id="TextBoxPenaltyTerms" runat="server" 
         Text='<%# Bind("PenaltyTerms") %>' 
           CssClass="TextBoxGeneralRev" ></asp:label>
        </td>
        <td>
        </td>
       </tr>

      </table>
    </ItemTemplate>

  </asp:FormView>
  <asp:SqlDataSource ID="SqlDataSourceContractClientData" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT [ContractID], 
    [ProjectID_PTS], 
    [ProjectManager], 
    [ProjectStartDate], 
    [ProjectFinishDate], 
    [PaymentTermsReCurrency], 
    [AdvancePercent], 
    [RetentionPercent], 
    [RetentionTerms], 
    [RetentionTermsAddComment], 
    [ValidatedBOQmarginPercent], 
    [IVexpectedSubmissionDate], 
    [IvexpectedApprovalDate], 
    [ExpectedPaymentDate], 
    [PenaltyClause], 
    [PenaltyTerms], 
    [ContractAmountWithVAT], 
    [CreatedBy], [UpdatedBy], 
    [DeletedBy], 
    [PersonCreated], 
    [PersonUpdated], 
    [PersonDeleted] 
    FROM [Table_ContractClientData] 
    WHERE ([ContractID] = @ContractID)"
    
    InsertCommand=" INSERT INTO [Table_ContractClientData]
           ([ContractID]
           ,[ProjectID_PTS]
           ,[ProjectManager]
           ,[ProjectStartDate]
           ,[ProjectFinishDate]
           ,[PaymentTermsReCurrency]
           ,[AdvancePercent]
           ,[RetentionPercent]
           ,[RetentionTerms]
           ,[RetentionTermsAddComment]
           ,[ValidatedBOQmarginPercent]
           ,[IVexpectedSubmissionDate]
           ,[IvexpectedApprovalDate]
           ,[ExpectedPaymentDate]
           ,[PenaltyClause]
           ,[PenaltyTerms]
           ,[ContractAmountWithVAT]
           ,[CreatedBy]
           ,[PersonCreated])
     VALUES
           (@ContractID
           ,@ProjectID_PTS
           ,@ProjectManager
           ,@ProjectStartDate
           ,@ProjectFinishDate
           ,@PaymentTermsReCurrency
           ,@AdvancePercent
           ,@RetentionPercent
           ,@RetentionTerms
           ,@RetentionTermsAddComment
           ,@ValidatedBOQmarginPercent
           ,@IVexpectedSubmissionDate
           ,@IvexpectedApprovalDate
           ,@ExpectedPaymentDate
           ,@PenaltyClause
           ,@PenaltyTerms
           ,@ContractAmountWithVAT
           ,@CreatedBy
           ,@PersonCreated) " 
      
      UpdateCommand = " UPDATE [Table_ContractClientData]
           SET 
               ProjectID_PTS = @ProjectID_PTS 
              ,ProjectManager = @ProjectManager 
              ,ProjectStartDate = @ProjectStartDate 
              ,ProjectFinishDate = @ProjectFinishDate 
              ,PaymentTermsReCurrency = @PaymentTermsReCurrency 
              ,AdvancePercent = @AdvancePercent 
              ,RetentionPercent = @RetentionPercent 
              ,RetentionTerms = @RetentionTerms 
              ,RetentionTermsAddComment = @RetentionTermsAddComment 
              ,ValidatedBOQmarginPercent = @ValidatedBOQmarginPercent 
              ,IVexpectedSubmissionDate = @IVexpectedSubmissionDate 
              ,IvexpectedApprovalDate = @IvexpectedApprovalDate 
              ,ExpectedPaymentDate = @ExpectedPaymentDate 
              ,PenaltyClause = @PenaltyClause 
              ,PenaltyTerms = @PenaltyTerms 
              ,ContractAmountWithVAT = @ContractAmountWithVAT 
              ,UpdatedBy = @UpdatedBy 
              ,PersonUpdated = @PersonUpdated 
         WHERE ContractID = @ContractID "  
        
       DeleteCommand = " DELETE FROM [Table_ContractClientData] WHERE ContractID = @ContractID " >
    <SelectParameters>
      <asp:QueryStringParameter DefaultValue="0" Name="ContractID" 
        QueryStringField="ContractID" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
      <asp:QueryStringParameter DefaultValue="0" Name="ContractID" 
        QueryStringField="ContractID" Type="Int32" />
    </UpdateParameters>
    <DeleteParameters>
      <asp:QueryStringParameter DefaultValue="0" Name="ContractID" 
        QueryStringField="ContractID" Type="Int32" />
    </DeleteParameters>
  </asp:SqlDataSource>
</asp:Content>

