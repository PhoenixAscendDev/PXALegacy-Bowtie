using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{

    public interface IExternalClass : IExternalClass<object>
    {

    }

    public interface IExternalClass<T> : JB2.Common.IClassConfig, JB2.Common.IIDProp<string>
    {
        T Construct();

        T Construct(KeyValuePair<string, object> parameter1);

        T Construct(KeyValuePair<string, object> parameter1, KeyValuePair<string, object> parameter2);

        T Construct(KeyValuePair<string, object> parameter1, KeyValuePair<string, object> parameter2, KeyValuePair<string, object> parameter3);

        T Construct(IEnumerable<KeyValuePair<string, object>> parameters);

    }
}
