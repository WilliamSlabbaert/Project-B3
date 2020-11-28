using BusinessLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataLayer
{
    public class AuthorRepository : IAuthorRepository
    {
        public SqlConnection context { get; set; }
        public AuthorRepository(SqlConnection context)
        {
            this.context = context;
        }

        public void Add(Author DataAuthor)
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 1(Id) FROM [dbo].[Author] ORDER BY Id DESC", context);
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Author] (Id,Firstname,Surname) VALUES (@Id,@Firstname,@Surname)", context);
            int i = -1;
            var x = cmd.ExecuteScalar();
            
            if(x != null)
                i = Convert.ToInt32(x);
            i++;

            command.Parameters.AddWithValue("@Id", i);
            command.Parameters.AddWithValue("@Firstname",DataAuthor.Firstname);
            command.Parameters.AddWithValue("@Surname", DataAuthor.Surname);

            command.ExecuteNonQuery();
            context.Close();
        }

        public void DeleteAll()
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[Author]",context);
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

        public List<Author> GetAll()
        {
            List<Author> temp_list = new List<Author>();
            context.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Author]", context);
            SqlDataAdapter reader = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            reader.Fill(dt);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    var temp = new Author(item["Firstname"].ToString(), item["Surname"].ToString());
                    temp.SetID(Convert.ToInt32(item["Id"].ToString()));
                    temp_list.Add(temp);
                }
                context.Close();
                return temp_list;
            }
            else
                return null;
        }

        public Author GetByID(int ID)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Author] WHERE Id = " + ID, context);
            SqlDataAdapter reader = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            reader.Fill(dt);

            if (dt.Rows[0] != null)
            {
                var temp = new Author( dt.Rows[0]["Firstname"].ToString(), dt.Rows[0]["Surname"].ToString());
                temp.SetID(Convert.ToInt32(dt.Rows[0]["Id"].ToString()));
                return temp;
            }
            else
                return null;
        }
    }
}
