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
                                DeliveryPhone = reader.IsDBNull(4) ? null : reader.GetString(4),
                                ProcessStatus = reader.GetInt32(5)
                            };
                        }
                    }
                }
            }
            return order;
        }

        public IList<OrderItem> GetOrderItems(int id)
        {
            IList<OrderItem> list = new List<OrderItem>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                                ,[order_id]
                                                ,[flower_id]
                                                ,[quantity]
                                                
                                                FROM [dbo].[order_item]
                                                WHERE [order_id] = @order_id";
                    conn.Open();
                    cmd.AddParameter("@order_id", System.Data.DbType.Int32, id);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderItem orderItem = new OrderItem()
                            {
                                Id = reader.GetInt32(0),
                                OrderId = reader.GetInt32(1),
                                FlowerId = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3)
                            };
                            list.Add(orderItem);
                        }
                    }
                }

            }
            return list;
        }

        public void CreateOrderItem(OrderItem orderItem)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpCreateOrderItem";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.AddParameter("@order_id", DbType.Int32, orderItem.OrderId);
                    cmd.AddParameter("@flower_id", DbType.Int32, orderItem.FlowerId);
                    cmd.AddParameter("@quantity", DbType.Int32, orderItem.Quantity);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FlowerOrder GetLastFlowerOrderFOrCustomer(int customerId)
        {
            FlowerOrder order = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT TOP 1 [id]
                                                ,[customer_id]
                                                ,[created_at]
                                                ,[delivery_address]
                                                ,[delivery_phone]
                                                ,[process_status]

                                        FROM [dbo].[flower_order]
                                        WHERE customer_id = @customer_id
                                        ORDER BY id DESC";
                    conn.Open();
                    cmd.AddParameter("@customer_id", System.Data.DbType.Int32, customerId);
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
                                DeliveryPhone = reader.IsDBNull(4) ? null : reader.GetString(4),
                                ProcessStatus = reader.GetInt32(5)
                            };
                        }
                    }
                }
            }
            return order;
        }

        public void CreateOrder(FlowerOrder order)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpCreateOrder";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.AddParameter("@customer_id", DbType.Int32, order.CustomerId);
                    cmd.AddParameter("@created_at", DbType.DateTime, order.CreatedAt);
                    cmd.AddParameter("@delivery_address", DbType.String, order.DeliveryAddress);
                    cmd.AddParameter("@delivery_phone", DbType.String, order.DeliveryPhone);
                    cmd.AddParameter("@process_status", DbType.Int32, order.ProcessStatus);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
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
                                ProcessStatus = reader.GetInt32(5)
                            };
                            list.Add(order);
                        }
                    }
                }

            }
            return list;
        }

        public IList<FlowerPurchaseReport> FlowerPurchaseReport(int customerId)
        {
            IList<FlowerPurchaseReport> report = new List<FlowerPurchaseReport>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM udfPurchasesReport(@customer_id)";

                    cmd.AddParameter("@customer_id", DbType.Int32, customerId);

                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlowerPurchaseReport reportItem = new FlowerPurchaseReport()
                            {
                                FlowerId = reader.GetInt32(0),
                                FlowerName = reader.GetString(1),
                                Amount = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                                FlowersCount = reader.GetInt32(3)
                            };
                            report.Add(reportItem);
                        }
                    }
                }
            }
            return report;
        }

    }
}