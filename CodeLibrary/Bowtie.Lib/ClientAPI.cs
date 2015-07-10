using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.API.Client
{
    public static class BaseGame
    {
        private static Application _application;

        public static bool InitilizeGame(string publicKey, string secret)
        {
            _application = new Application(publicKey, secret);

            return true;

        }
    }
}
