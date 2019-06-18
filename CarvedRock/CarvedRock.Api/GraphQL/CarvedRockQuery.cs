using CarvedRock.Api.GraphQL.Types;
using CarvedRock.Api.Repositories;
using GraphQL.Types;

namespace CarvedRock.Api.GraphQL
{
    public class CarvedRockQuery: ObjectGraphType
    {
        public CarvedRockQuery(ProductRepository productRepository)
        {
            Field<ListGraphType<ProductType>>(
                "products", 
                resolve: context => productRepository.GetAll()
            );
            Field<ListGraphType<ProductType>>(
                "productsById", 
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: context => {
                    var id = context.GetArgument<int>("id");
                    return productRepository.GetById(id);
                }
            );
        }
    }
}
