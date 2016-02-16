using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Common.Extensions;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public class BowtieObject : IDNamePair, IBowtieObject
    {
        protected List<ObjectTag> _tags;
        protected BowtieObjectType _kind;
              
        public BowtieObject(BowtieObjectType kind,string id): this(id)
        {
            this._kind = kind;
        }

        public BowtieObject(string id) : this()
        {
            this._id = id == null ? JB2.Bowtie.Utility.GenerateNewObjectID() : id;
            //if(id == null)
            //    this._id = ;
            
        }

        public BowtieObject()
        {
            this._tags = new List<ObjectTag>();
            this._name = string.Empty;
            this._id = JB2.Bowtie.Utility.GenerateNewObjectID();
            this._kind = BowtieObjectType.unknown;
        }

        public string UniqueToken
        {
            get
            {
                StringBuilder token = new StringBuilder(this._id);

                string tokenName = this._kind.GetAttributeOfType<Attributes.TokenName>().Name;
                token.Append(">*<");
                token.Append(tokenName);
                return token.ToString();
            }
        }

        public bool AddTag(Common.ObjectTag tag)
        {
            _tags.Add(tag);
            return true;
        }

        public Enum.BowtieObjectType Kind
        {
            get
            {
                return _kind;
            }
            
        }

        public bool RemoveTag(Common.ObjectTag tag)
        {
            return _tags.Remove(tag);

        }

        public Common.ObjectTag[] Tags
        {
            get
            {
                return _tags.ToArray();
            }
            set
            {
                _tags = value.ToList();
            }
        }

        public BowtieObjectType GetKind()
        {
            return _kind;
        }

        public DateTime GetLastUpdate()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ObjectTag> GetTags()
        {
            return _tags;
        }
    }
}
