using System;
using Protell.Model;

namespace Protell.DAL.Repository.v2
{
    public sealed class AppBitacoraRepository
    {
        private static readonly Lazy<AppBitacoraRepository> lazy = new Lazy<AppBitacoraRepository>(() => new AppBitacoraRepository());
        public static AppBitacoraRepository Instance { get { return lazy.Value; } }

        public static void Insert(AppBitacoraModel model)
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                entity.APP_BITACORA.AddObject(new APP_BITACORA()
                {
                    Fecha=model.Fecha,
                    Metodo=model.Metodo,
                    Mensaje=model.Mensaje
                });
                entity.SaveChanges();
            }
        }
    }

}
