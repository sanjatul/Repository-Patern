using RepositoryPatern.Models;

namespace RepositoryPatern.Services.IRepositories
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<int> GetProductCountAsync();
    }
}
