using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model.IRepository
{
    public interface IAppRol
    {
        // Return all entities.
        IEnumerable<AppRolModel> GetAppRolAll();


        //  Find a single entity by ID.
        AppRolModel GetAppRolById(AppRolModel rol);
        AppRolModel GetAppRolADD(AppRolModel rol);
        AppRolModel GetAppRolMOD(AppRolModel rol);


        // Set of CRUD methods.
        void InsertAppRol(AppRolModel rol);
        void UpdateAppRol(AppRolModel rol);
        void DeleteAppRoles(IEnumerable<AppRolModel> roles);
    }
}
