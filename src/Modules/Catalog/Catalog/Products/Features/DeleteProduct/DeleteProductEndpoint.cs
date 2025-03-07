namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("products/{productId}", async (Guid productId, ISender sender) =>
            {
                var command = new DeleteProductCommand(productId);

                var result = await sender.Send(command);

                var response = result.Adapt<DeleteProductResult>();

                return Results.Ok(response);

            }).WithName("DeleteProduct")
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product");
        }
    }
}
