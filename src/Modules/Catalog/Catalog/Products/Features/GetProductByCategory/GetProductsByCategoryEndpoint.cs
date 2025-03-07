namespace Catalog.Products.Features.GetProductByCategory
{
    public record GetProductsByCategoryResponse(IEnumerable<ProductDto> Products);
    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var command = new GetProductsByCategoryQuery(category);

                var result = await sender.Send(command);

                var response = result.Adapt<GetProductsByCategoryResponse>();

                return response;
            })
                .WithName("GetProductsByCategory")
                .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Products By Category")
                .WithDescription("Get Products By Category");
        }
    }
}
