using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSystem.Models.QueryParams
{
    public class SearchParams:QueryParams
    {
        [Required]
        public string? search_text {get; set;}
        public string? limit {get; set;}
        public string? search_type {get; set;}
        public string? realtime_start {get; set;}
        public string? realtime_end {get; set;}
        public string? offset {get; set;}
        public string? order_by {get; set;}
        public string? sort_order {get; set;}
        public string? filter_variable {get; set;}
        public string? filter_value {get; set;}
        public string? tag_names {get; set;}
        public string? exclude_tag_names {get; set;}
    }
}