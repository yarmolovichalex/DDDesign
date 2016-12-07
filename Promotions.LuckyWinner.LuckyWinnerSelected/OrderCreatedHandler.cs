using System;

namespace Promotions.LuckyWinner.LuckyWinnerSelected
{
    public class OrderCreatedHandler
    {
        public void Handle(OrderCreated message)
        {
            Console.WriteLine(
                $"MassTransit handling order place event: Order {message.OrderId} for User {message.UserId}");
        }
    }
}
