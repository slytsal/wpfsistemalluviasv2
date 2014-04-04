using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface IDependencia
    {
        // Create.
        void InsertDependencia(DependenciaModel dependencia);

        // Read.
        IEnumerable<DependenciaModel> GetDependencias();
        DependenciaModel GetDependenciaID(DependenciaModel dependencia);
        DependenciaModel GetDependenciaADD(DependenciaModel dependencia);
        DependenciaModel GetDependenciaMOD(DependenciaModel dependencia);

        // Update.
        void UpdateDependencia(DependenciaModel dependencia);

        // Delete.
        void DeleteDependencia(IEnumerable<DependenciaModel> dependencias);

        //Sync subida de datos servidor
        string GetJsonDependencia();
        ObservableCollection<DependenciaModel> GetDeserializeDependencia(string listDependencia);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<DependenciaModel> dependencias);
        void UpdateDependenciaSyncServer(DependenciaModel dependencia, ObjectContext context);
        void InsertDependenciaSyncServer(DependenciaModel dependencia, ObjectContext context);

        //Sync desacarga de datos local
        string GetJsonDependencia(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<DependenciaModel> dependencias);
        void UpdateDependenciaSyncLocal(DependenciaModel dependencia, ObjectContext context);
        void InsertDependenciaSyncLocal(DependenciaModel dependencia, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryDependencia(string response);
        long LastModifiedDateLocal();
        void ResetDependencia(List<ListUnidsModel> listUnids);
    }
}
