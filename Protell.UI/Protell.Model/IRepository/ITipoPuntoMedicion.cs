using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface ITipoPuntoMedicion
    {
        // Create.
        void InsertTipoPuntoMedicion(TipoPuntoMedicionModel tipopuntomedicion);

        // Read.
        IEnumerable<TipoPuntoMedicionModel> GetTipoPuntoMedicions();
        TipoPuntoMedicionModel GetTipoPuntoMedicionID(TipoPuntoMedicionModel tipopuntomedicion);
        TipoPuntoMedicionModel GetTipoPuntoMedicionADD(TipoPuntoMedicionModel tipopuntomedicion);
        TipoPuntoMedicionModel GetTipoPuntoMedicionMOD(TipoPuntoMedicionModel tipopuntomedicion);

        // Update.
        void UpdateTipoPuntoMedicion(TipoPuntoMedicionModel tipopuntomedicion);

        // Delete.
        void DeleteTipoPuntoMedicion(IEnumerable<TipoPuntoMedicionModel> tipopuntomedicions);

        //Sync subida de datos servidor
        string GetJsonTipoPuntoMedicion();
        ObservableCollection<TipoPuntoMedicionModel> GetDeserializeTipoPuntoMedicion(string listTipoPuntoMedicion);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<TipoPuntoMedicionModel> tipoPuntoMedicion);
        void UpdateTipoPuntoMedicionSyncServer(TipoPuntoMedicionModel tipoPuntoMedicion, ObjectContext context);
        void InsertTipoPuntoMedicionSyncServer(TipoPuntoMedicionModel tipoPuntoMedicion, ObjectContext context);

        //Sync descarga de datos local
        string GetJsonTipoPuntoMedicion(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<TipoPuntoMedicionModel> tipoPuntoMedicion);
        void UpdateTipoPuntoMedicionSyncLocal(TipoPuntoMedicionModel tipoPuntoMedicion, ObjectContext context);
        void InsertTipoPuntoMedicionSyncLocal(TipoPuntoMedicionModel tipoPuntoMedicion, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryTipoPuntoMedicion(string response);
        long LastModifiedDateLocal();
        void ResetTipoPuntoMedicion(List<ListUnidsModel> listUnids);
    }
}
