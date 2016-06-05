using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JB2.Bowtie.Web.Models;

namespace JB2.Bowtie.Web.Controllers
{
    public class AchievementController : BaseController
    {
        // GET: Achievement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            ViewBag.Applications = this.applicationSelectList();
            ViewBag.Achievements = AchievementService.Retrieve();
            AchievementViewModel model = new AchievementViewModel();
            return View(model);
        }
    }
}