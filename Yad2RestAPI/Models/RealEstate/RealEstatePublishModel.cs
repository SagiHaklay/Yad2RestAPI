using System.ComponentModel.DataAnnotations;

namespace Yad2RestAPI.Models.RealEstate
{
    public class RealEstatePublishModel
    {
        [Required]
        public RealEstatePropertyType PropertyType { get; set; }
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        [Range(0, int.MaxValue)]
        public int HouseNumber { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int TotalFloors { get; set; }
        [Required]
        public bool OnColumns { get; set; }
        [Required]
        public RealEstatePropertyStatus PropertyStatus { get; set; }
        [Required]
        [Range(0, 4)]
        public int AirDirectionCount { get; set; }
        [Required]
        public RealEstateView View { get; set; }
        [Required]
        [Range(1, 12.5)]
        public float RoomCount { get; set; }
        [Required]
        [Range(1, 4)]
        public int ShowerCount { get; set; } = 1;
        [Required]
        [Range(0, 3)]
        public int ParkingCount { get; set; }
        [Required]
        [Range(0, 3)]
        public int BalconyCount { get; set; }
        public List<RealEstatePropertyFeature> PropertyFeatures { get; } = new List<RealEstatePropertyFeature>();
        public string? PropertyDescription { get; set; }
        [Range(1, 12)]
        public int? PaymentCount { get; set; }
        [Range(0, float.MaxValue)]
        public float? HouseCommiteePayment { get; set; }
        [Range(0, float.MaxValue)]
        public float? PropertyTax { get; set; }
        [Range(0, double.MaxValue)]
        public double? BuiltArea { get; set; }
        [Range(0, double.MaxValue)]
        public double? GardenArea { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double TotalArea { get; set; }
        [Range(0, float.MaxValue)]
        public float? Price { get; set; }
        public string? EntryDate { get; set; }
        public bool IsLongTerm { get; set; }
        [Required]
        public string ContactName { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string ContactPhone { get; set; } = string.Empty;
        public bool IsBackProperty { get; set; }
        public IList<string> ImageUrls { get; } = new List<string>();
        public IList<string> VideoUrls { get; } = new List<string>();
    }
}
