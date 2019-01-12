
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
[System.Web.Script.Services.ScriptService()]
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
public class AutoComplete : System.Web.Services.WebService
{


    [WebMethod(), System.Web.Script.Services.ScriptMethod()]
    public string[] haydi(string prefixText, int count)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 15 SupplierID + N' ' + SupplierName AS Idname FROM   Table6_Supplier WHERE (SupplierID <> N'0000000000') AND (SupplierID <> N'1111111111') AND (Client = 0) AND (SupplierID + N' ' + SupplierName) like @prefixText", ConfigurationManager.ConnectionStrings["MERCURYConnectionString"].ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> items = new List<string>();

        int i;
        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            items.Add((dt.Rows[i]["IDname"]).ToString().ToUpper());
        }

        return items.ToArray();
    }

    [WebMethod(), System.Web.Script.Services.ScriptMethod()]
    public string[] SupplierIDEdit(string prefixText, int count)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 15 SupplierID + N' ' + SupplierName AS Idname FROM   Table6_Supplier WHERE (SupplierID <> N'1111111111') AND (SupplierID + N' ' + SupplierName) like @prefixText", ConfigurationManager.ConnectionStrings["MERCURYConnectionString"].ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> items = new List<string>();

        int i;
        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            items.Add((dt.Rows[i]["IDname"]).ToString().ToUpper());
        }

        return items.ToArray();
    }

    [WebMethod(), System.Web.Script.Services.ScriptMethod()]
    public string[] SupplierEdit(string prefixText, int count)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 15 SupplierID + N' ' + RTRIM(SupplierName) AS Idname FROM   Table6_Supplier WHERE (SupplierID <> N'1111111111') AND (SupplierID + N' ' + RTRIM(SupplierName)) like @prefixText ", ConfigurationManager.ConnectionStrings["MERCURYConnectionString"].ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> items = new List<string>();

        int i;
        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            items.Add((dt.Rows[i]["IDname"]).ToString().ToUpper());
        }

        return items.ToArray();
    }

    [WebMethod(), System.Web.Script.Services.ScriptMethod()]
    public string[] CostCodeEdit(string prefixText, int count)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT  TOP 10 CostCode, RTRIM(CAST(CostCode AS varchar(4)) + '  ' + CodeDescription) AS CostCode_Description FROM  dbo.Table7_CostCode WHERE RTRIM(CAST(CostCode AS varchar(4)) + '  ' + CodeDescription) like @prefixText", ConfigurationManager.ConnectionStrings["MERCURYConnectionString"].ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> items = new List<string>();

        int i;
        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            items.Add((dt.Rows[i]["CostCode_Description"]).ToString().ToUpper());
        }

        return items.ToArray();
    }

    [WebMethod(), System.Web.Script.Services.ScriptMethod()]
    public string[] SearchSupplier(string prefixText, int count)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 10 RTRIM([SupplierName]) AS SupplierName FROM [Table_ContractNotes] WHERE [SupplierName] like N'%' + @prefixText+ '%'", ConfigurationManager.ConnectionStrings["MERCURYConnectionString"].ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText;
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> items = new List<string>();

        int i;
        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            items.Add((dt.Rows[i]["SupplierName"]).ToString().ToUpper());
        }

        return items.ToArray();
    }

    [WebMethod(), System.Web.Script.Services.ScriptMethod()]
    public string[] SearchContractNo(string prefixText, string contextKey)
    {
        SqlDataAdapter da = new SqlDataAdapter(" select TOP 200  RIGHT('.....' + ISNULL(RTRIM(RTRIM(Convert(NVarChar(10),ContractID))),N''), 5)  + N'| ' +  Left(ISNULL(RTRIM(ContractNo),N'') + '....................', 20) " + " \t\t+ N' | ' + " + " \t   Left(ISNULL(RTRIM(ProjectName),N'') + '..............................', 30) " + " \t\t+ N' | ' + " + " \t   Left(ISNULL(RTRIM(SupplierName),N'') + '..............................', 30) " + "  " + " \t   AS ContractNo from Table_Contracts  " + "  " + " inner join Table1_Project on Table1_Project.ProjectID = Table_Contracts.ProjectID " + " inner join Table6_Supplier on Table6_Supplier.SupplierID = Table_Contracts.SupplierID " + " inner join dbo.Table_Prj_User_Junction ON dbo.Table_Prj_User_Junction.ProjectID = Table_Contracts.ProjectID " + " inner join dbo.aspnet_Users ON dbo.Table_Prj_User_Junction.UserID = dbo.aspnet_Users.UserId " + " where ContractNo like @prefixText AND UserName = @UserName ", ConfigurationManager.ConnectionStrings["MERCURYConnectionString"].ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";
        da.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar, 256).Value = contextKey.Trim();
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> items = new List<string>();

        int i;
        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            items.Add((dt.Rows[i]["ContractNo"]).ToString().ToUpper());
        }

        return items.ToArray();
    }

    [WebMethod(), System.Web.Script.Services.ScriptMethod()]
    public string[] CostCode(string prefixText, string contextKey)
    {
        SqlDataAdapter da = new SqlDataAdapter(" exec SP_CostCodeSelect @ProjectID, @UserName, @prefix ", ConfigurationManager.ConnectionStrings["MERCURYConnectionString"].ConnectionString);
        //Dim da As New SqlDataAdapter(" select CostCode from Table7_CostCode where CostCode like N'%' + @prefix + N'%' ", ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
        da.SelectCommand.Parameters.Add("@prefix", SqlDbType.VarChar, 50).Value = prefixText;

        da.SelectCommand.Parameters.Add("@ProjectID", SqlDbType.SmallInt).Value = contextKey.Substring( 1, contextKey.Substring(contextKey.IndexOf("|") + 2 - 1, contextKey.Length - 1 - 1).IndexOf("|")-1);

        //da.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar, 256).Value = Strings.Mid(contextKey, Strings.Mid(contextKey, contextKey.IndexOf("|") + 2, contextKey.Length - 1).IndexOf("|") + 1 + 2, contextKey.Length - 2 - (Strings.Mid(contextKey, contextKey.IndexOf("|") + 2, contextKey.Length - 1).IndexOf("|") + 1));
        da.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar, 256).Value = contextKey.Substring(contextKey.Substring( contextKey.IndexOf("|") + 2 - 1, contextKey.Length - 1 - 1).IndexOf("|") + 1 + 2 - 1, contextKey.Length - 2 - (contextKey.Substring( contextKey.IndexOf("|") + 2 - 1, contextKey.Length - 1 - 1).IndexOf("|") + 1)-1);

        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> items = new List<string>();

        int i;
        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            items.Add((dt.Rows[i]["CostCode_Description"]).ToString().ToUpper());
        }

        return items.ToArray();
    }

    [WebMethod(), System.Web.Script.Services.ScriptMethod()]
    public string[] SearchSupplierOnMonitoring(string prefixText, string contextKey)
    {
        //contextKey = "210";

        SqlDataAdapter da = new SqlDataAdapter(" exec SP_SelectSupplierOnMonitoringAutoExtender @ProjectID, @prefix ", ConfigurationManager.ConnectionStrings["MERCURYConnectionString"].ConnectionString);
        da.SelectCommand.Parameters.Add("@prefix", SqlDbType.VarChar, 50).Value = "%" + prefixText +"%" ;

        da.SelectCommand.Parameters.Add("@ProjectID", SqlDbType.SmallInt).Value = contextKey.Trim();

        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> items = new List<string>();

        int i;
        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            items.Add((dt.Rows[i]["SupplierName"]).ToString().ToUpper());
        }

        return items.ToArray();
    }


}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
