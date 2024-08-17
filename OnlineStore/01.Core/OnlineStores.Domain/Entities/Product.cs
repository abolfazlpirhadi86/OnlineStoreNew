namespace OnlineStores.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int SubFirstCategoryId { get; set; }
        public int SubSecondCategoryId { get; set; }
        public int View { get; set; }
        public decimal Price { get; set; }
        public bool IsShow { get; set; }
        public int Discount { get; set; }
        public int Count { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Product> Products { get; set; }

    }
}
