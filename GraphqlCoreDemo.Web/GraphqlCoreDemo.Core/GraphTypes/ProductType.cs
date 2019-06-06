using GraphQL.DataLoader;
using GraphQL.Types;
using GraphqlCoreDemo.Infrastructure.Models;
using GraphqlCoreDemo.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphqlCoreDemo.Core.GraphTypes
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(IUnitOfWork uow, IDataLoaderContextAccessor accessor)
        {
            Field(i => i.ProductId);
            Field(i => i.Barcode);
            Field(i => i.Title);
            Field(i => i.SellingPrice);
            Field<StoreType, Store>().Name("Store");
            //.ResolveAsync(context => {
            //    //var storeLoader = accessor.Context.GetOrAddBatchLoader<int, Store>("GetStoreById", storeRepository.GetStoresByIdAsync);
            //    //return storeLoader.LoadAsync(context.Source.StoreId);
            //    return storeRepository.GetStoreByIdAsync(context.Source.StoreId);
            //});
        }
    }
}
