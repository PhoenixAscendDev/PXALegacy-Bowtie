using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common.Enum;

namespace JB2.Bowtie
{
    public abstract class ObjectConfig : JB2.Bowtie.IExternalClass
    {

        #region Fields
        protected JB2.Bowtie.ExternalClass _classconfig;
        #endregion Fields

        #region Constructors
        public ObjectConfig()
        {
            _classconfig = new ExternalClass();
        }


        #endregion Constructors

        #region IExteneralConfig

        public virtual string Assembly
        {
            get
            {
                return _classconfig.Assembly;
            }

            set
            {
                _classconfig.Assembly = value;
            }
        }

        public virtual string AssemblyQualifiedName
        {
            get
            {
                return _classconfig.AssemblyQualifiedName;
            }

            set
            {
                _classconfig.AssemblyQualifiedName = value;
            }
        }

        public virtual string Classname
        {
            get
            {
                return _classconfig.Classname;
            }

            set
            {
                _classconfig.Classname = value;
            }
        }

        public virtual ClassType ClassType
        {
            get
            {
                return _classconfig.ClassType;
            }

            set
            {
                _classconfig.ClassType = value;
            }
        }

        public virtual string[] ConstructorParameters
        {
            get
            {
                return _classconfig.ConstructorParameters;
            }

            set
            {
                _classconfig.ConstructorParameters = value;
            }
        }

        public virtual string Namespace
        {
            get
            {
                return _classconfig.Namespace;
            }

            set
            {
                _classconfig.Namespace = value;
            }
        }

        public virtual string ClassConfigID
        {
            get
            {
                return _classconfig.ClassConfigID;
            }
            set
            {
                _classconfig.ClassConfigID = value;
            }
        }

        public virtual int GetConstructorParameterCount()
        {
            return _classconfig.GetConstructorParameterCount();
        }

        public abstract string GetID();
       


        #endregion IClassConfig


    }
}
