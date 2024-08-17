namespace OnlineStores.Domain.Entities
{
    public class Gallery
    {
        public int ProductId { get; set; }
        public int PictureId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public Product Product { get; set; }
    }
}
