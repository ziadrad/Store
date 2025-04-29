using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Entities.OrderModels
{
    public class OrderItem : BaseEntity<Guid>
    {

        public OrderItem()
        {

        }
public OrderItem(ProductInOrderItem myProperty, int quantity, decimal price)
        {

            Product = myProperty;
            Quantity = quantity;
            Price = price;

        }
        public ProductInOrderItem Product { get; set; }
   
        public int Quantity { get; set; }
    
        public decimal Price { get; set; }
    }
}