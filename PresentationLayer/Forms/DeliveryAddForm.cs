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
    public class DeliveryAddForm
    {
        private TextBox SupplierInput;
        private ComboBox ItemInput;
        private TextBox QuantityInput;
        private Button AddButton;
        private Button SubmitButton;

        private DeliveryItemGrid ItemGrid;

        private List<DeliveryItem> items = new List<DeliveryItem>();
        private List<ComicStrip> comicstrips = new List<ComicStrip>();

        public DeliveryAddForm(TextBox supplier, ComboBox item, TextBox quantity, DataGrid items, Button add, Button submit)
        {
            this.SupplierInput = supplier;
            this.SupplierInput.TextChanged += InputChanged;

            this.ItemInput = item;
            this.ItemInput.SelectionChanged += AddChanged;
            this.QuantityInput = quantity;
            this.QuantityInput.TextChanged += AddChanged;
            this.AddButton = add;
            this.AddButton.Click += AddItem;

            this.SubmitButton = submit;
            this.SubmitButton.Click += Submit;

            this.ItemGrid = new DeliveryItemGrid(items);
            Init();
        }

        private void Init()
        {
            this.items.Clear();
            this.ItemInput.Items.Clear();
            ComicStripManager sm = new ComicStripManager(new UnitOfWork());
            this.comicstrips = sm.GetAll();
            foreach (ComicStrip s in this.comicstrips)
                this.ItemInput.Items.Add("#" + s.ID + " - " + s.Titel);
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            try
            {
                DeliveryManager dm = new DeliveryManager(new UnitOfWork());
                dm.Add(new Delivery(this.SupplierInput.Text, DateTime.Now, this.items));
                MessageUtil.ShowAsyncMessage("Delivery has been added");
                Reset();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowAsyncMessage(ex.Message);
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = this.ItemInput.SelectedIndex;
                ComicStrip s = this.comicstrips[index];
                DeliveryItem di = new DeliveryItem(s, int.Parse(this.QuantityInput.Text));
                this.items.Add(di);
                this.ItemGrid.AddItem(di);
                this.ItemInput.SelectedIndex = -1;
                this.ItemInput.Items.RemoveAt(index);
                InputChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageUtil.ShowAsyncMessage(ex.Message);
            }
        }

        private void AddChanged(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            if (this.ItemInput.SelectedIndex < 0) valid = false;
            if (string.IsNullOrEmpty(this.QuantityInput.Text) || !int.TryParse(this.QuantityInput.Text, out int i) || i == 0) valid = false;
            this.AddButton.IsEnabled = valid;
        }

        private void InputChanged(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            if (this.items.Count <= 0) valid = false;
            this.SubmitButton.IsEnabled = valid;
        }

        private void Reset()
        {
            this.SupplierInput.Text = "";
            this.ItemInput.SelectedIndex = 0;
            this.QuantityInput.Text = "";
            this.items.Clear();
            // this.ItemGrid.Grid.Items.Clear();
            Init();
            this.SubmitButton.IsEnabled = false;
        }
    }
}
