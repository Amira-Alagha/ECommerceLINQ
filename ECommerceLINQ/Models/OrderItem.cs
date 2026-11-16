using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceLINQ.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; } // 0.10 = 10%
        public decimal LineTotal =>
       Quantity * UnitPrice * (1 - Discount);
    }
}
