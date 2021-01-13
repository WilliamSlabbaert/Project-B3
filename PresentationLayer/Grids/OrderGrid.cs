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
    public class OrderGrid
    {
        #region Attributres
        public List<Order> Orders { get; private set; } = new List<Order>();
        public DataTable Table { get; private set; } = BuildTable();
        public DataGrid Grid { get; private set; }

        private List<Button> DeleteButtons = new List<Button>();
        private List<Button> EditButtons = new List<Button>();
        #endregion

        public OrderGrid(DataGrid grid)
        {
            this.Grid = grid;
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        public OrderGrid(DataGrid grid, List<Order> orders)
        {
            this.Grid = grid;
            AddOrders(orders);
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        #region Table
        public void AddOrder(Order order)
        {
            this.Orders.Add(order);
            AddRow(order);
        }

        public void AddOrders(List<Order> orders)
        {
            this.Orders.AddRange(orders);
            foreach (Order order in orders)
                AddRow(order);
        }
        private void AddRow(Order order)
        {
            DataRow row = this.Table.NewRow();
            row[0] = order.ID;
            row[1] = order.Firstname;
            row[2] = order.Lastname;
            row[3] = order.Email;
            row[4] = order.Phone;
            row[5] = order.Date.ToShortDateString();
            row[6] = order.Items.Sum(x => x.Quantity);
            this.Table.Rows.Add(row);
        }

        private static DataTable BuildTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("#", typeof(int)));
            table.Columns.Add(new DataColumn("First name", typeof(string)));
            table.Columns.Add(new DataColumn("Last name", typeof(string)));
            table.Columns.Add(new DataColumn("Email", typeof(string)));
            table.Columns.Add(new DataColumn("Phone", typeof(string)));
            table.Columns.Add(new DataColumn("Date", typeof(string)));
            table.Columns.Add(new DataColumn("Items", typeof(string)));
            return table;
        }
        #endregion

        #region Selection
        public List<Order> GetSelected()
        {
            List<Order> orders = new List<Order>();
            List<int> gridIndexes = this.Grid.SelectedItems.Cast<DataRowView>().Select(x => this.Table.Rows.IndexOf(x.Row)).ToList();
            foreach (int i in gridIndexes)
            {
                int index = (int)this.Table.Rows[i][0];
                orders.Add(this.Orders.Where(x => x.ID == index).Single());
            }
            return orders;
        }
        private void GridselectionChanged(object sender, RoutedEventArgs e)
        {
            List<Order> selected = this.GetSelected();
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

            List<Order> selected = this.GetSelected();
            if (MessageUtil.ShowYesNoMessage("Delete (" + selected.Count + ") " + ((selected.Count > 1) ? "Orders" : "Order"), "You won't be able to revert!"))
            {
                int succeeded = 0;
                OrderManager om = new OrderManager(new UnitOfWork());
                foreach (Order o in selected)
                {
                    try
                    {
                        om.Delete(o.ID);
                        int i = this.Orders.IndexOf(o);
                        this.Table.Rows.RemoveAt(i);
                        this.Orders.Remove(o);
                        succeeded++;
                    }
                    catch (Exception) { }
                }
                MessageUtil.ShowMessage("Deleted (" + succeeded + ") " + ((succeeded > 1) ? "Orders" : "Order") + " and (" + (selected.Count - succeeded) + ") failed!");
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
