using Microsoft.EntityFrameworkCore;
using WebApi_1_dars_V.Model;

namespace WebApi_1_dars_V.AppDBContext
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) :
            base(options) { }

        public virtual DbSet<User> Users { get; set; }
       
    }
}
