﻿using PaymentSystemAPI.Models.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSystemAPI.Models
{
    public interface IRepository
    {
        IQueryable<PaymentInfo> PaymentInfo { get; }
        Task SavePaymentInfoAsync(PaymentInfo info);
        Task SaveCardInfoAsync(CardInfo info);
        Task SaveReceiptAsync(Receipt receipt);
    }
}