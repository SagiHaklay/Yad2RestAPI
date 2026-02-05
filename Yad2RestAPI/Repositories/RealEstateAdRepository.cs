using Microsoft.EntityFrameworkCore;
using Yad2RestAPI.Data;
using Yad2RestAPI.Models.Account;
using Yad2RestAPI.Models.RealEstate;

namespace Yad2RestAPI.Repositories
{
    public class RealEstateAdRepository : IRealEstateRepository
    {
        private readonly Yad2Context _context;
        public RealEstateAdRepository(Yad2Context context)
        {
            _context = context;
        }
        public async Task CreateAdAsync(RealEstatePublishModel publishModel, int? publisherId)
        {
            var newAd = new RealEstateAdModel()
            {
                PropertyType = publishModel.PropertyType,
                City = publishModel.City,
                Street = publishModel.Street,
                HouseNumber = publishModel.HouseNumber,
                Floor = publishModel.Floor,
                TotalFloors = publishModel.TotalFloors,
                OnColumns = publishModel.OnColumns,
                PropertyStatus = publishModel.PropertyStatus,
                AirDirectionCount = publishModel.AirDirectionCount,
                View = publishModel.IsBackProperty? publishModel.View | RealEstateView.Back : publishModel.View,
                RoomCount = publishModel.RoomCount,
                ShowerCount = publishModel.ShowerCount,
                ParkingCount = publishModel.ParkingCount,
                BalconyCount = publishModel.BalconyCount,
                PropertyDescription = publishModel.PropertyDescription,
                PaymentCount = publishModel.PaymentCount,
                HouseCommiteePayment = publishModel.HouseCommiteePayment,
                PropertyTax = publishModel.PropertyTax,
                BuiltArea = publishModel.BuiltArea,
                GardenArea = publishModel.GardenArea,
                TotalArea = publishModel.TotalArea,
                Price = publishModel.Price,
                EntryDate = publishModel.EntryDate != null? DateTime.Parse(publishModel.EntryDate) : null,
                IsLongTerm = publishModel.IsLongTerm,
                ImageUrls = publishModel.ImageUrls,
                VideoUrls = publishModel.VideoUrls,
                ContactName = publishModel.ContactName,
                ContactPhone = publishModel.ContactPhone
            };
            newAd.PropertyFeatures = publishModel.PropertyFeatures.Aggregate(RealEstatePropertyFeature.None, (acc, f) => acc | f);
            if (publisherId != null)
            {
                var publisher = await _context.Profiles.SingleAsync(p => p.Id == publisherId);
                newAd.Publisher = publisher;
            }
            _context.Add(newAd);
            await _context.SaveChangesAsync();
        }

        public async Task<RealEstateAdDetails?> DeleteAdAsync(int id)
        {
            var ad = await _context.RealEstateAds.FindAsync(id);
            if (ad == null) return null;
            _context.RealEstateAds.Remove(ad);
            await _context.SaveChangesAsync();
            return ToAdDetails(ad);
        }

        public async Task<RealEstateAdDetails?> GetAdByIdAsync(int id, int? userId)
        {
            var ad = await _context.RealEstateAds.FindAsync(id);
            if (ad == null) return null;
            IEnumerable<int>? favorites = null;
            if (userId != null)
            {
                var user = await _context.Profiles.Include(p => p.FavoriteAds).SingleOrDefaultAsync(p => p.Id == userId);
                favorites = user?.FavoriteAds.Select(ad => ad.Id);
            }
            
            var result = ToAdDetails(ad);
            result.IsFavorite = favorites != null && favorites.Contains(ad.Id);
            return result;
        }

        public async Task<List<RealEstateAdSummary>?> GetAllAdsAsync(int? userId)
        {
            IEnumerable<int>? favorites = null;
            if (userId != null)
            {
                var user = await _context.Profiles.Include(p => p.FavoriteAds).SingleOrDefaultAsync(p => p.Id == userId);
                favorites = user?.FavoriteAds.Select(ad => ad.Id);
            }
            var ads = await _context.RealEstateAds.Select(ad => new RealEstateAdSummary()
            {
                Id = ad.Id,
                Street = ad.Street,
                City = ad.City,
                HouseNumber = ad.HouseNumber,
                ImageUrl = ad.ImageUrls.FirstOrDefault(),
                IsFavorite = favorites != null && favorites.Contains(ad.Id),
                Floor = ad.Floor,
                TotalArea = ad.TotalArea,
                Price = ad.Price,
                PropertyType = ad.PropertyType
            }).ToListAsync();

            return ads;
        }
        private RealEstateAdDetails ToAdDetails(RealEstateAdModel model)
        {
            return new RealEstateAdDetails()
            {
                Price = model.Price,
                PropertyType = model.PropertyType,
                City = model.City,
                Street = model.Street,
                HouseNumber= model.HouseNumber,
                Floor = model.Floor,
                TotalArea = model.TotalArea,
                TotalFloors = model.TotalFloors,
                OnColumns = model.OnColumns,
                PropertyStatus = model.PropertyStatus,
                AirDirectionCount = model.AirDirectionCount,
                View = model.View,
                RoomCount = model.RoomCount,
                ShowerCount = model.ShowerCount,
                ParkingCount = model.ParkingCount,
                BalconyCount = model.BalconyCount,
                PropertyFeatures = model.PropertyFeatures.ToList(),
                PropertyDescription = model.PropertyDescription,
                PaymentCount = model.PaymentCount,
                HouseCommiteePayment = model.HouseCommiteePayment,
                PropertyTax = model.PropertyTax,
                BuiltArea = model.BuiltArea,
                GardenArea = model.GardenArea,
                EntryDate = model.EntryDate.ToString(),
                IsLongTerm = model.IsLongTerm,
                ImageUrls = model.ImageUrls.ToList(),
                VideoUrls = model.VideoUrls.ToList(),
                ContactName = model.ContactName,
                ContactPhone = model.ContactPhone,
            };
        }
    }
}
