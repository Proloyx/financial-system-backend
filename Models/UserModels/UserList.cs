using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace FinancialSystem.Models.UserModels
{
    public class UserList
    {
        public string? id {get; set;}
        public string? user_name {get; set;}
        public string? profession {get; set;}
    }
}