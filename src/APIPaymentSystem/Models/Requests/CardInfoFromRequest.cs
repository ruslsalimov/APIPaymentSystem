﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentSystemAPI.Models.Requests
{
    public class CardInfoFromRequest
    {
        [Required(ErrorMessage = "Missing the card number")]
        [MaxLength(19)]
        public string CardNumber { get; set; }                  //  Номер карты

        [Required(ErrorMessage = "Missing the card verification code")]
        [Range(100, 9999, ErrorMessage = "Invalid card verification code")]
        public ushort VerificationNumber { get; set; }          //  CVV/CVC

        [Required(ErrorMessage = "Missing the validity of the card")]
        [MaxLength(5, ErrorMessage = "Max Length = 5. Example: 02/23 - February 2023")]
        public string CardDate { get; set; }                    //  Срок действия карты 

        [Required(ErrorMessage = "Missing a sessionId")]
        public string SessionId { get; set; }
    }
}