using GraphQL.DataLoader;
using GraphQL.Types;
using GraphqlCoreDemo.Infrastructure.Repositories;

namespace GraphqlCoreDemo.Core.Queries
{
    public partial class QueryBase : ObjectGraphType
    {
        private readonly IUnitOfWork _uow;
        private readonly IDataLoaderContextAccessor _accessor;

        public QueryBase(IUnitOfWork uow, IDataLoaderContextAccessor accessor)
        {
            _uow = uow;
            _accessor = accessor;

            Name = "QueryBase";
            InitializeProductQuery();
            InitializeStoreQuery();
        }
    }
}
