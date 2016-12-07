using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public interface IClassConfig
    {
        string Namespace { get; set; }
        string Classname { get; set; }
        string Assembly { get; set; }
        string AssemblyQualifiedName { get; set; }
        string[] ConstructorParameters { get; set; }
        

        int GetConstructorParameterCount();

        string ClassConfigID { get; set; }
    }
}