using BusinessLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        public void Add(ComicStrip comicStrip)
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 1(Id) FROM [dbo].[ComicStrip] ORDER BY Id DESC", context);
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[ComicStrip] (Id,Titel,Serie,Genre,Comicstrip_Number) VALUES (@Id,@Titel,@Serie,@Genre,@Comicstrip_Number)", context);
            int i = -1;
            var x = cmd.ExecuteScalar();

            if (x != null)
                i = Convert.ToInt32(x);
            i++;

            command.Parameters.AddWithValue("@Id", i);
            command.Parameters.AddWithValue("@Titel", comicStrip.Titel);
            command.Parameters.AddWithValue("@Serie", comicStrip.Serie);
            
            command.Parameters.AddWithValue("@Comicstrip_Number", comicStrip.ComicStripNumber);

            command.ExecuteNonQuery();
            context.Close();
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

        public List<ComicStrip> GetAll()
        {
            List<ComicStrip> temp_list = new List<ComicStrip>();
            context.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[ComicStrip]", context);
            SqlDataAdapter reader = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            reader.Fill(dt);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    var temp = new ComicStrip(item["Titel"].ToString(), item["Serie"].ToString(), Convert.ToInt32(item["Comicstrip_Number"].ToString()));
                    temp.SetID(Convert.ToInt32(item["Id"].ToString()));
                    temp_list.Add(temp);
                }
                context.Close();
                return temp_list;
            }
            else
                return temp_list;
        }

        public ComicStrip GetByID(int ID)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[ComicStrip] WHERE Id = " + ID, context);
            SqlDataAdapter reader = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            reader.Fill(dt);

            if (dt.Rows[0] != null)
            {
                var temp = new ComicStrip(dt.Rows[0]["Titel"].ToString(), dt.Rows[0]["Serie"].ToString(), Convert.ToInt32(dt.Rows[0]["Comicstrip_Number"].ToString()));
                temp.SetID(Convert.ToInt32(dt.Rows[0]["Id"].ToString()));
                return temp;
            }
                
            else
                return null;
        }
    }
}
