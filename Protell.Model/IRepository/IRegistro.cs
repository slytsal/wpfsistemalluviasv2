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
        void InsertRegistro(RegistroModel condpro,UsuarioModel usuario);

        // Read.
        IEnumerable<RegistroModel> GetRegistros();
        IEnumerable<RegistroModel> GetByIdRegistros(RegistroModel registro);
        RegistroModel GetRegistroID(RegistroModel condpro);
        RegistroModel GetRegistroADD(RegistroModel condpro);
        RegistroModel GetRegistroMOD(RegistroModel condpro);

        // Update.
        void UpdateRegistro(RegistroModel condpro,UsuarioModel usuario);

        // Delete.
        void DeleteRegistro(IEnumerable<RegistroModel> condpros);

        //Sync subida de datos servidor
        string GetJsonRegistro();
        ObservableCollection<RegistroModel> GetDeserializeRegistro(string listRegistro);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<RegistroModel> registros);
        void UpdateRegistroSyncServer(RegistroModel registro, ObjectContext context);
        void InsertRegistroSyncServer(RegistroModel registro, ObjectContext context);

        //Sync descarga de datos local
        string GetJsonRegistro(long? Last_Modified_Date, long? serverLastModifiedDate);
        void LoadSyncLocal(ObservableCollection<RegistroModel> registros);
        void UpdateRegistroSyncLocal(RegistroModel registro, ObjectContext context);
        void InsertRegistroSyncLocal(RegistroModel registro, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryRegistro(string response);
        long LastModifiedDateLocal();
        long LastModifiedDateLocalServer();
        void ResetRegistro(List<ListUnidsModel> listUnids);
    }
}
