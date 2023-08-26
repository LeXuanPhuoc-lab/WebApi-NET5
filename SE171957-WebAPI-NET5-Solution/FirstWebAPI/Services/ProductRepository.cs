using FirstWebAPI.Entities;
using FirstWebAPI.Heplers;
using FirstWebAPI.Models;
using FirstWebAPI.Payload.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Services
{
    public class ProductRepository : IProductRepository
    {

        private readonly DbContextHelper _context;
        public ProductRepository(DbContextHelper context) { _context = context; }

        public bool Add(ProductModel product)
        {
            _context.Products.Add(new ProductEntity
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Voucher = product.Voucher
            });
            int result = _context.SaveChanges();
            return (result > 0) ? true : false;
         }

        public List<ProductResponse> SearchProducts(
            string searchValue, 
            double? from, double? to,
            string sortBy, int page, int pagesize)
        {
            var products
                = _context.Products.AsQueryable();

            #region Filtering
            if (!String.IsNullOrEmpty(searchValue))
            {
                products = products.Where(p => p.Name.Contains(searchValue)); 
            }

            if (from.HasValue)
            {
                products = products.Where(p => p.Price >= from);
            }
            if (to.HasValue)
            {
                products = products.Where(p => p.Price <= to);
            }
            #endregion

            #region Sorting
            // Default sort by Name (ProductName)
            products = products.OrderBy(p => p.Name);

            if (!String.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        products = products.OrderByDescending(p => p.Name);
                        break;
                    case "price_asc":
                        products = products.OrderBy(p => p.Price);
                        break;
                    case "price_desc":
                        products = products.OrderByDescending(p => p.Price);
                        break;
                }
            }
            #endregion

            #region Paging
            //products = products.Skip((page - 1)*pagesize).Take(pagesize);
            var result = PaginatedList<ProductEntity>.Create(products, page, pagesize);
            #endregion

            //var result
            //    = products.Select(p => new ProductResponse
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Description = p.Description,
            //        Price = p.Price,
            //        Voucher = p.Voucher
            //    });

            //return result.ToList();

            return result.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Voucher = p.Voucher
                //CategoryId = p.category.CategoryId
            }).ToList();

        }
    }
}
