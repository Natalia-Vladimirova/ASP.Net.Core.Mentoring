using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;
using NorthwindApp.Services.Interfaces;

namespace NorthwindApp.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            return await _supplierRepository.GetSuppliersAsync();
        }
    }
}
