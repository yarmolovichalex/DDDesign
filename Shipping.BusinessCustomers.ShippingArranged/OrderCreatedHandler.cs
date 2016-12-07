using System;
using NServiceBus;
using Sales.Messages.Events;

namespace Shipping.BusinessCustomers.ShippingArranged
{
    public class OrderCreatedHandler : IHandleMessages<OrderCreated_V2>
    {
        public IBus Bus { get; set; }

        public void Handle(OrderCreated_V2 message)
        {
            Console.WriteLine(
                $"Shipping BC storing: Order: {message.OrderId} User: {message.UserId} Address: {message.AddressId} Shipping Type: {message.ShippingTypeId}");

            var order = new ShippingOrder
            {
                UserId = message.UserId,
                OrderId = message.OrderId,
                AddressId = message.AddressId,
                ShippingTypeId = message.ShippingTypeId
            };

            ShippingDatabase.AddOrderDetails(order);
        }
    }
}
