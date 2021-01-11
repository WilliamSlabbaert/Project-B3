using BusinessLayer;
using BusinessLayer.DomainManagers;
using BusinessLayer.Models;
using DataLayer;
using PresentationLayer.Grids;
using PresentationLayer.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public class ComicstripBundleAddForm
    {
        private TextBox TitleInput;
        private ComboBox PublisherInput;
        private StripGrid ComicstripsInput;
        private Button SubmitButton;

        private List<Publisher> publishers = new List<Publisher>();

        public ComicstripBundleAddForm(TextBox title, ComboBox publisher, DataGrid comicstrips, Button submit)
        {
            this.TitleInput = title;
            this.TitleInput.TextChanged += InputChanged;
            this.PublisherInput = publisher;
            this.PublisherInput.SelectionChanged += InputChanged;
            ComicStripManager sm = new ComicStripManager(new UnitOfWork());
            this.ComicstripsInput = new StripGrid(comicstrips, sm.GetAll());
            this.ComicstripsInput.Grid.SelectionChanged += InputChanged;
            this.SubmitButton = submit;
            this.SubmitButton.Click += Submit;

            PublisherManager pm = new PublisherManager(new UnitOfWork());
            this.publishers = pm.GetAll();
            foreach (Publisher p in this.publishers)
                this.PublisherInput.Items.Add(p.Name);
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            try
            {
                Publisher p = this.publishers[this.PublisherInput.SelectedIndex];
                ComicstripBundleManager bm = new ComicstripBundleManager(new UnitOfWork());
                bm.Add(new ComicstripBundle(this.TitleInput.Text, this.ComicstripsInput.GetSelected(), p));
                MessageUtil.ShowAsyncMessage("Comicstrip Bundle has been added");
                Reset();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowAsyncMessage(ex.Message);
            }
        }

        private void InputChanged(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            if (string.IsNullOrWhiteSpace(this.TitleInput.Text)) valid = false;
            if (this.PublisherInput.SelectedIndex < 0) valid = false;
            if (this.ComicstripsInput.GetSelected().Count <= 0) valid = false;
            this.SubmitButton.IsEnabled = valid;
        }

        private void Reset()
        {
            this.TitleInput.Text = "";
            this.PublisherInput.SelectedIndex = -1;
            this.ComicstripsInput.Grid.SelectedItems.Clear();
            this.SubmitButton.IsEnabled = false;
        }
    }
}
