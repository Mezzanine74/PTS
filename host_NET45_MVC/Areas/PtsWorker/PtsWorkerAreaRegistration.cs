using System.Web.Mvc;

namespace host_NET45_MVC.Areas.PtsWorker
{
    public class PtsWorkerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PtsWorker";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PtsWorker_default",
                "PtsWorker/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}