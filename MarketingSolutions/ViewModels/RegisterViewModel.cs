﻿using System.ComponentModel.DataAnnotations;

namespace MarketingSolutions.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
