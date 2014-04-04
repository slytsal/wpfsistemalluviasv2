using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.ViewModel
{
    public interface IConfirmation
    {
        string Msg
        {
            get;
            set;
        }
        
        bool Response
        {
            get;
            set;
        }

        void Show();

        void ShowOk();
    }
}
