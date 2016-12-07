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
            Database2.SavePaymentAttempt(message.OrderId, message.Status);

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
                // 
            }
        }
    }

    public static class Database2
    {
        public static void SavePaymentAttempt(string orderId, PaymentStatus status)
        {
            //
        }
    }
}
