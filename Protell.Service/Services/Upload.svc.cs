using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Protell.Model.SyncModels;
using Protell.Server.DAL.Repository.v2;
using System.Collections.Generic;

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


        public List<CiRegistroUploadConfirmationModel> Upload_CiRegistro(CiRegistroUploadModel param)
        {
            List<CiRegistroUploadConfirmationModel> response = null;
            try
            {
                if (param != null && param.CiRegistro != null && param.CiRegistro.Count > 0)
                {
                    CiRegistroRepository crr = new CiRegistroRepository();
                    response=crr.UpsertUploaded(param.CiRegistro);
                }
            }
            catch (Exception ex)
            {                
                throw;
            }

            return response;
        }
    }
}
