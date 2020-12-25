namespace ProjectApi.Features.Identity
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using ProjectApi.Data;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class IdentityService : IIdentityService
    {
        private readonly ProjectDbContext _db;

        public IdentityService(ProjectDbContext db)
        {
            _db = db;
        }

        public string GenerateJwtToken(string userId, string userName, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return JsonExtensions.SerializeToJson(tokenHandler.WriteToken(token));
        }

        public Task<UserDetailsResponse> GetUserDetails(string userId) => _db.Users
                .Select(u => new UserDetailsResponse
                {
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    ProfilePicture = u.ProfilePicture,
                })
                .FirstOrDefaultAsync();

        public async Task SaveUserDetails(string userId, string email, string phoneNumber, string profilePicture)
        {
            var user = await _db.Users.FirstAsync(x => x.Id == userId);
            user.Email = email;
            user.PhoneNumber = phoneNumber;
            user.ProfilePicture = profilePicture;

            await _db.SaveChangesAsync();
        }
    }
}
