using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model.IRepository
{
    public interface ISync
    {
        bool SyncDummy();
        void ResetSyncDummy();
    }
}
