using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _00003951_DBSD_CW2.Models
{
    public class FlowerWrapper
    {
        public Flower flower { get; set; }
        public IList<FlowerCategory> categories { get; set; }
    }
}