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
    public class ApplicationManager
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
        public IList<Application> GetApplications(int vacancyId)
        {
            IList<Application> list = new List<Application>();
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [application_id],
                                               [vacancy_id],
                                               [stage_id],
                                               [candidate_email],
                                               [candidate_name],
                                               [candidate_phone],
                                               [candidate_address],
                                               [is_disqualified],
                                               [application_created_at]
                                               FROM [dbo].[application]
                                               WHERE vacancy_id = @vacancy_id AND is_disqualified = 0";
                    conn.Open();
                    cmd.AddParameter("@vacancy_id", System.Data.DbType.Int32, vacancyId);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Application application = new Application()
                            {
                                ApplicationId = reader.GetInt32(0),
                                VacancyId = reader.GetInt32(1),
                                StageId = reader.GetInt32(2),
                                CandidateEmail = reader.GetString(3),
                                CandidateName = reader.GetString(4),
                                CandidatePhone = reader.GetString(5),
                                CandidateAddress = reader.GetString(6),
                                IsDisqualified = reader.GetBoolean(7),
                                ApplicationCreatedAt = reader.GetDateTime(8)
                            };
                            list.Add(application);
                        }
                    }
                }

            }
            return list;
        }

        public Application GetApplicationById(int id)
        {
            Application application = null;
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [application_id],
                                               [vacancy_id],
                                               [stage_id],
                                               [candidate_email],
                                               [candidate_name],
                                               [candidate_phone],
                                               [candidate_address],
                                               [is_disqualified],
                                               [application_created_at]
                                               FROM [dbo].[application]
                                        WHERE application_id = @application_id";

                    cmd.AddParameter("@application_id", DbType.Int32, id);
                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            application = new Application()
                            {
                                ApplicationId = reader.GetInt32(0),
                                VacancyId = reader.GetInt32(1),
                                StageId = reader.GetInt32(2),
                                CandidateEmail = reader.GetString(3),
                                CandidateName = reader.GetString(4),
                                CandidatePhone = reader.GetString(5),
                                CandidateAddress = reader.GetString(6),
                                IsDisqualified = reader.GetBoolean(7),
                                ApplicationCreatedAt = reader.GetDateTime(8)
                            };
                        }
                    }
                }
            }
            return application;
        }
        public void CreateApplication(Application application)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"udpCreateApplication";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.AddParameter("@vacancy_id", System.Data.DbType.Int32, application.VacancyId);
                    cmd.AddParameter("@stage_id", System.Data.DbType.Int32, application.StageId);
                    cmd.AddParameter("@candidate_email", System.Data.DbType.String, application.CandidateEmail);
                    cmd.AddParameter("@candidate_name", System.Data.DbType.String, application.CandidateName);
                    cmd.AddParameter("@candidate_phone", System.Data.DbType.String, application.CandidatePhone);
                    cmd.AddParameter("@candidate_address", System.Data.DbType.String, application.CandidateAddress);
                    cmd.AddParameter("@application_created_at", System.Data.DbType.DateTime, application.ApplicationCreatedAt);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void MoveToStage(int id, int stageId)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE [dbo].[application]
                                        SET [stage_id] = @stage_id
                                        WHERE application_id = @application_id";
                    
                    cmd.AddParameter("@application_id", System.Data.DbType.Int32, id);
                    cmd.AddParameter("@stage_id", System.Data.DbType.Int32, stageId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Disqualify(int id)
        {
            using (DbConnection conn = new SqlConnection(ConnectionStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE [dbo].[application]
                                        SET [is_disqualified] = 1
                                        WHERE application_id = @application_id";

                    cmd.AddParameter("@application_id", System.Data.DbType.Int32, id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}