using BusinessLayer;
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
        private TextBox TitleInput;
        private TextBox SerieInput;
        private TextBox NumberInput;
        private ComboBox PublisherInput;
        private AuthorGrid AuthorsInput;
        private Button SubmitButton;

        private List<Publisher> publishers = new List<Publisher>();

        public ComicstripAddForm(TextBox title, TextBox serie, TextBox number, ComboBox publisher, DataGrid authors, Button submit)
        {
            this.TitleInput = title;
            this.TitleInput.TextChanged += InputChanged;
            this.SerieInput = serie;
            this.SerieInput.TextChanged += InputChanged;
            this.NumberInput = number;
            this.NumberInput.TextChanged += InputChanged;
            this.PublisherInput = publisher;
            this.PublisherInput.SelectionChanged += InputChanged;
            AuthorManager am = new AuthorManager(new UnitOfWork());
            this.AuthorsInput = new AuthorGrid(authors, am.GetAll());
            this.AuthorsInput.Grid.SelectionChanged += InputChanged;
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
                ComicStripManager sm = new ComicStripManager(new UnitOfWork());
                sm.Add(new ComicStrip(this.TitleInput.Text, this.SerieInput.Text, int.Parse(this.NumberInput.Text), this.AuthorsInput.GetSelected(), p));
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
            if (string.IsNullOrWhiteSpace(this.SerieInput.Text)) valid = false;
            if (!string.IsNullOrEmpty(this.NumberInput.Text) && !int.TryParse(this.NumberInput.Text,out int i)) valid = false;
            if (this.PublisherInput.SelectedIndex < 0) valid = false;
            if (this.AuthorsInput.GetSelected().Count <= 0) valid = false;
            this.SubmitButton.IsEnabled = valid;
        }

        private void Reset()
        {
            this.TitleInput.Text = "";
            this.SerieInput.Text = "";
            this.NumberInput.Text = "";
            this.PublisherInput.SelectedIndex = -1;
            this.AuthorsInput.Grid.SelectedItems.Clear();
            this.SubmitButton.IsEnabled = false;
        }
    }
}
