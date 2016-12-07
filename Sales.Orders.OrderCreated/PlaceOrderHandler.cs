using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using Sales.Messages.Commands;

namespace Sales.Orders.OrderCreated
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        public IBus Bus { get; set; }


        public void Handle(PlaceOrder message)
        {
            var orderId = Database.SaveOrder(message.ProductIds, message.UserId, message.ShippingTypeId);

            Console.WriteLine(
                @"Created order #{3} for Products: {0} with shipping: {1}" +
                " made by user: {2}", 
                string.Join(",", message.ProductIds), message.ShippingTypeId, message.UserId, orderId);

            var orderCreatedEvent = new Messages.Events.OrderCreated_V2
            {
                OrderId = orderId,
                UserId = message.UserId,
                ProductIds = message.ProductIds,
                ShippingTypeId = message.ShippingTypeId,
                TimeStamp = DateTime.Now,
                Amount = CalculateCostOf(message.ProductIds),
                AddressId = "AddressID123"
            };

            Bus.Publish(orderCreatedEvent);
        }

        private double CalculateCostOf(IEnumerable<string> productIds)
        {
            return 1000.0;
        }
    }

    public static class Database
    {
        private static int Id = 0;

        public static string SaveOrder(IEnumerable<string> productIds, string userId, string shippingTypeId)
        {
            var nextOrderId = Id++;
            return nextOrderId.ToString();
        }
    }
}
