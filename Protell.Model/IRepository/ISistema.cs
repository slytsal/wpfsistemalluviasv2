using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface ISistema
    {
        // Create.
        void InsertSistema(SistemaModel sistema);

        // Read.
        IEnumerable<SistemaModel> GetSistemas();
        SistemaModel GetSistemaID(SistemaModel sistema);
        SistemaModel GetSistemaADD(SistemaModel sistema);
        SistemaModel GetSistemaMOD(SistemaModel sistema);

        // Update.
        void UpdateSistema(SistemaModel sistema);

        // Delete.
        void DeleteSistema(IEnumerable<SistemaModel> sistemas);

        //Sync subida de datos servidor
        string GetJsonSistema();
        ObservableCollection<SistemaModel> GetDeserializeSistemas(string listSistema);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<SistemaModel> sistemas);
        void UpdateSistemaSyncServer(SistemaModel sistema,ObjectContext context);
        void InsertSistemaSyncServer(SistemaModel sistema, ObjectContext context);
        //Sync descarga de datos local
        string GetJsonSistema(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<SistemaModel> sistemas);
        void UpdateSistemaSyncLocal(SistemaModel sistema, ObjectContext context);
        void InsertSistemaSyncLocal(SistemaModel sistema, ObjectContext context);
        Dictionary<string, string> GetResponseDictionarySistema(string response);
        long LastModifiedDateLocal();
        void ResetSistema(List<ListUnidsModel> listUnids);

    }
}
