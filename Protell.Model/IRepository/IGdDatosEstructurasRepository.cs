using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model.IRepository
{
    public interface IGdDatosEstructurasRepository
    {
        IEnumerable<GdDatosEstructurasModel> GetData();
    }
}
