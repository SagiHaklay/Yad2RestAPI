using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yad2RestAPI.Models.Account;
using Yad2RestAPI.Models.RealEstate;
using Yad2RestAPI.Repositories;

namespace Yad2RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var allProfiles = await _profileRepository.GetAllProfilesAsync();
            return Ok(allProfiles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById([FromRoute] int id)
        {
            var profile = await _profileRepository.GetProfileByIdAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            var result = new ProfileDetails()
            {
                Email = profile.Email,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Phone = profile.Phone,
                DateOfBirth = profile.DateOfBirth?.ToString(),
                City = profile.City,
                Street = profile.Street,
                HouseNumber = profile.HouseNumber,
                ProfileImageUrl = profile.ProfileImageUrl,
                RealEstateAds = profile.RealEstateAds.Select(ad => new RealEstateAdSummary()
                {
                    Id = ad.Id,
                    City = ad.City,
                    Street = ad.Street,
                    HouseNumber = ad.HouseNumber,
                    Floor = ad.Floor,
                    PropertyType = ad.PropertyType,
                    ImageUrl = ad.ImageUrls.ElementAtOrDefault(0),
                    Price = ad.Price,
                    TotalArea = ad.TotalArea
                }).ToList(),
                FavoriteAds = profile.FavoriteAds.Select(ad => new RealEstateAdSummary()
                {
                    Id = ad.Id,
                    City = ad.City,
                    Street = ad.Street,
                    HouseNumber = ad.HouseNumber,
                    Floor = ad.Floor,
                    PropertyType = ad.PropertyType,
                    ImageUrl = ad.ImageUrls.ElementAtOrDefault(0),
                    Price = ad.Price,
                    TotalArea = ad.TotalArea
                }).ToList()
            };
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] int id, [FromBody] ProfileUpdateModel profileUpdate)
        {
            var profile = await _profileRepository.UpdateProfileAsync(id, profileUpdate);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }
        [HttpPost("{profileId}/favorite/{adId}")]
        public async Task<IActionResult> AddToFavorites([FromRoute] int profileId, [FromRoute] int adId)
        {
            var isSuccess = await _profileRepository.AddToFavoritesAsync(profileId, adId);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{profileId}/favorite/{adId}")]
        public async Task<IActionResult> RemoveFromFavorites([FromRoute] int profileId, [FromRoute] int adId)
        {
            var isSuccess = await _profileRepository.RemoveFromFavoritesAsync(profileId, adId);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
