using System.Windows;

namespace Protell.ViewModel.v2
{
    public static class DialogService 
    {

        public static void Show(string messageText)
        {
            MessageBox.Show(messageText);
        }

        public static MessageBoxResult ShowResult(string messageText, string caption)
        {
            return MessageBox.Show(messageText, caption, MessageBoxButton.OKCancel);
        }
    }
}
