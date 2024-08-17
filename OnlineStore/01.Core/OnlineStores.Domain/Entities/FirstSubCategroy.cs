using OnlineStores.Domain.Entities.Products;

namespace OnlineStores.Domain.Entities
{
    public class FirstSubCategroy
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
    }
}
