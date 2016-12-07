using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billing.Messages.Events;
using NServiceBus;

namespace Shipping.BusinessCustomers.ShippingArranged
{
    public class PaymentAcceptedHandler : IHandleMessages<PaymentAccepted>
    {
        public IBus Bus { get; set; }

        public void Handle(PaymentAccepted message)
        {
            var address = ShippingDatabase.GetCustomerAddress(message.OrderId);
            var confirmation = ShippingProvider.ArrangeShippingFor(address, message.OrderId);

            if (confirmation.Status == ShippingProvider.ShippingStatus.Success)
            {
                var evnt = new Shipping.Messages.Events.ShippingArranged
                {
                    OrderId = message.OrderId
                };

                Bus.Publish(evnt);

                Console.WriteLine($"Shipping BC arranged shipping for Order: {message.OrderId} to: {address}");
            }
            else
            {
                //
            }
        }
    }
}
