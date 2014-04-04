using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Protell.Server.DAL.POCOS;

namespace Protell.Server.DAL.Repository
{
    public class GdDatosEstructurasRepository : IGdDatosEstructurasRepository
    {

        public IEnumerable<Model.GdDatosEstructurasModel> GetData()
        {
            ObservableCollection<Model.GdDatosEstructurasModel> oc=new ObservableCollection<Model.GdDatosEstructurasModel>();

            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                (from o in entity.SpGdGetDatosEstructuras2()
                           select o).ToList().ForEach(r =>
                           {
                               oc.Add(
                                   new Model.GdDatosEstructurasModel()
                                   {
                                       NombreColumna = r.NombreColumna
                                       ,
                                       NumeroColumna = (int)r.NumeroColumna
                                       ,
                                       NumeroFila = (int)r.RowNum
                                       ,
                                       Valor = r.Valor
                                   }
                            );
                });
            }

            return oc;
        }
    }
}
