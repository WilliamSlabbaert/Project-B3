using BusinessLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class AuthorRepository : IAuthorRepository
    {
        private SqlConnection context { get; set; }

        public AuthorRepository(SqlConnection context)
        {
            this.context = context;
        }

        /// <summary> 
        /// Add a new Author 
        /// </summary>
        public void Add(Author a)
        {
            if (Exists(a)) throw new Exception("Author staat er al in");
            context.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Authors] (Firstname,Lastname) VALUES (@Firstname,@Lastname)", context);
            cmd.Parameters.AddWithValue("@Firstname", a.Firstname);
            cmd.Parameters.AddWithValue("@Lastname", a.Surname);
            cmd.ExecuteNonQuery();
            context.Close();
        }
        public bool Exists(Author a)
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Authors] WHERE LOWER(Firstname) = @Firstname AND LOWER(Lastname) = @Lastname", this.context);
            cmd.Parameters.AddWithValue("@Firstname", a.Firstname.ToLower());
            cmd.Parameters.AddWithValue("@Lastname", a.Surname.ToLower());
            cmd.ExecuteNonQuery();
            SqlDataAdapter reader = new SqlDataAdapter(cmd);

            DataTable table = new DataTable();
            reader.Fill(table);
            context.Close();
            return (table.Rows.Count > 0);

        }
        public void DeleteAll()
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[Author]", this.context);
            cmd.ExecuteNonQuery();
            context.Close();
        }


        public void DeleteByID(int ID)
        {
            context.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Author] WHERE Id = " + ID, context);
            command.ExecuteNonQuery();
            context.Close();
        }

        /// <summary> 
        /// Get list of all authors 
        /// </summary>
        public List<Author> GetAll()
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Authors]", this.context);
            SqlDataAdapter reader = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            reader.Fill(table);
            context.Close();
            if (table.Rows.Count > 0)
                return table.AsEnumerable().Select(a => new Author(a.Field<int>("Id"), a.Field<string>("Firstname"), a.Field<string>("Lastname"))).ToList<Author>();
            return new List<Author>();
        }

        public Author GetByID(int ID)
        {
            return null;
        }
    }
}
