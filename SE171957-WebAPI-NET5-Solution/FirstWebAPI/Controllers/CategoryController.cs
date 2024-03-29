﻿using FirstWebAPI.Entities;
using FirstWebAPI.Heplers;
using FirstWebAPI.Models;
using FirstWebAPI.Payload.Response;

using FirstWebAPI.Services;

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
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var categories = _categoryRepository.GetCategories();
            try
            {



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
            var categories = _categoryRepository.GetById(Id);
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
        [Authorize]
        public IActionResult Create(CategoryModel category)
        {
            try
            {
                _categoryRepository.Add(category);
                return Ok(
                    new BaseResponse { StatusCode = 201, Message = "Create Sucess"}
                    );
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update(int CategoryId, CategoryModel category)
        {
            try
            {
                bool isSucess = _categoryRepository.Update(category);

                if (!isSucess)
                {
                    return NotFound();
                }

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
