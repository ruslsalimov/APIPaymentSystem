﻿using System.Collections.Generic;

namespace APIPaymentSystem.Models
{
    public class Error
    {
        public string Status { get; set; }
        public string Title { get; set; }
    }

    public class ListErrors
    {
        public List<Error> Errors { get; set; }
    }
}