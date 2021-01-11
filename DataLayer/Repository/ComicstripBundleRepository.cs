using BusinessLayer;
using BusinessLayer.Models;
using DataLayer.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer.Repository
{
    public class ComicstripBundleRepository : IComicstripBundleRepository
    {
        public SqlConnection context { get; set; }

        /// <summary> 
        /// Create ComicstripBundole Repository with database Connection 
        /// </summary>
        public ComicstripBundleRepository(SqlConnection context)
        {
            this.context = context;
        }

        #region ComicStrip
        /// <summary> 
        /// Add a new ComicStrip 
        /// </summary>
        public ComicstripBundle Add(ComicstripBundle b)
        {
            int id = -1;
            var cmd = "INSERT INTO [dbo].[ComicstripBundles] (Title,Publisher_Id) VALUES (@Title,@Publisher);SELECT CAST(scope_identity() AS int)";
            using (var insertCmd = new SqlCommand(cmd, this.context))
            {
                insertCmd.Parameters.AddWithValue("@Title", b.Titel);
                insertCmd.Parameters.AddWithValue("@Publisher", b.Publisher.ID);
                try
                {
                    context.Open();
                    id = (int)insertCmd.ExecuteScalar();
                    context.Close();
                }
                catch (Exception) { throw new InsertException(); }
            }
            if (id < 0) throw new ComicstripBundleAddException();
            cmd = "INSERT INTO [dbo].[ComicstripBundleComicstrips] (ComicstripBundle_Id,Comicstrip_Id) VALUES (@Bundle,@Strip)";
            foreach (ComicStrip s in b.Comicstrips)
            {
                using (var insertCmd = new SqlCommand(cmd, this.context))
                {
                    insertCmd.Parameters.AddWithValue("@Bundle", id);
                    insertCmd.Parameters.AddWithValue("@Strip", s.ID);
                    try
                    {
                        context.Open();
                        insertCmd.ExecuteNonQuery();
                        context.Close();
                    }
                    catch (Exception) { throw new InsertException(); }
                }
            }
            return new ComicstripBundle(id, b.Titel, b.Comicstrips, b.Publisher);
        }

        /// <summary> 
        /// Get a ComicstripBundle by ID 
        /// </summary>
        public ComicstripBundle GetByID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[ComicstripBundles] WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                {
                    PublisherRepository pr = new PublisherRepository(this.context);
                    return table.AsEnumerable().Select(b => new ComicstripBundle(b.Field<int>("Id"), b.Field<string>("Title"), this.GetComicstrips(b.Field<int>("Id")), pr.GetByID(b.Field<int>("Publisher_Id")))).Single<ComicstripBundle>();
                }
            }
            catch (Exception) { throw new QueryException(); }
            return null;
        }

        /// <summary> 
        /// Get list of all ComicstripBundles 
        /// </summary>
        public List<ComicstripBundle> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[ComicstripBundles]", this.context);
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                {
                    List<Publisher> publishers = new PublisherRepository(this.context).GetAll();
                    return table.AsEnumerable().Select(b => new ComicstripBundle(b.Field<int>("Id"), b.Field<string>("Title"), this.GetComicstrips(b.Field<int>("Id")), publishers.Where(x => x.ID == b.Field<int>("Publisher_Id")).SingleOrDefault())).ToList<ComicstripBundle>();
                }
            }
            catch (Exception) { throw new QueryException(); }
            return new List<ComicstripBundle>();
        }

        /// <summary> 
        /// Delete ComicstripBundle by ID 
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[ComicstripBundles] WHERE Id = @Id;DELETE FROM [dbo].[ComicstripBundleComicstrips] WHERE ComicstripBundle_Id = @Bundle;", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Bundle", id);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Delete all ComicstripBundle 
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[ComicstripBundles];TRUNCATE TABLE [dbo].[ComicstripBundleComicstrips]", this.context);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Update existing ComicstripBundle 
        /// </summary>
        public void Update(ComicstripBundle b)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[ComicstripBundles] SET Title = @Title, Publisher_Id = @Publisher WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Titlel", b.Titel);
                cmd.Parameters.AddWithValue("@Publisher", b.Publisher.ID);
                cmd.Parameters.AddWithValue("@Id", b.ID);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Check if ComicstripBundle exist
        /// </summary>
        public bool Exist(ComicstripBundle b, bool ignoreId = false)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[ComicstripBundles] WHERE LOWER(Title) = @Title OR Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Title", b.Titel.ToLower());
                cmd.Parameters.AddWithValue("@Id", (!ignoreId) ? b.ID : -1);
                context.Open();
                int count = (int)cmd.ExecuteScalar();
                context.Close();
                return (count > 0);
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Get list of all ComicstripBundle Comicstrips
        /// </summary>
        public List<ComicStrip> GetComicstrips(int id)
        {
            var cmd = "SELECT * FROM [dbo].[ComicstripBundleComicstrips] WHERE ComicstripBundle_Id = @Bundle";
            using (var selectCmd = new SqlCommand(cmd, this.context))
            {
                try
                {
                    selectCmd.Parameters.AddWithValue("@Bundle", id);
                    this.context.Open();
                    SqlDataAdapter reader = new SqlDataAdapter(selectCmd);
                    DataTable table = new DataTable();
                    reader.Fill(table);
                    this.context.Close();
                    if (table.Rows.Count > 0)
                    {
                        //List<ComicStrip> comicstrips = new ComicStripRepository(this.context).GetAll();
                        //return table.AsEnumerable().Select(x => comicstrips.Where(y => y.ID == x.Field<int>("Comicstrip_Id")).Single()).ToList<ComicStrip>();
                        ComicStripRepository sRepoo = new ComicStripRepository(this.context);
                        return table.AsEnumerable().Select(x => sRepoo.GetByID(x.Field<int>("Comicstrip_Id"))).ToList<ComicStrip>();
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Something went wrong while retrieving the bundle strips");
                }
            }
            return new List<ComicStrip>();
        }
        #endregion

        #region Custom Exceptions
        public class ComicstripBundleAddException : Exception
        {
            public ComicstripBundleAddException() : base(String.Format("The comicstrip bundle was not created")) { }
        }
        #endregion
    }
}
