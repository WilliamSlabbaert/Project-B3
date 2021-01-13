using BusinessLayer.IRepository;
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
    public class OrderRepository : IOrderRepository
    {
        public SqlConnection context { get; set; }

        /// <summary> 
        /// Create Order Repository with database Connection
        /// </summary>
        public OrderRepository(SqlConnection context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add a new Delivery
        /// </summary>
        public Order Add(Order o)
        {
            int id = -1;
            var cmd = "INSERT INTO [dbo].[Orders] (FirstName,LastName,Email,Phone,Date) VALUES (@Firstname,@Lastname,@Email,@Phone,@Date);SELECT CAST(scope_identity() AS int)";
            using (var insertCmd = new SqlCommand(cmd, this.context))
            {
                insertCmd.Parameters.AddWithValue("@Firstname", o.Firstname);
                insertCmd.Parameters.AddWithValue("@Lastname", o.Lastname);
                insertCmd.Parameters.AddWithValue("@Email", o.Email);
                insertCmd.Parameters.AddWithValue("@Phone", o.Phone);
                insertCmd.Parameters.AddWithValue("@Date", o.Date.ToString());
                try
                {
                    context.Open();
                    id = (int)insertCmd.ExecuteScalar();
                    context.Close();
                }
                catch (Exception) { throw new InsertException(); }
            }
            if (id < 0) throw new OrderAddException();
            cmd = "INSERT INTO [dbo].[OrderItems] (Order_Id,Comicstrip_Id,Quantity) VALUES (@Order,@Strip,@Quantity);SELECT CAST(scope_identity() AS int)";
            List<OrderItem> items = new List<OrderItem>();
            foreach (OrderItem item in o.Items)
            {
                using (var insertCmd = new SqlCommand(cmd, this.context))
                {
                    insertCmd.Parameters.AddWithValue("@Order", id);
                    insertCmd.Parameters.AddWithValue("@Strip", item.Comicstrip.ID);
                    insertCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                    try
                    {
                        context.Open();
                        int itemId = (int)insertCmd.ExecuteScalar();
                        items.Add(new OrderItem(itemId, item.Comicstrip, item.Quantity));
                        context.Close();
                    }
                    catch (Exception) { throw new InsertException(); }
                }
            }
            return new Order(id, o.Firstname, o.Lastname, o.Email, o.Phone, o.Date, items);
        }


        /// <summary> 
        /// Get a Order by ID
        /// </summary>
        public Order GetByID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Orders] WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                    return table.AsEnumerable().Select(b => new Order(b.Field<int>("Id"), b.Field<string>("Firstname"), b.Field<string>("Lastname"), b.Field<string>("Email"), b.Field<string>("Phone"), Convert.ToDateTime(b.Field<string>("Date")), this.GetItems(b.Field<int>("Id")))).Single<Order>();
            }
            catch (Exception) { throw new QueryException(); }
            return null;
        }

        /// <summary> 
        /// Get list of all Orders
        /// </summary>
        public List<Order> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Orders]", this.context);
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                    return table.AsEnumerable().Select(b => new Order(b.Field<int>("Id"), b.Field<string>("Firstname"), b.Field<string>("Lastname"), b.Field<string>("Email"), b.Field<string>("Phone"), Convert.ToDateTime(b.Field<string>("Date")), this.GetItems(b.Field<int>("Id")))).ToList<Order>();
            }
            catch (Exception) { throw new QueryException(); }
            return new List<Order>();
        }

        /// <summary> 
        /// Delete Order by ID 
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Orders] WHERE Id = @Id;DELETE FROM [dbo].[OrderItems] WHERE Order_Id = @Order;", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Order", id);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Delete all Orders 
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[Orders];TRUNCATE TABLE [dbo].[OrderItems]", this.context);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Update existing Order 
        /// </summary>
        public void Update(Order o)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Orders] SET FirstName = @Firstname, LastName = @Lastname, Email = @Email, Phone = @Phone, Date = @Date WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Firstname", o.Firstname);
                cmd.Parameters.AddWithValue("@Lastname", o.Lastname);
                cmd.Parameters.AddWithValue("@Email", o.Email);
                cmd.Parameters.AddWithValue("@Phone", o.Phone);
                cmd.Parameters.AddWithValue("@Date", o.Date);
                cmd.Parameters.AddWithValue("@Id", o.ID);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Check if Order exist
        /// </summary>
        public bool Exist(Order o, bool ignoreId = false)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Orders] WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", (!ignoreId) ? o.ID : -1);
                context.Open();
                int count = (int)cmd.ExecuteScalar();
                context.Close();
                return (count > 0);
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Get list of all Order Items
        /// </summary>
        public List<OrderItem> GetItems(int id)
        {
            var cmd = "SELECT * FROM [dbo].[OrderItems] WHERE Order_Id = @Order";
            using (var selectCmd = new SqlCommand(cmd, this.context))
            {
                try
                {
                    selectCmd.Parameters.AddWithValue("@Order", id);
                    this.context.Open();
                    SqlDataAdapter reader = new SqlDataAdapter(selectCmd);
                    DataTable table = new DataTable();
                    reader.Fill(table);
                    this.context.Close();
                    if (table.Rows.Count > 0)
                    {
                        ComicStripRepository sRepoo = new ComicStripRepository(this.context);
                        return table.AsEnumerable().Select(x => new OrderItem(x.Field<int>("Id"), sRepoo.GetByID(x.Field<int>("Comicstrip_Id")), x.Field<int>("Quantity"))).ToList<OrderItem>();
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Something went wrong while retrieving the order items");
                }
            }
            return new List<OrderItem>();
        }

        #region Custom Exceptions
        public class OrderAddException : Exception
        {
            public OrderAddException() : base(String.Format("The order was not created")) { }
        }
        #endregion
    }
}
