﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentSystemAPI.Models.Responses
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
        [Range(0, 9999)]
        public ushort VerificationNumber { get; set; }  //  CVV/CVC

        [Required]
        public DateTime CardDate { get; set; }          //  Срок действия карты

        [Required]
        public PaymentInfo PaymentInfo { get; set; }
    }
}