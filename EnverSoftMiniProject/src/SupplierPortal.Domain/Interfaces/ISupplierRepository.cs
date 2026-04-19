using SupplierPortal.Domain.Entities;

namespace SupplierPortal.Domain.Interfaces;

public interface ISupplierRepository
{
    Task<Supplier> AddAsync(Supplier supplier, CancellationToken cancellationToken = default);
    Task<(List<Supplier> Items, int TotalCount)> SearchByCompanyNameAsync(string companyName, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCompanyNameAsync(string companyName, CancellationToken cancellationToken = default);
}
