using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;

namespace Protell.DAL.Repository
{
    public class MenuRepository : IMenu
    {
        public IEnumerable<Model.MenuModel> GetMenu()
        {
            ObservableCollection<Model.MenuModel> Menu = new ObservableCollection<Model.MenuModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.APP_MENU
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         Menu.Add(new Model.MenuModel()
                         {
                             IdMenu = p.IdMenu,
                             MenuName = p.MenuName,
                             IsLeaf = p.IsLeaf,
                             IsActive = p.IsActive,
                             IsModified = p.IsModified,
                             LastModifiedDate = p.LastModifiedDate
                         });
                     });
                }
                catch (Exception)
                {
                    ;
                }
            }
            return Menu;
        }

        public string GetJsonMenu()
        {
            throw new NotImplementedException();
        }

        public System.Collections.ObjectModel.ObservableCollection<Model.MenuModel> GetDeserializeMenu(string listMenu)
        {
            throw new NotImplementedException();
        }

        public List<Model.ListUnidsModel> LoadSyncServer(System.Collections.ObjectModel.ObservableCollection<Model.MenuModel> menu)
        {
            throw new NotImplementedException();
        }

        public void UpdateMenuSyncServer(Model.MenuModel menu, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertMenuSyncServer(Model.MenuModel menu, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public string GetJsonMenu(long? Last_Modified_Date)
        {
            throw new NotImplementedException();
        }

        public void LoadSyncLocal(System.Collections.ObjectModel.ObservableCollection<Model.MenuModel> menu)
        {
            throw new NotImplementedException();
        }

        public void UpdateMenuSyncLocal(Model.MenuModel menu, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public void InsertMenuSyncLocal(Model.MenuModel menu, System.Data.Objects.ObjectContext context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetResponseDictionaryMenu(string response)
        {
            throw new NotImplementedException();
        }

        public long LastModifiedDateLocal()
        {
            throw new NotImplementedException();
        }

        public void ResetMenu(List<Model.ListUnidsModel> listUnids)
        {
            throw new NotImplementedException();
        }
    }
}
