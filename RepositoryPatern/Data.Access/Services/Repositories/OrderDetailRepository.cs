using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RepositoryPatern.Data.Access.Data;
using RepositoryPatern.Data.Access.Services.IRepositories;
using RepositoryPatern.Models;

namespace RepositoryPatern.Data.Access.Services.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<OrderDetail>> GetAllAsync()
        {
            return await _dbSet.Include(od => od.Product).ToListAsync(); ;
        }
        public async Task<bool> ApplyDiscountAsync(int orderId, decimal dicount)
        {
            var order=await _context.OrderDetails.FirstOrDefaultAsync(x=>x.OrderId==orderId);
            if (order!=null)
            {
                order.Discount = dicount;
                _context.OrderDetails.Update(order);
                return true;
            }
            return false;
        }
    }
}
