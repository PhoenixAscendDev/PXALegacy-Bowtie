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
    public abstract class BowtieObject : IDNamePair, IBowtieObject, IClass
    {
        #region Fields
        protected List<Tag> _tags;
        protected BowtieObjectType _kind;
        protected int _rng = 0;
        protected MetaDataCollection _props;

        #endregion Fields

        #region Constructors
        public BowtieObject(BowtieObjectType kind, string id) : this(id)
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
            this._tags = new List<Tag>();
            this._name = string.Empty;
            this._kind = BowtieObjectType.unknown;
            this._rng = JB2.Helper.Bowtie.NewRNG();
            this._props = new MetaDataCollection();
            
        }

        #endregion Constructors

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

        public bool AddTag(Common.Tag tag)
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

        public bool RemoveTag(Common.Tag tag)
        {
            return _tags.Remove(tag);

        }

        public Common.Tag[] Tags
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
            return DateTime.Now;
        }

        public IEnumerable<Tag> GetTags()
        {
            return _tags;
        }

        public ServiceResult LoadTags(IEnumerable<Tag> tags)
        {
            try
            {
                if (_tags == null)
                    _tags = new List<Tag>();

                _tags.AddRange(tags);

                return true;
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }
        }

        public T GetProperty<T>(string index, T defaultValue)
        {
            return _props.GetProperty<T>(index);
        }

        public T GetProperity<T>(string index, T defaultValue)
        {
            return _props.GetProperty<T>(index);
        }

        public void SetProperty<T>(string index, T newValue, bool changeLastUpdate)
        {
            _props.SetProperty<T>(index, newValue, changeLastUpdate);
        }

        public int RNG
        {
           get
            {
                return _rng;
            }
            set
            {
                _rng = value;
            }
        }      
    }
}
