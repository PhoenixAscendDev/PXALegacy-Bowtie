using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class Client : BowtieObject, IClient
    {
        public Client() : base(Enum.BowtieObjectType.bowtie_client,null)
        {

        }

        public Client(string id) : base(Enum.BowtieObjectType.bowtie_client,id)
        {

        }


        public Common.IAddress MailingAddress
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Common.IPerson<string> POC
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
