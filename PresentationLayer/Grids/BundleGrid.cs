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
    public class BundleGrid
    {
        #region Attributres
        public List<ComicstripBundle> ComicstripBundles { get; private set; } = new List<ComicstripBundle>();
        public DataTable Table { get; private set; } = BuildTable();
        public DataGrid Grid { get; private set; }

        private List<Button> DeleteButtons = new List<Button>();
        private List<Button> EditButtons = new List<Button>();
        #endregion

        public BundleGrid(DataGrid grid)
        {
            this.Grid = grid;
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        public BundleGrid(DataGrid grid, List<ComicstripBundle> bundles)
        {
            this.Grid = grid;
            AddBundles(bundles);
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        #region Table
        public void AddBundle(ComicstripBundle bundle)
        {
            this.ComicstripBundles.Add(bundle);
            AddRow(bundle);
        }

        public void AddBundles(List<ComicstripBundle> bundles)
        {
            this.ComicstripBundles.AddRange(bundles);
            foreach (ComicstripBundle bundle in bundles)
                AddRow(bundle);
        }

        private void AddRow(ComicstripBundle bundle)
        {
            DataRow row = this.Table.NewRow();
            row[0] = bundle.ID;
            row[1] = bundle.Titel;
            row[2] = bundle.Publisher.Name;
            row[3] = string.Join(",", bundle.Comicstrips.Select(x => x.ID));
            this.Table.Rows.Add(row);
        }

        private static DataTable BuildTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("#", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(string)));
            table.Columns.Add(new DataColumn("Publisher", typeof(string)));
            table.Columns.Add(new DataColumn("Comicstrips", typeof(string)));
            return table;
        }
        #endregion

        #region Selection
        public List<ComicstripBundle> GetSelected()
        {
            List<ComicstripBundle> bundles = new List<ComicstripBundle>();
            List<int> gridIndexes = this.Grid.SelectedItems.Cast<DataRowView>().Select(x => this.Table.Rows.IndexOf(x.Row)).ToList();
            foreach (int i in gridIndexes)
            {
                int index = (int)this.Table.Rows[i][0];
                bundles.Add(this.ComicstripBundles.Where(x => x.ID == index).Single());
            }
            return bundles;
        }

        private void GridselectionChanged(object sender, RoutedEventArgs e)
        {
            List<ComicstripBundle> selected = this.GetSelected();
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

            List<ComicstripBundle> selected = this.GetSelected();
            if (MessageUtil.ShowYesNoMessage("Delete (" + selected.Count + ") " + ((selected.Count > 1) ? "Comicstrip Bundles" : "Comicstrip Bundle"), "You won't be able to revert!"))
            {
                int succeeded = 0;
                ComicstripBundleManager bm = new ComicstripBundleManager(new UnitOfWork());
                foreach (ComicstripBundle b in selected)
                {
                    try
                    {
                        bm.Delete(b.ID);
                        int i = this.ComicstripBundles.IndexOf(b);
                        this.Table.Rows.RemoveAt(i);
                        this.ComicstripBundles.Remove(b);
                        succeeded++;
                    }
                    catch (Exception) { }
                }
                MessageUtil.ShowMessage("Deleted (" + succeeded + ") Comicstrip Bundles and (" + (selected.Count - succeeded) + ") failed!");
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
