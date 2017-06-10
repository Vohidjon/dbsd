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
    public class VacancyManager
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

        public IList<Vacancy> GetVacancies(int recruiterId)
        {
            IList<Vacancy> list = new List<Vacancy>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [vacancy_id],
                                                [department_id],
                                                [recruiter_id],
                                                [vacancy_title],
                                                [vacancy_description],
                                                [vacancy_created_at]
                                                FROM [dbo].[vacancy]
                                                WHERE recruiter_id = @recruiter_id";
                    conn.Open();
                    cmd.AddParameter("@recruiter_id", System.Data.DbType.Int32, recruiterId);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Vacancy vacancy = new Vacancy()
                            {
                                VacancyId = reader.GetInt32(0),
                                DepartmentId = reader.GetInt32(1),
                                RecruiterId = reader.GetInt32(2),
                                VacancyTitle = reader.GetString(3),
                                VacancyDescription = reader.GetString(4),
                                VacancyCreatedAt = reader.GetDateTime(5)
                            };
                            list.Add(vacancy);
                        }
                    }
                }

            }
            return list;
        }

        public IList<Report> GenerateReport(int vacancyId, int recruiterId)
        {
            IList<Report> list = new List<Report>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM udfReport(@vacancy_id, @recruiter_id);";
                    conn.Open();
                    cmd.AddParameter("@vacancy_id", System.Data.DbType.Int32, vacancyId);
                    cmd.AddParameter("@recruiter_id", System.Data.DbType.Int32, recruiterId);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Report report = new Report()
                            {
                                StageId = reader.GetInt32(0),
                                StageName = reader.GetString(1),
                                VacancyTitle = reader.GetString(2),
                                Count = reader.GetInt32(3)
                            };
                            list.Add(report);
                        }
                    }
                }

            }
            return list;
        }

        public IList<Vacancy> FilterVacancies(int recruiterId, string title, string description)
        {
            IList<Vacancy> list = new List<Vacancy>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    if(title == null)
                    {
                        title = "";
                    }
                    if(description == null)
                    {
                        description = "";
                    }
                    cmd.CommandText = @"SELECT [vacancy_id],
                                                [department_id],
                                                [recruiter_id],
                                                [vacancy_title],
                                                [vacancy_description],
                                                [vacancy_created_at]
                                                FROM [dbo].[vacancy]
                                                WHERE
                                                recruiter_id = @recruiter_id 
                                                AND vacancy_title LIKE '%' + @title + '%' 
                                                AND vacancy_description LIKE  '%' + @description + '%'";
                    conn.Open();
                    cmd.AddParameter("@recruiter_id", System.Data.DbType.Int32, recruiterId);
                    cmd.AddParameter("@title", System.Data.DbType.String, title);
                    cmd.AddParameter("@description", System.Data.DbType.String, description);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Vacancy vacancy = new Vacancy()
                            {
                                VacancyId = reader.GetInt32(0),
                                DepartmentId = reader.GetInt32(1),
                                RecruiterId = reader.GetInt32(2),
                                VacancyTitle = reader.GetString(3),
                                VacancyDescription = reader.GetString(4),
                                VacancyCreatedAt = reader.GetDateTime(5)
                            };
                            list.Add(vacancy);
                        }
                    }
                }

            }
            return list;
        }

        public IList<Vacancy> GetAllVacancies(string title, string description)
        {
            IList<Vacancy> list = new List<Vacancy>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    if (title == null)
                    {
                        title = "";
                    }
                    if (description == null)
                    {
                        description = "";
                    }
                    cmd.CommandText = @"SELECT [vacancy_id],
                                                [department_id],
                                                [recruiter_id],
                                                [vacancy_title],
                                                [vacancy_description],
                                                [vacancy_created_at]
                                                FROM [dbo].[vacancy]
                                                WHERE
                                                vacancy_title LIKE '%' + @title + '%' 
                                                AND vacancy_description LIKE  '%' + @description + '%'";
                    conn.Open();
                    cmd.AddParameter("@title", System.Data.DbType.String, title);
                    cmd.AddParameter("@description", System.Data.DbType.String, description);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Vacancy vacancy = new Vacancy()
                            {
                                VacancyId = reader.GetInt32(0),
                                DepartmentId = reader.GetInt32(1),
                                RecruiterId = reader.GetInt32(2),
                                VacancyTitle = reader.GetString(3),
                                VacancyDescription = reader.GetString(4),
                                VacancyCreatedAt = reader.GetDateTime(5)
                            };
                            list.Add(vacancy);
                        }
                    }
                }

            }
            return list;
        }
        public Vacancy GetVacancyById(int id)
        {
            Vacancy vacancy = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [vacancy_id],
                                                [department_id],
                                                [recruiter_id],
                                                [vacancy_title],
                                                [vacancy_description],
                                                [vacancy_created_at]
                                                FROM [dbo].[vacancy]
                                        WHERE vacancy_id = @vacancy_id";
                    cmd.AddParameter("@vacancy_id", System.Data.DbType.Int32, id);
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            vacancy = new Vacancy()
                            {
                                VacancyId = reader.GetInt32(0),
                                DepartmentId = reader.GetInt32(1),
                                RecruiterId = reader.GetInt32(2),
                                VacancyTitle = reader.GetString(3),
                                VacancyDescription = reader.GetString(4),
                                VacancyCreatedAt = reader.GetDateTime(5)
                            };
                        }
                    }
                }
            }
            return vacancy;
        }
        public void CreateVacancy(Vacancy vacancy)
        {
           
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpCreateVacancy";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   
                    cmd.AddParameter("@department_id", System.Data.DbType.Int32, vacancy.DepartmentId);
                    cmd.AddParameter("@recruiter_id", System.Data.DbType.Int32, vacancy.RecruiterId);
                    cmd.AddParameter("@vacancy_title", System.Data.DbType.String, vacancy.VacancyTitle);
                    cmd.AddParameter("@vacancy_description", System.Data.DbType.String, vacancy.VacancyDescription);
                    cmd.AddParameter("@vacancy_created_at", System.Data.DbType.DateTime, DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateVacancy(int id, Vacancy vacancy)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpUpdateVacancy";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.AddParameter("@vacancy_id", System.Data.DbType.Int32, id);
                    cmd.AddParameter("@department_id", System.Data.DbType.Int32, vacancy.DepartmentId);
                    cmd.AddParameter("@vacancy_title", System.Data.DbType.String, vacancy.VacancyTitle);
                    cmd.AddParameter("@vacancy_description", System.Data.DbType.String, vacancy.VacancyDescription);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}