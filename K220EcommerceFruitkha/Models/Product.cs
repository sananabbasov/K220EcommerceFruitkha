namespace K220EcommerceFruitkha.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Discount { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool InStock { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
