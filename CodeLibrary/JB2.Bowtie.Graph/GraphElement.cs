using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Bowtie
{
    public abstract class GraphElement : JB2.Common.IDNamePair<string,string>,IGraphElement
    {

        #region Fields
        protected object _value;

        protected JB2.Common.MetaDataCollection _metadata;

        #endregion Fields

        public string ApplicationID
        {
            get; set;
        }

        public virtual Enum.GraphElementType ElementType
        {
            get;
        }

        public string ParentID
        {
            get; set;
        }

        public string PropertyName
        {
            get
            {
                return _name;
            }
        }

        public Type PropertyType
        {
            get
            {             
                return _value != null ? _value.GetType() : typeof(string).GetType();
            }
        }

        public object GetValue()
        {
            return _value;
        }

        public IMetaData MetaData(string propertyName)
        {
            return _metadata[propertyName];
        }

        public virtual string ToHtmlMetaTag()
        {
            string format = "<meta property=\"{0}\" content=\"{1}\">";
            return string.Format(format, this.PropertyName, this.GetValue());
        }
    }
}
