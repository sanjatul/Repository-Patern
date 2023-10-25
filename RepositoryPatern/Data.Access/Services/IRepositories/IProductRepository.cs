using RepositoryPatern.Models;

namespace RepositoryPatern.Data.Access.Services.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<int> GetProductCountAsync();
    }
}
