using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JB2.Economy.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var jBeanRepo = new JB2.Economy.Data.jBeanRespostory(JB2.Infrastructure.Storage.BowtieAccount);
            JB2.Common.BaseSetting s = new JB2.Common.BaseSetting()
            {
                ID = Economy.JbeanSettingName.CurrencyID,
                Name = "CurrencyID",
                Value = JB2.Configuration.GetjBeanCurrencyID()
            };

            JB2.Settings.Jbean.Configure(new JB2.Common.BaseSetting[1] { s }, jBeanRepo);
        }
    }
}
