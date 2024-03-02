using Common.Entity;

namespace ProductManagement.Domain.Entities
{
    public class ProductCategory : AuditableBaseEntityWithSoftRemove<long>
    {
        public string Title { get; private set; }
        public string Picture { get; private set; }
        public string Description { get; private set; }

        public ProductCategory()
        {
        }

        public ProductCategory(string title, string picture, string description)
        {
            Title = title;
            Picture = picture;
            Description = description;
        }

        public void Update(string title, string picture, string description) 
        {
            Title = title;
            Picture = picture;
            Description = description;
        }
    }
}
