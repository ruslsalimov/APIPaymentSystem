using APIPaymentSystem.Data;
using APIPaymentSystem.Models.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace APIPaymentSystem.Models
{
    public class EFRepository : IRepository
    {
        private AppIdentityDbContext db;

        public EFRepository(AppIdentityDbContext context)
        {
            this.db = context;
        }

        public IQueryable<PaymentInfo> Payments => db.Payments;

        public IQueryable<CardInfo> Cards => db.Cards;

        public IQueryable<Receipt> Receipts => db.Receipts;

        public async Task SaveCardInfoAsync(CardInfo info)
        {
            db.Cards.Add(info);
            await db.SaveChangesAsync();
        }

        public async Task SavePaymentInfoAsync(PaymentInfo info)
        {
            db.Payments.Add(info);
            await db.SaveChangesAsync();
        }

        public async Task SaveReceiptAsync(Receipt receipt)
        {
            db.Receipts.Add(receipt);
            await db.SaveChangesAsync();
        }
    }
}