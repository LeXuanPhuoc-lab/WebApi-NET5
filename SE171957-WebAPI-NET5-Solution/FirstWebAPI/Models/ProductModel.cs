using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Models
{
    public class ProductDetail
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public string Description { get; set; }
        public Byte Voucher { get; set; }

        public ProductDetail() { }
        public ProductDetail(string Name, double Price)
        {
            Name = Name;
            Price = Price;
        }
    }

    public class ProductModel : ProductDetail
    {
        public Guid Id { get; set; }

        public ProductModel() { }
        public ProductModel(int Id, string Name, double Price):base(Name, Price)
        {
            Id = Id;
        }
    }
}
