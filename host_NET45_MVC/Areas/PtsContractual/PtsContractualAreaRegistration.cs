using System.Web.Mvc;

namespace host_NET45_MVC.Areas.PtsContractual
{
    public class PtsContractualAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PtsContractual";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PtsContractual_default",
                "PtsContractual/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}