using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Common.Enum;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public class BowtieObject : IBowtieObject
    {
        private List<ObjectTag> _tags;
        private BowtieObjectType _kind;
        private string _id;
        private string _name;

        public BowtieObject(BowtieObjectType kind,string id): this(id)
        {
            this._kind = kind;
        }

        public BowtieObject(string id) : this()
        {
            this._id = id;
        }

        public BowtieObject()
        {
            this._tags = new List<ObjectTag>();
            this._name = string.Empty;
            this._id = string.Empty;
            this._kind = BowtieObjectType.unknown;
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

        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }
}
