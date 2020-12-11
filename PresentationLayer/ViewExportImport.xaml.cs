using PresentationLayer.Forms;
using PresentationLayer.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for ViewMakeReport.xaml
    /// </summary>
    public partial class ViewExportImport : UserControl
    {
        public ViewExportImport()
        {
            InitializeComponent();

            new ImportForm(Input_Import_Path, Button_Import_File, Button_Import);
            new ExportForm(Input_Export_Path, Button_Export_File, Button_Export);
        }
    }
}
