using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindApp.DAL.Infrastructure;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;

        public SupplierRepository(NorthwindDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            var suppliers = await _context.Suppliers.ToListAsync();

            return suppliers.Select(_mapper.Map<Supplier>);
        }
    }
}
