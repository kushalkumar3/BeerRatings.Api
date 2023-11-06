using BeerRatings.Api.App_Start;
using Serilog;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;

namespace BeerRatings.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();
        }
        protected void Application_End()
        {

        }
    }
}
