using BusinessLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class ComicStripRepository : IComicStripRepository
    {
        public SqlConnection context { get; set; }
        public ComicStripRepository(SqlConnection context)
        {
            this.context = context;
        }

        /// <summary> 
        /// Add a new ComicStrip 
        /// </summary>
        public ComicStrip Add(ComicStrip c)
        {
            int id = -1;
            var cmd = "INSERT INTO [dbo].[Comicstrips] (Title,Serie,Number,Publisher_Id) VALUES (@Title,@Serie,@Number,@Publisher);SELECT CAST(scope_identity() AS int)";
            using(var insertCmd = new SqlCommand(cmd, this.context))
            {
                insertCmd.Parameters.AddWithValue("@Title", c.Titel);
                insertCmd.Parameters.AddWithValue("@Serie", c.Serie);
                insertCmd.Parameters.AddWithValue("@Number", c.Number);
                insertCmd.Parameters.AddWithValue("@Publisher", c.Publisher.ID);
                context.Open();
                id = (int) insertCmd.ExecuteScalar();
                context.Close();
            }
            if (id < 0) throw new ComicstripAddException();
            cmd = "INSERT INTO [dbo].[ComicstripAuthors] (Comicstrip_Id,Author_Id) VALUES (@Strip,@Author)";
            foreach(Author a in c.Authors)
            {
                using (var insertCmd = new SqlCommand(cmd, this.context))
                {
                    insertCmd.Parameters.AddWithValue("@Strip", id);
                    insertCmd.Parameters.AddWithValue("@Author", a.ID);
                    context.Open();
                    insertCmd.ExecuteNonQuery();
                    context.Close();
                }
            }
            return new ComicStrip(id, c.Titel, c.Serie, c.Number, c.Authors, c.Publisher);
        }

        public void DeleteAll()
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[ComicStrip]", context);
            cmd.ExecuteNonQuery();
            context.Close();
        }

        public void DeleteByID(int ID)
        {
            context.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [dbo].[ComicStrip] WHERE Id = " + ID, context);
            command.ExecuteNonQuery();
            context.Close();
        }

        public ComicStrip GetByID(int ID)
        {
            return null;
        }

        /// <summary> 
        /// Get list of all Comicstrips 
        /// </summary>
        public List<ComicStrip> GetAll()
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Comicstrips]", this.context);
            SqlDataAdapter reader = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            reader.Fill(table);
            context.Close();
            if (table.Rows.Count > 0)
            {
                List<Publisher> publishers = new PublisherRepository(this.context).GetAll();
                return table.AsEnumerable().Select(s => new ComicStrip(s.Field<int>("Id"), s.Field<string>("Titel"), s.Field<string>("Serie"), s.Field<int>("Number"), this.GetAuthors(s.Field<int>("Id")), publishers.Where(x => x.ID == s.Field<int>("Publisher_Id")).SingleOrDefault())).ToList<ComicStrip>();
            }   
            return new List<ComicStrip>();
        }

        /// <summary> 
        /// Get list of all Comicstrip Authors
        /// </summary>
        public List<Author> GetAuthors(int id)
        {
            var cmd = "SELECT * FROM [dbo].[ComicstripAuthors] WHERE Comicstrip_Id = @Strip";
            using (var selectCmd = new SqlCommand(cmd, this.context))
            {
                try
                {
                    selectCmd.Parameters.AddWithValue("@Strip", id);
                    this.context.Open();
                    SqlDataAdapter reader = new SqlDataAdapter(selectCmd);
                    DataTable table = new DataTable();
                    reader.Fill(table);
                    this.context.Close();
                    if (table.Rows.Count > 0)
                    {
                        AuthorRepository aRepo = new AuthorRepository(this.context);
                        return table.AsEnumerable().Select(x => aRepo.GetByID(x.Field<int>("Author_Id"))).ToList<Author>();
                    } 
                }
                catch(Exception) {
                    throw new Exception("Something went wrong while retrieving the strip authors");
                }
            }
            return new List<Author>();
        }

        public class ComicstripAddException : Exception
        {
            public ComicstripAddException() : base(String.Format("The comicstrip was not created")) { }
        }
    }
}
