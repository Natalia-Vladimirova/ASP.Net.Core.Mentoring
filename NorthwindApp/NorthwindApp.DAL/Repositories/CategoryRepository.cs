﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindApp.DAL.Infrastructure;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(NorthwindDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();

            return categories.Select(_mapper.Map<Category>);
        }
    }
}
