﻿using Protell.DAL.Factory;
using Protell.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Protell.DAL.Repository.v2
{
    public class CatUnidadMedidaRepository : IDisposable, IServiceFactory
    {
        public ObservableCollection<UnidadMedidaModel> GetIsModified()
        {
            ObservableCollection<UnidadMedidaModel> result = new ObservableCollection<UnidadMedidaModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_UNIDAD_MEDIDA
                     where res.IsModified == true
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new UnidadMedidaModel()
                         {
                             IdUnidadMedida = row.IdUnidadMedida,
                             UnidadMedidaName = row.UnidadMedidaName,
                             UnidadMedidaShort = row.UnidadMedidaShort,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             ServerLastModifiedDate = row.ServerLastModifiedDate
                         });
                     });
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public void Dispose()
        {
            return;
        }

        public bool Download()
        {
            throw new NotImplementedException();
        }
    }
}
