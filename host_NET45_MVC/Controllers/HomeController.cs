using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace host_NET45_MVC.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (!Response.IsRequestBeingRedirected)
            {
                Response.Redirect("~/default.aspx");
            }

            return View();
        }

        public void Logout(string ReturnUrl = "")
        {
            FormsAuthentication.SignOut();

            string AllKeys = "";

            foreach (string key in Request.QueryString.AllKeys)
            {
                if (key != "ReturnUrl")
                {
                    AllKeys = AllKeys + "&" + key + "=" + Request.QueryString[key];
                }
            }

            Response.Redirect("~/login.aspx" + "?ReturnUrl=" + ReturnUrl + AllKeys);
        }

    }
}