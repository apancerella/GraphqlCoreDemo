using GraphQL;
using GraphQL.Types;
using GraphqlCoreDemo.Core.Mutations;
using GraphqlCoreDemo.Core.Queries;

namespace GraphqlCoreDemo.Core
{
    public class GraphqlSchema : Schema
    {
        public GraphqlSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<QueryBase>();
            Mutation = resolver.Resolve<MutationBase>();
        }
    }
}
