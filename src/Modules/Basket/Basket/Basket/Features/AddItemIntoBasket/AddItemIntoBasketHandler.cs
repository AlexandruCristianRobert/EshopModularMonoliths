using Catalog.Contracts.Products.Features.GetProductById;

namespace Basket.Basket.Features.AddItemIntoBasket
{
    public record AddItemToBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItem) : ICommand<AddItemToBasketResult>;
    public record AddItemToBasketResult(Guid Id);

    public class AddItemToBasketValidator : AbstractValidator<AddItemToBasketCommand>
    {
        public AddItemToBasketValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.ShoppingCartItem.ProductId).NotEmpty().WithMessage("ProductId is required");
            RuleFor(x => x.ShoppingCartItem.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }

    internal class AddItemIntoBasketCommandHandler(IBasketRepository repository, ISender sender) : ICommandHandler<AddItemToBasketCommand, AddItemToBasketResult>
    {
        public async Task<AddItemToBasketResult> Handle(AddItemToBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = await repository.GetBasket(command.UserName, false, cancellationToken);

            var result = await sender.Send(new GetProductByIdQuery(command.ShoppingCartItem.ProductId));

            shoppingCart.AddItem(
                command.ShoppingCartItem.ProductId,
                command.ShoppingCartItem.Quantity,
                command.ShoppingCartItem.Color,
                result.Product.Price,
                result.Product.Name
                );

            await repository.SaveChangesAsync(command.UserName, cancellationToken);

            return new AddItemToBasketResult(shoppingCart.Id);
        }
    }
}
