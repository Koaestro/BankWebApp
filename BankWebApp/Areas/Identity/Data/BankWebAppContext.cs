using BankWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BankWebApp.Models;

namespace BankWebApp.Data;

public class BankWebAppContext : IdentityDbContext<BankWebAppUser>
{
    public BankWebAppContext(DbContextOptions<BankWebAppContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<BankWebApp.Models.Transaction>? Transaction { get; set; }
}
