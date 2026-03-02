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
                EntryDate = publishModel.EntryDate,
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
                PropertyType = ad.PropertyType,
                RoomCount = ad.RoomCount
            }).ToListAsync();

            return ads;
        }

        public async Task<List<RealEstateAdSummary>?> SearchAdsAsync(RealEstateSearchFilters filters, int? userId)
        {
            IQueryable<RealEstateAdModel> adsQuery = _context.RealEstateAds;
            if (filters.Location != null)
            {
                adsQuery = adsQuery.Where(ad => ad.City == filters.Location || ad.Street == filters.Location);
            }
            
            
            if (filters.EntryDate != null)
            {
                adsQuery = adsQuery.Where(ad => ad.EntryDate <= filters.EntryDate);
            }
            if (filters.PriceIncluded) adsQuery = adsQuery.Where(ad => ad.Price != null);
            if (filters.ParkingIncluded) adsQuery = adsQuery.Where(ad => ad.ParkingCount > 0);
            if (filters.BalconyIncluded) adsQuery = adsQuery.Where(ad => ad.BalconyCount > 0);

            IEnumerable<RealEstateAdModel> ads = await adsQuery.ToListAsync();
            ads = ads.Where(ad => FilterRange(ad.Price, filters.MinPrice, filters.MaxPrice));
            ads = ads.Where(ad => FilterRange(ad.RoomCount, filters.MinRooms, filters.MaxRooms));


            ads = ads.Where(ad => FilterRange(ad.Floor, filters.MinFloor, filters.MaxFloor));
            ads = ads.Where(ad => FilterRange(ad.TotalArea, filters.MinArea, filters.MaxArea));
            ads = ads.Where(ad => FilterRange(ad.BuiltArea, filters.MinBuiltArea, filters.MaxBuiltArea));
            if (filters.ImageIncluded) ads = ads.Where(ad => ad.ImageUrls.Count > 0);
            if (filters.PropertyTypes != null && filters.PropertyTypes.Length > 0)
            {
                // property type is included in proprty type filter
                ads = ads.Where(ad => filters.PropertyTypes.Aggregate((acc, t) => acc | t).HasFlag(ad.PropertyType));
            }
            if (filters.Features != null && filters.Features.Length > 0)
            {
                // all feature filters are included in property
                ads = ads.Where(ad => ad.PropertyFeatures.HasFlag(filters.Features.Aggregate((acc, f) => acc | f)));
            }
            if (filters.PropertyStatuses != null && filters.PropertyStatuses.Length > 0)
            {
                // property status is included in property status filter
                ads = ads.Where(ad => filters.PropertyStatuses.Aggregate((acc, s) => acc | s).HasFlag(ad.PropertyStatus));
            }
            if (filters.FreeSearchQuery != null)
            {
                ads = ads.Where(ad => ad.PropertyDescription != null && ad.PropertyDescription.Contains(filters.FreeSearchQuery));
            }

            IEnumerable<int>? favorites = null;
            if (userId != null)
            {
                var user = await _context.Profiles.Include(p => p.FavoriteAds).SingleOrDefaultAsync(p => p.Id == userId);
                favorites = user?.FavoriteAds.Select(ad => ad.Id);
            }
            return ads.Select(ad => new RealEstateAdSummary()
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
                PropertyType = ad.PropertyType,
                RoomCount = ad.RoomCount
            }).ToList();
        }

        public async Task<RealEstateAdModel?> UpdateAdAsync(int id, RealEstatePublishModel publishModel, int? publisherId)
        {
            var ad = await _context.RealEstateAds.FindAsync(id);
            if (ad == null) return null;
            ad.PropertyType = publishModel.PropertyType;
            ad.City = publishModel.City;
            ad.Street = publishModel.Street;
            ad.HouseNumber = publishModel.HouseNumber;
            ad.Floor = publishModel.Floor;
            ad.TotalFloors = publishModel.TotalFloors;
            ad.OnColumns = publishModel.OnColumns;
            ad.PropertyStatus = publishModel.PropertyStatus;
            ad.AirDirectionCount = publishModel.AirDirectionCount;
            ad.View = publishModel.IsBackProperty ? publishModel.View | RealEstateView.Back : publishModel.View;
            ad.RoomCount = publishModel.RoomCount;
            ad.ShowerCount = publishModel.ShowerCount;
            ad.ParkingCount = publishModel.ParkingCount;
            ad.BalconyCount = publishModel.BalconyCount;
            ad.PropertyDescription = publishModel.PropertyDescription;
            ad.PaymentCount = publishModel.PaymentCount;
            ad.HouseCommiteePayment = publishModel.HouseCommiteePayment;
            ad.PropertyTax = publishModel.PropertyTax;
            ad.BuiltArea = publishModel.BuiltArea;
            ad.GardenArea = publishModel.GardenArea;
            ad.TotalArea = publishModel.TotalArea;
            ad.Price = publishModel.Price;
            ad.EntryDate = publishModel.EntryDate;
            ad.IsLongTerm = publishModel.IsLongTerm;
            ad.ImageUrls = publishModel.ImageUrls;
            ad.VideoUrls = publishModel.VideoUrls;
            ad.ContactName = publishModel.ContactName;
            ad.ContactPhone = publishModel.ContactPhone;
            ad.PropertyFeatures = publishModel.PropertyFeatures.Aggregate(RealEstatePropertyFeature.None, (acc, f) => acc | f);
            await _context.SaveChangesAsync();
            return ad;
        }
        
        private static bool FilterRange(double? value, double? min, double? max)
        {
            if (min != null)
            {
                if (value == null) return false;
                if (max == null)
                {
                    return value == min;
                }
                else
                {
                    return value >= min && value <= max;
                }
            }
            return true;
        }
        private static RealEstateAdDetails ToAdDetails(RealEstateAdModel model)
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
                View = model.View & (~RealEstateView.Back),
                IsBackProperty = model.View.HasFlag(RealEstateView.Back),
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
