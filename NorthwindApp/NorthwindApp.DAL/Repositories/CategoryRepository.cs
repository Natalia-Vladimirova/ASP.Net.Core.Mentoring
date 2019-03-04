using System;
using System.Collections.Generic;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}
