namespace OnlineStores.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string IP { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Confirm { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }

    }
}
