using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.Utils
{
    public class MessageUtil
    {
        public static void ShowAsyncMessage(String message)
        {
            Task.Run(() =>
            {
                MessageBox.Show(message);
            });
        }

        public static void ShowMessage(String message)
        {
            MessageBox.Show(message);
        }

        public static bool ShowYesNoMessage(String title, String message)
        {
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
            return (result == MessageBoxResult.OK);
        }
    }
}
