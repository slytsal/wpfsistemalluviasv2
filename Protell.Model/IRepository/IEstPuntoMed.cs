using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface IEstPuntoMed
    {
        // Create.
        void InsertEstPuntoMed(EstPuntoMedModel estpuntomed);
        void InsertEstPuntoMeds(IEnumerable<EstructuraModel> observableCollection, PuntoMedicionModel puntoMedicionModel);

        // Read.
        IEnumerable<EstPuntoMedModel> GetEstPuntoMeds();
        IEnumerable<EstPuntoMedModel> GetEstPuntoMedID(PuntoMedicionModel puntoMedicionModel);
        EstPuntoMedModel GetEstPuntoMedADD(EstPuntoMedModel estpuntomed);
        EstPuntoMedModel GetEstPuntoMedMOD(EstPuntoMedModel estpuntomed);

        // Update.
        void UpdateEstPuntoMed(EstPuntoMedModel estpuntomed);

        // Delete.
        void DeleteEstPuntoMed(IEnumerable<EstPuntoMedModel> estpuntomeds);

        //Sync subida de datos servidor
        string GetJsonEstPuntoMed();
        ObservableCollection<EstPuntoMedModel> GetDeserializeEstPuntoMed(string listEstPuntoModel);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<EstPuntoMedModel> estpuntomed);
        void UpdateEstPuntoMedSyncServer(EstPuntoMedModel estpuntomed, ObjectContext context);
        void InsertEstPuntoMedSyncServer(EstPuntoMedModel estpuntomed, ObjectContext context);

        //Sync desacarga de datos local
        string GetJsonEstPuntoMed(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<EstPuntoMedModel> estpuntomed);
        void UpdateEstPuntoMedSyncLocal(EstPuntoMedModel estpuntomed, ObjectContext context);
        void InsertEstPuntoMedSyncLocal(EstPuntoMedModel estpuntomed, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryEstPuntoMed(string response);
        long LastModifiedDateLocal();
        void ResetEstPuntoMed(List<ListUnidsModel> listUnids);


    }
}
