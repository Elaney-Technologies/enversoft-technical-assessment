using SupplierPortal.Application.Interfaces;
using SupplierPortal.Application.Models;
using SupplierPortal.Domain.Entities;
using SupplierPortal.Domain.Interfaces;

namespace SupplierPortal.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<SupplierResponse> CreateAsync(CreateSupplierRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.CompanyName))
        {
            throw new ArgumentException("Company name is required.");
        }

        if (string.IsNullOrWhiteSpace(request.TelephoneNo))
        {
            throw new ArgumentException("Telephone number is required.");
        }

        var existing = await _supplierRepository.ExistsByCompanyNameAsync(request.CompanyName.Trim(), cancellationToken);
        if (existing)
        {
            throw new InvalidOperationException("A supplier with this company name already exists.");
        }

        var supplier = new Supplier
        {
            Name = request.CompanyName.Trim(),
            TelephoneNo = request.TelephoneNo.Trim(),
            Code = 0
        };

        var saved = await _supplierRepository.AddAsync(supplier, cancellationToken);

        return new SupplierResponse
        {
            Code = saved.Code,
            CompanyName = saved.Name,
            TelephoneNo = saved.TelephoneNo
        };
    }

    public async Task<PagedResult<SupplierResponse>> SearchByCompanyNameAsync(string companyName, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(companyName))
        {
            return new PagedResult<SupplierResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = 0
            };
        }

        var (items, totalCount) = await _supplierRepository.SearchByCompanyNameAsync(companyName.Trim(), pageNumber, pageSize, cancellationToken);
        
        return new PagedResult<SupplierResponse>
        {
            Items = items.Select(supplier => new SupplierResponse
            {
                Code = supplier.Code,
                CompanyName = supplier.Name,
                TelephoneNo = supplier.TelephoneNo
            }).ToList(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}
