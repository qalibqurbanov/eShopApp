﻿using System.ComponentModel.DataAnnotations;

namespace eShopApp.WebUI.Models.IdentityModels.Login
{
    /// <summary>
    /// Qeydiyyat meqsedile userin formu doldurub post etdiyi datalari temsil edir.
    /// </summary>
    public class SignUpModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 12)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 12)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))] /* 'RePassword'-a gelmiw data 'Password' ile eyni olsun/olmalidir */
        public string RePassword { get; set; }
    }
}