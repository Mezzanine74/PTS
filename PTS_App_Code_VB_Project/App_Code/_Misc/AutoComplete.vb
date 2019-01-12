Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.Collections
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class AutoComplete
    Inherits System.Web.Services.WebService


    <WebMethod(), System.Web.Script.Services.ScriptMethod()> _
    Public Function haydi(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim da As New SqlDataAdapter("SELECT TOP 15 SupplierID + N' ' + SupplierName AS Idname FROM   Table6_Supplier WHERE (SupplierID <> N'0000000000') AND (SupplierID <> N'1111111111') AND (Client = 0) AND (SupplierID + N' ' + SupplierName) like @prefixText", ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = "%" + prefixText + "%"
        Dim dt As New DataTable
        da.Fill(dt)
        Dim items As New List(Of String)

        For i = 0 To dt.Rows.Count - 1
            items.Add(UCase(dt.Rows(i).Item("IDname")))
        Next

        Return items.ToArray
    End Function

    <WebMethod(), System.Web.Script.Services.ScriptMethod()> _
    Public Function SupplierIDEdit(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim da As New SqlDataAdapter("SELECT TOP 15 SupplierID + N' ' + SupplierName AS Idname FROM   Table6_Supplier WHERE (SupplierID <> N'1111111111') AND (SupplierID + N' ' + SupplierName) like @prefixText", ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = "%" + prefixText + "%"
        Dim dt As New DataTable
        da.Fill(dt)
        Dim items As New List(Of String)

        For i = 0 To dt.Rows.Count - 1
            items.Add(UCase(dt.Rows(i).Item("IDname")))
        Next

        Return items.ToArray
    End Function

    <WebMethod(), System.Web.Script.Services.ScriptMethod()> _
    Public Function SupplierEdit(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim da As New SqlDataAdapter("SELECT TOP 15 SupplierID + N' ' + RTRIM(SupplierName) AS Idname FROM   Table6_Supplier WHERE (SupplierID <> N'1111111111') AND (SupplierID + N' ' + RTRIM(SupplierName)) like @prefixText ", ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = "%" + prefixText + "%"
        Dim dt As New DataTable
        da.Fill(dt)
        Dim items As New List(Of String)

        For i = 0 To dt.Rows.Count - 1
            items.Add(UCase(dt.Rows(i).Item("IDname")))
        Next

        Return items.ToArray
    End Function

    <WebMethod(), System.Web.Script.Services.ScriptMethod()> _
    Public Function CostCodeEdit(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim da As New SqlDataAdapter("SELECT  TOP 10 CostCode, RTRIM(CAST(CostCode AS varchar(4)) + '  ' + CodeDescription) AS CostCode_Description FROM  dbo.Table7_CostCode WHERE RTRIM(CAST(CostCode AS varchar(4)) + '  ' + CodeDescription) like @prefixText", ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%"
        Dim dt As New DataTable
        da.Fill(dt)
        Dim items As New List(Of String)

        For i = 0 To dt.Rows.Count - 1
            items.Add(UCase(dt.Rows(i).Item("CostCode_Description")))
        Next

        Return items.ToArray
    End Function

    <WebMethod(), System.Web.Script.Services.ScriptMethod()> _
    Public Function SearchSupplier(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim da As New SqlDataAdapter("SELECT TOP 10 RTRIM([SupplierName]) AS SupplierName FROM [Table_ContractNotes] WHERE [SupplierName] like N'%' + @prefixText+ '%'", ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText
        Dim dt As New DataTable
        da.Fill(dt)
        Dim items As New List(Of String)

        For i = 0 To dt.Rows.Count - 1
            items.Add(UCase(dt.Rows(i).Item("SupplierName")))
        Next

        Return items.ToArray
    End Function

    <WebMethod(), System.Web.Script.Services.ScriptMethod()> _
    Public Function SearchContractNo(ByVal prefixText As String, ByVal contextKey As String) As String()
        Dim da As New SqlDataAdapter( _
                                    " select TOP 200  RIGHT('.....' + ISNULL(RTRIM(RTRIM(Convert(NVarChar(10),ContractID))),N''), 5)  + N'| ' +  Left(ISNULL(RTRIM(ContractNo),N'') + '....................', 20) " + _
                                    " 		+ N' | ' + " + _
                                    " 	   Left(ISNULL(RTRIM(ProjectName),N'') + '..............................', 30) " + _
                                    " 		+ N' | ' + " + _
                                    " 	   Left(ISNULL(RTRIM(SupplierName),N'') + '..............................', 30) " + _
                                    "  " + _
                                    " 	   AS ContractNo from Table_Contracts  " + _
                                    "  " + _
                                    " inner join Table1_Project on Table1_Project.ProjectID = Table_Contracts.ProjectID " + _
                                    " inner join Table6_Supplier on Table6_Supplier.SupplierID = Table_Contracts.SupplierID " + _
                                    " inner join dbo.Table_Prj_User_Junction ON dbo.Table_Prj_User_Junction.ProjectID = Table_Contracts.ProjectID " + _
                                    " inner join dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId " + _
                                    " where ContractNo like @prefixText AND UserName = @UserName ",
                                     ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = "%" + prefixText + "%"
        da.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar, 256).Value = contextKey.Trim
        Dim dt As New DataTable
        da.Fill(dt)
        Dim items As New List(Of String)

        For i = 0 To dt.Rows.Count - 1
            items.Add(dt.Rows(i).Item("ContractNo"))
        Next

        Return items.ToArray
    End Function

    <WebMethod(), System.Web.Script.Services.ScriptMethod()> _
    Public Function SearchSupplierOnMonitoring(ByVal prefixText As String, ByVal contextKey As String) As String()
        Dim da As New SqlDataAdapter(" exec SP_SelectSupplierOnMonitoringAutoExtender @ProjectID, @prefix ", ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
        da.SelectCommand.Parameters.Add("@prefix", SqlDbType.VarChar, 50).Value = prefixText

        da.SelectCommand.Parameters.Add("@ProjectID", SqlDbType.SmallInt).Value = contextKey.Trim

        Dim dt As New DataTable
        da.Fill(dt)
        Dim items As New List(Of String)

        For i = 0 To dt.Rows.Count - 1
            items.Add(dt.Rows(i).Item("SupplierName"))
        Next

        Return items.ToArray
    End Function


End Class
