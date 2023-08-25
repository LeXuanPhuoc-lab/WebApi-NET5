using FirstWebAPI.Entities;
using FirstWebAPI.Heplers;
using FirstWebAPI.Models;
using FirstWebAPI.Payload.Response;
using Microsoft.AspNetCore.Authorization;
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
    public class CategoryController : ControllerBase
    {
        private readonly DbContextHelper _context;
        public CategoryController(DbContextHelper context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var categories = _context.Categories.ToList();

                return Ok(
                    new BaseResponse { StatusCode = StatusCodes.Status200OK, Data = categories }
                    );
            }
            catch
            {
                return NotFound(); 
            }
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var categories = _context.Categories.SingleOrDefault(
                c => c.CategoryId == Id
                );
            if(categories == null)
            {
                return NotFound(
                    new BaseResponse { StatusCode = StatusCodes.Status404NotFound, Message = "Not Found Category with Id " + Id}
                    );
            }
            return Ok(
                new BaseResponse { StatusCode = StatusCodes.Status200OK, Data = categories}
                );
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Create(Category category)
        {
            try
            {
                CategoryEntity newCategory = new CategoryEntity()
                {
                    Name = category.Name
                };
                _context.Add(newCategory);
                _context.SaveChanges();
                //return Ok(
                //    new BaseResponse { StatusCode = StatusCodes.Status201Created, Message = "Create Sucess"}
                //    );
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update(int CategoryId, Category category)
        {
            try
            {
                var oldCategory 
                    = _context.Categories.Where(x => x.CategoryId == CategoryId ).FirstOrDefault();

                if(oldCategory == null)
                {
                    return NotFound();
                }

                oldCategory.Name = category.Name;
                _context.SaveChanges();
                return Ok(
                    new BaseResponse { StatusCode = StatusCodes.Status200OK, Message = "Update Sucess"}
                    );
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
