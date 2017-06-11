using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _00003951_DBSD_CW2.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int FlowerId { get; set; }
        public int Quantity { get; set; }
        public Customer Customer { get; set; }
        public Flower Flower { get; set; }
    }
}