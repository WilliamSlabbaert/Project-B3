using BusinessLayer;
using DataLayer;
using PresentationLayer.Forms;
using PresentationLayer.Grids;
using PresentationLayer.Utils;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ViewAddStrip.xaml
    /// </summary>
    public partial class ViewAdd : UserControl
    {
        public ViewAdd()
        {
            InitializeComponent();
            new ComicstripAddForm(Input_StripName, Box_StripSerie_Switcher, Input_StripSerie_Select, Input_StripSerie_New, Input_StripNumber, Input_StripPublisher, Input_StripAuthors, Button_CreateStrip);
            new AuthorAddForm(Input_AuthorFirstname, Input_AuthorLastname, Button_CreateAuthor);
            new PublisherAddForm(Input_PublisherName, Button_CreatePublisher);

            Box_StripSerie_Switcher.Click += Comicstrip_Serie_Switch;
        }

        private void Comicstrip_Serie_Switch(object sender, RoutedEventArgs e)
        {
            if (!((bool)Box_StripSerie_Switcher.IsChecked))
            {
                Box_StripSerie_Select.Visibility = Visibility.Visible;
                Box_StripSerie_New.Visibility = Visibility.Collapsed;
            }
            else
            {
                Box_StripSerie_Select.Visibility = Visibility.Collapsed;
                Box_StripSerie_New.Visibility = Visibility.Visible;
            }
 
        }
    }
}
