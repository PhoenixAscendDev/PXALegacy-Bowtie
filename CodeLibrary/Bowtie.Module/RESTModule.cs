using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;

using JB2.Common;


namespace JB2.Bowtie
{
    public class RESTModule : JB2.Common.JB2Class, IModule
    {

        #region Constructor
        public RESTModule(string id) : base()
        {
            this.SetProperty<string>("ID", id);
        }

        #endregion Constructor


        public BowtieAPI API
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

        public string ID
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

        public ModuleType ModuleType
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

        public string Name
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

        public int RNG
        {
            get
            {
                return JB2.Common.RNG.Randy;
            }

            set
            {
                this.SetProperty<int>("RNG", value);
            }
        }

        public ModuleStatusType Status
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

        public string UniqueToken
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

        public string GetID()
        {
            return this.ID;
        }

        public BowtieObjectType GetKind()
        {
            return BowtieObjectType.bowtie_module;
        }

        public string GetName()
        {
            return this.Name;
        }

        public BowtieMetadata GetPlayerData(string playerID)
        {
            throw new NotImplementedException();
        }

        #region Tags
        public bool AddTag(ObjectTag tag)
        {
            var listTags = this.GetProperity<List<ObjectTag>>("Tags", new List<ObjectTag>());
            listTags.Add(tag);

            this.SetProperty<List<ObjectTag>>("Tags", listTags);
            return true;
        }

        public IEnumerable<ObjectTag> GetTags()
        {
            return this.GetProperity<List<ObjectTag>>("Tags", new List<ObjectTag>());
        }

        public bool RemoveTag(ObjectTag tag)
        {
            var listTags = this.GetProperity<List<ObjectTag>>("Tags", new List<ObjectTag>());
            listTags.Remove(tag);

            this.SetProperty<List<ObjectTag>>("Tags", listTags);
            return true;
        }

        #endregion Tags
    }
}
