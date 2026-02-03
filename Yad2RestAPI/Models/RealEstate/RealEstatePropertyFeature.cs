namespace Yad2RestAPI.Models.RealEstate
{
    [Flags]
    public enum RealEstatePropertyFeature
    {
        None = 0,
        Accessible = 0b0000_0000_0000_0001, // 0x0001
        AirCondition = 0b0000_0000_0000_0010, // 0x0002
        Bars = 0b0000_0000_0000_0100, // 0x0004
        Heater = 0b0000_0000_0000_1000, // 0x0008
        Elevator = 0b0000_0000_0001_0000, // 0x0010 16
        Partners = 0b0000_0000_0010_0000, // 0x0020 32
        Furniture = 0b0000_0000_0100_0000, // 0x0040 64
        HousingUnit = 0b0000_0000_1000_0000, // 0x0080 128
        KosherKitchen = 0b0000_0001_0000_0000, // 0x0100 256
        PetsAllowed = 0b0000_0010_0000_0000, // 0x0200 512
        Renovated = 0b0000_0100_0000_0000, // 0x0400 1024
        Shelter = 0b0000_1000_0000_0000, // 0x0800 2048
        RavBariahDoors = 0b0001_0000_0000_0000, // 0x1000 4096
        TornadoAC = 0b0010_0000_0000_0000, // 0x2000 8192
        Storage = 0b0100_0000_0000_0000, // 0x4000 16384
        Exclusive = 0b1000_0000_0000_0000 // 0x8000 32768
    }

    public static class Extensions
    {
        public static List<RealEstatePropertyFeature> ToList(this RealEstatePropertyFeature features)
        {
            var list = new List<RealEstatePropertyFeature>();
            var values = Enum.GetValues(typeof(RealEstatePropertyFeature));
            foreach (RealEstatePropertyFeature value in values)
            {
                if (features.HasFlag(value))
                {
                    list.Add(value);
                    features &= ~value;
                    if (features == RealEstatePropertyFeature.None) return list;
                }
            }
            return list;
        }
    }
}
