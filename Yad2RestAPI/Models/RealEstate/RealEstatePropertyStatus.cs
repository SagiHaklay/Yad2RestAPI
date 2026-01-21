namespace Yad2RestAPI.Models.RealEstate
{
    [Flags]
    public enum RealEstatePropertyStatus
    {
        None = 0,
        BrandNew = 1,
        New = 2,
        Renovated = 4,
        Preserved = 8,
        RenovationRequired = 16
    }
}
