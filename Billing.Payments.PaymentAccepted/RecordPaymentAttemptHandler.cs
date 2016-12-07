using System;
using Billing.Messages.Commands;
using NServiceBus;

namespace Billing.Payments.PaymentAccepted
{
    public class RecordPaymentAttemptHandler : IHandleMessages<RecordPaymentAttempt>
    {
        public IBus Bus { get; set; }

        public void Handle(RecordPaymentAttempt message)
        {
            Database.SavePaymentAttempt(message.OrderId, message.Status);

            if (message.Status == PaymentStatus.Accepted)
            {
                var evnt = new Messages.Events.PaymentAccepted
                {
                    OrderId = message.OrderId
                };
                Bus.Publish(evnt);
                Console.WriteLine($"Received payment accepted notification for Order: {message.OrderId}. Published PaymentAccepted event");
            }
            else
            {
                // publish payment rejected event
            }
        }

        public static class Database
        {
            public static void SavePaymentAttempt(string orderId, PaymentStatus status)
            {
                // save to db
            }
        }
    }
}
