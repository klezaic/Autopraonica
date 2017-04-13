using System.Web.Mvc;

namespace Autopraonica.Web.Areas.Informacije
{
    public class InformacijeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Informacije";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Info",
                "Informacije/{controller}/{action}/{id}",
                new { controller = "AreaHome", action = "Index", AreaName = "Informacije", id = UrlParameter.Optional },
                new[] { "Autopraonica.Web.Areas.Informacije.Controllers" }
            );
        }
    }
}