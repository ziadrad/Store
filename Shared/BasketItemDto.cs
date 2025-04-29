using System.ComponentModel.DataAnnotations;

namespace Services.Abstraction
{
    public class BasketItemDto
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }
        [Range(minimum:0, maximum:double.MaxValue)]
        public decimal Price { get; set; }
        [Range(minimum: 0, maximum: 100)]
        public int Quantity { get; set; }
    }
}