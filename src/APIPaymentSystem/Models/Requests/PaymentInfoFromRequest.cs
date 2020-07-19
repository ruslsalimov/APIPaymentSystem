﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPaymentSystem.Models.Requests
{
    public class PaymentInfoFromRequest
    {
        [Required(ErrorMessage = "Missing payment amount")]
        [Column(TypeName = "decimal(18,5)")]
        public decimal Amount { get; set; }             //  Сумма платежа

        [Required(ErrorMessage = "Missing payment description")]
        public string Description { get; set; }         //  Назначение
    }
}