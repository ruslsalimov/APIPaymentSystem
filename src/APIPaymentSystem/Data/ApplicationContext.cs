using Microsoft.EntityFrameworkCore;
using PaymentSystemAPI.Models.Responses;

namespace PaymentSystemAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<PaymentInfo> PaymentInfo { get; set; }

        public DbSet<CardInfo> CardInfo { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base((DbContextOptions)options) { }
    }
}
