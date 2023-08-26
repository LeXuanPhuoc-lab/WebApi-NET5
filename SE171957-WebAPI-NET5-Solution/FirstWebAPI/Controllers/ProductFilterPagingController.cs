using FirstWebAPI.Models;
using FirstWebAPI.Payload.Response;
using FirstWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFilterPagingController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IConfiguration _configuration;

        public ProductFilterPagingController(IProductRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult SearchProduct(string searchValue,
            double? from, double? to,
            string sortBy, int page = 1)
        {
            try
            {
                int pagesize 
                    = Convert.ToInt32(_configuration.GetValue<string>("AppSettings:PageSize"));
                var result = _repository.SearchProducts(searchValue, from, to, sortBy, page, pagesize);

                if(result.Count == 0)
                {
                    return NotFound();
                }

                return Ok(
                    new BaseResponse{
                        StatusCode = StatusCodes.Status200OK,
                        Data = result
                    });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(ProductModel product)
        {
            bool addSucess = _repository.Add(product);

            if (addSucess)
            {
                return Ok(new BaseResponse { 
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Add Sucess"
                });
            }

            return BadRequest();
        }
    }
}
