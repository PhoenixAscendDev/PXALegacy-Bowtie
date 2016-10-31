using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JB2.Bowtie.Web.Models;

namespace JB2.Bowtie.Web.Controllers
{
    public class GraphController : BaseController
    {
        // GET: Graph
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult All()
        {
            var service = this.GraphService;

            //var list = service.RetreiveProperties();

            var list = service.RetrieveObjects();

            var model = new List<GraphElementViewModel>(list.Count());

            foreach (var d in list)
            {
                model.Add(AutoMapper.Mapper.Map<IGraphElement, GraphElementViewModel>(d));
            }

            ViewBag.ApplicationSelect = this.applicationSelectList();

            return View(model);

        }

        public ActionResult Properties()
        {
            var service = this.GraphService;

            var list = service.RetreiveProperties();

            var model = new List<GraphPropertyViewModel>(list.Count());

            foreach (var d in list)
            {
                model.Add(AutoMapper.Mapper.Map<GraphProperty, GraphPropertyViewModel>(d));
            }

            ViewBag.ApplicationSelect = this.applicationSelectList();

            return View(model);
        }

        // GET: Graph/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Graph/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Graph/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Graph/Edit/5
        public ActionResult Edit(string id)
        {
            var service = this.GraphService;


            return View();
        }



        // POST: Graph/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Graph/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Graph/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
