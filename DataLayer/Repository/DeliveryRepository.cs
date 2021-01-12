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
    public class DeliveryRepository : IDeliveryRepository
    {
        public SqlConnection context { get; set; }

        /// <summary> 
        /// Create Delivery Repository with database Connection
        /// </summary>
        public DeliveryRepository(SqlConnection context)
        {
            this.context = context;
        }

        #region ComicStrip
        /// <summary>
        /// Add a new ComicStrip
        /// </summary>
        public Delivery Add(Delivery d)
        {
            int id = -1;
            var cmd = "INSERT INTO [dbo].[Deliveries] (Supplier,Date) VALUES (@Supplier,@Date);SELECT CAST(scope_identity() AS int)";
            using (var insertCmd = new SqlCommand(cmd, this.context))
            {
                insertCmd.Parameters.AddWithValue("@Supplier", d.Supplier);
                insertCmd.Parameters.AddWithValue("@Date", d.Date.ToString());
                try
                {
                    context.Open();
                    id = (int)insertCmd.ExecuteScalar();
                    context.Close();
                }
                catch (Exception) { throw new InsertException(); }
            }
            if (id < 0) throw new DeliveryAddException();
            cmd = "INSERT INTO [dbo].[DeliveryItems] (Delivery_Id,Comicstrip_Id,Quantity) VALUES (@Delivery,@Strip,@Quantity);SELECT CAST(scope_identity() AS int)";
            List<DeliveryItem> items = new List<DeliveryItem>();
            foreach (DeliveryItem item in d.Items)
            {
                using (var insertCmd = new SqlCommand(cmd, this.context))
                {
                    insertCmd.Parameters.AddWithValue("@Delivery", id);
                    insertCmd.Parameters.AddWithValue("@Strip", item.Comicstrip.ID);
                    insertCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                    try
                    {
                        context.Open();
                        int itemId = (int)insertCmd.ExecuteScalar();
                        items.Add(new DeliveryItem(itemId, item.Comicstrip, item.Quantity));
                        context.Close();
                    }
                    catch (Exception) { throw new InsertException(); }
                }
            }
            return new Delivery(id, d.Supplier, d.Date, items);
        }

        /// <summary> 
        /// Get a Delivery by ID
        /// </summary>
        public Delivery GetByID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Deliveries] WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                    return table.AsEnumerable().Select(b => new Delivery(b.Field<int>("Id"), b.Field<string>("Supplier"), Convert.ToDateTime(b.Field<string>("Date")), this.GetItems(b.Field<int>("Id")))).Single<Delivery>();
            }
            catch (Exception) { throw new QueryException(); }
            return null;
        }

        /// <summary> 
        /// Get list of all Deliveries
        /// </summary>
        public List<Delivery> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Deliveries]", this.context);
                context.Open();
                SqlDataAdapter reader = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                reader.Fill(table);
                context.Close();
                if (table.Rows.Count > 0)
                    return table.AsEnumerable().Select(b => new Delivery(b.Field<int>("Id"), b.Field<string>("Supplier"), Convert.ToDateTime(b.Field<string>("Date")), this.GetItems(b.Field<int>("Id")))).ToList<Delivery>();
            }
            catch (Exception) { throw new QueryException(); }
            return new List<Delivery>();
        }

        /// <summary> 
        /// Delete Delivery by ID 
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Deliveries] WHERE Id = @Id;DELETE FROM [dbo].[DeliveryItems] WHERE Delivery_Id = @Delivery;", this.context);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Delivery", id);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Delete all Deliveries 
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[Deliveries];TRUNCATE TABLE [dbo].[DeliveryItems]", this.context);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Update existing Delivery 
        /// </summary>
        public void Update(Delivery d)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Deliveries] SET Supplier = @Supplier, Date = @Date WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Supplier", d.Supplier);
                cmd.Parameters.AddWithValue("@Date", d.Date);
                cmd.Parameters.AddWithValue("@Id", d.ID);
                context.Open();
                cmd.ExecuteNonQuery();
                context.Close();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Check if Delivery exist
        /// </summary>
        public bool Exist(Delivery d, bool ignoreId = false)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Deliveries] WHERE Id = @Id", this.context);
                cmd.Parameters.AddWithValue("@Id", (!ignoreId) ? d.ID : -1);
                context.Open();
                int count = (int)cmd.ExecuteScalar();
                context.Close();
                return (count > 0);
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Get list of all Delivery Items
        /// </summary>
        public List<DeliveryItem> GetItems(int id)
        {
            var cmd = "SELECT * FROM [dbo].[DeliveryItems] WHERE Delivery_Id = @Delivery";
            using (var selectCmd = new SqlCommand(cmd, this.context))
            {
                try
                {
                    selectCmd.Parameters.AddWithValue("@Delivery", id);
                    this.context.Open();
                    SqlDataAdapter reader = new SqlDataAdapter(selectCmd);
                    DataTable table = new DataTable();
                    reader.Fill(table);
                    this.context.Close();
                    if (table.Rows.Count > 0)
                    {
                        ComicStripRepository sRepoo = new ComicStripRepository(this.context);
                        return table.AsEnumerable().Select(x => new DeliveryItem(x.Field<int>("Id"), sRepoo.GetByID(x.Field<int>("Comicstrip_Id")), x.Field<int>("Quantity"))).ToList<DeliveryItem>();
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Something went wrong while retrieving the delivery items");
                }
            }
            return new List<DeliveryItem>();
        }
        #endregion

        #region Custom Exceptions
        public class DeliveryAddException : Exception
        {
            public DeliveryAddException() : base(String.Format("The delivery was not created")) { }
        }
        #endregion
    }
}
