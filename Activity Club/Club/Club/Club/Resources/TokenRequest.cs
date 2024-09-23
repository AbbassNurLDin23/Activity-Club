﻿using System.ComponentModel.DataAnnotations;

namespace Club.Resources
{
    public class TokenRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}