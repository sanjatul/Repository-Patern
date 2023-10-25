using Microsoft.EntityFrameworkCore;
using RepositoryPatern.Data.Access.Data;
using RepositoryPatern.Data.Access.Services.IRepositories;
using RepositoryPatern.Models;

namespace RepositoryPatern.Data.Access.Services.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> GetProductCountAsync()
        {
            return await _context.Set<Product>().CountAsync();
        }
    }
}
