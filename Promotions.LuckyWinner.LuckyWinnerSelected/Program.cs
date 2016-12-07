using System;
using System.Threading;
using MassTransit;

namespace Promotions.LuckyWinner.LuckyWinnerSelected
{
    class Program
    {
        static void Main(string[] args)
        {
            Bus.Initialize(config =>
            {
                config.UseMsmq();
                config.ReceiveFrom("msmq://localhost/promotions.ordercreated");
                config.Subscribe(sub =>
                {
                    sub.Handler<OrderCreated>(msg =>
                        new OrderCreatedHandler().Handle(msg));
                });
            });

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }

    public class OrderCreated
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public string[] ProductIds { get; set; }
        public string ShippingTypeId { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Amount { get; set; }
    }
}
