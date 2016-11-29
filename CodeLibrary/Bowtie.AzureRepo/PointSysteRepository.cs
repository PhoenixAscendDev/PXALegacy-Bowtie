using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

using System.Reflection;


using JB2.Common.Data;
namespace JB2.Bowtie.Data.Azure
{
    public class PointSysteRepository : BowtieRepository<IPointSystem>, IPointSystemRepository
    {

        #region Constructors
        public PointSysteRepository() : this(AzureStorage.PointSystemTable,AzureStorage.PointSystemTable,AzureStorage.GeneralBlob)
        {

        }

        public PointSysteRepository(AzureTableRepository configTable, AzureTableRepository playerData, AzureBlobRepository blob)
        {
            _table = configTable;
            _playerData = playerData;
            _blob = blob;
            _defaultPartitionKey = "pointsystem";
        }

        #endregion Constructors

        public IEnumerable<PointSystemConfig> GetAllConfigs()
        {
            var elist = _table.GetByRowKeyStartWith<DynamicTableEntity>(_defaultPartitionKey, "id:", 1000);


            List<PointSystemConfig> result = new List<PointSystemConfig>();
            foreach(var e in elist)
            {
                result.Add(convertToConfig(e));
            }

            return result;
        }
        public JB2.Common.ServiceResult SavePlayerPoint(IPlayerPoint playerPoint, IPointGiver pointgiver)
        {
            try
            {
                string ticks = DateTime.Now.Ticks.ToString();
                var id = JB2.Common.NewID.UriHash(new Uri(string.Format("http://bowtie.jbsquared?s1={0]&s2={1}&s3={2}&s3={3}", pointgiver.ID, playerPoint.GetPlayerID(), playerPoint.Points.ToString(), ticks)));

                DynamicTableEntity e = new DynamicTableEntity();
                e.SetProperty<string>("ID", id);
                e.SetProperty<int>("Points", playerPoint.Points);
                e.SetProperty<string>("PointSystem", playerPoint.PointSystem);
                e.SetProperty<string>("Description", pointgiver.Description);
                e.SetProperty<string>("GiverID", pointgiver.ID);
                e.SetProperty<string>("GiverName", pointgiver.Name);
                e.SetProperty<string>("GiverType", pointgiver.PointGiverType);
                e.SetProperty<long>("DateEnteredTicks", DateTime.Now.Ticks);


                e.PartitionKey = "point";
                e.RowKey = "id:" + id;
                _playerData.Insert<DynamicTableEntity>(e);

                e.PartitionKey = "point:player:" + playerPoint.GetPlayerID();
                e.RowKey = "id:" + id;
                _playerData.Insert<DynamicTableEntity>(e);

                e.PartitionKey = "point:pointsystem:" + playerPoint.PointSystem;
                e.RowKey = "id:" + id;
                _playerData.Insert<DynamicTableEntity>(e);

                e.PartitionKey = "point:player:" + playerPoint.GetPlayerID();
                e.RowKey = "playerpoint:" + playerPoint.ToString() + ":id:" + id;
                _playerData.Insert<DynamicTableEntity>(e);

                e.PartitionKey = "point:giver:" + pointgiver.ID;
                e.RowKey = "id:" + id;
                _playerData.Insert<DynamicTableEntity>(e);

                e.PartitionKey = "point:giver:" + pointgiver.ID;
                e.RowKey = "point:" + playerPoint.Points + ":id:" + id;
                _playerData.Insert<DynamicTableEntity>(e);
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return new JB2.Common.ServiceResult(ex);
            }

            return true;
        }

        #region protected

        protected  PointSystemConfig convertToConfig(DynamicTableEntity e)
        {
            var config = new PointSystemConfig();
            config.ID = e.GetPropertyValue<string>("ID", string.Empty);
            config.Name = e.GetPropertyValue<string>("Name", string.Empty);
            config.Namespace = e.GetPropertyValue<string>("Namespace", string.Empty);
            config.ClassName = e.GetPropertyValue<string>("Classname", string.Empty);


            return config;
        }

        
        protected override DynamicTableEntity convertToEntity(IPointSystem o)
        {
            DynamicTableEntity e = new DynamicTableEntity();
            e.SetProperty<string>("ID", o.ID);
            e.SetProperty<string>("Name", o.Name);
            e.SetProperty<string>("Pural", o.Plural);
            e.SetProperty<string>("Single", o.Single);
            e.SetProperty<string>("ImperativeTense", o.ReceiveTense.ImperativeTense);
            e.SetProperty<string>("Past", o.ReceiveTense.Past);
            e.SetProperty<string>("PluralPast", o.ReceiveTense.PluralPast);
            e.SetProperty<string>("PluralPresent", o.ReceiveTense.PluralPresent);
            e.SetProperty<string>("Present",o.ReceiveTense.Present);
            e.SetProperty<string>("Namespace", o.GetType().Namespace);
            e.SetProperty<string>("Classname", o.GetType().Name);
            e.SetProperty<string>("Assembly", o.GetType().Assembly.FullName);
            e.SetProperty<string>("AssemblyQualifiedName", o.GetType().AssemblyQualifiedName);

            return e;


        }



        protected override IEnumerable<IPointSystem> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            List<IPointSystem> points = new List<IPointSystem>();
            foreach(var e in list)
            {
                points.Add(convertToObject(e));
            }

            return points;
        }

        protected override IPointSystem convertToObject(DynamicTableEntity e)
        {
            string namespaceString = e.GetPropertyValue<string>("Namespace", string.Empty);
            string classString = e.GetPropertyValue<string>("Classname", string.Empty);
            string qualifiedName = e.GetPropertyValue<string>("AssemblyQualifiedName", string.Empty);
            try
            {
                var fullName = namespaceString + "." + classString;

                // This is assuming that the type will be in the same assembly
                // as the call. If that's not the case, we can look at that later.
                Type type = Type.GetType(qualifiedName);
                if (type == null)
                {
                    throw new ArgumentException("No such type: " + type);
                }
                if (!typeof(IPointSystem).IsAssignableFrom(type))
                {
                    throw new ArgumentException("Type " + type +
                                                " is not compatible with FooParent.");
                }

                
                Assembly a = Assembly.Load("JB2.BitScore");
                return (IPointSystem)Activator.CreateInstance(type);
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
                return null;
            }



        }

        protected override void deleteEntry(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "id:" + e.GetPropertyValue<string>("ID",string.Empty);
            _table.Insert<DynamicTableEntity>(e, replace);

            e.PartitionKey = _defaultPartitionKey + ":" + e.GetPropertyValue<string>("Assembly", string.Empty);
            e.RowKey = "id:" + e.GetPropertyValue<string>("ID", string.Empty);
            _table.Insert<DynamicTableEntity>(e, replace);

            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "id:" + e.GetPropertyValue<string>("ID", string.Empty);
        }

        #endregion protected
    }
}
