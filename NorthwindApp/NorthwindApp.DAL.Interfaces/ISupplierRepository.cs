using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync();
    }
}
