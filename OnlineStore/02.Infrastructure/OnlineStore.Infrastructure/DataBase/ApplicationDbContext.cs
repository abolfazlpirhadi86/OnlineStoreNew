using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Infrastructure.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
