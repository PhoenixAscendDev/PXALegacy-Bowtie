using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common.Enum;

namespace JB2.Bowtie
{
    public abstract class ObjectConfig : JB2.Common.IClassConfig
    {

        #region Fields
        protected JB2.Common.ClassConfig _classconfig;
        #endregion Fields

        #region Constructors
        public ObjectConfig()
        {
            _classconfig = new Common.ClassConfig();
        }


        #endregion Constructors

        #region IClassConfig

        public string Assembly
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

        public string AssemblyQualifiedName
        {
            get
            {
                return _classconfig.AssemblyQualifiedName
            }

            set
            {
                _classconfig.AssemblyQualifiedName = value;
            }
        }

        public string Classname
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

        public ClassType ClassType
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

        public string[] ConstructorParameters
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

        public string Namespace
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

        public int GetConstructorParameterCount()
        {
            return _classconfig.GetConstructorParameterCount();
        }


        #endregion IClassConfig


    }
}
