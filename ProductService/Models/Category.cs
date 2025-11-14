namespace ProductService.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public List<Product>? Categories { get; set; } = new List<Product>();
    }
}
