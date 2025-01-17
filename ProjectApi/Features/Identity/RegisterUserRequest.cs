﻿namespace ProjectApi.Features.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
