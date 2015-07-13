using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bowtie.WebMain.Controllers
{
    public class HomeController : JB2.Bowtie.Web.Controllers.BaseController
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {

            int ApplicationCount = this.ApplicationService.Retrieve().Count;
            ViewBag.Message = "Number of Applications: " + ApplicationCount.ToString();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}