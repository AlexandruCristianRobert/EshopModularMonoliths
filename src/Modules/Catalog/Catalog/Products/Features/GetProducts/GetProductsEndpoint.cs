﻿namespace Catalog.Products.Features.GetProduct
{
    public record GetProductsResponse(PaginatedResult<ProductDto> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery(request));

                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
                .WithName("GetProducts")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products Products")
                .WithDescription("Get Products Products");
        }
    }
}
