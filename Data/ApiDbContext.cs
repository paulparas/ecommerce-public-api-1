using Microsoft.EntityFrameworkCore;

namespace public_api_interface.Data
{
    public class ApiDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Product>(entity =>
            {
                //// One to One relationship
                //entity.HasOne(d => d.Id);
            });


        }
    }
}
