using _00003951_DBSD_CW2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace _00003951_DBSD_CW2.DataAccess
{
    public class ShoppingCartManager
    {
        public static string ConnectionStr
        {
            get
            {
                return WebConfigurationManager
                    .ConnectionStrings["ConnStr"]
                    .ConnectionString;
            }
        }

        public IList<ShoppingCartItem> GetItemsByCustomer(int customerId)
        {
            IList<ShoppingCartItem> list = new List<ShoppingCartItem>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                                ,[customer_id]
                                                ,[flower_id]
                                                ,[quantity]

                                                FROM [dbo].[shopping_cart_item]
                                                WHERE [customer_id] = @customer_id";
                    conn.Open();
                    cmd.AddParameter("@customer_id", System.Data.DbType.Int32, customerId);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShoppingCartItem item = new ShoppingCartItem()
                            {
                                Id = reader.GetInt32(0),
                                CustomerId = reader.GetInt32(1),
                                FlowerId = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3)
                            };
                            list.Add(item);
                        }
                    }
                }

            }
            return list;
        }

        public void CreateItem(ShoppingCartItem cartItem)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpCreateShoppingCartItem";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.AddParameter("@customer_id", DbType.Int32, cartItem.CustomerId);
                    cmd.AddParameter("@flower_id", DbType.Int32, cartItem.FlowerId);
                    cmd.AddParameter("@quantity", DbType.Int32, cartItem.Quantity);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteItem(int id, int customerId)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[shopping_cart_item] WHERE [id] = @id AND [customer_id] = @customer_id";
                    
                    cmd.AddParameter("@id", DbType.Int32, id);
                    cmd.AddParameter("@customer_id", DbType.Int32, customerId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateItem(int id, ShoppingCartItem cartItem)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpUpdateShoppingCartItem";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.AddParameter("@id", DbType.Int32, id);
                    cmd.AddParameter("@quantity", DbType.Int32, cartItem.Quantity);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public ShoppingCartItem GetItemById(int id, int customerId)
        {
            ShoppingCartItem item = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id],
                                                [customer_id],
                                                [flower_id],
                                                [quantity]
                                        FROM [dbo].[shopping_cart_item]
                                        WHERE id = @id and customer_id = @customer_id";

                    cmd.AddParameter("@id", DbType.Int32, id);
                    cmd.AddParameter("@customer_id", DbType.Int32, customerId);

                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            item = new ShoppingCartItem()
                            {
                                Id = reader.GetInt32(0),
                                CustomerId = reader.GetInt32(1),
                                FlowerId = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }
            return item;
        }

        public ShoppingCartItem GetItemByFlowerId(int flowerId, int customerId)
        {
            ShoppingCartItem item = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id],
                                                [customer_id],
                                                [flower_id],
                                                [quantity]
                                        FROM [dbo].[shopping_cart_item]
                                        WHERE flower_id = @flower_id and customer_id = @customer_id";

                    cmd.AddParameter("@flower_id", DbType.Int32, flowerId);
                    cmd.AddParameter("@customer_id", DbType.Int32, customerId);

                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            item = new ShoppingCartItem()
                            {
                                Id = reader.GetInt32(0),
                                CustomerId = reader.GetInt32(1),
                                FlowerId = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }
            return item;
        }
    }
}