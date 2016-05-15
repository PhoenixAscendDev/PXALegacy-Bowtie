using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;
using JB2.Identity;
using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Bowtie.Data.Azure
{
    public class PlayerRepository : BowtieRepository<IBowtiePlayer>, IBowtiePlayerRespository
    {
        public BowtieMetadata GetMetaDataByPlayerID(string playerID)
        {
            throw new NotImplementedException();
        }

        protected override DynamicTableEntity convertToEntity(IBowtiePlayer o)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<IBowtiePlayer> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            throw new NotImplementedException();
        }

        protected override IBowtiePlayer convertToObject(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void deleteAll(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            throw new NotImplementedException();
        }
    }
}
