using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Data;

namespace JB2.Bowtie.Data.Azure
{
    public class PlayerRepository : JB2.Common.Data.AzureRepository, IBowtiePlayerRespository
    {
        public PlayerRepository()
        {
            _table = AzureStorage.PlayersTable;
            _blob = null;
        }

        public PlayerRepository(JB2.Common.Data.AzureTableRepository table, JB2.Common.Data.AzureBlobRepository blob)
        {
            _table = table;
            _blob = blob;
        }
        public void Delete(IBowtiePlayer entity)
        {
            throw new NotImplementedException();
        }

        public IBowtiePlayer[] GetAll()
        {
            throw new NotImplementedException();
        }

        public IBowtiePlayer GetById(string id)
        {
            throw new NotImplementedException();
        }

        public BowtieMetadata GetMetaDataByPlayerID(string playerID)
        {
            var entry = _table.GetEntity<PlayerEntry>("player", "id:" + playerID);
            return BowtieMetadataFromEntry(entry); 
        }

        public void Insert(IBowtiePlayer entity)
        {
            throw new NotImplementedException();
        }

        public IBowtiePlayer[] SearchFor()
        {
            throw new NotImplementedException();
        }

        private BowtieMetadata BowtieMetadataFromEntry(PlayerEntry e)
        {
            BowtieMetadata md = new BowtieMetadata(e.ID)
            {
                BitScore = Convert.ToInt64(e.BitScore),
                jBeanAccountNumber = e.jBeanAccountNumber,
                Kenshin = new Kenshin() { ID = e.KenshinID, Name = e.KenshinName },
                Title = e.Title
            };
            return md;
        }

    }
}
