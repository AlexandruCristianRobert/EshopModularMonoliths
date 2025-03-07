namespace Catalog.Products.Features.GetProductById
{
    public record GetProductByIdResponse(ProductDto Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{productId}", async (Guid productId, ISender sender) =>
            {
                var command = new GetProductByIdQuery(productId);

                var result = await sender.Send(command);
                
                var response = result.Adapt<GetProductByIdResponse>();

                return response;
                               
            }).WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Product By Id")
                .WithDescription("Get Product By Id");
        }
    }

    
}
