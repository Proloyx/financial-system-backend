using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSystem.Models.QueryParams
{
    public class ObservationParams:QueryParams
    {
        [Required]
        public string? series_id {get; set;}
        public string? limit {get; set;}
        public string? realtime_start {get; set;}
        public string? realtime_end {get; set;}
        public string? offset {get; set;}
        public string? sort_order {get; set;}
        public string? observation_start {get; set;}
        public string? observation_end {get; set;}
        public string? units {get; set;}
        public string? frequency {get; set;}
        public string? aggregation_method {get; set;}
        public string? output_type {get; set;}
        public string? vintage_dates {get; set;}
    }
}