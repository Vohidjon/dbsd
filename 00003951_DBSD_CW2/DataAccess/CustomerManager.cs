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
    public class CustomerManager
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

        public Customer GetCustomerById(int id)
        {
            Customer customer = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id],
                                                [full_name],
                                                [email],
                                                [password]
                                        FROM [dbo].[customer]
                                        WHERE id = @id";
                    
                    cmd.AddParameter("@id", DbType.Int32, id);
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = new Customer()
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Email = reader.GetString(2),
                                Password = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return customer;
        }

        public IList<Customer> GetCustomers()
        {
            IList<Customer> list = new List<Customer>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id],
                                                [full_name],
                                                [email],
                                                [password]
                                        FROM [dbo].[customer]";
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer()
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Email = reader.GetString(2),
                                Password = reader.GetString(3)
                            };
                            list.Add(customer);
                        }
                    }
                }

            }
            return list;
        }

        public Customer GetCustomerByEmail(string email)
        {
            Customer customer = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id],
                                                [full_name],
                                                [email],
                                                [password]
                                        FROM [dbo].[customer]
                                        WHERE email = @email";

                    cmd.AddParameter("@email", DbType.String, email);
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = new Customer()
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Email = reader.GetString(2),
                                Password = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return customer;
        }

        public void CreateCustomer(Customer customer)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpCreateCustomer";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.AddParameter("@full_name", DbType.String, customer.FullName);
                    cmd.AddParameter("@email", DbType.String, customer.Email);
                    cmd.AddParameter("@password", DbType.String, customer.Password);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool Authenticate(string email, string password)
        {
            bool authenticated = false;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select dbo.udfLogin(@email, @password)";
                    cmd.AddParameter("@email", DbType.String, email);
                    cmd.AddParameter("@password", DbType.String, password);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            authenticated = reader.GetBoolean(0);
                        }
                    }
                }
            }

            return authenticated;
        }

    }
}