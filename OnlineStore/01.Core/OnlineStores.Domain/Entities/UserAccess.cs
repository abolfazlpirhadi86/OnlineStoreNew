namespace OnlineStores.Domain.Entities
{
    public class UserAccess
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public bool AddAction { get; set; }
        public bool UpdateAction { get; set; }
        public bool DeleteAction { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}
