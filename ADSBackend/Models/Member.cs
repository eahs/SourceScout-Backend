﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADSBackend.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "First name is required")]  // Max 32 characters, min 1 character
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Last name is required")]  // Max 32 characters, min 1 character
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress, ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }

        [JsonIgnore]
        public string PasswordSalt { get; set; }
    }
}
