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
    public class RecruiterManager
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

        public Recruiter GetRecruiterById(int id)
        {
            Recruiter recruiter = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [recruiter_id],
                                                [recruiter_name],
                                                [recruiter_email],
                                                [recruiter_password]
                                        FROM [dbo].[recruiter]
                                        WHERE recruiter_id = @recruiter_id";
                    
                    cmd.AddParameter("@recruiter_id", DbType.Int32, id);
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recruiter = new Recruiter()
                            {
                                RecruiterId = reader.GetInt32(0),
                                RecruiterName = reader.GetString(1),
                                RecruiterEmail = reader.GetString(2),
                                RecruiterPassword = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return recruiter;
        }

        public Recruiter GetRecruiterByEmail(string email)
        {
            Recruiter recruiter = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [recruiter_id],
                                                [recruiter_name],
                                                [recruiter_email],
                                                [recruiter_password]
                                        FROM [dbo].[recruiter]
                                        WHERE recruiter_email = @recruiter_email";

                    cmd.AddParameter("@recruiter_email", DbType.String, email);
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recruiter = new Recruiter()
                            {
                                RecruiterId = reader.GetInt32(0),
                                RecruiterName = reader.GetString(1),
                                RecruiterEmail = reader.GetString(2),
                                RecruiterPassword = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return recruiter;
        }

        public void CreateRecruiter(Recruiter recruiter)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpCreateRecruiter";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.AddParameter("@recruiter_name", DbType.String, recruiter.RecruiterName);
                    cmd.AddParameter("@recruiter_email", DbType.String, recruiter.RecruiterEmail);
                    cmd.AddParameter("@recruiter_password", DbType.String, recruiter.RecruiterPassword);
                    
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