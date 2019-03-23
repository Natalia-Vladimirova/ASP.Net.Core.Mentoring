using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindApp.DAL.Entities;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public SupplierRepository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            var suppliers = await _context.Set<SupplierDto>().ToListAsync();

            return suppliers.Select(_mapper.Map<Supplier>);
        }
    }
}
