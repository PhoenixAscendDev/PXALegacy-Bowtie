using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

using JB2.Bowtie.Web.Models;


namespace JB2.Bowtie.Web.Controllers
{

    public class RequireRequestValueAttribute : ActionMethodSelectorAttribute
    {
        public RequireRequestValueAttribute(string valueName)
        {
            ValueName = valueName;
        }
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return (controllerContext.HttpContext.Request[ValueName] != null);
        }
        public string ValueName { get; private set; }
    }

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

        public ActionResult Objects()
        {
            var service = this.GraphService;

            var list = service.RetrieveObjects();

            var model = new List<GraphObjectViewModel>(list.Count());

            foreach (var d in list)
            {
                model.Add(AutoMapper.Mapper.Map<GraphObject, GraphObjectViewModel>(d));
            }

            return View(model);
        }

        // GET: Graph/Details/5
        public ActionResult Details(string id)
        {
            return View();
        }

        #region Creates
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateProperty()
        {
            var model = new GraphPropertyViewModel();
            return View(model);
        }


        [HttpPost]
        public ActionResult CreateProperty(GraphPropertyViewModel m)
        {
            Enum.GraphPropertyType  ptype = Enum.GraphPropertyType.Text;
            System.Enum.TryParse<Enum.GraphPropertyType>(m.GraphPropertyType, out ptype);

            var newp = GraphProperty.NewProperty(m.Name, ptype, m.ApplicationID, m.isMultiValued);

            var service = this.GraphService;

            service.SaveProperty(newp);

            return RedirectToAction("Properties");
        }

        [HttpPost]
        public ActionResult CreateObject(GraphObjectViewModel m)
        {
            Enum.GraphDeterminer determiner = Enum.GraphDeterminer.A;

            System.Enum.TryParse<Enum.GraphDeterminer>(m.Determiner, out determiner);
            var newo = GraphObject.NewObject(m.Name, m.ApplicationID,determiner,m.Plural);

            newo.ParentID = m.ParentID;
            newo.Singular = m.Singular;

            //set the Properties
            var newProps = convertToGraphProperties(m.PostedPropertyIDs);
            foreach(var p in newProps)
            {
                newo.AddProperty(p);
            }

            var service = this.GraphService;

            service.SaveObject(newo);

            return RedirectToAction("Objects");
        }

        public ActionResult CreateObject()
        {
            var model = new GraphObjectViewModel();
            return View(model);
        }

        #endregion Creates

        #region Edits
        public ActionResult Edit(string id)
        {
            var service = this.GraphService;

            var element = service.RetrieveByID(id);
            ViewBag.ApplicationSelect = this.applicationSelectList();

            switch (element.ElementType)
            {
                case Enum.GraphElementType.Property:
                    return EditProperty((GraphProperty)element);
                case Enum.GraphElementType.Object:
                    return EditObject((GraphObject)element);
            }
            return View();
        }

        public ActionResult EditProperty(GraphProperty p)
        {
            var model = AutoMapper.Mapper.Map<GraphProperty, GraphPropertyViewModel>(p);

            
            return View("EditProperty", model);
        }

        [HttpPost]
        public ActionResult EditProperty(GraphPropertyViewModel mp)
        {
            var service = this.GraphService;

            var element = service.RetrievePropertyByID(mp.ID);

            element.ApplicationID = mp.ApplicationID;
            Enum.GraphPropertyType gpt = element.GraphPropertyType;
            System.Enum.TryParse<Enum.GraphPropertyType>(mp.GraphPropertyType, out gpt);
            element.GraphPropertyType = gpt;
            element.Name = mp.Name;
            element.ParentID = mp.ParentID;

            service.SaveProperty(element);

            return RedirectToAction("Properties");
        }


        public ActionResult EditObject(GraphObject o)
        {
            var model = AutoMapper.Mapper.Map<GraphObject, GraphObjectViewModel>(o);

            return View("EditObject", model);
        }

        [HttpPost]
        public ActionResult EditObject(GraphObjectViewModel mo)
        {
            var service = this.GraphService;

            var element = service.RetrieveObjectByID(mo.ID);

            Enum.GraphDeterminer mg = element.Determiner;
            System.Enum.TryParse<Enum.GraphDeterminer>(mo.Determiner, out mg);

            element.ApplicationID = mo.ApplicationID;
            element.Determiner = mg;
            element.Name = mo.Name;
            element.ParentID = mo.ParentID;
            element.Plural = mo.Plural;
            element.Singular = mo.Singular;

            var currentProps = element.GetProperties();
            foreach(var p in currentProps)
            {
                element.RemoveProperty(p);
            }
            var newProps = mo.PostedPropertyIDs;
            foreach (var i in newProps)
            {
                try
                {
                    var p = service.RetrievePropertyByID(i);

                    if (p != null)
                        element.AddProperty(p);
                    else
                        throw new Exception("Unable to retrieve graph property from Service [property ID: " + i);
                }
                catch(Exception ex)
                {
                    ex.BowtieLog();
                }        
            }

            service.SaveObject(element);

            return RedirectToAction("Objects");
        }

        #endregion Edits



        #region Deletes
        public ActionResult Delete(string id)
        {

            var service = this.GraphService;

            var element = service.RetrieveByID(id);

            service.Remove(element);

            switch(element.ElementType)
            {
                case Enum.GraphElementType.Property:
                    return RedirectToAction("Properties");
                case Enum.GraphElementType.Object:
                    return RedirectToAction("Objects");
            }

            
            return View();
        }

        #endregion Deltes


        #region Helpers

        protected IEnumerable<GraphProperty> convertToGraphProperties(IEnumerable<SelectListItem> items)
        {
            List<GraphProperty> list = new List<GraphProperty>(items.Count());
            foreach(var i in items)
            {
                if (i.Selected)
                {
                    try
                    {
                        var service = this.GraphService;
                        var p = service.RetrievePropertyByID(i.Value);
                        list.Add(p);
                    }
                    catch (Exception ex)
                    {
                        ex.BowtieLog();
                    }
                }
            }

            return list;
        }


        protected IEnumerable<GraphProperty> convertToGraphProperties(IEnumerable<string> ids)
        {
            List<GraphProperty> list = new List<GraphProperty>(ids.Count());
            foreach (var i in ids)
            {
                try
                {
                    var service = this.GraphService;
                    var p = service.RetrievePropertyByID(i);
                    list.Add(p);
                }
                catch (Exception ex)
                {
                    ex.BowtieLog();
                }      
            }

            return list;
        }

        #endregion Helpers

    }
}
