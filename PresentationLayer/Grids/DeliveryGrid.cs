using BusinessLayer.DomainManagers;
using BusinessLayer.Models;
using DataLayer;
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
    public class DeliveryGrid
    {
        #region Attributres
        public List<Delivery> Deliveries { get; private set; } = new List<Delivery>();
        public DataTable Table { get; private set; } = BuildTable();
        public DataGrid Grid { get; private set; }

        private List<Button> DeleteButtons = new List<Button>();
        private List<Button> EditButtons = new List<Button>();
        #endregion

        public DeliveryGrid(DataGrid grid)
        {
            this.Grid = grid;
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        public DeliveryGrid(DataGrid grid, List<Delivery> deliveries)
        {
            this.Grid = grid;
            AddDeliveries(deliveries);
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        #region Table
        public void AddDelivery(Delivery delivery)
        {
            this.Deliveries.Add(delivery);
            AddRow(delivery);
        }

        public void AddDeliveries(List<Delivery> deliveries)
        {
            this.Deliveries.AddRange(deliveries);
            foreach (Delivery delivery in deliveries)
                AddRow(delivery);
        }
        private void AddRow(Delivery delivery)
        {
            DataRow row = this.Table.NewRow();
            row[0] = delivery.ID;
            row[1] = delivery.Supplier;
            row[2] = delivery.Date.ToShortDateString();
            row[3] = delivery.Items.Sum(x => x.Quantity);
            this.Table.Rows.Add(row);
        }

        private static DataTable BuildTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("#", typeof(int)));
            table.Columns.Add(new DataColumn("Supplier", typeof(string)));
            table.Columns.Add(new DataColumn("Date", typeof(string)));
            table.Columns.Add(new DataColumn("Items", typeof(string)));
            return table;
        }
        #endregion

        #region Selection
        public List<Delivery> GetSelected()
        {
            List<Delivery> deliveries = new List<Delivery>();
            List<int> gridIndexes = this.Grid.SelectedItems.Cast<DataRowView>().Select(x => this.Table.Rows.IndexOf(x.Row)).ToList();
            foreach (int i in gridIndexes)
            {
                int index = (int)this.Table.Rows[i][0];
                deliveries.Add(this.Deliveries.Where(x => x.ID == index).Single());
            }
            return deliveries;
        }
        private void GridselectionChanged(object sender, RoutedEventArgs e)
        {
            List<Delivery> selected = this.GetSelected();
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

            List<Delivery> selected = this.GetSelected();
            if (MessageUtil.ShowYesNoMessage("Delete (" + selected.Count + ") " + ((selected.Count > 1) ? "Deliveries" : "Delivery"), "You won't be able to revert!"))
            {
                int succeeded = 0;
                DeliveryManager dm = new DeliveryManager(new UnitOfWork());
                foreach (Delivery d in selected)
                {
                    try
                    {
                        dm.Delete(d.ID);
                        int i = this.Deliveries.IndexOf(d);
                        this.Table.Rows.RemoveAt(i);
                        this.Deliveries.Remove(d);
                        succeeded++;
                    }
                    catch (Exception) { }
                }
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
