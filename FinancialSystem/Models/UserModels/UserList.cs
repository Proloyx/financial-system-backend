using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSystem.Models.UserModels
{
    public class UserList
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
    }
}