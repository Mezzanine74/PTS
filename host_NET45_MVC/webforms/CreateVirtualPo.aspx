<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="CreateVirtualPo.aspx.vb" Inherits="CreateVirtualPo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

		<script language="javascript"  type="text/javascript">
		    function SetAutoCompleteWidth(source, EventArgs) {
		        var a
		        a = document.getElementById('<%=(Master.FindControl("MainContent")).FindControl("AutoCompleteDiv").ClientID%>');
                a.style.width = '441px';
            }
		</script>

	<style type="text/css">

		#AutoCompleteDiv
		{
			width: 64px;
		}
		</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


	<asp:Panel ID="PanelVirtualPo" runat="server">
		<table class="table-condensed">
			<tr>
				<td>
					Project Name:
				</td>
				<td>
						<asp:DropDownList ID="DropDownListProject" runat="server"  
							DataSourceID="SqlDataSourceProjects" 
							DataTextField="ProjectName" DataValueField="ProjectID">
						</asp:DropDownList>
						<asp:CompareValidator ID="CompareValidatorDDLProject" runat="server" 
							ErrorMessage="Should be selected" ControlToValidate="DropDownListProject" 
							ValueToCompare="0" Operator="NotEqual" CssClass="LabelGeneral">
						</asp:CompareValidator>
						<asp:SqlDataSource ID="SqlDataSourceProjects" runat="server" 
							ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
							SelectCommand="
							SELECT ProjectID, ProjectName FROM (
							SELECT 0 AS ProjectID, N'_SELECT PROJECT' AS ProjectName

							UNION ALL

							SELECT dbo.Table1_Project.ProjectID, 
							RTRIM(dbo.Table1_Project.ProjectName) + ' - ' + RTRIM(CONVERT(nchar(5), dbo.Table1_Project.ProjectID))  
							AS ProjectName
							FROM dbo.Table1_Project
							WHERE dbo.Table1_Project.CurrentStatus = 1
							) AS Source ORDER BY ProjectName ">
						</asp:SqlDataSource>
				</td>
			</tr>
			<tr>
				<td>
					Supplier Name:
				</td>
				<td>
						<asp:TextBox ID="SupplierIDTextBox" runat="server" AutoPostBack="True" 
							OnTextChanged="SupplierIDTextBox_TextChanged" />
						<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
							ControlToValidate="SupplierIDTextBox" CssClass="LabelGeneral" 
							ErrorMessage="Required"></asp:RequiredFieldValidator>
						<div ID="AutoCompleteDiv" 
							class="LabelGeneral"  runat="server">
						</div>
						<asp:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" 
							runat="server" FilterType="Numbers" TargetControlID="SupplierIDTextBox">
						</asp:FilteredTextBoxExtender>
						<asp:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" 
							CompletionInterval="0" CompletionListElementID="AutoCompleteDiv" 
							CompletionSetCount="12" MinimumPrefixLength="0" 
							onclientshown="SetAutoCompleteWidth" ServiceMethod="haydi" 
							ServicePath="AutoComplete.asmx" TargetControlID="SupplierIDTextBox">
						</asp:AutoCompleteExtender>
						<asp:Label ID="LabelSplrError" runat="server" 
						 CssClass="LabelGeneral" Font-Bold="True" ForeColor="#FF3300">
						</asp:Label>
				</td>
			</tr>
			<tr>
				<td>
					PO Value With VAT:
				</td>
				<td>
                            <asp:TextBox ID="TotalPriceTextBox" runat="server"  />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="TotalPriceTextBox" CssClass="LabelGeneral" Display="Dynamic"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="TotalPriceTextBox" CssClass="LabelGeneral" Display="Dynamic"
                                ErrorMessage="not valid number" 
                                ValidationExpression="([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)|([0-9]+)">
                                </asp:RegularExpressionValidator>                            
				</td>
			</tr>
            <tr>
                <td>
                    Currency:
                </td>
                <td>
                            <asp:DropDownList ID="DropDownListCurrency" runat="server" >
                                <asp:ListItem Value="0">SELECT CURRENCY</asp:ListItem>
                                <asp:ListItem Value="Rub">Ruble</asp:ListItem>
                                <asp:ListItem Value="Dollar">Dollar</asp:ListItem>
                                <asp:ListItem Value="Euro">Euro</asp:ListItem>
                            </asp:DropDownList>                            
						<asp:CompareValidator ID="CompareValidator1" runat="server" 
							ErrorMessage="Should be selected" ControlToValidate="DropDownListCurrency" 
							ValueToCompare="0" Operator="NotEqual" CssClass="LabelGeneral">
						</asp:CompareValidator>
                </td>
            </tr>
			<tr>
				<td>
					VAT Percent:
				</td>
				<td>
                            <asp:TextBox ID="TextBoxVATpercent" runat="server" ></asp:TextBox>   
                            <asp:FilteredTextBoxExtender ID="TextBoxVATpercent_FilteredTextBoxExtender" 
                                runat="server" FilterType="Numbers" TargetControlID="TextBoxVATpercent">
                            </asp:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="TextBoxVATpercent" CssClass="LabelGeneral" Display="Dynamic"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server"  Display="Dynamic"
                                ErrorMessage="range to be 0-20" ControlToValidate="TextBoxVATpercent" 
                                CssClass="LabelGeneral" MaximumValue="20" MinimumValue="0" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
		</table>
		<asp:Button ID="ButtonEnter" runat="server" CssClass="btn btn-mini btn-default" Text="Create Virtual Items" OnClick="ButtonEnter_Click" />
	</asp:Panel>


</asp:Content>

