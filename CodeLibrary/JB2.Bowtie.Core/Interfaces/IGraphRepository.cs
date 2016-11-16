using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IGraphRepository
    {
        IGraphElement GetGraphElement(string id);
        IGraphElement GetGraphElementByName(string name);

        JB2.Common.IDNamePair GeDataTypeByName(string name);
        JB2.Common.IDNamePair GetDataTypeByID(string id);

        IEnumerable<JB2.Common.IDNamePair> GetDataTypes();

        IEnumerable<IGraphElement> GetGraphElementsByType(Enum.GraphElementType type);
        IEnumerable<IGraphElement> GetGraphElementsByApplication(string applicationID);
        IEnumerable<IGraphElement> GetAll();
        JB2.Common.ServiceResult InsertGraphElement(IGraphElement element);
        JB2.Common.ServiceResult DeleteGraphElement(IGraphElement element);

        
    }
}
