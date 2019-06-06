using GraphQL.Types;
using GraphqlCoreDemo.Core.GraphTypes;
using GraphqlCoreDemo.Core.InputGraphTypes;
using GraphqlCoreDemo.Infrastructure.Models;

namespace GraphqlCoreDemo.Core.Mutations
{
    public partial class MutationBase
    {
        protected void InitializeProductMutation()
        {
            Field<ProductType, Product>()
                .Name("createProduct")
                .Argument<NonNullGraphType<ProductInputType>>("product", "product input")
                .ResolveAsync(context =>
                {
                    var product = context.GetArgument<Product>("product");
                    _uow.Product.Add(product);
                    _uow.SaveAsync();
                    return null;
                });
        }
    }
}
