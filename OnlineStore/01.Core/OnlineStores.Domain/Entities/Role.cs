namespace OnlineStores.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleTitle { get; set; }

        public List<UserAccess> UserAccesses { get; set; }

    }
}
