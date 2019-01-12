using PTS_App_Code_VB_Project;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace host_NET45_MVC.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!Request.IsAuthenticated & Request.RawUrl.Trim().IndexOf("login") == -1)
            {
                TakeKeysToSession(filterContext);

                string AllKeys = "";

                foreach (string key in Request.QueryString.AllKeys)
                {
                    if (key != "ReturnUrl")
                    {
                        AllKeys = AllKeys + "&" + key + "=" + filterContext.HttpContext.Request[key];
                    }
                }

                if (!Response.IsRequestBeingRedirected)
                {
                    Response.Redirect("~/login.aspx" + "?ReturnUrl=" + Request.RawUrl.Trim().Replace("~/", string.Empty) + AllKeys);
                }

            }
            else if (!Request.IsAuthenticated & Request.RawUrl.Trim().IndexOf("login") != -1)
            {
                TakeKeysToSession(filterContext);

                if (filterContext.HttpContext.Request["username"] != null && filterContext.HttpContext.Request["password"] != null)
                {
                    if (System.Web.Security.Membership.ValidateUser(filterContext.HttpContext.Request["username"], filterContext.HttpContext.Request["password"]))
                    {
                        System.Web.Security.FormsAuthentication.RedirectFromLoginPage(filterContext.HttpContext.Request["username"], false);
                    }
                }
            }

            ViewBag.PageTitle = Request.RawUrl.Trim();

            if (Request.IsAuthenticated & Session["_preserve_allkeys_MVC"] != null)
            {
                string url = Request.Url.AbsolutePath;
                string updatedQueryString = "?" + Session["_preserve_allkeys_MVC"];

                Session.Remove("_preserve_allkeys_MVC");

                if (!Response.IsRequestBeingRedirected)
                {
                    Response.Redirect(url + updatedQueryString);
                }

            }


            // Update Follow User

            // ........ REFRESH LASTACTIVITYDATE ..........
            {
                    // To refresh LastActivityDate column correctly
                    string user = User.Identity.Name.ToLower();

                    if (user != null || user.Trim().Length != 0)
                    {
                        using (SqlConnection cn = ConnectionStringsPTS.GetConnectionStringMain())
                        {
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                            cmd.Connection = cn;
                            cmd.CommandText = "UPDATE aspnet_Users SET LastActivityDate = @LastActivityDate WHERE UserName = @UserName";
                            cmd.Parameters.AddWithValue("@UserName", user);
                            cmd.Parameters.AddWithValue("@LastActivityDate", LocalTime.GetTime());
                            cmd.CommandType = System.Data.CommandType.Text;
                            cn.Open();
                            cmd.ExecuteNonQuery();
                            cn.Close();
                            cn.Dispose();
                        }
                    }
            }

            PTS_App_Code_VB_Project.VisitorsLog.helper.Table_VisitorLogs.Insert(
                User.Identity.Name.ToString(),
                Request.RawUrl.Trim().Replace("~/", string.Empty),
                PTS_App_Code_VB_Project.LocalTime.GetTime(),
                Request.ServerVariables["remote_addr"],
                Request.Browser.Browser + " " + Request.Browser.Version,
                Request.UserAgent.Contains("Windows NT 6.0") == true ? "Vista" : (Request.UserAgent.Contains("Windows NT 6.1") == true ? "Windows 7" : Request.Browser.Platform),
                "-");

            //////////// Translation kontrol kismi

            // bunu buradan sonra kaldir, hata verdigi icin burada
            Session["Translation"] = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.GetTranslation();


            if (Session["ddl_lang"] == null)
            {
                // DEFAULT language ENG
                Session["ddl_lang"] = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.Lang.rus;
            }

            // this should be placed here later. DDL should switch
            //If Not IsPostBack Then
            //    If Session("ddl_lang") IsNot Nothing Then
            //        ddl_lang.SelectedValue = Session("ddl_lang").ToString()
            //    End If
            //End If


            if (Session["CountBodyText"] == null)
            {
                Session["CountBodyText"] = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.CountText();
                IEnumerable<PTS_App_Code_VB_Project.PTS_MERCURY.db.BodyText> Translation = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.GetTranslation();
                Session["Translation"] = Translation;
            }

            if (Session["CountBodyText"] != null)
            {
                try
                {
                    if (PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.CountText() > Convert.ToInt32(Session["CountBodyText"]))
                    {
                        IEnumerable<PTS_App_Code_VB_Project.PTS_MERCURY.db.BodyText> Translation = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.GetTranslation();
                        Session["Translation"] = Translation;
                    }

                }
                catch (Exception ex)
                {

                }
            }
            ///////////////// _________________Translation kontrol kismi
        }

        protected void TakeKeysToSession(ActionExecutingContext filterContext)
        {
            // I am not sure about session name. I have added MVC not to conflict with WEBFORM session.
            Session.Remove("_preserve_allkeys_MVC");

            string AllKeys = "";

            foreach (string key in Request.QueryString.AllKeys)
            {
                if (key != "ReturnUrl")
                {
                    if (key.ToLower() != "username")
                    {
                        if (key.ToLower() != "password")
                        {
                            AllKeys = AllKeys + "&" + key + "=" + filterContext.HttpContext.Request[key];
                        }
                    }
                }
            }

            Session["_preserve_allkeys_MVC"] = AllKeys;

        }

    }
}