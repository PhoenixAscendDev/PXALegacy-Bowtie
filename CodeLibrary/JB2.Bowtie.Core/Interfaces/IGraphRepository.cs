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

        IEnumerable<IGraphElement> GetGraphElementsByType(Enum.GraphElementType type);
        IEnumerable<IGraphElement> GetGraphElementsByApplication(string applicationID);
        IEnumerable<IGraphElement> GetAll();
        JB2.Common.ServiceResult InsertGraphElement(IGraphElement element);

        JB2.Common.ServiceResult DeleteGraphElement(IGraphElement element);

        
    }
}
