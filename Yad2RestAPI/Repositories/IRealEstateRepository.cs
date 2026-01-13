using Yad2RestAPI.Models.RealEstate;

namespace Yad2RestAPI.Repositories
{
    public interface IRealEstateRepository
    {
        Task<List<RealEstateAdSummary>?> GetAllAdsAsync();
        Task<RealEstateAdDetails?> GetAdByIdAsync(int id);
        Task<RealEstateAdDetails?> CreateAdAsync(RealEstatePublishModel publishModel);
        Task<RealEstateAdDetails?> DeleteAdAsync(int id);
    }
}
