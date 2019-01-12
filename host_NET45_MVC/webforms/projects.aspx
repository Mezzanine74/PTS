<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="projects.aspx.vb" Inherits="projectsREV" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


<style>

table, th, td {
   border: 1px solid #D3D3D3;
}
 

</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:LinkButton ID="LinkButtonAdd4Doc" runat="server" CausesValidation="false" ForeColor="blue"  Text="Add or Update Documents" OnClick="LinkButtonAdd4Doc_Click"/>
    <asp:Literal ID="LiteralSayac" runat="server" Visible="false" Text="0"></asp:Literal>

<table>
    <tr style="text-align:center;">
        <td style="width:100px; font-size:10px;">
            GENERAL INSURANCE POLICY
        </td>
        <td style="width:100px; font-size:10px; display:none;">
            Addendum 1 to GENERAL INSURANCE POLICY
        </td>
        <td style="width:100px; font-size:10px;">
            S.R.O.-S INSURANCE
        </td>
        <td style="width:100px; font-size:10px;">
            S.R.O.-P INSURANCE
        </td>
        <td style="width:100px; font-size:10px;">
            G.O. + PROF. INSURANCE
        </td>
        <td style="width:100px; font-size:10px;">
            &nbsp;</td>
        <td style="width:100px; font-size:10px;">
            &nbsp;</td>
        <td style="width:100px; font-size:10px;">
            <font style="color:blue;">Insurance Documents:</font> <br /> Damage To Our Works</td>
        <td style="width:100px; font-size:10px;">
            <font style="color:blue;">Insurance Documents:</font> <br /> Damage To Third Person</td>
    </tr>
    <tr style="text-align:center;">
        <td>
            <asp:ImageButton ID="ImageButtonDoc1" runat="server"  CausesValidation="False" OnClick="ImageButtonDoc1_Click" />
        </td>
        <td style=" display:none;">
            <asp:ImageButton ID="ImageButtonAdd1" runat="server"  CausesValidation="False" OnClick="ImageButtonAdd1_Click" />
        </td>
        <td>
            <asp:ImageButton ID="ImageButtonDoc2" runat="server"  CausesValidation="False" OnClick="ImageButtonDoc2_Click" />
        </td>
        <td>
            <asp:ImageButton ID="ImageButtonDoc3" runat="server"  CausesValidation="False" OnClick="ImageButtonDoc3_Click" />
        </td>
        <td>
            <asp:ImageButton ID="ImageButtonDoc4" runat="server"  CausesValidation="False" OnClick="ImageButtonDoc4_Click" />
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:ImageButton ID="ImageButtonIns1" runat="server"  CausesValidation="False" OnClick="ImageButtonIns1_Click"  />
        </td>
        <td>
            <asp:ImageButton ID="ImageButtonIns2" runat="server"  CausesValidation="False" OnClick="ImageButtonIns2_Click" />
        </td>
    </tr>
        <asp:Panel ID="Panel4DocActivation" runat="server" Visible="false">
            <tr style="font-size:10px;">
                <td>
                    Delete?&nbsp;<asp:CheckBox ID="CheckBoxDoc1" runat="server" /><br />
                    <asp:FileUpload ID="FileUploadDoc1" runat="server" /> 
                    <br />
                    <asp:Button id="ButtonUploadDoc1" runat="server" Text="Upload File" OnClick="ButtonUploadDoc1_Click" />
                    <asp:Button id="ButtonSaveDoc1" runat="server" Text="Save File" OnClick="ButtonSaveDoc1_Click" />
                    <br />
                    <asp:Label ID="LabelFileUploadInfo1" runat="server" ></asp:Label>
                    <asp:Label ID="LabelLinkDoc1" runat="server" CssClass="hidepanel"></asp:Label>
                </td>
                <td>
                    <%--Empty for addendums--%>
                </td>
                <td>
                    Delete?&nbsp;<asp:CheckBox ID="CheckBoxDoc2" runat="server" /> <br />
                    <asp:FileUpload ID="FileUploadDoc2" runat="server" /> 
                    <br />
                    <asp:Button id="ButtonUploadDoc2" runat="server" Text="Upload File" OnClick="ButtonUploadDoc2_Click" />
                    <asp:Button id="ButtonSaveDoc2" runat="server" Text="Save File" OnClick="ButtonSaveDoc2_Click" />
                    <br />
                    <asp:Label ID="LabelFileUploadInfo2" runat="server" ></asp:Label>
                    <asp:Label ID="LabelLinkDoc2" runat="server" CssClass="hidepanel"></asp:Label>
                </td>
                <td>
                    Delete?&nbsp;<asp:CheckBox ID="CheckBoxDoc3" runat="server" /> <br />
                    <asp:FileUpload ID="FileUploadDoc3" runat="server" /> 
                    <br />
                    <asp:Button id="ButtonUploadDoc3" runat="server" Text="Upload File" OnClick="ButtonUploadDoc3_Click" />
                    <asp:Button id="ButtonSaveDoc3" runat="server" Text="Save File" OnClick="ButtonSaveDoc3_Click" />
                    <br />
                    <asp:Label ID="LabelFileUploadInfo3" runat="server" ></asp:Label>
                    <asp:Label ID="LabelLinkDoc3" runat="server" CssClass="hidepanel"></asp:Label>
                </td>
                <td>
                    Delete?&nbsp;<asp:CheckBox ID="CheckBoxDoc4" runat="server" /><br />
                    <asp:FileUpload ID="FileUploadDoc4" runat="server" /> 
                    <br />
                    <asp:Button id="ButtonUploadDoc4" runat="server" Text="Upload File" OnClick="ButtonUploadDoc4_Click" />
                    <asp:Button id="ButtonSaveDoc4" runat="server" Text="Save File" OnClick="ButtonSaveDoc4_Click" />
                    <br />
                    <asp:Label ID="LabelFileUploadInfo4" runat="server" ></asp:Label>
                    <asp:Label ID="LabelLinkDoc4" runat="server" CssClass="hidepanel"></asp:Label>
                </td>
            </tr>
        </asp:Panel>
</table>

  <asp:Label ID="LabelKeepProjectID" runat="server" CssClass="hidepanel" ></asp:Label>
  <asp:Label ID="LabelFormViewVisibleStatus" runat="server" Text="0"  CssClass="hidepanel"></asp:Label>

<table>
 <tr>
  <td>
        <asp:ImageButton ID="ImageButtonAddProject" runat="server" 
          ImageUrl="~/Images/insert.png" CausesValidation="False" />
  </td>
  <td>
  <asp:FormView ID="FormViewInsertProject" runat="server" DataKeyNames="ProjectID"  Visible= "false"
    DataSourceID="SqlDataSourceInsertProject" EnableModelValidation="True" DefaultMode="Insert">
    <InsertItemTemplate>
     <asp:Panel runat="server" ID="pnlLogin" DefaultButton="InsertButton">
        <table>
          <tr>
          <td style="width: 80px; text-align: center;">
                <asp:Label ID="LabelProjectNo" runat="server" Text="Project No"  ></asp:Label>
          </td>
          <td style="width: 200px; text-align: center;">
                <asp:Label ID="LabelProjectName" runat="server" Text="Project Name" CssClass="LabelGeneral" ></asp:Label>
          </td>
          <td style="width: 80px; text-align: center;">
                <asp:Label ID="LabelCurrent" runat="server" Text="Current" CssClass="LabelGeneral" ></asp:Label>
          </td>
          <td style="width: 100px; text-align: center;">
                <asp:Label ID="LabelType" runat="server" Text="Project Type" CssClass="LabelGeneral" ></asp:Label>
          </td>
          <td style="width: 80px; text-align: center;">
                <asp:Label ID="LabelReporting" runat="server" Text="Reporting" CssClass="LabelGeneral" ></asp:Label>
          </td>
          <td style="width: 80px; text-align: center;">
                <asp:Label ID="LabelBackUp" runat="server" Text="BackUp" CssClass="LabelGeneral" ></asp:Label>
          </td>
          <td style="width: 80px; text-align: center;">
                <asp:Label ID="LabelPOcreate" runat="server" Text="POcreate" CssClass="LabelGeneral" ></asp:Label>
          </td>
          <td>
          </td>
        </tr>
        <tr>
          <td style="text-align: center; background-color: #F5F5F5;">
            <asp:TextBox ID="TextBoxProjectNo" runat="server" Width="50px"
              Text='<%# Bind("ProjectID") %>' />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="enter"
                    ErrorMessage="!" ControlToValidate="TextBoxProjectNo" Display="Dynamic"></asp:RequiredFieldValidator>
          </td>
          <td style="text-align: center; background-color: #F5F5F5;">
            <asp:TextBox ID="TextBoxProjectName" runat="server"  Width="150px"
              Text='<%# Bind("ProjectName") %>' />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="enter"
                    ErrorMessage="!" ControlToValidate="TextBoxProjectName" Display="Dynamic"></asp:RequiredFieldValidator>
          </td>
          <td style="text-align: center; background-color: #F5F5F5;">
            <asp:CheckBox ID="CheckBoxCurrent" runat="server" 
              Checked='<%# Bind("CurrentStatus") %>' />
          </td>
          <td style="text-align: center; background-color: #F5F5F5;">
                          <asp:DropDownList ID="DropDownListProjectType" runat="server"   Width="90px"
                              DataSourceID="SqlDataSourceProjectType" DataTextField="Type" DataValueField="Type" >
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
             <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" ValidationGroup="enter"
              CommandName="Insert" Text="Insert" CssClass="btn btn-mini btn-success" onmouseover="this.style.cursor='hand'" />
          </td>
        </tr>
        </table>
     </asp:Panel>
    </InsertItemTemplate>
  </asp:FormView>
  <asp:SqlDataSource ID="SqlDataSourceInsertProject" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    InsertCommand="INSERT INTO [Table1_Project] ([ProjectID], [ProjectName], [CurrentStatus], [Type], [Report], [BackUpRequired], [POcreate]) VALUES (@ProjectID, @ProjectName, @CurrentStatus, @Type, @Report, @BackUpRequired, @POcreate)" >
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

<div id="ModalInsuranceCertificate" class="modal"> 
    <div class="modal-dialog modal-dialog-center">
        <div class="modal-content modal_inlineBlock">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Enter your data</h4>
            </div>
            <div class="modal-body" style="width:750px;">

                <asp:TextBox ID="ProjectIDTextBox" runat="server" CssClass="hidepanel"  />
                <asp:Label ID="LabelProjectName" runat="server" ForeColor="Blue" Font-Size="X-Large" ></asp:Label>

                <asp:FormView ID="FormViewInsCert" runat="server" DataKeyNames="id" DataSourceID="ObjectDataSourceInsCert" DefaultMode="Insert" Visible="false" CssClass="table">
                    <InsertItemTemplate>

                        <div class="form-horizontal">
                            <div class="col-xs-12" >
                                    <div class="form-group">
                                        <asp:Label ID="LabelRemarks" runat="server" AssociatedControlID="TextBoxRemarks" CssClass="col-xs-6 control-label" Text="Remarks: "></asp:Label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="TextBoxRemarks" runat="server" CssClass="form-control"
                                               TextMode="MultiLine" Text='<%# Bind("Remarks") %>' />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="LabelInsStart" runat="server" AssociatedControlID="TextBoxInsStart" CssClass="col-xs-6 control-label" Text="Insurance Start: "></asp:Label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="TextBoxInsStart" runat="server" CssClass="form-control add_datepicker"
                                               Text='<%# Bind("InsuranceStart")%>' />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorInsStart" ControlToValidate="TextBoxInsStart" Display="Dynamic"
                                            runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" >
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" AssociatedControlID="TextBoxInsFinish" CssClass="col-xs-6 control-label" Text="Insurance Finish: "></asp:Label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="TextBoxInsFinish" runat="server" CssClass="form-control add_datepicker"
                                               Text='<%# Bind("InsuranceFinish")%>' />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorInsFinish2" ControlToValidate="TextBoxInsFinish" Display="Dynamic"
                                            runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" >
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="LabelDoc" runat="server" AssociatedControlID="FileUploadInsuranceCertificate" CssClass="col-xs-6 control-label" Text="Insurance Document: "></asp:Label>
                                        <div class="col-xs-6">
                                            <asp:FileUpload ID="FileUploadInsuranceCertificate" runat="server" />
                                            <asp:Button id="ButtonUploadInsuranceCertificate" runat="server" Text="Upload File" OnClick="ButtonUploadInsuranceCertificate_Click"
                                                CssClass="btn btn-mini btn-success" />
                                            <asp:TextBox ID="TextBoxFileLink" runat="server" CssClass="hidepanel" Text='<%# Bind("DocumentReference") %>' ></asp:TextBox> 
                                            <br />
                                            <asp:Label ID="LabelFileUploaded" runat="server" ></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-xs-offset-6">
                                        <asp:LinkButton id="ButtonInsertInsuranceCertificate" runat="server" Text="INSERT INSURANCE CERTIFICATE" OnClick="ButtonInsertInsuranceCertificate_Click" 
                                            CommandName="Insert" CssClass="btn btn-mini btn-primary"/>
                                    </div>

                            </div>
                        </div>


                    </InsertItemTemplate>
                </asp:FormView>
                   <hr />
                <asp:ObjectDataSource ID="ObjectDataSourceInsCert" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PTS_App_Code_VB_Project.MercuryTableAdapters.Table1_ProjectInsurCertfTableAdapter">
                    <InsertParameters>
                        <asp:ControlParameter Name="ProjectID" Type="Int16" ControlID="ProjectIDTextBox" DefaultValue="0" />
                        <asp:Parameter Name="Remarks" Type="String" />
                        <asp:Parameter Name="InsuranceStart" Type="DateTime" />
                        <asp:Parameter Name="InsuranceFinish" Type="DateTime" />
                        <asp:Parameter Name="DocumentReference" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>

                <asp:LinkButton ID="LinkButtonShowHide" runat="server" OnClick="LinkButtonShowHide_Click" CssClass="btn btn-mini btn-primary"  >
                  <i class="ace-icon glyphicon glyphicon-plus "></i>
                    Add New Item
                </asp:LinkButton>

                <hr />

                <asp:GridView ID="GridViewInstCert" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ObjectDataSourceInstCertForGrid" 
                    CssClass="table table-nonfluid table-hover" EmptyDataText="No information yet! You can insert by using button above." GridLines="None" >
                    <Columns>

                                <asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                                    <EditItemTemplate>
                                        <div class="btn-group-vertical" role="group" >
                                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CssClass="btn btn-minier btn-primary" OnClick="LinkButtonUpdate_Click"
                                                CommandName="Update" Text="Update"></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success " OnClick="LinkButton3_Click"
                                                CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                        </div>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <div class="btn-group-vertical" role="group">
                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success " OnClick="LinkButtonEdit_Click"
                                                CommandName="Edit" Text="Edit"></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonDelete" runat="server" CssClass="btn btn-minier btn-danger" OnClick="LinkButtonDelete_Click"
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                CommandName="Delete" Text="Delete"></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="150px" ItemStyle-Width="150px" >
                            <ItemTemplate>
                                        <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks")%>' >
                                        </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                        <asp:TextBox ID="TextBoxRemarks" runat="server" TextMode="MultiLine" Text='<%# Bind("Remarks") %>' CssClass="TextBoxGeneralRevMultiline" ></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Insurance Start" HeaderStyle-Width="100px" ItemStyle-Width="100px" ControlStyle-Width="100" >
                            <ItemTemplate>
                                        <asp:Label ID="LabelInsuranceStart" runat="server" Text='<%# Bind("InsuranceStart", "{0:dd/MM/yyyy}")%>' >
                                        </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxInsStart" runat="server" Text='<%# Bind("InsuranceStart", "{0:dd/MM/yyyy}")%>' CssClass="TextBoxGeneralRev add_datepicker"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorInsStart" ControlToValidate="TextBoxInsStart" Display="Dynamic"
                                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" >
                                    </asp:RegularExpressionValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Insurance Finish" HeaderStyle-Width="100px" ItemStyle-Width="100px" ControlStyle-Width="100" >
                            <ItemTemplate>
                                        <asp:Label ID="LabelInsuranceFinish" runat="server" Text='<%# Bind("InsuranceFinish", "{0:dd/MM/yyyy}")%>' >
                                        </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxInsFinish" runat="server" Text='<%# Bind("InsuranceFinish", "{0:dd/MM/yyyy}")%>' CssClass="TextBoxGeneralRev add_datepicker" ></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorInsFinish" ControlToValidate="TextBoxInsFinish" Display="Dynamic"
                                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" >
                                    </asp:RegularExpressionValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Doc" >
                            <ItemTemplate>
                                        <asp:ImageButton ID="ImageButtonItem" runat="server" CommandName="OpenDocument"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DocumentReference")%>'/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                        Delete?&nbsp;&nbsp;<asp:CheckBox ID="CheckBoxDelete" runat="server" />
                                        <br />
                                        <asp:FileUpload ID="FileUploadInsuranceCertificate" runat="server" />
                                        &nbsp;
                                        <asp:Button ID="ButtonUploadInsuranceCertificate" runat="server" Text="Upload" CausesValidation="false"  
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="UploadOnEdit" />
                                        <asp:TextBox ID="TextBoxFileLink" runat="server" CssClass="hidepanel" Text='<%# Bind("DocumentReference")%>' >
                                        </asp:TextBox>
                                        <br />
                                        <asp:Label ID="LabelFileUploaded" runat="server" ></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSourceInstCertForGrid" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByProjectID" TypeName="PTS_App_Code_VB_Project.MercuryTableAdapters.Table1_ProjectInsurCertfTableAdapter" UpdateMethod="Update">
                    <DeleteParameters>
                        <asp:Parameter Name="Original_id" Type="Int32" />
                    </DeleteParameters>
                    <SelectParameters>
                        <asp:ControlParameter Name="ProjectID" Type="Int16" ControlID="ProjectIDTextBox" DefaultValue="0" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:ControlParameter Name="ProjectID" Type="Int16" ControlID="ProjectIDTextBox" DefaultValue="0" />
                        <asp:Parameter Name="Remarks" Type="String" />
                        <asp:Parameter Name="InsuranceStart" Type="DateTime" />
                        <asp:Parameter Name="InsuranceFinish" Type="DateTime" />
                        <asp:Parameter Name="DocumentReference" Type="String" />
                        <asp:Parameter Name="Original_id" Type="Int32" />
                    </UpdateParameters>
                </asp:ObjectDataSource>

            </div>
        </div>
    </div>
</div>


  <asp:GridView ID="GridViewProjects" runat="server" AllowPaging="True" GridLines="None" 
    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ProjectID" PagerSettings-Position="TopAndBottom"
    DataSourceID="SqlDataSourceProjects" EnableModelValidation="True" 
    CssClass="table table-nonfluid table-hover" PageSize="50">
    <Columns>

                    <asp:TemplateField ShowHeader="False" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <div class="btn-group-vertical" role="group" >
                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CssClass="btn btn-minier btn-primary"
                                    CommandName="Update" Text="Update"  ></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
                                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </div>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <div class="btn-group-vertical" role="group">
                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CssClass="btn btn-minier btn-success "
                                    CommandName="Edit" Text="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonDelete" runat="server" CssClass="btn btn-minier btn-danger"
                                    OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                    CommandName="Delete" Text="Delete"></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

      <asp:TemplateField HeaderText="Project No" SortExpression="ProjectID" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="80" HeaderStyle-Width="80">
        <EditItemTemplate>
          <asp:TextBox ID="TextBoxProjectIDEdit" runat="server" Text='<%# Bind("ProjectID") %>'></asp:TextBox>
        </EditItemTemplate>
        <ItemTemplate>
          <h4><asp:Label ID="LabelProjectIDItem" runat="server" Text='<%# Bind("ProjectID") %>'></asp:Label></h4>
        </ItemTemplate>
      </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Image ID="ImageLogo" runat="server" ImageUrl='<%# Bind("Logo")%>' />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Image ID="ImageLogo" runat="server" ImageUrl='<%# Bind("Logo")%>' />
            </EditItemTemplate>
        </asp:TemplateField>
      <asp:TemplateField HeaderText="Project Name" SortExpression="ProjectName" ControlStyle-Width="200" HeaderStyle-Width="200">
        <EditItemTemplate>
          <asp:TextBox ID="TextBoxProjectNameEdit" runat="server" Text='<%# Bind("ProjectName") %>'></asp:TextBox>
          <asp:Panel ID="Color" runat="server">
              BackColor
              <br />
              <asp:TextBox ID="TextBoxBackColor" runat="server" Text='<%# Bind("BackColor") %>'></asp:TextBox>
              <br />
              ForeColor
              <asp:TextBox ID="TextBoxForeColor" runat="server" Text='<%# Bind("ForeColor") %>'></asp:TextBox>
          </asp:Panel>
        </EditItemTemplate>
        <ItemTemplate>
          <asp:Label ID="LabelProjectNameItem" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="  Current?  " SortExpression="CurrentStatus" ItemStyle-HorizontalAlign="Center" >
        <EditItemTemplate>
          <asp:CheckBox ID="CheckBoxCurrentStatusEdit" runat="server" 
            Checked='<%# Bind("CurrentStatus") %>' />
        </EditItemTemplate>
        <ItemTemplate>
          <asp:CheckBox ID="CheckBoxCurrentStatusItem" runat="server" 
            Checked='<%# Bind("CurrentStatus") %>' Visible="false" />
          <asp:Image ID="ImageCurrentStatus" runat="server" ImageUrl="~/Images/BlueDot60.png" />
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Type" SortExpression="Project Type" ControlStyle-Width="100" HeaderStyle-Width="100">
        <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListProjectType" runat="server" 
                        selectedvalue='<%# Bind("Type") %>' AppendDataBoundItems="True"
                        DataSourceID="SqlDataSourceProjectType" DataTextField="Type" DataValueField="Type" >
                    </asp:DropDownList>

                    <asp:SqlDataSource ID="SqlDataSourceProjectType" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
                    SelectCommand="SELECT RTRIM([Type]) AS [Type] FROM [Table1_Project] GROUP BY [Type]">
                    </asp:SqlDataSource>
        </EditItemTemplate>
        <ItemTemplate>
          <asp:Label ID="LabelTypeItem" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Type" SortExpression="Project Type" ControlStyle-Width="100" HeaderStyle-Width="100">
         <ItemTemplate>
            <asp:Label ID="LabelCurrency" runat="server" Text='<%# Bind("ContractCurrency") %>'></asp:Label>
         </ItemTemplate>
         <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListCurrency" runat="server" 
                            selectedvalue='<%# Bind("ContractCurrency") %>' AppendDataBoundItems="True" >
                            <asp:ListItem Value="-">-</asp:ListItem>
                            <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                            <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                            <asp:ListItem Value="Euro">Euro</asp:ListItem>
                    </asp:DropDownList>  
         </EditItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="  Reporting?  " SortExpression="Report" ItemStyle-HorizontalAlign="Center" >
        <EditItemTemplate>
          <asp:CheckBox ID="CheckBoxReportRequiredEdit" runat="server" Checked='<%# Bind("Report") %>' />
        </EditItemTemplate>
        <ItemTemplate>
          <asp:CheckBox ID="CheckBoxReportRequiredItem" runat="server" Checked='<%# Bind("Report") %>' 
            Visible="false" />
          <asp:Image ID="ImageReport" runat="server" ImageUrl="~/Images/BlueDot60.png" />
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="  BackUp?  " SortExpression="BackUpRequired" ItemStyle-HorizontalAlign="Center" >
        <EditItemTemplate>
          <asp:CheckBox ID="CheckBoxBackUpRequiredEdit" runat="server" 
            Checked='<%# Bind("BackUpRequired") %>' />
        </EditItemTemplate>
        <ItemTemplate>
          <asp:CheckBox ID="CheckBoxBackUpRequiredItem" runat="server" 
            Checked='<%# Bind("BackUpRequired") %>' Visible="false" />
          <asp:Image ID="ImageBackUpRequired" runat="server" ImageUrl="~/Images/BlueDot60.png" />
        </ItemTemplate>
      </asp:TemplateField>
      
      <asp:TemplateField HeaderText="Nakladnaya Enabled?" SortExpression="NakladnayaEnabled" ItemStyle-HorizontalAlign="Center" >
        <EditItemTemplate>
          <asp:CheckBox ID="CheckBoxNakladnayaEnabledEdit" runat="server" 
            Checked='<%# Bind("NakladnayaEnabled") %>' />
        </EditItemTemplate>
        <ItemTemplate>
          <asp:CheckBox ID="CheckBoxNakladnayaEnabledItem" runat="server" 
            Checked='<%# Bind("NakladnayaEnabled") %>' Visible="false" />
          <asp:Image ID="ImageNakladnayaEnabledRequired" runat="server" ImageUrl="~/Images/BlueDot60.png" />
        </ItemTemplate>
      </asp:TemplateField>
      
      <asp:TemplateField HeaderText=" POcreate? " SortExpression="POcreate" ItemStyle-HorizontalAlign="Center" >
        <EditItemTemplate>
          <asp:CheckBox ID="CheckBoxPOcreateEdit" runat="server" 
            Checked='<%# Bind("POCreate") %>' />
        </EditItemTemplate>
        <ItemTemplate>
          <asp:CheckBox ID="CheckBoxPOcreateItem" runat="server" 
            Checked='<%# Bind("POCreate") %>' Visible="false" />
          <asp:Image ID="ImagePOcreate" runat="server" ImageUrl="~/Images/BlueDot60.png" />
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="DaySinceLastAction" SortExpression="DaySinceLastAction" ItemStyle-HorizontalAlign="Center" >
        <EditItemTemplate>
            <asp:Label ID="LabelDaySinceLastActionItem" runat="server" Text='<%# Bind("DaySinceLastAction")%>'  ></asp:Label>
        </EditItemTemplate>
        <ItemTemplate>
            <asp:Label ID="LabelDaySinceLastActionEdit" runat="server" Text='<%# Bind("DaySinceLastAction")%>'  ></asp:Label>        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Insurance Certificate Details" HeaderStyle-Width="100px">
        <ItemTemplate>
            <asp:LinkButton ID="LinkButtonInsuranceCertificate" runat="server" CommandName="InsuranceCertificate" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProjectID")%>' />
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Insurance_Start" SortExpression="InsuranceStart" ItemStyle-HorizontalAlign="Center" Visible="false"  >
        <EditItemTemplate>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorInsuranceStart" ControlToValidate="TextBoxInsuranceStart" Display="Dynamic"
                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" ></asp:RegularExpressionValidator>

                    <asp:TextBox ID="TextBoxInsuranceStart" runat="server" CssClass="FontSizeBGcolorForEditControls" Text='<%# Bind("InsuranceStart","{0:dd/MM/yyyy}")%>'></asp:TextBox>

                    <asp:CalendarExtender ID="TextBox7_CalendarExtenderStart" runat="server" CssClass="cal_Theme1"
                    TargetControlID="TextBoxInsuranceStart" format="dd/MM/yyyy" >
                    </asp:CalendarExtender>
        </EditItemTemplate>
        <ItemTemplate>
                    <asp:Label ID="LabelInsuranceStart" runat="server" Text='<%# Bind("InsuranceStart","{0:dd/MM/yyyy}")%>'  ></asp:Label>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Insurance_Finish" SortExpression="InsuranceFinish" ItemStyle-HorizontalAlign="Center" Visible="false" >
        <EditItemTemplate>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorInsuranceFinish" ControlToValidate="TextBoxInsuranceFinish" Display="Dynamic"
                    runat="server" ErrorMessage="dd/mm/yyyy"  ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" ></asp:RegularExpressionValidator>

                    <asp:TextBox ID="TextBoxInsuranceFinish" runat="server" CssClass="FontSizeBGcolorForEditControls" Text='<%# Bind("InsuranceFinish","{0:dd/MM/yyyy}")%>'></asp:TextBox>

                    <asp:CalendarExtender ID="TextBox7_CalendarExtenderFinish" runat="server" CssClass="cal_Theme1"
                    TargetControlID="TextBoxInsuranceFinish" format="dd/MM/yyyy" >
                    </asp:CalendarExtender>
        </EditItemTemplate>
        <ItemTemplate>
                    <asp:Label ID="LabelInsuranceFinish" runat="server" Text='<%# Bind("InsuranceFinish","{0:dd/MM/yyyy}")%>'  ></asp:Label>
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager2" />
                <HeaderStyle CssClass="headergridnew" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceProjects" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand=" SELECT     dbo.Table1_Project.ProjectID, RTRIM(dbo.Table1_Project.ProjectName) AS ProjectName, dbo.Table1_Project.CurrentStatus, RTRIM(dbo.Table1_Project.Type) AS Type, 
                      dbo.Table1_Project.Report, dbo.Table1_Project.BackUpRequired, dbo.Table1_Project.InsuranceStart, dbo.Table1_Project.InsuranceFinish, 
                      dbo.Table1_Project.POcreate, (CASE WHEN t1.DaySinceLastAction IS NULL THEN - 1 ELSE t1.DaySinceLastAction END) AS DaySinceLastAction, 
                      (CASE WHEN dbo.Table1_Project.ContractCurrency IS NULL THEN N'-' ELSE RTRIM(dbo.Table1_Project.ContractCurrency) END) AS ContractCurrency, 
                      dbo.Table1_Project.NakladnayaEnabled, dbo.Table1_Project.BackColor, dbo.Table1_Project.ForeColor, RTRIM(Logo) AS Logo
FROM         dbo.Table1_Project LEFT OUTER JOIN
                          (SELECT     Table1_Project_1.ProjectID AS ProjectIDD, DATEDIFF(day, CASE WHEN maxpaymentdate IS NULL 
                                                   THEN maxpo_date WHEN maxpo_date > maxpaymentdate THEN maxpo_date WHEN maxpo_date < maxpaymentdate THEN maxpaymentdate WHEN maxpo_date
                                                    = maxpaymentdate THEN maxpaymentdate END, GETDATE()) AS DaySinceLastAction
                            FROM          dbo.Table1_Project AS Table1_Project_1 LEFT OUTER JOIN
                                                   dbo.View_SIL_MinPOdatePerProject ON Table1_Project_1.ProjectID = dbo.View_SIL_MinPOdatePerProject.ProjectID LEFT OUTER JOIN
                                                   dbo.View_SILmAxPaymentDatePerProject ON Table1_Project_1.ProjectID = dbo.View_SILmAxPaymentDatePerProject.ProjectID LEFT OUTER JOIN
                                                   dbo.View_SILMaxPoDatePerProject ON Table1_Project_1.ProjectID = dbo.View_SILMaxPoDatePerProject.ProjectID
                            WHERE      (dbo.View_SIL_MinPOdatePerProject.MinPO_Date IS NOT NULL)) AS t1 ON dbo.Table1_Project.ProjectID = t1.ProjectIDD "
    DeleteCommand="DELETE FROM [Table1_Project] WHERE ProjectID = @ProjectID">
    <UpdateParameters>
     <asp:Parameter Name="BackColor"  />
     <asp:Parameter Name="ForeColor"  />
    </UpdateParameters>
  </asp:SqlDataSource>

</asp:Content>

