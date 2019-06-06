using GraphQL.DataLoader;
using GraphQL.Types;
using GraphqlCoreDemo.Infrastructure.Models;
using GraphqlCoreDemo.Infrastructure.Repositories;
using System.Collections.Generic;

namespace GraphqlCoreDemo.Core.GraphTypes
{
    public class StoreType : ObjectGraphType<Store>
    {
        public StoreType(IUnitOfWork uow, IDataLoaderContextAccessor accessor)
        {
            Field(i => i.StoreId);
            Field(i => i.Name);
            Field<ListGraphType<ProductType>, IEnumerable<Product>>().Name("Inventory");
            //.ResolveAsync(context => {
            //    var productsLoader = accessor.Context.GetOrAddCollectionBatchLoader<int, Product>("GetProductsByStoreId", productRepository.GetProductsByStoreIdAsync);
            //    return productsLoader.LoadAsync(context.Source.StoreId);
            //});
        }
    }
}
