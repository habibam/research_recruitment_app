using Microsoft.EntityFrameworkCore;
 
namespace loginregister.Models
{
    public class MainContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        public DbSet<User> users { get; set;}
        public DbSet<Study> studies { get; set;}
        public DbSet<Participant> participants { get; set;}

    }
}