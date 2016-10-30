using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JB2.Bowtie.Web.Models;

namespace JB2.Bowtie.Web.Controllers
{
    public class ApplicationController : BaseController
    {
        // GET: Application
        public ActionResult Index()
        {
            return RedirectToAction("All");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ApplicationViewModel m)
        {

            try
            {
                var appService = this.ApplicationService;

                var newApp = appService.GenerateNewApplication();

                if (m.CompanyID == JB2.Info.HQ.ID)
                    newApp.Company = JB2.Info.HQ;

                newApp.Name = m.Name;
                newApp.Website = m.Website;

                appService.Save(newApp);

                return View("All");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        public ActionResult Edit(string id)
        {
            return View(GetByID(id));
        }

        
        [HttpPost]
        public ActionResult Edit(ApplicationViewModel m)
        {
            var appService = this.ApplicationService;

            var app = appService.RetrieveById(m.ID);

            app.Name = m.Name;
            app.Website = m.Website;
            app.Secret = m.Secret;
            app.APIkey = m.APIkey;

            var tkey = app.GetTreasuryRequestKey("jBean");
            tkey.Key = m.jBeanKey;

            ((Application)app).TreasuryKeys = new TreasuryRequestKey[1] {  tkey};

            appService.Save(app);

            return View();
        }


        public ActionResult Detail(string id)
        {
            
            return View(GetByID(id));
        }

        public ActionResult All()
        {

            var appService = this.ApplicationService;

            var appslist = appService.Retrieve();

            var model = new List<ApplicationViewModel>(appslist.Count());

            foreach (var a in appslist)
            {
                model.Add(AutoMapper.Mapper.Map<IApplication, ApplicationViewModel>(a));
            }

            ViewBag.ApplicationSelect = this.applicationSelectList();

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            var appservice = this.ApplicationService;

            var app = appservice.RetrieveById(id);

            if (app != null)
                appservice.Remove(app);

            return RedirectToAction("All");
        }


        private ApplicationViewModel GetByID(string id)
        {
            var appService = new JB2.Bowtie.Service.ApplicationService(JB2.Settings.Bowtie.UnitOfWork);

            var app = appService.RetrieveById(id);

            var model = AutoMapper.Mapper.Map<IApplication, ApplicationViewModel>(app);

            return model;
        }


    }
}