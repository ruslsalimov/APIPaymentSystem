using PaymentSystemAPI.Data;
using PaymentSystemAPI.Models.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSystemAPI.Models
{
    public class EFRepository : IRepository
    {
        private ApplicationContext db;

        public EFRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IQueryable<PaymentInfo> PaymentInfo => db.PaymentInfo;

        public async Task SaveCardInfoAsync(CardInfo info)
        {
            db.CardInfo.Add(info);
            await db.SaveChangesAsync();
        }

        public async Task SavePaymentInfoAsync(PaymentInfo info)
        {
            db.PaymentInfo.Add(info);
            await db.SaveChangesAsync();
        }

        public async Task SaveReceiptAsync(Receipt receipt)
        {
            db.Receipts.Add(receipt);
            await db.SaveChangesAsync();
        }
    }
}