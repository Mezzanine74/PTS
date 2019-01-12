using System.Web.Mvc;

namespace host_NET45_MVC.Areas.PtsPO
{
    public class PtsPOAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PtsPO";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PtsPO_default",
                "PtsPO/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}