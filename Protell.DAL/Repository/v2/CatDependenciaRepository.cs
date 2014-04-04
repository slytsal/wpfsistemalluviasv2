using Protell.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Protell.DAL.Repository.v2
{
    public class CatDependenciaRepository:IDisposable
    {
        public ObservableCollection<DependenciaModel> GetIsModified()
        {
            ObservableCollection<DependenciaModel> result = new ObservableCollection<DependenciaModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                    (from res in entity.CAT_DEPENDENCIA
                     where res.IsModified == true
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
    }
}
