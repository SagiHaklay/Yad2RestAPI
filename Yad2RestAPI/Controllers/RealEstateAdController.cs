using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yad2RestAPI.Models;

namespace Yad2RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateAdController : ControllerBase
    {
        [HttpGet]
        public async Task GetAllAds()
        {
            
        }
    }
}
