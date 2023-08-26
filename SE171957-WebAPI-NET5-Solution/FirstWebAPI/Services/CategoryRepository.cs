using FirstWebAPI.Entities;
using FirstWebAPI.Heplers;
using FirstWebAPI.Models;
using FirstWebAPI.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Services
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly DbContextHelper _context;

        public CategoryRepository (DbContextHelper context)
        {
            _context = context;
        }

        public void Add(CategoryModel category)
        {
            _context.Categories.Add(new CategoryEntity {
                Name = category.Name
            });
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = 
                _context.Categories.Where(x => x.CategoryId == id)// Where Category Id = id
                                   .FirstOrDefault();// first data found 

            if(category != null) // If found in db
            {
                // Remove
                _context.Categories.Remove(category);
            }
        }

        public CategoryResponse GetById(int id)
        {
            var category =
                _context.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            if(category != null)
            {
                return new CategoryResponse 
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name
                };
            }
            return null;
        }

        public List<CategoryResponse> GetCategories()
        {
            var categories =
                _context.Categories.Select(x => new CategoryResponse {
                    CategoryId = x.CategoryId,
                    Name = x.Name
                }).ToList();
            if(categories != null)
            {
                return categories;
            }
            return null;
        }

        public bool Update(CategoryModel category)
        {
            // find in DB
            var categoryEntity = // where categoryId == categoryUpdateId
                _context.Categories.Where(x => x.CategoryId == category.CategoryId) 
                                   .FirstOrDefault(); // first data found
            if(categoryEntity != null) // found in db 
            {
                // update
                categoryEntity.Name = category.Name;
                // save changes
                _context.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
