﻿namespace Basket.Basket.Features.UpdateItemPriceInBasket
{
    public record UpdateItemPriceInBasketCommand(Guid ProductId, decimal Price) : ICommand<UpdateItemPriceInBasketResult>;
    public record UpdateItemPriceInBasketResult(bool IsSuccess);
    public class UpdateItemPriceInBasketCommandValidator : AbstractValidator<UpdateItemPriceInBasketCommand>
    {
        public UpdateItemPriceInBasketCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    public class UpdateItemPriceInBasketHandler(BasketDbContext dbContext) : ICommandHandler<UpdateItemPriceInBasketCommand, UpdateItemPriceInBasketResult>
    {
        public async Task<UpdateItemPriceInBasketResult> Handle(UpdateItemPriceInBasketCommand command, CancellationToken cancellationToken)
        {
            var itemToUpdate = await dbContext.ShoppingCartItems
                .Where(x => x.ProductId == command.ProductId)
                .ToListAsync(cancellationToken);

            if (!itemToUpdate.Any())
                return new UpdateItemPriceInBasketResult(false);

            foreach (var item in itemToUpdate)
                item.UpdatePrice(command.Price);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateItemPriceInBasketResult(true);
        }
    }
}
