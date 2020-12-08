using BusinessLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
                context.Open();
                id = (int)insertCmd.ExecuteScalar();
                context.Close();
            }
            if (id < 0) throw new PublisherAddException();
            return new Publisher(id, p.Name);
        }

        /// <summary> 
        /// Get a publisher by ID 
        /// </summary>
        public Publisher GetByID(int id)
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Publishers] WHERE Id = @Id", this.context);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter reader = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            reader.Fill(table);
            context.Close();
            if (table.Rows.Count > 0)
                return table.AsEnumerable().Select(p => new Publisher(p.Field<int>("Id"), p.Field<string>("Name"))).Single<Publisher>();
            return null;
        }

        /// <summary> 
        /// Get list of all publishers 
        /// </summary>
        public List<Publisher> GetAll()
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Publishers]", this.context);
            SqlDataAdapter reader = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            reader.Fill(table);
            context.Close();
            if (table.Rows.Count > 0)
                return table.AsEnumerable().Select(p => new Publisher(p.Field<int>("Id"), p.Field<string>("Name"))).ToList<Publisher>();
            return new List<Publisher>();
        }

        /// <summary> 
        /// Delete publisher by ID 
        /// </summary>
        public void Delete(int id)
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Publishers] WHERE Id = @Id", this.context);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            context.Close();
        }

        /// <summary> 
        /// Delete all Publishers 
        /// </summary>
        public void DeleteAll()
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[Publishers]", this.context);
            cmd.ExecuteNonQuery();
            context.Close();

        }

        /// <summary> 
        /// Update existing Publisher 
        /// </summary>
        public void Update(Publisher p)
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Publishers] SET Name = @Name WHERE Id = @Id", this.context);
            cmd.Parameters.AddWithValue("@Name", p.Name);
            cmd.Parameters.AddWithValue("@Id", p.ID);
            cmd.ExecuteNonQuery();
            context.Close();
        }

        public class PublisherAddException : Exception
        {
            public PublisherAddException() : base(String.Format("The publisher was not created")) { }
        }
    }
}
