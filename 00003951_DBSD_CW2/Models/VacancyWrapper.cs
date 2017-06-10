using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _00003951_DBSD_CW2.Models
{
    public class VacancyWrapper
    {
        public Vacancy vacancy { get; set; }
        public IList<Department> departments { get; set; }
    }
}