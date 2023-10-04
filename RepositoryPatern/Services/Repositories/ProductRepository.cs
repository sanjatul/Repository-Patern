using Microsoft.EntityFrameworkCore;
using RepositoryPatern.Data.Access;
using RepositoryPatern.Models;
using RepositoryPatern.Services.IRepositories;

namespace RepositoryPatern.Services.Repositories
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
