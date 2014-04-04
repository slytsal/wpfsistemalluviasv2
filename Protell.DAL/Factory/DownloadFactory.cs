using Protell.DAL.Repository;
using Protell.DAL.Repository.v2;
using Protell.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.DAL.Factory
{
    public sealed class DownloadFactory
    {
        private static readonly Lazy<DownloadFactory> lazy = new Lazy<DownloadFactory>(() => new DownloadFactory());
        public static DownloadFactory Instance { get { return lazy.Value; } }

        
        public string CallWebService(string webMethodName, object bodyContent)
        {
            string res="";
            var client = new RestClient(SyncProperties.routeDownload);
            var request = new RestRequest(Method.POST);
            request.Resource = webMethodName;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-type", "application/json");
            request.AddBody(bodyContent);
            IRestResponse response = client.Execute(request);
            res = response.Content;
            return res;
        }

        //Para todas menos CI_REGISTRO
        public string CallWebService(string webMethodName, string tableName)
        {
            MaxTableModel maximos = new MaxTableModel();
            object bodyContent = null;
            try
            {
                using (var repository = new SyncRepository())
                {
                    maximos = repository.GetMaxTable(tableName);
                    bodyContent = maximos;
                }                
            }
            catch (Exception ex)
            {
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }
            return this.CallWebService(webMethodName, bodyContent);
            
        }

    }
}
