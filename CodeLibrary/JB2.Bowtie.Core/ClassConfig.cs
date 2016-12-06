using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common.Enum;

using JB2.Common;

namespace JB2.Common
{
    public class ClassConfig : JB2.Common.JB2Class, IClassConfig
    {
        public ClassConfig()
        {
            _props = new MetaDataCollection();
        }

        public string Assembly
        {
            get
            {
                return _props.GetProperty<string>("Assembly");
            }

            set
            {
                _props.SetProperty<string>("Assembly",value);
            }
        }

        public string AssemblyQualifiedName
        {
            get
            {
                return _props.GetProperty<string>("AssemblyQualifiedName");
            }

            set
            {
                _props.SetProperty<string>("AssemblyQualifiedName", value);
            }
        }

        public string Classname
        {
            get
            {
                return _props.GetProperty<string>("Classname");
            }

            set
            {
                _props.SetProperty<string>("Classname", value);
            }
        }

        public ClassType ClassType
        {
            get
            {
                return _props.GetProperty<ClassType>("ClassType");
            }

            set
            {
                _props.SetProperty<ClassType>("ClassType", value);
            }
        }

        public string[] ConstructorParameters
        {
            get
            {
                return _props.GetProperty<string[]>("ConstructorParameters");
            }

            set
            {
                _props.SetProperty<string[]>("ConstructorParameters", value);
            }
        }

        public string Namespace
        {
            get
            {
                return _props.GetProperty<string>("Namespace");
            }

            set
            {
                _props.SetProperty<string>("Namespace", value);
            }
        }

        public int GetConstructorParameterCount()
        {
            return ConstructorParameters.Count();
        }
    }
}
