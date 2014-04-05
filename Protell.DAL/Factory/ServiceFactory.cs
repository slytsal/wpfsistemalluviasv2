using System;
using System.Configuration;

namespace Protell.DAL.Factory
{
    public sealed class ServiceFactory
    {
        private static readonly Lazy<ServiceFactory> lazy = new Lazy<ServiceFactory>(() => new ServiceFactory());
        public static ServiceFactory Instance { get { return lazy.Value; } }

        public IServiceFactory getClass(string tableName)
        {
            IServiceFactory retorno = null;
            try
            {                
                string dClass = ConfigurationManager.AppSettings[tableName].ToString(); ;
                Type t = Type.GetType("Protell.DAL.Repository.v2." + dClass, false, true);
                if (t != null)
                    retorno = (IServiceFactory)Activator.CreateInstance(t);
               
            }
            catch (Exception)
            {

            }
            return retorno;
        }

    }
}
