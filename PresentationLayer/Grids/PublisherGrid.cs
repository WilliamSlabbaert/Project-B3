using BusinessLayer;
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
    public class PublisherGrid
    {
        #region Attributres
        public List<Publisher> Publishers { get; private set; } = new List<Publisher>();
        public DataTable Table { get; private set; } = BuildTable();
        public DataGrid Grid { get; private set; }

        private List<Button> DeleteButtons = new List<Button>();
        private List<Button> EditButtons = new List<Button>();
        #endregion

        public PublisherGrid(DataGrid grid)
        {
            this.Grid = grid;
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        public PublisherGrid(DataGrid grid, List<Publisher> publishers)
        {
            this.Grid = grid;
            AddPublishers(publishers);
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        #region Table
        public void AddPublisher(Publisher publisher)
        {
            this.Publishers.Add(publisher);
            AddRow(publisher);
        }

        public void AddPublishers(List<Publisher> publishers)
        {
            this.Publishers.AddRange(publishers);
            foreach (Publisher publisher in publishers)
                AddRow(publisher);
        }
 
        private void AddRow(Publisher publisher)
        {
            DataRow row = this.Table.NewRow();
            row[0] = publisher.ID;
            row[1] = publisher.Name;
            this.Table.Rows.Add(row);
        }

        private static DataTable BuildTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("#", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(string)));
            return table;
        }
        #endregion

        #region Selection
        public List<Publisher> GetSelected()
        {
            List<Publisher> publishers = new List<Publisher>();
            List<int> gridIndexes = this.Grid.SelectedItems.Cast<DataRowView>().Select(x => this.Table.Rows.IndexOf(x.Row)).ToList();
            foreach (int i in gridIndexes)
            {
                int index = (int)this.Table.Rows[i][0];
                publishers.Add(this.Publishers.Where(x => x.ID == index).Single());
            }
            return publishers;
        }

        private void GridselectionChanged(object sender, RoutedEventArgs e)
        {
            List<Publisher> selected = this.GetSelected();
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

            List<Publisher> selected = this.GetSelected();
            if (MessageUtil.ShowYesNoMessage("Delete (" + selected.Count + ") " + ((selected.Count > 1) ? "Publishers" : "Publisher"), "You won't be able to revert!"))
            {
                int succeeded = 0;
                PublisherManager pm = new PublisherManager(new UnitOfWork());
                foreach (Publisher p in selected)
                {
                    try
                    {
                        pm.Delete(p.ID);
                        int i = this.Publishers.IndexOf(p);
                        this.Table.Rows.RemoveAt(i);
                        this.Publishers.Remove(p);
                        succeeded++;
                    }
                    catch (Exception) { }
                }
                MessageUtil.ShowMessage("Deleted (" + succeeded + ") Publishers and (" + (selected.Count - succeeded) + ") failed!");
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
