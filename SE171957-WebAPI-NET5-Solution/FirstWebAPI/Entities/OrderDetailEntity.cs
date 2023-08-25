using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Entities
{
    public class OrderDetailEntity
    {
        public Guid ProductId { get; set; }
        public int OrderId { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalPrice { get; set; }

        public ProductEntity product;
        public OrderEntity order;
    }
}
