<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="ManageProjects.aspx.vb" Inherits="ManageProjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:FormView ID="FormViewProjects" runat="server" AllowPaging="True" 
        DataKeyNames="ProjectID" DataSourceID="SqlDataSourceProject">
        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" 
            LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />
        <EditItemTemplate>
            ProjectID:
            <asp:Label ID="ProjectIDLabel1" runat="server" 
                Text='<%# Eval("ProjectID") %>' />
            <br />
            ProjectName:
            <asp:TextBox ID="ProjectNameTextBox" runat="server" 
                Text='<%# Bind("ProjectName") %>' Width="350px" />
            <br />
            CurrentStatus:
            <asp:CheckBox ID="CurrentStatusCheckBox" runat="server" 
                Checked='<%# Bind("CurrentStatus") %>' />
             <br />   
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            ProjectID:
            <asp:TextBox ID="ProjectIDTextBox" runat="server" 
                Text='<%# Bind("ProjectID") %>' />
            <br />
            ProjectName:
            <asp:TextBox ID="ProjectNameTextBox" runat="server" 
                Text='<%# Bind("ProjectName") %>' />
            <br />
            CurrentStatus:
            <asp:CheckBox ID="CurrentStatusCheckBox" runat="server" 
                Checked='<%# Bind("CurrentStatus") %>' />
             <br />   
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            ProjectID:
            <asp:Label ID="ProjectIDLabel" runat="server" Text='<%# Eval("ProjectID") %>' />
            <br />
            ProjectName:
            <asp:Label ID="ProjectNameLabel" runat="server" 
                Text='<%# Bind("ProjectName") %>' />
            <br />
            CurrentStatus:
            <asp:CheckBox ID="CurrentStatusCheckBox" runat="server" 
                Checked='<%# Bind("CurrentStatus") %>' Enabled="false" />
             <br />     
            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" 
                CommandName="Edit" Text="Edit" />
            &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" 
                CommandName="Delete" Text="Delete" />
            &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" 
                CommandName="New" Text="New" />
        </ItemTemplate>
        <PagerStyle BackColor="#CCCCCC" ForeColor="Red" />
    </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSourceProject" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        DeleteCommand="DELETE FROM [Table1_Project] WHERE [ProjectID] = @ProjectID" 
        InsertCommand="INSERT INTO [Table1_Project] ([ProjectID], [ProjectName], [CurrentStatus]) VALUES (@ProjectID, @ProjectName, @CurrentStatus)" 
        SelectCommand="SELECT [ProjectID], RTRIM([ProjectName]) AS [ProjectName], [CurrentStatus] FROM [Table1_Project]" 
        UpdateCommand="UPDATE [Table1_Project] SET [ProjectName] = @ProjectName, [CurrentStatus] = @CurrentStatus WHERE [ProjectID] = @ProjectID">
        <DeleteParameters>
            <asp:Parameter Name="ProjectID" Type="Int16" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="ProjectName" Type="String" />
            <asp:Parameter Name="CurrentStatus" Type="Boolean" />
            <asp:Parameter Name="ProjectID" Type="Int16" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="ProjectID" Type="Int16" />
            <asp:Parameter Name="ProjectName" Type="String" />
            <asp:Parameter Name="CurrentStatus" Type="Boolean" />
        </InsertParameters>
    </asp:SqlDataSource>


</asp:Content>

