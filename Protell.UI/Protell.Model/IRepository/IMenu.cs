using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Objects;

namespace Protell.Model.IRepository
{
    public interface IMenu
    {
        IEnumerable<MenuModel> GetMenu();
        //Sync subida de datos servidor
        string GetJsonMenu();
        ObservableCollection<MenuModel> GetDeserializeMenu(string listMenu);
        List<ListUnidsModel> LoadSyncServer(ObservableCollection<MenuModel> menu);
        void UpdateMenuSyncServer(MenuModel menu, ObjectContext context);
        void InsertMenuSyncServer(MenuModel menu, ObjectContext context);
        //Sync descarga de datos local
        string GetJsonMenu(long? Last_Modified_Date);
        void LoadSyncLocal(ObservableCollection<MenuModel> menu);
        void UpdateMenuSyncLocal(MenuModel menu, ObjectContext context);
        void InsertMenuSyncLocal(MenuModel menu, ObjectContext context);
        Dictionary<string, string> GetResponseDictionaryMenu(string response);
        long LastModifiedDateLocal();
        void ResetMenu(List<ListUnidsModel> listUnids);
    }
}
