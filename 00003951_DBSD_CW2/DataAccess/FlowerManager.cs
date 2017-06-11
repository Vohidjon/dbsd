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
    public class FlowerManager
    {
        public static string ConnectionStr {
            get
            {
                return WebConfigurationManager
                    .ConnectionStrings["ConnStr"]
                    .ConnectionString;
            }
        }

        public IList<Flower> GetFlowers()
        {
            IList<Flower> list = new List<Flower>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                                ,[name]
                                                ,[description]
                                                ,[img_url]
                                                ,[price]
                                                ,[remaining]
                                                ,[flower_category_id]
                                                FROM [dbo].[flower]";
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flower flower = new Flower()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                ImgUrl = reader.GetString(3),
                                Price = reader.GetDouble(4),
                                Remaining = reader.GetInt32(5),
                                FlowerCategoryId = reader.GetInt32(6)
                            };
                            list.Add(flower);
                        }
                    }
                }
            
            }
            return list;
        }

        public IList<Flower> FilterFlowers(int? categoryId, string name, string description)
        {
            IList<Flower> list = new List<Flower>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    if (name == null)
                    {
                        name = "";
                    }
                    if (description == null)
                    {
                        description = "";
                    }
                    cmd.CommandText = @"SELECT [id]
                                                ,[name]
                                                ,[description]
                                                ,[img_url]
                                                ,[price]
                                                ,[remaining]
                                                ,[flower_category_id]
                                                FROM [dbo].[flower]
                                                WHERE
                                                name LIKE '%' + @name + '%' 
                                                AND description LIKE  '%' + @description + '%'" ;

                    conn.Open();

                    cmd.AddParameter("@name", System.Data.DbType.String, name);
                    cmd.AddParameter("@description", System.Data.DbType.String, description);

                    if (categoryId != null)
                    {
                        cmd.CommandText += " AND flower_category_id = @category_id";
                        cmd.AddParameter("@category_id", System.Data.DbType.Int32, categoryId);
                    } 
                    
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flower flower = new Flower()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                ImgUrl = reader.GetString(3),
                                Price = reader.GetDouble(4),
                                Remaining = reader.GetInt32(5),
                                FlowerCategoryId = reader.GetInt32(6)
                            };
                            list.Add(flower);
                        }
                    }
                }

            }
            return list;
        }

        public void CreateFlower(Flower flower)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpCreateFlower";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    cmd.AddParameter("@name", System.Data.DbType.String, flower.Name);
                    cmd.AddParameter("@description", System.Data.DbType.String, flower.Description);
                    cmd.AddParameter("@img_url", System.Data.DbType.String, flower.ImgUrl);
                    cmd.AddParameter("@price", System.Data.DbType.Double, flower.Price);
                    cmd.AddParameter("@remaining", System.Data.DbType.Int32, flower.Remaining);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Flower GetFlowerById(int id)
        {
            Flower flower = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                                ,[name]
                                                ,[description]
                                                ,[img_url]
                                                ,[price]
                                                ,[remaining]
                                                ,[flower_category_id]
                                        FROM [dbo].[flower]
                                        WHERE id = @id";
                    cmd.AddParameter("@id", System.Data.DbType.Int32, id);
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            flower = new Flower()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                ImgUrl = reader.GetString(3),
                                Price = reader.GetDouble(4),
                                Remaining = reader.GetInt32(5),
                                FlowerCategoryId = reader.GetInt32(6)
                            };
                        }
                    }
                }
            }
            return flower;
        }


        public void DeleteFlowerById(int id)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[flower]
                                        WHERE id = @id";
                   
                    cmd.AddParameter("@id", System.Data.DbType.Int32, id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateFlower(int id, Flower flower)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpUpdateFlower";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.AddParameter("@id", System.Data.DbType.Int32, id);
                    cmd.AddParameter("@name", System.Data.DbType.String, flower.Name);
                    cmd.AddParameter("@description", System.Data.DbType.String, flower.Description);
                    cmd.AddParameter("@img_url", System.Data.DbType.String, flower.ImgUrl);
                    cmd.AddParameter("@price", System.Data.DbType.Double, flower.Price);
                    cmd.AddParameter("@remaining", System.Data.DbType.Int32, flower.Remaining);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}