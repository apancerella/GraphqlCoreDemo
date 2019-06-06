using GraphQL.Types;
using GraphqlCoreDemo.Core.GraphTypes;
using GraphqlCoreDemo.Infrastructure.Models;
using System.Collections.Generic;

namespace GraphqlCoreDemo.Core.Queries
{
    public partial class QueryBase
    {
        protected void InitializeStoreQuery()
        {
            Field<StoreType, Store>()
                .Name("store")
                .Argument<NonNullGraphType<IntGraphType>>("storeId", "store id")
                .ResolveAsync(context => {
                    var storeId = context.GetArgument<int>("storeId");
                    return _uow.Store.FindByAsync(x => x.StoreId == storeId, false, true);
                });

            Field<ListGraphType<StoreType>, IEnumerable<Store>>()
                .Name("stores")
                .ResolveAsync(context => {
                    return _uow.Store.GetAllAsync(true);
                });
        }
    }
}
