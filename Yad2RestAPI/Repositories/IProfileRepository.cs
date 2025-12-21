using Yad2RestAPI.Models;

namespace Yad2RestAPI.Repositories
{
    public interface IProfileRepository
    {
        Task<ProfileModel?> RegisterAsync(RegisterRequestModel registerRequest);
        Task<LoginResponseModel?> LoginAsync(LoginRequestModel loginRequest);
        Task<ProfileModel?> GetProfileByIdAsync(int id);
        Task<ProfileModel?> UpdateProfileAsync(int id, ProfileUpdateModel profileUpdate);
    }
}
