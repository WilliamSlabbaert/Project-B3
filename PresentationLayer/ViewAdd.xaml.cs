﻿using BusinessLayer;
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
            new ComicstripAddForm(Input_StripName, Input_StripSerie, Input_StripNumber, Input_StripPublisher, Input_StripAuthors, Button_CreateStrip);
            new AuthorAddForm(Input_AuthorFirstname, Input_AuthorLastname, Button_CreateAuthor);
            new PublisherAddForm(Input_PublisherName, Button_CreatePublisher);
        }
    }
}
