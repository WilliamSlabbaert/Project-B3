using BusinessLayer;
using BusinessLayer.Models;
using DataLayer.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataLayer
{
    public class ComicStripRepository : IComicStripRepository
    {
        public SqlConnection context { get; set; }

        /// <summary> 
        /// Create Publisher Repository with database Connection 
        /// </summary>
        public ComicStripRepository(SqlConnection context)
        {
            this.context = context;
        }

        #region ComicStrip
        /// <summary> 
        /// Add a new ComicStrip 
        /// </summary>
        public ComicStrip Add(ComicStrip c)
        {
            int id = -1;
            var cmd = "INSERT INTO [dbo].[Comicstrips] (Title,Serie_Id,Number,Publisher_Id) VALUES (@Title,@Serie,@Number,@Publisher);SELECT CAST(scope_identity() AS int)";
            using (var insertCmd = new SqlCommand(cmd, this.context))
            {
                insertCmd.Parameters.AddWithValue("@Title", c.Titel);
                insertCmd.Parameters.AddWithValue("@Serie", c.Serie.ID);
                insertCmd.Parameters.AddWithValue("@Number", c.Number);
                insertCmd.Parameters.AddWithValue("@Publisher", c.Publisher.ID);
                try
                {
                    context.Open();
                    id = (int)insertCmd.ExecuteScalar();
                    context.Close();
                }
                catch (Exception) { throw new InsertException(); }
            }
            if (id < 0) throw new ComicstripAddException();
            cmd = "INSERT INTO [dbo].[ComicstripAuthors] (Comicstrip_Id,Author_Id) VALUES (@Strip,@Author)";
            foreach (Author a in c.Authors)
            {
                using (var insertCmd = new SqlCommand(cmd, this.context))
                {
                    insertCmd.Parameters.AddWithValue("@Strip", id);
                    insertCmd.Parameters.AddWithValue("@Author", a.ID);
                    try
                    {
                        context.Open();
                        insertCmd.ExecuteNonQuery();
                        context.Close();
                    }
                    catch (Exception) { throw new InsertException(); }
                }
            }
            return new ComicStrip(id, c.Titel, c.Serie, c.Number, c.Authors, c.Publisher);
        }

        /// <summary> 
        /// Get a comicstrip by ID 
        /// </summary>
        public ComicStrip GetByID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Comicstrips] Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                {
                    PublisherRepository pr = new PublisherRepository(this.context);
                    return table.AsEnumerable().Select(s => new ComicStrip(s.Field<int>("Id"), s.Field<string>("Title"), GetSerie(s.Field<int>("Serie_Id")), s.Field<int>("Number"), this.GetAuthors(s.Field<int>("Id")), pr.GetByID(s.Field<int>("Publisher_Id")))).Single<ComicStrip>();
                }
            }
            catch (Exception) { throw new QueryException(); }
            return null;
        }

        /// <summary> 
        /// Get list of all Comicstrips 
        /// </summary>
        public List<ComicStrip> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Comicstrips]", this.context);
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                {
                    List<Publisher> publishers = new PublisherRepository(this.context).GetAll();
                    return table.AsEnumerable().Select(s => new ComicStrip(s.Field<int>("Id"), s.Field<string>("Title"), GetSerie(s.Field<int>("Serie_Id")), s.Field<int>("Number"), this.GetAuthors(s.Field<int>("Id")), publishers.Where(x => x.ID == s.Field<int>("Publisher_Id")).SingleOrDefault())).ToList<ComicStrip>();
                }
            }
            catch (Exception) { throw new QueryException(); }
            return new List<ComicStrip>();
        }

        /// <summary> 
        /// Delete Comicstrip by ID 
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Comicstrips] WHERE Id = @Id;DELETE FROM [dbo].[ComicstripAuthors] WHERE Comicstrip_Id = @Strip;", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Strip", id);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Delete all Comicstrips 
        /// </summary>
        public void DeleteAll()
        {
            try
            { 
                SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[Comicstrips];TRUNCATE TABLE [dbo].[ComicstripAuthors]", this.context);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Update existing ComicStrip 
        /// </summary>
        public void Update(ComicStrip s)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Comicstrips] SET Title = @Title, Serie_Id = @Serie, Number = @Number, Publisher_Id = @Publisher  WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Titlel", s.Titel);
                cmd.Parameters.AddWithValue("@Serie", s.Serie.ID);
                cmd.Parameters.AddWithValue("@Number", s.Number);
                cmd.Parameters.AddWithValue("@Publisher", s.Publisher.ID);
                cmd.Parameters.AddWithValue("@Id", s.ID);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Check if ComicStrip exist
        /// </summary>
        public bool Exist(ComicStrip s, bool ignoreId = false)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Comicstrips] WHERE LOWER(Title) = @Title AND Serie_Id = @Serie OR Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Title", s.Titel.ToLower());
                cmd.Parameters.AddWithValue("@Serie", s.Serie.ID);
                cmd.Parameters.AddWithValue("@Id", (!ignoreId) ? s.ID : -1);
                context.Open();
                int count = (int)cmd.ExecuteScalar();
                context.Close();
                return (count > 0);
            }
            catch (Exception) { throw new QueryException(); }
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
                catch (Exception)
                {
                    throw new Exception("Something went wrong while retrieving the strip authors");
                }
            }
            return new List<Author>();
        }
        #endregion

        #region ComicstripSerie
        /// <summary> 
        /// Add a new ComicstripSerie 
        /// </summary>
        public ComicstripSerie AddSerie(ComicstripSerie cs)
        {
            int id = -1;
            String cmd = "INSERT INTO [dbo].[ComicstripSeries] (Name) VALUES (@Name);SELECT CAST(scope_identity() AS int)";
            using (var insertCmd = new SqlCommand(cmd, this.context))
            {
                insertCmd.Parameters.AddWithValue("@Name", cs.Name);
                try
                {
                    context.Open();
                    id = (int) insertCmd.ExecuteScalar();
                    context.Close();
                }
                catch (Exception) { throw new InsertException(); }
            }
            if (id < 0) throw new ComicstripSerieAddException();
            return new ComicstripSerie(id, cs.Name);
        }

        /// <summary> 
        /// Get a ComicstripSerie by ID 
        /// </summary>
        public ComicstripSerie GetSerie(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[ComicstripSeries] WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                    return table.AsEnumerable().Select(p => new ComicstripSerie(p.Field<int>("Id"), p.Field<string>("Name"))).Single<ComicstripSerie>();
            }
            catch (Exception) { throw new QueryException(); }
            return null;
        }

        /// <summary> 
        /// Get list of all ComicStripSeries
        /// </summary>
        public List<ComicstripSerie> GetAllSeries()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[ComicstripSeries]", this.context);
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                    return table.AsEnumerable().Select(p => new ComicstripSerie(p.Field<int>("Id"), p.Field<string>("Name"))).ToList<ComicstripSerie>();
            }
            catch (Exception) { throw new QueryException(); }
            return new List<ComicstripSerie>();
        }

        /// <summary> 
        /// Check if ComicstripSerie exist
        /// </summary>
        public bool ExistSerie(ComicstripSerie cs, bool ignoreId = false)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[ComicstripSeries] WHERE LOWER(Name) = @Name OR Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Name", cs.Name.ToLower());
                cmd.Parameters.AddWithValue("@Id", (!ignoreId) ? cs.ID : -1);
                context.Open();
                int count = (int) cmd.ExecuteScalar();
                context.Close();
                return (count > 0);
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Get ComicstripSerie by Name
        /// </summary>
        public ComicstripSerie GetSerieByName(String name)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[ComicstripSeries] WHERE Name = @Name", this.context);
                cmd.Parameters.AddWithValue("@Name", name.ToLower());
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                    return table.AsEnumerable().Select(p => new ComicstripSerie(p.Field<int>("Id"), p.Field<string>("Name"))).Single<ComicstripSerie>();
            }
            catch (Exception) { throw new QueryException(); }
            return null;
        }

        /// <summary> 
        /// Update existing ComicstripSerie 
        /// </summary>
        public void UpdateSerie(ComicstripSerie cs)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[ComicstripSeries] SET Name = @Name WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Name", cs.Name);
                cmd.Parameters.AddWithValue("@Id", cs.ID);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }
        #endregion

        #region Custom Exceptions
        public class ComicstripAddException : Exception
        {
            public ComicstripAddException() : base(String.Format("The comicstrip was not created")) { }
        }

        public class ComicstripSerieAddException : Exception
        {
            public ComicstripSerieAddException() : base(String.Format("The comicstripserie was not created")) { }
        }
        #endregion
    }
}