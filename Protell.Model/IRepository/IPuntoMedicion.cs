using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface IPuntoMedicion
    {
        // Create.
        void InsertPuntoMedicion(PuntoMedicionModel puntomedicion);

        // Read.
        IEnumerable<PuntoMedicionModel> GetPuntoMedicions();
        PuntoMedicionModel GetPuntoMedicionID(PuntoMedicionModel puntomedicion);
        PuntoMedicionModel GetPuntoMedicionADD(PuntoMedicionModel puntomedicion);
        PuntoMedicionModel GetPuntoMedicionMOD(PuntoMedicionModel puntomedicion);

        //Puntos Medición
        IEnumerable<Model.PuntoMedicionModel> GetPuntosMedicion();
        IEnumerable<Model.PuntoMedicionModel> GetLumbreras();
        IEnumerable<Model.PuntoMedicionModel> GetEstPluviograficas();

        // Update.
        void UpdatePuntoMedicion(PuntoMedicionModel puntomedicion);

        // Delete.
        void DeletePuntoMedicion(IEnumerable<PuntoMedicionModel> puntomedicions);

        //Sync subida de datos servidor
        string GetJsonPuntoMedicion();
        ObservableCollection<PuntoMedicionModel> GetDeserializePuntoMedicion(string listPuntoMedicion);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<PuntoMedicionModel> puntoMedicion);
        void UpdatePuntoMedicionSyncServer(PuntoMedicionModel puntoMedicion, ObjectContext context);
        void InsertPuntoMedicionSyncServer(PuntoMedicionModel puntoMedicion, ObjectContext context);

        //Sync descarga de datos local
        string GetJsonPuntoMedicion(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<PuntoMedicionModel> puntoMedicion);
        void UpdatePuntoMedicionSyncLocal(PuntoMedicionModel puntoMedicion, ObjectContext context);
        void InsertPuntoMedicionSyncLocal(PuntoMedicionModel puntoMedicion, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryPuntoMedicion(string response);
        long LastModifiedDateLocal();
        void ResetPuntoMedicion(List<ListUnidsModel> listUnids);
    }
}
