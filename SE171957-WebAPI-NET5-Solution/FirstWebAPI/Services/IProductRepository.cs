using FirstWebAPI.Models;
using FirstWebAPI.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Services
{
    public interface IProductRepository
    {
        List<ProductResponse> SearchProducts(string searchValue,
            double? from, double? to,
            string sortBy, int page, int pagesize);

        bool Add(ProductModel product);
    }
}
