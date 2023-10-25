namespace RepositoryPatern.Data.Access.Services.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IOrderDetailRepository OrderDetail { get; }
        Task SaveChangesAsync();
    }
}
