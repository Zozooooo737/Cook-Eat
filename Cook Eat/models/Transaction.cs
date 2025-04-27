using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cook_Eat.models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Amount { get; set; }
        public bool IsDelivered { get; set; }
        public int ClientId { get; set; }
    }
}
