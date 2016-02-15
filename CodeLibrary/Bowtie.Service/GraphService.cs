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


        #region Graph Properties
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
