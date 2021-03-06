﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindApp.Models;

namespace NorthwindApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<byte[]> GetCategoryImageAsync(int id);

        Task UploadCategoryImageAsync(int id, byte[] image);
    }
}
