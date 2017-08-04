using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public struct ApplicationStatePair
    {

        public ApplicationStatePair(string applicationID, Enum.APIAuthorizeState state)
        {
            this.ApplicationID = applicationID;
            this.AuthorizeState = state;
            this.APIKey = new Common.ApiKeySecretPair();
        }
        public string ApplicationID
        {
            get;set;
        }

        public Enum.APIAuthorizeState AuthorizeState
        {
            get;set;
        }

        public JB2.Common.ApiKeySecretPair APIKey
        {
            get;set;
        }
            
           

         
        
    }
}
