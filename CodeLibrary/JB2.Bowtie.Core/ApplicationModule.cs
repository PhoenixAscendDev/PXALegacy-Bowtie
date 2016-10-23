using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie
{
    public class ApplicationModulePermission :  JB2Class,IApplicationable<string>
    {
        #region Fields

        #endregion Fields

        #region Constructors

        public ApplicationModulePermission() : base()
        {

        }

        #endregion Constructos

        #region Properties

        public string ApplicationID
        {
            get
            {
                return this.GetProperity<string>("ApplicationID", string.Empty);
            }
            set
            {
                this.SetProperty<string>("ApplicationID", value);
            }
        }

        public string ModuleID
        {
            get
            {
                return this.GetProperity<string>("ModuleID", string.Empty);
            }
            set
            {
                this.SetProperty<string>("ModuleID", value);
            }
        }

        public Enum.ModulePermissionType Permission
        {
            get
            {
                return this.GetProperity<Enum.ModulePermissionType>("Permission",Enum.ModulePermissionType.None);
            }
            set
            {
                this.SetProperty<Enum.ModulePermissionType>("Permission", value);
            }
        }

        public JB2.Common.ApiKeySecretPair AccessKey
        {
            get
            {
                return this.GetProperity<ApiKeySecretPair>("AccessKey", new ApiKeySecretPair());
            }
            set
            {
                this.SetProperty<ApiKeySecretPair>("AccessKey", value);
            }
        }

        #endregion Properties

        #region IApplicationable
        public string GetApplicationID()
        {
            throw new NotImplementedException();
        }
        #endregion IApplicationable

    }
}
