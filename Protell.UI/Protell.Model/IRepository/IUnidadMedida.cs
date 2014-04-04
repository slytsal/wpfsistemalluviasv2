using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface IUnidadMedida
    {
        // Create.
        void InsertUnidadMedida(UnidadMedidaModel unidadmedida);

        // Read.
        IEnumerable<UnidadMedidaModel> GetUnidadMedidas();
        UnidadMedidaModel GetUnidadMedidaID(UnidadMedidaModel unidadmedida);
        UnidadMedidaModel GetUnidadMedidaADD(UnidadMedidaModel unidadmedida);
        UnidadMedidaModel GetUnidadMedidaMOD(UnidadMedidaModel unidadmedida);

        // Update.
        void UpdateUnidadMedida(UnidadMedidaModel unidadmedida);

        // Delete.
        void DeleteUnidadMedida(IEnumerable<UnidadMedidaModel> unidadmedidas);

        //Sync subida de datos servidor
        string GetJsonUnidadMedida();
        ObservableCollection<UnidadMedidaModel> GetDeserializeUnidadMedida(string listUnidadMedida);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<UnidadMedidaModel> unidadMedida);
        void UpdateUnidadMedidaSyncServer(UnidadMedidaModel unidadMedida, ObjectContext context);
        void InsertUnidadMedidaSyncServer(UnidadMedidaModel unidadMedida, ObjectContext context);

        //Sync descarga de datos local
        string GetJsonUnidadMedida(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<UnidadMedidaModel> unidadMedida);
        void UpdateUnidadMedidaSyncLocal(UnidadMedidaModel unidadMedida, ObjectContext context);
        void InsertUnidadMedidaSyncLocal(UnidadMedidaModel unidadMedida, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryUnidadMedida(string response);
        long LastModifiedDateLocal();
        void ResetUnidadMedida(List<ListUnidsModel> listUnids);
    }
}
