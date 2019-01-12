using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data;
using System.Data.Entity;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using PTS_App_Code_VB_Project;
using System.Web.Http;
using System.Globalization;
using System.Threading;

namespace host_NET45_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DevExtremeBundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void Application_Error(object sender, EventArgs e)
        {

            //MyCommonTasks Task = new MyCommonTasks();

            // Get the error details 
            HttpException lastErrorWrapper = Server.GetLastError() as HttpException;

            Exception lastError = lastErrorWrapper;
            if (lastErrorWrapper.InnerException != null)
            {
                lastError = lastErrorWrapper.InnerException;
            }

            if (lastErrorWrapper != null && lastErrorWrapper.GetHttpCode() == 404)
            {
                // You've handled the error, so clear it. Leaving the server in an error state can cause unintended side effects as the server continues its attempts to handle the error.
                Server.ClearError();

                // Possible that a partially rendered page has already been written to response buffer before encountering error, so clear it.
                Response.Clear();

                //404 errors
                string _url = HttpContext.Current.Request.RawUrl.ToString();

                _url = _url.Replace("/webforms/webforms/", "/webforms/");

                // aspx var, webforms yok
                if (_url.IndexOf(".aspx") != -1 && _url.IndexOf("/webforms/") == -1)
                {
                    //Task.SendEmailToAdmin(lastErrorWrapper.GetHttpCode().ToString(), "~/webforms" + _url);
                    Response.Redirect("~/webforms" + _url);
                }
                else
                {
                    //Task.SendEmailToAdmin(lastErrorWrapper.GetHttpCode().ToString(), "~" + _url);
                    Response.Redirect("~" + _url);
                }

            }

        }

        protected string UserEmail(string UserName)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MERCURYConnectionString"].ConnectionString);
            con.Open();
            string sqlstring = " SELECT     RTRIM(dbo.aspnet_Membership.Email) AS Email " + " FROM         dbo.aspnet_Users INNER JOIN " + " dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId AND  " + "                       dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId " + " WHERE     (dbo.aspnet_Users.UserName = @UserName) ";

            SqlCommand cmd = new SqlCommand(sqlstring, con);
            cmd.CommandType = System.Data.CommandType.Text;

            //syntax for parameter adding
            SqlParameter UserParm = cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 256);
            UserParm.Value = User.Identity.Name;
            SqlDataReader dr = cmd.ExecuteReader();
            string _return = "";
            while (dr.Read())
            {
                _return = dr[0].ToString();
            }
            return _return;
            con.Close();
            dr.Close();
        }

    }
}
