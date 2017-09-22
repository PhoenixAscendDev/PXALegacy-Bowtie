using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public delegate JB2.Common.ServiceResult CheckAuthorize(IApplication application, string authorizeKey);

    public delegate ushort NewRNG();

    public delegate string JsonSerializer(object obj);

    public delegate T JsonDeserializer<T>(string json, Type type);

    public delegate bool OnlineCheck();

}