using Shared.Exceptions;

namespace Basket.Basket.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(Guid id) : base("Basket", id)
        {

        }

        public BasketNotFoundException(string userName) : base("Basket", userName)
        {

        }
    }
}
