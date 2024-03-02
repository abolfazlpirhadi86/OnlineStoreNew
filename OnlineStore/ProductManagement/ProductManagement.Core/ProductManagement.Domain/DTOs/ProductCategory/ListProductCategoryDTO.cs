namespace ProductManagement.Domain.DTOs.ProductCategory
{
    public class ListProductCategoryDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public string CreatedAt { get; set; }
        public int ProductCount { get; set; }
    }
}
