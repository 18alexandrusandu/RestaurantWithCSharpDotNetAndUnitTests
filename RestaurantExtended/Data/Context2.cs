using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantExtended.Models;

namespace RestaurantExtended.Data;

public class Context2 : IdentityDbContext<User>
{
    public Context2(DbContextOptions<Context2> options)
        : base(options)
    {
    }
     public DbSet<User> users { get; set; }
    public DbSet<IdentityRole> roles { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
