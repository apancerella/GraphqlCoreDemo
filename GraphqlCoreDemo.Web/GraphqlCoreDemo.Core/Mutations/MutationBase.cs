using GraphQL.DataLoader;
using GraphQL.Types;
using GraphqlCoreDemo.Infrastructure.Repositories;

namespace GraphqlCoreDemo.Core.Mutations
{
    public partial class MutationBase : ObjectGraphType
    {
        private readonly IUnitOfWork _uow;
        private readonly IDataLoaderContextAccessor _accessor;

        public MutationBase(IUnitOfWork uow, IDataLoaderContextAccessor accessor)
        {
            _uow = uow;
            _accessor = accessor;

            Name = "MutationBase";
            InitializeProductMutation();
            InitializeStoreMutation();
        }
    }
}
