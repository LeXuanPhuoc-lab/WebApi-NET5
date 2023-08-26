using FirstWebAPI.Models;
using FirstWebAPI.Payload.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public static List<ProductModel> products = new List<ProductModel>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(String Id)
        {

            try
            {
                var product
                    = products.Where(x => x.Id == Guid.Parse(Id)).FirstOrDefault();
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(
                    new BaseResponse { StatusCode = 200, Data = product }
                    );
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult Create(ProductDetail productDetail)
        {
            var product = new ProductModel
            {
                Id = Guid.NewGuid(),
                Name = productDetail.Name,
                Price = productDetail.Price
            };
            products.Add(product);
            return Ok(new {
                Success = true,
                Data = product
            });
        }

        [HttpPut("Id")]
        public IActionResult Update(String Id, ProductModel product)
        {
            try
            {
                var oldProduct
                    = products.Where(x => x.Id == Guid.Parse(Id)).FirstOrDefault();

                if (product == null)// check if not found Id
                {
                    return BadRequest("Id Not Found");
                }
                // Update Product
                oldProduct.Name = product.Name;
                oldProduct.Price = product.Price;
                // return 200 OK
                return Ok(new BaseResponse { StatusCode = 200, Message = "Update Sucess" });
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete("{Id}")]
        public IActionResult Delete(String Id)
        {
            try
            {
                var oldProduct
                = products.Where(x => x.Id == Guid.Parse(Id)).FirstOrDefault();
                if (oldProduct == null)
                {
                    return NotFound();
                }
                products.Remove(oldProduct);
                return Ok(
                    new BaseResponse { StatusCode = 200, Message = "Remove Sucess"}
                    );
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
