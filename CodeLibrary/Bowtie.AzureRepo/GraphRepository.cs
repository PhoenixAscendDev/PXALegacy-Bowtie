using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;
using JB2.Common.Data;

namespace JB2.Bowtie.Data.Azure
{
    public class GraphRepository : IGraphRepository
    {
        private JB2.Common.Data.AzureTableRepository _table;
        private JB2.Common.Data.AzureBlobRepository _blob;

        #region Constructors
        public GraphRepository()
        {
            _table = AzureStorage.GraphTable;
            _blob = AzureStorage.GeneralBlob;
        }

        public GraphRepository(AzureTableRepository azureTable, AzureBlobRepository azureBlob)
        {
            _table = azureTable;
            _blob = azureBlob;
        }

        #endregion Constructors

        #region Methods
        public IGraphElement GetGraphElement(string id)
        {
            return convertfromEntity(_table.GetEntity<GraphElementEntity>("graph", "id:" + id));
        }

        public IGraphElement GetGraphElementByName(string name)
        {
            return convertfromEntity(_table.GetByRowKeyStartWith<GraphElementEntity>("graph", "name:" + name + ">*<",1).FirstOrDefault());
        }

        public IEnumerable<IGraphElement> GetGraphElementsByType(GraphElementType type)
        {
            
            var elements = _table.GetByRowKeyStartWith<GraphElementEntity>("graph", "type:" + type.ToString().ToLower(),1000);

            return convertfromEntity(elements); 
        }

        public IEnumerable<IGraphElement> GetGraphElementsByApplication(string applicationID)
        {
            var elements = _table.GetByRowKeyStartWith<GraphElementEntity>("app:" + applicationID, "id:", 1000);

            return convertfromEntity(elements);
        }

        public IEnumerable<IGraphElement> GetAll()
        {
            var elements = _table.GetByRowKeyStartWith<GraphElementEntity>("graph", "id:", 1000);

            return convertfromEntity(elements);
        }



        public ServiceResult InsertGraphElement(IGraphElement element)
        {
            GraphElementEntity e = new GraphElementEntity("graph", "id:" + element.ID);

            e.ApplicationID = element.ApplicationID;
            e.ElementType = element.ElementType.ToString().ToLower();
            e.ID = element.ID;
            e.Name = element.Name;

            switch (element.ElementType)
            {
                case GraphElementType.Property:
                    e.IsMultiValued = ((GraphProperty)element).isMultiValued;
                    e.GraphPropertyType = ((GraphProperty)element).GraphPropertyType.ToString();
                    break;
                case GraphElementType.Object:
                    GraphObject obj = (GraphObject)element;
                    List<string> props = new List<string>(obj.GetProperties().Count());
                    foreach (GraphProperty prop in obj.GetProperties())
                    {
                        props.Add(graphpropertyToString(prop));
                    }
                    e.PropertiesCSV = string.Join(",", props.ToArray());
                    e.Determiner = obj.Determiner.ToString();
                    e.Singular = obj.Singular;
                    e.Plural = obj.Plural;
                    break;
                case GraphElementType.Action:
                    GraphAction act = (GraphAction)element;
                    List<string> props2 = new List<string>(act.GetProperties().Count());
                    List<string> objlist = new List<string>();
                    foreach (GraphProperty prop in act.GetProperties())
                    {
                        props2.Add(graphpropertyToString(prop));
                    }

                    foreach(GraphObject o in act.GetAssociatedObjects())
                    {
                        objlist.Add(o.GetID());
                    }
                    e.PropertiesCSV = string.Join(",", props2.ToArray());
                    e.AssociateObjectCSV = string.Join(",", objlist.ToArray());
                    break;
                case GraphElementType.Story:
                    GraphStory story = (GraphStory)element;
                    e.AssociateObjectCSV = story.AssociatedObject.ID;
                    e.AssociateActionCSV = story.AssociatedAction.ID;
                    var tense = story.ActionTense;
                    e.WordTense_ImperativeTense = tense.ImperativeTense;
                    e.WordTense_Past = tense.Past;
                    e.WordTense_PluralPast = tense.PluralPast;
                    e.WordTense_Present = tense.PluralPresent;
                    e.WordTense_Present = tense.Present;                   
                    break;
            }
            return saveEntity(e);
        }

        #endregion Methods

        #region helpers

        private IEnumerable<IGraphElement> convertfromEntity(IEnumerable<GraphElementEntity> elements)
        {
            List<IGraphElement> list = new List<IGraphElement>(elements.Count());
            foreach (GraphElementEntity g in elements)
            {
                list.Add(convertfromEntity(g));
            }
            return list;
        }

        private IGraphElement convertfromEntity(GraphElementEntity e)
        {
            IGraphElement result = null;

            Enum.GraphElementType entityType = GraphElementType.Unknown;
            System.Enum.TryParse<Enum.GraphElementType>(char.ToUpper(e.ElementType[0]) + e.ElementType.Substring(1), out entityType);

            switch (entityType)
            {
                case GraphElementType.Property:
                    result = new JB2.Bowtie.GraphProperty();
                    ((GraphProperty)result).GraphPropertyType = (Enum.GraphPropertyType)System.Enum.Parse(typeof(Enum.GraphPropertyType), e.GraphPropertyType);
                    ((GraphProperty)result).isMultiValued = e.IsMultiValued;
                    break;

                case GraphElementType.Object:
                    result = new JB2.Bowtie.GraphObject();
                    string[] props = e.PropertiesCSV.Split(',');
                    foreach (string prop in props)
                    {
                        ((JB2.Bowtie.GraphObject)result).AddProperty(stringToGraphProperty(prop));
                    }
                    ((GraphObject)result).Determiner = (Enum.GraphDeterminer)System.Enum.Parse(typeof(Enum.GraphDeterminer), e.Determiner);
                    ((GraphObject)result).Singular = e.Singular;
                    ((GraphObject)result).Plural = e.Plural;
                    break;
                case GraphElementType.Action:
                    result = new JB2.Bowtie.GraphAction();
                    foreach (string prop in e.PropertiesCSV.Split(','))
                    {
                        ((JB2.Bowtie.GraphAction)result).AddProperty(stringToGraphProperty(prop));
                    }
                    if (!string.IsNullOrEmpty(e.AssociateObjectCSV))
                    {
                        foreach (string obj in e.AssociateObjectCSV.Split(','))
                        {
                            try
                            {
                                ((JB2.Bowtie.GraphAction)result).AddObject((GraphObject)this.GetGraphElement(obj));
                            }
                            catch(Exception ex)
                            {
                                ex.BowtieLog();
                            }
                        }
                    }
                    break;
                case GraphElementType.Story:
                    result = new JB2.Bowtie.GraphStory();
                    ((JB2.Bowtie.GraphStory)result).AssociatedObject = (GraphObject)this.GetGraphElement(e.AssociateObjectCSV);
                    ((JB2.Bowtie.GraphStory)result).AssociatedAction = (GraphAction)this.GetGraphElement(e.AssociateActionCSV); 
                    var tense = new WordTense();
                    tense.ImperativeTense = e.WordTense_ImperativeTense;
                    tense.Past = e.WordTense_Past;
                    tense.PluralPast = e.WordTense_PluralPast;
                    tense.PluralPresent = e.WordTense_Present;
                    tense.Present = e.WordTense_Present;
                    ((JB2.Bowtie.GraphStory)result).ActionTense = tense;
                    break;
            }
            result.ID = e.ID;
            result.Name = e.Name;
            result.ApplicationID = e.ApplicationID;

            return result;
        }

        private ServiceResult saveEntity(GraphElementEntity e)
        {
            //general partition
            e.PartitionKey = "graph";
            e.RowKey = "id:" + e.ID;
            _table.Insert<GraphElementEntity>(e, true);

            e.PartitionKey = "graph";
            e.RowKey = "name:" + e.Name + ">*<" + e.ID;     
            _table.Insert<GraphElementEntity>(e, true);

            e.PartitionKey = "graph";
            e.RowKey = "type:" + e.ElementType.ToString().ToLower() + "_" + e.ID;
            _table.Insert<GraphElementEntity>(e, true);

            //e.PartitionKey = "graph";
            //e.RowKey = "name:" + e.Name;
            //_table.Insert<GraphElementEntity>(e, true);

            //app partition
            e.PartitionKey = "app:" + e.ApplicationID;
            e.RowKey = "id:" + e.ID;
            _table.Insert<GraphElementEntity>(e, true);

            e.PartitionKey = "app:" + e.ApplicationID;
            e.RowKey = "name:" + e.Name + ">*<" + e.ID;
            _table.Insert<GraphElementEntity>(e, true);

            //app partition
            e.PartitionKey = "app:" + e.ApplicationID;
            e.RowKey = e.ElementType.ToString().ToLower() + ":" + e.Name;
            _table.Insert<GraphElementEntity>(e, true);

            return true;
        }

        private string graphpropertyToString(GraphProperty prop)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(prop.ID);
            sb.Append("|");
            sb.Append(prop.Name);
            sb.Append("|");
            sb.Append(prop.GraphPropertyType.ToString());
            sb.Append("|");
            sb.Append(prop.isMultiValued.ToString());
            sb.Append("|");
            sb.Append(prop.ApplicationID);

            return sb.ToString();
        }

        private GraphProperty stringToGraphProperty(string str)
        {
            GraphProperty p = new GraphProperty();

            string[] propSplits = str.Split('|');
            p.ID = propSplits[0];
            p.Name = propSplits[1];
            p.GraphPropertyType = (Enum.GraphPropertyType)System.Enum.Parse(typeof(GraphPropertyType), propSplits[2]);
            p.isMultiValued = Convert.ToBoolean(propSplits[3]);
            p.ApplicationID = propSplits[4];

            return p;

        }

        #endregion helpers
    }
}
