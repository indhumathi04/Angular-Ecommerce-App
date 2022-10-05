using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepo : IProductRepo
    {
        public readonly StoreContext context;
        public ProductRepo(StoreContext _context)
        {
            context=_context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await context.ProductBrands.ToListAsync();
        }

        public async Task<product> GetProductByIdAsync(int id)
        {
            return await context.product
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p=>p.Id==id);
        }

        public async Task<IReadOnlyList<product>> GetProductsAsync()
        {
            return await context.product
                .Include(p=>p.ProductType)
                .Include(p=>p.ProductBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await context.ProductTypes.ToListAsync();
        }
    }
}
