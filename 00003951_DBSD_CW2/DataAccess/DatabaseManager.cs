using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace _00003951_DBSD_CW2.DataAccess
{
    public class DatabaseManager
    {
        public static string ConnectionStr
        {
            get
            {
                return WebConfigurationManager
                    .ConnectionStrings["ConnStrDB"]
                    .ConnectionString;
            }
        }

        public static bool DatabaseExists()
        {
            try
            {
                var allCustomers = new CustomerManager().GetCustomers();
                return allCustomers.Count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void CreateDatabase()
        {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/00003951_DBSD_CW2_SEM9.sql");

            string script = File.ReadAllText(path);

            SqlConnection conn = new SqlConnection(ConnectionStr);

            Server server = new Server(new ServerConnection(conn));

            server.ConnectionContext.ExecuteNonQuery(script);
        }

        public static void CreateDatabaseIfNotExists()
        {
            if (!DatabaseExists())
            {
                CreateDatabase();
            }
        }

    }
}