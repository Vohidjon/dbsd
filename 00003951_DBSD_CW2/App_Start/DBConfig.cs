using _00003951_DBSD_CW2.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _00003951_DBSD_CW2.App_Start
{
    public class DBConfig
    {
        public static void initialize()
        {
            DBInitializer initializer = new DBInitializer();
            initializer.Run();
        }
    }
}