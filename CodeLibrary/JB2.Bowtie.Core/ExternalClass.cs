using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common.Enum;

using JB2.Common;

namespace JB2.Bowtie
{

    public class ExternalClass: ExternalClass<object>, IExternalClass
    {

    }
    public class ExternalClass<T> : JB2.Common.JB2Class, IExternalClass<T>
    {
        #region IExternalClass
        public ExternalClass()
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

        public string Category
        {
            get
            {
                return _props.GetProperty<string>("Category", string.Empty);
            }

            set
            {
                _props.SetProperty<string>("Category", value);
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

        public string ClassConfigID
        {
            get
            {
                return _props.GetProperty<string>("ClassConfigID");
            }
            set
            {
                _props.SetProperty<string>("ClassConfigID", value);
            }
        }


        public string GetID()
        {
            return ClassConfigID;
        }


        public int GetConstructorParameterCount()
        {
            return ConstructorParameters.Count();
        }

        #endregion


        #region Constructors

        public T Construct()
        {
            return Construct(new KeyValuePair<string, object>[0]);
        }

        public T Construct(KeyValuePair<string, object> parameter1)
        {
            return Construct(new KeyValuePair<string, object>[1] { parameter1 });
        }

        public T Construct(KeyValuePair<string, object> parameter1, KeyValuePair<string, object> parameter2)
        {
            return Construct(new KeyValuePair<string, object>[2] { parameter1, parameter2 } );
        }

        public T Construct(KeyValuePair<string, object> parameter1, KeyValuePair<string, object> parameter2, KeyValuePair<string, object> parameter3)
        {
            return Construct(new KeyValuePair<string, object>[3] { parameter1, parameter2, parameter3 } );
        }

        public T Construct(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            try
            {
                var fullName = this.AssemblyQualifiedName;

                // This is assuming that the type will be in the same assembly
                // as the call. If that's not the case, we can look at that later.
                Type type = Type.GetType(fullName);
                if (type == null)
                {
                    throw new ArgumentException("No such type: " + type);
                }
                if (!typeof(T).IsAssignableFrom(type))
                {
                    throw new ArgumentException("Type " + type +
                                                " is not compatible object.");
                }
                return (T)Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return default(T);
            }
        }

        #endregion Constructors

        #region Implicit Operators
        public static implicit operator T(ExternalClass<T> ec)
        {
            return ec.Construct();
        }


        #endregion Implicit Operators
    }
}
