using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSystem.Models.UserModels
{
    public class UserRegister
    {
        [Required]
        public string? user_name {get; set;}
        [Required]
        [EmailAddress]
        public string? email {get; set;}
        [Required]
        public string? password {get; set;}
        public string? profesion {get; set;}
    }
}