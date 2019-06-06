using GraphQL.Types;
using GraphqlCoreDemo.Core.GraphTypes;
using GraphqlCoreDemo.Core.InputGraphTypes;
using GraphqlCoreDemo.Infrastructure.Models;

namespace GraphqlCoreDemo.Core.Mutations
{
    public partial class MutationBase
    {
        protected void InitializeStoreMutation()
        {
            Field<StoreType, Store>()
                .Name("createStore")
                .Argument<NonNullGraphType<StoreInputType>>("store", "store input")
                .ResolveAsync(context =>
                {
                    var store = context.GetArgument<Store>("store");
                    _uow.Store.Add(store);
                    _uow.SaveAsync();
                    return null;
                });
        }
    }
}
