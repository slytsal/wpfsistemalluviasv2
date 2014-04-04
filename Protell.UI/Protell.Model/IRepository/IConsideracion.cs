using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface IConsideracion
    {
        // Create.
        void InsertConsideracion(IEnumerable<ConsideracionModel> consideracions, CondProModel CondPro);

        // Read.
        IEnumerable<ConsideracionModel> GetConsideracions(CondProModel CondPro);

        // Delete.
        void DeleteConsideracion(IEnumerable<ConsideracionModel> consideracions);

        //Sync subida de datos servidor
        string GetJsonAccionProtocolo();
        ObservableCollection<ConsideracionModel> GetDeserializeConsideracion(string listAccionProtocolo);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<ConsideracionModel> accionProtocolo);
        void UpdateConsideracionSyncServer(ConsideracionModel accionProtocolo, ObjectContext context);
        void InsertConsideracionSyncServer(ConsideracionModel accionProtocolo, ObjectContext context);
        //Sync descarga de datos local
        string GetJsonConsideracion(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<ConsideracionModel> accionProtocolo);
        void UpdateConsideracionSyncLocal(ConsideracionModel accionProtocolo, ObjectContext context);
        void InsertConsideracionSyncLocal(ConsideracionModel accionProtocolo, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryConsideracion(string response);
        long LastModifiedDateLocal();
        void ResetConsideracion(List<ListUnidsModel> listUnids);

        string GetJsonConsideracion();
    }
}
