using System;
using System.Collections.Generic;

namespace ReactNetCoreDB.Models
{
    public partial class TransactionHistory
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
    }
}
