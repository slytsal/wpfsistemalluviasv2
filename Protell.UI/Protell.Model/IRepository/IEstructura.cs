using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface IEstructura
    {
        // Create.
        void InsertEstructura(EstructuraModel estructura);

        // Read.
        IEnumerable<EstructuraModel> GetEstructuras();
        EstructuraModel GetEstructuraID(EstructuraModel estructura);
        EstructuraModel GetEstructuraADD(EstructuraModel estructura);
        EstructuraModel GetEstructuraMOD(EstructuraModel estructura);

        // Update.
        void UpdateEstructura(EstructuraModel estructura);

        // Delete.
        void DeleteEstructura(IEnumerable<EstructuraModel> estructuras);

        //Sync subida de datos servidor
        string GetJsonEstructura();
        ObservableCollection<EstructuraModel> GetDeserializeEstructura(string listEstructura);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<EstructuraModel> estructuras);
        void UpdateEstructuraSyncServer(EstructuraModel estructura, ObjectContext context);
        void InsertEstructuraSyncServer(EstructuraModel estructura, ObjectContext context);

        //Sync desacarga de datos local
        string GetJsonEstructura(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<EstructuraModel> estructuras);
        void UpdateEstructuraSyncLocal(EstructuraModel estructura, ObjectContext context);
        void InsertEstructuraSyncLocal(EstructuraModel estructura, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryEstructura(string response);
        long LastModifiedDateLocal();
        void ResetEstructura(List<ListUnidsModel> listUnids);
    }
}
