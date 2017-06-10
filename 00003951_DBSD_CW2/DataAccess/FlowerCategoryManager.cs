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
    public class FlowerCategoryManager
    {
        public static string ConnectionStr {
            get
            {
                return WebConfigurationManager
                    .ConnectionStrings["ConnStr"]
                    .ConnectionString;
            }
        }

        public IList<FlowerCategory> GetFlowerCategories()
        {
            IList<FlowerCategory> list = new List<FlowerCategory>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                                ,[name]
                                                FROM [dbo].[flower_category]";
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlowerCategory category = new FlowerCategory()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            list.Add(category);
                        }
                    }
                }
            
            }
            return list;
        }


        public FlowerCategory GetFlowerCategoryById(int id)
        {
            FlowerCategory category = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id],
                                                [name]
                                        FROM [dbo].[flower_category]
                                        WHERE id = @id";
                    cmd.AddParameter("@id", System.Data.DbType.Int32, id);
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            category = new FlowerCategory()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return category;
        }

    }
}