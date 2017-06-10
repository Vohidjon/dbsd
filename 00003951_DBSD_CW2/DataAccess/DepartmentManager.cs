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
    public class DepartmentManager
    {
        public static string ConnectionStr {
            get
            {
                return WebConfigurationManager
                    .ConnectionStrings["ConnStr"]
                    .ConnectionString;
            }
        }

        public IList<Department> GetDepartments()
        {
            IList<Department> list = new List<Department>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [department_id]
                                                ,[department_name]
                                                FROM [dbo].[department]";
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Department department = new Department()
                            {
                                DepartmentId = reader.GetInt32(0),
                                DepartmentName = reader.GetString(1)
                            };
                            list.Add(department);
                        }
                    }
                }
            
            }
            return list;
        }

        public void CreateDepartment(Department department)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpCreateDepartment";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    cmd.AddParameter("@department_name", System.Data.DbType.Int32, department.DepartmentName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Department GetDepartmentById(int id)
        {
            Department department = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [department_id],
                                                [department_name]
                                        FROM [dbo].[department]
                                        WHERE department_id = @department_id";
                    cmd.AddParameter("@department_id", System.Data.DbType.Int32, id);
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            department = new Department()
                            {
                                DepartmentId = reader.GetInt32(0),
                                DepartmentName = reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return department;
        }


        public void DeleteDepartmentById(int id)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[department]
                                        WHERE department_id = @department_id";
                   
                    cmd.AddParameter("@department_id", System.Data.DbType.Int32, id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateDepartment(Department department)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpUpdateDepartment";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.AddParameter("@department_id", System.Data.DbType.Int32, department.DepartmentId);
                    cmd.AddParameter("@department_name", System.Data.DbType.String, department.DepartmentName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}