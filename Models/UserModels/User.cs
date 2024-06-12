using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace FinancialSystem.Models.UserModels
{
    [FirestoreData]
    public class User
    { 
        [FirestoreDocumentId]
        public string? id {get; set;}
        [FirestoreProperty]
        public string? user_name {get; set;}
        [FirestoreProperty]
        public string? email {get; set;}
        [FirestoreProperty]
        public string? password {get; set;}
        [FirestoreProperty]
        public string? profesion {get; set;}
    }
}