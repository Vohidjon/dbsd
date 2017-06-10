using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _00003951_DBSD_CW2.Models
{
    public class FlowerOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DeliveryAddress { get; set; }
        /**
         * The number used when delivering
         */
        public string DeliveryPhone { get; set; }
        public bool IsGift { get; set; }
        /**
         * This text is attached to the flower if the order is a gift
         * For example: Happy birthday!!! Your family
         */
        public string GiftCardText { get; set; }
        public int ProcessStatus { get; set; }
        public Customer Customer { get; set; }
    }
}