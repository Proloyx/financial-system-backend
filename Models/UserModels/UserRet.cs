using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace FinancialSystem.Models.UserModels
{
    public class UserRet
    { 
        public string? id {get; set;}
        public string? user_name {get; set;}
        public string? profesion {get; set;}
    }
}