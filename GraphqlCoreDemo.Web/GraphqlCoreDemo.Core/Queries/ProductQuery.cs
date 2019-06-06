using GraphQL.Types;
using GraphqlCoreDemo.Core.GraphTypes;
using GraphqlCoreDemo.Infrastructure.Models;
using System.Collections.Generic;

namespace GraphqlCoreDemo.Core.Queries
{
    public partial class QueryBase
    {
        protected void InitializeProductQuery()
        {
            Field<ProductType, Product>()
                .Name("product")
                .Argument<NonNullGraphType<StringGraphType>>("barcode", "product barcode")
                .ResolveAsync(context =>
                {
                    var barcode = context.GetArgument<string>("barcode");
                    return _uow.Product.FindByAsync(x => x.Barcode == barcode, false, true);
                });

            Field<ListGraphType<ProductType>, IEnumerable<Product>>()
                .Name("products")
                .ResolveAsync(context =>
                {
                    return _uow.Product.GetAllAsync(true);
                });
        }
    }
}
