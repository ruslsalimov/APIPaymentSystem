﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPaymentSystem.Models.Responses
{
    /// <summary>
    /// Чек
    /// </summary>
    public class Receipt
    {
        [Key]
        [Required]
        public Guid SessionId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,5)")]
        public decimal Amount { get; set; }         //  Сумма платежа

        [Required]
        public string Description { get; set; }     //  Описание

        [Required]
        public DateTime ArrivalTime { get; set; }   //  Время покупки

        [Required]
        [MaxLength(19)]
        public string CardNumber { get; set; }      //  Карта, с которой была оплачена покупка 

        [Required]
        public DateTime TimePayment { get; set; }   //  Время оплаты
    }
}