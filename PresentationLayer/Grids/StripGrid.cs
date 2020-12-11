using BusinessLayer;
using BusinessLayer.Models;
using DataLayer;
using FluentAssertions;
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
    public class StripGrid
    {
        #region Attributres
        public List<ComicStrip> Comicstrips { get; private set; } = new List<ComicStrip>();
        public DataTable Table { get; private set; } = BuildTable();
        private DataGrid Grid { get; set; }

        private List<Button> DeleteButtons = new List<Button>();
        private List<Button> EditButtons = new List<Button>();
        #endregion

        public StripGrid(DataGrid grid)
        {
            this.Grid = grid;
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        public StripGrid(DataGrid grid, List<ComicStrip> comicstrips)
        {
            this.Grid = grid;
            AddStrips(comicstrips);
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        #region Table
        public void AddStrip(ComicStrip comicstrip)
        {
            this.Comicstrips.Add(comicstrip);
            AddRow(comicstrip);
        }

        public void AddStrips(List<ComicStrip> comicstrips)
        {
            this.Comicstrips.AddRange(comicstrips);
            foreach (ComicStrip comicstrip in comicstrips)
                AddRow(comicstrip);
        }

        private void AddRow(ComicStrip comicstrip)
        {
            DataRow row = this.Table.NewRow();
            row[0] = comicstrip.ID;
            row[1] = comicstrip.Titel;
            row[2] = comicstrip.Serie.Name;
            row[3] = comicstrip.Number;
            row[4] = comicstrip.Publisher.Name;
            row[5] = string.Join(",", comicstrip.Authors.Select(x => x));

            this.Table.Rows.Add(row);
        }

        private static DataTable BuildTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("#", typeof(int)));
            table.Columns.Add(new DataColumn("Title", typeof(string)));
            table.Columns.Add(new DataColumn("Serie", typeof(string)));
            table.Columns.Add(new DataColumn("Number", typeof(string)));
            table.Columns.Add(new DataColumn("Publisher", typeof(string)));
            table.Columns.Add(new DataColumn("Authors", typeof(string)));
            return table;
        }
        #endregion

        #region Selection
        public List<ComicStrip> GetSelected()
        {
            List<ComicStrip> comicstrips = new List<ComicStrip>();
            List<int> gridIndexes = this.Grid.SelectedItems.Cast<DataRowView>().Select(x => this.Table.Rows.IndexOf(x.Row)).ToList();
            foreach (int i in gridIndexes)
            {
                int index = (int)this.Table.Rows[i][0];
                comicstrips.Add(this.Comicstrips.Where(x => x.ID == index).Single());
            }
            return comicstrips;
        }

        private void GridselectionChanged(object sender, RoutedEventArgs e)
        {
            List<ComicStrip> selected = this.GetSelected();
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

            List<ComicStrip> selected = this.GetSelected();
            if (MessageUtil.ShowYesNoMessage("Delete (" + selected.Count + ") " + ((selected.Count > 1) ? "Publishers" : "Publisher"), "You won't be able to revert!"))
            {
                int succeeded = 0;
                ComicStripManager sm = new ComicStripManager(new UnitOfWork());
                foreach (ComicStrip s in selected)
                {
                    try
                    {
                        sm.Delete(s.ID);
                        int i = this.Comicstrips.IndexOf(s);
                        this.Table.Rows.RemoveAt(i);
                        this.Comicstrips.Remove(s);
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
