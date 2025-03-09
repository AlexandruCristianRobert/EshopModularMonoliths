
namespace Basket.Basket.Features.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketQueryResult>;
    public record GetBasketQueryResult(ShoppingCartDto ShoppingCart);

    public class GetBasketQueryValidator : AbstractValidator<GetBasketQuery>
    {
        public GetBasketQueryValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    internal class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketQueryResult>
    {
        public async Task<GetBasketQueryResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var shoppingCart = await repository.GetBasket(query.UserName, true, cancellationToken);

            var shoppingCartDto = shoppingCart.Adapt<ShoppingCartDto>();

            return new GetBasketQueryResult(shoppingCartDto);
        }
    }
}
