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

        public IEnumerable<IGraphElement> GetGraphElementsByType(GraphElementType type)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGraphElement> GetGraphElementsByApplication(string applicationID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGraphElement> GetAll()
        {
            throw new NotImplementedException();
        }

        public ServiceResult InsertGraphElement(IGraphElement element)
        {
            GraphElementEntity e = new GraphElementEntity("graph", "id:" + element.ID);

            e.ApplicationID = element.ApplicationID;
            e.ElementType = element.ElementType.ToString().ToLower();
            e.ID = element.ID;
            e.Name = element.Name;

            return saveEntity(e);
        }

        #endregion Methods

        #region helpers

        private IGraphElement convertfromEntity(GraphElementEntity e)
        {
            IGraphElement result = null;

            Enum.GraphElementType entityType = GraphElementType.Unknown;
            System.Enum.TryParse<Enum.GraphElementType>(e.ElementType, out entityType);

            switch (entityType)
            {
                case GraphElementType.Property:
                    result = new JB2.Bowtie.GraphProperty();
                    break;
                case GraphElementType.Object:
                    result = new JB2.Bowtie.GraphObject();
                    break;
                case GraphElementType.Action:
                    result = new JB2.Bowtie.GraphAction();
                    break;
                case GraphElementType.Story:
                    result = new JB2.Bowtie.GraphStory();
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
            e.RowKey = "name:" + e.Name;
            _table.Insert<GraphElementEntity>(e, true);

            //app partition
            e.PartitionKey = "app:" + e.ApplicationID;
            e.RowKey = "id:" + e.ID;
            _table.Insert<GraphElementEntity>(e, true);

            //app partition
            e.PartitionKey = "app:" + e.ApplicationID;
            e.RowKey = e.ElementType.ToString().ToLower() + ":" + e.Name;
            _table.Insert<GraphElementEntity>(e, true);

            return true;
        }

        #endregion helpers


    }
}
