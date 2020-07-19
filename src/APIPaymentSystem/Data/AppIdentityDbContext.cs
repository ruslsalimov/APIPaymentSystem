using APIPaymentSystem.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using APIPaymentSystem.Models.Responses;

namespace APIPaymentSystem.Data
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<PaymentInfo> Payments { get; set; }

        public DbSet<CardInfo> Cards { get; set; }

        public DbSet<Receipt> Receipts { get; set; }
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
