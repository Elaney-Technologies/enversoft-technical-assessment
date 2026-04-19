using SupplierPortal.Application.Models;

namespace SupplierPortal.Application.Interfaces;

public interface ISupplierService
{
    Task<SupplierResponse> CreateAsync(CreateSupplierRequest request, CancellationToken cancellationToken = default);
    Task<PagedResult<SupplierResponse>> SearchByCompanyNameAsync(string companyName, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
