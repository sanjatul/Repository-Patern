using RepositoryPatern.Data.Access.Data;
using RepositoryPatern.Data.Access.Services.IRepositories;

namespace RepositoryPatern.Data.Access.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Product { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Product = new ProductRepository(_context);
            OrderDetail=new OrderDetailRepository(_context);
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
