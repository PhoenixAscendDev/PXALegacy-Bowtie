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

        public ActionResult Actions()
        {
            var service = this.GraphService;

            var list = service.RetrieveActions();

            var model = new List<GraphActionViewModel>(list.Count());

            foreach (var d in list)
            {
                model.Add(AutoMapper.Mapper.Map<GraphAction, GraphActionViewModel>(d));
            }

            return View(model);
        }

        public ActionResult Stories()
        {
            var service = this.GraphService;

            var list = service.RetrieveStories();

            var model = new List<GraphStoryViewModel>(list.Count());

            foreach (var d in list)
            {
                model.Add(AutoMapper.Mapper.Map<GraphStory, GraphStoryViewModel>(d));
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

        public ActionResult CreateObject()
        {
            var model = new GraphObjectViewModel();
            return View(model);
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

        public ActionResult CreateAction()
        {
            var model = new GraphActionViewModel();
            return View(model);
        }





        [HttpPost]
        public ActionResult CreateAction(GraphActionViewModel m)
        {
            var a = GraphAction.NewAction(m.Name, m.ApplicationID);

            //set the Properties
            var newProps = convertToGraphProperties(m.PostedPropertyIDs);
            foreach (var p in newProps)
            {
                a.AddProperty(p);
            }
            
            //set the Objects
            var newObjects = convertToGraphObjects(m.PostedObjectIDs);
            foreach( var o in newObjects)
            {
                a.AddObject(o);
            }
            a.ParentID = m.ParentID;

            var service = this.GraphService;

            service.SaveAction(a);

            return RedirectToAction("Actions");

        }


        public ActionResult CreateStory()
        {
            var model = new GraphStoryViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateStory(GraphStoryViewModel m)
        {
            var s = GraphStory.NewStory(m.Name, m.ApplicationID);
            s.ParentID = m.ParentID;
            var service = this.GraphService;


            //set the AssociatedAction
            try
            {
                if (!string.IsNullOrEmpty(m.ActionID))
                {
                    var action = service.RetrieveActionByID(m.ActionID);
                    s.AssociatedAction = action;
                }
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
            }

            //set the AssciateObject
            try
            {
                if(!string.IsNullOrEmpty(m.ObjectID))
                {
                    var obj = service.RetrieveObjectByID(m.ObjectID);
                    s.AssociatedObject = obj;
                }
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
            }

            JB2.Common.WordTense tense = new Common.WordTense();
            tense.ImperativeTense = m.ImperativeTense;
            tense.Past = m.Past;
            tense.PluralPast = m.PluralPast;
            tense.PluralPresent = m.PluralPresent;
            tense.Present = m.Present;

            service.SaveStory(s);

            return RedirectToAction("Stories");
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
                case Enum.GraphElementType.Action:
                    return EditAction((GraphAction)element);
                case Enum.GraphElementType.Story:
                    return EditStory((GraphStory)element);
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

        public ActionResult EditAction(GraphAction a)
        {
            var model = AutoMapper.Mapper.Map<GraphAction, GraphActionViewModel>(a);

            return View("EditAction", model);
        }

        [HttpPost]
        public ActionResult EditAction(GraphActionViewModel ma)
        {
            var service = this.GraphService;

            var element = service.RetrieveActionByID(ma.ID);

            element.ApplicationID = ma.ApplicationID;
            element.Name = ma.Name;
            element.ParentID = ma.ParentID;


            //set Propropties
            var currentProps = element.GetProperties();
            foreach (var p in currentProps)
            {
                element.RemoveProperty(p);
            }
            var newProps = convertToGraphProperties(ma.PostedPropertyIDs);
            foreach( var p in newProps)
            {
                element.AddProperty(p);
            }

            //set Objects
            var currentObjects = element.GetAssociatedObjects();
            foreach (var p in currentObjects)
            {
                element.RemoveObject(p);
            }
            var newObjects = convertToGraphObjects(ma.PostedObjectIDs);
            foreach (var o in newObjects)
            {
                element.AddObject(o);
            }

            service.SaveAction(element);

            return RedirectToAction("Actions");



        }

        public ActionResult EditStory(GraphStory s)
        {
            var model = AutoMapper.Mapper.Map<GraphStory, GraphStoryViewModel>(s);

            return View("EditStory", model);
        }

        [HttpPost]
        public ActionResult EditStory(GraphStoryViewModel ms)
        {
            var service = this.GraphService;

            var element = service.RetrieveStoryByID(ms.ID);

            element.ApplicationID = ms.ApplicationID;
            element.Name = ms.Name;
            element.ParentID = ms.ParentID;

            try
            {
                if (!string.IsNullOrEmpty(ms.ActionID))
                {
                    var action = service.RetrieveActionByID(ms.ActionID);
                    element.AssociatedAction = action;
                }
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
            }

            //set the AssciateObject
            try
            {
                if (!string.IsNullOrEmpty(ms.ObjectID))
                {
                    var obj = service.RetrieveObjectByID(ms.ObjectID);
                    element.AssociatedObject = obj;
                }
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
            }

            JB2.Common.WordTense tense = new Common.WordTense();
            tense.ImperativeTense = ms.ImperativeTense;
            tense.Past = ms.Past;
            tense.PluralPast = ms.PluralPast;
            tense.PluralPresent = ms.PluralPresent;
            tense.Present = ms.Present;

            element.ActionTense = tense;

            
            service.SaveStory(element);

            return RedirectToAction("Stories");
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
                case Enum.GraphElementType.Action:
                    return RedirectToAction("Actions");
                case Enum.GraphElementType.Story:
                    return RedirectToAction("Stories");
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

        protected IEnumerable<GraphObject> convertToGraphObjects(IEnumerable<string> ids)
        {
            if (ids != null)
            {
                List<GraphObject> list = new List<GraphObject>(ids.Count());
                foreach (var i in ids)
                {
                    try
                    {
                        var service = this.GraphService;
                        var p = service.RetrieveObjectByID(i);
                        list.Add(p);
                    }
                    catch (Exception ex)
                    {
                        ex.BowtieLog();
                    }
                }
                return list;
            }
            else
            {
                return new GraphObject[0];
            }
        }

        #endregion Helpers

    }
}
