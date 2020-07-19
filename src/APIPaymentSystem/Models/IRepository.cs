﻿using APIPaymentSystem.Models.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace APIPaymentSystem.Models
{
    public interface IRepository
    {
        IQueryable<PaymentInfo> Payments { get; }
        IQueryable<CardInfo> Cards { get; }
        IQueryable<Receipt> Receipts { get; }
        Task SavePaymentInfoAsync(PaymentInfo info);
        Task SaveCardInfoAsync(CardInfo info);
        Task SaveReceiptAsync(Receipt receipt);
    }
}