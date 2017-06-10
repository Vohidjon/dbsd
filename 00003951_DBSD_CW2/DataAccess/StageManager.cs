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
    public class StageManager
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
        public Stage GetFirst()
        {
            Stage stage = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [stage_id],
                                                [stage_name],
                                                [stage_order]
                                        FROM [dbo].[stage]
                                        ORDER BY [stage_order] ASC";
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stage = new Stage()
                            {
                                StageId = reader.GetInt32(0),
                                StageName = reader.GetString(1),
                                StageOrder = reader.GetInt32(2)
                            };
                        }
                    }
                }
            }
            return stage;
        }

        public Stage GetStageById(int id)
        {
            Stage stage = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [stage_id],
                                                [stage_name],
                                                [stage_order]
                                        FROM [dbo].[stage]
                                        WHERE [stage_id] = @stage_id";
                    conn.Open();
                    cmd.AddParameter("@stage_id", System.Data.DbType.Int32, id);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stage = new Stage()
                            {
                                StageId = reader.GetInt32(0),
                                StageName = reader.GetString(1),
                                StageOrder = reader.GetInt32(2)
                            };
                        }
                    }
                }
            }
            return stage;
        }
        public IList<Stage> GetStages()
        {
            IList<Stage> list = new List<Stage>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [stage_id]
                                                ,[stage_name]
                                                ,[stage_order]
                                                FROM [dbo].[stage]
                                                ORDER BY [stage_order] ASC";
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Stage stage = new Stage()
                            {
                                StageId = reader.GetInt32(0),
                                StageName = reader.GetString(1),
                                StageOrder = reader.GetInt32(2)
                            };
                            list.Add(stage);
                        }
                    }
                }

            }
            return list;
        }
    }
}