using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public abstract class Module : JB2.Common.JB2Class, IModule
    {
        public Module() : base()
        {
            this.SetProperty<BowtieAPI>("API", new BowtieAPI());

        }

        public virtual BowtieAPI API
        {
            get
            {
                return this.GetProperity<BowtieAPI>("API", new BowtieAPI());
            }

            set
            {
                this.SetProperty<BowtieAPI>("API", value);
            }
        }

        public virtual string ID
        {
            get
            {
                return this.GetProperity<string>("ID", string.Empty);
            }

            set
            {
                this.SetProperty<string>("ID", value);
            }
        }

        public virtual ModuleType ModuleType
        {
            get
            {
                return this.GetProperity<ModuleType>("ModuleType", ModuleType.REST);
            }

            set
            {
                this.SetProperty<ModuleType>("ModuleType", value);
            }
        }

        public virtual string Name
        {
            get
            {
                return this.GetProperity<string>("Name", string.Empty);
            }

            set
            {
                this.SetProperty<string>("Name", value);
            }
        }

        public virtual int RNG
        {
            get
            {
                return JB2.Helper.Bowtie.NewRNG();
            }

            set
            {
                this.SetProperty<int>("RNG", value);
            }
        }

        public virtual ModuleStatusType Status
        {
            get
            {
                return this.GetProperity<ModuleStatusType>("Status", ModuleStatusType.Online);
            }

            set
            {
                this.SetProperty<ModuleStatusType>("Status", value);
            }
        }

        public virtual string UniqueToken
        {
            get
            {
                StringBuilder token = new StringBuilder(this.GetID());

                string tokenName = this.GetKind().GetAttributeOfType<Attributes.TokenName>().Name;
                token.Append(">*<");
                token.Append(tokenName);
                return token.ToString();
            }
        }

        public virtual IEnumerable<IInventoryItem> InventoryItems
        {
            get
            {
                return this.GetProperity<IEnumerable<IInventoryItem>>("InventoryItems", new List<IInventoryItem>());
            }

            set
            {
                this.SetProperty<IEnumerable<IInventoryItem>>("InventoryItems", value);
            }
        }

        public virtual IEnumerable<string> PlayerDataNames
        {
            get
            {
                return this.GetProperity<IEnumerable<string>>("PlayerDataName", new List<string>());
            }

            set
            {
                this.SetProperty<IEnumerable<string>>("PlayerDataName", value);
            }
        }

        public virtual string GetID()
        {
            return this.ID;
        }

        public virtual BowtieObjectType GetKind()
        {
            return BowtieObjectType.bowtie_module;
        }

        public virtual string GetName()
        {
            return this.Name;
        }

        public abstract BowtieMetadata GetPlayerData(string playerID,ApiKeySecretPair accesskey);

        public abstract IEnumerable<IPlayerInventoryItem> GetPlayerInventory(string playerID, ApiKeySecretPair accesskey);

        public abstract ServiceResult SetPlayerInventory(string playerID,ApiKeySecretPair accesskey,IPlayerInventoryItem item);

        public abstract ServiceResult SetPlayerData(string playerID, ApiKeySecretPair accesskey, IMetaData data);


        #region Tags
        public virtual bool AddTag(Tag tag)
        {
            var listTags = this.GetProperity<List<Tag>>("Tags", new List<Tag>());
            listTags.Add(tag);

            this.SetProperty<List<Tag>>("Tags", listTags);
            return true;
        }

        public virtual IEnumerable<Tag> GetTags()
        {
            return this.GetProperity<List<Tag>>("Tags", new List<Tag>());
        }

        public virtual bool RemoveTag(Tag tag)
        {
            var listTags = this.GetProperity<List<Tag>>("Tags", new List<Tag>());
            listTags.Remove(tag);

            this.SetProperty<List<Tag>>("Tags", listTags);
            return true;
        }

        public ServiceResult LoadTags(IEnumerable<Tag> tags)
        {
            try
            {
                var listTags = this.GetProperity<List<Tag>>("Tags", new List<Tag>());
                listTags.AddRange(tags);

                this.SetProperty<List<Tag>>("Tags", listTags);

                return true;
            }
            catch(Exception ex)
            {
                return new ServiceResult(ex);
            }


        }

        #endregion Tags
    }
}
