using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _00003951_DBSD_CW2.Models
{
    public class FlowerPurchaseReport
    {
        public int FlowerId { get; set; }
        public string FlowerName { get; set; }
        public double Amount { get; set; }
        
        public int FlowersCount { get; set; }
    }
}