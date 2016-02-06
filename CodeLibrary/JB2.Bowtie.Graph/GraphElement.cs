using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Bowtie
{
    public abstract class GraphElement : IGraphElement
    {

        #region Fields
        protected object _value;
        protected JB2.Common.MetaDataCollection _metadata;

        #endregion Fields

        public string ApplicationID
        {
            get; set;
        }

        public GraphElementType ElementType
        {
            get; set;
        }

        public string ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string ParentID
        {
            get; set;
        }

        public string PropertyName
        {
            get;
        }

        public Type PropertyType
        {
            get
            {
                return _value.GetType();
            }
        }

        public string GetID()
        {
            return ID;
        }

        public string GetName()
        {
            return Name;
        }

        public object GetValue()
        {
            return _value;
        }

        public IMetaData MetaData(string propertyName)
        {
            return _metadata[propertyName];
        }
    }
}
