﻿using System;
using System.ComponentModel.DataAnnotations;

namespace APIPaymentSystem.Models.Responses
{
    /// <summary>
    /// Данные карты 
    /// </summary>
    public class CardInfo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(19)]
        public string CardNumber { get; set; }          //  Номер карты

        [Required]
        [StringLength(4, MinimumLength = 3)]
        public string VerificationNumber { get; set; }  //  CVV/CVC

        [Required]
        public DateTime CardDate { get; set; }          //  Срок действия карты

        [Required]
        public PaymentInfo PaymentInfo { get; set; }
    }
}