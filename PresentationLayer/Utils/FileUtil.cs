using System;
using System.Collections.Generic;
using System.Text;

namespace PresentationLayer.Utils
{
    public class FileUtil
    {
        public static string SelectFile(String extension, String filter)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = extension,
                Filter = filter
            };
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
                return dlg.FileName;
            return "";
        }

        public static string SaveFile(String title, String filter)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                Title = title,
                Filter = filter
            };
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
                return dlg.FileName;
            return "";
        }
    }
}
