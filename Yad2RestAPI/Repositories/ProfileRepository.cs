using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Yad2RestAPI.Data;
using Yad2RestAPI.Models;

namespace Yad2RestAPI.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly Yad2Context _context;
        public ProfileRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, Yad2Context context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _config = configuration;
        }
        private string NewToken(string username)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"] ?? ""));
            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<ProfileModel?> GetProfileByIdAsync(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            return profile;
        }

        public async Task<LoginResponseModel?> LoginAsync(LoginRequestModel loginRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return null;
            }
            var token = NewToken(loginRequest.Email);
            return new LoginResponseModel()
            {
                Token = token,
                UserId = user.ProfileId
            };
        }

        public async Task<ProfileModel?> RegisterAsync(RegisterRequestModel registerRequest)
        {
            ProfileModel profile = new ProfileModel()
            {
                Email = registerRequest.Email,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Phone = registerRequest.Phone,
                AllowAds = registerRequest.AllowAds
            };
            _context.Add(profile);
            await _context.SaveChangesAsync();
            AppUser user = new()
            {
                UserName = registerRequest.Email,
                Email = registerRequest.Email,
                ProfileId = profile.Id
            };
            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
            {
                _context.Remove(profile);
                await _context.SaveChangesAsync();
                return null;
            }
            return profile;
        }

        public Task<ProfileModel?> UpdateProfileAsync(int id, ProfileUpdateModel profileUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
