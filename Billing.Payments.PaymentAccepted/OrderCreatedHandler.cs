using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billing.Messages.Commands;
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

            var cardDetails = Database.GetCardDetailsFor(message.UserId);

            var conf = PaymentProvider.ChargeCreditCard(cardDetails, message.Amount);

            var command = new RecordPaymentAttempt
            {
                OrderId = message.OrderId,
                Status = conf.Status
            };

            Bus.SendLocal(command);
        }
    }

    public static class PaymentProvider
    {
        private static int Attempts = 0;

        public static PaymentConfirmation ChargeCreditCard(CardDetails details, double amount)
        {
            if (Attempts < 2)
            {
                Attempts++;
                throw new Exception("Service unavailable.");
            }

            return new PaymentConfirmation
            {
                Status = PaymentStatus.Accepted
            };
        }
    }

    public class PaymentConfirmation
    {
        public PaymentStatus Status { get; set; }
    }

    public static class Database
    {
        public static CardDetails GetCardDetailsFor(string userId)
        {
            return new CardDetails();
        }
    }

    public class CardDetails
    {
        //
    }
}
