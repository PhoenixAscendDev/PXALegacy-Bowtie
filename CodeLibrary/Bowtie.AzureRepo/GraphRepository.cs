using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Enum;
using JB2.Common;
using JB2.Common.Data;
using JB2.Grab;

using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Bowtie.Data.Azure
{
    public class GraphRepository : IGraphRepository
    {
        private JB2.Common.Data.AzureTableRepository _table;
        private JB2.Common.Data.AzureBlobRepository _blob;
        private JB2.Common.Data.AzureTableRepository _playerStorytable;
       

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
            _playerStorytable = _table;
        }

        public GraphRepository(AzureTableRepository azureTable, AzureBlobRepository azureBlob, AzureTableRepository playerStoryTable)
        {
            _table = azureTable;
            _blob = azureBlob;
            _playerStorytable = playerStoryTable;
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


        public ServiceResult DeleteGraphElement(IGraphElement element)
        {
            try
            {
                string pkey = string.Empty;
                string rkey = string.Empty;

                pkey = "graph";
                rkey = "id" + element.ID;
                _table.Delete<DynamicTableEntity>(pkey, rkey);

                pkey = "graph";
                rkey = "name:" + element.Name + ">*<" + element.ID;
                _table.Delete<DynamicTableEntity>(pkey, rkey);

                pkey = "graph";
                rkey = "type:" + element.ElementType.ToString().ToLower() + "_" + element.ID;
                _table.Delete<DynamicTableEntity>(pkey, rkey);

                pkey = "app:" + element.ApplicationID;
                rkey = "id:" + element.ID;
                _table.Delete<DynamicTableEntity>(pkey, rkey);

                pkey = "app:" + element.ApplicationID;
                rkey = "name:" + element.ID;
                _table.Delete<DynamicTableEntity>(pkey, rkey);

                pkey = "app:" + element.ApplicationID;
                rkey = element.ElementType.ToString().ToLower() + ":" + element.Name;
                _table.Delete<DynamicTableEntity>(pkey, rkey);
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
                return new ServiceResult(ex);
            }
            return true;
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
                    e.GraphPropertyType = ((GraphProperty)element).GraphPropertyType.ID;
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
                    e.WordTenseImperativeTense = tense.ImperativeTense;
                    e.WordTensePast = tense.Past;
                    e.WordTensePluralPast = tense.PluralPast;
                    e.WordTensePresent = tense.PluralPresent;
                    e.WordTensePresent = tense.Present;                   
                    break;
            }
            return saveEntity(e);
        }

        #endregion Methods

        #region Stories
        public JB2.Common.ServiceResult InsertPlayerStory(IPlayerStory story)
        {         
            var pipe = convertToGraphPipe(story);

            return playerStoryRepo().Insert(pipe);

        }

        public IEnumerable<IPlayerStory> GetPlayerStoryByObject(string playerid, string objectid)
        {

            IGraphElement graphObject = this.GetGraphElement(objectid);
            var list = playerStoryRepo().Grab(new GraphLabel(playerid, "appPlayer"), GraphLabel.All, new GraphLabel(objectid, graphObject.Name),Grab.Enum.EdgeDirection.None);

            return convertToPlayerStory(list.GetPipes());
        }

        public IEnumerable<IPlayerStory> GetPlayerStoryByPlayer(string playerid)
        {
            
            var list = playerStoryRepo().Grab(new GraphLabel(playerid, "appPlayer"), GraphLabel.All, GraphLabel.All, Grab.Enum.EdgeDirection.None);

            return convertToPlayerStory(list.GetPipes());
        }

        public IEnumerable<IPlayerStory> GetPlayerStoryByAction(string playerid, string actionid)
        {
            IGraphElement graphAction = this.GetGraphElement(actionid);
            var list = playerStoryRepo().Grab(new GraphLabel(playerid, "appPlayer"), new GraphLabel(actionid, graphAction.Name), GraphLabel.All , Grab.Enum.EdgeDirection.None);

            return convertToPlayerStory(list.GetPipes());
        }
        #endregion Stories


        #region helpers

        private JB2.Grab.Data.GrabRepository playerStoryRepo()
        {
            return new Grab.Data.GrabRepository(_playerStorytable, _blob);
        }

        private JB2.Grab.IPipe convertToGraphPipe(IPlayerStory story)
        {
            JB2.Grab.Pipe pipe = Pipe.New(story.Name, "appPlayer".ToLower(), story.ActionName.ToLower(), story.ObjectName.ToLower());
            var playerRepo = JB2.Settings.Bowtie.UnitOfWork.PlayerRepository;

            var player = playerRepo.GetAppPlayerByID(story.GetPlayerID(), story.GetApplicationID());

            //set up the player node
            pipe.Node1.ID = story.GetPlayerID();
            pipe.Node1.AddProperty<string>("firstName", player.Name.First);
            pipe.Node1.AddProperty<string>("lastName", player.Name.Last);
            pipe.Node1.AddProperty<string>("displayName", player.DisplayName);
            pipe.Node1.AddProperty<string>("applicationID", story.GetApplicationID());


            //set the elements properties

            foreach (var p in story.PlayerData)
            {
                pipe.Node1.AddProperty<string>(p.PropertyName, p.GetValue().StringValue);
            }





            foreach (var p in story.ActionData)
            {
                pipe.Edge.AddProperty<string>(p.PropertyName, p.GetValue().StringValue);
            }

            

            foreach (var p in story.ObjectData)
            {
                pipe.Node2.AddProperty<string>(p.PropertyName, p.GetValue().StringValue);
            }
            


            return pipe;




        }

        private IPlayerStory convertToPlayerStory(JB2.Grab.IPipe pipe)
        {

            var playerid = pipe.Node1.ID;
            var applicationid = "";
            var playerData = pipe.Node1.GetProperties();
            var actionData = pipe.Edge.GetProperties();
            var objectData = pipe.Node2.GetProperties();
            IGraphElement graphAction = null;
            IGraphElement graphObject = null;


            foreach (var p in playerData)
            {
                if (p.PropertyName.ToLower() == "applicationid")
                    applicationid = p.GetValue().StringValue;
            }

            foreach (var p in actionData)
            {
                if (p.PropertyName.ToLower() == "actionid")
                {
                    graphAction = this.GetGraphElement(p.GetValue().StringValue);
                }
            }

            foreach (var p in objectData)
            {
                if (p.PropertyName.ToLower() == "objectid")
                {
                    graphObject = this.GetGraphElement(p.GetValue().StringValue);
                }
            }



            IPlayerStory story = new PlayerStory(playerid, applicationid, (GraphAction)graphAction, (GraphObject)graphObject);
            story.PlayerData = playerData;
            story.ActionData = actionData;
            story.ObjectData = objectData;

            return story;
        }

        private IEnumerable<IPlayerStory> convertToPlayerStory(IEnumerable<JB2.Grab.IPipe> elist)
        {
            var list = new List<IPlayerStory>();

            foreach (var e in elist)
            {
                list.Add(convertToPlayerStory(e));
            }

            return list;
        }


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

                    JB2.Common.IDNamePair propType = this.GeDataTypeByName(e.GraphPropertyType).ID == String.Empty ? this.GetDataTypeByID(e.GraphPropertyType) : this.GeDataTypeByName(e.GraphPropertyType);

                    ((GraphProperty)result).GraphPropertyType = propType;
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
                    tense.ImperativeTense = e.WordTenseImperativeTense;
                    tense.Past = e.WordTensePast;
                    tense.PluralPast = e.WordTensePluralPast;
                    tense.PluralPresent = e.WordTensePresent;
                    tense.Present = e.WordTensePresent;
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





            //remove any app partitions
            var apps = JB2.Settings.Bowtie.UnitOfWork.ApplicationRepository.GetAll();
            foreach (var a in apps)
            {
                var pkey = string.Empty;
                var rkey = string.Empty;
                if (a.GetID() != e.ApplicationID)
                {
                    pkey = "app:" + a.GetID();
                    rkey = "id:" + e.ID;
                    _table.Delete<GraphElementEntity>(pkey, rkey);

                    pkey = "app:" + a.GetID();
                    rkey = "name:" + e.ID;
                    _table.Delete<GraphElementEntity>(pkey, rkey);

                    pkey = "app:" + a.GetID();
                    rkey = e.ElementType.ToString().ToLower() + ":" + e.Name;
                    _table.Delete<GraphElementEntity>(pkey, rkey);
                }
            }


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
            sb.Append(prop.GraphPropertyType.ID);
            sb.Append("|");
            sb.Append(prop.isMultiValued.ToString());
            sb.Append("|");
            sb.Append(prop.ApplicationID);

            return sb.ToString();
        }

        private GraphProperty stringToGraphProperty(string str)
        {

            if (!string.IsNullOrEmpty(str))
            {
                GraphProperty p = new GraphProperty();

                string[] propSplits = str.Split('|');
                p.ID = propSplits[0];
                p.Name = propSplits[1];

                JB2.Common.IDNamePair propType = this.GeDataTypeByName(propSplits[2]).ID == String.Empty ? this.GetDataTypeByID(propSplits[2]) : this.GeDataTypeByName(propSplits[2]);
                p.GraphPropertyType = propType;
                p.isMultiValued = Convert.ToBoolean(propSplits[3]);
                p.ApplicationID = propSplits[4];
                return p;
            }
            else
                return null;

           

        }


        public JB2.Common.IDNamePair GeDataTypeByName(string name)
        {
            var list = GetDataTypes();

            return list.ToList().Find(x => x.Name == name) == null ? new IDNamePair(string.Empty, Enum.GraphSimplePropertyType.Unknown.ToString()) { ID = string.Empty } : list.ToList().Find(x => x.Name == name);
        }

        public JB2.Common.IDNamePair GetDataTypeByID(string id)
        {
            var list = GetDataTypes();

            return list.ToList().Find(x => x.ID == id) == null ? new IDNamePair(string.Empty, Enum.GraphSimplePropertyType.Unknown.ToString()) { ID = string.Empty } : list.ToList().Find(x => x.ID == id);


        }

        public IEnumerable<JB2.Common.IDNamePair> GetDataTypes()
        {
             var simple = System.Enum.GetValues(typeof(Enum.GraphSimplePropertyType));

            List<JB2.Common.IDNamePair> result = new List<IDNamePair>();

            //convert to enum to Data Type
            foreach(var s in simple)
            {
                result.Add(new IDNamePair( ((int)s).ToString(), ((Enum.GraphSimplePropertyType)s).ToString()));
            }


            //convert graph objects as a Data Type
            var objs = _table.GetByRowKeyStartWith<DynamicTableEntity>("graph", "type:" + GraphElementType.Object.ToString().ToLower(), 1000);
            foreach (var o in objs)
            {
                result.Add(new IDNamePair(o.GetPropertyValue<string>("ID",string.Empty), o.GetPropertyValue<string>("Name",string.Empty)));
            }

            return result;
        }

        #endregion helpers
    }
}
