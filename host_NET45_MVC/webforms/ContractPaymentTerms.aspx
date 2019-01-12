<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="ContractPaymentTerms.aspx.vb" Inherits="ContractPaymentTerms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <table style="width: 80%;">
      <tr style="font-size: 10px; text-align: center">
        <td>
          Advance<br />
          %
        </td>
        <td>
          Progress<br />
          payment<br />
          %
        </td>
        <td>
          Payment<br />
          After<br />
          delivery<br />
          %
        </td>
        <td>
          Payment<br />
          term<br />
          days
        </td>
        <td>
          Credit<br />
          amount,
          <br />
          RUR
          <br />
          incl VAT
        </td>
        <td>
          Penalties<br />
          for<br />
          late<br />
          payment
        </td>
        <td>
          Penalties<br />
          limit
        </td>
        <td>
        </td>
      </tr>
      <tr>
        <td>
          <asp:TextBox ID="TextBoxAdvance" runat="server" CssClass="TextBoxGeneral">
          </asp:TextBox>
        </td>
        <td>
          <asp:TextBox ID="TextBoxProgressPayment" runat="server" CssClass="TextBoxGeneral">
          </asp:TextBox>
        </td>
        <td>
          <asp:TextBox ID="TextBoxPaymentAfterDelivery" runat="server" CssClass="TextBoxGeneral">
          </asp:TextBox>
        </td>
        <td>
          <asp:TextBox ID="TextBoxPaymentTermDays" runat="server" CssClass="TextBoxGeneral">
          </asp:TextBox>
        </td>
        <td>
          <asp:TextBox ID="TextBoxCreditAmount" runat="server" CssClass="TextBoxGeneral">
          </asp:TextBox>
        </td>
        <td>
          <asp:TextBox ID="TextBoxPenaltiesForLatePayment" runat="server" CssClass="TextBoxGeneral">
          </asp:TextBox>
        </td>
        <td>
          <asp:TextBox ID="TextBoxPenaltiesLimit" runat="server" CssClass="TextBoxGeneral">
          </asp:TextBox>
        </td>
        <td>
          <asp:LinkButton ID="LinkButtonInsert" runat="server" Font-Size="10px">Insert</asp:LinkButton>
        </td>
      </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <asp:GridView ID="GridViewContractPaymentTerm" runat="server" AutoGenerateColumns="False" 
      DataKeyNames="ContractTermID" DataSourceID="SqlDataSourceContractPaymentTerms" 
      EnableModelValidation="True">
      <Columns>
        <asp:TemplateField HeaderText="Advance" SortExpression="Advance">
         <asp:ItemTemplate>
         </asp:ItemTemplate>
         <asp:EditItemTemplate>
         </asp:EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ProgressPayment" 
          SortExpression="ProgressPayment"></asp:TemplateField>
        <asp:TemplateField HeaderText="PaymentAfterDelivery" 
          SortExpression="PaymentAfterDelivery"></asp:TemplateField>
        <asp:TemplateField HeaderText="PaymentTermDay" SortExpression="PaymentTermDay">
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CreditRubWthVAT" 
          SortExpression="CreditRubWthVAT"></asp:TemplateField>
        <asp:TemplateField HeaderText="PenaltiesForLatePayment" 
          SortExpression="PenaltiesForLatePayment"></asp:TemplateField>
        <asp:TemplateField HeaderText="PenaltiesLimit" SortExpression="PenaltiesLimit">
        </asp:TemplateField>
      </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceContractPaymentTerms" runat="server" 
      ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
      DeleteCommand="DELETE FROM [Table_ContractsTerms] WHERE [ContractTermID] = @ContractTermID" 
      InsertCommand="INSERT INTO [Table_ContractsTerms] ([ContractID], [Advance], [ProgressPayment], [PaymentAfterDelivery], [PaymentTermDay], [CreditRubWthVAT], [PenaltiesForLatePayment], [PenaltiesLimit]) VALUES (@ContractID, @Advance, @ProgressPayment, @PaymentAfterDelivery, @PaymentTermDay, @CreditRubWthVAT, @PenaltiesForLatePayment, @PenaltiesLimit)" 
      SelectCommand="SELECT [ContractTermID], [ContractID], [Advance], [ProgressPayment], [PaymentAfterDelivery], [PaymentTermDay], [CreditRubWthVAT], [PenaltiesForLatePayment], [PenaltiesLimit] FROM [Table_ContractsTerms]" 
      UpdateCommand="UPDATE [Table_ContractsTerms] SET [ContractID] = @ContractID, [Advance] = @Advance, [ProgressPayment] = @ProgressPayment, [PaymentAfterDelivery] = @PaymentAfterDelivery, [PaymentTermDay] = @PaymentTermDay, [CreditRubWthVAT] = @CreditRubWthVAT, [PenaltiesForLatePayment] = @PenaltiesForLatePayment, [PenaltiesLimit] = @PenaltiesLimit WHERE [ContractTermID] = @ContractTermID">
      <DeleteParameters>
        <asp:Parameter Name="ContractTermID" Type="Int32" />
      </DeleteParameters>
      <InsertParameters>
        <asp:Parameter Name="ContractID" Type="Int32" />
        <asp:Parameter Name="Advance" Type="Int32" />
        <asp:Parameter Name="ProgressPayment" Type="Int32" />
        <asp:Parameter Name="PaymentAfterDelivery" Type="Int32" />
        <asp:Parameter Name="PaymentTermDay" Type="Int32" />
        <asp:Parameter Name="CreditRubWthVAT" Type="Decimal" />
        <asp:Parameter Name="PenaltiesForLatePayment" Type="String" />
        <asp:Parameter Name="PenaltiesLimit" Type="String" />
      </InsertParameters>
      <UpdateParameters>
        <asp:Parameter Name="ContractID" Type="Int32" />
        <asp:Parameter Name="Advance" Type="Int32" />
        <asp:Parameter Name="ProgressPayment" Type="Int32" />
        <asp:Parameter Name="PaymentAfterDelivery" Type="Int32" />
        <asp:Parameter Name="PaymentTermDay" Type="Int32" />
        <asp:Parameter Name="CreditRubWthVAT" Type="Decimal" />
        <asp:Parameter Name="PenaltiesForLatePayment" Type="String" />
        <asp:Parameter Name="PenaltiesLimit" Type="String" />
        <asp:Parameter Name="ContractTermID" Type="Int32" />
      </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

