namespace Yad2RestAPI.Models.RealEstate
{
    public class RealEstateAdSummary
    {
        public int Id { get; set; }
        public RealEstatePropertyType PropertyType { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int HouseNumber { get; set; }
        public int Floor { get; set; }
        public double TotalArea { get; set; }
        public float? Price { get; set; }
        public bool IsFavorite { get; set; }
        public string? ImageUrl { get; set; }
        public float? RoomCount { get; set; }
    }
}
