namespace OnlineStores.Domain.Entities
{
    public class SecondSubCategory
    {
        public int FirstSubCategoryId { get; set; }
        public string Title { get; set; }
        public FirstSubCategroy Category { get; set; }
    }
}
