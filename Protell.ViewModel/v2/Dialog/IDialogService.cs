using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Protell.ViewModel.v2.Dialog
{
    interface IDialogService
    {
        void Show(string messageText);
        void Show(string messageText, string caption, MessageBoxButton button);
    }
}
