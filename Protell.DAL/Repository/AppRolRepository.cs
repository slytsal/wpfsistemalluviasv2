using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;


namespace Protell.DAL.Repository
{
    public class AppRolRepository : IAppRol
    {
        public IEnumerable<Model.AppRolModel> GetAppRolAll()
        {
            ObservableCollection<Model.AppRolModel> Roles = new ObservableCollection<Model.AppRolModel>();
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    (from o in entity.APP_ROL
                     where o.IsActive == true
                     select o).ToList().ForEach(p =>
                     {
                         Roles.Add(new Model.AppRolModel()
                         {
                             IdRol = p.IdRol,
                             RolName = p.RolName,
                             IsActive = p.IsActive
                         });
                     });
                }
                catch (Exception)
                {
                    ;
                }
            }
            return Roles;
        }

        public Model.AppRolModel GetAppRolById(Model.AppRolModel rol)
        {
            throw new NotImplementedException();
        }

        public Model.AppRolModel GetAppRolADD(Model.AppRolModel rol)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (rol != null)
                {
                    //Validar si el elemento ya existe
                    APP_ROL result = null;
                    try
                    {
                        result = (from o in entity.APP_ROL
                                  where
                                  o.IdRol == rol.IdRol && o.IsActive == true ||
                                  o.RolName == rol.RolName && o.IsActive == true
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        rol = null;
                    }

                }
            }
            return rol;
        }

        public Model.AppRolModel GetAppRolMOD(Model.AppRolModel rol)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (rol != null)
                {
                    //Validar si el elemento ya existe
                    APP_ROL result = null;
                    try
                    {
                        result = (from o in entity.APP_ROL
                                  where
                                  o.RolName == rol.RolName
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        rol = null;
                    }

                }
            }
            return rol;
        }

        public void InsertAppRol(Model.AppRolModel rol)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                if (rol != null)
                {
                    //Validar si el elemento ya existe
                    APP_ROL result = null;
                    try
                    {
                        result = (from o in entity.APP_ROL
                                  where o.IdRol == rol.IdRol
                                  select o).First();
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (result == null)
                    {
                        entity.APP_ROL.AddObject(
                            new APP_ROL()
                            {
                                IdRol = rol.IdRol,
                                RolName = rol.RolName.Trim(),
                                IsActive = rol.IsActive
                            }
                        );

                        entity.INF_INFODOC.AddObject(
                            new INF_INFODOC()
                            {
                                IdInfoDoc = new UNID().getNewUNID(),
                                IdRol = 2,
                                IdUsuario = 3,
                                IdForm = 4,
                                IdAccion = 1,
                                Fecha = 6,
                                IdRef = 7,
                                IpAddress = "a",
                                MacAdress = "a",
                                PcName = "a",
                                IsActive = true,
                                IsModified = false,
                                LastModifiedDate = 1
                            }
                            );

                        entity.SaveChanges();
                    }

                }
            }
        }

        public void UpdateAppRol(Model.AppRolModel rol)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                APP_ROL result = null;
                try
                {
                    result = (from o in entity.APP_ROL
                              where o.IdRol == rol.IdRol
                              select o).First();
                }
                catch (Exception ex)
                {
                    ;
                }

                if (result != null)
                {
                    result.IdRol = rol.IdRol;
                    result.RolName = rol.RolName;
                    entity.SaveChanges();
                }
            }
        }

        public void DeleteAppRoles(IEnumerable<Model.AppRolModel> roles)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                foreach (Model.AppRolModel p in roles)
                {
                    try
                    {
                        var res = (from o in entity.APP_ROL
                                   where o.IdRol == p.IdRol
                                   select o).First().IsActive = false;
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
                
                entity.INF_INFODOC.AddObject(
                    new INF_INFODOC()
                    {
                        IdInfoDoc = new UNID().getNewUNID(),
                        IdRol = 2,
                        IdUsuario = 3,
                        IdForm = 4,
                        IdAccion = 2,
                        Fecha = 6,
                        IdRef = 7,
                        IpAddress = "a",
                        MacAdress = "a",
                        PcName = "a",
                        IsActive = true,
                        IsModified = false,
                        LastModifiedDate = 1
                    }
                    );
                entity.SaveChanges();
            }
        }


    }
}
