using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yad2RestAPI.Models;
using Yad2RestAPI.Models.RealEstate;
using Yad2RestAPI.Repositories;

namespace Yad2RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateAdController : ControllerBase
    {
        private readonly IRealEstateRepository _realEstateRepository;
        public RealEstateAdController(IRealEstateRepository realEstateRepository)
        {
            _realEstateRepository = realEstateRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAds([FromQuery] int? userId)
        {
            var allAds = await _realEstateRepository.GetAllAdsAsync(userId);
            return Ok(allAds);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdById([FromRoute] int id, [FromQuery] int? userId)
        {
            var ad = await _realEstateRepository.GetAdByIdAsync(id, userId);
            if (ad == null)
            {
                return NotFound();
            }
            return Ok(ad);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAd([FromBody] RealEstatePublishModel publishModel)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (!int.TryParse(userId, out int publisherId))
            //{
            //    return BadRequest();
            //}
            await _realEstateRepository.CreateAdAsync(publishModel, publishModel.PublisherId);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAd([FromRoute] int id)
        {
            var ad = await _realEstateRepository.DeleteAdAsync(id);
            if (ad == null)
            {
                return NotFound();
            }
            return Ok(ad);
        }
    }
}
