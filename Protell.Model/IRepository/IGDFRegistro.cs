using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface IGDFRegistro
    {
        // Create.
        void InsertGDFRegistro(GDFRegistroModel registro);

        // Update.
        void UpdateGDFRegistro(RegistroModel registro);

        //Sync subida de datos servidor
        string GetJsonGDFRegistro();
        ObservableCollection<RegistroModel> GetDeserializeGDFRegistro(string listRegistro);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<RegistroModel> registros);
        void UpdateGDFRegistroSyncServer(RegistroModel registro, ObjectContext context);
        void InsertGDFRegistroSyncServer(RegistroModel registro, ObjectContext context);

        //Sync descarga de datos local
        string GetJsonGDFRegistro(long? Last_Modified_Date, long? serverLastModifiedDate);
        void LoadSyncLocal(ObservableCollection<RegistroModel> registros);
        void UpdateGDFRegistroSyncLocal(RegistroModel registro, ObjectContext context);
        void InsertGDFRegistroSyncLocal(RegistroModel registro, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryGDFRegistro(string response);
        long LastModifiedDateLocal();
        long LastModifiedDateLocalServer();
        void ResetGDFRegistro(List<ListUnidsModel> listUnids);

    }
}
