using _00003951_DBSD_CW2.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace _00003951_DBSD_CW2.DataAccess
{
    public class FlowerOrderManager
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

        public FlowerOrder GetFlowerOrderById(int id)
        {
            FlowerOrder order = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                                ,[customer_id]
                                                ,[created_at]
                                                ,[delivery_address]
                                                ,[delivery_phone]
                                                ,[is_gift]
                                                ,[gift_card_text]
                                                ,[process_status]

                                        FROM [dbo].[flower_order]
                                        WHERE [id] = @id";
                    conn.Open();
                    cmd.AddParameter("@id", System.Data.DbType.Int32, id);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            order = new FlowerOrder()
                            {
                                Id = reader.GetInt32(0),
                                CustomerId = reader.GetInt32(1),
                                CreatedAt = reader.GetDateTime(2),
                                DeliveryAddress = reader.GetString(3),
                                DeliveryPhone = reader.GetString(4),
                                IsGift = reader.GetBoolean(5),
                                GiftCardText = reader.GetString(6),
                                ProcessStatus = reader.GetInt32(7)
                            };
                        }
                    }
                }
            }
            return order;
        }
        public IList<FlowerOrder> GetFlowerOrdersByCustomer(int customerId)
        {
            IList<FlowerOrder> list = new List<FlowerOrder>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                                ,[customer_id]
                                                ,[created_at]
                                                ,[delivery_address]
                                                ,[delivery_phone]
                                                ,[is_gift]
                                                ,[gift_card_text]
                                                ,[process_status]

                                                FROM [dbo].[flower_order]
                                                WHERE [customer_id] = @customer_id
                                                ORDER BY [created_at] ASC";
                    conn.Open();
                    cmd.AddParameter("@customer_id", System.Data.DbType.Int32, customerId);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlowerOrder order = new FlowerOrder()
                            {
                                Id = reader.GetInt32(0),
                                CustomerId = reader.GetInt32(1),
                                CreatedAt = reader.GetDateTime(2),
                                DeliveryAddress = reader.GetString(3),
                                DeliveryPhone = reader.IsDBNull(4) ? null : reader.GetString(4),
                                IsGift = reader.GetBoolean(5),
                                GiftCardText = reader.IsDBNull(6) ? null : reader.GetString(6),
                                ProcessStatus = reader.GetInt32(7)
                            };
                            list.Add(order);
                        }
                    }
                }

            }
            return list;
        }
    }
}