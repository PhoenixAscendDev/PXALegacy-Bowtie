using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JB2.Common.Enum;

namespace JB2.Bowtie
{
    public abstract class ObjectConfig<T> : JB2.Bowtie.ISystemConfig<T>
        where T: ISystem,new()
    {

        #region Fields
        protected JB2.Bowtie.SystemConfig<T> _classconfig;
        #endregion Fields

        #region Constructors
        public ObjectConfig()
        {
            _classconfig = new SystemConfig<T>();
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

        public virtual string Category
        {
            get
            {
                return _classconfig.Category;
            }
            set
            {
                _classconfig.Category = value;
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

        public virtual string ID
        {
            get
            {
                return _classconfig.ID;
            }
            set
            {
                _classconfig.ID = value;
            }
        }

        public virtual string Name
        {
            get
            {
                return _classconfig.Name;
            }
            set
            {
                _classconfig.Name = value;
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

        public abstract string GetName();

        public T Construct()
        {
            return _classconfig.Construct();
        }

        public T Construct(KeyValuePair<string, object> parameter1)
        {
            return _classconfig.Construct(parameter1);
        }

        public T Construct(KeyValuePair<string, object> parameter1, KeyValuePair<string, object> parameter2)
        {
            return _classconfig.Construct(parameter1,parameter2);
        }

        public T Construct(KeyValuePair<string, object> parameter1, KeyValuePair<string, object> parameter2, KeyValuePair<string, object> parameter3)
        {
            return _classconfig.Construct(parameter1,parameter2,parameter3);
        }

        public T Construct(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return _classconfig.Construct(parameters);
        }





        #endregion IClassConfig


    }
}
