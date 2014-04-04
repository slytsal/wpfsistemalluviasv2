using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model.IRepository
{
    public interface IAppUsuario
    {
        // Return all entities.
        IEnumerable<AppUsuarioModel> GetAppUsuarioAll();

        //  Find a single entity by ID.
        AppUsuarioModel GetAppUsuarioById(AppUsuarioModel usuario);

        // Set of CRUD methods.
        void InsertAppUsuario(AppUsuarioModel usuario);
        void InsertAppUsuarios(IEnumerable<AppUsuarioModel> usuarios);
        void UpdateAppUsuario(AppUsuarioModel usuario);
        void DeleteAppUsuario(IEnumerable<AppUsuarioModel> usuarios);
    }
}
