using RepositoryPatern.Models;

namespace RepositoryPatern.Data.Access.Services.IRepositories
{
    public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
    {
        Task<bool> ApplyDiscountAsync(int orderId,decimal dicount);
    }
}
