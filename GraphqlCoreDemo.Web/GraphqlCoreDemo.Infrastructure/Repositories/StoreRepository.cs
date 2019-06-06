using GraphqlCoreDemo.Infrastructure.Models;

namespace GraphqlCoreDemo.Infrastructure.Repositories
{
    public interface IStoreRepository : IRepositoryBase<Store>
    {
    }
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StoreRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
