using Yad2RestAPI.Models.Account;
using Yad2RestAPI.Models.RealEstate;

namespace Yad2RestAPI.Repositories
{
    public interface IRealEstateRepository
    {
        Task<List<RealEstateAdSummary>?> GetAllAdsAsync(int? userId);
        Task<RealEstateAdDetails?> GetAdByIdAsync(int id, int? userId);
        Task CreateAdAsync(RealEstatePublishModel publishModel, int? publisherId);
        Task<RealEstateAdDetails?> DeleteAdAsync(int id);
        Task<List<RealEstateAdSummary>?> SearchAdsAsync(RealEstateSearchFilters filters, int? userId);
        Task<RealEstateAdModel?> UpdateAdAsync(int id, RealEstatePublishModel publishModel, int? publisherId);
    }
}
