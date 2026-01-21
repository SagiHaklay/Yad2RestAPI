namespace Yad2RestAPI.Models.RealEstate
{
    public class RealEstateAdModel
    {
        public int Id { get; set; }
        public RealEstatePropertyType PropertyType { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int HouseNumber { get; set; }
        public int Floor { get; set; }
        public int TotalFloors { get; set; }
        public bool OnColumns { get; set; }
        public RealEstatePropertyStatus PropertyStatus { get; set; }
        public int AirDirectionCount { get; set; }
        public RealEstateView View { get; set; }
        public float RoomCount { get; set; }
        public int ShowerCount { get; set; } = 1;
        public int ParkingCount { get; set; }
        public int BalconyCount { get; set; }
        public RealEstatePropertyFeature PropertyFeatures { get; set; }
        public string? PropertyDescription { get; set; }
        public int? PaymentCount { get; set; }
        public float? HouseCommiteePayment { get; set; }
        public float? PropertyTax { get; set; }
        public double? BuiltArea { get; set; }
        public double? GardenArea { get; set; }
        public double TotalArea { get; set; }
        public float? Price { get; set; }
        public DateTime? EntryDate { get; set; }
        public bool IsLongTerm { get; set; }
        public IList<string> ImageUrls { get; set; } = new List<string>();
        public IList<string> VideoUrls { get; set; } = new List<string>();
        public string ContactName { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public ProfileModel Publisher { get; set; }
        public int PublisherId { get; set; }
    }
}
