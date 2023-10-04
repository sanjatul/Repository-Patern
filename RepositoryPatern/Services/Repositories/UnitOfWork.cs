using RepositoryPatern.Data.Access;
using RepositoryPatern.Services.IRepositories;

namespace RepositoryPatern.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Product { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Product=new ProductRepository(_context);
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
