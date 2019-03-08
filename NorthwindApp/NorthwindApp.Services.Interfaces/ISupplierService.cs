using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindApp.Models;

namespace NorthwindApp.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync();
    }
}
