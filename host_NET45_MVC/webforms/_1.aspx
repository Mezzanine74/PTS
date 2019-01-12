<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_1.aspx.cs" Inherits="host_NET45_MVC.webforms._1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <asp:GridView ID="GridViewShowContract" runat="server" AutoGenerateColumns="True" 
        DataSourceID="SqlDataSourceForContractGrid" DataKeyNames="ContractID" AllowPaging="true" PageSize="20" >
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceForContractGrid" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        SelectCommand="ContractGrid_Select" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" >
                         <SelectParameters>
                             <asp:Parameter Name="ProjectID" Type="Int16" DefaultValue="210" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SupplierID" Type="String" DefaultValue="" ConvertEmptyStringToNull="false" />
                             <asp:Parameter Name="entrytimeinterval" Type="String" DefaultValue="-" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="approvaltimeinterval" Type="String" DefaultValue="-" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="UserName" Type="String" DefaultValue="savas" ConvertEmptyStringToNull="false"/>

                             <asp:Parameter Name="SearchAdvanceMode" Type="Int16" DefaultValue="1" ConvertEmptyStringToNull="false"/>

                             <asp:Parameter Name="SearchInputProjects" Type="String" DefaultValue="" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputSuppliers" Type="String" DefaultValue="N'7727649295',N'7721802701'" ConvertEmptyStringToNull="true" />
                             <asp:Parameter Name="SearchInputContractNo" Type="String" DefaultValue="" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputContractDate" Type="String" DefaultValue="" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputContractDescription" Type="String" DefaultValue="" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputSignBysupplier" Type="String" DefaultValue="" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputSignByMercury" Type="String" DefaultValue="" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputAdvance" Type="String" DefaultValue="" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputRetention" Type="String" DefaultValue="" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputDraft" Type="String" DefaultValue="" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputFinal" Type="String" DefaultValue="" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputContractTypeSer" Type="Boolean" DefaultValue="True" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputContractTypeSub" Type="Boolean" DefaultValue="True" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputContractTypeSup" Type="Boolean" DefaultValue="True" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputCurrencyRuble" Type="Boolean" DefaultValue="True" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputCurrencyDollar" Type="Boolean" DefaultValue="True" ConvertEmptyStringToNull="false"/>
                             <asp:Parameter Name="SearchInputCurrencyEuro" Type="Boolean" DefaultValue="True" ConvertEmptyStringToNull="false"/>
                        </SelectParameters>   
    </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
            SelectCommand="ApprovalStatusContractEF" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="ContractId" Type="Int32" DefaultValue="6100" />
            </SelectParameters>
        </asp:SqlDataSource>


    </div>
    </form>
</body>
</html>
