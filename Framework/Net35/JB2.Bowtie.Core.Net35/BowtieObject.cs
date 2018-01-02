using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using JB2.Common;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public abstract class BowtieObject : JB2.Sprog.SprogItem, IBowtieObject, IClass
    {
        #region Fields
        
        protected int _rng = 0;
       

        #endregion Fields

        #region Constructors
        public BowtieObject(BowtieObjectType kind, string id) : this(id)
        {
            this._kind = (int)kind;
        }

        public BowtieObject(string id, JB2.Sprog.ISprogType sprogType = null) : base(sprogType)
        {

            this._id = id == null ? JB2.Helper.Bowtie.GenerateID<BowtieObject>() : id;
            //if(id == null)
            //    this._id = ;

            this._tags = new List<Tag>();
            this._name = string.Empty;
            this._kind = (int)BowtieObjectType.unknown;
            this._rng = JB2.Helper.Bowtie.NewRNG();
            

        }

        public BowtieObject() : this(null)
        {

            
        }

        #endregion Constructors

        public string UniqueToken
        {
            get
            {
                StringBuilder token = new StringBuilder(this._id);

                string tokenName = ((Enum.BowtieObjectType)this._kind).GetAttributeOfType<Attributes.TokenName>().Name;
                token.Append(">*<");
                token.Append(tokenName);
                return token.ToString();
            }
        }



        public Enum.BowtieObjectType Kind
        {
            get
            {
                return (Enum.BowtieObjectType)_kind;
            }

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
            return (Enum.BowtieObjectType)_kind;
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
