using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;

namespace Protell.DAL.Repository
{
    public class AppUsuarioRepository : IAppUsuario
    {
        public IEnumerable<Model.AppUsuarioModel> GetAppUsuarioAll()
        {
            throw new NotImplementedException();
        }

        public Model.AppUsuarioModel GetAppUsuarioById(Model.AppUsuarioModel usuario)
        {
            throw new NotImplementedException();
        }

        public void InsertAppUsuario(Model.AppUsuarioModel usuario)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (usuario != null)
                {
                    //Validar si el elemento ya existe
                    APP_USUARIO result = null;
                    try
                    {
                        result = (from o in entity.APP_USUARIO
                                  where o.IdUsuario == usuario.IdUsuario
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        entity.APP_USUARIO.AddObject(
                            new APP_USUARIO()
                            {
                                IdUsuario = usuario.IdUsuario,
                                Nombre = usuario.Nombre,
                                IsActive = usuario.IsActive
                            }
                        );

                        entity.SaveChanges();
                    }
                }
            }
        }

        public void InsertAppUsuarios(IEnumerable<Model.AppUsuarioModel> usuarios)
        {
            throw new NotImplementedException();
        }

        public void UpdateAppUsuario(Model.AppUsuarioModel usuario)
        {
            throw new NotImplementedException();
        }

        public void DeleteAppUsuario(IEnumerable<Model.AppUsuarioModel> usuarios)
        {
            throw new NotImplementedException();
        }
    }
}
