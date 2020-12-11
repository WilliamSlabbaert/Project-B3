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
    public class AuthorGrid
    {
        #region Attributres
        public List<Author> Authors { get; private set; } = new List<Author>();
        public DataTable Table { get; private set; } = BuildTable();
        public DataGrid Grid { get; private set; }

        private List<Button> DeleteButtons = new List<Button>();
        private List<Button> EditButtons = new List<Button>();
        #endregion

        public AuthorGrid(DataGrid grid)
        {
            this.Grid = grid;
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        public AuthorGrid(DataGrid grid, List<Author> authors)
        {
            this.Grid = grid;
            AddAuthors(authors);
            this.Grid.ItemsSource = this.Table.DefaultView;
            this.Grid.SelectionChanged += GridselectionChanged;
        }

        #region Table
        public void AddAuthor(Author author)
        {
            this.Authors.Add(author);
            AddRow(author);
        }

        public void AddAuthors(List<Author> authors)
        {
            this.Authors.AddRange(authors);
            foreach (Author author in authors)
                AddRow(author);
        }
        private void AddRow(Author author)
        {
            DataRow row = this.Table.NewRow();
            row[0] = author.ID;
            row[1] = author.Firstname;
            row[2] = author.Surname;
            this.Table.Rows.Add(row);
        }

        private static DataTable BuildTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("#", typeof(int)));
            table.Columns.Add(new DataColumn("Firstname", typeof(string)));
            table.Columns.Add(new DataColumn("Lastname", typeof(string)));
            return table;
        }
        #endregion

        #region Selection
        public List<Author> GetSelected()
        {
            List<Author> authors = new List<Author>();
            List<int> gridIndexes = this.Grid.SelectedItems.Cast<DataRowView>().Select(x => this.Table.Rows.IndexOf(x.Row)).ToList();
            foreach(int i in gridIndexes)
            {
                int index = (int)this.Table.Rows[i][0]; 
                authors.Add(this.Authors.Where(x => x.ID == index).Single());
            }
            return authors;
        }
        private void GridselectionChanged(object sender, RoutedEventArgs e)
        {
            List<Author> selected = this.GetSelected();
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

            List<Author> selected = this.GetSelected();
            if (MessageUtil.ShowYesNoMessage("Delete (" + selected.Count + ") " + ((selected.Count > 1) ? "Authors" : "Author"), "You won't be able to revert!"))
            {
                int succeeded = 0;
                AuthorManager am = new AuthorManager(new UnitOfWork());
                foreach (Author a in selected)
                {
                    try
                    {
                        am.Delete(a.ID);
                        int i = this.Authors.IndexOf(a);
                        this.Table.Rows.RemoveAt(i);
                        this.Authors.Remove(a);
                        succeeded++;
                    }
                    catch (Exception) { }
                }
                MessageUtil.ShowMessage("Deleted (" + succeeded + ") " + ((succeeded > 1) ? "Authors" : "Author") + " and (" + (selected.Count - succeeded) + ") failed!");
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
