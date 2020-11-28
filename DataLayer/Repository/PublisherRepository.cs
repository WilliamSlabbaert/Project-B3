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
        //Set Given connection (Connection is given in UnitOfWork)
        public PublisherRepository(SqlConnection context)
        {
            this.context = context;
        }

        // Add Publisher (command)
        // Generate unique id (cmd)
        public void Add(Publisher dataPublisher)
        {
            context.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Publisher] (Id,Name) VALUES (@Id,@Name)", context);
            SqlCommand cmd = new SqlCommand("SELECT TOP 1(Id) FROM [dbo].[Publisher] ORDER BY Id DESC", context);
            int i = -1;
            var x = cmd.ExecuteScalar();

            if (x != null)
                i = Convert.ToInt32(x);
            i++;

            command.Parameters.AddWithValue("@Name", dataPublisher.Name);
            command.Parameters.AddWithValue("@Id", i);
            command.ExecuteNonQuery();
            context.Close();

        }
        //Delete Publisher by id with MySql (command)
        public void DeleteByID(int ID)
        {
            context.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Publisher] WHERE Id = "+ID , context);
            command.ExecuteNonQuery();
            context.Close();
        }

        //Get All Publishers with MySql (command)
        public List<Publisher> GetAll()
        {
            List<Publisher> temp_list = new List<Publisher>();
            context.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Publisher]", context);
            SqlDataAdapter reader = new SqlDataAdapter(command);
            
            DataTable dt = new DataTable();
            reader.Fill(dt);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow item in dt.Rows) 
                {
                    var temp = new Publisher( item["Name"].ToString());
                    temp.SetID(Convert.ToInt32(item["Id"].ToString()));
                    temp_list.Add(temp);
                }
                context.Close();
                return temp_list;
            }
            else
                return null;
        }

        //Get Publisher by Id with MySql (command)
        public Publisher GetByID(int ID)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Publisher] WHERE Id = " + ID, context);
            SqlDataAdapter reader = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            reader.Fill(dt);

            if (dt.Rows[0] != null)
            {
                var temp = new Publisher(dt.Rows[0]["Name"].ToString());
                temp.SetID(Convert.ToInt32(dt.Rows[0]["Id"].ToString()));
                return temp;
            }
            else
                return null;
        }

        // Truncate table "Delete's everthing" (cmd)
        public void DeleteAll()
        {
            context.Open();
            SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[Publisher]", context);
            cmd.ExecuteNonQuery();
            context.Close();

        }
    }
}
