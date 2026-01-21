namespace Yad2RestAPI.Models.RealEstate
{
    [Flags]
    public enum RealEstateView
    {
        None = 0,
        Sea = 1,
        Park = 2,
        City = 4,
        Back = 8
    }
}
