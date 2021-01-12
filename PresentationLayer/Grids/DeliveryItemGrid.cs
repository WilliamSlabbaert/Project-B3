using BusinessLayer.Models;
using PresentationLayer.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Grids
{
    public class DeliveryItemGrid
    {
        #region Attributres
        public List<DeliveryItem> Items { get; private set; } = new List<DeliveryItem>();
        public DataTable Table { get; private set; } = BuildTable();
        public DataGrid Grid { get; private set; }

        private List<Button> DeleteButtons = new List<Button>();
        private List<Button> EditButtons = new List<Button>();
        #endregion

        public DeliveryItemGrid(DataGrid grid)
        {
            this.Grid = grid;
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        public DeliveryItemGrid(DataGrid grid, List<DeliveryItem> items)
        {
            this.Grid = grid;
            AddItems(items);
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        #region Table
        public void AddItem(DeliveryItem item)
        {
            this.Items.Add(item);
            AddRow(item);
        }

        public void AddItems(List<DeliveryItem> items)
        {
            this.Items.AddRange(items);
            foreach (DeliveryItem item in items)
                AddRow(item);
        }
        private void AddRow(DeliveryItem item)
        {
            DataRow row = this.Table.NewRow();
            row[0] = item.Comicstrip.ID;
            row[1] = item.Comicstrip.Titel;
            row[2] = item.Quantity;
            this.Table.Rows.Add(row);
        }

        private static DataTable BuildTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Strip ID", typeof(int)));
            table.Columns.Add(new DataColumn("Strip Title", typeof(string)));
            table.Columns.Add(new DataColumn("Quantity", typeof(string)));
            return table;
        }
        #endregion

        #region Selection
        public List<DeliveryItem> GetSelected()
        {
            List<DeliveryItem> items = new List<DeliveryItem>();
            List<int> gridIndexes = this.Grid.SelectedItems.Cast<DataRowView>().Select(x => this.Table.Rows.IndexOf(x.Row)).ToList();
            foreach (int i in gridIndexes)
            {
                int index = (int)this.Table.Rows[i][0];
                items.Add(this.Items.Where(x => x.ID == index).Single());
            }
            return items;
        }
        private void GridselectionChanged(object sender, RoutedEventArgs e)
        {
            List<DeliveryItem> selected = this.GetSelected();
            foreach (Button b in this.DeleteButtons)
                b.IsEnabled = (selected.Count > 0);
            foreach (Button b in this.EditButtons)
                b.IsEnabled = (selected.Count == 1);
        }
        #endregion

        #region Controls
        public void SetDeleteButton(Button button)
        {
            this.DeleteButtons.Add(button);
            button.Click += DeleteButtonEvent;
        }
        private void DeleteButtonEvent(object sender, RoutedEventArgs e)
        {

            List<DeliveryItem> selected = this.GetSelected();
            if (MessageUtil.ShowYesNoMessage("Delete (" + selected.Count + ") Delivery " + ((selected.Count > 1) ? "Items" : "Item"), "You won't be able to revert!"))
            {
                int succeeded = 0;
                // Not required
                MessageUtil.ShowMessage("Deleted (" + succeeded + ") " + ((succeeded > 1) ? "Deliveries" : "Delivery") + " and (" + (selected.Count - succeeded) + ") failed!");
            }
        }

        public void SetEditButton(Button button)
        {
            this.EditButtons.Add(button);
            button.Click += EditButtonEvent;
        }
        private void EditButtonEvent(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
