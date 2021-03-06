﻿using Protell.Model;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.Server.DAL.Repository.v2
{
    public class CatDependenciaRepository : IDisposable
    {

        public ObservableCollection<DependenciaModel> GetCatDependencia(long LastModifiedDate, long ServerLastModifiedDate)
        {
            ObservableCollection<DependenciaModel> result = new ObservableCollection<DependenciaModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from res in entity.CAT_DEPENDENCIA
                     where res.LastModifiedDate >= LastModifiedDate || res.ServerLastModifiedDate >= ServerLastModifiedDate
                     select res).ToList().ForEach(row =>
                     {
                         result.Add(new DependenciaModel()
                         {
                             IdDependencia = row.IdDependencia,
                             DependenciaName = row.DependenciaName,
                             IsActive = row.IsActive,
                             IsModified = row.IsModified,
                             LastModifiedDate = row.LastModifiedDate,
                             ServerLastModifiedDate = row.ServerLastModifiedDate
                         });
                     });
                }
            }
            catch (Exception)
            {


            }
            return result;
        }

        public void Dispose()
        {
            return;
        }

    }
}
