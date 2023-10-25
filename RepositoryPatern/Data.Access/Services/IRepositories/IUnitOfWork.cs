namespace RepositoryPatern.Data.Access.Services.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        Task SaveChangesAsync();
    }
}
