using System.Web.Mvc;

namespace HXWebApp.Areas.HRS
{
    public class HRSAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HRS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HRS_default",
                "HRS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}