using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface ICondPro
    {
        // Create.
        void InsertCondPro(CondProModel condpro);

        // Read.
        IEnumerable<CondProModel> GetCondPros();
        CondProModel GetCondProID(CondProModel condpro);
        CondProModel GetCondProADD(CondProModel condpro);
        CondProModel GetCondProMOD(CondProModel condpro);

        // Update.
        void UpdateCondPro(CondProModel condpro);

        // Delete.
        void DeleteCondPro(IEnumerable<CondProModel> condpros);

        //Sync subida de datos servidor
        string GetJsonCondPro();
        ObservableCollection<CondProModel> GetDeserializeCondPro(string listCondPro);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<CondProModel> condpro);
        void UpdateCondProSyncServer(CondProModel condpro, ObjectContext context);
        void InsertCondProSyncServer(CondProModel condpro, ObjectContext context);
        //Sync desacarga de datos local
        string GetJsonCondPro(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<CondProModel> condpro);
        void UpdateCondProSyncLocal(CondProModel condpro, ObjectContext context);
        void InsertCondProSyncLocal(CondProModel condpro, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryCondPro(string response);
        long LastModifiedDateLocal();
        void ResetCondPro(List<ListUnidsModel> listUnids);
    }
}
