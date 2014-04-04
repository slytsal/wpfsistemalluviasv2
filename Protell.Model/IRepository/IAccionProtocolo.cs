using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface IAccionProtocolo
    {
        // Create.
        void InsertAccionProtocolo(AccionProtocoloModel accionprotocolo);

        // Read.
        IEnumerable<AccionProtocoloModel> GetAccionProtocolos();
        AccionProtocoloModel GetAccionProtocoloID(AccionProtocoloModel accionprotocolo);
        AccionProtocoloModel GetAccionProtocoloADD(AccionProtocoloModel accionprotocolo);
        AccionProtocoloModel GetAccionProtocoloMOD(AccionProtocoloModel accionprotocolo);

        // Update.
        void UpdateAccionProtocolo(AccionProtocoloModel accionprotocolo);

        // Delete.
        void DeleteAccionProtocolo(IEnumerable<AccionProtocoloModel> accionprotocolos);



        //Sync subida de datos servidor
        string GetJsonAccionProtocolo();
        ObservableCollection<AccionProtocoloModel> GetDeserializeAccionProtocolo(string listAccionProtocolo);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<AccionProtocoloModel> accionProtocolo);
        void UpdateAccionProtocoloSyncServer(AccionProtocoloModel accionProtocolo, ObjectContext context);
        void InsertAccionProtocoloSyncServer(AccionProtocoloModel accionProtocolo, ObjectContext context);
        //Sync descarga de datos local
        string GetJsonAccionProtocolo(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<AccionProtocoloModel> accionProtocolo);
        void UpdateAccionProtocoloSyncLocal(AccionProtocoloModel accionProtocolo, ObjectContext context);
        void InsertAccionProtocoloSyncLocal(AccionProtocoloModel accionProtocolo, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryAccionProtocolo(string response);
        long LastModifiedDateLocal();
        void ResetAccionProtocolo(List<ListUnidsModel> listUnids);
    }
}
