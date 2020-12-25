namespace ProjectApi.Features.Identity
{
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        public string GenerateJwtToken(string userId, string userName, string secret);

        public Task<UserDetailsResponse> GetUserDetails(string userId);

        public Task SaveUserDetails(string userId, string email, string phoneNumber, string profilePicture);
    }
}
