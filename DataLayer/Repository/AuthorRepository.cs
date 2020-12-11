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
    public class AuthorRepository : IAuthorRepository
    {
        private SqlConnection context { get; set; }

        /// <summary> 
        /// Create Author Repository with database Connection 
        /// </summary>
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
                try
                {
                    context.Open();
                    id = (int)insertCmd.ExecuteScalar();
                    context.Close();
                }
                catch (Exception) { throw new InsertException(); }
            }
            if (id < 0) throw new AuthorAddException();
            return new Author(id, a.Firstname, a.Surname);
        }

        /// <summary> 
        /// Get an Author by Id
        /// </summary>
        public Author GetByID(int id)
        {
            try
            {
                context.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Authors] WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                    return table.AsEnumerable().Select(a => new Author(a.Field<int>("Id"), a.Field<string>("Firstname"), a.Field<string>("Lastname"))).Single<Author>();
            }
            catch (Exception) { throw new QueryException(); }
            return null;
        }

        /// <summary> 
        /// Get list of all authors 
        /// </summary>
        public List<Author> GetAll()
        {
            try
            {
                context.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Authors]", this.context);
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                    return table.AsEnumerable().Select(a => new Author(a.Field<int>("Id"), a.Field<string>("Firstname"), a.Field<string>("Lastname"))).ToList<Author>();
            }
            catch (Exception) { throw new QueryException(); }
            return new List<Author>();
        }

        /// <summary> 
        /// Delete Author by ID 
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Authors] WHERE Id = @Id;DELETE FROM [dbo].[ComicstripAuthors] WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary>
        /// Delete all Authors
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[Authors];TRUNCATE TABLE [dbo].[ComicstripAuthors]", this.context);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Update existing Publisher 
        /// </summary>
        public void Update(Author a)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Authors] SET Firstname = @Firstname, Lastname = @Lastname WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Firstname", a.Firstname);
                cmd.Parameters.AddWithValue("@Lastname", a.Surname);
                cmd.Parameters.AddWithValue("@Id", a.ID);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Check if Publisher exist
        /// </summary>
        public bool Exist(Author a, bool ignoreId = false)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Authors] WHERE LOWER(Firstname) = @Firstname AND LOWER(Lastname) = @Lastname OR Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Firstname", a.Firstname.ToLower());
                cmd.Parameters.AddWithValue("@Lastname", a.Surname.ToLower());
                cmd.Parameters.AddWithValue("@Id", (!ignoreId) ? a.ID : -1);
                context.Open();
                int count = (int)cmd.ExecuteScalar();
                context.Close();
                return (count > 0);
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Check if Author is included at Comicstrips
        /// </summary>
        public bool HasStrips(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[ComicstripAuthors] WHERE Author_Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                context.Open();
                int count = (int)cmd.ExecuteScalar();
                context.Close();
                return (count > 0);
            }
            catch (Exception) { throw new QueryException(); }
        }

        public class AuthorAddException : Exception
        {
            public AuthorAddException() : base(String.Format("The author was not created")) { }
        }
    }
}