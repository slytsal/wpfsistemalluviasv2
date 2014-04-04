using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.ViewModel.Sync
{
    public static class ContSettings 
    {
        public static int ContadorSettings 
        {
            get 
            {
                return _ContadorSettings;
            }
            set
            {
                if (_ContadorSettings != value)
                {
                    _ContadorSettings = value;
                    
                }
            }
        }
        private static int _ContadorSettings =0;

    }
}
