using Protell.Model.SyncModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Protell.Service.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUpload" in both code and config file together.
    [ServiceContract]
    public interface IUpload
    {
        [OperationContract]
        [WebInvoke(Method = "POST",RequestFormat = WebMessageFormat.Json,ResponseFormat = WebMessageFormat.Json,BodyStyle = WebMessageBodyStyle.Wrapped)]
        AppRolResultModel Upload_AppRol(AppRolResultModel param);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<CiRegistroUploadConfirmationModel> Upload_CiRegistro(CiRegistroUploadModel param);
    }
}
