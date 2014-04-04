using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface IRegistro
    {
        // Create.
        void InsertRegistro(RegistroModel condpro);

        // Read.
        IEnumerable<RegistroModel> GetRegistros();
        RegistroModel GetRegistroID(RegistroModel registro);
        RegistroModel GetRegistroADD(RegistroModel registro);
        RegistroModel GetRegistroMOD(RegistroModel registro);

        // Update.
        void UpdateRegistro(RegistroModel registro);

        // Delete.
        void DeleteRegistro(IEnumerable<RegistroModel> registro);

        //Sync subida de datos servidor
        string GetJsonRegistro();
        ObservableCollection<RegistroModel> GetDeserializeRegistro(string listRegistro);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<RegistroModel> registros);
        void UpdateRegistroSyncServer(RegistroModel registro, ObjectContext context);
        void InsertRegistroSyncServer(RegistroModel registro, ObjectContext context);

        //Sync descarga de datos local
        string GetJsonRegistro(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<RegistroModel> registros);
        void UpdateRegistroSyncLocal(RegistroModel registro, ObjectContext context);
        void InsertRegistroSyncLocal(RegistroModel registro, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryRegistro(string response);
        long LastModifiedDateLocal();
        void ResetRegistro(List<ListUnidsModel> listUnids);
    }
}
