using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;

namespace Protell.DAL.Repository
{
    public class GDFRegistroRepository : IGDFRegistro
    {
        public void InsertGDFRegistro(Model.GDFRegistroModel registro)
        {
            throw new NotImplementedException();
        }

        public void UpdateGDFRegistro(Model.RegistroModel registro)
        {
            throw new NotImplementedException();
        }

        public string GetJsonGDFRegistro()
        {
            throw new NotImplementedException();
        }

        public System.Collections.ObjectModel.ObservableCollection<Model.RegistroModel> GetDeserializeGDFRegistro(string listRegistro)
        {
            throw new NotImplementedException();
        }

        public List<Model.ListUnidsModel> LoadSyncServer(System.Collections.ObjectModel.ObservableCollection<Model.RegistroModel> registros)
        {
            throw new NotImplementedException();
        }

        public void UpdateGDFRegistroSyncServer(Model.RegistroModel registro, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertGDFRegistroSyncServer(Model.RegistroModel registro, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public string GetJsonGDFRegistro(long? Last_Modified_Date, long? serverLastModifiedDate)
        {
            throw new NotImplementedException();
        }

        public void LoadSyncLocal(System.Collections.ObjectModel.ObservableCollection<Model.RegistroModel> registros)
        {
            throw new NotImplementedException();
        }

        public void UpdateGDFRegistroSyncLocal(Model.RegistroModel registro, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertGDFRegistroSyncLocal(Model.RegistroModel registro, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionaryGDFRegistro(string response)
        {
            throw new NotImplementedException();
        }

        public long LastModifiedDateLocal()
        {
            throw new NotImplementedException();
        }

        public long LastModifiedDateLocalServer()
        {
            throw new NotImplementedException();
        }

        public void ResetGDFRegistro(List<Model.ListUnidsModel> listUnids)
        {
            throw new NotImplementedException();
        }
    }
}
