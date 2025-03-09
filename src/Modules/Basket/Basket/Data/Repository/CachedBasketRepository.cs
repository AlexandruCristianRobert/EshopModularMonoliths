namespace Basket.Data.Repository
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {

        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new ShoppingCartConverter(), new ShoppingCartItemConverter() }
        };

        public async Task<ShoppingCart> GetBasket(string userName, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            if (!asNoTracking)
            {
                return await repository.GetBasket(userName, asNoTracking, cancellationToken);
            }

            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if(!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket, _options)!;
            }

            var basket = await repository.GetBasket(userName, asNoTracking, cancellationToken);

            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket, _options), cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> CreateBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            await repository.CreateBasket(cart, cancellationToken);

            await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart, _options), cancellationToken);
            return cart;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasket(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);

            return true;
        }

        public async Task<int> SaveChangesAsync(string? userName = null,CancellationToken cancellationToken = default)
        {
            var result = await repository.SaveChangesAsync(userName, cancellationToken);
            
            if(userName is not null)
            {
                await cache.RemoveAsync(userName, cancellationToken);
            }

            return result;
        }
    }
}
