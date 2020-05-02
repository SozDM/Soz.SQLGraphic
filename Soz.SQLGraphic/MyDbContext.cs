using System.Data.Entity;

namespace Soz.SQLConsole
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("DbConnectionString")
        {
        }

        public DbSet<UserManager> UserManagers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}