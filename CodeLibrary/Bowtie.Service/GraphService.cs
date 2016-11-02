using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class GraphService
    {
        #region Fields
        protected IGraphRepository  _repo;
        protected IUnitOfWork _uofw;
        #endregion Fields

        #region Constructors

        public GraphService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }
        public GraphService(IUnitOfWork uofw)
        {
            _uofw = uofw;
            _repo = uofw.GraphRepository;
        }

        public GraphService(IGraphRepository repo)
        {
            _repo = repo;
            _uofw = JB2.Settings.Bowtie.UnitOfWork;
        }

        #endregion Constructors

        public IEnumerable<GraphStory> RetreiveStoriesByApplication(Application app)
        {
            var elements = _repo.GetGraphElementsByApplication(app.ID);

            return filterByType<GraphStory>(elements, Enum.GraphElementType.Story);          
        }

        #region All

        public IEnumerable<IGraphElement> Retrieve()
        {
            var elements = _repo.GetAll();

            return elements;

        }

        public IGraphElement RetrieveByID(string id)
        {
            var element = _repo.GetGraphElement(id);

            return element;
        }

        public JB2.Common.ServiceResult Remove(IGraphElement element)
        {
            return _repo.DeleteGraphElement(element);
        }

        #endregion All

        #region Graph Properties

        public GraphProperty RetrievePropertyByID(string id)
        {
            var element = _repo.GetGraphElement(id);

            return (GraphProperty)element;
        }

        public IEnumerable<GraphProperty> RetreiveProperties()
        {
            var elements = _repo.GetGraphElementsByType(Enum.GraphElementType.Property);

            return filterByType<GraphProperty>(elements, Enum.GraphElementType.Property);
        }

        public IEnumerable<GraphProperty> RetreivePropertiesByApplication(Application app)
        {
            var elements = _repo.GetGraphElementsByApplication(app.ID);
            return filterByType<GraphProperty>(elements, Enum.GraphElementType.Property);
        }

        public void SaveProperty(GraphProperty prop)
        {
            _repo.InsertGraphElement(prop);
        }
        #endregion Graph Properties

        #region Graph Objects

        public IEnumerable<GraphObject> RetrieveObjects()
        {
            var elements = _repo.GetGraphElementsByType(Enum.GraphElementType.Object);
            return filterByType<GraphObject>(elements, Enum.GraphElementType.Object);
        }

        public GraphObject RetrieveObjectByID(string id)
        {
            return (GraphObject)_repo.GetGraphElement(id);
        }

        public GraphObject RetrieveObjectByName(string name)
        {
            return (GraphObject)_repo.GetGraphElementByName(name);
        }

        public IEnumerable<GraphObject> RetrieveObjectsByApplication(Application app)
        {
            var elements = _repo.GetGraphElementsByApplication(app.ID);
            return filterByType<GraphObject>(elements, Enum.GraphElementType.Object);
        }

        public void SaveObject(GraphObject obj)
        {
            _repo.InsertGraphElement(obj);
        }


        #endregion Graph Objects

        #region Graph Action

        public GraphAction RetrieveActionByID(string id)
        {
            var element = _repo.GetGraphElement(id);

            return (GraphAction)element;
        }
        public IEnumerable<GraphAction> RetrieveActions()
        {
            var elements = _repo.GetGraphElementsByType(Enum.GraphElementType.Action);

            return filterByType<GraphAction>(elements, Enum.GraphElementType.Action);
        }

        public IEnumerable<GraphAction> RetrieveActionsByApplication(Application app)
        {
            var elements = _repo.GetGraphElementsByApplication(app.ID);
            return filterByType<GraphAction>(elements, Enum.GraphElementType.Action);
        }

        public void SaveAction(GraphAction act)
        {
            
            _repo.InsertGraphElement(act);
        }


        #endregion Graph Action


        #region Graph Story
        public GraphStory RetrieveStoryByID(string id)
        {
            var element = _repo.GetGraphElement(id);

            return (GraphStory)element;
        }

        public IEnumerable<GraphStory> RetrieveStories()
        {
            var elements = _repo.GetGraphElementsByType(Enum.GraphElementType.Story);

            return filterByType<GraphStory>(elements, Enum.GraphElementType.Story);
        }

        public IEnumerable<GraphStory> RetrieveStoriesByApplicationID(string id)
        {
            var elements = _repo.GetGraphElementsByApplication(id);
            return filterByType<GraphStory>(elements, Enum.GraphElementType.Story);
        }

        public void SaveStory(GraphStory story)
        {
            _repo.InsertGraphElement(story);
        }


        #endregion Graph Story


        public IEnumerable<JB2.Common.IDNamePair> RetrievePropertyTypes()
        {
            return _repo.GetDataTypes();
        }
        public JB2.Common.IDNamePair RetrievePropertyType(string key)
        {
            JB2.Common.IDNamePair propType = _repo.GeDataTypeByName(key).ID == String.Empty ? _repo.GetDataTypeByID(key) : _repo.GeDataTypeByName(key);

            return propType;

        }

        #region internal helpers
        protected IEnumerable<T> filterByType<T>(IEnumerable<IGraphElement> list,Enum.GraphElementType type)
            where T : IGraphElement
        {
            List<T> result = new List<T>(list.Count());

            foreach (IGraphElement e in list)
            {
                if (e.ElementType == type)
                    result.Add((T)e);
            }

            return result;
        }

        #endregion internal helpers

    }
}
