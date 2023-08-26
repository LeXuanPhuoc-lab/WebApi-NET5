using FirstWebAPI.Entities;
using FirstWebAPI.Models;
using FirstWebAPI.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Services
{
    public interface ICategoryRepository
    {
        List<CategoryResponse> GetCategories();
        CategoryResponse GetById(int id);
        void Add(CategoryModel category);
        bool Update(CategoryModel category);
        void Delete(int id);
    }
}
