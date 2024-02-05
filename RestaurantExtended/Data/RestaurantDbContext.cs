using Microsoft.EntityFrameworkCore;
using RestaurantExtended.Models;
using System.Reflection.Emit;

namespace RestaurantExtended.Data
{
    public class RestaurantDbContext:DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
      : base(options)
        {
        }

        public DbSet<RestaurantExtended.Models.Comanda>? Comanda { get; set; }
        public DbSet<RestaurantExtended.Models.Produs>? Product { get; set; }
        public DbSet<RestaurantExtended.Models.Image>? Image { get; set; }
        public DbSet<RestaurantExtended.Models.ComandaProdus>? comandaProdus { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Comanda>().HasKey(a => a.Id);
            builder.Entity<Produs>().HasKey(a => a.Id);

            builder.Entity<Image>().HasKey(a => a.Id);

            builder.Entity<Comanda>().HasMany(a=>a.produseComanda).WithOne().HasForeignKey(a => a.ComandaId);
            builder.Entity<Produs>().HasMany<ComandaProdus>().WithOne().HasForeignKey(a => a.ProductId);

            builder.Entity<Produs>().HasMany(a => a.images).WithOne();

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }





    }
}
