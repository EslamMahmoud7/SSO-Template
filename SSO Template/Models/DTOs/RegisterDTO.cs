﻿using System.ComponentModel.DataAnnotations;

namespace SSO_Template.Models.DTOs
{
   public class RegisterDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
