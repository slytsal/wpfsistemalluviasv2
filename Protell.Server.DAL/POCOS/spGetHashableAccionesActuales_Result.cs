using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Protell.Server.DAL.POCOS
{
   public partial class spGetHashableAccionesActuales_Result
    {
       public long IdTipoPuntoMedicion
       {
           get;
           set;
       }
       public long IdPuntoMedicion
       {
           get;
           set;
       }
       public Nullable<long> FechaNumerica
       {
           get;
           set;
       }
       public string AccionActual
       {
           get;
           set;
       }
    }
}
