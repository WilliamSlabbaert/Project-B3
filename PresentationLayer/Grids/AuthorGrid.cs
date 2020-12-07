using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace PresentationLayer.Grids
{
    public class AuthorGrid
    {
        public List<Author> Authors { get; private set; } = new List<Author>();
        public DataTable Table { get; private set; } = BuildTable();
        public DataGrid Grid { get; private set; }

        public AuthorGrid(DataGrid grid)
        {
            this.Grid = grid;
            this.Grid.ItemsSource = this.Table.DefaultView;
        }

        public AuthorGrid(DataGrid grid, List<Author> authors)
        {
            this.Grid = grid;
            AddAuthors(authors);
            this.Grid.ItemsSource = this.Table.DefaultView;
        }

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
    }
}
