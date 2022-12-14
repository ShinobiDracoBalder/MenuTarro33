using MenuTarro33.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace MenuTarro33.Common.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TbPlatillo> TbPlatillo { get; set; }
        public DbSet<TbCategoria> TbCategoria { get; set; }
        public DbSet<TbPlatilloImage> PlatilloImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
