using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphqlCoreDemo.Core.InputGraphTypes
{
    public class ProductInputType : InputObjectGraphType
    {
        public ProductInputType()
        {
            Name = "ProductInput";
            Field<NonNullGraphType<StringGraphType>>("barcode");
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<NonNullGraphType<DecimalGraphType>>("sellingPrice");
            Field<NonNullGraphType<IntGraphType>>("storeId");
        }
    }
}
