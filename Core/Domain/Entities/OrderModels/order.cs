using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.identity;

namespace Domain.Entities.OrderModels
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, Address shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subTotal, string paymentIntentId)
        {
            Id = new Guid();
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            this.DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; }

        // Shipping Address

        public Address ShippingAddress { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public DeliveryMethod DeliveryMethod
        { get; set;}

        public int? DeliveryMethodId { get; set; } // FK

        // Payment Status
        public OrderPaymentStatus PaymentStatus { get; set; }

        // Sub Total
  
public decimal SubTotal { get; set; }

        // Order Date
     
public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now; 

// Payment

public string PaymentIntentId { get; set; }
    }
}
