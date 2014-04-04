using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface IEstructuraDependencia
    {
        // Create.
        void InsertEstructuraDependencia(EstructuraDependenciaModel estructuradependencia);

        // Read.
        IEnumerable<EstructuraDependenciaModel> GetEstructuraDependencias();
        EstructuraDependenciaModel GetEstructuraDependenciaID(EstructuraDependenciaModel estructuradependencia);
        EstructuraDependenciaModel GetEstructuraDependenciaADD(EstructuraDependenciaModel estructuradependencia);
        EstructuraDependenciaModel GetEstructuraDependenciaMOD(EstructuraDependenciaModel estructuradependencia);

        // Update.
        void UpdateEstructuraDependencia(EstructuraDependenciaModel estructuradependencia);

        // Delete.
        void DeleteEstructuraDependencia(IEnumerable<EstructuraDependenciaModel> estructuradependencias);


        //Sync subida de datos servidor
        string GetJsonEstructuraDependencia();
        ObservableCollection<EstructuraDependenciaModel> GetDeserializeEstructuraDependencia(string listEstructuraDependencia);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<EstructuraDependenciaModel> accionProtocolo);
        void UpdateEstructuraDependenciasSyncServer(EstructuraDependenciaModel estructuraDependencia, ObjectContext context);
        void InsertEstructuraDependenciasSyncServer(EstructuraDependenciaModel estructuraDependencia, ObjectContext context);
        //Sync descarga de datos local
        string GetJsonEstructuraDependencia(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<EstructuraDependenciaModel> estructuraDependencia);
        void UpdateEstructuraDependenciaSyncLocal(EstructuraDependenciaModel estructuraDependencia, ObjectContext context);
        void InsertEstructuraDependenciaSyncLocal(EstructuraDependenciaModel estructuraDependencia, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryEstructuraDependencia(string response);
        long LastModifiedDateLocal();
        void ResetEstructuraDependencia(List<ListUnidsModel> listUnids);



    }
}
