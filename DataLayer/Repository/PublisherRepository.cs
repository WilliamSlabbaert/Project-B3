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
    public class PublisherRepository : IPublisherRepository
    {
        private SqlConnection context { get; set; }

        /// <summary> 
        /// Create Publisher Repository with database Connection 
        /// </summary>
        public PublisherRepository(SqlConnection context)
        {
            this.context = context;
        }

        /// <summary> 
        /// Add a new Publisher 
        /// </summary>
        public Publisher Add(Publisher p)
        {
            int id = -1;
            String cmd = "INSERT INTO [dbo].[Publishers] (Name) VALUES (@Name);SELECT CAST(scope_identity() AS int)";
            using (var insertCmd = new SqlCommand(cmd, this.context))
            {
                insertCmd.Parameters.AddWithValue("@Name", p.Name);
                try
                {
                    context.Open();
                    id = (int)insertCmd.ExecuteScalar();
                    context.Close();
                }
                catch (Exception) { throw new InsertException(); }
            }
            if (id < 0) throw new PublisherAddException();
            return new Publisher(id, p.Name);
        }

        /// <summary> 
        /// Get a publisher by ID 
        /// </summary>
        public Publisher GetByID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Publishers] WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                    return table.AsEnumerable().Select(p => new Publisher(p.Field<int>("Id"), p.Field<string>("Name"))).Single<Publisher>();
            }
            catch (Exception) { throw new QueryException(); }
            return null;
        }

        /// <summary> 
        /// Get list of all Publishers 
        /// </summary>
        public List<Publisher> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Publishers]", this.context);
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                    return table.AsEnumerable().Select(p => new Publisher(p.Field<int>("Id"), p.Field<string>("Name"))).ToList<Publisher>();
            }
            catch (Exception) { throw new QueryException(); }
            return new List<Publisher>();
        }

        /// <summary> 
        /// Delete publisher by ID 
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Publishers] WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Delete all Publishers 
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[Publishers]", this.context);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Update existing Publisher 
        /// </summary>
        public void Update(Publisher p)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Publishers] SET Name = @Name WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Id", p.ID);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Check if Publisher exist
        /// </summary>
        public bool Exist(Publisher p, bool ignoreId = false)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Publishers] WHERE LOWER(Name) = @Name OR Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Name", p.Name.ToLower());
                cmd.Parameters.AddWithValue("@Id", (!ignoreId) ? p.ID : -1);
                context.Open();
                int count = (int)cmd.ExecuteScalar();
                context.Close();
                return (count > 0);
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Check if Publisher is included at Comicstrips
        /// </summary>
        public bool HasStrips(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Comicstrips] WHERE Publisher_Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                context.Open();
                int count = (int)cmd.ExecuteScalar();
                context.Close();
                return (count > 0);
            }
            catch (Exception) { throw new QueryException(); }
        }

        public class PublisherAddException : Exception
        {
            public PublisherAddException() : base(String.Format("The publisher was not created")) { }
        }
    }
}