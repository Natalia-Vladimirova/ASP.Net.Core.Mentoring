using System.Collections.Generic;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
    }
}
