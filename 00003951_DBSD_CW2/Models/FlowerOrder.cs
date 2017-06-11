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
        private int _ProcessStatus;
        public int ProcessStatus {
            get { return _ProcessStatus; }
            set {
                _ProcessStatus = value;
                switch(value)
                {
                    case 1:
                        {
                            ProcessStatusText = "Under Process";
                            break;
                        }
                    case 2:
                        {
                            ProcessStatusText = "Confirmed";
                            break;
                        }
                    case 3:
                        {
                            ProcessStatusText = "Completed";
                            break;
                        }
                }
            }
        }
        public Customer Customer { get; set; }
        public string ProcessStatusText { get; set; }
        public const int UNDER_PROCESS = 1;
        public const int CONFIRMED = 2;
        public const int COMPLETED = 3;


    }
}