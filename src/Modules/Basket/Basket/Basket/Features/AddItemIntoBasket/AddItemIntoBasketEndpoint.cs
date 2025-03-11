
namespace Basket.Basket.Features.AddItemIntoBasket
{
    public record AddItemIntoBasketRequest(ShoppingCartItemDto ShoppingCartItem);
    public record AddItemIntoBasketResponse(Guid Id);

    public class AddItemIntoBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/{userName}/items", async ([FromRoute] string userName, [FromBody] AddItemIntoBasketRequest request, ISender sender) =>
            {
                var command = new AddItemToBasketCommand(userName, request.ShoppingCartItem);
                var result = await sender.Send(command);

                var response = result.Adapt<AddItemIntoBasketResponse>();

                return Results.Created($"/basket/{response.Id}", response);
            })
                .WithName("AddItemIntoBasket")
                .Produces<AddItemIntoBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("AddItemInto Basket")
                .WithDescription("AddItemInto Basket")
                .RequireAuthorization();
        }
    }
}
