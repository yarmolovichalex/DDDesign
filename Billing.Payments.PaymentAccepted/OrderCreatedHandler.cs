using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using Sales.Messages.Events;

namespace Billing.Payments.PaymentAccepted
{
    public class OrderCreatedHandler : IHandleMessages<OrderCreated>
    {
        public IBus Bus { get; set; }

        public void Handle(OrderCreated message)
        {
            Console.WriteLine("Received order created event: OrderId: {0}", message.OrderId);
        }
    }
}
