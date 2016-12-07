using System.Collections.Generic;
using System.Linq;

namespace Shipping.BusinessCustomers.ShippingArranged
{
    public static class ShippingDatabase
    {
        private static List<ShippingOrder> Orders = new List<ShippingOrder>(); 

        public static void AddOrderDetails(ShippingOrder order)
        {
            Orders.Add(order);
        }

        public static string GetCustomerAddress(string orderId)
        {
            var order = Orders.Single(o => o.OrderId == orderId);
            return $"{order.UserId}, {order.AddressId}";
        }
    }
}