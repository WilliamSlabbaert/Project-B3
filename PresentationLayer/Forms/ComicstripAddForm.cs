using BusinessLayer;
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
    public class ComicstripAddForm
    {
        private CheckBox SerieSwitcher;

        private TextBox TitleInput;
        private ComboBox SerieInputSelect;
        private TextBox SerieInputNew;
        private TextBox NumberInput;
        private ComboBox PublisherInput;
        private AuthorGrid AuthorsInput;
        private Button SubmitButton;

        private List<Publisher> publishers = new List<Publisher>();
        private List<ComicstripSerie> series = new List<ComicstripSerie>();

        public ComicstripAddForm(TextBox title, CheckBox serieSwitcher, ComboBox serieSelect, TextBox serieNew, TextBox number, ComboBox publisher, DataGrid authors, Button submit)
        {
            this.TitleInput = title;
            this.TitleInput.TextChanged += InputChanged;
            this.SerieInputSelect = serieSelect;
            this.SerieInputSelect.SelectionChanged += InputChanged;
            this.SerieInputNew = serieNew;
            this.SerieInputNew.TextChanged += InputChanged;
            this.SerieSwitcher = serieSwitcher;
            this.SerieSwitcher.Click += InputChanged;
            this.NumberInput = number;
            this.NumberInput.TextChanged += InputChanged;
            this.PublisherInput = publisher;
            this.PublisherInput.SelectionChanged += InputChanged;
            AuthorManager am = new AuthorManager(new UnitOfWork());
            this.AuthorsInput = new AuthorGrid(authors, am.GetAll());
            this.AuthorsInput.Grid.SelectionChanged += InputChanged;
            this.SubmitButton = submit;
            this.SubmitButton.Click += Submit;

            ComicStripManager cm = new ComicStripManager(new UnitOfWork());
            this.series = cm.GetAllSeries();
            foreach (ComicstripSerie serie in this.series)
                this.SerieInputSelect.Items.Add(serie.Name);
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
                ComicStripManager sm = new ComicStripManager(new UnitOfWork());
                ComicstripSerie cs = null;
                if (!((bool) this.SerieSwitcher.IsChecked))
                    cs = this.series[this.SerieInputSelect.SelectedIndex];
                else
                    cs = new ComicstripSerie(this.SerieInputNew.Text);
                sm.Add(new ComicStrip(this.TitleInput.Text, cs, int.Parse(this.NumberInput.Text), this.AuthorsInput.GetSelected(), p));
                MessageUtil.ShowAsyncMessage("Comicstrip has been added");
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
            if (!((bool) this.SerieSwitcher.IsChecked) && this.SerieInputSelect.SelectedIndex < 0) valid = false;
            if ((bool)this.SerieSwitcher.IsChecked && string.IsNullOrWhiteSpace(this.SerieInputNew.Text)) valid = false;
            if (!string.IsNullOrEmpty(this.NumberInput.Text) && !int.TryParse(this.NumberInput.Text,out int i)) valid = false;
            if (this.PublisherInput.SelectedIndex < 0) valid = false;
            if (this.AuthorsInput.GetSelected().Count <= 0) valid = false;
            this.SubmitButton.IsEnabled = valid;
        }

        private void Reset()
        {
            this.TitleInput.Text = "";
            this.SerieInputSelect.SelectedIndex = -1;
            this.SerieInputNew.Text = "";
            this.NumberInput.Text = "";
            this.PublisherInput.SelectedIndex = -1;
            this.AuthorsInput.Grid.SelectedItems.Clear();
            this.SubmitButton.IsEnabled = false;
        }
    }
}
