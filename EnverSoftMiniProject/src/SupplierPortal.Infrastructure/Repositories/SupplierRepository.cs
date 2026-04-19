using Microsoft.EntityFrameworkCore;
using SupplierPortal.Domain.Entities;
using SupplierPortal.Domain.Interfaces;
using SupplierPortal.Infrastructure.Data;

namespace SupplierPortal.Infrastructure.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly AppDbContext _dbContext;

    public SupplierRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Supplier> AddAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        if (supplier.Code == 0)
        {
            var currentMaxCode = await _dbContext.Suppliers
                .Select(x => (int?)x.Code)
                .MaxAsync(cancellationToken) ?? 0;

            supplier.Code = currentMaxCode + 1;
        }

        _dbContext.Suppliers.Add(supplier);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return supplier;
    }

    public async Task<(List<Supplier> Items, int TotalCount)> SearchByCompanyNameAsync(string companyName, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Suppliers
            .AsNoTracking()
            .Where(x => x.Name.Contains(companyName));

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(x => x.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<bool> ExistsByCompanyNameAsync(string companyName, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Suppliers.AnyAsync(x => x.Name == companyName, cancellationToken);
    }
}
