using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphqlCoreDemo.Core.InputGraphTypes
{
    public class StoreInputType : InputObjectGraphType
    {
        public StoreInputType()
        {
            Name = "StoreInput";
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
