namespace ProjectApi.Features.Identity
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;

    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using ProjectApi.Infrastructure.Extensions;

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IIdentityService _identityService;
        private readonly ApplicationSettings _applicationSettings;

        public IdentityController(
            UserManager<User> userManager,
            IIdentityService identityService,
            IOptions<ApplicationSettings> applicationSettings)
        {
            _userManager = userManager;
            _identityService = identityService;
            _applicationSettings = applicationSettings.Value;
        }

        [Route(nameof(Register))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Register(RegisterUserRequest model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.Username,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(result.Errors);
        }

        [Route(nameof(Login))]
        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginUserRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return this.Unauthorized();
            }

            var isPasswordValid = await this._userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordValid)
            {
                return this.Unauthorized();
            }

            // generate token that is valid for 7 days
            var encryptedToken = _identityService.GenerateJwtToken(
                user.Id,
                user.UserName,
                _applicationSettings.Secret);

            return encryptedToken;
        }

        [Route(nameof(Details))]
        [HttpGet]
        public async Task<ActionResult<UserDetailsResponse>> Details()
            => await _identityService.GetUserDetails(User.GetId());

        [Route(nameof(Details))]
        [HttpPost]
        public async Task<ActionResult> Details(UserDetailsResponse data)
        {
            await _identityService.SaveUserDetails(User.GetId(), data.Email, data.PhoneNumber, data.ProfilePicture);
            return Ok();
        }
    }
}
