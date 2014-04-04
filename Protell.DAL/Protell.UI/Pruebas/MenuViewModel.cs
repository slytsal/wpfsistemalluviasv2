using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.UI.Pruebas
{
    public class MenuViewModel
    {
        public List<MenuModel> GetDataSubMenu()
        {
            List<MenuModel> listSubMenu = new List<MenuModel>();
            listSubMenu.Add(new MenuModel() { ID = 11, Name = "SISTEMA" });
            listSubMenu.Add(new MenuModel() { ID = 22, Name = "DEPENDENCIA" });
            listSubMenu.Add(new MenuModel() { ID = 33, Name = "CONSIDERACION" });
            if (listSubMenu.Count > 0)
            {
                return listSubMenu;
            }
            else
                return null;
        }
    }
}
