using System.Web;
using System.Web.Mvc;

namespace _00003951_DBSD_CW2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
