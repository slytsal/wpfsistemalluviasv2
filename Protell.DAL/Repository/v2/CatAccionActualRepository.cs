using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Protell.Model;

namespace Protell.DAL.Repository.v2
{
    public class CatAccionActualRepository
    {
        public string GetAccionActual(long IdPuntoMedicion)
        {
            string res = null;
            try
            {
                using (var entity= new db_SeguimientoProtocolo_r2Entities())
                {
                    (from pm in entity.CAT_PUNTO_MEDICION
                     join ac in entity.CAT_ACCION_ACTUAL
                         on pm.IdAccionActual equals ac.IdAccionActual
                         where pm.IdPuntoMedicion == IdPuntoMedicion
                     select new { AccionActual = ac.AccionAcualName }).ToList().ForEach(row =>
                     {
                         res = row.AccionActual;
                     });
                }
                
            }
            catch (Exception ex)
            {
                AppBitacoraRepository.Insert(new AppBitacoraModel() { Fecha = DateTime.Now, Metodo = ex.StackTrace, Mensaje = ex.Message });
            }
            return res;

        }
    }
}
