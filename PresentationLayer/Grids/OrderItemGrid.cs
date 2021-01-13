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
    public class OrderItemGrid
    {
        #region Attributres
        public List<OrderItem> Items { get; private set; } = new List<OrderItem>();
        public DataTable Table { get; private set; } = BuildTable();
        public DataGrid Grid { get; private set; }

        readonly List<Button> DeleteButtons = new List<Button>();
        readonly List<Button> EditButtons = new List<Button>();
        #endregion

        public OrderItemGrid(DataGrid grid)
        {
            this.Grid = grid;
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        public OrderItemGrid(DataGrid grid, List<OrderItem> items)
        {
            this.Grid = grid;
            AddItems(items);
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        #region Table
        public void AddItem(OrderItem item)
        {
            this.Items.Add(item);
            AddRow(item);
        }

        public void AddItems(List<OrderItem> items)
        {
            this.Items.AddRange(items);
            foreach (OrderItem item in items)
                AddRow(item);
        }
        private void AddRow(OrderItem item)
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
        public List<OrderItem> GetSelected()
        {
            List<OrderItem> items = new List<OrderItem>();
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
            List<OrderItem> selected = this.GetSelected();
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

            List<OrderItem> selected = this.GetSelected();
            if (MessageUtil.ShowYesNoMessage("Delete (" + selected.Count + ") Order " + ((selected.Count > 1) ? "Items" : "Item"), "You won't be able to revert!"))
            {
                int succeeded = 0;
                // Not required
                MessageUtil.ShowMessage("Deleted (" + succeeded + ") Order " + ((succeeded > 1) ? "Items" : "Item") + " and (" + (selected.Count - succeeded) + ") failed!");
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
