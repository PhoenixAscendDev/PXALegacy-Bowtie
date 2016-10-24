using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Bowtie.WebMain
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<JB2.Bowtie.IApplication, JB2.Bowtie.Web.Models.ApplicationViewModel>());

            JB2.Bowtie.Manager.Initialize("BT-1F4ACB33EAE78E37", JB2.Configuration.GetAppSetting("JB2:bowtie-apisecret"));
        }
    }
}
