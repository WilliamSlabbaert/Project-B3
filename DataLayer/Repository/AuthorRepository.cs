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
        public Author Add(Author a)
        {
            int id = -1;
            String cmd = "INSERT INTO [dbo].[Authors] (Firstname,Lastname) VALUES (@Firstname,@Lastname);SELECT CAST(scope_identity() AS int)";
            using (var insertCmd = new SqlCommand(cmd, this.context))
            {
                insertCmd.Parameters.AddWithValue("@Firstname", a.Firstname);
                insertCmd.Parameters.AddWithValue("@Lastname", a.Surname);
                context.Open();
                id = (int) insertCmd.ExecuteScalar();
                context.Close();
            }
            if (id < 0) throw new AuthorAddException();
            return new Author(id, a.Firstname, a.Surname);
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
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Authors] WHERE Id = " + ID, context);
            SqlDataAdapter reader = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            reader.Fill(dt);

            if (dt.Rows[0] != null)
            {
                var temp = new Author(Convert.ToInt32(dt.Rows[0]["Id"].ToString()),dt.Rows[0]["Firstname"].ToString(), dt.Rows[0]["Lastname"].ToString());
                return temp;
            }

            else return null;
        }

        public bool Exists(Author a)
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Authors] WHERE LOWER(Firstname) = @Firstname AND LOWER(Lastname) = @Lastname", this.context);
            cmd.Parameters.AddWithValue("@Firstname", a.Firstname.ToLower());
            cmd.Parameters.AddWithValue("@Lastname", a.Surname.ToLower());
            SqlDataAdapter reader = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            reader.Fill(table);
            context.Close();
            return (table.Rows.Count > 0);
        }

        public class AuthorAddException : Exception
        {
            public AuthorAddException() : base(String.Format("The author was not created")) { }
        }
    }
}
