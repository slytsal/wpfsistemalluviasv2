using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.ViewModel;
using System.Windows;

namespace Protell.UI
{
    public class Confirmation : IConfirmation
    {
        public string Msg
        {
            get
            {
                return msg;
            }
            set
            {
                msg = value;
            }
        }
        private string msg;

        public bool Response
        {
            get
            {
                return response;
            }
            set
            {
                response = value;
            }
        }
        private bool response;

        public void Show()
        {
            MessageBoxResult result = MessageBox.Show(Msg, "Validar Medición", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                response = true;
            }
            else
            {
                response = false;
            }
        }


        public void ShowOk()
        {
            MessageBox.Show(Msg, "Validar Fecha", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
