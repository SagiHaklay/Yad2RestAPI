namespace Yad2RestAPI.Models.RealEstate
{
    public class RealEstateSearchFilters
    {
        public string? Location { get; set; }
        public RealEstatePropertyType[] PropertyTypes { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinRooms { get; set; }
        public float? MaxRooms { get; set; }
        public bool ImageIncluded { get; set; }
        public bool PriceIncluded { get; set; }
        public bool IsBroker { get; set; }
        public bool IsContractor { get; set; }
        public RealEstatePropertyFeature[] Features { get; set; }
        public RealEstatePropertyStatus[] PropertyStatuses { get; set; }
        public int? MinFloor { get; set; }
        public int? MaxFloor { get; set; }
        public double? MinArea { get; set; }
        public double? MaxArea { get; set; }
        public double? MinBuiltArea { get; set; }
        public double? MaxBuiltArea { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? FreeSearchQuery { get; set; }

    }
}
