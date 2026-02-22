using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Yad2RestAPI.Models.RealEstate
{
    public class RealEstateSearchFilters
    {
        [FromQuery]
        public string? Location { get; set; }
        [FromQuery]
        public RealEstatePropertyType[]? PropertyTypes { get; set; }
        [FromQuery]
        [Range(0, float.MaxValue)]
        public float? MinPrice { get; set; }
        [FromQuery]
        [Range(0, float.MaxValue)]
        public float? MaxPrice { get; set; }
        [FromQuery]
        [Range(1, 12.5)]
        public float? MinRooms { get; set; }
        [FromQuery]
        [Range(1, 12.5)]
        public float? MaxRooms { get; set; }
        [FromQuery]
        public bool ImageIncluded { get; set; }
        [FromQuery]
        public bool PriceIncluded { get; set; }
        [FromQuery]
        public bool IsBroker { get; set; }
        [FromQuery]
        public bool IsContractor { get; set; }
        [FromQuery]
        public RealEstatePropertyFeature[]? Features { get; set; }
        [FromQuery]
        public RealEstatePropertyStatus[]? PropertyStatuses { get; set; }
        [FromQuery]
        [Range(-10, 100)]
        public int? MinFloor { get; set; }
        [FromQuery]
        [Range(-10, 100)]
        public int? MaxFloor { get; set; }
        [FromQuery]
        [Range(0, double.MaxValue)]
        public double? MinArea { get; set; }
        [FromQuery]
        [Range(0, double.MaxValue)]
        public double? MaxArea { get; set; }
        [FromQuery]
        [Range(0, double.MaxValue)]
        public double? MinBuiltArea { get; set; }
        [FromQuery]
        [Range(0, double.MaxValue)]
        public double? MaxBuiltArea { get; set; }
        [FromQuery]
        public DateTime? EntryDate { get; set; }
        [FromQuery]
        public string? FreeSearchQuery { get; set; }

    }
}
