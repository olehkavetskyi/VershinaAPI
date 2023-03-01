using Core.Entities;
using Core.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;

    public ProductRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _context.ProductBrands.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _context.Products
            .Include(b => b.ProductBrand)
            .Include(t => t.ProductType)
            .FirstOrDefaultAsync(p => p.Id.ToString() == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        var typeId = Guid.NewGuid();

        var products = _context.Products
            .Where(x => x.ProductTypeId == typeId)
            .Include(p => p.ProductType)
            .Include(p => p.ProductType)
            .ToListAsync();

        return await _context.Products
            .Include(b => b.ProductBrand)
            .Include(t => t.ProductType)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await _context.ProductTypes.ToListAsync();
    }

    
}
