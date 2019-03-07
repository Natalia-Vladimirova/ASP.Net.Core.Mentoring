using System.Collections.Generic;
using NorthwindApp.Models;

namespace NorthwindApp.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
