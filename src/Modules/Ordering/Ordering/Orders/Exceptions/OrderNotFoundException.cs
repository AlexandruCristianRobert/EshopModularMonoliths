﻿using Shared.Exceptions;

namespace Ordering.Orders.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid id) : base("Order", id)
        {

        }

        public OrderNotFoundException(string userName) : base("Order", userName)
        {

        }
    }
}
