using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Protell.Model.SyncModels;

namespace Protell.Service.Services
{
    
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class Upload : IUpload
    {

        public AppRolResultModel Upload_AppRol(AppRolResultModel param)
        {
            AppRolResultModel res = new AppRolResultModel();
            try
            {

            }
            catch (Exception)
            {
                
                throw;
            }
            return param;
        }
    }
}
